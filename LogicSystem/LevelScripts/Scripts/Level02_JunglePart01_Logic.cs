using UnityEngine;
using System.Collections;

public class Level02_JunglePart01_Logic : LevelLogic
{
    public CutsceneController firstCutscene;
    public CutsceneController snipeCutscene;

    int allyChainJobsGlobalLogicIndex = -1;

    public GameObject ally01;
    public GameObject ally02;

    public LogicVoiceCollection logVoiceCollection_Ally01;
    public LogicVoiceCollection logVoiceCollection_Ally02;

    public MapLogicJob_ChainJobsGroup chainJobGroup_01_JungleStart;
    public MapLogicJob_FightInRegsGroup fightInRegsGroup_01_JungleStart01;
    public LogicTrigger stepA_Objects_ExitTrigger_JungleStart01;
    public MapLogicJob_FightInRegsGroup fightInRegsGroup_01_JungleStart02;
    public LogicTrigger stepA_Objects_ExitTrigger_JungleStart02;
    public LogicDieTrigger stepA_Objects_LogicDieTrigger_01;
    public LogicDieTrigger stepA_Objects_LogicDieTrigger_02;
    public Transform stepA_HUD_DatMinimapTr;
    //

    public StartPoint stepA1_Objects_StartPoint_Player;
    public StartPoint stepA1_Objects_StartPoint_Ally01;
    public StartPoint stepA1_Objects_StartPoint_Ally02;

    public StartPoint stepB_Objects_StartPoint_Player;
    public StartPoint stepB_Objects_StartPoint_Ally01;
    public StartPoint stepB_Objects_StartPoint_Ally02;

    public LogicTrigger stepB_Objects_LogicTrigger_Bandits;
    public MapLogicJob_ChainJobsGroup stepB_Ally_ChainJobGroup_Start;
    public MapLogicJob_FightInRegsGroup stepB_Enemy_FightInRegsGroup_Bandits;
    public LogicTrigger stepB_Objects_LogicTrigger_Camp;
    public MapLogicJob_MachineGun stepB_Enemy_MachineGun_A;
    public MapLogicJob_MachineGun stepB_Enemy_MachineGun_B;
    public MapLogicJob_FightInRegsGroup stepB_Enemy_FightInRegsGroup_Camp;
    public GameObject[] stepB_Objects_TowerExplodableBarrels;
    public GameObject stepB_Objects_Tower;
    public AudioInfo stepB_Objects_TowerSound;
    public AudioInfo stepB_Objects_TowerExplosionSound;
    public ParticleSystem stepB_Objects_TowerParticle;
    public Collider stepB_Objects_Tower_RemovableCollider;
    public GameObject stepB_Objects_CampDieTrigger;
    public LogicDieTrigger stepB_Objects_Camp_End_DieTrigger;
    public ExecutionArea stepB_Objects_CampExecutionArea;
    public LogicTrigger stepB_Objects_LogicTrigger_CampExit;

    public float stepB_DelayCounterToStartBorjakVoice = 5;

    public Transform stepB_HUD_BeforeCampMinimapTr;
    public Transform stepB_HUD_CampMinimapTr;
    public Transform stepB_HUD_BorjakObj3DTr;
    public Transform stepB1_HUD_BeforeHiddenMinimapTr;

    //

    public StartPoint stepB1_Objects_StartPoint_Player;
    public StartPoint stepB1_Objects_StartPoint_Ally01;
    public StartPoint stepB1_Objects_StartPoint_Ally02;

    public StartPoint stepC_Objects_StartPoint_Player;
    public StartPoint stepC_Objects_StartPoint_Ally01;
    public StartPoint stepC_Objects_StartPoint_Ally02;
    public MapLogicJob_ChainJobsGroup stepC_Ally_ChainJobGroup_Start;
    public LogicTrigger stepC_Objects_LogicTrigger_Hidden;
    public MapLogicJob_FightInRegsGroup stepC_Enemy_FightInRegsGroup_Hidden;
    public LogicDieTrigger stepC_Objects_LogicDieTrigger_01;
    public LogicTrigger stepC_Objects_LogicTrigger_Hidden_Part02;
    public MapLogicJob_FightInRegsGroup stepC_Enemy_FightInRegsGroup_Hidden_Part02;
    public LogicDieTrigger stepC_Objects_LogicDieTrigger_02;
    public LogicTrigger stepC_Objects_ExitTrigger_Hidden_Part02;
    public LogicTrigger stepC_Objects_LogicTrigger_SnipeHillCutscene;
    public Transform stepC_HUD_HiddenMinimapTr;

    //

    public StartPoint stepD_Objects_StartPoint_Player;
    public StartPoint stepD_Objects_StartPoint_Ally01;
    public StartPoint stepD_Objects_StartPoint_Ally02;
    public AudioInfo stepD_Objects_AudioInfo_CrowdShout;
    public int stepD_MinNumOfNearEnemies;
    public LogicTrigger stepD_Objects_LogicTrigger_NearEnemies;
    public float stepD_DieTriggerFrontDelay = 50;
    public LogicDieTrigger stepD_Objects_LogicDieTrigger_Front;
    public int stepD_MaxNumOfFrontEnemiesToStartDieTrigger = 2;
    public LogicTrigger stepD_Objects_ExitTrigger_JeloRush;
    public LogicTrigger stepD_Objects_ExitTrigger_PoshtRush;
    public MapLogicJob_ChainJobsGroup stepD_Ally_ChainJobGroup_Start;
    public MapLogicJob_FightInRegsGroup stepD_Enemy_FightInRegsGroup_Kolli;
    public MapLogicJob_FightInRegsGroup stepD_Enemy_FightInRegsGroup_Jelo;
    public MapLogicJob_FightInRegsGroup stepD_Enemy_FightInRegsGroup_Posht;
    public ExecutionArea stepD_Objects_ExecutionArea_PoshtRush;
    public LogicTrigger stepD_Objects_Trigger_PoshtRush_StartKillingRemainingEnemies;
    public LogicDieTrigger stepD_Objects_LogicDieTrigger_Back;
    public Transform stepD_HUD_SnipeZoneMinimapTr;
    public Transform stepD_HUD_SniperRifleObj3DTr;
    public SkinnedMeshRenderer stepD_SnipeItemRendererToHideAndShowInCutscene;

    public StartPoint stepE_Objects_StartPoint_Player;
    public StartPoint stepE_Objects_StartPoint_Ally01;
    public StartPoint stepE_Objects_StartPoint_Ally02;
    public MapLogicJob_ChainJobsGroup stepE_Ally_ChainJobGroup;
    public LogicTrigger stepE_Objects_LogicTrigger_EnemyStart_A;
    public LogicTrigger stepE_Objects_LogicTrigger_EnemyStart_B;
    public LogicTrigger stepE_Objects_LogicTrigger_EnemyStart_C;
    public LogicTrigger stepE_Objects_LogicTrigger_TheEnd;
    public MapLogicJob_FightInRegsGroup stepE_Enemy_FightInRegsGroup_A;
    public MapLogicJob_FightInRegsGroup stepE_Enemy_FightInRegsGroup_B;
    public MapLogicJob_FightInRegsGroup stepE_Enemy_FightInRegsGroup_C;
    public Transform stepE_HUD_JungleEndMinimapTr;

    //

    LogicFlag flag_A_AllyChainJob_Finished = new LogicFlag();
    LogicFlag flag_A_Fight01_Finished = new LogicFlag();
    LogicFlag flag_A_PlayerEntered_ExitTrigger01 = new LogicFlag();
    LogicFlag flag_A_Fight02_Finished = new LogicFlag();
    LogicFlag flag_A_PlayerEntered_ExitTrigger02 = new LogicFlag();

    LogicFlag flag_B_SomeoneEntered_BanditsTrigger = new LogicFlag();
    LogicFlag flag_B_SomeoneEntered_CampTrigger = new LogicFlag();
    LogicFlag flag_B_AllyChainJob_Finished = new LogicFlag();
    LogicFlag flag_B_TowerExpBarrelDestroyed = new LogicFlag();
    LogicFlag flag_B_SomeoneEntered_CampExitTrigger = new LogicFlag();
    LogicFlag flag_B_Camp_FightInRegs_Finished = new LogicFlag();

    LogicFlag flag_C_AllyChainJob_Finished = new LogicFlag();
    LogicFlag flag_C_SomeoneEntered_HiddenTrigger = new LogicFlag();
    LogicFlag flag_C_Hidden_FightInRegs_Finished = new LogicFlag();
    LogicFlag flag_C_SomeoneEntered_HiddenTriggerPart02 = new LogicFlag();
    LogicFlag flag_C_HiddenPart2_FightInRegs_Finished = new LogicFlag();
    LogicFlag flag_C_SomeoneEntered_ExitTriggerPart02 = new LogicFlag();
    LogicFlag flag_C_SomeoneEntered_LogicTrigger_SnipeHillCutscene = new LogicFlag();

    LogicFlag flag_D_AllyChainJob_Finished = new LogicFlag();
    LogicFlag flag_D_Kolli_FightInRegs_Finished = new LogicFlag();
    LogicFlag flag_D_Jelo_FightInRegs_Finished = new LogicFlag();
    LogicFlag flag_D_Posht_FightInRegs_Finished = new LogicFlag();
    LogicFlag flag_D_NearEnemiesAreFullIn_LogicTrigger_NearEnemies = new LogicFlag();
    LogicFlag flag_D_OneOfAlliesIsAttckable = new LogicFlag();
    LogicFlag flag_D_PlayerEntered_JeloRushExitTrigger = new LogicFlag();
    LogicFlag flag_D_PlayerEntered_PoshtRushExitTrigger = new LogicFlag();
    LogicFlag flag_D_PlayerEntered_PoshtStartKillingRemainingEnemiesTrigger = new LogicFlag();

    LogicFlag flag_E_SomeoneEntered_EnemyStartTrigger_A = new LogicFlag();
    LogicFlag flag_E_SomeoneEntered_EnemyStartTrigger_B = new LogicFlag();
    LogicFlag flag_E_SomeoneEntered_EnemyStartTrigger_C = new LogicFlag();
    LogicFlag flag_E_PlayerEntered_TheEndTrigger = new LogicFlag();
    LogicFlag flag_E_AllyChainJob_Finished = new LogicFlag();
    LogicFlag flag_E_Enemies_A_Finished = new LogicFlag();
    LogicFlag flag_E_Enemies_B_Finished = new LogicFlag();
    LogicFlag flag_E_Enemies_C_Finished = new LogicFlag();

    //

    bool isBorjakVoiceStarted = false;

    float sniperRifleFeleshTimeCounter = 15;
    bool isSniperRifleFeleshRemoved = false;

    //

    CharacterInfo ally01CharInfo;
    CharacterInfo ally02CharInfo;

    //

    public override void StartIt()
    {
        base.StartIt();

        ally01CharInfo = ally01.GetComponent<CharacterInfo>();
        ally02CharInfo = ally02.GetComponent<CharacterInfo>();

        for (int i = 0; i < stepB_Objects_TowerExplodableBarrels.Length; i++)
        {
            stepB_Objects_TowerExplodableBarrels[i].GetComponent<DynamicObject>().enabled = false;
        }

        //LoadCheckPoint(5.96f);
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
                //SetLevelStep(1f);

                firstCutscene.StartIt();

                SetLevelStep(0.2f);

                //SetLevelStep(1f);
            }
            #endregion

            #region 0.2 Run first cutscene
            if (levelStep == 0.2f)
            {
                if (firstCutscene.status == CutsceneStatus.Finished)
                {
                    SetLevelStep(0.3f);
                }
            }
            #endregion

            #region 0.3 BlackScreen after first cutscene
            if (levelStep == 0.3f)
            {
                mapLogic.blackScreenFader.StartFadingIn();
                SetLevelStep(1f);
            }
            #endregion

