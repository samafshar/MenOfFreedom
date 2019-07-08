using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class MapLogicJob_FightInReg : MapLogicJob
{
    public bool doMoveFight = true;
    public GameObject initialSoldier;
    public float initialDelay = 0;
    public bool resetCurLaneForNewSoldiers = true;
    public NumList lanesTimeList = null;

    public string __Camp____________________________ = "______________________________";
    //    public bool startCampMode = false;
    public SoldierCampType campType = SoldierCampType._NotUsed_;
    public bool useCampCurves = false;
    public CampCurveInfo[] campCurveInfos;

    public string __Follow____________________________ = "______________________________";
    public MapLogicJob_FightInReg followDatJob = null;
    public float followDelayTimeMin = 0.5f;
    public float followDelayTimeMax = 1f;

    public string __Grenade____________________________ = "______________________________";
    public bool grenadeIsControlledByOwnerGroup = true;
    public bool grenadeEnabled = false;
    public int grenadeCount = 0;
    public float grenadeStartDelayTimeMin = 5;
    public float grenadeStartDelayTimeMax = 20;
    public float grenadeNextDelayTimeMin = 20;
    public float grenadeNextDelayTimeMax = 45;


    public string __CustomProperties_________________________ = "_______________________________";
    public bool customPropsAreUsed = false;
    public float customRecievingDamageCoef = 1;
    public bool onlyGetDamageFromPlayer = false;
    public LogicTrigger gettingDamageArea;




    [HideInInspector]
    public int counterOfCreatedSoldiers = 0;

    //[HideInInspector]
    //public GameObject currentSoldier;

    [HideInInspector]
    public MapLogicJob_FightInRegsGroup ownerGroup;

    [HideInInspector]
    public FightRegion fightReg;

    FightPoint[] fPs;
    RespawnPoint_New[] rPs;

    [HideInInspector]
    public int soldierCurrentLaneNum = 0;

    [HideInInspector]
    public int soldierMaxLaneNum = 0;

    float laneTimeCounter = 0;
    bool shouldCountLaneTimes = false;

    [HideInInspector]
    public float[] laneTimes = null;

    float grenadeDelayTimeCounter = 0f;

    [HideInInspector]
    public bool nowReadyForGreandeLaunch = false;

    SoldierAction_FightInReg curFightInRegAct;

    [HideInInspector]
    public bool isOwnerRequestedAGrenade = false;

    bool canAddGettingDamageAreaToNewSolds = true;

    float followJobCounter = 0;
    bool isFollowWaiting = false;

    //

    // Use this for initialization
    void Start()
    {
        fightReg = transform.GetComponentInChildren<FightRegion>();

        foreach (FightPoint fp in fightReg.fightPoints)
        {
            soldierMaxLaneNum = Mathf.Max(soldierMaxLaneNum, fp.lane);
        }

        if (lanesTimeList)
        {
            laneTimes = new float[lanesTimeList.nums.Length];

            for (int i = 0; i < laneTimes.Length; i++)
            {
                laneTimes[i] = lanesTimeList.nums[i];
            }

            if (laneTimes.Length < soldierMaxLaneNum)
            {
                Debug.LogError("Not enough lane times exist in list!");
            }
        }

        SetSoldierCurrentLaneNum(0, true);
    }

    //

    public void Init_AddInitialDelay(float _value)
    {
        initialDelay += _value;
    }

    //

    public override void StartIt()
    {
        base.StartIt();

        //if (customPropsAreUsed)
        //{
        //    if (gettingDamageArea)
        //        gettingDamageArea.SetEnabled(true);
        //}

        if (laneTimes != null)
        {
            for (int i = 0; i < laneTimes.Length; i++)
            {
                laneTimes[i] += initialDelay;
            }
        }

        if (initialSoldier != null)
            Init_SetControlledSoldier(initialSoldier);

        if (fightReg.respawnPointCollection != null)
        {
            fightReg.respawnPointCollection.Init_SetInitialDelay(initialDelay);
            fightReg.respawnPointCollection.Init_Start();
        }

        if (controlledSoldier != null)
        {
            if (fightReg.respawnPointCollection != null)
                fightReg.respawnPointCollection.SetInitialSoldier(controlledSoldier);

            //currentSoldier = controlledSoldier;

            SetCurrentSoldierOwner(this);

            AddFightRegActToSoldier(controlledSoldier);
        }

        grenadeDelayTimeCounter = Random.Range(grenadeStartDelayTimeMin, grenadeStartDelayTimeMax) + initialDelay;

        if (grenadeEnabled && grenadeIsControlledByOwnerGroup && ownerGroup == null)
        {
            Debug.LogError("Fight in reg grenade is enabled and it's set to be controlled by it's owner, but it's owner is null!");
        }

        if (followDatJob)
        {
            isFollowWaiting = true;
        }
    }

    public override void RunIt()
    {
        base.RunIt();

        if (shouldCountLaneTimes)
        {
            laneTimeCounter = MathfPlus.DecByDeltatimeToZero(laneTimeCounter);

            if (laneTimeCounter == 0)
            {
                SetSoldierCurrentLaneNum(soldierCurrentLaneNum + 1, true);
            }
        }

        if (isFollowWaiting)
        {
            if ((followDatJob == null) || (followDatJob != null && followDatJob.status == LogicJobStatus.Finished))
            {
                isFollowWaiting = false;
                followJobCounter = MathfPlus.GetRandomFloat(followDelayTimeMin, followDelayTimeMax);
            }
        }

        if (followJobCounter > 0)
        {
            followJobCounter = MathfPlus.DecByDeltatimeToZero(followJobCounter);
        }

    StartSteps:

        #region 1 Running
        if (step == 1) //Running
        {
            if (IsCreatingSoldiersStoppedAndAllSoldsDead())
            {
                SetFinished(true);
                return;
            }

            if (needsToBeFinished)
            {
                SetStep(2);
                goto StartSteps;
            }

            if (fightReg.respawnPointCollection != null)
            {
                if (!isFollowWaiting && followJobCounter == 0)
                    if (fightReg.respawnPointCollection.IsReady())
                        CreateAndInitSoldier();
            }

            if (grenadeEnabled)
            {
                if (!nowReadyForGreandeLaunch && grenadeCount > 0)
                {
                    grenadeDelayTimeCounter = MathfPlus.DecByDeltatimeToZero(grenadeDelayTimeCounter);
                }

                if (!grenadeIsControlledByOwnerGroup)
                {
                    if (CanSetReadyForLaunchGrenade())
                    {
                        SetItsNowReadyForLaunchGrenade();
                    }
                }
            }
        }
        #endregion

        #region 2 Start Finishing
        if (step == 2) //StartFinishing
        {
            StopCreatingMoreSoldiers();

            if (GeneralStats.IsCharacterAlive(controlledSoldier))
                curFightInRegAct.SetNeedsToBeFinished(evenStopMovingForFinish);

            SetStep(3);
        }
        #endregion

        #region 3 Check Finished
        if (step == 3) //Check Finished
        {
            if (!GeneralStats.IsCharacterAlive(controlledSoldier))
            {
                SetFinished(true);
                return;
            }
            else
            {
                //<Alpha>
                if (needsToBeFinished)
                    curFightInRegAct.SetNeedsToBeFinished(evenStopMovingForFinish);
                //</Alpha>

                if (curFightInRegAct.status == SoldierAction.ActionStatusEnum.Finished)
                {
                    SetFinished(true);
                    return;
                }
            }
        }
        #endregion

    EndSteps:
        ;
    }

    void CreateAndInitSoldier()
    {
        if (resetCurLaneForNewSoldiers)
            SetSoldierCurrentLaneNum(0, true);

        RespawnPointCollection resPointCol = fightReg.respawnPointCollection;

        GameObject sold = resPointCol.CreateSoldier();

        //currentSoldier = sold;

        Init_SetControlledSoldier(sold);

        AddFightRegActToSoldier(controlledSoldier);

        SetCurrentSoldierOwner(this);

        counterOfCreatedSoldiers++;
    }

    void AddFightRegActToSoldier(GameObject _sold)
    {
        GameObject sold = _sold;

        SetSoldierCurrentLaneNum(soldierCurrentLaneNum, false);

        curFightInRegAct = sold.AddComponent<SoldierAction_FightInReg>();
        curFightInRegAct.Init(sold.transform);

        FightPoint[] fps = fightReg.GetFightPointsOfLane(soldierCurrentLaneNum);

        List<FightPoint> highRateFPs = new List<FightPoint>();

        for (int i = 0; i < fps.Length; i++)
        {
            if (!fps[i].isLowRate)
                highRateFPs.Add(fps[i]);
        }

        if (highRateFPs.Count == 0)
            Debug.LogError("FightReg hasn't any high rated fight points!");

        FightPoint initialPoint = null;

        for (int i = 0; i < highRateFPs.Count; i++)
        {
            if (highRateFPs[i].isInitialPoint)
            {
                initialPoint = highRateFPs[i];
                break;
            }
        }

        if (!initialPoint)
            initialPoint = highRateFPs[Random.Range(0, highRateFPs.Count)];

        curFightInRegAct.InitForFightReg(fightReg, initialPoint);

        if (useCampCurves)
        {
            if (campCurveInfos == null || (campCurveInfos != null && campCurveInfos.Length == 0))
            {
                Debug.LogError("Using camp curves is requested while no curve infos has been assigned to logic job!");
            }

            curFightInRegAct.InitCampCurveInfos(campCurveInfos);
        }
        else
        {
            if (campType != SoldierCampType._NotUsed_)
                curFightInRegAct.InitCampInfo(campType);
        }

        //actFightInReg.InitAIVoiceSitu(_mlj_NF_IP.voiceSituation);

        curFightInRegAct.doMoveFight = false; // doMoveFight;
        curFightInRegAct.SetOwnerLogicJob(this);
        curFightInRegAct.StartAct();

        if (nowReadyForGreandeLaunch)
            curFightInRegAct.SetItsNowReadyForLaunchGrenade();

        if (customPropsAreUsed)
        {
            soldCharInfo.SetRecievedDamageCoef(customRecievingDamageCoef);

            soldInfo.SetShouldOnlyTakeDamageFromPlayer(onlyGetDamageFromPlayer);

            if (gettingDamageArea)
                if (canAddGettingDamageAreaToNewSolds)
                    soldInfo.SetGettingDamageArea(gettingDamageArea);
        }
    }

    public void StopCreatingMoreSoldiers()
    {
        if (fightReg.respawnPointCollection != null)
        {
            fightReg.respawnPointCollection.SetFinished();
        }
    }

    public void StopCreatingMoreSoldiersAndMakeAliveSoldierSoWeak()
    {
        StopCreatingMoreSoldiers();

        if (controlledSoldier != null)
        {
            //CharacterInfo curSoldCharInf = currentSoldier.GetComponent<CharacterInfo>();

            if (soldCharInfo != null)
            {
                soldCharInfo.SetRecievedDamageCoefMax();
            }
        }
    }

    public bool IsCreatingSoldiersStoppedAndAllSoldsDead()
    {
        if (fightReg.respawnPointCollection != null)
        {
            if (!fightReg.respawnPointCollection.isFinished)
            {
                return false;
            }
        }

        if (controlledSoldier != null)
        {
            if (!soldCharInfo.IsDeadOrDisabled())
                return false;
        }

        return true;
    }

    public void ResetCounterOfCreatedSoldiers()
    {
        counterOfCreatedSoldiers = 0;
    }

    public override void SetFinished(bool _isFinishedOK)
    {
        base.SetFinished(_isFinishedOK);

        if (ownerGroup != null)
        {
            if (isOwnerRequestedAGrenade)
            {
                ownerGroup.SetAChildFinishedWithoutThrowingRequestedGrenade();
            }
        }

        if (controlledSoldier != null)
        {
            if (curFightInRegAct != null)
                Destroy(curFightInRegAct);

            SetCurrentSoldierOwner(null);
        }

        if (customPropsAreUsed)
        {
            if (gettingDamageArea)
            {
                //gettingDamageArea.SetEnabled(false);

                if (soldInfo != null)
                    soldInfo.SetGettingDamageArea(null);
            }
        }
    }

    public void DecreaseCountOfCreatedSoldiers_ForCountRespawnStyle()
    {
        if (ownerGroup != null)
            ownerGroup.DecreaseCountOfCreatedSoldiers_ForCountRespawnStyle();
    }

    public void SetCurrentSoldierOwner(MapLogicJob_FightInReg _value)
    {
        if (controlledSoldier != null)
        {
            soldInfo.SetOwnerFightInReg(_value);

            if (_value == null)
            {
                soldInfo.shouldDecreaseCountOfOwnerFightInRegOnDeath = false;
            }
            else
            {
                if (fightReg.respawnPointCollection == null || (fightReg.respawnPointCollection != null && fightReg.respawnPointCollection.RespawnPointType == RespawnPointTypeEnum.Counter))
                {
                    soldInfo.shouldDecreaseCountOfOwnerFightInRegOnDeath = true;
                }
            }
        }
    }

    void Update()
    {
        DrawDebugLines();
    }

    void DrawDebugLines()
    {
        fPs = transform.GetComponentsInChildren<FightPoint>();
        rPs = transform.GetComponentsInChildren<RespawnPoint_New>();

        if (fPs != null && fPs.Length > 1)
        {
            for (int i = 0; i < fPs.Length - 1; i++)
            {
                Debug.DrawLine(fPs[i].transform.position, fPs[i + 1].transform.position, Color.red);
            }
        }

        if (rPs != null && rPs.Length > 1)
        {
            for (int i = 0; i < rPs.Length - 1; i++)
            {
                Debug.DrawLine(rPs[i].transform.position, rPs[i + 1].transform.position, new Color(0.88f, 0.71f, 0.18f));
            }
        }

        if ((fPs != null) && (fPs.Length > 0) && (rPs != null) && (rPs.Length > 0))
        {
            Debug.DrawLine(fPs[0].transform.position, rPs[0].transform.position, new Color(0.88f, 0.71f, 0.18f));
        }
    }

    public void SetSoldierCurrentLaneNum(int _num, bool _doResetLaneTimeCtr)
    {
        int lNum = _num;
        bool doResetLaneTimeCtr = _doResetLaneTimeCtr;

        if (lNum > soldierMaxLaneNum)
        {
            Debug.LogError("Requested lane num for soldier is bigger than max lanes in fight reg!");
            return;
        }

        soldierCurrentLaneNum = lNum;

        if (controlledSoldier != null)
        {
            soldInfo.SetCurrentFightRegLaneNum(soldierCurrentLaneNum);
        }

        if (doResetLaneTimeCtr)
            ResetLaneTimeCounter(lNum);
    }

    void ResetLaneTimeCounter(int _laneNum)
    {
        int lNum = _laneNum;

        if (laneTimes == null)
        {
            laneTimeCounter = 0;
            shouldCountLaneTimes = false;
            return;
        }

        if (lNum >= soldierMaxLaneNum)
        {
            laneTimeCounter = 0;
            shouldCountLaneTimes = false;
            return;
        }

        laneTimeCounter = laneTimes[lNum];
        shouldCountLaneTimes = true;
    }

    //Grenade

    public bool CanSetReadyForLaunchGrenade()
    {
        if (grenadeDelayTimeCounter > 0)
            return false;

        if (!grenadeEnabled)
            return false;

        if (grenadeCount == 0)
            return false;

        if (status != LogicJobStatus.Running)
            return false;

        if (IsCreatingSoldiersStoppedAndAllSoldsDead())
            return false;

        if (controlledSoldier == null)
            return false;

        //SoldierInfo sInfo = currentSoldier.GetComponent<SoldierInfo>();

        if (soldInfo == null)
            return false;

        bool haveAnyOkPoint = false;

        foreach (FightPoint fPoint in fightReg.fightPoints)
        {
            if (fPoint.lane != soldInfo.curFightRegLaneNum)
                continue;

            bool haveAnyGrenadeFInfo = false;

            foreach (SoldierFightInPointInfo fInfo in fPoint.fightInfos)
            {
                if (fInfo.grenadeEnabled)
                {
                    haveAnyGrenadeFInfo = true;
                    break;
                }
            }

            if (!haveAnyGrenadeFInfo)
                continue;

            if (!fPoint.IsGenerallyOkForGrenade())
                continue;

            haveAnyOkPoint = true;
            break;
        }

        if (!haveAnyOkPoint)
            return false;

        return true;
    }

    public void SetItsNowReadyForLaunchGrenade()
    {
        nowReadyForGreandeLaunch = true;

        if (curFightInRegAct != null)
        {
            curFightInRegAct.SetItsNowReadyForLaunchGrenade();
        }
    }

    public void SetGrenadeIsLaunchedNow()
    {
        if (!nowReadyForGreandeLaunch)
            return;

        grenadeDelayTimeCounter = Random.Range(grenadeNextDelayTimeMin, grenadeNextDelayTimeMax);

        grenadeCount--;
        grenadeCount = Mathf.Clamp(grenadeCount, 0, int.MaxValue);

        nowReadyForGreandeLaunch = false;
        isOwnerRequestedAGrenade = false;
    }

    public void SetOwnerRequestedAGrenade()
    {
        isOwnerRequestedAGrenade = true;
    }

    public void RemoveGettingDamageAreaFromCurSoldierAndStopAddingToNewSolds()
    {
        if (customPropsAreUsed)
        {
            //if (gettingDamageArea)
            //    gettingDamageArea.SetEnabled(false);

            if (soldInfo != null)
                soldInfo.SetGettingDamageArea(null);

            canAddGettingDamageAreaToNewSolds = false;
        }
    }
}
