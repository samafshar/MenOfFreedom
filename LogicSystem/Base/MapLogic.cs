//<91-04-12>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum The3DObjIconType
{
    Dot,
    FeleshRooBePayin,
    FeleshRooBeBala,
}

public class CharRayCastRsltForFightInPoInf
{
    public SoldierFightInPointInfo fightInPointInfo;
    public List<CharRaycastResult> targets = new List<CharRaycastResult>();

}

public class FightPointWithRating
{
    public FightPoint fightPoint = null;
    public float rating = 0;
}

public class CurveInfo
{
    public AntaresBezierCurve curve;
    public bool startToEnd = false;
    public bool endToStart = false;
}

public class MapLogic : MonoBehaviour
{
    public static MapLogic Instance;

    [HideInInspector]
    public List<GameObject> mapChars = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> mapAllyChars = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> mapEnemyChars = new List<GameObject>();
    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public PlayerCharacterNew playerCharNew;
    [HideInInspector]
    public CharacterInfo playerCharInfo;
    [HideInInspector]
    public GameObject[] mapPathCurves;

    [HideInInspector]
    public List<MapLogicJob> mapActiveLogicJobs = new List<MapLogicJob>();

    public LogicGUIHandler logicGUIHandler;

    //<Test>
    public MapLogicJob sceneLogic;
    //</Test>

    public BlackScreenFadeInOut blackScreenFader;

    public AudioInfo audioInfo_Music;

    public float campSoldierViewRangeCoef = 1;

    //public Transform example3DObj;

    float delayTimeToStartCounter = 0.11f;

    float step = 0;

    [HideInInspector]
    public bool isCutsceneMode = false;


    [HideInInspector]
    public bool isPlayerHidden = false;

    float time_PlayerHideCheck_Max = 0.7f;
    float time_PlayerVisibleCheck_Max = 0.7f;

    float time_PlayerVisibilityCheck_Counter = 0.7f;

    [HideInInspector]
    public bool isB2_SnipeMode = false;

    bool isEscapeKeyDownFromMenu = false;

    [HideInInspector]
    public bool nowInCutscene = false;

    List<GameObject> activeChars;

    //public GameObject[] initialAllyChars;
    //public GameObject[] initialEnemyChars;

    //Menu inGameMenu;

    [HideInInspector]
    public MenuParent ingameMenu;

    Camera[] mapAllCameras;

    CampLightArea[] campLightAreas;
    int numOfCampLightAreasInThisMap = 0;
    LogicTrigger[] campLightAreaLogTriggers;

    CampHiddenArea[] campHiddenAreas;
    int numOfCampHiddenAreasInThisMap = 0;
    LogicTrigger[] campHiddenAreaLogTriggers;

    CampBadArea[] campBadAreas;
    int numOfCampBadAreasInThisMap = 0;
    LogicTrigger[] campBadAreaLogTriggers;

    bool isPlayerDetectedInCampMode = false;

    [HideInInspector]
    public int camp_CurUpdate_IndexOfLightAreaThatContainsPlayer = -1;
    [HideInInspector]
    public int camp_CurUpdate_IndexOfHiddenAreaThatContainsPlayer = -1;
    [HideInInspector]
    public int camp_CurUpdate_IndexOfBadAreaThatContainsPlayer = -1;

    [HideInInspector]
    public float camp_CurUpdate_SoldsViewRange = 0;
    [HideInInspector]
    public float camp_CurUpdate_SoldsViewAngle = 0;

    float oldTimeScale = 1;

    [HideInInspector]
    public HUDParent mapHUDParent;

    [HideInInspector]
    public HUDGroup hudGroup_ObjectivesPage;

    [HideInInspector]
    public HUDGroup hudGroup_ObjectivesPageBG;

    [HideInInspector]
    public HUDControl hudControl_GameSaved;

    [HideInInspector]
    public HUDControl hudControl_CurrentMission;

    [HideInInspector]
    public HUDControl hudControl_MissionComplete;

    [HideInInspector]
    public HUDControl hudControl_MainMission;

    float hudTextAlphaIncSpeed = 5f;
    float hudTextAlphaDecSpeed = 5f;

    float hudNewObjectiveAlphaIncSpeed = 5f;
    float hudNewObjectiveAlphaDecSpeed = 2f;

    float hudTextUpToDownAdditionalY = 30;

    float hud_GameSaved_Duration = 5;
    float hud_NewMission_Duration = 7;
    float hud_MissionComplete_Duration = 2f;
    float hud_MainMissionComplete_Duration = 3;

    bool hud_IsBusyShowingMainMission = false;
    bool hud_IsBusyShowingNewMission = false;
    bool hud_IsBusyShowingMissionComplete = false;

    bool hud_ShouldShowNewMission = false;
    int hud_SelectedNewMissionIndexToShow = 0;

    bool hud_ShouldShowMissionComplete = false;
    int hud_SelectedMissionCompleteIndexToShow = 0;

    bool hud_ShouldShowCompletedMainMission = false;
    bool hud_IsMainMissionCompleted = false;
    bool hud_NextMainMissionIsCompletedOne = false;

    [HideInInspector]
    public List<PlayerGrenade> mapActiveGrenades = new List<PlayerGrenade>();

    [HideInInspector]
    public bool doNotShowGameSavedMessageForOneTime = false;

    public The3DObjective hud_3DObjTypeSource_Dot;
    public The3DObjective hud_3DObjTypeSource_FeleshRooBeBala;
    public The3DObjective hud_3DObjTypSource_FeleshRooBePayin;

    [HideInInspector]
    public List<The3DObjective> hud_CurrentActive3DObjectives = new List<The3DObjective>();

    [HideInInspector]
    public bool isShowingObjectivesPageByHoldingTabKeyByPlayer = false;

    [HideInInspector]
    public List<DeadSoldierBMW> mapDeadSoldierBMWs = new List<DeadSoldierBMW>();

    HUDControl curInGameHint = null;
    HUDControl curInGameHintBlackBG = null;

    HUDGroupName neededHint_GroupName = HUDGroupName._noName;
    HUDControlName neededHint_HintName = HUDControlName._noName;
    HUDControlName neededHint_BlackBGName = HUDControlName._noName;

    bool isGamePausedForShowingHint = false;

    bool showingAHintIsNeeded = false;

    [HideInInspector]
    public SSAOEffect mapSSAO;

    [HideInInspector]
    public BloomAndLensFlares mapBloom;

    void Awake()
    {
        //Screen.lockCursor = true;

        Instance = this;

        mapSSAO = GetComponent<SSAOEffect>();
        mapBloom = GetComponent<BloomAndLensFlares>();

        mapHUDParent = GameObject.FindObjectOfType(typeof(HUDParent)) as HUDParent;

        hudGroup_ObjectivesPage = mapHUDParent.GetChildGroupByName(HUDGroupName.LevelObjectives);
        hudGroup_ObjectivesPageBG = mapHUDParent.GetChildGroupByName(HUDGroupName.LevelObjectivesBG);

        hudControl_GameSaved = mapHUDParent.GetChildGroupByName(HUDGroupName.GameSaved).GetChildControlByName(HUDControlName.GameSaved);
        hudControl_CurrentMission = mapHUDParent.GetChildGroupByName(HUDGroupName.CurrentMission).GetChildControlByName(HUDControlName.CurrentMission);
        hudControl_MissionComplete = mapHUDParent.GetChildGroupByName(HUDGroupName.MissionComplete).GetChildControlByName(HUDControlName.MissionComplete);
        hudControl_MainMission = mapHUDParent.GetChildGroupByName(HUDGroupName.MainMission).GetChildControlByName(HUDControlName.MainMission);

        GameController.currentMapLogic = this;

        GameController.ResetSettingsForNewScene(true);

        mapPathCurves = GameObject.FindGameObjectsWithTag("PathCurve");

        if (PlayerCharacterNew.Instance)
            player = PlayerCharacterNew.Instance.gameObject;

        if (player != null)
        {
            playerCharNew = player.GetComponent<PlayerCharacterNew>();
            playerCharInfo = player.GetComponent<CharacterInfo>();
        }

        blackScreenFader.StartFadingIn();

        ingameMenu = GameObject.FindGameObjectWithTag("InGameMenu").GetComponent<MenuParent>();

        mapAllCameras = GameObject.FindSceneObjectsOfType(typeof(Camera)) as Camera[];

        campLightAreas = GameObject.FindSceneObjectsOfType(typeof(CampLightArea)) as CampLightArea[];

        if (campLightAreas != null && campLightAreas.Length > 0)
        {
            numOfCampLightAreasInThisMap = campLightAreas.Length;

            campLightAreaLogTriggers = new LogicTrigger[numOfCampLightAreasInThisMap];

            for (int i = 0; i < numOfCampLightAreasInThisMap; i++)
            {
                campLightAreaLogTriggers[i] = campLightAreas[i].gameObject.GetComponent<LogicTrigger>();
            }
        }

        campHiddenAreas = GameObject.FindSceneObjectsOfType(typeof(CampHiddenArea)) as CampHiddenArea[];

        if (campHiddenAreas != null && campHiddenAreas.Length > 0)
        {
            numOfCampHiddenAreasInThisMap = campHiddenAreas.Length;

            campHiddenAreaLogTriggers = new LogicTrigger[numOfCampHiddenAreasInThisMap];

            for (int i = 0; i < numOfCampHiddenAreasInThisMap; i++)
            {
                campHiddenAreaLogTriggers[i] = campHiddenAreas[i].gameObject.GetComponent<LogicTrigger>();
            }
        }

        campBadAreas = GameObject.FindSceneObjectsOfType(typeof(CampBadArea)) as CampBadArea[];

        if (campBadAreas != null && campBadAreas.Length > 0)
        {
            numOfCampBadAreasInThisMap = campBadAreas.Length;

            campBadAreaLogTriggers = new LogicTrigger[numOfCampBadAreasInThisMap];

            for (int i = 0; i < numOfCampBadAreasInThisMap; i++)
            {
                campBadAreaLogTriggers[i] = campBadAreas[i].gameObject.GetComponent<LogicTrigger>();
            }
        }

        //<Test>
        GameSaveLoadController.LoadGameState();
        GameSaveLoadController.LoadPlayerState();
        //</Test>
    }

