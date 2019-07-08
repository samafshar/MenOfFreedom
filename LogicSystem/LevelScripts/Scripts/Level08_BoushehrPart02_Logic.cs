using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level08_BoushehrPart02_Logic : LevelLogic
{
    public GameObject Ally01;
    public GameObject Ally02;
    public GameObject Khaloo;

    public CutsceneController cutsceneBorj;

    public CutsceneController cutsceneEnd_Part00_TirKhordaneKhaloo;
    public CutsceneController cutsceneEnd_Part01;
    public CutsceneController cutsceneEnd_Part02;
    public GameObject beforeEndCutscene_DatPackToHide;

    public GameObject sea;

    public AudioInfo ambientExplosionDistance;

    public LogicVoiceCollection logicVoiceCollection_Ally01;
    public LogicVoiceCollection logicVoiceCollection_Ally02;

    public string _________________;

    public Transform miniMapObjectTransform_01_StepA_01;
    public Transform miniMapObjectTransform_02_StepB_01;
    public Transform miniMapObjectTransform_03_StepC_01;
    public Transform miniMapObjectTransform_04_StepC_02;
    public Transform miniMapObjectTransform_05_StepD_01;
    public Transform miniMapObjectTransform_06_StepD_02;

    public string __________________;

    #region Step_A Variables
    //Enemy
    public MapLogicJob_FightInRegsGroup step_A_Enemy_FightInRegsGroup_01;
    public MapLogicJob_FightInRegsGroup step_A_Enemy_FightInRegsGroup_02;
    public MapLogicJob_FightInRegsGroup step_A_Enemy_FightInRegsGroup_03;
    public MapLogicJob_FightInRegsGroup step_A_Enemy_FightInRegsGroup_04;

    //Objects
    public LogicTrigger step_A_Objects_01_StartFight02Trigger;
    public LogicTrigger step_A_Objects_02_StartFight03Trigger;
    public LogicTrigger step_A_Objects_03_StartFight04Trigger;
    public LogicTrigger step_A_Objects_04_EndStepTrigger;
    public LogicDieTrigger step_A_Objects_05_LogicDieTrigger01;
    public AudioInfo step_A_Objects_AudioInfoEarlyAllyFights;

    public StartPoint step_A_Objects_StartPointPlayer;

    //

    LogicFlag step_A_PlayerEntered_StartFight02Trigger = new LogicFlag();
    LogicFlag step_A_PlayerEntered_StartFight03Trigger = new LogicFlag();
    LogicFlag step_A_PlayerEntered_StartFight04Trigger = new LogicFlag();
    LogicFlag step_A_PlayerEntered_EndStepTrigger = new LogicFlag();

    #endregion

    public string ___________________;

    #region Step_B_Variables
    //Ally
    public MapLogicJob_ChainJobsGroup step_B_Ally_chainJobGroup_01_Allies;

    //Enemy
    public MapLogicJob_FightInRegsGroup step_B_Enemy_FightInRegsGroup_01;
    public MapLogicJob_FightInRegsGroup step_B_Enemy_FightInRegsGroup_02;
    public MapLogicJob_FightInRegsGroup step_B_Enemy_FightInRegsGroup_03;
    public MapLogicJob_FightInRegsGroup step_B_Enemy_FightInRegsGroup_04;
    public MapLogicJob_MachineGun step_B_Enemy_MachineGun_01;

    //Objects
    public LogicTrigger step_B_Objects_01_StopFight1_2Trigger;
    public ExecutionArea step_B_Objects_02_Execution1;
    public LogicTrigger step_B_Objects_03_StartFight3_4Trigger;
    public LogicTrigger step_B_Objects_04_StopFight3_4Trigger;
    public ExecutionArea step_B_Objects_05_Execution2;
    public GameObject step_B_Objects_06_SignalingDynamite;
    public GameObject step_B_Objects_06_NormalDynamite;
    public AudioInfo step_B_Objects_06_BombTickTick;
    public GameObject step_B_Objects_06_ExplodableDoor;
    public GameObject step_B_Objects_06_ExplodedDoor;
    public GameObject step_B_Objects_06_ExplosionEffects;
    public LogicTrigger step_B_Objects_07_EndStepBTrigger;
    public LogicDieTrigger step_B_Objects_07_DieTrigger;

    public StartPoint step_B_Objects_StartPoint_Player;
    public StartPoint step_B_Objects_StartPoint_Ally01;
    public StartPoint step_B_Objects_StartPoint_Ally02;

    LogicFlag step_B_PlayerEntered_StopFight1_2Trigger = new LogicFlag();
    LogicFlag step_B_ChainJobAllies_Finished = new LogicFlag();
    LogicFlag step_B_PlayerEntered_StartFight3_4Trigger = new LogicFlag();
    LogicFlag step_B_PlayerEntered_StopFight3_4Trigger = new LogicFlag();
    LogicFlag step_B_PlayerEntered_EndStepBTrigger = new LogicFlag();
    LogicFlag step_B_Fight01_Finish = new LogicFlag();
    LogicFlag step_B_Fight02_Finish = new LogicFlag();
    LogicFlag step_B_Fight03_Finish = new LogicFlag();
    LogicFlag step_B_Fight04_Finish = new LogicFlag();
    LogicFlag step_B_MachineGun_Finish = new LogicFlag();

    LogicObjective step_B_Dynamite;

    float step_B_Timer = 0f;
    float step_B_MaxTimeToExplodeBomb = 2f;

    int step_B_AllyChainJobsGlobalLogicIndex = -1;

    #endregion

    public string _____________________;

    #region Step_C_Variables
    //Ally
    public MapLogicJob_ChainJobsGroup step_C_Ally_chainJobGroup_01_Allies;

    //Enemy
    public MapLogicJob_FightInRegsGroup step_C_Enemy_FightInRegsGroup_01;
    public MapLogicJob_FightInRegsGroup step_C_Enemy_FightInRegsGroup_02;

    //Objects
    public LogicTrigger step_C_Objects_01_StartStepTrigger;
    public ExecutionArea step_C_Objects_02_Execution01;
    public LogicTrigger step_C_Objects_03_EndStepTrigger;
    public LogicDieTrigger step_C_Objects_04_DieTrigger;

    public StartPoint step_C_Objects_StartPoint_Player;
    public StartPoint step_C_Objects_StartPoint_Ally01;
    public StartPoint step_C_Objects_StartPoint_Ally02;

    public float step_C_MaxTimeForFightBandar = 20f;

    LogicFlag step_C_PlayerEntered_StartStepTrigger = new LogicFlag();
    LogicFlag step_C_PlayerEntered_EndStepTrigger = new LogicFlag();
    LogicFlag step_C_AllyChainJob01_Finished = new LogicFlag();
    LogicFlag step_C_Fight01_Finished = new LogicFlag();
    LogicFlag step_C_Fight02_Finished = new LogicFlag();

    int step_C_AllyChainJobsGlobalLogicIndex = -1;

    float step_C_Timer = 0f;

    #endregion

    public string _______________________;

    #region Step_D_Variables
    //Ally
    public MapLogicJob_ChainJobsGroup step_D_Ally_chainJobGroup_01_Allies;

    //Enemy
    public MapLogicJob_FightInRegsGroup step_D_Enemy_FightInRegsGroup_01;
    public MapLogicJob_FightInRegsGroup step_D_Enemy_FightInRegsGroup_02;

    //Objects
    public LogicTrigger step_D_Objects_01_StartStepTrigger;
    public LogicTrigger step_D_Objects_01_FailTrigger;
    public LogicTrigger step_D_Objects_02_EndFight01Trigger;
    public LogicTrigger step_D_Objects_03_AlliesChangePosTrigger;
    public LogicTrigger step_D_Objects_04_StartFight02Trigger;
    public LogicDieTrigger step_D_Objects_05_Fight01DieTrigger;
    public LogicTrigger step_D_Objects_06_CutSceneTrigger;
    public LogicDieTrigger step_D_Objects_06_Fight02DieTrigger;
    public LogicTrigger step_D_Objects_07_PellehaTrigger;

    public StartPoint step_D_Objects_StartPoint_Player;
    public StartPoint step_D_Objects_StartPoint_Ally01;
    public StartPoint step_D_Objects_StartPoint_Ally02;
    public StartPoint step_D_Objects_StartPoint_PlayerAfterCutscene;

    int snipeAmmoCount = 200;

    LogicFlag step_D_PlayerEntered_StartStepTrigger = new LogicFlag();
    LogicFlag step_D_PlayerEntered_FailTrigger = new LogicFlag();
    LogicFlag step_D_PlayerEntered_EndFight01Trigger = new LogicFlag();
    LogicFlag step_D_PlayerEntered_AlliesChangePosTrigger = new LogicFlag();
    LogicFlag step_D_PlayerEntered_StartFight02Trigger = new LogicFlag();
    LogicFlag step_D_PlayerEntered_CutSceneTrigger = new LogicFlag();
    LogicFlag step_D_PlayerEntered_PellehaTrigger = new LogicFlag();
    LogicFlag step_D_Fight01_Finished = new LogicFlag();
    LogicFlag step_D_Fight02_Finished = new LogicFlag();

    int step_D_AllyChainJobsGlobalLogicIndex = -1;

    bool isStepD = false;

    #endregion

    public string _________________________;

    #region Step_E_Variables
    //Ally
    public MapLogicJob_ChainJobsGroup step_E_Ally_ChainJobGroup_Ally01;
    public MapLogicJob_ChainJobsGroup step_E_Ally_ChainJobGroup_Ally02;

    //Enemy
    public MapLogicJob_FightInRegsGroup step_E_Enemy_FightInRegsGroup_01;

    //Objects
    public LogicTrigger step_E_Objects_01_PelleFailTrigger;
    public SoundLoopByDelayTime step_E_Objects_01_FarExplosionSounds;
    public AudioInfo step_E_Objects_01_Alarm;
    public LogicTrigger step_E_Objects_02_AllyEndMasirCheckTrigger;
    public GameObject step_E_Objects_03_Door;

    public List<SoldierInfo> step_E_HiddenSoldiers = new List<SoldierInfo>();

    public StartPoint step_E_Objects_StartPoint_Player;
    public StartPoint step_E_Objects_StartPoint_Ally01;
    public StartPoint step_E_Objects_StartPoint_Ally02;

    public StartPoint step_E_Objects_StartPoint_Ally01_AfterKickDoor;
    public AnimsList step_E_Objects_AnimsList_Ally01_AfterKickDoor;

    //

    LogicFlag step_E_PlayerEntered_PelleFailTrigger = new LogicFlag();
    LogicFlag step_E_Fight01_Finished = new LogicFlag();
    LogicFlag step_E_AllyEnter_EndMasirCheckTrigger = new LogicFlag();

    List<SoldierDetectorCube> soldierDetectorCubes = new List<SoldierDetectorCube>();
    List<SoldierNearbyHitDetection> SoldierNearbyHitDetections = new List<SoldierNearbyHitDetection>();

    int step_E_Ally01ChainJobsGlobalLogicIndex = -1;
    int step_E_Ally02ChainJobsGlobalLogicIndex = -1;

    float step_E_TimeToFailMission = 5f;
    float step_E_TimeToAlarm = 5f;

    float step_E_TimeToEnterJailAlly02 = 1.2f;
    float step_E_TimeToStartKick = .5f;
    float step_E_TimeToStartDoorAnim = 1.5f;
    float step_E_TimeToWaitEnterTheJailDoor = 0.81f;
    float step_E_PlaceAllyAfterKickDoorTimer = 0.7f;
    bool step_E_IsAllyPlacedAfterKickDoor = false;

    bool step_E_shouldCountGoingToWallsTime = false;
    float step_E_GoingToWallsTimeCounter = 4f;

    float step_E_StandNearWallsTimeCounter = 1.5f;

    float step_E_TimeCounterToShowShootingSoldsHint = 2.7f;

    float step_E_Timer = 0f;

    bool isStepE = false;

    #endregion

    public string ___________________________;

    #region Step_F_Variables

    //Ally
    public MapLogicJob_ChainJobsGroup step_F_Ally_ChainJobGroup_Ally01;
    public MapLogicJob_ChainJobsGroup step_F_Ally_ChainJobGroup_Ally02;
    public MapLogicJob_ChainJobsGroup step_F_Ally_ChainJobGroup_Khaloo;
    public GameObject[] step_F_Ally_HeadArmors;

    //Enemy
    public MapLogicJob_FightInRegsGroup step_F_Enemy_FightInRegsGroup_01;
    public MapLogicJob_FightInRegsGroup step_F_Enemy_FightInRegsGroup_02;
    public MapLogicJob_FightInRegsGroup step_F_Enemy_FightInRegsGroup_03;
    public MapLogicJob_FightInRegsGroup step_F_Enemy_FightInRegsGroup_04;
    public MapLogicJob_FightInRegsGroup step_F_Enemy_FightInRegsGroup_05;

    //Objects
    public GameObject step_F_Objects_01_Door_Ruberu;
    public GameObject step_F_Objects_01_Door_Chapi;
    public GameObject step_F_Objects_01_Door_Rasti;
    public LogicTrigger step_F_Objects_02_CountingJailSoldiersTrigger;
    public LogicDieTrigger step_F_Objects_03_DieTrigger_JailKhaloo;
    public LogicTrigger step_F_Objects_04_CountingInsideSoldiersTrigger;
    public LogicTrigger step_F_Objects_04_CountingOutsideSoldiersTrigger;
    public LogicTrigger step_F_Objects_05_EscapeTrigger;

    public StartPoint step_F_Objects_StartPoint_Player;
    public StartPoint step_F_Objects_StartPoint_Ally01;
    public StartPoint step_F_Objects_StartPoint_Ally02;
    public StartPoint step_F_Objects_StartPoint_Khaloo;

    //

    public int step_F_NumOfInsideTriggerSoldiersToFail = 3;

    public float step_F_TimeForKhalooPrisonBreak = 40f;

    LogicFlag step_F_Fight01_Finished = new LogicFlag();
    LogicFlag step_F_Fight02_Finished = new LogicFlag();
    LogicFlag step_F_Fight03_Finished = new LogicFlag();
    LogicFlag step_F_Fight04_Finished = new LogicFlag();
    LogicFlag step_F_Fight05_Finished = new LogicFlag();
    LogicFlag step_F_CountingJailSoldierTrigger = new LogicFlag();
    LogicFlag step_F_CountingInsideSoldierTrigger = new LogicFlag();
    LogicFlag step_F_CountingOutsideSoldierTrigger = new LogicFlag();
    LogicFlag step_F_AlliesEntered_EscapeTrigger = new LogicFlag();

    int step_F_Ally01ChainJobsGlobalLogicIndex = -1;
    int step_F_Ally02ChainJobsGlobalLogicIndex = -1;
    int step_F_KhalooChainJobsGlobalLogicIndex = -1;

    int maxNumOfSoldiersInsidePrison = 2;

    float step_F_Timer = 0f;

    float step_F_TimeForAlarmOShuresh = 5f;
    float step_F_TimeForAnimationDoorChap = 3f;
    float step_F_TimeForAnimationDoorRuBRu = 2f;
    float step_F_TimeForAnimationDoorRasti = 2f;
    float step_F_TimeForStartChainJobsAlly01 = 9f;
    float step_F_TimeForStartChainJobsAlly02 = 2f;
    float step_F_TimeForStartChainJobsKhaloo = 1f;
    float step_F_TimeForFightInsidePrison = 30f;
    float step_F_TimeForFightOutsidePrison = 15f;

    float step_F_Ally_ReceiveDamageCoef = .4f;

    float step_F_TimeCounterToShowSnipeTimeScaleHint = 2f;

    bool isStepF = false;

    #endregion

    float missionFailType;

    public override void StartIt()
    {
        base.StartIt();

       // LoadCheckPoint(6.4f);
    }

    public override void RunIt()
    {
        base.RunIt();

        if (step == 1)
        {
        StartLevelSteps:

            #region Step A

            #region 0.1 Start first cutscene
            if (levelStep == 0.1f)
            {
                SetLevelStep(1);
            }
            #endregion

            #region 1 Start First Trigger For Player Enter
            if (levelStep == 1)
            {
                SaveCheckPoint(1f);

                mapLogic.HUD_AddOnlyMinimap3DObj(miniMapObjectTransform_01_StepA_01, "StepA_01");

                mapLogic.HUD_ObjectivesPage_SetActiveObjective(1);
                mapLogic.HUD_ShowNewMission(0);

                step_A_Objects_StartPointPlayer.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);

                step_A_Enemy_FightInRegsGroup_01.StartOutStepIfNotStarted();

                step_A_Objects_01_StartFight02Trigger.StartOutStepIfNotStarted();

                PlayFirstMusic();

                step_A_Objects_AudioInfoEarlyAllyFights.Play();

                SetLevelStep(1.1f);
            }
            #endregion

            #region 1.1 Wait For Fight02 Trigger
            if (levelStep == 1.1f)
            {
                if (step_A_PlayerEntered_StartFight02Trigger.IsEverActivated())
                {
                    step_A_Enemy_FightInRegsGroup_02.StartOutStepIfNotStarted();

                    step_A_Objects_02_StartFight03Trigger.StartOutStepIfNotStarted();

                    SetLevelStep(1.2f);
                }
            }
            #endregion

            #region 1.2 Wait For Fight03 Trigger
            if (levelStep == 1.2f)
            {
                if (step_A_PlayerEntered_StartFight03Trigger.IsEverActivated())
                {
                    step_A_Enemy_FightInRegsGroup_03.StartOutStepIfNotStarted();

                    step_A_Objects_03_StartFight04Trigger.StartOutStepIfNotStarted();

                    SetLevelStep(1.3f);
                }
            }
            #endregion

            #region 1.3 Wait For Fight04 Trigger
            if (levelStep == 1.3f)
            {
                if (step_A_PlayerEntered_StartFight04Trigger.IsEverActivated())
                {
                    step_A_Enemy_FightInRegsGroup_04.StartOutStepIfNotStarted();

                    step_A_Objects_04_EndStepTrigger.StartOutStepIfNotStarted();

                    SetLevelStep(1.4f);
                }
            }
            #endregion

            #region 1.4 Wait For End Trigger
            if (levelStep == 1.4f)
            {
                if (step_A_PlayerEntered_EndStepTrigger.IsEverActivated())
                {
                    mapLogic.HUD_RemoveMinimap3DObj("StepA_01");

                    mapLogic.HUD_ObjectivesPage_SetObjectiveDone(1);
                    mapLogic.HUD_ShowCompleteMission(0);

                    step_A_Enemy_FightInRegsGroup_01.StopCreatingMoreSoldiers();
                    step_A_Enemy_FightInRegsGroup_02.StopCreatingMoreSoldiers();
                    step_A_Enemy_FightInRegsGroup_03.StopCreatingMoreSoldiers();
                    step_A_Enemy_FightInRegsGroup_04.StopCreatingMoreSoldiers();

                    step_A_Objects_05_LogicDieTrigger01.StartIt();

                    SetLevelStep(2f);
                }
            }
            #endregion

            #endregion

            #region Step B

            #region 2 Start Step B
            if (levelStep == 2f)
            {
                SaveCheckPoint(2f);

                mapLogic.HUD_AddOnlyMinimap3DObj(miniMapObjectTransform_02_StepB_01, "StepB_01");

                mapLogic.HUD_ObjectivesPage_SetActiveObjective(2);
                mapLogic.HUD_ShowNewMission(1);

                logicVoiceCollection_Ally01.PlayName("StepB_01_Fight1Va2");
                logicVoiceCollection_Ally02.PlayName("StepB_01_Fight1Va2");

                step_B_Enemy_FightInRegsGroup_01.StartOutStepIfNotStarted();

                step_B_Enemy_FightInRegsGroup_02.StartOutStepIfNotStarted();

                step_B_Objects_01_StopFight1_2Trigger.StartOutStepIfNotStarted();

                step_B_Objects_02_Execution1.StartIt();

                step_B_Objects_StartPoint_Ally01.PlaceCharacterOnIt(Ally01);
                step_B_Objects_StartPoint_Ally01.PlaceCharacterOnIt(Ally02);

                step_B_AllyChainJobsGlobalLogicIndex = 0;
                step_B_Ally_chainJobGroup_01_Allies.StartOutStepIfNotStarted();

                PlayFirstMusic();

                SetLevelStep(2.1f);
            }
            #endregion

            #region 2.1 Wait For Stop Fight 1 And 2
            if (levelStep == 2.1f)
            {
                if (step_B_PlayerEntered_StopFight1_2Trigger.IsEverActivated())
                {
                    logicVoiceCollection_Ally01.StopCurVoiceAfterItsFinishing();
                    logicVoiceCollection_Ally02.StopCurVoiceAfterItsFinishing();

                    step_B_Enemy_FightInRegsGroup_01.StopCreatingMoreSoldiers();

                    step_B_Enemy_FightInRegsGroup_02.StopCreatingMoreSoldiers();

                    step_B_Objects_03_StartFight3_4Trigger.StartOutStepIfNotStarted();

                    SetLevelStep(2.2f);
                }
            }
            #endregion

            #region 2.2 Wait For Start Fight 3 And 4
            if (levelStep == 2.2f)
            {
                if (step_B_PlayerEntered_StartFight3_4Trigger.IsEverActivated()
                                || (step_B_Fight01_Finish.IsEverActivated() && step_B_Fight02_Finish.IsEverActivated()))
                {
                    logicVoiceCollection_Ally01.PlayName("StepB_02_MachineGun");
                    logicVoiceCollection_Ally02.PlayName("StepB_02_Fight3Va4");

                    step_B_AllyChainJobsGlobalLogicIndex = 1;
                    step_B_Ally_chainJobGroup_01_Allies.Init_SetNewGlobalLogicIndex(step_B_AllyChainJobsGlobalLogicIndex);

                    step_B_Enemy_FightInRegsGroup_03.StartOutStepIfNotStarted();

                    step_B_Enemy_FightInRegsGroup_04.StartOutStepIfNotStarted();

                    step_B_Enemy_MachineGun_01.StartOutStepIfNotStarted();

                    step_B_Objects_04_StopFight3_4Trigger.StartOutStepIfNotStarted();

                    step_B_Objects_05_Execution2.StartIt();

                    SetLevelStep(2.3f);
                }
            }
            #endregion

            #region 2.3 Wait For Stop Fight 3 And 4
            if (levelStep == 2.3f)
            {
                if (step_B_PlayerEntered_StopFight3_4Trigger.IsEverActivated())
                {
                    step_B_AllyChainJobsGlobalLogicIndex = 2;
                    step_B_Ally_chainJobGroup_01_Allies.Init_SetNewGlobalLogicIndex(step_B_AllyChainJobsGlobalLogicIndex);

                    step_B_Enemy_FightInRegsGroup_03.StopCreatingMoreSoldiers();

                    step_B_Enemy_FightInRegsGroup_04.StopCreatingMoreSoldiers();

                    step_B_Enemy_MachineGun_01.StopCreatingMoreSoldiers();

                    step_B_Objects_07_EndStepBTrigger.StartOutStepIfNotStarted();

                    SetLevelStep(2.31f);
                }
            }
            #endregion

            #region 2.31 Wait For Finish Fights 03-04
            if (levelStep == 2.31f)
            {
                if (step_B_Fight03_Finish.IsEverActivated() && step_B_Fight04_Finish.IsEverActivated()
                                                            && step_B_MachineGun_Finish.IsEverActivated())
                {
                    mapLogic.HUD_ObjectivesPage_SetObjectiveDone(2);
                    mapLogic.HUD_ShowCompleteMission(1);

                    mapLogic.HUD_ObjectivesPage_SetActiveObjective(3);
                    mapLogic.HUD_ShowNewMission(2);

                    logicVoiceCollection_Ally01.StopCurVoiceAfterItsFinishing();
                    logicVoiceCollection_Ally02.StopCurVoiceAfterItsFinishing();

                    step_B_Objects_06_SignalingDynamite.SetActiveRecursively(true);

                    step_B_Dynamite = step_B_Objects_06_SignalingDynamite.GetComponent<LogicObjective>();

                    SetLevelStep(2.32f);
                }
            }
            #endregion

            #region 2.32 Wait For Finish Fights 03-04
            if (levelStep == 2.32f)
            {
                if (step_B_Dynamite.IsDone)
                {
                    step_B_Objects_06_NormalDynamite.SetActiveRecursively(true);

                    step_B_Objects_06_SignalingDynamite.SetActiveRecursively(false);

                    step_B_Objects_06_BombTickTick.Play();

                    step_B_Timer = step_B_MaxTimeToExplodeBomb;

                    SetLevelStep(2.33f);
                }
            }
            #endregion

            #region 2.33 Timer For Bomb
            if (levelStep == 2.33f)
            {
                step_B_Timer = MathfPlus.DecByDeltatimeToZero(step_B_Timer);

                if (step_B_Timer == 0)
                {
                    SetLevelStep(2.34f);
                }
            }
            #endregion

            #region 2.34 Explode
            if (levelStep == 2.34f)
            {
                step_B_Objects_06_ExplodableDoor.SetActiveRecursively(false);

                step_B_Objects_06_ExplodedDoor.SetActiveRecursively(true);

                step_B_Objects_06_NormalDynamite.SetActiveRecursively(false);

                step_B_Objects_06_ExplosionEffects.SetActiveRecursively(true);

                step_B_Objects_06_BombTickTick.Stop();

                SetLevelStep(2.4f);
            }
            #endregion

            #region 2.4 Wait For End Step B
            if (levelStep == 2.4f)
            {
                if (step_B_PlayerEntered_EndStepBTrigger.IsEverActivated())
                {
                    mapLogic.HUD_ObjectivesPage_SetObjectiveDone(3);
                    mapLogic.HUD_ShowCompleteMission(2);

                    mapLogic.HUD_RemoveMinimap3DObj("StepB_01");

                    step_B_Ally_chainJobGroup_01_Allies.StartFinishing_OutStepIfNotFinishing();

                    step_B_Objects_07_DieTrigger.StartIt();

                    step_B_Objects_02_Execution1.EndIt();

                    step_B_Objects_05_Execution2.EndIt();

                    SetLevelStep(3f);
                }
            }
            #endregion

            #endregion

            #region Step C

            #region 3 Start Step C
            if (levelStep == 3f)
            {
                SaveCheckPoint(3f);

                sea.SetActiveRecursively(true);

                mapLogic.HUD_ObjectivesPage_SetActiveObjective(4);
                mapLogic.HUD_ShowNewMission(3);

                mapLogic.HUD_AddOnlyMinimap3DObj(miniMapObjectTransform_03_StepC_01, "StepC_01");

                step_C_AllyChainJobsGlobalLogicIndex = 0;
                step_C_Ally_chainJobGroup_01_Allies.StartOutStepIfNotStarted();

                step_C_Objects_01_StartStepTrigger.StartOutStepIfNotStarted();

                step_E_Objects_01_FarExplosionSounds.StartIt();

                PlayFirstMusic();

                SetLevelStep(3.1f);
            }
            #endregion

            #region 3.1 Wait For Start Step
            if (levelStep == 3.1f)
            {
                if (step_C_PlayerEntered_StartStepTrigger.IsEverActivated())
                {
                    mapLogic.HUD_RemoveMinimap3DObj("StepC_01");

                    logicVoiceCollection_Ally01.PlayName("StepC_01_FightSkele");
                    logicVoiceCollection_Ally02.PlayName("StepC_01_FightSkele");

                    step_C_AllyChainJobsGlobalLogicIndex = 1;
                    step_C_Ally_chainJobGroup_01_Allies.Init_SetNewGlobalLogicIndex(step_C_AllyChainJobsGlobalLogicIndex);

                    step_C_Enemy_FightInRegsGroup_01.StartOutStepIfNotStarted();

                    step_C_Enemy_FightInRegsGroup_02.StartOutStepIfNotStarted();

                    ambientExplosionDistance.Play();

                    step_C_Objects_02_Execution01.StartIt();

                    step_C_Timer = step_C_MaxTimeForFightBandar;

                    SetLevelStep(3.2f);
                }
            }
            #endregion

            #region 3.2 Timer For Eskele
            if (levelStep == 3.2f)
            {
                if (!MapLogic.Instance.isPlayerHidden)
                {
                    step_C_Timer = MathfPlus.DecByDeltatimeToZero(step_C_Timer);
                    if (step_C_Timer == 0)
                    {
                        SetLevelStep(3.3f);
                    }
                }
            }
            #endregion

            #region 3.3 Stop Eskle Fight
            if (levelStep == 3.3f)
            {
                mapLogic.HUD_AddOnlyMinimap3DObj(miniMapObjectTransform_04_StepC_02, "StepC_02");

                step_C_AllyChainJobsGlobalLogicIndex = 2;
                step_C_Ally_chainJobGroup_01_Allies.Init_SetNewGlobalLogicIndex(step_C_AllyChainJobsGlobalLogicIndex);

                step_C_Enemy_FightInRegsGroup_01.StopCreatingMoreSoldiers();

                step_C_Enemy_FightInRegsGroup_02.StopCreatingMoreSoldiers();

                step_C_Objects_02_Execution01.EndIt();

                step_C_Objects_03_EndStepTrigger.StartOutStepIfNotStarted();

                SetLevelStep(3.4f);
            }
            #endregion

            #region 3.4 Check End Step
            if (levelStep == 3.4f)
            {
                if (step_C_PlayerEntered_EndStepTrigger.IsEverActivated()
                                || (step_C_Fight01_Finished.IsEverActivated() && step_C_Fight02_Finished.IsEverActivated()))
                {
                    mapLogic.HUD_RemoveMinimap3DObj("StepC_02");

                    logicVoiceCollection_Ally01.StopCurVoiceAfterItsFinishing();
                    logicVoiceCollection_Ally02.StopCurVoiceAfterItsFinishing();

                    step_C_Objects_04_DieTrigger.StartIt();

                    step_C_Ally_chainJobGroup_01_Allies.StartFinishing_OutStepIfNotFinishing();

                    SetLevelStep(3.5f);
                }
            }
            #endregion

            #region 3.5 Wait For Chain Job Stop
            if (levelStep == 3.5f)
            {
                if (step_C_AllyChainJob01_Finished.IsEverActivated())
                {
                    SetLevelStep(4f);
                }
            }
            #endregion

            #endregion

            #region Step D

            #region Fail
            if (isStepD && step_D_PlayerEntered_FailTrigger.IsEverActivated())
            {
                SetLevelStep(step_MissionFail_YouLeftFightArea);
                goto EndLevelSteps;
            }
            #endregion

            #region 4 Start Step D
            if (levelStep == 4f)
            {
                SaveCheckPoint(4f);

                isStepD = true;

                mapLogic.HUD_AddOnlyMinimap3DObj(miniMapObjectTransform_05_StepD_01, "StepD_01");

                logicVoiceCollection_Ally01.PlayName("StepD_01_RaBioft");

                step_D_AllyChainJobsGlobalLogicIndex = 0;
                step_D_Ally_chainJobGroup_01_Allies.StartOutStepIfNotStarted();

                step_D_Objects_01_StartStepTrigger.StartOutStepIfNotStarted();

                PlayFirstMusic();

                SetLevelStep(4.1f);
            }
            #endregion

            #region 4.1 Wait For Start Fight01
            if (levelStep == 4.1f)
            {
                if (step_D_PlayerEntered_StartStepTrigger.IsEverActivated())
                {
                    sea.SetActiveRecursively(false);

                    logicVoiceCollection_Ally02.PlayName("StepD_01_Fight01");

                    step_D_Enemy_FightInRegsGroup_01.StartOutStepIfNotStarted();

                    step_D_AllyChainJobsGlobalLogicIndex = 1;
                    step_D_Ally_chainJobGroup_01_Allies.Init_SetNewGlobalLogicIndex(step_D_AllyChainJobsGlobalLogicIndex);

                    step_D_Objects_02_EndFight01Trigger.StartOutStepIfNotStarted();

                    step_D_Objects_01_FailTrigger.StartOutStepIfNotStarted();

                    SetLevelStep(4.2f);
                }
            }
            #endregion

            #region 4.2 End Fight01
            if (levelStep == 4.2f)
            {
                if (step_D_PlayerEntered_EndFight01Trigger.IsEverActivated() || step_D_Fight01_Finished.IsEverActivated())
                {
                    step_D_Enemy_FightInRegsGroup_01.StopCreatingMoreSoldiers();

                    step_D_Objects_05_Fight01DieTrigger.StartIt();

                    step_D_AllyChainJobsGlobalLogicIndex = 2;
                    step_D_Ally_chainJobGroup_01_Allies.Init_SetNewGlobalLogicIndex(step_D_AllyChainJobsGlobalLogicIndex);

                    step_D_Objects_03_AlliesChangePosTrigger.StartOutStepIfNotStarted();

                    step_D_Objects_04_StartFight02Trigger.StartOutStepIfNotStarted();

                    SetLevelStep(4.3f);
                }
            }
            #endregion

            #region 4.3 Allies Change Pos
            if (levelStep == 4.3f)
            {
                if (step_D_PlayerEntered_AlliesChangePosTrigger.IsEverActivated() || step_D_Fight01_Finished.IsEverActivated())
                {
                    step_D_AllyChainJobsGlobalLogicIndex = 3;
                    step_D_Ally_chainJobGroup_01_Allies.Init_SetNewGlobalLogicIndex(step_D_AllyChainJobsGlobalLogicIndex);

                    SetLevelStep(4.4f);
                }
            }
            #endregion

            #region 4.4 Start Fight02
            if (levelStep == 4.4f)
            {
                if (step_D_PlayerEntered_StartFight02Trigger.IsEverActivated())
                {
                    logicVoiceCollection_Ally01.PlayName("StepD_02_Fight02");

                    step_D_Enemy_FightInRegsGroup_02.StartOutStepIfNotStarted();

                    step_D_AllyChainJobsGlobalLogicIndex = 4;
                    step_D_Ally_chainJobGroup_01_Allies.Init_SetNewGlobalLogicIndex(step_D_AllyChainJobsGlobalLogicIndex);

                    step_D_Objects_06_CutSceneTrigger.StartOutStepIfNotStarted();

                    SetLevelStep(4.5f);
                }
            }
            #endregion

            #region 4.5 Wait For Cut Scene Trigger
            if (levelStep == 4.5f)
            {
                if (step_D_PlayerEntered_CutSceneTrigger.IsEverActivated())
                {
                    SetLevelStep(4.6f);
                }
            }
            #endregion

            #region 4.6 Fade Out
            if (levelStep == 4.6f)
            {
                mapLogic.HUD_RemoveMinimap3DObj("StepD_01");

                step_D_Objects_06_Fight02DieTrigger.StartIt();

                mapLogic.blackScreenFader.StartFadingOut();

                MusicController.Instance.EndMusicWithFade(MusicFadeType.Fast);

                SetLevelStep(4.62f);
            }
            #endregion

            #region 4.62 Wait For Fade Out
            if (levelStep == 4.62f)
            {
                if (mapLogic.blackScreenFader.isFadingFinished)
                {
                    SetLevelStep(4.64f);
                }
            }
            #endregion

            #region 4.64 Run Cutscene
            if (levelStep == 4.64f)
            {
                cutsceneBorj.StartIt();

                SetLevelStep(4.65f);
            }
            #endregion

            #region 4.65 Wait Till Cutscene End
            if (levelStep == 4.65f)
            {
                if (cutsceneBorj.status == CutsceneStatus.Finished)
                {
                    SetLevelStep(4.7f);
                }
            }
            #endregion

            #region 4.7f Start End black screen
            if (levelStep == 4.7f)
            {
                step_D_Objects_StartPoint_PlayerAfterCutscene.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);

                mapLogic.blackScreenFader.StartFadingIn();

                mapLogic.HUD_ObjectivesPage_SetActiveObjective(5);
                mapLogic.HUD_ShowNewMission(4);

                mapLogic.HUD_AddOnlyMinimap3DObj(miniMapObjectTransform_06_StepD_02, "StepD_02");

                step_E_Objects_StartPoint_Ally01.PlaceCharacterOnIt(Ally01);
                step_E_Objects_StartPoint_Ally02.PlaceCharacterOnIt(Ally02);

                step_D_Objects_07_PellehaTrigger.StartOutStepIfNotStarted();

                PlayerCharacterNew.Instance.ReplaceGun(PlayerGunName.Snipe, snipeAmmoCount, PlayerGunName.Winchester);

                SetLevelStep(4.75f);
            }
            #endregion

            #region 4.75f Wait For Balaye Borj
            if (levelStep == 4.75f)
            {
                if (step_D_PlayerEntered_PellehaTrigger.IsEverActivated())
                {
                    mapLogic.HUD_ObjectivesPage_SetObjectiveDone(5);
                    mapLogic.HUD_ShowCompleteMission(4);

                    mapLogic.HUD_RemoveMinimap3DObj("StepD_02");

                    isStepD = false;

                    ambientExplosionDistance.StartDecreasingCustomVolumeToEnd(3f);

                    SetLevelStep(5f);
                }
            }
            #endregion

            #endregion

            #region Step E

            #region Fail
            if ((isStepE || isStepF) && step_E_PlayerEntered_PelleFailTrigger.IsEverActivated())
            {
                SetLevelStep(step_MissionFail_YouLeftFightArea);
                goto EndLevelSteps;
            }
            #endregion

            #region GoingToWallsTime
            if (step_E_shouldCountGoingToWallsTime)
                step_E_GoingToWallsTimeCounter = MathfPlus.DecByDeltatimeToZero(step_E_GoingToWallsTimeCounter); 
            #endregion

            #region 5 Start Step E
            if (levelStep == 5)
            {
                SaveCheckPoint(5f);

                isStepE = true;

                mapLogic.HUD_ObjectivesPage_SetActiveObjective(6);
                mapLogic.HUD_ShowNewMission(5);

               // step_E_Objects_01_FarExplosionSounds.StartIt();

                step_E_Objects_01_PelleFailTrigger.StartOutStepIfNotStarted();

                step_E_Ally01ChainJobsGlobalLogicIndex = 0;
                step_E_Ally_ChainJobGroup_Ally01.StartOutStepIfNotStarted();

                step_E_Ally02ChainJobsGlobalLogicIndex = 0;
                step_E_Ally_ChainJobGroup_Ally02.StartOutStepIfNotStarted();

                ShowHiddenSoldiers();
                step_E_Enemy_FightInRegsGroup_01.StartOutStepIfNotStarted();

                SetLevelStep(5.1f);

                goto EndLevelSteps;
            }
            #endregion

            #region 5.1 Init Part -> Stealth Kills
            if (levelStep == 5.1f)
            {
                InitLogicPartStealthKills();

                SetLevelStep(5.2f);
            }
            #endregion

            #region 5.2 Check For Stealth Kills
            if (levelStep == 5.2f)
            {
                if (IsSnipeModeFail())
                {
                    SetLevelStep(5.3f);

                    goto EndLevelSteps;
                }

                if (CanGoNearWall())
                {
                    SetLevelStep(5.6f);
                }

                if (!GameController.lvlBushehr2_IsShootingHintShownBefore)
                {
                    step_E_TimeCounterToShowShootingSoldsHint = MathfPlus.DecByDeltatimeToZero(step_E_TimeCounterToShowShootingSoldsHint);

                    if (step_E_TimeCounterToShowShootingSoldsHint == 0)
                    {
                        GameController.lvlBushehr2_IsShootingHintShownBefore = true;

                        mapLogic.TryToPauseGameAndJustShowHint(HUDGroupName.LvlBushehr02, HUDControlName.BlackBG, HUDControlName.LvlBushehr02Hint_Shooting);
                    }
                }
            }
            #endregion

            #region 5.3 Init Fail Situation
            if (levelStep == 5.3f)
            {
                step_E_Timer = step_E_TimeToAlarm;

                mapLogic.SetPlayerIsDetectedInCampMode();

                SetLevelStep(5.35f);
            }
            #endregion

            #region 5.35 Timer To Alarm
            if (levelStep == 5.35f)
            {
                step_E_Timer = MathfPlus.DecByDeltatimeToZero(step_E_Timer);
                if (step_E_Timer == 0)
                {
                    step_E_Objects_01_Alarm.Play();

                    step_E_Timer = step_E_TimeToFailMission;

                    SetLevelStep(5.4f);
                }

                if (DoAllSoldiersDie())
                {
                    SetLevelStep(5.6f);
                }
            }
            #endregion

            #region 5.4 Timer To Fail
            if (levelStep == 5.4f)
            {
                step_E_Timer = MathfPlus.DecByDeltatimeToZero(step_E_Timer);
                if (step_E_Timer == 0)
                {
                    SetLevelStep(5.5f);
                }
            }
            #endregion

            #region 5.5 Fail Mission
            if (levelStep == 5.5f)
            {
                SetLevelStep(missionFailType);
                goto EndLevelSteps;
            }
            #endregion

            #region 5.6 Go Near Wall
            if (levelStep == 5.6f)
            {
                step_E_Ally01ChainJobsGlobalLogicIndex = 1;
                step_E_Ally_ChainJobGroup_Ally01.Init_SetNewGlobalLogicIndex(step_E_Ally01ChainJobsGlobalLogicIndex);

                step_E_Ally02ChainJobsGlobalLogicIndex = 1;
                step_E_Ally_ChainJobGroup_Ally02.Init_SetNewGlobalLogicIndex(step_E_Ally02ChainJobsGlobalLogicIndex);

                step_E_shouldCountGoingToWallsTime = true;

                SetLevelStep(5.65f);
            }
            #endregion

            #region 5.65 Check If Can Go Through Jail
            if (levelStep == 5.65f)
            {
                if (IsSnipeModeFail())
                {
                    SetLevelStep(5.66f);

                    goto EndLevelSteps;
                }

                if (CanGoThroughJail())
                {
                    SetLevelStep(5.7f);
                }
            }
            #endregion

            #region 5.66 Init Fail Situation
            if (levelStep == 5.66f)
            {
                step_E_Timer = step_E_TimeToAlarm;

                mapLogic.SetPlayerIsDetectedInCampMode();

                SetLevelStep(5.67f);
            }
            #endregion

            #region 5.67 Timer To Alarm
            if (levelStep == 5.67f)
            {
                step_E_Timer = MathfPlus.DecByDeltatimeToZero(step_E_Timer);
                if (step_E_Timer == 0)
                {
                    step_E_Objects_01_Alarm.Play();

                    step_E_Timer = step_E_TimeToFailMission;

                    SetLevelStep(5.68f);
                }

                if (DoAllSoldiersDie())
                {
                    SetLevelStep(5.7f);
                }
            }
            #endregion

            #region 5.68 Timer To Fail
            if (levelStep == 5.68f)
            {
                step_E_Timer = MathfPlus.DecByDeltatimeToZero(step_E_Timer);
                if (step_E_Timer == 0)
                {
                    SetLevelStep(5.69f);
                }
            }
            #endregion

            #region 5.69 Fail Mission
            if (levelStep == 5.69f)
            {
                SetLevelStep(missionFailType);
                goto EndLevelSteps;
            }
            #endregion

            #region 5.7 Try to Go Through Jail
            if (levelStep == 5.7f)
            {
                if (step_E_GoingToWallsTimeCounter == 0)
                {
                    step_E_StandNearWallsTimeCounter = MathfPlus.DecByDeltatimeToZero(step_E_StandNearWallsTimeCounter);

                    if (step_E_StandNearWallsTimeCounter == 0)
                    {
                        mapLogic.HUD_ObjectivesPage_SetObjectiveDone(6);
                        mapLogic.HUD_ShowCompleteMission(5);

                        step_E_Ally01ChainJobsGlobalLogicIndex = 2;
                        step_E_Ally_ChainJobGroup_Ally01.Init_SetNewGlobalLogicIndex(step_E_Ally01ChainJobsGlobalLogicIndex);

                        step_E_Timer = step_E_TimeToEnterJailAlly02;

                        step_E_Objects_02_AllyEndMasirCheckTrigger.StartOutStepIfNotStarted();

                        SetLevelStep(5.72f);
                    }
                }
            }
            #endregion

            #region 5.72 Wait For Ally02
            if (levelStep == 5.72f)
            {
                step_E_Timer = MathfPlus.DecByDeltatimeToZero(step_E_Timer);
                if (step_E_Timer == 0)
                {
                    step_E_Ally02ChainJobsGlobalLogicIndex = 2;
                    step_E_Ally_ChainJobGroup_Ally02.Init_SetNewGlobalLogicIndex(step_E_Ally02ChainJobsGlobalLogicIndex);

                    SetLevelStep(5.74f);
                }
            }
            #endregion

            #region 5.74 Wait Till Ally Reach The Door
            if (levelStep == 5.74f)
            {
                if (step_E_AllyEnter_EndMasirCheckTrigger.IsEverActivated())
                {
                    step_E_Timer = step_E_TimeToStartKick;

                    SetLevelStep(5.76f);
                }
            }
            #endregion

            #region 5.76 Wait Till Soldier Kick Door
            if (levelStep == 5.76f)
            {
                step_E_Timer = MathfPlus.DecByDeltatimeToZero(step_E_Timer);
                if (step_E_Timer == 0)
                {
                    step_E_Ally01ChainJobsGlobalLogicIndex = 3;
                    step_E_Ally_ChainJobGroup_Ally01.Init_SetNewGlobalLogicIndex(step_E_Ally01ChainJobsGlobalLogicIndex);

                    step_E_Timer = step_E_TimeToStartDoorAnim;

                    SetLevelStep(5.78f);
                }
            }
            #endregion

            #region 5.78 Kick
            if (levelStep == 5.78f)
            {
                step_E_Timer = MathfPlus.DecByDeltatimeToZero(step_E_Timer);
                if (step_E_Timer == 0)
                {
                    step_E_Objects_03_Door.animation.Play();

                    step_E_Timer = step_E_TimeToWaitEnterTheJailDoor;

                    SetLevelStep(5.8f);
                }
            }
            #endregion

            #region 5.8 Go To Jail
            if (levelStep == 5.8f)
            {
                if (!step_E_IsAllyPlacedAfterKickDoor)
                {
                    step_E_PlaceAllyAfterKickDoorTimer = MathfPlus.DecByDeltatimeToZero(step_E_PlaceAllyAfterKickDoorTimer);

                    if (step_E_PlaceAllyAfterKickDoorTimer == 0)
                    {
                        step_E_IsAllyPlacedAfterKickDoor = true;

                        step_E_Objects_StartPoint_Ally01_AfterKickDoor.PlaceCharacterOnIt(Ally01);
                        Ally01.GetComponent<SoldierInfo>().body.animation.Play(step_E_Objects_AnimsList_Ally01_AfterKickDoor.GetRandomAnimName());
                    }

                }

                step_E_Timer = MathfPlus.DecByDeltatimeToZero(step_E_Timer);
                if (step_E_Timer == 0)
                {
                    step_E_Ally01ChainJobsGlobalLogicIndex = 4;
                    step_E_Ally_ChainJobGroup_Ally01.Init_SetNewGlobalLogicIndex(step_E_Ally01ChainJobsGlobalLogicIndex);

                    step_E_Ally02ChainJobsGlobalLogicIndex = 3;
                    step_E_Ally_ChainJobGroup_Ally02.Init_SetNewGlobalLogicIndex(step_E_Ally02ChainJobsGlobalLogicIndex);

                    isStepE = false;

                    SetLevelStep(6f);
                }
            }
            #endregion

            #endregion

            #region Step F

            #region 6 Start Step
            if (levelStep == 6f)
            {
                SaveCheckPoint(6f);

                isStepF = true;

                step_F_Objects_StartPoint_Khaloo.PlaceCharacterOnIt(Khaloo);

                step_E_Objects_01_PelleFailTrigger.StartOutStepIfNotStarted();

                AddHeadArmors();

                step_F_Timer = step_F_TimeForAlarmOShuresh;

                mapLogic.HUD_ShowGameSaved();

                SetLevelStep(6.05f);
            }
            #endregion

            #region 6.05 Wait For Alarm
            if (levelStep == 6.05f)
            {
                if (!GameController.lvlBushehr2_IsSnipeTimeScaleHintShownBefore)
                {
                    if (GameController.lvlBushehr2_ShouldShowSnipeTimeScaleHint)
                    {
                        step_F_TimeCounterToShowSnipeTimeScaleHint = MathfPlus.DecByDeltatimeToZero(step_F_TimeCounterToShowSnipeTimeScaleHint);

                        if (step_F_TimeCounterToShowSnipeTimeScaleHint == 0)
                        {
                            GameController.lvlBushehr2_IsSnipeTimeScaleHintShownBefore = true;

                            mapLogic.TryToPauseGameAndJustShowHint(HUDGroupName.LvlBushehr02, HUDControlName.BlackBG, HUDControlName.LvlBushehr02Hint_SnipeTimeScale);
                        }
                    }
                }

                step_F_Timer = MathfPlus.DecByDeltatimeToZero(step_F_Timer);
                if (step_F_Timer == 0)
                {
                    mapLogic.HUD_ObjectivesPage_SetActiveObjective(7);
                    mapLogic.HUD_ShowNewMission(6);

                    step_E_Ally_ChainJobGroup_Ally01.StartFinishing_OutStepIfNotFinishing();
                    step_E_Ally_ChainJobGroup_Ally02.StartFinishing_OutStepIfNotFinishing();

                    step_E_Objects_01_FarExplosionSounds.EndIt();

                    step_E_Objects_01_Alarm.Play();

                    step_F_Timer = step_F_TimeForAnimationDoorChap;

                    step_F_Objects_02_CountingJailSoldiersTrigger.StartOutStepIfNotStarted();

                    step_F_Objects_04_CountingInsideSoldiersTrigger.StartOutStepIfNotStarted();
                    step_F_Objects_04_CountingOutsideSoldiersTrigger.StartOutStepIfNotStarted();

                    SetLevelStep(6.1f);
                }
            }
            #endregion

            #region 6.1 Wait For Door Chap
            if (levelStep == 6.1f)
            {
                step_F_Timer = MathfPlus.DecByDeltatimeToZero(step_F_Timer);
                if (step_F_Timer == 0)
                {
                    step_F_Objects_01_Door_Chapi.animation.Play();

                    step_F_Enemy_FightInRegsGroup_01.StartOutStepIfNotStarted();

                    step_F_Timer = step_F_TimeForAnimationDoorRuBRu;

                    SetLevelStep(6.12f);
                }
            }
            #endregion

            #region 6.12 Wait For Door Ru B Ru
            if (levelStep == 6.12f)
            {
                step_F_Timer = MathfPlus.DecByDeltatimeToZero(step_F_Timer);
                if (step_F_Timer == 0)
                {
                    step_F_Objects_01_Door_Ruberu.animation.Play();

                    step_F_Enemy_FightInRegsGroup_02.StartOutStepIfNotStarted();

                    step_F_Timer = step_F_TimeForAnimationDoorRasti;

                    SetLevelStep(6.14f);
                }
            }
            #endregion

            #region 6.14f Wait For Door Rasti
            if (levelStep == 6.14f)
            {
                step_F_Timer = MathfPlus.DecByDeltatimeToZero(step_F_Timer);
                if (step_F_Timer == 0)
                {
                    step_F_Objects_01_Door_Rasti.animation.Play();

                    step_F_Enemy_FightInRegsGroup_03.StartOutStepIfNotStarted();

                    step_F_Timer = step_F_TimeForKhalooPrisonBreak;

                    SetLevelStep(6.16f);
                }
            }
            #endregion

            #region 6.16f Wait For Stop Fight 1&2&3
            if (levelStep == 6.16f)
            {
                step_F_Timer = MathfPlus.DecByDeltatimeToZero(step_F_Timer);
                if (step_F_Timer == 0)
                {
                    //mapLogic.HUD_ObjectivesPage_SetObjectiveDone(7);
                    //mapLogic.HUD_ShowCompleteMission(6);

                    step_F_Enemy_FightInRegsGroup_01.StopCreatingMoreSoldiers();
                    step_F_Enemy_FightInRegsGroup_02.StopCreatingMoreSoldiers();
                    step_F_Enemy_FightInRegsGroup_03.StopCreatingMoreSoldiers();

                    step_F_Timer = step_F_TimeForStartChainJobsAlly01;

                    SetLevelStep(6.18f);
                }
            }
            #endregion

            #region 6.18 Start Chian Jobs Out From Jail
            if (levelStep == 6.18f)
            {
                step_F_Timer = MathfPlus.DecByDeltatimeToZero(step_F_Timer);
                if (step_F_Timer == 0)
                {
                    //mapLogic.HUD_ObjectivesPage_SetObjectiveDone(7);
                    mapLogic.HUD_ShowCompleteMission(6);

                    mapLogic.HUD_ObjectivesPage_SetActiveObjective(8);
                    mapLogic.HUD_ShowNewMission(7);

                    MakeDudesDieable();

                    step_F_Objects_03_DieTrigger_JailKhaloo.StartIt();

                    step_F_Ally01ChainJobsGlobalLogicIndex = 0;
                    step_F_Ally_ChainJobGroup_Ally01.StartOutStepIfNotStarted();

                    step_F_Enemy_FightInRegsGroup_04.StartOutStepIfNotStarted();

                    step_F_Timer = step_F_TimeForStartChainJobsAlly02;

                    SetLevelStep(6.2f);
                }
            }
            #endregion

            #region 6.2 Start Chain Job Ally02
            if (levelStep == 6.2f)
            {
                step_F_Timer = MathfPlus.DecByDeltatimeToZero(step_F_Timer);
                if (step_F_Timer == 0)
                {
                    step_F_Ally02ChainJobsGlobalLogicIndex = 0;
                    step_F_Ally_ChainJobGroup_Ally02.StartOutStepIfNotStarted();

                    step_F_Enemy_FightInRegsGroup_05.StartOutStepIfNotStarted();

                    step_F_Timer = step_F_TimeForStartChainJobsKhaloo;

                    SetLevelStep(6.22f);
                }
            }
            #endregion

            #region 6.22 Start Chain Job Khaloo
            if (levelStep == 6.22f)
            {
                step_F_Timer = MathfPlus.DecByDeltatimeToZero(step_F_Timer);
                if (step_F_Timer == 0)
                {
                    step_F_KhalooChainJobsGlobalLogicIndex = 0;
                    step_F_Ally_ChainJobGroup_Khaloo.StartOutStepIfNotStarted();

                    step_F_Timer = step_F_TimeForFightInsidePrison;

                    SetLevelStep(6.24f);
                }
            }
            #endregion

            #region 6.24 Time For Inside Prison
            if (levelStep == 6.24f)
            {
                step_F_Timer = MathfPlus.DecByDeltatimeToZero(step_F_Timer);
                if (step_F_Timer == 0)
                {
                    step_F_Enemy_FightInRegsGroup_04.StopCreatingMoreSoldiers();

                    SetLevelStep(6.25f);
                }
            }
            #endregion

            #region 6.25 Go OutSide
            if (levelStep == 6.25f)
            {
                if (step_F_Objects_04_CountingInsideSoldiersTrigger.objectsIn.Count < maxNumOfSoldiersInsidePrison)
                {
                    step_F_Ally01ChainJobsGlobalLogicIndex = 1;
                    step_F_Ally_ChainJobGroup_Ally01.Init_SetNewGlobalLogicIndex(step_F_Ally01ChainJobsGlobalLogicIndex);

                    step_F_Ally02ChainJobsGlobalLogicIndex = 1;
                    step_F_Ally_ChainJobGroup_Ally02.Init_SetNewGlobalLogicIndex(step_F_Ally02ChainJobsGlobalLogicIndex);

                    step_F_KhalooChainJobsGlobalLogicIndex = 1;
                    step_F_Ally_ChainJobGroup_Khaloo.Init_SetNewGlobalLogicIndex(step_F_KhalooChainJobsGlobalLogicIndex);

                    step_F_Objects_04_CountingInsideSoldiersTrigger.StartFinishing_OutStepIfNotFishining();

                    step_F_Timer = step_F_TimeForFightOutsidePrison;

                    SetLevelStep(6.3f);
                }
            }
            #endregion

            #region 6.3 Time For Outside Prison
            if (levelStep == 6.3f)
            {
                step_F_Timer = MathfPlus.DecByDeltatimeToZero(step_F_Timer);
                if (step_F_Timer == 0)
                {
                    step_F_Ally01ChainJobsGlobalLogicIndex = 2;
                    step_F_Ally_ChainJobGroup_Ally01.Init_SetNewGlobalLogicIndex(step_F_Ally01ChainJobsGlobalLogicIndex);

                    step_F_Ally02ChainJobsGlobalLogicIndex = 2;
                    step_F_Ally_ChainJobGroup_Ally02.Init_SetNewGlobalLogicIndex(step_F_Ally02ChainJobsGlobalLogicIndex);

                    step_F_KhalooChainJobsGlobalLogicIndex = 2;
                    step_F_Ally_ChainJobGroup_Khaloo.Init_SetNewGlobalLogicIndex(step_F_KhalooChainJobsGlobalLogicIndex);

                    step_F_Objects_04_CountingOutsideSoldiersTrigger.StartFinishing_OutStepIfNotFishining();

                    step_F_Objects_05_EscapeTrigger.StartOutStepIfNotStarted();

                    SetLevelStep(6.35f);
                }
            }
            #endregion

            #region 6.35f Wait Till Allies Escape
            if (levelStep == 6.35f)
            {
                if (step_F_AlliesEntered_EscapeTrigger.IsEverActivated())
                {
                    mapLogic.HUD_ObjectivesPage_SetActiveObjective(8);
                    mapLogic.HUD_ShowNewMission(7);

                    isStepF = false;

                    SetLevelStep(6.37f);
                }
            }
            #endregion

            #region 6.37 Fade Out
            if (levelStep == 6.37f)
            {
                mapLogic.blackScreenFader.StartFadingOut();

                SetLevelStep(6.39f);
            }
            #endregion

            #region 6.39 Wait For Fade Out
            if (levelStep == 6.39f)
            {
                if (mapLogic.blackScreenFader.isFadingFinished)
                {
                    SetLevelStep(6.4f);
                }
            }
            #endregion

            #region 6.4 Run Cutscene 00 TirKhordaneKhaloo
            if (levelStep == 6.4f)
            {
                cutsceneEnd_Part00_TirKhordaneKhaloo.StartIt();

                SetLevelStep(6.41f);
            }
            #endregion

            #region 6.41 Run Cutscene 00
            if (levelStep == 6.41f)
            {
                if (cutsceneEnd_Part00_TirKhordaneKhaloo.status == CutsceneStatus.Finished)
                {
                    cutsceneEnd_Part01.StartIt();

                    SetLevelStep(6.45f);
                }
            }
            #endregion

            #region 6.45 Run Cutscene 01
            if (levelStep == 6.45f)
            {
                if (cutsceneEnd_Part01.status == CutsceneStatus.Finished)
                {
                    beforeEndCutscene_DatPackToHide.SetActiveRecursively(false);

                    cutsceneEnd_Part02.StartIt();

                    SetLevelStep(6.5f);
                }
            }
            #endregion

            #region 6.5 Wait Cutscene 02
            if (levelStep == 6.5f)
            {
                if (cutsceneEnd_Part02.status == CutsceneStatus.Finished)
                {
                    SetLevelStep(8f);
                }
            }
            #endregion

            #region Fail States For Step F
            if (isStepF)
            {
                bool fail = false;

                if (step_F_CountingJailSoldierTrigger.IsEverActivated())
                {
                    fail = true;
                }

                if (IsAnyOneDie())
                {
                    fail = true;
                }

                if (fail)
                {
                    SetLevelStep(6.9f);

                    if (!GameController.isMouseMiddleClickUsedInSnipe)
                    {
                        GameController.lvlBushehr2_ShouldShowSnipeTimeScaleHint = true;
                    }
                }
            }
            #endregion

            #region 6.9 Fail
            if (levelStep == 6.9f)
            {
                SetLevelStep(step_MissionFail_AlliesNotSupported);
                goto EndLevelSteps;
            }
            #endregion

            #endregion

            #region 8 Set mission is finished if black screen fading is done.
            if (levelStep == 8f)
            {
                if (mapLogic.blackScreenFader.isFadingFinished)
                {
                    SetMissionIsFinished();
                    SetLevelStep(10f);
                }
            }
            #endregion

        EndLevelSteps:
            ;

            #region A

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
                }
            }
            #endregion

        Step_A_Enemy_FightInRegsGroup_01_End: ;

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
                }
            }
            #endregion

        Step_A_Enemy_FightInRegsGroup_02_End: ;

            #endregion

            #region Step_A_Enemy_FightInRegsGroup_03

            #region 1 Start
            if (step_A_Enemy_FightInRegsGroup_03.outStep == 1f)
            {
                step_A_Enemy_FightInRegsGroup_03.StartIt();
                step_A_Enemy_FightInRegsGroup_03.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_A_Enemy_FightInRegsGroup_03.outStep == 1.1f)
            {
                step_A_Enemy_FightInRegsGroup_03.RunIt();

                if (step_A_Enemy_FightInRegsGroup_03.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    step_A_Enemy_FightInRegsGroup_03.StartFinishing_OutStepIfNotFinishing();
                    goto step_A_Enemy_FightInRegsGroup_03_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (step_A_Enemy_FightInRegsGroup_03.outStep == 900f)
            {
                step_A_Enemy_FightInRegsGroup_03.SetNeedsToBeFinished();
                step_A_Enemy_FightInRegsGroup_03.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (step_A_Enemy_FightInRegsGroup_03.outStep == 901f)
            {
                step_A_Enemy_FightInRegsGroup_03.RunIt();

                if (step_A_Enemy_FightInRegsGroup_03.status == LogicJobStatus.Finished)
                {
                    step_A_Enemy_FightInRegsGroup_03.SetOutStep(1000f);
                }
            }
            #endregion

        step_A_Enemy_FightInRegsGroup_03_End: ;

            #endregion

            #region Step_A_Enemy_FightInRegsGroup_04

            #region 1 Start
            if (step_A_Enemy_FightInRegsGroup_04.outStep == 1f)
            {
                step_A_Enemy_FightInRegsGroup_04.StartIt();
                step_A_Enemy_FightInRegsGroup_04.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_A_Enemy_FightInRegsGroup_04.outStep == 1.1f)
            {
                step_A_Enemy_FightInRegsGroup_04.RunIt();

                if (step_A_Enemy_FightInRegsGroup_04.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    step_A_Enemy_FightInRegsGroup_04.StartFinishing_OutStepIfNotFinishing();
                    goto step_A_Enemy_FightInRegsGroup_04_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (step_A_Enemy_FightInRegsGroup_04.outStep == 900f)
            {
                step_A_Enemy_FightInRegsGroup_04.SetNeedsToBeFinished();
                step_A_Enemy_FightInRegsGroup_04.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (step_A_Enemy_FightInRegsGroup_04.outStep == 901f)
            {
                step_A_Enemy_FightInRegsGroup_04.RunIt();

                if (step_A_Enemy_FightInRegsGroup_04.status == LogicJobStatus.Finished)
                {
                    step_A_Enemy_FightInRegsGroup_04.SetOutStep(1000f);
                }
            }
            #endregion

        step_A_Enemy_FightInRegsGroup_04_End: ;

            #endregion

            #region step_A_Objects_01_StartFight02Trigger

            #region 1 Start
            if (step_A_Objects_01_StartFight02Trigger.OutStep == 1) //Start
            {
                step_A_Objects_01_StartFight02Trigger.SetEnabled(true);
                step_A_Objects_01_StartFight02Trigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_A_Objects_01_StartFight02Trigger.OutStep == 1.1f) //Run
            {
                if (step_A_Objects_01_StartFight02Trigger.IsPlayerIn())
                {
                    step_A_PlayerEntered_StartFight02Trigger.SetStatus(LogicFlagStatus.Active);
                    step_A_Objects_01_StartFight02Trigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_A_Objects_01_StartFight02Trigger.OutStep == 900f) //Finish
            {
                step_A_Objects_01_StartFight02Trigger.SetEnabled(false);
                step_A_Objects_01_StartFight02Trigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_A_Objects_02_StartFight03Trigger

            #region 1 Start
            if (step_A_Objects_02_StartFight03Trigger.OutStep == 1) //Start
            {
                step_A_Objects_02_StartFight03Trigger.SetEnabled(true);
                step_A_Objects_02_StartFight03Trigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_A_Objects_02_StartFight03Trigger.OutStep == 1.1f) //Run
            {
                if (step_A_Objects_02_StartFight03Trigger.IsPlayerIn())
                {
                    step_A_PlayerEntered_StartFight03Trigger.SetStatus(LogicFlagStatus.Active);
                    step_A_Objects_02_StartFight03Trigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_A_Objects_02_StartFight03Trigger.OutStep == 900f) //Finish
            {
                step_A_Objects_02_StartFight03Trigger.SetEnabled(false);
                step_A_Objects_02_StartFight03Trigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_A_Objects_03_StartFight04Trigger

            #region 1 Start
            if (step_A_Objects_03_StartFight04Trigger.OutStep == 1) //Start
            {
                step_A_Objects_03_StartFight04Trigger.SetEnabled(true);
                step_A_Objects_03_StartFight04Trigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_A_Objects_03_StartFight04Trigger.OutStep == 1.1f) //Run
            {
                if (step_A_Objects_03_StartFight04Trigger.IsPlayerIn())
                {
                    step_A_PlayerEntered_StartFight04Trigger.SetStatus(LogicFlagStatus.Active);
                    step_A_Objects_03_StartFight04Trigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_A_Objects_03_StartFight04Trigger.OutStep == 900f) //Finish
            {
                step_A_Objects_03_StartFight04Trigger.SetEnabled(false);
                step_A_Objects_03_StartFight04Trigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_A_Objects_04_EndStepTrigger

            #region 1 Start
            if (step_A_Objects_04_EndStepTrigger.OutStep == 1) //Start
            {
                step_A_Objects_04_EndStepTrigger.SetEnabled(true);
                step_A_Objects_04_EndStepTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_A_Objects_04_EndStepTrigger.OutStep == 1.1f) //Run
            {
                if (step_A_Objects_04_EndStepTrigger.IsPlayerIn())
                {
                    step_A_PlayerEntered_EndStepTrigger.SetStatus(LogicFlagStatus.Active);
                    step_A_Objects_04_EndStepTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_A_Objects_04_EndStepTrigger.OutStep == 900f) //Finish
            {
                step_A_Objects_04_EndStepTrigger.SetEnabled(false);
                step_A_Objects_04_EndStepTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #endregion

            #region B

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
                    step_B_Fight01_Finish.SetStatus(LogicFlagStatus.Active);

                    step_B_Enemy_FightInRegsGroup_01.StartFinishing_OutStepIfNotFinishing();
                    goto step_B_Enemy_FightInRegsGroup_01_End;
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
                }
            }
            #endregion

        step_B_Enemy_FightInRegsGroup_01_End: ;

            #endregion

            #region step_B_Enemy_FightInRegsGroup_02

            #region 1 Start
            if (step_B_Enemy_FightInRegsGroup_02.outStep == 1f)
            {
                step_B_Enemy_FightInRegsGroup_02.StartIt();
                step_B_Enemy_FightInRegsGroup_02.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_B_Enemy_FightInRegsGroup_02.outStep == 1.1f)
            {
                step_B_Enemy_FightInRegsGroup_02.RunIt();

                if (step_B_Enemy_FightInRegsGroup_02.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    step_B_Fight02_Finish.SetStatus(LogicFlagStatus.Active);

                    step_B_Enemy_FightInRegsGroup_02.StartFinishing_OutStepIfNotFinishing();
                    goto step_B_Enemy_FightInRegsGroup_02_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (step_B_Enemy_FightInRegsGroup_02.outStep == 900f)
            {
                step_B_Enemy_FightInRegsGroup_02.SetNeedsToBeFinished();
                step_B_Enemy_FightInRegsGroup_02.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (step_B_Enemy_FightInRegsGroup_02.outStep == 901f)
            {
                step_B_Enemy_FightInRegsGroup_02.RunIt();

                if (step_B_Enemy_FightInRegsGroup_02.status == LogicJobStatus.Finished)
                {
                    step_B_Enemy_FightInRegsGroup_02.SetOutStep(1000f);
                }
            }
            #endregion

        step_B_Enemy_FightInRegsGroup_02_End: ;

            #endregion

            #region step_B_Enemy_FightInRegsGroup_03

            #region 1 Start
            if (step_B_Enemy_FightInRegsGroup_03.outStep == 1f)
            {
                step_B_Enemy_FightInRegsGroup_03.StartIt();
                step_B_Enemy_FightInRegsGroup_03.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_B_Enemy_FightInRegsGroup_03.outStep == 1.1f)
            {
                step_B_Enemy_FightInRegsGroup_03.RunIt();

                if (step_B_Enemy_FightInRegsGroup_03.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    step_B_Fight03_Finish.SetStatus(LogicFlagStatus.Active);

                    step_B_Enemy_FightInRegsGroup_03.StartFinishing_OutStepIfNotFinishing();
                    goto step_B_Enemy_FightInRegsGroup_03_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (step_B_Enemy_FightInRegsGroup_03.outStep == 900f)
            {
                step_B_Enemy_FightInRegsGroup_03.SetNeedsToBeFinished();
                step_B_Enemy_FightInRegsGroup_03.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (step_B_Enemy_FightInRegsGroup_03.outStep == 901f)
            {
                step_B_Enemy_FightInRegsGroup_03.RunIt();

                if (step_B_Enemy_FightInRegsGroup_03.status == LogicJobStatus.Finished)
                {
                    step_B_Enemy_FightInRegsGroup_03.SetOutStep(1000f);
                }
            }
            #endregion

        step_B_Enemy_FightInRegsGroup_03_End: ;

            #endregion

            #region step_B_Enemy_FightInRegsGroup_04

            #region 1 Start
            if (step_B_Enemy_FightInRegsGroup_04.outStep == 1f)
            {
                step_B_Enemy_FightInRegsGroup_04.StartIt();
                step_B_Enemy_FightInRegsGroup_04.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_B_Enemy_FightInRegsGroup_04.outStep == 1.1f)
            {
                step_B_Enemy_FightInRegsGroup_04.RunIt();

                if (step_B_Enemy_FightInRegsGroup_04.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    step_B_Fight04_Finish.SetStatus(LogicFlagStatus.Active);

                    step_B_Enemy_FightInRegsGroup_04.StartFinishing_OutStepIfNotFinishing();
                    goto step_B_Enemy_FightInRegsGroup_04_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (step_B_Enemy_FightInRegsGroup_04.outStep == 900f)
            {
                step_B_Enemy_FightInRegsGroup_04.SetNeedsToBeFinished();
                step_B_Enemy_FightInRegsGroup_04.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (step_B_Enemy_FightInRegsGroup_04.outStep == 901f)
            {
                step_B_Enemy_FightInRegsGroup_04.RunIt();

                if (step_B_Enemy_FightInRegsGroup_04.status == LogicJobStatus.Finished)
                {
                    step_B_Enemy_FightInRegsGroup_04.SetOutStep(1000f);
                }
            }
            #endregion

        step_B_Enemy_FightInRegsGroup_04_End: ;

            #endregion

            #region step_B_Objects_01_StopFight1_2Trigger

            #region 1 Start
            if (step_B_Objects_01_StopFight1_2Trigger.OutStep == 1) //Start
            {
                step_B_Objects_01_StopFight1_2Trigger.SetEnabled(true);
                step_B_Objects_01_StopFight1_2Trigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_B_Objects_01_StopFight1_2Trigger.OutStep == 1.1f) //Run
            {
                if (step_B_Objects_01_StopFight1_2Trigger.IsPlayerIn())
                {
                    step_B_PlayerEntered_StopFight1_2Trigger.SetStatus(LogicFlagStatus.Active);
                    step_B_Objects_01_StopFight1_2Trigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_B_Objects_01_StopFight1_2Trigger.OutStep == 900f) //Finish
            {
                step_B_Objects_01_StopFight1_2Trigger.SetEnabled(false);
                step_B_Objects_01_StopFight1_2Trigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_B_Ally_chainJobGroup_01_Allies

            #region 1 Start
            if (step_B_Ally_chainJobGroup_01_Allies.outStep == 1)
            {
                step_B_Ally_chainJobGroup_01_Allies.Init_SetNewGlobalLogicIndex(step_B_AllyChainJobsGlobalLogicIndex);
                step_B_Ally_chainJobGroup_01_Allies.StartIt();

                step_B_Ally_chainJobGroup_01_Allies.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_B_Ally_chainJobGroup_01_Allies.outStep == 1.1f)
            {
                step_B_Ally_chainJobGroup_01_Allies.RunIt();
            }
            #endregion

            #region 900 Start finishing
            if (step_B_Ally_chainJobGroup_01_Allies.outStep == 900f)
            {
                step_B_Ally_chainJobGroup_01_Allies.SetNeedsToBeFinished();

                step_B_Ally_chainJobGroup_01_Allies.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (step_B_Ally_chainJobGroup_01_Allies.outStep == 901f)
            {
                step_B_Ally_chainJobGroup_01_Allies.RunIt();

                if (step_B_Ally_chainJobGroup_01_Allies.status == LogicJobStatus.Finished)
                {
                    step_B_ChainJobAllies_Finished.SetStatus(LogicFlagStatus.Active);
                    step_B_Ally_chainJobGroup_01_Allies.SetOutStep(1000);
                }
            }
            #endregion

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
                    step_B_MachineGun_Finish.SetStatus(LogicFlagStatus.Active);

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

            #region step_B_Objects_03_StartFight3_4Trigger

            #region 1 Start
            if (step_B_Objects_03_StartFight3_4Trigger.OutStep == 1) //Start
            {
                step_B_Objects_03_StartFight3_4Trigger.SetEnabled(true);
                step_B_Objects_03_StartFight3_4Trigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_B_Objects_03_StartFight3_4Trigger.OutStep == 1.1f) //Run
            {
                if (step_B_Objects_03_StartFight3_4Trigger.IsPlayerIn())
                {
                    step_B_PlayerEntered_StartFight3_4Trigger.SetStatus(LogicFlagStatus.Active);
                    step_B_Objects_03_StartFight3_4Trigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_B_Objects_03_StartFight3_4Trigger.OutStep == 900f) //Finish
            {
                step_B_Objects_03_StartFight3_4Trigger.SetEnabled(false);
                step_B_Objects_03_StartFight3_4Trigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_B_Objects_04_StopFight3_4Trigger

            #region 1 Start
            if (step_B_Objects_04_StopFight3_4Trigger.OutStep == 1) //Start
            {
                step_B_Objects_04_StopFight3_4Trigger.SetEnabled(true);
                step_B_Objects_04_StopFight3_4Trigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_B_Objects_04_StopFight3_4Trigger.OutStep == 1.1f) //Run
            {
                if (step_B_Objects_04_StopFight3_4Trigger.IsPlayerIn())
                {
                    step_B_PlayerEntered_StopFight3_4Trigger.SetStatus(LogicFlagStatus.Active);
                    step_B_Objects_04_StopFight3_4Trigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_B_Objects_04_StopFight3_4Trigger.OutStep == 900f) //Finish
            {
                step_B_Objects_04_StopFight3_4Trigger.SetEnabled(false);
                step_B_Objects_04_StopFight3_4Trigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_B_Objects_06_EndStepBTrigger

            #region 1 Start
            if (step_B_Objects_07_EndStepBTrigger.OutStep == 1) //Start
            {
                step_B_Objects_07_EndStepBTrigger.SetEnabled(true);
                step_B_Objects_07_EndStepBTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_B_Objects_07_EndStepBTrigger.OutStep == 1.1f) //Run
            {
                if (step_B_Objects_07_EndStepBTrigger.IsPlayerIn())
                {
                    step_B_PlayerEntered_EndStepBTrigger.SetStatus(LogicFlagStatus.Active);
                    step_B_Objects_07_EndStepBTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_B_Objects_07_EndStepBTrigger.OutStep == 900f) //Finish
            {
                step_B_Objects_07_EndStepBTrigger.SetEnabled(false);
                step_B_Objects_07_EndStepBTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #endregion

            #region C

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
                step_C_Fight01_Finished.SetStatus(LogicFlagStatus.Active);

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
                }
            }
            #endregion

        step_C_Enemy_FightInRegsGroup_01_End: ;

            #endregion

            #region step_C_Enemy_FightInRegsGroup_02

            #region 1 Start
            if (step_C_Enemy_FightInRegsGroup_02.outStep == 1f)
            {
                step_C_Enemy_FightInRegsGroup_02.StartIt();
                step_C_Enemy_FightInRegsGroup_02.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_C_Enemy_FightInRegsGroup_02.outStep == 1.1f)
            {
                step_C_Enemy_FightInRegsGroup_02.RunIt();

                if (step_C_Enemy_FightInRegsGroup_02.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    step_C_Enemy_FightInRegsGroup_02.StartFinishing_OutStepIfNotFinishing();
                    goto step_C_Enemy_FightInRegsGroup_02_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (step_C_Enemy_FightInRegsGroup_02.outStep == 900f)
            {
                step_C_Fight02_Finished.SetStatus(LogicFlagStatus.Active);

                step_C_Enemy_FightInRegsGroup_02.SetNeedsToBeFinished();
                step_C_Enemy_FightInRegsGroup_02.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (step_C_Enemy_FightInRegsGroup_02.outStep == 901f)
            {
                step_C_Enemy_FightInRegsGroup_02.RunIt();

                if (step_C_Enemy_FightInRegsGroup_02.status == LogicJobStatus.Finished)
                {
                    step_C_Enemy_FightInRegsGroup_02.SetOutStep(1000f);
                }
            }
            #endregion

        step_C_Enemy_FightInRegsGroup_02_End: ;

            #endregion

            #region step_C_Ally_chainJobGroup_01_Allies

            #region 1 Start
            if (step_C_Ally_chainJobGroup_01_Allies.outStep == 1)
            {
                step_C_Ally_chainJobGroup_01_Allies.Init_SetNewGlobalLogicIndex(step_C_AllyChainJobsGlobalLogicIndex);
                step_C_Ally_chainJobGroup_01_Allies.StartIt();

                step_C_Ally_chainJobGroup_01_Allies.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_C_Ally_chainJobGroup_01_Allies.outStep == 1.1f)
            {
                step_C_Ally_chainJobGroup_01_Allies.RunIt();
            }
            #endregion

            #region 900 Start finishing
            if (step_C_Ally_chainJobGroup_01_Allies.outStep == 900f)
            {
                step_C_Ally_chainJobGroup_01_Allies.SetNeedsToBeFinished_EvenStopMoving();

                step_C_Ally_chainJobGroup_01_Allies.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (step_C_Ally_chainJobGroup_01_Allies.outStep == 901f)
            {
                step_C_Ally_chainJobGroup_01_Allies.RunIt();

                if (step_C_Ally_chainJobGroup_01_Allies.status == LogicJobStatus.Finished)
                {
                    step_C_AllyChainJob01_Finished.SetStatus(LogicFlagStatus.Active);

                    step_C_Ally_chainJobGroup_01_Allies.SetOutStep(1000);
                }
            }
            #endregion

            #endregion

            #region step_C_Objects_01_StartStepTrigger

            #region 1 Start
            if (step_C_Objects_01_StartStepTrigger.OutStep == 1) //Start
            {
                step_C_Objects_01_StartStepTrigger.SetEnabled(true);
                step_C_Objects_01_StartStepTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_C_Objects_01_StartStepTrigger.OutStep == 1.1f) //Run
            {
                if (step_C_Objects_01_StartStepTrigger.IsPlayerIn())
                {
                    step_C_PlayerEntered_StartStepTrigger.SetStatus(LogicFlagStatus.Active);
                    step_C_Objects_01_StartStepTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_C_Objects_01_StartStepTrigger.OutStep == 900f) //Finish
            {
                step_C_Objects_01_StartStepTrigger.SetEnabled(false);
                step_C_Objects_01_StartStepTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_C_Objects_03_EndStepTrigger

            #region 1 Start
            if (step_C_Objects_03_EndStepTrigger.OutStep == 1) //Start
            {
                step_C_Objects_03_EndStepTrigger.SetEnabled(true);
                step_C_Objects_03_EndStepTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_C_Objects_03_EndStepTrigger.OutStep == 1.1f) //Run
            {
                if (step_C_Objects_03_EndStepTrigger.IsPlayerIn())
                {
                    step_C_PlayerEntered_EndStepTrigger.SetStatus(LogicFlagStatus.Active);
                    step_C_Objects_03_EndStepTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_C_Objects_03_EndStepTrigger.OutStep == 900f) //Finish
            {
                step_C_Objects_03_EndStepTrigger.SetEnabled(false);
                step_C_Objects_03_EndStepTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #endregion

            #region D

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
                    step_D_Fight01_Finished.SetStatus(LogicFlagStatus.Active);

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
                    step_D_Fight02_Finished.SetStatus(LogicFlagStatus.Active);

                    step_D_Enemy_FightInRegsGroup_02.SetOutStep(1000f);
                }
            }
            #endregion

        step_D_Enemy_FightInRegsGroup_02_End: ;

            #endregion

            #region step_D_Ally_chainJobGroup_01_Allies

            #region 1 Start
            if (step_D_Ally_chainJobGroup_01_Allies.outStep == 1)
            {
                step_D_Ally_chainJobGroup_01_Allies.Init_SetNewGlobalLogicIndex(step_D_AllyChainJobsGlobalLogicIndex);
                step_D_Ally_chainJobGroup_01_Allies.StartIt();

                step_D_Ally_chainJobGroup_01_Allies.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_D_Ally_chainJobGroup_01_Allies.outStep == 1.1f)
            {
                step_D_Ally_chainJobGroup_01_Allies.RunIt();
            }
            #endregion

            #region 900 Start finishing
            if (step_D_Ally_chainJobGroup_01_Allies.outStep == 900f)
            {
                step_D_Ally_chainJobGroup_01_Allies.SetNeedsToBeFinished();

                step_D_Ally_chainJobGroup_01_Allies.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (step_D_Ally_chainJobGroup_01_Allies.outStep == 901f)
            {
                step_D_Ally_chainJobGroup_01_Allies.RunIt();

                if (step_D_Ally_chainJobGroup_01_Allies.status == LogicJobStatus.Finished)
                {
                    step_D_Ally_chainJobGroup_01_Allies.SetOutStep(1000);
                }
            }
            #endregion

            #endregion

            #region step_D_Objects_01_StartStepTrigger

            #region 1 Start
            if (step_D_Objects_01_StartStepTrigger.OutStep == 1) //Start
            {
                step_D_Objects_01_StartStepTrigger.SetEnabled(true);
                step_D_Objects_01_StartStepTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_D_Objects_01_StartStepTrigger.OutStep == 1.1f) //Run
            {
                if (step_D_Objects_01_StartStepTrigger.IsPlayerIn())
                {
                    step_D_PlayerEntered_StartStepTrigger.SetStatus(LogicFlagStatus.Active);
                    step_D_Objects_01_StartStepTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_D_Objects_01_StartStepTrigger.OutStep == 900f) //Finish
            {
                step_D_Objects_01_StartStepTrigger.SetEnabled(false);
                step_D_Objects_01_StartStepTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_D_Objects_01_FailTrigger

            #region 1 Start
            if (step_D_Objects_01_FailTrigger.OutStep == 1) //Start
            {
                step_D_Objects_01_FailTrigger.SetEnabled(true);
                step_D_Objects_01_FailTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_D_Objects_01_FailTrigger.OutStep == 1.1f) //Run
            {
                if (step_D_Objects_01_FailTrigger.IsPlayerIn())
                {
                    step_D_PlayerEntered_FailTrigger.SetStatus(LogicFlagStatus.Active);
                    step_D_Objects_01_FailTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_D_Objects_01_FailTrigger.OutStep == 900f) //Finish
            {
                step_D_Objects_01_FailTrigger.SetEnabled(false);
                step_D_Objects_01_FailTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_D_Objects_02_EndFight01Trigger

            #region 1 Start
            if (step_D_Objects_02_EndFight01Trigger.OutStep == 1) //Start
            {
                step_D_Objects_02_EndFight01Trigger.SetEnabled(true);
                step_D_Objects_02_EndFight01Trigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_D_Objects_02_EndFight01Trigger.OutStep == 1.1f) //Run
            {
                if (step_D_Objects_02_EndFight01Trigger.IsPlayerIn())
                {
                    step_D_PlayerEntered_EndFight01Trigger.SetStatus(LogicFlagStatus.Active);
                    step_D_Objects_02_EndFight01Trigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_D_Objects_02_EndFight01Trigger.OutStep == 900f) //Finish
            {
                step_D_Objects_02_EndFight01Trigger.SetEnabled(false);
                step_D_Objects_02_EndFight01Trigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_D_Objects_03_AlliesChangePosTrigger

            #region 1 Start
            if (step_D_Objects_03_AlliesChangePosTrigger.OutStep == 1) //Start
            {
                step_D_Objects_03_AlliesChangePosTrigger.SetEnabled(true);
                step_D_Objects_03_AlliesChangePosTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_D_Objects_03_AlliesChangePosTrigger.OutStep == 1.1f) //Run
            {
                if (step_D_Objects_03_AlliesChangePosTrigger.IsPlayerIn())
                {
                    step_D_PlayerEntered_AlliesChangePosTrigger.SetStatus(LogicFlagStatus.Active);
                    step_D_Objects_03_AlliesChangePosTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_D_Objects_03_AlliesChangePosTrigger.OutStep == 900f) //Finish
            {
                step_D_Objects_03_AlliesChangePosTrigger.SetEnabled(false);
                step_D_Objects_03_AlliesChangePosTrigger.SetOutStep(1000f);
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

            #region step_D_Objects_06_CutSceneTrigger

            #region 1 Start
            if (step_D_Objects_06_CutSceneTrigger.OutStep == 1) //Start
            {
                step_D_Objects_06_CutSceneTrigger.SetEnabled(true);
                step_D_Objects_06_CutSceneTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_D_Objects_06_CutSceneTrigger.OutStep == 1.1f) //Run
            {
                if (step_D_Objects_06_CutSceneTrigger.IsPlayerIn())
                {
                    step_D_PlayerEntered_CutSceneTrigger.SetStatus(LogicFlagStatus.Active);
                    step_D_Objects_06_CutSceneTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_D_Objects_06_CutSceneTrigger.OutStep == 900f) //Finish
            {
                step_D_Objects_06_CutSceneTrigger.SetEnabled(false);
                step_D_Objects_06_CutSceneTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_D_Objects_07_PellehaTrigger

            #region 1 Start
            if (step_D_Objects_07_PellehaTrigger.OutStep == 1) //Start
            {
                step_D_Objects_07_PellehaTrigger.SetEnabled(true);
                step_D_Objects_07_PellehaTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_D_Objects_07_PellehaTrigger.OutStep == 1.1f) //Run
            {
                if (step_D_Objects_07_PellehaTrigger.IsPlayerIn())
                {
                    step_D_PlayerEntered_PellehaTrigger.SetStatus(LogicFlagStatus.Active);
                    step_D_Objects_07_PellehaTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_D_Objects_07_PellehaTrigger.OutStep == 900f) //Finish
            {
                step_D_Objects_07_PellehaTrigger.SetEnabled(false);
                step_D_Objects_07_PellehaTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #endregion

            #region E

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
                    step_E_Fight01_Finished.SetStatus(LogicFlagStatus.Active);

                    step_E_Enemy_FightInRegsGroup_01.SetOutStep(1000f);
                }
            }
            #endregion

        step_E_Enemy_FightInRegsGroup_01_End: ;

            #endregion

            #region step_E_Ally_ChainJobGroup_Ally01

            #region 1 Start
            if (step_E_Ally_ChainJobGroup_Ally01.outStep == 1)
            {
                step_E_Ally_ChainJobGroup_Ally01.Init_SetNewGlobalLogicIndex(step_E_Ally01ChainJobsGlobalLogicIndex);
                step_E_Ally_ChainJobGroup_Ally01.StartIt();

                step_E_Ally_ChainJobGroup_Ally01.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_E_Ally_ChainJobGroup_Ally01.outStep == 1.1f)
            {
                step_E_Ally_ChainJobGroup_Ally01.RunIt();
            }
            #endregion

            #region 900 Start finishing
            if (step_E_Ally_ChainJobGroup_Ally01.outStep == 900f)
            {
                step_E_Ally_ChainJobGroup_Ally01.SetNeedsToBeFinished();

                step_E_Ally_ChainJobGroup_Ally01.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (step_E_Ally_ChainJobGroup_Ally01.outStep == 901f)
            {
                step_E_Ally_ChainJobGroup_Ally01.RunIt();

                if (step_E_Ally_ChainJobGroup_Ally01.status == LogicJobStatus.Finished)
                {
                    step_E_Ally_ChainJobGroup_Ally01.SetOutStep(1000);
                }
            }
            #endregion

            #endregion

            #region step_E_Ally_ChainJobGroup_Ally02

            #region 1 Start
            if (step_E_Ally_ChainJobGroup_Ally02.outStep == 1)
            {
                step_E_Ally_ChainJobGroup_Ally02.Init_SetNewGlobalLogicIndex(step_E_Ally02ChainJobsGlobalLogicIndex);
                step_E_Ally_ChainJobGroup_Ally02.StartIt();

                step_E_Ally_ChainJobGroup_Ally02.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_E_Ally_ChainJobGroup_Ally02.outStep == 1.1f)
            {
                step_E_Ally_ChainJobGroup_Ally02.RunIt();
            }
            #endregion

            #region 900 Start finishing
            if (step_E_Ally_ChainJobGroup_Ally02.outStep == 900f)
            {
                step_E_Ally_ChainJobGroup_Ally02.SetNeedsToBeFinished();

                step_E_Ally_ChainJobGroup_Ally02.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (step_E_Ally_ChainJobGroup_Ally02.outStep == 901f)
            {
                step_E_Ally_ChainJobGroup_Ally02.RunIt();

                if (step_E_Ally_ChainJobGroup_Ally02.status == LogicJobStatus.Finished)
                {
                    step_E_Ally_ChainJobGroup_Ally02.SetOutStep(1000);
                }
            }
            #endregion

            #endregion

            #region step_E_Objects_02_AllyEndMasirCheckTrigger

            #region 1 Start
            if (step_E_Objects_02_AllyEndMasirCheckTrigger.OutStep == 1) //Start
            {
                step_E_Objects_02_AllyEndMasirCheckTrigger.SetEnabled(true);
                step_E_Objects_02_AllyEndMasirCheckTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_E_Objects_02_AllyEndMasirCheckTrigger.OutStep == 1.1f) //Run
            {
                if (step_E_Objects_02_AllyEndMasirCheckTrigger.IsSomethingIn())
                {
                    step_E_AllyEnter_EndMasirCheckTrigger.SetStatus(LogicFlagStatus.Active);
                    step_E_Objects_02_AllyEndMasirCheckTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_E_Objects_02_AllyEndMasirCheckTrigger.OutStep == 900f) //Finish
            {
                step_E_Objects_02_AllyEndMasirCheckTrigger.SetEnabled(false);
                step_E_Objects_02_AllyEndMasirCheckTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_E_Objects_01_PelleFailTrigger

            #region 1 Start
            if (step_E_Objects_01_PelleFailTrigger.OutStep == 1) //Start
            {
                step_E_Objects_01_PelleFailTrigger.SetEnabled(true);
                step_E_Objects_01_PelleFailTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_E_Objects_01_PelleFailTrigger.OutStep == 1.1f) //Run
            {
                if (step_E_Objects_01_PelleFailTrigger.IsPlayerIn())
                {
                    step_E_PlayerEntered_PelleFailTrigger.SetStatus(LogicFlagStatus.Active);
                    step_E_Objects_01_PelleFailTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_E_Objects_01_PelleFailTrigger.OutStep == 900f) //Finish
            {
                step_E_Objects_01_PelleFailTrigger.SetEnabled(false);
                step_E_Objects_01_PelleFailTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #endregion

            #region F

            #region step_F_Ally_ChainJobGroup_Ally01

            #region 1 Start
            if (step_F_Ally_ChainJobGroup_Ally01.outStep == 1)
            {
                step_F_Ally_ChainJobGroup_Ally01.Init_SetNewGlobalLogicIndex(step_F_Ally01ChainJobsGlobalLogicIndex);
                step_F_Ally_ChainJobGroup_Ally01.StartIt();

                step_F_Ally_ChainJobGroup_Ally01.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_F_Ally_ChainJobGroup_Ally01.outStep == 1.1f)
            {
                step_F_Ally_ChainJobGroup_Ally01.RunIt();
            }
            #endregion

            #region 900 Start finishing
            if (step_F_Ally_ChainJobGroup_Ally01.outStep == 900f)
            {
                step_F_Ally_ChainJobGroup_Ally01.SetNeedsToBeFinished();

                step_F_Ally_ChainJobGroup_Ally01.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (step_F_Ally_ChainJobGroup_Ally01.outStep == 901f)
            {
                step_F_Ally_ChainJobGroup_Ally01.RunIt();

                if (step_F_Ally_ChainJobGroup_Ally01.status == LogicJobStatus.Finished)
                {
                    step_F_Ally_ChainJobGroup_Ally01.SetOutStep(1000);
                }
            }
            #endregion

            #endregion

            #region step_F_Ally_ChainJobGroup_Ally02

            #region 1 Start
            if (step_F_Ally_ChainJobGroup_Ally02.outStep == 1)
            {
                step_F_Ally_ChainJobGroup_Ally02.Init_SetNewGlobalLogicIndex(step_F_Ally02ChainJobsGlobalLogicIndex);
                step_F_Ally_ChainJobGroup_Ally02.StartIt();

                step_F_Ally_ChainJobGroup_Ally02.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_F_Ally_ChainJobGroup_Ally02.outStep == 1.1f)
            {
                step_F_Ally_ChainJobGroup_Ally02.RunIt();
            }
            #endregion

            #region 900 Start finishing
            if (step_F_Ally_ChainJobGroup_Ally02.outStep == 900f)
            {
                step_F_Ally_ChainJobGroup_Ally02.SetNeedsToBeFinished();

                step_F_Ally_ChainJobGroup_Ally02.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (step_F_Ally_ChainJobGroup_Ally02.outStep == 901f)
            {
                step_F_Ally_ChainJobGroup_Ally02.RunIt();

                if (step_F_Ally_ChainJobGroup_Ally02.status == LogicJobStatus.Finished)
                {
                    step_F_Ally_ChainJobGroup_Ally02.SetOutStep(1000);
                }
            }
            #endregion

            #endregion

            #region step_F_Ally_ChainJobGroup_Khaloo

            #region 1 Start
            if (step_F_Ally_ChainJobGroup_Khaloo.outStep == 1)
            {
                step_F_Ally_ChainJobGroup_Khaloo.Init_SetNewGlobalLogicIndex(step_F_KhalooChainJobsGlobalLogicIndex);
                step_F_Ally_ChainJobGroup_Khaloo.StartIt();

                step_F_Ally_ChainJobGroup_Khaloo.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_F_Ally_ChainJobGroup_Khaloo.outStep == 1.1f)
            {
                step_F_Ally_ChainJobGroup_Khaloo.RunIt();
            }
            #endregion

            #region 900 Start finishing
            if (step_F_Ally_ChainJobGroup_Khaloo.outStep == 900f)
            {
                step_F_Ally_ChainJobGroup_Khaloo.SetNeedsToBeFinished();

                step_F_Ally_ChainJobGroup_Khaloo.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (step_F_Ally_ChainJobGroup_Khaloo.outStep == 901f)
            {
                step_F_Ally_ChainJobGroup_Khaloo.RunIt();

                if (step_F_Ally_ChainJobGroup_Khaloo.status == LogicJobStatus.Finished)
                {
                    step_F_Ally_ChainJobGroup_Khaloo.SetOutStep(1000);
                }
            }
            #endregion

            #endregion

            #region step_F_Enemy_FightInRegsGroup_01

            #region 1 Start
            if (step_F_Enemy_FightInRegsGroup_01.outStep == 1f)
            {
                step_F_Enemy_FightInRegsGroup_01.StartIt();
                step_F_Enemy_FightInRegsGroup_01.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_F_Enemy_FightInRegsGroup_01.outStep == 1.1f)
            {
                step_F_Enemy_FightInRegsGroup_01.RunIt();

                if (step_F_Enemy_FightInRegsGroup_01.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    step_F_Enemy_FightInRegsGroup_01.StartFinishing_OutStepIfNotFinishing();
                    goto step_F_Enemy_FightInRegsGroup_01_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (step_F_Enemy_FightInRegsGroup_01.outStep == 900f)
            {
                step_F_Enemy_FightInRegsGroup_01.SetNeedsToBeFinished();
                step_F_Enemy_FightInRegsGroup_01.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (step_F_Enemy_FightInRegsGroup_01.outStep == 901f)
            {
                step_F_Enemy_FightInRegsGroup_01.RunIt();

                if (step_F_Enemy_FightInRegsGroup_01.status == LogicJobStatus.Finished)
                {
                    step_F_Fight01_Finished.SetStatus(LogicFlagStatus.Active);

                    step_F_Enemy_FightInRegsGroup_01.SetOutStep(1000f);
                }
            }
            #endregion

        step_F_Enemy_FightInRegsGroup_01_End: ;

            #endregion

            #region step_F_Enemy_FightInRegsGroup_02

            #region 1 Start
            if (step_F_Enemy_FightInRegsGroup_02.outStep == 1f)
            {
                step_F_Enemy_FightInRegsGroup_02.StartIt();
                step_F_Enemy_FightInRegsGroup_02.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_F_Enemy_FightInRegsGroup_02.outStep == 1.1f)
            {
                step_F_Enemy_FightInRegsGroup_02.RunIt();

                if (step_F_Enemy_FightInRegsGroup_02.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    step_F_Enemy_FightInRegsGroup_02.StartFinishing_OutStepIfNotFinishing();
                    goto step_F_Enemy_FightInRegsGroup_02_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (step_F_Enemy_FightInRegsGroup_02.outStep == 900f)
            {
                step_F_Enemy_FightInRegsGroup_02.SetNeedsToBeFinished();
                step_F_Enemy_FightInRegsGroup_02.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (step_F_Enemy_FightInRegsGroup_02.outStep == 901f)
            {
                step_F_Enemy_FightInRegsGroup_02.RunIt();

                if (step_F_Enemy_FightInRegsGroup_02.status == LogicJobStatus.Finished)
                {
                    step_F_Fight02_Finished.SetStatus(LogicFlagStatus.Active);

                    step_F_Enemy_FightInRegsGroup_02.SetOutStep(1000f);
                }
            }
            #endregion

        step_F_Enemy_FightInRegsGroup_02_End: ;

            #endregion

            #region step_F_Enemy_FightInRegsGroup_03

            #region 1 Start
            if (step_F_Enemy_FightInRegsGroup_03.outStep == 1f)
            {
                step_F_Enemy_FightInRegsGroup_03.StartIt();
                step_F_Enemy_FightInRegsGroup_03.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_F_Enemy_FightInRegsGroup_03.outStep == 1.1f)
            {
                step_F_Enemy_FightInRegsGroup_03.RunIt();

                if (step_F_Enemy_FightInRegsGroup_03.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    step_F_Enemy_FightInRegsGroup_03.StartFinishing_OutStepIfNotFinishing();
                    goto step_F_Enemy_FightInRegsGroup_03_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (step_F_Enemy_FightInRegsGroup_03.outStep == 900f)
            {
                step_F_Enemy_FightInRegsGroup_03.SetNeedsToBeFinished();
                step_F_Enemy_FightInRegsGroup_03.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (step_F_Enemy_FightInRegsGroup_03.outStep == 901f)
            {
                step_F_Enemy_FightInRegsGroup_03.RunIt();

                if (step_F_Enemy_FightInRegsGroup_03.status == LogicJobStatus.Finished)
                {
                    step_F_Fight03_Finished.SetStatus(LogicFlagStatus.Active);

                    step_F_Enemy_FightInRegsGroup_03.SetOutStep(1000f);
                }
            }
            #endregion

        step_F_Enemy_FightInRegsGroup_03_End: ;

            #endregion

            #region step_F_Enemy_FightInRegsGroup_04

            #region 1 Start
            if (step_F_Enemy_FightInRegsGroup_04.outStep == 1f)
            {
                step_F_Enemy_FightInRegsGroup_04.StartIt();
                step_F_Enemy_FightInRegsGroup_04.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_F_Enemy_FightInRegsGroup_04.outStep == 1.1f)
            {
                step_F_Enemy_FightInRegsGroup_04.RunIt();

                if (step_F_Enemy_FightInRegsGroup_04.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    step_F_Enemy_FightInRegsGroup_04.StartFinishing_OutStepIfNotFinishing();
                    goto step_F_Enemy_FightInRegsGroup_04_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (step_F_Enemy_FightInRegsGroup_04.outStep == 900f)
            {
                step_F_Enemy_FightInRegsGroup_04.SetNeedsToBeFinished();
                step_F_Enemy_FightInRegsGroup_04.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (step_F_Enemy_FightInRegsGroup_04.outStep == 901f)
            {
                step_F_Enemy_FightInRegsGroup_04.RunIt();

                if (step_F_Enemy_FightInRegsGroup_04.status == LogicJobStatus.Finished)
                {
                    step_F_Fight04_Finished.SetStatus(LogicFlagStatus.Active);

                    step_F_Enemy_FightInRegsGroup_04.SetOutStep(1000f);
                }
            }
            #endregion

        step_F_Enemy_FightInRegsGroup_04_End: ;

            #endregion

            #region step_F_Enemy_FightInRegsGroup_05

            #region 1 Start
            if (step_F_Enemy_FightInRegsGroup_05.outStep == 1f)
            {
                step_F_Enemy_FightInRegsGroup_05.StartIt();
                step_F_Enemy_FightInRegsGroup_05.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_F_Enemy_FightInRegsGroup_05.outStep == 1.1f)
            {
                step_F_Enemy_FightInRegsGroup_05.RunIt();

                if (step_F_Enemy_FightInRegsGroup_05.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    step_F_Enemy_FightInRegsGroup_05.StartFinishing_OutStepIfNotFinishing();
                    goto step_F_Enemy_FightInRegsGroup_05_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (step_F_Enemy_FightInRegsGroup_05.outStep == 900f)
            {
                step_F_Enemy_FightInRegsGroup_05.SetNeedsToBeFinished();
                step_F_Enemy_FightInRegsGroup_05.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (step_F_Enemy_FightInRegsGroup_05.outStep == 901f)
            {
                step_F_Enemy_FightInRegsGroup_05.RunIt();

                if (step_F_Enemy_FightInRegsGroup_05.status == LogicJobStatus.Finished)
                {
                    step_F_Fight05_Finished.SetStatus(LogicFlagStatus.Active);

                    step_F_Enemy_FightInRegsGroup_05.SetOutStep(1000f);
                }
            }
            #endregion

        step_F_Enemy_FightInRegsGroup_05_End: ;

            #endregion

            #region step_F_Objects_02_CountingJailSoldiersTrigger

            #region 1 Start
            if (step_F_Objects_02_CountingJailSoldiersTrigger.OutStep == 1) //Start
            {
                step_F_Objects_02_CountingJailSoldiersTrigger.SetEnabled(true);
                step_F_Objects_02_CountingJailSoldiersTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_F_Objects_02_CountingJailSoldiersTrigger.OutStep == 1.1f) //Run
            {
                if (step_F_Objects_02_CountingJailSoldiersTrigger.IsInsideObjsCountEqualOrBiggerThanValue(step_F_NumOfInsideTriggerSoldiersToFail))
                {
                    step_F_CountingJailSoldierTrigger.SetStatus(LogicFlagStatus.Active);
                    step_F_Objects_02_CountingJailSoldiersTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_F_Objects_02_CountingJailSoldiersTrigger.OutStep == 900f) //Finish
            {
                step_F_Objects_02_CountingJailSoldiersTrigger.SetEnabled(false);
                step_F_Objects_02_CountingJailSoldiersTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_F_Objects_04_CountingInsideSoldiersTrigger

            #region 1 Start
            if (step_F_Objects_04_CountingInsideSoldiersTrigger.OutStep == 1) //Start
            {
                step_F_Objects_04_CountingInsideSoldiersTrigger.SetEnabled(true);
                step_F_Objects_04_CountingInsideSoldiersTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_F_Objects_04_CountingInsideSoldiersTrigger.OutStep == 1.1f) //Run
            {
                if (step_F_Objects_04_CountingInsideSoldiersTrigger.IsPlayerIn())
                {
                    step_F_CountingInsideSoldierTrigger.SetStatus(LogicFlagStatus.Active);
                    step_F_Objects_04_CountingInsideSoldiersTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_F_Objects_04_CountingInsideSoldiersTrigger.OutStep == 900f) //Finish
            {
                step_F_Objects_04_CountingInsideSoldiersTrigger.SetEnabled(false);
                step_F_Objects_04_CountingInsideSoldiersTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_F_Objects_04_CountingOutsideSoldiersTrigger

            #region 1 Start
            if (step_F_Objects_04_CountingOutsideSoldiersTrigger.OutStep == 1) //Start
            {
                step_F_Objects_04_CountingOutsideSoldiersTrigger.SetEnabled(true);
                step_F_Objects_04_CountingOutsideSoldiersTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_F_Objects_04_CountingOutsideSoldiersTrigger.OutStep == 1.1f) //Run
            {
                if (step_F_Objects_04_CountingOutsideSoldiersTrigger.IsPlayerIn())
                {
                    step_F_CountingOutsideSoldierTrigger.SetStatus(LogicFlagStatus.Active);
                    step_F_Objects_04_CountingOutsideSoldiersTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_F_Objects_04_CountingOutsideSoldiersTrigger.OutStep == 900f) //Finish
            {
                step_F_Objects_04_CountingOutsideSoldiersTrigger.SetEnabled(false);
                step_F_Objects_04_CountingOutsideSoldiersTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_F_Objects_05_EscapeTrigger

            #region 1 Start
            if (step_F_Objects_05_EscapeTrigger.OutStep == 1) //Start
            {
                step_F_Objects_05_EscapeTrigger.SetEnabled(true);
                step_F_Objects_05_EscapeTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_F_Objects_05_EscapeTrigger.OutStep == 1.1f) //Run
            {
                if (step_F_Objects_05_EscapeTrigger.IsInsideObjsCountEqualOrBiggerThanValue(3))
                {
                    step_F_AlliesEntered_EscapeTrigger.SetStatus(LogicFlagStatus.Active);
                    step_F_Objects_05_EscapeTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_F_Objects_05_EscapeTrigger.OutStep == 900f) //Finish
            {
                step_F_Objects_05_EscapeTrigger.SetEnabled(false);
                step_F_Objects_05_EscapeTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

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

            return;
        }
        #endregion

        #region CheckPoint C
        if (levelStep == 3)
        {
            step_B_Objects_06_ExplodableDoor.SetActiveRecursively(false);

            step_C_Objects_StartPoint_Player.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);

            step_C_Objects_StartPoint_Ally01.PlaceCharacterOnIt(Ally01);
            step_C_Objects_StartPoint_Ally02.PlaceCharacterOnIt(Ally02);

            return;
        }
        #endregion

        #region CheckPoint D
        if (levelStep == 4)
        {
            mapLogic.HUD_ObjectivesPage_SetActiveObjective(4);
            mapLogic.HUD_ShowNewMission(3);

            step_D_Objects_StartPoint_Player.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);

            step_D_Objects_StartPoint_Ally01.PlaceCharacterOnIt(Ally01);
            step_D_Objects_StartPoint_Ally02.PlaceCharacterOnIt(Ally02);

            ambientExplosionDistance.Play();
            step_E_Objects_01_FarExplosionSounds.StartIt();

            return;
        }
        #endregion

        #region CheckPoint E
        if (levelStep == 5)
        {
            step_E_Objects_StartPoint_Player.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);

            step_E_Objects_StartPoint_Ally01.PlaceCharacterOnIt(Ally01);
            step_E_Objects_StartPoint_Ally02.PlaceCharacterOnIt(Ally02);

            step_E_Objects_01_FarExplosionSounds.StartIt();

            return;
        }
        #endregion

        #region CheckPoint F
        if (levelStep == 6)
        {
            mapLogic.HUD_ObjectivesPage_SetObjectiveDone(6);
            mapLogic.HUD_ShowCompleteMission(5);

            step_F_Objects_StartPoint_Player.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);

            step_F_Objects_StartPoint_Ally01.PlaceCharacterOnIt(Ally01);
            step_F_Objects_StartPoint_Ally02.PlaceCharacterOnIt(Ally02);

            string animName = step_E_Objects_03_Door.animation.clip.name;

            step_E_Objects_03_Door.animation.Play(animName);
            step_E_Objects_03_Door.animation[animName].time = step_E_Objects_03_Door.animation[animName].length;

            //step_E_Objects_01_FarExplosionSounds.StartIt();

            return;
        }
        #endregion
    }

    void ShowHiddenSoldiers()
    {
        foreach (SoldierInfo solInf in step_E_HiddenSoldiers)
        {
            foreach (SkinnedMeshRenderer skmrend in solInf.childSkinnedMeshRenderers)
            {
                skmrend.enabled = true;
            }

            foreach (MeshRenderer mrend in solInf.childMeshRenderers)
            {
                mrend.enabled = true;
            }
        }
    }

    void InitLogicPartStealthKills()
    {
        mapLogic.isB2_SnipeMode = true;

        foreach (MapLogicJob_FightInReg fightReg in step_E_Enemy_FightInRegsGroup_01.fightInRegs)
        {
            GameObject soldier = fightReg.controlledSoldier;

            SoldierDetectorCube solDetector = soldier.GetComponentInChildren<SoldierDetectorCube>();
            solDetector.StartIfItsNotStarted();
            soldierDetectorCubes.Add(solDetector);

            SoldierNearbyHitDetection solNearHitDetec = soldier.GetComponentInChildren<SoldierNearbyHitDetection>();
            SoldierNearbyHitDetections.Add(solNearHitDetec);
        }
    }

    bool CanGoNearWall()
    {
        if (DoSoldierDie(0) && DoSoldierDie(1))
        {
            return true;
        }
        return false;
    }

    bool CanGoThroughJail()
    {
        if (DoAllSoldiersDie())
        {
            return true;
        }
        return false;
    }

    bool FailByFiringSound()
    {
        float fireTime = PlayerCharacterNew.Instance.GetActiveGun().GetCurrentFireTime();

        if (fireTime > 0)
        {
            if (!step_E_Objects_01_FarExplosionSounds.audio.isPlaying)
            {
                return true;
            }
        }

        return false;
    }

    bool FailByNashSeen()
    {
        foreach (SoldierDetectorCube solDetec in soldierDetectorCubes)
        {
            if (solDetec.IsDudeNashSeen())
            {
                return true;
            }
        }

        return false;
    }

    bool FailByHitDetection()
    {
        foreach (SoldierNearbyHitDetection solNearHitDetec in SoldierNearbyHitDetections)
        {
            if (solNearHitDetec.IsBulletHitNearbySeen())
            {
                return true;
            }
        }

        return false;
    }

    bool DoAllSoldiersDie()
    {
        foreach (MapLogicJob_FightInReg fightReg in step_E_Enemy_FightInRegsGroup_01.fightInRegs)
        {
            GameObject sol = fightReg.controlledSoldier;

            if (sol != null)
            {
                return false;
            }
        }

        return true;
    }

    bool DoSoldierDie(int _index)
    {
        int index = _index;

        return (step_E_Enemy_FightInRegsGroup_01.fightInRegs[index].controlledSoldier == null);
    }

    bool IsSnipeModeFail()
    {
        if (FailByNashSeen())
        {
            missionFailType = step_MissionFail_EnemySawHisMateNash;

            return true;
        }

        if (FailByFiringSound())
        {
            missionFailType = step_MissionFail_EnemyHeardYourFire;

            return true;
        }

        if (FailByHitDetection())
        {
            missionFailType = step_MissionFail_YouAreDetectedByEnemies;

            return true;
        }

        return false;
    }

    void AddHeadArmors()
    {
        foreach (GameObject gobj in step_F_Ally_HeadArmors)
        {
            gobj.SetActiveRecursively(true);
        }
    }

    void RemoveHeadArmors()
    {
        foreach (GameObject gobj in step_F_Ally_HeadArmors)
        {
            gobj.SetActiveRecursively(false);
        }
    }

    void MakeDudesDieable()
    {
        Ally01.GetComponent<CharacterInfo>().SetRecievedDamageCoef(step_F_Ally_ReceiveDamageCoef);

        Ally02.GetComponent<CharacterInfo>().SetRecievedDamageCoef(step_F_Ally_ReceiveDamageCoef);

        Khaloo.GetComponent<CharacterInfo>().SetRecievedDamageCoef(step_F_Ally_ReceiveDamageCoef);
    }

    bool IsAnyOneDie()
    {
        if (Ally01 == null || Ally02 == null || Khaloo == null)
        {
            return true;
        }

        return false;
    }

    void PlayFirstMusic()
    {
        MusicController.Instance.PlayMusic(MusicSong.Action_D, 0);
    }

    //void PlaySadMusic()
    //{
    //    MusicController.Instance.PlayMusic(MusicSong.Ambient_A, 0);
    //}
}
