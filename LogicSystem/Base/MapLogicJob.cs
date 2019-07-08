//<91-04-12>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum LogicJobStatus
{
    NotStarted,
    Running,
    Finished,
}

public enum LogicJobFinishingStatus
{
    NotFinished,
    FinishedOK,
    FinishedBad,
}

public class MapLogicJob : MonoBehaviour
{

    [HideInInspector]
    public GameObject controlledSoldier;

    [HideInInspector]
    public GameObject soldAnimObj = null;

    [HideInInspector]
    public SoldierInfo soldInfo = null;

    [HideInInspector]
    public CharacterInfo soldCharInfo = null;

    [HideInInspector]
    public SoldierBodyInfo soldBodyInfo = null;

    [HideInInspector]
    public float step = 0;

    [HideInInspector]
    public float outStep = 0;

    //[HideInInspector]
    //public List<MapLogicStep> childSteps = new List<MapLogicStep>();

    [HideInInspector]
    public MapLogic mapLogic;

    [HideInInspector]
    public LogicJobStatus status = LogicJobStatus.NotStarted;

    [HideInInspector]
    public LogicJobFinishingStatus finishStatus = LogicJobFinishingStatus.NotFinished;

    [HideInInspector]
    public bool shouldStopOnLogicStop = true;

    protected bool needsToBeFinished = false;

    protected bool evenStopMovingForFinish = false;

    public void Init_SetControlledSoldier(GameObject _soldier)
    {
        controlledSoldier = _soldier;

        if (controlledSoldier != null)
        {
            soldInfo = controlledSoldier.GetComponent<SoldierInfo>();
            soldCharInfo = controlledSoldier.GetComponent<CharacterInfo>();
            soldAnimObj = soldInfo.animObject;
            soldBodyInfo = soldInfo.bodyInfo;
        }
        else
        {
            soldInfo = null;
            soldCharInfo = null;
            soldAnimObj = null;
            soldBodyInfo = null;
        }
    }

    public virtual void StartIt()
    {
        mapLogic = MapLogic.Instance;

        mapLogic.AddActiveLogicJob(this);

        //step = 1;
        SetStep(1);

        status = LogicJobStatus.Running;

        //Init_SetControlledSoldier(controlledSoldier);
    }

    public virtual void RunIt()
    {
        //foreach (MapLogicStep child in childSteps)
        //{
        //    child.RunIt();
        //}

    }

    public virtual void StopIt()
    {
        if (shouldStopOnLogicStop)
            if (status == LogicJobStatus.Running)
                SetFinished(false);
    }

    protected void SetStep(float _value)
    {
        step = _value;

    }

    public void SetOutStep(float _value)
    {
        outStep = _value;
    }

    //public virtual void SetObjectEnteredToTrigger(LogicTrigger _trigger, GameObject _obj)
    //{

    //}


    public virtual void SetFinished(bool _isFinishedOK)
    {
        status = LogicJobStatus.Finished;

        if (_isFinishedOK)
            finishStatus = LogicJobFinishingStatus.FinishedOK;
        else
            finishStatus = LogicJobFinishingStatus.FinishedBad;

        SetStep(10000000);
    }

    public void SetNeedsToBeFinished()
    {
        needsToBeFinished = true;
    }

    public void SetNeedsToBeFinished_EvenStopMoving()
    {
        needsToBeFinished = true;
        evenStopMovingForFinish = true;
    }

    public void StartOutStepIfNotStarted()
    {
        if (outStep == 0)
            SetOutStep(1);
    }

    public void StartFinishing_OutStepIfNotFinishing()
    {
        if ((status == LogicJobStatus.Finished) || needsToBeFinished)
            return;

        if (outStep >= 900)
            return;

        SetOutStep(900);
    }

    protected void SetChildNeedsToBeFinished(MapLogicJob _child)
    {
        MapLogicJob chl = _child;

        if (evenStopMovingForFinish)
            chl.SetNeedsToBeFinished_EvenStopMoving();
        else
            chl.SetNeedsToBeFinished();
    }
}
