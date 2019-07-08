using UnityEngine;
using System.Collections;

public class Level04_Camp_Logic : LevelLogic
{
    public GameObject ally01;
    public GameObject ally02;
    public GameObject ally03;
    public GameObject ally04;
    public GameObject ally05;

    public AlarmLamp[] alarmLamps;

    public AudioInfo[] alarmAudioInfos;

    public Collider[] antiShootWalls;

    public CutsceneController midCutscene;

    public AudioInfo[] whisperAudioInfos;

    // // // // //

    public MapLogicJob_FightInRegsGroup stepA_FightInRegsGroup_01;

    public LogicTrigger stepA_SaveAreaB;
    public LogicTrigger stepA_SaveAreaC;
    public LogicTrigger stepA_SaveAreaD;
    public LogicTrigger stepA_SaveAreaE;
    public LogicTrigger stepA_SaveAreaF;

    public StartPoint stepA_StartPointB;
    public StartPoint stepA_StartPointC;
    public StartPoint stepA_StartPointD;
    public StartPoint stepA_StartPointE;
    public StartPoint stepA_StartPointF;
    public StartPoint stepA_StartPointG;

    public LogicTrigger stepA_Trigger_MainHouseWin;
    public LogicObjective stepA_AnbarDynamite01_Object;
    public GameObject stepA_AnbarDynamite01_Normal;
    public AudioInfo stepA_AnbarDynamiteTickTickAudioInfo_01;
    public AudioInfo stepA_AnbarDynamiteExplosionAudioInfo;
    public AudioInfo stepA_AnbarDynamiteExplosionAudioInfo_02;
    public AudioInfo stepA_AnbarDynamiteExplosionFire;
    public ParticleSystem[] stepA_AnbarDynamiteExplosionParticles;
    public ParticleSystem[] stepA_AnbarExpParticlesToStopAfterSomeSeconds;
    public Explosion stepA_AnbarExplosion;
    public Light stepA_AnbarExplosionLight;
    public AnimationCurve stepA_AnbarExplosionLight_AnimCurveStart;
    public AnimationCurve stepA_AnbarExplosionLight_AnimCurveLoop;



    public GameObject stepA_MainHouseWinCollider;
    public AudioInfo stepA_MainHouseRadioAudioInfo;
    public LogicObjective stepA_RadioWiresObject;
    public GameObject stepA_RadioWiresNormal;
    public AudioInfo stepA_MainHouseRadioWireCutAudioInfo;
    public ParticleSystem[] stepA_MainHouseRadioWireCutParticles;
    public float delayTimeToStartMidCutscene = 3f;
    public float delayTimeToStartAlarmsFromMainHouse = 1.5f;

    //

    bool areAlarmsStartedBefore = false;

    int allyChainJobsGlobalLogicIndex = -1;

    //

    LogicFlag flag_A_Fight01_Finished = new LogicFlag();
    LogicFlag flag_A_PlayerEnteredSaveArea_B = new LogicFlag();
    LogicFlag flag_A_PlayerEnteredSaveArea_C = new LogicFlag();
    LogicFlag flag_A_PlayerEnteredSaveArea_D = new LogicFlag();
    LogicFlag flag_A_PlayerEnteredSaveArea_E = new LogicFlag();
    LogicFlag flag_A_PlayerEnteredSaveArea_F = new LogicFlag();

    // // //

    public StartPoint stepB_StartPointAlly01;
    public StartPoint stepB_StartPointAlly02;
    public StartPoint stepB_StartPointAlly03;
    public StartPoint stepB_StartPointAlly04;
    public StartPoint stepB_StartPointAlly05;

    public MapLogicJob_FightInRegsGroup stepB_FightInRegsGroup_01;

    public float stepB_DelayTimeToExplodeAnbar = 12;

    public float stepB_DelayTimeToStartAllies = 20;

    public MapLogicJob_ChainJobsGroup stepB_AllyChainJobGroup;

    public float stepB_DelayTimeToOpenMainHouseDoor = 4;

    public GameObject mainHouseDoor;
    public AudioInfo stepB_AudioInfo_MainHouseDoor;
    public AudioInfo stepB_AudioInfo_MainHouseCallingKhosro;

    public LogicTrigger stepB_Trigger_BottomFloor;
    public LogicTrigger stepB_Trigger_BesideDoorAlly;
    public LogicTrigger stepB_Trigger_BesideMissionLastTir;

    public Transform stepB_HUD_LeaderAlly3DObjTr;

    public LogicTrigger stepB_LevelExitArea;

    public LogicVoiceCollection stepB_LogVoiceCol_KhosroBiaPayin;


    public LogicTrigger[] stepB_Triggers_StartPlayerKillStart;
    public Transform stepB_PlayerKillTr;

    public float stepB_DelayTimeToKillPlayer = 1.5f;

    //

    LogicFlag flag_B_Fight01_Finished = new LogicFlag();

    // // //


    // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // //

    float timeCounter = 0;

    float stepB_AnbarExplosiontimeCounter = 0;

    float startAlramTimeCounter = 1.4f;


    bool shouldDecTimeToSetMissionIsFailed = false;
    float timeCounterToSetMissionIsFailed = 3f;
    bool settingMissionFailByDetectionIsDone = false;

    float tryStartSittingPlayerDelayMaxTime = 1.6f;
    bool sittingTryDone = false;

    bool isFirstMissionHUDShowed = false;

    bool anbarBombIsExploded = false;
    float delayAfterAnbarBombExplosion = 2f;

    float anbarExplosionLightCounter = 0;
    bool anbarExplosionFirstLightEnabled = false;
    bool anbarExplosionLoopLightEnabled = false;
    float anbarExplosionStartLightAnimDuration = 2f;