    void Start()
    {
        hudGroup_ObjectivesPageBG.SetAlphaOfAllChilds(1);

        //HUD_ObjectivesPage_SetObjectiveDone(10);

        //playerCharNew.HUD_Add3DObj(example3DObj, example3DObj.position, "aaasdsa");
    }

    float ctr = 3;

    void Update()
    {
        //int j = 0;
        //for (int i = 0; i < 11000000; i++)
        //{
        //    if (i < 160000000)
        //        j++;
        //}

        //ctr -= Time.deltaTime;

        //if (ctr <= 0)
        //{
        //    ctr = 1000;

        //playerCharNew.HUD_Add3DObj(example3DObj, example3DObj.position, "aaasdsa");

        //HUD_ShowGameSaved();
        //HUD_ShowNewMission(0);
        //}

        //if (ctr > 100 && ctr <= 998)
        //{
        //    ctr = 1000000;

        //    HUD_ShowCompleteMission(0);
        //    HUD_ShowNewMission(1);
        //}

        //if (ctr > 900000 && ctr <= 999995)
        //{
        //    ctr = 100000000;

        //    HUD_ShowCompleteMission(1);
        //    HUD_ShowMainMission(true);
        //}

        //

        if (isEscapeKeyDownFromMenu)
        {
            if (CustomInputManager.KeyUp_Escape()) //(GameController.GetButtonUp(GeneralStats.key_Escape))
                isEscapeKeyDownFromMenu = false;
        }

        if (CustomInputManager.KeyDownIfGameIsNotPaused_Escape() && !isEscapeKeyDownFromMenu) //(GameController.GetButtonDown_IfGameIsNotPaused(GeneralStats.key_Escape) && !isEscapeKeyDownFromMenu)
        {
            PauseGame();
        }

        if (isGamePausedForShowingHint)
        {
            if (CustomInputManager.KeyDown_SkipHints()) //(GameController.GetKeyDown(KeyCode.Return))
            {
                UnPauseGameAndRemoveHint();
            }
        }

        if (showingAHintIsNeeded)
        {
            if (!GameController.isGamePaused)
            {
                PauseGameAndJustShowHint(neededHint_GroupName, neededHint_BlackBGName, neededHint_HintName);
            }
        }

        //if (Input.GetKeyDown(KeyCode.P) && !GameController.isGamePaused && !isEscapeKeyDownFromMenu)
        //{
        //    PauseGame();
        //}

        SlowlyCheckPlayerVisibility();

        #region step == 0, Start delay time before scene logic start
        if (step == 0)
        {
            delayTimeToStartCounter = MathfPlus.DecByDeltatimeToZero(delayTimeToStartCounter);

            if (delayTimeToStartCounter == 0)
                step = 1f;
        }
        #endregion

        #region step == 1, Start scene logic
        if (step == 1)
        {
            if (sceneLogic != null)
            {
                sceneLogic.StartIt();

                if (numOfCampLightAreasInThisMap > 0)
                {
                    for (int i = 0; i < numOfCampLightAreasInThisMap; i++)
                    {
                        campLightAreaLogTriggers[i].SetEnabled(true);
                    }
                }

                if (numOfCampHiddenAreasInThisMap > 0)
                {
                    for (int i = 0; i < numOfCampHiddenAreasInThisMap; i++)
                    {
                        campHiddenAreaLogTriggers[i].SetEnabled(true);
                    }
                }

                if (numOfCampBadAreasInThisMap > 0)
                {
                    for (int i = 0; i < numOfCampBadAreasInThisMap; i++)
                    {
                        campBadAreaLogTriggers[i].SetEnabled(true);
                    }
                }

                step = 2;
            }
        }
        #endregion

        #region step == 2, Run scene logic
        if (step == 2)
        {
            sceneLogic.RunIt();

            if (playerCharNew.isCampPlayer)
            {

                camp_CurUpdate_IndexOfLightAreaThatContainsPlayer = GetIndexOfCampLightAreaThatContainsPlayer();
                camp_CurUpdate_IndexOfHiddenAreaThatContainsPlayer = GetIndexOfCampHiddenAreaThatContainsPlayer();
                camp_CurUpdate_IndexOfBadAreaThatContainsPlayer = GetIndexOfCampBadAreaThatContainsPlayer();

                camp_CurUpdate_SoldsViewRange = GetCampCurrentSoldsViewRange();
                camp_CurUpdate_SoldsViewAngle = GetCampCurrentSoldsViewAngle();


                if (camp_CurUpdate_IndexOfBadAreaThatContainsPlayer >= 0)
                {
                    SetPlayerIsDetectedInCampMode();
                }
            }
        }
        #endregion


        #region HUD_GameSaved

        if (hudControl_GameSaved.isControlVisible)
        {
            if (!HUD_ShouldFinishGameSavedHUD())
            {
                #region Starting
                if (hudControl_GameSaved.IsOutStep(HUDOutStep.Starting))
                {
                    hudControl_GameSaved.SetAlpha(hudControl_GameSaved.alpha + hudTextAlphaIncSpeed * Time.deltaTime);

                    hudControl_GameSaved.additionalY = hudControl_GameSaved.alpha * hudTextUpToDownAdditionalY;
                    hudControl_GameSaved.ReInitRect();

                    if (hudControl_GameSaved.alphaStatus == HUDAlphaStat.Full)
                    {
                        hudControl_GameSaved.SetOutCounter(hud_GameSaved_Duration);
                        hudControl_GameSaved.SetOutStep(HUDOutStep.RunningA);
                    }
                }
                #endregion

                #region RunningA
                if (hudControl_GameSaved.IsOutStep(HUDOutStep.RunningA))
                {
                    hudControl_GameSaved.DecOutCounterByTime_ToZero();

                    if (hudControl_GameSaved.outCounter == 0)
                    {
                        hudControl_GameSaved.SetOutStep(HUDOutStep.Finishing);
                    }
                }
                #endregion

                #region Finishing
                if (hudControl_GameSaved.IsOutStep(HUDOutStep.Finishing))
                {
                    hudControl_GameSaved.SetAlpha(hudControl_GameSaved.alpha - hudTextAlphaDecSpeed * Time.deltaTime);

                    hudControl_GameSaved.additionalY = hudControl_GameSaved.alpha * hudTextUpToDownAdditionalY;
                    hudControl_GameSaved.ReInitRect();

                    if (hudControl_GameSaved.alphaStatus == HUDAlphaStat.Zero)
                    {
                        hudControl_GameSaved.SetOutStep(HUDOutStep.Finished);

                        hudControl_GameSaved.SetIsVisible(false);
                        hudControl_GameSaved.SetAlpha(0);
                        hudControl_GameSaved.SetOutCounter(0);

                        hudControl_GameSaved.additionalY = 0;
                        hudControl_GameSaved.ReInitRect();
                    }
                }
                #endregion
            }
            else
            {
                hudControl_GameSaved.SetAlpha(hudControl_GameSaved.alpha - hudTextAlphaDecSpeed * Time.deltaTime);

                hudControl_GameSaved.additionalY = hudControl_GameSaved.alpha * hudTextUpToDownAdditionalY;
                hudControl_GameSaved.ReInitRect();

                if (hudControl_GameSaved.alphaStatus == HUDAlphaStat.Zero)
                {
                    hudControl_GameSaved.SetOutStep(HUDOutStep.Finished);

                    hudControl_GameSaved.SetIsVisible(false);
                    hudControl_GameSaved.SetAlpha(0);
                    hudControl_GameSaved.SetOutCounter(0);

                    hudControl_GameSaved.additionalY = 0;
                    hudControl_GameSaved.ReInitRect();
                }
            }
        }

        #endregion


        #region hud_ShouldShowMissionComplete
        if (!hud_IsBusyShowingMissionComplete && !hud_IsBusyShowingNewMission)
        {
            if (hud_ShouldShowMissionComplete)
            {
                HUD_ShowCompleteMission(hud_SelectedMissionCompleteIndexToShow);
                hud_ShouldShowMissionComplete = false;
            }
        }
        #endregion

        #region hud_ShouldShowNewMission
        if (!blackScreenFader.IsFadingIn() && !hud_IsBusyShowingMissionComplete && !hud_IsBusyShowingNewMission)
        {
            if (hud_ShouldShowNewMission)
            {
                HUD_ShowNewMission(hud_SelectedNewMissionIndexToShow);
                hud_ShouldShowNewMission = false;
            }
        }
        #endregion

        #region hud_ShouldShowMainMission
        if (!hud_IsBusyShowingMainMission)
        {
            if (hud_ShouldShowCompletedMainMission)
            {
                HUD_ShowMainMission(true);
                hud_ShouldShowCompletedMainMission = false;
            }
        }
        #endregion


        #region HUD_MainMission
        if (hud_IsBusyShowingMainMission)
        {
            if (!HUD_ShouldFinishMainMissionHUD())
            {
                #region Starting
                if (hudControl_MainMission.IsOutStep(HUDOutStep.Starting))
                {
                    if (isShowingObjectivesPageByHoldingTabKeyByPlayer)
                    {
                        hudControl_MainMission.SetIsVisible(false);
                    }
                    else
                    {
                        hudControl_MainMission.SetIsVisible(true);
                    }

                    hudControl_MainMission.SetAlpha(hudControl_MainMission.alpha + Time.deltaTime * hudNewObjectiveAlphaIncSpeed);

                    hudControl_MainMission.additionalY = hudControl_MainMission.alpha * hudTextUpToDownAdditionalY;
                    hudControl_MainMission.ReInitRect();

                    if (hudControl_MainMission.alphaStatus == HUDAlphaStat.Full)
                    {
                        hudControl_MainMission.SetOutCounter(hud_MainMissionComplete_Duration);
                        hudControl_MainMission.SetOutStep(HUDOutStep.RunningA);
                    }
                }
                #endregion

                #region RunningA
                if (hudControl_MainMission.IsOutStep(HUDOutStep.RunningA))
                {
                    if (isShowingObjectivesPageByHoldingTabKeyByPlayer)
                    {
                        hudControl_MainMission.SetIsVisible(false);
                    }
                    else
                    {
                        hudControl_MainMission.SetIsVisible(true);
                    }

                    if (hud_IsMainMissionCompleted)
                        hudControl_MainMission.DecOutCounterByTime_ToZero();

                    if (hudControl_MainMission.outCounter == 0)
                    {
                        hudControl_MainMission.SetOutStep(HUDOutStep.Finishing);
                    }
                }
                #endregion

                #region Finishing
                if (hudControl_MainMission.IsOutStep(HUDOutStep.Finishing))
                {
                    if (isShowingObjectivesPageByHoldingTabKeyByPlayer)
                    {
                        hudControl_MainMission.SetIsVisible(false);
                    }
                    else
                    {
                        hudControl_MainMission.SetIsVisible(true);
                    }

                    hudControl_MainMission.SetAlpha(hudControl_MainMission.alpha - Time.deltaTime * hudNewObjectiveAlphaDecSpeed);

                    hudControl_MainMission.additionalY = hudControl_MainMission.alpha * hudTextUpToDownAdditionalY;
                    hudControl_MainMission.ReInitRect();

                    if (hudControl_MainMission.alphaStatus == HUDAlphaStat.Zero)
                    {
                        hud_IsBusyShowingMainMission = false;
                        hudControl_MainMission.SetOutStep(HUDOutStep.Finished);

                        hudControl_MainMission.SetIsVisible(false);
                        hudControl_MainMission.SetAlpha(0);
                        hudControl_MainMission.SetOutCounter(0);

                        hudControl_MainMission.additionalY = 0;
                        hudControl_MainMission.ReInitRect();
                    }
                }
                #endregion
            }
            else
            {
                hudControl_MainMission.SetAlpha(hudControl_MainMission.alpha - Time.deltaTime * hudTextAlphaDecSpeed);

                hudControl_MainMission.additionalY = hudControl_MainMission.alpha * hudTextUpToDownAdditionalY;
                hudControl_MainMission.ReInitRect();

                if (hudControl_MainMission.alphaStatus == HUDAlphaStat.Zero)
                {
                    hud_IsBusyShowingMainMission = false;
                    hudControl_MainMission.SetOutStep(HUDOutStep.Finished);

                    hudControl_MainMission.SetIsVisible(false);
                    hudControl_MainMission.SetAlpha(0);
                    hudControl_MainMission.SetOutCounter(0);

                    hudControl_MainMission.additionalY = 0;
                    hudControl_MainMission.ReInitRect();
                }

            }
        }

        #endregion

        #region HUD_NewMission
        if (hud_IsBusyShowingNewMission)
        {
            if (!HUD_ShouldFinishNewMissionHUD())
            {
                #region Starting
                if (hudControl_CurrentMission.IsOutStep(HUDOutStep.Starting))
                {
                    if (isShowingObjectivesPageByHoldingTabKeyByPlayer)
                    {
                        hudControl_CurrentMission.SetIsVisible(false);
                    }
                    else
                    {
                        hudControl_CurrentMission.SetIsVisible(true);
                    }

                    hudControl_CurrentMission.SetAlpha(hudControl_CurrentMission.alpha + Time.deltaTime * hudNewObjectiveAlphaIncSpeed);

                    hudControl_CurrentMission.additionalY = hudControl_CurrentMission.alpha * hudTextUpToDownAdditionalY;
                    hudControl_CurrentMission.ReInitRect();

                    if (hudControl_CurrentMission.alphaStatus == HUDAlphaStat.Full)
                    {
                        hudControl_CurrentMission.SetOutCounter(hud_NewMission_Duration);
                        hudControl_CurrentMission.SetOutStep(HUDOutStep.RunningA);
                    }
                }
                #endregion

                #region RunningA
                if (hudControl_CurrentMission.IsOutStep(HUDOutStep.RunningA))
                {
                    if (isShowingObjectivesPageByHoldingTabKeyByPlayer)
                    {
                        hudControl_CurrentMission.SetIsVisible(false);
                    }
                    else
                    {
                        hudControl_CurrentMission.SetIsVisible(true);
                    }

                    hudControl_CurrentMission.DecOutCounterByTime_ToZero();

                    if (hudControl_CurrentMission.outCounter == 0)
                    {
                        hudControl_CurrentMission.SetOutStep(HUDOutStep.Finishing);
                    }
                }
                #endregion

                #region Finishing
                if (hudControl_CurrentMission.IsOutStep(HUDOutStep.Finishing))
                {
                    if (isShowingObjectivesPageByHoldingTabKeyByPlayer)
                    {
                        hudControl_CurrentMission.SetIsVisible(false);
                    }
                    else
                    {
                        hudControl_CurrentMission.SetIsVisible(true);
                    }

                    hudControl_CurrentMission.SetAlpha(hudControl_CurrentMission.alpha - Time.deltaTime * hudNewObjectiveAlphaDecSpeed);

                    hudControl_CurrentMission.additionalY = hudControl_CurrentMission.alpha * hudTextUpToDownAdditionalY;
                    hudControl_CurrentMission.ReInitRect();

                    if (hudControl_CurrentMission.alphaStatus == HUDAlphaStat.Zero)
                    {
                        hud_IsBusyShowingNewMission = false;
                        hudControl_CurrentMission.SetOutStep(HUDOutStep.Finished);

                        hudControl_CurrentMission.SetIsVisible(false);
                        hudControl_CurrentMission.SetAlpha(0);
                        hudControl_CurrentMission.SetOutCounter(0);

                        hudControl_CurrentMission.additionalY = 0;
                        hudControl_CurrentMission.ReInitRect();
                    }
                }
                #endregion
            }
            else
            {
                hudControl_CurrentMission.SetAlpha(hudControl_CurrentMission.alpha - Time.deltaTime * hudTextAlphaDecSpeed);

                hudControl_CurrentMission.additionalY = hudControl_CurrentMission.alpha * hudTextUpToDownAdditionalY;
                hudControl_CurrentMission.ReInitRect();

                if (hudControl_CurrentMission.alphaStatus == HUDAlphaStat.Zero)
                {
                    hud_IsBusyShowingNewMission = false;
                    hudControl_CurrentMission.SetOutStep(HUDOutStep.Finished);

                    hudControl_CurrentMission.SetIsVisible(false);
                    hudControl_CurrentMission.SetAlpha(0);
                    hudControl_CurrentMission.SetOutCounter(0);

                    hudControl_CurrentMission.additionalY = 0;
                    hudControl_CurrentMission.ReInitRect();
                }

            }
        }

        #endregion

        #region HUD_MissionComplete
        if (hud_IsBusyShowingMissionComplete)
        {
            if (!HUD_ShouldFinishMissionCompleteHUD())
            {
                #region Starting
                if (hudControl_MissionComplete.IsOutStep(HUDOutStep.Starting))
                {
                    if (isShowingObjectivesPageByHoldingTabKeyByPlayer)
                    {
                        hudControl_MissionComplete.SetIsVisible(false);
                    }
                    else
                    {
                        hudControl_MissionComplete.SetIsVisible(true);
                    }

                    hudControl_MissionComplete.SetAlpha(hudControl_MissionComplete.alpha + Time.deltaTime * hudTextAlphaIncSpeed);

                    hudControl_MissionComplete.additionalY = hudControl_MissionComplete.alpha * hudTextUpToDownAdditionalY;
                    hudControl_MissionComplete.ReInitRect();

                    if (hudControl_MissionComplete.alphaStatus == HUDAlphaStat.Full)
                    {
                        hudControl_MissionComplete.SetOutCounter(hud_MissionComplete_Duration);
                        hudControl_MissionComplete.SetOutStep(HUDOutStep.RunningA);
                    }
                }
                #endregion

                #region RunningA
                if (hudControl_MissionComplete.IsOutStep(HUDOutStep.RunningA))
                {
                    if (isShowingObjectivesPageByHoldingTabKeyByPlayer)
                    {
                        hudControl_MissionComplete.SetIsVisible(false);
                    }
                    else
                    {
                        hudControl_MissionComplete.SetIsVisible(true);
                    }

                    hudControl_MissionComplete.DecOutCounterByTime_ToZero();

                    if (hudControl_MissionComplete.outCounter == 0)
                    {
                        hudControl_MissionComplete.SetOutStep(HUDOutStep.Finishing);
                    }
                }
                #endregion

                #region Finishing
                if (hudControl_MissionComplete.IsOutStep(HUDOutStep.Finishing))
                {
                    if (isShowingObjectivesPageByHoldingTabKeyByPlayer)
                    {
                        hudControl_MissionComplete.SetIsVisible(false);
                    }
                    else
                    {
                        hudControl_MissionComplete.SetIsVisible(true);
                    }

                    hudControl_MissionComplete.SetAlpha(hudControl_MissionComplete.alpha - Time.deltaTime * hudTextAlphaDecSpeed);

                    hudControl_MissionComplete.additionalY = hudControl_MissionComplete.alpha * hudTextUpToDownAdditionalY;
                    hudControl_MissionComplete.ReInitRect();

                    if (hudControl_MissionComplete.alphaStatus == HUDAlphaStat.Zero)
                    {
                        hud_IsBusyShowingMissionComplete = false;
                        hudControl_MissionComplete.SetOutStep(HUDOutStep.Finished);

                        hudControl_MissionComplete.SetIsVisible(false);
                        hudControl_MissionComplete.SetAlpha(0);
                        hudControl_MissionComplete.SetOutCounter(0);

                        hudControl_MissionComplete.additionalY = 0;
                        hudControl_MissionComplete.ReInitRect();
                    }
                }
                #endregion
            }
            else
            {
                hudControl_MissionComplete.SetAlpha(hudControl_MissionComplete.alpha - Time.deltaTime * hudTextAlphaDecSpeed);

                hudControl_MissionComplete.additionalY = hudControl_MissionComplete.alpha * hudTextUpToDownAdditionalY;
                hudControl_MissionComplete.ReInitRect();

                if (hudControl_MissionComplete.alphaStatus == HUDAlphaStat.Zero)
                {
                    hud_IsBusyShowingMissionComplete = false;
                    hudControl_MissionComplete.SetOutStep(HUDOutStep.Finished);

                    hudControl_MissionComplete.SetIsVisible(false);
                    hudControl_MissionComplete.SetAlpha(0);
                    hudControl_MissionComplete.SetOutCounter(0);

                    hudControl_MissionComplete.additionalY = 0;
                    hudControl_MissionComplete.ReInitRect();
                }

            }
        }

        #endregion
    }

