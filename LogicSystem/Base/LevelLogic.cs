using UnityEngine;
using System.Collections;

public class LevelCheckPoint
{

}

public enum LogicFlagStatus
{
    Deactive,
    Active,
    Finished,
}

public class LogicFlag
{
    public bool isEverActivated = false;

    public LogicFlagStatus status = LogicFlagStatus.Deactive;

    public void SetStatus(LogicFlagStatus _status)
    {
        LogicFlagStatus st = _status;

        if (status == LogicFlagStatus.Finished)
            return;

        if (status == LogicFlagStatus.Active)
        {
            if (st == LogicFlagStatus.Deactive)
                return;
        }

        status = st;

        if (status == LogicFlagStatus.Active)
            isEverActivated = true;
    }
        
    public void SetDeactive()
    {
        if (status == LogicFlagStatus.Finished)
            return;

        if (status == LogicFlagStatus.Active)
        {
            isEverActivated = false;
        }

        status = LogicFlagStatus.Deactive;
    }

    public bool IsEverActivated()
    {
        return isEverActivated;
    }

    public bool IsActiveNow()
    {
        return (status == LogicFlagStatus.Active);
    }

    public bool IsActiveNow_IfItIs_FinishIt()
    {
        if (status == LogicFlagStatus.Active)
        {
            SetStatus(LogicFlagStatus.Finished);

            return true;
        }

        return false;
    }

    public void FinishIt()
    {
        SetStatus(LogicFlagStatus.Finished);
    }
}

public class LevelLogic : MapLogicJob
{
    [HideInInspector]
    public float levelStep = 0;

    public LevelCheckPoint currentCheckPoint = null;

    float missionFailCounter = 1.5f;

    [HideInInspector]
    public float step_MissionFail_EnemySawHisMateNash = -7;

    [HideInInspector]
    public float step_MissionFail_EnemyHeardYourFire = -6;

    [HideInInspector]
    public float step_MissionFail_DynamteHasBeenExplodedBeforeCommunicationBreakdown = -5;

    [HideInInspector]
    public float step_MissionFail_YouLeftAreaWithoutPlantingDynamites = -4;

    [HideInInspector]
    public float step_MissionFail_YouAreDetectedByEnemies = -3;

    [HideInInspector]
    public float step_MissionFail_YouLeftFightArea = -2;

    [HideInInspector]
    public float step_MissionFail_AlliesNotSupported = -1;

    [HideInInspector]
    public float step_TheEndBro = 1000000;

    [HideInInspector]
    public float hudCounter = 0;
    //

    public override void StartIt()
    {
        base.StartIt();

        shouldStopOnLogicStop = false;

        mapLogic.ActiveOnlyPlayerCameras();

        if (GameController.gameCurrentLevelLastCheckPoint == 0)
        {
            SetLevelStep(0.1f);
            GameController.SetGameCurrentLevelLastCheckPoint(1);
        }
        else
        {
            if (GameController.gameCurrentLevelLastCheckPoint == 1)
            {
                SetLevelStep(1);
            }
            else
            {
                if (GameController.gameCurrentLevelLastCheckPoint > 1)
                {
                    LoadCheckPoint(GameController.gameCurrentLevelLastCheckPoint);
                }
            }
        }
    }

    public override void RunIt()
    {
        base.RunIt();

        if (levelStep < 0)
            missionFailCounter = MathfPlus.DecByDeltatimeToZero(missionFailCounter);

        #region -1: Mission failed by not supporting allies
        if (levelStep == step_MissionFail_AlliesNotSupported)
        {
            IfItsOkSetMissionFailed(MissionFailType.AlliesNotSupported);
        }
        #endregion

        #region -2: Mission failed by leaving fight area
        if (levelStep == step_MissionFail_YouLeftFightArea)
        {
            IfItsOkSetMissionFailed(MissionFailType.YouLeftFightArea);
        }
        #endregion

        #region -3: Mission failed. You are detected by enemies.
        if (levelStep == step_MissionFail_YouAreDetectedByEnemies)
        {
            IfItsOkSetMissionFailed(MissionFailType.YouAreDetectedByEnemies);
        }
        #endregion

        #region -4: Mission failed. You left area without planting dynamites.
        if (levelStep == step_MissionFail_YouLeftAreaWithoutPlantingDynamites)
        {
            IfItsOkSetMissionFailed(MissionFailType.YouLeftAreaWithoutPlantingDynamites);
        }
        #endregion

        #region -5: Mission failed. Dynamte Has Been Exploded Before Communication Breakdown.
        if (levelStep == step_MissionFail_DynamteHasBeenExplodedBeforeCommunicationBreakdown)
        {
            IfItsOkSetMissionFailed(MissionFailType.DynamteHasBeenExplodedBeforeCommunicationBreakdown);
        }
        #endregion

        #region -6: Mission failed. Enemy Heard Your Fire.
        if (levelStep == step_MissionFail_EnemyHeardYourFire)
        {
            IfItsOkSetMissionFailed(MissionFailType.EnemyHeardYourFire);
        }
        #endregion

        #region -7: Mission failed. Enemy Saw His Mate Nash.
        if (levelStep == step_MissionFail_EnemySawHisMateNash)
        {
            IfItsOkSetMissionFailed(MissionFailType.EnemySawHisMateNash);
        }
        #endregion
    }

    public virtual void LoadCheckPoint(float _levelStep)
    {
        SetLevelStep(_levelStep);

        mapLogic.doNotShowGameSavedMessageForOneTime = true;

        if (PlayerController.LoadWasOK)
            PlayerCharacterNew.Instance.CheckPointLoadTransition();
    }

    public virtual void SaveCheckPoint(float _step)
    {
        int st = (int)_step;

        PlayerCharacterNew.Instance.CheckPointSaveTransition();
        GameSaveLoadController.SavePlayerState();

        GameController.SetGameCurrentLevelLastCheckPoint(st);
        GameSaveLoadController.SaveGameState();
    }

    public virtual void SetLevelStep(float _value)
    {
        levelStep = _value;
    }

    bool IsTimerOkToSetMissionFailed()
    {
        return missionFailCounter == 0;
    }

    void SetMissionFailed(MissionFailType _failType)
    {
        MissionFailType failType = _failType;

        if (PlayerCharacterNew.Instance != null && !PlayerCharacterNew.Instance.IsMissionFailed())
        {
            PlayerCharacterNew.Instance.SetMissionFailedByOutMistake(failType);
        }
    }

    void IfItsOkSetMissionFailed(MissionFailType _failType)
    {
        MissionFailType failType = _failType;

        if (IsTimerOkToSetMissionFailed())
        {
            SetMissionFailed(failType);

            SetLevelStep(step_TheEndBro);
        }
    }

    //

    protected void SetMissionIsFinished()
    {
        //IMMMMMMMPORTAAAAAANT!!!!!!
        //Truck level has a copy of this function!!!!!!!!

        if (mapLogic.playerCharNew.IsMissionFailed())
            return;

        int nextLvlNum = GameController.GetNextLevelNumber();
        int oldLvlNum = GameController.gameCurrentLevel;

        if (GameController.gameLastLevel < nextLvlNum)
        {
            GameController.SetGameLastLevel(nextLvlNum);
            GameSaveLoadController.SaveGameState();
        }

        if (nextLvlNum > oldLvlNum)
        {
            GameController.LoadLevelLoadingPage(nextLvlNum, 0);
        }
        else
        {
            GameController.LoadCredits();
        }
    }
}