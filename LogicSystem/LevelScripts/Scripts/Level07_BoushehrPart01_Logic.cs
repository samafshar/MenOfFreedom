using UnityEngine;
using System.Collections;

public class Level07_BoushehrPart01_Logic : LevelLogic
{
    public LogicVoiceCollection logVoiceCol_Ally01;
    public LogicVoiceCollection logVoiceCol_Ally02;

    public CutsceneController dorahiCutscene;

    int allyChainJobsGlobalLogicIndex = -1;

    public GameObject ally01;
    public GameObject ally02;

    // // // // // 

    public StartPoint stepA_Objects_StartPoint_Ally01;

    public MapLogicJob_ChainJobsGroup stepA_chainJobGroup;
    public MapLogicJob_FightInRegsGroup stepA_FightInRegsGroup_01;
    public LogicTrigger stepA_Objects_ExitTrigger_01;
    public ExecutionArea stepA_Objects_ExecAreaA;
    public MapLogicJob_FightInRegsGroup stepA_FightInRegsGroup_02;
    public LogicTrigger stepA_Objects_ExitTrigger_02;
    public LogicDieTrigger stepA_Objects_DieTrigger_All;
    public ExecutionArea stepA_Objects_ExecAreaB;
    public Transform stepA_HUD_TaheKuchehMinimapTr;

    //

    LogicFlag flag_A_AllyChainJob_Finished = new LogicFlag();
    LogicFlag flag_A_Fight01_Finished = new LogicFlag();
    LogicFlag flag_A_PlayerEntered_ExitTrigger01 = new LogicFlag();
    LogicFlag flag_A_Fight02_Finished = new LogicFlag();
    LogicFlag flag_A_PlayerEntered_ExitTrigger02 = new LogicFlag();

    //

    // //

    public StartPoint stepB_Objects_StartPoint_Player;
    public StartPoint stepB_Objects_StartPoint_Ally01;

    public MapLogicJob_ChainJobsGroup stepB_AllyChainJobGroup;
    public MapLogicJob_FightInRegsGroup stepB_FightInRegsGroup_01;
    public MapLogicJob_FightInRegsGroup stepB_FightInRegsGroup_MG;
    public MapLogicJob_FightInRegsGroup stepB_FightInRegsGroup_Kh;
    public LogicTrigger stepB_Objects_ExitTrigger_Kh;
    public LogicTrigger stepB_Objects_ExitTrigger_01;
    public LogicTrigger stepB_Objects_ExitTrigger_02;
    public LogicTrigger stepB_Objects_LogicTrigger_EndOfTheEnd;
    public ExecutionArea stepB_Objects_ExecAreaA;
    public MapLogicJob_MachineGun stepB_Enemy_MachineGun_A;
    public LogicTrigger stepB_GettingDamageTrigger_MG;
    public LogicTrigger stepB_GettingDamageTrigger_Ally;
    public CharacterInfo stepB_Sold_MG;
    public CharacterInfo stepB_Sold_A;
    public CharacterInfo stepB_Sold_B;
    public GameObject stepB_Boshkeh_A;
    public GameObject stepB_Boshkeh_B;
    public LogicDieTrigger stepB_Objects_DieTrigger_FightA;
    public LogicDieTrigger stepB_Objects_DieTrigger_All;
    public GameObject[] stepB_AllInitialSolds;
    public Transform stepB_HUD_Kharabeh3DObjTr;
    public Transform stepB_HUD_MachineGun3DObjTr;

    //

    LogicFlag flag_B_AllyChainJob_Finished = new LogicFlag();
    LogicFlag flag_B_Fight01_Finished = new LogicFlag();
    LogicFlag flag_B_FightMG_Finished = new LogicFlag();
    LogicFlag flag_B_FightKh_Finished = new LogicFlag();
    LogicFlag flag_B_PlayerEntered_ExitTriggerKh = new LogicFlag();
    LogicFlag flag_B_PlayerEntered_ExitTrigger01 = new LogicFlag();
    LogicFlag flag_B_PlayerEntered_ExitTrigger02 = new LogicFlag();
    LogicFlag flag_B_PlayerEntered_LogicTrigger_EndOfTheEnd = new LogicFlag();

    // //

    public StartPoint stepC_Objects_StartPoint_Player;
    public StartPoint stepC_Objects_StartPoint_Ally01;
    public StartPoint stepC_Objects_StartPoint_Ally02;

    public MapLogicJob_ChainJobsGroup stepC_AllyChainJobGroup;
    public LogicTrigger stepC_Objects_ExitTrigger_Allies;
    public LogicTrigger stepC_Objects_StartTrigger_A;
    public MapLogicJob_FightInRegsGroup stepC_FightInRegsGroup_A;
    public LogicTrigger stepC_Objects_StartTrigger_B;
    public MapLogicJob_FightInRegsGroup stepC_FightInRegsGroup_B;
    public LogicTrigger stepC_Objects_StepExitTrigger;

    public Transform stepC_HUD_MeydunMinimapTr;

    //

    LogicFlag flag_C_AlliesEntered_ExitTriggerAllies = new LogicFlag();
    LogicFlag flag_C_PlayerEntered_StartTrigger_A = new LogicFlag();
    LogicFlag flag_C_FightA_Finished = new LogicFlag();
    LogicFlag flag_C_PlayerEntered_StartTrigger_B = new LogicFlag();
    LogicFlag flag_C_FightB_Finished = new LogicFlag();
    LogicFlag flag_C_PlayerEntered_StepExitTrigger = new LogicFlag();

    // // 

    public StartPoint stepD_Objects_StartPoint_Player;

    public MapLogicJob_FightInRegsGroup stepD_FightInRegsGroup_A;
    public float stepD_DelayToStartFightB = 18;
    public MapLogicJob_FightInRegsGroup stepD_FightInRegsGroup_B;
    public MapLogicJob_MachineGun stepD_Enemy_MachineGun_B;
    public ExecutionArea stepD_Objects_ExecArea;
    public LogicTrigger stepD_Objects_KillMGArea_A;
    public LogicTrigger stepD_Objects_KillMGArea_B;
    public LogicTrigger stepD_Objects_MGAlliesArea;
    public LogicTrigger stepD_Objects_ExitTrigger;
    public LogicDieTrigger stepD_Objects_DieTrigger_MG;

    public Transform stepD_HUD_KucheyeBadAzMeydunMinimapTr;
    //

    LogicFlag flag_D_FightA_Finished = new LogicFlag();
    LogicFlag flag_D_FightB_Finished = new LogicFlag();
    LogicFlag flag_D_ShouldKillMGSold = new LogicFlag();
    LogicFlag flag_D_PlayerEntered_ExitTrigger = new LogicFlag();

    // // 

    public StartPoint stepE_Objects_StartPoint_Player;

    public MapLogicJob_FightInRegsGroup stepE_FightInRegsGroup_A;
    public ExecutionArea stepE_Objects_ExecArea;
    public LogicTrigger stepE_Objects_StartTrigger;
    public LogicTrigger stepE_Objects_ExitTrigger;

    public Transform stepE_HUD_KucheyeBadAzDatTikkehMinimapTr;

    //

    LogicFlag flag_E_FightA_Finished = new LogicFlag();
    LogicFlag flag_E_PlayerEntered_StartTrigger = new LogicFlag();
    LogicFlag flag_E_PlayerEntered_ExitTrigger = new LogicFlag();

    // // 

    public StartPoint stepF_Objects_StartPoint_Player;

    public MapLogicJob_FightInRegsGroup stepF_FightInRegsGroup_A;
    public ExecutionArea stepF_Objects_ExecArea;
    public LogicTrigger stepF_Objects_StartTrigger;
    public LogicTrigger stepF_Objects_ExitTrigger;

    public Transform stepF_HUD_KucheyeGhablAzAllyhaMinimapTr;

    //

    LogicFlag flag_F_FightA_Finished = new LogicFlag();
    LogicFlag flag_F_PlayerEntered_StartTrigger = new LogicFlag();
    LogicFlag flag_F_PlayerEntered_ExitTrigger = new LogicFlag();

    // // 

    public StartPoint stepG_Objects_StartPoint_Player;
    public StartPoint stepG_Objects_StartPoint_Ally01;
    public StartPoint stepG_Objects_StartPoint_Ally02;

    public MapLogicJob_ChainJobsGroup stepG_AllyChainJobGroup;
    public MapLogicJob_FightInRegsGroup stepG_FightInRegsGroup_A;
    public MapLogicJob_FightInRegsGroup stepG_FightInRegsGroup_B;
    public MapLogicJob_MachineGun stepG_Enemy_MachineGun_B_01;
    public MapLogicJob_MachineGun stepG_Enemy_MachineGun_B_02;
    public LogicTrigger stepG_Objects_StartTrigger;
    public LogicTrigger stepG_Objects_ExitTrigger;
    public LogicTrigger stepG_Objects_StopCreatingFightATrigger;
    public LogicTrigger stepG_Objects_KucheyeChapTrigger;
    public ExecutionArea stepG_Objects_ExecArea;

    public Transform stepG_HUD_DoshmanayeTaheMapMinimapTr;
    public Transform stepG_HUD_KucheyeSamteChap3DObjTr;
    public Transform stepG_HUD_Poshtebum3DObjTr;

    //

    LogicFlag flag_G_AllyChainJob_Finished = new LogicFlag();
    LogicFlag flag_G_FightA_Finished = new LogicFlag();
    LogicFlag flag_G_PlayerEntered_StartTrigger = new LogicFlag();
    LogicFlag flag_G_PlayerEntered_ExitTrigger = new LogicFlag();
    LogicFlag flag_G_PlayerEntered_KucheyeChapTrigger = new LogicFlag();
    LogicFlag flag_G_PlayerEntered_StopCreatingFightATrigger = new LogicFlag();

    // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // //

    float timeCounter = 0;

    CharacterInfo ally01CharInfo;

    DamageInfluence B_Boshkeh_A_DamageInfluence;

    float hudTime_MaxTimeBeforeKharabeh = 27;
    bool isBoroKharabehVoiceStarted = false;
    bool isMachineGun3DObjStarted = false;

    float hudTime_MaxTimeTaheMapFight = 20;
    bool isBoroTuKucheyeSamteChapVoiceStarted = false;
    bool isKucheyeChapHUDEverStoppedWhileItWasRunning = false;

    //

    public override void StartIt()
    {
        base.StartIt();

        ally01CharInfo = ally01.GetComponent<CharacterInfo>();
        //ally02CharInfo = ally02.GetComponent<CharacterInfo>();

        //LoadCheckPoint(2.96f);


    }