    public void AddAllyChar(GameObject obj)
    {
        if (!mapAllyChars.Contains(obj))
        {
            mapAllyChars.Add(obj);
        }
    }
    public void AddEnemyChar(GameObject obj)
    {
        if (!mapEnemyChars.Contains(obj))
        {
            mapEnemyChars.Add(obj);
        }
    }
    public void AddChar(GameObject obj)
    {
        if (mapChars.Contains(obj))
            return;

        mapChars.Add(obj);

        CharacterInfo charInfo = obj.GetComponent<CharacterInfo>();

        if (charInfo.FightSide == FightSideEnum.Ally)
        {
            if (!mapAllyChars.Contains(obj))
                AddAllyChar(obj);
        }
        else
        {
            if (!mapEnemyChars.Contains(obj))
                AddEnemyChar(obj);
        }
    }
    public void RemoveChar(GameObject obj)
    {
        if (mapChars.Contains(obj))
            mapChars.Remove(obj);

        if (mapAllyChars.Contains(obj))
            mapAllyChars.Remove(obj);

        if (mapEnemyChars.Contains(obj))
            mapEnemyChars.Remove(obj);
    }

    public void AddActiveLogicJob(MapLogicJob _mapLogicJob)
    {
        if (!mapActiveLogicJobs.Contains(_mapLogicJob))
            mapActiveLogicJobs.Add(_mapLogicJob);
    }