            #region 1 Init JunlgeStart01 - OK
            if (levelStep == 1)
            {
                SaveCheckPoint(1);

                allyChainJobsGlobalLogicIndex = 0;

                chainJobGroup_01_JungleStart.StartOutStepIfNotStarted();

                fightInRegsGroup_01_JungleStart01.StartOutStepIfNotStarted();
                stepA_Objects_ExitTrigger_JungleStart01.StartOutStepIfNotStarted();

                logVoiceCollection_Ally01.PlayName("A_01_TirAndaziKonin");
                logVoiceCollection_Ally01.AddToPlayQueue("A_04_Bezanineshun", 3.5f);
                logVoiceCollection_Ally01.AddToPlayQueue("A_05_NazarinBianJelotar", 5);
                logVoiceCollection_Ally01.AddToPlayQueue("A_03_poshteSanga", 13);
                logVoiceCollection_Ally01.AddToPlayQueue("A_06_Bokoshideshun", 11);
                logVoiceCollection_Ally02.PlayName("A_02_KenareUnSanga");
                logVoiceCollection_Ally02.AddToPlayQueue("A_03_Derakht", 16);
                logVoiceCollection_Ally02.AddToPlayQueue("A_01_HamintoriTirAndaziKonin", 10);
                logVoiceCollection_Ally02.AddToPlayQueue("A_05_YallaDige", 10);

                mapLogic.HUD_ObjectivesPage_SetActiveObjective(1);
                mapLogic.HUD_ShowNewMission(0);

                mapLogic.HUD_AddOnlyMinimap3DObj(stepA_HUD_DatMinimapTr, "DatJungInitialFight");

                PlayFirstMusic();

                SetLevelStep(1.1f);
            }
            #endregion

            #region 1.1 Run JunlgeStart01 - OK
            if (levelStep == 1.1f)
            {
                if (flag_A_Fight01_Finished.IsEverActivated())
                {
                    SetLevelStep(1.2f);
                }

                if (flag_A_PlayerEntered_ExitTrigger01.IsEverActivated())
                {
                    fightInRegsGroup_01_JungleStart01.StopCreatingMoreSoldiersAndMakeAliveSoldiersSoWeak();
                    stepA_Objects_LogicDieTrigger_01.StartItIfItsNotStartedBefore();

                    SetLevelStep(1.2f);
                }
            }
            #endregion

            #region 1.2 Start JunlgeStart02 - OK
            if (levelStep == 1.2f)
            {
                fightInRegsGroup_01_JungleStart02.StartOutStepIfNotStarted();
                stepA_Objects_ExitTrigger_JungleStart02.StartOutStepIfNotStarted();

                SetLevelStep(1.3f);
            }
            #endregion

            #region 1.3 Run JunlgeStart 01 & 02 - OK
            if (levelStep == 1.3f)
            {
                if (flag_A_Fight01_Finished.IsActiveNow())
                {
                    SetLevelStep(1.35f);
                }

                if (flag_A_Fight02_Finished.IsEverActivated())
                {
                    SetLevelStep(1.5f);
                }

                if (flag_A_PlayerEntered_ExitTrigger02.IsEverActivated())
                {
                    fightInRegsGroup_01_JungleStart02.StopCreatingMoreSoldiersAndMakeAliveSoldiersSoWeak();
                    stepA_Objects_LogicDieTrigger_02.StartItIfItsNotStartedBefore();
                    SetLevelStep(1.5f);
                }
            }
            #endregion

            #region 1.35 Start AllyChainJobs Part 2 - OK
            if (levelStep == 1.35f)
            {
                allyChainJobsGlobalLogicIndex = 1;
                chainJobGroup_01_JungleStart.Init_SetNewGlobalLogicIndex(allyChainJobsGlobalLogicIndex);

                logVoiceCollection_Ally01.EmptyQueue();
                logVoiceCollection_Ally02.EmptyQueue();

                logVoiceCollection_Ally01.PlayName("A_02_Move");
                logVoiceCollection_Ally01.AddToPlayQueue("A_07_poshteSanga", 16);

                logVoiceCollection_Ally02.AddToPlayQueue("A_06_GoGoGo", 10);
                logVoiceCollection_Ally02.AddToPlayQueue("A_04_PanahBegir", 3);
                logVoiceCollection_Ally02.AddToPlayQueue("A_07_BezaneshBezanesh", 12);

                SetLevelStep(1.4f);
            }
            #endregion

            #region 1.4 Wait for JunlgeStart02 finish - OK
            if (levelStep == 1.4f)
            {
                if (flag_A_Fight02_Finished.IsEverActivated())
                {
                    SetLevelStep(1.5f);
                }

                if (flag_A_PlayerEntered_ExitTrigger02.IsEverActivated())
                {
                    fightInRegsGroup_01_JungleStart02.StopCreatingMoreSoldiersAndMakeAliveSoldiersSoWeak();
                    stepA_Objects_LogicDieTrigger_02.StartItIfItsNotStartedBefore();
                    SetLevelStep(1.5f);
                }
            }
            #endregion

            #region 1.5 Start finishing AllyChainJob - OK
            if (levelStep == 1.5f)
            {
                chainJobGroup_01_JungleStart.StartFinishing_OutStepIfNotFinishing();
                stepA_Objects_LogicDieTrigger_01.StartItIfItsNotStartedBefore();
                stepA_Objects_LogicDieTrigger_02.StartItIfItsNotStartedBefore();

                SetLevelStep(1.6f);
            }
            #endregion

            #region 1.6 Wait for AllyChainJob finish
            if (levelStep == 1.6f)
            {
                if (flag_A_AllyChainJob_Finished.IsEverActivated())
                {
                    mapLogic.HUD_ObjectivesPage_SetObjectiveDone(1);
                    mapLogic.HUD_ShowCompleteMission(0);

                    mapLogic.HUD_RemoveMinimap3DObj("DatJungInitialFight");

                    SetLevelStep(2);
                }
            }
            #endregion

            #region 2 Init StepA+1 - OK
            if (levelStep == 2)
            {
                SaveCheckPoint(2);

                allyChainJobsGlobalLogicIndex = 0;

                //stepB_Objects_LogicTrigger_Bandits.StartOutStepIfNotStarted();

                stepB_Ally_ChainJobGroup_Start.StartOutStepIfNotStarted();

                stepB_Objects_LogicTrigger_Camp.StartOutStepIfNotStarted();

                //stepB_Objects_TowerExplodableBarrels[0].GetComponent<DynamicObject>().flag_ObjectDestroyed = flag_B_TowerExpBarrelDestroyed;

                mapLogic.HUD_ShowGameSaved();

                mapLogic.HUD_ObjectivesPage_SetObjectiveDone(1);
                mapLogic.HUD_ShowNewMission(1);

                mapLogic.HUD_AddOnlyMinimap3DObj(stepB_HUD_BeforeCampMinimapTr, "BeforeCamp");

                logVoiceCollection_Ally01.EmptyQueue();
                logVoiceCollection_Ally02.EmptyQueue();

                logVoiceCollection_Ally02.PlayName("AB_01_Move");

                PlayFirstMusic();

                SetLevelStep(2.1f);
            }
            #endregion

            #region 2.1 W8 for an ally reaching the before-camp trigger.
            if (levelStep == 2.1f)
            {
                if (flag_B_SomeoneEntered_CampTrigger.IsEverActivated())
                {
                    //stepB_Enemy_MachineGun_A.StartOutStepIfNotStarted();

                    //stepB_Enemy_MachineGun_B.StartOutStepIfNotStarted();

                    //stepB_Enemy_FightInRegsGroup_Camp.StartOutStepIfNotStarted();

                    //stepB_Objects_LogicTrigger_CampExit.StartOutStepIfNotStarted();

                    //stepB_Objects_CampExecutionArea.StartIt();

                    //for (int i = 0; i < stepB_Objects_TowerExplodableBarrels.Length; i++)
                    //{
                    //    stepB_Objects_TowerExplodableBarrels[i].GetComponent<DynamicObject>().enabled = true;
                    //}

                    stepB_Objects_LogicTrigger_Camp.StartFinishing_OutStepIfNotFishining();

                    mapLogic.HUD_RemoveMinimap3DObj("BeforeCamp");

                    SetLevelStep(3f);

                    //logVoiceCollection_Ally01.PlayName("B_01_Mosalsal");
                }
            }
            #endregion

            #region 3 Init StepB - Camp
            if (levelStep == 3)
            {
                SaveCheckPoint(3);

                allyChainJobsGlobalLogicIndex = 0;

                stepB_Ally_ChainJobGroup_Start.StartOutStepIfNotStarted();

                stepB_Objects_TowerExplodableBarrels[0].GetComponent<DynamicObject>().flag_ObjectDestroyed = flag_B_TowerExpBarrelDestroyed;

                stepB_Enemy_MachineGun_A.StartOutStepIfNotStarted();

                stepB_Enemy_MachineGun_B.StartOutStepIfNotStarted();

                stepB_Enemy_FightInRegsGroup_Camp.StartOutStepIfNotStarted();

                stepB_Objects_LogicTrigger_CampExit.StartOutStepIfNotStarted();

                stepB_Objects_CampExecutionArea.StartIt();

                for (int i = 0; i < stepB_Objects_TowerExplodableBarrels.Length; i++)
                {
                    stepB_Objects_TowerExplodableBarrels[i].GetComponent<DynamicObject>().enabled = true;
                }

                mapLogic.HUD_ShowGameSaved();

                mapLogic.HUD_ObjectivesPage_SetActiveObjective(2);
                mapLogic.HUD_ShowNewMission(2);

                mapLogic.HUD_AddOnlyMinimap3DObj(stepB_HUD_CampMinimapTr, "Camp");

                logVoiceCollection_Ally01.EmptyQueue();
                logVoiceCollection_Ally02.EmptyQueue();

                logVoiceCollection_Ally01.AddToPlayQueue("B_01_Mosalsal", 3);
                logVoiceCollection_Ally02.AddToPlayQueue("B_01_Mosalsal", 5);

                PlayFirstMusic();

                SetLevelStep(3.3f);
            }
            #endregion

            #region 3.3 Running camp
            if (levelStep == 3.3f)
            {
                if (!isBorjakVoiceStarted)
                {
                    stepB_DelayCounterToStartBorjakVoice = MathfPlus.DecByDeltatimeToZero(stepB_DelayCounterToStartBorjakVoice);

                    if (stepB_DelayCounterToStartBorjakVoice == 0)
                    {
                        stepB_DelayCounterToStartBorjakVoice = 100000000;

                        isBorjakVoiceStarted = true;

                        logVoiceCollection_Ally01.EmptyQueue();
                        logVoiceCollection_Ally02.EmptyQueue();

                        logVoiceCollection_Ally01.AddToPlayQueue("B_02_Borjak", 0);
                        logVoiceCollection_Ally02.AddToPlayQueue("B_02_Borjak", 3.0f);

                        mapLogic.HUD_Add3DObjective(stepB_HUD_BorjakObj3DTr, The3DObjIconType.FeleshRooBePayin, "Borjak", The3DObjViewRange.Far);
                    }
                }

                if (isBorjakVoiceStarted)
                {
                    if (logVoiceCollection_Ally01.IsPlayedRightNow())
                    {
                        mapLogic.HUD_3DObjBlinkInMinimap("Borjak");
                        mapLogic.HUD_ShowNewMission(3);
                    }
                }

                if (flag_B_TowerExpBarrelDestroyed.IsEverActivated())
                {
                    if (isBorjakVoiceStarted)
                    {
                        isBorjakVoiceStarted = false;
                        logVoiceCollection_Ally02.StopCurVoiceAfterItsFinishing();
                        mapLogic.HUD_ShowCompleteMission(3);
                        mapLogic.HUD_Remove3DObjective("Borjak");
                    }

                    SetLevelStep(3.4f);
                }
            }
            #endregion

