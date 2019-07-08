using UnityEngine;
using System.Collections;

public class DynamicObject : MonoBehaviour
{
    public bool skipSoldierDamages = false;

    public float MaxHealth;

    public float damageCoef = 1;

    private float currentHealth;

    public DieObject[] DieObjects;

    public bool copyVelocity = false;

    public LogicTrigger gettingDamageArea;

    public DynamicObject[] dynamicObjectsToKillOnDie;

    public bool invulnerable = false;

    private bool isDead = false;
    //public DieObject DieObjects;

    bool needsToDie = false;

    public float CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    [HideInInspector]
    public LogicFlag flag_ObjectDestroyed = null;

    private DamageInfluence dmgInfluenceHandler;

    MultiDamageController multiDmgCtrl = new MultiDamageController();

    // Use this for initialization
    void Start()
    {
        CurrentHealth = MaxHealth;

        dmgInfluenceHandler = GetComponent<DamageInfluence>();
    }

    // Update is called once per frame
    void Update()
    {
        if (needsToDie)
        {
            Die();
        }
    }

    public void ApplyDamage(DamageInfo dmg)
    {
        if (enabled == false)
            return;

        if (invulnerable)
            return;

        if (gettingDamageArea)
        {
            if (gettingDamageArea.isEnabled)
            {
                if (dmg.damageSource == null)
                    return;

                if (!gettingDamageArea.IsGameObjectIn(dmg.damageSource))
                    return;
            }
        }

        if (!isDead)
        {
            if (skipSoldierDamages)
            {
                if (dmg.damageSource != null)
                {
                    string tg = dmg.damageSource.transform.root.tag.ToLower();

                    if (tg == GeneralStats.allyTagName_ToLower || tg == GeneralStats.enemyTagName_ToLower)
                        return;
                }
            }

            if (multiDmgCtrl.IsDamageAppliedBefore(dmg))
                return;

            multiDmgCtrl.AddDamage(dmg);

            ChangeHealth(-dmg.damageAmount * damageCoef);
        }
    }

    public void ChangeHealth(float amount)
    {
        CurrentHealth += amount;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Die();
        }
        else
        {
            if (dmgInfluenceHandler != null)
            {
                dmgInfluenceHandler.DamageOccur();
            }
        }
    }

    public void Die()
    {
        isDead = true;
        foreach (DieObject obj in DieObjects)
        {
            //Should check for using obj in this way and velocity
            GameObject gObj = Instantiate(obj.Object, transform.position + obj.Trans.position, transform.rotation) as GameObject;
            //gObj.transform.localPosition = obj.Trans.position;
            //gObj.transform.localRotation = obj.Trans.rotation;

            if (gameObject.rigidbody != null && copyVelocity)
            {
                if (gObj.rigidbody != null)
                {
                    gObj.rigidbody.velocity = gameObject.rigidbody.velocity;
                }
            }

        }

        foreach (DynamicObject dynObj in dynamicObjectsToKillOnDie)
        {
            if (dynObj)
                dynObj.SetNeedsToDie();
        }

        Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        if (flag_ObjectDestroyed != null)
        {
            flag_ObjectDestroyed.SetStatus(LogicFlagStatus.Active);
        }
    }

    void SetNeedsToDie()
    {
        needsToDie = true;
    }

    public void SetInvulnerable(bool isInvulnerable)
    {
        invulnerable = isInvulnerable;
    }

    public bool IsInvulnerable()
    {
        return invulnerable;
    }
}