    public void RemoveActiveLogicJob(MapLogicJob _mapLogicJob)
    {
        if (mapActiveLogicJobs.Contains(_mapLogicJob))
            mapActiveLogicJobs.Remove(_mapLogicJob);
    }

    public List<GameObject> GetMapFriendSideChars(FightSideEnum _selfSide)
    {
        if (_selfSide == FightSideEnum.Ally)
            return mapAllyChars;
        else
            return mapEnemyChars;
    }
    public List<GameObject> GetMapOppositeSideChars(FightSideEnum _selfSide)
    {
        if (_selfSide == FightSideEnum.Ally)
            return mapEnemyChars;
        else
            return mapAllyChars;
    }

    public List<CharRaycastResult> GetAttackableEnemies(GameObject sourceSoldier, Vector3 rayCastPos, Quaternion rayCastPosRotation, float range, float angleRangeStart, float angleRangeEnd)
    {
        CharacterInfo sourceCharInfo = sourceSoldier.GetComponent<CharacterInfo>();
        List<GameObject> oppositeChars = GetMapOppositeSideChars(sourceCharInfo.FightSide);

        return GetAttackableCharsFromList(oppositeChars, sourceSoldier, rayCastPos, rayCastPosRotation, range, angleRangeStart, angleRangeEnd);
    }

    public List<CharRaycastResult> GetAttackableCharsFromList(List<GameObject> charactersList, GameObject sourceSoldier, Vector3 rayCastPos, Quaternion rayCastPosRotation, float range, float angleRangeStart, float angleRangeEnd)
    {
        List<CharRaycastResult> raycastResults = new List<CharRaycastResult>();

        for (int i = 0; i < charactersList.Count; i++)
        {
            GameObject charac = charactersList[i];
            CharRaycastResult rcRes;
            if (IsCharacterOkAsTarget(sourceSoldier, charac, rayCastPos, rayCastPosRotation, range, angleRangeStart, angleRangeEnd, out rcRes))
                raycastResults.Add(rcRes);
        }

        return raycastResults;
    }

    public List<CharRaycastResult> RateEnemiesAndSort(List<CharRaycastResult> enemyList, GameObject sourceSoldier, Vector3 sourcePos)
    {
        SoldierInfo sourceSoldInfo = sourceSoldier.GetComponent<SoldierInfo>();

        for (int i = 0; i < enemyList.Count; i++)
        {
            CharRaycastResult enemy = enemyList[i];
            enemy.rating = 0;

            if (!IsCharacterTotallyFightable(enemy.character))
                continue;

            float rating = 0;

            float rnd = Random.Range(0.92f, 1.08f);

            Vector3 dist = enemy.character.transform.position - sourcePos;
            float fightRange = 40;

            float distMag = dist.magnitude;

            rating += fightRange;

            if (distMag <= fightRange)
            {
                rating += 2.5f * fightRange - distMag;
            }

            if (enemy.character.tag.ToLower() == GeneralStats.playerTagName_ToLower)
                rating *= 1.15f;

            for (int j = 0; j < enemy.character.GetComponent<CharacterInfo>().targettingEnemiesCount; j++)
            {
                rating *= 0.95f;
            }

            if (!enemy.isCharacterHitted)
                rating *= 0.85f;

            rating *= rnd;

            enemy.rating = rating;
        }

        List<CharRaycastResult> unsortedList = enemyList;

        List<CharRaycastResult> sortedList = new List<CharRaycastResult>();

        while (unsortedList.Count != 0)
        {
            float maxRating = float.NegativeInfinity;
            int selectedIndex = -1;

            for (int i = 0; i < unsortedList.Count; i++)
            {
                if (unsortedList[i].rating > maxRating)
                {
                    maxRating = unsortedList[i].rating;
                    selectedIndex = i;
                }
            }

            sortedList.Add(unsortedList[selectedIndex]);
            unsortedList.RemoveAt(selectedIndex);
        }

        return sortedList;
    }

    public bool IsCharacterTotallyFightable(GameObject targetCharacter)
    {
        if (targetCharacter == null)
            return false;

        CharacterInfo targetCharInfo = targetCharacter.GetComponent<CharacterInfo>();

        if (!targetCharInfo.IsAttackable())
            return false;

        return true;
    }

    public bool IsCharacterOkAsTarget(GameObject sourceSoldier, GameObject targetCharacter, Vector3 rayCastPos, Quaternion rayCastPosRotation, float viewRadius, float angleRangeStart, float angleRangeEnd, out CharRaycastResult raycastResult)
    {
        SoldierInfo sourceSoldInfo = sourceSoldier.GetComponent<SoldierInfo>();

        CharRaycastResult res = new CharRaycastResult();
        res.character = targetCharacter;
        raycastResult = res;

        if (!IsCharacterTotallyFightable(targetCharacter))
            return false;

        if (!GeneralStats.IsVecInView(targetCharacter.transform.position, rayCastPos, rayCastPosRotation, angleRangeStart, angleRangeEnd, viewRadius))
            return false;

        List<Transform> characterHittedPoses = new List<Transform>();
        List<Transform> haloHittedPoses = new List<Transform>();

        Vector3 raycastOrigin = rayCastPos;
        float raycastRange = sourceSoldInfo.fightRange;

        CharacterInfo targetCharInfo = targetCharacter.GetComponent<CharacterInfo>();

        //Character
        foreach (Transform tr in targetCharInfo.shootRaycastTargets)
        {
            RaycastHit hit;

            Vector3 raycastDirection = tr.position - rayCastPos;
            float rayDirMag = raycastDirection.magnitude;

            if (raycastRange < rayDirMag)
                continue;

            float range = rayDirMag;
            Vector3 raycastStart = raycastOrigin;

            while (true)
            {

                if (!Physics.Raycast(raycastStart, raycastDirection, out hit, range, GameGeneralInfo.Instance.SoldierRaycastLayer))
                {
                    characterHittedPoses.Add(tr);
                    break;
                }


                if (hit.transform.root.gameObject == sourceSoldier)
                {
                    raycastStart = hit.point + 0.01f * raycastDirection.normalized;
                    range = (tr.position - hit.point).magnitude;
                    continue;
                }

                if (hit.transform.root.gameObject == targetCharacter)
                {
                    characterHittedPoses.Add(tr);
                }

                break;
            }
        }

        //Halo
        foreach (Transform tr in targetCharInfo.fightHaloRaycastTargets)
        {
            RaycastHit hit;

            Vector3 raycastDirection = tr.position - rayCastPos;
            float rayDirMag = raycastDirection.magnitude;

            if (raycastRange < rayDirMag)
                continue;

            float range = rayDirMag;
            Vector3 raycastStart = raycastOrigin;

            while (true)
            {

                if (!Physics.Raycast(raycastStart, raycastDirection, out hit, range, GameGeneralInfo.Instance.SoldierRaycastLayer))
                {
                    haloHittedPoses.Add(tr);
                    break;
                }

                if (hit.transform.root.gameObject == sourceSoldier)
                {
                    raycastStart = hit.point + 0.01f * raycastDirection.normalized;
                    range = (tr.position - hit.point).magnitude;
                    continue;
                }

                break;
            }
        }

        bool result = false;

        if (characterHittedPoses.Count > 0)
        {
            res.characterHittedPoses = characterHittedPoses;
            res.isCharacterHitted = true;
            result = true;
        }

        if (haloHittedPoses.Count > 0)
        {
            res.haloHittedPoses = haloHittedPoses;
            res.isHaloHitted = true;
            result = true;
        }

        raycastResult = res;

        return result;
    }

