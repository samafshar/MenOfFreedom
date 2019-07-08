using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExplosionDamageInfo
{
    public DamageInfo damageInfo;
    public float playerDamageSheddat = 0;
}

public class ExplosionHittedSold
{
    public GameObject datSold;
    public float dmgAmount = -1;
}

public class Explosion : MonoBehaviour
{
    public float Radius;
    public float ForcePower;
    public float DamageAmount;
    public float PlayerCamShakeRadiusCoef = 1.2f;

    public bool explodeOnStart = true;
    public bool isItPlayerGrenade = false;

    float playerMinDistToDoMaxShakeCoefOfRadius = 0.33f;
    //float playerShakeCamRangeCoefOfRadius = 1.1f;
    AnimationCurve damageCurve = new AnimationCurve(new Keyframe(0f, 0.1f),
                                                    new Keyframe(0.1f, 0.12f),
                                                    new Keyframe(0.2f, 0.15f),
                                                    new Keyframe(0.3f, 0.2f),
                                                    new Keyframe(0.4f, 0.25f),
                                                    new Keyframe(0.5f, 0.5f),
                                                    new Keyframe(0.6f, 0.75f),
                                                    new Keyframe(0.7f, 1f),
                                                    new Keyframe(0.8f, 1f),
                                                    new Keyframe(0.9f, 1f),
                                                    new Keyframe(1f, 1f));

    // AnimationCurve damageCurve = new AnimationCurve(new Keyframe(0f, 0),
    //                                           new Keyframe(0.1f, 0.03f),
    //                                           new Keyframe(0.2f, 0.11f),
    //                                           new Keyframe(0.3f, 0.22f),
    //                                           new Keyframe(0.4f, 0.35f),
    //                                           new Keyframe(0.5f, 0.5f),
    //                                           new Keyframe(0.6f, 0.65f),
    //                                           new Keyframe(0.7f, 0.78f),
    //                                           new Keyframe(0.8f, 0.89f),
    //                                           new Keyframe(0.9f, 0.95f),
    //                                           new Keyframe(1f, 1f));

    LayerMask ExplosionLayer;

    Vector3 explosionPos;

    List<ExplosionHittedSold> hittedSolds = new List<ExplosionHittedSold>();

    void Start()
    {
        ExplosionLayer = GameGeneralInfo.Instance.ExplosionLayer;

        if (explodeOnStart)
            Explode();
    }

    void Update()
    {

    }

    public void Explode()
    {
        explosionPos = transform.position;

        DamageInfo dmg = new DamageInfo();

        if (isItPlayerGrenade && PlayerCharacterNew.Instance != null)
        {
            dmg.damageSource = PlayerCharacterNew.Instance.gameObject;
            //dmg.damageSourcePosition = PlayerCharacterNew.Instance.gameObject.transform.position;
            dmg.damageSourcePosition = transform.position;
        }
        else
        {
            dmg.damageSource = this.gameObject;
            dmg.damageSourcePosition = transform.position;
        }

        dmg.damageType = DamageType.Explosion;

        //

        Collider[] colliders = Physics.OverlapSphere(explosionPos, Radius, ExplosionLayer);
        foreach (Collider hit in colliders)
        {
            Vector3 closestPoint = hit.ClosestPointOnBounds(explosionPos);

            if ((hit.transform.root.tag.ToLower() == GeneralStats.allyTagName_ToLower) || (hit.transform.root.tag.ToLower() == GeneralStats.enemyTagName_ToLower))
            {
                closestPoint = hit.transform.root.position;
            }

            float distance = Vector3.Distance(closestPoint, explosionPos);

            float hitRatio = Mathf.Clamp01(damageCurve.Evaluate(1 - Mathf.Clamp01(distance / Radius)));
            dmg.damageAmount = hitRatio * DamageAmount;

            if ((hit.transform.root.tag.ToLower() == GeneralStats.allyTagName_ToLower) || (hit.transform.root.tag.ToLower() == GeneralStats.enemyTagName_ToLower))
            {
                bool isSoldAddedBefore = false;

                foreach (ExplosionHittedSold ehs in hittedSolds)
                {
                    if (ehs.datSold == hit.transform.root.gameObject)
                    {
                        isSoldAddedBefore = true;

                        if (ehs.dmgAmount < dmg.damageAmount)
                            ehs.dmgAmount = dmg.damageAmount;
                    }
                }

                if (!isSoldAddedBefore)
                {
                    ExplosionHittedSold ehs = new ExplosionHittedSold();
                    ehs.datSold = hit.transform.root.gameObject;
                    ehs.dmgAmount = dmg.damageAmount;

                    hittedSolds.Add(ehs);
                }
            }
            else
            {
                hit.SendMessageUpwards("ApplyDamage", dmg, SendMessageOptions.DontRequireReceiver);
            }
        }

        foreach (ExplosionHittedSold ehs in hittedSolds)
        {
            if (ehs != null)
            {
                dmg.damageAmount = ehs.dmgAmount;
                if (ehs.datSold != null)
                    ehs.datSold.SendMessage("ApplyDamage", dmg, SendMessageOptions.DontRequireReceiver);
            }
        }

        //

        colliders = Physics.OverlapSphere(explosionPos, Radius, ExplosionLayer);
        foreach (Collider hit in colliders)
        {
            if (hit.rigidbody && hit.gameObject.layer.ToString() != "Player")
            {
                hit.rigidbody.AddExplosionForce(ForcePower, explosionPos, Radius);
            }
        }

        //

        if (PlayerCharacterNew.Instance != null)
        {
            PlayerCharacterNew player = PlayerCharacterNew.Instance;

            float playerDist = (player.transform.position - explosionPos).magnitude;

            float playerCameraShakeRange = Radius * PlayerCamShakeRadiusCoef;

            float playerCameraShake_MinDistToDoMaxShakeEee = playerCameraShakeRange * playerMinDistToDoMaxShakeCoefOfRadius;


            float sourat = playerCameraShakeRange - playerDist;

            float makhraj = playerCameraShakeRange - playerCameraShake_MinDistToDoMaxShakeEee;

            float result = Mathf.Clamp01(sourat / makhraj);

            ExplosionDamageInfo expDmgInf = new ExplosionDamageInfo();
            expDmgInf.damageInfo = dmg;
            expDmgInf.playerDamageSheddat = result;

            player.SetExplosionEffectIsNeeded(expDmgInf, true);
        }
    }
}