            #region 3.4 Destroy tower and stop creating more soldiers
            if (levelStep == 3.4f)
            {
                stepB_Objects_TowerSound.Play();
                stepB_Objects_TowerExplosionSound.Play();
                stepB_Objects_Tower.animation.Play();
                stepB_Objects_TowerParticle.Play();

                stepB_Objects_Tower_RemovableCollider.enabled = false;
                stepB_Objects_Tower_RemovableCollider.active = false;

                stepB_Objects_CampDieTrigger.GetComponent<LogicDieTrigger>().StartIt();

                stepB_Enemy_MachineGun_A.StopCreatingMoreSoldiers();
                stepB_Enemy_MachineGun_B.StopCreatingMoreSoldiers();
                stepB_Enemy_FightInRegsGroup_Camp.StopCreatingMoreSoldiers();

                logVoiceCollection_Ally01.EmptyQueue();
                logVoiceCollection_Ally02.EmptyQueue();

                logVoiceCollection_Ally02.AddToPlayQueue("B_03_Bokoshideshun", 3.3f);

                SetLevelStep(3.5f);
            }
            #endregion

            #region 3.5 Wait for fight group finish or camp exit
            if (levelStep == 3.5f)
            {
                if (flag_B_SomeoneEntered_CampExitTrigger.IsEverActivated())
                {
                    SetLevelStep(3.6f);
                }

                if (flag_B_Camp_FightInRegs_Finished.IsEverActivated())
                {
                    SetLevelStep(3.6f);
                }
            }
            #endregion

            #region 3.6 End camp and start finishing ally chain jobs
            if (levelStep == 3.6f)
            {
                stepB_Objects_Camp_End_DieTrigger.StartIt();

                stepB_Objects_CampExecutionArea.EndIt();

                stepB_Ally_ChainJobGroup_Start.StartFinishing_OutStepIfNotFinishing();

                mapLogic.HUD_ObjectivesPage_SetObjectiveDone(2);
                mapLogic.HUD_ShowCompleteMission(2);

                mapLogic.HUD_RemoveMinimap3DObj("Camp");

                logVoiceCollection_Ally01.PlayName("B_03_Move");

                SetLevelStep(3.7f);
            }
            #endregion

            #region 3.7 Wait for ally chain job finish
            if (levelStep == 3.7f)
            {
                if (flag_B_AllyChainJob_Finished.IsEverActivated())
                {
                    SetLevelStep(4f);
                }
            }
            #endregion

            #region 4 Init StepB+1 - OK
            if (levelStep == 4)
            {
                SaveCheckPoint(4);

                allyChainJobsGlobalLogicIndex = 0;

                stepC_Ally_ChainJobGroup_Start.StartOutStepIfNotStarted();

                stepC_Objects_LogicTrigger_Hidden.StartOutStepIfNotStarted();

                mapLogic.HUD_ShowGameSaved();

                mapLogic.HUD_ObjectivesPage_SetObjectiveDone(2);
                mapLogic.HUD_ShowNewMission(4);

                mapLogic.HUD_AddOnlyMinimap3DObj(stepB1_HUD_BeforeHiddenMinimapTr, "BeforeHidden");

                logVoiceCollection_Ally01.EmptyQueue();
                logVoiceCollection_Ally02.EmptyQueue();

                logVoiceCollection_Ally01.AddToPlayQueue("BC_01_Move", 1f);

                PlayFirstMusic();

                SetLevelStep(4.1f);
            }
            #endregion

            #region 4.1 Wait for an ally reaching before-hidden trigger
            if (levelStep == 4.1f)
            {
                if (flag_C_SomeoneEntered_HiddenTrigger.IsEverActivated())
                {
                    //stepC_Enemy_FightInRegsGroup_Hidden.StartOutStepIfNotStarted();

                    //stepC_Objects_LogicTrigger_Hidden_Part02.StartOutStepIfNotStarted();

                    stepC_Objects_LogicTrigger_Hidden.StartFinishing_OutStepIfNotFishining();

                    mapLogic.HUD_ShowCompleteMission(4);

                    mapLogic.HUD_RemoveMinimap3DObj("BeforeHidden");

                    SetLevelStep(5f);
                }
            }
            #endregion

            #region 5 Init StepC
            if (levelStep == 5f)
            {
                //if (flag_C_SomeoneEntered_HiddenTrigger.IsEverActivated())
                //{

                SaveCheckPoint(5);

                allyChainJobsGlobalLogicIndex = 0;

                stepC_Ally_ChainJobGroup_Start.StartOutStepIfNotStarted();

                stepC_Enemy_FightInRegsGroup_Hidden.StartOutStepIfNotStarted();

                stepC_Objects_LogicTrigger_Hidden_Part02.StartOutStepIfNotStarted();

                mapLogic.HUD_ShowGameSaved();

                mapLogic.HUD_ObjectivesPage_SetActiveObjective(3);
                mapLogic.HUD_ShowNewMission(5);

                mapLogic.HUD_AddOnlyMinimap3DObj(stepC_HUD_HiddenMinimapTr, "Hidden");

                //logVoiceCollection_Ally01.PlayName("C_01_Attack");

                logVoiceCollection_Ally01.EmptyQueue();
                logVoiceCollection_Ally02.EmptyQueue();

                logVoiceCollection_Ally01.AddToPlayQueue("C_01_Attack", 6f);
                logVoiceCollection_Ally01.AddToPlayQueue("C_03_Attack", 12);
                logVoiceCollection_Ally02.AddToPlayQueue("C_03_PoshteDerakht", 0.5f);
                logVoiceCollection_Ally02.AddToPlayQueue("C_04_Bokoshideshun", 27f);

                PlayFirstMusic();

                SetLevelStep(5.2f);

                //}
            }
            #endregion

            #region 5.2 Running hidden - OK
            if (levelStep == 5.2f)
            {
                if (flag_C_Hidden_FightInRegs_Finished.IsEverActivated())
                {
                    SetLevelStep(5.3f);
                }

                if (flag_C_SomeoneEntered_HiddenTriggerPart02.IsEverActivated())
                {
                    SetLevelStep(5.3f);
                }
            }
            #endregion

            #region 5.3 Stop creating solds and start moving allies to hidden 2
            if (levelStep == 5.3f)
            {
                allyChainJobsGlobalLogicIndex = 1;
                stepC_Ally_ChainJobGroup_Start.Init_SetNewGlobalLogicIndex(allyChainJobsGlobalLogicIndex);

                stepC_Objects_LogicDieTrigger_01.StartIt();

                if (!flag_C_Hidden_FightInRegs_Finished.IsEverActivated())
                {
                    stepC_Enemy_FightInRegsGroup_Hidden.StopCreatingMoreSoldiers();
                }

                logVoiceCollection_Ally01.EmptyQueue();
                logVoiceCollection_Ally02.EmptyQueue();

                logVoiceCollection_Ally01.AddToPlayQueue("C_04_Move", 1f);
                logVoiceCollection_Ally01.AddToPlayQueue("C_02_Move", 0);
                logVoiceCollection_Ally01.AddToPlayQueue("C_06_PoshteUnSang", 13);
                logVoiceCollection_Ally02.AddToPlayQueue("C_01_Move", 12f);
                logVoiceCollection_Ally02.AddToPlayQueue("C_05_TirAndaziKonin", 15f);

                SetLevelStep(5.4f);
            }
            #endregion

            #region 5.4 Wait for starting hidden part 2 - OK
            if (levelStep == 5.4f)
            {
                if (flag_C_SomeoneEntered_HiddenTriggerPart02.IsEverActivated())
                {
                    SetLevelStep(5.5f);
                }
            }
            #endregion

            #region 5.5 Start hidden part 2 - OK
            if (levelStep == 5.5f)
            {
                stepC_Enemy_FightInRegsGroup_Hidden_Part02.StartOutStepIfNotStarted();
                stepC_Objects_ExitTrigger_Hidden_Part02.StartOutStepIfNotStarted();
                stepC_Objects_LogicTrigger_SnipeHillCutscene.StartOutStepIfNotStarted();
                SetLevelStep(5.6f);
            }
            #endregion

            #region 5.6 Running hidden part 2 - OK
            if (levelStep == 5.6f)
            {
                if (flag_C_HiddenPart2_FightInRegs_Finished.IsEverActivated())
                {
                    SetLevelStep(5.7f);
                }

                if (flag_C_SomeoneEntered_ExitTriggerPart02.IsEverActivated())
                {
                    SetLevelStep(5.7f);
                }
            }
            #endregion

            #region 5.7 Stop creating solds and start moving allies to snipe hill
            if (levelStep == 5.7f)
            {
                allyChainJobsGlobalLogicIndex = 2;
                stepC_Ally_ChainJobGroup_Start.Init_SetNewGlobalLogicIndex(allyChainJobsGlobalLogicIndex);

                stepC_Objects_LogicDieTrigger_02.StartIt();

                if (!flag_C_HiddenPart2_FightInRegs_Finished.IsEverActivated())
                {
                    stepC_Enemy_FightInRegsGroup_Hidden_Part02.StopCreatingMoreSoldiers();
                }

                mapLogic.HUD_ObjectivesPage_SetObjectiveDone(3);
                mapLogic.HUD_ShowCompleteMission(5);

                mapLogic.HUD_RemoveMinimap3DObj("Hidden");

                logVoiceCollection_Ally01.EmptyQueue();
                logVoiceCollection_Ally02.EmptyQueue();

                logVoiceCollection_Ally01.AddToPlayQueue("C_05_Move", 1f);
                logVoiceCollection_Ally02.AddToPlayQueue("C_02_Move", 15f);

                SetLevelStep(5.8f);
            }
            #endregion

            #region 5.8 Wait for reaching snipe hill trigger - OK
            if (levelStep == 5.8f)
            {
                if (flag_C_SomeoneEntered_LogicTrigger_SnipeHillCutscene.IsEverActivated())
                {
                    SetLevelStep(5.9f);
                }
            }
            #endregion

            #region 5.9 Start screen fading
            if (levelStep == 5.9f)
            {
                MusicController.Instance.EndMusicWithFade(MusicFadeType.Fast);

                mapLogic.blackScreenFader.StartFadingOut();
                SetLevelStep(5.95f);
            }
            #endregion

            #region 5.95 fading screen
            if (levelStep == 5.95f)
            {
                if (mapLogic.blackScreenFader.isFadingFinished)
                {
                    SetLevelStep(5.96f);
                }
            }
            #endregion

            #region 5.96 Start snipe cutscene
            if (levelStep == 5.96f)
            {
                stepD_SnipeItemRendererToHideAndShowInCutscene.enabled = false;

                snipeCutscene.StartIt();

                SetLevelStep(5.97f);
            }
            #endregion

            #region 5.97 Run cutscene
            if (levelStep == 5.97f)
            {
                if (snipeCutscene.status == CutsceneStatus.Finished)
                {
                    SetLevelStep(5.98f);
                }
            }
            #endregion

            #region 5.98 BlackScreen after cutscene
            if (levelStep == 5.98f)
            {
                stepD_SnipeItemRendererToHideAndShowInCutscene.enabled = true;
                mapLogic.blackScreenFader.StartFadingIn();
                SetLevelStep(6f);
            }
            #endregion

            #region 6 Init StepD - SnipeZone
            if (levelStep == 6)
            {
                SaveCheckPoint(6);

                stepD_Objects_AudioInfo_CrowdShout.Play();

                D_PlaceSolds();

                allyChainJobsGlobalLogicIndex = 0;

                stepD_Objects_LogicTrigger_NearEnemies.StartOutStepIfNotStarted();

                stepD_Ally_ChainJobGroup_Start.StartOutStepIfNotStarted();

                stepD_Enemy_FightInRegsGroup_Jelo.StartOutStepIfNotStarted();
                stepD_Enemy_FightInRegsGroup_Kolli.StartOutStepIfNotStarted();

                stepD_Objects_ExitTrigger_JeloRush.StartOutStepIfNotStarted();

                stepD_Objects_ExecutionArea_PoshtRush.StartIt();

                mapLogic.HUD_ShowGameSaved();

                mapLogic.HUD_ObjectivesPage_SetActiveObjective(4);
                mapLogic.HUD_ShowNewMission(6);

                mapLogic.HUD_AddOnlyMinimap3DObj(stepD_HUD_SnipeZoneMinimapTr, "SnipeZone");

                mapLogic.HUD_Add3DObjective(stepD_HUD_SniperRifleObj3DTr, The3DObjIconType.FeleshRooBePayin, "SniperRifle", The3DObjViewRange.Near);

                SetLevelStep(6.1f);
            }
            #endregion

