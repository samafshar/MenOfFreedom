using UnityEngine;
using System.Collections;

public class Level03_JunglePart02_Logic : LevelLogic
{
    public CutsceneController PoleDehdashtCutscene;

    public CutsceneController ExplosionCutscene;

    public CutsceneController WatchTruckFromHillCutscene;

    bool shouldStartTimeCounterForSetMissionFail = false;
    bool missionFailHasSet = false;

    float timeCounterForSettingMissionFail = 2.2f;

    #region Step_A Variables

    //Enemy
    public MapLogicJob_FightInRegsGroup step_A_Enemy_FightInRegsGroup_01;
    public MapLogicJob_FightInRegsGroup step_A_Enemy_FightInRegsGroup_02;

    //Objects
    public LogicTrigger step_A_Objects_01_StartingLevelTrigger;
    public ExecutionArea step_A_Objects_02_ExecutionAreaForFight01;
    public LogicTrigger step_A_Objects_03_StartFight02Trigger;
    public ExecutionArea step_A_Objects_04_ExecutionAreaForFight02;
    public LogicTrigger step_A_Objects_05_KillFight01Trigger;
    public LogicDieTrigger step_A_Objects_06_Fight01DieTrigger;
    public LogicTrigger step_A_Objects_07_EndStepTrigger;
    public LogicDieTrigger step_A_Objects_08_Fight02DieTrigger;

    LogicFlag step_A_PlayerEntered_StartingLevelTrigger = new LogicFlag();
    LogicFlag step_A_PlayerEntered_StartFight02Trigger = new LogicFlag();
    LogicFlag step_A_PlayerEntered_KillFight01Trigger = new LogicFlag();
    LogicFlag step_A_PlayerEntered_EndStepTrigger = new LogicFlag();
    LogicFlag step_A_Fight01_Finished = new LogicFlag();
    LogicFlag step_A_Fight02_Finished = new LogicFlag();

    #endregion

    public string __________________;

    #region Step_B Variables

    //Enemy
    public MapLogicJob_FightInRegsGroup step_B_Enemy_FightInRegsGroup_01;
    public MapLogicJob_MachineGun step_B_Enemy_MachineGun_01;
    public MapLogicJob_MachineGun step_B_Enemy_MachineGun_02;

    //Objects
    public ExecutionArea step_B_Objects_01_ExecutionAreaForFight01;
    public LogicTrigger step_B_Objects_02_FinishingFightsTrigger;
    public LogicTrigger step_B_Objects_03_KillFight01Trigger;
    public LogicDieTrigger step_B_Objects_04_Fight01DieTrigger;
    public LogicTrigger step_B_Objects_05_EndStepTrigger;

    public float step_B_MaxTimeForCreateInFight01;

    public int step_B_MinNumOfCreatedSoldiersInFightReg01;

    public StartPoint step_B_Objects_StartPoint_Player;

    LogicFlag step_B_Fight01_Finished = new LogicFlag();
    LogicFlag step_B_PlayerEntered_FinishingFightsTrigger = new LogicFlag();
    LogicFlag step_B_PlayerEntered_KillFight01Trigger = new LogicFlag();
    LogicFlag step_B_PlayerEntered_EndStepTrigger = new LogicFlag();

    int step_B_NumbersOfCreatedSoldiersInFightReg01;

    float step_B_Timer;

    #endregion

    public string ____________________;

    #region Step_C Variables

    //Enemy
    public MapLogicJob_FightInRegsGroup step_C_Enemy_FightInRegsGroup_01;

    //Objects
    public Transform step_C_HUD_BombA_Obj3DTr;
    public Transform step_C_HUD_BombB_Obj3DTr;
    public GameObject step_C_Objects_01_SignalDynamite_01;
    public GameObject step_C_Objects_01_NormalDynamite_01;
    public GameObject step_C_Objects_02_SignalDynamite_02;
    public GameObject step_C_Objects_02_NormalDynamite_02;
    public AudioInfo step_C_Objects_03_TickTick;
    public LogicTrigger step_C_Objects_04_RunAwayFromBridgeFailTrigger;
    public ExecutionArea step_C_Objects_05_ExecutionArea01;
    public LogicTrigger step_C_Objects_06_ExplosionCutSceneTrigger;
    public LogicDieTrigger step_C_Objects_07_Fight01DieTrigger;
    public GameObject step_C_Objects_08_ExplosionForKillPlayer;
    public GameObject step_C_Objects_09_ExplosionForCutscene;
    public GameObject step_C_Objects_10_PolAnimObject;
    public GameObject step_C_Objects_11_SmokeParticlePol;
    public LogicTrigger step_C_Objects_12_RiverTrigger;
    public GameObject step_C_Objects_13_StopBackCollider;
    public GameObject[] step_C_Objects_14_CollidersToDisableAfterExplosion;
    public GameObject[] step_C_Objects_15_CollidersToEnabAfterExplosion;

    public StartPoint step_C_Objects_StartPoint_Player;

    public StartPoint step_C_Objects_StartPoint_Player_AfterExplosionCutscene;

    public float step_C_MaxTimeTillExplosion = 10f;
    public float step_C_TimeForExplosionInCutscene = 3f;

    //

    float step_C_Timer = 0f;

    bool isPolExplode = false;

    LogicFlag step_C_Fight01_Finished = new LogicFlag();
    LogicFlag step_C_PlayerEntered_RunAwayFromBridgeFailTrigger = new LogicFlag();
    LogicFlag step_C_PlayerEntered_ExplosionCutSceneTrigger = new LogicFlag();

    LogicObjective dynamite01;
    LogicObjective dynamite02;

    bool isDynamite01Installed = false;
    bool isDynamite02Installed = false;

    #endregion

    public string _______________________;

    #region Step_D Variables

    //Enemy
    public MapLogicJob_FightInRegsGroup step_D_Enemy_FightInRegsGroup_01;
    public MapLogicJob_FightInRegsGroup step_D_Enemy_FightInRegsGroup_02;

    //Objects
    public Transform step_D_HUD_Truck_Obj3DTr;

    public LogicTrigger step_D_Objects_01_StartFight01Trigger;
    public ExecutionArea step_D_Objects_02_ExecutionArea01;
    public LogicDieTrigger step_D_Objects_03_Fight01DieTrigger;
    public LogicTrigger step_D_Objects_04_StartFight02Trigger;
    public ExecutionArea step_D_Objects_05_ExecutionArea02;
    public LogicDieTrigger step_D_Objects_06_Fight02DieTrigger;
    public LogicTrigger step_D_Objects_07_EndFight02Trigger;
    public LogicTrigger step_D_Objects_07_EndStepTrigger;

    public StartPoint step_D_Objects_StartPoint_Player;
    //

