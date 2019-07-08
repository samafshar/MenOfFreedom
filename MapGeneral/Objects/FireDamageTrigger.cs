using UnityEngine;
using System.Collections;

public class FireDamageTrigger : MonoBehaviour
{
    public Transform damageOccurPosition;

    public int damageAmountPercent;

    public float timeToApplyDamage = 1f;

    //

    float timer = 0f;

    DamageInfo dmgInfo = new DamageInfo();

    //

    void Start()
    {
        MakeDamagePack();

        timer = timeToApplyDamage;
    }

    void OnTriggerEnter(Collider _col)
    {
        Collider col = _col;

        if (col == null)
            return;

        string colTag = col.transform.root.tag.ToLower();

        if (colTag == GeneralStats.playerTagName_ToLower)
        {
            DoDamage();
        }
    }

    void OnTriggerStay(Collider _col)
    {
        Collider col = _col;

        if (col == null)
            return;

        string colTag = col.transform.root.tag.ToLower();

        if (colTag == GeneralStats.playerTagName_ToLower)
        {
            timer = MathfPlus.DecByDeltatimeToZero(timer);

            if (timer == 0)
            {
                DoDamage();

                timer = timeToApplyDamage;
            }
        }
    }

    void MakeDamagePack()
    {
        dmgInfo.damageSource = this.gameObject;

        dmgInfo.damageSourcePosition = damageOccurPosition.position;

        dmgInfo.damageAmount = PlayerCharacterNew.Instance.GetComponent<CharacterInfo>().MaxHealth * (damageAmountPercent / 100f);

        dmgInfo.damageType = DamageType.Fire;
    }

    void DoDamage()
    {
        PlayerCharacterNew.Instance.ApplyDamage(dmgInfo);
    }
}
