using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level01_FlashBack_Logic : LevelLogic
{
    public CutsceneController RunCutscene;
    public GameObject PejvakLogoController;

    public GameObject doorLeft;
    public GameObject doorRight;

    public List<GameObject> objectsToDisableAfterCutscene;
    public List<GameObject> objectsToEnableAfterCutscene;

    public CutsceneController firstCutscene;

    public CutsceneController secondCutscene;

    public GameObject khaloo;

    public LogicVoiceCollection logicVoiceCollection_Khaloo;

    #region Step_A Variables
    //Enemy
    public MapLogicJob_FightInRegsGroup step_A_Enemy_FightInRegsGroup_01;
    public MapLogicJob_FightInRegsGroup step_A_Enemy_FightInRegsGroup_02;
    public MapLogicJob_FightInRegsGroup step_A_Enemy_FightInRegsGroup_03;

    //Ally
    public MapLogicJob_ChainJobsGroup step_A_Ally_chainJobGroup_01_Khaloo;

    //Objects
    public LogicTrigger step_A_Objects_01_PanjerehTrigger;

    public GameObject step_A_Objects_02_Panjereh3DObjTr;

    public StartPoint step_A_Objects_StartPoint_Player;
    public StartPoint step_A_Objects_StartPoint_Khaloo;

    public float step_A_MaxTimeForFight = 10f;

    public int step_A_minEnemiesToCreate = 10;

    LogicFlag step_A_PlayerEntered_PanjerehTrigger = new LogicFlag();

    int step_A_Ally01ChainJobsGlobalLogicIndex = -1;
    int step_A_NumberOfCreatedSoldiersInRegionA = 0;

    float step_A_Timer = 0f;
    float step_A_TimerForCheckCreatedSoldiers = 0f;
    float step_A_MaxTimeForCheckCreatedSoldiers = 1.5f;
    float step_A_MaxTimeForKhalooDialog_AzBaghche = 4f;
    float step_A_MaxTimeForKhalooDialog_AzBaghche_Loop = 15f;

    bool step_A_IsPanjereh3DObjStarted = false;
    float step_A_Panjereh3DObjDelayTime = 4f;

    #endregion

    public string _____________________________;

    #region Step_B Variables
    //Enemy
    public MapLogicJob_FightInRegsGroup step_B_Enemy_FightInRegsGroup_01;

    //Objects
    public GameObject step_B_Objects_01_Sardar_Normal;
    public AnimationClip step_B_Objects_01_SardarAnimClip;
    public GameObject step_B_Objects_02_Sardar_Signal;
    public ParticleSystem step_B_Objects_03_SardarParticle;
    public AudioInfo step_B_Objects_04_SardarAudioInfo;
    public LogicDieTrigger step_B_Objects_05_DieTrigger;
    public GameObject step_B_Objects_06_Sardar3DObjTr;


    public StartPoint step_B_Objects_StartPoint_Player;
    public StartPoint step_B_Objects_StartPoint_Khaloo;

    //

    public float step_B_MaxTimeForRegionBFight = 10f;

    //

    float step_B_Timer = 0f;
    float step_B_MaxTimeForKhalooDialog_Sardar_Loop = 8f;
    float step_B_TimeForDieTriggerStart = 0.7f;

    bool step_B_IsSardar3DObjStarted = false;
    float step_B_Sardar3DObjDelayTime = 20f;

    LogicObjective step_B_Sardar;

    #endregion

    public string ______________________________;

    #region Step_C Variables
    //Enemy
    public MapLogicJob_FightInRegsGroup step_C_Enemy_FightInRegsGroup_01;
    public MapLogicJob_MachineGun step_C_Enemy_MachineGun_01;
    public MapLogicJob_MachineGun step_C_Enemy_MachineGun_02;

    //Objects
    public LogicTrigger step_C_Objects_01_OtaghTrigger;
    public GameObject step_C_Objects_02_Mosalsal_A_3DObjTr;
    public GameObject step_C_Objects_03_Mosalsal_B_3DObjTr;

    public GameObject step_C_Objects_04_LastMortar_A;
    public GameObject step_C_Objects_05_LastMortar_B;
    public GameObject step_C_Objects_06_LastMortar_C;

    public StartPoint step_C_Objects_StartPoint_Player;
    public StartPoint step_C_Objects_StartPoint_Khaloo;

    //

    LogicFlag step_C_PlayerEntered_OtaghTrigger = new LogicFlag();

    float step_C_Timer = 0f;

    float step_C_MaxTimeForKhalooDialog_Machinegun = 4f;
    float step_C_MaxTimeForKhalooDialog_Otagh = 12f;
    float step_C_MaxTimeForKhalooDialog_MachinegunOrOtagh_Loop = 15f;

    bool step_C_IsMosalsal_A_3DObjStarted = false;
    bool step_C_IsMosalsal_A_3DObjDone = false;

    bool step_C_IsMosalsal_B_3DObjStarted = false;
    bool step_C_IsMosalsal_B_3DObjDone = false;

    float step_C_Mosalsals3DObjDelayTime = 5f;

    float step_C_DelayToStartEndCutscene = 6f;

    float step_C_LastMortar_A_Time = 4f;
    bool step_C_IsLastMortar_A_Done = false;

    float step_C_LastMortar_B_Time = 1f;
    bool step_C_IsLastMortar_B_Done = false;

    float step_C_LastMortar_C_Time = 1f;
    bool step_C_IsLastMortar_C_Done = false;

    #endregion

    float timeCounter = 0;
    bool doorOpened = false;

    public override void StartIt()
    {
        base.StartIt();

        //LoadCheckPoint(3.32f);
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
                //Test
                RunCutscene.StartIt();

                PlayerCharacterNew.Instance.GetComponent<Compass>().enabled = false;

                PejvakLogoController.active = true;

                SetLevelStep(0.11f);
            }
            #endregion

            #region 0.11
            if (levelStep == 0.11f)
            {
                if (RunCutscene.status == CutsceneStatus.Finished)
                {
                    firstCutscene.StartIt();

                    ChangeEnableForObjects(objectsToDisableAfterCutscene, false);

                    ChangeEnableForObjects(objectsToEnableAfterCutscene, true);

                    SetLevelStep(0.2f);
                }
            }
            #endregion

            #region 0.2 Run first cutscene
            if (levelStep == 0.2f)
            {
                if (!doorOpened)
                {
                    OpenDoor();
                }

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

            //-------------------------------------------

            #region 1 Start Step A
            if (levelStep == 1f)
            {
                SaveCheckPoint(1f);

                PlayerCharacterNew.Instance.GetComponent<Compass>().enabled = true;

                step_A_Enemy_FightInRegsGroup_01.StartOutStepIfNotStarted();

                step_A_Enemy_FightInRegsGroup_02.StartOutStepIfNotStarted();

                step_A_Enemy_FightInRegsGroup_03.StartOutStepIfNotStarted();

                step_A_Objects_StartPoint_Player.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);

                step_A_Objects_StartPoint_Khaloo.PlaceCharacterOnIt(khaloo);

                step_A_Ally01ChainJobsGlobalLogicIndex = 0;
                step_A_Ally_chainJobGroup_01_Khaloo.StartOutStepIfNotStarted();

                step_A_Timer = step_A_MaxTimeForFight;

                step_A_TimerForCheckCreatedSoldiers = step_A_MaxTimeForCheckCreatedSoldiers;

                logicVoiceCollection_Khaloo.PlayName("A_01_NazarBianJolo");

                mapLogic.HUD_ObjectivesPage_SetActiveObjective(1);
                mapLogic.HUD_ShowNewMission(0);

                //
                //mapLogic.logicGUIHandler.StartObjective(0);

                PlayFirstMusic();

                SetLevelStep(1.1f);
            }
            #endregion

            #region 1.1 Wait Till Fights Finish
            if (levelStep == 1.1f)
            {
                step_A_TimerForCheckCreatedSoldiers = MathfPlus.DecByDeltatimeToZero(step_A_TimerForCheckCreatedSoldiers);
                if (step_A_TimerForCheckCreatedSoldiers == 0)
                {
                    step_A_TimerForCheckCreatedSoldiers = step_A_MaxTimeForCheckCreatedSoldiers;

                    step_A_NumberOfCreatedSoldiersInRegionA = step_A_Enemy_FightInRegsGroup_01.GetNumOfCreatedSoldiers();
                    step_A_NumberOfCreatedSoldiersInRegionA += step_A_Enemy_FightInRegsGroup_02.GetNumOfCreatedSoldiers();
                    step_A_NumberOfCreatedSoldiersInRegionA += step_A_Enemy_FightInRegsGroup_03.GetNumOfCreatedSoldiers();
                }

                if (!mapLogic.isPlayerHidden)
                    step_A_Timer = MathfPlus.DecByDeltatimeToZero(step_A_Timer);
                if (step_A_Timer == 0)
                {
                    if (step_A_NumberOfCreatedSoldiersInRegionA > step_A_minEnemiesToCreate)
                    {
                        SetLevelStep(1.2f);
                    }
                }
            }
            #endregion

            #region 1.2 Finish Fights Start Fight From Baghche
            if (levelStep == 1.2f)
            {
                step_A_Enemy_FightInRegsGroup_02.StopCreatingMoreSoldiers();

                logicVoiceCollection_Khaloo.StopCurVoiceAfterItsFinishing();

                SetLevelStep(1.3f);
            }
            #endregion

            #region 1.3 Start Fight Baghche
            if (levelStep == 1.3f)
            {
                step_B_Enemy_FightInRegsGroup_01.StartOutStepIfNotStarted();

                step_A_Timer = step_A_MaxTimeForKhalooDialog_AzBaghche;

                SetLevelStep(1.4f);
            }
            #endregion

            #region 1.4 Timer For Start Khaloo Dialog
            if (levelStep == 1.4f)
            {
                step_A_Timer = MathfPlus.DecByDeltatimeToZero(step_A_Timer);
                if (step_A_Timer == 0)
                {
                    SetLevelStep(1.45f);
                }
            }
            #endregion

            #region 1.45 Khaloo Dialog Az Baghche mian
            if (levelStep == 1.45f)
            {
                logicVoiceCollection_Khaloo.PlayName("A_02_Baghche");

                step_A_Objects_01_PanjerehTrigger.StartOutStepIfNotStarted();

                step_A_Timer = step_A_MaxTimeForKhalooDialog_AzBaghche_Loop;

                SetLevelStep(1.5f);
            }
            #endregion

            #region 1.5 Wait For Player Enter Panjereh Trigger
            if (levelStep == 1.5f)
            {
                if (!step_A_IsPanjereh3DObjStarted)
                {
                    step_A_Panjereh3DObjDelayTime = MathfPlus.DecByDeltatimeToZero(step_A_Panjereh3DObjDelayTime);

                    if (step_A_Panjereh3DObjDelayTime == 0)
                    {
                        step_A_IsPanjereh3DObjStarted = true;

                        mapLogic.HUD_Add3DObjective(step_A_Objects_02_Panjereh3DObjTr.transform, The3DObjIconType.FeleshRooBePayin, "Panjereh", The3DObjViewRange.Near);
                    }
                }

                step_A_Timer = MathfPlus.DecByDeltatimeToZero(step_A_Timer);
                if (step_A_Timer == 0)
                {
                    logicVoiceCollection_Khaloo.PlayName("A_02_Baghche");

                    step_A_Timer = step_A_MaxTimeForKhalooDialog_AzBaghche_Loop;
                }

                if (step_A_PlayerEntered_PanjerehTrigger.IsEverActivated())
                {
                    if (step_A_IsPanjereh3DObjStarted)
                    {
                        mapLogic.HUD_Remove3DObjective("Panjereh");
                    }

                    SetLevelStep(2f);
                }
            }
            #endregion

            //-------------------------------------------

            #region 2 Start Step B
            if (levelStep == 2f)
            {
                SaveCheckPoint(2f);

                step_B_Timer = step_B_MaxTimeForRegionBFight;

                mapLogic.HUD_ShowGameSaved();

                mapLogic.HUD_ObjectivesPage_SetActiveObjective(2);
                mapLogic.HUD_ShowNewMission(1);

                PlayFirstMusic();

                SetLevelStep(2.2f);
            }
            #endregion

            #region 2.2 Wait Specific Time For Fight In Region B
            if (levelStep == 2.2f)
            {
                if (!mapLogic.isPlayerHidden)
                    step_B_Timer = MathfPlus.DecByDeltatimeToZero(step_B_Timer);

                if (step_B_Timer == 0)
                {
                    SetLevelStep(2.21f);
                }
            }
            #endregion

            #region 2.21 Khaloo Dialog Sardar
            if (levelStep == 2.21f)
            {
                logicVoiceCollection_Khaloo.PlayName("B_01_Sardar");

                SetLevelStep(2.22f);
            }
            #endregion

            #region 2.22 Check If Sardar Dialog Finish
            if (levelStep == 2.22f)
            {
                if (logicVoiceCollection_Khaloo.IsCurVoiceFinished())
                {
                    SetLevelStep(2.3f);
                }
            }
            #endregion

            #region 2.3 Start Sardar Blinking
            if (levelStep == 2.3f)
            {
                step_B_Objects_01_Sardar_Normal.SetActiveRecursively(false);
                step_B_Objects_02_Sardar_Signal.SetActiveRecursively(true);

                step_B_Sardar = step_B_Objects_02_Sardar_Signal.GetComponent<LogicObjective>();

                step_B_Timer = step_B_MaxTimeForKhalooDialog_Sardar_Loop;

                //mapLogic.logicGUIHandler.StartObjective(1);

                SetLevelStep(2.5f);
            }
            #endregion

            #region 2.5 Check if Sardar Done
            if (levelStep == 2.5f)
            {
                if (!step_B_IsSardar3DObjStarted)
                {
                    step_B_Sardar3DObjDelayTime = MathfPlus.DecByDeltatimeToZero(step_B_Sardar3DObjDelayTime);

                    if (step_B_Sardar3DObjDelayTime == 0)
                    {
                        step_B_IsSardar3DObjStarted = true;

                        mapLogic.HUD_Add3DObjective(step_B_Objects_06_Sardar3DObjTr.transform, The3DObjIconType.FeleshRooBePayin, "Sardar", The3DObjViewRange.Far);
                    }
                }

                step_B_Timer = MathfPlus.DecByDeltatimeToZero(step_B_Timer);
                if (step_B_Timer == 0)
                {
                    logicVoiceCollection_Khaloo.PlayName("B_02_SardarLoop");

                    step_B_Timer = step_B_MaxTimeForKhalooDialog_Sardar_Loop;
                }

                if (step_B_Sardar.IsDone)
                {
                    if (step_B_IsSardar3DObjStarted)
                    {
                        mapLogic.HUD_Remove3DObjective("Sardar");
                    }

                    SetLevelStep(2.6f);
                }
            }
            #endregion

            #region 2.6 Sardar Fall
            if (levelStep == 2.6f)
            {
                step_B_Objects_01_Sardar_Normal.SetActiveRecursively(true);
                step_B_Objects_02_Sardar_Signal.SetActiveRecursively(false);

                MakeSardarFall(false, step_B_Objects_01_Sardar_Normal, step_B_Objects_01_SardarAnimClip);

                step_B_Timer = step_B_TimeForDieTriggerStart;

                SetLevelStep(2.65f);
            }
            #endregion

            #region 2.6 Time For Run Die Trigger
            if (levelStep == 2.65f)
            {
                step_B_Timer = MathfPlus.DecByDeltatimeToZero(step_B_Timer);

                if (step_B_Timer == 0)
                {
                    step_B_Objects_05_DieTrigger.StartIt();

                    step_B_Enemy_FightInRegsGroup_01.StopCreatingMoreSoldiers();

                    SetLevelStep(2.7f);
                }
            }
            #endregion

            #region 2.7 Finish Step B
            if (levelStep == 2.7f)
            {
                mapLogic.HUD_ObjectivesPage_SetObjectiveDone(2);
                mapLogic.HUD_ShowCompleteMission(1);

                SetLevelStep(3f);
            }
            #endregion

            //-------------------------------------------

            #region 3 Start Step C
            if (levelStep == 3f)
            {
                SaveCheckPoint(3f);

                step_C_Enemy_FightInRegsGroup_01.StartOutStepIfNotStarted();

                step_C_Enemy_MachineGun_01.StartOutStepIfNotStarted();

                step_C_Enemy_MachineGun_02.StartOutStepIfNotStarted();

                step_C_Objects_01_OtaghTrigger.StartOutStepIfNotStarted();

                step_C_Timer = step_C_MaxTimeForKhalooDialog_Machinegun;

                //mapLogic.logicGUIHandler.StartObjective(2);

                mapLogic.HUD_ShowGameSaved();

                mapLogic.HUD_ObjectivesPage_SetActiveObjective(3);
                mapLogic.HUD_ShowNewMission(2);

                PlayFirstMusic();

                SetLevelStep(3.1f);
            }
            #endregion

            #region 3.1 Wait Time For Start Khaloo Dialog
            if (levelStep == 3.1f)
            {
                step_C_Timer = MathfPlus.DecByDeltatimeToZero(step_C_Timer);

                if (step_C_Timer == 0)
                {
                    SetLevelStep(3.15f);
                }
            }
            #endregion

            #region 3.15 Khaloo Dialog Machine Gun
            if (levelStep == 3.15f)
            {
                logicVoiceCollection_Khaloo.PlayName("C_01_Mosalsal");

                step_C_Timer = step_C_MaxTimeForKhalooDialog_Otagh;

                SetLevelStep(3.16f);
            }
            #endregion

            #region 3.16 Check If Player Go To Otagh And Dialog Otagh
            if (levelStep == 3.16f)
            {
                #region Machine Guns 3D Objectives
                if (!step_C_IsMosalsal_A_3DObjStarted)
                {
                    step_C_Mosalsals3DObjDelayTime = MathfPlus.DecByDeltatimeToZero(step_C_Mosalsals3DObjDelayTime);

                    if (step_C_Mosalsals3DObjDelayTime == 0)
                    {
                        step_C_IsMosalsal_A_3DObjStarted = true;
                        step_C_IsMosalsal_B_3DObjStarted = true;

                        if (IsMachineGun_A_SoldierIsDead())
                        {
                            step_C_IsMosalsal_A_3DObjDone = true;
                        }
                        else
                        {
                            mapLogic.HUD_Add3DObjective(step_C_Objects_02_Mosalsal_A_3DObjTr.transform, The3DObjIconType.Dot, "MachineGun A", The3DObjViewRange.Far);
                        }

                        if (IsMachineGun_B_SoldierIsDead())
                        {
                            step_C_IsMosalsal_B_3DObjDone = true;
                        }
                        else
                        {
                            mapLogic.HUD_Add3DObjective(step_C_Objects_03_Mosalsal_B_3DObjTr.transform, The3DObjIconType.Dot, "MachineGun B", The3DObjViewRange.Far);
                        }
                    }
                }
                else
                {
                    if (!step_C_IsMosalsal_A_3DObjDone)
                    {
                        if (IsMachineGun_A_SoldierIsDead())
                        {
                            step_C_IsMosalsal_A_3DObjDone = true;

                            mapLogic.HUD_Remove3DObjective("MachineGun A");
                        }
                    }

                    if (!step_C_IsMosalsal_B_3DObjDone)
                    {
                        if (IsMachineGun_B_SoldierIsDead())
                        {
                            step_C_IsMosalsal_B_3DObjDone = true;

                            mapLogic.HUD_Remove3DObjective("MachineGun B");
                        }
                    }
                }
                #endregion

                step_C_Timer = MathfPlus.DecByDeltatimeToZero(step_C_Timer);

                if (step_C_Timer == 0)
                {
                    if (!step_C_PlayerEntered_OtaghTrigger.IsActiveNow())
                    {
                        logicVoiceCollection_Khaloo.PlayName("C_02_BoroToOtagh");
                    }
                    else
                    {
                        logicVoiceCollection_Khaloo.PlayName("C_01_Mosalsal");
                    }

                    step_C_Timer = step_C_MaxTimeForKhalooDialog_MachinegunOrOtagh_Loop;

                    SetLevelStep(3.2f);
                }

                if (IsMachineGunsDie())
                {
                    SetLevelStep(3.21f);
                }
            }
            #endregion

            #region 3.2 Wait For Killing Machine Guns And Khaloo Dialog Loop
            if (levelStep == 3.2f)
            {
                #region Machine Guns 3D Objectives
                if (!step_C_IsMosalsal_A_3DObjStarted)
                {
                    step_C_Mosalsals3DObjDelayTime = MathfPlus.DecByDeltatimeToZero(step_C_Mosalsals3DObjDelayTime);

                    if (step_C_Mosalsals3DObjDelayTime == 0)
                    {
                        step_C_IsMosalsal_A_3DObjStarted = true;
                        step_C_IsMosalsal_B_3DObjStarted = true;

                        if (IsMachineGun_A_SoldierIsDead())
                        {
                            step_C_IsMosalsal_A_3DObjDone = true;
                        }
                        else
                        {
                            mapLogic.HUD_Add3DObjective(step_C_Objects_02_Mosalsal_A_3DObjTr.transform, The3DObjIconType.Dot, "MachineGun A", The3DObjViewRange.Far);
                        }

                        if (IsMachineGun_B_SoldierIsDead())
                        {
                            step_C_IsMosalsal_B_3DObjDone = true;
                        }
                        else
                        {
                            mapLogic.HUD_Add3DObjective(step_C_Objects_03_Mosalsal_B_3DObjTr.transform, The3DObjIconType.Dot, "MachineGun B", The3DObjViewRange.Far);
                        }
                    }
                }
                else
                {
                    if (!step_C_IsMosalsal_A_3DObjDone)
                    {
                        if (IsMachineGun_A_SoldierIsDead())
                        {
                            step_C_IsMosalsal_A_3DObjDone = true;

                            mapLogic.HUD_Remove3DObjective("MachineGun A");
                        }
                    }

                    if (!step_C_IsMosalsal_B_3DObjDone)
                    {
                        if (IsMachineGun_B_SoldierIsDead())
                        {
                            step_C_IsMosalsal_B_3DObjDone = true;

                            mapLogic.HUD_Remove3DObjective("MachineGun B");
                        }
                    }
                }
                #endregion

                step_C_Timer = MathfPlus.DecByDeltatimeToZero(step_C_Timer);

                if (step_C_Timer == 0)
                {
                    if (!step_C_PlayerEntered_OtaghTrigger.IsActiveNow())
                    {
                        logicVoiceCollection_Khaloo.PlayName("C_02_BoroToOtagh");
                    }
                    else
                    {
                        logicVoiceCollection_Khaloo.PlayName("C_01_Mosalsal");
                    }

                    step_C_Timer = step_C_MaxTimeForKhalooDialog_MachinegunOrOtagh_Loop;
                }

                if (IsMachineGunsDie())
                {
                    SetLevelStep(3.21f);
                }
            }
            #endregion

            #region 3.21 Start delay for starting end cutscene
            if (levelStep == 3.21f)
            {
                timeCounter = step_C_DelayToStartEndCutscene;

                SetLevelStep(3.22f);
            }
            #endregion

            #region 3.22 counting delay for starting end cutscene
            if (levelStep == 3.22f)
            {
                timeCounter = MathfPlus.DecByDeltatimeToZero(timeCounter);

                if (!step_C_IsLastMortar_A_Done)
                {
                    if (timeCounter <= step_C_LastMortar_A_Time)
                    {
                        step_C_IsLastMortar_A_Done = true;

                        step_C_Objects_04_LastMortar_A.SetActiveRecursively(true);
                    }
                }

                if (!step_C_IsLastMortar_B_Done)
                {
                    if (timeCounter <= step_C_LastMortar_B_Time)
                    {
                        step_C_IsLastMortar_B_Done = true;

                        step_C_Objects_05_LastMortar_B.SetActiveRecursively(true);
                    }
                }

                //if (!step_C_IsLastMortar_C_Done)
                //{
                //    if (timeCounter <= step_C_LastMortar_C_Time)
                //    {
                //        step_C_IsLastMortar_C_Done = true;

                //        step_C_Objects_06_LastMortar_C.SetActiveRecursively(true);
                //    }
                //}

                if (timeCounter == 0)
                {
                    SetLevelStep(3.3f);
                }
            }
            #endregion

            #region 3.3f Start screen fading
            if (levelStep == 3.3f)
            {
                mapLogic.HUD_Remove3DObjective("MachineGun A");
                mapLogic.HUD_Remove3DObjective("MachineGun B");

                mapLogic.HUD_ObjectivesPage_SetObjectiveDone(3);
                mapLogic.HUD_ShowCompleteMission(2);

                mapLogic.blackScreenFader.StartFadingOut();

                MusicController.Instance.EndMusicWithFade(MusicFadeType.VeryFast);

                SetLevelStep(3.31f);
            }
            #endregion

            #region 3.31 fading screen
            if (levelStep == 3.31f)
            {
                if (mapLogic.blackScreenFader.isFadingFinished)
                {
                    SetLevelStep(3.32f);
                }
            }
            #endregion

            #region 3.32 Start end cutscene
            if (levelStep == 3.32f)
            {
                step_A_Ally_chainJobGroup_01_Khaloo.StartFinishing_OutStepIfNotFinishing();

                step_C_Objects_01_OtaghTrigger.StartFinishing_OutStepIfNotFishining();

                secondCutscene.StartIt();

                SetLevelStep(3.33f);
            }
            #endregion

            #region 3.33 Run cutscene
            if (levelStep == 3.33f)
            {
                if (secondCutscene.status == CutsceneStatus.Finished)
                {
                    SetLevelStep(3.5f);
                }
            }
            #endregion

            #region 3.5f Set mission is finished if black screen fading is done.
            if (levelStep == 3.5f)
            {
                if (mapLogic.blackScreenFader.isFadingFinished)
                {
                    SetMissionIsFinished();
                    SetLevelStep(4f);
                }
            }
            #endregion

            #endregion

        EndLevelSteps: ;

            //A

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

            #region step_A_Enemy_FightInRegsGroup_02

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
                    goto step_A_Enemy_FightInRegsGroup_02_End;
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

        step_A_Enemy_FightInRegsGroup_02_End: ;

            #endregion

            #region step_A_Enemy_FightInRegsGroup_03

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

            #region step_A_Ally_chainJobGroup_01_Khaloo

            #region 1 Start
            if (step_A_Ally_chainJobGroup_01_Khaloo.outStep == 1)
            {
                step_A_Ally_chainJobGroup_01_Khaloo.Init_SetNewGlobalLogicIndex(step_A_Ally01ChainJobsGlobalLogicIndex);
                step_A_Ally_chainJobGroup_01_Khaloo.StartIt();

                step_A_Ally_chainJobGroup_01_Khaloo.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_A_Ally_chainJobGroup_01_Khaloo.outStep == 1.1f)
            {
                step_A_Ally_chainJobGroup_01_Khaloo.RunIt();
            }
            #endregion

            #region 900 Start finishing
            if (step_A_Ally_chainJobGroup_01_Khaloo.outStep == 900f)
            {
                step_A_Ally_chainJobGroup_01_Khaloo.SetNeedsToBeFinished();

                step_A_Ally_chainJobGroup_01_Khaloo.SetOutStep(901f);
            }
            #endregion

            #region 901 Finishing
            if (step_A_Ally_chainJobGroup_01_Khaloo.outStep == 901f)
            {
                step_A_Ally_chainJobGroup_01_Khaloo.RunIt();

                if (step_A_Ally_chainJobGroup_01_Khaloo.status == LogicJobStatus.Finished)
                {
                    step_A_Ally_chainJobGroup_01_Khaloo.SetOutStep(1000);
                }
            }
            #endregion

            #endregion

            #region step_A_Objects_01_PanjerehTrigger

            #region 1 Start
            if (step_A_Objects_01_PanjerehTrigger.OutStep == 1) //Start
            {
                step_A_Objects_01_PanjerehTrigger.SetEnabled(true);
                step_A_Objects_01_PanjerehTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_A_Objects_01_PanjerehTrigger.OutStep == 1.1f) //Run
            {
                if (step_A_Objects_01_PanjerehTrigger.IsPlayerIn())
                {
                    step_A_PlayerEntered_PanjerehTrigger.SetStatus(LogicFlagStatus.Active);
                    step_A_Objects_01_PanjerehTrigger.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (step_A_Objects_01_PanjerehTrigger.OutStep == 900f) //Finish
            {
                step_A_Objects_01_PanjerehTrigger.SetEnabled(false);
                step_A_Objects_01_PanjerehTrigger.SetOutStep(1000f);
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
                }
            }
            #endregion

        step_C_Enemy_FightInRegsGroup_01_End: ;

            #endregion

            #region step_C_Enemy_MachineGun_01

            #region 1 Start
            if (step_C_Enemy_MachineGun_01.outStep == 1)
            {
                step_C_Enemy_MachineGun_01.StartIt();

                step_C_Enemy_MachineGun_01.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_C_Enemy_MachineGun_01.outStep == 1.1f)
            {
                step_C_Enemy_MachineGun_01.RunIt();

                if (step_C_Enemy_MachineGun_01.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    step_C_Enemy_MachineGun_01.StartFinishing_OutStepIfNotFinishing();

                    goto step_C_Enemy_MachineGun_01_End;
                }
            }
            #endregion

            #region 900 Start finishing
            if (step_C_Enemy_MachineGun_01.outStep == 900f)
            {
                step_C_Enemy_MachineGun_01.SetNeedsToBeFinished();
                step_C_Enemy_MachineGun_01.SetOutStep(901);
            }
            #endregion

            #region 901 Finishing
            if (step_C_Enemy_MachineGun_01.outStep == 901f)
            {
                step_C_Enemy_MachineGun_01.RunIt();

                if (step_C_Enemy_MachineGun_01.status == LogicJobStatus.Finished)
                {
                    step_C_Enemy_MachineGun_01.SetOutStep(1000f);
                }
            }
            #endregion

        step_C_Enemy_MachineGun_01_End: ;

            #endregion

            #region step_C_Enemy_MachineGun_02

            #region 1 Start
            if (step_C_Enemy_MachineGun_02.outStep == 1)
            {
                step_C_Enemy_MachineGun_02.StartIt();

                step_C_Enemy_MachineGun_02.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_C_Enemy_MachineGun_02.outStep == 1.1f)
            {
                step_C_Enemy_MachineGun_02.RunIt();

                if (step_C_Enemy_MachineGun_02.IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    step_C_Enemy_MachineGun_02.StartFinishing_OutStepIfNotFinishing();

                    goto step_C_Enemy_MachineGun_02_End;
                }
            }
            #endregion

            #region 900 Start finishing
            if (step_C_Enemy_MachineGun_02.outStep == 900f)
            {
                step_C_Enemy_MachineGun_02.SetNeedsToBeFinished();
                step_C_Enemy_MachineGun_02.SetOutStep(901);
            }
            #endregion

            #region 901 Finishing
            if (step_C_Enemy_MachineGun_02.outStep == 901f)
            {
                step_C_Enemy_MachineGun_02.RunIt();

                if (step_C_Enemy_MachineGun_02.status == LogicJobStatus.Finished)
                {
                    step_C_Enemy_MachineGun_02.SetOutStep(1000f);
                }
            }
            #endregion

        step_C_Enemy_MachineGun_02_End: ;

            #endregion

            #region step_C_Objects_01_OtaghTrigger

            #region 1 Start
            if (step_C_Objects_01_OtaghTrigger.OutStep == 1) //Start
            {
                step_C_Objects_01_OtaghTrigger.SetEnabled(true);
                step_C_Objects_01_OtaghTrigger.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (step_C_Objects_01_OtaghTrigger.OutStep == 1.1f) //Run
            {
                if (step_C_Objects_01_OtaghTrigger.IsPlayerIn())
                {
                    step_C_PlayerEntered_OtaghTrigger.SetStatus(LogicFlagStatus.Active);
                    //step_C_Objects_01_OtaghTrigger.StartFinishing_OutStepIfNotFishining();
                }
                else
                {
                    step_C_PlayerEntered_OtaghTrigger.SetDeactive();
                }
            }
            #endregion

            #region 900 Finish
            if (step_C_Objects_01_OtaghTrigger.OutStep == 900f) //Finish
            {
                step_C_Objects_01_OtaghTrigger.SetEnabled(false);
                step_C_Objects_01_OtaghTrigger.SetOutStep(1000f);
            }
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
            step_B_Objects_StartPoint_Khaloo.PlaceCharacterOnIt(khaloo);

            step_A_Enemy_FightInRegsGroup_01.StartOutStepIfNotStarted();
            step_A_Enemy_FightInRegsGroup_03.StartOutStepIfNotStarted();

            step_B_Enemy_FightInRegsGroup_01.StartOutStepIfNotStarted();

            step_A_Ally01ChainJobsGlobalLogicIndex = 0;
            step_A_Ally_chainJobGroup_01_Khaloo.StartOutStepIfNotStarted();

            return;
        }
        #endregion

        #region CheckPoint C
        if (levelStep == 3)
        {
            step_C_Objects_StartPoint_Player.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);
            step_C_Objects_StartPoint_Khaloo.PlaceCharacterOnIt(khaloo);

            step_A_Enemy_FightInRegsGroup_01.StartOutStepIfNotStarted();
            step_A_Enemy_FightInRegsGroup_03.StartOutStepIfNotStarted();

            step_A_Ally01ChainJobsGlobalLogicIndex = 0;
            step_A_Ally_chainJobGroup_01_Khaloo.StartOutStepIfNotStarted();

            MakeSardarFall(true, step_B_Objects_01_Sardar_Normal, step_B_Objects_01_SardarAnimClip);

            return;
        }
        #endregion
    }

    void MakeSardarFall(bool _shouldStayAtLastFrame, GameObject _animObj, AnimationClip _animClip)
    {
        bool shouldStayAtLastFrame = _shouldStayAtLastFrame;
        GameObject animobj = _animObj;
        AnimationClip clip = _animClip;
        string name = clip.name;

        if (!shouldStayAtLastFrame)
        {
            animobj.animation.Play(name);

            //particle
            step_B_Objects_03_SardarParticle.Play(true);

            step_B_Objects_04_SardarAudioInfo.Play();
        }
        else
        {
            animobj.animation.Play(name);
            animobj.animation[name].time = animobj.animation[name].length;
        }
    }

    bool IsMachineGunsDie()
    {
        if (step_C_Enemy_MachineGun_01.IsCreatingSoldiersStoppedAndAllSoldsDead())
        {
            if (step_C_Enemy_MachineGun_02.IsCreatingSoldiersStoppedAndAllSoldsDead())
            {
                return true;
            }
        }

        return false;
    }

    bool IsMachineGun_A_SoldierIsDead()
    {
        return step_C_Enemy_MachineGun_01.IsCreatingSoldiersStoppedAndAllSoldsDead();
    }

    bool IsMachineGun_B_SoldierIsDead()
    {
        return step_C_Enemy_MachineGun_02.IsCreatingSoldiersStoppedAndAllSoldsDead();
    }

    void OpenDoor()
    {
        doorLeft.animation.Play();

        doorRight.animation.Play();

        doorOpened = true;
    }

    void ChangeEnableForObjects(List<GameObject> objs, bool situation)
    {
        foreach (GameObject obj in objs)
        {
            obj.SetActiveRecursively(situation);
        }
    }

    void PlayFirstMusic()
    {
        MusicController.Instance.PlayMusic(MusicSong.Action_A, 0);
    }
}