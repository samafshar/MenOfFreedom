using UnityEngine;
using System.Collections;

public enum SoldierCampType
{
    _NotUsed_,
    Sigar_A,
    Waiting_A,
    Waiting_B,
    Waiting_C,
    Waiting_D,
    Chatting_A,
    Chatting_B,
    Kamiun_A,
}

public class SoldierAction_Camp : SoldierAction
{
    enum StepEnum
    {
        Start01,
        Update01,
        UseCurves01,
        UseCurves02,
        UseCurves03,
    }

    //
    //float gravity = -200;

    StepEnum step;

    SoldierCampInfo camp_Info;
    AnimsList anims;
    string selectedAnim;

    float animToAnimCampCrossfadeTime = 0.4f;

    float playerDetectionCounter = 0.33f;
    float playerDetectionTimeMaxCoef = 1.28f;
    float playerDetectionTimeCounterBaseSpeed = 0.92f;

    [HideInInspector]
    public bool playerHasBeenDetected = false;

    bool useCurves = false;
    CampCurveInfo[] curveInfos;

    float curveThreshold = 0.03f;

    int curCurveInfoIndex = 0;
    int nextCurveInfoIndex = 0;
    bool nextAnimIsRanBefore = false;
    string nextAnim = "";
    float nextAnimCFTime = 0;
    float curCurveInfoCounter = 0;
    CampCurveInfo curCurveInfo;
    AntaresBezierCurve curAntaresCurve;
    StartPoint curCurvePointToStay = null;
    float curCurveCFTime = 0;
    float curCurveMaxSpeed = 0;
    float curCurveEndMinSpeed = 0;
    float curCurve_SpeedInc_Speed = 0;
    float curCurve_SpeedDec_Speed = 0;
    float curCurveEndLengthToDecSpeed = 0;
    float curCurvePointStayTime = 0;
    float curCurve_CurSpeed = 0;
    float curCurveEndLengthToChangeAnim = 0;
    bool isFirstTickOnCurveInfo = false;

    float stayPointRotLerpSpeed = 1f;
    float stayPointPosLerpSpeed = 0.5f;

    bool otherSourcePlayerDetectionionProcessStarted = false;
    bool otherSourcePlayerDetectionionProcessFinished = false;

    float otherSourcePlayerDetectionTimeCounter;
    float otherSourcePlayerDetectionMaxTimeBaseAmount_Min = 0.55f;
    float otherSourcePlayerDetectionMaxTimeBaseAmount_Max = 0.95f;
    float otherSourcePlayerDetectionMaxTimeCoefOfDist = 0.0075f;

    //-----------------------------------------------------------------------

    public void InitCampType(SoldierCampType _camp_Type)
    {
        camp_Info = soldInfo.soldierGeneralInfo.GetCampInfoByType(_camp_Type);
        useCurves = false;
        anims = camp_Info.anims;
        animToAnimCampCrossfadeTime = camp_Info.defaultCrossfadeTime;
    }

    public void InitCampCurveInfos(CampCurveInfo[] _camp_Curves)
    {
        useCurves = true;

        curveInfos = _camp_Curves;

        if (curveInfos == null || (curveInfos != null && curveInfos.Length == 0))
        {
            Debug.LogError("No camp curve info is added to camp info!");
        }
    }

    //

    public override void Init(Transform contSoldier)
    {
        base.Init(contSoldier);
    }

    public override void StartAct()
    {
        base.StartAct();
        step = StepEnum.Start01;
    }