    LogicFlag step_D_PlayerEntered_StartFight01Trigger = new LogicFlag();
    LogicFlag step_D_PlayerEntered_StartFight02Trigger = new LogicFlag();
    LogicFlag step_D_PlayerEntered_EndFight02Trigger = new LogicFlag();
    LogicFlag step_D_PlayerEntered_EndStepTrigger = new LogicFlag();

    #endregion

    public string __________________________;

    #region Step_E Vriables

    //Enemy
    public MapLogicJob_FightInRegsGroup step_E_Enemy_FightInRegsGroup_01;

    //Objects
    public LogicTrigger step_E_Objects_01_EndStepTrigger;

    public StartPoint step_E_Objects_StartPoint_Player;

    LogicFlag step_E_PlayerEntered_EndStepTrigger = new LogicFlag();

    #endregion

    public override void StartIt()
    {
        base.StartIt();

        //LoadCheckPoint(4.4f);
    }

    public override void RunIt()
    {
        base.RunIt();

        if (step == 1)
        {
            CheckIfShouldSetMissionFailByDetection();

        StartLevelSteps:

            #region 0.1 Start
            if (levelStep == 0.1f)
            {
                SetLevelStep(1);
            }
            #endregion

            #region 1 Start First Trigger For Player Enter
            if (levelStep == 1)
            {
                SaveCheckPoint(1f);

                step_A_Objects_01_StartingLevelTrigger.StartOutStepIfNotStarted();


                PlayFirstMusic();

                SetLevelStep(1.1f);
            }
            #endregion

            #region 1.1 Wait For Trigger Active
            if (levelStep == 1.1f)
            {
                if (step_A_PlayerEntered_StartingLevelTrigger.IsEverActivated())
                {
                    mapLogic.HUD_ObjectivesPage_SetActiveObjective(1);
                    mapLogic.HUD_ShowNewMission(0);

                    SetLevelStep(1.2f);
                }
            }
            #endregion

            #region 1.2 Start Enemy FightReg 01
            if (levelStep == 1.2f)
            {
                step_A_Enemy_FightInRegsGroup_01.StartOutStepIfNotStarted();

                step_A_Objects_02_ExecutionAreaForFight01.StartIt();

                step_A_Objects_03_StartFight02Trigger.StartOutStepIfNotStarted();

                SetLevelStep(1.25f);
            }
            #endregion

            #region 1.25 Wait For Second Trigger
            if (levelStep == 1.25f)
            {
                if (step_A_PlayerEntered_StartFight02Trigger.IsEverActivated())
                {
                    SetLevelStep(1.3f);
                }
            }
            #endregion

            #region 1.3 Start Enemy FightReg 02
            if (levelStep == 1.3f)
            {
                step_A_Enemy_FightInRegsGroup_02.StartOutStepIfNotStarted();

                step_A_Objects_04_ExecutionAreaForFight02.StartIt();

                step_A_Objects_05_KillFight01Trigger.StartOutStepIfNotStarted();

                step_A_Enemy_FightInRegsGroup_01.StopCreatingMoreSoldiers();

                SetLevelStep(1.35f);
            }
            #endregion

            #region 1.35 Wait For Kill Trigger
            if (levelStep == 1.35f)
            {
                if (step_A_PlayerEntered_KillFight01Trigger.IsEverActivated())
                {
                    step_A_Objects_06_Fight01DieTrigger.StartIt();

                    step_A_Objects_02_ExecutionAreaForFight01.EndIt();

                    step_A_Objects_07_EndStepTrigger.StartOutStepIfNotStarted();

                    SetLevelStep(1.4f);
                }
            }
            #endregion

            #region 1.4 Wait For End Step Trigger
            if (levelStep == 1.4f)
            {
                if (step_A_PlayerEntered_EndStepTrigger.IsEverActivated())
                {
                    step_A_Enemy_FightInRegsGroup_02.StopCreatingMoreSoldiers();

                    step_A_Objects_08_Fight02DieTrigger.StartIt();

                    step_A_Objects_04_ExecutionAreaForFight02.EndIt();

                    mapLogic.HUD_ObjectivesPage_SetObjectiveDone(1);
                    mapLogic.HUD_ShowCompleteMission(0);

                    SetLevelStep(2f);
                }
            }
            #endregion

            #region 2 Start Step
            if (levelStep == 2)
            {
                SaveCheckPoint(2f);

                step_B_Enemy_FightInRegsGroup_01.StartOutStepIfNotStarted();

                step_B_Enemy_MachineGun_01.StartOutStepIfNotStarted();

                step_B_Enemy_MachineGun_02.StartOutStepIfNotStarted();

                step_B_Objects_01_ExecutionAreaForFight01.StartIt();

                step_B_Objects_02_FinishingFightsTrigger.StartOutStepIfNotStarted();

                step_B_Timer = step_B_MaxTimeForCreateInFight01;

                mapLogic.HUD_ShowGameSaved();

                PlayFirstMusic();

                SetLevelStep(2.1f);
            }
            #endregion

            #region 2.1 Wait Specific Time To Stop Creating Soldiers
            if (levelStep == 2.1f)
            {
                step_B_NumbersOfCreatedSoldiersInFightReg01 = step_B_Enemy_FightInRegsGroup_01.GetNumOfCreatedSoldiers();

                if (!mapLogic.isPlayerHidden)
                {
                    step_B_Timer = MathfPlus.DecByDeltatimeToZero(step_B_Timer);
                    if (step_B_Timer == 0)
                    {
                        if (step_B_NumbersOfCreatedSoldiersInFightReg01 > step_B_MinNumOfCreatedSoldiersInFightReg01)
                        {
                            SetLevelStep(2.2f);
                        }
                    }
                }

                if (step_B_PlayerEntered_FinishingFightsTrigger.IsEverActivated())
                {
                    SetLevelStep(2.2f);
                }
            }
            #endregion

            #region 2.2 Stop All Fights
            if (levelStep == 2.2f)
            {
                step_B_Enemy_FightInRegsGroup_01.StopCreatingMoreSoldiers();

                step_B_Enemy_MachineGun_01.StopCreatingMoreSoldiers();
                step_B_Enemy_MachineGun_02.StopCreatingMoreSoldiers();

                step_B_Objects_03_KillFight01Trigger.StartOutStepIfNotStarted();

                step_B_Objects_05_EndStepTrigger.StartOutStepIfNotStarted();

                SetLevelStep(2.3f);
            }
            #endregion

            #region 2.3 Kill Fight01
            if (levelStep == 2.3f)
            {
                if (step_B_PlayerEntered_KillFight01Trigger.IsEverActivated())
                {
                    step_B_Objects_04_Fight01DieTrigger.StartIt();

                    SetLevelStep(2.4f);
                }
            }
            #endregion

            #region 2.4 Wait For End Step
            if (levelStep == 2.4f)
            {
                if (step_B_PlayerEntered_EndStepTrigger.IsEverActivated())
                {
                    //Use Fade In Cutscene Start
                    //SetLevelStep(2.5f);

                    SetLevelStep(2.41f);
                }
            }
            #endregion

            #region 2.41 Start fading out black screen
            if (levelStep == 2.41f)
            {
                mapLogic.blackScreenFader.StartFadingOut();
                SetLevelStep(2.42f);

            }
            #endregion

            #region 2.42 Waiting for fading out finish
            if (levelStep == 2.42f)
            {
                if (mapLogic.blackScreenFader.isFadingFinished)
                {
                    SetLevelStep(2.5f);
                }
            }
            #endregion

            #region 2.5 Cutscene
            if (levelStep == 2.5f)
            {
                PoleDehdashtCutscene.StartIt();

                SetLevelStep(2.6f);
            }
            #endregion

            #region 2.6 Wait Till Cutscene End
            if (levelStep == 2.6f)
            {
                if (PoleDehdashtCutscene.status == CutsceneStatus.Finished)
                {
                    SetLevelStep(2.7f);
                }
            }
            #endregion

            #region 2.7 BlackScreen after cutscene
            if (levelStep == 2.7f)
            {
                step_C_Objects_StartPoint_Player.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);

                mapLogic.blackScreenFader.StartFadingIn();

                SetLevelStep(3f);
            }
            #endregion

            #region 3 Start Step 3
            if (levelStep == 3f)
            {
                SaveCheckPoint(3f);

                mapLogic.HUD_Add3DObjective(step_C_HUD_BombA_Obj3DTr, The3DObjIconType.Dot, "BombA", The3DObjViewRange.SoFar);
                mapLogic.HUD_Add3DObjective(step_C_HUD_BombB_Obj3DTr, The3DObjIconType.Dot, "BombB", The3DObjViewRange.SoFar);

                step_C_Objects_01_SignalDynamite_01.SetActiveRecursively(true);
                step_C_Objects_02_SignalDynamite_02.SetActiveRecursively(true);

                dynamite01 = step_C_Objects_01_SignalDynamite_01.GetComponent<LogicObjective>();

                dynamite02 = step_C_Objects_02_SignalDynamite_02.GetComponent<LogicObjective>();

                step_C_Objects_04_RunAwayFromBridgeFailTrigger.StartOutStepIfNotStarted();

                mapLogic.HUD_ShowGameSaved();

                mapLogic.HUD_ObjectivesPage_SetActiveObjective(2);
                mapLogic.HUD_ShowNewMission(1);

                PlayFirstMusic();

                step_C_Objects_12_RiverTrigger.SetEnabled(true);

                SetLevelStep(3.1f);
            }
            #endregion

            #region 3.1 Check Dynamite
            if (levelStep == 3.1f)
            {
                if (!step_C_Objects_13_StopBackCollider.gameObject.active)
                {
                    if (step_C_Objects_12_RiverTrigger.IsPlayerIn())
                    {
                        step_C_Objects_13_StopBackCollider.gameObject.active = true;
                    }
                }

                if (dynamite01.IsDone && !isDynamite01Installed)
                {
                    step_C_Objects_01_SignalDynamite_01.SetActiveRecursively(false);
                    step_C_Objects_01_NormalDynamite_01.SetActiveRecursively(true);

                    mapLogic.HUD_Remove3DObjective("BombA");

                    isDynamite01Installed = true;
                }

                if (dynamite02.IsDone && !isDynamite02Installed)
                {
                    step_C_Objects_02_SignalDynamite_02.SetActiveRecursively(false);
                    step_C_Objects_02_NormalDynamite_02.SetActiveRecursively(true);

                    mapLogic.HUD_Remove3DObjective("BombB");

                    isDynamite02Installed = true;
                }

                if (isDynamite01Installed && isDynamite02Installed)
                {
                    SetLevelStep(3.15f);
                }

                if (step_C_PlayerEntered_RunAwayFromBridgeFailTrigger.IsEverActivated())
                {
                    SetLevelStep(step_MissionFail_YouLeftFightArea);
                    goto EndLevelSteps;
                }
            }
            #endregion

            #region 3.15 Enemies Attack
            if (levelStep == 3.15f)
            {
                step_C_Objects_03_TickTick.Play();

                step_C_Enemy_FightInRegsGroup_01.StartOutStepIfNotStarted();

                step_C_Objects_06_ExplosionCutSceneTrigger.StartOutStepIfNotStarted();

                step_C_Objects_05_ExecutionArea01.StartIt();

                step_C_Timer = step_C_MaxTimeTillExplosion;

                SetLevelStep(3.2f);
            }
            #endregion

            #region 3.2 Wait For Explosion CutScene
            if (levelStep == 3.2f)
            {
                step_C_Timer = MathfPlus.DecByDeltatimeToZero(step_C_Timer);

                if (step_C_Timer == 0)
                {
                    step_C_Objects_08_ExplosionForKillPlayer.SetActiveRecursively(true);

                    step_C_Objects_10_PolAnimObject.animation.Play();

                    step_C_Objects_03_TickTick.Stop();

                    SetLevelStep(1000f);
                }

                if (step_C_PlayerEntered_ExplosionCutSceneTrigger.IsEverActivated())
                {
                    step_C_Objects_07_Fight01DieTrigger.StartIt();

                    step_C_Objects_05_ExecutionArea01.EndIt();

                    step_C_Objects_01_NormalDynamite_01.SetActiveRecursively(false);
                    step_C_Objects_02_NormalDynamite_02.SetActiveRecursively(false);

                    step_C_Timer = step_C_TimeForExplosionInCutscene;
                                        
                    SetLevelStep(3.3f);
                }
            }
            #endregion

            #region 3.3 Start fading out black screen
            if (levelStep == 3.3f)
            {
                mapLogic.blackScreenFader.StartFadingOut();
                SetLevelStep(3.31f);

            }
            #endregion

            #region 3.31 Waiting for fading out finish
            if (levelStep == 3.31f)
            {
                if (mapLogic.blackScreenFader.isFadingFinished)
                {
                    SetLevelStep(3.4f);
                }
            }
            #endregion

            #region 3.4 Cutscene
            if (levelStep == 3.4f)
            {
                step_C_Objects_03_TickTick.SetCustomVolume(0.45f);
                ExplosionCutscene.StartIt();

                SetLevelStep(3.5f);
            }
            #endregion

            #region 3.5 Wait Till Cutscene End
            if (levelStep == 3.5f)
            {
                step_C_Timer = MathfPlus.DecByDeltatimeToZero(step_C_Timer);

                if (step_C_Timer == 0 && !isPolExplode)
                {
                    step_C_Objects_03_TickTick.Stop();

                    step_C_Objects_09_ExplosionForCutscene.SetActiveRecursively(true);

                    step_C_Objects_10_PolAnimObject.animation.Play();

                    SetPolColliders();

                    isPolExplode = true;
                }

                if (ExplosionCutscene.status == CutsceneStatus.Finished)
                {
                    SetLevelStep(3.6f);
                }
            }
            #endregion

            #region 3.6 BlackScreen after cutscene
            if (levelStep == 3.6f)
            {
                step_C_Objects_StartPoint_Player_AfterExplosionCutscene.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);

                mapLogic.blackScreenFader.StartFadingIn();

                mapLogic.HUD_ObjectivesPage_SetObjectiveDone(2);
                mapLogic.HUD_ShowCompleteMission(1);

                SetLevelStep(4f);
            }
            #endregion