    public List<CharRaycastResult> GetAttackableEnemiesForFightInfo(SoldierFightInPointInfo _fightInfo, GameObject _sourceSoldier, List<GameObject> _initialEnemies, Vector3 _fightPos, Quaternion _fightRot)
    {
        SoldierFightInPointInfo fightInfo = _fightInfo;
        GameObject sold = _sourceSoldier;
        SoldierInfo soldInfo = sold.GetComponent<SoldierInfo>();
        Vector3 fightPos = _fightPos;
        Quaternion fightRot = _fightRot;
        Vector3 gunRaycastOffset = fightInfo.GetRaycastOffsetForGun(soldInfo.gun.name);
        List<GameObject> initialEnemies = _initialEnemies;
        float range = soldInfo.fightRange;

        Vector3 rayCastPos = SoldierStats.GetShootingRaycastPos(fightPos, fightRot, gunRaycastOffset);
        Quaternion raycastPosRot = fightRot;

        List<CharRaycastResult> result;

        if (initialEnemies != null && initialEnemies.Count > 0)
        {
            result = GetAttackableCharsFromList(initialEnemies, sold, rayCastPos, raycastPosRot, range, fightInfo.shootingStartAngle, fightInfo.shootingEndAngle);
        }
        else
        {
            result = GetAttackableEnemies(sold, rayCastPos, raycastPosRot, range, fightInfo.shootingStartAngle, fightInfo.shootingEndAngle);
        }

        result = RateEnemiesAndSort(result, sold, rayCastPos);

        return result;
    }

    public List<CharRayCastRsltForFightInPoInf> GetAllAttackableEnemiesForListOfFightInfos(SoldierFightInPointInfo[] _fightInfos, GameObject _sourceSoldier, List<GameObject> _initialEnemies, Vector3 _fightPos, Quaternion _fightRot)
    {
        List<CharRayCastRsltForFightInPoInf> results = new List<CharRayCastRsltForFightInPoInf>();

        foreach (SoldierFightInPointInfo fightInf in _fightInfos)
        {
            CharRayCastRsltForFightInPoInf newRes = new CharRayCastRsltForFightInPoInf();
            newRes.fightInPointInfo = fightInf;
            newRes.targets = GetAttackableEnemiesForFightInfo(fightInf, _sourceSoldier, _initialEnemies, _fightPos, _fightRot);

            results.Add(newRes);
        }

        return results;
    }

    public float GetSumOfRatings(List<CharRaycastResult> _raycastResults)
    {
        List<CharRaycastResult> raycastResults = _raycastResults;

        float sumOfRatings = 0;

        foreach (CharRaycastResult rcRslt in raycastResults)
        {
            sumOfRatings += rcRslt.rating;
        }

        return sumOfRatings;
    }

    public float GetSumOfRatings(List<CharRayCastRsltForFightInPoInf> _raycastRsltsForFightInPInfs)
    {
        List<CharRayCastRsltForFightInPoInf> raycastRsltsForFightInPInfs = _raycastRsltsForFightInPInfs;

        float sumOfRatings = 0;

        foreach (CharRayCastRsltForFightInPoInf r in raycastRsltsForFightInPInfs)
        {
            sumOfRatings += GetSumOfRatings(r.targets);
        }

        return sumOfRatings;
    }

    //public bool FindCurvePath(Vector3 _startPoint, Vector3 _endPoint, float _maxError, out Vector3[] _curvePoints)
    //{
    //    Vector3 startPoint = _startPoint;
    //    Vector3 endPoint = _endPoint;
    //    float maxError = _maxError;

    //    _curvePoints = null;

    //    foreach (GameObject go in mapPathCurves)
    //    {
    //        AntaresBezierCurve curve = go.GetComponent<AntaresBezierCurve>();
    //        Vector3[] curvePoints = GetPointsOnCurve(curve, SoldierGeneral.CurvePointsDistance);

    //        if (Vector3.Distance(curvePoints[curvePoints.Length - 1], endPoint) <= maxError &&
    //            Vector3.Distance(curvePoints[0], startPoint) <= maxError)
    //        {
    //            _curvePoints = curvePoints;
    //            return true;
    //        }

    //        if (Vector3.Distance(curvePoints[0], endPoint) <= maxError &&
    //            Vector3.Distance(curvePoints[curvePoints.Length - 1], startPoint) <= maxError)
    //        {
    //            _curvePoints = General.ReverseArray<Vector3>(curvePoints);
    //            return true;
    //        }
    //    }

    //    return false;
    //}

    public bool FindCurvePath(Vector3 _startPoint, Vector3 _endPoint, float _maxError, out Vector3[] _curvePoints)
    {
        Vector3 startPoint = _startPoint;
        Vector3 endPoint = _endPoint;
        float maxError = _maxError;
        float maxMidError = 0.8f * SoldierStats.CurvePointsDistance;

        _curvePoints = null;

        foreach (GameObject go in mapPathCurves)
        {
            AntaresBezierCurve curve = go.GetComponent<AntaresBezierCurve>();
            Vector3[] curvePoints = GetPointsOnCurve(curve, SoldierStats.CurvePointsDistance);

            if (Vector3.Distance(curvePoints[curvePoints.Length - 1], endPoint) <= maxError)
            {
                if (Vector3.Distance(curvePoints[0], startPoint) <= maxError)
                {
                    _curvePoints = curvePoints;
                    return true;
                }

                for (int i = 0; i < curvePoints.Length - 1; i++)
                {
                    if (Vector3.Distance(curvePoints[i], startPoint) <= maxMidError)
                    {
                        _curvePoints = new Vector3[curvePoints.Length - i];

                        _curvePoints[0] = startPoint;

                        for (int j = 1; j < _curvePoints.Length; j++)
                        {
                            _curvePoints[j] = curvePoints[i + j];
                        }

                        return true;
                    }
                }
            }

            if (Vector3.Distance(curvePoints[0], endPoint) <= maxError)
            {
                if (Vector3.Distance(curvePoints[curvePoints.Length - 1], startPoint) <= maxError)
                {
                    _curvePoints = GeneralStats.ReverseArray<Vector3>(curvePoints);
                    return true;
                }

                for (int i = curvePoints.Length - 1; i > 0; i--)
                {
                    if (Vector3.Distance(curvePoints[i], startPoint) <= maxMidError)
                    {
                        _curvePoints = new Vector3[i + 1];

                        _curvePoints[0] = startPoint;

                        for (int j = 1; j < _curvePoints.Length; j++)
                        {
                            _curvePoints[j] = curvePoints[i - j];
                        }

                        return true;
                    }
                }
            }

            //if (Vector3.Distance(curvePoints[0], endPoint) <= maxError &&
            //    Vector3.Distance(curvePoints[curvePoints.Length - 1], startPoint) <= maxError)
            //{
            //    _curvePoints = General.ReverseArray<Vector3>(curvePoints);
            //    return true;
            //}
        }

        #region Recursive
        //List<AntaresBezierCurve> remainingMapCurves = new List<AntaresBezierCurve>();
        //foreach (GameObject go in mapPathCurves)
        //{
        //    remainingMapCurves.Add(go.GetComponent<AntaresBezierCurve>());
        //}

        //List<CurveInfo> curCurveInfos = new List<CurveInfo>();

        //List<CurveInfo> result = new List<CurveInfo>();

        //if (NextCurvePath(remainingMapCurves, curCurveInfos, startPoint, endPoint, maxError, out result))
        //{
        //    List<Vector3> finalPointsList = new List<Vector3>();

        //    foreach (CurveInfo curveInf in result)
        //    {
        //        AntaresBezierCurve curve = curveInf.curve;
        //        Vector3[] curvePoints = GetPointsOnCurve(curve, SoldierGeneral.CurvePointsDistance);

        //        if (curveInf.endToStart)
        //        {
        //            curvePoints = General.ReverseArray<Vector3>(curvePoints);
        //        }

        //        for (int i = 0; i < curvePoints.Length; i++)
        //        {
        //            finalPointsList.Add(curvePoints[i]);
        //        }
        //    }

        //    _curvePoints = new Vector3[finalPointsList.Count];

        //    for(int i=0; i<finalPointsList.Count; i++)
        //    {
        //        _curvePoints[i] = finalPointsList[i];
        //    }

        //    return true;
        //} 
        #endregion

        return false;
    }

