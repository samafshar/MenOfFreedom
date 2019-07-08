//<91-04-12>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum FightInRegStep
{
    Start01,
    Start02,
    Start03,
    TryStartMoving01,
    TryStartMoving02,
    TryStartMoving03,
    ShortMoving01,
    ShortMoving02,
    NoramlMoving01,
    NoramlMoving02,
    NoramlMoving03,
    NoramlMoving_StopMovingAndGoToFightInPos01,
    NoramlMoving_StopMovingAndGoToFightInPos02,
    FightingInPoint01,
    FightingInPoint02,
    FightingInPoint_ChangePoint01,
    FightingInPoint_ChangePoint02,
    FightingInPos00,
    FightingInPos01,
    FightingInPos02,
    FightingInPoint_FightCritWithPlayer01,
    FightingInPoint_FightCritWithPlayer02,
    Finishing01,
    Finishing02,
    Camp01,
    Camp02,
    CampFinishingForStartingNormalMode01,
    CampFinishingForStartingNormalMode02,
}

public class SoldierAction_FightInReg : SoldierAction
{
    FightInRegStep step;

    public FightRegion fightReg;
    public FightPoint selectedFightPoint;
    public bool doMoveFight = true;

    FightPoint newFightPointToGo;

    public List<GameObject> initialEnemies = null;

    SoldierAction_Movement movementAct;
    MovementTypeEnum currentMovementType;
    float currentMovementFirstAccTime;
    float currentMovementEndAccTime;
    float currentMovementFirstAnimTime;
    float currentMovementEndAnimTime;
    Vector3[] currentMovementPath;

    SoldierAction_FightInPoint fightInPointAct;

    SoldierAction_Camp campAct;

    public bool shouldConsiderPlayerCriticalSituation = true;

    float maxErrorToPos;
    float minDistForLongMoves = 2;

    float aStarResultMaxTime = 0.4f;
    float aStarResultTimeCounter = 0.4f;

    //float delayTimeToChangePoint_Min = 0.5f;
    //float delayTimeToChangePoint_Max = 1f;
    //float delayTimeToChangePoint_Counter;

    //float timeToGoToClosePoint_Min = 5f;
    //float timeToGoToClosePoint_Max = 12f;
    //float timeToGoToClosePoint_Counter;

    float timeToCheckAvailableTargets_Min = 0.8f;
    float timeToCheckAvailableTargets_Max = 1.8f;
    float timeToCheckAvailableTargets_Counter;


    float time_CheckPlayerCritStateWhileMoving_Max = 0.1f;
    float time_CheckPlayerCritStateWhileMoving_Counter = 0.1f;


    float time_CheckPlayerInCritStateWhileInFightInPos_Max = 0.1f;
    float time_CheckPlayerInCritStateWhileInFightInPos_Counter = 0.1f;

    float time_CheckPlayerInCritStateWhileInFightInPoint_Max = 0.1f;
    float time_CheckPlayerInCritStateWhileInFightInPoint_Counter = 0.1f;

    float time_SelectNewPointToGoFromFightingInPos_Max_WhileNoPath = 1;
    float time_SelectNewPointToGoFromFightingInPos_Max = 0.2f;
    float time_SelectNewPointToGoFromFightingInPos_Counter;


    float time_ChangePoint_Max = 12f;
    float time_ChangePoint_Min = 4f;
    float time_ChangePoint_Counter = 0;

    float maxValueToDecreaseFromCurrentPointRating = 0.4f;

    float minMoveTimeForPlayerCriticalFight = 0.2f;

    float normalMovementStopTime = 0.28f;
    SoldierFightInPointInfo onPosFightInPointInfo;

    SoldierAction currentAction;

    bool doPres = true;

    bool pre_FightInPoint = false;

    bool needsToGoToInitialPoint = true;
    FightPoint initialPoint;

    bool initialTargetForAfterMovementChecked = false;
    bool shouldSetInitialTargForFightInPoint = false;
    GameObject initialTargetForFightInPoint = null;
    SoldierGunDirectionEnum gunDirForFightInPointInitialTarget = SoldierGunDirectionEnum.Forward;

    bool shouldSetPlayerAsTargetForFightInPos = false;
    SoldierGunDirectionEnum gunDirForTargettingPlayerInFightInPos = SoldierGunDirectionEnum.Forward;

    public AIVoiceSituation voiceSituation = AIVoiceSituation.Silence;

    MapLogicJob_FightInReg ownerLogicJob;

    bool nowReadyForGreandeLaunch = false;

    SoldierCampInfo campInfo = null;
    CampCurveInfo[] campCurveInfos = null;

    bool isPlayerDetectedInCampMode = false;

    public override void Init(Transform contSoldier)
    {
        base.Init(contSoldier);

        //maxErrorToPos = SoldierGeneral.SoldierMaxErrorToPos;
        maxErrorToPos = 1;

        onPosFightInPointInfo = soldInfo.soldierGeneralInfo.defaultOnPosFightInPointInfo;

        if (soldCharInfo.FightSide == FightSideEnum.Enemy)
            InitAIVoiceSitu(AIVoiceSituation.Agressive);

    }

