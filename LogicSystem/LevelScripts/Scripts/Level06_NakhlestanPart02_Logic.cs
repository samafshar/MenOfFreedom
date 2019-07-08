using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level06_NakhlestanPart02_Logic : LevelLogic
{
    public CutsceneController cutscene_CrowdHappy;
    public CutsceneController cutScene_KhalooShirKhorde;

    public GameObject Ally01;
    public GameObject Ally02;
    public GameObject Ally03;
    public GameObject Ally04;

    public LogicVoiceCollection logicVoiceCollection_Ally01;
    public LogicVoiceCollection logicVoiceCollection_Ally02;
    public LogicVoiceCollection logicVoiceCollection_Ally03;
    public LogicVoiceCollection logicVoiceCollection_Ally04;

    public GameObject particlesPart01;
    public GameObject particlesPart02;
    public GameObject particlesPart03;
    public GameObject particlesPart04;

    public Transform miniMapObjectTransform_01_StepA_01;
    public Transform miniMapObjectTransform_02_StepA_02;
    public Transform miniMapObjectTransform_03_StepA_03;
    public Transform miniMapObjectTransform_04_StepA_04;
    public Transform miniMapObjectTransform_05_StepA_05;
    public Transform miniMapObjectTransform_06_StepB_01;
    public Transform miniMapObjectTransform_07_StepB_02;
    public Transform miniMapObjectTransform_08_StepB_03;
    public Transform miniMapObjectTransform_09_StepB_04;
    public Transform miniMapObjectTransform_10_StepB_05;
    public Transform miniMapObjectTransform_11_StepB_06;
    public Transform miniMapObjectTransform_12_StepB_07;

    #region Step_A Variables
    //Enemy

    //Ally
    public GameObject step_A_Ally_01_BurningMan01;
    public GameObject step_A_Ally_02_BurningMan02;

    //Objects
    public LogicTrigger step_A_Objects_01_StartingLevelTrigger;
    public LogicTrigger step_A_Objects_02_Part2MortarsTrigger;
    public LogicTrigger step_A_Objects_03_BurningManTrigger;
    public LogicTrigger step_A_Objects_03_Part3MortarsTrigger;
    public LogicTrigger step_A_Objects_03_BackFromPart3Trigger;
    public LogicTrigger step_A_Objects_04_Part4MortarsTrigger;
    public LogicTrigger step_A_Objects_04_BackFromPart4Trigger;
    public LogicTrigger step_A_Objects_05_BurningMan02Trigger;
    public LogicTrigger step_A_Objects_05_Part5MortarsTrigger;
    public LogicTrigger step_A_Objects_05_BackFromPart5Trigger;
    public LogicTrigger step_A_Objects_06_ExitTrigger;
    public GameObject step_A_Objects_07_AnimatedFallingTree;
    public GameObject step_A_Objects_08_ColliderExit;
    public GameObject step_A_Objects_08_ColliderExit_B;
    public Mortar step_A_Objects_09_Mortar;

    public MortarHeadQuarter step_A_Objects_MortarHQ_Part00;
    public MortarHeadQuarter step_A_Objects_MortarHQ_Part01;
    public MortarHeadQuarter step_A_Objects_MortarHQ_Part02;
    public MortarHeadQuarter step_A_Objects_MortarHQ_Part03;
    public MortarHeadQuarter step_A_Objects_MortarHQ_Part04;
    public MortarHeadQuarter step_A_Objects_MortarHQ_Part05;

    public int step_A_DeathChanceWhenBackFromStep3 = 20;
    public int step_A_DeathChanceWhenBackFromStep4 = 25;
    public int step_A_DeathChanceWhenBackFromStep5 = 35;

    public StartPoint step_A_Objects_StartPoint_Player;

    LogicFlag step_A_PlayerEntered_StartingLevelTrigger = new LogicFlag();
    LogicFlag step_A_PlayerEntered_Part2MortarsTrigger = new LogicFlag();
    LogicFlag step_A_PlayerEntered_BurningManTrigger = new LogicFlag();
    LogicFlag step_A_PlayerEntered_Part3MortarsTrigger = new LogicFlag();
    LogicFlag step_A_PlayerEntered_BackFromPart3MortarsTrigger = new LogicFlag();
    LogicFlag step_A_PlayerEntered_Part4MortarsTrigger = new LogicFlag();
    LogicFlag step_A_PlayerEntered_BackFromPart4MortarsTrigger = new LogicFlag();
    LogicFlag step_A_PlayerEntered_BurningMan02Trigger = new LogicFlag();
    LogicFlag step_A_PlayerEntered_Part5MortarsTrigger = new LogicFlag();
    LogicFlag step_A_PlayerEntered_BackFromPart5MortarsTrigger = new LogicFlag();
    LogicFlag step_A_PlayerEntered_ExitTrigger = new LogicFlag();

    bool step_A_IsBurningMan01Start = false;
    bool step_A_IsBurningMan02Start = false;

    #endregion

    public string _______________;

    #region Step_B Variables
    //Ally
    public MapLogicJob_ChainJobsGroup step_B_Ally_ChainJobGroup_01_Ally01;
    public MapLogicJob_ChainJobsGroup step_B_Ally_ChainJobGroup_02_Ally02;
    public MapLogicJob_ChainJobsGroup step_B_Ally_ChainJobGroup_03_Ally03;
    public MapLogicJob_ChainJobsGroup step_B_Ally_ChainJobGroup_04_Ally04;

    //Objects
    public LogicTrigger step_B_Objects_01_Part2MortarsTrigger;
    public LogicTrigger step_B_Objects_02_FallingPalmTrigger;
    public GameObject step_B_Objects_03_FallingPalm;
    public Mortar step_B_Objects_04_Mortar;
    public LogicTrigger step_B_Objects_05_ExplodeHouseTrigger;
    public GameObject step_B_Objects_06_MortarsForHouse;
    public GameObject step_B_Objects_06_ParticlesForHouse;
    public LogicTrigger step_B_Objects_07_Part3MortarsTrigger;
    public LogicTrigger step_B_Objects_08_Part4MortarsTrigger;
    public LogicTrigger step_B_Objects_09_BackFromPart4Trigger;
    public LogicTrigger step_B_Objects_10_ExitFromMortarsTrigger;
    public GameObject step_B_Objects_11_SmokeDenseRoot;
    public GameObject step_B_Objects_12_FinalSmokeRoot;
    public LogicTrigger step_B_Objects_13_BackFromExitTrigger;
    public LogicTrigger step_B_Objects_14_AlliesStartsTrigger;
    public LogicTrigger step_B_Objects_15_BackFromAlliesStartTrigger;
    public LogicTrigger step_B_Objects_16_EndStepTrigger;

    public MortarHeadQuarter step_B_Objects_MortarHQ_Part_01;
    public MortarHeadQuarter step_B_Objects_MortarHQ_Part_02;
    public MortarHeadQuarter step_B_Objects_MortarHQ_Part_03;
    public MortarHeadQuarter step_B_Objects_MortarHQ_Part_04;

    public float step_B_MaxDelayForHouseParticles = 1f;

    public int step_B_DeathChanceWhenBackFromStep4 = 20;
    public int step_B_DeathChanceWhenBackFromExit = 35;

    public StartPoint step_B_Objects_StartPoint_Player;

    LogicFlag step_B_PlayerEntered_Part2MortarsTrigger = new LogicFlag();
    LogicFlag step_B_PlayerEntered_FallingPalmTrigger = new LogicFlag();
    LogicFlag step_B_PlayerEntered_ExplodeHouseTrigger = new LogicFlag();
    LogicFlag step_B_PlayerEntered_Part3MortarsTrigger = new LogicFlag();
    LogicFlag step_B_PlayerEntered_Part4MortarsTrigger = new LogicFlag();
    LogicFlag step_B_PlayerEntered_BackFromPart4Trigger = new LogicFlag();
    LogicFlag step_B_PlayerEntered_ExitFromMortarsTrigger = new LogicFlag();
    LogicFlag step_B_PlayerEntered_BackFromExitTrigger = new LogicFlag();
    LogicFlag step_B_PlayerEntered_AlliesStartsTrigger = new LogicFlag();
    LogicFlag step_B_PlayerEntered_BackFromAlliesTrigger = new LogicFlag();
    LogicFlag step_B_PlayerEntered_EndStepTrigger = new LogicFlag();

    float step_B_timer;

    int step_B_Ally01ChainJobsGlobalLogicIndex = -1;
    int step_B_Ally02ChainJobsGlobalLogicIndex = -1;
    int step_B_Ally03ChainJobsGlobalLogicIndex = -1;
    int step_B_Ally04ChainJobsGlobalLogicIndex = -1;

    bool step_B_IsFromLoad = false;

    #endregion

    public string _________________;

    #region Step_C Variables
    //Ally
    public MapLogicJob_ChainJobsGroup step_C_Ally_ChainJobGroup_01_Ally01;
    public MapLogicJob_ChainJobsGroup step_C_Ally_ChainJobGroup_02_Ally02;
    public MapLogicJob_ChainJobsGroup step_C_Ally_ChainJobGroup_03_Ally03;
    public MapLogicJob_ChainJobsGroup step_C_Ally_ChainJobGroup_04_Ally04;

    //Enemy
    public MapLogicJob_FightInRegsGroup step_C_Enemy_FightInRegsGroup_Static;
    public MapLogicJob_FightInRegsGroup step_C_Enemy_FightInRegsGroup_Rush;

    //Object
    public GameObject step_C_Objects_01_ColliderForDood;
    public AudioInfo step_C_Objects_01_AudioInfo_EnemysCrowd;
    public LogicTrigger step_C_Objects_01_CountingInsideSoldiersTrigger;
    public ExecutionArea step_C_Objects_01_ExecutionArea01Trigger;
    public LogicTrigger step_C_Objects_02_GoToEnemyMortarsTrigger;

    public MortarHeadQuarter step_C_Objects_MortarHQ_Part_01;

    //

    public float step_C_TimeToWaitForSoldiersCome = 2f;
    public float step_C_TimeToCreateSoldier = 20f;
    public float step_C_TimeToStartTalkingAfterMortars = 3f;
    public float step_C_TimeToStartTalkingHamaroBokoshin = 3.1f;
    public float step_C_TimeToFinishMortarsAndStartCutScene = 5f;
    public float step_C_TimeToStartCutSceneAfterAllEnemyDie = 1.5f;

    public int step_C_MinNumOfCreatedSoldier = 10;
    public int step_C_NumOfInsideTriggerSoldiersToFail = 3;

    public List<GameObject> step_C_ObjectsThatShouldntRender = new List<GameObject>();

    public StartPoint step_C_Objects_StartPoint_Player;
    public StartPoint step_C_Objects_StartPoint_Ally01;
    public StartPoint step_C_Objects_StartPoint_Ally02;
    public StartPoint step_C_Objects_StartPoint_Ally03;
    public StartPoint step_C_Objects_StartPoint_Ally04;

    //

    LogicFlag step_C_FightStatic_Finished = new LogicFlag();
    LogicFlag step_C_FightRush_Finished = new LogicFlag();
    LogicFlag step_C_CountingInsideSoldierTrigger = new LogicFlag();
    LogicFlag step_C_GoToEnemyMortarsTrigger = new LogicFlag();

    int step_C_Ally01ChainJobsGlobalLogicIndex = -1;
    int step_C_Ally02ChainJobsGlobalLogicIndex = -1;
    int step_C_Ally03ChainJobsGlobalLogicIndex = -1;
    int step_C_Ally04ChainJobsGlobalLogicIndex = -1;
    int step_C_NumOfCreatedSoldiers = 0;

    float step_C_Timer = 0f;
    float step_C_TimerForCheckingNumOfCreatedSoldiers = 0f;
    float step_C_TimeToCheckCreatedSoldier = 2f;

    bool step_C_IsLoadedFromCheckPoint = false;

    #endregion

    public override void StartIt()
    {
        base.StartIt();

        //LoadCheckPoint(3.4f);
    }

    public override void RunIt()
    {
        base.RunIt();

        if (step == 1)
        {
        StartLevelSteps:

            #region Step A

            #region 0.1 Start
            if (levelStep == 0.1f)
            {
                SetLevelStep(1);
            }
            #endregion

            #region 1 Start Level
            if (levelStep == 1)
            {
                SaveCheckPoint(1f);

                RunParticle(particlesPart01);
                RunParticle(particlesPart02);

                step_A_Objects_StartPoint_Player.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);

                step_A_Objects_01_StartingLevelTrigger.StartOutStepIfNotStarted();

                PlayFirstMusic();

                SetLevelStep(1.1f);
            }
            #endregion

            #region 1.1 Check Trigger 1
            if (levelStep == 1.1f)
            {
                if (step_A_PlayerEntered_StartingLevelTrigger.IsEverActivated())
                {
                    SetLevelStep(1.12f);
                }
            }
            #endregion

            #region 1.12 Run Mortars Part 1
            if (levelStep == 1.12f)
            {
                mapLogic.HUD_ObjectivesPage_SetActiveObjective(1);
                mapLogic.HUD_ShowNewMission(0);

                mapLogic.HUD_AddOnlyMinimap3DObj(miniMapObjectTransform_01_StepA_01, "StepA_01");

                step_A_Objects_MortarHQ_Part00.StartItIfNotStarted();
                step_A_Objects_MortarHQ_Part01.StartItIfNotStarted();

                step_A_Objects_02_Part2MortarsTrigger.StartOutStepIfNotStarted();

                SetLevelStep(1.2f);
            }
            #endregion

            #region 1.2 Check Trigger 2
            if (levelStep == 1.2f)
            {
                if (step_A_PlayerEntered_Part2MortarsTrigger.IsEverActivated())
                {
                    SetLevelStep(1.22f);
                }
            }
            #endregion

            #region 1.22 Run Mortars Part 2
            if (levelStep == 1.22f)
            {
                mapLogic.HUD_RemoveMinimap3DObj("StepA_01");
                mapLogic.HUD_AddOnlyMinimap3DObj(miniMapObjectTransform_02_StepA_02, "StepA_02");

                step_A_Objects_MortarHQ_Part02.StartItIfNotStarted();

                step_A_Objects_MortarHQ_Part00.EndIt();

                step_A_Objects_03_BurningManTrigger.StartOutStepIfNotStarted();
                step_A_Objects_03_Part3MortarsTrigger.StartOutStepIfNotStarted();

                SetLevelStep(1.3f);
            }
            #endregion

            #region 1.3 Check Trigger 3
            if (levelStep == 1.3f)
            {
                if (step_A_PlayerEntered_Part3MortarsTrigger.IsEverActivated())
                {
                    SetLevelStep(1.32f);
                }
            }
            #endregion

            #region 1.32 Run Mortars Part 3
            if (levelStep == 1.32f)
            {
                mapLogic.HUD_RemoveMinimap3DObj("StepA_02");
                mapLogic.HUD_AddOnlyMinimap3DObj(miniMapObjectTransform_03_StepA_03, "StepA_03");

                step_A_Objects_MortarHQ_Part03.StartItIfNotStarted();

                step_A_Objects_MortarHQ_Part01.EndIt();

                RunParticle(particlesPart03);

                step_A_Objects_03_BackFromPart3Trigger.StartOutStepIfNotStarted();

                step_A_Objects_04_Part4MortarsTrigger.StartOutStepIfNotStarted();

                SetLevelStep(1.4f);
            }
            #endregion

            #region 1.4 Check Trigger 4 And Trigger Back From3
            if (levelStep == 1.4f)
            {
                if (step_A_PlayerEntered_BurningManTrigger.IsEverActivated() && !step_A_IsBurningMan01Start)
                {
                    BurnTheMan(step_A_Ally_01_BurningMan01);

                    step_A_IsBurningMan01Start = true;
                }

                if (step_A_PlayerEntered_Part4MortarsTrigger.IsEverActivated())
                {
                    SetLevelStep(1.42f);
                }

                if (step_A_PlayerEntered_BackFromPart3MortarsTrigger.IsEverActivated())
                {
                    SetLevelStep(1.44f);
                }
            }
            #endregion

            #region 1.42 Run Mortars Part 4
            if (levelStep == 1.42f)
            {
                mapLogic.HUD_RemoveMinimap3DObj("StepA_03");
                mapLogic.HUD_AddOnlyMinimap3DObj(miniMapObjectTransform_04_StepA_04, "StepA_04");

                step_A_Objects_MortarHQ_Part04.StartItIfNotStarted();

                step_A_Objects_MortarHQ_Part02.EndIt();

                StopParticle(particlesPart01);

                step_A_Objects_05_BurningMan02Trigger.StartOutStepIfNotStarted();

                step_A_Objects_04_BackFromPart4Trigger.StartOutStepIfNotStarted();

                step_A_Objects_05_Part5MortarsTrigger.StartOutStepIfNotStarted();

                SetLevelStep(1.5f);
            }
            #endregion

            #region 1.44 Back From Part 3
            if (levelStep == 1.44f)
            {
                step_A_Objects_MortarHQ_Part01.StartItIfNotStarted();

                step_A_Objects_MortarHQ_Part01.SetDeathChance(step_A_DeathChanceWhenBackFromStep3);

                SetLevelStep(1.46f);
            }
            #endregion

            #region 1.46 Check Trigger Part 4
            if (levelStep == 1.46f)
            {
                if (step_A_PlayerEntered_Part4MortarsTrigger.IsEverActivated())
                {
                    step_A_Objects_MortarHQ_Part01.EndIt();

                    SetLevelStep(1.42f);
                }
            }
            #endregion

            #region 1.5 Check Trigger 5 And Trigger Back From4
            if (levelStep == 1.5f)
            {
                if (step_A_PlayerEntered_BurningMan02Trigger.IsEverActivated() && !step_A_IsBurningMan02Start)
                {
                    BurnTheMan(step_A_Ally_02_BurningMan02);

                    step_A_IsBurningMan02Start = true;
                }

                if (step_A_PlayerEntered_Part5MortarsTrigger.IsEverActivated())
                {
                    SetLevelStep(1.52f);
                }

                if (step_A_PlayerEntered_BackFromPart4MortarsTrigger.IsEverActivated())
                {
                    SetLevelStep(1.54f);
                }
            }
            #endregion

            #region 1.52 Run Mortars Part 5
            if (levelStep == 1.52f)
            {
                mapLogic.HUD_RemoveMinimap3DObj("StepA_04");
                mapLogic.HUD_AddOnlyMinimap3DObj(miniMapObjectTransform_05_StepA_05, "StepA_05");

                step_A_Objects_MortarHQ_Part05.StartItIfNotStarted();

                step_A_Objects_MortarHQ_Part03.EndIt();

                step_A_Objects_05_BackFromPart5Trigger.StartOutStepIfNotStarted();

                step_A_Objects_06_ExitTrigger.StartOutStepIfNotStarted();

                SetLevelStep(1.6f);
            }
            #endregion

            #region 1.54 Back From Part 4
            if (levelStep == 1.54f)
            {
                step_A_Objects_MortarHQ_Part02.StartItIfNotStarted();

                RunParticle(particlesPart01);

                step_A_Objects_MortarHQ_Part02.SetDeathChance(step_A_DeathChanceWhenBackFromStep4);

                SetLevelStep(1.56f);
            }
            #endregion

            #region 1.56 Check Trigger 5
            if (levelStep == 1.56f)
            {
                if (step_A_PlayerEntered_Part5MortarsTrigger.IsEverActivated())
                {
                    step_A_Objects_MortarHQ_Part02.EndIt();

                    StopParticle(particlesPart01);

                    SetLevelStep(1.52f);
                }
            }
            #endregion

            #region 1.6 Check Exit Trigger And Trigger Back From5
            if (levelStep == 1.6f)
            {
                if (step_A_PlayerEntered_ExitTrigger.IsEverActivated())
                {
                    SetLevelStep(1.62f);
                }

                if (step_A_PlayerEntered_BackFromPart5MortarsTrigger.IsEverActivated())
                {
                    SetLevelStep(1.64f);
                }
            }
            #endregion

            #region 1.62 Exit
            if (levelStep == 1.62f)
            {
                mapLogic.HUD_RemoveMinimap3DObj("StepA_05");

                step_A_Objects_09_Mortar.StartIt();

                MakeTreeFall(step_A_Objects_07_AnimatedFallingTree, false, "Palm Falling");

                step_A_Objects_08_ColliderExit.active = true;

                step_A_Objects_08_ColliderExit_B.active = true;

                StopParticle(particlesPart02);

                SetLevelStep(2f);
            }
            #endregion

            #region 1.64 Back From Part 5
            if (levelStep == 1.64f)
            {
                step_A_Objects_MortarHQ_Part03.StartItIfNotStarted();

                RunParticle(particlesPart01);

                step_A_Objects_MortarHQ_Part03.SetDeathChance(step_A_DeathChanceWhenBackFromStep5);

                SetLevelStep(1.66f);
            }
            #endregion

            #region 1.66 Check Exit Trigger
            if (levelStep == 1.66f)
            {
                if (step_A_PlayerEntered_ExitTrigger.IsEverActivated())
                {
                    step_A_Objects_MortarHQ_Part03.EndIt();

                    StopParticle(particlesPart01);

                    SetLevelStep(1.62f);
                }
            }
            #endregion

            #endregion

            //////////////////////////////////////

            #region Step B

            #region 2 Start Step B
            if (levelStep == 2)
            {
                SaveCheckPoint(2f);

                mapLogic.HUD_ShowGameSaved();

                mapLogic.HUD_AddOnlyMinimap3DObj(miniMapObjectTransform_06_StepB_01, "StepB_01");

                if (step_B_IsFromLoad)
                {
                    RunParticle(particlesPart03);

                    mapLogic.HUD_ShowNewMission(0);
                }

                //Safe House
                List<MortarHeadQuarter> mrtHQ = new List<MortarHeadQuarter>();

                mrtHQ.Add(step_A_Objects_MortarHQ_Part05);
                mrtHQ.Add(step_B_Objects_MortarHQ_Part_01);

                foreach (MortarHeadQuarter hq in mrtHQ)
                {
                    hq.StartItIfNotStarted();

                    hq.SetDeathChance(0);
                }

                step_B_Objects_01_Part2MortarsTrigger.StartOutStepIfNotStarted();

                PlayFirstMusic();

                SetLevelStep(2.1f);
            }
            #endregion

            #region 2.1 Check Part 2 Trigger
            if (levelStep == 2.1f)
            {
                if (step_B_PlayerEntered_Part2MortarsTrigger.IsEverActivated())
                {
                    SetLevelStep(2.12f);
                }
            }
            #endregion

            #region 2.12 Run Part 2 Mortars
            if (levelStep == 2.12f)
            {
                mapLogic.HUD_RemoveMinimap3DObj("StepB_01");
                mapLogic.HUD_AddOnlyMinimap3DObj(miniMapObjectTransform_07_StepB_02, "StepB_02");

                step_B_Objects_MortarHQ_Part_02.StartItIfNotStarted();

                step_A_Objects_MortarHQ_Part05.EndIt();

                step_B_Ally01ChainJobsGlobalLogicIndex = 0;
                step_B_Ally_ChainJobGroup_01_Ally01.StartOutStepIfNotStarted();

                step_B_Ally02ChainJobsGlobalLogicIndex = 0;
                step_B_Ally_ChainJobGroup_02_Ally02.StartOutStepIfNotStarted();

                step_B_Objects_02_FallingPalmTrigger.StartOutStepIfNotStarted();

                step_B_Objects_05_ExplodeHouseTrigger.StartOutStepIfNotStarted();

                step_B_Objects_07_Part3MortarsTrigger.StartOutStepIfNotStarted();

                SetLevelStep(2.2f);
            }
            #endregion

            #region 2.2 Check Falling Palm Trigger
            if (levelStep == 2.2f)
            {
                if (step_B_PlayerEntered_FallingPalmTrigger.IsEverActivated())
                {
                    MakeTreeFall(step_B_Objects_03_FallingPalm, false, "Palm Falling 2");

                    step_B_Objects_04_Mortar.StartIt();

                    RunParticle(particlesPart04);

                    SetLevelStep(2.3f);
                }
            }
            #endregion

            #region 2.3 Check Explode House Trigger
            if (levelStep == 2.3f)
            {
                if (step_B_PlayerEntered_ExplodeHouseTrigger.IsEverActivated())
                {
                    mapLogic.HUD_RemoveMinimap3DObj("StepB_02");
                    mapLogic.HUD_AddOnlyMinimap3DObj(miniMapObjectTransform_08_StepB_03, "StepB_03");

                    Mortar[] mrts = step_B_Objects_06_MortarsForHouse.GetComponentsInChildren<Mortar>();

                    foreach (Mortar mrt in mrts)
                    {
                        mrt.StartIt();
                    }

                    step_B_timer = step_B_MaxDelayForHouseParticles;

                    SetLevelStep(2.31f);
                }
            }
            #endregion

            #region 2.31 Counting Delay For House Particles
            if (levelStep == 2.31f)
            {
                step_B_timer = MathfPlus.DecByDeltatimeToZero(step_B_timer);

                if (step_B_timer == 0)
                {
                    SetLevelStep(2.32f);
                }
            }
            #endregion

            #region 2.32 Running House Particles
            if (levelStep == 2.32f)
            {
                RunParticle(step_B_Objects_06_ParticlesForHouse);

                SetLevelStep(2.4f);
            }
            #endregion

            #region 2.4 Check Mortar Trigger Part 3
            if (levelStep == 2.4f)
            {
                if (step_B_PlayerEntered_Part3MortarsTrigger.IsEverActivated())
                {
                    SetLevelStep(2.42f);
                }
            }
            #endregion

            #region 2.42 Run Mortars Part 3
            if (levelStep == 2.42f)
            {
                mapLogic.HUD_RemoveMinimap3DObj("StepB_03");
                mapLogic.HUD_AddOnlyMinimap3DObj(miniMapObjectTransform_09_StepB_04, "StepB_04");

                step_B_Objects_MortarHQ_Part_03.StartItIfNotStarted();

                step_B_Objects_MortarHQ_Part_01.EndIt();

                RunParticle(step_B_Objects_11_SmokeDenseRoot);

                step_B_Objects_08_Part4MortarsTrigger.StartOutStepIfNotStarted();

                SetLevelStep(2.5f);
            }
            #endregion

            #region 2.5 Check Mortar Trigger Part 4
            if (levelStep == 2.5f)
            {
                if (step_B_PlayerEntered_Part4MortarsTrigger.IsEverActivated())
                {
                    SetLevelStep(2.52f);
                }
            }
            #endregion

            #region 2.52 Run Mortars Part 4 And Ally 01
            if (levelStep == 2.52f)
            {
                mapLogic.HUD_RemoveMinimap3DObj("StepB_04");
                mapLogic.HUD_AddOnlyMinimap3DObj(miniMapObjectTransform_10_StepB_05, "StepB_05");

                logicVoiceCollection_Ally01.PlayName("Step_B_01_BoroBoro");

                step_B_Objects_MortarHQ_Part_04.StartItIfNotStarted();

                step_B_Objects_MortarHQ_Part_02.EndIt();

                step_B_Ally01ChainJobsGlobalLogicIndex = 1;
                step_B_Ally_ChainJobGroup_01_Ally01.Init_SetNewGlobalLogicIndex(step_B_Ally01ChainJobsGlobalLogicIndex);

                step_B_Objects_09_BackFromPart4Trigger.StartOutStepIfNotStarted();

                step_B_Objects_10_ExitFromMortarsTrigger.StartOutStepIfNotStarted();

                SetLevelStep(2.6f);
            }
            #endregion

            #region 2.6 Check Exit From Mortars And Back From Part 4
            if (levelStep == 2.6f)
            {
                if (step_B_PlayerEntered_ExitFromMortarsTrigger.IsEverActivated())
                {
                    SetLevelStep(2.62f);
                }

                if (step_B_PlayerEntered_BackFromPart4Trigger.IsEverActivated())
                {
                    SetLevelStep(2.64f);
                }
            }
            #endregion

            #region 2.62 Exit Mortars Ally Move
            if (levelStep == 2.62f)
            {
                mapLogic.HUD_RemoveMinimap3DObj("StepB_05");
                mapLogic.HUD_AddOnlyMinimap3DObj(miniMapObjectTransform_11_StepB_06, "StepB_06");

                RunParticle(step_B_Objects_12_FinalSmokeRoot);

                step_B_Objects_MortarHQ_Part_03.EndIt();
                step_B_Objects_MortarHQ_Part_04.SetDeathChance(0);

                step_B_Ally02ChainJobsGlobalLogicIndex = 1;
                step_B_Ally_ChainJobGroup_02_Ally02.Init_SetNewGlobalLogicIndex(step_B_Ally02ChainJobsGlobalLogicIndex);

                step_B_Objects_13_BackFromExitTrigger.StartOutStepIfNotStarted();

                step_B_Objects_14_AlliesStartsTrigger.StartOutStepIfNotStarted();

                SetLevelStep(2.7f);
            }
            #endregion

            #region 2.64 Back From Part 4
            if (levelStep == 2.64f)
            {
                step_B_Objects_MortarHQ_Part_02.StartItIfNotStarted();

                step_B_Objects_MortarHQ_Part_02.SetDeathChance(step_B_DeathChanceWhenBackFromStep4);

                SetLevelStep(2.66f);
            }
            #endregion

            #region 2.66 Check Exit Trigger
            if (levelStep == 2.66f)
            {
                if (step_B_PlayerEntered_ExitFromMortarsTrigger.IsEverActivated())
                {
                    step_B_Objects_MortarHQ_Part_02.EndIt();

                    SetLevelStep(2.62f);
                }
            }
            #endregion

            #region 2.7 Allies Starts And Back From Exit
            if (levelStep == 2.7f)
            {
                if (step_B_PlayerEntered_AlliesStartsTrigger.IsEverActivated())
                {
                    SetLevelStep(2.72f);
                }

                if (step_B_PlayerEntered_BackFromExitTrigger.IsEverActivated())
                {
                    SetLevelStep(2.74f);
                }
            }
            #endregion

            #region 2.72 Change Allies 03 - 04 Chain Job
            if (levelStep == 2.72f)
            {
                mapLogic.HUD_RemoveMinimap3DObj("StepB_06");
                mapLogic.HUD_AddOnlyMinimap3DObj(miniMapObjectTransform_12_StepB_07, "StepB_07");

                step_B_Ally03ChainJobsGlobalLogicIndex = 0;
                step_B_Ally_ChainJobGroup_03_Ally03.StartOutStepIfNotStarted();

                step_B_Ally04ChainJobsGlobalLogicIndex = 0;
                step_B_Ally_ChainJobGroup_04_Ally04.StartOutStepIfNotStarted();

                MusicController.Instance.EndMusicWithFade(MusicFadeType.Normal);

                SetLevelStep(2.8f);
            }
            #endregion

            #region 2.74 Run Mortars Again
            if (levelStep == 2.74f)
            {
                step_B_Objects_MortarHQ_Part_03.StartItIfNotStarted();

                step_B_Objects_MortarHQ_Part_03.SetDeathChance(step_B_DeathChanceWhenBackFromExit);

                SetLevelStep(2.76f);
            }
            #endregion

            #region 2.76 Allies Start Mortars
            if (levelStep == 2.76f)
            {
                if (step_B_PlayerEntered_AlliesStartsTrigger.IsEverActivated())
                {
                    step_B_Objects_MortarHQ_Part_03.EndIt();
                    step_B_Objects_MortarHQ_Part_02.EndIt();

                    step_B_Objects_MortarHQ_Part_03.SetDeathChance(0);

                    SetLevelStep(2.72f);
                }
            }
            #endregion

            #region 2.8 Inform Player To Come
            if (levelStep == 2.8f)
            {
                logicVoiceCollection_Ally01.PlayName("Step_B_02_BiaIntaraf");
                logicVoiceCollection_Ally02.PlayName("Step_B_01_SangarBegir");

                step_B_Objects_15_BackFromAlliesStartTrigger.StartOutStepIfNotStarted();

                step_B_Objects_16_EndStepTrigger.StartOutStepIfNotStarted();

                SetLevelStep(2.85f);
            }
            #endregion

            #region 2.85 Check End Step And Back Trigger
            if (levelStep == 2.85f)
            {
                if (step_B_PlayerEntered_EndStepTrigger.IsEverActivated())
                {
                    SetLevelStep(2.86f);
                }

                if (step_B_PlayerEntered_BackFromAlliesTrigger.IsEverActivated())
                {
                    SetLevelStep(2.87f);
                }
            }
            #endregion

            #region 2.86 End Step
            if (levelStep == 2.86f)
            {
                mapLogic.HUD_RemoveMinimap3DObj("StepB_07");

                logicVoiceCollection_Ally01.StopCurVoiceAfterItsFinishing();
                logicVoiceCollection_Ally02.StopCurVoiceAfterItsFinishing();

                step_B_Objects_MortarHQ_Part_01.EndIt();
                step_B_Objects_MortarHQ_Part_02.EndIt();
                step_B_Objects_MortarHQ_Part_03.EndIt();
                step_B_Objects_MortarHQ_Part_04.EndIt();

                step_B_Ally_ChainJobGroup_01_Ally01.StartFinishing_OutStepIfNotFinishing();
                step_B_Ally_ChainJobGroup_02_Ally02.StartFinishing_OutStepIfNotFinishing();
                step_B_Ally_ChainJobGroup_03_Ally03.StartFinishing_OutStepIfNotFinishing();
                step_B_Ally_ChainJobGroup_04_Ally04.StartFinishing_OutStepIfNotFinishing();

                StopParticle(step_B_Objects_06_ParticlesForHouse);

                StopParticle(particlesPart03);
                StopParticle(particlesPart04);

                SetLevelStep(3f);
            }
            #endregion

            #region 2.87 Kill Player
            if (levelStep == 2.87f)
            {
                step_B_Objects_MortarHQ_Part_04.SetDeathChance(100);

                SetLevelStep(2.88f);
            }
            #endregion

            #region 2.88 Check End Again
            if (levelStep == 2.88f)
            {
                if (step_B_PlayerEntered_EndStepTrigger.IsEverActivated())
                {
                    SetLevelStep(2.86f);
                }
            }
            #endregion

            #endregion

            ///////////////////////////////////////////////////

            #region Step C

            #region 3 Start Step
            if (levelStep == 3f)
            {
                SaveCheckPoint(3f);

                logicVoiceCollection_Ally01.PlayName("Step_C_01_HameAmadeBashan");
                logicVoiceCollection_Ally04.PlayName("Step_C_01_AghabNeshiniNadarim");

                step_C_Objects_01_ColliderForDood.active = true;

                if (step_C_ObjectsThatShouldntRender.Count != 0)
                {
                    SetGameObjectsRender(step_C_ObjectsThatShouldntRender, false);
                }

                step_C_Ally01ChainJobsGlobalLogicIndex = 0;
                step_C_Ally_ChainJobGroup_01_Ally01.StartOutStepIfNotStarted();

                step_C_Ally02ChainJobsGlobalLogicIndex = 0;
                step_C_Ally_ChainJobGroup_02_Ally02.StartOutStepIfNotStarted();

                step_C_Ally03ChainJobsGlobalLogicIndex = 0;
                step_C_Ally_ChainJobGroup_03_Ally03.StartOutStepIfNotStarted();

                step_C_Ally04ChainJobsGlobalLogicIndex = 0;
                step_C_Ally_ChainJobGroup_04_Ally04.StartOutStepIfNotStarted();

                step_C_Timer = step_C_TimeToWaitForSoldiersCome;

                SetLevelStep(3.1f);
            }
            #endregion

            #region 3.1 Timer Wait Enemies Start
            if (levelStep == 3.1f)
            {
                step_C_Timer = MathfPlus.DecByDeltatimeToZero(step_C_Timer);

                if (step_C_Timer == 0)
                {
                    SetLevelStep(3.12f);
                }
            }
            #endregion

            #region 3.12 Start Fight Regs
            if (levelStep == 3.12f)
            {
                mapLogic.HUD_ObjectivesPage_SetActiveObjective(2);
                mapLogic.HUD_ShowNewMission(1);

                step_C_Objects_01_AudioInfo_EnemysCrowd.Play();

                logicVoiceCollection_Ally02.PlayName("Step_C_02_Moghavemat");
                logicVoiceCollection_Ally03.PlayName("Step_C_01_Moghavemat");
                logicVoiceCollection_Ally04.PlayName("Step_C_02_Moghavemat");

                step_C_Enemy_FightInRegsGroup_Static.StartOutStepIfNotStarted();

                step_C_Enemy_FightInRegsGroup_Rush.StartOutStepIfNotStarted();

                step_C_Objects_01_ExecutionArea01Trigger.StartIt();

                step_C_Objects_01_CountingInsideSoldiersTrigger.StartOutStepIfNotStarted();

                step_C_Timer = step_C_TimeToCreateSoldier;

                step_C_TimerForCheckingNumOfCreatedSoldiers = step_C_TimeToCheckCreatedSoldier;

                PlaySecondMusic();

                SetLevelStep(3.15f);
            }
            #endregion

            #region 3.15 Timer For Fight
            if (levelStep == 3.15f)
            {
                step_C_TimerForCheckingNumOfCreatedSoldiers = MathfPlus.DecByDeltatimeToZero(step_C_TimerForCheckingNumOfCreatedSoldiers);
                if (step_C_TimerForCheckingNumOfCreatedSoldiers == 0)
                {
                    step_C_NumOfCreatedSoldiers = step_C_Enemy_FightInRegsGroup_Static.GetNumOfCreatedSoldiers();

                    step_C_NumOfCreatedSoldiers += step_C_Enemy_FightInRegsGroup_Rush.GetNumOfCreatedSoldiers();

                    step_C_TimerForCheckingNumOfCreatedSoldiers = step_C_TimeToCheckCreatedSoldier;
                }

                step_C_Timer = MathfPlus.DecByDeltatimeToZero(step_C_Timer);
                if (step_C_Timer == 0)
                {
                    if (step_C_NumOfCreatedSoldiers > step_C_MinNumOfCreatedSoldier)
                    {
                        SetLevelStep(3.2f);
                    }
                }

                if (step_C_CountingInsideSoldierTrigger.IsEverActivated())
                {
                    SetLevelStep(step_MissionFail_AlliesNotSupported);
                    goto EndLevelSteps;
                }
            }
            #endregion

            #region 3.2 Mortars Starts
            if (levelStep == 3.2f)
            {
                logicVoiceCollection_Ally02.StopCurVoiceAfterItsFinishing();
                logicVoiceCollection_Ally03.StopCurVoiceAfterItsFinishing();
                logicVoiceCollection_Ally04.StopCurVoiceAfterItsFinishing();

                step_C_Objects_MortarHQ_Part_01.StartItIfNotStarted();

                step_C_Timer = step_C_TimeToStartTalkingAfterMortars;

                step_C_Objects_02_GoToEnemyMortarsTrigger.StartOutStepIfNotStarted();

                SetLevelStep(3.25f);
            }
            #endregion

            #region 3.25 Wait For Talk Daran Toop Mirizan Ru Khodeshun
            if (levelStep == 3.25f)
            {
                step_C_Timer = MathfPlus.DecByDeltatimeToZero(step_C_Timer);

                if (step_C_Timer == 0 && logicVoiceCollection_Ally04.IsCurVoiceFinished())
                {
                    SetLevelStep(3.3f);
                }

                if (step_C_GoToEnemyMortarsTrigger.IsEverActivated())
                {
                    step_C_Objects_MortarHQ_Part_01.SetDeathChance(100);
                }
            }
            #endregion

            #region 3.3 Talking Daran Toop Mirizan Ru Khodeshun
            if (levelStep == 3.3f)
            {
                logicVoiceCollection_Ally04.PlayName("Step_C_03_DaranToopMirizanRuKhodeshun");

                step_C_Timer = step_C_TimeToStartTalkingHamaroBokoshin;

                SetLevelStep(3.32f);
            }
            #endregion

            #region 3.32 Wait For Talk Amuneshun Nadin
            if (levelStep == 3.32f)
            {
                step_C_Timer = MathfPlus.DecByDeltatimeToZero(step_C_Timer);

                if (step_C_Timer == 0 && logicVoiceCollection_Ally04.IsCurVoiceFinished())
                {
                    SetLevelStep(3.34f);
                }

                if (step_C_GoToEnemyMortarsTrigger.IsEverActivated())
                {
                    step_C_Objects_MortarHQ_Part_01.SetDeathChance(100);
                }
            }
            #endregion

            #region 3.34 Talking Amuneshun Nadin
            if (levelStep == 3.34f)
            {
                logicVoiceCollection_Ally04.PlayName("Step_C_04_AmuneshunNadin");

                step_C_Timer = step_C_TimeToFinishMortarsAndStartCutScene;

                SetLevelStep(3.35f);
            }
            #endregion

            #region 3.35 Wait For Mortars
            if (levelStep == 3.35f)
            {
                step_C_Timer = MathfPlus.DecByDeltatimeToZero(step_C_Timer);

                if (step_C_Timer == 0)
                {
                    SetLevelStep(3.36f);
                }
            }
            #endregion

            #region 3.36 Make all enemy solds so weak
            if (levelStep == 3.36f)
            {
                step_C_Enemy_FightInRegsGroup_Static.StopCreatingMoreSoldiersAndMakeAliveSoldiersSoWeak();
                step_C_Enemy_FightInRegsGroup_Rush.StopCreatingMoreSoldiersAndMakeAliveSoldiersSoWeak();

                SetLevelStep(3.37f);
            }
            #endregion

            #region 3.37 Wait for all enemy die
            if (levelStep == 3.37f)
            {
                if (step_C_Enemy_FightInRegsGroup_Static.IsCreatingSoldiersStoppedAndAllSoldsDead()
                   && step_C_Enemy_FightInRegsGroup_Rush.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    SetLevelStep(3.371f);
                }
            }
            #endregion

            #region 3.371 Start delay before cutscene and stop mortars
            if (levelStep == 3.371f)
            {
                step_C_Objects_MortarHQ_Part_01.EndIt();

                step_C_Timer = step_C_TimeToStartCutSceneAfterAllEnemyDie;

                SetLevelStep(3.372f);
            }
            #endregion

            #region 3.372 W8 for delay before cutscene
            if (levelStep == 3.372f)
            {
                step_C_Timer = MathfPlus.DecByDeltatimeToZero(step_C_Timer);

                if (step_C_Timer == 0)
                    SetLevelStep(3.38f);
            }
            #endregion

            #region 3.38 Start screen fading
            if (levelStep == 3.38f)
            {
                mapLogic.blackScreenFader.StartFadingOut();
                SetLevelStep(3.39f);
            }
            #endregion

            #region 3.39 fading screen
            if (levelStep == 3.39f)
            {
                if (mapLogic.blackScreenFader.isFadingFinished)
                {
                    SetLevelStep(3.4f);
                }
            }
            #endregion

            #region 3.4f Start Cutscene Crowd Happy
            if (levelStep == 3.4f)
            {
                step_C_Enemy_FightInRegsGroup_Static.StopCreatingMoreSoldiers();

                step_C_Enemy_FightInRegsGroup_Rush.StopCreatingMoreSoldiers();

                //step_C_Objects_MortarHQ_Part_01.EndIt();

                cutscene_CrowdHappy.StartIt();

                MusicController.Instance.EndMusicWithFade(MusicFadeType.VeryFast);

                SetLevelStep(3.42f);
            }
            #endregion

            #region 3.42f Wait For Cutscene
            if (levelStep == 3.42f)
            {
                if (cutscene_CrowdHappy.status == CutsceneStatus.Finished)
                {
                    //PlayerCharacterNew.Instance.gameObject.GetComponent<Compass>().active = false;

                    //SetLevelStep(3.44f);

                    SetLevelStep(4f);
                }
            }
            #endregion

            #region 3.44 Start Cutscene Khaloo Shir Khorde
            if (levelStep == 3.44f)
            {
                cutScene_KhalooShirKhorde.StartIt();

                SetLevelStep(3.5f);
            }
            #endregion

            #region 3.5f Wait For Cutscene
            if (levelStep == 3.5f)
            {
                if (cutScene_KhalooShirKhorde.status == CutsceneStatus.Finished)
                {
                    SetLevelStep(4f);
                }
            }
            #endregion

            #region 4f Set mission is finished if black screen fading is done.
            if (levelStep == 4f)
            {
                if (mapLogic.blackScreenFader.isFadingFinished)
                {
                    SetMissionIsFinished();
                    SetLevelStep(5f);
                }
            }
            #endregion

            #endregion

        EndLevelSteps:
            ;

            #region A

            #region step_A_Objects_StartingLevelTrigger

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

            #region step_A_Objects_StartingPart2MortarsTrigger

            #region 1 Start
            if (step_A_Objects_02_Part2MortarsTrigger.OutStep == 1) //Start
            {
                step_A_Objects_02_Part2MortarsTrigger.SetEnabled(true);
                step_A_Objects_02_Part2MortarsTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_A_Objects_02_Part2MortarsTrigger.OutStep == 1.1f) //Run
            {
                if (step_A_Objects_02_Part2MortarsTrigger.IsPlayerIn())
                {
                    step_A_PlayerEntered_Part2MortarsTrigger.SetStatus(LogicFlagStatus.Active);
                    step_A_Objects_02_Part2MortarsTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_A_Objects_02_Part2MortarsTrigger.OutStep == 900f) //Finish
            {
                step_A_Objects_02_Part2MortarsTrigger.SetEnabled(false);
                step_A_Objects_02_Part2MortarsTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_A_Objects_03_BurningManTrigger

            #region 1 Start
            if (step_A_Objects_03_BurningManTrigger.OutStep == 1) //Start
            {
                step_A_Objects_03_BurningManTrigger.SetEnabled(true);
                step_A_Objects_03_BurningManTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_A_Objects_03_BurningManTrigger.OutStep == 1.1f) //Run
            {
                if (step_A_Objects_03_BurningManTrigger.IsPlayerIn())
                {
                    step_A_PlayerEntered_BurningManTrigger.SetStatus(LogicFlagStatus.Active);
                    step_A_Objects_03_BurningManTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_A_Objects_03_BurningManTrigger.OutStep == 900f) //Finish
            {
                step_A_Objects_03_BurningManTrigger.SetEnabled(false);
                step_A_Objects_03_BurningManTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_A_Objects_StartingPart3MortarsTrigger

            #region 1 Start
            if (step_A_Objects_03_Part3MortarsTrigger.OutStep == 1) //Start
            {
                step_A_Objects_03_Part3MortarsTrigger.SetEnabled(true);
                step_A_Objects_03_Part3MortarsTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_A_Objects_03_Part3MortarsTrigger.OutStep == 1.1f) //Run
            {
                if (step_A_Objects_03_Part3MortarsTrigger.IsPlayerIn())
                {
                    step_A_PlayerEntered_Part3MortarsTrigger.SetStatus(LogicFlagStatus.Active);
                    step_A_Objects_03_Part3MortarsTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_A_Objects_03_Part3MortarsTrigger.OutStep == 900f) //Finish
            {
                step_A_Objects_03_Part3MortarsTrigger.SetEnabled(false);
                step_A_Objects_03_Part3MortarsTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_A_Objects_03_BackFromPart3Trigger

            #region 1 Start
            if (step_A_Objects_03_BackFromPart3Trigger.OutStep == 1) //Start
            {
                step_A_Objects_03_BackFromPart3Trigger.SetEnabled(true);
                step_A_Objects_03_BackFromPart3Trigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_A_Objects_03_BackFromPart3Trigger.OutStep == 1.1f) //Run
            {
                if (step_A_Objects_03_BackFromPart3Trigger.IsPlayerIn())
                {
                    step_A_PlayerEntered_BackFromPart3MortarsTrigger.SetStatus(LogicFlagStatus.Active);
                    step_A_Objects_03_BackFromPart3Trigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_A_Objects_03_BackFromPart3Trigger.OutStep == 900f) //Finish
            {
                step_A_Objects_03_BackFromPart3Trigger.SetEnabled(false);
                step_A_Objects_03_BackFromPart3Trigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_A_Objects_StartingPart4MortarsTrigger

            #region 1 Start
            if (step_A_Objects_04_Part4MortarsTrigger.OutStep == 1) //Start
            {
                step_A_Objects_04_Part4MortarsTrigger.SetEnabled(true);
                step_A_Objects_04_Part4MortarsTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_A_Objects_04_Part4MortarsTrigger.OutStep == 1.1f) //Run
            {
                if (step_A_Objects_04_Part4MortarsTrigger.IsPlayerIn())
                {
                    step_A_PlayerEntered_Part4MortarsTrigger.SetStatus(LogicFlagStatus.Active);
                    step_A_Objects_04_Part4MortarsTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_A_Objects_04_Part4MortarsTrigger.OutStep == 900f) //Finish
            {
                step_A_Objects_04_Part4MortarsTrigger.SetEnabled(false);
                step_A_Objects_04_Part4MortarsTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_A_Objects_04_BackFromPart4Trigger

            #region 1 Start
            if (step_A_Objects_04_BackFromPart4Trigger.OutStep == 1) //Start
            {
                step_A_Objects_04_BackFromPart4Trigger.SetEnabled(true);
                step_A_Objects_04_BackFromPart4Trigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_A_Objects_04_BackFromPart4Trigger.OutStep == 1.1f) //Run
            {
                if (step_A_Objects_04_BackFromPart4Trigger.IsPlayerIn())
                {
                    step_A_PlayerEntered_BackFromPart4MortarsTrigger.SetStatus(LogicFlagStatus.Active);
                    step_A_Objects_04_BackFromPart4Trigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_A_Objects_04_BackFromPart4Trigger.OutStep == 900f) //Finish
            {
                step_A_Objects_04_BackFromPart4Trigger.SetEnabled(false);
                step_A_Objects_04_BackFromPart4Trigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_A_Objects_05_BurningMan02Trigger

            #region 1 Start
            if (step_A_Objects_05_BurningMan02Trigger.OutStep == 1) //Start
            {
                step_A_Objects_05_BurningMan02Trigger.SetEnabled(true);
                step_A_Objects_05_BurningMan02Trigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_A_Objects_05_BurningMan02Trigger.OutStep == 1.1f) //Run
            {
                if (step_A_Objects_05_BurningMan02Trigger.IsPlayerIn())
                {
                    step_A_PlayerEntered_BurningMan02Trigger.SetStatus(LogicFlagStatus.Active);
                    step_A_Objects_05_BurningMan02Trigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_A_Objects_05_BurningMan02Trigger.OutStep == 900f) //Finish
            {
                step_A_Objects_05_BurningMan02Trigger.SetEnabled(false);
                step_A_Objects_05_BurningMan02Trigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_A_Objects_StartingPart5MortarsTrigger

            #region 1 Start
            if (step_A_Objects_05_Part5MortarsTrigger.OutStep == 1) //Start
            {
                step_A_Objects_05_Part5MortarsTrigger.SetEnabled(true);
                step_A_Objects_05_Part5MortarsTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_A_Objects_05_Part5MortarsTrigger.OutStep == 1.1f) //Run
            {
                if (step_A_Objects_05_Part5MortarsTrigger.IsPlayerIn())
                {
                    step_A_PlayerEntered_Part5MortarsTrigger.SetStatus(LogicFlagStatus.Active);
                    step_A_Objects_05_Part5MortarsTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_A_Objects_05_Part5MortarsTrigger.OutStep == 900f) //Finish
            {
                step_A_Objects_05_Part5MortarsTrigger.SetEnabled(false);
                step_A_Objects_05_Part5MortarsTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_A_Objects_05_BackFromPart5Trigger

            #region 1 Start
            if (step_A_Objects_05_BackFromPart5Trigger.OutStep == 1) //Start
            {
                step_A_Objects_05_BackFromPart5Trigger.SetEnabled(true);
                step_A_Objects_05_BackFromPart5Trigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_A_Objects_05_BackFromPart5Trigger.OutStep == 1.1f) //Run
            {
                if (step_A_Objects_05_BackFromPart5Trigger.IsPlayerIn())
                {
                    step_A_PlayerEntered_BackFromPart5MortarsTrigger.SetStatus(LogicFlagStatus.Active);
                    step_A_Objects_05_BackFromPart5Trigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_A_Objects_05_BackFromPart5Trigger.OutStep == 900f) //Finish
            {
                step_A_Objects_05_BackFromPart5Trigger.SetEnabled(false);
                step_A_Objects_05_BackFromPart5Trigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_A_Objects_06_ExitTrigger

            #region 1 Start
            if (step_A_Objects_06_ExitTrigger.OutStep == 1) //Start
            {
                step_A_Objects_06_ExitTrigger.SetEnabled(true);
                step_A_Objects_06_ExitTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_A_Objects_06_ExitTrigger.OutStep == 1.1f) //Run
            {
                if (step_A_Objects_06_ExitTrigger.IsPlayerIn())
                {
                    step_A_PlayerEntered_ExitTrigger.SetStatus(LogicFlagStatus.Active);
                    step_A_Objects_06_ExitTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_A_Objects_06_ExitTrigger.OutStep == 900f) //Finish
            {
                step_A_Objects_06_ExitTrigger.SetEnabled(false);
                step_A_Objects_06_ExitTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #endregion

            #region B

            #region step_B_Ally_ChainJobGroup_01_Ally01

            #region 1 Start
            if (step_B_Ally_ChainJobGroup_01_Ally01.outStep == 1)
            {
                step_B_Ally_ChainJobGroup_01_Ally01.Init_SetNewGlobalLogicIndex(step_B_Ally01ChainJobsGlobalLogicIndex);
                step_B_Ally_ChainJobGroup_01_Ally01.StartIt();

                step_B_Ally_ChainJobGroup_01_Ally01.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_B_Ally_ChainJobGroup_01_Ally01.outStep == 1.1f)
            {
                step_B_Ally_ChainJobGroup_01_Ally01.RunIt();
            }
            #endregion

            #region 900 Start finishing
            if (step_B_Ally_ChainJobGroup_01_Ally01.outStep == 900f)
            {
                step_B_Ally_ChainJobGroup_01_Ally01.SetNeedsToBeFinished();

                step_B_Ally_ChainJobGroup_01_Ally01.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (step_B_Ally_ChainJobGroup_01_Ally01.outStep == 901f)
            {
                step_B_Ally_ChainJobGroup_01_Ally01.RunIt();

                if (step_B_Ally_ChainJobGroup_01_Ally01.status == LogicJobStatus.Finished)
                {
                    step_B_Ally_ChainJobGroup_01_Ally01.SetOutStep(1000);
                }
            }
            #endregion

            #endregion

            #region step_B_Ally_ChainJobGroup_02_Ally02

            #region 1 Start
            if (step_B_Ally_ChainJobGroup_02_Ally02.outStep == 1)
            {
                step_B_Ally_ChainJobGroup_02_Ally02.Init_SetNewGlobalLogicIndex(step_B_Ally02ChainJobsGlobalLogicIndex);
                step_B_Ally_ChainJobGroup_02_Ally02.StartIt();

                step_B_Ally_ChainJobGroup_02_Ally02.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_B_Ally_ChainJobGroup_02_Ally02.outStep == 1.1f)
            {
                step_B_Ally_ChainJobGroup_02_Ally02.RunIt();
            }
            #endregion

            #region 900 Start finishing
            if (step_B_Ally_ChainJobGroup_02_Ally02.outStep == 900f)
            {
                step_B_Ally_ChainJobGroup_02_Ally02.SetNeedsToBeFinished();

                step_B_Ally_ChainJobGroup_02_Ally02.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (step_B_Ally_ChainJobGroup_02_Ally02.outStep == 901f)
            {
                step_B_Ally_ChainJobGroup_02_Ally02.RunIt();

                if (step_B_Ally_ChainJobGroup_02_Ally02.status == LogicJobStatus.Finished)
                {
                    step_B_Ally_ChainJobGroup_02_Ally02.SetOutStep(1000);
                }
            }
            #endregion

            #endregion

            #region step_B_Ally_ChainJobGroup_03_Ally03

            #region 1 Start
            if (step_B_Ally_ChainJobGroup_03_Ally03.outStep == 1)
            {
                step_B_Ally_ChainJobGroup_03_Ally03.Init_SetNewGlobalLogicIndex(step_B_Ally03ChainJobsGlobalLogicIndex);
                step_B_Ally_ChainJobGroup_03_Ally03.StartIt();

                step_B_Ally_ChainJobGroup_03_Ally03.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_B_Ally_ChainJobGroup_03_Ally03.outStep == 1.1f)
            {
                step_B_Ally_ChainJobGroup_03_Ally03.RunIt();
            }
            #endregion

            #region 900 Start finishing
            if (step_B_Ally_ChainJobGroup_03_Ally03.outStep == 900f)
            {
                step_B_Ally_ChainJobGroup_03_Ally03.SetNeedsToBeFinished();

                step_B_Ally_ChainJobGroup_03_Ally03.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (step_B_Ally_ChainJobGroup_03_Ally03.outStep == 901f)
            {
                step_B_Ally_ChainJobGroup_03_Ally03.RunIt();

                if (step_B_Ally_ChainJobGroup_03_Ally03.status == LogicJobStatus.Finished)
                {
                    step_B_Ally_ChainJobGroup_03_Ally03.SetOutStep(1000);
                }
            }
            #endregion

            #endregion

            #region step_B_Ally_ChainJobGroup_04_Ally04

            #region 1 Start
            if (step_B_Ally_ChainJobGroup_04_Ally04.outStep == 1)
            {
                step_B_Ally_ChainJobGroup_04_Ally04.Init_SetNewGlobalLogicIndex(step_B_Ally04ChainJobsGlobalLogicIndex);
                step_B_Ally_ChainJobGroup_04_Ally04.StartIt();

                step_B_Ally_ChainJobGroup_04_Ally04.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_B_Ally_ChainJobGroup_04_Ally04.outStep == 1.1f)
            {
                step_B_Ally_ChainJobGroup_04_Ally04.RunIt();
            }
            #endregion

            #region 900 Start finishing
            if (step_B_Ally_ChainJobGroup_04_Ally04.outStep == 900f)
            {
                step_B_Ally_ChainJobGroup_04_Ally04.SetNeedsToBeFinished();

                step_B_Ally_ChainJobGroup_04_Ally04.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (step_B_Ally_ChainJobGroup_04_Ally04.outStep == 901f)
            {
                step_B_Ally_ChainJobGroup_04_Ally04.RunIt();

                if (step_B_Ally_ChainJobGroup_04_Ally04.status == LogicJobStatus.Finished)
                {
                    step_B_Ally_ChainJobGroup_04_Ally04.SetOutStep(1000);
                }
            }
            #endregion

            #endregion

            #region step_B_Objects_01_StartingLevelTrigger

            #region 1 Start
            if (step_B_Objects_01_Part2MortarsTrigger.OutStep == 1) //Start
            {
                step_B_Objects_01_Part2MortarsTrigger.SetEnabled(true);
                step_B_Objects_01_Part2MortarsTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_B_Objects_01_Part2MortarsTrigger.OutStep == 1.1f) //Run
            {
                if (step_B_Objects_01_Part2MortarsTrigger.IsPlayerIn())
                {
                    step_B_PlayerEntered_Part2MortarsTrigger.SetStatus(LogicFlagStatus.Active);
                    step_B_Objects_01_Part2MortarsTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_B_Objects_01_Part2MortarsTrigger.OutStep == 900f) //Finish
            {
                step_B_Objects_01_Part2MortarsTrigger.SetEnabled(false);
                step_B_Objects_01_Part2MortarsTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_B_Objects_02_FallingPalmTrigger

            #region 1 Start
            if (step_B_Objects_02_FallingPalmTrigger.OutStep == 1) //Start
            {
                step_B_Objects_02_FallingPalmTrigger.SetEnabled(true);
                step_B_Objects_02_FallingPalmTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_B_Objects_02_FallingPalmTrigger.OutStep == 1.1f) //Run
            {
                if (step_B_Objects_02_FallingPalmTrigger.IsPlayerIn())
                {
                    step_B_PlayerEntered_FallingPalmTrigger.SetStatus(LogicFlagStatus.Active);
                    step_B_Objects_02_FallingPalmTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_B_Objects_02_FallingPalmTrigger.OutStep == 900f) //Finish
            {
                step_B_Objects_02_FallingPalmTrigger.SetEnabled(false);
                step_B_Objects_02_FallingPalmTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_B_Objects_05_ExplodeHouseTrigger

            #region 1 Start
            if (step_B_Objects_05_ExplodeHouseTrigger.OutStep == 1) //Start
            {
                step_B_Objects_05_ExplodeHouseTrigger.SetEnabled(true);
                step_B_Objects_05_ExplodeHouseTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_B_Objects_05_ExplodeHouseTrigger.OutStep == 1.1f) //Run
            {
                if (step_B_Objects_05_ExplodeHouseTrigger.IsPlayerIn())
                {
                    step_B_PlayerEntered_ExplodeHouseTrigger.SetStatus(LogicFlagStatus.Active);
                    step_B_Objects_05_ExplodeHouseTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_B_Objects_05_ExplodeHouseTrigger.OutStep == 900f) //Finish
            {
                step_B_Objects_05_ExplodeHouseTrigger.SetEnabled(false);
                step_B_Objects_05_ExplodeHouseTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_B_Objects_07_Part3MortarsTrigger

            #region 1 Start
            if (step_B_Objects_07_Part3MortarsTrigger.OutStep == 1) //Start
            {
                step_B_Objects_07_Part3MortarsTrigger.SetEnabled(true);
                step_B_Objects_07_Part3MortarsTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_B_Objects_07_Part3MortarsTrigger.OutStep == 1.1f) //Run
            {
                if (step_B_Objects_07_Part3MortarsTrigger.IsPlayerIn())
                {
                    step_B_PlayerEntered_Part3MortarsTrigger.SetStatus(LogicFlagStatus.Active);
                    step_B_Objects_07_Part3MortarsTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_B_Objects_07_Part3MortarsTrigger.OutStep == 900f) //Finish
            {
                step_B_Objects_07_Part3MortarsTrigger.SetEnabled(false);
                step_B_Objects_07_Part3MortarsTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_B_Objects_08_Part4MortarsTrigger

            #region 1 Start
            if (step_B_Objects_08_Part4MortarsTrigger.OutStep == 1) //Start
            {
                step_B_Objects_08_Part4MortarsTrigger.SetEnabled(true);
                step_B_Objects_08_Part4MortarsTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_B_Objects_08_Part4MortarsTrigger.OutStep == 1.1f) //Run
            {
                if (step_B_Objects_08_Part4MortarsTrigger.IsPlayerIn())
                {
                    step_B_PlayerEntered_Part4MortarsTrigger.SetStatus(LogicFlagStatus.Active);
                    step_B_Objects_08_Part4MortarsTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_B_Objects_08_Part4MortarsTrigger.OutStep == 900f) //Finish
            {
                step_B_Objects_08_Part4MortarsTrigger.SetEnabled(false);
                step_B_Objects_08_Part4MortarsTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_B_Objects_09_BackFromPart4Trigger

            #region 1 Start
            if (step_B_Objects_09_BackFromPart4Trigger.OutStep == 1) //Start
            {
                step_B_Objects_09_BackFromPart4Trigger.SetEnabled(true);
                step_B_Objects_09_BackFromPart4Trigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_B_Objects_09_BackFromPart4Trigger.OutStep == 1.1f) //Run
            {
                if (step_B_Objects_09_BackFromPart4Trigger.IsPlayerIn())
                {
                    step_B_PlayerEntered_BackFromPart4Trigger.SetStatus(LogicFlagStatus.Active);
                    step_B_Objects_09_BackFromPart4Trigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_B_Objects_09_BackFromPart4Trigger.OutStep == 900f) //Finish
            {
                step_B_Objects_09_BackFromPart4Trigger.SetEnabled(false);
                step_B_Objects_09_BackFromPart4Trigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_B_Objects_10_ExitFromMortarsTrigger

            #region 1 Start
            if (step_B_Objects_10_ExitFromMortarsTrigger.OutStep == 1) //Start
            {
                step_B_Objects_10_ExitFromMortarsTrigger.SetEnabled(true);
                step_B_Objects_10_ExitFromMortarsTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_B_Objects_10_ExitFromMortarsTrigger.OutStep == 1.1f) //Run
            {
                if (step_B_Objects_10_ExitFromMortarsTrigger.IsPlayerIn())
                {
                    step_B_PlayerEntered_ExitFromMortarsTrigger.SetStatus(LogicFlagStatus.Active);
                    step_B_Objects_10_ExitFromMortarsTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_B_Objects_10_ExitFromMortarsTrigger.OutStep == 900f) //Finish
            {
                step_B_Objects_10_ExitFromMortarsTrigger.SetEnabled(false);
                step_B_Objects_10_ExitFromMortarsTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_B_Objects_13_BackFromExitTrigger

            #region 1 Start
            if (step_B_Objects_13_BackFromExitTrigger.OutStep == 1) //Start
            {
                step_B_Objects_13_BackFromExitTrigger.SetEnabled(true);
                step_B_Objects_13_BackFromExitTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_B_Objects_13_BackFromExitTrigger.OutStep == 1.1f) //Run
            {
                if (step_B_Objects_13_BackFromExitTrigger.IsPlayerIn())
                {
                    step_B_PlayerEntered_BackFromExitTrigger.SetStatus(LogicFlagStatus.Active);
                    step_B_Objects_13_BackFromExitTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_B_Objects_13_BackFromExitTrigger.OutStep == 900f) //Finish
            {
                step_B_Objects_13_BackFromExitTrigger.SetEnabled(false);
                step_B_Objects_13_BackFromExitTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_B_Objects_14_EndMortarsTrigger

            #region 1 Start
            if (step_B_Objects_14_AlliesStartsTrigger.OutStep == 1) //Start
            {
                step_B_Objects_14_AlliesStartsTrigger.SetEnabled(true);
                step_B_Objects_14_AlliesStartsTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_B_Objects_14_AlliesStartsTrigger.OutStep == 1.1f) //Run
            {
                if (step_B_Objects_14_AlliesStartsTrigger.IsPlayerIn())
                {
                    step_B_PlayerEntered_AlliesStartsTrigger.SetStatus(LogicFlagStatus.Active);
                    step_B_Objects_14_AlliesStartsTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_B_Objects_14_AlliesStartsTrigger.OutStep == 900f) //Finish
            {
                step_B_Objects_14_AlliesStartsTrigger.SetEnabled(false);
                step_B_Objects_14_AlliesStartsTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_B_Objects_15_BackFromAlliesStartTrigger

            #region 1 Start
            if (step_B_Objects_15_BackFromAlliesStartTrigger.OutStep == 1) //Start
            {
                step_B_Objects_15_BackFromAlliesStartTrigger.SetEnabled(true);
                step_B_Objects_15_BackFromAlliesStartTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_B_Objects_15_BackFromAlliesStartTrigger.OutStep == 1.1f) //Run
            {
                if (step_B_Objects_15_BackFromAlliesStartTrigger.IsPlayerIn())
                {
                    step_B_PlayerEntered_BackFromAlliesTrigger.SetStatus(LogicFlagStatus.Active);
                    step_B_Objects_15_BackFromAlliesStartTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_B_Objects_15_BackFromAlliesStartTrigger.OutStep == 900f) //Finish
            {
                step_B_Objects_15_BackFromAlliesStartTrigger.SetEnabled(false);
                step_B_Objects_15_BackFromAlliesStartTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_B_Objects_16_EndStepTrigger

            #region 1 Start
            if (step_B_Objects_16_EndStepTrigger.OutStep == 1) //Start
            {
                step_B_Objects_16_EndStepTrigger.SetEnabled(true);
                step_B_Objects_16_EndStepTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_B_Objects_16_EndStepTrigger.OutStep == 1.1f) //Run
            {
                if (step_B_Objects_16_EndStepTrigger.IsPlayerIn())
                {
                    step_B_PlayerEntered_EndStepTrigger.SetStatus(LogicFlagStatus.Active);
                    step_B_Objects_16_EndStepTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_B_Objects_16_EndStepTrigger.OutStep == 900f) //Finish
            {
                step_B_Objects_16_EndStepTrigger.SetEnabled(false);
                step_B_Objects_16_EndStepTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #endregion

            #region C

            #region step_C_Ally_ChainJobGroup_01_Ally01

            #region 1 Start
            if (step_C_Ally_ChainJobGroup_01_Ally01.outStep == 1)
            {
                step_C_Ally_ChainJobGroup_01_Ally01.Init_SetNewGlobalLogicIndex(step_C_Ally01ChainJobsGlobalLogicIndex);
                step_C_Ally_ChainJobGroup_01_Ally01.StartIt();

                step_C_Ally_ChainJobGroup_01_Ally01.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_C_Ally_ChainJobGroup_01_Ally01.outStep == 1.1f)
            {
                step_C_Ally_ChainJobGroup_01_Ally01.RunIt();
            }
            #endregion

            #region 900 Start finishing
            if (step_C_Ally_ChainJobGroup_01_Ally01.outStep == 900f)
            {
                step_C_Ally_ChainJobGroup_01_Ally01.SetNeedsToBeFinished();

                step_C_Ally_ChainJobGroup_01_Ally01.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (step_C_Ally_ChainJobGroup_01_Ally01.outStep == 901f)
            {
                step_C_Ally_ChainJobGroup_01_Ally01.RunIt();

                if (step_C_Ally_ChainJobGroup_01_Ally01.status == LogicJobStatus.Finished)
                {
                    step_C_Ally_ChainJobGroup_01_Ally01.SetOutStep(1000);
                }
            }
            #endregion

            #endregion

            #region step_C_Ally_ChainJobGroup_02_Ally02

            #region 1 Start
            if (step_C_Ally_ChainJobGroup_02_Ally02.outStep == 1)
            {
                step_C_Ally_ChainJobGroup_02_Ally02.Init_SetNewGlobalLogicIndex(step_C_Ally02ChainJobsGlobalLogicIndex);
                step_C_Ally_ChainJobGroup_02_Ally02.StartIt();

                step_C_Ally_ChainJobGroup_02_Ally02.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_C_Ally_ChainJobGroup_02_Ally02.outStep == 1.1f)
            {
                step_C_Ally_ChainJobGroup_02_Ally02.RunIt();
            }
            #endregion

            #region 900 Start finishing
            if (step_C_Ally_ChainJobGroup_02_Ally02.outStep == 900f)
            {
                step_C_Ally_ChainJobGroup_02_Ally02.SetNeedsToBeFinished();

                step_C_Ally_ChainJobGroup_02_Ally02.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (step_C_Ally_ChainJobGroup_02_Ally02.outStep == 901f)
            {
                step_C_Ally_ChainJobGroup_02_Ally02.RunIt();

                if (step_C_Ally_ChainJobGroup_02_Ally02.status == LogicJobStatus.Finished)
                {
                    step_C_Ally_ChainJobGroup_02_Ally02.SetOutStep(1000);
                }
            }
            #endregion

            #endregion

            #region step_C_Ally_ChainJobGroup_03_Ally03

            #region 1 Start
            if (step_C_Ally_ChainJobGroup_03_Ally03.outStep == 1)
            {
                step_C_Ally_ChainJobGroup_03_Ally03.Init_SetNewGlobalLogicIndex(step_C_Ally03ChainJobsGlobalLogicIndex);
                step_C_Ally_ChainJobGroup_03_Ally03.StartIt();

                step_C_Ally_ChainJobGroup_03_Ally03.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_C_Ally_ChainJobGroup_03_Ally03.outStep == 1.1f)
            {
                step_C_Ally_ChainJobGroup_03_Ally03.RunIt();
            }
            #endregion

            #region 900 Start finishing
            if (step_C_Ally_ChainJobGroup_03_Ally03.outStep == 900f)
            {
                step_C_Ally_ChainJobGroup_03_Ally03.SetNeedsToBeFinished();

                step_C_Ally_ChainJobGroup_03_Ally03.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (step_C_Ally_ChainJobGroup_03_Ally03.outStep == 901f)
            {
                step_C_Ally_ChainJobGroup_03_Ally03.RunIt();

                if (step_C_Ally_ChainJobGroup_03_Ally03.status == LogicJobStatus.Finished)
                {
                    step_C_Ally_ChainJobGroup_03_Ally03.SetOutStep(1000);
                }
            }
            #endregion

            #endregion

            #region step_C_Ally_ChainJobGroup_04_Ally04

            #region 1 Start
            if (step_C_Ally_ChainJobGroup_04_Ally04.outStep == 1)
            {
                step_C_Ally_ChainJobGroup_04_Ally04.Init_SetNewGlobalLogicIndex(step_C_Ally04ChainJobsGlobalLogicIndex);
                step_C_Ally_ChainJobGroup_04_Ally04.StartIt();

                step_C_Ally_ChainJobGroup_04_Ally04.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_C_Ally_ChainJobGroup_04_Ally04.outStep == 1.1f)
            {
                step_C_Ally_ChainJobGroup_04_Ally04.RunIt();
            }
            #endregion

            #region 900 Start finishing
            if (step_C_Ally_ChainJobGroup_04_Ally04.outStep == 900f)
            {
                step_C_Ally_ChainJobGroup_04_Ally04.SetNeedsToBeFinished();

                step_C_Ally_ChainJobGroup_04_Ally04.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (step_C_Ally_ChainJobGroup_04_Ally04.outStep == 901f)
            {
                step_C_Ally_ChainJobGroup_04_Ally04.RunIt();

                if (step_C_Ally_ChainJobGroup_04_Ally04.status == LogicJobStatus.Finished)
                {
                    step_C_Ally_ChainJobGroup_04_Ally04.SetOutStep(1000);
                }
            }
            #endregion

            #endregion

            #region step_C_Enemy_FightInRegsGroup_Static

            #region 1 Start
            if (step_C_Enemy_FightInRegsGroup_Static.outStep == 1f)
            {
                step_C_Enemy_FightInRegsGroup_Static.StartIt();
                step_C_Enemy_FightInRegsGroup_Static.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_C_Enemy_FightInRegsGroup_Static.outStep == 1.1f)
            {
                step_C_Enemy_FightInRegsGroup_Static.RunIt();

                if (step_C_Enemy_FightInRegsGroup_Static.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    step_C_Enemy_FightInRegsGroup_Static.StartFinishing_OutStepIfNotFinishing();
                    goto step_C_Enemy_FightInRegsGroup_01_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (step_C_Enemy_FightInRegsGroup_Static.outStep == 900f)
            {
                step_C_Enemy_FightInRegsGroup_Static.SetNeedsToBeFinished();
                step_C_Enemy_FightInRegsGroup_Static.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (step_C_Enemy_FightInRegsGroup_Static.outStep == 901f)
            {
                step_C_Enemy_FightInRegsGroup_Static.RunIt();

                if (step_C_Enemy_FightInRegsGroup_Static.status == LogicJobStatus.Finished)
                {
                    step_C_Enemy_FightInRegsGroup_Static.SetOutStep(1000f);
                    step_C_FightStatic_Finished.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

        step_C_Enemy_FightInRegsGroup_01_End: ;

            #endregion

            #region step_C_Enemy_FightInRegsGroup_Rush

            #region 1 Start
            if (step_C_Enemy_FightInRegsGroup_Rush.outStep == 1f)
            {
                step_C_Enemy_FightInRegsGroup_Rush.StartIt();
                step_C_Enemy_FightInRegsGroup_Rush.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_C_Enemy_FightInRegsGroup_Rush.outStep == 1.1f)
            {
                step_C_Enemy_FightInRegsGroup_Rush.RunIt();

                if (step_C_Enemy_FightInRegsGroup_Rush.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    step_C_Enemy_FightInRegsGroup_Rush.StartFinishing_OutStepIfNotFinishing();
                    goto step_C_Enemy_FightInRegsGroup_02_End;
                }
            }
            #endregion

            #region 900 Start Finishing
            if (step_C_Enemy_FightInRegsGroup_Rush.outStep == 900f)
            {
                step_C_Enemy_FightInRegsGroup_Rush.SetNeedsToBeFinished();
                step_C_Enemy_FightInRegsGroup_Rush.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (step_C_Enemy_FightInRegsGroup_Rush.outStep == 901f)
            {
                step_C_Enemy_FightInRegsGroup_Rush.RunIt();

                if (step_C_Enemy_FightInRegsGroup_Rush.status == LogicJobStatus.Finished)
                {
                    step_C_Enemy_FightInRegsGroup_Rush.SetOutStep(1000f);
                    step_C_FightRush_Finished.SetStatus(LogicFlagStatus.Active);
                }
            }
            #endregion

        step_C_Enemy_FightInRegsGroup_02_End: ;

            #endregion

            #region step_C_Objects_01_CountingInsideSoldiersTrigger

            #region 1 Start
            if (step_C_Objects_01_CountingInsideSoldiersTrigger.OutStep == 1) //Start
            {
                step_C_Objects_01_CountingInsideSoldiersTrigger.SetEnabled(true);
                step_C_Objects_01_CountingInsideSoldiersTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_C_Objects_01_CountingInsideSoldiersTrigger.OutStep == 1.1f) //Run
            {
                if (step_C_Objects_01_CountingInsideSoldiersTrigger.IsInsideObjsCountEqualOrBiggerThanValue(step_C_NumOfInsideTriggerSoldiersToFail))
                {
                    step_C_CountingInsideSoldierTrigger.SetStatus(LogicFlagStatus.Active);
                    step_C_Objects_01_CountingInsideSoldiersTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_C_Objects_01_CountingInsideSoldiersTrigger.OutStep == 900f) //Finish
            {
                step_C_Objects_01_CountingInsideSoldiersTrigger.SetEnabled(false);
                step_C_Objects_01_CountingInsideSoldiersTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region step_C_Objects_02_GoToEnemyMortarsTrigger

            #region 1 Start
            if (step_C_Objects_02_GoToEnemyMortarsTrigger.OutStep == 1) //Start
            {
                step_C_Objects_02_GoToEnemyMortarsTrigger.SetEnabled(true);
                step_C_Objects_02_GoToEnemyMortarsTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_C_Objects_02_GoToEnemyMortarsTrigger.OutStep == 1.1f) //Run
            {
                if (step_C_Objects_02_GoToEnemyMortarsTrigger.IsPlayerIn())
                {
                    step_C_GoToEnemyMortarsTrigger.SetStatus(LogicFlagStatus.Active);
                    step_C_Objects_02_GoToEnemyMortarsTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_C_Objects_02_GoToEnemyMortarsTrigger.OutStep == 900f) //Finish
            {
                step_C_Objects_02_GoToEnemyMortarsTrigger.SetEnabled(false);
                step_C_Objects_02_GoToEnemyMortarsTrigger.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #endregion
        }
    }

    void MakeTreeFall(GameObject _animatedFallingTree, bool _isFromLoad, string _animName)
    {
        GameObject animObj = _animatedFallingTree;
        bool isFromLoad = _isFromLoad;
        string animName = _animName;

        if (!isFromLoad)
            animObj.animation.Play();
        else
        {
            animObj.animation.Play(animName);
            animObj.animation[animName].time = animObj.animation[animName].length;
        }

        //Collider[] col;
        //col = animObj.GetComponentsInChildren<Collider>();

        //foreach (Collider c in col)
        //{
        //    c.enabled = false;
        //}
    }

    void RunParticlesFromRoot(GameObject _root)
    {
        GameObject root = _root;

        int count = root.transform.GetChildCount();

        for (int i = 0; i < count; i++)
        {
            GameObject particleGameObject = (root.transform.GetChild(i)).gameObject;

            RunParticle(particleGameObject);
        }
    }

    void RunParticle(ParticleSystem _particle)
    {
        ParticleSystem particle = _particle;

        particle.Play();
    }

    void RunParticle(GameObject _particle)
    {
        GameObject particle = _particle;

        particle.SetActiveRecursively(true);
    }

    void StopParticleFromRoot(GameObject _root)
    {
        GameObject root = _root;

        int count = root.transform.GetChildCount();

        for (int i = 0; i < count; i++)
        {
            GameObject particleGameObject = (root.transform.GetChild(i)).gameObject;

            StopParticle(particleGameObject);
        }
    }

    void StopParticle(ParticleSystem _particle)
    {
        ParticleSystem particle = _particle;

        particle.Stop();
    }

    void StopParticle(GameObject _particle)
    {
        GameObject particle = _particle;

        particle.SetActiveRecursively(false);
    }

    void SetGameObjectsRender(List<GameObject> _gObjs, bool _shouldRender)
    {
        List<GameObject> gobjs = _gObjs;
        bool render = _shouldRender;

        foreach (GameObject obj in gobjs)
        {
            obj.SetActiveRecursively(render);
        }
    }

    void BurnTheMan(GameObject _datMan)
    {
        GameObject datMan = _datMan;

        datMan.SetActiveRecursively(true);

        BurningManInfo burningManInfo = datMan.GetComponent<BurningManInfo>();

        burningManInfo.SetAnimObj();

        burningManInfo.animObject.animation.Play();

        burningManInfo.burningManAudio.Play();

        burningManInfo.fireOfBurningAudio.Play();
    }

    public override void LoadCheckPoint(float _levelStep)
    {
        base.LoadCheckPoint(_levelStep);

        #region CheckPoint B
        if (levelStep == 2)
        {
            step_B_IsFromLoad = true;

            mapLogic.HUD_ShowNewMission(0);

            step_B_Objects_StartPoint_Player.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);

            MakeTreeFall(step_A_Objects_07_AnimatedFallingTree, true, "Palm Falling");

            step_A_Objects_08_ColliderExit.active = true;

            step_A_Objects_08_ColliderExit_B.active = true;

            return;
        }
        #endregion

        #region CheckPoint C
        if (levelStep == 3f)
        {
            step_C_Objects_StartPoint_Player.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);
            step_C_Objects_StartPoint_Ally01.PlaceCharacterOnIt(Ally01);
            step_C_Objects_StartPoint_Ally02.PlaceCharacterOnIt(Ally02);
            step_C_Objects_StartPoint_Ally03.PlaceCharacterOnIt(Ally03);
            step_C_Objects_StartPoint_Ally04.PlaceCharacterOnIt(Ally04);

            RunParticle(step_B_Objects_11_SmokeDenseRoot);
            RunParticle(step_B_Objects_12_FinalSmokeRoot);

            step_C_IsLoadedFromCheckPoint = true;
        }
        #endregion
    }

    void PlayFirstMusic()
    {
        MusicController.Instance.PlayMusic(MusicSong.Action_B, 0);
    }

    void PlaySecondMusic()
    {
        MusicController.Instance.PlayMusic(MusicSong.Action_E, 0);
    }
}