    bool NextCurvePath(List<AntaresBezierCurve> _remainingMapCurves, List<CurveInfo> _curCurveInfos, Vector3 _startPos, Vector3 _endPos, float _maxError, out List<CurveInfo> _result)
    {
        List<AntaresBezierCurve> remainingMapCurves = _remainingMapCurves;
        List<CurveInfo> curCurveInfos = _curCurveInfos;
        Vector3 startPos = _startPos;
        Vector3 endPos = _endPos;
        float maxError = _maxError;

        _result = null;

        List<CurveInfo> newCurveInfos = new List<CurveInfo>();

        for (int i = 0; i < curCurveInfos.Count; i++)
        {
            newCurveInfos.Add(curCurveInfos[i]);
        }

        List<AntaresBezierCurve> newRemainingMapCurves = new List<AntaresBezierCurve>();

        for (int i = 0; i < remainingMapCurves.Count; i++)
        {
            newRemainingMapCurves.Add(remainingMapCurves[i]);
        }

        List<CurveInfo> newResult;

        for (int i = 0; i < remainingMapCurves.Count; i++)
        {
            AntaresBezierCurve curve = newRemainingMapCurves[i];

            Vector3[] curvePoints = GetPointsOnCurve(curve, SoldierStats.CurvePointsDistance);

            if (IsCurveStartOnPoint(curve, startPos, maxError))
            {
                CurveInfo curveInf = new CurveInfo();

                curveInf.curve = curve;
                curveInf.startToEnd = true;

                newCurveInfos.Add(curveInf);
                newRemainingMapCurves.RemoveAt(i);

                if (IsCurveEndOnPoint(curve, endPos, maxError))
                {
                    _result = newCurveInfos;

                    return true;
                }
                else
                {
                    if (NextCurvePath(newRemainingMapCurves, newCurveInfos, curvePoints[curvePoints.Length - 1], endPos, maxError, out newResult))
                    {
                        _result = newResult;

                        return true;
                    }
                    else
                    {
                        newCurveInfos.RemoveAt(newCurveInfos.Count - 1);
                        newRemainingMapCurves.Insert(i, curve);
                    }
                }
            }

            if (IsCurveEndOnPoint(curve, startPos, maxError))
            {
                CurveInfo curveInf = new CurveInfo();

                curveInf.curve = curve;
                curveInf.endToStart = true;

                newCurveInfos.Add(curveInf);
                newRemainingMapCurves.RemoveAt(i);

                if (IsCurveStartOnPoint(curve, endPos, maxError))
                {
                    _result = newCurveInfos;

                    return true;
                }
                else
                {
                    if (NextCurvePath(newRemainingMapCurves, newCurveInfos, curvePoints[0], endPos, maxError, out newResult))
                    {
                        _result = newResult;

                        return true;
                    }
                    else
                    {
                        newCurveInfos.RemoveAt(newCurveInfos.Count - 1);
                        newRemainingMapCurves.Insert(i, curve);
                    }
                }
            }
        }

        return false;
    }

    bool IsCurveStartOnPoint(AntaresBezierCurve _curve, Vector3 _point, float _maxError)
    {
        AntaresBezierCurve curve = _curve;
        Vector3 point = _point;
        float maxError = _maxError;

        Vector3[] curvePoints = GetPointsOnCurve(curve, SoldierStats.CurvePointsDistance);

        if (Vector3.Distance(curvePoints[0], point) <= maxError)
        {
            return true;
        }

        return false;
    }

    bool IsCurveEndOnPoint(AntaresBezierCurve _curve, Vector3 _point, float _maxError)
    {
        AntaresBezierCurve curve = _curve;
        Vector3 point = _point;
        float maxError = _maxError;

        Vector3[] curvePoints = GetPointsOnCurve(curve, SoldierStats.CurvePointsDistance);

        if (Vector3.Distance(curvePoints[curvePoints.Length - 1], point) <= maxError)
        {
            return true;
        }

        return false;
    }

    public List<FightPointWithRating> RateFightPointsAndSort(FightPoint[] _fightPoints, GameObject _sourceSoldier, List<GameObject> _initialEnemies)
    {
        FightPoint[] fightPoints = _fightPoints;
        GameObject sourceSoldier = _sourceSoldier;
        List<GameObject> initialEnemies = _initialEnemies;

        List<FightPointWithRating> unsortedFPsWithRating = new List<FightPointWithRating>();

        foreach (FightPoint fp in fightPoints)
        {
            List<CharRayCastRsltForFightInPoInf> rslts = GetAllAttackableEnemiesForListOfFightInfos(fp.fightInfos, sourceSoldier, initialEnemies, fp.transform.position, fp.transform.rotation);
            float sumOfRatings = GetSumOfRatings(rslts);

            FightPointWithRating fpWithRating = new FightPointWithRating();

            fpWithRating.fightPoint = fp;
            fpWithRating.rating = sumOfRatings;

            float rateAdditCoef = 1;

            if (fpWithRating.fightPoint.isLowRate)
                rateAdditCoef = 0.01f;

            fpWithRating.rating = fpWithRating.rating * fpWithRating.fightPoint.ratingCoef * rateAdditCoef;

            fpWithRating.rating = fpWithRating.rating * Random.Range(0.88f, 1.12f);


            unsortedFPsWithRating.Add(fpWithRating);
        }

        List<FightPointWithRating> sortedFPsWithRating = new List<FightPointWithRating>();

        while (unsortedFPsWithRating.Count != 0)
        {
            float maxRating = float.NegativeInfinity;
            int selectedIndex = -1;

            for (int i = 0; i < unsortedFPsWithRating.Count; i++)
            {
                if (unsortedFPsWithRating[i].rating > maxRating)
                {
                    maxRating = unsortedFPsWithRating[i].rating;
                    selectedIndex = i;
                }
            }

            sortedFPsWithRating.Add(unsortedFPsWithRating[selectedIndex]);
            unsortedFPsWithRating.RemoveAt(selectedIndex);
        }

        return sortedFPsWithRating;
    }

    public Vector3[] GetPointsOnCurve(AntaresBezierCurve _curve, float _curveStepLength)
    {
        AntaresBezierCurve curve = _curve;
        float curveStepLength = _curveStepLength;
        int numOfSteps = 0;

        curve.Init();

        numOfSteps = 2 + (int)(curve.Length / curveStepLength);

        Vector3[] resultVecs = new Vector3[numOfSteps];

        for (int i = 0; i < numOfSteps; i++)
        {
            float dist = i * curveStepLength;
            dist = Mathf.Clamp(dist, 0, curve.Length - 0.01f);
            resultVecs[i] = curve.GetInterpolatedPoint(dist);
        }

        return resultVecs;
    }


    void SlowlyCheckPlayerVisibility()
    {
        time_PlayerVisibilityCheck_Counter = MathfPlus.DecByDeltatimeToZero(time_PlayerVisibilityCheck_Counter);

        if (time_PlayerVisibilityCheck_Counter == 0)
        {
            if (!isPlayerHidden)
            {
                if (CheckIsPlayerHidden())
                {
                    isPlayerHidden = true;
                    time_PlayerVisibilityCheck_Counter = time_PlayerVisibleCheck_Max;
                }
                else
                {
                    time_PlayerVisibilityCheck_Counter = time_PlayerHideCheck_Max;
                }

            }
            else
            {
                if (!CheckIsPlayerHidden())
                {
                    isPlayerHidden = false;
                    time_PlayerVisibilityCheck_Counter = time_PlayerHideCheck_Max;
                }
                else
                {
                    time_PlayerVisibilityCheck_Counter = time_PlayerVisibleCheck_Max;
                }
            }
        }
    }

    bool CheckIsPlayerHidden()
    {
        foreach (GameObject sold in mapEnemyChars)
        {
            if (sold == null)
                continue;

            SoldierInfo soldInf = sold.GetComponent<SoldierInfo>();

            if (soldInf.IsPlayerInView())
                return false;
        }
        return true;
    }

    //

    public void PauseGame()
    {
        GameController.isGamePaused = true;

        AudioController.Pause();

        if (PlayerCharacterNew.Instance)
            PlayerCharacterNew.Instance.OnGamePause();

        Screen.lockCursor = false;

        ingameMenu.ActivateIt(true);

        oldTimeScale = Time.timeScale;

        Time.timeScale = GeneralStats.pausedGameTimeScale;
    }

    public void UnPauseGame(bool _isEscapeKeyUsed)
    {
        isEscapeKeyDownFromMenu = _isEscapeKeyUsed;

        GameController.isGamePaused = false;

        AudioController.UnPause();

        if (PlayerCharacterNew.Instance)
            PlayerCharacterNew.Instance.OnGameUnpause();

        Screen.lockCursor = true;

        Time.timeScale = oldTimeScale;
    }

    public void TryToPauseGameAndJustShowHint(HUDGroupName _groupName, HUDControlName _blackBGName, HUDControlName _hintName)
    {
        neededHint_GroupName = _groupName;
        neededHint_HintName = _hintName;
        neededHint_BlackBGName = _blackBGName;

        showingAHintIsNeeded = true;
    }

    void PauseGameAndJustShowHint(HUDGroupName _groupName, HUDControlName _blackBGName, HUDControlName _hintName)
    {
        HUDGroupName groupName = _groupName;
        HUDControlName hintName = _hintName;
        HUDControlName blackBGName = _blackBGName;

        //

        showingAHintIsNeeded = false;

        isGamePausedForShowingHint = true;

        GameController.isGamePaused = true;

        AudioController.Pause();

        if (PlayerCharacterNew.Instance)
            PlayerCharacterNew.Instance.OnGamePause();

        oldTimeScale = Time.timeScale;

        Time.timeScale = GeneralStats.pausedGameTimeScale;

        //

        curInGameHint = mapHUDParent.GetChildGroupByName(groupName).GetChildControlByName(hintName);
        curInGameHint.SetAlpha(1);
        curInGameHint.SetIsVisible(true);

        curInGameHintBlackBG = mapHUDParent.GetChildGroupByName(groupName).GetChildControlByName(blackBGName);
        curInGameHintBlackBG.SetAlpha(1);
        curInGameHintBlackBG.SetIsVisible(true);
    }