    public void InitCampInfo(SoldierCampType _campType)
    {
        campInfo = soldInfo.soldierGeneralInfo.GetCampInfoByType(_campType);
    }


    public void InitCampCurveInfos(CampCurveInfo[] _campCurveInfos)
    {
        campCurveInfos = _campCurveInfos;
    }

    public void InitForFightReg(FightRegion _fightReg, FightPoint _initialFightPoint)
    {
        fightReg = _fightReg;
        selectedFightPoint = _initialFightPoint;
        needsToGoToInitialPoint = true;
        initialPoint = _initialFightPoint;
    }

    public void InitAIVoiceSitu(AIVoiceSituation _situ)
    {
        voiceSituation = _situ;
    }

    public override void StartAct()
    {
        base.StartAct();

        step = FightInRegStep.Start01;
    }

    public override void UpdateAct()
    {
        base.UpdateAct();

        #region Pre
        if (doPres)
        {
            if (ShouldCheckPlayerCriticalSitu())
                soldInfo.SlowlyUpdatePlayerCritic();

            #region FightInPoint
            if (pre_FightInPoint)
            {
                DoPre_FightInPoint();
            }
            #endregion
        }
        #endregion

    StartSteps:

        #region Start01
        if (step == FightInRegStep.Start01)
        {
            if (ShouldStartCampMode())
                step = FightInRegStep.Camp01;
            else
                step = FightInRegStep.TryStartMoving01;
        }
        #endregion

        #region TryStartMoving01
        if (step == FightInRegStep.TryStartMoving01)
        {
            if (needsToBeFinished)
            {
                SetFinished(true);
                return;
            }

            if (!NeedsDistantMoveToGetToPos(controlledSoldier.transform.position, selectedFightPoint.transform.position))
            {
                step = FightInRegStep.ShortMoving01;
                goto StartSteps;
            }

            step = FightInRegStep.NoramlMoving01;
            goto StartSteps;

            //if (IsSoldOnPoint(controlledSoldier.gameObject, selectedFightPoint.transform.position, maxErrorToPos))
            //{
            //    step = FightInRegStep.FightingInPoint01;
            //    goto StartSteps;
            //}
            //else
            //{
            //    Vector3[] newPath;

            //    if (mapLogic.FindCurvePath(controlledSoldier.transform.position, selectedFightPoint.transform.position, maxErrorToPos, out newPath))
            //    {
            //        currentMovementPath = newPath;
            //        step = FightInRegStep.NoramlMoving01;
            //        goto StartSteps;
            //    }
            //    else
            //    {
            //        step = FightInRegStep.TryStartMoving02;
            //        goto StartSteps;
            //    }
            //}
        }
        #endregion

        #region TryStartMoving02
        if (step == FightInRegStep.TryStartMoving02)
        {
            aStarResultTimeCounter = aStarResultMaxTime;
            soldInfo.FindNewAStarPath(controlledSoldier.transform.position, selectedFightPoint.transform.position);
            step = FightInRegStep.TryStartMoving03;
            goto StartSteps;
        }
        #endregion

        #region TryStartMoving03
        if (step == FightInRegStep.TryStartMoving03)
        {
            if (needsToBeFinished)
            {
                SetFinished(true);
                return;
            }

            if (soldInfo.isAStarPathResultRecievedInThisRun)
            {
                if (!soldInfo.isAStarPathError)
                {
                    currentMovementPath = soldInfo.aStarLastPath;
                    step = FightInRegStep.NoramlMoving01;
                    goto StartSteps;
                }
                else
                {
                    //FightPoint fp;
                    //if (IsSoldierOnAPoint(out fp))
                    //{
                    //    //<Temp>
                    //    Debug.LogError("No path founded to point!");
                    //    //</Temp>

                    //    selectedFightPoint = fp;
                    //    step = FightInRegStep.FightingInPoint01;
                    //    goto StartSteps;
                    //}

                    //<Temp>
                    Debug.LogError("No path founded to point!");
                    //</Temp>

                    time_SelectNewPointToGoFromFightingInPos_Counter = time_SelectNewPointToGoFromFightingInPos_Max_WhileNoPath;
                    step = FightInRegStep.FightingInPos01;
                    goto StartSteps;
                }
            }

            aStarResultTimeCounter -= Time.deltaTime;
            if (aStarResultTimeCounter <= 0)
            {
                //<Temp>
                Debug.LogError("No path founded in needed time!");
                //</Temp>

                //FightPoint fp;
                //if (IsSoldierOnAPoint(out fp))
                //{
                //    selectedFightPoint = fp;
                //    step = FightInRegStep.FightingInPoint01;
                //    goto StartSteps;
                //}

                time_SelectNewPointToGoFromFightingInPos_Counter = time_SelectNewPointToGoFromFightingInPos_Max_WhileNoPath;
                step = FightInRegStep.FightingInPos01;
                goto StartSteps;
            }
        }
        #endregion

        #region NoramlMoving01
        if (step == FightInRegStep.NoramlMoving01)
        {
            if (needsToBeFinished)
            {
                SetFinished(true);
                return;
            }

            initialTargetForAfterMovementChecked = false;
            shouldSetInitialTargForFightInPoint = false;
            initialTargetForFightInPoint = null;

            currentMovementType = fightReg.movementType; //GetMovementTypeForMovingToTarget(currentMovementPath);

            movementAct = controlledSoldier.gameObject.AddComponent<SoldierAction_Movement>();
            movementAct.Init(controlledSoldier);
            movementAct.InitDefaultParams(currentMovementType);
            movementAct.initialEnemies = initialEnemies;

            if (doMoveFight)
                movementAct.Init_DoFightWhileMove(true);


            //SetMovementParams(currentMovementType);

            movementAct.SetNextActAnimToCrossfade(selectedFightPoint.defaultfightInfo.GetAStartAnim());
            movementAct.SetEndingRotNormal(selectedFightPoint.transform.forward);

            movementAct.Init_PosToFindPath(selectedFightPoint.transform.position);

            //movementAct.Init_SetPath(currentMovementPath);

            movementAct.StartAct();
            SetCurrentAction(movementAct);

            step = FightInRegStep.NoramlMoving02;
        }
        #endregion

        #region NoramlMoving02
        if (step == FightInRegStep.NoramlMoving02)
        {
            if (needsToBeFinished)
            {
                movementAct.SetNeedsToBeFinished(evenStopMovingForFinish);
                step = FightInRegStep.Finishing01;
                goto StartSteps;
            }

            if (!initialTargetForAfterMovementChecked && movementAct.isBeforeEndAnimRun)
            {
                initialTargetForAfterMovementChecked = true;

                GameObject targ = SelectBestTargetForPoint(selectedFightPoint);

                if (targ != null)
                {
                    shouldSetInitialTargForFightInPoint = true;
                    initialTargetForFightInPoint = targ;
                    movementAct.SetEndingRotLookTarget(initialTargetForFightInPoint.transform);

                    gunDirForFightInPointInitialTarget = SoldierStats.GetSoldierGunDirectionForTarget(controlledSoldier.gameObject, initialTargetForFightInPoint);

                    movementAct.SetNextActAnimToCrossfade(selectedFightPoint.defaultfightInfo.GetAnimList_FightLookAllBody(gunDirForFightInPointInitialTarget).GetRandomAnimName());
                }
            }

            if (movementAct.finishReport == FinishReportEnum.FinishedOK)
            {
                if (selectedFightPoint == initialPoint)
                    needsToGoToInitialPoint = false;

                Destroy(movementAct);
                SetCurrentAction(null);
                step = FightInRegStep.FightingInPoint01;
                goto StartSteps;
            }


            if (!movementAct.isEndingV && SlowlyCheckPlayerCritStateWhileMoving())
            {
                step = FightInRegStep.NoramlMoving_StopMovingAndGoToFightInPos01;
                goto StartSteps;
            }


        }
        #endregion

        #region NoramlMoving_StopMovingAndGoToFightInPos01
        if (step == FightInRegStep.NoramlMoving_StopMovingAndGoToFightInPos01)
        {
            if (IsPlayerStillInCriticSitu())
            {
                shouldSetPlayerAsTargetForFightInPos = true;
                gunDirForTargettingPlayerInFightInPos = SoldierStats.GetSoldierGunDirectionForTarget(controlledSoldier.gameObject, mapLogic.player);

                Transform playerTr = null;

                if (mapLogic.player != null)
                    playerTr = mapLogic.player.transform;

                movementAct.SetNeedsToStop(normalMovementStopTime, onPosFightInPointInfo.GetAnimList_FightLookAllBody(gunDirForTargettingPlayerInFightInPos).GetRandomAnimName(), playerTr);

            }
            else
                movementAct.SetNeedsToStop(normalMovementStopTime, onPosFightInPointInfo.GetAStartAnim());

            step = FightInRegStep.NoramlMoving_StopMovingAndGoToFightInPos02;
        }
        #endregion

        #region NoramlMoving_StopMovingAndGoToFightInPos02
        if (step == FightInRegStep.NoramlMoving_StopMovingAndGoToFightInPos02)
        {
            if (movementAct.finishReport == FinishReportEnum.FinishedOK)
            {
                Destroy(movementAct);
                SetCurrentAction(null);
                step = FightInRegStep.FightingInPos00;
            }
        }
        #endregion

        #region FightingInPos00
        if (step == FightInRegStep.FightingInPos00)
        {
            time_SelectNewPointToGoFromFightingInPos_Counter = time_SelectNewPointToGoFromFightingInPos_Max;
            step = FightInRegStep.FightingInPos01;
        }
        #endregion

        #region FightingInPos01
        if (step == FightInRegStep.FightingInPos01)
        {
            if (needsToBeFinished)
            {
                SetFinished(true);
                return;
            }

            fightInPointAct = controlledSoldier.gameObject.AddComponent<SoldierAction_FightInPoint>();
            fightInPointAct.Init(controlledSoldier);
            fightInPointAct.InitForPos(controlledSoldier.transform.position, controlledSoldier.transform.rotation, onPosFightInPointInfo);
            fightInPointAct.InitAIVoiceSitu(voiceSituation);
            fightInPointAct.initialEnemies = initialEnemies;

            if (shouldSetPlayerAsTargetForFightInPos)
            {
                fightInPointAct.Init_StartInShootSituForTarget(mapLogic.player);
                fightInPointAct.currentGunDirection = gunDirForTargettingPlayerInFightInPos;

                shouldSetPlayerAsTargetForFightInPos = false;
            }

            fightInPointAct.SetOwnerFightInRegAct(this);

            fightInPointAct.StartAct();

            SetCurrentAction(fightInPointAct);

            step = FightInRegStep.FightingInPos02;
        }
        #endregion

        #region FightingInPos02
        if (step == FightInRegStep.FightingInPos02)
        {
            if (needsToBeFinished)
            {
                fightInPointAct.SetNeedsToBeFinished(evenStopMovingForFinish);
                step = FightInRegStep.Finishing01;
                goto StartSteps;
            }

            time_SelectNewPointToGoFromFightingInPos_Counter = MathfPlus.DecByDeltatimeToZero(time_SelectNewPointToGoFromFightingInPos_Counter);

            if (IsPlayerStillInCriticSitu())
            {
                time_CheckPlayerInCritStateWhileInFightInPos_Counter = MathfPlus.DecByDeltatimeToZero(time_CheckPlayerInCritStateWhileInFightInPos_Counter);

                if (time_CheckPlayerInCritStateWhileInFightInPos_Counter <= 0)
                {
                    time_CheckPlayerInCritStateWhileInFightInPos_Counter = time_CheckPlayerInCritStateWhileInFightInPos_Max;

                    if (!soldInfo.IsPlayerInView())
                    {
                        if (time_SelectNewPointToGoFromFightingInPos_Counter == 0)
                        {
                            time_SelectNewPointToGoFromFightingInPos_Counter = time_SelectNewPointToGoFromFightingInPos_Max;

                            //<Temp>
                            newFightPointToGo = SelectBestFightPointForPlayerCritState();
                            step = FightInRegStep.FightingInPoint_ChangePoint01;
                            //</Temp>

                            goto StartSteps;
                        }
                    }
                }
            }
            else
            {
                if (time_SelectNewPointToGoFromFightingInPos_Counter == 0)
                {
                    time_SelectNewPointToGoFromFightingInPos_Counter = time_SelectNewPointToGoFromFightingInPos_Max;

                    newFightPointToGo = SelectNewFightPoint();
                    step = FightInRegStep.FightingInPoint_ChangePoint01;
                    goto StartSteps;
                }
            }
        }
        #endregion

        #region FightingInPoint01
        if (step == FightInRegStep.FightingInPoint01)
        {
            if (needsToBeFinished)
            {
                SetFinished(true);
                return;
            }

            pre_FightInPoint = true;

            //ResetDelayTimeToChangePoint();
            //ResetTimeToGoToClosePoint();
            ResetTimeToCheckAvailableTargets();
            ResetTime_ChangePoint();

            fightInPointAct = controlledSoldier.gameObject.AddComponent<SoldierAction_FightInPoint>();
            fightInPointAct.Init(controlledSoldier);
            fightInPointAct.InitForFightPoint(selectedFightPoint);
            fightInPointAct.InitAIVoiceSitu(voiceSituation);
            fightInPointAct.initialEnemies = initialEnemies;

            if (shouldSetInitialTargForFightInPoint)
            {
                fightInPointAct.Init_StartInShootSituForTarget(initialTargetForFightInPoint);
                fightInPointAct.currentGunDirection = gunDirForFightInPointInitialTarget;
            }

            fightInPointAct.SetOwnerFightInRegAct(this);

            fightInPointAct.StartAct();

            if (nowReadyForGreandeLaunch)
            {
                fightInPointAct.SetItsNowReadyForLaunchGrenade();
            }

            SetCurrentAction(fightInPointAct);

            step = FightInRegStep.FightingInPoint02;
        }
        #endregion

        #region FightingInPoint02
        if (step == FightInRegStep.FightingInPoint02)
        {
            if (needsToBeFinished)
            {
                fightInPointAct.SetNeedsToBeFinished(evenStopMovingForFinish);
                step = FightInRegStep.Finishing01;
                goto StartSteps;
            }

            if (IsPlayerStillInCriticSitu())
            {
                time_CheckPlayerInCritStateWhileInFightInPoint_Counter = MathfPlus.DecByDeltatimeToZero(time_CheckPlayerInCritStateWhileInFightInPoint_Counter);

                if (time_CheckPlayerInCritStateWhileInFightInPoint_Counter <= 0)
                {
                    time_CheckPlayerInCritStateWhileInFightInPoint_Counter = time_CheckPlayerInCritStateWhileInFightInPoint_Max;

                    if (!CheckIsCurrentPointOkForPlayerCritState())
                    {
                        FightPoint newFP = SelectBestFightPointForPlayerCritState();

                        if (newFP != selectedFightPoint)
                        {
                            newFightPointToGo = newFP;

                            step = FightInRegStep.FightingInPoint_ChangePoint01;
                            goto StartSteps;
                        }
                    }
                }
            }
            else
            {
                bool newPointSelected = false;

                if (timeToCheckAvailableTargets_Counter == 0)
                {
                    ResetTimeToCheckAvailableTargets();

                    List<CharRayCastRsltForFightInPoInf> targsResults;

                    targsResults = mapLogic.GetAllAttackableEnemiesForListOfFightInfos(selectedFightPoint.fightInfos, controlledSoldier.gameObject, initialEnemies, selectedFightPoint.transform.position, selectedFightPoint.transform.rotation);

                    float sumOfRatings = 0;

                    sumOfRatings = mapLogic.GetSumOfRatings(targsResults);

                    if (sumOfRatings == 0)
                    {
                        FightPointWithRating newFPWithRating = SelectNewFightPointWithRating();

                        if (newFPWithRating != null
                           && newFPWithRating.fightPoint != null
                           && newFPWithRating.fightPoint != selectedFightPoint
                           && newFPWithRating.rating > sumOfRatings)
                        {
                            newPointSelected = true;
                            newFightPointToGo = newFPWithRating.fightPoint;

                            step = FightInRegStep.FightingInPoint_ChangePoint01;
                            goto StartSteps;
                        }
                    }
                }

                if (!newPointSelected)
                {
                    if (time_ChangePoint_Counter == 0)
                    {
                        ResetTime_ChangePoint();

                        List<FightPointWithRating> fightPointWithRatings = mapLogic.RateFightPointsAndSort(fightReg.GetFightPointsOfLane(soldInfo.curFightRegLaneNum), controlledSoldier.gameObject, initialEnemies);

                        if (fightPointWithRatings.Count == 1)
                        {
                            if (fightPointWithRatings[0].fightPoint != selectedFightPoint)
                            {
                                newPointSelected = true;
                                newFightPointToGo = fightPointWithRatings[0].fightPoint;

                                step = FightInRegStep.FightingInPoint_ChangePoint01;
                                goto StartSteps;
                            }
                        }

                        if (fightPointWithRatings.Count > 1)
                        {
                            if (fightPointWithRatings[0].fightPoint != selectedFightPoint)
                            {
                                newPointSelected = true;
                                newFightPointToGo = fightPointWithRatings[0].fightPoint;

                                step = FightInRegStep.FightingInPoint_ChangePoint01;
                                goto StartSteps;
                            }
                            else
                            {
                                fightPointWithRatings[0].rating *= (1 - Random.Range(0f, maxValueToDecreaseFromCurrentPointRating));

                                if (fightPointWithRatings[1].rating >= fightPointWithRatings[0].rating)
                                {
                                    if (fightPointWithRatings[1].rating > 0)
                                    {
                                        newPointSelected = true;
                                        newFightPointToGo = fightPointWithRatings[1].fightPoint;

                                        step = FightInRegStep.FightingInPoint_ChangePoint01;
                                        goto StartSteps;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region FightingInPoint_ChangePoint01
        if (step == FightInRegStep.FightingInPoint_ChangePoint01)
        {
            if (fightInPointAct != null && fightInPointAct.status != ActionStatusEnum.Finished)
                fightInPointAct.SetNeedsToBeFinished(evenStopMovingForFinish);

            step = FightInRegStep.FightingInPoint_ChangePoint02;
        }
        #endregion

        #region FightingInPoint_ChangePoint02
        if (step == FightInRegStep.FightingInPoint_ChangePoint02)
        {
            if ((fightInPointAct != null && fightInPointAct.status == ActionStatusEnum.Finished) || fightInPointAct == null)
            {
                if (fightInPointAct != null)
                    Destroy(fightInPointAct);

                SetCurrentAction(null);
                pre_FightInPoint = false;

                selectedFightPoint = newFightPointToGo;
                step = FightInRegStep.TryStartMoving01;
            }
        }
        #endregion

        #region Finishing01
        if (step == FightInRegStep.Finishing01)
        {
            if (currentAction == null)
            {
                SetFinished(true);
                step = FightInRegStep.Finishing02;
                goto StartSteps;
            }

            //<Alpha>
            if (needsToBeFinished)
            {
                currentAction.SetNeedsToBeFinished(evenStopMovingForFinish);
            }
            //</Alpha>

            if (currentAction.status == ActionStatusEnum.Finished)
            {
                Destroy(currentAction);
                SetCurrentAction(null);

                SetFinished(true);
                step = FightInRegStep.Finishing02;
                goto StartSteps;
            }
        }
        #endregion

        #region Camp01
        if (step == FightInRegStep.Camp01)
        {
            if (needsToBeFinished)
            {
                SetFinished(true);
                return;
            }

            campAct = controlledSoldier.gameObject.AddComponent<SoldierAction_Camp>();
            campAct.Init(controlledSoldier);

            if (campCurveInfos != null)
            {
                campAct.InitCampCurveInfos(campCurveInfos);
            }
            else
            {
                campAct.InitCampType(campInfo.campType);
            }

            campAct.StartAct();

            SetCurrentAction(campAct);

            step = FightInRegStep.Camp02;
        }
        #endregion

        #region Camp02
        if (step == FightInRegStep.Camp02)
        {
            if (needsToBeFinished)
            {
                campAct.SetNeedsToBeFinished(evenStopMovingForFinish);
                step = FightInRegStep.Finishing01;
                goto StartSteps;
            }

            if (campAct.playerHasBeenDetected)
            {
                SetPlayerIsDetectedInCampMode();
            }

            if (isPlayerDetectedInCampMode)
            {
                step = FightInRegStep.CampFinishingForStartingNormalMode01;
                goto StartSteps;
            }
        }
        #endregion

        #region CampFinishingForStartingNormalMode01
        if (step == FightInRegStep.CampFinishingForStartingNormalMode01)
        {
            campAct.SetNeedsToBeFinished(true);

            step = FightInRegStep.CampFinishingForStartingNormalMode02;
            goto StartSteps;
        }
        #endregion

        #region CampFinishingForStartingNormalMode02
        if (step == FightInRegStep.CampFinishingForStartingNormalMode02)
        {
            if (campAct == null || (campAct != null && campAct.status == ActionStatusEnum.Finished))
            {
                if (campAct != null)
                    Destroy(campAct);

                SetCurrentAction(null);
                step = FightInRegStep.TryStartMoving01;
            }
        }
        #endregion
    }

    bool IsSoldOnPoint(GameObject _soldier, Vector3 _pos, float _maxError)
    {
        return Vector3.Distance(_soldier.transform.position, _pos) <= _maxError;
    }

    bool NeedsDistantMoveToGetToPos(Vector3 _sourcePos, Vector3 _endPos)
    {
        //return (Vector3.Distance(_sourcePos, _endPos) > minDistForLongMoves);
        //<Temp>
        return true;
        //</Temp>
    }

    MovementTypeEnum GetMovementTypeForMovingToTarget(Vector3[] _path)
    {
        //<Temp>
        return MovementTypeEnum.RunFast;
        //</Temp>
    }

    //void SetMovementParams(MovementTypeEnum _type)
    //{
    //    switch (_type)
    //    {
    //        case MovementTypeEnum.RunFast:
    //            currentMovementEndAccTime = 0.6f;
    //            currentMovementEndAnimTime = 0.6f;
    //            currentMovementFirstAccTime = 0.6f;
    //            currentMovementFirstAnimTime = 0.6f;
    //            break;
    //    }

    //    movementAct.tFirstAcc = currentMovementFirstAccTime;
    //    movementAct.tEndAcc = currentMovementEndAccTime;
    //    movementAct.startMovingCrossfadeTime = currentMovementFirstAnimTime;
    //    movementAct.endMovingCrossfadeTime = currentMovementEndAnimTime;
    //}

    //void ResetDelayTimeToChangePoint()
    //{
    //    delayTimeToChangePoint_Counter = Random.Range(delayTimeToChangePoint_Min, delayTimeToChangePoint_Max);
    //}

    //void ResetTimeToGoToClosePoint()
    //{
    //    timeToGoToClosePoint_Counter = Random.Range(timeToGoToClosePoint_Min, timeToGoToClosePoint_Max);
    //}

    void ResetTimeToCheckAvailableTargets()
    {
        timeToCheckAvailableTargets_Counter = Random.Range(timeToCheckAvailableTargets_Min, timeToCheckAvailableTargets_Max);
    }

    void ResetTime_ChangePoint()
    {
        time_ChangePoint_Counter = Random.Range(time_ChangePoint_Min, time_ChangePoint_Max);
    }



    bool AreThesePointsClose(FightPoint fp1, FightPoint fp2)
    {
        return (Vector3.Distance(fp1.transform.position, fp2.transform.position) < minDistForLongMoves);
    }

    //Pre

    void DoPre_FightInPoint()
    {
        //timeToGoToClosePoint_Counter = MathfPlus.DecByDeltatimeToZero(timeToGoToClosePoint_Counter);

        timeToCheckAvailableTargets_Counter = MathfPlus.DecByDeltatimeToZero(timeToCheckAvailableTargets_Counter);

        time_ChangePoint_Counter = MathfPlus.DecByDeltatimeToZero(time_ChangePoint_Counter);
    }

    FightPointWithRating SelectNewFightPointWithRating()
    {
        FightPointWithRating fpWithRating = new FightPointWithRating();

        List<FightPointWithRating> fightPointWithRatings = mapLogic.RateFightPointsAndSort(fightReg.GetFightPointsOfLane(soldInfo.curFightRegLaneNum), controlledSoldier.gameObject, initialEnemies);

        if (fightPointWithRatings != null && fightPointWithRatings.Count > 0)
            return fightPointWithRatings[0];

        return fpWithRating;
    }

    void SetCurrentAction(SoldierAction _act)
    {
        currentAction = _act;
    }

    //bool IsSoldierOnAPoint(out FightPoint _point)
    //{
    //    foreach (FightPoint fp in fightReg.fightPoints)
    //    {
    //        if (IsSoldOnPoint(controlledSoldier.gameObject, fp.transform.position, SoldierStats.SoldierMaxErrorToPos))
    //        {
    //            _point = fp;
    //            return true;
    //        }
    //    }

    //    _point = null;
    //    return false;
    //}

    bool ShouldCheckPlayerCriticalSitu()
    {
        if (!soldInfo.ShouldCheckPlayerCritSitu_Generally())
            return false;

        if (initialEnemies != null && initialEnemies.Count > 0 && !initialEnemies.Contains(mapLogic.player))
            return false;

        return shouldConsiderPlayerCriticalSituation;
    }

    bool IsPlayerStillInCriticSitu()
    {
        return soldInfo.IsPlayerStillInCriticalState();
    }

    bool SlowlyCheckPlayerCritStateWhileMoving()
    {
        time_CheckPlayerCritStateWhileMoving_Counter = MathfPlus.DecByDeltatimeToZero(time_CheckPlayerCritStateWhileMoving_Counter);

        if (time_CheckPlayerCritStateWhileMoving_Counter == 0)
        {
            time_CheckPlayerCritStateWhileMoving_Counter = time_CheckPlayerCritStateWhileMoving_Max;

            if (IsPlayerStillInCriticSitu())
            {
                if ((movementAct.remainingPathLength / movementAct.vMid) > minMoveTimeForPlayerCriticalFight)
                {
                    if (soldInfo.IsPlayerInView() && soldInfo.ArePlayerSidesInView())
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    //bool SlowlyCheckPlayerInCritStateWhileInFightInPos()
    //{
    //    time_CheckPlayerInCritStateWhileInFightInPos_Counter = MathfPlus.DecByDeltatimeToZero(time_CheckPlayerInCritStateWhileInFightInPos_Counter);

    //    if (time_CheckPlayerInCritStateWhileInFightInPos_Counter <= 0)
    //    {
    //        if (soldInfo.IsCharInViewAndTargettable(mapLogic.player))
    //            return true;
    //    }

    //    return false;
    //}

    bool CheckIsCurrentPointOkForPlayerCritState()
    {
        bool isThisFightPointStillOk = false;

        foreach (SoldierFightInPointInfo fipi in selectedFightPoint.fightInfos)
        {
            SoldierFightInPointInfo sfi = fipi;

            Vector3 rayCastPos = SoldierStats.GetShootingRaycastPos(controlledSoldier.position, selectedFightPoint.transform.rotation, sfi.GetRaycastOffsetForGun(soldInfo.gun.name));

            CharRaycastResult charRayRes;

            if (mapLogic.IsCharacterOkAsTarget(controlledSoldier.gameObject, mapLogic.player, rayCastPos, selectedFightPoint.transform.rotation, soldInfo.fightRange, sfi.shootingStartAngle, sfi.shootingEndAngle, out charRayRes))
            {
                isThisFightPointStillOk = true;
                break;
            }

            if (fipi.playerCriticalFightInfoInCover != null)
            {
                sfi = fipi.playerCriticalFightInfoInCover;

                rayCastPos = SoldierStats.GetShootingRaycastPos(controlledSoldier.position, selectedFightPoint.transform.rotation, sfi.GetRaycastOffsetForGun(soldInfo.gun.name));

                if (mapLogic.IsCharacterOkAsTarget(controlledSoldier.gameObject, mapLogic.player, rayCastPos, selectedFightPoint.transform.rotation, soldInfo.fightRange, sfi.shootingStartAngle, sfi.shootingEndAngle, out charRayRes))
                {
                    isThisFightPointStillOk = true;
                    break;
                }
            }

            if (fipi.playerCriticalFightInfoInShoot != null)
            {
                sfi = fipi.playerCriticalFightInfoInShoot;

                rayCastPos = SoldierStats.GetShootingRaycastPos(controlledSoldier.position, selectedFightPoint.transform.rotation, sfi.GetRaycastOffsetForGun(soldInfo.gun.name));

                if (mapLogic.IsCharacterOkAsTarget(controlledSoldier.gameObject, mapLogic.player, rayCastPos, selectedFightPoint.transform.rotation, soldInfo.fightRange, sfi.shootingStartAngle, sfi.shootingEndAngle, out charRayRes))
                {
                    isThisFightPointStillOk = true;
                    break;
                }
            }
        }

        return isThisFightPointStillOk;
    }

    //<Beta>  <------------- Shit (Don't forget to consider fight point's rating coef) (If needed!)
    FightPoint SelectBestFightPointForPlayerCritState()
    {
        List<FightPoint> selectedFightPoints = new List<FightPoint>();
        List<float> selectedFightPoints_Ratings = new List<float>();

        List<FightPoint> allFightPoints = new List<FightPoint>();
        List<float> allFightPoints_Dists = new List<float>();


        foreach (FightPoint fp in fightReg.fightPoints)
        {
            List<CharRaycastResult> charResults = mapLogic.GetAttackableEnemiesForFightInfo(fp.defaultfightInfo.playerCriticalFightInfoInCover, controlledSoldier.gameObject, initialEnemies, fp.transform.position, fp.transform.rotation);

            float dist = Vector3.Distance(controlledSoldier.transform.position, fp.transform.position);

            allFightPoints.Add(fp);

            allFightPoints_Dists.Add(dist);

            if (charResults != null && charResults.Count > 0)
            {
                float rating = 0;

                float numToAddRating = 0;

                for (int i = 0; i < charResults.Count; i++)
                {
                    if (charResults[i].character == mapLogic.player)
                    {
                        if (charResults[i].isCharacterHitted)
                        {
                            numToAddRating = 1;
                            break;
                        }

                        if (charResults[i].isHaloHitted)
                        {
                            numToAddRating = 0.7f;
                            break;
                        }
                    }
                }

                rating += numToAddRating;

                if (rating > 0)
                {
                    if (dist == 0)
                        return fp;

                    rating *= 20 / dist;

                    selectedFightPoints.Add(fp);
                    selectedFightPoints_Ratings.Add(rating);
                }
            }
        }

        int selectedIndex = -1;
        float selectedValue = float.NegativeInfinity;

        if (selectedFightPoints.Count > 0)
        {
            for (int i = 0; i < selectedFightPoints.Count; i++)
            {
                if (selectedFightPoints_Ratings[i] > selectedValue)
                {
                    selectedValue = selectedFightPoints_Ratings[i];
                    selectedIndex = i;
                }
            }

            if (selectedIndex >= 0)
                return selectedFightPoints[selectedIndex];
        }

        selectedIndex = -1;
        selectedValue = float.PositiveInfinity;

        if (allFightPoints.Count > 0)
        {
            for (int i = 0; i < allFightPoints.Count; i++)
            {
                if (allFightPoints_Dists[i] < selectedValue)
                {
                    selectedValue = allFightPoints_Dists[i];
                    selectedIndex = i;
                }
            }

            if (selectedIndex >= 0)
                return allFightPoints[selectedIndex];
        }

        return fightReg.fightPoints[Random.Range(0, fightReg.fightPoints.Length)];
    }
    //</Beta>

    FightPoint SelectNewFightPoint()
    {
        if (needsToGoToInitialPoint)
            return initialPoint;

        FightPoint fp = mapLogic.RateFightPointsAndSort(fightReg.GetFightPointsOfLane(soldInfo.curFightRegLaneNum), controlledSoldier.gameObject, initialEnemies)[0].fightPoint;
        return fp;
    }

    GameObject SelectBestTargetForPoint(FightPoint _fp)
    {
        GameObject result = null;

        List<CharRaycastResult> rayCastResults = mapLogic.GetAttackableEnemiesForFightInfo(_fp.defaultfightInfo, controlledSoldier.gameObject, initialEnemies, _fp.transform.position, _fp.transform.rotation);

        foreach (CharRaycastResult rayCastRes in rayCastResults)
        {
            if (rayCastRes == null)
                continue;

            if (rayCastRes.isCharacterHitted && rayCastRes.character != null)
            {
                result = rayCastRes.character;
                break;
            }
        }

        return result;
    }

    public void SetOwnerLogicJob(MapLogicJob_FightInReg _owner)
    {
        ownerLogicJob = _owner;
    }

    public void SetItsNowReadyForLaunchGrenade()
    {
        nowReadyForGreandeLaunch = true;

        if (fightInPointAct != null)
        {
            fightInPointAct.SetItsNowReadyForLaunchGrenade();
        }
    }

    public void SetGrenadeIsLaunchedNow()
    {
        if (!nowReadyForGreandeLaunch)
            return;

        nowReadyForGreandeLaunch = false;

        if (ownerLogicJob != null)
        {
            ownerLogicJob.SetGrenadeIsLaunchedNow();
        }
    }

    bool ShouldStartCampMode()
    {
        if (isPlayerDetectedInCampMode)
            return false;

        if (campCurveInfos != null)
            return true;

        if (campInfo != null)
            return true;

        return false;
    }

    void SetPlayerIsDetectedInCampMode()
    {
        isPlayerDetectedInCampMode = true;
    }
}