            #region 6.1 Run First Rush
            if (levelStep == 6.1f)
            {
                if (!isSniperRifleFeleshRemoved)
                {
                    if (mapLogic.playerCharNew.IsGunAvailable(PlayerGunName.Snipe))
                    {
                        isSniperRifleFeleshRemoved = true;
                        mapLogic.HUD_Remove3DObjective("SniperRifle");
                    }

                    if (!isSniperRifleFeleshRemoved)
                    {
                        sniperRifleFeleshTimeCounter = MathfPlus.DecByDeltatimeToZero(sniperRifleFeleshTimeCounter);

                        if (sniperRifleFeleshTimeCounter == 0)
                        {
                            isSniperRifleFeleshRemoved = true;
                            mapLogic.HUD_Remove3DObjective("SniperRifle");
                        }
                    }
                }

                D_CheckShouldMakeOneOfAlliesAttackableIfHeIsNot();

                if (IsAnyAllyDead())
                {
                    SetLevelStep(step_MissionFail_AlliesNotSupported);
                    goto EndLevelSteps;
                }

                stepD_DieTriggerFrontDelay = MathfPlus.DecByDeltatimeToZero(stepD_DieTriggerFrontDelay);

                D_CheckShouldStartFrontDieTrigger_IfItsNotStartedEver();

                if (flag_D_Jelo_FightInRegs_Finished.IsEverActivated())
                {
                    SetLevelStep(6.2f);
                    goto StartLevelSteps;
                }

                if (flag_D_PlayerEntered_JeloRushExitTrigger.IsEverActivated())
                {
                    SetLevelStep(6.15f);
                    goto EndLevelSteps;
                }
            }
            #endregion

            #region 6.15 Start posht rush and keep running first rush
            if (levelStep == 6.15f)
            {
                if (!isSniperRifleFeleshRemoved)
                {
                    isSniperRifleFeleshRemoved = true;
                    mapLogic.HUD_Remove3DObjective("SniperRifle");
                }

                stepD_Enemy_FightInRegsGroup_Posht.StartOutStepIfNotStarted();
                stepD_Objects_ExitTrigger_PoshtRush.StartOutStepIfNotStarted();
                stepD_Objects_Trigger_PoshtRush_StartKillingRemainingEnemies.StartOutStepIfNotStarted();

                D_CheckShouldMakeOneOfAlliesAttackableIfHeIsNot();

                if (IsAnyAllyDead())
                {
                    SetLevelStep(step_MissionFail_AlliesNotSupported);
                    goto EndLevelSteps;
                }

                stepD_DieTriggerFrontDelay = MathfPlus.DecByDeltatimeToZero(stepD_DieTriggerFrontDelay);

                D_CheckShouldStartFrontDieTrigger_IfItsNotStartedEver();

                if (flag_D_Jelo_FightInRegs_Finished.IsEverActivated())
                {
                    SetLevelStep(6.2f);
                    goto StartLevelSteps;
                }

                if (flag_D_PlayerEntered_PoshtRushExitTrigger.IsEverActivated())
                {
                    SetLevelStep(step_MissionFail_AlliesNotSupported);
                    goto EndLevelSteps;
                }
            }
            #endregion

            #region 6.2 Start allies next step
            if (levelStep == 6.2f)
            {
                if (!isSniperRifleFeleshRemoved)
                {
                    isSniperRifleFeleshRemoved = true;
                    mapLogic.HUD_Remove3DObjective("SniperRifle");
                }

                if (IsAnyAllyDead())
                {
                    SetLevelStep(step_MissionFail_AlliesNotSupported);
                    goto EndLevelSteps;
                }

                D_MakeAlliesUndiable();

                allyChainJobsGlobalLogicIndex = 1;
                stepD_Ally_ChainJobGroup_Start.Init_SetNewGlobalLogicIndex(allyChainJobsGlobalLogicIndex);

                stepD_Enemy_FightInRegsGroup_Posht.StartOutStepIfNotStarted();
                stepD_Objects_ExitTrigger_PoshtRush.StartOutStepIfNotStarted();
                stepD_Objects_Trigger_PoshtRush_StartKillingRemainingEnemies.StartOutStepIfNotStarted();

                SetLevelStep(6.3f);
            }
            #endregion

            #region 6.3 Run allies next step
            if (levelStep == 6.3f)
            {
                if (flag_D_Kolli_FightInRegs_Finished.IsEverActivated() && flag_D_Posht_FightInRegs_Finished.IsEverActivated())
                {
                    SetLevelStep(6.5f);
                    goto StartLevelSteps;
                }

                if (flag_D_PlayerEntered_PoshtRushExitTrigger.IsEverActivated())
                {
                    SetLevelStep(6.35f);
                }
            }
            #endregion

            #region 6.35 Player entered posht exit trigger
            if (levelStep == 6.35f)
            {
                stepD_Enemy_FightInRegsGroup_Kolli.StopCreatingMoreSoldiersAndMakeAliveSoldiersSoWeak();
                stepD_Enemy_FightInRegsGroup_Posht.StopCreatingMoreSoldiersAndMakeAliveSoldiersSoWeak();

                SetLevelStep(6.4f);
            }
            #endregion

            #region 6.4 Keep running weak kolli and posht
            if (levelStep == 6.4f)
            {
                if (flag_D_Kolli_FightInRegs_Finished.IsEverActivated() && flag_D_Posht_FightInRegs_Finished.IsEverActivated())
                {
                    SetLevelStep(6.5f);
                    goto StartLevelSteps;
                }

                if (flag_D_PlayerEntered_PoshtStartKillingRemainingEnemiesTrigger.IsEverActivated())
                {
                    stepD_Objects_LogicDieTrigger_Back.StartItIfItsNotStartedBefore();
                }
            }
            #endregion

            #region 6.5 All enemies finished - Start finishing ally jobs
            if (levelStep == 6.5f)
            {
                stepD_Objects_ExecutionArea_PoshtRush.EndIt();

                stepD_Ally_ChainJobGroup_Start.StartFinishing_OutStepIfNotFinishing();

                mapLogic.HUD_ObjectivesPage_SetObjectiveDone(4);
                mapLogic.HUD_ShowCompleteMission(6);

                mapLogic.HUD_RemoveMinimap3DObj("SnipeZone");

                SetLevelStep(6.6f);
            }
            #endregion

            #region 6.6 Wait for AllyChainJob finish
            if (levelStep == 6.6f)
            {
                if (flag_D_AllyChainJob_Finished.IsEverActivated())
                {
                    SetLevelStep(7);
                }
            }
            #endregion

            #region 7 Init Step E
            if (levelStep == 7)
            {
                SaveCheckPoint(7);

                allyChainJobsGlobalLogicIndex = 0;

                stepE_Ally_ChainJobGroup.StartOutStepIfNotStarted();

                //stepE_Objects_LogicTrigger_EnemyStart_A.StartOutStepIfNotStarted();
                //stepE_Objects_LogicTrigger_EnemyStart_B.StartOutStepIfNotStarted();
                //stepE_Objects_LogicTrigger_EnemyStart_C.StartOutStepIfNotStarted();

                stepE_Objects_LogicTrigger_TheEnd.StartOutStepIfNotStarted();

                mapLogic.HUD_ShowGameSaved();

                mapLogic.HUD_ObjectivesPage_SetActiveObjective(5);
                mapLogic.HUD_ShowNewMission(9);

                mapLogic.HUD_AddOnlyMinimap3DObj(stepE_HUD_JungleEndMinimapTr, "JungleEnd");

                logVoiceCollection_Ally02.PlayName("E_01_Move");

                SetLevelStep(7.1f);
            }
            #endregion

            #region 7.1 Wait for player reaching EndTrigger
            if (levelStep == 7.1f)
            {
                if (flag_E_PlayerEntered_TheEndTrigger.IsEverActivated())
                {
                    //stepE_Enemy_FightInRegsGroup_A.StartOutStepIfNotStarted();

                    SetLevelStep(7.92f);
                }
            }
            #endregion

            #region 7.92 Start TheEnd
            if (levelStep == 7.92f)
            {
                mapLogic.HUD_ObjectivesPage_SetObjectiveDone(5);
                mapLogic.HUD_ShowCompleteMission(9);
                mapLogic.HUD_ShowMainMission(true);

                mapLogic.HUD_RemoveMinimap3DObj("JungleEnd");

                mapLogic.blackScreenFader.StartFadingOut();
                SetLevelStep(7.93f);
            }
            #endregion

            #region 7.93f Set mission is finished if black screen fading is done.
            if (levelStep == 7.93f)
            {
                if (mapLogic.blackScreenFader.isFadingFinished)
                {
                    SetMissionIsFinished();
                    SetLevelStep(8f);
                }
            }
            #endregion

            #region // Chert

        //#region 7.2 Run enemies A
        //if (levelStep == 7.2f)
        //{
        //    if (flag_E_Enemies_A_Finished.IsEverActivated())
        //    {
        //        SetLevelStep(7.3f);
        //        goto StartLevelSteps;
        //    }

            //    if (flag_E_SomeoneEntered_EnemyStartTrigger_B.IsEverActivated())
        //    {
        //        SetLevelStep(step_MissionFail_AlliesNotSupported);
        //        goto EndLevelSteps;
        //    }
        //}
        //#endregion

            //#region 7.3 Start AllyChainJobs Part 2
        //if (levelStep == 7.3f)
        //{
        //    allyChainJobsGlobalLogicIndex = 1;
        //    stepE_Ally_ChainJobGroup.Init_SetNewGlobalLogicIndex(allyChainJobsGlobalLogicIndex);

            //    SetLevelStep(7.4f);
        //}
        //#endregion

            //#region 7.4 Wait for starting enemies B
        //if (levelStep == 7.4f)
        //{
        //    if (flag_E_SomeoneEntered_EnemyStartTrigger_B.IsEverActivated())
        //    {
        //        stepE_Enemy_FightInRegsGroup_B.StartOutStepIfNotStarted();

            //        SetLevelStep(7.5f);
        //    }
        //}
        //#endregion

            //#region 7.5 Run enemies B
        //if (levelStep == 7.5f)
        //{
        //    if (flag_E_Enemies_B_Finished.IsEverActivated())
        //    {
        //        SetLevelStep(7.6f);
        //        goto StartLevelSteps;
        //    }

            //    if (flag_E_SomeoneEntered_EnemyStartTrigger_C.IsEverActivated())
        //    {
        //        SetLevelStep(step_MissionFail_AlliesNotSupported);
        //        goto EndLevelSteps;
        //    }
        //}
        //#endregion

            //#region 7.6 Start AllyChainJobs Part 3
        //if (levelStep == 7.6f)
        //{
        //    allyChainJobsGlobalLogicIndex = 2;
        //    stepE_Ally_ChainJobGroup.Init_SetNewGlobalLogicIndex(allyChainJobsGlobalLogicIndex);

            //    SetLevelStep(7.7f);
        //}
        //#endregion

            //#region 7.7 Wait for starting enemies C
        //if (levelStep == 7.7f)
        //{
        //    if (flag_E_SomeoneEntered_EnemyStartTrigger_C.IsEverActivated())
        //    {
        //        stepE_Enemy_FightInRegsGroup_C.StartOutStepIfNotStarted();

            //        SetLevelStep(7.8f);
        //    }
        //}
        //#endregion

            //#region 7.8 Run enemies C
        //if (levelStep == 7.8f)
        //{
        //    if (flag_E_Enemies_C_Finished.IsEverActivated())
        //    {
        //        SetLevelStep(7.9f);
        //        goto StartLevelSteps;
        //    }

            //    if (flag_E_SomeoneEntered_TheEndTrigger.IsEverActivated())
        //    {
        //        SetLevelStep(step_MissionFail_AlliesNotSupported);
        //        goto EndLevelSteps;
        //    }
        //}
        //#endregion

