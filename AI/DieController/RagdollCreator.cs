using UnityEngine;
using System.Collections;

public class RagdollCreator : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Transform Ragdoll;

    public void MakeRagdoll(DamageInfo dmg, DeadSoldInitialInfo _inf, bool _playDieVoice)
    {
        Transform rd = (Transform)Instantiate(Ragdoll, gameObject.transform.position, gameObject.transform.rotation);

        DeadSoldierBMW datDeadSoldierBMW = rd.GetComponent<DeadSoldierBMW>();

        datDeadSoldierBMW.InitInitialInfo(_inf);

        if (_playDieVoice)
            datDeadSoldierBMW.SetShouldPlayDieVoice();

        AdvancedRagdoll advRd = rd.GetComponent<AdvancedRagdoll>();
        advRd.SynchRagdollIn(gameObject.transform);

        Destroy(gameObject);

        if (dmg.damageType == DamageType.Bullet)
        {
            AddForceToRagdoll adForce = rd.GetComponent<AddForceToRagdoll>();
            adForce.AddForce(dmg.HitPoint, dmg.BulletDirection, dmg.Impulse);
        }
    }
}
