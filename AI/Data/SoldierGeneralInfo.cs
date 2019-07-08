//<91-04-12>

using UnityEngine;
using System.Collections;



public class SoldierGeneralInfo : MonoBehaviour
{
    public LayerMask pathFindObstacleMask;

    public Transform[] fightHaloRaycastTargets;

    public SoldierMovementInfo[] movementInfos;

    public SoldierIdleInfo[] idleInfos;

    public SoldierFightInPointInfo defaultOnPosFightInPointInfo;

    //public AudioSource AudioSource_Voice;
    //public AudioSource AudioSource_Gun;
    //public AudioSource AudioSource_FootSteps;

    public AudioInfo AudioInfo_Voice;
    public AudioInfo AudioInfo_Gun;
    public AudioInfo AudioInfo_FootSteps;

    public AudioInfo AudioInfo_Ally_Voice;
    public AudioInfo AudioInfo_Ally_Gun;
    public AudioInfo AudioInfo_Ally_FootSteps;

    public AnimsList[] SoldierAnimations;

    public SoldierMachineGunInfo[] machineGunInfos;

    public Transform footRayCastStartTr;
    public Transform footRayCastEndTr;

    public AnimationCurve footStepDelayGraph;

    public FootStepSoundList footStepSoundList;

    public DeadSoldEquivAnimInfo[] deadSoldEquivAnimInfos;

    public SoldierCampInfo[] campInfos;

    public SoldierMachineGunInfo GetMachineGunInfoByType(MachineGunType _type)
    {
        foreach (SoldierMachineGunInfo mgi in machineGunInfos)
        {
            if (mgi.machineGunType == _type)
                return mgi;
        }

        Debug.LogError("Couldn't find " + _type + " in machine gun infos. Add it!");

        return null;
    }

    public SoldierCampInfo GetCampInfoByType(SoldierCampType _campType)
    {
        foreach (SoldierCampInfo inf in campInfos)
        {
            if (inf.campType == _campType)
                return inf;
        }

        Debug.LogError("Couldn't find " + _campType + " in camp infos. Add it!");

        return null;
    }

    public float DieAnimChance = 0.5f;

    public GameObject soldierGrenadeObject;
}