    bool shouldExplodeAnbarForStepB = false;

    bool isFirstKhosroCallPlayed = false;
    float timeToStartFirstKhosroCallBeforeAlliesStart = 2.9f;

    float killingPlayerTimeCounter;

    bool shouldCheckPlayerStartDyingTriggers = false;
    bool shouldKillPlayerAfterDelay = false;

    bool shouldStopDemAnbarExpAprticlesAfterSecs = false;
    float timeCounterToStopDemAnbarExpAprticles = 4f;

    float anbarDynamiteTickTickSoundVolAfterRadioCut = 0.75f;

    float whisperingVolumeDecreaseSpeed = 1.6f;
    bool shouldDeacreseVolumeOfWhisperingAudios = false;

    //

    public override void StartIt()
    {
        base.StartIt();

        stepA_AnbarExplosionLight_AnimCurveLoop.postWrapMode = WrapMode.Loop;

        //LoadCheckPoint(6.96f);
    }

    public override void RunIt()
    {
        base.RunIt();

        if (step == 1)
        {
            if (!settingMissionFailByDetectionIsDone)
            {
                if (levelStep < 6)
                {
                    if (mapLogic.IsPlayerDetectedInCampMode())
                    {
                        shouldDecTimeToSetMissionIsFailed = true;
                    }
                }

                if (levelStep > 0)
                {
                    if (shouldDecTimeToSetMissionIsFailed)
                    {
                        timeCounterToSetMissionIsFailed = MathfPlus.DecByDeltatimeToZero(timeCounterToSetMissionIsFailed);

                        if (timeCounterToSetMissionIsFailed == 0)
                        {
                            SetLevelStep(step_MissionFail_YouAreDetectedByEnemies);
                            settingMissionFailByDetectionIsDone = true;
                        }
                    }
                }
            }

            //

            if (mapLogic.IsPlayerDetectedInCampMode())
            {
                startAlramTimeCounter = MathfPlus.DecByDeltatimeToZero(startAlramTimeCounter);
                if (startAlramTimeCounter == 0)
                    StartAlarms();
            }

            //

            if (anbarExplosionFirstLightEnabled)
            {
                anbarExplosionLightCounter += Time.deltaTime;

                stepA_AnbarExplosionLight.intensity = stepA_AnbarExplosionLight_AnimCurveStart.Evaluate(anbarExplosionLightCounter);

                if (anbarExplosionLightCounter >= anbarExplosionStartLightAnimDuration)
                {
                    anbarExplosionFirstLightEnabled = false;
                    anbarExplosionLoopLightEnabled = true;

                    anbarExplosionLightCounter = 0;
                }
            }

            if (anbarExplosionLoopLightEnabled)
            {
                anbarExplosionLightCounter += Time.deltaTime;

                stepA_AnbarExplosionLight.intensity = stepA_AnbarExplosionLight_AnimCurveLoop.Evaluate(anbarExplosionLightCounter);
            }

            //

            if (shouldExplodeAnbarForStepB)
            {

                stepB_AnbarExplosiontimeCounter = MathfPlus.DecByDeltatimeToZero(stepB_AnbarExplosiontimeCounter);

                if (stepB_AnbarExplosiontimeCounter == 0)
                {
                    ExplodeAnbarBomb();
                    shouldExplodeAnbarForStepB = false;
                }
            }

            //

            if (shouldCheckPlayerStartDyingTriggers)
            {
                for (int i = 0; i < stepB_Triggers_StartPlayerKillStart.Length; i++)
                {
                    if (stepB_Triggers_StartPlayerKillStart[i].IsPlayerIn())
                    {
                        shouldKillPlayerAfterDelay = true;

                        shouldCheckPlayerStartDyingTriggers = false;
                    }
                }

            }

            if (shouldKillPlayerAfterDelay)
            {
                killingPlayerTimeCounter = MathfPlus.DecByDeltatimeToZero(killingPlayerTimeCounter);

                if (killingPlayerTimeCounter == 0)
                {
                    shouldKillPlayerAfterDelay = false;

                    if (PlayerCharacterNew.Instance != null)
                    {
                        DamageInfo dmgInf = new DamageInfo();

                        dmgInf.damageSource = stepB_PlayerKillTr.gameObject;
                        dmgInf.damageSourcePosition = stepB_PlayerKillTr.position;
                        dmgInf.damageAmount = 100000000;

                        PlayerCharacterNew.Instance.ApplyDamage(dmgInf);
                    }
                }
            }

            //

            if (shouldStopDemAnbarExpAprticlesAfterSecs)
            {
                timeCounterToStopDemAnbarExpAprticles = MathfPlus.DecByDeltatimeToZero(timeCounterToStopDemAnbarExpAprticles);

                if (timeCounterToStopDemAnbarExpAprticles == 0)
                {
                    shouldStopDemAnbarExpAprticlesAfterSecs = false;

                    for (int i = 0; i < stepA_AnbarExpParticlesToStopAfterSomeSeconds.Length; i++)
                    {
                        stepA_AnbarExpParticlesToStopAfterSomeSeconds[i].gameObject.SetActiveRecursively(false);
                    }
                }
            }

            //

            if (shouldDeacreseVolumeOfWhisperingAudios)
            {
                for (int i = 0; i < whisperAudioInfos.Length; i++)
                {
                    whisperAudioInfos[i].SetCustomVolume(whisperAudioInfos[i].customVolume - whisperingVolumeDecreaseSpeed * Time.deltaTime);
                }

                if (whisperAudioInfos[0].customVolume == 0)
                {
                    for (int i = 0; i < whisperAudioInfos.Length; i++)
                    {
                        whisperAudioInfos[i].SetCustomVolume(0);
                        whisperAudioInfos[i].Stop();
                    }

                    shouldDeacreseVolumeOfWhisperingAudios = false;
                }
            }


        StartLevelSteps:

            #region Level

            #region 0.1 Start first cutscene
            if (levelStep == 0.1f)
            {
                SetLevelStep(1f);
            }
            #endregion

            //A (Part A -> F)

            #region 1 Init StepA_PartA
            if (levelStep == 1)
            {
                SaveCheckPoint(1);

                A_MutualInit();

                stepA_SaveAreaB.StartOutStepIfNotStarted();

                mapLogic.playerCharNew.HUD_TryToStartShowingSneakingHints();

                timeCounter = tryStartSittingPlayerDelayMaxTime;

                //mapLogic.HUD_ShowGameSaved();

                mapLogic.HUD_ObjectivesPage_SetActiveObjective(1);

                mapLogic.HUD_ShowNewMission(0);
                isFirstMissionHUDShowed = true;

                SetLevelStep(1.1f);
            }
            #endregion

            #region 1.1
            if (levelStep == 1.1f)
            {
                if (!sittingTryDone)
                {
                    timeCounter = MathfPlus.DecByDeltatimeToZero(timeCounter);

                    if (timeCounter == 0)
                    {
                        sittingTryDone = true;
                        mapLogic.playerCharNew.TryStartSitting();
                    }
                }

                if (flag_A_PlayerEnteredSaveArea_B.IsEverActivated())
                {
                    SetLevelStep(2f);
                }
            }
            #endregion

            #region 2 Init StepA_PartB
            if (levelStep == 2)
            {
                SaveCheckPoint(2);

                A_MutualInit();

                stepA_SaveAreaC.StartOutStepIfNotStarted();

                mapLogic.playerCharNew.HUD_TryToStartShowingSneakingHints();

                mapLogic.HUD_ShowGameSaved();

                mapLogic.HUD_ObjectivesPage_SetActiveObjective(1);

                if (!isFirstMissionHUDShowed)
                {
                    mapLogic.HUD_ShowNewMission(0);
                    isFirstMissionHUDShowed = true;
                }

                SetLevelStep(2.1f);
            }
            #endregion

            #region 2.1
            if (levelStep == 2.1f)
            {
                if (flag_A_PlayerEnteredSaveArea_C.IsEverActivated())
                {
                    SetLevelStep(3f);
                }
            }
            #endregion

            #region 3 Init StepA_PartC
            if (levelStep == 3)
            {
                SaveCheckPoint(3);

                A_MutualInit();

                stepA_SaveAreaD.StartOutStepIfNotStarted();

                mapLogic.playerCharNew.HUD_TryToStartShowingSneakingHints();

                mapLogic.HUD_ShowGameSaved();

                mapLogic.HUD_ObjectivesPage_SetActiveObjective(1);

                if (!isFirstMissionHUDShowed)
                {
                    mapLogic.HUD_ShowNewMission(0);
                    isFirstMissionHUDShowed = true;
                }

                SetLevelStep(3.1f);
            }
            #endregion

            #region 3.1
            if (levelStep == 3.1f)
            {
                if (flag_A_PlayerEnteredSaveArea_D.IsEverActivated())
                {
                    mapLogic.HUD_ObjectivesPage_SetObjectiveDone(1);
                    mapLogic.HUD_ShowCompleteMission(0);

                    SetLevelStep(4f);
                }
            }
            #endregion

            #region 4 Init StepA_PartD
            if (levelStep == 4)
            {
                SaveCheckPoint(4);

                A_MutualInit();

                stepA_SaveAreaE.StartOutStepIfNotStarted();

                mapLogic.playerCharNew.HUD_TryToStartShowingSneakingHints();

                mapLogic.HUD_ShowGameSaved();

                mapLogic.HUD_ObjectivesPage_SetActiveObjective(2);

                mapLogic.HUD_ShowNewMission(1);

                SetLevelStep(4.1f);
            }
            #endregion

            #region 4.1
            if (levelStep == 4.1f)
            {
                if (flag_A_PlayerEnteredSaveArea_E.IsEverActivated())
                {
                    {
                        SetLevelStep(step_MissionFail_YouLeftAreaWithoutPlantingDynamites);
                        goto EndLevelSteps;
                    }
                }

                if (stepA_AnbarDynamite01_Object.transform.active == true)
                {
                    if (stepA_AnbarDynamite01_Object.IsDone)
                    {
                        MakeAnbarDynamiteVisible();

                        mapLogic.HUD_ObjectivesPage_SetObjectiveDone(2);
                        mapLogic.HUD_ShowCompleteMission(1);

                        SetLevelStep(5);
                    }
                }
            }
            #endregion

            #region 5 Init StepA_PartE
            if (levelStep == 5)
            {
                SaveCheckPoint(5);

                A_MutualInit();

                stepA_SaveAreaF.StartOutStepIfNotStarted();

                mapLogic.playerCharNew.HUD_TryToStartShowingSneakingHints();

                mapLogic.HUD_ShowGameSaved();

                mapLogic.HUD_ObjectivesPage_SetActiveObjective(3);

                mapLogic.HUD_ShowNewMission(2);

                SetLevelStep(5.1f);
            }
            #endregion

            #region 5.1
            if (levelStep == 5.1f)
            {
                if (mapLogic.playerCharNew.hud_LvlCamp_ClockTimeCounter == 0)
                {
                    ExplodeAnbarBomb();
                    stepA_RadioWiresObject.IsActive = false;
                }

                if (anbarBombIsExploded)
                {
                    delayAfterAnbarBombExplosion = MathfPlus.DecByDeltatimeToZero(delayAfterAnbarBombExplosion);

                    if (delayAfterAnbarBombExplosion == 0)
                    {
                        if (!mapLogic.playerCharNew.IsMissionFailed())
                        {
                            SetLevelStep(step_MissionFail_DynamteHasBeenExplodedBeforeCommunicationBreakdown);
                            goto EndLevelSteps;
                        }
                        else
                        {
                            SetLevelStep(step_TheEndBro);
                            goto EndLevelSteps;
                        }
                    }
                }

                if (flag_A_PlayerEnteredSaveArea_F.IsEverActivated())
                {
                    mapLogic.HUD_ObjectivesPage_SetObjectiveDone(3);
                    mapLogic.HUD_ShowCompleteMission(2);

                    SetLevelStep(6f);
                }
            }
            #endregion

            #region 6 Init StepA_PartF
            if (levelStep == 6)
            {
                SaveCheckPoint(6);

                A_MutualInit();

                mapLogic.HUD_ShowGameSaved();

                mapLogic.HUD_ObjectivesPage_SetActiveObjective(4);

                mapLogic.HUD_ShowNewMission(3);

                SetLevelStep(6.1f);
            }
            #endregion

            #region 6.1 Wait for breaking down radio communication
            if (levelStep == 6.1f)
            {
                //if (stepA_DynamiteObject.IsDone)
                //{
                //    SetLevelStep(6.2f);
                //}

                if (mapLogic.playerCharNew.hud_LvlCamp_ClockTimeCounter == 0)
                {
                    ExplodeAnbarBomb();
                    stepA_RadioWiresObject.IsActive = false;
                }

                if (anbarBombIsExploded)
                {
                    delayAfterAnbarBombExplosion = MathfPlus.DecByDeltatimeToZero(delayAfterAnbarBombExplosion);

                    if (delayAfterAnbarBombExplosion == 0)
                    {
                        if (!mapLogic.playerCharNew.IsMissionFailed())
                        {
                            SetLevelStep(step_MissionFail_DynamteHasBeenExplodedBeforeCommunicationBreakdown);
                            goto EndLevelSteps;
                        }
                        else
                        {
                            SetLevelStep(step_TheEndBro);
                            goto EndLevelSteps;
                        }
                    }
                }
                else
                {
                    if (stepA_RadioWiresObject.IsDone)
                    {
                        SetLevelStep(6.2f);
                    }
                }
            }
            #endregion

            #region 6.2 Damage radio wire
            if (levelStep == 6.2f)
            {
                //stepA_DynamiteObject.transform.active = false;
                //stepA_DynamiteNormal.transform.active = true;

                //stepA_MainHouseDynamiteTickTickAudioInfo.Play();

                ShowDamagedRadioWiresAndStopPlayingRadioSounds();

                foreach (ParticleSystem ps in stepA_MainHouseRadioWireCutParticles)
                {
                    ps.Play(true);

                    ps.gameObject.GetComponent<DieOverTime>().enabled = true;
                }

                stepA_MainHouseRadioWireCutAudioInfo.Play();

                //timeCounter = delayTimeToExplodeMainHouseDynamite;

                timeCounter = delayTimeToStartAlarmsFromMainHouse;

                mapLogic.playerCharNew.HUD_StopShowingLvlCampCounterClock();
                stepA_AnbarDynamiteTickTickAudioInfo_01.SetCustomVolume(anbarDynamiteTickTickSoundVolAfterRadioCut);

                mapLogic.HUD_ObjectivesPage_SetObjectiveDone(4);
                mapLogic.HUD_ShowCompleteMission(3);

                SetLevelStep(6.5f);
            }
            #endregion

            #region Oldies
            //#region 6.3 Wait for dynamite explosion
            //if (levelStep == 6.3f)
            //{
            //    timeCounter = MathfPlus.DecByDeltatimeToZero(timeCounter);

            //    if (timeCounter == 0)
            //    {
            //        SetLevelStep(6.4f);
            //    }
            //}
            //#endregion

            //#region 6.4 Explode dat dynamite
            //if (levelStep == 6.4f)
            //{
            //    stepA_DynamiteNormal.active = false;

            //    foreach (ParticleSystem ps in stepA_MainHouseDynamiteExplosions)
            //    {
            //        ps.Play(true);
            //    }
            //    stepA_MainHouseDynamiteAudioInfo.Play();
            //    stepA_MainHouseRadioAudioInfo.Stop();
            //    stepA_MainHouseDynamiteTickTickAudioInfo.Stop();

            //    stepA_MainHouseRadio_Left.active = false;
            //    stepA_MainHouseRadio_Mid.active = false;
            //    stepA_MainHouseRadio_Right.active = false;

            //    stepA_MainHouseRadio_Exploded_Left.active = true;
            //    stepA_MainHouseRadio_Exploded_Mid.active = true;
            //    stepA_MainHouseRadio_Exploded_Right.active = true;

            //    timeCounter = delayTimeToStartAlarmsFromMainHouse;

            //    SetLevelStep(6.5f);
            //}
            //#endregion
            #endregion

            #region 6.5 Wait for starting alarms
            if (levelStep == 6.5f)
            {
                timeCounter = MathfPlus.DecByDeltatimeToZero(timeCounter);

                if (timeCounter == 0)
                {
                    SetLevelStep(6.6f);
                }
            }
            #endregion

            #region 6.6 Start alarms
            if (levelStep == 6.6f)
            {
                StartAlarms();
                mapLogic.SetPlayerIsDetectedInCampMode();
                timeCounter = delayTimeToStartMidCutscene;

                SetLevelStep(6.7f);
            }
            #endregion

            #region 6.7 Wait for starting mid cutscene
            if (levelStep == 6.7f)
            {
                timeCounter = MathfPlus.DecByDeltatimeToZero(timeCounter);

                if (timeCounter == 0)
                {
                    SetLevelStep(6.9f);
                }
            }
            #endregion

            #region 6.9 Start screen fading
            if (levelStep == 6.9f)
            {
                mapLogic.blackScreenFader.StartFadingOut();
                SetLevelStep(6.95f);
            }
            #endregion

            #region 6.95 fading screen
            if (levelStep == 6.95f)
            {
                if (mapLogic.blackScreenFader.isFadingFinished)
                {
                    SetLevelStep(6.96f);
                }
            }
            #endregion

            #region 6.96 Start cutscene
            if (levelStep == 6.96f)
            {
                midCutscene.StartIt();

                SetLevelStep(6.97f);
            }
            #endregion

            #region 6.97 Run cutscene
            if (levelStep == 6.97f)
            {
                if (midCutscene.status == CutsceneStatus.Finished)
                {
                    SetLevelStep(6.98f);
                }
            }
            #endregion

            #region 6.98 Player: Camp -> Normal - Destroy Solds
            if (levelStep == 6.98f)
            {
                mapLogic.playerCharNew.ResetPlayerCampStatus(false);
                DestroyAllMapSolds();
                SetLevelStep(6.99f);
            }
            #endregion

            #region 6.99 BlackScreen after cutscene
            if (levelStep == 6.99f)
            {
                mapLogic.blackScreenFader.StartFadingIn();
                SetLevelStep(7f);
            }
            #endregion

            //B

            #region 7 Init StepB
            if (levelStep == 7)
            {
                SaveCheckPoint(7);

                RemoveAntiShootWalls();

                stepB_FightInRegsGroup_01.StartOutStepIfNotStarted();

                timeCounter = stepB_DelayTimeToStartAllies;

                stepB_AnbarExplosiontimeCounter = stepB_DelayTimeToExplodeAnbar;

                shouldExplodeAnbarForStepB = true;

                stepB_Trigger_BottomFloor.SetEnabled(true);

                stepB_Trigger_BesideDoorAlly.SetEnabled(true);

                stepB_Trigger_BesideMissionLastTir.SetEnabled(true);

                killingPlayerTimeCounter = stepB_DelayTimeToKillPlayer;

                for (int i = 0; i < stepB_Triggers_StartPlayerKillStart.Length; i++)
                {
                    stepB_Triggers_StartPlayerKillStart[i].SetEnabled(true);
                }

                shouldCheckPlayerStartDyingTriggers = true;

                mapLogic.HUD_ShowGameSaved();

                mapLogic.HUD_ObjectivesPage_SetActiveObjective(5);

                mapLogic.HUD_ShowNewMission(4);

                SetLevelStep(7.1f);
            }
            #endregion

            #region 7.1f W8 for Starting Allies
            if (levelStep == 7.1f)
            {
                timeCounter = MathfPlus.DecByDeltatimeToZero(timeCounter);

                if (!isFirstKhosroCallPlayed)
                {
                    if (timeCounter <= timeToStartFirstKhosroCallBeforeAlliesStart)
                    {
                        isFirstKhosroCallPlayed = true;
                        stepB_AudioInfo_MainHouseCallingKhosro.Play();
                    }
                }

                if (timeCounter == 0)
                {
                    SetLevelStep(7.2f);
                }
            }
            #endregion

            #region 7.2f Start allies
            if (levelStep == 7.2f)
            {
                //foreach (LogicTrigger logTr in stepB_MissionFailAreas)
                //{
                //    logTr.SetEnabled(true);
                //}

                //stepB_PlayerInsideArea.SetEnabled(true);

                //stepB_DeathAreaStartTrigger.SetEnabled(true);

                stepB_LevelExitArea.SetEnabled(true);

                B_PlaceAllies();

                allyChainJobsGlobalLogicIndex = 0;

                stepB_AllyChainJobGroup.StartOutStepIfNotStarted();

                timeCounter = stepB_DelayTimeToOpenMainHouseDoor;

                SetLevelStep(7.3f);
            }
            #endregion

            #region 7.3f W8 for Opening door
            if (levelStep == 7.3f)
            {
                timeCounter = MathfPlus.DecByDeltatimeToZero(timeCounter);

                if (timeCounter == 0)
                {
                    SetLevelStep(7.4f);
                }
            }
            #endregion

            #region 7.4f Open the door
            if (levelStep == 7.4f)
            {
                mainHouseDoor.animation.Play();

                stepB_AudioInfo_MainHouseDoor.Play();

                mapLogic.HUD_Add3DObjective(stepB_HUD_LeaderAlly3DObjTr, The3DObjIconType.Dot, "LeaderAlly", The3DObjViewRange.SoFar);

                mapLogic.HUD_ObjectivesPage_SetActiveObjective(6);

                mapLogic.HUD_ShowNewMission(5);

                if (stepB_Trigger_BottomFloor.IsPlayerIn())
                {
                    SetLevelStep(7.6f);
                }
                else
                {
                    stepB_LogVoiceCol_KhosroBiaPayin.PlayName("KhosroBiaPayin");
                    SetLevelStep(7.5f);
                }

            }
            #endregion

            #region 7.5f W8 till Khosro comes bottom
            if (levelStep == 7.5f)
            {
                if (stepB_Trigger_BottomFloor.IsPlayerIn())
                {
                    stepB_Trigger_BottomFloor.SetEnabled(false);
                    stepB_LogVoiceCol_KhosroBiaPayin.StopCurVoiceAfterItsFinishing();
                    SetLevelStep(7.6f);
                }
            }
            #endregion

            #region 7.6f W8 till Khosro comes near door
            if (levelStep == 7.6f)
            {
                if (stepB_Trigger_BesideDoorAlly.IsPlayerIn())
                {
                    stepB_Trigger_BesideDoorAlly.SetEnabled(false);
                    
                    SetLevelStep(7.7f);
                }
            }
            #endregion

            #region 7.7f Start moving leader ally
            if (levelStep == 7.7f)
            {
                stepB_LogVoiceCol_KhosroBiaPayin.PlayName("FollowMe");

                allyChainJobsGlobalLogicIndex = 1;

                stepB_AllyChainJobGroup.Init_SetNewGlobalLogicIndex(allyChainJobsGlobalLogicIndex);

                SetLevelStep(7.8f);
            }
            #endregion

            #region 7.8f W8 till Khosro comes near ally
            if (levelStep == 7.8f)
            {
                if (stepB_Trigger_BesideMissionLastTir.IsPlayerIn())
                {
                    stepB_Trigger_BesideMissionLastTir.SetEnabled(false);

                    SetLevelStep(7.81f);
                }
            }
            #endregion

            #region 7.81f Start moving leader ally
            if (levelStep == 7.81f)
            {
                stepB_LogVoiceCol_KhosroBiaPayin.PlayName("FollowMe2");

                allyChainJobsGlobalLogicIndex = 2;

                stepB_AllyChainJobGroup.Init_SetNewGlobalLogicIndex(allyChainJobsGlobalLogicIndex);

                SetLevelStep(7.82f);
            }
            #endregion

            #region 7.82f W8 for player go out
            if (levelStep == 7.82f)
            {
                if (stepB_LevelExitArea.IsPlayerIn())
                {
                    SetLevelStep(7.83f);
                }
            }
            #endregion

            #region 7.83f Start End black screen
            if (levelStep == 7.83f)
            {
                mapLogic.HUD_ObjectivesPage_SetObjectiveDone(6);
                mapLogic.HUD_ShowCompleteMission(5);
                mapLogic.HUD_ShowMainMission(true);

                mapLogic.HUD_Remove3DObjective("LeaderAlly");

                mapLogic.blackScreenFader.StartFadingOut();
                SetLevelStep(7.9f);
            }
            #endregion

            #region 7.9f Set mission is finished if black screen fading is done.
            if (levelStep == 7.9f)
            {
                if (mapLogic.blackScreenFader.isFadingFinished)
                {
                    SetMissionIsFinished();
                    SetLevelStep(8f);
                }
            }
            #endregion 

            #region Oldies
        //#region 7.5f CheckMissionFailAreas - CheckPlayerInsideArea - CheckFinishingOfTheFight
        //if (levelStep == 7.5f)
        //{
        //    foreach (LogicTrigger logTr in stepB_MissionFailAreas)
        //    {
        //        if (logTr.IsPlayerIn())
        //        {
        //            SetLevelStep(step_MissionFail_YouLeftFightArea);
        //            goto EndLevelSteps;
        //        }
        //    }

            //    if (stepB_FightInRegsGroup_01.countOfRemainingSolds <= stepB_SkippableNumOfSolds)
        //    {
        //        if (!stepB_PlayerInsideArea.IsPlayerIn())
        //        {
        //            stepB_FightInRegsGroup_01.StopCreatingMoreSoldiersAndMakeAliveSoldiersSoWeak();

            //            SetLevelStep(7.55f);
        //            goto StartLevelSteps;
        //        }
        //    }

            //    if (stepB_FightInRegsGroup_01.IsCreatingSoldiersStoppedAndAllSoldsDead())
        //    {
        //        SetLevelStep(7.6f);
        //    }
        //}
        //#endregion

            //#region 7.55f CheckFinishingOfTheFight
        //if (levelStep == 7.55f)
        //{
        //    if (stepB_DeathAreaStartTrigger.IsPlayerIn())
        //    {
        //        stepB_DieTriggerAll.StartItIfItsNotStartedBefore();
        //    }

            //    if (stepB_FightInRegsGroup_01.IsCreatingSoldiersStoppedAndAllSoldsDead())
        //    {
        //        SetLevelStep(7.6f);
        //    }
        //}
        //#endregion

            //#region 7.6f Start Allies Next Step
        //if (levelStep == 7.6f)
        //{
        //    allyChainJobsGlobalLogicIndex = 1;

            //    stepB_AllyChainJobGroup.Init_SetNewGlobalLogicIndex(allyChainJobsGlobalLogicIndex);

            //    SetLevelStep(7.7f);
        //}
        //#endregion

            //#region 7.7f Check Level Exit Area
        //if (levelStep == 7.7f)
        //{
        //    if (stepB_LevelExitArea.IsPlayerIn())
        //    {
        //        SetLevelStep(7.8f);
        //    }
        //}
        //#endregion

            //#region 7.8f Start End black screen
        //if (levelStep == 7.8f)
        //{
        //    mapLogic.blackScreenFader.StartFadingOut();
        //    SetLevelStep(7.9f);
        //}
        //#endregion


            //#region 7.9f Set mission is finished if black screen fading is done.
        //if (levelStep == 7.9f)
        //{
        //    if (mapLogic.blackScreenFader.isFadingFinished)
        //    {
        //        SetMissionIsFinished();
        //        SetLevelStep(8f);
        //    }
        //}
        //#endregion 
            #endregion

            #endregion

        EndLevelSteps: ;

            // A

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

            #region Trigger_MainHouseWin

            #region 1 Start
            if (stepA_Trigger_MainHouseWin.OutStep == 1) //Start
            {
                stepA_Trigger_MainHouseWin.SetEnabled(true);
                stepA_Trigger_MainHouseWin.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepA_Trigger_MainHouseWin.OutStep == 1.1f) //Run
            {
                if (stepA_Trigger_MainHouseWin.IsPlayerIn())
                {
                    stepA_MainHouseWinCollider.collider.enabled = true;

                    stepA_Trigger_MainHouseWin.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (stepA_Trigger_MainHouseWin.OutStep == 900f) //Finish
            {
                stepA_Trigger_MainHouseWin.SetEnabled(false);
                stepA_Trigger_MainHouseWin.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region Trigger_SaveAreaB

            #region 1 Start
            if (stepA_SaveAreaB.OutStep == 1) //Start
            {
                stepA_SaveAreaB.SetEnabled(true);
                stepA_SaveAreaB.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepA_SaveAreaB.OutStep == 1.1f) //Run
            {
                if (stepA_SaveAreaB.IsPlayerIn())
                {
                    flag_A_PlayerEnteredSaveArea_B.SetStatus(LogicFlagStatus.Active);

                    stepA_SaveAreaB.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (stepA_SaveAreaB.OutStep == 900f) //Finish
            {
                stepA_SaveAreaB.SetEnabled(false);
                stepA_SaveAreaB.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region Trigger_SaveAreaC

            #region 1 Start
            if (stepA_SaveAreaC.OutStep == 1) //Start
            {
                stepA_SaveAreaC.SetEnabled(true);
                stepA_SaveAreaC.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepA_SaveAreaC.OutStep == 1.1f) //Run
            {
                if (stepA_SaveAreaC.IsPlayerIn())
                {
                    flag_A_PlayerEnteredSaveArea_C.SetStatus(LogicFlagStatus.Active);

                    stepA_SaveAreaC.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (stepA_SaveAreaC.OutStep == 900f) //Finish
            {
                stepA_SaveAreaC.SetEnabled(false);
                stepA_SaveAreaC.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region Trigger_SaveAreaD

            #region 1 Start
            if (stepA_SaveAreaD.OutStep == 1) //Start
            {
                stepA_SaveAreaD.SetEnabled(true);
                stepA_SaveAreaD.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepA_SaveAreaD.OutStep == 1.1f) //Run
            {
                if (stepA_SaveAreaD.IsPlayerIn())
                {
                    flag_A_PlayerEnteredSaveArea_D.SetStatus(LogicFlagStatus.Active);

                    stepA_SaveAreaD.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (stepA_SaveAreaD.OutStep == 900f) //Finish
            {
                stepA_SaveAreaD.SetEnabled(false);
                stepA_SaveAreaD.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region Trigger_SaveAreaE

            #region 1 Start
            if (stepA_SaveAreaE.OutStep == 1) //Start
            {
                stepA_SaveAreaE.SetEnabled(true);
                stepA_SaveAreaE.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepA_SaveAreaE.OutStep == 1.1f) //Run
            {
                if (stepA_SaveAreaE.IsPlayerIn())
                {
                    flag_A_PlayerEnteredSaveArea_E.SetStatus(LogicFlagStatus.Active);

                    stepA_SaveAreaE.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (stepA_SaveAreaE.OutStep == 900f) //Finish
            {
                stepA_SaveAreaE.SetEnabled(false);
                stepA_SaveAreaE.SetOutStep(1000f);
            }
            #endregion

            #endregion

            #region Trigger_SaveAreaF

            #region 1 Start
            if (stepA_SaveAreaF.OutStep == 1) //Start
            {
                stepA_SaveAreaF.SetEnabled(true);
                stepA_SaveAreaF.SetOutStep(1.1f);
            }
            #endregion

            #region 1.1 Run
            if (stepA_SaveAreaF.OutStep == 1.1f) //Run
            {
                if (stepA_SaveAreaF.IsPlayerIn())
                {
                    flag_A_PlayerEnteredSaveArea_F.SetStatus(LogicFlagStatus.Active);

                    stepA_SaveAreaF.StartFinishing_OutStepIfNotFishining();
                }
            }
            #endregion

            #region 900 Finish
            if (stepA_SaveAreaF.OutStep == 900f) //Finish
            {
                stepA_SaveAreaF.SetEnabled(false);
                stepA_SaveAreaF.SetOutStep(1000f);
            }
            #endregion

            #endregion

            // B

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
                    stepB_AllyChainJobGroup.SetOutStep(1000);
                }
            }
            #endregion

            #endregion
        }
    }

    //

    void A_MutualInit()
    {
        stepA_FightInRegsGroup_01.StartOutStepIfNotStarted();

        stepA_Trigger_MainHouseWin.StartOutStepIfNotStarted();

        StartWhisperingAudioInfos();
    }

    void StartAlarms()
    {
        if (!areAlarmsStartedBefore)
        {
            areAlarmsStartedBefore = true;

            OnlyStartAlarmLamps();

            OnlyStartAlarmSounds();

            StartDecreasingVolumeOfWhisperingAudioInfos();
        }
    }

    void OnlyStartAlarmLamps()
    {
        foreach (AlarmLamp lamp in alarmLamps)
        {
            lamp.StartIt();
        }
    }

    void OnlyStartAlarmSounds()
    {
        foreach (AudioInfo alarmAudInfo in alarmAudioInfos)
        {
            alarmAudInfo.Play();
        }
    }

    void DestroyAllMapSolds()
    {
        foreach (GameObject eneObj in mapLogic.mapEnemyChars)
        {
            if (eneObj)
                Destroy(eneObj);
        }
    }

    void RemoveAntiShootWalls()
    {
        for (int i = 0; i < antiShootWalls.Length; i++)
        {
            antiShootWalls[i].collider.enabled = false;
        }
    }

    void B_PlaceAllies()
    {
        stepB_StartPointAlly01.PlaceCharacterOnIt(ally01);
        stepB_StartPointAlly02.PlaceCharacterOnIt(ally02);
        stepB_StartPointAlly03.PlaceCharacterOnIt(ally03);
        stepB_StartPointAlly04.PlaceCharacterOnIt(ally04);
        stepB_StartPointAlly05.PlaceCharacterOnIt(ally05);
    }

    void MakeAnbarDynamiteVisible()
    {
        stepA_AnbarDynamite01_Object.gameObject.SetActiveRecursively(false);
        stepA_AnbarDynamite01_Normal.gameObject.SetActiveRecursively(true);

        stepA_AnbarDynamiteTickTickAudioInfo_01.Play();

        mapLogic.playerCharNew.HUD_StartShowingLvlCampCounterClock();
    }

    void MakeAnbarDynamiteVisible_Step7Specific()
    {
        stepA_AnbarDynamite01_Object.gameObject.SetActiveRecursively(false);
        stepA_AnbarDynamite01_Normal.gameObject.SetActiveRecursively(true);

        stepA_AnbarDynamiteTickTickAudioInfo_01.Play();
        stepA_AnbarDynamiteTickTickAudioInfo_01.SetCustomVolume(anbarDynamiteTickTickSoundVolAfterRadioCut);
    }

    void RemoveAnbarDynamite()
    {
        stepA_AnbarDynamite01_Object.gameObject.SetActiveRecursively(false);
        stepA_AnbarDynamite01_Normal.gameObject.SetActiveRecursively(false);

        stepA_AnbarDynamiteTickTickAudioInfo_01.Stop();

        mapLogic.playerCharNew.HUD_StopShowingLvlCampCounterClock();
    }

    void ExplodeAnbarBomb()
    {
        if (!anbarBombIsExploded)
        {
            anbarBombIsExploded = true;
            RemoveAnbarDynamite();

            stepA_AnbarDynamiteExplosionAudioInfo.Play();
            stepA_AnbarDynamiteExplosionAudioInfo_02.Play();
            stepA_AnbarDynamiteExplosionFire.Play();
            stepA_AnbarExplosion.Explode();

            anbarExplosionFirstLightEnabled = true;
            stepA_AnbarExplosionLight.active = true;

            foreach (ParticleSystem prtcl in stepA_AnbarDynamiteExplosionParticles)
            {
                prtcl.Play();
            }

            mapLogic.SetPlayerIsDetectedInCampMode();

            StartAlarms();

            ShakePlayerCamByExplosion();

            shouldStopDemAnbarExpAprticlesAfterSecs = true;
        }
    }

    void ShakePlayerCamByExplosion()
    {
        mapLogic.playerCharNew.StartCustomExplosionCamShake(stepA_AnbarExplosion.transform, 70, 10, false);
    }

    void ShowDamagedRadioWiresAndStopPlayingRadioSounds()
    {
        stepA_RadioWiresObject.gameObject.SetActiveRecursively(false);
        stepA_RadioWiresNormal.SetActiveRecursively(true);

        stepA_MainHouseRadioAudioInfo.Stop();
    }

    void StartWhisperingAudioInfos()
    {
        for (int i = 0; i < whisperAudioInfos.Length; i++)
        {
            whisperAudioInfos[i].Play();
        }
    }

    void StartDecreasingVolumeOfWhisperingAudioInfos()
    {
        shouldDeacreseVolumeOfWhisperingAudios = true;
    }

    //

    public override void LoadCheckPoint(float _levelStep)
    {
        base.LoadCheckPoint(_levelStep);

        #region A_PartB
        if (levelStep == 2)
        {
            Load_A_PartB_PlacePlayer();
            return;
        }
        #endregion

        #region A_PartC
        if (levelStep == 3)
        {
            Load_A_PartC_PlacePlayer();
            return;
        }
        #endregion

        #region A_PartD
        if (levelStep == 4)
        {
            Load_A_PartD_PlacePlayer();
            return;
        }
        #endregion

        #region A_PartE
        if (levelStep == 5)
        {
            Load_A_PartE_PlacePlayer();

            MakeAnbarDynamiteVisible();

            return;
        }
        #endregion

        #region A_PartF
        if (levelStep == 6)
        {
            Load_A_PartF_PlacePlayer();

            MakeAnbarDynamiteVisible();

            mapLogic.playerCharNew.hud_LvlCamp_ClockTimeCounter = 60;

            return;
        }
        #endregion

        #region A_PartG
        if (levelStep == 7)
        {
            stepA_Trigger_MainHouseWin.StartOutStepIfNotStarted();

            Load_A_PartG_PlacePlayer();

            MakeAnbarDynamiteVisible_Step7Specific();

            ShowDamagedRadioWiresAndStopPlayingRadioSounds();

            OnlyStartAlarmLamps();

            areAlarmsStartedBefore = true;

            mapLogic.playerCharNew.ResetPlayerCampStatus(false);
            DestroyAllMapSolds();

            return;
        }
        #endregion
    }

    void Load_A_PartB_PlacePlayer()
    {
        stepA_StartPointB.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);
    }

    void Load_A_PartC_PlacePlayer()
    {
        stepA_StartPointC.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);
    }

    void Load_A_PartD_PlacePlayer()
    {
        stepA_StartPointD.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);
    }

    void Load_A_PartE_PlacePlayer()
    {
        stepA_StartPointE.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);
    }

    void Load_A_PartF_PlacePlayer()
    {
        stepA_StartPointF.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);
    }

    void Load_A_PartG_PlacePlayer()
    {
        stepA_StartPointG.PlaceCharacterOnIt(PlayerCharacterNew.Instance.gameObject);
    }
}