            //#region 7.9 Start AllyChainJobs Part 4
        //if (levelStep == 7.9f)
        //{
        //    allyChainJobsGlobalLogicIndex = 3;
        //    stepE_Ally_ChainJobGroup.Init_SetNewGlobalLogicIndex(allyChainJobsGlobalLogicIndex);

            //    SetLevelStep(7.91f);
        //}
        //#endregion

            //#region 7.91 Wait for reaching TheEnd trigger
        //if (levelStep == 7.91f)
        //{
        //    if (flag_E_SomeoneEntered_TheEndTrigger.IsEverActivated())
        //    {
        //        SetLevelStep(7.92f);
        //    }
        //}
        //#endregion 
            #endregion

            #endregion

        EndLevelSteps: ;

            // A

            #region ChainJobGroup_01_JungleStart

            #region 1 Start
            if (chainJobGroup_01_JungleStart.outStep == 1)
            {
                chainJobGroup_01_JungleStart.Init_SetNewGlobalLogicIndex(allyChainJobsGlobalLogicIndex);
                chainJobGroup_01_JungleStart.StartIt();

                chainJobGroup_01_JungleStart.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (chainJobGroup_01_JungleStart.outStep == 1.1f)
            {
                chainJobGroup_01_JungleStart.RunIt();
            }
            #endregion

            #region 900 Start finishing
            if (chainJobGroup_01_JungleStart.outStep == 900f)
            {
                chainJobGroup_01_JungleStart.SetNeedsToBeFinished();

                chainJobGroup_01_JungleStart.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (chainJobGroup_01_JungleStart.outStep == 901f)
            {
                chainJobGroup_01_JungleStart.RunIt();

                if (chainJobGroup_01_JungleStart.status == LogicJobStatus.Finished)
                {
                    flag_A_AllyChainJob_Finished.SetStatus(LogicFlagStatus.Active);

                    chainJobGroup_01_JungleStart.SetOutStep(1000);
                }
            }
            #endregion

            #endregion

            #region fightInRegsGroup_01_JungleStart01

            #region 1 Start
            if (fightInRegsGroup_01_JungleStart01.outStep == 1)
            {
                fightInRegsGroup_01_JungleStart01.StartIt();

                fightInRegsGroup_01_JungleStart01.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (fightInRegsGroup_01_JungleStart01.outStep == 1.1f)
            {
                fightInRegsGroup_01_JungleStart01.RunIt();

                if (fightInRegsGroup_01_JungleStart01.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    fightInRegsGroup_01_JungleStart01.StartFinishing_OutStepIfNotFinishing();

                    goto fightInRegsGroup_01_JungleStart01_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (fightInRegsGroup_01_JungleStart01.outStep == 900f)
            {
                fightInRegsGroup_01_JungleStart01.SetNeedsToBeFinished();
                fightInRegsGroup_01_JungleStart01.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (fightInRegsGroup_01_JungleStart01.outStep == 901f)
            {
                fightInRegsGroup_01_JungleStart01.RunIt();

                if (fightInRegsGroup_01_JungleStart01.status == LogicJobStatus.Finished)
                {
                    fightInRegsGroup_01_JungleStart01.SetOutStep(1000f);
                    flag_A_Fight01_Finished.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

        fightInRegsGroup_01_JungleStart01_End: ;

            #endregion

            #region StepA_Objects_ExitTrigger_JungleStart01

            #region 1 Start
            if (stepA_Objects_ExitTrigger_JungleStart01.OutStep == 1) //Start
            {
                stepA_Objects_ExitTrigger_JungleStart01.SetEnabled(true);
                stepA_Objects_ExitTrigger_JungleStart01.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepA_Objects_ExitTrigger_JungleStart01.OutStep == 1.1f) //Run
            {
                if (stepA_Objects_ExitTrigger_JungleStart01.IsPlayerIn())
                {
                    flag_A_PlayerEntered_ExitTrigger01.SetStatus(LogicFlagStatus.Active);
                    stepA_Objects_ExitTrigger_JungleStart01.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (stepA_Objects_ExitTrigger_JungleStart01.OutStep == 900f) //Finish
            {
                stepA_Objects_ExitTrigger_JungleStart01.SetEnabled(false);
                stepA_Objects_ExitTrigger_JungleStart01.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region fightInRegsGroup_01_JungleStart02

            #region 1 Start
            if (fightInRegsGroup_01_JungleStart02.outStep == 1)
            {
                fightInRegsGroup_01_JungleStart02.StartIt();

                fightInRegsGroup_01_JungleStart02.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (fightInRegsGroup_01_JungleStart02.outStep == 1.1f)
            {
                fightInRegsGroup_01_JungleStart02.RunIt();

                if (fightInRegsGroup_01_JungleStart02.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    fightInRegsGroup_01_JungleStart02.StartFinishing_OutStepIfNotFinishing();

                    goto fightInRegsGroup_01_JungleStart02_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (fightInRegsGroup_01_JungleStart02.outStep == 900f)
            {
                fightInRegsGroup_01_JungleStart02.SetNeedsToBeFinished();
                fightInRegsGroup_01_JungleStart02.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (fightInRegsGroup_01_JungleStart02.outStep == 901f)
            {
                fightInRegsGroup_01_JungleStart02.RunIt();

                if (fightInRegsGroup_01_JungleStart02.status == LogicJobStatus.Finished)
                {
                    fightInRegsGroup_01_JungleStart02.SetOutStep(1000f);
                    flag_A_Fight02_Finished.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

        fightInRegsGroup_01_JungleStart02_End: ;

            #endregion

            #region StepA_Objects_ExitTrigger_JungleStart02

            #region 1 Start
            if (stepA_Objects_ExitTrigger_JungleStart02.OutStep == 1) //Start
            {
                stepA_Objects_ExitTrigger_JungleStart02.SetEnabled(true);
                stepA_Objects_ExitTrigger_JungleStart02.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepA_Objects_ExitTrigger_JungleStart02.OutStep == 1.1f) //Run
            {
                if (stepA_Objects_ExitTrigger_JungleStart02.IsPlayerIn())
                {
                    flag_A_PlayerEntered_ExitTrigger02.SetStatus(LogicFlagStatus.Active);
                    stepA_Objects_ExitTrigger_JungleStart02.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (stepA_Objects_ExitTrigger_JungleStart02.OutStep == 900f) //Finish
            {
                stepA_Objects_ExitTrigger_JungleStart02.SetEnabled(false);
                stepA_Objects_ExitTrigger_JungleStart02.SetOutStep(1000f);
            }
            #endregion

            #endregion

            // B

            #region Logic Trigger "Bandits"

            #region 1 Start
            if (stepB_Objects_LogicTrigger_Bandits.OutStep == 1)
            {
                stepB_Objects_LogicTrigger_Bandits.SetEnabled(true);
                stepB_Objects_LogicTrigger_Bandits.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Wait
            if (stepB_Objects_LogicTrigger_Bandits.OutStep == 1.1f)
            {
                if (stepB_Objects_LogicTrigger_Bandits.IsSomethingIn())
                {
                    stepB_Objects_LogicTrigger_Bandits.StartFinishing_OutStepIfNotFishining();
                    flag_B_SomeoneEntered_BanditsTrigger.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

            #region 900 End
            if (stepB_Objects_LogicTrigger_Bandits.OutStep == 900)
            {
                stepB_Objects_LogicTrigger_Bandits.SetEnabled(false);

                stepB_Objects_LogicTrigger_Bandits.OutStep = 1000;
            }
            #endregion

            #endregion

            #region StepB_Ally_ChainJobGroup_Start

            #region 1 Start
            if (stepB_Ally_ChainJobGroup_Start.outStep == 1)
            {
                stepB_Ally_ChainJobGroup_Start.Init_SetNewGlobalLogicIndex(allyChainJobsGlobalLogicIndex);
                stepB_Ally_ChainJobGroup_Start.StartIt();

                stepB_Ally_ChainJobGroup_Start.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepB_Ally_ChainJobGroup_Start.outStep == 1.1f)
            {
                stepB_Ally_ChainJobGroup_Start.RunIt();
            }
            #endregion

            #region 900 Start finishing
            if (stepB_Ally_ChainJobGroup_Start.outStep == 900f)
            {
                stepB_Ally_ChainJobGroup_Start.SetNeedsToBeFinished();

                stepB_Ally_ChainJobGroup_Start.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (stepB_Ally_ChainJobGroup_Start.outStep == 901f)
            {
                stepB_Ally_ChainJobGroup_Start.RunIt();

                if (stepB_Ally_ChainJobGroup_Start.status == LogicJobStatus.Finished)
                {
                    flag_B_AllyChainJob_Finished.SetStatus(LogicFlagStatus.Active);

                    stepB_Ally_ChainJobGroup_Start.SetOutStep(1000);
                }
            }
            #endregion

            #endregion

            #region stepB_Enemy_FightInRegsGroup_Bandits

            #region 1 Start
            if (stepB_Enemy_FightInRegsGroup_Bandits.outStep == 1)
            {
                stepB_Enemy_FightInRegsGroup_Bandits.StartIt();

                stepB_Enemy_FightInRegsGroup_Bandits.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepB_Enemy_FightInRegsGroup_Bandits.outStep == 1.1f)
            {
                stepB_Enemy_FightInRegsGroup_Bandits.RunIt();

                if (stepB_Enemy_FightInRegsGroup_Bandits.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    stepB_Enemy_FightInRegsGroup_Bandits.StartFinishing_OutStepIfNotFinishing();

                    goto stepB_Enemy_FightInRegsGroup_Bandits_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (stepB_Enemy_FightInRegsGroup_Bandits.outStep == 900f)
            {
                stepB_Enemy_FightInRegsGroup_Bandits.SetNeedsToBeFinished();
                stepB_Enemy_FightInRegsGroup_Bandits.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (stepB_Enemy_FightInRegsGroup_Bandits.outStep == 901f)
            {
                stepB_Enemy_FightInRegsGroup_Bandits.RunIt();

                if (stepB_Enemy_FightInRegsGroup_Bandits.status == LogicJobStatus.Finished)
                {
                    stepB_Enemy_FightInRegsGroup_Bandits.SetOutStep(1000f);
                }
            }
            #endregion

        stepB_Enemy_FightInRegsGroup_Bandits_End: ;

            #endregion

            #region Logic Trigger "Camp"

            #region 1 Start
            if (stepB_Objects_LogicTrigger_Camp.OutStep == 1)
            {
                stepB_Objects_LogicTrigger_Camp.SetEnabled(true);
                stepB_Objects_LogicTrigger_Camp.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Wait
            if (stepB_Objects_LogicTrigger_Camp.OutStep == 1.1f)
            {
                if (stepB_Objects_LogicTrigger_Camp.IsSomethingIn())
                {
                    stepB_Objects_LogicTrigger_Camp.StartFinishing_OutStepIfNotFishining();
                    flag_B_SomeoneEntered_CampTrigger.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

            #region 900 End
            if (stepB_Objects_LogicTrigger_Camp.OutStep == 900)
            {
                stepB_Objects_LogicTrigger_Camp.SetEnabled(false);

                stepB_Objects_LogicTrigger_Camp.OutStep = 1000;
            }
            #endregion

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

            #region stepB_Enemy_MachineGun_B

            #region 1 Start
            if (stepB_Enemy_MachineGun_B.outStep == 1)
            {
                stepB_Enemy_MachineGun_B.StartIt();

                stepB_Enemy_MachineGun_B.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepB_Enemy_MachineGun_B.outStep == 1.1f)
            {
                stepB_Enemy_MachineGun_B.RunIt();

                if (stepB_Enemy_MachineGun_B.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    stepB_Enemy_MachineGun_B.StartFinishing_OutStepIfNotFinishing();

                    goto stepB_Enemy_MachineGun_B_End;
                }
            }
            #endregion

            #region 900 Start finishing
            if (stepB_Enemy_MachineGun_B.outStep == 900f)
            {
                stepB_Enemy_MachineGun_B.SetNeedsToBeFinished();
                stepB_Enemy_MachineGun_B.SetOutStep(901);
            }
            #endregion

            #region 901 Finishing
            if (stepB_Enemy_MachineGun_B.outStep == 901f)
            {
                stepB_Enemy_MachineGun_B.RunIt();

                if (stepB_Enemy_MachineGun_B.status == LogicJobStatus.Finished)
                {
                    stepB_Enemy_MachineGun_B.SetOutStep(1000f);
                }
            }
            #endregion

        stepB_Enemy_MachineGun_B_End: ;

            #endregion

            #region stepB_Enemy_FightInRegsGroup_Camp

            #region 1 Start
            if (stepB_Enemy_FightInRegsGroup_Camp.outStep == 1)
            {
                stepB_Enemy_FightInRegsGroup_Camp.StartIt();

                stepB_Enemy_FightInRegsGroup_Camp.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepB_Enemy_FightInRegsGroup_Camp.outStep == 1.1f)
            {
                stepB_Enemy_FightInRegsGroup_Camp.RunIt();

                if (stepB_Enemy_FightInRegsGroup_Camp.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    stepB_Enemy_FightInRegsGroup_Camp.StartFinishing_OutStepIfNotFinishing();

                    goto stepB_Enemy_FightInRegsGroup_Camp_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (stepB_Enemy_FightInRegsGroup_Camp.outStep == 900f)
            {
                stepB_Enemy_FightInRegsGroup_Camp.SetNeedsToBeFinished();
                stepB_Enemy_FightInRegsGroup_Camp.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (stepB_Enemy_FightInRegsGroup_Camp.outStep == 901f)
            {
                stepB_Enemy_FightInRegsGroup_Camp.RunIt();

                if (stepB_Enemy_FightInRegsGroup_Camp.status == LogicJobStatus.Finished)
                {
                    stepB_Enemy_FightInRegsGroup_Camp.SetOutStep(1000f);
                    flag_B_Camp_FightInRegs_Finished.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

        stepB_Enemy_FightInRegsGroup_Camp_End: ;

            #endregion

            #region stepB Logic Trigger "Camp Exit"

            #region 1 Start
            if (stepB_Objects_LogicTrigger_CampExit.OutStep == 1)
            {
                stepB_Objects_LogicTrigger_CampExit.SetEnabled(true);
                stepB_Objects_LogicTrigger_CampExit.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Wait
            if (stepB_Objects_LogicTrigger_CampExit.OutStep == 1.1f)
            {
                if (stepB_Objects_LogicTrigger_CampExit.IsSomethingIn())
                {
                    stepB_Objects_LogicTrigger_CampExit.StartFinishing_OutStepIfNotFishining();
                    flag_B_SomeoneEntered_CampExitTrigger.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

            #region 900 End
            if (stepB_Objects_LogicTrigger_CampExit.OutStep == 900)
            {
                stepB_Objects_LogicTrigger_CampExit.SetEnabled(false);

                stepB_Objects_LogicTrigger_CampExit.OutStep = 1000;
            }
            #endregion

            #endregion

            // C

            #region StepC_Ally_ChainJobGroup_Start

            #region 1 Start
            if (stepC_Ally_ChainJobGroup_Start.outStep == 1)
            {
                stepC_Ally_ChainJobGroup_Start.Init_SetNewGlobalLogicIndex(allyChainJobsGlobalLogicIndex);
                stepC_Ally_ChainJobGroup_Start.StartIt();

                stepC_Ally_ChainJobGroup_Start.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepC_Ally_ChainJobGroup_Start.outStep == 1.1f)
            {
                stepC_Ally_ChainJobGroup_Start.RunIt();
            }
            #endregion

            #region 900 Start finishing
            if (stepC_Ally_ChainJobGroup_Start.outStep == 900f)
            {
                stepC_Ally_ChainJobGroup_Start.SetNeedsToBeFinished();

                stepC_Ally_ChainJobGroup_Start.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (stepC_Ally_ChainJobGroup_Start.outStep == 901f)
            {
                stepC_Ally_ChainJobGroup_Start.RunIt();

                if (stepC_Ally_ChainJobGroup_Start.status == LogicJobStatus.Finished)
                {
                    flag_C_AllyChainJob_Finished.SetStatus(LogicFlagStatus.Active);

                    stepC_Ally_ChainJobGroup_Start.SetOutStep(1000);
                }
            }
            #endregion

            #endregion

            #region stepC_Objects_LogicTrigger_Hidden

            #region 1 Start
            if (stepC_Objects_LogicTrigger_Hidden.OutStep == 1)
            {
                stepC_Objects_LogicTrigger_Hidden.SetEnabled(true);
                stepC_Objects_LogicTrigger_Hidden.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Wait
            if (stepC_Objects_LogicTrigger_Hidden.OutStep == 1.1f)
            {
                if (stepC_Objects_LogicTrigger_Hidden.IsSomethingIn())
                {
                    stepC_Objects_LogicTrigger_Hidden.StartFinishing_OutStepIfNotFishining();
                    flag_C_SomeoneEntered_HiddenTrigger.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

            #region 900 End
            if (stepC_Objects_LogicTrigger_Hidden.OutStep == 900)
            {
                stepC_Objects_LogicTrigger_Hidden.SetEnabled(false);

                stepC_Objects_LogicTrigger_Hidden.OutStep = 1000;
            }
            #endregion

            #endregion

            #region stepC_Enemy_FightInRegsGroup_Hidden

            #region 1 Start
            if (stepC_Enemy_FightInRegsGroup_Hidden.outStep == 1)
            {
                stepC_Enemy_FightInRegsGroup_Hidden.StartIt();

                stepC_Enemy_FightInRegsGroup_Hidden.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepC_Enemy_FightInRegsGroup_Hidden.outStep == 1.1f)
            {
                stepC_Enemy_FightInRegsGroup_Hidden.RunIt();

                if (stepC_Enemy_FightInRegsGroup_Hidden.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    stepC_Enemy_FightInRegsGroup_Hidden.StartFinishing_OutStepIfNotFinishing();

                    goto stepC_Enemy_FightInRegsGroup_Hidden_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (stepC_Enemy_FightInRegsGroup_Hidden.outStep == 900f)
            {
                stepC_Enemy_FightInRegsGroup_Hidden.SetNeedsToBeFinished();
                stepC_Enemy_FightInRegsGroup_Hidden.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (stepC_Enemy_FightInRegsGroup_Hidden.outStep == 901f)
            {
                stepC_Enemy_FightInRegsGroup_Hidden.RunIt();

                if (stepC_Enemy_FightInRegsGroup_Hidden.status == LogicJobStatus.Finished)
                {
                    stepC_Enemy_FightInRegsGroup_Hidden.SetOutStep(1000f);

                    flag_C_Hidden_FightInRegs_Finished.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

        stepC_Enemy_FightInRegsGroup_Hidden_End: ;

            #endregion

            #region stepC_Objects_LogicTrigger_Hidden_Part02

            #region 1 Start
            if (stepC_Objects_LogicTrigger_Hidden_Part02.OutStep == 1)
            {
                stepC_Objects_LogicTrigger_Hidden_Part02.SetEnabled(true);
                stepC_Objects_LogicTrigger_Hidden_Part02.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Wait
            if (stepC_Objects_LogicTrigger_Hidden_Part02.OutStep == 1.1f)
            {
                if (stepC_Objects_LogicTrigger_Hidden_Part02.IsSomethingIn())
                {
                    stepC_Objects_LogicTrigger_Hidden_Part02.StartFinishing_OutStepIfNotFishining();
                    flag_C_SomeoneEntered_HiddenTriggerPart02.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

            #region 900 End
            if (stepC_Objects_LogicTrigger_Hidden_Part02.OutStep == 900)
            {
                stepC_Objects_LogicTrigger_Hidden_Part02.SetEnabled(false);

                stepC_Objects_LogicTrigger_Hidden_Part02.SetOutStep(1000);
            }
            #endregion

            #endregion

            #region stepC_Enemy_FightInRegsGroup_Hidden_Part02

            #region 1 Start
            if (stepC_Enemy_FightInRegsGroup_Hidden_Part02.outStep == 1)
            {
                stepC_Enemy_FightInRegsGroup_Hidden_Part02.StartIt();

                stepC_Enemy_FightInRegsGroup_Hidden_Part02.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepC_Enemy_FightInRegsGroup_Hidden_Part02.outStep == 1.1f)
            {
                stepC_Enemy_FightInRegsGroup_Hidden_Part02.RunIt();

                if (stepC_Enemy_FightInRegsGroup_Hidden_Part02.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    stepC_Enemy_FightInRegsGroup_Hidden_Part02.StartFinishing_OutStepIfNotFinishing();

                    goto stepC_Enemy_FightInRegsGroup_Hidden_Part02_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (stepC_Enemy_FightInRegsGroup_Hidden_Part02.outStep == 900f)
            {
                stepC_Enemy_FightInRegsGroup_Hidden_Part02.SetNeedsToBeFinished();
                stepC_Enemy_FightInRegsGroup_Hidden_Part02.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (stepC_Enemy_FightInRegsGroup_Hidden_Part02.outStep == 901f)
            {
                stepC_Enemy_FightInRegsGroup_Hidden_Part02.RunIt();

                if (stepC_Enemy_FightInRegsGroup_Hidden_Part02.status == LogicJobStatus.Finished)
                {
                    stepC_Enemy_FightInRegsGroup_Hidden_Part02.SetOutStep(1000f);

                    flag_C_HiddenPart2_FightInRegs_Finished.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

        stepC_Enemy_FightInRegsGroup_Hidden_Part02_End: ;

            #endregion

            #region stepC_Objects_ExitTrigger_Hidden_Part02

            #region 1 Start
            if (stepC_Objects_ExitTrigger_Hidden_Part02.OutStep == 1)
            {
                stepC_Objects_ExitTrigger_Hidden_Part02.SetEnabled(true);
                stepC_Objects_ExitTrigger_Hidden_Part02.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Wait
            if (stepC_Objects_ExitTrigger_Hidden_Part02.OutStep == 1.1f)
            {
                if (stepC_Objects_ExitTrigger_Hidden_Part02.IsSomethingIn())
                {
                    stepC_Objects_ExitTrigger_Hidden_Part02.StartFinishing_OutStepIfNotFishining();
                    flag_C_SomeoneEntered_ExitTriggerPart02.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

            #region 900 End
            if (stepC_Objects_ExitTrigger_Hidden_Part02.OutStep == 900)
            {
                stepC_Objects_ExitTrigger_Hidden_Part02.SetEnabled(false);

                stepC_Objects_ExitTrigger_Hidden_Part02.SetOutStep(1000);
            }
            #endregion

            #endregion

            #region stepC_Objects_LogicTrigger_SnipeHillCutscene

            #region 1 Start
            if (stepC_Objects_LogicTrigger_SnipeHillCutscene.OutStep == 1)
            {
                stepC_Objects_LogicTrigger_SnipeHillCutscene.SetEnabled(true);
                stepC_Objects_LogicTrigger_SnipeHillCutscene.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Wait
            if (stepC_Objects_LogicTrigger_SnipeHillCutscene.OutStep == 1.1f)
            {
                if (stepC_Objects_LogicTrigger_SnipeHillCutscene.IsSomethingIn())
                {
                    stepC_Objects_LogicTrigger_SnipeHillCutscene.StartFinishing_OutStepIfNotFishining();
                    flag_C_SomeoneEntered_LogicTrigger_SnipeHillCutscene.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

            #region 900 End
            if (stepC_Objects_LogicTrigger_SnipeHillCutscene.OutStep == 900)
            {
                stepC_Objects_LogicTrigger_SnipeHillCutscene.SetEnabled(false);

                stepC_Objects_LogicTrigger_SnipeHillCutscene.SetOutStep(1000);
            }
            #endregion

            #endregion

            // D

            #region StepD_Ally_ChainJobGroup_Start

            #region 1 Start
            if (stepD_Ally_ChainJobGroup_Start.outStep == 1)
            {
                stepD_Ally_ChainJobGroup_Start.Init_SetNewGlobalLogicIndex(allyChainJobsGlobalLogicIndex);
                stepD_Ally_ChainJobGroup_Start.StartIt();

                stepD_Ally_ChainJobGroup_Start.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepD_Ally_ChainJobGroup_Start.outStep == 1.1f)
            {
                stepD_Ally_ChainJobGroup_Start.RunIt();
            }
            #endregion

            #region 900 Start finishing
            if (stepD_Ally_ChainJobGroup_Start.outStep == 900f)
            {
                stepD_Ally_ChainJobGroup_Start.SetNeedsToBeFinished();

                stepD_Ally_ChainJobGroup_Start.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (stepD_Ally_ChainJobGroup_Start.outStep == 901f)
            {
                stepD_Ally_ChainJobGroup_Start.RunIt();

                if (stepD_Ally_ChainJobGroup_Start.status == LogicJobStatus.Finished)
                {
                    flag_D_AllyChainJob_Finished.SetStatus(LogicFlagStatus.Active);

                    stepD_Ally_ChainJobGroup_Start.SetOutStep(1000);
                }
            }
            #endregion

            #endregion

            #region stepD_Enemy_FightInRegsGroup_Kolli

            #region 1 Start
            if (stepD_Enemy_FightInRegsGroup_Kolli.outStep == 1)
            {
                stepD_Enemy_FightInRegsGroup_Kolli.StartIt();

                stepD_Enemy_FightInRegsGroup_Kolli.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepD_Enemy_FightInRegsGroup_Kolli.outStep == 1.1f)
            {
                stepD_Enemy_FightInRegsGroup_Kolli.RunIt();

                if (stepD_Enemy_FightInRegsGroup_Kolli.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    stepD_Enemy_FightInRegsGroup_Kolli.StartFinishing_OutStepIfNotFinishing();

                    goto stepD_Enemy_FightInRegsGroup_Kolli_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (stepD_Enemy_FightInRegsGroup_Kolli.outStep == 900f)
            {
                stepD_Enemy_FightInRegsGroup_Kolli.SetNeedsToBeFinished();
                stepD_Enemy_FightInRegsGroup_Kolli.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (stepD_Enemy_FightInRegsGroup_Kolli.outStep == 901f)
            {
                stepD_Enemy_FightInRegsGroup_Kolli.RunIt();

                if (stepD_Enemy_FightInRegsGroup_Kolli.status == LogicJobStatus.Finished)
                {
                    stepD_Enemy_FightInRegsGroup_Kolli.SetOutStep(1000f);

                    flag_D_Kolli_FightInRegs_Finished.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

        stepD_Enemy_FightInRegsGroup_Kolli_End: ;

            #endregion

            #region stepD_Enemy_FightInRegsGroup_Jelo

            #region 1 Start
            if (stepD_Enemy_FightInRegsGroup_Jelo.outStep == 1)
            {
                stepD_Enemy_FightInRegsGroup_Jelo.StartIt();

                stepD_Enemy_FightInRegsGroup_Jelo.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepD_Enemy_FightInRegsGroup_Jelo.outStep == 1.1f)
            {
                stepD_Enemy_FightInRegsGroup_Jelo.RunIt();

                if (stepD_Enemy_FightInRegsGroup_Jelo.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    stepD_Enemy_FightInRegsGroup_Jelo.StartFinishing_OutStepIfNotFinishing();

                    goto stepD_Enemy_FightInRegsGroup_Jelo_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (stepD_Enemy_FightInRegsGroup_Jelo.outStep == 900f)
            {
                stepD_Enemy_FightInRegsGroup_Jelo.SetNeedsToBeFinished();
                stepD_Enemy_FightInRegsGroup_Jelo.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (stepD_Enemy_FightInRegsGroup_Jelo.outStep == 901f)
            {
                stepD_Enemy_FightInRegsGroup_Jelo.RunIt();

                if (stepD_Enemy_FightInRegsGroup_Jelo.status == LogicJobStatus.Finished)
                {
                    stepD_Enemy_FightInRegsGroup_Jelo.SetOutStep(1000f);

                    flag_D_Jelo_FightInRegs_Finished.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

        stepD_Enemy_FightInRegsGroup_Jelo_End: ;

            #endregion

            #region stepD_Objects_LogicTrigger_NearEnemies

            #region 1 Start
            if (stepD_Objects_LogicTrigger_NearEnemies.OutStep == 1)
            {
                stepD_Objects_LogicTrigger_NearEnemies.SetEnabled(true);
                stepD_Objects_LogicTrigger_NearEnemies.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Wait
            if (stepD_Objects_LogicTrigger_NearEnemies.OutStep == 1.1f)
            {
                if (stepD_Objects_LogicTrigger_NearEnemies.IsInsideObjsCountEqualOrBiggerThanValue(stepD_MinNumOfNearEnemies))
                {
                    stepD_Objects_LogicTrigger_NearEnemies.StartFinishing_OutStepIfNotFishining();
                    flag_D_NearEnemiesAreFullIn_LogicTrigger_NearEnemies.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

            #region 900 End
            if (stepD_Objects_LogicTrigger_NearEnemies.OutStep == 900)
            {
                stepD_Objects_LogicTrigger_NearEnemies.SetEnabled(false);

                stepD_Objects_LogicTrigger_NearEnemies.SetOutStep(1000);
            }
            #endregion

            #endregion

            #region stepD_Enemy_FightInRegsGroup_Posht

            #region 1 Start
            if (stepD_Enemy_FightInRegsGroup_Posht.outStep == 1)
            {
                stepD_Enemy_FightInRegsGroup_Posht.StartIt();

                stepD_Enemy_FightInRegsGroup_Posht.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepD_Enemy_FightInRegsGroup_Posht.outStep == 1.1f)
            {
                stepD_Enemy_FightInRegsGroup_Posht.RunIt();

                if (stepD_Enemy_FightInRegsGroup_Posht.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    stepD_Enemy_FightInRegsGroup_Posht.StartFinishing_OutStepIfNotFinishing();

                    goto stepD_Enemy_FightInRegsGroup_Posht_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (stepD_Enemy_FightInRegsGroup_Posht.outStep == 900f)
            {
                stepD_Enemy_FightInRegsGroup_Posht.SetNeedsToBeFinished();
                stepD_Enemy_FightInRegsGroup_Posht.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (stepD_Enemy_FightInRegsGroup_Posht.outStep == 901f)
            {
                stepD_Enemy_FightInRegsGroup_Posht.RunIt();

                if (stepD_Enemy_FightInRegsGroup_Posht.status == LogicJobStatus.Finished)
                {
                    stepD_Enemy_FightInRegsGroup_Posht.SetOutStep(1000f);

                    flag_D_Posht_FightInRegs_Finished.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

        stepD_Enemy_FightInRegsGroup_Posht_End: ;

            #endregion

            #region stepD_Objects_ExitTrigger_JeloRush

            #region 1 Start
            if (stepD_Objects_ExitTrigger_JeloRush.OutStep == 1)
            {
                stepD_Objects_ExitTrigger_JeloRush.SetEnabled(true);
                stepD_Objects_ExitTrigger_JeloRush.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Wait
            if (stepD_Objects_ExitTrigger_JeloRush.OutStep == 1.1f)
            {
                if (stepD_Objects_ExitTrigger_JeloRush.IsPlayerIn())
                {
                    stepD_Objects_ExitTrigger_JeloRush.StartFinishing_OutStepIfNotFishining();
                    flag_D_PlayerEntered_JeloRushExitTrigger.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

            #region 900 End
            if (stepD_Objects_ExitTrigger_JeloRush.OutStep == 900)
            {
                stepD_Objects_ExitTrigger_JeloRush.SetEnabled(false);

                stepD_Objects_ExitTrigger_JeloRush.SetOutStep(1000);
            }
            #endregion

            #endregion

            #region stepD_Objects_ExitTrigger_PoshtRush

            #region 1 Start
            if (stepD_Objects_ExitTrigger_PoshtRush.OutStep == 1)
            {
                stepD_Objects_ExitTrigger_PoshtRush.SetEnabled(true);
                stepD_Objects_ExitTrigger_PoshtRush.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Wait
            if (stepD_Objects_ExitTrigger_PoshtRush.OutStep == 1.1f)
            {
                if (stepD_Objects_ExitTrigger_PoshtRush.IsPlayerIn())
                {
                    stepD_Objects_ExitTrigger_PoshtRush.StartFinishing_OutStepIfNotFishining();
                    flag_D_PlayerEntered_PoshtRushExitTrigger.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

            #region 900 End
            if (stepD_Objects_ExitTrigger_PoshtRush.OutStep == 900)
            {
                stepD_Objects_ExitTrigger_PoshtRush.SetEnabled(false);

                stepD_Objects_ExitTrigger_PoshtRush.SetOutStep(1000);
            }
            #endregion

            #endregion

            #region stepD_Objects_Trigger_PoshtRush_StartKillingRemainingEnemies

            #region 1 Start
            if (stepD_Objects_Trigger_PoshtRush_StartKillingRemainingEnemies.OutStep == 1)
            {
                stepD_Objects_Trigger_PoshtRush_StartKillingRemainingEnemies.SetEnabled(true);
                stepD_Objects_Trigger_PoshtRush_StartKillingRemainingEnemies.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Wait
            if (stepD_Objects_Trigger_PoshtRush_StartKillingRemainingEnemies.OutStep == 1.1f)
            {
                if (stepD_Objects_Trigger_PoshtRush_StartKillingRemainingEnemies.IsPlayerIn())
                {
                    stepD_Objects_Trigger_PoshtRush_StartKillingRemainingEnemies.StartFinishing_OutStepIfNotFishining();
                    flag_D_PlayerEntered_PoshtStartKillingRemainingEnemiesTrigger.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

            #region 900 End
            if (stepD_Objects_Trigger_PoshtRush_StartKillingRemainingEnemies.OutStep == 900)
            {
                stepD_Objects_Trigger_PoshtRush_StartKillingRemainingEnemies.SetEnabled(false);

                stepD_Objects_Trigger_PoshtRush_StartKillingRemainingEnemies.SetOutStep(1000);
            }
            #endregion

            #endregion

            //

            #region Logic Trigger "Enemies_A"

            #region 1 Start
            if (stepE_Objects_LogicTrigger_EnemyStart_A.OutStep == 1)
            {
                stepE_Objects_LogicTrigger_EnemyStart_A.SetEnabled(true);
                stepE_Objects_LogicTrigger_EnemyStart_A.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Wait
            if (stepE_Objects_LogicTrigger_EnemyStart_A.OutStep == 1.1f)
            {
                if (stepE_Objects_LogicTrigger_EnemyStart_A.IsSomethingIn())
                {
                    stepE_Objects_LogicTrigger_EnemyStart_A.StartFinishing_OutStepIfNotFishining();
                    flag_E_SomeoneEntered_EnemyStartTrigger_A.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

            #region 900 End
            if (stepE_Objects_LogicTrigger_EnemyStart_A.OutStep == 900)
            {
                stepE_Objects_LogicTrigger_EnemyStart_A.SetEnabled(false);

                stepE_Objects_LogicTrigger_EnemyStart_A.OutStep = 1000;
            }
            #endregion

            #endregion

            #region Logic Trigger "Enemies_B"

            #region 1 Start
            if (stepE_Objects_LogicTrigger_EnemyStart_B.OutStep == 1)
            {
                stepE_Objects_LogicTrigger_EnemyStart_B.SetEnabled(true);
                stepE_Objects_LogicTrigger_EnemyStart_B.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Wait
            if (stepE_Objects_LogicTrigger_EnemyStart_B.OutStep == 1.1f)
            {
                if (stepE_Objects_LogicTrigger_EnemyStart_B.IsSomethingIn())
                {
                    stepE_Objects_LogicTrigger_EnemyStart_B.StartFinishing_OutStepIfNotFishining();
                    flag_E_SomeoneEntered_EnemyStartTrigger_B.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

            #region 900 End
            if (stepE_Objects_LogicTrigger_EnemyStart_B.OutStep == 900)
            {
                stepE_Objects_LogicTrigger_EnemyStart_B.SetEnabled(false);

                stepE_Objects_LogicTrigger_EnemyStart_B.OutStep = 1000;
            }
            #endregion

            #endregion

            #region Logic Trigger "Enemies_C"

            #region 1 Start
            if (stepE_Objects_LogicTrigger_EnemyStart_C.OutStep == 1)
            {
                stepE_Objects_LogicTrigger_EnemyStart_C.SetEnabled(true);
                stepE_Objects_LogicTrigger_EnemyStart_C.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Wait
            if (stepE_Objects_LogicTrigger_EnemyStart_C.OutStep == 1.1f)
            {
                if (stepE_Objects_LogicTrigger_EnemyStart_C.IsSomethingIn())
                {
                    stepE_Objects_LogicTrigger_EnemyStart_C.StartFinishing_OutStepIfNotFishining();
                    flag_E_SomeoneEntered_EnemyStartTrigger_C.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

            #region 900 End
            if (stepE_Objects_LogicTrigger_EnemyStart_C.OutStep == 900)
            {
                stepE_Objects_LogicTrigger_EnemyStart_C.SetEnabled(false);

                stepE_Objects_LogicTrigger_EnemyStart_C.OutStep = 1000;
            }
            #endregion

            #endregion

            #region Logic Trigger "TheEnd"

            #region 1 Start
            if (stepE_Objects_LogicTrigger_TheEnd.OutStep == 1)
            {
                stepE_Objects_LogicTrigger_TheEnd.SetEnabled(true);
                stepE_Objects_LogicTrigger_TheEnd.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Wait
            if (stepE_Objects_LogicTrigger_TheEnd.OutStep == 1.1f)
            {
                if (stepE_Objects_LogicTrigger_TheEnd.IsSomethingIn())
                {
                    stepE_Objects_LogicTrigger_TheEnd.StartFinishing_OutStepIfNotFishining();
                    flag_E_PlayerEntered_TheEndTrigger.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

            #region 900 End
            if (stepE_Objects_LogicTrigger_TheEnd.OutStep == 900)
            {
                stepE_Objects_LogicTrigger_TheEnd.SetEnabled(false);

                stepE_Objects_LogicTrigger_TheEnd.OutStep = 1000;
            }
            #endregion

            #endregion

            #region stepE_Ally_ChainJobGroup

            #region 1 Start
            if (stepE_Ally_ChainJobGroup.outStep == 1)
            {
                stepE_Ally_ChainJobGroup.Init_SetNewGlobalLogicIndex(allyChainJobsGlobalLogicIndex);
                stepE_Ally_ChainJobGroup.StartIt();

                stepE_Ally_ChainJobGroup.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepE_Ally_ChainJobGroup.outStep == 1.1f)
            {
                stepE_Ally_ChainJobGroup.RunIt();
            }
            #endregion

            #region 900 Start finishing
            if (stepE_Ally_ChainJobGroup.outStep == 900f)
            {
                stepE_Ally_ChainJobGroup.SetNeedsToBeFinished();

                stepE_Ally_ChainJobGroup.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (stepE_Ally_ChainJobGroup.outStep == 901f)
            {
                stepE_Ally_ChainJobGroup.RunIt();

                if (stepE_Ally_ChainJobGroup.status == LogicJobStatus.Finished)
                {
                    flag_E_AllyChainJob_Finished.SetStatus(LogicFlagStatus.Active);

                    stepE_Ally_ChainJobGroup.SetOutStep(1000);
                }
            }
            #endregion

            #endregion

            #region stepE_Enemy_FightInRegsGroup_A

            #region 1 Start
            if (stepE_Enemy_FightInRegsGroup_A.outStep == 1)
            {
                stepE_Enemy_FightInRegsGroup_A.StartIt();

                stepE_Enemy_FightInRegsGroup_A.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepE_Enemy_FightInRegsGroup_A.outStep == 1.1f)
            {
                stepE_Enemy_FightInRegsGroup_A.RunIt();

                if (stepE_Enemy_FightInRegsGroup_A.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    stepE_Enemy_FightInRegsGroup_A.StartFinishing_OutStepIfNotFinishing();

                    goto stepE_Enemy_FightInRegsGroup_A_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (stepE_Enemy_FightInRegsGroup_A.outStep == 900f)
            {
                stepE_Enemy_FightInRegsGroup_A.SetNeedsToBeFinished();
                stepE_Enemy_FightInRegsGroup_A.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (stepE_Enemy_FightInRegsGroup_A.outStep == 901f)
            {
                stepE_Enemy_FightInRegsGroup_A.RunIt();

                if (stepE_Enemy_FightInRegsGroup_A.status == LogicJobStatus.Finished)
                {
                    stepE_Enemy_FightInRegsGroup_A.SetOutStep(1000f);

                    flag_E_Enemies_A_Finished.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

        stepE_Enemy_FightInRegsGroup_A_End: ;

            #endregion

            #region stepE_Enemy_FightInRegsGroup_B

            #region 1 Start
            if (stepE_Enemy_FightInRegsGroup_B.outStep == 1)
            {
                stepE_Enemy_FightInRegsGroup_B.StartIt();

                stepE_Enemy_FightInRegsGroup_B.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepE_Enemy_FightInRegsGroup_B.outStep == 1.1f)
            {
                stepE_Enemy_FightInRegsGroup_B.RunIt();

                if (stepE_Enemy_FightInRegsGroup_B.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    stepE_Enemy_FightInRegsGroup_B.StartFinishing_OutStepIfNotFinishing();

                    goto stepE_Enemy_FightInRegsGroup_B_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (stepE_Enemy_FightInRegsGroup_B.outStep == 900f)
            {
                stepE_Enemy_FightInRegsGroup_B.SetNeedsToBeFinished();
                stepE_Enemy_FightInRegsGroup_B.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (stepE_Enemy_FightInRegsGroup_B.outStep == 901f)
            {
                stepE_Enemy_FightInRegsGroup_B.RunIt();

                if (stepE_Enemy_FightInRegsGroup_B.status == LogicJobStatus.Finished)
                {
                    stepE_Enemy_FightInRegsGroup_B.SetOutStep(1000f);

                    flag_E_Enemies_B_Finished.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

        stepE_Enemy_FightInRegsGroup_B_End: ;

            #endregion

            #region stepE_Enemy_FightInRegsGroup_C

            #region 1 Start
            if (stepE_Enemy_FightInRegsGroup_C.outStep == 1)
            {
                stepE_Enemy_FightInRegsGroup_C.StartIt();

                stepE_Enemy_FightInRegsGroup_C.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepE_Enemy_FightInRegsGroup_C.outStep == 1.1f)
            {
                stepE_Enemy_FightInRegsGroup_C.RunIt();

                if (stepE_Enemy_FightInRegsGroup_C.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    stepE_Enemy_FightInRegsGroup_C.StartFinishing_OutStepIfNotFinishing();

                    goto stepE_Enemy_FightInRegsGroup_C_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (stepE_Enemy_FightInRegsGroup_C.outStep == 900f)
            {
                stepE_Enemy_FightInRegsGroup_C.SetNeedsToBeFinished();
                stepE_Enemy_FightInRegsGroup_C.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (stepE_Enemy_FightInRegsGroup_C.outStep == 901f)
            {
                stepE_Enemy_FightInRegsGroup_C.RunIt();

                if (stepE_Enemy_FightInRegsGroup_C.status == LogicJobStatus.Finished)
                {
                    stepE_Enemy_FightInRegsGroup_C.SetOutStep(1000f);

                    flag_E_Enemies_C_Finished.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

        stepE_Enemy_FightInRegsGroup_C_End: ;

            #endregion
        }
    }

    void D_CheckShouldMakeOneOfAlliesAttackableIfHeIsNot()
    {
        if (!flag_D_OneOfAlliesIsAttckable.IsEverActivated() && flag_D_NearEnemiesAreFullIn_LogicTrigger_NearEnemies.IsEverActivated())
        {
            flag_D_OneOfAlliesIsAttckable.SetStatus(LogicFlagStatus.Active);

            float num = Random.Range(0f, 1f);

            if (num <= 0.53f)
            {
                ally01CharInfo.SetRecievedDamageCoefMax();
            }

            if (num >= 0.47f)
            {
                ally02CharInfo.SetRecievedDamageCoefMax();
            }
        }
    }

    void D_CheckShouldStartFrontDieTrigger_IfItsNotStartedEver()
    {
        if ((stepD_DieTriggerFrontDelay == 0) && (stepD_Enemy_FightInRegsGroup_Jelo.countOfRemainingSolds <= stepD_MaxNumOfFrontEnemiesToStartDieTrigger))
            stepD_Objects_LogicDieTrigger_Front.StartItIfItsNotStartedBefore();
    }

    void D_MakeAlliesUndiable()
    {
        ally01CharInfo.SetRecievedDamageCoefZero();
        ally02CharInfo.SetRecievedDamageCoefZero();
    }

    bool IsAnyAllyDead()
    {
        if (ally01CharInfo == null)
            return true;

        if (ally01CharInfo.IsDead)
            return true;

        if (ally02CharInfo == null)
            return true;

        if (ally02CharInfo.IsDead)
            return true;

        return false;
    }

    //

    public override void LoadCheckPoint(float _levelStep)
    {
        base.LoadCheckPoint(_levelStep);

        #region A+1
        if (levelStep == 2)
        {
            stepA1_Objects_StartPoint_Player.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);
            stepA1_Objects_StartPoint_Ally01.PlaceCharacterOnIt(ally01);
            stepA1_Objects_StartPoint_Ally02.PlaceCharacterOnIt(ally02);

            return;
        }
        #endregion

        #region B
        if (levelStep == 3)
        {
            stepB_Objects_StartPoint_Player.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);
            stepB_Objects_StartPoint_Ally01.PlaceCharacterOnIt(ally01);
            stepB_Objects_StartPoint_Ally02.PlaceCharacterOnIt(ally02);

            return;
        }
        #endregion

        #region B+1
        if (levelStep == 4)
        {
            LoadFunc_B_CampTower();

            stepB1_Objects_StartPoint_Player.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);
            stepB1_Objects_StartPoint_Ally01.PlaceCharacterOnIt(ally01);
            stepB1_Objects_StartPoint_Ally02.PlaceCharacterOnIt(ally02);

            return;
        }
        #endregion

        #region C
        if (levelStep == 5)
        {
            LoadFunc_B_CampTower();

            stepC_Objects_StartPoint_Player.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);
            stepC_Objects_StartPoint_Ally01.PlaceCharacterOnIt(ally01);
            stepC_Objects_StartPoint_Ally02.PlaceCharacterOnIt(ally02);

            return;
        }
        #endregion

        #region D
        if (levelStep == 6)
        {
            LoadFunc_B_CampTower();

            D_PlaceSolds();

            return;
        }
        #endregion

        #region E
        if (levelStep == 7)
        {
            LoadFunc_B_CampTower();

            E_PlaceSolds();

            return;
        }
        #endregion

    }

    void LoadFunc_B_CampTower()
    {
        for (int i = 0; i < stepB_Objects_TowerExplodableBarrels.Length; i++)
        {
            Destroy(stepB_Objects_TowerExplodableBarrels[i]);
        }

        stepB_Objects_Tower.animation.Play("takhrib");
        stepB_Objects_Tower.animation["takhrib"].time = stepB_Objects_Tower.animation["takhrib"].length;

        stepB_Objects_Tower_RemovableCollider.enabled = false;
        stepB_Objects_Tower_RemovableCollider.active = false;
    }

    void D_PlaceSolds()
    {
        stepD_Objects_StartPoint_Player.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);
        stepD_Objects_StartPoint_Ally01.PlaceCharacterOnIt(ally01);
        stepD_Objects_StartPoint_Ally02.PlaceCharacterOnIt(ally02);
    }

    void E_PlaceSolds()
    {
        stepE_Objects_StartPoint_Player.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);
        stepE_Objects_StartPoint_Ally01.PlaceCharacterOnIt(ally01);
        stepE_Objects_StartPoint_Ally02.PlaceCharacterOnIt(ally02);
    }

    void PlayFirstMusic()
    {
        MusicController.Instance.PlayMusic(MusicSong.Action_E, 0);
    }
}
