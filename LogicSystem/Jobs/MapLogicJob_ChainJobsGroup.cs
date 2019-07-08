using UnityEngine;
using System.Collections;

public class MapLogicJob_ChainJobsGroup : MapLogicJob
{
    public MapLogicJob_ChainJobs[] chainJobs;

    public void Init_SetNewGlobalLogicIndex(int _newgGobalLogicIndex)
    {
        foreach (MapLogicJob_ChainJobs cj in chainJobs)
        {
            cj.Init_SetNewGlobalLogicIndex(_newgGobalLogicIndex);
        }
    }

    public override void StartIt()
    {
        base.StartIt();

        foreach (MapLogicJob_ChainJobs cj in chainJobs)
        {
            cj.StartIt();
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
                step = 2;

                goto StartSteps;
            }

            foreach (MapLogicJob_ChainJobs cj in chainJobs)
            {
                cj.RunIt();
            }
        }

        if (step == 2)
        {
            foreach (MapLogicJob_ChainJobs cj in chainJobs)
            {
                SetChildNeedsToBeFinished(cj);
            }

            step = 2.1f;
        }

        if (step == 2.1f)
        {
            //<Alpha>
            if (needsToBeFinished)
            {
                foreach (MapLogicJob_ChainJobs cj in chainJobs)
                {
                    SetChildNeedsToBeFinished(cj);
                }
            }
            //</Alpha>

            foreach (MapLogicJob_ChainJobs cj in chainJobs)
            {
                cj.RunIt();
            }

            bool allFinished = true;

            foreach (MapLogicJob_ChainJobs cj in chainJobs)
            {
                if (cj.status != LogicJobStatus.Finished)
                {
                    allFinished = false;
                    break;
                }
            }

            if (allFinished)
            {
                SetFinished(true);
                return;
            }
        }
    }
}
