using UnityEngine;
using System.Collections;

public class CutsceneSoldierAct
{

    enum StepEnum
    {
        Start01,
        UseCurves01,
        UseCurves02,
        UseCurves03,
    }

    StepEnum step;

    SoldierCampInfo camp_Info;
    AnimsList anims;
    string selectedAnim;

    float animToAnimCampCrossfadeTime = 0.4f;

    CampCurveInfo[] curveInfos;

    float curveThreshold = 0.001f;

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

    float curCurveDelayCoefForStartAnimation_Min = 0;
    float curCurveDelayCoefForStartAnimation_Max = 0;

    float stayPointRotLerpSpeed = 1f;
    float stayPointPosLerpSpeed = 0.5f;

    bool shouldUpdateAct = false;

    Transform controlledSoldier;

    float height=2.2f;

    string mainAnim;

    //-----------------------------------------------------------------------

    public void InitCampCurveInfos(CampCurveInfo[] _camp_Curves)
    {
        curveInfos = _camp_Curves;

        if (curveInfos == null || (curveInfos != null && curveInfos.Length == 0))
        {
            Debug.LogError("No camp curve info is added to CUTSCENE sold act!");
        }
    }

    //

    public void Init(Transform contSoldier)
    {
        controlledSoldier = contSoldier;
    }

    public void StartAct()
    {
        step = StepEnum.Start01;

        shouldUpdateAct = true;
    }

    public void UpdateAct()
    {

    Start:

        #region Start01
        if (step == StepEnum.Start01)
        {
            curCurveInfoIndex = 0;
            step = StepEnum.UseCurves01;
        }
        #endregion

        #region UseCurves01
        if (step == StepEnum.UseCurves01)
        {
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

            curCurveDelayCoefForStartAnimation_Min = curCurveInfo.delayCoefForStartAnimation_Min;
            curCurveDelayCoefForStartAnimation_Max = curCurveInfo.delayCoefForStartAnimation_Max;

            nextCurveInfoIndex = GetNextCurveInfoIndex();


            curCurveInfoCounter = 0;
            curCurve_CurSpeed = 0;

            if (!nextAnimIsRanBefore)
            {
                StartNewMainAnimWithCrossfadeTime(selectedAnim, curCurveCFTime, curCurveDelayCoefForStartAnimation_Min,
                                                        curCurveDelayCoefForStartAnimation_Max);
            }

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
            curCurveInfoIndex = nextCurveInfoIndex;

            step = StepEnum.UseCurves01;
            goto Start;
        }
        #endregion

    Finish:
        return;
    }

    void Update()
    {
        if (shouldUpdateAct)
        {
            UpdateAct();
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

                StartNewMainAnimWithCrossfadeTime(nextAnim, nextAnimCFTime);
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

                StartNewMainAnimWithCrossfadeTime(nextAnim, nextAnimCFTime);
            }
        }
    }

    //

    int GetNextCurveInfoIndex()
    {
        int nextIndex = curCurveInfoIndex + 1;

        if (nextIndex >= curveInfos.Length)
        {
            nextIndex = 0;
        }

        return nextIndex;
    }

    void StartNewMainAnimWithCrossfadeTime(string _anim, float _startCrossfadeTime, float _newAnimStartTimeCoefMin, float _newAnimStartTimeCoefMax)
    {
        float startMin = _newAnimStartTimeCoefMin;
        float startMax = _newAnimStartTimeCoefMax;

        float startTimeCoef = Random.Range(startMin, startMax);

        float cfTime = _startCrossfadeTime;
        mainAnim = _anim;

        float startTime = controlledSoldier.gameObject.animation[mainAnim].length * startTimeCoef;

        controlledSoldier.gameObject.animation[mainAnim].time = startTime;
        controlledSoldier.gameObject.animation.CrossFade(mainAnim, cfTime);
    }

    void StartNewMainAnimWithCrossfadeTime(string _anim, float _startCrossfadeTime)
    {
        StartNewMainAnimWithCrossfadeTime(_anim, _startCrossfadeTime, 0, 0);
    }

    float GetGroundY()
    {
        Vector3 raycastDirection = new Vector3(0, -1000, 0);
        float rayDirMag = raycastDirection.magnitude;
        float range = rayDirMag;

        Vector3 raycastStart = controlledSoldier.position + new Vector3(0, height, 0);

        RaycastHit hit;

        if (Physics.Raycast(raycastStart, raycastDirection, out hit, range, GameGeneralInfo.Instance.OnlyGroundLayer))
        {
            return hit.point.y;
        }

        return -1000;
    }

    public void SetFinished()
    {

    }
}
