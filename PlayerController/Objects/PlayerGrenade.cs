using UnityEngine;
using System.Collections;

public class PlayerGrenade : MonoBehaviour
{
    public GameObject expEffect;
    public float delayTimeToEnableCollider = 0;
    public float explosionTimeAfterFirstHit = 1;

    public float time = 4;

    [HideInInspector]
    public float timeCounter;

    Explosion explosion;

    bool isHitted = false;
    float hitStartingTime = 0;

    float minTimeBeforeConsideringHit = 0.6f;

    void Start()
    {
        explosion = GetComponent<Explosion>();
        timeCounter = time;

        MapLogic.Instance.AddActiveGrenade(this);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isHitted)
        {
            isHitted = true;
            hitStartingTime = timeCounter;
        }

        //explosion.Explode();

        //ContactPoint contact = collision.contacts[0];
        //Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        //Vector3 pos = contact.point;
        //GameObject.Instantiate(expEffect, pos, rot);

        //Destroy(gameObject);
    }

    void Update()
    {
        timeCounter = MathfPlus.DecByDeltatimeToZero(timeCounter);
        delayTimeToEnableCollider = MathfPlus.DecByDeltatimeToZero(delayTimeToEnableCollider);

        if (timeCounter <= time - minTimeBeforeConsideringHit)
        {
            if (isHitted)
            {
                if (timeCounter <= hitStartingTime - explosionTimeAfterFirstHit)
                {
                    Explode();
                    return;
                }
            }
        }

        if (timeCounter == 0)
        {
            Explode();
            return;
        }

        if (delayTimeToEnableCollider == 0)
        {
            delayTimeToEnableCollider = 1000000;

            Collider col = GetComponent<Collider>();
            if (!col.enabled)
                col.enabled = true;
        }
    }

    void Explode()
    {
        MapLogic.Instance.RemoveActiveGrenade(this);

        explosion.Explode();
        GameObject.Instantiate(expEffect, gameObject.transform.position, Quaternion.LookRotation(Vector3.up));
        Destroy(gameObject);
    }
}
