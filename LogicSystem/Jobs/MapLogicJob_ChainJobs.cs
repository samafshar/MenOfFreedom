//<91-04-12>

using UnityEngine;
using System.Collections;

public class MapLogicJob_ChainJobs : MapLogicJob
{
    public ChainJobs_LogGlobStepInfo[] logGlobStepInfos;

    public GameObject initialSoldier;

    //

    int globalStepInfo_CurrentIndex = -1;

    int globalStepInfo_NewIndex = -1;

    int globalStepInfo_CurrentJobIndex = 0;


    ChainJobs_LogGlobStepInfo curGlobalStepInfo;

    MapLogicJob curGlobalStepInfo_CurJob = null;

    //

    public void Init_SetNewGlobalLogicIndex(int _newgGobalLogicIndex)
    {
        if (_newgGobalLogicIndex != globalStepInfo_NewIndex)
        {
            globalStepInfo_NewIndex = _newgGobalLogicIndex;

            if (globalStepInfo_CurrentIndex < 0)
            {
                SetCurrentGlobalStepInfo(globalStepInfo_NewIndex);
            }
        }
    }

    void SetCurrentGlobalStepInfo(int _globStepInfIndex)
    {
        globalStepInfo_CurrentIndex = _globStepInfIndex;
        curGlobalStepInfo = logGlobStepInfos[globalStepInfo_CurrentIndex];

        SetCurrentJob(0);
    }

    void SetCurrentJob(int _jobIndex)
    {
        globalStepInfo_CurrentJobIndex = _jobIndex;
        curGlobalStepInfo_CurJob = curGlobalStepInfo.jobsForThisStep[globalStepInfo_CurrentJobIndex];
    }

    //

    public override void StartIt()
    {
        base.StartIt();

        if (initialSoldier != null)
            Init_SetControlledSoldier(initialSoldier);

        foreach (ChainJobs_LogGlobStepInfo inf in logGlobStepInfos)
        {
            foreach (MapLogicJob job in inf.jobsForThisStep)
            {
                job.Init_SetControlledSoldier(controlledSoldier);
            }
        }
    }

    public override void RunIt()
    {
        base.RunIt();

    StartSteps:

        if (step == 1)
        {
            if (needsToBeFinished)
            {
                SetFinished(true);
                return;
            }

            SetCurrentJob(globalStepInfo_CurrentJobIndex);
            curGlobalStepInfo_CurJob.StartIt();

            SetStep(2);
        }

        if (step == 2)
        {
            curGlobalStepInfo_CurJob.RunIt();

            if (needsToBeFinished)
            {
                if (curGlobalStepInfo_CurJob == null)
                {
                    SetFinished(true);
                    return;
                }

                if (curGlobalStepInfo_CurJob.status == LogicJobStatus.Finished)
                {
                    SetFinished(true);
                    return;
                }

                SetChildNeedsToBeFinished(curGlobalStepInfo_CurJob);

                SetStep(3);
                goto EndSteps;
            }

            if (globalStepInfo_NewIndex > globalStepInfo_CurrentIndex)
            {
                if (curGlobalStepInfo_CurJob == null)
                {
                    SetStep(4);
                    goto StartSteps;
                }

                if (logGlobStepInfos[globalStepInfo_NewIndex].jobsForThisStep[0] == curGlobalStepInfo_CurJob)
                {
                    SetCurrentGlobalStepInfo(globalStepInfo_NewIndex);
                }
                else
                {
                    if (curGlobalStepInfo_CurJob.status == LogicJobStatus.Finished)
                    {
                        SetStep(4);
                        goto StartSteps;
                    }

                    curGlobalStepInfo_CurJob.SetNeedsToBeFinished();

                    //SetChildNeedsToBeFinished(curGlobalStepInfo_CurJob);

                    SetStep(3);
                    goto EndSteps;
                }
            }

            if (curGlobalStepInfo_CurJob.status == LogicJobStatus.Finished)
            {
                if (globalStepInfo_CurrentJobIndex < curGlobalStepInfo.jobsForThisStep.Length - 1)
                {
                    globalStepInfo_CurrentJobIndex++;
                    SetStep(1);
                }
            }
        }

        if (step == 3)
        {
            //<Alpha>
            if (needsToBeFinished)
            {
                SetChildNeedsToBeFinished(curGlobalStepInfo_CurJob);
            }
            //</Alpha>

            curGlobalStepInfo_CurJob.RunIt();

            if (curGlobalStepInfo_CurJob.status == LogicJobStatus.Finished)
            {
                if (needsToBeFinished)
                {
                    SetFinished(true);
                    return;
                }

                step = 4;
            }
        }

        if (step == 4)
        {
            SetCurrentGlobalStepInfo(globalStepInfo_NewIndex);
            step = 1;
        }

    EndSteps:
        ;
    }
}
