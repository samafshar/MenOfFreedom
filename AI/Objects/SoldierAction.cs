using UnityEngine;
using System.Collections;

public class SoldierAction : MonoBehaviour{

    public enum ActionStatusEnum
    {
        NotStarted,
        Running,
        Finished,
    }
    public enum FinishReportEnum
    {
        NotFinished,
        FinishedOK,
        FinishedBad,
    }

    public Transform controlledSoldier = null;

    [HideInInspector]
    public ActionStatusEnum status = ActionStatusEnum.NotStarted;

    [HideInInspector]
    public FinishReportEnum finishReport = FinishReportEnum.NotFinished;

    [HideInInspector]
    public GameObject soldAnimObj = null;

    [HideInInspector]
    public SoldierInfo soldInfo = null;

    [HideInInspector]
    public CharacterInfo charInfo = null;

    //mf
    [HideInInspector]
    public SoldierBodyInfo soldBodyInfo = null;
    //~mf

    protected Transform soldTr;
    protected CharacterController soldCharController;
    protected CharacterInfo soldCharInfo;
    protected MapLogic mapLogic;
    protected PlayerCharacterNew playerCharNew;
    protected GameObject playerObj;

    protected bool needsToBeFinished = false;
    protected bool evenStopMovingForFinish = false;

    //protected AudioSource mainAudioSource;

    //protected SoundPlay mainSoundPlay = new SoundPlay();

    //

    void Update()
    {
        if (status == ActionStatusEnum.Running)
            UpdateAct();
    }

    void FixedUpdate()
    {
        if (status == ActionStatusEnum.Running)
            FixedUpdateAct();
    }

    //

    public virtual void Init(Transform contSoldier)
    {
        controlledSoldier = contSoldier;
        soldInfo = controlledSoldier.GetComponent<SoldierInfo>();
        charInfo = controlledSoldier.GetComponent<CharacterInfo>();
        soldAnimObj = soldInfo.animObject;
        soldTr = controlledSoldier.transform;
        soldCharController = controlledSoldier.GetComponent<CharacterController>();
        soldCharInfo = controlledSoldier.GetComponent<CharacterInfo>();
        mapLogic = soldInfo.mapLogic;
        playerObj = mapLogic.player;
        playerCharNew = mapLogic.playerCharNew;

        //mf
        soldBodyInfo = soldInfo.bodyInfo;
        //~mf
    } 

    public virtual void StartAct()
    {
        status = ActionStatusEnum.Running;
    }

    public virtual void UpdateAct()
    {

    }

    public virtual void FixedUpdateAct()
    {

    }

    public virtual bool ShouldTakeDamage(DamageInfo dmg)
    {
        return true;
    }

    public virtual void SetFinished(bool OK)
    {
        status = ActionStatusEnum.Finished;

        if (OK)
            finishReport = FinishReportEnum.FinishedOK;
        else
            finishReport = FinishReportEnum.FinishedBad;
    }

    //

    public void SetNeedsToBeFinished()
    {
        needsToBeFinished = true;
    }

    public void SetNeedsToBeFinished(bool _shouldEvenStopMoving)
    {
        SetNeedsToBeFinished();

        if (_shouldEvenStopMoving)
            evenStopMovingForFinish = true;
    }

    protected bool CheckFirstDamage()
    {
        return (soldInfo.isDamageRecievedInThisRun && ShouldTakeDamage(soldInfo.firstDamage)) ;
    }

    //protected void InitMainSoundPlay(AudioSource _audSource)
    //{
    //    mainAudioSource = _audSource;
    //    mainSoundPlay.Init(mainAudioSource);
    //}
}