            #region 4 Start Step
            if (levelStep == 4f)
            {
                SaveCheckPoint(4f);

                step_D_Objects_01_StartFight01Trigger.StartOutStepIfNotStarted();

                mapLogic.HUD_ShowGameSaved();

                mapLogic.HUD_ObjectivesPage_SetActiveObjective(3);
                mapLogic.HUD_ShowNewMission(2);

                PlayFirstMusic();

                SetLevelStep(4.1f);
            }
            #endregion

            #region 4.1 Wait For Start Fight01
            if (levelStep == 4.1f)
            {
                if (step_D_PlayerEntered_StartFight01Trigger.IsEverActivated())
                {
                    step_D_Enemy_FightInRegsGroup_01.StartOutStepIfNotStarted();

                    step_D_Objects_04_StartFight02Trigger.StartOutStepIfNotStarted();

                    step_D_Objects_07_EndStepTrigger.StartOutStepIfNotStarted();

                    step_D_Objects_02_ExecutionArea01.StartIt();

                    SetLevelStep(4.2f);
                }
            }
            #endregion

            #region 4.2 Wait For Start Fight02
            if (levelStep == 4.2f)
            {
                if (step_D_PlayerEntered_StartFight02Trigger.IsEverActivated())
                {
                    step_D_Enemy_FightInRegsGroup_02.StartOutStepIfNotStarted();

                    step_D_Objects_05_ExecutionArea02.StartIt();

                    step_D_Enemy_FightInRegsGroup_01.StopCreatingMoreSoldiers();

                    step_D_Objects_02_ExecutionArea01.EndIt();

                    step_D_Objects_07_EndFight02Trigger.StartOutStepIfNotStarted();

                    SetLevelStep(4.25f);
                }
            }
            #endregion

