using UnityEngine;
using System.Collections;

public class DeadSoldierBMW : MonoBehaviour
{
    public AudioInfo dieAudioInfo;
    public Transform spineTr;

    DeadSoldInitialInfo initialInfo;
    MapLogic mapLogic;
    PlayerCharacterNew playerCharNew;

    AudioClip[] dieVoicesToPlay;
    bool shouldPlayVoice = false;

    bool isFirstRun = true;

    //float minLifeTime = 8;
    //float maxLifeTime = 50;

    float minLifeTimeCounter;
    float maxLifeTimeCounter;

    float timeToSleepPhysics = 6f;
    bool isPhysicsSleeped = false;

    [HideInInspector]
    public bool isGoingToHideBecauseOfHittingNewNash = false;

    float hittingNewNashStartingTime = 0;

    void Start()
    {
        mapLogic = MapLogic.Instance;
        playerCharNew = PlayerCharacterNew.Instance;

        mapLogic.mapDeadSoldierBMWs.Add(this);

        minLifeTimeCounter = GeneralStats.deadNash_minLifeTime;
        maxLifeTimeCounter = GeneralStats.deadNash_maxLifeTime;
    }

    void Update()
    {
        minLifeTimeCounter = MathfPlus.DecByDeltatimeToZero(minLifeTimeCounter);

        if (minLifeTimeCounter == 0)
        {
            if (!IsInPlayerView())
            {
                DestroyIt();
                return;
            }
        }

        maxLifeTimeCounter = MathfPlus.DecByDeltatimeToZero(maxLifeTimeCounter);

        if (!isPhysicsSleeped)
        {
            if (maxLifeTimeCounter <= GeneralStats.deadNash_maxLifeTime - timeToSleepPhysics)
            {
                isPhysicsSleeped = true;

                Rigidbody[] rigids = GetComponentsInChildren<Rigidbody>();

                for (int i = 0; i < rigids.Length; i++)
                {
                    rigids[i].active = false;
                }
            }
        }

        //if (!isGoingToHideBecauseOfHittingNewNash)
        //{
        //    if (maxLifeTimeCounter <= GeneralStats.deadNash_maxLifeTime - GeneralStats.deadNash_minLifeTimeWhileTouchingAnotherNash)
        //    {
        //        for (int i = 0; i < mapLogic.mapDeadSoldierBMWs.Count; i++)
        //        {
        //            if (mapLogic.mapDeadSoldierBMWs[i] == this)
        //                continue;

        //            if (mapLogic.mapDeadSoldierBMWs[i].isGoingToHideBecauseOfHittingNewNash)
        //                continue;

        //            if (Vector3.Magnitude(transform.position - mapLogic.mapDeadSoldierBMWs[i].transform.position) <= GeneralStats.deadNash_HittingNewNashMaxDist)
        //            {
        //                isGoingToHideBecauseOfHittingNewNash = true;
        //                hittingNewNashStartingTime = maxLifeTimeCounter;
        //                break;
        //            }
        //        }
        //    }
        //}
        //else
        //{
        //    if (maxLifeTimeCounter <= hittingNewNashStartingTime - GeneralStats.deadNash_maxLifeTimeAfterTouchingAnotherNash)
        //    {
        //        DestroyIt();
        //        return;
        //    }
        //}

        if (maxLifeTimeCounter == 0)
        {
            DestroyIt();
            return;
        }

        if (!isFirstRun)
        {
            if (shouldPlayVoice)
            {
                shouldPlayVoice = false;

                if (playerCharNew.isCampPlayer)
                {
                    dieVoicesToPlay = initialInfo.voiceInfo.CampModeDie;
                }
                else
                {
                    dieVoicesToPlay = initialInfo.voiceInfo.Die;
                }

                AudioInfo dieNewAudInf = GameObject.Instantiate(dieAudioInfo, transform.position, transform.rotation) as AudioInfo;
                dieNewAudInf.Init();
                dieNewAudInf.PlayClip(dieVoicesToPlay);

                DieInTime dieAudInfDieInTime = dieNewAudInf.gameObject.AddComponent<DieInTime>();
                dieAudInfDieInTime.time = SoldierStats.MaxTimeOfDeadSoldVoice;
            }
        }

        if (isFirstRun)
            isFirstRun = false;
    }

    public void InitInitialInfo(DeadSoldInitialInfo _initialInfo)
    {
        initialInfo = _initialInfo;
    }

    public void SetShouldPlayDieVoice()
    {
        shouldPlayVoice = true;
    }

    public void DestroyIt()
    {
        if (mapLogic.mapDeadSoldierBMWs.Contains(this))
            mapLogic.mapDeadSoldierBMWs.Remove(this);

        Destroy(gameObject);
    }

    public bool IsInPlayerView()
    {
        return GeneralStats.IsVecInView(spineTr.position, playerCharNew.soldNashDetectorTr.position, playerCharNew.soldNashDetectorTr.rotation, -100, 100, 120);
    }
}