    public override void UpdateAct()
    {
        base.UpdateAct();

        if (!otherSourcePlayerDetectionionProcessFinished)
        {
            if (mapLogic.IsPlayerDetectedInCampMode())
            {
                if (!otherSourcePlayerDetectionionProcessStarted)
                {
                    otherSourcePlayerDetectionionProcessStarted = true;

                    float distToPlayer = (PlayerCharacterNew.Instance.gameObject.transform.position - gameObject.transform.position).magnitude;

                    otherSourcePlayerDetectionTimeCounter = Random.Range(otherSourcePlayerDetectionMaxTimeBaseAmount_Min, otherSourcePlayerDetectionMaxTimeBaseAmount_Max) + otherSourcePlayerDetectionMaxTimeCoefOfDist * distToPlayer;
                }
                else
                {
                    otherSourcePlayerDetectionTimeCounter = MathfPlus.DecByDeltatimeToZero(otherSourcePlayerDetectionTimeCounter);

                    if (otherSourcePlayerDetectionTimeCounter == 0)
                    {
                        SetPlayerHasBeenDetected(false);

                        otherSourcePlayerDetectionionProcessFinished = true;
                    }
                }
            }
        }

    Start:

        #region Start01
        if (step == StepEnum.Start01)
        {
            if (needsToBeFinished)
            {
                SetFinished(false);
                return;
            }

            if (useCurves)
            {
                curCurveInfoIndex = 0;

                step = StepEnum.UseCurves01;
            }
            else
            {
                selectedAnim = anims.GetRandomAnimName();

                soldInfo.StartNewMainAnimWithCrossfadeTime(selectedAnim, animToAnimCampCrossfadeTime);
                step = StepEnum.Update01;
            }
        }
        #endregion

        #region Update01
        if (step == StepEnum.Update01)
        {
            if (needsToBeFinished)
            {
                SetFinished(false);
                return;
            }

            if (IsPlayerInView())
            {
                DecPlayerDectectionCounter();
            }
            else
            {
                ResetPlayerDectectionCounter();
            }

            if (IsPlayerDetected())
            {
                SetPlayerHasBeenDetected(true);
            }

            if (IsNearLoudSoundsHappened())
            {
                SetPlayerHasBeenDetected(true);
            }
        }
        #endregion

        #region UseCurves01
        if (step == StepEnum.UseCurves01)
        {
            if (needsToBeFinished)
            {
                SetFinished(false);
                return;
            }

            curCurveInfo = curveInfos[curCurveInfoIndex];
            curAntaresCurve = curCurveInfo.curve;
            selectedAnim = curCurveInfo.animsList.GetRandomAnimName();
            curCurveCFTime = curCurveInfo.crossfadeTime;
            curCurveMaxSpeed = curCurveInfo.speed;
            curCurvePointToStay = curCurveInfo.pointToStay;
            curCurveEndMinSpeed = curCurveInfo.endMinSpeed;
            curCurveEndLengthToDecSpeed = curCurveInfo.endLengthToStartDecreasingSpeed;
            curCurve_SpeedInc_Speed = curCurveInfo.speedInc_Speed;
            curCurve_SpeedDec_Speed = curCurveInfo.speedDec_Speed;
            curCurvePointStayTime = curCurveInfo.pointStayTime;
            curCurveEndLengthToChangeAnim = curCurveInfo.endLengthToChangeAnim;

            nextCurveInfoIndex = GetNextCurveInfoIndex();


            curCurveInfoCounter = 0;
            curCurve_CurSpeed = 0;

            if (!nextAnimIsRanBefore)
                soldInfo.StartNewMainAnimWithCrossfadeTime(selectedAnim, curCurveCFTime);

            nextAnimIsRanBefore = false;
            nextAnim = "";
            nextAnimCFTime = 0;

            isFirstTickOnCurveInfo = true;

            step = StepEnum.UseCurves02;
        }
        #endregion

        #region UseCurves02
        if (step == StepEnum.UseCurves02)
        {
            if (needsToBeFinished)
            {
                SetFinished(false);
                return;
            }

            if (IsPlayerInView())
            {
                DecPlayerDectectionCounter();
            }
            else
            {
                ResetPlayerDectectionCounter();
            }

            if (IsPlayerDetected())
            {
                SetPlayerHasBeenDetected(true);
            }

            if (IsNearLoudSoundsHappened())
            {
                SetPlayerHasBeenDetected(true);
            }

            //

            if (curCurvePointToStay)
            {
                IncreasePointStayTimeCounter();

                SetPosAndRotByStayPoint();

                CheckIfNeedsStartNextAnimInStayMode();

                if (IsPointStayTimeFinished())
                {
                    step = StepEnum.UseCurves03;
                    goto Finish;
                }
            }
            else
            {
                if (IsSoldierReachedToTheEndMinSpeedArea())
                {
                    DecCurveInfoCurrentSpeedForEnd();
                }
                else
                {
                    IncCurveInfoCurrentSpeed();
                }

                IncreaseCurCurveCounterByCurrentSpeed();

                SetPosAndRotByCurCurve();

                CheckIfNeedsStartNextAnimInCurveMode();

                if (IsCurveCounterReachedToEnd())
                {
                    step = StepEnum.UseCurves03;
                    goto Start;
                }
            }

            isFirstTickOnCurveInfo = false;
        }
        #endregion

        #region UseCurves03
        if (step == StepEnum.UseCurves03)
        {

            if (needsToBeFinished)
            {
                SetFinished(false);
                return;
            }

            if (IsPlayerInView())
            {
                DecPlayerDectectionCounter();
            }
            else
            {
                ResetPlayerDectectionCounter();
            }

            if (IsPlayerDetected())
            {
                SetPlayerHasBeenDetected(true);
            }

            if (IsNearLoudSoundsHappened())
            {
                SetPlayerHasBeenDetected(true);
            }

            //

            curCurveInfoIndex = nextCurveInfoIndex;

            step = StepEnum.UseCurves01;
            goto Start;
        }
        #endregion

    Finish:
        return;
    }