            #region 4.25 Wait For End Fight02 Trigger
            if (levelStep == 4.25f)
            {
                if (step_D_PlayerEntered_EndFight02Trigger.IsEverActivated())
                {
                    mapLogic.HUD_ObjectivesPage_SetObjectiveDone(3);
                    mapLogic.HUD_ShowCompleteMission(2);

                    SetLevelStep(4.3f);
                }
            }
            #endregion

            #region 4.3 Wait For End Step
            if (levelStep == 4.3f)
            {
                if (step_D_PlayerEntered_EndStepTrigger.IsEverActivated())
                {
                    step_D_Enemy_FightInRegsGroup_02.StopCreatingMoreSoldiers();

                    step_D_Objects_05_ExecutionArea02.EndIt();

                    step_D_Objects_03_Fight01DieTrigger.StartIt();

                    step_D_Objects_06_Fight02DieTrigger.StartIt();

                    SetLevelStep(4.31f);
                }
            }
            #endregion

            #region 4.31 Start fading out black screen
            if (levelStep == 4.31f)
            {
                mapLogic.blackScreenFader.StartFadingOut();
                SetLevelStep(4.32f);

            }
            #endregion

            #region 4.32 Waiting for fading out finish
            if (levelStep == 4.32f)
            {
                if (mapLogic.blackScreenFader.isFadingFinished)
                {
                    SetLevelStep(4.4f);
                }
            }
            #endregion

            #region 4.4 Cutscene Watch Truck From Hill
            if (levelStep == 4.4f)
            {
                WatchTruckFromHillCutscene.StartIt();

                MusicController.Instance.EndMusicWithFade(MusicFadeType.Fast);

                SetLevelStep(4.5f);
            }
            #endregion

            #region 4.5 Wait Till Cutscene End
            if (levelStep == 4.5f)
            {
                if (WatchTruckFromHillCutscene.status == CutsceneStatus.Finished)
                {
                    SetLevelStep(4.6f);
                }
            }
            #endregion

            #region 4.6 Black Screen After Cutscene
            if (levelStep == 4.6f)
            {
                mapLogic.blackScreenFader.StartFadingIn();

                SetLevelStep(5f);
            }
            #endregion

            #region 5 Start Step
            if (levelStep == 5f)
            {
                SaveCheckPoint(5f);

                mapLogic.HUD_Add3DObjective(step_D_HUD_Truck_Obj3DTr, The3DObjIconType.Dot, "Truck", The3DObjViewRange.SoFar);

                mapLogic.playerCharNew.TryStartSitting();

                step_E_Objects_StartPoint_Player.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);

                mapLogic.playerCharNew.ResetPlayerCampStatus(true);

                mapLogic.HUD_ShowGameSaved();

                mapLogic.HUD_ObjectivesPage_SetActiveObjective(4);
                mapLogic.HUD_ShowNewMission(3);

                PlaySecondMusic();