    void UnPauseGameAndRemoveHint()
    {
        isGamePausedForShowingHint = false;

        GameController.isGamePaused = false;

        AudioController.UnPause();

        if (PlayerCharacterNew.Instance)
            PlayerCharacterNew.Instance.OnGameUnpause();

        Time.timeScale = oldTimeScale;

        //

        curInGameHint.SetAlpha(0);
        curInGameHint.SetIsVisible(false);

        curInGameHintBlackBG.SetAlpha(0);
        curInGameHintBlackBG.SetIsVisible(false);
    }

    //

    public void DeactiveAllActiveChars()
    {
        activeChars = new List<GameObject>();

        foreach (GameObject go in mapChars)
        {
            if ((go != null) && (go != player) && go.active)
            {
                activeChars.Add(go);

                //go.active = false;

                SoldierInfo soldInf = go.GetComponent<SoldierInfo>();

                soldInf.Stop();

                //soldInf.charController.enabled = false;

                //soldInf.body.SetActiveRecursively(false);

                //Renderer[] rends = go.GetComponentsInChildren<Renderer>();

                //for (int i = 0; i < rends.Length; i++)
                //{
                //    rends[i].enabled = false;
                //}
            }
        }
    }

    public void ActiveAllOldActiveChars()
    {
        foreach (GameObject go in activeChars)
        {
            if (go != null)
            {
                //go.active = true;

                SoldierInfo soldInf = go.GetComponent<SoldierInfo>();

                soldInf.Resume();

                // soldInf.charController.enabled = true;

                // soldInf.body.SetActiveRecursively(true);

                //  Renderer[] rends = go.GetComponentsInChildren<Renderer>();

                //for (int i = 0; i < rends.Length; i++)
                //{
                //    rends[i].enabled = true;
                //}
            }
        }
    }

    public void StopActiveLogics()
    {
        PlayerCharacterNew.Instance.StopPlayer(true);

        foreach (MapLogicJob mlj in mapActiveLogicJobs)
        {
            mlj.StopIt();
        }

        //foreach (GameObject obj in mapChars)
        //{
        //    if (obj != null)
        //    {
        //        SoldierInfo soldInf = obj.GetComponent<SoldierInfo>();

        //        if (soldInf != null)
        //        {
        //            soldInf.Stop();
        //        }
        //    }
        //}

        DeactiveAllActiveChars();
    }

    public void RestartActiveLogics()
    {
        PlayerCharacterNew.Instance.RestartPlayer();

        ActiveAllOldActiveChars();
    }

    //

    public List<GameObject> FindNearMates(GameObject _src, float _radius, FightSideEnum _friendFightSide)
    {
        GameObject src = _src;
        float rad = _radius;
        FightSideEnum fSide = _friendFightSide;

        List<GameObject> allFriends = GetMapFriendSideChars(fSide);

        List<GameObject> nearFriends = new List<GameObject>();

        for (int i = 0; i < allFriends.Count; i++)
        {
            if (allFriends[i] != src)
            {
                if (Vector3.Distance(src.transform.position, allFriends[i].transform.position) <= rad)
                    nearFriends.Add(allFriends[i]);
            }
        }

        return nearFriends;
    }

    public void SetCameraActivation(Camera _cam, bool _value)
    {
        Camera cam = _cam;
        bool val = _value;

        AudioListener audListener = cam.gameObject.GetComponent<AudioListener>();

        cam.enabled = val;

        if (audListener != null)
            audListener.enabled = val;
    }

    public void SetMapAllCamerasDeactive()
    {
        for (int i = 0; i < mapAllCameras.Length; i++)
        {
            SetCameraActivation(mapAllCameras[i], false);
        }
    }

    public void SetMapActiveCamera(Camera _cam)
    {
        Camera cam = _cam;

        SetMapAllCamerasDeactive();

        SetCameraActivation(cam, true);
    }

    public void ActiveOnlyPlayerCameras()
    {
        SetMapAllCamerasDeactive();

        if (PlayerCharacterNew.Instance != null)
        {
            SetCameraActivation(PlayerCharacterNew.Instance.fpsCamera, true);
            SetCameraActivation(PlayerCharacterNew.Instance.mainCam, true);
        }
    }

    //

    public float GetCampCurrentSoldsViewRange()
    {
        float finalRange = SoldierStats.campSoldier_ViewRange;

        float playerHorizVelocity = playerCharNew.GetHorizVelocity();


        // ///// Sitting

        if (playerCharNew.IsVertMovementState(PlayerVertMovementStateEnum.Sit))
        {
            finalRange *= SoldierStats.campSoldier_PlayerSitCoef;
        }


        // ///// Horiz Speed

        finalRange *= (1 + ((playerHorizVelocity / 10) * SoldierStats.campSoldier_PlayerHorizSpeedAdditionalCoefPer10));


        // ///// Hidden Areas

        bool isHiddenAreaFounded = false;

        float valueFromHiddenAreas = 0;

        int indexOfHiddenAreaThatContainsPlayer = camp_CurUpdate_IndexOfHiddenAreaThatContainsPlayer;

        if (indexOfHiddenAreaThatContainsPlayer >= 0)
        {
            isHiddenAreaFounded = true;

            CampHiddenArea hiddenArea = campHiddenAreas[indexOfHiddenAreaThatContainsPlayer];

            float movingEffectToValuePer10 = SoldierStats.campSoldier_HiddenAreasMovingEffectToValuePer10;
            bool mustPlayerBeSitting = hiddenArea.mustPlayerBeSitting;
            valueFromHiddenAreas = SoldierStats.campSoldier_HiddenAreasDefaultEffectValue;

            valueFromHiddenAreas += (playerHorizVelocity / 10) * movingEffectToValuePer10;

            if (mustPlayerBeSitting && !playerCharNew.IsVertMovementState(PlayerVertMovementStateEnum.Sit))
            {
                isHiddenAreaFounded = false;
            }
        }

        if (isHiddenAreaFounded)
            finalRange = valueFromHiddenAreas;


        // //// Lights

        float additionalRangeFromLight = 0;

        int indexOfLightAreaThatContainsPlayer = camp_CurUpdate_IndexOfLightAreaThatContainsPlayer;

        if (indexOfLightAreaThatContainsPlayer >= 0)
        {
            CampLightArea lightArea = campLightAreas[indexOfLightAreaThatContainsPlayer];
            float intensity = lightArea.intensity;
            float range = lightArea.range;
            Transform centerTr = lightArea.centerTr;

            float playerDistToCampLight = Vector3.Magnitude(player.transform.position - centerTr.position);
            playerDistToCampLight = Mathf.Clamp(playerDistToCampLight, 0, range);

            additionalRangeFromLight = (1 - (playerDistToCampLight / range)) * intensity * SoldierStats.campSoldier_MaxAdditionalViewRangeFromLights;
            additionalRangeFromLight = Mathf.Clamp(additionalRangeFromLight, 0, SoldierStats.campSoldier_MaxAdditionalViewRangeFromLights);
        }

        finalRange += additionalRangeFromLight;


        //

        return finalRange;
    }

    public float GetCampCurrentSoldsViewAngle()
    {
        float finalAngle = SoldierStats.campSoldier_BaseViewHalfAngle;

        float playerHorizVelocity = playerCharNew.GetHorizVelocity();

        finalAngle += ((playerHorizVelocity / 10) * SoldierStats.campSoldier_PlayerHorizSpeedAdditionalViewHalfAnglePer10);

        if (playerCharNew.IsVertMovementState(PlayerVertMovementStateEnum.Stand))
        {
            finalAngle += SoldierStats.campSoldier_PlayerStandAdditionalViewHalfAngle;
        }

        finalAngle = Mathf.Clamp(finalAngle, 0, SoldierStats.campSoldier_MaxViewHalfAngle);

        return finalAngle;
    }

    int GetIndexOfCampLightAreaThatContainsPlayer()
    {
        if (numOfCampLightAreasInThisMap <= 0)
            return -1;

        for (int i = 0; i < numOfCampLightAreasInThisMap; i++)
        {
            if (campLightAreaLogTriggers[i].IsPlayerIn())
                return i;
        }

        return -1;
    }

    int GetIndexOfCampHiddenAreaThatContainsPlayer()
    {
        if (numOfCampHiddenAreasInThisMap <= 0)
            return -1;

        for (int i = 0; i < numOfCampHiddenAreasInThisMap; i++)
        {
            if (campHiddenAreaLogTriggers[i].IsPlayerIn())
                return i;
        }

        return -1;
    }

    int GetIndexOfCampBadAreaThatContainsPlayer()
    {
        if (numOfCampBadAreasInThisMap <= 0)
            return -1;

        for (int i = 0; i < numOfCampBadAreasInThisMap; i++)
        {
            if (campBadAreaLogTriggers[i].IsPlayerIn())
                return i;
        }

        return -1;
    }