    bool IsPlayerInView()
    {
        return soldInfo.IsPlayerInView_ForCamp();
    }

    void ResetPlayerDectectionCounter()
    {
        float viewRange = mapLogic.camp_CurUpdate_SoldsViewRange;
        float minTime = SoldierStats.campSoldier_farViewMinDetectTime;
        float maxTime = SoldierStats.campSoldier_farViewMaxDetectTime;

        float distToPlayer = (PlayerCharacterNew.Instance.gameObject.transform.position - gameObject.transform.position).magnitude;
        distToPlayer = Mathf.Clamp(distToPlayer, 0, viewRange);

        float newTime = minTime + (distToPlayer / viewRange) * (maxTime - minTime);

        playerDetectionCounter = newTime;
    }

    void DecPlayerDectectionCounter()
    {
        float viewRange = mapLogic.camp_CurUpdate_SoldsViewRange;

        float distToPlayer = (PlayerCharacterNew.Instance.gameObject.transform.position - gameObject.transform.position).magnitude;
        distToPlayer = Mathf.Clamp(distToPlayer, 0, viewRange);

        playerDetectionCounter = MathfPlus.DecByDeltatimeToZeroWithAdditionalCoef(playerDetectionCounter, playerDetectionTimeCounterBaseSpeed + (playerDetectionTimeMaxCoef - 1) * (1 - (distToPlayer / viewRange)));
    }

    bool IsPlayerDetected()
    {
        if (!IsPlayerInView())
            return false;

        if (playerDetectionCounter > 0)
            return false;

        return true;
    }

    void SetPlayerHasBeenDetected(bool _playEnemyDetectedVoice)
    {
        bool playEnemyDetectedVoice = _playEnemyDetectedVoice;

        playerHasBeenDetected = true;

        mapLogic.SetPlayerIsDetectedInCampMode();

        if (playEnemyDetectedVoice)
        {
            PlayEnemyDetectedVoice();
        }
    }

    //

    void IncreaseCurCurveCounterByCurrentSpeed()
    {
        curCurveInfoCounter += Time.deltaTime * curCurve_CurSpeed;
        curCurveInfoCounter = Mathf.Clamp(curCurveInfoCounter, 0f, curAntaresCurve.Length - curveThreshold);
    }

    bool IsCurveCounterReachedToEnd()
    {
        return curCurveInfoCounter >= curAntaresCurve.Length - curveThreshold * 1.01f;
    }

    void SetPosAndRotByCurCurve()
    {
        if (!GameController.isGamePaused && Time.deltaTime != 0)
        {
            Vector3 interpolatedPoint = curAntaresCurve.GetInterpolatedPoint(curCurveInfoCounter);

            Vector3 destRot = interpolatedPoint - controlledSoldier.position;

            if (IsCurveCounterReachedToEnd())
            {
                Vector3 endInterPoint = curAntaresCurve.GetInterpolatedPoint(curCurveInfoCounter + curveThreshold);
                destRot = endInterPoint - controlledSoldier.position;
            }

            float deltaAngle = Mathf.DeltaAngle(
                MathfPlus.HorizontalAngle(controlledSoldier.forward),
                MathfPlus.HorizontalAngle(destRot));

            float rotAmount = deltaAngle;

            Quaternion rotVec = Quaternion.Euler(0, rotAmount, 0);

            if (!isFirstTickOnCurveInfo)
                controlledSoldier.rotation *= rotVec;

            //controlledSoldier.position = new Vector3(interpolatedPoint.x, controlledSoldier.position.y, interpolatedPoint.z);
            controlledSoldier.position = new Vector3(interpolatedPoint.x, GetGroundY(), interpolatedPoint.z);
        }

    }

