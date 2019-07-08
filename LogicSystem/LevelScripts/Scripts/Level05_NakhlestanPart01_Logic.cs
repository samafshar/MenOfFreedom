using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level05_NakhlestanPart01_Logic : LevelLogic
{
    SoldierGun ally01GunInfo;
    SoldierGun ally02GunInfo;
    SoldierGun ally03GunInfo;
    SoldierGun ally04GunInfo;
    SoldierGun khalooGunInfo;

    public bool isForPart2 = false;

    public GameObject Khaloo;
    public GameObject Ally01;
    public GameObject Ally02;
    public GameObject Ally03;
    public GameObject Ally04;

    public LogicVoiceCollection logicVoiceCollection_Khaloo;

    public LogicVoiceCollection logicVoiceCollection_Ally01;

    public LogicVoiceCollection logicVoiceCollection_Ally02;

    public LogicVoiceCollection logicVoiceCollection_Ally03;

    public CutsceneController cutsceneFirst;

    public CutsceneController cutsceneSorkhordan;

    public CutsceneController cutsceneParidanAzDivar;

    public CutsceneController cutsceneLast;

    public CutsceneController cutsceneBombaraneNakhlestan_Part1;

    public CutsceneController cutsceneBombaraneNakhlestan;

    //

    public Transform miniMapObjectTransform_01_TaheKuche;
    public Transform miniMapObjectTransform_02_EndStepE;
    public Transform miniMapObjectTransform_03_EndStepEP2;
    public Transform miniMapObjectTransform_04_PaiineSarBalaii;
    public Transform miniMapObjectTransform_05_SangareMoghavematKardan;

    //

    bool loadFromSaveGame = false;

    List<GameObject> tempListForCharacters = new List<GameObject>();

    #region Step_A Variables
    //Enemy
    public MapLogicJob_FightInRegsGroup step_A_Enemy_FightInRegsGroup_01;
    public MapLogicJob_FightInRegsGroup step_A_Enemy_FightInRegsGroup_02;

    //Ally
    public MapLogicJob_FightInReg step_A_Ally_KhalooStartFightReg;

    //Objects
    public LogicTrigger step_A_Objects_StartingLevelTrigger01;
    public ExecutionArea step_A_Objects_ExecutionArea01;
    public LogicTrigger step_A_Objects_StartingLevelTrigger02;
    public ExecutionArea step_A_Objects_ExecutionArea02;
    public LogicTrigger step_A_Objects_StartKhalooTrigger;
    public LogicTrigger step_A_Objects_ExitStepATrigger;
    public LogicDieTrigger step_A_Objects_ExitStepADieTrigger;

    public ExecutionArea step_A_Objects_ExecutionAreaExitStep;

    public Transform stepA_HUD_MinimapTr_Khaloo;

    LogicFlag step_A_PlayerEntered_StartingLevelTrigger01 = new LogicFlag();
    LogicFlag step_A_PlayerEntered_StartingLevelTrigger02 = new LogicFlag();
    LogicFlag step_A_PlayerEntered_StartKhalooTrigger = new LogicFlag();
    LogicFlag step_A_Fight01_Finished = new LogicFlag();
    LogicFlag step_A_Fight02_Finished = new LogicFlag();
    LogicFlag step_A_StartingKhaloo_Finished = new LogicFlag();
    LogicFlag step_A_PlayerEntered_ExitStepATrigger = new LogicFlag();
    #endregion

    public string __________________________;

    #region Step_B_Variables

    //Enemy
    public MapLogicJob_FightInRegsGroup step_B_Enemy_FightInRegsGroup_01;
    public MapLogicJob_FightInRegsGroup step_B_Enemy_FightInRegsGroup_02;
    public MapLogicJob_FightInRegsGroup step_B_Enemy_FightInRegsGroup_03;
    public MapLogicJob_MachineGun step_B_Enemy_MachineGun_01;
    public MapLogicJob_MachineGun step_B_Enemy_MachineGun_02;

    //Ally
    public MapLogicJob_ChainJobsGroup step_B_Ally_chainJobGroup_01_khalooStart;
    public MapLogicJob_ChainJobsGroup step_B_Ally_chainJobGroup_02_Dustashun;

    //Objects
    public LogicDieTrigger step_B_Objects_KillStepASoldiers;
    public LogicTrigger step_B_Objects_01_StartingStepBTrigger;
    public LogicTrigger step_B_Objects_02_KhalooChangePos;
    public LogicTrigger step_B_Objects_03_KhalooChangePos02;
    public LogicTrigger step_B_Objects_04_StartFightReg02Trigger;
    public ExecutionArea step_B_Objects_04_Fight2Execution;
    public ExecutionArea step_B_Objects_04_Fight2Execution2;
    public LogicTrigger step_B_Objects_05_KhalooCommandTrigger;
    public LogicDieTrigger step_B_Objects_06_LogicDieTriggerKhalooEnemy;
    public LogicTrigger step_B_Objects_07_CutsceneBTrigger;
    public LogicTrigger step_B_Objects_08_EndStepB;
    public LogicTrigger step_B_Objects_09_FailMissionTrigger;
    public LogicDieTrigger step_B_Objects_09_LogicDieTriggerAll;

    public StartPoint step_B_Objects_StartPoint_Player;
    public StartPoint step_B_Objects_StartPoint_Khaloo;

    //
    public float step_B_MaxTimForFightReg02_Reg03Creation = 10f;

    public int step_B_MinNumOfCreatedSoldiersInFightReg02 = 10;

    LogicFlag step_B_Fight01_Finished = new LogicFlag();
    LogicFlag step_B_Fight02_Finished = new LogicFlag();
    LogicFlag step_B_Fight03_Finished = new LogicFlag();
    LogicFlag step_B_ChainJobDustashun_Finished = new LogicFlag();
    LogicFlag step_B_ChainJobKhaloo_Finished = new LogicFlag();
    LogicFlag step_B_SomeOneEntered_StartingStepBTrigger = new LogicFlag();
    LogicFlag step_B_PlayerEntered_KhalooChangePosTrigger = new LogicFlag();
    LogicFlag step_B_PlayerEntered_KhalooChangePos2Trigger = new LogicFlag();
    LogicFlag step_B_PlayerEntered_StartFightReg2Trigger = new LogicFlag();
    LogicFlag step_B_SomeOneEntered_KhalooCommandTrigger = new LogicFlag();
    LogicFlag step_B_PlayerEntered_EndStepBTrigger = new LogicFlag();
    LogicFlag step_B_PlayerEntered_CutsceneBTrigger = new LogicFlag();
    LogicFlag step_B_PlayerEntered_FailMissionTrigger = new LogicFlag();

    float step_B_TimerForFightReg02_Reg03Creation;

    float step_B_MaxTimeToGetNumOfCreatedSoldierInReg02 = 1f;
    float step_B_TimerToGetNumOfCreatedSoldierInReg02;

    int step_B_NumbersOfCreatedSoldiersInFightReg02 = 0;

    int step_B_KhalooChainJobsGlobalLogicIndex = -1;
    int step_B_AllyChainJobsGlobalLogicIndex = -1;

    #endregion

    public string ___________________________;

    #region Step_C Variables

    //Ally
    public MapLogicJob_ChainJobsGroup step_C_Ally_chainJobGroup_01_Khaloo;
    public MapLogicJob_ChainJobsGroup step_C_Ally_chainJobGroup_02_Ally01;
    public MapLogicJob_ChainJobsGroup step_C_Ally_chainJobGroup_03_Ally02;

    //Enemy
    public MapLogicJob_FightInRegsGroup step_C_Enemy_FightInRegsGroup_01;
    public MapLogicJob_FightInRegsGroup step_C_Enemy_FightInRegsGroup_02;
    public MapLogicJob_FightInRegsGroup step_C_Enemy_FightInRegsGroup_03;

    //Objects
    public LogicTrigger step_C_Objects_01_StartFight01Trigger;
    public LogicTrigger step_C_Objects_02_StartFight02Trigger;
    public LogicDieTrigger step_C_Objects_02_DieTrigger;
    public LogicTrigger step_C_Objects_03_StartFight03Trigger;
    public LogicTrigger step_C_Objects_04_StopFight03Trigger;
    public LogicTrigger step_C_Objects_06_ExitStepTrigger;
    public LogicDieTrigger step_C_Objects_07_DieTrigger;

    public ExecutionArea step_C_Objects_05_ExecutionAreaExitFightReg03;
    public ExecutionArea step_C_Objects_05_ExecutionAreaExitFightReg02;

    public StartPoint step_C_Objects_StartPoint_Player;
    public StartPoint step_C_Objects_StartPoint_Khaloo;
    public StartPoint step_C_Objects_StartPoint_Ally01;
    public StartPoint step_C_Objects_StartPoint_Ally02;

    //Others
    public float step_C_Enemy_Fight02_MaxTime = 10f;
    public float step_C_Ally_Fight03MaxTimeBeforeProgress = 10f;

    //
    float step_C_Enemy_Fight02_Timer = 0f;
    float step_C_Ally_Fight03BeforeProgressTimer = 0f;

    float step_C_Timer;
    float step_C_MaxTimeToKillFight1 = 3f;

    bool step_C_Fight01Died = false;

    LogicFlag step_C_Fight01_Finished = new LogicFlag();
    LogicFlag step_C_Fight02_Finished = new LogicFlag();
    LogicFlag step_C_Fight03_Finished = new LogicFlag();
    LogicFlag step_C_ChainJobKhaloo_Finished = new LogicFlag();
    LogicFlag step_C_ChainJobAlly01_Finished = new LogicFlag();
    LogicFlag step_C_ChainJobAlly02_Finished = new LogicFlag();
    LogicFlag step_C_PlayerEntered_StartFight01Trigger = new LogicFlag();
    LogicFlag step_C_PlayerEntered_StartFight02Trigger = new LogicFlag();
    LogicFlag step_C_PlayerEntered_StartFight03Trigger = new LogicFlag();
    LogicFlag step_C_PlayerEntered_StopFight03Trigger = new LogicFlag();
    LogicFlag step_C_PlayerEntered_ExitStepTrigger = new LogicFlag();

    int step_C_KhalooChainJobsGlobalLogicIndex = -1;
    int step_C_Ally01ChainJobsGlobalLogicIndex = -1;
    int step_C_Ally02ChainJobsGlobalLogicIndex = -1;

    #endregion

    public string ____________________________;

    #region Step_D Variables

    //Ally
    public MapLogicJob_ChainJobsGroup step_D_Ally_chainJobGroup_01_Khaloo;
    public MapLogicJob_ChainJobsGroup step_D_Ally_chainJobGroup_02_Ally01;
    public MapLogicJob_ChainJobsGroup step_D_Ally_chainJobGroup_03_Ally02;

    public MapLogicJob_ChainJobsGroup step_D_Ally_chainJobGroup_01_Khaloo_C01;
    public MapLogicJob_ChainJobsGroup step_D_Ally_chainJobGroup_02_Ally01_C01;
    public MapLogicJob_ChainJobsGroup step_D_Ally_chainJobGroup_03_Ally02_C01;

    public MapLogicJob_ChainJobsGroup step_D_Ally_chainJobGroup_01_Khaloo_C02;
    public MapLogicJob_ChainJobsGroup step_D_Ally_chainJobGroup_02_Ally01_C02;
    public MapLogicJob_ChainJobsGroup step_D_Ally_chainJobGroup_03_Ally02_C02;

    //Enemy
    public MapLogicJob_FightInRegsGroup step_D_Enemy_FightInRegsGroup_01;
    public MapLogicJob_FightInRegsGroup step_D_Enemy_FightInRegsGroup_02;
    public MapLogicJob_FightInRegsGroup step_D_Enemy_FightInRegsGroup_03;
    public MapLogicJob_FightInRegsGroup step_D_Enemy_FightInRegsGroup_04;
    public MapLogicJob_FightInRegsGroup step_D_Enemy_FightInRegsGroup_05;
    public MapLogicJob_FightInRegsGroup step_D_Enemy_FightInRegsGroup_C02;
    public MapLogicJob_FightInRegsGroup step_D_Enemy_FightInRegsGroup_C03;
    public MapLogicJob_FightInRegsGroup step_D_Enemy_FightInRegsGroup_Fail;
    public MapLogicJob_MachineGun step_D_Enemy_MachineGun_01;
    public MapLogicJob_MachineGun step_D_Enemy_MachineGun_02;
    public MapLogicJob_MachineGun step_D_Enemy_MachineGun_C01;
    public MapLogicJob_MachineGun step_D_Enemy_MachineGun_C02;

    //Objects
    public ExecutionArea step_D_Objects_01_ExecutionArea01;
    public LogicTrigger step_D_Objects_02_StopFight01;
    public GameObject[] step_D_Objects_03_SignallingBoshkes;
    public ParticleSystem step_D_Objects_04_Particle01;
    public ParticleSystem step_D_Objects_05_Particle02;
    public GameObject[] step_D_Objects_06_HedgesToDestroy;
    public LogicTrigger step_D_Objects_07_CutSceneTrigger;
    public LogicTrigger step_D_Objects_08_AzPoshtTrigger;
    public GameObject[] step_D_Objects_08_BoshkesDakheleKhune;
    public LogicTrigger step_D_Objects_08_FailTrigger;
    public GameObject[] step_D_Objects_08_FiresOfMachineGuns;
    public LogicTrigger step_D_Objects_09_AllyDoDmgTrigger;
    public ExecutionArea step_D_Objects_10_ExecutionArea02;
    public LogicTrigger step_D_Objects_11_EndOfStepDTrigger;
    public LogicDieTrigger step_D_Objects_12_DieTrigger;
    public StartPoint step_D_Objects_StartPoint_Player;
    public StartPoint step_D_Objects_StartPoint_Khaloo;
    public StartPoint step_D_Objects_StartPoint_Ally01;
    public StartPoint step_D_Objects_StartPoint_Ally02;
    public StartPoint step_D_Objects_StartPoint_PlayerAfterSor;
    public StartPoint step_D_Objects_StartPoint_PlayerBeforeHouseBack;
    public StartPoint step_D_Objects_StartPoint_PlayerAfterCutsceneParidaneDivar;
    public StartPoint step_D_Objects_StartPoint_Khaloo_C;
    public StartPoint step_D_Objects_StartPoint_Ally01_C;
    public StartPoint step_D_Objects_StartPoint_Ally02_C;

    public Transform step_D_3DObj_Wall;
    public Transform step_D_3DObj_BoshkesMachineGuns;

    //
    public float step_D_MaxTimeForKhalooStartAction;

    float step_D_TimerForKhalooStartAction = 0f;
    float step_D_MaxTimeForDialogBayadBeriUnvar = 1.5f;
    float step_D_MaxTimeForDialogBayadBeriUnvarLoop = 3f;
    float step_D_MaxTimeForDialogBoroChapLoop = 3f;
    float step_D_TimerBetweenDialogs = 0f;

    float step_D_Timer = 0f;
    float step_D_MaxTimeToShowHUD = 12f;
    float step_D_MaxTimeToFail = 2f;

    LogicFlag step_D_Fight01_Finished = new LogicFlag();
    LogicFlag step_D_Fight02_Finished = new LogicFlag();
    LogicFlag step_D_Fight03_Finished = new LogicFlag();
    LogicFlag step_D_Fight04_Finished = new LogicFlag();
    LogicFlag step_D_Fight05_Finished = new LogicFlag();
    LogicFlag step_D_FightC02_Finished = new LogicFlag();
    LogicFlag step_D_FightC03_Finished = new LogicFlag();
    LogicFlag step_D_PlayerEntered_StopFight01Trigger = new LogicFlag();
    LogicFlag step_D_PlayerEntered_CutSceneTrigger = new LogicFlag();
    LogicFlag step_D_PlayerEntered_AzPoshtTrigger = new LogicFlag();
    LogicFlag step_D_PlayerEntered_FailTrigger = new LogicFlag();
    LogicFlag step_D_PlayerEntered_AllyDoDmgTrigger = new LogicFlag();
    LogicFlag step_D_PlayerEntered_EndOfStepDTrigger = new LogicFlag();
    LogicFlag step_D_VoiceCommand_01_Khaloo_InjuriNemishe = new LogicFlag();
    LogicFlag step_D_VoiceCommand_02_Ally_Chekonim = new LogicFlag();
    LogicFlag step_D_VoiceCommand_03_Khaloo_Boshke = new LogicFlag();
    LogicFlag step_D_VoiceCommand_04_Khaloo_KhosroBoro = new LogicFlag();
    LogicFlag step_D_ChainJobKhaloo_Finished = new LogicFlag();
    LogicFlag step_D_ChainJobAlly01_Finished = new LogicFlag();
    LogicFlag step_D_ChainJobAlly02_Finished = new LogicFlag();
    LogicFlag step_D_FlagForCheckBoshkesExplosion = new LogicFlag();
    LogicFlag step_D_FlagForCheckBoshkesKhuneExplosion = new LogicFlag();

    bool step_D_ExplosionChecked = false;
    bool step_D_DamageOccuredInBoshkesKhune = false;

    bool step_D_IsLoadedToStepD2 = false;

    int step_D_KhalooChainJobsGlobalLogicIndex = -1;
    int step_D_Ally01ChainJobsGlobalLogicIndex = -1;
    int step_D_Ally02ChainJobsGlobalLogicIndex = -1;

    #endregion

    public string ______________________________;

    #region Step_E Variables

    //Ally
    public MapLogicJob_ChainJobsGroup step_E_Ally_chainJobGroup_01_Ally01;

    //Enemy
    public MapLogicJob_FightInRegsGroup step_E_Enemy_FightInRegsGroup_01;
    public MapLogicJob_FightInRegsGroup step_E_Enemy_FightInRegsGroup_02;
    public MapLogicJob_FightInRegsGroup step_E_Enemy_FightInRegsGroup_03;
    public MapLogicJob_FightInRegsGroup step_E_Enemy_FightInRegsGroup_04;
    public MapLogicJob_FightInRegsGroup step_E_Enemy_FightInRegsGroup_05;
    public MapLogicJob_FightInRegsGroup step_E_Enemy_FightInRegsGroup_06;

    //Object
    public LogicTrigger step_E_Objects_01_StartStepTrigger;
    public LogicTrigger step_E_Objects_02_StartFight02Trigger;
    public ExecutionArea step_E_Objects_03_ExecutionArea01;
    public LogicTrigger step_E_Objects_04_AllyChangePosTrigger;
    public LogicDieTrigger step_E_Objects_05_DieTriggerForFight01;
    public LogicTrigger step_E_Objects_06_StartFight04Trigger;
    public LogicTrigger step_E_Objects_07_StartFight05Trigger;
    public LogicDieTrigger step_E_Objects_08_DieTriggerForFight03;
    public ExecutionArea step_E_Objects_09_ExecutionArea02;
    public LogicTrigger step_E_Objects_10_AllyChangePos02Trigger;
    public LogicDieTrigger step_E_Objects_11_DieTriggerForFight04;
    public LogicTrigger step_E_Objects_12_AllyChangePos03Trigger;
    public LogicDieTrigger step_E_Objects_13_DieTriggerForFight05;
    public LogicTrigger step_E_Objects_14_StartFight06Trigger;
    public ExecutionArea step_E_Objects_15_ExecutionArea03;
    public LogicTrigger step_E_Objects_16_AllyChangePos04Trigger;
    public LogicTrigger step_E_Objects_17_EndTrigger;
    public LogicDieTrigger step_E_Objects_18_DieTriggerForFight06;

    public StartPoint step_E_Objects_StartPoint_Player;
    public StartPoint step_E_Objects_StartPoint_Ally01;
    public StartPoint step_EP2_Objects_StartPoint_Player;
    public StartPoint step_EP2_Objects_StartPoint_Ally01;

    //

    public float step_E_MaxTimeForFightReg03 = 5f;

    LogicFlag step_E_PlayerEntered_StartStepTrigger = new LogicFlag();
    LogicFlag step_E_PlayerEntered_StartFight02Trigger = new LogicFlag();
    LogicFlag step_E_PlayerEntered_AllyChangePosTrigger = new LogicFlag();
    LogicFlag step_E_PlayerEntered_StartFight04Trigger = new LogicFlag();
    LogicFlag step_E_PlayerEntered_StartFight05Trigger = new LogicFlag();
    LogicFlag step_E_PlayerEntered_AllyChangePos2Trigger = new LogicFlag();
    LogicFlag step_E_PlayerEntered_AllyChangePos3Trigger = new LogicFlag();
    LogicFlag step_E_PlayerEntered_StartFight06Trigger = new LogicFlag();
    LogicFlag step_E_PlayerEntered_AllyChangePos4Trigger = new LogicFlag();
    LogicFlag step_E_PlayerEntered_EndTrigger = new LogicFlag();
    LogicFlag step_E_Fight01_Finished = new LogicFlag();
    LogicFlag step_E_Fight02_Finished = new LogicFlag();
    LogicFlag step_E_Fight03_Finished = new LogicFlag();
    LogicFlag step_E_Fight04_Finished = new LogicFlag();
    LogicFlag step_E_Fight05_Finished = new LogicFlag();
    LogicFlag step_E_Fight06_Finished = new LogicFlag();
    LogicFlag step_E_ChainJobAlly01_Finished = new LogicFlag();

    //    
    float step_E_TimerForFightRegs = 0f;

    float step_E_TimerForFight01 = 0f;
    float step_E_MaxTimeForKillingFight01 = 6f;

    int step_E_Ally01ChainJobsGlobalLogicIndex = -1;

    #endregion

    public string _________________________________;

    #region Step_F Variables
    //Ally
    public MapLogicJob_ChainJobsGroup step_F_Ally_chainJobGroup_01_Ally01;
    public MapLogicJob_ChainJobsGroup step_F_Ally_chainJobGroup_02_Dustashun;

    //Enemy
    public MapLogicJob_FightInRegsGroup step_F_Enemy_FightInRegsGroup_01;
    public MapLogicJob_FightInRegsGroup step_F_Enemy_FightInRegsGroup_02;

    //Object
    public LogicTrigger step_F_Objects_01_StartStepTrigger;
    public LogicTrigger step_F_Objects_02_StartFight01Trigger;
    public LogicTrigger step_F_Objects_03_ExitFight01Trigger;
    public ExecutionArea step_F_Objects_04_ExecutionArea01;
    public LogicDieTrigger step_F_Objects_05_DieTriggerForFight01;
    public LogicTrigger step_F_Objects_06_StartFight02Trigger;
    public LogicTrigger step_F_Objects_07_PlayerInFight02Trigger;
    public AudioInfo step_F_Objects_08_AudioInfo_VoiceAAAAAAA;

    public Mortar[] step_F_Objects_Mortars;

    public StartPoint step_F_Objects_StartPoint_Player;
    public StartPoint step_F_Objects_StartPoint_Ally01;
    public StartPoint step_F_Objects_StartPoint_Ally02;
    public StartPoint step_F_Objects_StartPoint_Ally03;
    public StartPoint step_F_Objects_StartPoint_Ally04;

    //
    public float step_F_MaxTimeForFightBozorg;

    LogicFlag step_F_PlayerEntered_StartStepTrigger = new LogicFlag();
    LogicFlag step_F_PlayerEntered_StartFight01Trigger = new LogicFlag();
    LogicFlag step_F_PlayerEntered_ExitFight01Trigger = new LogicFlag();
    LogicFlag step_F_PlayerEntered_StartFight02Trigger = new LogicFlag();
    LogicFlag step_F_Fight01_Finished = new LogicFlag();
    LogicFlag step_F_Fight02_Finished = new LogicFlag();
    LogicFlag step_F_PlayerInFight02Trigger = new LogicFlag();

    float step_F_TimerForFightRegion = 0f;

    int step_F_Ally01ChainJobsGlobalLogicIndex = -1;
    int step_F_DustashunChainJobsGlobalLogicIndex = -1;

    bool step_F_IsLoaded = false;
    bool step_F_Ally01ChainJobStepChanged = false;

    float step_F_MortarsTimeCounter = 5;

    #endregion

    public override void StartIt()
    {
        base.StartIt();

        Initialize();

        //LoadCheckPoint(7.511f);
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
                if (isForPart2)
                {
                    StartPart2();
                }
                else
                {
                    SetLevelStep(1f);
                }
            }
            #endregion

            #region 1 Start First Trigger For Player Enter
            if (levelStep == 1f)
            {
                SaveCheckPoint(1f);

                step_A_Objects_StartingLevelTrigger01.StartOutStepIfNotStarted();

                PlayFirstMusic();

                SetLevelStep(1.1f);
            }
            #endregion

            #region 1.1 Wait For Trigger Active
            if (levelStep == 1.1f)
            {
                if (step_A_PlayerEntered_StartingLevelTrigger01.IsEverActivated())
                {
                    mapLogic.HUD_ObjectivesPage_SetActiveObjective(1);
                    mapLogic.HUD_ShowNewMission(0);

                    mapLogic.HUD_AddOnlyMinimap3DObj(stepA_HUD_MinimapTr_Khaloo, "Khaloo");

                    SetLevelStep(1.2f);
                }
            }
            #endregion

            #region 1.2 Start Enemy FightReg 01
            if (levelStep == 1.2f)
            {
                step_A_Objects_StartingLevelTrigger01.StartFinishing_OutStepIfNotFishining();

                step_A_Enemy_FightInRegsGroup_01.StartOutStepIfNotStarted();

                step_A_Objects_StartingLevelTrigger02.StartOutStepIfNotStarted();

                step_A_Objects_ExecutionArea01.StartIt();
                step_A_Objects_ExecutionArea02.StartIt();

                SetLevelStep(1.3f);
            }
            #endregion

            #region 1.3 Wait For Second Trigger
            if (levelStep == 1.3f)
            {
                if (step_A_PlayerEntered_StartingLevelTrigger02.IsEverActivated())
                {
                    SetLevelStep(1.4f);
                }
            }
            #endregion

            #region 1.4 Start Enemy FightReg 02
            if (levelStep == 1.4f)
            {
                step_A_Objects_StartingLevelTrigger02.StartFinishing_OutStepIfNotFishining();

                step_A_Enemy_FightInRegsGroup_02.StartOutStepIfNotStarted();

                step_A_Objects_StartKhalooTrigger.StartOutStepIfNotStarted();

                SetLevelStep(1.5f);
            }
            #endregion

            #region 1.5 Wait For Start Khaloo Trigger
            if (levelStep == 1.5f)
            {
                if (step_A_PlayerEntered_StartKhalooTrigger.IsEverActivated())
                {
                    SetLevelStep(1.6f);
                }
            }
            #endregion

            #region 1.6 Starting Khaloo
            if (levelStep == 1.6f)
            {
                step_A_Objects_StartKhalooTrigger.StartFinishing_OutStepIfNotFishining();

                AllyDoCriticalShoot(true, khalooGunInfo);

                step_A_Ally_KhalooStartFightReg.StartOutStepIfNotStarted();

                step_A_Objects_ExitStepATrigger.StartOutStepIfNotStarted();

                step_A_Objects_ExecutionAreaExitStep.StartIt();

                SetLevelStep(1.7f);
            }
            #endregion

            #region 1.7 Exit StepA Trigger
            if (levelStep == 1.7f)
            {
                if (step_A_PlayerEntered_ExitStepATrigger.IsEverActivated())
                {
                    step_A_Objects_ExitStepATrigger.StartFinishing_OutStepIfNotFishining();

                    step_A_Objects_ExitStepADieTrigger.StartIt();

                    step_A_Ally_KhalooStartFightReg.StartFinishing_OutStepIfNotFinishing();

                    step_A_Enemy_FightInRegsGroup_01.StopCreatingMoreSoldiers();

                    step_A_Enemy_FightInRegsGroup_02.StopCreatingMoreSoldiers();

                    SetLevelStep(1.8f);
                }
            }
            #endregion

            #region 1.8 Wait For Khaloo Finish Fight
            if (levelStep == 1.8f)
            {
                if (step_A_StartingKhaloo_Finished.IsEverActivated())
                {
                    step_A_Objects_ExecutionAreaExitStep.EndIt();

                    step_A_Objects_ExecutionArea01.EndIt();

                    step_A_Objects_ExecutionArea02.EndIt();

                    mapLogic.HUD_RemoveMinimap3DObj("Khaloo");

                    SetLevelStep(2f);
                }
            }
            #endregion

            #endregion

            //--------------------------------------------------------------------------------

            #region Step B

            #region 2 Start Khaloo Talking
            if (levelStep == 2f)
            {
                SaveCheckPoint(2);

                logicVoiceCollection_Khaloo.PlayName("B_01_Khosro");

                step_B_KhalooChainJobsGlobalLogicIndex = 0;

                step_B_Ally_chainJobGroup_01_khalooStart.StartOutStepIfNotStarted();

                step_B_Objects_01_StartingStepBTrigger.StartOutStepIfNotStarted();

                mapLogic.HUD_ObjectivesPage_SetObjectiveDone(1);
                mapLogic.HUD_ShowCompleteMission(0);

                mapLogic.HUD_ShowGameSaved();

                PlayFirstMusic();

                SetLevelStep(2.1f);
            }
            #endregion

            #region 2.1 Wait For Starting StepB Trigger Start Fight Reg 01 Enemy
            if (levelStep == 2.1f)
            {
                if (step_B_SomeOneEntered_StartingStepBTrigger.IsEverActivated())
                {
                    step_B_Objects_01_StartingStepBTrigger.StartFinishing_OutStepIfNotFishining();

                    AllyDoCriticalShoot(false, khalooGunInfo);

                    Khaloo.GetComponent<CharacterInfo>().IsInvulnerable = false;

                    step_B_Enemy_FightInRegsGroup_01.StartOutStepIfNotStarted();

                    step_B_Objects_02_KhalooChangePos.StartOutStepIfNotStarted();

                    SetLevelStep(2.2f);
                }
            }
            #endregion

            #region 2.2 Wait For Player Enter To Change Khaloo Pos And Kill 'A' Soldiers
            if (levelStep == 2.2f)
            {
                if (step_B_PlayerEntered_KhalooChangePosTrigger.IsEverActivated())
                {
                    step_B_Objects_02_KhalooChangePos.StartFinishing_OutStepIfNotFishining();

                    step_B_KhalooChainJobsGlobalLogicIndex = 1;
                    step_B_Ally_chainJobGroup_01_khalooStart.Init_SetNewGlobalLogicIndex(step_B_KhalooChainJobsGlobalLogicIndex);

                    step_B_Objects_KillStepASoldiers.StartIt();

                    step_B_Objects_03_KhalooChangePos02.StartOutStepIfNotStarted();

                    SetLevelStep(2.3f);
                }
            }
            #endregion

            #region 2.3 Wait For Player Enter To Change Khaloo Again
            if (levelStep == 2.3f)
            {
                if (step_B_PlayerEntered_KhalooChangePos2Trigger.IsEverActivated())
                {
                    step_B_Objects_03_KhalooChangePos02.StartFinishing_OutStepIfNotFishining();

                    step_B_Enemy_FightInRegsGroup_01.StopCreatingMoreSoldiers();

                    step_B_KhalooChainJobsGlobalLogicIndex = 2;
                    step_B_Ally_chainJobGroup_01_khalooStart.Init_SetNewGlobalLogicIndex(step_B_KhalooChainJobsGlobalLogicIndex);

                    logicVoiceCollection_Khaloo.PlayName("B_02_BjonbNazarBian");

                    step_B_Objects_04_StartFightReg02Trigger.StartOutStepIfNotStarted();

                    SetLevelStep(2.4f);
                }
            }
            #endregion

            #region 2.4 Start Fight Reg02_03_Machine Guns
            if (levelStep == 2.4f)
            {
                if (step_B_PlayerEntered_StartFightReg2Trigger.IsEverActivated())
                {
                    step_B_Objects_04_StartFightReg02Trigger.StartFinishing_OutStepIfNotFishining();

                    step_B_Enemy_FightInRegsGroup_02.StartOutStepIfNotStarted();
                    step_B_Enemy_FightInRegsGroup_03.StartOutStepIfNotStarted();

                    step_B_Enemy_MachineGun_01.StartOutStepIfNotStarted();
                    step_B_Enemy_MachineGun_02.StartOutStepIfNotStarted();

                    step_B_KhalooChainJobsGlobalLogicIndex = 3;
                    step_B_Ally_chainJobGroup_01_khalooStart.Init_SetNewGlobalLogicIndex(step_B_KhalooChainJobsGlobalLogicIndex);

                    step_B_Objects_05_KhalooCommandTrigger.StartOutStepIfNotStarted();

                    step_B_Objects_04_Fight2Execution.StartIt();
                    step_B_Objects_04_Fight2Execution2.StartIt();

                    logicVoiceCollection_Khaloo.PlayName("B_03_MosalsalChi");

                    SetLevelStep(2.5f);
                }
            }
            #endregion

            #region 2.5 Khaloo Command Bayad Davum Biarim
            if (levelStep == 2.5f)
            {
                if (step_B_SomeOneEntered_KhalooCommandTrigger.IsEverActivated())
                {
                    logicVoiceCollection_Khaloo.PlayName("B_04_BezanNazarBian_Loop");

                    step_B_TimerForFightReg02_Reg03Creation = step_B_MaxTimForFightReg02_Reg03Creation;
                    step_B_TimerToGetNumOfCreatedSoldierInReg02 = step_B_MaxTimeToGetNumOfCreatedSoldierInReg02;

                    mapLogic.HUD_ObjectivesPage_SetActiveObjective(2);
                    mapLogic.HUD_ShowNewMission(1);

                    SetLevelStep(2.6f);
                }
            }
            #endregion

            #region 2.6 Wait Specific Time To Stop Creating Soldiers
            if (levelStep == 2.6f)
            {
                step_B_TimerToGetNumOfCreatedSoldierInReg02 = MathfPlus.DecByDeltatimeToZero(step_B_TimerToGetNumOfCreatedSoldierInReg02);
                if (step_B_TimerToGetNumOfCreatedSoldierInReg02 == 0)
                {
                    step_B_TimerToGetNumOfCreatedSoldierInReg02 = step_B_MaxTimeToGetNumOfCreatedSoldierInReg02;

                    step_B_NumbersOfCreatedSoldiersInFightReg02 = step_B_Enemy_FightInRegsGroup_02.GetNumOfCreatedSoldiers();
                }

                step_B_TimerForFightReg02_Reg03Creation = MathfPlus.DecByDeltatimeToZero(step_B_TimerForFightReg02_Reg03Creation);
                if (step_B_TimerForFightReg02_Reg03Creation == 0)
                {
                    if (step_B_NumbersOfCreatedSoldiersInFightReg02 > step_B_MinNumOfCreatedSoldiersInFightReg02)
                    {
                        logicVoiceCollection_Khaloo.StopCurVoiceAfterItsFinishing();

                        SetLevelStep(2.65f);
                    }
                }
            }
            #endregion

            #region 2.65 Stop All Fights And Allies Join
            if (levelStep == 2.65f)
            {
                step_B_Enemy_FightInRegsGroup_02.StopCreatingMoreSoldiers();
                step_B_Enemy_FightInRegsGroup_03.StopCreatingMoreSoldiers();

                step_B_Enemy_MachineGun_01.StopCreatingMoreSoldiers();
                step_B_Enemy_MachineGun_02.StopCreatingMoreSoldiers();

                step_B_Objects_06_LogicDieTriggerKhalooEnemy.StartIt();

                step_B_Objects_09_FailMissionTrigger.StartOutStepIfNotStarted();

                step_B_KhalooChainJobsGlobalLogicIndex = 4;
                step_B_Ally_chainJobGroup_01_khalooStart.Init_SetNewGlobalLogicIndex(step_B_KhalooChainJobsGlobalLogicIndex);

                logicVoiceCollection_Khaloo.PlayName("B_05_HaHa");

                step_B_AllyChainJobsGlobalLogicIndex = 0;
                step_B_Ally_chainJobGroup_02_Dustashun.StartOutStepIfNotStarted();

                step_B_Objects_04_Fight2Execution.EndIt();
                step_B_Objects_04_Fight2Execution2.EndIt();

                mapLogic.HUD_ObjectivesPage_SetObjectiveDone(2);
                mapLogic.HUD_ShowCompleteMission(1);

                SetLevelStep(2.66f);
            }
            #endregion

            #region 2.66 Make Khaloo And Allies Do Critical Shoots
            if (levelStep == 2.66f)
            {
                AllyDoCriticalShoot(true, ally01GunInfo);
                AllyDoCriticalShoot(true, ally02GunInfo);
                AllyDoCriticalShoot(true, khalooGunInfo);

                SetLevelStep(2.7f);
            }
            #endregion

            #region 2.7 Wait For End Of Fight Regs
            if (levelStep == 2.7f)
            {
                if (step_B_Fight02_Finished.IsEverActivated())
                {
                    AllyDoCriticalShoot(false, ally01GunInfo);
                    AllyDoCriticalShoot(false, ally02GunInfo);
                    AllyDoCriticalShoot(false, khalooGunInfo);

                    step_B_KhalooChainJobsGlobalLogicIndex = 5;
                    step_B_Ally_chainJobGroup_01_khalooStart.Init_SetNewGlobalLogicIndex(step_B_KhalooChainJobsGlobalLogicIndex);

                    step_B_AllyChainJobsGlobalLogicIndex = 1;
                    step_B_Ally_chainJobGroup_02_Dustashun.Init_SetNewGlobalLogicIndex(step_B_AllyChainJobsGlobalLogicIndex);

                    step_B_Objects_07_CutsceneBTrigger.StartOutStepIfNotStarted();

                    step_B_Objects_08_EndStepB.StartOutStepIfNotStarted();

                    step_B_Objects_09_LogicDieTriggerAll.StartIt();

                    SetLevelStep(2.8f);
                }

                if (step_B_PlayerEntered_FailMissionTrigger.IsEverActivated())
                {
                    SetLevelStep(step_MissionFail_YouLeftFightArea);
                    goto EndLevelSteps;
                }
            }
            #endregion

            #region 2.8 Wait For Player To Enter Finish StepB Trigger
            if (levelStep == 2.8f)
            {
                if (step_B_PlayerEntered_CutsceneBTrigger.IsEverActivated())
                {
                    SetLevelStep(2.85f);
                }
            }
            #endregion

            #region 2.85 Run Cutscene
            if (levelStep == 2.85f)
            {
                step_B_Ally_chainJobGroup_01_khalooStart.StartFinishing_OutStepIfNotFinishing();

                step_B_Ally_chainJobGroup_02_Dustashun.StartFinishing_OutStepIfNotFinishing();

                cutsceneFirst.StartIt();

                SetLevelStep(2.9f);
            }
            #endregion

            #region 2.9 Till Cutscene End
            if (levelStep == 2.9f)
            {
                if (cutsceneFirst.status == CutsceneStatus.Finished)
                {
                    SetLevelStep(2.91f);
                }
            }
            #endregion

            #region 2.91 Black Screen Fader
            if (levelStep == 2.91f)
            {
                mapLogic.blackScreenFader.StartFadingIn();

                SetLevelStep(2.95f);
            }
            #endregion

            #region 2.95 Change Allies Pos
            if (levelStep == 2.95f)
            {
                ChangeObjectTransform(Ally01, step_C_Objects_StartPoint_Ally01.transform);
                ChangeObjectTransform(Ally02, step_C_Objects_StartPoint_Ally02.transform);
                ChangeObjectTransform(Khaloo, step_C_Objects_StartPoint_Khaloo.transform);

                step_C_Objects_StartPoint_Player.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);

                SetLevelStep(3f);
            }
            #endregion

            #region 2.96 Wait To Finish Step B
            if (levelStep == 2.96f)
            {
                if (step_B_PlayerEntered_EndStepBTrigger.IsEverActivated())
                {
                    SetLevelStep(3f);
                }
            }
            #endregion

            #endregion

            //--------------------------------------------------------------------------------

            #region Step C

            #region 3 Start Step C
            if (levelStep == 3f)
            {
                SaveCheckPoint(3);

                step_C_KhalooChainJobsGlobalLogicIndex = 0;
                step_C_Ally_chainJobGroup_01_Khaloo.StartOutStepIfNotStarted();

                step_C_Ally01ChainJobsGlobalLogicIndex = 0;
                step_C_Ally_chainJobGroup_02_Ally01.StartOutStepIfNotStarted();

                step_C_Ally02ChainJobsGlobalLogicIndex = 0;
                step_C_Ally_chainJobGroup_03_Ally02.StartOutStepIfNotStarted();

                step_C_Objects_01_StartFight01Trigger.StartOutStepIfNotStarted();

                logicVoiceCollection_Khaloo.PlayName("C_01_YalaRahBioft");

                mapLogic.HUD_ObjectivesPage_SetActiveObjective(3);
                mapLogic.HUD_ShowNewMission(2);

                mapLogic.HUD_ShowGameSaved();

                PlayFirstMusic();

                SetLevelStep(3.1f);
            }
            #endregion

            #region 3.1 Wait For Start Fight01
            if (levelStep == 3.1f)
            {
                if (step_C_PlayerEntered_StartFight01Trigger.IsEverActivated())
                {
                    step_C_Objects_01_StartFight01Trigger.StartFinishing_OutStepIfNotFishining();

                    step_C_Enemy_FightInRegsGroup_01.StartOutStepIfNotStarted();

                    step_C_Objects_02_StartFight02Trigger.StartOutStepIfNotStarted();

                    step_C_Objects_05_ExecutionAreaExitFightReg02.StartIt();

                    logicVoiceCollection_Khaloo.PlayName("C_02_Bokoshineshun");

                    mapLogic.HUD_ObjectivesPage_SetObjectiveDone(3);
                    mapLogic.HUD_ShowCompleteMission(2);

                    SetLevelStep(3.15f);
                }
            }
            #endregion

            #region 3.15 Wait For Fight01 Finished Or Trigger
            if (levelStep == 3.15f)
            {
                if (step_C_PlayerEntered_StartFight02Trigger.IsEverActivated())
                {
                    step_C_Objects_03_StartFight03Trigger.StartOutStepIfNotStarted();

                    step_C_Enemy_FightInRegsGroup_02.StartOutStepIfNotStarted();
                    step_C_Enemy_Fight02_Timer = step_C_Enemy_Fight02_MaxTime;

                    step_C_Timer = step_C_MaxTimeToKillFight1;

                    SetLevelStep(3.16f);
                }

                if (step_C_Fight01_Finished.IsEverActivated())
                {
                    step_C_KhalooChainJobsGlobalLogicIndex = 1;
                    step_C_Ally_chainJobGroup_01_Khaloo.Init_SetNewGlobalLogicIndex(step_C_KhalooChainJobsGlobalLogicIndex);

                    step_C_Ally01ChainJobsGlobalLogicIndex = 1;
                    step_C_Ally_chainJobGroup_02_Ally01.Init_SetNewGlobalLogicIndex(step_C_Ally01ChainJobsGlobalLogicIndex);

                    step_C_Ally02ChainJobsGlobalLogicIndex = 1;
                    step_C_Ally_chainJobGroup_03_Ally02.Init_SetNewGlobalLogicIndex(step_C_Ally02ChainJobsGlobalLogicIndex);

                    step_C_Enemy_FightInRegsGroup_02.StartOutStepIfNotStarted();
                    step_C_Enemy_Fight02_Timer = step_C_Enemy_Fight02_MaxTime;

                    step_C_Objects_03_StartFight03Trigger.StartOutStepIfNotStarted();

                    SetLevelStep(3.2f);
                }
            }
            #endregion

            #region 3.16 Wait For Fight01 Finished
            if (levelStep == 3.16f)
            {
                if (step_C_Fight01_Finished.IsEverActivated())
                {
                    step_C_KhalooChainJobsGlobalLogicIndex = 1;
                    step_C_Ally_chainJobGroup_01_Khaloo.Init_SetNewGlobalLogicIndex(step_C_KhalooChainJobsGlobalLogicIndex);

                    step_C_Ally01ChainJobsGlobalLogicIndex = 1;
                    step_C_Ally_chainJobGroup_02_Ally01.Init_SetNewGlobalLogicIndex(step_C_Ally01ChainJobsGlobalLogicIndex);

                    step_C_Ally02ChainJobsGlobalLogicIndex = 1;
                    step_C_Ally_chainJobGroup_03_Ally02.Init_SetNewGlobalLogicIndex(step_C_Ally02ChainJobsGlobalLogicIndex);

                    SetLevelStep(3.2f);
                }

                step_C_Timer = MathfPlus.DecByDeltatimeToZero(step_C_Timer);
                if (step_C_Timer == 0 && !step_C_Fight01Died)
                {
                    step_C_Objects_02_DieTrigger.StartIt();

                    step_C_Fight01Died = true;
                }

                print(step_C_Fight01_Finished);
            }
            #endregion

            #region 3.2 Show HUD And Khaloo Dialog
            if (levelStep == 3.2f)
            {
                mapLogic.HUD_ObjectivesPage_SetActiveObjective(4);
                mapLogic.HUD_ShowNewMission(3);

                logicVoiceCollection_Khaloo.PlayName("C_03_YalaDgBezanineshun");

                SetLevelStep(3.21f);
            }
            #endregion

            #region 3.21 Wait For Fight Reg02 Time Or Next Trigger
            if (levelStep == 3.21f)
            {
                step_C_Enemy_Fight02_Timer = MathfPlus.DecByDeltatimeToZero(step_C_Enemy_Fight02_Timer);

                if (step_C_PlayerEntered_StartFight03Trigger.IsEverActivated() || step_C_Enemy_Fight02_Timer == 0)
                {
                    step_C_Objects_05_ExecutionAreaExitFightReg03.StartIt();

                    step_C_Objects_06_ExitStepTrigger.StartOutStepIfNotStarted();

                    step_C_Objects_04_StopFight03Trigger.StartOutStepIfNotStarted();

                    step_C_Enemy_FightInRegsGroup_03.StartOutStepIfNotStarted();

                    step_C_Ally_Fight03BeforeProgressTimer = step_C_Ally_Fight03MaxTimeBeforeProgress;

                    step_C_Enemy_FightInRegsGroup_02.StopCreatingMoreSoldiers();

                    SetLevelStep(3.25f);
                }
            }
            #endregion

            #region 3.25 Wait For Fight Reg02 Finish
            if (levelStep == 3.25f)
            {
                if (step_C_PlayerEntered_StopFight03Trigger.IsEverActivated())
                {
                    step_C_Enemy_FightInRegsGroup_03.StopCreatingMoreSoldiers();

                    SetLevelStep(3.35f);
                }

                if (step_C_Fight02_Finished.IsEverActivated())
                {
                    step_C_KhalooChainJobsGlobalLogicIndex = 2;
                    step_C_Ally_chainJobGroup_01_Khaloo.Init_SetNewGlobalLogicIndex(step_C_KhalooChainJobsGlobalLogicIndex);

                    step_C_Ally01ChainJobsGlobalLogicIndex = 2;
                    step_C_Ally_chainJobGroup_02_Ally01.Init_SetNewGlobalLogicIndex(step_C_Ally01ChainJobsGlobalLogicIndex);

                    step_C_Ally02ChainJobsGlobalLogicIndex = 2;
                    step_C_Ally_chainJobGroup_03_Ally02.Init_SetNewGlobalLogicIndex(step_C_Ally02ChainJobsGlobalLogicIndex);

                    SetLevelStep(3.3f);
                }
            }
            #endregion

            #region 3.3 Wait For Player Enter Trigger For FightReg03 Finished
            if (levelStep == 3.3f)
            {
                if (step_C_PlayerEntered_StopFight03Trigger.IsEverActivated())
                {
                    step_C_Enemy_FightInRegsGroup_03.StopCreatingMoreSoldiers();

                    SetLevelStep(3.35f);
                }

                step_C_Ally_Fight03BeforeProgressTimer = MathfPlus.DecByDeltatimeToZero(step_C_Ally_Fight03BeforeProgressTimer);

                if (step_C_Ally_Fight03BeforeProgressTimer == 0f)
                {
                    step_C_KhalooChainJobsGlobalLogicIndex = 3;
                    step_C_Ally_chainJobGroup_01_Khaloo.Init_SetNewGlobalLogicIndex(step_C_KhalooChainJobsGlobalLogicIndex);

                    step_C_Ally01ChainJobsGlobalLogicIndex = 3;
                    step_C_Ally_chainJobGroup_02_Ally01.Init_SetNewGlobalLogicIndex(step_C_Ally01ChainJobsGlobalLogicIndex);

                    step_C_Ally02ChainJobsGlobalLogicIndex = 3;
                    step_C_Ally_chainJobGroup_03_Ally02.Init_SetNewGlobalLogicIndex(step_C_Ally02ChainJobsGlobalLogicIndex);

                    SetLevelStep(3.4f);
                }
            }
            #endregion

            #region 3.35 Wait For Player Enter Trigger For FightReg03 Finished
            if (levelStep == 3.35f)
            {
                if (step_C_Fight02_Finished.IsEverActivated() || step_C_PlayerEntered_ExitStepTrigger.IsEverActivated())
                {
                    step_C_KhalooChainJobsGlobalLogicIndex = 3;
                    step_C_Ally_chainJobGroup_01_Khaloo.Init_SetNewGlobalLogicIndex(step_C_KhalooChainJobsGlobalLogicIndex);

                    step_C_Ally01ChainJobsGlobalLogicIndex = 3;
                    step_C_Ally_chainJobGroup_02_Ally01.Init_SetNewGlobalLogicIndex(step_C_Ally01ChainJobsGlobalLogicIndex);

                    step_C_Ally02ChainJobsGlobalLogicIndex = 3;
                    step_C_Ally_chainJobGroup_03_Ally02.Init_SetNewGlobalLogicIndex(step_C_Ally02ChainJobsGlobalLogicIndex);

                    SetLevelStep(3.5f);
                }
            }
            #endregion

            #region 3.4 Wait For Player Enter Stop Fight03 Trigger
            if (levelStep == 3.4f)
            {
                if (step_C_PlayerEntered_StopFight03Trigger.IsEverActivated())
                {
                    step_C_Enemy_FightInRegsGroup_03.StopCreatingMoreSoldiers();

                    SetLevelStep(3.5f);
                }
            }
            #endregion

            #region 3.5 Exit Step
            if (levelStep == 3.5f)
            {
                if (step_C_PlayerEntered_ExitStepTrigger.IsEverActivated() || step_C_Fight03_Finished.IsEverActivated())
                {
                    step_C_Objects_07_DieTrigger.StartIt();

                    step_C_Ally_chainJobGroup_01_Khaloo.StartFinishing_OutStepIfNotFinishing();
                    step_C_Ally_chainJobGroup_02_Ally01.StartFinishing_OutStepIfNotFinishing();
                    step_C_Ally_chainJobGroup_03_Ally02.StartFinishing_OutStepIfNotFinishing();

                    step_C_Objects_05_ExecutionAreaExitFightReg02.EndIt();
                    step_C_Objects_05_ExecutionAreaExitFightReg03.EndIt();

                    mapLogic.HUD_ObjectivesPage_SetObjectiveDone(4);
                    mapLogic.HUD_ShowCompleteMission(3);

                    SetLevelStep(3.6f);
                }
            }
            #endregion

            #region 3.6 Exit Step
            if (levelStep == 3.6f)
            {
                if (step_C_ChainJobAlly01_Finished.IsEverActivated() && step_C_ChainJobAlly02_Finished.IsEverActivated() && step_C_ChainJobKhaloo_Finished.IsEverActivated())
                    SetLevelStep(4f);
            }
            #endregion

            #endregion

            //--------------------------------------------------------------------------------

            #region Step D

            #region 4 Start Step D
            if (levelStep == 4f)
            {
                SaveCheckPoint(4f);

                SetBoshkesReadyToExplode(step_D_Objects_03_SignallingBoshkes, step_D_FlagForCheckBoshkesExplosion);

                mapLogic.HUD_ShowGameSaved();

                mapLogic.HUD_ObjectivesPage_SetActiveObjective(5);
                mapLogic.HUD_ShowNewMission(4);

                step_D_KhalooChainJobsGlobalLogicIndex = 0;
                step_D_Ally_chainJobGroup_01_Khaloo.StartOutStepIfNotStarted();

                step_D_Ally01ChainJobsGlobalLogicIndex = 0;
                step_D_Ally_chainJobGroup_02_Ally01.StartOutStepIfNotStarted();

                step_D_Ally02ChainJobsGlobalLogicIndex = 0;
                step_D_Ally_chainJobGroup_03_Ally02.StartOutStepIfNotStarted();

                step_D_Enemy_FightInRegsGroup_01.StartOutStepIfNotStarted();
                step_D_Enemy_FightInRegsGroup_02.StartOutStepIfNotStarted();
                step_D_Enemy_FightInRegsGroup_03.StartOutStepIfNotStarted();
                step_D_Enemy_FightInRegsGroup_04.StartOutStepIfNotStarted();
                step_D_Enemy_MachineGun_01.StartOutStepIfNotStarted();
                step_D_Enemy_MachineGun_02.StartOutStepIfNotStarted();

                step_D_Objects_01_ExecutionArea01.StartIt();

                step_D_Objects_02_StopFight01.StartOutStepIfNotStarted();

                step_D_TimerForKhalooStartAction = step_D_MaxTimeForKhalooStartAction;

                logicVoiceCollection_Ally01.PlayName("D_01_MosalsalChiBezanesh");

                PlayFirstMusic();

                SetLevelStep(4.1f);
            }
            #endregion

            #region 4.1 Wait For StopFightTrigger And Time
            if (levelStep == 4.1f)
            {
                if (step_D_PlayerEntered_StopFight01Trigger.IsEverActivated())
                {
                    step_D_Enemy_FightInRegsGroup_01.StopCreatingMoreSoldiers();
                }

                step_D_TimerForKhalooStartAction = MathfPlus.DecByDeltatimeToZero(step_D_TimerForKhalooStartAction);
                if (step_D_TimerForKhalooStartAction == 0f)
                {
                    step_D_Enemy_FightInRegsGroup_01.StopCreatingMoreSoldiers();

                    mapLogic.HUD_ObjectivesPage_SetObjectiveDone(5);

                    mapLogic.HUD_ShowCompleteMission(4);

                    SetLevelStep(4.2f);
                }

                if (step_D_FlagForCheckBoshkesExplosion.IsEverActivated() && !step_D_ExplosionChecked)
                {
                    ExplodeTheWall();
                }

                if (step_D_PlayerEntered_CutSceneTrigger.IsEverActivated())
                {
                    SetLevelStep(4.65f);
                }
            }
            #endregion

            #region 4.2 Dialog Khaloo Injuri Nemishe
            if (levelStep == 4.2f)
            {
                logicVoiceCollection_Khaloo.PlayName("D_01_injuriNemishe");

                SetLevelStep(4.25f);
            }
            #endregion

            #region 4.25 Wait To Finish Dialog
            if (levelStep == 4.25f)
            {
                if (logicVoiceCollection_Khaloo.IsCurVoiceFinished())
                {
                    if (!step_D_FlagForCheckBoshkesExplosion.IsEverActivated())
                    {
                        foreach (GameObject gObj in step_D_Objects_03_SignallingBoshkes)
                        {
                            gObj.GetComponent<BlinkingMaterial>().StartBlinking();
                        }

                        mapLogic.HUD_ShowNewMission(5);
                    }

                    step_D_TimerBetweenDialogs = step_D_MaxTimeForDialogBayadBeriUnvar;

                    SetLevelStep(4.3f);
                }

                if (step_D_FlagForCheckBoshkesExplosion.IsEverActivated() && !step_D_ExplosionChecked)
                {
                    ExplodeTheWall();
                }

                if (step_D_PlayerEntered_CutSceneTrigger.IsEverActivated())
                {
                    SetLevelStep(4.65f);
                }
            }
            #endregion

            #region 4.3 Dialog Khaloo Bayad Beri Unvar
            if (levelStep == 4.3f)
            {
                step_D_TimerBetweenDialogs = MathfPlus.DecByDeltatimeToZero(step_D_TimerBetweenDialogs);

                if (step_D_TimerBetweenDialogs == 0)
                {
                    logicVoiceCollection_Khaloo.PlayName("D_02_BayadBeriUnvar");

                    SetLevelStep(4.35f);
                }

                if (step_D_FlagForCheckBoshkesExplosion.IsEverActivated() && !step_D_ExplosionChecked)
                {
                    ExplodeTheWall();
                }

                if (step_D_PlayerEntered_CutSceneTrigger.IsEverActivated())
                {
                    SetLevelStep(4.65f);
                }
            }
            #endregion

            #region 4.35 Wait To Finish Dialog
            if (levelStep == 4.35f)
            {
                if (logicVoiceCollection_Khaloo.IsCurVoiceFinished())
                {
                    step_D_TimerBetweenDialogs = step_D_MaxTimeForDialogBayadBeriUnvarLoop;

                    mapLogic.HUD_Add3DObjective(step_D_3DObj_Wall, The3DObjIconType.FeleshRooBePayin, "TheWall", The3DObjViewRange.Medium);

                    SetLevelStep(4.4f);
                }

                if (step_D_FlagForCheckBoshkesExplosion.IsEverActivated() && !step_D_ExplosionChecked)
                {
                    ExplodeTheWall();
                }

                if (step_D_PlayerEntered_CutSceneTrigger.IsEverActivated())
                {
                    SetLevelStep(4.65f);
                }
            }
            #endregion

            #region 4.4 Dialog Khaloo Bayad Beri Unvar Loop
            if (levelStep == 4.4f)
            {
                if (!step_D_FlagForCheckBoshkesExplosion.IsEverActivated())
                {
                    step_D_TimerBetweenDialogs = MathfPlus.DecByDeltatimeToZero(step_D_TimerBetweenDialogs);

                    if (step_D_TimerBetweenDialogs == 0)
                    {
                        logicVoiceCollection_Khaloo.PlayName("D_03_BayadBeriUnvarLoop");

                        step_D_Timer = step_D_MaxTimeToShowHUD;

                        mapLogic.HUD_3DObjBlinkInMinimap("TheWall");

                        SetLevelStep(4.45f);
                    }
                }
                else
                {
                    step_D_Timer = step_D_MaxTimeToShowHUD;

                    SetLevelStep(4.45f);
                }
            }
            #endregion

            #region 4.45 Wait To Explode Boshkes
            if (levelStep == 4.45f)
            {
                if (step_D_FlagForCheckBoshkesExplosion.IsEverActivated() && !step_D_ExplosionChecked)
                {
                    ExplodeTheWall();

                    logicVoiceCollection_Khaloo.StopCurVoiceAfterItsFinishing();

                    step_D_TimerBetweenDialogs = step_D_MaxTimeForDialogBoroChapLoop;

                    SetLevelStep(4.5f);
                }

                if (step_D_FlagForCheckBoshkesExplosion.IsEverActivated() && step_D_ExplosionChecked)
                {
                    SetLevelStep(4.5f);
                }

                step_D_Timer = MathfPlus.DecByDeltatimeToZero(step_D_Timer);

                if (step_D_Timer == 0)
                {
                    mapLogic.HUD_ShowNewMission(5);

                    step_D_Timer = step_D_MaxTimeToShowHUD;
                }
            }
            #endregion

            #region 4.5 Dialog Boro Chap Loop
            if (levelStep == 4.5f)
            {
                step_D_TimerBetweenDialogs = MathfPlus.DecByDeltatimeToZero(step_D_TimerBetweenDialogs);

                if (step_D_TimerBetweenDialogs == 0)
                {
                    logicVoiceCollection_Khaloo.PlayName("D_04_BoroChapLoop");

                    mapLogic.HUD_ObjectivesPage_SetActiveObjective(6);
                    mapLogic.HUD_ShowNewMission(6);

                    step_D_Timer = step_D_MaxTimeToShowHUD;

                    SetLevelStep(4.55f);
                }

                if (step_D_PlayerEntered_CutSceneTrigger.IsEverActivated())
                {
                    SetLevelStep(4.65f);
                }
            }
            #endregion

            #region 4.55 Wait For Player Enter Cutscene Trigger
            if (levelStep == 4.55f)
            {
                if (step_D_PlayerEntered_CutSceneTrigger.IsEverActivated())
                {
                    SetLevelStep(4.65f);
                }

                step_D_Timer = MathfPlus.DecByDeltatimeToZero(step_D_Timer);

                if (step_D_Timer == 0)
                {
                    mapLogic.HUD_ShowNewMission(6);

                    step_D_Timer = step_D_MaxTimeToShowHUD;
                }
            }
            #endregion

            #region 4.65 Finish Voices
            if (levelStep == 4.65f)
            {
                mapLogic.HUD_Remove3DObjective("TheWall");

                logicVoiceCollection_Khaloo.StopCurVoiceAfterItsFinishing();

                SetLevelStep(4.7f);
            }
            #endregion

            #region 4.7 Run Cutscene
            if (levelStep == 4.7f)
            {
                cutsceneSorkhordan.StartIt();

                SetCharactersInTempList(mapLogic.mapEnemyChars);

                SetLevelStep(4.71f);
            }
            #endregion

            #region 4.71 Till Cutscene End
            if (levelStep == 4.71f)
            {
                if (cutsceneSorkhordan.status == CutsceneStatus.Finished)
                {
                    SetLevelStep(4.72f);
                }
            }
            #endregion

            #region 4.72 Black Screen Fader
            if (levelStep == 4.72f)
            {
                mapLogic.blackScreenFader.StartFadingIn();

                SetLevelStep(9f);
            }
            #endregion

            #region 9 After sor cutscene
            if (levelStep == 9f)
            {
                SaveCheckPoint(9f);

                mapLogic.HUD_ShowGameSaved();

                mapLogic.HUD_ObjectivesPage_SetActiveObjective(6);
                mapLogic.HUD_ShowNewMission(6);

                PlayFirstMusic();

                if (GameController.lvlNakhl1_AfterSorCutscene_PlacePlayerInRiver)
                {
                    step_D_Objects_StartPoint_PlayerAfterSor.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);
                }
                else
                {
                    step_D_Objects_StartPoint_PlayerBeforeHouseBack.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);
                }

                step_D_Objects_08_AzPoshtTrigger.StartOutStepIfNotStarted();


                if (!step_D_IsLoadedToStepD2)
                    KillAllCharactersInTempList();

                step_D_Objects_StartPoint_Khaloo_C.PlaceCharacterOnIt(Khaloo);
                step_D_Objects_StartPoint_Ally01_C.PlaceCharacterOnIt(Ally01);
                step_D_Objects_StartPoint_Ally02_C.PlaceCharacterOnIt(Ally02);

                step_D_KhalooChainJobsGlobalLogicIndex = 0;
                step_D_Ally_chainJobGroup_01_Khaloo_C01.StartOutStepIfNotStarted();

                step_D_Ally01ChainJobsGlobalLogicIndex = 0;
                step_D_Ally_chainJobGroup_02_Ally01_C01.StartOutStepIfNotStarted();

                step_D_Ally02ChainJobsGlobalLogicIndex = 0;
                step_D_Ally_chainJobGroup_03_Ally02_C01.StartOutStepIfNotStarted();

                step_D_Enemy_FightInRegsGroup_C02.StartOutStepIfNotStarted();
                step_D_Enemy_FightInRegsGroup_C03.StartOutStepIfNotStarted();

                step_D_Enemy_MachineGun_C01.StartOutStepIfNotStarted();
                step_D_Enemy_MachineGun_C01.GetComponent<ChangeObjectVisibility>().SetVisibility(true);

                step_D_Enemy_MachineGun_C02.StartOutStepIfNotStarted();
                step_D_Enemy_MachineGun_C02.GetComponent<ChangeObjectVisibility>().SetVisibility(true);

                khalooGunInfo.ApplyExternalLogicDamageCoef(true, 0f);
                ally01GunInfo.ApplyExternalLogicDamageCoef(true, 0f);
                ally02GunInfo.ApplyExternalLogicDamageCoef(true, 0f);

                SetLevelStep(9.1f);
            }
            #endregion

            #region 9.1 PlayerEntered Az Posht Trigger
            if (levelStep == 9.1f)
            {
                if (step_D_PlayerEntered_AzPoshtTrigger.IsEverActivated())
                {
                    GameController.lvlNakhl1_AfterSorCutscene_PlacePlayerInRiver = false;

                    step_D_Objects_08_FailTrigger.StartOutStepIfNotStarted();

                    step_D_Objects_09_AllyDoDmgTrigger.StartOutStepIfNotStarted();

                    step_D_Enemy_FightInRegsGroup_C02.StopCreatingMoreSoldiers();
                    step_D_Enemy_FightInRegsGroup_C03.StopCreatingMoreSoldiers();

                    step_D_Enemy_MachineGun_C01.StopCreatingMoreSoldiers();
                    step_D_Enemy_MachineGun_C02.StopCreatingMoreSoldiers();

                    step_D_Objects_01_ExecutionArea01.EndIt();

                    SetBoshkesReadyToExplode(step_D_Objects_08_BoshkesDakheleKhune, step_D_FlagForCheckBoshkesKhuneExplosion);

                    mapLogic.HUD_ShowNewMission(7);

                    mapLogic.HUD_Add3DObjective(step_D_3DObj_BoshkesMachineGuns, The3DObjIconType.FeleshRooBePayin, "BoshkesMachineGuns", The3DObjViewRange.Far);

                    step_D_Timer = step_D_MaxTimeToShowHUD;

                    SetLevelStep(9.2f);
                }
            }
            #endregion

            #region 9.2 Wait For Boshkes Khune Explode
            if (levelStep == 9.2f)
            {
                if (IsMachineGunsSoldiersDead() && !step_D_DamageOccuredInBoshkesKhune)
                {
                    step_D_Objects_08_BoshkesDakheleKhune[0].GetComponent<DamageInfluence>().DamageOccur();

                    step_D_DamageOccuredInBoshkesKhune = true;
                }

                if (step_D_FlagForCheckBoshkesKhuneExplosion.IsEverActivated())
                {
                    SetLevelStep(9.3f);
                    goto StartLevelSteps;
                }

                step_D_Timer = MathfPlus.DecByDeltatimeToZero(step_D_Timer);

                if (step_D_Timer == 0)
                {
                    mapLogic.HUD_ShowNewMission(7);

                    step_D_Timer = step_D_MaxTimeToShowHUD;
                }

                if (step_D_PlayerEntered_FailTrigger.IsEverActivated())
                {
                    step_D_Enemy_FightInRegsGroup_Fail.StartOutStepIfNotStarted();

                    step_D_Timer = step_D_MaxTimeToFail;

                    SetLevelStep(9.25f);
                }
            }
            #endregion

            #region 9.25 Wait To Fail
            if (levelStep == 9.25f)
            {
                step_D_Timer = MathfPlus.DecByDeltatimeToZero(step_D_Timer);

                if (step_D_Timer == 0)
                {
                    SetLevelStep(step_MissionFail_YouAreDetectedByEnemies);
                    goto EndLevelSteps;
                }
            }
            #endregion

            #region 9.3
            if (levelStep == 9.3f)
            {
                foreach (GameObject gobj in step_D_Objects_08_FiresOfMachineGuns)
                {
                    gobj.SetActiveRecursively(true);
                }

                mapLogic.HUD_Remove3DObjective("BoshkesMachineGuns");

                mapLogic.HUD_ObjectivesPage_SetObjectiveDone(6);
                mapLogic.HUD_ShowCompleteMission(7);

                step_D_Enemy_FightInRegsGroup_05.StopCreatingMoreSoldiersAndMakeAliveSoldiersSoWeak();
                step_D_Enemy_FightInRegsGroup_C02.StopCreatingMoreSoldiersAndMakeAliveSoldiersSoWeak();
                step_D_Enemy_FightInRegsGroup_C03.StopCreatingMoreSoldiersAndMakeAliveSoldiersSoWeak();

                khalooGunInfo.ApplyExternalLogicDamageCoef(false, 1f);
                ally01GunInfo.ApplyExternalLogicDamageCoef(false, 1f);
                ally02GunInfo.ApplyExternalLogicDamageCoef(false, 1f);

                SetLevelStep(9.4f);
            }
            #endregion

            #region 9.4 PlayerEntered Ally DoDmg Trigger
            if (levelStep == 9.4f)
            {
                if (step_D_PlayerEntered_AllyDoDmgTrigger.IsEverActivated())
                {
                    //khalooGunInfo.ApplyExternalLogicDamageCoef(false, 1f);
                    //ally01GunInfo.ApplyExternalLogicDamageCoef(false, 1f);
                    //ally02GunInfo.ApplyExternalLogicDamageCoef(false, 1f);

                    //step_D_Objects_10_ExecutionArea02.StartIt();

                    //step_D_Enemy_FightInRegsGroup_05.StopCreatingMoreSoldiers();

                    //step_D_Objects_11_EndOfStepDTrigger.StartOutStepIfNotStarted();

                    SetLevelStep(9.5f);
                }
            }
            #endregion

            #region 9.5 Start screen fading
            if (levelStep == 9.5f)
            {
                mapLogic.blackScreenFader.StartFadingOut();
                SetLevelStep(9.55f);
            }
            #endregion

            #region 9.55 fading screen
            if (levelStep == 9.55f)
            {
                if (mapLogic.blackScreenFader.isFadingFinished)
                {
                    SetLevelStep(9.6f);
                }
            }
            #endregion

            #region 9.6 Run Cutscene Last
            if (levelStep == 9.6f)
            {
                cutsceneLast.StartIt();

                SetLevelStep(9.7f);
            }
            #endregion

            #region 9.7f Till Cutscene End
            if (levelStep == 9.7f)
            {
                if (cutsceneLast.status == CutsceneStatus.Finished)
                {
                    SetLevelStep(9.8f);
                }
            }
            #endregion

            #region 9.8 Black Screen Fader
            if (levelStep == 9.8f)
            {
                mapLogic.blackScreenFader.StartFadingIn();

                SetLevelStep(9.9f);
            }
            #endregion

            #region 9.9f End
            if (levelStep == 9.9f)
            {
                SetLevelStep(7.6f);
            }
            #endregion

            #endregion

            //End Of Nakhlestan Part 1-1 -----------------------------------------------------

            #region Step E

            #region 5 Start Step E
            if (levelStep == 5f)
            {
                SaveCheckPoint(5f);

                logicVoiceCollection_Ally01.PlayName("E_01_BiaJolo");

                step_E_Ally01ChainJobsGlobalLogicIndex = 0;
                step_E_Ally_chainJobGroup_01_Ally01.StartOutStepIfNotStarted();

                step_E_Objects_01_StartStepTrigger.StartOutStepIfNotStarted();

                PlaySecondMusic();

                //mapLogic.HUD_ShowGameSaved();

                SetLevelStep(5.1f);
            }
            #endregion

            #region 5.1 Wait For Player Enter Start Trigger
            if (levelStep == 5.1f)
            {
                if (step_E_PlayerEntered_StartStepTrigger.IsEverActivated())
                {
                    mapLogic.HUD_ObjectivesPage_SetActiveObjective(1);
                    mapLogic.HUD_ShowNewMission(0);

                    mapLogic.HUD_AddOnlyMinimap3DObj(miniMapObjectTransform_01_TaheKuche, "TaheKuche");

                    logicVoiceCollection_Ally01.PlayName("E_02_BezanNazarBianJolo");

                    step_E_Ally01ChainJobsGlobalLogicIndex = 1;
                    step_E_Ally_chainJobGroup_01_Ally01.Init_SetNewGlobalLogicIndex(step_E_Ally01ChainJobsGlobalLogicIndex);

                    step_E_Enemy_FightInRegsGroup_01.StartOutStepIfNotStarted();

                    step_E_Objects_02_StartFight02Trigger.StartOutStepIfNotStarted();

                    SetLevelStep(5.15f);
                }
            }
            #endregion

            #region 5.15 Wait For Player Enter For Fight02
            if (levelStep == 5.15f)
            {
                if (step_E_PlayerEntered_StartFight02Trigger.IsEverActivated())
                {
                    mapLogic.HUD_RemoveMinimap3DObj("TaheKuche");
                    mapLogic.HUD_AddOnlyMinimap3DObj(miniMapObjectTransform_02_EndStepE, "EndStepE");

                    step_E_Enemy_FightInRegsGroup_02.StartOutStepIfNotStarted();
                    step_E_Enemy_FightInRegsGroup_03.StartOutStepIfNotStarted();

                    step_E_TimerForFightRegs = step_E_MaxTimeForFightReg03;

                    step_E_TimerForFight01 = step_E_MaxTimeForKillingFight01;

                    step_E_Objects_03_ExecutionArea01.StartIt();

                    step_E_Objects_04_AllyChangePosTrigger.StartOutStepIfNotStarted();

                    step_E_Objects_06_StartFight04Trigger.StartOutStepIfNotStarted();

                    step_E_Enemy_FightInRegsGroup_01.StopCreatingMoreSoldiers();

                    SetLevelStep(5.2f);
                }
            }
            #endregion

            #region 5.2 Ally Position
            if (levelStep == 5.2f)
            {
                step_E_TimerForFight01 = MathfPlus.DecByDeltatimeToZero(step_E_TimerForFight01);
                if (step_E_TimerForFight01 == 0)
                {
                    step_E_Objects_05_DieTriggerForFight01.StartItIfItsNotStartedBefore();
                }

                if (step_E_Fight01_Finished.IsEverActivated())
                {
                    step_E_Ally01ChainJobsGlobalLogicIndex = 2;
                    step_E_Ally_chainJobGroup_01_Ally01.Init_SetNewGlobalLogicIndex(step_E_Ally01ChainJobsGlobalLogicIndex);

                    SetLevelStep(5.23f);
                }

                if (step_E_PlayerEntered_AllyChangePosTrigger.IsEverActivated())
                {
                    step_E_Objects_05_DieTriggerForFight01.StartIt();

                    SetLevelStep(5.25f);
                }
            }
            #endregion

            #region 5.23 Counter For Ally Change Pos
            if (levelStep == 5.23f)
            {
                if (!MapLogic.Instance.isPlayerHidden)
                {
                    step_E_TimerForFightRegs = MathfPlus.DecByDeltatimeToZero(step_E_TimerForFightRegs);
                }

                if (step_E_TimerForFightRegs < (step_E_MaxTimeForFightReg03 / 3))
                {
                    SetLevelStep(5.25f);
                }

                if (step_E_PlayerEntered_AllyChangePosTrigger.IsEverActivated() || step_E_Fight02_Finished.IsEverActivated())
                {
                    SetLevelStep(5.25f);
                }
            }
            #endregion

            #region 5.25 Ally Change Pos
            if (levelStep == 5.25f)
            {
                logicVoiceCollection_Ally01.PlayName("E_03_UntarafSangar");

                step_E_Ally01ChainJobsGlobalLogicIndex = 3;
                step_E_Ally_chainJobGroup_01_Ally01.Init_SetNewGlobalLogicIndex(step_E_Ally01ChainJobsGlobalLogicIndex);

                SetLevelStep(5.3f);
            }
            #endregion

            #region 5.3 Counter For Fight 03
            if (levelStep == 5.3f)
            {
                if (!MapLogic.Instance.isPlayerHidden)
                {
                    step_E_TimerForFightRegs = MathfPlus.DecByDeltatimeToZero(step_E_TimerForFightRegs);
                }

                if (step_E_TimerForFightRegs == 0)
                {
                    step_E_Enemy_FightInRegsGroup_02.StopCreatingMoreSoldiers();
                    step_E_Enemy_FightInRegsGroup_03.StopCreatingMoreSoldiers();

                    SetLevelStep(5.35f);
                }
            }
            #endregion

            #region 5.35 Wait For Fight02 Finished
            if (levelStep == 5.35f)
            {
                if (step_E_Fight02_Finished.IsEverActivated())
                {
                    logicVoiceCollection_Ally01.PlayName("E_04_BezanNazarBianJolo");

                    step_E_Ally01ChainJobsGlobalLogicIndex = 4;
                    step_E_Ally_chainJobGroup_01_Ally01.Init_SetNewGlobalLogicIndex(step_E_Ally01ChainJobsGlobalLogicIndex);

                    SetLevelStep(5.38f);
                }

                if (step_E_PlayerEntered_StartFight04Trigger.IsEverActivated())
                {
                    SetLevelStep(5.39f);
                }
            }
            #endregion

            #region 5.38 Wait For Player Enter Fight04 Trigger
            if (levelStep == 5.38f)
            {
                if (step_E_PlayerEntered_StartFight04Trigger.IsEverActivated())
                {
                    SetLevelStep(5.39f);
                }
            }
            #endregion

            #region 5.39 Start Fight04
            if (levelStep == 5.39f)
            {
                step_E_Enemy_FightInRegsGroup_04.StartOutStepIfNotStarted();

                step_E_Objects_07_StartFight05Trigger.StartOutStepIfNotStarted();

                SetLevelStep(5.4f);
            }
            #endregion

            #region 5.4 Wait For Fight03 Finished
            if (levelStep == 5.4f)
            {
                if (step_E_Fight03_Finished.IsEverActivated())
                {
                    step_E_Ally01ChainJobsGlobalLogicIndex = 5;
                    step_E_Ally_chainJobGroup_01_Ally01.Init_SetNewGlobalLogicIndex(step_E_Ally01ChainJobsGlobalLogicIndex);

                    SetLevelStep(5.45f);
                }

                if (step_E_PlayerEntered_StartFight05Trigger.IsEverActivated())
                {
                    SetLevelStep(5.5f);
                }
            }
            #endregion

            #region 5.45 Wait For Player Enter Fight05 Trigger
            if (levelStep == 5.45f)
            {
                if (step_E_PlayerEntered_StartFight05Trigger.IsEverActivated())
                {
                    SetLevelStep(5.5f);
                }
            }
            #endregion

            #region 5.5 Player Entered Fight05 Trigger
            if (levelStep == 5.5f)
            {
                logicVoiceCollection_Ally01.PlayName("E_05_PoshteSanga");

                step_E_Objects_08_DieTriggerForFight03.StartIt();

                if (step_E_Ally01ChainJobsGlobalLogicIndex != 5)
                {
                    step_E_Ally01ChainJobsGlobalLogicIndex = 5;
                    step_E_Ally_chainJobGroup_01_Ally01.Init_SetNewGlobalLogicIndex(step_E_Ally01ChainJobsGlobalLogicIndex);
                }

                step_E_Enemy_FightInRegsGroup_05.StartOutStepIfNotStarted();

                step_E_Objects_09_ExecutionArea02.StartIt();

                step_E_Objects_10_AllyChangePos02Trigger.StartOutStepIfNotStarted();

                step_E_Objects_12_AllyChangePos03Trigger.StartOutStepIfNotStarted();

                step_E_Objects_03_ExecutionArea01.EndIt();

                step_E_Enemy_FightInRegsGroup_04.StopCreatingMoreSoldiers();

                SetLevelStep(5.55f);
            }
            #endregion

            #region 5.55 Wait To Change Ally Position
            if (levelStep == 5.55f)
            {
                if (step_E_Fight04_Finished.IsEverActivated())
                {
                    step_E_Ally01ChainJobsGlobalLogicIndex = 6;
                    step_E_Ally_chainJobGroup_01_Ally01.Init_SetNewGlobalLogicIndex(step_E_Ally01ChainJobsGlobalLogicIndex);

                    SetLevelStep(5.6f);
                }

                if (step_E_PlayerEntered_AllyChangePos2Trigger.IsEverActivated())
                {
                    SetLevelStep(5.58f);
                }
            }
            #endregion

            #region 5.58 Kill Soldiers Fight04
            if (levelStep == 5.58f)
            {
                step_E_Objects_11_DieTriggerForFight04.StartIt();

                step_E_Ally01ChainJobsGlobalLogicIndex = 6;
                step_E_Ally_chainJobGroup_01_Ally01.Init_SetNewGlobalLogicIndex(step_E_Ally01ChainJobsGlobalLogicIndex);

                SetLevelStep(5.6f);
            }
            #endregion

            #region 5.6 Wait For Player Enter Change Ally03 Trigger
            if (levelStep == 5.6f)
            {
                if (step_E_PlayerEntered_AllyChangePos3Trigger.IsEverActivated() || step_E_Fight05_Finished.IsEverActivated())
                {
                    mapLogic.HUD_RemoveMinimap3DObj("EndStepE");

                    step_E_Objects_09_ExecutionArea02.EndIt();

                    step_E_Objects_13_DieTriggerForFight05.StartIt();

                    SetLevelStep(6f);
                }
            }
            #endregion

            #endregion

            //--------------------------------------------------------------------------------

            #region Step E2

            #region 6 Start Step E2
            if (levelStep == 6f)
            {
                SaveCheckPoint(6f);

                mapLogic.HUD_ShowGameSaved();

                mapLogic.HUD_AddOnlyMinimap3DObj(miniMapObjectTransform_03_EndStepEP2, "EndStepEP2");

                logicVoiceCollection_Ally01.PlayName("EP2_01_BoroBoroLoop");

                step_E_Objects_14_StartFight06Trigger.StartOutStepIfNotStarted();

                if (!loadFromSaveGame)
                {
                    step_E_Ally01ChainJobsGlobalLogicIndex = 7;
                    step_E_Ally_chainJobGroup_01_Ally01.Init_SetNewGlobalLogicIndex(step_E_Ally01ChainJobsGlobalLogicIndex);
                }
                else
                {
                    step_E_Ally01ChainJobsGlobalLogicIndex = 7;
                    step_E_Ally_chainJobGroup_01_Ally01.StartOutStepIfNotStarted();
                }

                PlaySecondMusic();

                SetLevelStep(6.1f);
            }
            #endregion

            #region 6.1 Wait For Player Enter Fight06 Trigger
            if (levelStep == 6.1f)
            {
                if (step_E_PlayerEntered_StartFight06Trigger.IsEverActivated())
                {
                    logicVoiceCollection_Ally01.StopCurVoiceAfterItsFinishing();
                    logicVoiceCollection_Ally01.PlayName("EP2_02_PoshteDerakhta");

                    step_E_Ally01ChainJobsGlobalLogicIndex = 8;
                    step_E_Ally_chainJobGroup_01_Ally01.Init_SetNewGlobalLogicIndex(step_E_Ally01ChainJobsGlobalLogicIndex);

                    step_E_Enemy_FightInRegsGroup_06.StartOutStepIfNotStarted();

                    step_E_Objects_16_AllyChangePos04Trigger.StartOutStepIfNotStarted();

                    step_E_Objects_17_EndTrigger.StartOutStepIfNotStarted();

                    step_E_Objects_15_ExecutionArea03.StartIt();

                    SetLevelStep(6.2f);
                }
            }
            #endregion

            #region 6.2 Ally Change Pos
            if (levelStep == 6.2f)
            {
                if (step_E_PlayerEntered_AllyChangePos4Trigger.IsEverActivated())
                {
                    step_E_Ally01ChainJobsGlobalLogicIndex = 9;
                    step_E_Ally_chainJobGroup_01_Ally01.Init_SetNewGlobalLogicIndex(step_E_Ally01ChainJobsGlobalLogicIndex);

                    SetLevelStep(6.3f);
                }

                if (step_E_Fight06_Finished.IsEverActivated())
                {
                    SetLevelStep(6.3f);
                }
            }
            #endregion

            #region 6.3 End Step
            if (levelStep == 6.3f)
            {
                if (step_E_PlayerEntered_EndTrigger.IsEverActivated() || step_E_Fight06_Finished.IsEverActivated())
                {
                    step_E_Enemy_FightInRegsGroup_06.StopCreatingMoreSoldiers();

                    step_E_Objects_18_DieTriggerForFight06.StartIt();

                    step_E_Ally_chainJobGroup_01_Ally01.StartFinishing_OutStepIfNotFinishing();

                    SetLevelStep(6.4f);
                }
            }
            #endregion

            #region 6.4 End Step
            if (levelStep == 6.4f)
            {
                step_E_Objects_15_ExecutionArea03.EndIt();

                mapLogic.HUD_ObjectivesPage_SetObjectiveDone(1);
                mapLogic.HUD_ShowCompleteMission(0);

                mapLogic.HUD_RemoveMinimap3DObj("EndStepEP2");

                SetLevelStep(7f);
            }
            #endregion

            #endregion

            //--------------------------------------------------------------------------------

            #region Step F

            #region 7 Start Step F
            if (levelStep == 7f)
            {
                SaveCheckPoint(7f);

                mapLogic.HUD_ShowGameSaved();

                mapLogic.HUD_AddOnlyMinimap3DObj(miniMapObjectTransform_04_PaiineSarBalaii, "PaiineSarBalaii");

                if (step_F_IsLoaded || (!step_F_IsLoaded && step_E_ChainJobAlly01_Finished.IsEverActivated()))
                {
                    step_F_Ally01ChainJobsGlobalLogicIndex = 0;
                    step_F_Ally_chainJobGroup_01_Ally01.StartOutStepIfNotStarted();
                    step_F_Ally01ChainJobStepChanged = true;
                }

                step_F_Objects_StartPoint_Ally02.PlaceCharacterOnIt(Ally02);
                step_F_Objects_StartPoint_Ally03.PlaceCharacterOnIt(Ally03);
                step_F_Objects_StartPoint_Ally04.PlaceCharacterOnIt(Ally04);

                step_F_Objects_01_StartStepTrigger.StartOutStepIfNotStarted();

                step_F_Objects_02_StartFight01Trigger.StartOutStepIfNotStarted();

                PlaySecondMusic();

                SetLevelStep(7.1f);
            }
            #endregion

            #region 7.1 Wait For Player Enter Start Step
            if (levelStep == 7.1f)
            {
                if (!step_F_Ally01ChainJobStepChanged)
                {
                    if (step_E_ChainJobAlly01_Finished.IsEverActivated())
                    {
                        step_F_Ally01ChainJobsGlobalLogicIndex = 0;
                        step_F_Ally_chainJobGroup_01_Ally01.StartOutStepIfNotStarted();

                        step_F_Ally01ChainJobStepChanged = true;
                    }
                }

                if (step_F_PlayerEntered_StartStepTrigger.IsEverActivated())
                {
                    step_F_DustashunChainJobsGlobalLogicIndex = 0;
                    step_F_Ally_chainJobGroup_02_Dustashun.StartOutStepIfNotStarted();

                    step_F_Enemy_FightInRegsGroup_01.StartOutStepIfNotStarted();

                    ally01GunInfo.ApplyExternalLogicDamageCoef(true, 0f);
                    ally02GunInfo.ApplyExternalLogicDamageCoef(true, 0f);
                    ally03GunInfo.ApplyExternalLogicDamageCoef(true, 0f);
                    ally04GunInfo.ApplyExternalLogicDamageCoef(true, 0f);

                    if (step_F_Ally01ChainJobStepChanged)
                    {
                        step_F_Ally01ChainJobsGlobalLogicIndex = 1;
                        step_F_Ally_chainJobGroup_01_Ally01.Init_SetNewGlobalLogicIndex(step_F_Ally01ChainJobsGlobalLogicIndex);

                        SetLevelStep(7.2f);
                    }
                    else
                    {
                        SetLevelStep(7.15f);
                    }
                }
            }
            #endregion

            #region 7.15 Wait For Chain Job Prev Step Finished
            if (levelStep == 7.15f)
            {
                if (step_E_ChainJobAlly01_Finished.IsEverActivated())
                {
                    step_F_Ally01ChainJobsGlobalLogicIndex = 1;
                    step_F_Ally_chainJobGroup_01_Ally01.StartOutStepIfNotStarted();

                    SetLevelStep(7.2f);
                }
            }
            #endregion

            #region 7.2 Fight01
            if (levelStep == 7.2f)
            {
                if (step_F_PlayerEntered_StartFight01Trigger.IsEverActivated())
                {
                    logicVoiceCollection_Ally02.PlayName("F_01_Bezanideshun");
                    logicVoiceCollection_Ally03.PlayName("F_01_Bezanideshun");

                    ally01GunInfo.ApplyExternalLogicDamageCoef(false, 1f);
                    ally02GunInfo.ApplyExternalLogicDamageCoef(false, 1f);
                    ally03GunInfo.ApplyExternalLogicDamageCoef(false, 1f);
                    ally04GunInfo.ApplyExternalLogicDamageCoef(false, 1f);

                    step_F_Objects_03_ExitFight01Trigger.StartOutStepIfNotStarted();

                    step_F_Objects_04_ExecutionArea01.StartIt();

                    mapLogic.HUD_ObjectivesPage_SetActiveObjective(2);
                    mapLogic.HUD_ShowNewMission(1);

                    mapLogic.HUD_RemoveMinimap3DObj("PaiineSarBalaii");
                    mapLogic.HUD_AddOnlyMinimap3DObj(miniMapObjectTransform_05_SangareMoghavematKardan, "Moghavemat");

                    SetLevelStep(7.3f);
                }
            }
            #endregion

            #region 7.3 Wait For Fight01 Finish Or exit Trigger
            if (levelStep == 7.3f)
            {
                if (step_F_PlayerEntered_ExitFight01Trigger.IsEverActivated() || step_F_Fight01_Finished.IsEverActivated())
                {
                    logicVoiceCollection_Ally02.PlayName("F_02_RaBioftin");

                    step_F_Ally01ChainJobsGlobalLogicIndex = 2;
                    step_F_Ally_chainJobGroup_01_Ally01.Init_SetNewGlobalLogicIndex(step_F_Ally01ChainJobsGlobalLogicIndex);

                    step_F_DustashunChainJobsGlobalLogicIndex = 1;
                    step_F_Ally_chainJobGroup_02_Dustashun.Init_SetNewGlobalLogicIndex(step_F_DustashunChainJobsGlobalLogicIndex);

                    step_F_Enemy_FightInRegsGroup_01.StopCreatingMoreSoldiers();

                    step_F_Objects_05_DieTriggerForFight01.StartIt();

                    step_F_Objects_06_StartFight02Trigger.StartOutStepIfNotStarted();

                    mapLogic.HUD_ObjectivesPage_SetObjectiveDone(2);
                    mapLogic.HUD_ShowCompleteMission(1);

                    SetLevelStep(7.4f);
                }
            }
            #endregion

            #region 7.4 Start Fight02
            if (levelStep == 7.4f)
            {
                if (step_F_PlayerEntered_StartFight02Trigger.IsEverActivated())
                {
                    logicVoiceCollection_Ally01.PlayName("F_03_MoghavematLoop");
                    logicVoiceCollection_Ally02.PlayName("F_03_MoghavematLoop");
                    logicVoiceCollection_Ally03.PlayName("F_03_MoghavematLoop");

                    step_F_Objects_08_AudioInfo_VoiceAAAAAAA.Play();

                    step_F_Ally01ChainJobsGlobalLogicIndex = 3;
                    step_F_Ally_chainJobGroup_01_Ally01.Init_SetNewGlobalLogicIndex(step_F_Ally01ChainJobsGlobalLogicIndex);

                    step_F_DustashunChainJobsGlobalLogicIndex = 2;
                    step_F_Ally_chainJobGroup_02_Dustashun.Init_SetNewGlobalLogicIndex(step_F_DustashunChainJobsGlobalLogicIndex);

                    step_F_Objects_04_ExecutionArea01.EndIt();

                    step_F_Enemy_FightInRegsGroup_02.StartOutStepIfNotStarted();

                    step_F_Objects_07_PlayerInFight02Trigger.StartOutStepIfNotStarted();

                    step_F_TimerForFightRegion = step_F_MaxTimeForFightBozorg;

                    mapLogic.HUD_ObjectivesPage_SetActiveObjective(3);
                    mapLogic.HUD_ShowNewMission(2);

                    mapLogic.HUD_RemoveMinimap3DObj("Moghavemat");

                    MusicController.Instance.EndMusicWithFade(MusicFadeType.Slow);

                    SetLevelStep(7.5f);
                }
            }
            #endregion

            #region 7.5 Wait For Fight02 Finish
            if (levelStep == 7.5f)
            {
                if (!MapLogic.Instance.isPlayerHidden)
                {
                    step_F_TimerForFightRegion = MathfPlus.DecByDeltatimeToZero(step_F_TimerForFightRegion);
                }

                if (step_F_TimerForFightRegion == 0)
                {
                    SetLevelStep(7.51f);
                }
            }
            #endregion

            #region 7.51 Check Is Player In Fight02
            if (levelStep == 7.51f)
            {
                if (step_F_PlayerInFight02Trigger.IsActiveNow())
                {
                    logicVoiceCollection_Ally01.StopCurVoiceAfterItsFinishing();
                    logicVoiceCollection_Ally02.StopCurVoiceAfterItsFinishing();
                    logicVoiceCollection_Ally03.StopCurVoiceAfterItsFinishing();

                    //step_F_Enemy_FightInRegsGroup_02.StopCreatingMoreSoldiers();

                    //mapLogic.HUD_ObjectivesPage_SetObjectiveDone(3);
                    //mapLogic.HUD_ShowCompleteMission(2);

                    SetLevelStep(7.5101f);
                }
            }
            #endregion

            #region 7.5101 Start Mortars
            if (levelStep == 7.5101f)
            {
                for (int i = 0; i < step_F_Objects_Mortars.Length; i++)
                {
                    step_F_Objects_Mortars[i].StartIt();
                }

                SetLevelStep(7.5102f);
            }
            #endregion

            #region 7.5102 W8 for mortars time counter finish.
            if (levelStep == 7.5102f)
            {
                step_F_MortarsTimeCounter = MathfPlus.DecByDeltatimeToZero(step_F_MortarsTimeCounter);

                if (step_F_MortarsTimeCounter == 0)
                {
                    step_F_Enemy_FightInRegsGroup_02.StopCreatingMoreSoldiers();

                    mapLogic.HUD_ObjectivesPage_SetObjectiveDone(3);
                    mapLogic.HUD_ShowCompleteMission(2);

                    SetLevelStep(7.5103f);
                }
            }
            #endregion

            #region 7.5103 Start black screen fade out
            if (levelStep == 7.5103f)
            {
                mapLogic.blackScreenFader.StartFadingOut();
                SetLevelStep(7.5104f);
            }
            #endregion

            #region 7.5104 fading out black screen
            if (levelStep == 7.5104f)
            {
                if (mapLogic.blackScreenFader.isFadingFinished)
                {
                    SetLevelStep(7.511f);
                }
            }
            #endregion

            #region 7.511 Run Cutscene
            if (levelStep == 7.511f)
            {
                cutsceneBombaraneNakhlestan_Part1.StartIt();

                //cutsceneBombaraneNakhlestan_Part1.GetComponent<RunParticles>().StartIt();

                SetLevelStep(7.512f);
            }
            #endregion

            #region 7.512 Wait For Cutscene End
            if (levelStep == 7.512f)
            {
                if (cutsceneBombaraneNakhlestan_Part1.status == CutsceneStatus.Finished)
                {
                    //cutsceneBombaraneNakhlestan_Part1.GetComponent<RunParticles>().StopIt();

                    SetLevelStep(7.7f);
                }
            }
            #endregion

            #region Old

            //#region 7.52 Run Cutscene
            //if (levelStep == 7.52f)
            //{
            //    cutsceneBombaraneNakhlestan.StartIt();

            //    SetLevelStep(7.53f);
            //}
            //#endregion

            //#region 7.53 Wait For Cutscene End
            //if (levelStep == 7.53f)
            //{
            //    if (cutsceneBombaraneNakhlestan.status == CutsceneStatus.Finished)
            //    {
            //        SetLevelStep(7.6f);
            //    }
            //}
            //#endregion

            #endregion

            #region 7.6 Start End black screen
            if (levelStep == 7.6f)
            {
                //mapLogic.blackScreenFader.StartFadingOut();

                SetLevelStep(7.7f);
            }
            #endregion 

            #region 7.7 Set mission is finished if black screen fading is done.
            if (levelStep == 7.7f)
            {
                //if (mapLogic.blackScreenFader.isFadingFinished)
                //{
                SetMissionIsFinished();
                SetLevelStep(8f);
                //}
            }
            #endregion

            #endregion

        EndLevelSteps: ;

            if (!isForPart2)
            {
                #region A

                #region Step_A_Objects_StartingLevelTrigger01

                #region 1 Start
                if (step_A_Objects_StartingLevelTrigger01.OutStep == 1) //Start
                {
                    step_A_Objects_StartingLevelTrigger01.SetEnabled(true);
                    step_A_Objects_StartingLevelTrigger01.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_A_Objects_StartingLevelTrigger01.OutStep == 1.1f) //Run
                {
                    if (step_A_Objects_StartingLevelTrigger01.IsPlayerIn())
                    {
                        step_A_PlayerEntered_StartingLevelTrigger01.SetStatus(LogicFlagStatus.Active);
                        step_A_Objects_StartingLevelTrigger01.StartFinishing_OutStepIfNotFishining();
                    }
                }
                #endregion

                #region 900 Finish
                if (step_A_Objects_StartingLevelTrigger01.OutStep == 900f) //Finish
                {
                    step_A_Objects_StartingLevelTrigger01.SetEnabled(false);
                    step_A_Objects_StartingLevelTrigger01.SetOutStep(1000f);
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

                #region Step_A_Objects_StartingLevelTrigger02

                #region 1 Start
                if (step_A_Objects_StartingLevelTrigger02.OutStep == 1) //Start
                {
                    step_A_Objects_StartingLevelTrigger02.SetEnabled(true);
                    step_A_Objects_StartingLevelTrigger02.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_A_Objects_StartingLevelTrigger02.OutStep == 1.1f) //Run
                {
                    if (step_A_Objects_StartingLevelTrigger02.IsPlayerIn())
                    {
                        step_A_PlayerEntered_StartingLevelTrigger02.SetStatus(LogicFlagStatus.Active);
                        step_A_Objects_StartingLevelTrigger02.StartFinishing_OutStepIfNotFishining();
                    }
                }
                #endregion

                #region 900 Finish
                if (step_A_Objects_StartingLevelTrigger02.OutStep == 900f) //Finish
                {
                    step_A_Objects_StartingLevelTrigger02.SetEnabled(false);
                    step_A_Objects_StartingLevelTrigger02.SetOutStep(1000f);
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

                #region Step_A_Objects_StartKhalooTrigger

                #region 1 Start
                if (step_A_Objects_StartKhalooTrigger.OutStep == 1) //Start
                {
                    step_A_Objects_StartKhalooTrigger.SetEnabled(true);
                    step_A_Objects_StartKhalooTrigger.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_A_Objects_StartKhalooTrigger.OutStep == 1.1f) //Run
                {
                    if (step_A_Objects_StartKhalooTrigger.IsPlayerIn())
                    {
                        step_A_PlayerEntered_StartKhalooTrigger.SetStatus(LogicFlagStatus.Active);
                        step_A_Objects_StartKhalooTrigger.StartFinishing_OutStepIfNotFishining();
                    }
                }
                #endregion

                #region 900 Finish
                if (step_A_Objects_StartKhalooTrigger.OutStep == 900f) //Finish
                {
                    step_A_Objects_StartKhalooTrigger.SetEnabled(false);
                    step_A_Objects_StartKhalooTrigger.SetOutStep(1000f);
                }
                #endregion

                #endregion

                #region Step_A_Ally_KhalooStartFightReg

                #region 1 Start
                if (step_A_Ally_KhalooStartFightReg.outStep == 1f)
                {
                    step_A_Ally_KhalooStartFightReg.StartIt();
                    step_A_Ally_KhalooStartFightReg.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_A_Ally_KhalooStartFightReg.outStep == 1.1f)
                {
                    step_A_Ally_KhalooStartFightReg.RunIt();
                }
                #endregion

                #region 900 Start Finishing
                if (step_A_Ally_KhalooStartFightReg.outStep == 900f)
                {
                    step_A_Ally_KhalooStartFightReg.SetNeedsToBeFinished();
                    step_A_Ally_KhalooStartFightReg.SetOutStep(901f);
                }
                #endregion

                #region 901 Finishing
                if (step_A_Ally_KhalooStartFightReg.outStep == 901f)
                {
                    step_A_Ally_KhalooStartFightReg.RunIt();

                    if (step_A_Ally_KhalooStartFightReg.status == LogicJobStatus.Finished)
                    {
                        step_A_Ally_KhalooStartFightReg.SetOutStep(1000f);
                        step_A_StartingKhaloo_Finished.SetStatus(LogicFlagStatus.Active);
                    }
                }
                #endregion

                #endregion

                #region Step_A_Objects_ExitStepATrigger

                #region 1 Start
                if (step_A_Objects_ExitStepATrigger.OutStep == 1) //Start
                {
                    step_A_Objects_ExitStepATrigger.SetEnabled(true);
                    step_A_Objects_ExitStepATrigger.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_A_Objects_ExitStepATrigger.OutStep == 1.1f) //Run
                {
                    if (step_A_Objects_ExitStepATrigger.IsPlayerIn())
                    {
                        step_A_PlayerEntered_ExitStepATrigger.SetStatus(LogicFlagStatus.Active);
                        step_A_Objects_ExitStepATrigger.StartFinishing_OutStepIfNotFishining();
                    }
                }
                #endregion

                #region 900 Finish
                if (step_A_Objects_ExitStepATrigger.OutStep == 900f) //Finish
                {
                    step_A_Objects_ExitStepATrigger.SetEnabled(false);
                    step_A_Objects_ExitStepATrigger.SetOutStep(1000f);
                }
                #endregion

                #endregion

                #endregion

                #region B

                #region step_B_Objects_StartingStepBTrigger

                #region 1 Start
                if (step_B_Objects_01_StartingStepBTrigger.OutStep == 1) //Start
                {
                    step_B_Objects_01_StartingStepBTrigger.SetEnabled(true);
                    step_B_Objects_01_StartingStepBTrigger.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_B_Objects_01_StartingStepBTrigger.OutStep == 1.1f) //Run
                {
                    if (step_B_Objects_01_StartingStepBTrigger.IsSomethingIn())
                    {
                        step_B_SomeOneEntered_StartingStepBTrigger.SetStatus(LogicFlagStatus.Active);
                        step_B_Objects_01_StartingStepBTrigger.StartFinishing_OutStepIfNotFishining();
                    }
                }
                #endregion

                #region 900 Finish
                if (step_B_Objects_01_StartingStepBTrigger.OutStep == 900f) //Finish
                {
                    step_B_Objects_01_StartingStepBTrigger.SetEnabled(false);
                    step_B_Objects_01_StartingStepBTrigger.SetOutStep(1000f);
                }
                #endregion

                #endregion

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
                        step_B_Fight01_Finished.SetStatus(LogicFlagStatus.Active);
                    }
                }
                #endregion

            step_B_Enemy_FightInRegsGroup_01_End: ;

                #endregion

                #region step_B_chainJobGroup_01_khalooStart

                #region 1 Start
                if (step_B_Ally_chainJobGroup_01_khalooStart.outStep == 1)
                {
                    step_B_Ally_chainJobGroup_01_khalooStart.Init_SetNewGlobalLogicIndex(step_B_KhalooChainJobsGlobalLogicIndex);
                    step_B_Ally_chainJobGroup_01_khalooStart.StartIt();

                    step_B_Ally_chainJobGroup_01_khalooStart.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_B_Ally_chainJobGroup_01_khalooStart.outStep == 1.1f)
                {
                    step_B_Ally_chainJobGroup_01_khalooStart.RunIt();
                }
                #endregion

                #region 900 Start finishing
                if (step_B_Ally_chainJobGroup_01_khalooStart.outStep == 900f)
                {
                    step_B_Ally_chainJobGroup_01_khalooStart.SetNeedsToBeFinished();

                    step_B_Ally_chainJobGroup_01_khalooStart.SetOutStep(901f);
                }
                #endregion

                #region 901 Finishing
                if (step_B_Ally_chainJobGroup_01_khalooStart.outStep == 901f)
                {
                    step_B_Ally_chainJobGroup_01_khalooStart.RunIt();

                    if (step_B_Ally_chainJobGroup_01_khalooStart.status == LogicJobStatus.Finished)
                    {
                        step_B_ChainJobKhaloo_Finished.SetStatus(LogicFlagStatus.Active);
                        step_B_Ally_chainJobGroup_01_khalooStart.SetOutStep(1000);
                    }
                }
                #endregion

                #endregion

                #region step_B_Objects_02_KhalooChangePos

                #region 1 Start
                if (step_B_Objects_02_KhalooChangePos.OutStep == 1) //Start
                {
                    step_B_Objects_02_KhalooChangePos.SetEnabled(true);
                    step_B_Objects_02_KhalooChangePos.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_B_Objects_02_KhalooChangePos.OutStep == 1.1f) //Run
                {
                    if (step_B_Objects_02_KhalooChangePos.IsPlayerIn())
                    {
                        step_B_PlayerEntered_KhalooChangePosTrigger.SetStatus(LogicFlagStatus.Active);
                        step_B_Objects_02_KhalooChangePos.StartFinishing_OutStepIfNotFishining();
                    }
                }
                #endregion

                #region 900 Finish
                if (step_B_Objects_02_KhalooChangePos.OutStep == 900f) //Finish
                {
                    step_B_Objects_02_KhalooChangePos.SetEnabled(false);
                    step_B_Objects_02_KhalooChangePos.SetOutStep(1000f);
                }
                #endregion

                #endregion

                #region step_B_Objects_03_KhalooChangePos02

                #region 1 Start
                if (step_B_Objects_03_KhalooChangePos02.OutStep == 1) //Start
                {
                    step_B_Objects_03_KhalooChangePos02.SetEnabled(true);
                    step_B_Objects_03_KhalooChangePos02.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_B_Objects_03_KhalooChangePos02.OutStep == 1.1f) //Run
                {
                    if (step_B_Objects_03_KhalooChangePos02.IsPlayerIn())
                    {
                        step_B_PlayerEntered_KhalooChangePos2Trigger.SetStatus(LogicFlagStatus.Active);
                        step_B_Objects_03_KhalooChangePos02.StartFinishing_OutStepIfNotFishining();
                    }
                }
                #endregion

                #region 900 Finish
                if (step_B_Objects_03_KhalooChangePos02.OutStep == 900f) //Finish
                {
                    step_B_Objects_03_KhalooChangePos02.SetEnabled(false);
                    step_B_Objects_03_KhalooChangePos02.SetOutStep(1000f);
                }
                #endregion

                #endregion

                #region step_B_Objects_04_StartFightReg02Trigger

                #region 1 Start
                if (step_B_Objects_04_StartFightReg02Trigger.OutStep == 1) //Start
                {
                    step_B_Objects_04_StartFightReg02Trigger.SetEnabled(true);
                    step_B_Objects_04_StartFightReg02Trigger.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_B_Objects_04_StartFightReg02Trigger.OutStep == 1.1f) //Run
                {
                    if (step_B_Objects_04_StartFightReg02Trigger.IsPlayerIn())
                    {
                        step_B_PlayerEntered_StartFightReg2Trigger.SetStatus(LogicFlagStatus.Active);
                        step_B_Objects_04_StartFightReg02Trigger.StartFinishing_OutStepIfNotFishining();
                    }
                }
                #endregion

                #region 900 Finish
                if (step_B_Objects_04_StartFightReg02Trigger.OutStep == 900f) //Finish
                {
                    step_B_Objects_04_StartFightReg02Trigger.SetEnabled(false);
                    step_B_Objects_04_StartFightReg02Trigger.SetOutStep(1000f);
                }
                #endregion

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
                        step_B_Fight02_Finished.SetStatus(LogicFlagStatus.Active);
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
                        step_B_Fight03_Finished.SetStatus(LogicFlagStatus.Active);
                    }
                }
                #endregion

            step_B_Enemy_FightInRegsGroup_03_End: ;

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

                #region step_B_Objects_05_StartKhalooCommandTrigger

                #region 1 Start
                if (step_B_Objects_05_KhalooCommandTrigger.OutStep == 1) //Start
                {
                    step_B_Objects_05_KhalooCommandTrigger.SetEnabled(true);
                    step_B_Objects_05_KhalooCommandTrigger.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_B_Objects_05_KhalooCommandTrigger.OutStep == 1.1f) //Run
                {
                    if (step_B_Objects_05_KhalooCommandTrigger.IsSomethingIn())
                    {
                        step_B_SomeOneEntered_KhalooCommandTrigger.SetStatus(LogicFlagStatus.Active);
                        step_B_Objects_05_KhalooCommandTrigger.StartFinishing_OutStepIfNotFishining();
                    }
                }
                #endregion

                #region 900 Finish
                if (step_B_Objects_05_KhalooCommandTrigger.OutStep == 900f) //Finish
                {
                    step_B_Objects_05_KhalooCommandTrigger.SetEnabled(false);
                    step_B_Objects_05_KhalooCommandTrigger.SetOutStep(1000f);
                }
                #endregion

                #endregion

                #region step_B_chainJobGroup_02_Dustashun

                #region 1 Start
                if (step_B_Ally_chainJobGroup_02_Dustashun.outStep == 1)
                {
                    step_B_Ally_chainJobGroup_02_Dustashun.Init_SetNewGlobalLogicIndex(step_B_AllyChainJobsGlobalLogicIndex);
                    step_B_Ally_chainJobGroup_02_Dustashun.StartIt();

                    step_B_Ally_chainJobGroup_02_Dustashun.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_B_Ally_chainJobGroup_02_Dustashun.outStep == 1.1f)
                {
                    step_B_Ally_chainJobGroup_02_Dustashun.RunIt();
                }
                #endregion

                #region 900 Start finishing
                if (step_B_Ally_chainJobGroup_02_Dustashun.outStep == 900f)
                {
                    step_B_Ally_chainJobGroup_02_Dustashun.SetNeedsToBeFinished();

                    step_B_Ally_chainJobGroup_02_Dustashun.SetOutStep(901f);
                }
                #endregion

                #region 901 Finishing
                if (step_B_Ally_chainJobGroup_02_Dustashun.outStep == 901f)
                {
                    step_B_Ally_chainJobGroup_02_Dustashun.RunIt();

                    if (step_B_Ally_chainJobGroup_02_Dustashun.status == LogicJobStatus.Finished)
                    {
                        step_B_ChainJobDustashun_Finished.SetStatus(LogicFlagStatus.Active);
                        step_B_Ally_chainJobGroup_02_Dustashun.SetOutStep(1000);
                    }
                }
                #endregion

                #endregion

                #region step_B_Objects_07_CutsceneBTrigger

                #region 1 Start
                if (step_B_Objects_07_CutsceneBTrigger.OutStep == 1) //Start
                {
                    step_B_Objects_07_CutsceneBTrigger.SetEnabled(true);
                    step_B_Objects_07_CutsceneBTrigger.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_B_Objects_07_CutsceneBTrigger.OutStep == 1.1f) //Run
                {
                    if (step_B_Objects_07_CutsceneBTrigger.IsPlayerIn())
                    {
                        step_B_PlayerEntered_CutsceneBTrigger.SetStatus(LogicFlagStatus.Active);
                        step_B_Objects_07_CutsceneBTrigger.StartFinishing_OutStepIfNotFishining();
                    }
                }
                #endregion

                #region 900 Finish
                if (step_B_Objects_07_CutsceneBTrigger.OutStep == 900f) //Finish
                {
                    step_B_Objects_07_CutsceneBTrigger.SetEnabled(false);
                    step_B_Objects_07_CutsceneBTrigger.SetOutStep(1000f);
                }
                #endregion

                #endregion

                #region step_B_Objects_08_EndStepB

                #region 1 Start
                if (step_B_Objects_08_EndStepB.OutStep == 1) //Start
                {
                    step_B_Objects_08_EndStepB.SetEnabled(true);
                    step_B_Objects_08_EndStepB.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_B_Objects_08_EndStepB.OutStep == 1.1f) //Run
                {
                    if (step_B_Objects_08_EndStepB.IsPlayerIn())
                    {
                        step_B_PlayerEntered_EndStepBTrigger.SetStatus(LogicFlagStatus.Active);
                        step_B_Objects_08_EndStepB.StartFinishing_OutStepIfNotFishining();
                    }
                }
                #endregion

                #region 900 Finish
                if (step_B_Objects_08_EndStepB.OutStep == 900f) //Finish
                {
                    step_B_Objects_08_EndStepB.SetEnabled(false);
                    step_B_Objects_08_EndStepB.SetOutStep(1000f);
                }
                #endregion

                #endregion

                #region step_B_Objects_09_FailMissionTrigger

                #region 1 Start
                if (step_B_Objects_09_FailMissionTrigger.OutStep == 1) //Start
                {
                    step_B_Objects_09_FailMissionTrigger.SetEnabled(true);
                    step_B_Objects_09_FailMissionTrigger.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_B_Objects_09_FailMissionTrigger.OutStep == 1.1f) //Run
                {
                    if (step_B_Objects_09_FailMissionTrigger.IsPlayerIn())
                    {
                        step_B_PlayerEntered_FailMissionTrigger.SetStatus(LogicFlagStatus.Active);
                        step_B_Objects_09_FailMissionTrigger.StartFinishing_OutStepIfNotFishining();
                    }
                }
                #endregion

                #region 900 Finish
                if (step_B_Objects_09_FailMissionTrigger.OutStep == 900f) //Finish
                {
                    step_B_Objects_09_FailMissionTrigger.SetEnabled(false);
                    step_B_Objects_09_FailMissionTrigger.SetOutStep(1000f);
                }
                #endregion

                #endregion

                #endregion

                #region C

                #region step_C_Ally_chainJobGroup_01_Khaloo

                #region 1 Start
                if (step_C_Ally_chainJobGroup_01_Khaloo.outStep == 1)
                {
                    step_C_Ally_chainJobGroup_01_Khaloo.Init_SetNewGlobalLogicIndex(step_C_KhalooChainJobsGlobalLogicIndex);
                    step_C_Ally_chainJobGroup_01_Khaloo.StartIt();

                    step_C_Ally_chainJobGroup_01_Khaloo.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_C_Ally_chainJobGroup_01_Khaloo.outStep == 1.1f)
                {
                    step_C_Ally_chainJobGroup_01_Khaloo.RunIt();
                }
                #endregion

                #region 900 Start finishing
                if (step_C_Ally_chainJobGroup_01_Khaloo.outStep == 900f)
                {
                    step_C_Ally_chainJobGroup_01_Khaloo.SetNeedsToBeFinished();

                    step_C_Ally_chainJobGroup_01_Khaloo.SetOutStep(901f);
                }
                #endregion

                #region 901 Finishing
                if (step_C_Ally_chainJobGroup_01_Khaloo.outStep == 901f)
                {
                    step_C_Ally_chainJobGroup_01_Khaloo.RunIt();

                    if (step_C_Ally_chainJobGroup_01_Khaloo.status == LogicJobStatus.Finished)
                    {
                        step_C_ChainJobKhaloo_Finished.SetStatus(LogicFlagStatus.Active);

                        step_C_Ally_chainJobGroup_01_Khaloo.SetOutStep(1000);
                    }
                }
                #endregion

                #endregion

                #region step_C_Ally_chainJobGroup_02_Ally01

                #region 1 Start
                if (step_C_Ally_chainJobGroup_02_Ally01.outStep == 1)
                {
                    step_C_Ally_chainJobGroup_02_Ally01.Init_SetNewGlobalLogicIndex(step_C_Ally01ChainJobsGlobalLogicIndex);
                    step_C_Ally_chainJobGroup_02_Ally01.StartIt();

                    step_C_Ally_chainJobGroup_02_Ally01.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_C_Ally_chainJobGroup_02_Ally01.outStep == 1.1f)
                {
                    step_C_Ally_chainJobGroup_02_Ally01.RunIt();
                }
                #endregion

                #region 900 Start finishing
                if (step_C_Ally_chainJobGroup_02_Ally01.outStep == 900f)
                {
                    step_C_Ally_chainJobGroup_02_Ally01.SetNeedsToBeFinished();

                    step_C_Ally_chainJobGroup_02_Ally01.SetOutStep(901f);
                }
                #endregion

                #region 901 Finishing
                if (step_C_Ally_chainJobGroup_02_Ally01.outStep == 901f)
                {
                    step_C_Ally_chainJobGroup_02_Ally01.RunIt();

                    if (step_C_Ally_chainJobGroup_02_Ally01.status == LogicJobStatus.Finished)
                    {
                        step_C_ChainJobAlly02_Finished.SetStatus(LogicFlagStatus.Active);

                        step_C_Ally_chainJobGroup_02_Ally01.SetOutStep(1000);
                    }
                }
                #endregion

                #endregion

                #region step_C_Ally_chainJobGroup_03_Ally02

                #region 1 Start
                if (step_C_Ally_chainJobGroup_03_Ally02.outStep == 1)
                {
                    step_C_Ally_chainJobGroup_03_Ally02.Init_SetNewGlobalLogicIndex(step_C_Ally02ChainJobsGlobalLogicIndex);
                    step_C_Ally_chainJobGroup_03_Ally02.StartIt();

                    step_C_Ally_chainJobGroup_03_Ally02.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_C_Ally_chainJobGroup_03_Ally02.outStep == 1.1f)
                {
                    step_C_Ally_chainJobGroup_03_Ally02.RunIt();
                }
                #endregion

                #region 900 Start finishing
                if (step_C_Ally_chainJobGroup_03_Ally02.outStep == 900f)
                {
                    step_C_Ally_chainJobGroup_03_Ally02.SetNeedsToBeFinished();

                    step_C_Ally_chainJobGroup_03_Ally02.SetOutStep(901f);
                }
                #endregion

                #region 901 Finishing
                if (step_C_Ally_chainJobGroup_03_Ally02.outStep == 901f)
                {
                    step_C_Ally_chainJobGroup_03_Ally02.RunIt();

                    if (step_C_Ally_chainJobGroup_03_Ally02.status == LogicJobStatus.Finished)
                    {
                        step_C_ChainJobAlly01_Finished.SetStatus(LogicFlagStatus.Active);

                        step_C_Ally_chainJobGroup_03_Ally02.SetOutStep(1000);
                    }
                }
                #endregion

                #endregion

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

                #region step_C_Objects_01_StartFight01Trigger

                #region 1 Start
                if (step_C_Objects_01_StartFight01Trigger.OutStep == 1) //Start
                {
                    step_C_Objects_01_StartFight01Trigger.SetEnabled(true);
                    step_C_Objects_01_StartFight01Trigger.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_C_Objects_01_StartFight01Trigger.OutStep == 1.1f) //Run
                {
                    if (step_C_Objects_01_StartFight01Trigger.IsPlayerIn())
                    {
                        step_C_PlayerEntered_StartFight01Trigger.SetStatus(LogicFlagStatus.Active);
                        step_C_Objects_01_StartFight01Trigger.StartFinishing_OutStepIfNotFishining();
                    }
                }
                #endregion

                #region 900 Finish
                if (step_C_Objects_01_StartFight01Trigger.OutStep == 900f) //Finish
                {
                    step_C_Objects_01_StartFight01Trigger.SetEnabled(false);
                    step_C_Objects_01_StartFight01Trigger.SetOutStep(1000f);
                }
                #endregion

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
                        step_C_Fight02_Finished.SetStatus(LogicFlagStatus.Active);
                    }
                }
                #endregion

            step_C_Enemy_FightInRegsGroup_02_End: ;

                #endregion

                #region step_C_Enemy_FightInRegsGroup_03

                #region 1 Start
                if (step_C_Enemy_FightInRegsGroup_03.outStep == 1f)
                {
                    step_C_Enemy_FightInRegsGroup_03.StartIt();
                    step_C_Enemy_FightInRegsGroup_03.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_C_Enemy_FightInRegsGroup_03.outStep == 1.1f)
                {
                    step_C_Enemy_FightInRegsGroup_03.RunIt();

                    if (step_C_Enemy_FightInRegsGroup_03.IsCreatingSoldiersStoppedAndAllSoldsDead())
                    {
                        step_C_Enemy_FightInRegsGroup_03.StartFinishing_OutStepIfNotFinishing();
                        goto step_C_Enemy_FightInRegsGroup_03_End;
                    }
                }
                #endregion

                #region 900 Start Finishing
                if (step_C_Enemy_FightInRegsGroup_03.outStep == 900f)
                {
                    step_C_Enemy_FightInRegsGroup_03.SetNeedsToBeFinished();
                    step_C_Enemy_FightInRegsGroup_03.SetOutStep(901f);
                }
                #endregion

                #region 901 Finishing
                if (step_C_Enemy_FightInRegsGroup_03.outStep == 901f)
                {
                    step_C_Enemy_FightInRegsGroup_03.RunIt();

                    if (step_C_Enemy_FightInRegsGroup_03.status == LogicJobStatus.Finished)
                    {
                        step_C_Enemy_FightInRegsGroup_03.SetOutStep(1000f);
                        step_C_Fight03_Finished.SetStatus(LogicFlagStatus.Active);
                    }
                }
                #endregion

            step_C_Enemy_FightInRegsGroup_03_End: ;

                #endregion

                #region step_C_Objects_02_StartFight02Trigger

                #region 1 Start
                if (step_C_Objects_02_StartFight02Trigger.OutStep == 1) //Start
                {
                    step_C_Objects_02_StartFight02Trigger.SetEnabled(true);
                    step_C_Objects_02_StartFight02Trigger.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_C_Objects_02_StartFight02Trigger.OutStep == 1.1f) //Run
                {
                    if (step_C_Objects_02_StartFight02Trigger.IsPlayerIn())
                    {
                        step_C_PlayerEntered_StartFight02Trigger.SetStatus(LogicFlagStatus.Active);
                        step_C_Objects_02_StartFight02Trigger.StartFinishing_OutStepIfNotFishining();
                    }
                }
                #endregion

                #region 900 Finish
                if (step_C_Objects_02_StartFight02Trigger.OutStep == 900f) //Finish
                {
                    step_C_Objects_02_StartFight02Trigger.SetEnabled(false);
                    step_C_Objects_02_StartFight02Trigger.SetOutStep(1000f);
                }
                #endregion

                #endregion

                #region step_C_Objects_03_StartFight03Trigger

                #region 1 Start
                if (step_C_Objects_03_StartFight03Trigger.OutStep == 1) //Start
                {
                    step_C_Objects_03_StartFight03Trigger.SetEnabled(true);
                    step_C_Objects_03_StartFight03Trigger.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_C_Objects_03_StartFight03Trigger.OutStep == 1.1f) //Run
                {
                    if (step_C_Objects_03_StartFight03Trigger.IsPlayerIn())
                    {
                        step_C_PlayerEntered_StartFight03Trigger.SetStatus(LogicFlagStatus.Active);
                        step_C_Objects_03_StartFight03Trigger.StartFinishing_OutStepIfNotFishining();
                    }
                }
                #endregion

                #region 900 Finish
                if (step_C_Objects_03_StartFight03Trigger.OutStep == 900f) //Finish
                {
                    step_C_Objects_03_StartFight03Trigger.SetEnabled(false);
                    step_C_Objects_03_StartFight03Trigger.SetOutStep(1000f);
                }
                #endregion

                #endregion

                #region step_C_Objects_04_StopFight03Trigger

                #region 1 Start
                if (step_C_Objects_04_StopFight03Trigger.OutStep == 1) //Start
                {
                    step_C_Objects_04_StopFight03Trigger.SetEnabled(true);
                    step_C_Objects_04_StopFight03Trigger.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_C_Objects_04_StopFight03Trigger.OutStep == 1.1f) //Run
                {
                    if (step_C_Objects_04_StopFight03Trigger.IsPlayerIn())
                    {
                        step_C_PlayerEntered_StopFight03Trigger.SetStatus(LogicFlagStatus.Active);
                        step_C_Objects_04_StopFight03Trigger.StartFinishing_OutStepIfNotFishining();
                    }
                }
                #endregion

                #region 900 Finish
                if (step_C_Objects_04_StopFight03Trigger.OutStep == 900f) //Finish
                {
                    step_C_Objects_04_StopFight03Trigger.SetEnabled(false);
                    step_C_Objects_04_StopFight03Trigger.SetOutStep(1000f);
                }
                #endregion

                #endregion

                #region step_C_Objects_06_ExitStepTrigger

                #region 1 Start
                if (step_C_Objects_06_ExitStepTrigger.OutStep == 1) //Start
                {
                    step_C_Objects_06_ExitStepTrigger.SetEnabled(true);
                    step_C_Objects_06_ExitStepTrigger.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_C_Objects_06_ExitStepTrigger.OutStep == 1.1f) //Run
                {
                    if (step_C_Objects_06_ExitStepTrigger.IsPlayerIn())
                    {
                        step_C_PlayerEntered_ExitStepTrigger.SetStatus(LogicFlagStatus.Active);
                        step_C_Objects_06_ExitStepTrigger.StartFinishing_OutStepIfNotFishining();
                    }
                }
                #endregion

                #region 900 Finish
                if (step_C_Objects_06_ExitStepTrigger.OutStep == 900f) //Finish
                {
                    step_C_Objects_06_ExitStepTrigger.SetEnabled(false);
                    step_C_Objects_06_ExitStepTrigger.SetOutStep(1000f);
                }
                #endregion

                #endregion

                #endregion

                #region D

                #region step_D_Ally_chainJobGroup_01_Khaloo

                #region 1 Start
                if (step_D_Ally_chainJobGroup_01_Khaloo.outStep == 1)
                {
                    step_D_Ally_chainJobGroup_01_Khaloo.Init_SetNewGlobalLogicIndex(step_D_KhalooChainJobsGlobalLogicIndex);
                    step_D_Ally_chainJobGroup_01_Khaloo.StartIt();

                    step_D_Ally_chainJobGroup_01_Khaloo.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_D_Ally_chainJobGroup_01_Khaloo.outStep == 1.1f)
                {
                    step_D_Ally_chainJobGroup_01_Khaloo.RunIt();
                }
                #endregion

                #region 900 Start finishing
                if (step_D_Ally_chainJobGroup_01_Khaloo.outStep == 900f)
                {
                    step_D_Ally_chainJobGroup_01_Khaloo.SetNeedsToBeFinished();

                    step_D_Ally_chainJobGroup_01_Khaloo.SetOutStep(901f);
                }
                #endregion

                #region 901 Finishing
                if (step_D_Ally_chainJobGroup_01_Khaloo.outStep == 901f)
                {
                    step_D_Ally_chainJobGroup_01_Khaloo.RunIt();

                    if (step_D_Ally_chainJobGroup_01_Khaloo.status == LogicJobStatus.Finished)
                    {
                        step_D_ChainJobKhaloo_Finished.SetStatus(LogicFlagStatus.Active);

                        step_D_Ally_chainJobGroup_01_Khaloo.SetOutStep(1000);
                    }
                }
                #endregion

                #endregion

                #region step_D_Ally_chainJobGroup_02_Ally01

                #region 1 Start
                if (step_D_Ally_chainJobGroup_02_Ally01.outStep == 1)
                {
                    step_D_Ally_chainJobGroup_02_Ally01.Init_SetNewGlobalLogicIndex(step_D_Ally01ChainJobsGlobalLogicIndex);
                    step_D_Ally_chainJobGroup_02_Ally01.StartIt();

                    step_D_Ally_chainJobGroup_02_Ally01.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_D_Ally_chainJobGroup_02_Ally01.outStep == 1.1f)
                {
                    step_D_Ally_chainJobGroup_02_Ally01.RunIt();
                }
                #endregion

                #region 900 Start finishing
                if (step_D_Ally_chainJobGroup_02_Ally01.outStep == 900f)
                {
                    step_D_Ally_chainJobGroup_02_Ally01.SetNeedsToBeFinished();

                    step_D_Ally_chainJobGroup_02_Ally01.SetOutStep(901f);
                }
                #endregion

                #region 901 Finishing
                if (step_D_Ally_chainJobGroup_02_Ally01.outStep == 901f)
                {
                    step_D_Ally_chainJobGroup_02_Ally01.RunIt();

                    if (step_D_Ally_chainJobGroup_02_Ally01.status == LogicJobStatus.Finished)
                    {
                        step_D_ChainJobAlly01_Finished.SetStatus(LogicFlagStatus.Active);

                        step_D_Ally_chainJobGroup_02_Ally01.SetOutStep(1000);
                    }
                }
                #endregion

                #endregion

                #region step_D_Ally_chainJobGroup_03_Ally02

                #region 1 Start
                if (step_D_Ally_chainJobGroup_03_Ally02.outStep == 1)
                {
                    step_D_Ally_chainJobGroup_03_Ally02.Init_SetNewGlobalLogicIndex(step_D_Ally02ChainJobsGlobalLogicIndex);
                    step_D_Ally_chainJobGroup_03_Ally02.StartIt();

                    step_D_Ally_chainJobGroup_03_Ally02.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_D_Ally_chainJobGroup_03_Ally02.outStep == 1.1f)
                {
                    step_D_Ally_chainJobGroup_03_Ally02.RunIt();
                }
                #endregion

                #region 900 Start finishing
                if (step_D_Ally_chainJobGroup_03_Ally02.outStep == 900f)
                {
                    step_D_Ally_chainJobGroup_03_Ally02.SetNeedsToBeFinished();

                    step_D_Ally_chainJobGroup_03_Ally02.SetOutStep(901f);
                }
                #endregion

                #region 901 Finishing
                if (step_D_Ally_chainJobGroup_03_Ally02.outStep == 901f)
                {
                    step_D_Ally_chainJobGroup_03_Ally02.RunIt();

                    if (step_D_Ally_chainJobGroup_03_Ally02.status == LogicJobStatus.Finished)
                    {
                        step_D_ChainJobAlly02_Finished.SetStatus(LogicFlagStatus.Active);

                        step_D_Ally_chainJobGroup_03_Ally02.SetOutStep(1000);
                    }
                }
                #endregion

                #endregion

                #region step_D_Ally_chainJobGroup_01_Khaloo_C01

                #region 1 Start
                if (step_D_Ally_chainJobGroup_01_Khaloo_C01.outStep == 1)
                {
                    step_D_Ally_chainJobGroup_01_Khaloo_C01.Init_SetNewGlobalLogicIndex(step_D_KhalooChainJobsGlobalLogicIndex);
                    step_D_Ally_chainJobGroup_01_Khaloo_C01.StartIt();

                    step_D_Ally_chainJobGroup_01_Khaloo_C01.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_D_Ally_chainJobGroup_01_Khaloo_C01.outStep == 1.1f)
                {
                    step_D_Ally_chainJobGroup_01_Khaloo_C01.RunIt();
                }
                #endregion

                #region 900 Start finishing
                if (step_D_Ally_chainJobGroup_01_Khaloo_C01.outStep == 900f)
                {
                    step_D_Ally_chainJobGroup_01_Khaloo_C01.SetNeedsToBeFinished();

                    step_D_Ally_chainJobGroup_01_Khaloo_C01.SetOutStep(901f);
                }
                #endregion

                #region 901 Finishing
                if (step_D_Ally_chainJobGroup_01_Khaloo_C01.outStep == 901f)
                {
                    step_D_Ally_chainJobGroup_01_Khaloo_C01.RunIt();

                    if (step_D_Ally_chainJobGroup_01_Khaloo_C01.status == LogicJobStatus.Finished)
                    {
                        step_D_Ally_chainJobGroup_01_Khaloo_C01.SetOutStep(1000);
                    }
                }
                #endregion

                #endregion

                #region step_D_Ally_chainJobGroup_02_Ally01_C01

                #region 1 Start
                if (step_D_Ally_chainJobGroup_02_Ally01_C01.outStep == 1)
                {
                    step_D_Ally_chainJobGroup_02_Ally01_C01.Init_SetNewGlobalLogicIndex(step_D_Ally01ChainJobsGlobalLogicIndex);
                    step_D_Ally_chainJobGroup_02_Ally01_C01.StartIt();

                    step_D_Ally_chainJobGroup_02_Ally01_C01.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_D_Ally_chainJobGroup_02_Ally01_C01.outStep == 1.1f)
                {
                    step_D_Ally_chainJobGroup_02_Ally01_C01.RunIt();
                }
                #endregion

                #region 900 Start finishing
                if (step_D_Ally_chainJobGroup_02_Ally01_C01.outStep == 900f)
                {
                    step_D_Ally_chainJobGroup_02_Ally01_C01.SetNeedsToBeFinished();

                    step_D_Ally_chainJobGroup_02_Ally01_C01.SetOutStep(901f);
                }
                #endregion

                #region 901 Finishing
                if (step_D_Ally_chainJobGroup_02_Ally01_C01.outStep == 901f)
                {
                    step_D_Ally_chainJobGroup_02_Ally01_C01.RunIt();

                    if (step_D_Ally_chainJobGroup_02_Ally01_C01.status == LogicJobStatus.Finished)
                    {
                        step_D_Ally_chainJobGroup_02_Ally01_C01.SetOutStep(1000);
                    }
                }
                #endregion

                #endregion

                #region step_D_Ally_chainJobGroup_03_Ally02_C01

                #region 1 Start
                if (step_D_Ally_chainJobGroup_03_Ally02_C01.outStep == 1)
                {
                    step_D_Ally_chainJobGroup_03_Ally02_C01.Init_SetNewGlobalLogicIndex(step_D_Ally02ChainJobsGlobalLogicIndex);
                    step_D_Ally_chainJobGroup_03_Ally02_C01.StartIt();

                    step_D_Ally_chainJobGroup_03_Ally02_C01.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_D_Ally_chainJobGroup_03_Ally02_C01.outStep == 1.1f)
                {
                    step_D_Ally_chainJobGroup_03_Ally02_C01.RunIt();
                }
                #endregion

                #region 900 Start finishing
                if (step_D_Ally_chainJobGroup_03_Ally02_C01.outStep == 900f)
                {
                    step_D_Ally_chainJobGroup_03_Ally02_C01.SetNeedsToBeFinished();

                    step_D_Ally_chainJobGroup_03_Ally02_C01.SetOutStep(901f);
                }
                #endregion

                #region 901 Finishing
                if (step_D_Ally_chainJobGroup_03_Ally02_C01.outStep == 901f)
                {
                    step_D_Ally_chainJobGroup_03_Ally02_C01.RunIt();

                    if (step_D_Ally_chainJobGroup_03_Ally02_C01.status == LogicJobStatus.Finished)
                    {
                        step_D_Ally_chainJobGroup_03_Ally02_C01.SetOutStep(1000);
                    }
                }
                #endregion

                #endregion

                #region step_D_Ally_chainJobGroup_01_Khaloo_C02

                #region 1 Start
                if (step_D_Ally_chainJobGroup_01_Khaloo_C02.outStep == 1)
                {
                    step_D_Ally_chainJobGroup_01_Khaloo_C02.Init_SetNewGlobalLogicIndex(step_D_KhalooChainJobsGlobalLogicIndex);
                    step_D_Ally_chainJobGroup_01_Khaloo_C02.StartIt();

                    step_D_Ally_chainJobGroup_01_Khaloo_C02.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_D_Ally_chainJobGroup_01_Khaloo_C02.outStep == 1.1f)
                {
                    step_D_Ally_chainJobGroup_01_Khaloo_C02.RunIt();
                }
                #endregion

                #region 900 Start finishing
                if (step_D_Ally_chainJobGroup_01_Khaloo_C02.outStep == 900f)
                {
                    step_D_Ally_chainJobGroup_01_Khaloo_C02.SetNeedsToBeFinished();

                    step_D_Ally_chainJobGroup_01_Khaloo_C02.SetOutStep(901f);
                }
                #endregion

                #region 901 Finishing
                if (step_D_Ally_chainJobGroup_01_Khaloo_C02.outStep == 901f)
                {
                    step_D_Ally_chainJobGroup_01_Khaloo_C02.RunIt();

                    if (step_D_Ally_chainJobGroup_01_Khaloo_C02.status == LogicJobStatus.Finished)
                    {
                        step_D_Ally_chainJobGroup_01_Khaloo_C02.SetOutStep(1000);
                    }
                }
                #endregion

                #endregion

                #region step_D_Ally_chainJobGroup_02_Ally01_C02

                #region 1 Start
                if (step_D_Ally_chainJobGroup_02_Ally01_C02.outStep == 1)
                {
                    step_D_Ally_chainJobGroup_02_Ally01_C02.Init_SetNewGlobalLogicIndex(step_D_Ally01ChainJobsGlobalLogicIndex);
                    step_D_Ally_chainJobGroup_02_Ally01_C02.StartIt();

                    step_D_Ally_chainJobGroup_02_Ally01_C02.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_D_Ally_chainJobGroup_02_Ally01_C02.outStep == 1.1f)
                {
                    step_D_Ally_chainJobGroup_02_Ally01_C02.RunIt();
                }
                #endregion

                #region 900 Start finishing
                if (step_D_Ally_chainJobGroup_02_Ally01_C02.outStep == 900f)
                {
                    step_D_Ally_chainJobGroup_02_Ally01_C02.SetNeedsToBeFinished();

                    step_D_Ally_chainJobGroup_02_Ally01_C02.SetOutStep(901f);
                }
                #endregion

                #region 901 Finishing
                if (step_D_Ally_chainJobGroup_02_Ally01_C02.outStep == 901f)
                {
                    step_D_Ally_chainJobGroup_02_Ally01_C02.RunIt();

                    if (step_D_Ally_chainJobGroup_02_Ally01_C02.status == LogicJobStatus.Finished)
                    {
                        step_D_Ally_chainJobGroup_02_Ally01_C02.SetOutStep(1000);
                    }
                }
                #endregion

                #endregion

                #region step_D_Ally_chainJobGroup_03_Ally02_C02

                #region 1 Start
                if (step_D_Ally_chainJobGroup_03_Ally02_C02.outStep == 1)
                {
                    step_D_Ally_chainJobGroup_03_Ally02_C02.Init_SetNewGlobalLogicIndex(step_D_Ally02ChainJobsGlobalLogicIndex);
                    step_D_Ally_chainJobGroup_03_Ally02_C02.StartIt();

                    step_D_Ally_chainJobGroup_03_Ally02_C02.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_D_Ally_chainJobGroup_03_Ally02_C02.outStep == 1.1f)
                {
                    step_D_Ally_chainJobGroup_03_Ally02_C02.RunIt();
                }
                #endregion

                #region 900 Start finishing
                if (step_D_Ally_chainJobGroup_03_Ally02_C02.outStep == 900f)
                {
                    step_D_Ally_chainJobGroup_03_Ally02_C02.SetNeedsToBeFinished();

                    step_D_Ally_chainJobGroup_03_Ally02_C02.SetOutStep(901f);
                }
                #endregion

                #region 901 Finishing
                if (step_D_Ally_chainJobGroup_03_Ally02_C02.outStep == 901f)
                {
                    step_D_Ally_chainJobGroup_03_Ally02_C02.RunIt();

                    if (step_D_Ally_chainJobGroup_03_Ally02_C02.status == LogicJobStatus.Finished)
                    {
                        step_D_Ally_chainJobGroup_03_Ally02_C02.SetOutStep(1000);
                    }
                }
                #endregion

                #endregion

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
                        step_D_Fight01_Finished.SetStatus(LogicFlagStatus.Active);
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
                        step_D_Fight02_Finished.SetStatus(LogicFlagStatus.Active);
                    }
                }
                #endregion

            step_D_Enemy_FightInRegsGroup_02_End: ;

                #endregion

                #region step_D_Enemy_FightInRegsGroup_03

                #region 1 Start
                if (step_D_Enemy_FightInRegsGroup_03.outStep == 1f)
                {
                    step_D_Enemy_FightInRegsGroup_03.StartIt();
                    step_D_Enemy_FightInRegsGroup_03.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_D_Enemy_FightInRegsGroup_03.outStep == 1.1f)
                {
                    step_D_Enemy_FightInRegsGroup_03.RunIt();

                    if (step_D_Enemy_FightInRegsGroup_03.IsCreatingSoldiersStoppedAndAllSoldsDead())
                    {
                        step_D_Enemy_FightInRegsGroup_03.StartFinishing_OutStepIfNotFinishing();
                        goto step_D_Enemy_FightInRegsGroup_03_End;
                    }
                }
                #endregion

                #region 900 Start Finishing
                if (step_D_Enemy_FightInRegsGroup_03.outStep == 900f)
                {
                    step_D_Enemy_FightInRegsGroup_03.SetNeedsToBeFinished();
                    step_D_Enemy_FightInRegsGroup_03.SetOutStep(901f);
                }
                #endregion

                #region 901 Finishing
                if (step_D_Enemy_FightInRegsGroup_03.outStep == 901f)
                {
                    step_D_Enemy_FightInRegsGroup_03.RunIt();

                    if (step_D_Enemy_FightInRegsGroup_03.status == LogicJobStatus.Finished)
                    {
                        step_D_Enemy_FightInRegsGroup_03.SetOutStep(1000f);
                        step_D_Fight03_Finished.SetStatus(LogicFlagStatus.Active);
                    }
                }
                #endregion

            step_D_Enemy_FightInRegsGroup_03_End: ;

                #endregion

                #region step_D_Enemy_FightInRegsGroup_04

                #region 1 Start
                if (step_D_Enemy_FightInRegsGroup_04.outStep == 1f)
                {
                    step_D_Enemy_FightInRegsGroup_04.StartIt();
                    step_D_Enemy_FightInRegsGroup_04.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_D_Enemy_FightInRegsGroup_04.outStep == 1.1f)
                {
                    step_D_Enemy_FightInRegsGroup_04.RunIt();

                    if (step_D_Enemy_FightInRegsGroup_04.IsCreatingSoldiersStoppedAndAllSoldsDead())
                    {
                        step_D_Enemy_FightInRegsGroup_04.StartFinishing_OutStepIfNotFinishing();
                        goto step_D_Enemy_FightInRegsGroup_04_End;
                    }
                }
                #endregion

                #region 900 Start Finishing
                if (step_D_Enemy_FightInRegsGroup_04.outStep == 900f)
                {
                    step_D_Enemy_FightInRegsGroup_04.SetNeedsToBeFinished();
                    step_D_Enemy_FightInRegsGroup_04.SetOutStep(901f);
                }
                #endregion

                #region 901 Finishing
                if (step_D_Enemy_FightInRegsGroup_04.outStep == 901f)
                {
                    step_D_Enemy_FightInRegsGroup_04.RunIt();

                    if (step_D_Enemy_FightInRegsGroup_04.status == LogicJobStatus.Finished)
                    {
                        step_D_Enemy_FightInRegsGroup_04.SetOutStep(1000f);
                        step_D_Fight04_Finished.SetStatus(LogicFlagStatus.Active);
                    }
                }
                #endregion

            step_D_Enemy_FightInRegsGroup_04_End: ;

                #endregion

                #region step_D_Enemy_FightInRegsGroup_05

                #region 1 Start
                if (step_D_Enemy_FightInRegsGroup_05.outStep == 1f)
                {
                    step_D_Enemy_FightInRegsGroup_05.StartIt();
                    step_D_Enemy_FightInRegsGroup_05.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_D_Enemy_FightInRegsGroup_05.outStep == 1.1f)
                {
                    step_D_Enemy_FightInRegsGroup_05.RunIt();

                    if (step_D_Enemy_FightInRegsGroup_05.IsCreatingSoldiersStoppedAndAllSoldsDead())
                    {
                        step_D_Enemy_FightInRegsGroup_05.StartFinishing_OutStepIfNotFinishing();
                        goto step_D_Enemy_FightInRegsGroup_05_End;
                    }
                }
                #endregion

                #region 900 Start Finishing
                if (step_D_Enemy_FightInRegsGroup_05.outStep == 900f)
                {
                    step_D_Enemy_FightInRegsGroup_05.SetNeedsToBeFinished();
                    step_D_Enemy_FightInRegsGroup_05.SetOutStep(901f);
                }
                #endregion

                #region 901 Finishing
                if (step_D_Enemy_FightInRegsGroup_05.outStep == 901f)
                {
                    step_D_Enemy_FightInRegsGroup_05.RunIt();

                    if (step_D_Enemy_FightInRegsGroup_05.status == LogicJobStatus.Finished)
                    {
                        step_D_Enemy_FightInRegsGroup_05.SetOutStep(1000f);
                        step_D_Fight05_Finished.SetStatus(LogicFlagStatus.Active);
                    }
                }
                #endregion

            step_D_Enemy_FightInRegsGroup_05_End: ;

                #endregion

                #region step_D_Enemy_FightInRegsGroup_C02

                #region 1 Start
                if (step_D_Enemy_FightInRegsGroup_C02.outStep == 1f)
                {
                    step_D_Enemy_FightInRegsGroup_C02.StartIt();
                    step_D_Enemy_FightInRegsGroup_C02.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_D_Enemy_FightInRegsGroup_C02.outStep == 1.1f)
                {
                    step_D_Enemy_FightInRegsGroup_C02.RunIt();

                    if (step_D_Enemy_FightInRegsGroup_C02.IsCreatingSoldiersStoppedAndAllSoldsDead())
                    {
                        step_D_Enemy_FightInRegsGroup_C02.StartFinishing_OutStepIfNotFinishing();
                        goto step_D_Enemy_FightInRegsGroup_C02_End;
                    }
                }
                #endregion

                #region 900 Start Finishing
                if (step_D_Enemy_FightInRegsGroup_C02.outStep == 900f)
                {
                    step_D_Enemy_FightInRegsGroup_C02.SetNeedsToBeFinished();
                    step_D_Enemy_FightInRegsGroup_C02.SetOutStep(901f);
                }
                #endregion

                #region 901 Finishing
                if (step_D_Enemy_FightInRegsGroup_C02.outStep == 901f)
                {
                    step_D_Enemy_FightInRegsGroup_C02.RunIt();

                    if (step_D_Enemy_FightInRegsGroup_C02.status == LogicJobStatus.Finished)
                    {
                        step_D_Enemy_FightInRegsGroup_C02.SetOutStep(1000f);
                        step_D_FightC02_Finished.SetStatus(LogicFlagStatus.Active);
                    }
                }
                #endregion

            step_D_Enemy_FightInRegsGroup_C02_End: ;

                #endregion

                #region step_D_Enemy_FightInRegsGroup_C03

                #region 1 Start
                if (step_D_Enemy_FightInRegsGroup_C03.outStep == 1f)
                {
                    step_D_Enemy_FightInRegsGroup_C03.StartIt();
                    step_D_Enemy_FightInRegsGroup_C03.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_D_Enemy_FightInRegsGroup_C03.outStep == 1.1f)
                {
                    step_D_Enemy_FightInRegsGroup_C03.RunIt();

                    if (step_D_Enemy_FightInRegsGroup_C03.IsCreatingSoldiersStoppedAndAllSoldsDead())
                    {
                        step_D_Enemy_FightInRegsGroup_C03.StartFinishing_OutStepIfNotFinishing();
                        goto step_D_Enemy_FightInRegsGroup_C03_End;
                    }
                }
                #endregion

                #region 900 Start Finishing
                if (step_D_Enemy_FightInRegsGroup_C03.outStep == 900f)
                {
                    step_D_Enemy_FightInRegsGroup_C03.SetNeedsToBeFinished();
                    step_D_Enemy_FightInRegsGroup_C03.SetOutStep(901f);
                }
                #endregion

                #region 901 Finishing
                if (step_D_Enemy_FightInRegsGroup_C03.outStep == 901f)
                {
                    step_D_Enemy_FightInRegsGroup_C03.RunIt();

                    if (step_D_Enemy_FightInRegsGroup_C03.status == LogicJobStatus.Finished)
                    {
                        step_D_Enemy_FightInRegsGroup_C03.SetOutStep(1000f);
                        step_D_FightC03_Finished.SetStatus(LogicFlagStatus.Active);
                    }
                }
                #endregion

            step_D_Enemy_FightInRegsGroup_C03_End: ;

                #endregion

                #region step_D_Enemy_FightInRegsGroup_Fail

                #region 1 Start
                if (step_D_Enemy_FightInRegsGroup_Fail.outStep == 1f)
                {
                    step_D_Enemy_FightInRegsGroup_Fail.StartIt();
                    step_D_Enemy_FightInRegsGroup_Fail.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_D_Enemy_FightInRegsGroup_Fail.outStep == 1.1f)
                {
                    step_D_Enemy_FightInRegsGroup_Fail.RunIt();

                    if (step_D_Enemy_FightInRegsGroup_Fail.IsCreatingSoldiersStoppedAndAllSoldsDead())
                    {
                        step_D_Enemy_FightInRegsGroup_Fail.StartFinishing_OutStepIfNotFinishing();
                        goto step_D_Enemy_FightInRegsGroup_Fail_End;
                    }
                }
                #endregion

                #region 900 Start Finishing
                if (step_D_Enemy_FightInRegsGroup_Fail.outStep == 900f)
                {
                    step_D_Enemy_FightInRegsGroup_Fail.SetNeedsToBeFinished();
                    step_D_Enemy_FightInRegsGroup_Fail.SetOutStep(901f);
                }
                #endregion

                #region 901 Finishing
                if (step_D_Enemy_FightInRegsGroup_Fail.outStep == 901f)
                {
                    step_D_Enemy_FightInRegsGroup_Fail.RunIt();

                    if (step_D_Enemy_FightInRegsGroup_Fail.status == LogicJobStatus.Finished)
                    {
                        step_D_Enemy_FightInRegsGroup_Fail.SetOutStep(1000f);
                    }
                }
                #endregion

            step_D_Enemy_FightInRegsGroup_Fail_End: ;

                #endregion

                #region step_D_Enemy_MachineGun_01

                #region 1 Start
                if (step_D_Enemy_MachineGun_01.outStep == 1)
                {
                    step_D_Enemy_MachineGun_01.StartIt();

                    step_D_Enemy_MachineGun_01.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_D_Enemy_MachineGun_01.outStep == 1.1f)
                {
                    step_D_Enemy_MachineGun_01.RunIt();

                    if (step_D_Enemy_MachineGun_01.IsCreatingSoldiersStoppedAndAllSoldsDead())
                    {
                        step_D_Enemy_MachineGun_01.StartFinishing_OutStepIfNotFinishing();

                        goto step_D_Enemy_MachineGun_01_End;
                    }
                }
                #endregion

                #region 900 Start finishing
                if (step_D_Enemy_MachineGun_01.outStep == 900f)
                {
                    step_D_Enemy_MachineGun_01.SetNeedsToBeFinished();
                    step_D_Enemy_MachineGun_01.SetOutStep(901);
                }
                #endregion

                #region 901 Finishing
                if (step_D_Enemy_MachineGun_01.outStep == 901f)
                {
                    step_D_Enemy_MachineGun_01.RunIt();

                    if (step_D_Enemy_MachineGun_01.status == LogicJobStatus.Finished)
                    {
                        step_D_Enemy_MachineGun_01.SetOutStep(1000f);
                    }
                }
                #endregion

            step_D_Enemy_MachineGun_01_End: ;

                #endregion

                #region step_D_Enemy_MachineGun_02

                #region 1 Start
                if (step_D_Enemy_MachineGun_02.outStep == 1)
                {
                    step_D_Enemy_MachineGun_02.StartIt();

                    step_D_Enemy_MachineGun_02.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_D_Enemy_MachineGun_02.outStep == 1.1f)
                {
                    step_D_Enemy_MachineGun_02.RunIt();

                    if (step_D_Enemy_MachineGun_02.IsCreatingSoldiersStoppedAndAllSoldsDead())
                    {
                        step_D_Enemy_MachineGun_02.StartFinishing_OutStepIfNotFinishing();

                        goto step_D_Enemy_MachineGun_02_End;
                    }
                }
                #endregion

                #region 900 Start finishing
                if (step_D_Enemy_MachineGun_02.outStep == 900f)
                {
                    step_D_Enemy_MachineGun_02.SetNeedsToBeFinished();
                    step_D_Enemy_MachineGun_02.SetOutStep(901);
                }
                #endregion

                #region 901 Finishing
                if (step_D_Enemy_MachineGun_02.outStep == 901f)
                {
                    step_D_Enemy_MachineGun_02.RunIt();

                    if (step_D_Enemy_MachineGun_02.status == LogicJobStatus.Finished)
                    {
                        step_D_Enemy_MachineGun_02.SetOutStep(1000f);
                    }
                }
                #endregion

            step_D_Enemy_MachineGun_02_End: ;

                #endregion

                #region step_D_Enemy_MachineGun_C01

                #region 1 Start
                if (step_D_Enemy_MachineGun_C01.outStep == 1)
                {
                    step_D_Enemy_MachineGun_C01.StartIt();

                    step_D_Enemy_MachineGun_C01.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_D_Enemy_MachineGun_C01.outStep == 1.1f)
                {
                    step_D_Enemy_MachineGun_C01.RunIt();

                    if (step_D_Enemy_MachineGun_C01.IsCreatingSoldiersStoppedAndAllSoldsDead())
                    {
                        step_D_Enemy_MachineGun_C01.StartFinishing_OutStepIfNotFinishing();

                        goto step_D_Enemy_MachineGun_C01_End;
                    }
                }
                #endregion

                #region 900 Start finishing
                if (step_D_Enemy_MachineGun_C01.outStep == 900f)
                {
                    step_D_Enemy_MachineGun_C01.SetNeedsToBeFinished();
                    step_D_Enemy_MachineGun_C01.SetOutStep(901);
                }
                #endregion

                #region 901 Finishing
                if (step_D_Enemy_MachineGun_C01.outStep == 901f)
                {
                    step_D_Enemy_MachineGun_C01.RunIt();

                    if (step_D_Enemy_MachineGun_C01.status == LogicJobStatus.Finished)
                    {
                        step_D_Enemy_MachineGun_C01.SetOutStep(1000f);
                    }
                }
                #endregion

            step_D_Enemy_MachineGun_C01_End: ;

                #endregion

                #region step_D_Enemy_MachineGun_C02

                #region 1 Start
                if (step_D_Enemy_MachineGun_C02.outStep == 1)
                {
                    step_D_Enemy_MachineGun_C02.StartIt();

                    step_D_Enemy_MachineGun_C02.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_D_Enemy_MachineGun_C02.outStep == 1.1f)
                {
                    step_D_Enemy_MachineGun_C02.RunIt();

                    if (step_D_Enemy_MachineGun_C02.IsCreatingSoldiersStoppedAndAllSoldsDead())
                    {
                        step_D_Enemy_MachineGun_C02.StartFinishing_OutStepIfNotFinishing();

                        goto step_D_Enemy_MachineGun_C02_End;
                    }
                }
                #endregion

                #region 900 Start finishing
                if (step_D_Enemy_MachineGun_C02.outStep == 900f)
                {
                    step_D_Enemy_MachineGun_C02.SetNeedsToBeFinished();
                    step_D_Enemy_MachineGun_C02.SetOutStep(901);
                }
                #endregion

                #region 901 Finishing
                if (step_D_Enemy_MachineGun_C02.outStep == 901f)
                {
                    step_D_Enemy_MachineGun_C02.RunIt();

                    if (step_D_Enemy_MachineGun_C02.status == LogicJobStatus.Finished)
                    {
                        step_D_Enemy_MachineGun_C02.SetOutStep(1000f);
                    }
                }
                #endregion

            step_D_Enemy_MachineGun_C02_End: ;

                #endregion

                #region step_D_Objects_02_StopFight01

                #region 1 Start
                if (step_D_Objects_02_StopFight01.OutStep == 1) //Start
                {
                    step_D_Objects_02_StopFight01.SetEnabled(true);
                    step_D_Objects_02_StopFight01.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_D_Objects_02_StopFight01.OutStep == 1.1f) //Run
                {
                    if (step_D_Objects_02_StopFight01.IsPlayerIn())
                    {
                        step_D_PlayerEntered_StopFight01Trigger.SetStatus(LogicFlagStatus.Active);
                        step_D_Objects_02_StopFight01.StartFinishing_OutStepIfNotFishining();
                    }
                }
                #endregion

                #region 900 Finish
                if (step_D_Objects_02_StopFight01.OutStep == 900f) //Finish
                {
                    step_D_Objects_02_StopFight01.SetEnabled(false);
                    step_D_Objects_02_StopFight01.SetOutStep(1000f);
                }
                #endregion

                #endregion

                #region step_D_Objects_07_EndStepTrigger

                #region 1 Start
                if (step_D_Objects_07_CutSceneTrigger.OutStep == 1) //Start
                {
                    step_D_Objects_07_CutSceneTrigger.SetEnabled(true);
                    step_D_Objects_07_CutSceneTrigger.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_D_Objects_07_CutSceneTrigger.OutStep == 1.1f) //Run
                {
                    if (step_D_Objects_07_CutSceneTrigger.IsPlayerIn())
                    {
                        step_D_PlayerEntered_CutSceneTrigger.SetStatus(LogicFlagStatus.Active);
                        step_D_Objects_07_CutSceneTrigger.StartFinishing_OutStepIfNotFishining();
                    }
                }
                #endregion

                #region 900 Finish
                if (step_D_Objects_07_CutSceneTrigger.OutStep == 900f) //Finish
                {
                    step_D_Objects_07_CutSceneTrigger.SetEnabled(false);
                    step_D_Objects_07_CutSceneTrigger.SetOutStep(1000f);
                }
                #endregion

                #endregion

                #region step_D_Objects_08_AzPoshtTrigger

                #region 1 Start
                if (step_D_Objects_08_AzPoshtTrigger.OutStep == 1) //Start
                {
                    step_D_Objects_08_AzPoshtTrigger.SetEnabled(true);
                    step_D_Objects_08_AzPoshtTrigger.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_D_Objects_08_AzPoshtTrigger.OutStep == 1.1f) //Run
                {
                    if (step_D_Objects_08_AzPoshtTrigger.IsPlayerIn())
                    {
                        step_D_PlayerEntered_AzPoshtTrigger.SetStatus(LogicFlagStatus.Active);
                        step_D_Objects_08_AzPoshtTrigger.StartFinishing_OutStepIfNotFishining();
                    }
                }
                #endregion

                #region 900 Finish
                if (step_D_Objects_08_AzPoshtTrigger.OutStep == 900f) //Finish
                {
                    step_D_Objects_08_AzPoshtTrigger.SetEnabled(false);
                    step_D_Objects_08_AzPoshtTrigger.SetOutStep(1000f);
                }
                #endregion

                #endregion

                #region step_D_Objects_08_FailTrigger

                #region 1 Start
                if (step_D_Objects_08_FailTrigger.OutStep == 1) //Start
                {
                    step_D_Objects_08_FailTrigger.SetEnabled(true);
                    step_D_Objects_08_FailTrigger.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_D_Objects_08_FailTrigger.OutStep == 1.1f) //Run
                {
                    if (step_D_Objects_08_FailTrigger.IsPlayerIn())
                    {
                        step_D_PlayerEntered_FailTrigger.SetStatus(LogicFlagStatus.Active);
                        step_D_Objects_08_FailTrigger.StartFinishing_OutStepIfNotFishining();
                    }
                }
                #endregion

                #region 900 Finish
                if (step_D_Objects_08_FailTrigger.OutStep == 900f) //Finish
                {
                    step_D_Objects_08_FailTrigger.SetEnabled(false);
                    step_D_Objects_08_FailTrigger.SetOutStep(1000f);
                }
                #endregion

                #endregion

                #region step_D_Objects_09_AllyDoDmgTrigger

                #region 1 Start
                if (step_D_Objects_09_AllyDoDmgTrigger.OutStep == 1) //Start
                {
                    step_D_Objects_09_AllyDoDmgTrigger.SetEnabled(true);
                    step_D_Objects_09_AllyDoDmgTrigger.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_D_Objects_09_AllyDoDmgTrigger.OutStep == 1.1f) //Run
                {
                    if (step_D_Objects_09_AllyDoDmgTrigger.IsPlayerIn())
                    {
                        step_D_PlayerEntered_AllyDoDmgTrigger.SetStatus(LogicFlagStatus.Active);
                        step_D_Objects_09_AllyDoDmgTrigger.StartFinishing_OutStepIfNotFishining();
                    }
                }
                #endregion

                #region 900 Finish
                if (step_D_Objects_09_AllyDoDmgTrigger.OutStep == 900f) //Finish
                {
                    step_D_Objects_09_AllyDoDmgTrigger.SetEnabled(false);
                    step_D_Objects_09_AllyDoDmgTrigger.SetOutStep(1000f);
                }
                #endregion

                #endregion

                #region step_D_Objects_11_EndOfStepDTrigger

                #region 1 Start
                if (step_D_Objects_11_EndOfStepDTrigger.OutStep == 1) //Start
                {
                    step_D_Objects_11_EndOfStepDTrigger.SetEnabled(true);
                    step_D_Objects_11_EndOfStepDTrigger.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_D_Objects_11_EndOfStepDTrigger.OutStep == 1.1f) //Run
                {
                    if (step_D_Objects_11_EndOfStepDTrigger.IsPlayerIn())
                    {
                        step_D_PlayerEntered_EndOfStepDTrigger.SetStatus(LogicFlagStatus.Active);
                        step_D_Objects_11_EndOfStepDTrigger.StartFinishing_OutStepIfNotFishining();
                    }
                }
                #endregion

                #region 900 Finish
                if (step_D_Objects_11_EndOfStepDTrigger.OutStep == 900f) //Finish
                {
                    step_D_Objects_11_EndOfStepDTrigger.SetEnabled(false);
                    step_D_Objects_11_EndOfStepDTrigger.SetOutStep(1000f);
                }
                #endregion

                #endregion

                #endregion
            }
            else
            {
                #region E

                #region step_E_Ally_chainJobGroup_01_Ally01

                #region 1 Start
                if (step_E_Ally_chainJobGroup_01_Ally01.outStep == 1)
                {
                    step_E_Ally_chainJobGroup_01_Ally01.Init_SetNewGlobalLogicIndex(step_E_Ally01ChainJobsGlobalLogicIndex);
                    step_E_Ally_chainJobGroup_01_Ally01.StartIt();

                    step_E_Ally_chainJobGroup_01_Ally01.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_E_Ally_chainJobGroup_01_Ally01.outStep == 1.1f)
                {
                    step_E_Ally_chainJobGroup_01_Ally01.RunIt();
                }
                #endregion

                #region 900 Start finishing
                if (step_E_Ally_chainJobGroup_01_Ally01.outStep == 900f)
                {
                    step_E_Ally_chainJobGroup_01_Ally01.SetNeedsToBeFinished();

                    step_E_Ally_chainJobGroup_01_Ally01.SetOutStep(901f);
                }
                #endregion

                #region 901 Finishing
                if (step_E_Ally_chainJobGroup_01_Ally01.outStep == 901f)
                {
                    step_E_Ally_chainJobGroup_01_Ally01.RunIt();

                    if (step_E_Ally_chainJobGroup_01_Ally01.status == LogicJobStatus.Finished)
                    {
                        step_E_ChainJobAlly01_Finished.SetStatus(LogicFlagStatus.Active);

                        step_E_Ally_chainJobGroup_01_Ally01.SetOutStep(1000);
                    }
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
                        step_E_Fight01_Finished.SetStatus(LogicFlagStatus.Active);
                    }
                }
                #endregion

            step_E_Enemy_FightInRegsGroup_01_End: ;

                #endregion

                #region step_E_Enemy_FightInRegsGroup_02

                #region 1 Start
                if (step_E_Enemy_FightInRegsGroup_02.outStep == 1f)
                {
                    step_E_Enemy_FightInRegsGroup_02.StartIt();
                    step_E_Enemy_FightInRegsGroup_02.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_E_Enemy_FightInRegsGroup_02.outStep == 1.1f)
                {
                    step_E_Enemy_FightInRegsGroup_02.RunIt();

                    if (step_E_Enemy_FightInRegsGroup_02.IsCreatingSoldiersStoppedAndAllSoldsDead())
                    {
                        step_E_Enemy_FightInRegsGroup_02.StartFinishing_OutStepIfNotFinishing();
                        goto step_E_Enemy_FightInRegsGroup_02_End;
                    }
                }
                #endregion

                #region 900 Start Finishing
                if (step_E_Enemy_FightInRegsGroup_02.outStep == 900f)
                {
                    step_E_Enemy_FightInRegsGroup_02.SetNeedsToBeFinished();
                    step_E_Enemy_FightInRegsGroup_02.SetOutStep(901f);
                }
                #endregion

                #region 901 Finishing
                if (step_E_Enemy_FightInRegsGroup_02.outStep == 901f)
                {
                    step_E_Enemy_FightInRegsGroup_02.RunIt();

                    if (step_E_Enemy_FightInRegsGroup_02.status == LogicJobStatus.Finished)
                    {
                        step_E_Enemy_FightInRegsGroup_02.SetOutStep(1000f);
                        step_E_Fight02_Finished.SetStatus(LogicFlagStatus.Active);
                    }
                }
                #endregion

            step_E_Enemy_FightInRegsGroup_02_End: ;

                #endregion

                #region step_E_Enemy_FightInRegsGroup_03

                #region 1 Start
                if (step_E_Enemy_FightInRegsGroup_03.outStep == 1f)
                {
                    step_E_Enemy_FightInRegsGroup_03.StartIt();
                    step_E_Enemy_FightInRegsGroup_03.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_E_Enemy_FightInRegsGroup_03.outStep == 1.1f)
                {
                    step_E_Enemy_FightInRegsGroup_03.RunIt();

                    if (step_E_Enemy_FightInRegsGroup_03.IsCreatingSoldiersStoppedAndAllSoldsDead())
                    {
                        step_E_Enemy_FightInRegsGroup_03.StartFinishing_OutStepIfNotFinishing();
                        goto step_E_Enemy_FightInRegsGroup_03_End;
                    }
                }
                #endregion

                #region 900 Start Finishing
                if (step_E_Enemy_FightInRegsGroup_03.outStep == 900f)
                {
                    step_E_Enemy_FightInRegsGroup_03.SetNeedsToBeFinished();
                    step_E_Enemy_FightInRegsGroup_03.SetOutStep(901f);
                }
                #endregion

                #region 901 Finishing
                if (step_E_Enemy_FightInRegsGroup_03.outStep == 901f)
                {
                    step_E_Enemy_FightInRegsGroup_03.RunIt();

                    if (step_E_Enemy_FightInRegsGroup_03.status == LogicJobStatus.Finished)
                    {
                        step_E_Enemy_FightInRegsGroup_03.SetOutStep(1000f);
                        step_E_Fight03_Finished.SetStatus(LogicFlagStatus.Active);
                    }
                }
                #endregion

            step_E_Enemy_FightInRegsGroup_03_End: ;

                #endregion

                #region step_E_Objects_01_StartStepTrigger

                #region 1 Start
                if (step_E_Objects_01_StartStepTrigger.OutStep == 1) //Start
                {
                    step_E_Objects_01_StartStepTrigger.SetEnabled(true);
                    step_E_Objects_01_StartStepTrigger.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_E_Objects_01_StartStepTrigger.OutStep == 1.1f) //Run
                {
                    if (step_E_Objects_01_StartStepTrigger.IsPlayerIn())
                    {
                        step_E_PlayerEntered_StartStepTrigger.SetStatus(LogicFlagStatus.Active);
                        step_E_Objects_01_StartStepTrigger.StartFinishing_OutStepIfNotFishining();
                    }
                }
                #endregion

                #region 900 Finish
                if (step_E_Objects_01_StartStepTrigger.OutStep == 900f) //Finish
                {
                    step_E_Objects_01_StartStepTrigger.SetEnabled(false);
                    step_E_Objects_01_StartStepTrigger.SetOutStep(1000f);
                }
                #endregion

                #endregion

                #region step_E_Objects_02_StartFight02Trigger

                #region 1 Start
                if (step_E_Objects_02_StartFight02Trigger.OutStep == 1) //Start
                {
                    step_E_Objects_02_StartFight02Trigger.SetEnabled(true);
                    step_E_Objects_02_StartFight02Trigger.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_E_Objects_02_StartFight02Trigger.OutStep == 1.1f) //Run
                {
                    if (step_E_Objects_02_StartFight02Trigger.IsPlayerIn())
                    {
                        step_E_PlayerEntered_StartFight02Trigger.SetStatus(LogicFlagStatus.Active);
                        step_E_Objects_02_StartFight02Trigger.StartFinishing_OutStepIfNotFishining();
                    }
                }
                #endregion

                #region 900 Finish
                if (step_E_Objects_02_StartFight02Trigger.OutStep == 900f) //Finish
                {
                    step_E_Objects_02_StartFight02Trigger.SetEnabled(false);
                    step_E_Objects_02_StartFight02Trigger.SetOutStep(1000f);
                }
                #endregion

                #endregion

                #region step_E_Objects_04_AllyChangePosTrigger

                #region 1 Start
                if (step_E_Objects_04_AllyChangePosTrigger.OutStep == 1) //Start
                {
                    step_E_Objects_04_AllyChangePosTrigger.SetEnabled(true);
                    step_E_Objects_04_AllyChangePosTrigger.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_E_Objects_04_AllyChangePosTrigger.OutStep == 1.1f) //Run
                {
                    if (step_E_Objects_04_AllyChangePosTrigger.IsPlayerIn())
                    {
                        step_E_PlayerEntered_AllyChangePosTrigger.SetStatus(LogicFlagStatus.Active);
                        step_E_Objects_04_AllyChangePosTrigger.StartFinishing_OutStepIfNotFishining();
                    }
                }
                #endregion

                #region 900 Finish
                if (step_E_Objects_04_AllyChangePosTrigger.OutStep == 900f) //Finish
                {
                    step_E_Objects_04_AllyChangePosTrigger.SetEnabled(false);
                    step_E_Objects_04_AllyChangePosTrigger.SetOutStep(1000f);
                }
                #endregion

                #endregion

                #region step_E_Objects_06_StartFight04

                #region 1 Start
                if (step_E_Objects_06_StartFight04Trigger.OutStep == 1) //Start
                {
                    step_E_Objects_06_StartFight04Trigger.SetEnabled(true);
                    step_E_Objects_06_StartFight04Trigger.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_E_Objects_06_StartFight04Trigger.OutStep == 1.1f) //Run
                {
                    if (step_E_Objects_06_StartFight04Trigger.IsPlayerIn())
                    {
                        step_E_PlayerEntered_StartFight04Trigger.SetStatus(LogicFlagStatus.Active);
                        step_E_Objects_06_StartFight04Trigger.StartFinishing_OutStepIfNotFishining();
                    }
                }
                #endregion

                #region 900 Finish
                if (step_E_Objects_06_StartFight04Trigger.OutStep == 900f) //Finish
                {
                    step_E_Objects_06_StartFight04Trigger.SetEnabled(false);
                    step_E_Objects_06_StartFight04Trigger.SetOutStep(1000f);
                }
                #endregion

                #endregion

                #region step_E_Enemy_FightInRegsGroup_04

                #region 1 Start
                if (step_E_Enemy_FightInRegsGroup_04.outStep == 1f)
                {
                    step_E_Enemy_FightInRegsGroup_04.StartIt();
                    step_E_Enemy_FightInRegsGroup_04.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_E_Enemy_FightInRegsGroup_04.outStep == 1.1f)
                {
                    step_E_Enemy_FightInRegsGroup_04.RunIt();

                    if (step_E_Enemy_FightInRegsGroup_04.IsCreatingSoldiersStoppedAndAllSoldsDead())
                    {
                        step_E_Enemy_FightInRegsGroup_04.StartFinishing_OutStepIfNotFinishing();
                        goto step_E_Enemy_FightInRegsGroup_04_End;
                    }
                }
                #endregion

                #region 900 Start Finishing
                if (step_E_Enemy_FightInRegsGroup_04.outStep == 900f)
                {
                    step_E_Enemy_FightInRegsGroup_04.SetNeedsToBeFinished();
                    step_E_Enemy_FightInRegsGroup_04.SetOutStep(901f);
                }
                #endregion

                #region 901 Finishing
                if (step_E_Enemy_FightInRegsGroup_04.outStep == 901f)
                {
                    step_E_Enemy_FightInRegsGroup_04.RunIt();

                    if (step_E_Enemy_FightInRegsGroup_04.status == LogicJobStatus.Finished)
                    {
                        step_E_Enemy_FightInRegsGroup_04.SetOutStep(1000f);
                        step_E_Fight04_Finished.SetStatus(LogicFlagStatus.Active);
                    }
                }
                #endregion

            step_E_Enemy_FightInRegsGroup_04_End: ;

                #endregion

                #region step_E_Objects_07_StartFight05Trigger

                #region 1 Start
                if (step_E_Objects_07_StartFight05Trigger.OutStep == 1) //Start
                {
                    step_E_Objects_07_StartFight05Trigger.SetEnabled(true);
                    step_E_Objects_07_StartFight05Trigger.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_E_Objects_07_StartFight05Trigger.OutStep == 1.1f) //Run
                {
                    if (step_E_Objects_07_StartFight05Trigger.IsPlayerIn())
                    {
                        step_E_PlayerEntered_StartFight05Trigger.SetStatus(LogicFlagStatus.Active);
                        step_E_Objects_07_StartFight05Trigger.StartFinishing_OutStepIfNotFishining();
                    }
                }
                #endregion

                #region 900 Finish
                if (step_E_Objects_07_StartFight05Trigger.OutStep == 900f) //Finish
                {
                    step_E_Objects_07_StartFight05Trigger.SetEnabled(false);
                    step_E_Objects_07_StartFight05Trigger.SetOutStep(1000f);
                }
                #endregion

                #endregion

                #region step_E_Enemy_FightInRegsGroup_05

                #region 1 Start
                if (step_E_Enemy_FightInRegsGroup_05.outStep == 1f)
                {
                    step_E_Enemy_FightInRegsGroup_05.StartIt();
                    step_E_Enemy_FightInRegsGroup_05.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_E_Enemy_FightInRegsGroup_05.outStep == 1.1f)
                {
                    step_E_Enemy_FightInRegsGroup_05.RunIt();

                    if (step_E_Enemy_FightInRegsGroup_05.IsCreatingSoldiersStoppedAndAllSoldsDead())
                    {
                        step_E_Enemy_FightInRegsGroup_05.StartFinishing_OutStepIfNotFinishing();
                        goto step_E_Enemy_FightInRegsGroup_05_End;
                    }
                }
                #endregion

                #region 900 Start Finishing
                if (step_E_Enemy_FightInRegsGroup_05.outStep == 900f)
                {
                    step_E_Enemy_FightInRegsGroup_05.SetNeedsToBeFinished();
                    step_E_Enemy_FightInRegsGroup_05.SetOutStep(901f);
                }
                #endregion

                #region 901 Finishing
                if (step_E_Enemy_FightInRegsGroup_05.outStep == 901f)
                {
                    step_E_Enemy_FightInRegsGroup_05.RunIt();

                    if (step_E_Enemy_FightInRegsGroup_05.status == LogicJobStatus.Finished)
                    {
                        step_E_Enemy_FightInRegsGroup_05.SetOutStep(1000f);
                        step_E_Fight05_Finished.SetStatus(LogicFlagStatus.Active);
                    }
                }
                #endregion

            step_E_Enemy_FightInRegsGroup_05_End: ;

                #endregion

                #region step_E_Objects_10_AllyChangePos02Trigger

                #region 1 Start
                if (step_E_Objects_10_AllyChangePos02Trigger.OutStep == 1) //Start
                {
                    step_E_Objects_10_AllyChangePos02Trigger.SetEnabled(true);
                    step_E_Objects_10_AllyChangePos02Trigger.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_E_Objects_10_AllyChangePos02Trigger.OutStep == 1.1f) //Run
                {
                    if (step_E_Objects_10_AllyChangePos02Trigger.IsPlayerIn())
                    {
                        step_E_PlayerEntered_AllyChangePos2Trigger.SetStatus(LogicFlagStatus.Active);
                        step_E_Objects_10_AllyChangePos02Trigger.StartFinishing_OutStepIfNotFishining();
                    }
                }
                #endregion

                #region 900 Finish
                if (step_E_Objects_10_AllyChangePos02Trigger.OutStep == 900f) //Finish
                {
                    step_E_Objects_10_AllyChangePos02Trigger.SetEnabled(false);
                    step_E_Objects_10_AllyChangePos02Trigger.SetOutStep(1000f);
                }
                #endregion

                #endregion

                #region step_E_Objects_12_AllyChangePos03Trigger

                #region 1 Start
                if (step_E_Objects_12_AllyChangePos03Trigger.OutStep == 1) //Start
                {
                    step_E_Objects_12_AllyChangePos03Trigger.SetEnabled(true);
                    step_E_Objects_12_AllyChangePos03Trigger.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_E_Objects_12_AllyChangePos03Trigger.OutStep == 1.1f) //Run
                {
                    if (step_E_Objects_12_AllyChangePos03Trigger.IsPlayerIn())
                    {
                        step_E_PlayerEntered_AllyChangePos3Trigger.SetStatus(LogicFlagStatus.Active);
                        step_E_Objects_12_AllyChangePos03Trigger.StartFinishing_OutStepIfNotFishining();
                    }
                }
                #endregion

                #region 900 Finish
                if (step_E_Objects_12_AllyChangePos03Trigger.OutStep == 900f) //Finish
                {
                    step_E_Objects_12_AllyChangePos03Trigger.SetEnabled(false);
                    step_E_Objects_12_AllyChangePos03Trigger.SetOutStep(1000f);
                }
                #endregion

                #endregion

                #region step_E_Objects_14_StartFight06Trigger

                #region 1 Start
                if (step_E_Objects_14_StartFight06Trigger.OutStep == 1) //Start
                {
                    step_E_Objects_14_StartFight06Trigger.SetEnabled(true);
                    step_E_Objects_14_StartFight06Trigger.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_E_Objects_14_StartFight06Trigger.OutStep == 1.1f) //Run
                {
                    if (step_E_Objects_14_StartFight06Trigger.IsPlayerIn())
                    {
                        step_E_PlayerEntered_StartFight06Trigger.SetStatus(LogicFlagStatus.Active);
                        step_E_Objects_14_StartFight06Trigger.StartFinishing_OutStepIfNotFishining();
                    }
                }
                #endregion

                #region 900 Finish
                if (step_E_Objects_14_StartFight06Trigger.OutStep == 900f) //Finish
                {
                    step_E_Objects_14_StartFight06Trigger.SetEnabled(false);
                    step_E_Objects_14_StartFight06Trigger.SetOutStep(1000f);
                }
                #endregion

                #endregion

                #region step_E_Enemy_FightInRegsGroup_06

                #region 1 Start
                if (step_E_Enemy_FightInRegsGroup_06.outStep == 1f)
                {
                    step_E_Enemy_FightInRegsGroup_06.StartIt();
                    step_E_Enemy_FightInRegsGroup_06.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_E_Enemy_FightInRegsGroup_06.outStep == 1.1f)
                {
                    step_E_Enemy_FightInRegsGroup_06.RunIt();

                    if (step_E_Enemy_FightInRegsGroup_06.IsCreatingSoldiersStoppedAndAllSoldsDead())
                    {
                        step_E_Enemy_FightInRegsGroup_06.StartFinishing_OutStepIfNotFinishing();
                        goto step_E_Enemy_FightInRegsGroup_06_End;
                    }
                }
                #endregion

                #region 900 Start Finishing
                if (step_E_Enemy_FightInRegsGroup_06.outStep == 900f)
                {
                    step_E_Enemy_FightInRegsGroup_06.SetNeedsToBeFinished();
                    step_E_Enemy_FightInRegsGroup_06.SetOutStep(901f);
                }
                #endregion

                #region 901 Finishing
                if (step_E_Enemy_FightInRegsGroup_06.outStep == 901f)
                {
                    step_E_Enemy_FightInRegsGroup_06.RunIt();

                    if (step_E_Enemy_FightInRegsGroup_06.status == LogicJobStatus.Finished)
                    {
                        step_E_Enemy_FightInRegsGroup_06.SetOutStep(1000f);
                        step_E_Fight06_Finished.SetStatus(LogicFlagStatus.Active);
                    }
                }
                #endregion

            step_E_Enemy_FightInRegsGroup_06_End: ;

                #endregion

                #region step_E_Objects_16_AllyChangePos04Trigger

                #region 1 Start
                if (step_E_Objects_16_AllyChangePos04Trigger.OutStep == 1) //Start
                {
                    step_E_Objects_16_AllyChangePos04Trigger.SetEnabled(true);
                    step_E_Objects_16_AllyChangePos04Trigger.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_E_Objects_16_AllyChangePos04Trigger.OutStep == 1.1f) //Run
                {
                    if (step_E_Objects_16_AllyChangePos04Trigger.IsPlayerIn())
                    {
                        step_E_PlayerEntered_AllyChangePos4Trigger.SetStatus(LogicFlagStatus.Active);
                        step_E_Objects_16_AllyChangePos04Trigger.StartFinishing_OutStepIfNotFishining();
                    }
                }
                #endregion

                #region 900 Finish
                if (step_E_Objects_16_AllyChangePos04Trigger.OutStep == 900f) //Finish
                {
                    step_E_Objects_16_AllyChangePos04Trigger.SetEnabled(false);
                    step_E_Objects_16_AllyChangePos04Trigger.SetOutStep(1000f);
                }
                #endregion

                #endregion

                #region step_E_Objects_17_EndTrigger

                #region 1 Start
                if (step_E_Objects_17_EndTrigger.OutStep == 1) //Start
                {
                    step_E_Objects_17_EndTrigger.SetEnabled(true);
                    step_E_Objects_17_EndTrigger.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_E_Objects_17_EndTrigger.OutStep == 1.1f) //Run
                {
                    if (step_E_Objects_17_EndTrigger.IsPlayerIn())
                    {
                        step_E_PlayerEntered_EndTrigger.SetStatus(LogicFlagStatus.Active);
                        step_E_Objects_17_EndTrigger.StartFinishing_OutStepIfNotFishining();
                    }
                }
                #endregion

                #region 900 Finish
                if (step_E_Objects_17_EndTrigger.OutStep == 900f) //Finish
                {
                    step_E_Objects_17_EndTrigger.SetEnabled(false);
                    step_E_Objects_17_EndTrigger.SetOutStep(1000f);
                }
                #endregion

                #endregion

                #endregion

                #region F

                #region step_F_Ally_chainJobGroup_01_Ally01

                #region 1 Start
                if (step_F_Ally_chainJobGroup_01_Ally01.outStep == 1)
                {
                    step_F_Ally_chainJobGroup_01_Ally01.Init_SetNewGlobalLogicIndex(step_F_Ally01ChainJobsGlobalLogicIndex);
                    step_F_Ally_chainJobGroup_01_Ally01.StartIt();

                    step_F_Ally_chainJobGroup_01_Ally01.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_F_Ally_chainJobGroup_01_Ally01.outStep == 1.1f)
                {
                    step_F_Ally_chainJobGroup_01_Ally01.RunIt();
                }
                #endregion

                #region 900 Start finishing
                if (step_F_Ally_chainJobGroup_01_Ally01.outStep == 900f)
                {
                    step_F_Ally_chainJobGroup_01_Ally01.SetNeedsToBeFinished();

                    step_F_Ally_chainJobGroup_01_Ally01.SetOutStep(901f);
                }
                #endregion

                #region 901 Finishing
                if (step_F_Ally_chainJobGroup_01_Ally01.outStep == 901f)
                {
                    step_F_Ally_chainJobGroup_01_Ally01.RunIt();

                    if (step_F_Ally_chainJobGroup_01_Ally01.status == LogicJobStatus.Finished)
                    {
                        step_F_Ally_chainJobGroup_01_Ally01.SetOutStep(1000);
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
                        step_F_Enemy_FightInRegsGroup_01.SetOutStep(1000f);
                        step_F_Fight01_Finished.SetStatus(LogicFlagStatus.Active);
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
                        step_F_Enemy_FightInRegsGroup_02.SetOutStep(1000f);
                        step_F_Fight02_Finished.SetStatus(LogicFlagStatus.Active);
                    }
                }
                #endregion

            step_F_Enemy_FightInRegsGroup_02_End: ;

                #endregion

                #region step_F_Objects_01_StartStepTrigger

                #region 1 Start
                if (step_F_Objects_01_StartStepTrigger.OutStep == 1) //Start
                {
                    step_F_Objects_01_StartStepTrigger.SetEnabled(true);
                    step_F_Objects_01_StartStepTrigger.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_F_Objects_01_StartStepTrigger.OutStep == 1.1f) //Run
                {
                    if (step_F_Objects_01_StartStepTrigger.IsPlayerIn())
                    {
                        step_F_PlayerEntered_StartStepTrigger.SetStatus(LogicFlagStatus.Active);
                        step_F_Objects_01_StartStepTrigger.StartFinishing_OutStepIfNotFishining();
                    }
                }
                #endregion

                #region 900 Finish
                if (step_F_Objects_01_StartStepTrigger.OutStep == 900f) //Finish
                {
                    step_F_Objects_01_StartStepTrigger.SetEnabled(false);
                    step_F_Objects_01_StartStepTrigger.SetOutStep(1000f);
                }
                #endregion

                #endregion

                #region step_F_Ally_chainJobGroup_02_Dustashun

                #region 1 Start
                if (step_F_Ally_chainJobGroup_02_Dustashun.outStep == 1)
                {
                    step_F_Ally_chainJobGroup_02_Dustashun.Init_SetNewGlobalLogicIndex(step_F_DustashunChainJobsGlobalLogicIndex);
                    step_F_Ally_chainJobGroup_02_Dustashun.StartIt();

                    step_F_Ally_chainJobGroup_02_Dustashun.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_F_Ally_chainJobGroup_02_Dustashun.outStep == 1.1f)
                {
                    step_F_Ally_chainJobGroup_02_Dustashun.RunIt();
                }
                #endregion

                #region 900 Start finishing
                if (step_F_Ally_chainJobGroup_02_Dustashun.outStep == 900f)
                {
                    step_F_Ally_chainJobGroup_02_Dustashun.SetNeedsToBeFinished();

                    step_F_Ally_chainJobGroup_02_Dustashun.SetOutStep(901f);
                }
                #endregion

                #region 901 Finishing
                if (step_F_Ally_chainJobGroup_02_Dustashun.outStep == 901f)
                {
                    step_F_Ally_chainJobGroup_02_Dustashun.RunIt();

                    if (step_F_Ally_chainJobGroup_02_Dustashun.status == LogicJobStatus.Finished)
                    {
                        step_F_Ally_chainJobGroup_02_Dustashun.SetOutStep(1000);
                    }
                }
                #endregion

                #endregion

                #region step_F_Objects_02_StartFight01Trigger

                #region 1 Start
                if (step_F_Objects_02_StartFight01Trigger.OutStep == 1) //Start
                {
                    step_F_Objects_02_StartFight01Trigger.SetEnabled(true);
                    step_F_Objects_02_StartFight01Trigger.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_F_Objects_02_StartFight01Trigger.OutStep == 1.1f) //Run
                {
                    if (step_F_Objects_02_StartFight01Trigger.IsPlayerIn())
                    {
                        step_F_PlayerEntered_StartFight01Trigger.SetStatus(LogicFlagStatus.Active);
                        step_F_Objects_02_StartFight01Trigger.StartFinishing_OutStepIfNotFishining();
                    }
                }
                #endregion

                #region 900 Finish
                if (step_F_Objects_02_StartFight01Trigger.OutStep == 900f) //Finish
                {
                    step_F_Objects_02_StartFight01Trigger.SetEnabled(false);
                    step_F_Objects_02_StartFight01Trigger.SetOutStep(1000f);
                }
                #endregion

                #endregion

                #region step_F_Objects_03_ExitFight01Trigger

                #region 1 Start
                if (step_F_Objects_03_ExitFight01Trigger.OutStep == 1) //Start
                {
                    step_F_Objects_03_ExitFight01Trigger.SetEnabled(true);
                    step_F_Objects_03_ExitFight01Trigger.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_F_Objects_03_ExitFight01Trigger.OutStep == 1.1f) //Run
                {
                    if (step_F_Objects_03_ExitFight01Trigger.IsPlayerIn())
                    {
                        step_F_PlayerEntered_ExitFight01Trigger.SetStatus(LogicFlagStatus.Active);
                        step_F_Objects_03_ExitFight01Trigger.StartFinishing_OutStepIfNotFishining();
                    }
                }
                #endregion

                #region 900 Finish
                if (step_F_Objects_03_ExitFight01Trigger.OutStep == 900f) //Finish
                {
                    step_F_Objects_03_ExitFight01Trigger.SetEnabled(false);
                    step_F_Objects_03_ExitFight01Trigger.SetOutStep(1000f);
                }
                #endregion

                #endregion

                #region step_F_Objects_06_StartFight02Trigger

                #region 1 Start
                if (step_F_Objects_06_StartFight02Trigger.OutStep == 1) //Start
                {
                    step_F_Objects_06_StartFight02Trigger.SetEnabled(true);
                    step_F_Objects_06_StartFight02Trigger.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_F_Objects_06_StartFight02Trigger.OutStep == 1.1f) //Run
                {
                    if (step_F_Objects_06_StartFight02Trigger.IsPlayerIn())
                    {
                        step_F_PlayerEntered_StartFight02Trigger.SetStatus(LogicFlagStatus.Active);
                        step_F_Objects_06_StartFight02Trigger.StartFinishing_OutStepIfNotFishining();
                    }
                }
                #endregion

                #region 900 Finish
                if (step_F_Objects_06_StartFight02Trigger.OutStep == 900f) //Finish
                {
                    step_F_Objects_06_StartFight02Trigger.SetEnabled(false);
                    step_F_Objects_06_StartFight02Trigger.SetOutStep(1000f);
                }
                #endregion

                #endregion

                #region step_F_Objects_07_PlayerInFight02Trigger

                #region 1 Start
                if (step_F_Objects_07_PlayerInFight02Trigger.OutStep == 1) //Start
                {
                    step_F_Objects_07_PlayerInFight02Trigger.SetEnabled(true);
                    step_F_Objects_07_PlayerInFight02Trigger.SetOutStep(1.1f);
                }
                #endregion

                #region 1.1 Run
                if (step_F_Objects_07_PlayerInFight02Trigger.OutStep == 1.1f) //Run
                {
                    if (step_F_Objects_07_PlayerInFight02Trigger.IsPlayerIn())
                    {
                        step_F_PlayerInFight02Trigger.SetStatus(LogicFlagStatus.Active);
                    }
                    else
                    {
                        step_F_PlayerInFight02Trigger.SetDeactive();
                    }
                }
                #endregion

                #region 900 Finish
                if (step_F_Objects_07_PlayerInFight02Trigger.OutStep == 900f) //Finish
                {
                    step_F_Objects_07_PlayerInFight02Trigger.SetEnabled(false);
                    step_F_Objects_07_PlayerInFight02Trigger.SetOutStep(1000f);
                }
                #endregion

                #endregion

                #endregion
            }
        }
    }

    public override void LoadCheckPoint(float _levelStep)
    {
        base.LoadCheckPoint(_levelStep);

        #region CheckPoint B
        if (levelStep == 2f)
        {
            step_B_Objects_StartPoint_Player.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);
            step_B_Objects_StartPoint_Khaloo.PlaceCharacterOnIt(Khaloo);

            return;
        }
        #endregion

        #region CheckPoint C
        if (levelStep == 3f)
        {
            step_C_Objects_StartPoint_Player.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);
            step_C_Objects_StartPoint_Khaloo.PlaceCharacterOnIt(Khaloo);

            step_C_Objects_StartPoint_Ally01.PlaceCharacterOnIt(Ally01);
            step_C_Objects_StartPoint_Ally02.PlaceCharacterOnIt(Ally02);
            return;
        }
        #endregion

        #region CheckPoint D
        if (levelStep == 4f)
        {
            step_D_Objects_StartPoint_Player.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);
            step_D_Objects_StartPoint_Khaloo.PlaceCharacterOnIt(Khaloo);

            step_D_Objects_StartPoint_Ally01.PlaceCharacterOnIt(Ally01);
            step_D_Objects_StartPoint_Ally02.PlaceCharacterOnIt(Ally02);
            return;
        }
        #endregion

        #region CheckPoint D2
        if (levelStep == 9f)
        {
            step_D_IsLoadedToStepD2 = true;

            foreach (GameObject gObj in step_D_Objects_06_HedgesToDestroy)
            {
                GameObject.Destroy(gObj);
            }

            foreach (GameObject gObj in step_D_Objects_03_SignallingBoshkes)
            {
                GameObject.Destroy(gObj);
            }

            return;
        }
        #endregion

        #region CheckPoint E
        if (levelStep == 5f)
        {
            step_E_Objects_StartPoint_Player.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);

            step_E_Objects_StartPoint_Ally01.PlaceCharacterOnIt(Ally01);
            return;
        }
        #endregion

        #region CheckPoint E Part2
        if (levelStep == 6f)
        {
            step_EP2_Objects_StartPoint_Player.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);

            step_EP2_Objects_StartPoint_Ally01.PlaceCharacterOnIt(Ally01);

            loadFromSaveGame = true;

            return;
        }
        #endregion

        #region CheckPoint F
        if (levelStep == 7f)
        {
            step_F_Objects_StartPoint_Player.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);

            step_F_Objects_StartPoint_Ally01.PlaceCharacterOnIt(Ally01);

            step_F_IsLoaded = true;

            return;
        }
        #endregion
    }

    public void Initialize()
    {
        ally01GunInfo = Ally01.GetComponent<SoldierGun>() as SoldierGun;
        ally02GunInfo = Ally02.GetComponent<SoldierGun>() as SoldierGun;
        ally03GunInfo = Ally03.GetComponent<SoldierGun>() as SoldierGun;
        ally04GunInfo = Ally04.GetComponent<SoldierGun>() as SoldierGun;
        khalooGunInfo = Khaloo.GetComponent<SoldierGun>() as SoldierGun;
    }

    void AllyDoCriticalShoot(bool _isCritical, SoldierGun _gunInfo)
    {
        bool isCritic = _isCritical;
        SoldierGun gunInfo = _gunInfo;

        gunInfo.DoCriticalDamage(isCritic);
    }

    void ChangeObjectTransform(GameObject _object, Transform _targetTransform)
    {
        GameObject obj = _object;
        Transform target = _targetTransform;

        obj.transform.position = target.position;
        obj.transform.rotation = target.rotation;
    }

    void SetComponentActivity(GameObject _object, string _component, bool _isAactive)
    {
        GameObject gObj = _object;
        string comp = _component;
        bool isActive = _isAactive;

        gObj.GetComponent(comp).active = isActive;
    }

    void SetComponentActivity(GameObject _object, bool _isAactive)
    {
        GameObject gObj = _object;
        bool isActive = _isAactive;

        string componentName = gObj.GetComponent<DynamicObject>().GetType().ToString();

        SetComponentActivity(gObj, componentName, isActive);
    }

    void SetBoshkesReadyToExplode(GameObject[] _boshkes, LogicFlag _flag)
    {
        GameObject[] boshkes = _boshkes;
        LogicFlag flag = _flag;

        boshkes[0].GetComponent<DynamicObject>().flag_ObjectDestroyed = flag;

        foreach (GameObject gObj in boshkes)
        {
            gObj.GetComponent<DynamicObject>().SetInvulnerable(false);
        }
    }

    void ExplodeTheWall()
    {
        step_D_Objects_04_Particle01.Play();
        step_D_Objects_05_Particle02.Play();

        foreach (GameObject gObj in step_D_Objects_06_HedgesToDestroy)
        {
            GameObject.Destroy(gObj);
        }

        step_D_ExplosionChecked = true;

        step_D_Objects_07_CutSceneTrigger.StartOutStepIfNotStarted();

        mapLogic.HUD_ObjectivesPage_SetActiveObjective(6);
        mapLogic.HUD_ShowNewMission(6);
    }

    void SetCharactersInTempList(List<GameObject> _list)
    {
        tempListForCharacters.Clear();

        foreach (GameObject gObj in _list)
        {
            if (gObj != null)
            {
                tempListForCharacters.Add(gObj);
            }
        }
    }

    void KillAllCharactersInTempList()
    {
        foreach (GameObject gObj in tempListForCharacters)
        {
            if (gObj.tag != "Player")
            {
                mapLogic.RemoveChar(gObj);

                Destroy(gObj);
            }
        }
    }

    bool IsMachineGunsSoldiersDead()
    {
        if (step_D_Enemy_MachineGun_C01.controlledSoldier != null)
        {
            if (step_D_Enemy_MachineGun_C01.controlledSoldier.GetComponent<CharacterInfo>().IsDead)
                return true;
        }

        if (step_D_Enemy_MachineGun_C02.controlledSoldier != null)
        {
            if (step_D_Enemy_MachineGun_C02.controlledSoldier.GetComponent<CharacterInfo>().IsDead)
                return true;
        }

        return false;
    }

    void StartPart2()
    {
        step_E_Objects_StartPoint_Player.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);

        step_E_Objects_StartPoint_Ally01.PlaceCharacterOnIt(Ally01);

        SetLevelStep(5f);
    }


    void PlayFirstMusic()
    {
        MusicController.Instance.PlayMusic(MusicSong.Action_C, 0);
    }

    void PlaySecondMusic()
    {
        MusicController.Instance.PlayMusic(MusicSong.Action_C, 0);
    }
}