                SetLevelStep(5.1f);
            }
            #endregion

            #region 5.1 Start Fight Region
            {
                if (levelStep == 5.1f)
                {
                    step_E_Enemy_FightInRegsGroup_01.StartOutStepIfNotStarted();

                    step_E_Objects_01_EndStepTrigger.StartOutStepIfNotStarted();

                    SetLevelStep(5.2f);
                }
            }
            #endregion

            #region 5.2 Start Fight Region
            {
                if (levelStep == 5.2f)
                {
                    if (step_E_PlayerEntered_EndStepTrigger.IsEverActivated())
                    {
                        mapLogic.HUD_Remove3DObjective("Truck");

                        mapLogic.blackScreenFader.StartFadingOut();

                        SetLevelStep(6.9f);
                    }
                }
            }
            #endregion

            #region 6.9f Set mission is finished if black screen fading is done.
            if (levelStep == 6.9f)
            {
                if (mapLogic.blackScreenFader.isFadingFinished)
                {
                    SetMissionIsFinished();
                    SetLevelStep(7f);
                }
            }
            #endregion

        EndLevelSteps:
            ;

            //A

            #region step_A_Objects_01_StartingLevelTrigger

            #region 1 Start
            if (step_A_Objects_01_StartingLevelTrigger.OutStep == 1) //Start
            {
                step_A_Objects_01_StartingLevelTrigger.SetEnabled(true);
                step_A_Objects_01_StartingLevelTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_A_Objects_01_StartingLevelTrigger.OutStep == 1.1f) //Run
            {
                if (step_A_Objects_01_StartingLevelTrigger.IsPlayerIn())
                {
                    step_A_PlayerEntered_StartingLevelTrigger.SetStatus(LogicFlagStatus.Active);
                    step_A_Objects_01_StartingLevelTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_A_Objects_01_StartingLevelTrigger.OutStep == 900f) //Finish
            {
                step_A_Objects_01_StartingLevelTrigger.SetEnabled(false);
                step_A_Objects_01_StartingLevelTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region Step_A_Enemy_FightInRegsGroup_01

            #region 1 Start
            if (step_A_Enemy_FightInRegsGroup_01.outStep == 1f)
            {
                step_A_Enemy_FightInRegsGroup_01.StartIt();
                step_A_Enemy_FightInRegsGroup_01.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_A_Enemy_FightInRegsGroup_01.outStep == 1.1f)
            {
                step_A_Enemy_FightInRegsGroup_01.RunIt();

                if (step_A_Enemy_FightInRegsGroup_01.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    step_A_Enemy_FightInRegsGroup_01.StartFinishing_OutStepIfNotFinishing();
                    goto Step_A_Enemy_FightInRegsGroup_01_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (step_A_Enemy_FightInRegsGroup_01.outStep == 900f)
            {
                step_A_Enemy_FightInRegsGroup_01.SetNeedsToBeFinished();
                step_A_Enemy_FightInRegsGroup_01.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (step_A_Enemy_FightInRegsGroup_01.outStep == 901f)
            {
                step_A_Enemy_FightInRegsGroup_01.RunIt();

                if (step_A_Enemy_FightInRegsGroup_01.status == LogicJobStatus.Finished)
                {
                    step_A_Enemy_FightInRegsGroup_01.SetOutStep(1000f);
                    step_A_Fight01_Finished.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

        Step_A_Enemy_FightInRegsGroup_01_End: ;

            #endregion

            #region step_A_Objects_03_StartFight02Trigger

            #region 1 Start
            if (step_A_Objects_03_StartFight02Trigger.OutStep == 1) //Start
            {
                step_A_Objects_03_StartFight02Trigger.SetEnabled(true);
                step_A_Objects_03_StartFight02Trigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_A_Objects_03_StartFight02Trigger.OutStep == 1.1f) //Run
            {
                if (step_A_Objects_03_StartFight02Trigger.IsPlayerIn())
                {
                    step_A_PlayerEntered_StartFight02Trigger.SetStatus(LogicFlagStatus.Active);
                    step_A_Objects_03_StartFight02Trigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_A_Objects_03_StartFight02Trigger.OutStep == 900f) //Finish
            {
                step_A_Objects_03_StartFight02Trigger.SetEnabled(false);
                step_A_Objects_03_StartFight02Trigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region Step_A_Enemy_FightInRegsGroup_02

            #region 1 Start
            if (step_A_Enemy_FightInRegsGroup_02.outStep == 1f)
            {
                step_A_Enemy_FightInRegsGroup_02.StartIt();
                step_A_Enemy_FightInRegsGroup_02.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_A_Enemy_FightInRegsGroup_02.outStep == 1.1f)
            {
                step_A_Enemy_FightInRegsGroup_02.RunIt();

                if (step_A_Enemy_FightInRegsGroup_02.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    step_A_Enemy_FightInRegsGroup_02.StartFinishing_OutStepIfNotFinishing();
                    goto Step_A_Enemy_FightInRegsGroup_02_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (step_A_Enemy_FightInRegsGroup_02.outStep == 900f)
            {
                step_A_Enemy_FightInRegsGroup_02.SetNeedsToBeFinished();
                step_A_Enemy_FightInRegsGroup_02.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (step_A_Enemy_FightInRegsGroup_02.outStep == 901f)
            {
                step_A_Enemy_FightInRegsGroup_02.RunIt();

                if (step_A_Enemy_FightInRegsGroup_02.status == LogicJobStatus.Finished)
                {
                    step_A_Enemy_FightInRegsGroup_02.SetOutStep(1000f);
                    step_A_Fight02_Finished.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

        Step_A_Enemy_FightInRegsGroup_02_End: ;

            #endregion

            #region step_A_Objects_05_KillFight01Trigger

            #region 1 Start
            if (step_A_Objects_05_KillFight01Trigger.OutStep == 1) //Start
            {
                step_A_Objects_05_KillFight01Trigger.SetEnabled(true);
                step_A_Objects_05_KillFight01Trigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_A_Objects_05_KillFight01Trigger.OutStep == 1.1f) //Run
            {
                if (step_A_Objects_05_KillFight01Trigger.IsPlayerIn())
                {
                    step_A_PlayerEntered_KillFight01Trigger.SetStatus(LogicFlagStatus.Active);
                    step_A_Objects_05_KillFight01Trigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_A_Objects_05_KillFight01Trigger.OutStep == 900f) //Finish
            {
                step_A_Objects_05_KillFight01Trigger.SetEnabled(false);
                step_A_Objects_05_KillFight01Trigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_A_Objects_07_StartFight03Trigger

            #region 1 Start
            if (step_A_Objects_07_EndStepTrigger.OutStep == 1) //Start
            {
                step_A_Objects_07_EndStepTrigger.SetEnabled(true);
                step_A_Objects_07_EndStepTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_A_Objects_07_EndStepTrigger.OutStep == 1.1f) //Run
            {
                if (step_A_Objects_07_EndStepTrigger.IsPlayerIn())
                {
                    step_A_PlayerEntered_EndStepTrigger.SetStatus(LogicFlagStatus.Active);
                    step_A_Objects_07_EndStepTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_A_Objects_07_EndStepTrigger.OutStep == 900f) //Finish
            {
                step_A_Objects_07_EndStepTrigger.SetEnabled(false);
                step_A_Objects_07_EndStepTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            //B

            #region step_B_Enemy_FightInRegsGroup_01

            #region 1 Start
            if (step_B_Enemy_FightInRegsGroup_01.outStep == 1f)
            {
                step_B_Enemy_FightInRegsGroup_01.StartIt();
                step_B_Enemy_FightInRegsGroup_01.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_B_Enemy_FightInRegsGroup_01.outStep == 1.1f)
            {
                step_B_Enemy_FightInRegsGroup_01.RunIt();

                if (step_B_Enemy_FightInRegsGroup_01.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    step_B_Enemy_FightInRegsGroup_01.StartFinishing_OutStepIfNotFinishing();
                    goto step_A_Enemy_FightInRegsGroup_03_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (step_B_Enemy_FightInRegsGroup_01.outStep == 900f)
            {
                step_B_Enemy_FightInRegsGroup_01.SetNeedsToBeFinished();
                step_B_Enemy_FightInRegsGroup_01.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (step_B_Enemy_FightInRegsGroup_01.outStep == 901f)
            {
                step_B_Enemy_FightInRegsGroup_01.RunIt();

                if (step_B_Enemy_FightInRegsGroup_01.status == LogicJobStatus.Finished)
                {
                    step_B_Enemy_FightInRegsGroup_01.SetOutStep(1000f);
                    step_B_Fight01_Finished.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

        step_A_Enemy_FightInRegsGroup_03_End: ;

            #endregion

            #region step_B_Enemy_MachineGun_01

            #region 1 Start
            if (step_B_Enemy_MachineGun_01.outStep == 1)
            {
                step_B_Enemy_MachineGun_01.StartIt();

                step_B_Enemy_MachineGun_01.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_B_Enemy_MachineGun_01.outStep == 1.1f)
            {
                step_B_Enemy_MachineGun_01.RunIt();

                if (step_B_Enemy_MachineGun_01.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    step_B_Enemy_MachineGun_01.StartFinishing_OutStepIfNotFinishing();

                    goto step_B_Enemy_MachineGun_01_End;
                }
            }
            #endregion

            #region 900 Start finishing
            if (step_B_Enemy_MachineGun_01.outStep == 900f)
            {
                step_B_Enemy_MachineGun_01.SetNeedsToBeFinished();
                step_B_Enemy_MachineGun_01.SetOutStep(901);
            }
            #endregion

            #region 901 Finishing
            if (step_B_Enemy_MachineGun_01.outStep == 901f)
            {
                step_B_Enemy_MachineGun_01.RunIt();

                if (step_B_Enemy_MachineGun_01.status == LogicJobStatus.Finished)
                {
                    step_B_Enemy_MachineGun_01.SetOutStep(1000f);
                }
            }
            #endregion

        step_B_Enemy_MachineGun_01_End: ;

            #endregion

            #region step_B_Enemy_MachineGun_02

            #region 1 Start
            if (step_B_Enemy_MachineGun_02.outStep == 1)
            {
                step_B_Enemy_MachineGun_02.StartIt();

                step_B_Enemy_MachineGun_02.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_B_Enemy_MachineGun_02.outStep == 1.1f)
            {
                step_B_Enemy_MachineGun_02.RunIt();

                if (step_B_Enemy_MachineGun_02.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    step_B_Enemy_MachineGun_02.StartFinishing_OutStepIfNotFinishing();

                    goto step_B_Enemy_MachineGun_02_End;
                }
            }
            #endregion

            #region 900 Start finishing
            if (step_B_Enemy_MachineGun_02.outStep == 900f)
            {
                step_B_Enemy_MachineGun_02.SetNeedsToBeFinished();
                step_B_Enemy_MachineGun_02.SetOutStep(901);
            }
            #endregion

            #region 901 Finishing
            if (step_B_Enemy_MachineGun_02.outStep == 901f)
            {
                step_B_Enemy_MachineGun_02.RunIt();

                if (step_B_Enemy_MachineGun_02.status == LogicJobStatus.Finished)
                {
                    step_B_Enemy_MachineGun_02.SetOutStep(1000f);
                }
            }
            #endregion

        step_B_Enemy_MachineGun_02_End: ;

            #endregion

            #region step_B_Objects_02_FinishingFightsTrigger

            #region 1 Start
            if (step_B_Objects_02_FinishingFightsTrigger.OutStep == 1) //Start
            {
                step_B_Objects_02_FinishingFightsTrigger.SetEnabled(true);
                step_B_Objects_02_FinishingFightsTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_B_Objects_02_FinishingFightsTrigger.OutStep == 1.1f) //Run
            {
                if (step_B_Objects_02_FinishingFightsTrigger.IsPlayerIn())
                {
                    step_B_PlayerEntered_FinishingFightsTrigger.SetStatus(LogicFlagStatus.Active);
                    step_B_Objects_02_FinishingFightsTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_B_Objects_02_FinishingFightsTrigger.OutStep == 900f) //Finish
            {
                step_B_Objects_02_FinishingFightsTrigger.SetEnabled(false);
                step_B_Objects_02_FinishingFightsTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_B_Objects_03_KillFight01Trigger

            #region 1 Start
            if (step_B_Objects_03_KillFight01Trigger.OutStep == 1) //Start
            {
                step_B_Objects_03_KillFight01Trigger.SetEnabled(true);
                step_B_Objects_03_KillFight01Trigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_B_Objects_03_KillFight01Trigger.OutStep == 1.1f) //Run
            {
                if (step_B_Objects_03_KillFight01Trigger.IsPlayerIn())
                {
                    step_B_PlayerEntered_KillFight01Trigger.SetStatus(LogicFlagStatus.Active);
                    step_B_Objects_03_KillFight01Trigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_B_Objects_03_KillFight01Trigger.OutStep == 900f) //Finish
            {
                step_B_Objects_03_KillFight01Trigger.SetEnabled(false);
                step_B_Objects_03_KillFight01Trigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_B_Objects_05_EndStepTrigger

            #region 1 Start
            if (step_B_Objects_05_EndStepTrigger.OutStep == 1) //Start
            {
                step_B_Objects_05_EndStepTrigger.SetEnabled(true);
                step_B_Objects_05_EndStepTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_B_Objects_05_EndStepTrigger.OutStep == 1.1f) //Run
            {
                if (step_B_Objects_05_EndStepTrigger.IsPlayerIn())
                {
                    step_B_PlayerEntered_EndStepTrigger.SetStatus(LogicFlagStatus.Active);
                    step_B_Objects_05_EndStepTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_B_Objects_05_EndStepTrigger.OutStep == 900f) //Finish
            {
                step_B_Objects_05_EndStepTrigger.SetEnabled(false);
                step_B_Objects_05_EndStepTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            //C

            #region step_C_Enemy_FightInRegsGroup_01

            #region 1 Start
            if (step_C_Enemy_FightInRegsGroup_01.outStep == 1f)
            {
                step_C_Enemy_FightInRegsGroup_01.StartIt();
                step_C_Enemy_FightInRegsGroup_01.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_C_Enemy_FightInRegsGroup_01.outStep == 1.1f)
            {
                step_C_Enemy_FightInRegsGroup_01.RunIt();

                if (step_C_Enemy_FightInRegsGroup_01.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    step_C_Enemy_FightInRegsGroup_01.StartFinishing_OutStepIfNotFinishing();
                    goto step_C_Enemy_FightInRegsGroup_01_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (step_C_Enemy_FightInRegsGroup_01.outStep == 900f)
            {
                step_C_Enemy_FightInRegsGroup_01.SetNeedsToBeFinished();
                step_C_Enemy_FightInRegsGroup_01.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (step_C_Enemy_FightInRegsGroup_01.outStep == 901f)
            {
                step_C_Enemy_FightInRegsGroup_01.RunIt();

                if (step_C_Enemy_FightInRegsGroup_01.status == LogicJobStatus.Finished)
                {
                    step_C_Enemy_FightInRegsGroup_01.SetOutStep(1000f);
                    step_C_Fight01_Finished.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

        step_C_Enemy_FightInRegsGroup_01_End: ;

            #endregion

            #region step_C_Objects_04_RunAwayFromBridgeFailTrigger

            #region 1 Start
            if (step_C_Objects_04_RunAwayFromBridgeFailTrigger.OutStep == 1) //Start
            {
                step_C_Objects_04_RunAwayFromBridgeFailTrigger.SetEnabled(true);
                step_C_Objects_04_RunAwayFromBridgeFailTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_C_Objects_04_RunAwayFromBridgeFailTrigger.OutStep == 1.1f) //Run
            {
                if (step_C_Objects_04_RunAwayFromBridgeFailTrigger.IsPlayerIn())
                {
                    step_C_PlayerEntered_RunAwayFromBridgeFailTrigger.SetStatus(LogicFlagStatus.Active);
                    step_C_Objects_04_RunAwayFromBridgeFailTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_C_Objects_04_RunAwayFromBridgeFailTrigger.OutStep == 900f) //Finish
            {
                step_C_Objects_04_RunAwayFromBridgeFailTrigger.SetEnabled(false);
                step_C_Objects_04_RunAwayFromBridgeFailTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_C_Objects_06_ExplosionCutSceneTrigger

            #region 1 Start
            if (step_C_Objects_06_ExplosionCutSceneTrigger.OutStep == 1) //Start
            {
                step_C_Objects_06_ExplosionCutSceneTrigger.SetEnabled(true);
                step_C_Objects_06_ExplosionCutSceneTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_C_Objects_06_ExplosionCutSceneTrigger.OutStep == 1.1f) //Run
            {
                if (step_C_Objects_06_ExplosionCutSceneTrigger.IsPlayerIn())
                {
                    step_C_PlayerEntered_ExplosionCutSceneTrigger.SetStatus(LogicFlagStatus.Active);
                    step_C_Objects_06_ExplosionCutSceneTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_C_Objects_06_ExplosionCutSceneTrigger.OutStep == 900f) //Finish
            {
                step_C_Objects_06_ExplosionCutSceneTrigger.SetEnabled(false);
                step_C_Objects_06_ExplosionCutSceneTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            //D

            #region step_D_Enemy_FightInRegsGroup_01

            #region 1 Start
            if (step_D_Enemy_FightInRegsGroup_01.outStep == 1f)
            {
                step_D_Enemy_FightInRegsGroup_01.StartIt();
                step_D_Enemy_FightInRegsGroup_01.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_D_Enemy_FightInRegsGroup_01.outStep == 1.1f)
            {
                step_D_Enemy_FightInRegsGroup_01.RunIt();

                if (step_D_Enemy_FightInRegsGroup_01.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    step_D_Enemy_FightInRegsGroup_01.StartFinishing_OutStepIfNotFinishing();
                    goto step_D_Enemy_FightInRegsGroup_01_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (step_D_Enemy_FightInRegsGroup_01.outStep == 900f)
            {
                step_D_Enemy_FightInRegsGroup_01.SetNeedsToBeFinished();
                step_D_Enemy_FightInRegsGroup_01.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (step_D_Enemy_FightInRegsGroup_01.outStep == 901f)
            {
                step_D_Enemy_FightInRegsGroup_01.RunIt();

                if (step_D_Enemy_FightInRegsGroup_01.status == LogicJobStatus.Finished)
                {
                    step_D_Enemy_FightInRegsGroup_01.SetOutStep(1000f);
                }
            }
            #endregion

        step_D_Enemy_FightInRegsGroup_01_End: ;

            #endregion

            #region step_D_Enemy_FightInRegsGroup_02

            #region 1 Start
            if (step_D_Enemy_FightInRegsGroup_02.outStep == 1f)
            {
                step_D_Enemy_FightInRegsGroup_02.StartIt();
                step_D_Enemy_FightInRegsGroup_02.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_D_Enemy_FightInRegsGroup_02.outStep == 1.1f)
            {
                step_D_Enemy_FightInRegsGroup_02.RunIt();

                if (step_D_Enemy_FightInRegsGroup_02.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    step_D_Enemy_FightInRegsGroup_02.StartFinishing_OutStepIfNotFinishing();
                    goto step_D_Enemy_FightInRegsGroup_02_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (step_D_Enemy_FightInRegsGroup_02.outStep == 900f)
            {
                step_D_Enemy_FightInRegsGroup_02.SetNeedsToBeFinished();
                step_D_Enemy_FightInRegsGroup_02.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (step_D_Enemy_FightInRegsGroup_02.outStep == 901f)
            {
                step_D_Enemy_FightInRegsGroup_02.RunIt();

                if (step_D_Enemy_FightInRegsGroup_02.status == LogicJobStatus.Finished)
                {
                    step_D_Enemy_FightInRegsGroup_02.SetOutStep(1000f);
                }
            }
            #endregion

        step_D_Enemy_FightInRegsGroup_02_End: ;

            #endregion

            #region step_D_Objects_01_StartFight01Trigger

            #region 1 Start
            if (step_D_Objects_01_StartFight01Trigger.OutStep == 1) //Start
            {
                step_D_Objects_01_StartFight01Trigger.SetEnabled(true);
                step_D_Objects_01_StartFight01Trigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_D_Objects_01_StartFight01Trigger.OutStep == 1.1f) //Run
            {
                if (step_D_Objects_01_StartFight01Trigger.IsPlayerIn())
                {
                    step_D_PlayerEntered_StartFight01Trigger.SetStatus(LogicFlagStatus.Active);
                    step_D_Objects_01_StartFight01Trigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_D_Objects_01_StartFight01Trigger.OutStep == 900f) //Finish
            {
                step_D_Objects_01_StartFight01Trigger.SetEnabled(false);
                step_D_Objects_01_StartFight01Trigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_D_Objects_04_StartFight02Trigger

            #region 1 Start
            if (step_D_Objects_04_StartFight02Trigger.OutStep == 1) //Start
            {
                step_D_Objects_04_StartFight02Trigger.SetEnabled(true);
                step_D_Objects_04_StartFight02Trigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_D_Objects_04_StartFight02Trigger.OutStep == 1.1f) //Run
            {
                if (step_D_Objects_04_StartFight02Trigger.IsPlayerIn())
                {
                    step_D_PlayerEntered_StartFight02Trigger.SetStatus(LogicFlagStatus.Active);
                    step_D_Objects_04_StartFight02Trigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_D_Objects_04_StartFight02Trigger.OutStep == 900f) //Finish
            {
                step_D_Objects_04_StartFight02Trigger.SetEnabled(false);
                step_D_Objects_04_StartFight02Trigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_D_Objects_07_EndFight02Trigger

            #region 1 Start
            if (step_D_Objects_07_EndFight02Trigger.OutStep == 1) //Start
            {
                step_D_Objects_07_EndFight02Trigger.SetEnabled(true);
                step_D_Objects_07_EndFight02Trigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_D_Objects_07_EndFight02Trigger.OutStep == 1.1f) //Run
            {
                if (step_D_Objects_07_EndFight02Trigger.IsPlayerIn())
                {
                    step_D_PlayerEntered_EndFight02Trigger.SetStatus(LogicFlagStatus.Active);
                    step_D_Objects_07_EndFight02Trigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_D_Objects_07_EndFight02Trigger.OutStep == 900f) //Finish
            {
                step_D_Objects_07_EndFight02Trigger.SetEnabled(false);
                step_D_Objects_07_EndFight02Trigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_D_Objects_07_EndStepTrigger

            #region 1 Start
            if (step_D_Objects_07_EndStepTrigger.OutStep == 1) //Start
            {
                step_D_Objects_07_EndStepTrigger.SetEnabled(true);
                step_D_Objects_07_EndStepTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_D_Objects_07_EndStepTrigger.OutStep == 1.1f) //Run
            {
                if (step_D_Objects_07_EndStepTrigger.IsPlayerIn())
                {
                    step_D_PlayerEntered_EndStepTrigger.SetStatus(LogicFlagStatus.Active);
                    step_D_Objects_07_EndStepTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_D_Objects_07_EndStepTrigger.OutStep == 900f) //Finish
            {
                step_D_Objects_07_EndStepTrigger.SetEnabled(false);
                step_D_Objects_07_EndStepTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            //E

            #region step_E_Objects_01_EndStepTrigger

            #region 1 Start
            if (step_E_Objects_01_EndStepTrigger.OutStep == 1) //Start
            {
                step_E_Objects_01_EndStepTrigger.SetEnabled(true);
                step_E_Objects_01_EndStepTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_E_Objects_01_EndStepTrigger.OutStep == 1.1f) //Run
            {
                if (step_E_Objects_01_EndStepTrigger.IsPlayerIn())
                {
                    step_E_PlayerEntered_EndStepTrigger.SetStatus(LogicFlagStatus.Active);
                    step_E_Objects_01_EndStepTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_E_Objects_01_EndStepTrigger.OutStep == 900f) //Finish
            {
                step_E_Objects_01_EndStepTrigger.SetEnabled(false);
                step_E_Objects_01_EndStepTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_E_Enemy_FightInRegsGroup_01

            #region 1 Start
            if (step_E_Enemy_FightInRegsGroup_01.outStep == 1f)
            {
                step_E_Enemy_FightInRegsGroup_01.StartIt();
                step_E_Enemy_FightInRegsGroup_01.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_E_Enemy_FightInRegsGroup_01.outStep == 1.1f)
            {
                step_E_Enemy_FightInRegsGroup_01.RunIt();

                if (step_E_Enemy_FightInRegsGroup_01.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    step_E_Enemy_FightInRegsGroup_01.StartFinishing_OutStepIfNotFinishing();
                    goto step_E_Enemy_FightInRegsGroup_01_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (step_E_Enemy_FightInRegsGroup_01.outStep == 900f)
            {
                step_E_Enemy_FightInRegsGroup_01.SetNeedsToBeFinished();
                step_E_Enemy_FightInRegsGroup_01.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (step_E_Enemy_FightInRegsGroup_01.outStep == 901f)
            {
                step_E_Enemy_FightInRegsGroup_01.RunIt();

                if (step_E_Enemy_FightInRegsGroup_01.status == LogicJobStatus.Finished)
                {
                    step_E_Enemy_FightInRegsGroup_01.SetOutStep(1000f);
                }
            }
            #endregion

        step_E_Enemy_FightInRegsGroup_01_End: ;

            #endregion
        }
    }

    public override void LoadCheckPoint(float _levelStep)
    {
        base.LoadCheckPoint(_levelStep);

        #region CheckPoint B
        if (levelStep == 2)
        {
            step_B_Objects_StartPoint_Player.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);

            mapLogic.HUD_ObjectivesPage_SetObjectiveDone(1);

            return;
        }
        #endregion

        #region CheckPoint C
        if (levelStep == 3)
        {
            step_C_Objects_StartPoint_Player.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);

            return;
        }
        #endregion

        #region CheckPoint D
        if (levelStep == 4)
        {
            step_D_Objects_StartPoint_Player.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);

            TakhribePol();

            return;
        }
        #endregion

        #region CheckPoint E
        if (levelStep == 5)
        {
            step_E_Objects_StartPoint_Player.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);

            TakhribePol();

            return;
        }
        #endregion
    }

    void TakhribePol()
    {
        step_C_Objects_01_SignalDynamite_01.SetActiveRecursively(false);
        step_C_Objects_02_SignalDynamite_02.SetActiveRecursively(false);

        string animName = "Take 001";

        step_C_Objects_10_PolAnimObject.animation.Play(animName);
        step_C_Objects_10_PolAnimObject.animation[animName].time = step_C_Objects_10_PolAnimObject.animation[animName].length;

        step_C_Objects_11_SmokeParticlePol.SetActiveRecursively(true);

        SetPolColliders();
    }

    void SetPolColliders()
    {
        foreach (GameObject go in step_C_Objects_14_CollidersToDisableAfterExplosion)
        {
            go.active = false;
        }

        foreach (GameObject go in step_C_Objects_15_CollidersToEnabAfterExplosion)
        {
            go.active = true;
        }
    }

    void CheckIfShouldSetMissionFailByDetection()
    {
        if (!missionFailHasSet)
        {
            if (mapLogic.IsPlayerDetectedInCampMode())
            {
                shouldStartTimeCounterForSetMissionFail = true;
            }

            if (shouldStartTimeCounterForSetMissionFail)
            {
                timeCounterForSettingMissionFail = MathfPlus.DecByDeltatimeToZero(timeCounterForSettingMissionFail);

                if (timeCounterForSettingMissionFail == 0f)
                {
                    SetLevelStep(step_MissionFail_YouAreDetectedByEnemies);
                    missionFailHasSet = true;
                }
            }
        }
    }

    void PlayFirstMusic()
    {
        MusicController.Instance.PlayMusic(MusicSong.Action_B, 0);
    }

    void PlaySecondMusic()
    {
        MusicController.Instance.PlayMusic(MusicSong.Stress_A, 0);
    }
}