    void IncCurveInfoCurrentSpeed()
    {
        curCurve_CurSpeed += Time.deltaTime * curCurve_SpeedInc_Speed;
        curCurve_CurSpeed = Mathf.Clamp(curCurve_CurSpeed, 0, curCurveMaxSpeed);
    }

    void DecCurveInfoCurrentSpeedForEnd()
    {
        curCurve_CurSpeed -= Time.deltaTime * curCurve_SpeedDec_Speed;
        curCurve_CurSpeed = Mathf.Clamp(curCurve_CurSpeed, curCurveEndMinSpeed, curCurveMaxSpeed);
    }

    bool IsSoldierReachedToTheEndMinSpeedArea()
    {
        return curCurveInfoCounter >= curAntaresCurve.Length - curCurveEndLengthToDecSpeed;
    }

    void CheckIfNeedsStartNextAnimInCurveMode()
    {
        if (curCurveEndLengthToChangeAnim > 0 && !nextAnimIsRanBefore)
        {
            if (curCurveInfoCounter >= (curAntaresCurve.Length - curCurveEndLengthToChangeAnim))
            {
                nextAnimIsRanBefore = true;

                nextAnim = curveInfos[nextCurveInfoIndex].animsList.GetRandomAnimName();
                nextAnimCFTime = curveInfos[nextCurveInfoIndex].crossfadeTime;

                soldInfo.StartNewMainAnimWithCrossfadeTime(nextAnim, nextAnimCFTime);
            }
        }
    }

    //

    void IncreasePointStayTimeCounter()
    {
        curCurveInfoCounter += Time.deltaTime;
        curCurveInfoCounter = Mathf.Clamp(curCurveInfoCounter, 0f, curCurvePointStayTime);
    }

    bool IsPointStayTimeFinished()
    {
        return curCurveInfoCounter == curCurvePointStayTime;
    }

    void SetPosAndRotByStayPoint()
    {
        //controlledSoldier.rotation = Quaternion.Lerp(controlledSoldier.rotation, curCurvePointToStay.transform.rotation, stayPointRotLerpSpeed*Time.deltaTime);
        controlledSoldier.rotation = curCurvePointToStay.transform.rotation;
        controlledSoldier.position = new Vector3(curCurvePointToStay.transform.position.x, GetGroundY(), curCurvePointToStay.transform.position.z);
        //controlledSoldier.position = Vector3.Lerp(controlledSoldier.position, new Vector3(curCurvePointToStay.transform.position.x, GetGroundY(), curCurvePointToStay.transform.position.z), stayPointPosLerpSpeed * Time.deltaTime);
    }

    void CheckIfNeedsStartNextAnimInStayMode()
    {
        if (curCurveEndLengthToChangeAnim > 0 && !nextAnimIsRanBefore)
        {
            if (curCurveInfoCounter >= (curCurvePointStayTime - curCurveEndLengthToChangeAnim))
            {
                nextAnimIsRanBefore = true;

                nextAnim = curveInfos[nextCurveInfoIndex].animsList.GetRandomAnimName();
                nextAnimCFTime = curveInfos[nextCurveInfoIndex].crossfadeTime;

                soldInfo.StartNewMainAnimWithCrossfadeTime(nextAnim, nextAnimCFTime);
            }
        }
    }

    //

    bool IsNearLoudSoundsHappened()
    {
        if (playerCharNew.camp_DidPlayerMakeALoudMovingSound || playerCharNew.camp_DidPlayerMakeALoudLandingSound)
        {
            if (soldInfo.IsPlayerInView_ConsideringCampWall())
            {
                float distToPlayer = Vector3.Magnitude(playerObj.transform.position - controlledSoldier.position);

                if (playerCharNew.camp_DidPlayerMakeALoudLandingSound)
                {
                    if (distToPlayer < SoldierStats.campSoldier_PlayerLoudLandingSoundRange)
                        return true;
                }

                if (playerCharNew.camp_DidPlayerMakeALoudMovingSound)
                {
                    if (distToPlayer < SoldierStats.campSoldier_PlayerLoudMovingSoundRange)
                        return true;
                }
            }
        }

        return false;
    }

    int GetNextCurveInfoIndex()
    {
        int nextIndex = curCurveInfoIndex + 1;

        if (nextIndex >= curveInfos.Length)
        {
            nextIndex = 0;
        }

        return nextIndex;
    }

    float GetGroundY()
    {
        return soldInfo.GetGroundY();
    }

    void PlayEnemyDetectedVoice()
    {
        soldInfo.Talk_Camp_EnemyDetected();
    }
}