    public bool CanKeepRaycastingThroughCampWall(CampWall _wall, Vector3 _campWallHitPoint)
    {
        CampWall wall = _wall;
        Vector3 campWallHitPoint = _campWallHitPoint;
        CampWallType wallType = wall.wallType;

        if (wallType == CampWallType.AlwaysBanView)
            return false;

        float distToPlayer = Vector3.Magnitude(player.transform.position - campWallHitPoint);

        if (distToPlayer > SoldierStats.campSoldier_CampWallMinNeededDist)
            return false;

        if (wallType == CampWallType.OnBeingCloseLetView)
        {
            return true;
        }

        if (wallType == CampWallType.OnCloseMovingLetView)
        {
            if (distToPlayer > SoldierStats.campSoldier_CampWallSpeedMode_MinNeededDist)
                return false;

            float playerHorizMoveSpeed = playerCharNew.GetHorizVelocity();

            if (playerHorizMoveSpeed < SoldierStats.campSoldier_CampWallSpeedMode_MinNeededSpeed)
                return false;

            return true;
        }

        return false;
    }

    public void SetPlayerIsDetectedInCampMode()
    {
        if (!isPlayerDetectedInCampMode)
            isPlayerDetectedInCampMode = true;
    }

    public bool IsPlayerDetectedInCampMode()
    {
        return isPlayerDetectedInCampMode;
    }

    public bool CanGenerallyShowNormalHUD()
    {
        if (playerCharNew.IsMissionFailed())
            return false;

        if (isCutsceneMode)
            return false;

        return true;
    }

    public void HUD_ObjectivesPage_SetActiveObjective(int _objIndex)
    {
        if (_objIndex <= 0)
        {
            Debug.LogError("HUD objective index must be bigger than zero!");
            return;
        }

        int objIndex = _objIndex;

        HUDGroup objHUDGroup = hudGroup_ObjectivesPage;

        for (int i = 0; i < objIndex - 1; i++)
        {
            objHUDGroup.hudControls[i].SetAlpha(1);
            objHUDGroup.hudControls[i].SetSelectedTextureIndex(1);
        }

        objHUDGroup.hudControls[objIndex - 1].SetAlpha(1);
        objHUDGroup.hudControls[objIndex - 1].SetSelectedTextureIndex(0);
    }
    public void HUD_ObjectivesPage_SetObjectiveDone(int _objIndex)
    {
        if (_objIndex <= 0)
        {
            Debug.LogError("HUD objective index must be bigger than zero!");
            return;
        }

        int objIndex = _objIndex;

        HUDGroup objHUDGroup = hudGroup_ObjectivesPage;

        for (int i = 0; i < objIndex; i++)
        {
            objHUDGroup.hudControls[i].SetAlpha(1);
            objHUDGroup.hudControls[i].SetSelectedTextureIndex(1);
        }
    }

    public void HUD_ShowGameSaved()
    {
        if (doNotShowGameSavedMessageForOneTime)
        {
            doNotShowGameSavedMessageForOneTime = false;
            return;
        }

        hudControl_GameSaved.SetIsVisible(true);
        hudControl_GameSaved.SetAlpha(0);
        hudControl_GameSaved.SetOutCounter(0);
        hudControl_GameSaved.SetOutStep(HUDOutStep.Starting);

        hudControl_GameSaved.additionalY = 0;
        hudControl_GameSaved.ReInitRect();
    }
    bool HUD_ShouldFinishGameSavedHUD()
    {
        if (!CanGenerallyShowNormalHUD())
            return true;

        return false;
    }

    public void HUD_ShowNewMission(int _index)
    {
        int ind = _index;

        if (blackScreenFader.IsFadingIn() || hud_IsBusyShowingNewMission || hud_IsBusyShowingMissionComplete)
        {
            hud_ShouldShowNewMission = true;
            hud_SelectedNewMissionIndexToShow = ind;
            return;
        }

        if (!hud_IsBusyShowingMainMission)
            HUD_ShowMainMission(false);

        hud_ShouldShowNewMission = false;

        hud_IsBusyShowingNewMission = true;

        hudControl_CurrentMission.SetSelectedTextureIndex(ind);

        hudControl_CurrentMission.SetIsVisible(true);
        hudControl_CurrentMission.SetAlpha(0);
        hudControl_CurrentMission.SetOutCounter(0);
        hudControl_CurrentMission.SetOutStep(HUDOutStep.Starting);

        hudControl_CurrentMission.additionalY = 0;
        hudControl_CurrentMission.ReInitRect();
    }
    bool HUD_ShouldFinishNewMissionHUD()
    {
        if (!CanGenerallyShowNormalHUD())
            return true;

        if (hud_ShouldShowNewMission || hud_ShouldShowMissionComplete)
            return true;

        return false;
    }

    public void HUD_ShowMainMission(bool _isCompleted)
    {
        if (_isCompleted)
            hud_NextMainMissionIsCompletedOne = true;

        if (hud_IsBusyShowingMainMission)
        {
            if (hud_NextMainMissionIsCompletedOne)
                hud_ShouldShowCompletedMainMission = true;

            return;
        }

        if (hud_NextMainMissionIsCompletedOne)
            hud_IsMainMissionCompleted = true;

        hud_ShouldShowCompletedMainMission = false;

        hud_IsBusyShowingMainMission = true;

        if (hud_IsMainMissionCompleted)
        {
            hudControl_MainMission.SetSelectedTextureIndex(1);
        }
        else
        {
            hudControl_MainMission.SetSelectedTextureIndex(0);
        }

        hudControl_MainMission.SetIsVisible(true);
        hudControl_MainMission.SetAlpha(0);
        hudControl_MainMission.SetOutCounter(0);
        hudControl_MainMission.SetOutStep(HUDOutStep.Starting);

        hudControl_MainMission.additionalY = 0;
        hudControl_MainMission.ReInitRect();
    }
    bool HUD_ShouldFinishMainMissionHUD()
    {
        if (!CanGenerallyShowNormalHUD())
            return true;

        if (!hud_IsMainMissionCompleted)
            if (!hud_IsBusyShowingMissionComplete && !hud_IsBusyShowingNewMission)
                return true;

        return false;
    }

    public void HUD_ShowCompleteMission(int _index)
    {
        int ind = _index;

        if (hud_IsBusyShowingNewMission)
        {
            hud_ShouldShowMissionComplete = true;
            hud_SelectedMissionCompleteIndexToShow = ind;
            return;
        }

        if (hud_IsBusyShowingMissionComplete)
        {
            return;
        }

        if (!hud_IsBusyShowingMainMission)
            HUD_ShowMainMission(false);

        hud_ShouldShowMissionComplete = false;

        hud_IsBusyShowingMissionComplete = true;


        hudControl_MissionComplete.SetSelectedTextureIndex(ind);

        hudControl_MissionComplete.SetIsVisible(true);
        hudControl_MissionComplete.SetAlpha(0);
        hudControl_MissionComplete.SetOutCounter(0);
        hudControl_MissionComplete.SetOutStep(HUDOutStep.Starting);

        hudControl_MissionComplete.additionalY = 0;
        hudControl_MissionComplete.ReInitRect();
    }
    bool HUD_ShouldFinishMissionCompleteHUD()
    {
        if (!CanGenerallyShowNormalHUD())
            return true;

        return false;
    }

    public void AddActiveGrenade(PlayerGrenade _grenade)
    {
        mapActiveGrenades.Add(_grenade);
    }

    public void RemoveActiveGrenade(PlayerGrenade _grenade)
    {
        mapActiveGrenades.Remove(_grenade);
    }

    public void HUD_Add3DObjective(Transform _tr, The3DObjIconType _iconType, string _objName, The3DObjViewRange _viewRange)
    {
        Transform tr = _tr;
        The3DObjIconType iconType = _iconType;
        string objName = _objName;
        The3DObjViewRange viewRange = _viewRange;

        foreach (The3DObjective the3dObj in hud_CurrentActive3DObjectives)
        {
            if (the3dObj.the3DObjName == objName)
            {
                Debug.LogError("Can not add 3DObj. '" + objName + "' is used bafore!");
                return;
            }
        }

        playerCharNew.HUD_Add3DObj(tr, tr.position, objName, true);

        The3DObjective dat3DObj = null;

        switch (iconType)
        {
            case The3DObjIconType.Dot:
                dat3DObj = Instantiate(hud_3DObjTypeSource_Dot) as The3DObjective;
                break;

            case The3DObjIconType.FeleshRooBeBala:
                dat3DObj = Instantiate(hud_3DObjTypeSource_FeleshRooBeBala) as The3DObjective;
                break;

            case The3DObjIconType.FeleshRooBePayin:
                dat3DObj = Instantiate(hud_3DObjTypSource_FeleshRooBePayin) as The3DObjective;
                break;
        }

        dat3DObj.StartIt(tr, objName, viewRange);

        hud_CurrentActive3DObjectives.Add(dat3DObj);
    }

    public void HUD_Remove3DObjective(string _name)
    {
        for (int i = 0; i < hud_CurrentActive3DObjectives.Count; i++)
        {
            if (hud_CurrentActive3DObjectives[i].the3DObjName == _name)
            {
                playerCharNew.HUD_Remove3DObj(_name);
                hud_CurrentActive3DObjectives[i].StopIt();
                hud_CurrentActive3DObjectives.RemoveAt(i);
            }
        }
    }

    public void HUD_3DObjBlinkInMinimap(string _name)
    {
        playerCharNew.HUD_Get3DObjByName(_name).Blink();
    }

    public void HUD_AddOnlyMinimap3DObj(Transform _tr, string _objName)
    {
        playerCharNew.HUD_Add3DObj(_tr, _tr.position, _objName, false);
    }

    public void HUD_RemoveMinimap3DObj(string _objName)
    {
        playerCharNew.HUD_Remove3DObj(_objName);
    }
}