    public override void RunIt()
    {
        base.RunIt();

        if (step == 1)
        {

        StartLevelSteps:

            #region Level

            #region 0.1 Start first cutscene
            if (levelStep == 0.1f)
            {
                SetLevelStep(1f);
            }
            #endregion

            #region 1 Init StepA_Rush01
            if (levelStep == 1)
            {
                SaveCheckPoint(1);

                A_PlaceAlly();

                allyChainJobsGlobalLogicIndex = 0;

                stepA_chainJobGroup.StartOutStepIfNotStarted();

                stepA_FightInRegsGroup_01.StartOutStepIfNotStarted();
                stepA_Objects_ExitTrigger_01.StartOutStepIfNotStarted();
                stepA_Objects_ExecAreaA.StartIt();

                logVoiceCol_Ally01.PlayName("A_01_BoroBoro");

                mapLogic.HUD_ObjectivesPage_SetActiveObjective(1);
                mapLogic.HUD_ShowNewMission(0);

                mapLogic.HUD_AddOnlyMinimap3DObj(stepA_HUD_TaheKuchehMinimapTr, "TaheKucheh");

                //

                logVoiceCol_Ally01.AddToPlayQueue("A_02_KenareBoshkeha", 6);
                logVoiceCol_Ally01.AddToPlayQueue("A_03_Bezaneshun", 7);
                logVoiceCol_Ally01.AddToPlayQueue("A_04_KenareDivar", 3);

                PlayFirstMusic();

                SetLevelStep(1.1f);
            }
            #endregion

            #region 1.1 Run StepA_Rush01
            if (levelStep == 1.1f)
            {
                if (flag_A_Fight01_Finished.IsEverActivated())
                {
                    SetLevelStep(1.2f);
                    goto StartLevelSteps;
                }

                if (flag_A_PlayerEntered_ExitTrigger01.IsEverActivated())
                {
                    SetLevelStep(1.11f);
                }
            }
            #endregion

            #region 1.11 Stop creating more soldiers in StepA_Rush01 and Make alive solds weak - Start Rush02
            if (levelStep == 1.11f)
            {
                stepA_FightInRegsGroup_01.StopCreatingMoreSoldiersAndMakeAliveSoldiersSoWeak();

                stepA_FightInRegsGroup_02.StartOutStepIfNotStarted();
                stepA_Objects_ExitTrigger_02.StartOutStepIfNotStarted();
                stepA_Objects_ExecAreaB.StartItIfItsNotStartedBefore();

                SetLevelStep(1.12f);
            }
            #endregion

            #region 1.12 W8 for StepA_Rush01 finish
            if (levelStep == 1.12f)
            {
                if (flag_A_PlayerEntered_ExitTrigger02.IsEverActivated())
                {
                    SetLevelStep(1.5f);
                    goto StartLevelSteps;
                }

                if (flag_A_Fight01_Finished.IsEverActivated())
                {
                    SetLevelStep(1.2f);
                }
            }
            #endregion

            #region 1.2 Start finishing allies - Start Rush02 if not started
            if (levelStep == 1.2f)
            {
                allyChainJobsGlobalLogicIndex = 1;
                stepA_chainJobGroup.Init_SetNewGlobalLogicIndex(allyChainJobsGlobalLogicIndex);

                stepA_FightInRegsGroup_02.StartOutStepIfNotStarted();
                stepA_Objects_ExitTrigger_02.StartOutStepIfNotStarted();
                stepA_Objects_ExecAreaB.StartItIfItsNotStartedBefore();

                logVoiceCol_Ally01.EmptyQueue();
                logVoiceCol_Ally01.AddToPlayQueue("A_05_Move", 1.5f);
                logVoiceCol_Ally01.AddToPlayQueue("A_06_Sangar", 5.5f);
                logVoiceCol_Ally01.AddToPlayQueue("A_07_Bezanesh", 7f);

                SetLevelStep(1.3f);
            }
            #endregion

            #region 1.3 Run StepA_Rush02
            if (levelStep == 1.3f)
            {
                if (flag_A_PlayerEntered_ExitTrigger02.IsEverActivated())
                {
                    SetLevelStep(1.5f);
                    goto StartLevelSteps;
                }

                if (flag_A_Fight02_Finished.IsEverActivated())
                {
                    SetLevelStep(1.5f);
                }
            }
            #endregion

            #region 1.5 Start Ending StepA
            if (levelStep == 1.5f)
            {
                stepA_FightInRegsGroup_02.StopCreatingMoreSoldiers();
                stepA_Objects_DieTrigger_All.StartIt();
                stepA_chainJobGroup.StartFinishing_OutStepIfNotFinishing();

                mapLogic.HUD_ObjectivesPage_SetObjectiveDone(1);
                mapLogic.HUD_ShowCompleteMission(0);

                mapLogic.HUD_RemoveMinimap3DObj("TaheKucheh");

                SetLevelStep(1.6f);
                goto EndLevelSteps;
            }
            #endregion

            #region 1.6 Ending StepA
            if (levelStep == 1.6f)
            {
                if (flag_A_AllyChainJob_Finished.IsEverActivated())
                {
                    stepA_Objects_ExecAreaA.EndIt();
                    stepA_Objects_ExecAreaB.EndIt();

                    SetLevelStep(2f);
                }
            }
            #endregion

            #region 2 Init StepB
            if (levelStep == 2)
            {
                SaveCheckPoint(2);

                B_Boshkeh_A_DamageInfluence = stepB_Boshkeh_A.GetComponent<DamageInfluence>();

                allyChainJobsGlobalLogicIndex = 0;

                stepB_AllyChainJobGroup.StartOutStepIfNotStarted();

                stepB_FightInRegsGroup_01.StartOutStepIfNotStarted();
                stepB_FightInRegsGroup_MG.StartOutStepIfNotStarted();
                stepB_Enemy_MachineGun_A.StartOutStepIfNotStarted();

                stepB_Objects_ExitTrigger_Kh.StartOutStepIfNotStarted();
                stepB_Objects_ExitTrigger_01.StartOutStepIfNotStarted();
                stepB_Objects_ExitTrigger_02.StartOutStepIfNotStarted();

                stepB_Objects_LogicTrigger_EndOfTheEnd.StartOutStepIfNotStarted();

                stepB_Objects_ExecAreaA.StartIt();

                logVoiceCol_Ally01.EmptyQueue();
                logVoiceCol_Ally01.PlayName("B_01_BiaEeTaraf");

                stepB_GettingDamageTrigger_MG.SetEnabled(true);
                stepB_GettingDamageTrigger_Ally.SetEnabled(true);

                mapLogic.HUD_ShowGameSaved();

                mapLogic.HUD_ObjectivesPage_SetActiveObjective(2);
                mapLogic.HUD_ShowNewMission(1);

                hudCounter = hudTime_MaxTimeBeforeKharabeh;

                logVoiceCol_Ally01.AddToPlayQueue("B_03_Mosalsal", 3.8f);

                PlayFirstMusic();

                SetLevelStep(2.1f);
            }
            #endregion

            #region 2.1 Wait for player to enter exit trigger A
            if (levelStep == 2.1f)
            {
                if (flag_B_PlayerEntered_ExitTriggerKh.IsEverActivated())
                {
                    stepB_FightInRegsGroup_Kh.StartOutStepIfNotStarted();

                    if (isBoroKharabehVoiceStarted)
                    {
                        isBoroKharabehVoiceStarted = false;
                        logVoiceCol_Ally01.StopCurVoiceAfterItsFinishing();
                        mapLogic.HUD_Remove3DObjective("Kharabeh");
                    }

                    if (!isMachineGun3DObjStarted)
                    {
                        isMachineGun3DObjStarted = true;
                        mapLogic.HUD_Add3DObjective(stepB_HUD_MachineGun3DObjTr, The3DObjIconType.Dot, "MachineGun", The3DObjViewRange.Medium);
                    }
                }
                else
                {
                    hudCounter = MathfPlus.DecByDeltatimeToZero(hudCounter);

                    if (hudCounter == 0)
                    {
                        logVoiceCol_Ally01.PlayName("B_02_KharabieSamteRast");
                        hudCounter = 1000000000;
                        isBoroKharabehVoiceStarted = true;

                        mapLogic.HUD_Add3DObjective(stepB_HUD_Kharabeh3DObjTr, The3DObjIconType.FeleshRooBePayin, "Kharabeh", The3DObjViewRange.Medium);
                    }

                    if (isBoroKharabehVoiceStarted)
                    {
                        if (logVoiceCol_Ally01.IsPlayedRightNow())
                        {
                            mapLogic.HUD_3DObjBlinkInMinimap("Kharabeh");
                        }
                    }
                }

                if (flag_B_PlayerEntered_ExitTrigger01.IsEverActivated())
                {
                    stepB_FightInRegsGroup_01.StopCreatingMoreSoldiersAndMakeAliveSoldiersSoWeak();

                    SetLevelStep(2.2f);
                }
            }
            #endregion

            #region 2.2 Waiting for killing an MG Object - Waiting for Finishing Fight01 by ally
            if (levelStep == 2.2f)
            {
                if (isBoroKharabehVoiceStarted)
                {
                    isBoroKharabehVoiceStarted = false;
                    logVoiceCol_Ally01.StopCurVoiceAfterItsFinishing();
                    mapLogic.HUD_Remove3DObjective("Kharabeh");
                }

                if (flag_B_PlayerEntered_LogicTrigger_EndOfTheEnd.IsEverActivated())
                {
                    SetLevelStep(step_MissionFail_AlliesNotSupported);
                    goto EndLevelSteps;
                }

                if (flag_B_PlayerEntered_ExitTrigger02.IsEverActivated())
                {
                    stepB_Objects_DieTrigger_FightA.StartItIfItsNotStartedBefore();
                }

                if (flag_B_Fight01_Finished.IsEverActivated())
                {
                    SetLevelStep(2.3f);
                    goto StartLevelSteps;
                }

                if (B_IsOneOfDatEnemiesDead())
                {
                    if (B_Boshkeh_A_DamageInfluence != null)
                    {
                        B_Boshkeh_A_DamageInfluence.DamageOccur();
                    }
                }

                if (B_Boshkeh_A_DamageInfluence == null)
                {
                    SetLevelStep(2.5f);
                }
            }
            #endregion

            #region 2.3 Start ally next step
            if (levelStep == 2.3f)
            {
                allyChainJobsGlobalLogicIndex = 1;
                stepB_AllyChainJobGroup.Init_SetNewGlobalLogicIndex(allyChainJobsGlobalLogicIndex);

                SetLevelStep(2.4f);
            }
            #endregion

            #region 2.4 Waiting for killing an MG Object
            if (levelStep == 2.4f)
            {
                if (flag_B_PlayerEntered_LogicTrigger_EndOfTheEnd.IsEverActivated())
                {
                    SetLevelStep(step_MissionFail_AlliesNotSupported);
                    goto EndLevelSteps;
                }

                if (B_IsOneOfDatEnemiesDead())
                {
                    if (B_Boshkeh_A_DamageInfluence != null)
                    {
                        B_Boshkeh_A_DamageInfluence.DamageOccur();
                    }
                }

                if (B_Boshkeh_A_DamageInfluence == null)
                {
                    SetLevelStep(2.5f);
                }
            }
            #endregion

            #region 2.5 Kill all enemies and stop some things
            if (levelStep == 2.5f)
            {
                stepB_Objects_DieTrigger_All.StartItIfItsNotStartedBefore();

                //

                stepB_Objects_LogicTrigger_EndOfTheEnd.StartFinishing_OutStepIfNotFishining();

                stepB_Objects_ExecAreaA.EndIt();

                stepB_GettingDamageTrigger_MG.SetEnabled(false);
                stepB_GettingDamageTrigger_Ally.SetEnabled(false);

                if (isMachineGun3DObjStarted)
                {
                    isMachineGun3DObjStarted = false;
                    mapLogic.HUD_Remove3DObjective("MachineGun");
                }

                mapLogic.HUD_ObjectivesPage_SetObjectiveDone(2);
                mapLogic.HUD_ShowCompleteMission(1);

                SetLevelStep(2.6f);
            }
            #endregion

            #region 2.6 Start ally last step
            if (levelStep == 2.6f)
            {
                allyChainJobsGlobalLogicIndex = 2;
                stepB_AllyChainJobGroup.Init_SetNewGlobalLogicIndex(allyChainJobsGlobalLogicIndex);

                timeCounter = 1.5f;

                SetLevelStep(2.7f);
            }
            #endregion

            #region 2.7 Wait some secs to start cutscene
            if (levelStep == 2.7f)
            {
                timeCounter = MathfPlus.DecByDeltatimeToZero(timeCounter);

                if (timeCounter == 0)
                {
                    SetLevelStep(2.9f);
                }
            }
            #endregion

            #region 2.9 Start screen fading
            if (levelStep == 2.9f)
            {
                mapLogic.blackScreenFader.StartFadingOut();
                SetLevelStep(2.95f);
            }
            #endregion

            #region 2.95 fading screen
            if (levelStep == 2.95f)
            {
                if (mapLogic.blackScreenFader.isFadingFinished)
                {
                    SetLevelStep(2.96f);
                }
            }
            #endregion

            #region 2.96 Start dorahi cutscene
            if (levelStep == 2.96f)
            {
                stepB_AllyChainJobGroup.StartFinishing_OutStepIfNotFinishing();

                dorahiCutscene.StartIt();

                SetLevelStep(2.97f);
            }
            #endregion

            #region 2.97 Run cutscene
            if (levelStep == 2.97f)
            {
                if (dorahiCutscene.status == CutsceneStatus.Finished)
                {
                    SetLevelStep(2.98f);
                }
            }
            #endregion

            #region 2.98 BlackScreen after cutscene
            if (levelStep == 2.98f)
            {
                mapLogic.blackScreenFader.StartFadingIn();
                SetLevelStep(3f);
            }
            #endregion

            #region 3 Init
            if (levelStep == 3)
            {
                SaveCheckPoint(3);

                Load_C_PlaceSolds();

                allyChainJobsGlobalLogicIndex = 0;

                stepC_AllyChainJobGroup.StartOutStepIfNotStarted();

                stepC_Objects_ExitTrigger_Allies.StartOutStepIfNotStarted();

                stepC_Objects_StartTrigger_A.StartOutStepIfNotStarted();

                stepC_Objects_StartTrigger_B.StartOutStepIfNotStarted();

                stepC_Objects_StepExitTrigger.StartOutStepIfNotStarted();

                logVoiceCol_Ally01.PlayName("C_01_YallaRahBioftin");

                mapLogic.HUD_ShowGameSaved();

                mapLogic.HUD_ObjectivesPage_SetActiveObjective(3);
                mapLogic.HUD_ShowNewMission(2);

                mapLogic.HUD_AddOnlyMinimap3DObj(stepC_HUD_MeydunMinimapTr, "Meydun");

                PlayFirstMusic();

                SetLevelStep(3.1f);
            }
            #endregion

            #region 3.1 Wait for starting FightA
            if (levelStep == 3.1f)
            {
                if (flag_C_AlliesEntered_ExitTriggerAllies.IsEverActivated())
                {
                    stepC_AllyChainJobGroup.StartFinishing_OutStepIfNotFinishing();
                }

                if (flag_C_PlayerEntered_StartTrigger_A.IsEverActivated())
                {
                    stepC_FightInRegsGroup_A.StartOutStepIfNotStarted();

                    SetLevelStep(3.2f);
                }
            }
            #endregion

            #region 3.2 Running FightA - Waiting to start FightB
            if (levelStep == 3.2f)
            {
                if (flag_C_AlliesEntered_ExitTriggerAllies.IsEverActivated())
                {
                    stepC_AllyChainJobGroup.StartFinishing_OutStepIfNotFinishing();
                }

                if (flag_C_PlayerEntered_StartTrigger_B.IsEverActivated())
                {
                    stepC_FightInRegsGroup_B.StartOutStepIfNotStarted();

                    SetLevelStep(3.3f);
                }
            }
            #endregion

            #region 3.3 Running FightB - Waiting for player enter step exit trigger
            if (levelStep == 3.3f)
            {
                if (flag_C_PlayerEntered_StepExitTrigger.IsEverActivated())
                {
                    mapLogic.HUD_ObjectivesPage_SetObjectiveDone(3);
                    mapLogic.HUD_ShowCompleteMission(2);

                    mapLogic.HUD_RemoveMinimap3DObj("Meydun");

                    SetLevelStep(4f);
                }
            }
            #endregion

            #region 4 Init
            if (levelStep == 4)
            {
                SaveCheckPoint(4);

                timeCounter = stepD_DelayToStartFightB;

                stepD_FightInRegsGroup_A.StartOutStepIfNotStarted();

                stepD_Objects_ExecArea.StartIt();

                stepD_Objects_ExitTrigger.StartOutStepIfNotStarted();

                mapLogic.HUD_ShowGameSaved();

                mapLogic.HUD_ObjectivesPage_SetActiveObjective(4);
                mapLogic.HUD_ShowNewMission(3);

                mapLogic.HUD_AddOnlyMinimap3DObj(stepD_HUD_KucheyeBadAzMeydunMinimapTr, "KucheyeBadAzMeydun");

                PlayFirstMusic();

                SetLevelStep(4.1f);
            }
            #endregion

            #region 4.1 W8 for starting fightB
            if (levelStep == 4.1f)
            {
                timeCounter = MathfPlus.DecByDeltatimeToZero(timeCounter);

                if (timeCounter == 0)
                {
                    SetLevelStep(4.2f);
                }
            }
            #endregion

            #region 4.2 Start fightB and related areas
            if (levelStep == 4.2f)
            {
                stepD_FightInRegsGroup_B.StartOutStepIfNotStarted();
                stepD_Enemy_MachineGun_B.StartOutStepIfNotStarted();

                stepD_Objects_KillMGArea_A.StartOutStepIfNotStarted();
                stepD_Objects_KillMGArea_B.StartOutStepIfNotStarted();
                stepD_Objects_MGAlliesArea.StartOutStepIfNotStarted();

                SetLevelStep(4.3f);
            }
            #endregion

            #region 4.3 Run fights
            if (levelStep == 4.3f)
            {
                if (flag_D_PlayerEntered_ExitTrigger.IsEverActivated())
                {
                    flag_D_ShouldKillMGSold.SetStatus(LogicFlagStatus.Active);
                }

                if (flag_D_ShouldKillMGSold.IsEverActivated())
                {
                    stepD_Objects_DieTrigger_MG.StartItIfItsNotStartedBefore();
                }

                if (flag_D_PlayerEntered_ExitTrigger.IsEverActivated())
                {
                    SetLevelStep(4.4f);
                }
            }
            #endregion

            #region 4.4 End step
            if (levelStep == 4.4f)
            {
                stepD_Objects_ExecArea.EndIt();
                stepD_Objects_KillMGArea_A.StartFinishing_OutStepIfNotFishining();
                stepD_Objects_KillMGArea_B.StartFinishing_OutStepIfNotFishining();
                stepD_Objects_MGAlliesArea.StartFinishing_OutStepIfNotFishining();

                mapLogic.HUD_ObjectivesPage_SetObjectiveDone(4);
                mapLogic.HUD_ShowCompleteMission(3);

                mapLogic.HUD_RemoveMinimap3DObj("KucheyeBadAzMeydun");

                SetLevelStep(5f);
            }
            #endregion

            #region 5 Init
            if (levelStep == 5)
            {
                SaveCheckPoint(5);

                stepE_Objects_ExecArea.StartIt();

                stepE_Objects_StartTrigger.StartOutStepIfNotStarted();

                stepE_Objects_ExitTrigger.StartOutStepIfNotStarted();

                mapLogic.HUD_ShowGameSaved();

                mapLogic.HUD_ObjectivesPage_SetActiveObjective(5);
                mapLogic.HUD_ShowNewMission(4);

                mapLogic.HUD_AddOnlyMinimap3DObj(stepE_HUD_KucheyeBadAzDatTikkehMinimapTr, "KucheyeBadAzDatTikkeh");

                PlayFirstMusic();

                SetLevelStep(5.1f);
            }
            #endregion

            #region 5.1 W8 for player entering start trigger
            if (levelStep == 5.1f)
            {
                if (flag_E_PlayerEntered_StartTrigger.IsEverActivated())
                {
                    SetLevelStep(5.2f);
                }
            }
            #endregion

            #region 5.2 Start fightA
            if (levelStep == 5.2f)
            {
                stepE_FightInRegsGroup_A.StartOutStepIfNotStarted();

                SetLevelStep(5.3f);
            }
            #endregion

            #region 5.3 Run fightA
            if (levelStep == 5.3f)
            {
                if (flag_E_PlayerEntered_ExitTrigger.IsEverActivated())
                {
                    SetLevelStep(5.4f);
                }
            }
            #endregion

            #region 5.4 End step
            if (levelStep == 5.4f)
            {
                stepE_Objects_ExecArea.EndIt();

                mapLogic.HUD_ObjectivesPage_SetObjectiveDone(5);
                mapLogic.HUD_ShowCompleteMission(4);

                mapLogic.HUD_RemoveMinimap3DObj("KucheyeBadAzDatTikkeh");

                SetLevelStep(6f);
            }
            #endregion

            #region 6 Init
            if (levelStep == 6)
            {
                SaveCheckPoint(6);

                stepF_Objects_ExecArea.StartIt();

                stepF_Objects_StartTrigger.StartOutStepIfNotStarted();

                stepF_Objects_ExitTrigger.StartOutStepIfNotStarted();

                mapLogic.HUD_ShowGameSaved();

                mapLogic.HUD_ObjectivesPage_SetActiveObjective(6);
                mapLogic.HUD_ShowNewMission(5);

                mapLogic.HUD_AddOnlyMinimap3DObj(stepF_HUD_KucheyeGhablAzAllyhaMinimapTr, "KucheyeGhablAzAllyha");

                PlayFirstMusic();

                SetLevelStep(6.1f);
            }
            #endregion

            #region 6.1 W8 for player entering start trigger
            if (levelStep == 6.1f)
            {
                if (flag_F_PlayerEntered_StartTrigger.IsEverActivated())
                {
                    SetLevelStep(6.2f);
                }
            }
            #endregion

            #region 6.2 Start fightA
            if (levelStep == 6.2f)
            {
                stepF_FightInRegsGroup_A.StartOutStepIfNotStarted();

                SetLevelStep(6.3f);
            }
            #endregion

            #region 6.3 Run fightA
            if (levelStep == 6.3f)
            {
                if (flag_F_PlayerEntered_ExitTrigger.IsEverActivated())
                {
                    SetLevelStep(6.4f);
                }
            }
            #endregion

            #region 6.4 End step
            if (levelStep == 6.4f)
            {
                stepF_Objects_ExecArea.EndIt();

                mapLogic.HUD_ObjectivesPage_SetObjectiveDone(6);
                mapLogic.HUD_ShowCompleteMission(5);

                mapLogic.HUD_RemoveMinimap3DObj("KucheyeGhablAzAllyha");

                SetLevelStep(7f);
            }
            #endregion

            #region 7 Init
            if (levelStep == 7)
            {
                SaveCheckPoint(7);

                stepG_Objects_StartTrigger.StartOutStepIfNotStarted();

                stepG_Objects_KucheyeChapTrigger.StartOutStepIfNotStarted();

                stepG_Objects_ExitTrigger.StartOutStepIfNotStarted();

                stepG_Objects_StopCreatingFightATrigger.StartOutStepIfNotStarted();

                stepG_Objects_ExecArea.StartIt();

                mapLogic.HUD_ShowGameSaved();

                mapLogic.HUD_ObjectivesPage_SetActiveObjective(7);
                mapLogic.HUD_ShowNewMission(6);

                mapLogic.HUD_AddOnlyMinimap3DObj(stepG_HUD_DoshmanayeTaheMapMinimapTr, "DoshmanayeTaheMap");

                logVoiceCol_Ally01.PlayName("G_02_TirandaziKonin");
                logVoiceCol_Ally01.AddToPlayQueue("G_03_Bokoshideshun", 12);
                logVoiceCol_Ally02.PlayName("G_01_UnPosht");
                logVoiceCol_Ally02.AddToPlayQueue("G_02_KenareBoshkeha", 3);
                logVoiceCol_Ally02.AddToPlayQueue("G_03_YallaDigeBezanesh", 21);

                PlayFirstMusic();

                SetLevelStep(7.1f);
            }
            #endregion

            #region 7.1 W8 for player entering start trigger
            if (levelStep == 7.1f)
            {
                if (flag_G_PlayerEntered_StartTrigger.IsEverActivated())
                {
                    SetLevelStep(7.2f);
                }
            }
            #endregion

            #region 7.2 Start Allies and fightA
            if (levelStep == 7.2f)
            {
                G_PlaceAllies();

                allyChainJobsGlobalLogicIndex = 0;

                stepG_AllyChainJobGroup.StartOutStepIfNotStarted();

                stepG_FightInRegsGroup_A.StartOutStepIfNotStarted();

                SetLevelStep(7.3f);
                goto EndLevelSteps;
            }
            #endregion

            #region 7.3 Run fightA and w8 for player entering trigger or fightA solds be less than a number
            if (levelStep == 7.3f)
            {
                if (flag_G_PlayerEntered_StopCreatingFightATrigger.IsEverActivated())
                {
                    SetLevelStep(7.5f);
                    goto StartLevelSteps;
                }

                if (stepG_FightInRegsGroup_A.countOfRemainingSolds <= 2)
                {
                    SetLevelStep(7.5f);
                    goto StartLevelSteps;
                }
            }
            #endregion

            #region 7.5 Stop creating more solds in fightA and start fightB
            if (levelStep == 7.5f)
            {
                stepG_FightInRegsGroup_A.StopCreatingMoreSoldiersAndMakeAliveSoldiersSoWeak();

                stepG_FightInRegsGroup_B.StartOutStepIfNotStarted();
                stepG_Enemy_MachineGun_B_01.StartOutStepIfNotStarted();
                stepG_Enemy_MachineGun_B_02.StartOutStepIfNotStarted();

                hudCounter = hudTime_MaxTimeTaheMapFight;

                logVoiceCol_Ally01.EmptyQueue();
                logVoiceCol_Ally02.EmptyQueue();
                logVoiceCol_Ally01.AddToPlayQueue("G_04_Mosalsal", 4.2f);
                logVoiceCol_Ally02.AddToPlayQueue("G_04_PoshteDivara", 2.5f);
                SetLevelStep(7.6f);
            }
            #endregion

            #region 7.6 W8 for finishing fightA - W8 for player enter kucheh trigger
            if (levelStep == 7.6f)
            {
                if (flag_G_FightA_Finished.IsEverActivated())
                {
                    SetLevelStep(7.8f);
                    goto StartLevelSteps;
                }

                if (!isBoroTuKucheyeSamteChapVoiceStarted)
                {
                    hudCounter = MathfPlus.DecByDeltatimeToZero(hudCounter);

                    if (hudCounter == 0)
                    {
                        logVoiceCol_Ally01.PlayName("G_01_BoroKucheyeChap");
                        isBoroTuKucheyeSamteChapVoiceStarted = true;

                        mapLogic.HUD_RemoveMinimap3DObj("DoshmanayeTaheMap");

                        mapLogic.HUD_Add3DObjective(stepG_HUD_KucheyeSamteChap3DObjTr, The3DObjIconType.FeleshRooBePayin, "KucheyeChap", The3DObjViewRange.Medium);
                    }
                }

                if (isBoroTuKucheyeSamteChapVoiceStarted)
                {
                    if (logVoiceCol_Ally01.IsPlayedRightNow())
                    {
                        mapLogic.HUD_3DObjBlinkInMinimap("KucheyeChap");
                        mapLogic.HUD_ShowNewMission(7);
                    }
                }

                if (flag_G_PlayerEntered_KucheyeChapTrigger.IsEverActivated())
                {
                    SetLevelStep(7.65f);
                }
            }
            #endregion

            #region 7.65 Player entered KucheyeChapTrigger before ally fightA finished
            if (levelStep == 7.65f)
            {
                if (isBoroTuKucheyeSamteChapVoiceStarted)
                {
                    isBoroTuKucheyeSamteChapVoiceStarted = false;
                    isKucheyeChapHUDEverStoppedWhileItWasRunning = true;

                    logVoiceCol_Ally01.StopCurVoiceAfterItsFinishing();
                    mapLogic.HUD_Remove3DObjective("KucheyeChap");
                }

                mapLogic.HUD_ShowNewMission(8);

                mapLogic.HUD_RemoveMinimap3DObj("DoshmanayeTaheMap");

                mapLogic.HUD_Add3DObjective(stepG_HUD_Poshtebum3DObjTr, The3DObjIconType.FeleshRooBePayin, "Poshtebum", The3DObjViewRange.Medium);

                SetLevelStep(7.66f);
            }
            #endregion

            #region 7.66 (KucheyeChapTrigger is started before) W8 for finishing fightA - W8 for player enter exit trigger
            if (levelStep == 7.66f)
            {
                if (flag_G_FightA_Finished.IsEverActivated())
                {
                    SetLevelStep(7.7f);
                    goto StartLevelSteps;
                }

                if (flag_G_PlayerEntered_ExitTrigger.IsEverActivated())
                {
                    SetLevelStep(7.9f);
                }
            }
            #endregion

            #region 7.7 Start allies next step
            if (levelStep == 7.7f)
            {
                allyChainJobsGlobalLogicIndex = 1;
                stepG_AllyChainJobGroup.Init_SetNewGlobalLogicIndex(allyChainJobsGlobalLogicIndex);

                logVoiceCol_Ally02.AddToPlayQueue("G_05_BoroBoro", 2f);

                SetLevelStep(7.75f);
            }
            #endregion

            #region 7.75 W8 for player enter exit trigger
            if (levelStep == 7.75f)
            {
                if (flag_G_PlayerEntered_ExitTrigger.IsEverActivated())
                {
                    SetLevelStep(7.9f);
                }
            }
            #endregion

            #region 7.8 Start allies next step
            if (levelStep == 7.8f)
            {
                allyChainJobsGlobalLogicIndex = 1;
                stepG_AllyChainJobGroup.Init_SetNewGlobalLogicIndex(allyChainJobsGlobalLogicIndex);

                SetLevelStep(7.81f);
            }
            #endregion

            #region 7.81 W8 for player enter kucheh trigger
            if (levelStep == 7.81f)
            {
                if (!isBoroTuKucheyeSamteChapVoiceStarted)
                {
                    hudCounter = MathfPlus.DecByDeltatimeToZero(hudCounter);

                    if (hudCounter == 0)
                    {
                        logVoiceCol_Ally01.PlayName("G_01_BoroKucheyeChap");
                        isBoroTuKucheyeSamteChapVoiceStarted = true;

                        mapLogic.HUD_RemoveMinimap3DObj("DoshmanayeTaheMap");

                        mapLogic.HUD_Add3DObjective(stepG_HUD_KucheyeSamteChap3DObjTr, The3DObjIconType.FeleshRooBePayin, "KucheyeChap", The3DObjViewRange.Medium);
                    }
                }

                if (isBoroTuKucheyeSamteChapVoiceStarted)
                {
                    if (logVoiceCol_Ally01.IsPlayedRightNow())
                    {
                        mapLogic.HUD_3DObjBlinkInMinimap("KucheyeChap");
                        mapLogic.HUD_ShowNewMission(7);
                    }
                }

                if (flag_G_PlayerEntered_KucheyeChapTrigger.IsEverActivated())
                {
                    SetLevelStep(7.82f);
                }
            }
            #endregion

            #region 7.82 Player entered KucheyeChapTrigger
            if (levelStep == 7.82f)
            {
                if (isBoroTuKucheyeSamteChapVoiceStarted)
                {
                    isBoroTuKucheyeSamteChapVoiceStarted = false;
                    isKucheyeChapHUDEverStoppedWhileItWasRunning = true;

                    logVoiceCol_Ally01.StopCurVoiceAfterItsFinishing();
                    mapLogic.HUD_Remove3DObjective("KucheyeChap");
                }

                mapLogic.HUD_ShowNewMission(8);

                mapLogic.HUD_RemoveMinimap3DObj("DoshmanayeTaheMap");

                mapLogic.HUD_Add3DObjective(stepG_HUD_Poshtebum3DObjTr, The3DObjIconType.FeleshRooBePayin, "Poshtebum", The3DObjViewRange.Medium);

                SetLevelStep(7.83f);
            }
            #endregion

            #region 7.83 (KucheyeChapTrigger is started before) W8 for player enter exit trigger
            if (levelStep == 7.83f)
            {
                if (flag_G_PlayerEntered_ExitTrigger.IsEverActivated())
                {
                    SetLevelStep(7.9f);
                }
            }
            #endregion

            #region 7.9 Start screen fading
            if (levelStep == 7.9f)
            {
                mapLogic.HUD_ObjectivesPage_SetObjectiveDone(7);
                mapLogic.HUD_ShowCompleteMission(6);
                mapLogic.HUD_ShowMainMission(true);

                mapLogic.HUD_Remove3DObjective("Poshtebum");

                mapLogic.blackScreenFader.StartFadingOut();
                SetLevelStep(7.95f);
            }
            #endregion

            #region 7.95
            if (levelStep == 7.95f)
            {
                stepG_Objects_ExecArea.EndIt();
                SetLevelStep(7.96f);
            }
            #endregion

            #region 7.96f Start End black screen
            if (levelStep == 7.96f)
            {
                mapLogic.blackScreenFader.StartFadingOut();
                SetLevelStep(7.97f);
            }
            #endregion

            #region 7.97f Set mission is finished if black screen fading is done.
            if (levelStep == 7.97f)
            {
                if (mapLogic.blackScreenFader.isFadingFinished)
                {
                    SetMissionIsFinished();
                    SetLevelStep(8f);
                }
            }
            #endregion
            #endregion

        EndLevelSteps: ;

            // A

            #region Ally

            #region 1 Start
            if (stepA_chainJobGroup.outStep == 1)
            {
                stepA_chainJobGroup.Init_SetNewGlobalLogicIndex(allyChainJobsGlobalLogicIndex);
                stepA_chainJobGroup.StartIt();

                stepA_chainJobGroup.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepA_chainJobGroup.outStep == 1.1f)
            {
                stepA_chainJobGroup.RunIt();
            }
            #endregion

            #region 900 Start finishing
            if (stepA_chainJobGroup.outStep == 900f)
            {
                stepA_chainJobGroup.SetNeedsToBeFinished_EvenStopMoving();

                stepA_chainJobGroup.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (stepA_chainJobGroup.outStep == 901f)
            {
                stepA_chainJobGroup.RunIt();

                if (stepA_chainJobGroup.status == LogicJobStatus.Finished)
                {
                    flag_A_AllyChainJob_Finished.SetStatus(LogicFlagStatus.Active);

                    stepA_chainJobGroup.SetOutStep(1000);
                }
            }
            #endregion

            #endregion

            #region Fight01

            #region 1 Start
            if (stepA_FightInRegsGroup_01.outStep == 1)
            {
                stepA_FightInRegsGroup_01.StartIt();

                stepA_FightInRegsGroup_01.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepA_FightInRegsGroup_01.outStep == 1.1f)
            {
                stepA_FightInRegsGroup_01.RunIt();

                if (stepA_FightInRegsGroup_01.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    stepA_FightInRegsGroup_01.StartFinishing_OutStepIfNotFinishing();

                    goto fightInRegsGroup_A_01_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (stepA_FightInRegsGroup_01.outStep == 900f)
            {
                stepA_FightInRegsGroup_01.SetNeedsToBeFinished();
                stepA_FightInRegsGroup_01.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (stepA_FightInRegsGroup_01.outStep == 901f)
            {
                stepA_FightInRegsGroup_01.RunIt();

                if (stepA_FightInRegsGroup_01.status == LogicJobStatus.Finished)
                {
                    stepA_FightInRegsGroup_01.SetOutStep(1000f);
                    flag_A_Fight01_Finished.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

        fightInRegsGroup_A_01_End: ;

            #endregion

            #region ExitTrigger01

            #region 1 Start
            if (stepA_Objects_ExitTrigger_01.OutStep == 1) //Start
            {
                stepA_Objects_ExitTrigger_01.SetEnabled(true);
                stepA_Objects_ExitTrigger_01.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepA_Objects_ExitTrigger_01.OutStep == 1.1f) //Run
            {
                if (stepA_Objects_ExitTrigger_01.IsPlayerIn())
                {
                    flag_A_PlayerEntered_ExitTrigger01.SetStatus(LogicFlagStatus.Active);
                    stepA_Objects_ExitTrigger_01.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (stepA_Objects_ExitTrigger_01.OutStep == 900f) //Finish
            {
                stepA_Objects_ExitTrigger_01.SetEnabled(false);
                stepA_Objects_ExitTrigger_01.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region Fight02

            #region 1 Start
            if (stepA_FightInRegsGroup_02.outStep == 1)
            {
                stepA_FightInRegsGroup_02.StartIt();

                stepA_FightInRegsGroup_02.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepA_FightInRegsGroup_02.outStep == 1.1f)
            {
                stepA_FightInRegsGroup_02.RunIt();

                if (stepA_FightInRegsGroup_02.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    stepA_FightInRegsGroup_02.StartFinishing_OutStepIfNotFinishing();

                    goto fightInRegsGroup_A_02_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (stepA_FightInRegsGroup_02.outStep == 900f)
            {
                stepA_FightInRegsGroup_02.SetNeedsToBeFinished();
                stepA_FightInRegsGroup_02.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (stepA_FightInRegsGroup_02.outStep == 901f)
            {
                stepA_FightInRegsGroup_02.RunIt();

                if (stepA_FightInRegsGroup_02.status == LogicJobStatus.Finished)
                {
                    stepA_FightInRegsGroup_02.SetOutStep(1000f);
                    flag_A_Fight02_Finished.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

        fightInRegsGroup_A_02_End: ;

            #endregion

            #region ExitTrigger02

            #region 1 Start
            if (stepA_Objects_ExitTrigger_02.OutStep == 1) //Start
            {
                stepA_Objects_ExitTrigger_02.SetEnabled(true);
                stepA_Objects_ExitTrigger_02.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepA_Objects_ExitTrigger_02.OutStep == 1.1f) //Run
            {
                if (stepA_Objects_ExitTrigger_02.IsPlayerIn())
                {
                    flag_A_PlayerEntered_ExitTrigger02.SetStatus(LogicFlagStatus.Active);
                    stepA_Objects_ExitTrigger_02.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (stepA_Objects_ExitTrigger_02.OutStep == 900f) //Finish
            {
                stepA_Objects_ExitTrigger_02.SetEnabled(false);
                stepA_Objects_ExitTrigger_02.SetOutStep(1000f);
            }
            #endregion

            #endregion

            // B

            #region Ally

            #region 1 Start
            if (stepB_AllyChainJobGroup.outStep == 1)
            {
                stepB_AllyChainJobGroup.Init_SetNewGlobalLogicIndex(allyChainJobsGlobalLogicIndex);
                stepB_AllyChainJobGroup.StartIt();

                stepB_AllyChainJobGroup.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepB_AllyChainJobGroup.outStep == 1.1f)
            {
                stepB_AllyChainJobGroup.RunIt();
            }
            #endregion

            #region 900 Start finishing
            if (stepB_AllyChainJobGroup.outStep == 900f)
            {
                stepB_AllyChainJobGroup.SetNeedsToBeFinished_EvenStopMoving();

                stepB_AllyChainJobGroup.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (stepB_AllyChainJobGroup.outStep == 901f)
            {
                stepB_AllyChainJobGroup.RunIt();

                if (stepB_AllyChainJobGroup.status == LogicJobStatus.Finished)
                {
                    flag_B_AllyChainJob_Finished.SetStatus(LogicFlagStatus.Active);

                    stepB_AllyChainJobGroup.SetOutStep(1000);
                }
            }
            #endregion

            #endregion

            #region Fight01

            #region 1 Start
            if (stepB_FightInRegsGroup_01.outStep == 1)
            {
                stepB_FightInRegsGroup_01.StartIt();

                stepB_FightInRegsGroup_01.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepB_FightInRegsGroup_01.outStep == 1.1f)
            {
                stepB_FightInRegsGroup_01.RunIt();

                if (stepB_FightInRegsGroup_01.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    stepB_FightInRegsGroup_01.StartFinishing_OutStepIfNotFinishing();

                    goto fightInRegsGroup_B_01_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (stepB_FightInRegsGroup_01.outStep == 900f)
            {
                stepB_FightInRegsGroup_01.SetNeedsToBeFinished();
                stepB_FightInRegsGroup_01.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (stepB_FightInRegsGroup_01.outStep == 901f)
            {
                stepB_FightInRegsGroup_01.RunIt();

                if (stepB_FightInRegsGroup_01.status == LogicJobStatus.Finished)
                {
                    stepB_FightInRegsGroup_01.SetOutStep(1000f);
                    flag_B_Fight01_Finished.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

        fightInRegsGroup_B_01_End: ;

            #endregion

            #region FightMG

            #region 1 Start
            if (stepB_FightInRegsGroup_MG.outStep == 1)
            {
                stepB_FightInRegsGroup_MG.StartIt();

                stepB_FightInRegsGroup_MG.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepB_FightInRegsGroup_MG.outStep == 1.1f)
            {
                stepB_FightInRegsGroup_MG.RunIt();

                if (stepB_FightInRegsGroup_MG.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    stepB_FightInRegsGroup_MG.StartFinishing_OutStepIfNotFinishing();

                    goto fightInRegsGroup_B_MG_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (stepB_FightInRegsGroup_MG.outStep == 900f)
            {
                stepB_FightInRegsGroup_MG.SetNeedsToBeFinished();
                stepB_FightInRegsGroup_MG.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (stepB_FightInRegsGroup_MG.outStep == 901f)
            {
                stepB_FightInRegsGroup_MG.RunIt();

                if (stepB_FightInRegsGroup_MG.status == LogicJobStatus.Finished)
                {
                    stepB_FightInRegsGroup_MG.SetOutStep(1000f);
                    flag_B_FightMG_Finished.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

        fightInRegsGroup_B_MG_End: ;

            #endregion

            #region FightKh

            #region 1 Start
            if (stepB_FightInRegsGroup_Kh.outStep == 1)
            {
                stepB_FightInRegsGroup_Kh.StartIt();

                stepB_FightInRegsGroup_Kh.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepB_FightInRegsGroup_Kh.outStep == 1.1f)
            {
                stepB_FightInRegsGroup_Kh.RunIt();

                if (stepB_FightInRegsGroup_Kh.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    stepB_FightInRegsGroup_Kh.StartFinishing_OutStepIfNotFinishing();

                    goto fightInRegsGroup_B_Kh_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (stepB_FightInRegsGroup_Kh.outStep == 900f)
            {
                stepB_FightInRegsGroup_Kh.SetNeedsToBeFinished();
                stepB_FightInRegsGroup_Kh.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (stepB_FightInRegsGroup_Kh.outStep == 901f)
            {
                stepB_FightInRegsGroup_Kh.RunIt();

                if (stepB_FightInRegsGroup_Kh.status == LogicJobStatus.Finished)
                {
                    stepB_FightInRegsGroup_Kh.SetOutStep(1000f);
                    flag_B_FightKh_Finished.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

        fightInRegsGroup_B_Kh_End: ;

            #endregion

            #region stepB_Enemy_MachineGun_A

            #region 1 Start
            if (stepB_Enemy_MachineGun_A.outStep == 1)
            {
                stepB_Enemy_MachineGun_A.StartIt();

                stepB_Enemy_MachineGun_A.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepB_Enemy_MachineGun_A.outStep == 1.1f)
            {
                stepB_Enemy_MachineGun_A.RunIt();

                if (stepB_Enemy_MachineGun_A.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    stepB_Enemy_MachineGun_A.StartFinishing_OutStepIfNotFinishing();

                    goto stepB_Enemy_MachineGun_A_End;
                }
            }
            #endregion

            #region 900 Start finishing
            if (stepB_Enemy_MachineGun_A.outStep == 900f)
            {
                stepB_Enemy_MachineGun_A.SetNeedsToBeFinished();
                stepB_Enemy_MachineGun_A.SetOutStep(901);
            }
            #endregion

            #region 901 Finishing
            if (stepB_Enemy_MachineGun_A.outStep == 901f)
            {
                stepB_Enemy_MachineGun_A.RunIt();

                if (stepB_Enemy_MachineGun_A.status == LogicJobStatus.Finished)
                {
                    stepB_Enemy_MachineGun_A.SetOutStep(1000f);
                }
            }
            #endregion

        stepB_Enemy_MachineGun_A_End: ;

            #endregion

            #region ExitTrigger01

            #region 1 Start
            if (stepB_Objects_ExitTrigger_01.OutStep == 1) //Start
            {
                stepB_Objects_ExitTrigger_01.SetEnabled(true);
                stepB_Objects_ExitTrigger_01.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepB_Objects_ExitTrigger_01.OutStep == 1.1f) //Run
            {
                if (stepB_Objects_ExitTrigger_01.IsPlayerIn())
                {
                    flag_B_PlayerEntered_ExitTrigger01.SetStatus(LogicFlagStatus.Active);
                    stepB_Objects_ExitTrigger_01.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (stepB_Objects_ExitTrigger_01.OutStep == 900f) //Finish
            {
                stepB_Objects_ExitTrigger_01.SetEnabled(false);
                stepB_Objects_ExitTrigger_01.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region ExitTrigger02

            #region 1 Start
            if (stepB_Objects_ExitTrigger_02.OutStep == 1) //Start
            {
                stepB_Objects_ExitTrigger_02.SetEnabled(true);
                stepB_Objects_ExitTrigger_02.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepB_Objects_ExitTrigger_02.OutStep == 1.1f) //Run
            {
                if (stepB_Objects_ExitTrigger_02.IsPlayerIn())
                {
                    flag_B_PlayerEntered_ExitTrigger02.SetStatus(LogicFlagStatus.Active);
                    stepB_Objects_ExitTrigger_02.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (stepB_Objects_ExitTrigger_02.OutStep == 900f) //Finish
            {
                stepB_Objects_ExitTrigger_02.SetEnabled(false);
                stepB_Objects_ExitTrigger_02.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region ExitTriggerKh

            #region 1 Start
            if (stepB_Objects_ExitTrigger_Kh.OutStep == 1) //Start
            {
                stepB_Objects_ExitTrigger_Kh.SetEnabled(true);
                stepB_Objects_ExitTrigger_Kh.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepB_Objects_ExitTrigger_Kh.OutStep == 1.1f) //Run
            {
                if (stepB_Objects_ExitTrigger_Kh.IsPlayerIn())
                {
                    flag_B_PlayerEntered_ExitTriggerKh.SetStatus(LogicFlagStatus.Active);
                    stepB_Objects_ExitTrigger_Kh.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (stepB_Objects_ExitTrigger_Kh.OutStep == 900f) //Finish
            {
                stepB_Objects_ExitTrigger_Kh.SetEnabled(false);
                stepB_Objects_ExitTrigger_Kh.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region LogicTrigger_EndOfTheEnd

            #region 1 Start
            if (stepB_Objects_LogicTrigger_EndOfTheEnd.OutStep == 1) //Start
            {
                stepB_Objects_LogicTrigger_EndOfTheEnd.SetEnabled(true);
                stepB_Objects_LogicTrigger_EndOfTheEnd.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepB_Objects_LogicTrigger_EndOfTheEnd.OutStep == 1.1f) //Run
            {
                if (stepB_Objects_LogicTrigger_EndOfTheEnd.IsPlayerIn())
                {
                    flag_B_PlayerEntered_LogicTrigger_EndOfTheEnd.SetStatus(LogicFlagStatus.Active);
                    stepB_Objects_LogicTrigger_EndOfTheEnd.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (stepB_Objects_LogicTrigger_EndOfTheEnd.OutStep == 900f) //Finish
            {
                stepB_Objects_LogicTrigger_EndOfTheEnd.SetEnabled(false);
                stepB_Objects_LogicTrigger_EndOfTheEnd.SetOutStep(1000f);
            }
            #endregion

            #endregion

            // C

            #region Ally

            #region 1 Start
            if (stepC_AllyChainJobGroup.outStep == 1)
            {
                stepC_AllyChainJobGroup.Init_SetNewGlobalLogicIndex(allyChainJobsGlobalLogicIndex);
                stepC_AllyChainJobGroup.StartIt();

                stepC_AllyChainJobGroup.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepC_AllyChainJobGroup.outStep == 1.1f)
            {
                stepC_AllyChainJobGroup.RunIt();
            }
            #endregion

            #region 900 Start finishing
            if (stepC_AllyChainJobGroup.outStep == 900f)
            {
                stepC_AllyChainJobGroup.SetNeedsToBeFinished_EvenStopMoving();

                stepC_AllyChainJobGroup.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (stepC_AllyChainJobGroup.outStep == 901f)
            {
                stepC_AllyChainJobGroup.RunIt();

                if (stepC_AllyChainJobGroup.status == LogicJobStatus.Finished)
                {
                    stepC_AllyChainJobGroup.SetOutStep(1000);
                }
            }
            #endregion

            #endregion //<= NotDefault

            #region ExitTriggerAllies

            #region 1 Start
            if (stepC_Objects_ExitTrigger_Allies.OutStep == 1) //Start
            {
                stepC_Objects_ExitTrigger_Allies.SetEnabled(true);
                stepC_Objects_ExitTrigger_Allies.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepC_Objects_ExitTrigger_Allies.OutStep == 1.1f) //Run
            {
                if (stepC_Objects_ExitTrigger_Allies.IsGameObjectIn(ally01)
                    && stepC_Objects_ExitTrigger_Allies.IsGameObjectIn(ally02))
                {
                    flag_C_AlliesEntered_ExitTriggerAllies.SetStatus(LogicFlagStatus.Active);
                    stepC_Objects_ExitTrigger_Allies.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (stepC_Objects_ExitTrigger_Allies.OutStep == 900f) //Finish
            {
                stepC_Objects_ExitTrigger_Allies.SetEnabled(false);
                stepC_Objects_ExitTrigger_Allies.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region StartTrigger_A

            #region 1 Start
            if (stepC_Objects_StartTrigger_A.OutStep == 1) //Start
            {
                stepC_Objects_StartTrigger_A.SetEnabled(true);
                stepC_Objects_StartTrigger_A.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepC_Objects_StartTrigger_A.OutStep == 1.1f) //Run
            {
                if (stepC_Objects_StartTrigger_A.IsSomethingIn())
                {
                    flag_C_PlayerEntered_StartTrigger_A.SetStatus(LogicFlagStatus.Active);
                    stepC_Objects_StartTrigger_A.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (stepC_Objects_StartTrigger_A.OutStep == 900f) //Finish
            {
                stepC_Objects_StartTrigger_A.SetEnabled(false);
                stepC_Objects_StartTrigger_A.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region Fight A

            #region 1 Start
            if (stepC_FightInRegsGroup_A.outStep == 1)
            {
                stepC_FightInRegsGroup_A.StartIt();

                stepC_FightInRegsGroup_A.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepC_FightInRegsGroup_A.outStep == 1.1f)
            {
                stepC_FightInRegsGroup_A.RunIt();

                if (stepC_FightInRegsGroup_A.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    stepC_FightInRegsGroup_A.StartFinishing_OutStepIfNotFinishing();

                    goto fightInRegsGroup_C_A_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (stepC_FightInRegsGroup_A.outStep == 900f)
            {
                stepC_FightInRegsGroup_A.SetNeedsToBeFinished();
                stepC_FightInRegsGroup_A.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (stepC_FightInRegsGroup_A.outStep == 901f)
            {
                stepC_FightInRegsGroup_A.RunIt();

                if (stepC_FightInRegsGroup_A.status == LogicJobStatus.Finished)
                {
                    stepC_FightInRegsGroup_A.SetOutStep(1000f);
                    flag_C_FightA_Finished.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

        fightInRegsGroup_C_A_End: ;
            #endregion

            #region StartTrigger_B

            #region 1 Start
            if (stepC_Objects_StartTrigger_B.OutStep == 1) //Start
            {
                stepC_Objects_StartTrigger_B.SetEnabled(true);
                stepC_Objects_StartTrigger_B.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepC_Objects_StartTrigger_B.OutStep == 1.1f) //Run
            {
                if (stepC_Objects_StartTrigger_B.IsSomethingIn())
                {
                    flag_C_PlayerEntered_StartTrigger_B.SetStatus(LogicFlagStatus.Active);
                    stepC_Objects_StartTrigger_B.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (stepC_Objects_StartTrigger_B.OutStep == 900f) //Finish
            {
                stepC_Objects_StartTrigger_B.SetEnabled(false);
                stepC_Objects_StartTrigger_B.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region Fight B

            #region 1 Start
            if (stepC_FightInRegsGroup_B.outStep == 1)
            {
                stepC_FightInRegsGroup_B.StartIt();

                stepC_FightInRegsGroup_B.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepC_FightInRegsGroup_B.outStep == 1.1f)
            {
                stepC_FightInRegsGroup_B.RunIt();

                if (stepC_FightInRegsGroup_B.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    stepC_FightInRegsGroup_B.StartFinishing_OutStepIfNotFinishing();

                    goto fightInRegsGroup_C_B_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (stepC_FightInRegsGroup_B.outStep == 900f)
            {
                stepC_FightInRegsGroup_B.SetNeedsToBeFinished();
                stepC_FightInRegsGroup_B.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (stepC_FightInRegsGroup_B.outStep == 901f)
            {
                stepC_FightInRegsGroup_B.RunIt();

                if (stepC_FightInRegsGroup_B.status == LogicJobStatus.Finished)
                {
                    stepC_FightInRegsGroup_B.SetOutStep(1000f);
                    flag_C_FightB_Finished.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

        fightInRegsGroup_C_B_End: ;
            #endregion

            #region StepExitTrigger

            #region 1 Start
            if (stepC_Objects_StepExitTrigger.OutStep == 1) //Start
            {
                stepC_Objects_StepExitTrigger.SetEnabled(true);
                stepC_Objects_StepExitTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepC_Objects_StepExitTrigger.OutStep == 1.1f) //Run
            {
                if (stepC_Objects_StepExitTrigger.IsSomethingIn())
                {
                    flag_C_PlayerEntered_StepExitTrigger.SetStatus(LogicFlagStatus.Active);
                    stepC_Objects_StepExitTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (stepC_Objects_StepExitTrigger.OutStep == 900f) //Finish
            {
                stepC_Objects_StepExitTrigger.SetEnabled(false);
                stepC_Objects_StepExitTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            // D

            #region Fight A

            #region 1 Start
            if (stepD_FightInRegsGroup_A.outStep == 1)
            {
                stepD_FightInRegsGroup_A.StartIt();

                stepD_FightInRegsGroup_A.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepD_FightInRegsGroup_A.outStep == 1.1f)
            {
                stepD_FightInRegsGroup_A.RunIt();

                if (stepD_FightInRegsGroup_A.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    stepD_FightInRegsGroup_A.StartFinishing_OutStepIfNotFinishing();

                    goto fightInRegsGroup_D_A_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (stepD_FightInRegsGroup_A.outStep == 900f)
            {
                stepD_FightInRegsGroup_A.SetNeedsToBeFinished();
                stepD_FightInRegsGroup_A.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (stepD_FightInRegsGroup_A.outStep == 901f)
            {
                stepD_FightInRegsGroup_A.RunIt();

                if (stepD_FightInRegsGroup_A.status == LogicJobStatus.Finished)
                {
                    stepD_FightInRegsGroup_A.SetOutStep(1000f);
                    flag_D_FightA_Finished.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

        fightInRegsGroup_D_A_End: ;
            #endregion

            #region Fight B

            #region 1 Start
            if (stepD_FightInRegsGroup_B.outStep == 1)
            {
                stepD_FightInRegsGroup_B.StartIt();

                stepD_FightInRegsGroup_B.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepD_FightInRegsGroup_B.outStep == 1.1f)
            {
                stepD_FightInRegsGroup_B.RunIt();

                if (stepD_FightInRegsGroup_B.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    stepD_FightInRegsGroup_B.StartFinishing_OutStepIfNotFinishing();

                    goto fightInRegsGroup_D_B_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (stepD_FightInRegsGroup_B.outStep == 900f)
            {
                stepD_FightInRegsGroup_B.SetNeedsToBeFinished();
                stepD_FightInRegsGroup_B.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (stepD_FightInRegsGroup_B.outStep == 901f)
            {
                stepD_FightInRegsGroup_B.RunIt();

                if (stepD_FightInRegsGroup_B.status == LogicJobStatus.Finished)
                {
                    stepD_FightInRegsGroup_B.SetOutStep(1000f);
                    flag_D_FightB_Finished.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

        fightInRegsGroup_D_B_End: ;
            #endregion

            #region stepD_Enemy_MachineGun_B

            #region 1 Start
            if (stepD_Enemy_MachineGun_B.outStep == 1)
            {
                stepD_Enemy_MachineGun_B.StartIt();

                stepD_Enemy_MachineGun_B.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepD_Enemy_MachineGun_B.outStep == 1.1f)
            {
                stepD_Enemy_MachineGun_B.RunIt();

                if (stepD_Enemy_MachineGun_B.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    stepD_Enemy_MachineGun_B.StartFinishing_OutStepIfNotFinishing();

                    goto stepD_Enemy_MachineGun_B_End;
                }
            }
            #endregion

            #region 900 Start finishing
            if (stepD_Enemy_MachineGun_B.outStep == 900f)
            {
                stepD_Enemy_MachineGun_B.SetNeedsToBeFinished();
                stepD_Enemy_MachineGun_B.SetOutStep(901);
            }
            #endregion

            #region 901 Finishing
            if (stepD_Enemy_MachineGun_B.outStep == 901f)
            {
                stepD_Enemy_MachineGun_B.RunIt();

                if (stepD_Enemy_MachineGun_B.status == LogicJobStatus.Finished)
                {
                    stepD_Enemy_MachineGun_B.SetOutStep(1000f);
                }
            }
            #endregion

        stepD_Enemy_MachineGun_B_End: ;

            #endregion

            #region KillMGArea_A

            #region 1 Start
            if (stepD_Objects_KillMGArea_A.OutStep == 1) //Start
            {
                stepD_Objects_KillMGArea_A.SetEnabled(true);
                stepD_Objects_KillMGArea_A.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepD_Objects_KillMGArea_A.OutStep == 1.1f) //Run
            {
                if (stepD_Objects_KillMGArea_A.IsPlayerIn())
                {
                    if (!stepD_Objects_MGAlliesArea.IsInsideObjsCountEqualOrBiggerThanValue(3))
                    {
                        flag_D_ShouldKillMGSold.SetStatus(LogicFlagStatus.Active);

                        stepD_Objects_MGAlliesArea.StartFinishing_OutStepIfNotFishining();
                        stepD_Objects_KillMGArea_A.StartFinishing_OutStepIfNotFishining();
                        stepD_Objects_KillMGArea_B.StartFinishing_OutStepIfNotFishining();
                    }
                }
            }
            #endregion

            #region 900 Finish
            if (stepD_Objects_KillMGArea_A.OutStep == 900f) //Finish
            {
                stepD_Objects_KillMGArea_A.SetEnabled(false);
                stepD_Objects_KillMGArea_A.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region KillMGArea_B

            #region 1 Start
            if (stepD_Objects_KillMGArea_B.OutStep == 1) //Start
            {
                stepD_Objects_KillMGArea_B.SetEnabled(true);
                stepD_Objects_KillMGArea_B.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepD_Objects_KillMGArea_B.OutStep == 1.1f) //Run
            {
                if (stepD_Objects_KillMGArea_B.IsPlayerIn())
                {
                    if (!stepD_Objects_MGAlliesArea.IsInsideObjsCountEqualOrBiggerThanValue(3))
                    {
                        flag_D_ShouldKillMGSold.SetStatus(LogicFlagStatus.Active);

                        stepD_Objects_MGAlliesArea.StartFinishing_OutStepIfNotFishining();
                        stepD_Objects_KillMGArea_A.StartFinishing_OutStepIfNotFishining();
                        stepD_Objects_KillMGArea_B.StartFinishing_OutStepIfNotFishining();
                    }
                }
            }
            #endregion

            #region 900 Finish
            if (stepD_Objects_KillMGArea_B.OutStep == 900f) //Finish
            {
                stepD_Objects_KillMGArea_B.SetEnabled(false);
                stepD_Objects_KillMGArea_B.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region MGAlliesArea

            #region 1 Start
            if (stepD_Objects_MGAlliesArea.OutStep == 1) //Start
            {
                stepD_Objects_MGAlliesArea.SetEnabled(true);
                stepD_Objects_MGAlliesArea.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepD_Objects_MGAlliesArea.OutStep == 1.1f) //Run
            {

            }
            #endregion

            #region 900 Finish
            if (stepD_Objects_MGAlliesArea.OutStep == 900f) //Finish
            {
                stepD_Objects_MGAlliesArea.SetEnabled(false);
                stepD_Objects_MGAlliesArea.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region ExitTrigger

            #region 1 Start
            if (stepD_Objects_ExitTrigger.OutStep == 1) //Start
            {
                stepD_Objects_ExitTrigger.SetEnabled(true);
                stepD_Objects_ExitTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepD_Objects_ExitTrigger.OutStep == 1.1f) //Run
            {
                if (stepD_Objects_ExitTrigger.IsPlayerIn())
                {
                    flag_D_PlayerEntered_ExitTrigger.SetStatus(LogicFlagStatus.Active);
                    stepD_Objects_ExitTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (stepD_Objects_ExitTrigger.OutStep == 900f) //Finish
            {
                stepD_Objects_ExitTrigger.SetEnabled(false);
                stepD_Objects_ExitTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            // E

            #region Fight A

            #region 1 Start
            if (stepE_FightInRegsGroup_A.outStep == 1)
            {
                stepE_FightInRegsGroup_A.StartIt();

                stepE_FightInRegsGroup_A.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepE_FightInRegsGroup_A.outStep == 1.1f)
            {
                stepE_FightInRegsGroup_A.RunIt();

                if (stepE_FightInRegsGroup_A.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    stepE_FightInRegsGroup_A.StartFinishing_OutStepIfNotFinishing();

                    goto fightInRegsGroup_E_A_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (stepE_FightInRegsGroup_A.outStep == 900f)
            {
                stepE_FightInRegsGroup_A.SetNeedsToBeFinished();
                stepE_FightInRegsGroup_A.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (stepE_FightInRegsGroup_A.outStep == 901f)
            {
                stepE_FightInRegsGroup_A.RunIt();

                if (stepE_FightInRegsGroup_A.status == LogicJobStatus.Finished)
                {
                    stepE_FightInRegsGroup_A.SetOutStep(1000f);
                    flag_E_FightA_Finished.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

        fightInRegsGroup_E_A_End: ;
            #endregion

            #region ExitTrigger

            #region 1 Start
            if (stepE_Objects_ExitTrigger.OutStep == 1) //Start
            {
                stepE_Objects_ExitTrigger.SetEnabled(true);
                stepE_Objects_ExitTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepE_Objects_ExitTrigger.OutStep == 1.1f) //Run
            {
                if (stepE_Objects_ExitTrigger.IsPlayerIn())
                {
                    flag_E_PlayerEntered_ExitTrigger.SetStatus(LogicFlagStatus.Active);
                    stepE_Objects_ExitTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (stepE_Objects_ExitTrigger.OutStep == 900f) //Finish
            {
                stepE_Objects_ExitTrigger.SetEnabled(false);
                stepE_Objects_ExitTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region StartTrigger

            #region 1 Start
            if (stepE_Objects_StartTrigger.OutStep == 1) //Start
            {
                stepE_Objects_StartTrigger.SetEnabled(true);
                stepE_Objects_StartTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepE_Objects_StartTrigger.OutStep == 1.1f) //Run
            {
                if (stepE_Objects_StartTrigger.IsPlayerIn())
                {
                    flag_E_PlayerEntered_StartTrigger.SetStatus(LogicFlagStatus.Active);
                    stepE_Objects_StartTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (stepE_Objects_StartTrigger.OutStep == 900f) //Finish
            {
                stepE_Objects_StartTrigger.SetEnabled(false);
                stepE_Objects_StartTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            // F

            #region Fight A

            #region 1 Start
            if (stepF_FightInRegsGroup_A.outStep == 1)
            {
                stepF_FightInRegsGroup_A.StartIt();

                stepF_FightInRegsGroup_A.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepF_FightInRegsGroup_A.outStep == 1.1f)
            {
                stepF_FightInRegsGroup_A.RunIt();

                if (stepF_FightInRegsGroup_A.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    stepF_FightInRegsGroup_A.StartFinishing_OutStepIfNotFinishing();

                    goto fightInRegsGroup_F_A_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (stepF_FightInRegsGroup_A.outStep == 900f)
            {
                stepF_FightInRegsGroup_A.SetNeedsToBeFinished();
                stepF_FightInRegsGroup_A.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (stepF_FightInRegsGroup_A.outStep == 901f)
            {
                stepF_FightInRegsGroup_A.RunIt();

                if (stepF_FightInRegsGroup_A.status == LogicJobStatus.Finished)
                {
                    stepF_FightInRegsGroup_A.SetOutStep(1000f);
                    flag_F_FightA_Finished.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

        fightInRegsGroup_F_A_End: ;
            #endregion

            #region ExitTrigger

            #region 1 Start
            if (stepF_Objects_ExitTrigger.OutStep == 1) //Start
            {
                stepF_Objects_ExitTrigger.SetEnabled(true);
                stepF_Objects_ExitTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepF_Objects_ExitTrigger.OutStep == 1.1f) //Run
            {
                if (stepF_Objects_ExitTrigger.IsPlayerIn())
                {
                    flag_F_PlayerEntered_ExitTrigger.SetStatus(LogicFlagStatus.Active);
                    stepF_Objects_ExitTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (stepF_Objects_ExitTrigger.OutStep == 900f) //Finish
            {
                stepF_Objects_ExitTrigger.SetEnabled(false);
                stepF_Objects_ExitTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region StartTrigger

            #region 1 Start
            if (stepF_Objects_StartTrigger.OutStep == 1) //Start
            {
                stepF_Objects_StartTrigger.SetEnabled(true);
                stepF_Objects_StartTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepF_Objects_StartTrigger.OutStep == 1.1f) //Run
            {
                if (stepF_Objects_StartTrigger.IsPlayerIn())
                {
                    flag_F_PlayerEntered_StartTrigger.SetStatus(LogicFlagStatus.Active);
                    stepF_Objects_StartTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (stepF_Objects_StartTrigger.OutStep == 900f) //Finish
            {
                stepF_Objects_StartTrigger.SetEnabled(false);
                stepF_Objects_StartTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            // G

            #region Ally

            #region 1 Start
            if (stepG_AllyChainJobGroup.outStep == 1)
            {
                stepG_AllyChainJobGroup.Init_SetNewGlobalLogicIndex(allyChainJobsGlobalLogicIndex);
                stepG_AllyChainJobGroup.StartIt();

                stepG_AllyChainJobGroup.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepG_AllyChainJobGroup.outStep == 1.1f)
            {
                stepG_AllyChainJobGroup.RunIt();
            }
            #endregion

            #region 900 Start finishing
            if (stepG_AllyChainJobGroup.outStep == 900f)
            {
                stepG_AllyChainJobGroup.SetNeedsToBeFinished_EvenStopMoving();

                stepG_AllyChainJobGroup.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (stepG_AllyChainJobGroup.outStep == 901f)
            {
                stepG_AllyChainJobGroup.RunIt();

                if (stepG_AllyChainJobGroup.status == LogicJobStatus.Finished)
                {
                    flag_G_AllyChainJob_Finished.SetStatus(LogicFlagStatus.Active);

                    stepG_AllyChainJobGroup.SetOutStep(1000);
                }
            }
            #endregion

            #endregion

            #region Fight A

            #region 1 Start
            if (stepG_FightInRegsGroup_A.outStep == 1)
            {
                stepG_FightInRegsGroup_A.StartIt();

                stepG_FightInRegsGroup_A.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepG_FightInRegsGroup_A.outStep == 1.1f)
            {
                stepG_FightInRegsGroup_A.RunIt();

                if (stepG_FightInRegsGroup_A.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    stepG_FightInRegsGroup_A.StartFinishing_OutStepIfNotFinishing();

                    goto fightInRegsGroup_G_A_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (stepG_FightInRegsGroup_A.outStep == 900f)
            {
                stepG_FightInRegsGroup_A.SetNeedsToBeFinished();
                stepG_FightInRegsGroup_A.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (stepG_FightInRegsGroup_A.outStep == 901f)
            {
                stepG_FightInRegsGroup_A.RunIt();

                if (stepG_FightInRegsGroup_A.status == LogicJobStatus.Finished)
                {
                    stepG_FightInRegsGroup_A.SetOutStep(1000f);
                    flag_G_FightA_Finished.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

        fightInRegsGroup_G_A_End: ;
            #endregion

            #region Fight B

            #region 1 Start
            if (stepG_FightInRegsGroup_B.outStep == 1)
            {
                stepG_FightInRegsGroup_B.StartIt();

                stepG_FightInRegsGroup_B.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepG_FightInRegsGroup_B.outStep == 1.1f)
            {
                stepG_FightInRegsGroup_B.RunIt();

                if (stepG_FightInRegsGroup_B.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    stepG_FightInRegsGroup_B.StartFinishing_OutStepIfNotFinishing();

                    goto fightInRegsGroup_G_B_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (stepG_FightInRegsGroup_B.outStep == 900f)
            {
                stepG_FightInRegsGroup_B.SetNeedsToBeFinished();
                stepG_FightInRegsGroup_B.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (stepG_FightInRegsGroup_B.outStep == 901f)
            {
                stepG_FightInRegsGroup_B.RunIt();

                if (stepG_FightInRegsGroup_B.status == LogicJobStatus.Finished)
                {
                    stepG_FightInRegsGroup_B.SetOutStep(1000f);
                }
            }
            #endregion

        fightInRegsGroup_G_B_End: ;
            #endregion

            #region MachineGun_B_01

            #region 1 Start
            if (stepG_Enemy_MachineGun_B_01.outStep == 1)
            {
                stepG_Enemy_MachineGun_B_01.StartIt();

                stepG_Enemy_MachineGun_B_01.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepG_Enemy_MachineGun_B_01.outStep == 1.1f)
            {
                stepG_Enemy_MachineGun_B_01.RunIt();

                if (stepG_Enemy_MachineGun_B_01.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    stepG_Enemy_MachineGun_B_01.StartFinishing_OutStepIfNotFinishing();

                    goto stepG_Enemy_MachineGun_B_01_End;
                }
            }
            #endregion

            #region 900 Start finishing
            if (stepG_Enemy_MachineGun_B_01.outStep == 900f)
            {
                stepG_Enemy_MachineGun_B_01.SetNeedsToBeFinished();
                stepG_Enemy_MachineGun_B_01.SetOutStep(901);
            }
            #endregion

            #region 901 Finishing
            if (stepG_Enemy_MachineGun_B_01.outStep == 901f)
            {
                stepG_Enemy_MachineGun_B_01.RunIt();

                if (stepG_Enemy_MachineGun_B_01.status == LogicJobStatus.Finished)
                {
                    stepG_Enemy_MachineGun_B_01.SetOutStep(1000f);
                }
            }
            #endregion

        stepG_Enemy_MachineGun_B_01_End: ;

            #endregion

            #region MachineGun_B_02

            #region 1 Start
            if (stepG_Enemy_MachineGun_B_02.outStep == 1)
            {
                stepG_Enemy_MachineGun_B_02.StartIt();

                stepG_Enemy_MachineGun_B_02.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepG_Enemy_MachineGun_B_02.outStep == 1.1f)
            {
                stepG_Enemy_MachineGun_B_02.RunIt();

                if (stepG_Enemy_MachineGun_B_02.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    stepG_Enemy_MachineGun_B_02.StartFinishing_OutStepIfNotFinishing();

                    goto stepG_Enemy_MachineGun_B_02_End;
                }
            }
            #endregion

            #region 900 Start finishing
            if (stepG_Enemy_MachineGun_B_02.outStep == 900f)
            {
                stepG_Enemy_MachineGun_B_02.SetNeedsToBeFinished();
                stepG_Enemy_MachineGun_B_02.SetOutStep(901);
            }
            #endregion

            #region 901 Finishing
            if (stepG_Enemy_MachineGun_B_02.outStep == 901f)
            {
                stepG_Enemy_MachineGun_B_02.RunIt();

                if (stepG_Enemy_MachineGun_B_02.status == LogicJobStatus.Finished)
                {
                    stepG_Enemy_MachineGun_B_02.SetOutStep(1000f);
                }
            }
            #endregion

        stepG_Enemy_MachineGun_B_02_End: ;

            #endregion

            #region ExitTrigger

            #region 1 Start
            if (stepG_Objects_ExitTrigger.OutStep == 1) //Start
            {
                stepG_Objects_ExitTrigger.SetEnabled(true);
                stepG_Objects_ExitTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepG_Objects_ExitTrigger.OutStep == 1.1f) //Run
            {
                if (stepG_Objects_ExitTrigger.IsPlayerIn())
                {
                    flag_G_PlayerEntered_ExitTrigger.SetStatus(LogicFlagStatus.Active);
                    stepG_Objects_ExitTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (stepG_Objects_ExitTrigger.OutStep == 900f) //Finish
            {
                stepG_Objects_ExitTrigger.SetEnabled(false);
                stepG_Objects_ExitTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region StartTrigger

            #region 1 Start
            if (stepG_Objects_StartTrigger.OutStep == 1) //Start
            {
                stepG_Objects_StartTrigger.SetEnabled(true);
                stepG_Objects_StartTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepG_Objects_StartTrigger.OutStep == 1.1f) //Run
            {
                if (stepG_Objects_StartTrigger.IsPlayerIn())
                {
                    flag_G_PlayerEntered_StartTrigger.SetStatus(LogicFlagStatus.Active);
                    stepG_Objects_StartTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (stepG_Objects_StartTrigger.OutStep == 900f) //Finish
            {
                stepG_Objects_StartTrigger.SetEnabled(false);
                stepG_Objects_StartTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region StopCreatingFightATrigger

            #region 1 Start
            if (stepG_Objects_StopCreatingFightATrigger.OutStep == 1) //Start
            {
                stepG_Objects_StopCreatingFightATrigger.SetEnabled(true);
                stepG_Objects_StopCreatingFightATrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepG_Objects_StopCreatingFightATrigger.OutStep == 1.1f) //Run
            {
                if (stepG_Objects_StopCreatingFightATrigger.IsPlayerIn())
                {
                    flag_G_PlayerEntered_StopCreatingFightATrigger.SetStatus(LogicFlagStatus.Active);
                    stepG_Objects_StopCreatingFightATrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (stepG_Objects_StopCreatingFightATrigger.OutStep == 900f) //Finish
            {
                stepG_Objects_StopCreatingFightATrigger.SetEnabled(false);
                stepG_Objects_StopCreatingFightATrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region KucheyeChapTrigger

            #region 1 Start
            if (stepG_Objects_KucheyeChapTrigger.OutStep == 1) //Start
            {
                stepG_Objects_KucheyeChapTrigger.SetEnabled(true);
                stepG_Objects_KucheyeChapTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepG_Objects_KucheyeChapTrigger.OutStep == 1.1f) //Run
            {
                if (stepG_Objects_KucheyeChapTrigger.IsPlayerIn())
                {
                    flag_G_PlayerEntered_KucheyeChapTrigger.SetStatus(LogicFlagStatus.Active);
                    stepG_Objects_KucheyeChapTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (stepG_Objects_KucheyeChapTrigger.OutStep == 900f) //Finish
            {
                stepG_Objects_KucheyeChapTrigger.SetEnabled(false);
                stepG_Objects_KucheyeChapTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion
        }
    }

    //

    void A_PlaceAlly()
    {
        stepA_Objects_StartPoint_Ally01.PlaceCharacterOnIt(ally01);
    }

    bool B_IsOneOfDatEnemiesDead()
    {
        if (stepB_Sold_MG == null)
            return true;

        if (stepB_Sold_MG.IsDead)
            return true;

        if (stepB_Sold_A == null)
            return true;

        if (stepB_Sold_A.IsDead)
            return true;

        if (stepB_Sold_B == null)
            return true;

        if (stepB_Sold_B.IsDead)
            return true;

        return false;

    }

    void G_PlaceAllies()
    {
        stepG_Objects_StartPoint_Ally01.PlaceCharacterOnIt(ally01);
        stepG_Objects_StartPoint_Ally02.PlaceCharacterOnIt(ally02);
    }

    //

    public override void LoadCheckPoint(float _levelStep)
    {
        base.LoadCheckPoint(_levelStep);

        #region B
        if (levelStep == 2)
        {
            Load_B_PlaceSolds();
            return;
        }
        #endregion

        #region C
        if (levelStep == 3)
        {
            Load_C_RemoveSoldsAndBoshkes();
            Load_C_PlaceSolds();
            return;
        }
        #endregion

        #region D
        if (levelStep == 4)
        {
            Load_C_RemoveSoldsAndBoshkes();
            Load_D_PlaceSolds();
            return;
        }
        #endregion

        #region E
        if (levelStep == 5)
        {
            Load_C_RemoveSoldsAndBoshkes();
            Load_E_PlaceSolds();
            return;
        }
        #endregion

        #region F
        if (levelStep == 6)
        {
            Load_C_RemoveSoldsAndBoshkes();
            Load_F_PlaceSolds();
            return;
        }
        #endregion

        #region G
        if (levelStep == 7)
        {
            Load_C_RemoveSoldsAndBoshkes();
            Load_G_PlaceSolds();
            return;
        }
        #endregion

    }

    //

    void Load_B_PlaceSolds()
    {
        stepB_Objects_StartPoint_Player.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);
        stepB_Objects_StartPoint_Ally01.PlaceCharacterOnIt(ally01);
    }

    void Load_C_PlaceSolds()
    {
        stepC_Objects_StartPoint_Player.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);
        stepC_Objects_StartPoint_Ally01.PlaceCharacterOnIt(ally01);
        stepC_Objects_StartPoint_Ally02.PlaceCharacterOnIt(ally02);
    }
    void Load_C_RemoveSoldsAndBoshkes()
    {
        Destroy(stepB_Boshkeh_A);
        Destroy(stepB_Boshkeh_B);

        for (int i = 0; i < stepB_AllInitialSolds.Length; i++)
        {
            Destroy(stepB_AllInitialSolds[i]);
        }
    }

    void Load_D_PlaceSolds()
    {
        stepD_Objects_StartPoint_Player.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);
    }

    void Load_E_PlaceSolds()
    {
        stepE_Objects_StartPoint_Player.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);
    }

    void Load_F_PlaceSolds()
    {
        stepF_Objects_StartPoint_Player.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);
    }

    void Load_G_PlaceSolds()
    {
        stepG_Objects_StartPoint_Player.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);
    }

    void PlayFirstMusic()
    {
        MusicController.Instance.PlayMusic(MusicSong.Action_A, 0);
    }
}
