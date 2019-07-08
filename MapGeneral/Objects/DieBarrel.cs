using UnityEngine;
using System.Collections;

public class DieBarrel : MonoBehaviour
{
    public GameObject Barrel;

    public GameObject Particle_Fire;

    public Vector3 Particle_Transform_Fire;

    public GameObject Tower;

    public float health = 100;
    private float maxHealth;

    MultiDamageController multiDmgCtrl = new MultiDamageController();
    // Use this for initialization
    void Start()
    {
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ApplyDamage(DamageInfo dmg)
    {
        if (multiDmgCtrl.IsDamageAppliedBefore(dmg))
            return;

        multiDmgCtrl.AddDamage(dmg);

        if (health == maxHealth)
        {
            GameObject fireObj = Instantiate(Particle_Fire) as GameObject;
            fireObj.transform.parent = transform;
            fireObj.transform.localPosition = Particle_Transform_Fire;
        }
        health -= dmg.damageAmount;
        if (health <= 0)
        {
            GameObject gObj = Instantiate(Barrel, transform.position, transform.rotation) as GameObject;
            Tower.animation.Play();
            Destroy(gameObject);
        }
    }
}
