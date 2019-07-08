using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum MenuEventType
{
    click,
}

public enum MenuStep
{
    start_init,
    end_init,
    ingameEndWithEscapeKey_init,

    ingameMenu_init,
    ingameMenu_update,
    ingameMenu_To_Options,
    ingameMenu_To_LastCheckpointDialogue,
    ingameMenu_To_RestartMissionDialgoue,
    ingameMenu_To_SaveAndQuitDialogue,

    mainMenu_init,
    mainMenu_update,
    mainMenu_To_Options,
    mainMenu_To_MissionSelect,
    mainMenu_To_LastCheckpointWillBeLostDialogue,
    mainMenu_To_Difficulty,
    mainMenu_To_Credits,
    mainMenu_To_Quit,

    missionSelectMenu_init,
    missionSelectMenu_update,
    missionSelectMenu_To_Difficulty,
    missionSelectMenu_To_MainMenu,

    difficulty_init,
    difficulty_update,
    difficulty_back,

    lastCheckpointWillBeLostDialogue_init,
    lastCheckpointWillBeLostDialogue_update,
    lastCheckpointWillBeLostDialogue_To_Next,
    lastCheckpointWillBeLostDialogue_back,

    quitDialogue_init,
    quitDialogue_update,
    quitDialogue_back,
    quitDialogue_ExitApp,

    lastCheckpointDialogue_init,
    lastCheckpointDialogue_update,
    lastCheckpointDialogue_back,
    lastCheckpointDialogue_LoadLastCheckpoint,

    restartMissionDialogue_init,
    restartMissionDialogue_update,
    restartMissionDialogue_back,
    restartMissionDialogue_RestartMission,

    saveAndQuitDialogue_init,
    saveAndQuitDialogue_update,
    saveAndQuitDialogue_back,
    saveAndQuitDialogue_QuitToMainMenu,

    options_init,
    options_update,
    options_back,
    options_controls_waitingForPlayerKey_init,
    options_controls_waitingForPlayerKey_update,
    options_controls_waitingForPlayerKey_ToOptionsControls,

    options_controls_DefaultsDialogue_init,
    options_controls_DefaultsDialogue_update,
    options_controls_DefaultsDialogue_back,

    options_Audio_DefaultsDialogue_init,
    options_Audio_DefaultsDialogue_update,
    options_Audio_DefaultsDialogue_back,

    options_Video_DefaultsDialogue_init,
    options_Video_DefaultsDialogue_update,
    options_Video_DefaultsDialogue_back,

    options_Video_ApplyDialogue_init,
    options_Video_ApplyDialogue_update,
    options_Video_ApplyDialogue_back,
}

public enum ActiveOptionsPage
{
    Video,
    Controls,
    Audio,
}

public enum VideoOptionsNeededPageToGo
{
    Controls,
    Audio,
    ExitOptions,
    Video,
}

public class MenuParent : MonoBehaviour
{
    public MenuGroup[] menuGroups;
    public KeyPropsList allKeyProps;
    public ComboBoxItem comboTempRefItem_Normal;
    public ComboBoxItem comboTempRefItem_Mini;
    public ComboBoxRef comboItemRef;
    public ComboBoxRef miniComboItemRef;

    float defWidth = 1920;
    float defHeight = 1080;

    float sliderThumbPaddingCoefToHeight = 0.0166666f;
    float sliderOverflowBottom_ZeroHeight = 540;
    float sliderOverflowBottom_HeightStep = 40;
    float sliderOverflowTop_HeightStep = 180;

    float text_FontCoefToHeight = 0.026f;
    float combo_FontCoefToHeight = 0.02f;
    float combo_AlphaSpeed = 0.09f;
    float combo_AlphaStep = 0.15f;

    float scale = 1;

    bool aComboBoxIsOpen = false;
    MenuControl curOpenComboBox;
    Rect curOpenComboBoxFinalRect;

    bool isAnyVideoSettingsControlChanged = false;

    bool isReloadingVideoSettingsControlsNeeded = true;

    int vidSettings_Selected_ResolutionIndex = 0;
    int vidSettings_Selected_ResolutionIndex_Old = 0;
    bool vidSettings_Selected_SSAO = false;
    bool vidSettings_Selected_SSAO_Old = false;
    bool vidSettings_Selected_Bloom = false;
    bool vidSettings_Selected_Bloom_Old = false;
    float vidSettings_Selected_Brightness = 0;
    float vidSettings_Selected_Brightness_Old = 0;
    bool vidSettings_Selected_Shadows = false;
    bool vidSettings_Selected_Shadows_Old = false;
    ShadowQual vidSettings_Selected_ShadowQual = ShadowQual.low;
    ShadowQual vidSettings_Selected_ShadowQual_Old = ShadowQual.low;
    float vidSettings_Selected_ShadowDistance = 50;
    float vidSettings_Selected_ShadowDistance_Old = 50;
    TextureQual vidSettings_Selected_TextureQual = TextureQual.full;
    TextureQual vidSettings_Selected_TextureQual_Old = TextureQual.full;
    VideoSettingTypes vidSettings_Selected_VidSetType = VideoSettingTypes.High;
    VideoSettingTypes vidSettings_Selected_VidSetType_Old = VideoSettingTypes.High;
    bool vidSettings_Selected_VSync = false;
    bool vidSettings_Selected_VSync_Old = false;
    bool vidSettings_Selected_Anisotropic = false;
    bool vidSettings_Selected_Anisotropic_Old = false;

    [HideInInspector]
    public MenuStep step = MenuStep.start_init;

    MenuStep prevStep = MenuStep.start_init;

    MenuStep nextStep = MenuStep.start_init;

    bool isMenuEventHappened = false;

    MenuGroupName menuEventGroupName;
    MenuControlName menuEventControlName;
    MenuEventType menuEventType;

    bool isActive = false;
    bool isIngame = false;

    //

    int selectedLevel = 1;
    int selectedLevelCheckPoint = 0;

    int curScreenHight;
    int curScreenWidth;

    int firstLvlNum = 1;
    int firstCheckPointNum = 0;
    int minProgressedCheckPointNum = 2;

    int selectedTextureIndForLvlScreenshot = 1;

    bool shouldW8ForEscapeKeyUp = false;
    bool shouldW8ForEnterKeyUp = false;

    MenuGroup MenuGroup_OptionsControls;
    MenuGroup MenuGroup_OptionsAudio;
    MenuGroup MenuGroup_OptionsVideo;

    //KeyCode playerEnteredCustomKey = KeyCode.None;

    GameKeyInfo playerRequestedKeyInfoToChange;

    MenuControlName playerRequestedKey_MenuControlName;

    bool playerRequestedKeyInfo_IsPrimary;

    bool shouldChangeGameControlsToDefault = false;
    bool shouldChangeGameAudioToDefault = false;
    bool shouldChangeGameVideoToDefault = false;
    bool videoApplyDialogue_ShouldApplyVideoChanges = false;
    bool videoApplyDialogue_ShouldUnapplyVideoChanges = false;

    VideoOptionsNeededPageToGo neededPageToGoFromVideoOptions = VideoOptionsNeededPageToGo.Audio;

    MenuControl MenuControl_OptionsControls_MouseSensitivitySlider;
    MenuControl MenuControl_OptionsControls_MouseSensitivityText;
    MenuControl MenuControl_OptionsControls_InvertMouseToggle;
    MenuControl MenuControl_OptionsControls_UseMouseWheelToChangeWeaponToggle;

    MenuControl MenuControl_OptionsAudio_MasterVolumeSlider;
    MenuControl MenuControl_OptionsAudio_MasterVolumeText;
    MenuControl MenuControl_OptionsAudio_SFXVolumeSlider;
    MenuControl MenuControl_OptionsAudio_SFXVolumeText;
    MenuControl MenuControl_OptionsAudio_MusicVolumeSlider;
    MenuControl MenuControl_OptionsAudio_MusicVolumeText;

    MenuControl MenuControl_OptionsVideo_ResolutionsCombo;
    MenuControl MenuControl_OptionsVideo_ApplyButton;
    MenuControl MenuControl_OptionsVideo_BrightnessSlider;
    MenuControl MenuControl_OptionsVideo_BrightnessText;
    MenuControl MenuControl_OptionsVideo_TexturesQualityCombo;
    MenuControl MenuControl_OptionsVideo_OverallQualityCombo;
    MenuControl MenuControl_OptionsVideo_AnisotropicToggle;
    MenuControl MenuControl_OptionsVideo_SSAOToggle;
    MenuControl MenuControl_OptionsVideo_BloomToggle;
    MenuControl MenuControl_OptionsVideo_ShadowOnToggle;
    //MenuControl MenuControl_OptionsVideo_ShadowDistanceSlider;
    //MenuControl MenuControl_OptionsVideo_ShadowDistanceText;
    MenuControl MenuControl_OptionsVideo_ShadowQualityCombo;
    MenuControl MenuControl_OptionsVideo_VSyncToggle;
    //MenuControl MenuControl_OptionsVideo_ShadowLabel;
    //MenuControl MenuControl_OptionsVideo_ShadowDistanceLabel;
    MenuControl MenuControl_OptionsVideo_ShadowQualityLabel;

    //

    //
    void Awake()
    {
        GameController.GameFirstInit();

        MenuGroup_OptionsControls = GetChildGroupByName(MenuGroupName.options_Controls);
        MenuGroup_OptionsAudio = GetChildGroupByName(MenuGroupName.options_Audio);
        MenuGroup_OptionsVideo = GetChildGroupByName(MenuGroupName.options_Video);

        MenuControl_OptionsControls_MouseSensitivitySlider = MenuGroup_OptionsControls.GetChildControlByName(MenuControlName.sliderOptionsControlsMouseSensitivity);
        MenuControl_OptionsControls_MouseSensitivityText = MenuGroup_OptionsControls.GetChildControlByName(MenuControlName.txtOptionsControlsMouseSensitivityText);
        MenuControl_OptionsControls_InvertMouseToggle = MenuGroup_OptionsControls.GetChildControlByName(MenuControlName.togOptionsControlsInvertMouse);
        MenuControl_OptionsControls_UseMouseWheelToChangeWeaponToggle = MenuGroup_OptionsControls.GetChildControlByName(MenuControlName.togOptionsControlsUseMouseWheelToChangeWeapons);

        MenuControl_OptionsAudio_MasterVolumeSlider = MenuGroup_OptionsAudio.GetChildControlByName(MenuControlName.sliderOptionsAudioMasterVolume);
        MenuControl_OptionsAudio_MasterVolumeText = MenuGroup_OptionsAudio.GetChildControlByName(MenuControlName.txtOptionsAudioMasterVolumeText);
        MenuControl_OptionsAudio_SFXVolumeSlider = MenuGroup_OptionsAudio.GetChildControlByName(MenuControlName.sliderOptionsAudioSFXVolume);
        MenuControl_OptionsAudio_SFXVolumeText = MenuGroup_OptionsAudio.GetChildControlByName(MenuControlName.txtOptionsAudioSFXVolumeText);
        MenuControl_OptionsAudio_MusicVolumeSlider = MenuGroup_OptionsAudio.GetChildControlByName(MenuControlName.sliderOptionsAudioMusicVolume);
        MenuControl_OptionsAudio_MusicVolumeText = MenuGroup_OptionsAudio.GetChildControlByName(MenuControlName.txtOptionsAudioMusicVolumeText);

        MenuControl_OptionsVideo_ResolutionsCombo = MenuGroup_OptionsVideo.GetChildControlByName(MenuControlName.comboOptionsVideoResolutions);
        MenuControl_OptionsVideo_ApplyButton = MenuGroup_OptionsVideo.GetChildControlByName(MenuControlName.btnApply);
        MenuControl_OptionsVideo_BrightnessSlider = MenuGroup_OptionsVideo.GetChildControlByName(MenuControlName.sliderOptionsVideoBrightness);
        MenuControl_OptionsVideo_BrightnessText = MenuGroup_OptionsVideo.GetChildControlByName(MenuControlName.txtOptionsVideoBrightness);
        MenuControl_OptionsVideo_TexturesQualityCombo = MenuGroup_OptionsVideo.GetChildControlByName(MenuControlName.comboOptionsVideoTexturesQuality);
        MenuControl_OptionsVideo_OverallQualityCombo = MenuGroup_OptionsVideo.GetChildControlByName(MenuControlName.comboOptionsVideoOverallQuality);
        MenuControl_OptionsVideo_AnisotropicToggle = MenuGroup_OptionsVideo.GetChildControlByName(MenuControlName.togOptionsVideoAnisotropic);
        MenuControl_OptionsVideo_SSAOToggle = MenuGroup_OptionsVideo.GetChildControlByName(MenuControlName.togOptionsVideo_SSAOToggle);
        MenuControl_OptionsVideo_BloomToggle = MenuGroup_OptionsVideo.GetChildControlByName(MenuControlName.togOptionsVideo_BloomToggle);
        MenuControl_OptionsVideo_ShadowOnToggle = MenuGroup_OptionsVideo.GetChildControlByName(MenuControlName.togOptionsVideo_ShadowOnToggle);
        //MenuControl_OptionsVideo_ShadowDistanceSlider = MenuGroup_OptionsVideo.GetChildControlByName(MenuControlName.sliderOptionsVideo_ShadowDistanceSlider);
        //MenuControl_OptionsVideo_ShadowDistanceText = MenuGroup_OptionsVideo.GetChildControlByName(MenuControlName.txtOptionsVideo_ShadowDistanceText);
        MenuControl_OptionsVideo_ShadowQualityCombo = MenuGroup_OptionsVideo.GetChildControlByName(MenuControlName.comboOptionsVideo_ShadowQualityCombo);
        MenuControl_OptionsVideo_VSyncToggle = MenuGroup_OptionsVideo.GetChildControlByName(MenuControlName.togOptionsVideo_VSyncToggle);

        //MenuControl_OptionsVideo_ShadowLabel = MenuGroup_OptionsVideo.GetChildControlByName(MenuControlName.lblOptionsVideo_Shadow);
        //MenuControl_OptionsVideo_ShadowDistanceLabel = MenuGroup_OptionsVideo.GetChildControlByName(MenuControlName.lblOptionsVideo_ShadowDistance);
        MenuControl_OptionsVideo_ShadowQualityLabel = MenuGroup_OptionsVideo.GetChildControlByName(MenuControlName.lblOptionsVideo_ShadowQuality);

        Options_Controls_ReInitAllKeysTextures();
    }

    void Update()
    {

        //print(QualitySettings.GetQualityLevel());
        if (!VideoSettingsController.gameOneTimeResolutionIsSet)
        {
            VideoSettingsController.SetResolution(VideoSettingsController.curResolutionIndex, true);
            VideoSettingsController.gameOneTimeResolutionIsSet = true;
        }
        else
        {

            if (isActive)
            {
                if (curScreenHight != Screen.height || curScreenWidth != Screen.width)
                {
                    CalcCurHeightAndWidthAndScale();
                    ReInitScale(scale);
                }

                if (!CustomInputManager.Key_Escape()) //(!GameController.GetButton(GeneralStats.key_Escape))
                {
                    if (shouldW8ForEscapeKeyUp)
                        shouldW8ForEscapeKeyUp = false;
                }

                if (!CustomInputManager.Key_Enter()) //(!GameController.GetButton(GeneralStats.key_Enter))
                {
                    if (shouldW8ForEnterKeyUp)
                        shouldW8ForEnterKeyUp = false;
                }

            StartSteps:

                #region start
                if (IsStep(MenuStep.start_init))
                {
                    GameSaveLoadController.LoadGameState();
                    GameSaveLoadController.LoadPlayerState();

                    if (isIngame)
                        SetStep(MenuStep.ingameMenu_init);
                    else
                        SetStep(MenuStep.mainMenu_init);
                }
                #endregion

                #region end
                if (IsStep(MenuStep.end_init))
                {
                    DeactivateIt();

                    if (isIngame)
                        GameController.UnPauseGame(false);

                    return;
                }
                #endregion

                #region ingameEndWithEscapeKey
                if (IsStep(MenuStep.ingameEndWithEscapeKey_init))
                {
                    DeactivateIt();

                    shouldW8ForEscapeKeyUp = false;
                    shouldW8ForEnterKeyUp = false;

                    GameController.UnPauseGame(true);

                    return;
                }
                #endregion


                #region missionSelectMenu_init
                if (IsStep(MenuStep.missionSelectMenu_init))
                {
                    MenuGroup mgLB = GetChildGroupByName(MenuGroupName.missionSelect_levelButtons);
                    MenuGroup mgNB = GetChildGroupByName(MenuGroupName.missionSelect_numsBack);

                    mgLB.SetEnabled(true);
                    mgNB.SetEnabled(true);

                    for (int i = 0; i < mgLB.menuControls.Length; i++)
                    {
                        mgLB.menuControls[i].SetIsVisible(i < GameController.gameLastLevel);
                    }

                    MenuGroup mgOthers = GetChildGroupByName(MenuGroupName.missionSelect_others);
                    mgOthers.SetEnabled(true);

                    selectedTextureIndForLvlScreenshot = 0;

                    SetStep(MenuStep.missionSelectMenu_update);
                }
                #endregion

                #region missionSelectMenu_update
                if (IsStep(MenuStep.missionSelectMenu_update))
                {
                    if (IsEscapeKeyPressed_IfItIs_DisableItTillUp())
                    {
                        SetStep(MenuStep.missionSelectMenu_To_MainMenu);
                        goto StartSteps;
                    }

                    if (IsMenuEventHappened())
                    {
                        #region missionSelect_levelButtons
                        if (menuEventGroupName == MenuGroupName.missionSelect_levelButtons)
                        {
                            SetMenuEventISChecked();

                            switch (menuEventControlName)
                            {
                                case MenuControlName.btnLevel01:
                                    MissionSelect_SetSelectedLevel(1);
                                    SetStep(MenuStep.missionSelectMenu_To_Difficulty);
                                    goto StartSteps;
                                    break;

                                case MenuControlName.btnLevel02:
                                    MissionSelect_SetSelectedLevel(2);
                                    SetStep(MenuStep.missionSelectMenu_To_Difficulty);
                                    goto StartSteps;
                                    break;

                                case MenuControlName.btnLevel03:
                                    MissionSelect_SetSelectedLevel(3);
                                    SetStep(MenuStep.missionSelectMenu_To_Difficulty);
                                    goto StartSteps;
                                    break;

                                case MenuControlName.btnLevel04:
                                    MissionSelect_SetSelectedLevel(4);
                                    SetStep(MenuStep.missionSelectMenu_To_Difficulty);
                                    goto StartSteps;
                                    break;

                                case MenuControlName.btnLevel05:
                                    MissionSelect_SetSelectedLevel(5);
                                    SetStep(MenuStep.missionSelectMenu_To_Difficulty);
                                    goto StartSteps;
                                    break;

                                case MenuControlName.btnLevel06:
                                    MissionSelect_SetSelectedLevel(6);
                                    SetStep(MenuStep.missionSelectMenu_To_Difficulty);
                                    goto StartSteps;
                                    break;

                                case MenuControlName.btnLevel07:
                                    MissionSelect_SetSelectedLevel(7);
                                    SetStep(MenuStep.missionSelectMenu_To_Difficulty);
                                    goto StartSteps;
                                    break;

                                case MenuControlName.btnLevel08:
                                    MissionSelect_SetSelectedLevel(8);
                                    SetStep(MenuStep.missionSelectMenu_To_Difficulty);
                                    goto StartSteps;
                                    break;

                                case MenuControlName.btnLevel09:
                                    MissionSelect_SetSelectedLevel(9);
                                    SetStep(MenuStep.missionSelectMenu_To_Difficulty);
                                    goto StartSteps;
                                    break;

                                case MenuControlName.btnLevel10:
                                    MissionSelect_SetSelectedLevel(10);
                                    SetStep(MenuStep.missionSelectMenu_To_Difficulty);
                                    goto StartSteps;
                                    break;
                            }

                        }
                        #endregion

                        #region missionSelect_others
                        if (menuEventGroupName == MenuGroupName.missionSelect_others)
                        {
                            SetMenuEventISChecked();

                            switch (menuEventControlName)
                            {
                                case MenuControlName.btnBack:
                                    SetStep(MenuStep.missionSelectMenu_To_MainMenu);
                                    goto StartSteps;
                                    break;

                                //case MenuControlName.btnStart:
                                //    SetStep(MenuStep.missionSelectMenu_To_Difficulty);
                                //    goto StartSteps;
                                //    break;
                            }
                        }
                        #endregion
                    }

                    #region Mouse over level buttons

                    MenuGroup mgMisSelLvlButts = GetChildGroupByName(MenuGroupName.missionSelect_levelButtons);
                    MenuGroup mgMisSelOthers = GetChildGroupByName(MenuGroupName.missionSelect_others);

                    if (GameController.gameLastLevel >= 1)
                        if (IsMouseOverRect(mgMisSelLvlButts.GetChildControlByName(MenuControlName.btnLevel01)))
                            selectedTextureIndForLvlScreenshot = 0;
                    if (GameController.gameLastLevel >= 2)
                        if (IsMouseOverRect(mgMisSelLvlButts.GetChildControlByName(MenuControlName.btnLevel02)))
                            selectedTextureIndForLvlScreenshot = 1;
                    if (GameController.gameLastLevel >= 3)
                        if (IsMouseOverRect(mgMisSelLvlButts.GetChildControlByName(MenuControlName.btnLevel03)))
                            selectedTextureIndForLvlScreenshot = 2;
                    if (GameController.gameLastLevel >= 4)
                        if (IsMouseOverRect(mgMisSelLvlButts.GetChildControlByName(MenuControlName.btnLevel04)))
                            selectedTextureIndForLvlScreenshot = 3;
                    if (GameController.gameLastLevel >= 5)
                        if (IsMouseOverRect(mgMisSelLvlButts.GetChildControlByName(MenuControlName.btnLevel05)))
                            selectedTextureIndForLvlScreenshot = 4;
                    if (GameController.gameLastLevel >= 6)
                        if (IsMouseOverRect(mgMisSelLvlButts.GetChildControlByName(MenuControlName.btnLevel06)))
                            selectedTextureIndForLvlScreenshot = 5;
                    if (GameController.gameLastLevel >= 7)
                        if (IsMouseOverRect(mgMisSelLvlButts.GetChildControlByName(MenuControlName.btnLevel07)))
                            selectedTextureIndForLvlScreenshot = 6;
                    if (GameController.gameLastLevel >= 8)
                        if (IsMouseOverRect(mgMisSelLvlButts.GetChildControlByName(MenuControlName.btnLevel08)))
                            selectedTextureIndForLvlScreenshot = 7;
                    if (GameController.gameLastLevel >= 9)
                        if (IsMouseOverRect(mgMisSelLvlButts.GetChildControlByName(MenuControlName.btnLevel09)))
                            selectedTextureIndForLvlScreenshot = 8;
                    if (GameController.gameLastLevel >= 10)
                        if (IsMouseOverRect(mgMisSelLvlButts.GetChildControlByName(MenuControlName.btnLevel10)))
                            selectedTextureIndForLvlScreenshot = 9;

                    mgMisSelOthers.GetChildControlByName(MenuControlName.boxLevelScreenshot).SetSelectedTextureIndex(selectedTextureIndForLvlScreenshot);

                    #endregion
                }
                #endregion

                #region missionSelectMenu_To_Difficulty
                if (IsStep(MenuStep.missionSelectMenu_To_Difficulty))
                {
                    DeactiveMissionSelectMenu();

                    SetPrevStep(MenuStep.missionSelectMenu_init);

                    SetStep(MenuStep.difficulty_init);
                }
                #endregion

                #region missionSelectMenu_To_MainMenu
                if (IsStep(MenuStep.missionSelectMenu_To_MainMenu))
                {
                    RemoveMissionSelectMenu();

                    SetStep(MenuStep.mainMenu_init);
                }
                #endregion


                #region difficulty_init
                if (IsStep(MenuStep.difficulty_init))
                {
                    GetChildGroupByName(MenuGroupName.difficulty).SetEnabled(true);

                    SetStep(MenuStep.difficulty_update);
                }
                #endregion

                #region difficulty_update
                if (IsStep(MenuStep.difficulty_update))
                {
                    if (IsEscapeKeyPressed_IfItIs_DisableItTillUp())
                    {
                        SetStep(MenuStep.difficulty_back);
                        goto StartSteps;
                    }

                    if (IsMenuEventHappened())
                    {
                        #region difficulty
                        if (menuEventGroupName == MenuGroupName.difficulty)
                        {
                            SetMenuEventISChecked();

                            switch (menuEventControlName)
                            {
                                case MenuControlName.btnEasy:
                                    GameController.SetPlayerInitialHPCoef(1.225f);
                                    GameController.SetGameDifficulty(GameDifficulty.Easy);
                                    LoadSelectedLevelLoadingPage();
                                    break;

                                case MenuControlName.btnMedium:
                                    GameController.SetPlayerInitialHPCoef(1f);
                                    GameController.SetGameDifficulty(GameDifficulty.Medium);
                                    LoadSelectedLevelLoadingPage();
                                    break;

                                case MenuControlName.btnHard:
                                    GameController.SetPlayerInitialHPCoef(0.775f);
                                    GameController.SetGameDifficulty(GameDifficulty.Hard);
                                    LoadSelectedLevelLoadingPage();
                                    break;

                                case MenuControlName.btnBack:
                                    SetStep(MenuStep.difficulty_back);
                                    goto StartSteps;
                                    break;
                            }
                        }
                        #endregion
                    }
                }
                #endregion

                #region difficulty_back
                if (IsStep(MenuStep.difficulty_back))
                {
                    RemoveDifficultyDialogue();

                    SetStep(prevStep);
                }
                #endregion


                #region mainMenu_init
                if (IsStep(MenuStep.mainMenu_init))
                {
                    GetChildGroupByName(MenuGroupName.mainMenu_Main).SetEnabled(true);
                    GetChildGroupByName(MenuGroupName.mainMenu_Others).SetEnabled(true);

                    SetSelectedLevel(GameController.gameCurrentLevel);
                    SetSelectedLevelCheckPoint(GameController.gameCurrentLevelLastCheckPoint);

                    // GetMenuControlInGroup(MenuGroupName.mainMenu_Main, MenuControlName.btnOptions).SetIsActive(false);
                    //GetMenuControlInGroup(MenuGroupName.mainMenu_Main, MenuControlName.btnCredits).SetIsActive(false);

                    if (selectedLevel <= firstLvlNum && selectedLevelCheckPoint == firstCheckPointNum)
                        GetMenuControlInGroup(MenuGroupName.mainMenu_Main, MenuControlName.btnContinue).SetIsActive(false);
                    else
                        GetMenuControlInGroup(MenuGroupName.mainMenu_Main, MenuControlName.btnContinue).SetIsActive(true);

                    SetStep(MenuStep.mainMenu_update);
                }
                #endregion

                #region mainMenu_update
                if (IsStep(MenuStep.mainMenu_update))
                {
                    if (IsEscapeKeyPressed_IfItIs_DisableItTillUp())
                    {
                        SetStep(MenuStep.mainMenu_To_Quit);
                        goto StartSteps;
                    }

                    if (IsMenuEventHappened())
                    {
                        #region mainMenu_Main
                        if (menuEventGroupName == MenuGroupName.mainMenu_Main)
                        {
                            SetMenuEventISChecked();

                            switch (menuEventControlName)
                            {
                                #region btnNewGame
                                case MenuControlName.btnNewGame:

                                    SetSelectedLevel(firstLvlNum);
                                    SetSelectedLevelCheckPoint(firstCheckPointNum);

                                    if (WillMissionProgressBeRuined())
                                    {
                                        SetNextStep(MenuStep.mainMenu_To_Difficulty);
                                        SetStep(MenuStep.mainMenu_To_LastCheckpointWillBeLostDialogue);
                                        goto StartSteps;
                                    }
                                    else
                                    {
                                        SetStep(MenuStep.mainMenu_To_Difficulty);
                                        goto StartSteps;
                                    }
                                    break;
                                #endregion

                                #region btnContinue
                                case MenuControlName.btnContinue:

                                    SetStep(MenuStep.mainMenu_To_Difficulty);
                                    goto StartSteps;

                                    break;
                                #endregion

                                #region btnMissionSelect
                                case MenuControlName.btnMissionSelect:

                                    SetSelectedLevel(firstLvlNum);
                                    SetSelectedLevelCheckPoint(firstCheckPointNum);

                                    if (WillMissionProgressBeRuined())
                                    {
                                        SetNextStep(MenuStep.mainMenu_To_MissionSelect);
                                        SetStep(MenuStep.mainMenu_To_LastCheckpointWillBeLostDialogue);
                                        goto StartSteps;
                                    }
                                    else
                                    {
                                        SetStep(MenuStep.mainMenu_To_MissionSelect);
                                        goto StartSteps;
                                    }
                                    break;
                                #endregion

                                #region btnOptions
                                case MenuControlName.btnOptions:
                                    SetStep(MenuStep.mainMenu_To_Options);
                                    goto StartSteps;
                                    break;
                                #endregion

                                #region btnCredits
                                case MenuControlName.btnCredits:
                                    SetStep(MenuStep.mainMenu_To_Credits);
                                    goto StartSteps;
                                    break;
                                #endregion

                                #region btnQuit
                                case MenuControlName.btnQuit:
                                    SetStep(MenuStep.mainMenu_To_Quit);
                                    goto StartSteps;
                                    break;
                                #endregion
                            }
                        }
                        #endregion
                    }
                }
                #endregion

                #region mainMenu_To_LastCheckpointWillBeLostDialogue
                if (IsStep(MenuStep.mainMenu_To_LastCheckpointWillBeLostDialogue))
                {
                    DeactiveMainMenu();

                    SetPrevStep(MenuStep.mainMenu_init);

                    SetStep(MenuStep.lastCheckpointWillBeLostDialogue_init);
                }
                #endregion

                #region mainMenu_To_Difficulty
                if (IsStep(MenuStep.mainMenu_To_Difficulty))
                {
                    DeactiveMainMenu();

                    SetPrevStep(MenuStep.mainMenu_init);

                    SetStep(MenuStep.difficulty_init);
                }
                #endregion

                #region mainMenu_To_MissionSelect
                if (IsStep(MenuStep.mainMenu_To_MissionSelect))
                {
                    RemoveMainMenu();

                    SetStep(MenuStep.missionSelectMenu_init);
                }
                #endregion

                #region mainMenu_To_Options
                if (IsStep(MenuStep.mainMenu_To_Options))
                {
                    RemoveMainMenu();

                    SetPrevStep(MenuStep.mainMenu_init);

                    SetStep(MenuStep.options_init);
                }
                #endregion

                #region mainMenu_To_Credits
                if (IsStep(MenuStep.mainMenu_To_Credits))
                {
                    GameController.LoadCredits();
                }
                #endregion

                #region mainMenu_To_Quit
                if (IsStep(MenuStep.mainMenu_To_Quit))
                {
                    DeactiveMainMenu();

                    SetPrevStep(MenuStep.mainMenu_init);

                    SetStep(MenuStep.quitDialogue_init);
                }
                #endregion


                #region lastCheckpointWillBeLostDialogue_init
                if (IsStep(MenuStep.lastCheckpointWillBeLostDialogue_init))
                {
                    GetChildGroupByName(MenuGroupName.lastCheckpointWillBeLostDialogue).SetEnabled(true);

                    SetStep(MenuStep.lastCheckpointWillBeLostDialogue_update);
                }
                #endregion

                #region lastCheckpointWillBeLostDialogue_update
                if (IsStep(MenuStep.lastCheckpointWillBeLostDialogue_update))
                {
                    if (IsEscapeKeyPressed_IfItIs_DisableItTillUp())
                    {
                        SetStep(MenuStep.lastCheckpointWillBeLostDialogue_back);
                        goto StartSteps;
                    }

                    if (IsEnterKeyPressed_IfItIs_DisableItTillUp())
                    {
                        SetStep(MenuStep.lastCheckpointWillBeLostDialogue_To_Next);
                        goto StartSteps;
                    }

                    if (IsMenuEventHappened())
                    {
                        #region lastCheckpointWillBeLostDialogue
                        if (menuEventGroupName == MenuGroupName.lastCheckpointWillBeLostDialogue)
                        {
                            SetMenuEventISChecked();

                            switch (menuEventControlName)
                            {
                                case MenuControlName.btnYes:
                                    SetStep(MenuStep.lastCheckpointWillBeLostDialogue_To_Next);
                                    goto StartSteps;
                                    break;

                                case MenuControlName.btnNo:
                                    SetStep(MenuStep.lastCheckpointWillBeLostDialogue_back);
                                    goto StartSteps;
                                    break;
                            }
                        }
                        #endregion
                    }
                }
                #endregion

                #region lastCheckpointWillBeLostDialogue_back
                if (IsStep(MenuStep.lastCheckpointWillBeLostDialogue_back))
                {
                    RemoveLastCheckpointWillBeLostDialogue();

                    SetStep(prevStep);
                }
                #endregion

                #region lastCheckpointWillBeLostDialogue_To_Next
                if (IsStep(MenuStep.lastCheckpointWillBeLostDialogue_To_Next))
                {
                    RemoveLastCheckpointWillBeLostDialogue();

                    SetStep(nextStep);
                }
                #endregion


                #region quitDialogue_init
                if (IsStep(MenuStep.quitDialogue_init))
                {
                    GetChildGroupByName(MenuGroupName.quitDialogue).SetEnabled(true);

                    SetStep(MenuStep.quitDialogue_update);
                }
                #endregion

                #region quitDialogue_update
                if (IsStep(MenuStep.quitDialogue_update))
                {
                    if (IsEscapeKeyPressed_IfItIs_DisableItTillUp())
                    {
                        SetStep(MenuStep.quitDialogue_back);
                        goto StartSteps;
                    }

                    if (IsEnterKeyPressed_IfItIs_DisableItTillUp())
                    {
                        SetStep(MenuStep.quitDialogue_ExitApp);
                        goto StartSteps;
                    }

                    if (IsMenuEventHappened())
                    {
                        #region quitDialogue
                        if (menuEventGroupName == MenuGroupName.quitDialogue)
                        {
                            SetMenuEventISChecked();

                            switch (menuEventControlName)
                            {
                                case MenuControlName.btnYes:
                                    SetStep(MenuStep.quitDialogue_ExitApp);
                                    goto StartSteps;
                                    break;

                                case MenuControlName.btnNo:
                                    SetStep(MenuStep.quitDialogue_back);
                                    goto StartSteps;
                                    break;
                            }
                        }
                        #endregion
                    }
                }
                #endregion

                #region quitDialogue_back
                if (IsStep(MenuStep.quitDialogue_back))
                {
                    RemoveQuitDialogue();

                    SetStep(prevStep);
                }
                #endregion

                #region quitDialogue_ExitApp
                if (IsStep(MenuStep.quitDialogue_ExitApp))
                {
                    Application.Quit();

                    return;
                }
                #endregion


                //

                #region ingameMenu_init
                if (IsStep(MenuStep.ingameMenu_init))
                {
                    GetChildGroupByName(MenuGroupName.ingameMenu).SetEnabled(true);

                    SetSelectedLevel(GameController.gameCurrentLevel);
                    SetSelectedLevelCheckPoint(GameController.gameCurrentLevelLastCheckPoint);

                    //GetMenuControlInGroup(MenuGroupName.ingameMenu, MenuControlName.btnOptions).SetIsActive(false);

                    SetStep(MenuStep.ingameMenu_update);
                }
                #endregion

                #region ingameMenu_update
                if (IsStep(MenuStep.ingameMenu_update))
                {
                    if (IsEscapeKeyPressed_IfItIs_DisableItTillUp())
                    {
                        SetStep(MenuStep.ingameEndWithEscapeKey_init);
                        goto StartSteps;
                    }

                    if (IsMenuEventHappened())
                    {
                        #region ingameMenu
                        if (menuEventGroupName == MenuGroupName.ingameMenu)
                        {
                            SetMenuEventISChecked();

                            switch (menuEventControlName)
                            {
                                #region btnContinue
                                case MenuControlName.btnContinue:
                                    SetStep(MenuStep.end_init);
                                    goto StartSteps;
                                #endregion

                                #region btnOptions
                                case MenuControlName.btnOptions:
                                    SetStep(MenuStep.ingameMenu_To_Options);
                                    goto StartSteps;
                                    break;
                                #endregion

                                #region btnLastCheckPoint
                                case MenuControlName.btnLastCheckPoint:
                                    SetStep(MenuStep.ingameMenu_To_LastCheckpointDialogue);
                                    goto StartSteps;
                                    break;
                                #endregion

                                #region btnRestart
                                case MenuControlName.btnRestart:
                                    SetStep(MenuStep.ingameMenu_To_RestartMissionDialgoue);
                                    goto StartSteps;
                                    break;
                                #endregion

                                #region btnQuit
                                case MenuControlName.btnQuit:
                                    SetStep(MenuStep.ingameMenu_To_SaveAndQuitDialogue);
                                    goto StartSteps;
                                    break;
                                #endregion
                            }
                        }
                        #endregion
                    }
                }
                #endregion

                #region ingameMenu_To_LastCheckpointDialogue
                if (IsStep(MenuStep.ingameMenu_To_LastCheckpointDialogue))
                {
                    DeactiveIngameMenu();

                    SetPrevStep(MenuStep.end_init);

                    SetStep(MenuStep.lastCheckpointDialogue_init);
                }
                #endregion

                #region ingameMenu_To_RestartMissionDialgoue
                if (IsStep(MenuStep.ingameMenu_To_RestartMissionDialgoue))
                {
                    DeactiveIngameMenu();

                    SetPrevStep(MenuStep.end_init);

                    SetStep(MenuStep.restartMissionDialogue_init);
                }
                #endregion

                #region ingameMenu_To_Options
                if (IsStep(MenuStep.ingameMenu_To_Options))
                {
                    RemoveIngameMenu();

                    SetPrevStep(MenuStep.ingameMenu_init);

                    SetStep(MenuStep.options_init);
                }
                #endregion

                #region ingameMenu_To_SaveAndQuitDialogue
                if (IsStep(MenuStep.ingameMenu_To_SaveAndQuitDialogue))
                {
                    DeactiveIngameMenu();

                    SetPrevStep(MenuStep.end_init);

                    SetStep(MenuStep.saveAndQuitDialogue_init);
                }
                #endregion


                #region lastCheckpointDialogue_init
                if (IsStep(MenuStep.lastCheckpointDialogue_init))
                {
                    GetChildGroupByName(MenuGroupName.ingame_lastCheckpointDialogue).SetEnabled(true);

                    SetStep(MenuStep.lastCheckpointDialogue_update);
                }
                #endregion

                #region lastCheckpointDialogue_update
                if (IsStep(MenuStep.lastCheckpointDialogue_update))
                {
                    if (IsEscapeKeyPressed_IfItIs_DisableItTillUp())
                    {
                        SetStep(MenuStep.lastCheckpointDialogue_back);
                        goto StartSteps;
                    }

                    if (IsEnterKeyPressed_IfItIs_DisableItTillUp())
                    {
                        SetStep(MenuStep.lastCheckpointDialogue_LoadLastCheckpoint);
                        goto StartSteps;
                    }

                    if (IsMenuEventHappened())
                    {
                        #region ingame_lastCheckpointDialogue
                        if (menuEventGroupName == MenuGroupName.ingame_lastCheckpointDialogue)
                        {
                            SetMenuEventISChecked();

                            switch (menuEventControlName)
                            {
                                case MenuControlName.btnYes:
                                    SetStep(MenuStep.lastCheckpointDialogue_LoadLastCheckpoint);
                                    goto StartSteps;
                                    break;

                                case MenuControlName.btnNo:
                                    SetStep(MenuStep.lastCheckpointDialogue_back);
                                    goto StartSteps;
                                    break;
                            }
                        }
                        #endregion
                    }
                }
                #endregion

                #region lastCheckpointDialogue_back
                if (IsStep(MenuStep.lastCheckpointDialogue_back))
                {
                    RemoveLastCheckpointDialogue();

                    SetStep(prevStep);
                }
                #endregion

                #region lastCheckpointDialogue_LoadLastCheckpoint
                if (IsStep(MenuStep.lastCheckpointDialogue_LoadLastCheckpoint))
                {
                    LoadSelectedLevel();

                    return;
                }
                #endregion


                #region restartMissionDialogue_init
                if (IsStep(MenuStep.restartMissionDialogue_init))
                {
                    GetChildGroupByName(MenuGroupName.ingame_restartMissionDialogue).SetEnabled(true);

                    SetStep(MenuStep.restartMissionDialogue_update);
                }
                #endregion

                #region restartMissionDialogue_update
                if (IsStep(MenuStep.restartMissionDialogue_update))
                {
                    if (IsEscapeKeyPressed_IfItIs_DisableItTillUp())
                    {
                        SetStep(MenuStep.restartMissionDialogue_back);
                        goto StartSteps;
                    }

                    if (IsEnterKeyPressed_IfItIs_DisableItTillUp())
                    {
                        SetStep(MenuStep.restartMissionDialogue_RestartMission);
                        goto StartSteps;
                    }

                    if (IsMenuEventHappened())
                    {
                        #region ingame_restartMissionDialogue
                        if (menuEventGroupName == MenuGroupName.ingame_restartMissionDialogue)
                        {
                            SetMenuEventISChecked();

                            switch (menuEventControlName)
                            {
                                case MenuControlName.btnYes:
                                    SetStep(MenuStep.restartMissionDialogue_RestartMission);
                                    goto StartSteps;
                                    break;

                                case MenuControlName.btnNo:
                                    SetStep(MenuStep.restartMissionDialogue_back);
                                    goto StartSteps;
                                    break;
                            }
                        }
                        #endregion
                    }
                }
                #endregion

                #region restartMissionDialogue_back
                if (IsStep(MenuStep.restartMissionDialogue_back))
                {
                    RemoveRestartMissionDialogue();

                    SetStep(prevStep);
                }
                #endregion

                #region restartMissionDialogue_RestartMission
                if (IsStep(MenuStep.restartMissionDialogue_RestartMission))
                {
                    SetSelectedLevelCheckPoint(firstCheckPointNum);
                    LoadSelectedLevel();

                    return;
                }
                #endregion


                #region saveAndQuitDialogue_init
                if (IsStep(MenuStep.saveAndQuitDialogue_init))
                {
                    GetChildGroupByName(MenuGroupName.ingame_saveAndQuitDialogue).SetEnabled(true);

                    SetStep(MenuStep.saveAndQuitDialogue_update);
                }
                #endregion

                #region saveAndQuitDialogue_update
                if (IsStep(MenuStep.saveAndQuitDialogue_update))
                {
                    if (IsEscapeKeyPressed_IfItIs_DisableItTillUp())
                    {
                        SetStep(MenuStep.saveAndQuitDialogue_back);
                        goto StartSteps;
                    }

                    if (IsEnterKeyPressed_IfItIs_DisableItTillUp())
                    {
                        SetStep(MenuStep.saveAndQuitDialogue_QuitToMainMenu);
                        goto StartSteps;
                    }

                    if (IsMenuEventHappened())
                    {
                        #region ingame_saveAndQuitDialogue
                        if (menuEventGroupName == MenuGroupName.ingame_saveAndQuitDialogue)
                        {
                            SetMenuEventISChecked();

                            switch (menuEventControlName)
                            {
                                case MenuControlName.btnYes:
                                    SetStep(MenuStep.saveAndQuitDialogue_QuitToMainMenu);
                                    goto StartSteps;
                                    break;

                                case MenuControlName.btnNo:
                                    SetStep(MenuStep.saveAndQuitDialogue_back);
                                    goto StartSteps;
                                    break;
                            }
                        }
                        #endregion
                    }
                }
                #endregion

                #region saveAndQuitDialogue_back
                if (IsStep(MenuStep.saveAndQuitDialogue_back))
                {
                    RemoveSaveAndQuitDialogue();

                    SetStep(prevStep);
                }
                #endregion

                #region saveAndQuitDialogue_QuitToMainMenu
                if (IsStep(MenuStep.saveAndQuitDialogue_QuitToMainMenu))
                {
                    GameController.LoadMainMenu();

                    return;
                }
                #endregion


                #region options_init
                if (IsStep(MenuStep.options_init))
                {
                    GetChildGroupByName(MenuGroupName.options).SetEnabled(true);
                    SetActiveOptionsPage(ActiveOptionsPage.Video);

                    MenuControl_OptionsControls_MouseSensitivitySlider.sliderCurValue = CustomInputManager.sensitivityX;
                    MenuControl_OptionsControls_MouseSensitivityText.text = GetDoRaghamAsharTextFromFloat(CustomInputManager.sensitivityX);
                    MenuControl_OptionsControls_InvertMouseToggle.toggleValue = CustomInputManager.invertMouse;
                    MenuControl_OptionsControls_UseMouseWheelToChangeWeaponToggle.toggleValue = CustomInputManager.useMouseWheelToChangeWeapon;

                    //

                    MenuControl_OptionsAudio_MasterVolumeSlider.sliderCurValue = AudioController.GeneralVolume;
                    MenuControl_OptionsAudio_MasterVolumeText.text = GetDoRaghamAsharTextFromFloat(AudioController.GeneralVolume);
                    MenuControl_OptionsAudio_SFXVolumeSlider.sliderCurValue = AudioController.sfxVolume;
                    MenuControl_OptionsAudio_SFXVolumeText.text = GetDoRaghamAsharTextFromFloat(AudioController.sfxVolume);
                    MenuControl_OptionsAudio_MusicVolumeSlider.sliderCurValue = AudioController.musicVolume;
                    MenuControl_OptionsAudio_MusicVolumeText.text = GetDoRaghamAsharTextFromFloat(AudioController.musicVolume);

                    //

                    #region Init Combo_Resolutions
                    if (!MenuControl_OptionsVideo_ResolutionsCombo.comboIsInited)
                    {
                        MenuControl_OptionsVideo_ResolutionsCombo.comboIsInited = true;

                        for (int i = 0; i < Screen.resolutions.Length; i++)
                        {
                            ComboBoxItem comboItem = null;



                            if (MenuControl_OptionsVideo_ResolutionsCombo.comboIsMini)
                            {
                                comboItem = GameObject.Instantiate(comboTempRefItem_Mini) as ComboBoxItem;

                                CopyGUIStyleContent(comboItem.guiSkin_Active.button, miniComboItemRef.guiSkin_Active.button);
                                CopyGUIStyleContent(comboItem.guiSkin_OnTop.button, miniComboItemRef.guiSkin_OnTop.button);
                                CopyGUIStyleContent(comboItem.guiSkin.button, miniComboItemRef.guiSkin.button);
                            }
                            else
                            {
                                comboItem = GameObject.Instantiate(comboTempRefItem_Normal) as ComboBoxItem;

                                CopyGUIStyleContent(comboItem.guiSkin_Active.button, comboItemRef.guiSkin_Active.button);
                                CopyGUIStyleContent(comboItem.guiSkin_OnTop.button, comboItemRef.guiSkin_OnTop.button);
                                CopyGUIStyleContent(comboItem.guiSkin.button, comboItemRef.guiSkin.button);
                            }

                            comboItem.transform.parent = MenuControl_OptionsVideo_ResolutionsCombo.transform;

                            comboItem.id = i;
                            comboItem.name = "Resolution " + (i + 1).ToString();
                            comboItem.text = Screen.resolutions[i].width.ToString() + " x " + Screen.resolutions[i].height.ToString();
                            comboItem.additionalNums.Add((float)(Screen.resolutions[i].width));
                            comboItem.additionalNums.Add((float)(Screen.resolutions[i].height));

                            MenuControl_OptionsVideo_ResolutionsCombo.comboItems.Add(comboItem);
                        }

                        MenuControl_OptionsVideo_ResolutionsCombo.comboSelectedIndex = VideoSettingsController.curResolutionIndex;
                    }
                    #endregion

                    MenuControl_OptionsVideo_BrightnessSlider.sliderCurValue = VideoSettingsController.curBrightness;
                    MenuControl_OptionsVideo_BrightnessText.text = GetDoRaghamAsharTextFromFloat(VideoSettingsController.curBrightness);

                    #region Init Combo_TexturesQuality
                    if (!MenuControl_OptionsVideo_TexturesQualityCombo.comboIsInited)
                    {
                        MenuControl_OptionsVideo_TexturesQualityCombo.comboIsInited = true;

                        for (int i = 0; i < 3; i++)
                        {
                            ComboBoxItem comboItem = null;

                            if (MenuControl_OptionsVideo_TexturesQualityCombo.comboIsMini)
                            {
                                comboItem = GameObject.Instantiate(comboTempRefItem_Mini) as ComboBoxItem;

                                CopyGUIStyleContent(comboItem.guiSkin_Active.button, miniComboItemRef.guiSkin_Active.button);
                                CopyGUIStyleContent(comboItem.guiSkin_OnTop.button, miniComboItemRef.guiSkin_OnTop.button);
                                CopyGUIStyleContent(comboItem.guiSkin.button, miniComboItemRef.guiSkin.button);
                            }
                            else
                            {
                                comboItem = GameObject.Instantiate(comboTempRefItem_Normal) as ComboBoxItem;

                                CopyGUIStyleContent(comboItem.guiSkin_Active.button, comboItemRef.guiSkin_Active.button);
                                CopyGUIStyleContent(comboItem.guiSkin_OnTop.button, comboItemRef.guiSkin_OnTop.button);
                                CopyGUIStyleContent(comboItem.guiSkin.button, comboItemRef.guiSkin.button);
                            }

                            comboItem.transform.parent = MenuControl_OptionsVideo_TexturesQualityCombo.transform;

                            comboItem.id = i;
                            comboItem.name = "Texture quality " + (i + 1).ToString();

                            switch (i)
                            {
                                case 0:
                                    comboItem.text = "Low";
                                    break;

                                case 1:
                                    comboItem.text = "Medium";
                                    break;

                                case 2:
                                    comboItem.text = "High";
                                    break;
                            }

                            MenuControl_OptionsVideo_TexturesQualityCombo.comboItems.Add(comboItem);
                        }

                        MenuControl_OptionsVideo_TexturesQualityCombo.comboSelectedIndex = GetTextureQualityIndexByItsName(VideoSettingsController.curTextureQual);
                    }
                    #endregion

                    #region Init Combo_OverallQuality
                    if (!MenuControl_OptionsVideo_OverallQualityCombo.comboIsInited)
                    {
                        MenuControl_OptionsVideo_OverallQualityCombo.comboIsInited = true;

                        for (int i = 0; i < 6; i++)
                        {
                            ComboBoxItem comboItem = null;

                            if (MenuControl_OptionsVideo_OverallQualityCombo.comboIsMini)
                            {
                                comboItem = GameObject.Instantiate(comboTempRefItem_Mini) as ComboBoxItem;

                                CopyGUIStyleContent(comboItem.guiSkin_Active.button, miniComboItemRef.guiSkin_Active.button);
                                CopyGUIStyleContent(comboItem.guiSkin_OnTop.button, miniComboItemRef.guiSkin_OnTop.button);
                                CopyGUIStyleContent(comboItem.guiSkin.button, miniComboItemRef.guiSkin.button);
                            }
                            else
                            {
                                comboItem = GameObject.Instantiate(comboTempRefItem_Normal) as ComboBoxItem;

                                CopyGUIStyleContent(comboItem.guiSkin_Active.button, comboItemRef.guiSkin_Active.button);
                                CopyGUIStyleContent(comboItem.guiSkin_OnTop.button, comboItemRef.guiSkin_OnTop.button);
                                CopyGUIStyleContent(comboItem.guiSkin.button, comboItemRef.guiSkin.button);
                            }

                            comboItem.transform.parent = MenuControl_OptionsVideo_OverallQualityCombo.transform;

                            comboItem.id = i;
                            comboItem.name = "Overall quality " + (i + 1).ToString();

                            switch (i)
                            {
                                case 0:
                                    comboItem.text = "Low";
                                    break;

                                case 1:
                                    comboItem.text = "Medium";
                                    break;

                                case 2:
                                    comboItem.text = "High";
                                    break;

                                case 3:
                                    comboItem.text = "Very High";
                                    break;

                                case 4:
                                    comboItem.text = "Ultra";
                                    break;

                                case 5:
                                    comboItem.text = "Custom";
                                    break;
                            }

                            MenuControl_OptionsVideo_OverallQualityCombo.comboItems.Add(comboItem);
                        }

                        MenuControl_OptionsVideo_OverallQualityCombo.comboSelectedIndex = GetOverallQualityIndexByItsName(VideoSettingsController.curVideoSettingType);
                    }
                    #endregion

                    MenuControl_OptionsVideo_AnisotropicToggle.toggleValue = VideoSettingsController.curUseAnisotropic;
                    MenuControl_OptionsVideo_SSAOToggle.toggleValue = VideoSettingsController.curUseSSAO;
                    MenuControl_OptionsVideo_BloomToggle.toggleValue = VideoSettingsController.curUseBloom;
                    MenuControl_OptionsVideo_ShadowOnToggle.toggleValue = VideoSettingsController.curIsShadowOn;
                    //MenuControl_OptionsVideo_ShadowDistanceSlider.sliderCurValue = VideoSettingsController.curShadowDistance;
                    //MenuControl_OptionsVideo_ShadowDistanceText.text = GetIntTextFromFloat(VideoSettingsController.curShadowDistance);
                    MenuControl_OptionsVideo_VSyncToggle.toggleValue = VideoSettingsController.curIsVSyncOn;

                    #region Init Combo_ShadowQuality
                    if (!MenuControl_OptionsVideo_ShadowQualityCombo.comboIsInited)
                    {
                        MenuControl_OptionsVideo_ShadowQualityCombo.comboIsInited = true;

                        for (int i = 0; i < 3; i++)
                        {
                            ComboBoxItem comboItem = null;

                            if (MenuControl_OptionsVideo_ShadowQualityCombo.comboIsMini)
                            {
                                comboItem = GameObject.Instantiate(comboTempRefItem_Mini) as ComboBoxItem;

                                CopyGUIStyleContent(comboItem.guiSkin_Active.button, miniComboItemRef.guiSkin_Active.button);
                                CopyGUIStyleContent(comboItem.guiSkin_OnTop.button, miniComboItemRef.guiSkin_OnTop.button);
                                CopyGUIStyleContent(comboItem.guiSkin.button, miniComboItemRef.guiSkin.button);
                            }
                            else
                            {
                                comboItem = GameObject.Instantiate(comboTempRefItem_Normal) as ComboBoxItem;

                                CopyGUIStyleContent(comboItem.guiSkin_Active.button, comboItemRef.guiSkin_Active.button);
                                CopyGUIStyleContent(comboItem.guiSkin_OnTop.button, comboItemRef.guiSkin_OnTop.button);
                                CopyGUIStyleContent(comboItem.guiSkin.button, comboItemRef.guiSkin.button);
                            }

                            comboItem.transform.parent = MenuControl_OptionsVideo_ShadowQualityCombo.transform;

                            comboItem.id = i;
                            comboItem.name = "Shadow quality " + (i + 1).ToString();

                            switch (i)
                            {
                                case 0:
                                    comboItem.text = "Low";
                                    break;

                                case 1:
                                    comboItem.text = "Medium";
                                    break;

                                case 2:
                                    comboItem.text = "High";
                                    break;
                            }

                            MenuControl_OptionsVideo_ShadowQualityCombo.comboItems.Add(comboItem);
                        }

                        MenuControl_OptionsVideo_ShadowQualityCombo.comboSelectedIndex = GetShadowQualityIndexByItsName(VideoSettingsController.curShadowQual);
                    }
                    #endregion

                    SetStep(MenuStep.options_update);
                }
                #endregion

                #region options_update
                if (IsStep(MenuStep.options_update))
                {
                    CustomInputManager.sensitivityX = MenuControl_OptionsControls_MouseSensitivitySlider.sliderCurValue;
                    CustomInputManager.sensitivityY = CustomInputManager.sensitivityX;
                    MenuControl_OptionsControls_MouseSensitivityText.text = GetDoRaghamAsharTextFromFloat(CustomInputManager.sensitivityX);
                    CustomInputManager.invertMouse = MenuControl_OptionsControls_InvertMouseToggle.toggleValue;
                    CustomInputManager.useMouseWheelToChangeWeapon = MenuControl_OptionsControls_UseMouseWheelToChangeWeaponToggle.toggleValue;

                    //

                    AudioController.SetVolume_General(MenuControl_OptionsAudio_MasterVolumeSlider.sliderCurValue);
                    MenuControl_OptionsAudio_MasterVolumeText.text = GetDoRaghamAsharTextFromFloat(AudioController.GeneralVolume);
                    AudioController.SetVolume_SFX(MenuControl_OptionsAudio_SFXVolumeSlider.sliderCurValue);
                    AudioController.SetVolume_Voice(MenuControl_OptionsAudio_SFXVolumeSlider.sliderCurValue);
                    MenuControl_OptionsAudio_SFXVolumeText.text = GetDoRaghamAsharTextFromFloat(AudioController.sfxVolume);
                    AudioController.SetVolume_Music(MenuControl_OptionsAudio_MusicVolumeSlider.sliderCurValue);
                    MenuControl_OptionsAudio_MusicVolumeText.text = GetDoRaghamAsharTextFromFloat(AudioController.musicVolume);

                    //

                    #region IsReloadingVideoSettingsControlsNeeded
                    if (isReloadingVideoSettingsControlsNeeded)
                    {
                        isReloadingVideoSettingsControlsNeeded = false;

                        vidSettings_Selected_ResolutionIndex = VideoSettingsController.curResolutionIndex;
                        vidSettings_Selected_ResolutionIndex_Old = VideoSettingsController.curResolutionIndex;
                        vidSettings_Selected_SSAO = VideoSettingsController.curUseSSAO;
                        vidSettings_Selected_SSAO_Old = VideoSettingsController.curUseSSAO;
                        vidSettings_Selected_Bloom = VideoSettingsController.curUseBloom;
                        vidSettings_Selected_Bloom_Old = VideoSettingsController.curUseBloom;
                        vidSettings_Selected_Brightness = VideoSettingsController.curBrightness;
                        vidSettings_Selected_Brightness_Old = VideoSettingsController.curBrightness;
                        vidSettings_Selected_Shadows = VideoSettingsController.curIsShadowOn;
                        vidSettings_Selected_Shadows_Old = VideoSettingsController.curIsShadowOn;
                        vidSettings_Selected_ShadowQual = VideoSettingsController.curShadowQual;
                        vidSettings_Selected_ShadowQual_Old = VideoSettingsController.curShadowQual;
                        vidSettings_Selected_ShadowDistance = VideoSettingsController.curShadowDistance;
                        vidSettings_Selected_ShadowDistance_Old = VideoSettingsController.curShadowDistance;
                        vidSettings_Selected_TextureQual = VideoSettingsController.curTextureQual;
                        vidSettings_Selected_TextureQual_Old = VideoSettingsController.curTextureQual;
                        vidSettings_Selected_VidSetType = VideoSettingsController.curVideoSettingType;
                        vidSettings_Selected_VidSetType_Old = VideoSettingsController.curVideoSettingType;
                        vidSettings_Selected_VSync = VideoSettingsController.curIsVSyncOn;
                        vidSettings_Selected_VSync_Old = VideoSettingsController.curIsVSyncOn;
                        vidSettings_Selected_Anisotropic = VideoSettingsController.curUseAnisotropic;
                        vidSettings_Selected_Anisotropic_Old = VideoSettingsController.curUseAnisotropic;

                        //

                        MenuControl_OptionsVideo_ResolutionsCombo.comboSelectedIndex = vidSettings_Selected_ResolutionIndex;
                        MenuControl_OptionsVideo_BrightnessSlider.sliderCurValue = vidSettings_Selected_Brightness;
                        MenuControl_OptionsVideo_BrightnessText.text = GetDoRaghamAsharTextFromFloat(VideoSettingsController.curBrightness);
                        MenuControl_OptionsVideo_TexturesQualityCombo.comboSelectedIndex = GetTextureQualityIndexByItsName(vidSettings_Selected_TextureQual);
                        MenuControl_OptionsVideo_OverallQualityCombo.comboSelectedIndex = GetOverallQualityIndexByItsName(vidSettings_Selected_VidSetType);
                        MenuControl_OptionsVideo_AnisotropicToggle.toggleValue = vidSettings_Selected_Anisotropic;
                        MenuControl_OptionsVideo_SSAOToggle.toggleValue = vidSettings_Selected_SSAO;
                        MenuControl_OptionsVideo_BloomToggle.toggleValue = vidSettings_Selected_Bloom;
                        MenuControl_OptionsVideo_ShadowOnToggle.toggleValue = vidSettings_Selected_Shadows;
                        //MenuControl_OptionsVideo_ShadowDistanceSlider.sliderCurValue = vidSettings_Selected_ShadowDistance;
                        //MenuControl_OptionsVideo_ShadowDistanceText.text = GetIntTextFromFloat(VideoSettingsController.curShadowDistance);
                        MenuControl_OptionsVideo_VSyncToggle.toggleValue = vidSettings_Selected_VSync;
                        MenuControl_OptionsVideo_ShadowQualityCombo.comboSelectedIndex = GetShadowQualityIndexByItsName(vidSettings_Selected_ShadowQual);
                    }
                    #endregion

                    #region MenuControl_OptionsVideo_ResolutionsCombo.isChanged
                    if (MenuControl_OptionsVideo_ResolutionsCombo.isChanged)
                    {
                        MenuControl_OptionsVideo_ResolutionsCombo.isChanged = false;

                        SetAnyVideoSettingsControlChanged(true);

                        vidSettings_Selected_ResolutionIndex = MenuControl_OptionsVideo_ResolutionsCombo.comboSelectedIndex;
                    }
                    #endregion

                    #region Brightness
                    VideoSettingsController.SetBightness(MenuControl_OptionsVideo_BrightnessSlider.sliderCurValue);
                    MenuControl_OptionsVideo_BrightnessText.text = GetDoRaghamAsharTextFromFloat(VideoSettingsController.curBrightness);

                    #region MenuControl_OptionsVideo_BrightnessSlider.isChanged
                    if (MenuControl_OptionsVideo_BrightnessSlider.isChanged)
                    {
                        MenuControl_OptionsVideo_BrightnessSlider.isChanged = false;

                        SetAnyVideoSettingsControlChanged(true);

                        vidSettings_Selected_Brightness = MenuControl_OptionsVideo_BrightnessSlider.sliderCurValue;
                    }
                    #endregion
                    #endregion

                    #region MenuControl_OptionsVideo_TexturesQualityCombo.isChanged
                    if (MenuControl_OptionsVideo_TexturesQualityCombo.isChanged)
                    {
                        MenuControl_OptionsVideo_TexturesQualityCombo.isChanged = false;

                        SetAnyVideoSettingsControlChanged(true);

                        vidSettings_Selected_TextureQual = GetTextureQualityNameByItsIndex(MenuControl_OptionsVideo_TexturesQualityCombo.comboSelectedIndex);

                        ChangeVideoPresetToCustom();
                    }
                    #endregion

                    #region MenuControl_OptionsVideo_OverallQualityCombo.isChanged
                    if (MenuControl_OptionsVideo_OverallQualityCombo.isChanged)
                    {
                        MenuControl_OptionsVideo_OverallQualityCombo.isChanged = false;

                        SetAnyVideoSettingsControlChanged(true);

                        ChangeControlsAndNeedValuesToPreset(GetOverallQualityNameByItsIndex(MenuControl_OptionsVideo_OverallQualityCombo.comboSelectedIndex));
                    }
                    #endregion

                    #region MenuControl_OptionsVideo_AnisotropicToggle.isChanged
                    if (MenuControl_OptionsVideo_AnisotropicToggle.isChanged)
                    {
                        MenuControl_OptionsVideo_AnisotropicToggle.isChanged = false;

                        SetAnyVideoSettingsControlChanged(true);

                        vidSettings_Selected_Anisotropic = MenuControl_OptionsVideo_AnisotropicToggle.toggleValue;

                        ChangeVideoPresetToCustom();
                    }
                    #endregion

                    #region MenuControl_OptionsVideo_SSAOToggle.isChanged
                    if (MenuControl_OptionsVideo_SSAOToggle.isChanged)
                    {
                        MenuControl_OptionsVideo_SSAOToggle.isChanged = false;

                        SetAnyVideoSettingsControlChanged(true);

                        vidSettings_Selected_SSAO = MenuControl_OptionsVideo_SSAOToggle.toggleValue;

                        ChangeVideoPresetToCustom();
                    }
                    #endregion

                    #region MenuControl_OptionsVideo_BloomToggle.isChanged
                    if (MenuControl_OptionsVideo_BloomToggle.isChanged)
                    {
                        MenuControl_OptionsVideo_BloomToggle.isChanged = false;

                        SetAnyVideoSettingsControlChanged(true);

                        vidSettings_Selected_Bloom = MenuControl_OptionsVideo_BloomToggle.toggleValue;

                        ChangeVideoPresetToCustom();
                    }
                    #endregion

                    #region MenuControl_OptionsVideo_ShadowOnToggle.isChanged
                    if (MenuControl_OptionsVideo_ShadowOnToggle.isChanged)
                    {
                        MenuControl_OptionsVideo_ShadowOnToggle.isChanged = false;

                        SetAnyVideoSettingsControlChanged(true);

                        vidSettings_Selected_Shadows = MenuControl_OptionsVideo_ShadowOnToggle.toggleValue;

                        ChangeVideoPresetToCustom();
                    }
                    #endregion

                    //MenuControl_OptionsVideo_ShadowDistanceSlider.SetIsActive(MenuControl_OptionsVideo_ShadowOnToggle.toggleValue);
                    //MenuControl_OptionsVideo_ShadowDistanceText.SetIsActive(MenuControl_OptionsVideo_ShadowOnToggle.toggleValue);
                    MenuControl_OptionsVideo_ShadowQualityCombo.SetIsActive(MenuControl_OptionsVideo_ShadowOnToggle.toggleValue);
                    //MenuControl_OptionsVideo_ShadowLabel.SetIsActive(MenuControl_OptionsVideo_ShadowOnToggle.toggleValue);
                    //MenuControl_OptionsVideo_ShadowDistanceLabel.SetIsActive(MenuControl_OptionsVideo_ShadowOnToggle.toggleValue);
                    MenuControl_OptionsVideo_ShadowQualityLabel.SetIsActive(MenuControl_OptionsVideo_ShadowOnToggle.toggleValue);

                    #region ShadowDistance Slider
                    //VideoSettingsController.SetShadowDistance(MenuControl_OptionsVideo_ShadowDistanceSlider.sliderCurValue);
                    //MenuControl_OptionsVideo_ShadowDistanceText.text = GetIntTextFromFloat(VideoSettingsController.curShadowDistance);

                    //#region MenuControl_OptionsVideo_ShadowDistanceSlider.isChanged
                    //if (MenuControl_OptionsVideo_ShadowDistanceSlider.isChanged)
                    //{
                    //    MenuControl_OptionsVideo_ShadowDistanceSlider.isChanged = false;

                    //    SetAnyVideoSettingsControlChanged(true);

                    //    vidSettings_Selected_ShadowDistance = MenuControl_OptionsVideo_ShadowDistanceSlider.sliderCurValue;

                    //    ChangeVideoPresetToCustom();
                    //}
                    //#endregion
                    #endregion

                    #region MenuControl_OptionsVideo_ShadowQualityCombo.isChanged
                    if (MenuControl_OptionsVideo_ShadowQualityCombo.isChanged)
                    {
                        MenuControl_OptionsVideo_ShadowQualityCombo.isChanged = false;

                        SetAnyVideoSettingsControlChanged(true);

                        vidSettings_Selected_ShadowQual = GetShadowQualityNameByItsIndex(MenuControl_OptionsVideo_ShadowQualityCombo.comboSelectedIndex);

                        ChangeVideoPresetToCustom();
                    }
                    #endregion

                    #region MenuControl_OptionsVideo_VSyncToggle.isChanged
                    if (MenuControl_OptionsVideo_VSyncToggle.isChanged)
                    {
                        MenuControl_OptionsVideo_VSyncToggle.isChanged = false;

                        SetAnyVideoSettingsControlChanged(true);

                        vidSettings_Selected_VSync = MenuControl_OptionsVideo_VSyncToggle.toggleValue;
                    }
                    #endregion

                    #region MenuControl_OptionsVideo_ApplyButton.SetIsActive
                    if (isAnyVideoSettingsControlChanged)
                    {
                        MenuControl_OptionsVideo_ApplyButton.SetIsActive(true);
                    }
                    else
                    {
                        MenuControl_OptionsVideo_ApplyButton.SetIsActive(false);
                    }
                    #endregion

                    // /////////////////////////

                    if (IsEscapeKeyPressed_IfItIs_DisableItTillUp())
                    {
                        if (isAnyVideoSettingsControlChanged)
                        {
                            neededPageToGoFromVideoOptions = VideoOptionsNeededPageToGo.ExitOptions;
                            SetStep(MenuStep.options_Video_ApplyDialogue_init);
                        }
                        else
                        {
                            SetStep(MenuStep.options_back);
                            goto StartSteps;
                        }
                    }

                    if (IsMenuEventHappened())
                    {
                        #region optionsPageButtons
                        if (menuEventGroupName == MenuGroupName.options)
                        {
                            SetMenuEventISChecked();

                            switch (menuEventControlName)
                            {
                                case MenuControlName.btnOptionsControls:
                                    if (isAnyVideoSettingsControlChanged)
                                    {
                                        neededPageToGoFromVideoOptions = VideoOptionsNeededPageToGo.Controls;
                                        SetStep(MenuStep.options_Video_ApplyDialogue_init);
                                    }
                                    else
                                    {
                                        SetActiveOptionsPage(ActiveOptionsPage.Controls);
                                    }
                                    break;

                                case MenuControlName.btnOptionsVideo:
                                    SetActiveOptionsPage(ActiveOptionsPage.Video);
                                    break;

                                case MenuControlName.btnOptionsAudio:
                                    if (isAnyVideoSettingsControlChanged)
                                    {
                                        neededPageToGoFromVideoOptions = VideoOptionsNeededPageToGo.Audio;
                                        SetStep(MenuStep.options_Video_ApplyDialogue_init);
                                    }
                                    else
                                    {
                                        SetActiveOptionsPage(ActiveOptionsPage.Audio);
                                    }
                                    break;

                                case MenuControlName.btnBack:
                                    if (isAnyVideoSettingsControlChanged)
                                    {
                                        neededPageToGoFromVideoOptions = VideoOptionsNeededPageToGo.ExitOptions;
                                        SetStep(MenuStep.options_Video_ApplyDialogue_init);
                                    }
                                    else
                                    {
                                        SetStep(MenuStep.options_back);
                                        goto StartSteps;
                                    }
                                    break;
                            }

                        }
                        #endregion

                        #region options_controls
                        if (menuEventGroupName == MenuGroupName.options_Controls)
                        {
                            SetMenuEventISChecked();

                            switch (menuEventControlName)
                            {
                                case MenuControlName.btnOptionsDefaults:
                                    SetStep(MenuStep.options_controls_DefaultsDialogue_init);
                                    goto EndSteps;
                                    break;

                                case MenuControlName.btnOptionsControls_Key_Action_Pr:

                                    Options_Controls_SetPlayerRequstedKeyToChange(MenuControlName.btnOptionsControls_Key_Action_Pr, CustomInputManager.keys.Action, true);
                                    SetStep(MenuStep.options_controls_waitingForPlayerKey_init);
                                    goto EndSteps;

                                    break;

                                case MenuControlName.btnOptionsControls_Key_Action_Sec:

                                    Options_Controls_SetPlayerRequstedKeyToChange(MenuControlName.btnOptionsControls_Key_Action_Sec, CustomInputManager.keys.Action, false);
                                    SetStep(MenuStep.options_controls_waitingForPlayerKey_init);
                                    goto EndSteps;

                                    break;

                                case MenuControlName.btnOptionsControls_Key_Aim_Pr:

                                    Options_Controls_SetPlayerRequstedKeyToChange(MenuControlName.btnOptionsControls_Key_Aim_Pr, CustomInputManager.keys.Aim, true);
                                    SetStep(MenuStep.options_controls_waitingForPlayerKey_init);
                                    goto EndSteps;

                                    break;

                                case MenuControlName.btnOptionsControls_Key_Aim_Sec:

                                    Options_Controls_SetPlayerRequstedKeyToChange(MenuControlName.btnOptionsControls_Key_Aim_Sec, CustomInputManager.keys.Aim, false);
                                    SetStep(MenuStep.options_controls_waitingForPlayerKey_init);
                                    goto EndSteps;

                                    break;

                                case MenuControlName.btnOptionsControls_Key_ChangeGun_Pr:

                                    Options_Controls_SetPlayerRequstedKeyToChange(MenuControlName.btnOptionsControls_Key_ChangeGun_Pr, CustomInputManager.keys.ChangeGun, true);
                                    SetStep(MenuStep.options_controls_waitingForPlayerKey_init);
                                    goto EndSteps;

                                    break;

                                case MenuControlName.btnOptionsControls_Key_ChangeGun_Sec:

                                    Options_Controls_SetPlayerRequstedKeyToChange(MenuControlName.btnOptionsControls_Key_ChangeGun_Sec, CustomInputManager.keys.ChangeGun, false);
                                    SetStep(MenuStep.options_controls_waitingForPlayerKey_init);
                                    goto EndSteps;

                                    break;

                                case MenuControlName.btnOptionsControls_Key_Crouch_Pr:

                                    Options_Controls_SetPlayerRequstedKeyToChange(MenuControlName.btnOptionsControls_Key_Crouch_Pr, CustomInputManager.keys.Crouch, true);
                                    SetStep(MenuStep.options_controls_waitingForPlayerKey_init);
                                    goto EndSteps;

                                    break;

                                case MenuControlName.btnOptionsControls_Key_Crouch_Sec:

                                    Options_Controls_SetPlayerRequstedKeyToChange(MenuControlName.btnOptionsControls_Key_Crouch_Sec, CustomInputManager.keys.Crouch, false);
                                    SetStep(MenuStep.options_controls_waitingForPlayerKey_init);
                                    goto EndSteps;

                                    break;

                                case MenuControlName.btnOptionsControls_Key_Fire_Pr:

                                    Options_Controls_SetPlayerRequstedKeyToChange(MenuControlName.btnOptionsControls_Key_Fire_Pr, CustomInputManager.keys.Fire, true);
                                    SetStep(MenuStep.options_controls_waitingForPlayerKey_init);
                                    goto EndSteps;

                                    break;

                                case MenuControlName.btnOptionsControls_Key_Fire_Sec:

                                    Options_Controls_SetPlayerRequstedKeyToChange(MenuControlName.btnOptionsControls_Key_Fire_Sec, CustomInputManager.keys.Fire, false);
                                    SetStep(MenuStep.options_controls_waitingForPlayerKey_init);
                                    goto EndSteps;

                                    break;

                                case MenuControlName.btnOptionsControls_Key_Grenade_SnipeTimeControl_Pr:

                                    Options_Controls_SetPlayerRequstedKeyToChange(MenuControlName.btnOptionsControls_Key_Grenade_SnipeTimeControl_Pr, CustomInputManager.keys.Grenade_SnipeTimeController, true);
                                    SetStep(MenuStep.options_controls_waitingForPlayerKey_init);
                                    goto EndSteps;

                                    break;

                                case MenuControlName.btnOptionsControls_Key_Grenade_SnipeTimeControl_Sec:

                                    Options_Controls_SetPlayerRequstedKeyToChange(MenuControlName.btnOptionsControls_Key_Grenade_SnipeTimeControl_Sec, CustomInputManager.keys.Grenade_SnipeTimeController, false);
                                    SetStep(MenuStep.options_controls_waitingForPlayerKey_init);
                                    goto EndSteps;

                                    break;

                                case MenuControlName.btnOptionsControls_Key_Jump_Pr:

                                    Options_Controls_SetPlayerRequstedKeyToChange(MenuControlName.btnOptionsControls_Key_Jump_Pr, CustomInputManager.keys.Jump, true);
                                    SetStep(MenuStep.options_controls_waitingForPlayerKey_init);
                                    goto EndSteps;

                                    break;

                                case MenuControlName.btnOptionsControls_Key_Jump_Sec:

                                    Options_Controls_SetPlayerRequstedKeyToChange(MenuControlName.btnOptionsControls_Key_Jump_Sec, CustomInputManager.keys.Jump, false);
                                    SetStep(MenuStep.options_controls_waitingForPlayerKey_init);
                                    goto EndSteps;

                                    break;

                                case MenuControlName.btnOptionsControls_Key_Melee_Pr:

                                    Options_Controls_SetPlayerRequstedKeyToChange(MenuControlName.btnOptionsControls_Key_Melee_Pr, CustomInputManager.keys.Melee, true);
                                    SetStep(MenuStep.options_controls_waitingForPlayerKey_init);
                                    goto EndSteps;

                                    break;

                                case MenuControlName.btnOptionsControls_Key_Melee_Sec:

                                    Options_Controls_SetPlayerRequstedKeyToChange(MenuControlName.btnOptionsControls_Key_Melee_Sec, CustomInputManager.keys.Melee, false);
                                    SetStep(MenuStep.options_controls_waitingForPlayerKey_init);
                                    goto EndSteps;

                                    break;

                                case MenuControlName.btnOptionsControls_Key_Missions_Pr:

                                    Options_Controls_SetPlayerRequstedKeyToChange(MenuControlName.btnOptionsControls_Key_Missions_Pr, CustomInputManager.keys.Missions, true);
                                    SetStep(MenuStep.options_controls_waitingForPlayerKey_init);
                                    goto EndSteps;

                                    break;

                                case MenuControlName.btnOptionsControls_Key_Missions_Sec:

                                    Options_Controls_SetPlayerRequstedKeyToChange(MenuControlName.btnOptionsControls_Key_Missions_Sec, CustomInputManager.keys.Missions, false);
                                    SetStep(MenuStep.options_controls_waitingForPlayerKey_init);
                                    goto EndSteps;

                                    break;

                                case MenuControlName.btnOptionsControls_Key_MoveBackward_Pr:

                                    Options_Controls_SetPlayerRequstedKeyToChange(MenuControlName.btnOptionsControls_Key_MoveBackward_Pr, CustomInputManager.keys.MoveBackward, true);
                                    SetStep(MenuStep.options_controls_waitingForPlayerKey_init);
                                    goto EndSteps;

                                    break;

                                case MenuControlName.btnOptionsControls_Key_MoveBackward_Sec:

                                    Options_Controls_SetPlayerRequstedKeyToChange(MenuControlName.btnOptionsControls_Key_MoveBackward_Sec, CustomInputManager.keys.MoveBackward, false);
                                    SetStep(MenuStep.options_controls_waitingForPlayerKey_init);
                                    goto EndSteps;

                                    break;

                                case MenuControlName.btnOptionsControls_Key_MoveForward_Pr:

                                    Options_Controls_SetPlayerRequstedKeyToChange(MenuControlName.btnOptionsControls_Key_MoveForward_Pr, CustomInputManager.keys.MoveForward, true);
                                    SetStep(MenuStep.options_controls_waitingForPlayerKey_init);
                                    goto EndSteps;

                                    break;

                                case MenuControlName.btnOptionsControls_Key_MoveForward_Sec:

                                    Options_Controls_SetPlayerRequstedKeyToChange(MenuControlName.btnOptionsControls_Key_MoveForward_Sec, CustomInputManager.keys.MoveForward, false);
                                    SetStep(MenuStep.options_controls_waitingForPlayerKey_init);
                                    goto EndSteps;

                                    break;

                                case MenuControlName.btnOptionsControls_Key_MoveLeft_Pr:

                                    Options_Controls_SetPlayerRequstedKeyToChange(MenuControlName.btnOptionsControls_Key_MoveLeft_Pr, CustomInputManager.keys.MoveLeft, true);
                                    SetStep(MenuStep.options_controls_waitingForPlayerKey_init);
                                    goto EndSteps;

                                    break;

                                case MenuControlName.btnOptionsControls_Key_MoveLeft_Sec:

                                    Options_Controls_SetPlayerRequstedKeyToChange(MenuControlName.btnOptionsControls_Key_MoveLeft_Sec, CustomInputManager.keys.MoveLeft, false);
                                    SetStep(MenuStep.options_controls_waitingForPlayerKey_init);
                                    goto EndSteps;

                                    break;

                                case MenuControlName.btnOptionsControls_Key_MoveRight_Pr:

                                    Options_Controls_SetPlayerRequstedKeyToChange(MenuControlName.btnOptionsControls_Key_MoveRight_Pr, CustomInputManager.keys.MoveRight, true);
                                    SetStep(MenuStep.options_controls_waitingForPlayerKey_init);
                                    goto EndSteps;

                                    break;

                                case MenuControlName.btnOptionsControls_Key_MoveRight_Sec:

                                    Options_Controls_SetPlayerRequstedKeyToChange(MenuControlName.btnOptionsControls_Key_MoveRight_Sec, CustomInputManager.keys.MoveRight, false);
                                    SetStep(MenuStep.options_controls_waitingForPlayerKey_init);
                                    goto EndSteps;

                                    break;

                                case MenuControlName.btnOptionsControls_Key_Reload_Pr:

                                    Options_Controls_SetPlayerRequstedKeyToChange(MenuControlName.btnOptionsControls_Key_Reload_Pr, CustomInputManager.keys.Reload, true);
                                    SetStep(MenuStep.options_controls_waitingForPlayerKey_init);
                                    goto EndSteps;

                                    break;

                                case MenuControlName.btnOptionsControls_Key_Reload_Sec:

                                    Options_Controls_SetPlayerRequstedKeyToChange(MenuControlName.btnOptionsControls_Key_Reload_Sec, CustomInputManager.keys.Reload, false);
                                    SetStep(MenuStep.options_controls_waitingForPlayerKey_init);
                                    goto EndSteps;

                                    break;

                                case MenuControlName.btnOptionsControls_Key_Sprint_SnipeSteady_Pr:

                                    Options_Controls_SetPlayerRequstedKeyToChange(MenuControlName.btnOptionsControls_Key_Sprint_SnipeSteady_Pr, CustomInputManager.keys.Sprint_SnipeSteady, true);
                                    SetStep(MenuStep.options_controls_waitingForPlayerKey_init);
                                    goto EndSteps;

                                    break;

                                case MenuControlName.btnOptionsControls_Key_Sprint_SnipeSteady_Sec:

                                    Options_Controls_SetPlayerRequstedKeyToChange(MenuControlName.btnOptionsControls_Key_Sprint_SnipeSteady_Sec, CustomInputManager.keys.Sprint_SnipeSteady, false);
                                    SetStep(MenuStep.options_controls_waitingForPlayerKey_init);
                                    goto EndSteps;

                                    break;
                            }
                        }
                        #endregion

                        #region options_audio
                        if (menuEventGroupName == MenuGroupName.options_Audio)
                        {
                            SetMenuEventISChecked();

                            switch (menuEventControlName)
                            {
                                case MenuControlName.btnOptionsDefaults:
                                    SetStep(MenuStep.options_Audio_DefaultsDialogue_init);
                                    goto EndSteps;
                                    break;
                            }
                        }
                        #endregion

                        #region options_video
                        if (menuEventGroupName == MenuGroupName.options_Video)
                        {
                            SetMenuEventISChecked();

                            switch (menuEventControlName)
                            {
                                case MenuControlName.btnApply:
                                    ApplyVideoSettings();
                                    break;

                                case MenuControlName.btnOptionsDefaults:
                                    SetStep(MenuStep.options_Video_DefaultsDialogue_init);
                                    goto EndSteps;
                                    break;
                            }
                        }
                        #endregion
                    }
                }
                #endregion

                #region options_back
                if (IsStep(MenuStep.options_back))
                {
                    RemoveOptionsMenu();

                    GameSaveLoadController.SaveOptions();

                    SetStep(prevStep);
                    goto StartSteps;
                }
                #endregion


                #region options_controls_waitingForPlayerKey_init
                if (IsStep(MenuStep.options_controls_waitingForPlayerKey_init))
                {
                    GetChildGroupByName(MenuGroupName.options).SetEnabled(false);
                    GetChildGroupByName(MenuGroupName.options_Controls).SetEnabled(false);

                    GetChildGroupByName(MenuGroupName.options_Controls_WaitingForKey).SetEnabled(true);
                    GetChildGroupByName(MenuGroupName.options_Controls_WaitingForKey).GetChildControlByName(MenuControlName.boxOptionsControls_EnteredkeyIsIncorrect).SetIsVisible(false);


                    SetStep(MenuStep.options_controls_waitingForPlayerKey_update);
                }
                #endregion

                #region options_controls_waitingForPlayerKey_update
                if (IsStep(MenuStep.options_controls_waitingForPlayerKey_update))
                {
                    if (CustomInputManager.IsAnyKeyDown())
                    {
                        if (CustomInputManager.KeyDown_Escape())
                        {
                            SetStep(MenuStep.options_controls_waitingForPlayerKey_ToOptionsControls);
                            goto StartSteps;
                        }

                        KeyCode downKey = CustomInputManager.GetValidDownKeyCode();

                        if (downKey == KeyCode.None)
                        {
                            GetChildGroupByName(MenuGroupName.options_Controls_WaitingForKey).GetChildControlByName(MenuControlName.boxOptionsControls_EnteredkeyIsIncorrect).SetIsVisible(true);
                        }
                        else
                        {
                            CustomInputManager.AssignKeyToKeyInfo(playerRequestedKeyInfoToChange, downKey, playerRequestedKeyInfo_IsPrimary);

                            Options_Controls_ReInitAllKeysTextures();
                            SetStep(MenuStep.options_controls_waitingForPlayerKey_ToOptionsControls);
                            goto StartSteps;
                        }
                    }
                }
                #endregion

                #region options_controls_waitingForPlayerKey_ToOptionsControls
                if (IsStep(MenuStep.options_controls_waitingForPlayerKey_ToOptionsControls))
                {
                    GetChildGroupByName(MenuGroupName.options).SetEnabled(true);
                    SetActiveOptionsPage(ActiveOptionsPage.Controls);

                    GetChildGroupByName(MenuGroupName.options_Controls_WaitingForKey).SetEnabled(false);

                    SetStep(MenuStep.options_update);
                }
                #endregion


                #region options_controls_DefaultsDialogue_init
                if (IsStep(MenuStep.options_controls_DefaultsDialogue_init))
                {
                    GetChildGroupByName(MenuGroupName.options).SetEnabled(false);
                    GetChildGroupByName(MenuGroupName.options_Controls).SetEnabled(false);

                    GetChildGroupByName(MenuGroupName.options_Controls_DefaultsDialogue).SetEnabled(true);

                    SetStep(MenuStep.options_controls_DefaultsDialogue_update);
                }
                #endregion

                #region options_controls_DefaultsDialogue_update
                if (IsStep(MenuStep.options_controls_DefaultsDialogue_update))
                {
                    if (IsEscapeKeyPressed_IfItIs_DisableItTillUp())
                    {
                        shouldChangeGameControlsToDefault = false;

                        SetStep(MenuStep.options_controls_DefaultsDialogue_back);
                        goto StartSteps;
                    }

                    if (IsEnterKeyPressed_IfItIs_DisableItTillUp())
                    {
                        shouldChangeGameControlsToDefault = true;

                        SetStep(MenuStep.options_controls_DefaultsDialogue_back);
                        goto StartSteps;
                    }

                    if (IsMenuEventHappened())
                    {
                        #region options_controls_DefaultsDialogue
                        if (menuEventGroupName == MenuGroupName.options_Controls_DefaultsDialogue)
                        {
                            SetMenuEventISChecked();

                            switch (menuEventControlName)
                            {
                                case MenuControlName.btnYes:
                                    shouldChangeGameControlsToDefault = true;

                                    SetStep(MenuStep.options_controls_DefaultsDialogue_back);
                                    goto StartSteps;
                                    break;

                                case MenuControlName.btnNo:
                                    shouldChangeGameControlsToDefault = false;

                                    SetStep(MenuStep.options_controls_DefaultsDialogue_back);
                                    goto StartSteps;
                                    break;
                            }
                        }
                        #endregion
                    }
                }
                #endregion

                #region options_controls_DefaultsDialogue_back
                if (IsStep(MenuStep.options_controls_DefaultsDialogue_back))
                {
                    if (shouldChangeGameControlsToDefault)
                    {
                        CustomInputManager.InitDefaultKeys();
                    }

                    GetChildGroupByName(MenuGroupName.options).SetEnabled(true);
                    SetActiveOptionsPage(ActiveOptionsPage.Controls);

                    if (shouldChangeGameControlsToDefault)
                    {
                        shouldChangeGameControlsToDefault = false;

                        Options_Controls_ReInitAllKeysTextures();
                    }

                    GetChildGroupByName(MenuGroupName.options_Controls_DefaultsDialogue).SetEnabled(false);

                    SetStep(MenuStep.options_update);
                }
                #endregion


                #region options_Audio_DefaultsDialogue_init
                if (IsStep(MenuStep.options_Audio_DefaultsDialogue_init))
                {
                    GetChildGroupByName(MenuGroupName.options).SetEnabled(false);
                    GetChildGroupByName(MenuGroupName.options_Audio).SetEnabled(false);

                    GetChildGroupByName(MenuGroupName.options_Audio_DefaultsDialogue).SetEnabled(true);

                    SetStep(MenuStep.options_Audio_DefaultsDialogue_update);
                }
                #endregion

                #region options_Audio_DefaultsDialogue_update
                if (IsStep(MenuStep.options_Audio_DefaultsDialogue_update))
                {
                    if (IsEscapeKeyPressed_IfItIs_DisableItTillUp())
                    {
                        shouldChangeGameAudioToDefault = false;

                        SetStep(MenuStep.options_Audio_DefaultsDialogue_back);
                        goto StartSteps;
                    }

                    if (IsEnterKeyPressed_IfItIs_DisableItTillUp())
                    {
                        shouldChangeGameAudioToDefault = true;

                        SetStep(MenuStep.options_Audio_DefaultsDialogue_back);
                        goto StartSteps;
                    }

                    if (IsMenuEventHappened())
                    {
                        #region options_Audio_DefaultsDialogue
                        if (menuEventGroupName == MenuGroupName.options_Audio_DefaultsDialogue)
                        {
                            SetMenuEventISChecked();

                            switch (menuEventControlName)
                            {
                                case MenuControlName.btnYes:
                                    shouldChangeGameAudioToDefault = true;

                                    SetStep(MenuStep.options_Audio_DefaultsDialogue_back);
                                    goto StartSteps;
                                    break;

                                case MenuControlName.btnNo:
                                    shouldChangeGameAudioToDefault = false;

                                    SetStep(MenuStep.options_Audio_DefaultsDialogue_back);
                                    goto StartSteps;
                                    break;
                            }
                        }
                        #endregion
                    }
                }
                #endregion

                #region options_Audio_DefaultsDialogue_back
                if (IsStep(MenuStep.options_Audio_DefaultsDialogue_back))
                {
                    if (shouldChangeGameAudioToDefault)
                    {
                        AudioController.InitDefaultVolumes();
                    }

                    GetChildGroupByName(MenuGroupName.options).SetEnabled(true);
                    SetActiveOptionsPage(ActiveOptionsPage.Audio);

                    if (shouldChangeGameAudioToDefault)
                    {
                        shouldChangeGameAudioToDefault = false;
                    }

                    GetChildGroupByName(MenuGroupName.options_Audio_DefaultsDialogue).SetEnabled(false);

                    SetStep(MenuStep.options_update);
                }
                #endregion


                #region options_Video_DefaultsDialogue_init
                if (IsStep(MenuStep.options_Video_DefaultsDialogue_init))
                {
                    GetChildGroupByName(MenuGroupName.options).SetEnabled(false);
                    GetChildGroupByName(MenuGroupName.options_Video).SetEnabled(false);

                    GetChildGroupByName(MenuGroupName.options_Video_DefaultsDialogue).SetEnabled(true);

                    SetStep(MenuStep.options_Video_DefaultsDialogue_update);
                }
                #endregion

                #region options_Video_DefaultsDialogue_update
                if (IsStep(MenuStep.options_Video_DefaultsDialogue_update))
                {
                    if (IsEscapeKeyPressed_IfItIs_DisableItTillUp())
                    {
                        shouldChangeGameVideoToDefault = false;

                        SetStep(MenuStep.options_Video_DefaultsDialogue_back);
                        goto StartSteps;
                    }

                    if (IsEnterKeyPressed_IfItIs_DisableItTillUp())
                    {
                        shouldChangeGameVideoToDefault = true;

                        SetStep(MenuStep.options_Video_DefaultsDialogue_back);
                        goto StartSteps;
                    }

                    if (IsMenuEventHappened())
                    {
                        #region options_Video_DefaultsDialogue
                        if (menuEventGroupName == MenuGroupName.options_Video_DefaultsDialogue)
                        {
                            SetMenuEventISChecked();

                            switch (menuEventControlName)
                            {
                                case MenuControlName.btnYes:
                                    shouldChangeGameVideoToDefault = true;

                                    SetStep(MenuStep.options_Video_DefaultsDialogue_back);
                                    goto StartSteps;
                                    break;

                                case MenuControlName.btnNo:
                                    shouldChangeGameVideoToDefault = false;

                                    SetStep(MenuStep.options_Video_DefaultsDialogue_back);
                                    goto StartSteps;
                                    break;
                            }
                        }
                        #endregion
                    }
                }
                #endregion

                #region options_Video_DefaultsDialogue_back
                if (IsStep(MenuStep.options_Video_DefaultsDialogue_back))
                {
                    if (shouldChangeGameVideoToDefault)
                    {
                        VideoSettingsController.InitDefaultVideoSetting(true);
                        isReloadingVideoSettingsControlsNeeded = true;

                        isAnyVideoSettingsControlChanged = false;
                        MenuControl_OptionsVideo_ApplyButton.SetIsActive(false);
                    }

                    GetChildGroupByName(MenuGroupName.options).SetEnabled(true);
                    SetActiveOptionsPage(ActiveOptionsPage.Video);

                    if (shouldChangeGameVideoToDefault)
                    {
                        shouldChangeGameVideoToDefault = false;
                    }

                    GetChildGroupByName(MenuGroupName.options_Video_DefaultsDialogue).SetEnabled(false);

                    SetStep(MenuStep.options_update);
                }
                #endregion


                #region options_Video_ApplyDialogue_init
                if (IsStep(MenuStep.options_Video_ApplyDialogue_init))
                {
                    GetChildGroupByName(MenuGroupName.options).SetEnabled(false);
                    GetChildGroupByName(MenuGroupName.options_Video).SetEnabled(false);

                    GetChildGroupByName(MenuGroupName.options_Video_ApplyDialogue).SetEnabled(true);

                    SetStep(MenuStep.options_Video_ApplyDialogue_update);
                }
                #endregion

                #region options_Video_ApplyDialogue_update
                if (IsStep(MenuStep.options_Video_ApplyDialogue_update))
                {
                    if (IsMenuEventHappened())
                    {
                        #region options_Video_ApplyDialogue
                        if (menuEventGroupName == MenuGroupName.options_Video_ApplyDialogue)
                        {
                            SetMenuEventISChecked();

                            switch (menuEventControlName)
                            {
                                case MenuControlName.btnYes:
                                    videoApplyDialogue_ShouldApplyVideoChanges = true;
                                    videoApplyDialogue_ShouldUnapplyVideoChanges = false;

                                    SetStep(MenuStep.options_Video_ApplyDialogue_back);
                                    goto StartSteps;
                                    break;

                                case MenuControlName.btnNo:
                                    videoApplyDialogue_ShouldApplyVideoChanges = false;
                                    videoApplyDialogue_ShouldUnapplyVideoChanges = true;

                                    SetStep(MenuStep.options_Video_ApplyDialogue_back);
                                    goto StartSteps;
                                    break;

                                case MenuControlName.btnBack:
                                    videoApplyDialogue_ShouldApplyVideoChanges = false;
                                    videoApplyDialogue_ShouldUnapplyVideoChanges = false;

                                    SetStep(MenuStep.options_Video_ApplyDialogue_back);
                                    goto StartSteps;
                                    break;
                            }
                        }
                        #endregion
                    }
                }
                #endregion

                #region options_Video_ApplyDialogue_back
                if (IsStep(MenuStep.options_Video_ApplyDialogue_back))
                {
                    if (videoApplyDialogue_ShouldApplyVideoChanges)
                    {
                        ApplyVideoSettings();
                        MenuControl_OptionsVideo_ApplyButton.SetIsActive(false);
                    }
                    else
                    {
                        if (videoApplyDialogue_ShouldUnapplyVideoChanges)
                        {
                            UnapplyVideoSettingsToOld();
                            MenuControl_OptionsVideo_ApplyButton.SetIsActive(false);
                        }
                        else
                        {
                            neededPageToGoFromVideoOptions = VideoOptionsNeededPageToGo.Video;
                        }
                    }

                    videoApplyDialogue_ShouldApplyVideoChanges = false;
                    videoApplyDialogue_ShouldUnapplyVideoChanges = false;

                    switch (neededPageToGoFromVideoOptions)
                    {
                        case VideoOptionsNeededPageToGo.Audio:
                            GetChildGroupByName(MenuGroupName.options).SetEnabled(true);
                            SetActiveOptionsPage(ActiveOptionsPage.Audio);
                            SetStep(MenuStep.options_update);
                            break;

                        case VideoOptionsNeededPageToGo.Controls:
                            GetChildGroupByName(MenuGroupName.options).SetEnabled(true);
                            SetActiveOptionsPage(ActiveOptionsPage.Controls);
                            SetStep(MenuStep.options_update);
                            break;

                        case VideoOptionsNeededPageToGo.ExitOptions:
                            SetStep(MenuStep.options_back);
                            break;

                        case VideoOptionsNeededPageToGo.Video:
                            GetChildGroupByName(MenuGroupName.options).SetEnabled(true);
                            SetActiveOptionsPage(ActiveOptionsPage.Video);
                            SetStep(MenuStep.options_update);
                            break;
                    }

                    GetChildGroupByName(MenuGroupName.options_Video_ApplyDialogue).SetEnabled(false);
                    goto StartSteps;
                }
                #endregion

            EndSteps:
                ;
            }
        }
    }

    void OnGUI()
    {
        GUI.depth = -1000;

        bool isMouseOverItsRelativeOpenComboBox = false;

        foreach (MenuGroup mg in menuGroups)
        {
            if (mg.isMenuGroupEnabled)
            {
                if (mg.bgTexture != null)
                    GUI.DrawTexture(new Rect(0, 0, Screen.width + 10, Screen.height + 10), mg.bgTexture); //GUI.DrawTexture(new Rect(0, 0, defWidth, defHeight), mg.bgTexture);

                foreach (MenuControl mc in mg.menuControls)
                {
                    if (mc.isControlVisible)
                    {
                        GUI.enabled = mc.isControlActive;

                        Rect rct;

                        switch (mc.type)
                        {
                            #region Button
                            case MenuControlType.Button:
                                rct = new Rect(mc.rect.x, mc.rect.y, mc.rect.w, mc.rect.h);

                                GUI.skin = mc.guiSkin;

                                if (!CheckOpenComboBoxInterlace(rct))
                                {
                                    if (GUI.Button(rct, ""))
                                        MenuEventHappened(mg.groupName, mc.controlName, MenuEventType.click);
                                }
                                else
                                {
                                    GUIStyle boxStyle = new GUIStyle();
                                    CopyGUIStyleContent(boxStyle, mc.guiSkin.button);

                                    boxStyle.normal = mc.guiSkin.button.normal;
                                    boxStyle.active = mc.guiSkin.button.normal;
                                    boxStyle.hover = mc.guiSkin.button.normal;


                                    GUI.Box(rct, "", boxStyle);
                                }

                                break;
                            #endregion

                            #region Slider
                            case MenuControlType.Slider:
                                rct = new Rect(mc.rect.x, mc.rect.y, mc.rect.w, mc.rect.h);

                                mc.guiSkin.horizontalSliderThumb.padding.left = (int)(Screen.height * sliderThumbPaddingCoefToHeight);
                                mc.guiSkin.horizontalSliderThumb.padding.right = mc.guiSkin.horizontalSliderThumb.padding.left;

                                mc.guiSkin.horizontalSlider.overflow.bottom = (int)((Screen.height - sliderOverflowBottom_ZeroHeight) / sliderOverflowBottom_HeightStep);
                                mc.guiSkin.horizontalSlider.overflow.top = -(int)(Screen.height / sliderOverflowTop_HeightStep);

                                GUI.skin = mc.guiSkin;

                                if (!CheckOpenComboBoxInterlace(rct))
                                {
                                    float sliderOldVal = mc.sliderCurValue;

                                    mc.sliderCurValue = GUI.HorizontalSlider(rct, mc.sliderCurValue, mc.sliderMinValue, mc.sliderMaxValue);

                                    if (sliderOldVal != mc.sliderCurValue)
                                        mc.isChanged = true;
                                }
                                else
                                {
                                    GUIStyle boxStyle = new GUIStyle();

                                    CopyGUIStyleContent(boxStyle, mc.guiSkin.horizontalSlider);

                                    boxStyle.normal = mc.guiSkin.horizontalSlider.normal;
                                    boxStyle.active = mc.guiSkin.horizontalSlider.normal;
                                    boxStyle.hover = mc.guiSkin.horizontalSlider.normal;

                                    GUI.Box(rct, "", boxStyle);

                                    boxStyle = new GUIStyle();

                                    CopyGUIStyleContent(boxStyle, mc.guiSkin.horizontalSliderThumb);

                                    boxStyle.normal = mc.guiSkin.horizontalSliderThumb.normal;
                                    boxStyle.active = mc.guiSkin.horizontalSliderThumb.normal;
                                    boxStyle.hover = mc.guiSkin.horizontalSliderThumb.normal;

                                    rct = new Rect(mc.rect.x + (((mc.sliderCurValue - mc.sliderMinValue) / (mc.sliderMaxValue - mc.sliderMinValue)) * (mc.rect.w - (boxStyle.padding.left + boxStyle.padding.right))), mc.rect.y, boxStyle.padding.left + boxStyle.padding.right, mc.rect.h);

                                    GUI.Box(rct, "", boxStyle);
                                }

                                break;
                            #endregion

                            #region PictureBox
                            case MenuControlType.PictureBox:

                                if (!GUI.enabled)
                                    GUI.color = new Color(1, 1, 1, 0.5f);

                                GUI.DrawTexture(new Rect(mc.rect.x, mc.rect.y, mc.rect.w, mc.rect.h), mc.textures[mc.selectedTextureIndex]);

                                GUI.color = new Color(1, 1, 1, 1f);

                                break;
                            #endregion

                            #region TextBox
                            case MenuControlType.TextBox:

                                mc.guiSkin.label.fontSize = (int)(text_FontCoefToHeight * Screen.height);

                                GUI.Label(new Rect(mc.rect.x, mc.rect.y, mc.rect.w, mc.rect.h), mc.text, mc.guiSkin.label);

                                break;
                            #endregion

                            #region Toggle
                            case MenuControlType.Toggle:
                                rct = new Rect(mc.rect.x, mc.rect.y, mc.rect.w, mc.rect.h);

                                if (!CheckOpenComboBoxInterlace(rct))
                                {
                                    bool oldTogVal = mc.toggleValue;

                                    mc.toggleValue = GUI.Toggle(rct, mc.toggleValue, " ", mc.guiSkin.toggle);

                                    if (oldTogVal != mc.toggleValue)
                                        mc.isChanged = true;
                                }
                                else
                                {
                                    GUIStyle boxStyle = new GUIStyle();
                                    CopyGUIStyleContent(boxStyle, mc.guiSkin.toggle);

                                    boxStyle.normal = mc.guiSkin.toggle.normal;
                                    boxStyle.active = mc.guiSkin.toggle.normal;
                                    boxStyle.hover = mc.guiSkin.toggle.normal;

                                    if (mc.toggleValue)
                                    {
                                        boxStyle.normal = mc.guiSkin.toggle.onNormal;
                                        boxStyle.active = mc.guiSkin.toggle.onNormal;
                                        boxStyle.hover = mc.guiSkin.toggle.onNormal;
                                    }

                                    GUI.Box(rct, "", boxStyle);
                                }

                                break;
                            #endregion

                            #region Combo
                            case MenuControlType.ComboBox:

                                if (mc.comboIsInited)
                                {
                                    int comboFirstIndex = 0;
                                    List<ComboBoxItem> comboItems = mc.comboItems;
                                    int comboListCount = comboItems.Count;
                                    int comboSelectedIndex = mc.comboSelectedIndex;
                                    ComboBoxItem selectedComboItem = comboItems[comboSelectedIndex];
                                    float scaledComboItemsH = 0;
                                    float scaledComboRollButtonH = 0;
                                    int numOfButtons = comboListCount;
                                    bool comboBoxNeedsRoll = false;
                                    int numOfAlphaDecUsed = 0;
                                    bool rollShouldShowRollUpButton = true;
                                    bool rollShouldShowRollDownButton = true;

                                    if (mc.comboIsMini)
                                    {
                                        scaledComboRollButtonH = miniComboItemRef.comboRollButton_H * mc.rect.scale;
                                        scaledComboItemsH = miniComboItemRef.comboItemsH * mc.rect.scale;
                                    }
                                    else
                                    {
                                        scaledComboRollButtonH = comboItemRef.comboRollButton_H * mc.rect.scale;
                                        scaledComboItemsH = comboItemRef.comboItemsH * mc.rect.scale;
                                    }

                                    #region ComboNeedsRoll Init
                                    if (!mc.comboNeedsRollIsInited)
                                    {
                                        mc.comboNeedsRollIsInited = true;

                                        float sumOfItemsHeight = scaledComboItemsH * (comboListCount + 1);

                                        if (sumOfItemsHeight > mc.rect.h)
                                        {
                                            mc.comboNeedsRoll = true;
                                        }

                                        mc.comboRollNumOfButtons = (int)((mc.rect.h - scaledComboItemsH) / (scaledComboItemsH));
                                    }
                                    #endregion

                                    comboBoxNeedsRoll = mc.comboNeedsRoll;

                                    if (comboBoxNeedsRoll)
                                    {
                                        numOfButtons = mc.comboRollNumOfButtons;

                                        #region ComboFirstIndex Init (Roll)
                                        if (!mc.comboFirstIndexIsInited)
                                        {
                                            int rollHalfNumOfButtons = 0;
                                            bool rollDecreaseIndexInEndCase = false;

                                            rollHalfNumOfButtons = (int)(numOfButtons / 2);

                                            if (((float)(numOfButtons) / 2f) == (float)(rollHalfNumOfButtons))
                                                rollDecreaseIndexInEndCase = true;

                                            comboFirstIndex = rollHalfNumOfButtons;

                                            if ((comboSelectedIndex + rollHalfNumOfButtons) > (comboListCount - 1))
                                            {
                                                comboFirstIndex += ((comboSelectedIndex + rollHalfNumOfButtons) - (comboListCount - 1));

                                                if (rollDecreaseIndexInEndCase)
                                                    comboFirstIndex--;
                                            }
                                            else
                                            {
                                                if ((comboSelectedIndex - rollHalfNumOfButtons) < 0)
                                                {
                                                    comboFirstIndex += (comboSelectedIndex - rollHalfNumOfButtons);
                                                }
                                            }

                                            comboFirstIndex = comboSelectedIndex - comboFirstIndex;

                                            mc.comboFirstIndex = comboFirstIndex;
                                            mc.comboFirstIndexIsInited = true;
                                        }
                                        #endregion

                                        rollShouldShowRollUpButton = true;
                                        rollShouldShowRollDownButton = true;
                                    }
                                    else
                                    {
                                        #region ComboFirstIndex Init (Not Roll)
                                        if (!mc.comboFirstIndexIsInited)
                                        {
                                            comboFirstIndex = 0;

                                            mc.comboFirstIndex = comboFirstIndex;
                                            mc.comboFirstIndexIsInited = true;
                                        }
                                        #endregion

                                        rollShouldShowRollUpButton = false;
                                        rollShouldShowRollDownButton = false;
                                    }

                                    comboFirstIndex = mc.comboFirstIndex;

                                    rct = new Rect(mc.rect.x, mc.rect.y, mc.rect.w, scaledComboItemsH);

                                    if ((aComboBoxIsOpen && (curOpenComboBox == mc)) || !CheckOpenComboBoxInterlace(rct))
                                    {
                                        #region TopBtn
                                        if (!mc.comboIsOpen)
                                        {
                                            GUI.color = new Color(1, 1, 1, 1f);

                                            selectedComboItem.guiSkin.button.fontSize = (int)(combo_FontCoefToHeight * Screen.height);

                                            if ((GUI.Button(rct, selectedComboItem.text, selectedComboItem.guiSkin.button)))
                                            {
                                                mc.Combo_SetIsOpenOrClose(true);

                                                aComboBoxIsOpen = true;
                                                curOpenComboBox = mc;
                                            }
                                        }
                                        else
                                        {
                                            GUI.color = new Color(1, 1, 1, 1f);

                                            selectedComboItem.guiSkin_OnTop.button.fontSize = (int)(combo_FontCoefToHeight * Screen.height);
                                            if ((GUI.Button(rct, selectedComboItem.text, selectedComboItem.guiSkin_OnTop.button)))
                                            {
                                                mc.Combo_SetIsOpenOrClose(false);

                                                if (aComboBoxIsOpen && (curOpenComboBox == mc))
                                                {
                                                    aComboBoxIsOpen = false;
                                                    curOpenComboBox = null;
                                                }
                                            }
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        GUI.color = new Color(1, 1, 1, 1f);

                                        selectedComboItem.guiSkin.button.fontSize = (int)(combo_FontCoefToHeight * Screen.height);

                                        GUIStyle boxStyle = new GUIStyle();
                                        CopyGUIStyleContent(boxStyle, selectedComboItem.guiSkin.button);

                                        boxStyle.normal = selectedComboItem.guiSkin.button.normal;
                                        boxStyle.active = selectedComboItem.guiSkin.button.normal;
                                        boxStyle.hover = selectedComboItem.guiSkin.button.normal;

                                        GUI.Box(rct, selectedComboItem.text, boxStyle);
                                    }

                                    #region ComboIsOpen
                                    if (mc.comboIsOpen)
                                    {
                                        float itemsBaseY = mc.rect.y + scaledComboItemsH;

                                        if (mc.comboCurUnlimitedAlpha < 1000000)
                                            mc.comboCurUnlimitedAlpha += combo_AlphaSpeed;

                                        if (comboBoxNeedsRoll)
                                        {
                                            itemsBaseY += scaledComboRollButtonH;

                                            if (comboFirstIndex == comboListCount - numOfButtons)
                                            {
                                                rollShouldShowRollDownButton = false;
                                            }

                                            if (comboFirstIndex == 0)
                                            {
                                                rollShouldShowRollUpButton = false;
                                            }

                                            #region RollUpBtn
                                            if (rollShouldShowRollUpButton)
                                            {
                                                GUI.color = new Color(1, 1, 1, Mathf.Clamp01(mc.comboCurUnlimitedAlpha - numOfAlphaDecUsed * combo_AlphaStep));
                                                numOfAlphaDecUsed++;

                                                GUISkin sk = null;

                                                if (mc.comboIsMini)
                                                    sk = miniComboItemRef.comboRollUpButton_Normal_Skin;
                                                else
                                                    sk = comboItemRef.comboRollUpButton_Normal_Skin;

                                                if ((GUI.Button(new Rect(mc.rect.x, mc.rect.y + scaledComboItemsH, mc.rect.w, scaledComboRollButtonH), "", sk.button)))
                                                {
                                                    mc.comboFirstIndex--;
                                                }
                                            }
                                            else
                                            {
                                                GUI.color = new Color(1, 1, 1, Mathf.Clamp01(mc.comboCurUnlimitedAlpha - numOfAlphaDecUsed * combo_AlphaStep));
                                                numOfAlphaDecUsed++;

                                                GUISkin sk = null;

                                                if (mc.comboIsMini)
                                                    sk = miniComboItemRef.comboRollUpButton_Deactive_Skin;
                                                else
                                                    sk = comboItemRef.comboRollUpButton_Deactive_Skin;

                                                if (GUI.Button(new Rect(mc.rect.x, mc.rect.y + scaledComboItemsH, mc.rect.w, scaledComboRollButtonH), "", sk.button))
                                                {

                                                }
                                            }
                                            #endregion
                                        }

                                        #region Btns

                                        int oldComboSelectedIndex = mc.comboSelectedIndex;

                                        for (int i = 0; i < numOfButtons; i++)
                                        {
                                            comboItems[mc.comboFirstIndex + i].guiSkin_Active.button.fontSize = (int)(combo_FontCoefToHeight * Screen.height);

                                            GUI.color = new Color(1, 1, 1, Mathf.Clamp01(mc.comboCurUnlimitedAlpha - numOfAlphaDecUsed * combo_AlphaStep));
                                            numOfAlphaDecUsed++;

                                            if ((GUI.Button(new Rect(mc.rect.x, itemsBaseY + i * scaledComboItemsH, mc.rect.w, scaledComboItemsH), comboItems[mc.comboFirstIndex + i].text, comboItems[mc.comboFirstIndex + i].guiSkin_Active.button)))
                                            {
                                                mc.comboSelectedIndex = mc.comboFirstIndex + i;
                                                mc.Combo_SetIsOpenOrClose(false);

                                                if (aComboBoxIsOpen && (curOpenComboBox == mc))
                                                {
                                                    aComboBoxIsOpen = false;
                                                    curOpenComboBox = null;
                                                }
                                            }

                                            if (mc.comboSelectedIndex == mc.comboFirstIndex + i)
                                            {
                                                Texture2D comboTickTexture = null;

                                                if (mc.comboIsMini)
                                                    comboTickTexture = miniComboItemRef.comboTickTexture;
                                                else
                                                    comboTickTexture = comboItemRef.comboTickTexture;

                                                GUI.DrawTexture(new Rect(mc.rect.x, itemsBaseY + i * scaledComboItemsH, mc.rect.w, scaledComboItemsH), comboTickTexture);
                                            }
                                        }

                                        if (oldComboSelectedIndex != mc.comboSelectedIndex)
                                            mc.isChanged = true;

                                        #endregion

                                        if (comboBoxNeedsRoll)
                                        {
                                            #region RollDownBtn
                                            float rollDownY = mc.rect.y + (numOfButtons + 1) * scaledComboItemsH + scaledComboRollButtonH;

                                            if (rollShouldShowRollDownButton)
                                            {
                                                GUI.color = new Color(1, 1, 1, Mathf.Clamp01(mc.comboCurUnlimitedAlpha - numOfAlphaDecUsed * combo_AlphaStep));
                                                numOfAlphaDecUsed++;

                                                GUISkin sk = null;

                                                if (mc.comboIsMini)
                                                    sk = miniComboItemRef.comboRollDownButton_Normal_Skin;
                                                else
                                                    sk = comboItemRef.comboRollDownButton_Normal_Skin;

                                                if ((GUI.Button(new Rect(mc.rect.x, rollDownY, mc.rect.w, scaledComboRollButtonH), "", sk.button)))
                                                {
                                                    mc.comboFirstIndex++;
                                                }
                                            }
                                            else
                                            {
                                                GUI.color = new Color(1, 1, 1, Mathf.Clamp01(mc.comboCurUnlimitedAlpha - numOfAlphaDecUsed * combo_AlphaStep));
                                                numOfAlphaDecUsed++;

                                                GUISkin sk = null;

                                                if (mc.comboIsMini)
                                                    sk = miniComboItemRef.comboRollDownButton_Deactive_Skin;
                                                else
                                                    sk = comboItemRef.comboRollDownButton_Deactive_Skin;

                                                if (GUI.Button(new Rect(mc.rect.x, rollDownY, mc.rect.w, scaledComboRollButtonH), "", sk.button))
                                                {

                                                }
                                            }
                                            #endregion
                                        }
                                    }
                                    #endregion

                                    GUI.color = new Color(1, 1, 1, 1);

                                    if (aComboBoxIsOpen)
                                    {
                                        if (curOpenComboBox == mc)
                                        {
                                            curOpenComboBoxFinalRect = new Rect();
                                            curOpenComboBoxFinalRect.x = mc.rect.x;
                                            curOpenComboBoxFinalRect.y = mc.rect.y;
                                            curOpenComboBoxFinalRect.width = mc.rect.w;
                                            curOpenComboBoxFinalRect.height = 0;

                                            if (comboBoxNeedsRoll)
                                                curOpenComboBoxFinalRect.height += (2 * scaledComboRollButtonH);

                                            curOpenComboBoxFinalRect.height += (numOfButtons + 1) * scaledComboItemsH;

                                            if (IsMouseOverRect(curOpenComboBoxFinalRect))
                                            {
                                                isMouseOverItsRelativeOpenComboBox = true;
                                            }
                                        }
                                    }
                                }

                                break;
                            #endregion
                        }
                    }
                }
            }
        }

        if (aComboBoxIsOpen)
        {
            if (!isMouseOverItsRelativeOpenComboBox)
            {
                if (CustomInputManager.GetMouse_LeftButton() || CustomInputManager.GetMouse_MidButton() || CustomInputManager.GetMouse_RightButton())
                {
                    aComboBoxIsOpen = false;

                    if (curOpenComboBox != null)
                    {
                        curOpenComboBox.Combo_SetIsOpenOrClose(false);
                        curOpenComboBox = null;
                    }
                }

            }


            if (CustomInputManager.Key_Escape())
            {
                aComboBoxIsOpen = false;

                if (curOpenComboBox != null)
                {
                    curOpenComboBox.Combo_SetIsOpenOrClose(false);
                    curOpenComboBox = null;
                }
            }
        }
    }

    //

    void Init()
    {
        CalcCurHeightAndWidthAndScale();

        foreach (MenuGroup mg in menuGroups)
        {
            mg.Init(scale);
        }
    }

    public void ReInitScale(float _scale)
    {
        scale = _scale;

        foreach (MenuGroup mg in menuGroups)
        {
            mg.ReInitScale(scale);
        }
    }

    void MenuEventHappened(MenuGroupName _groupName, MenuControlName _controlName, MenuEventType _eventType)
    {
        isMenuEventHappened = true;

        menuEventGroupName = _groupName;
        menuEventControlName = _controlName;
        menuEventType = _eventType;
    }

    public MenuGroup GetChildGroupByName(MenuGroupName _groupName)
    {
        MenuGroupName groupName = _groupName;

        for (int i = 0; i < menuGroups.Length; i++)
        {
            if (menuGroups[i].groupName == groupName)
                return menuGroups[i];
        }

        Debug.LogError("Menu group: '" + groupName + "' not founded!");
        return null;
    }

    public MenuControl GetMenuControlInGroup(MenuGroupName _groupName, MenuControlName _controlName)
    {
        MenuGroup mg = GetChildGroupByName(_groupName);
        return mg.GetChildControlByName(_controlName);
    }

    bool IsStep(MenuStep _step)
    {
        return step == _step;
    }

    void SetStep(MenuStep _step)
    {
        step = _step;
    }

    void SetPrevStep(MenuStep _step)
    {
        prevStep = _step;
    }

    void SetNextStep(MenuStep _step)
    {
        nextStep = _step;
    }

    bool IsMenuEventHappened()
    {
        return isMenuEventHappened;
    }

    void SetMenuEventISChecked()
    {
        isMenuEventHappened = false;
    }

    void CalcCurHeightAndWidthAndScale()
    {
        curScreenHight = Screen.height;
        curScreenWidth = Screen.width;

        scale = curScreenHight / defHeight;
    }

    //

    void SetSelectedLevel(int _levelNum)
    {
        selectedLevel = _levelNum;
    }

    void SetSelectedLevelCheckPoint(int _checkPointNum)
    {
        selectedLevelCheckPoint = _checkPointNum;
    }

    void MissionSelect_SetSelectedLevel(int _levelNum)
    {
        SetSelectedLevel(_levelNum);
        SetSelectedLevelCheckPoint(firstCheckPointNum);
    }

    bool IsMouseOverRect(MenuControl _menuCtrl)
    {
        MenuControl menuCtrl = _menuCtrl;

        MenuRect menuRect = menuCtrl.rect;

        float mouseX = CustomInputManager.GetMouseX(); //Input.mousePosition.x;
        float mouseY = Screen.height - CustomInputManager.GetMouseY(); //Input.mousePosition.y;

        if (mouseX >= menuRect.x &&
            mouseX <= menuRect.x + menuRect.w &&
            mouseY >= menuRect.y &&
            mouseY <= menuRect.y + menuRect.h)
            return true;

        return false;
    }

    bool IsMouseOverRect(Rect _rect)
    {
        Rect rect = _rect;

        float mouseX = CustomInputManager.GetMouseX(); //Input.mousePosition.x;
        float mouseY = Screen.height - CustomInputManager.GetMouseY(); //Input.mousePosition.y;

        if (mouseX >= rect.x &&
            mouseX <= rect.x + rect.width &&
            mouseY >= rect.y &&
            mouseY <= rect.y + rect.height)
            return true;

        return false;
    }

    void LoadSelectedLevelLoadingPage()
    {
        GameController.LoadLevelLoadingPage(selectedLevel, selectedLevelCheckPoint);
    }

    void LoadSelectedLevel()
    {
        GameController.LoadLevel(selectedLevel, selectedLevelCheckPoint);
    }

    //

    void RemoveOptionsMenu()
    {
        GetChildGroupByName(MenuGroupName.options).SetEnabled(false);
        GetChildGroupByName(MenuGroupName.options_Video).SetEnabled(false);
        GetChildGroupByName(MenuGroupName.options_Audio).SetEnabled(false);
        GetChildGroupByName(MenuGroupName.options_Controls).SetEnabled(false);
    }

    void RemoveMissionSelectMenu()
    {
        GetChildGroupByName(MenuGroupName.missionSelect_numsBack).SetEnabled(false);

        GetChildGroupByName(MenuGroupName.missionSelect_levelButtons).SetEnabled(false);

        GetChildGroupByName(MenuGroupName.missionSelect_others).SetEnabled(false);
    }

    void DeactiveMissionSelectMenu()
    {
        GetChildGroupByName(MenuGroupName.missionSelect_numsBack).SetEnabledTrueButVisibleChildsBecameDeactive();

        GetChildGroupByName(MenuGroupName.missionSelect_levelButtons).SetEnabledTrueButVisibleChildsBecameDeactive();

        GetChildGroupByName(MenuGroupName.missionSelect_others).SetEnabledTrueButVisibleChildsBecameDeactive();
    }

    void RemoveMainMenu()
    {
        GetChildGroupByName(MenuGroupName.mainMenu_Main).SetEnabled(false);
    }

    void DeactiveMainMenu()
    {
        GetChildGroupByName(MenuGroupName.mainMenu_Main).SetEnabledTrueButVisibleChildsBecameDeactive();
    }

    void RemoveLastCheckpointWillBeLostDialogue()
    {
        GetChildGroupByName(MenuGroupName.lastCheckpointWillBeLostDialogue).SetEnabled(false);
    }

    void RemoveDifficultyDialogue()
    {
        GetChildGroupByName(MenuGroupName.difficulty).SetEnabled(false);
    }

    void RemoveQuitDialogue()
    {
        GetChildGroupByName(MenuGroupName.quitDialogue).SetEnabled(false);
    }

    void RemoveIngameMenu()
    {
        GetChildGroupByName(MenuGroupName.ingameMenu).SetEnabled(false);
    }

    void DeactiveIngameMenu()
    {
        GetChildGroupByName(MenuGroupName.ingameMenu).SetEnabledTrueButVisibleChildsBecameDeactive();
    }

    void RemoveRestartMissionDialogue()
    {
        GetChildGroupByName(MenuGroupName.ingame_restartMissionDialogue).SetEnabled(false);
    }

    void RemoveLastCheckpointDialogue()
    {
        GetChildGroupByName(MenuGroupName.ingame_lastCheckpointDialogue).SetEnabled(false);
    }

    void RemoveSaveAndQuitDialogue()
    {
        GetChildGroupByName(MenuGroupName.ingame_saveAndQuitDialogue).SetEnabled(false);
    }

    //

    bool WillMissionProgressBeRuined()
    {
        return GameController.gameCurrentLevelLastCheckPoint >= minProgressedCheckPointNum;
    }

    //

    public void ActivateIt(bool _isIngame)
    {
        isActive = true;
        isIngame = _isIngame;
        SetStep(MenuStep.start_init);

        Init();
    }

    public void DeactivateIt()
    {
        isActive = false;

        foreach (MenuGroup mg in menuGroups)
        {
            mg.SetEnabled(false);
        }
    }

    bool IsEscapeKeyPressed_IfItIs_DisableItTillUp()
    {
        if (shouldW8ForEscapeKeyUp)
            return false;

        if (CustomInputManager.KeyDown_Escape()) //(GameController.GetButtonDown(GeneralStats.key_Escape))
        {
            shouldW8ForEscapeKeyUp = true;

            return true;
        }

        return false;
    }

    bool IsEnterKeyPressed_IfItIs_DisableItTillUp()
    {
        if (shouldW8ForEnterKeyUp)
            return false;

        if (CustomInputManager.KeyDown_Enter()) //(GameController.GetButtonDown(GeneralStats.key_Enter))
        {
            shouldW8ForEnterKeyUp = true;

            return true;
        }

        return false;
    }

    //

    void SetActiveOptionsPage(ActiveOptionsPage _opPage)
    {
        switch (_opPage)
        {
            case ActiveOptionsPage.Video:
                GetChildGroupByName(MenuGroupName.options_Video).SetEnabled(true);
                GetChildGroupByName(MenuGroupName.options_Audio).SetEnabled(false);
                GetChildGroupByName(MenuGroupName.options_Controls).SetEnabled(false);

                GetChildGroupByName(MenuGroupName.options).GetChildControlByName(MenuControlName.btnOptionsVideo).SetIsActive(false);
                GetChildGroupByName(MenuGroupName.options).GetChildControlByName(MenuControlName.btnOptionsAudio).SetIsActive(true);
                GetChildGroupByName(MenuGroupName.options).GetChildControlByName(MenuControlName.btnOptionsControls).SetIsActive(true);

                GetChildGroupByName(MenuGroupName.options).GetChildControlByName(MenuControlName.boxOptions_Video_Active).SetIsVisible(true);
                GetChildGroupByName(MenuGroupName.options).GetChildControlByName(MenuControlName.boxOptions_Audio_Active).SetIsVisible(false);
                GetChildGroupByName(MenuGroupName.options).GetChildControlByName(MenuControlName.boxOptions_Controls_Active).SetIsVisible(false);

                MenuControl_OptionsVideo_ApplyButton.SetIsActive(false);
                break;

            case ActiveOptionsPage.Controls:
                GetChildGroupByName(MenuGroupName.options_Video).SetEnabled(false);
                GetChildGroupByName(MenuGroupName.options_Audio).SetEnabled(false);
                GetChildGroupByName(MenuGroupName.options_Controls).SetEnabled(true);

                GetChildGroupByName(MenuGroupName.options).GetChildControlByName(MenuControlName.btnOptionsVideo).SetIsActive(true);
                GetChildGroupByName(MenuGroupName.options).GetChildControlByName(MenuControlName.btnOptionsAudio).SetIsActive(true);
                GetChildGroupByName(MenuGroupName.options).GetChildControlByName(MenuControlName.btnOptionsControls).SetIsActive(false);

                GetChildGroupByName(MenuGroupName.options).GetChildControlByName(MenuControlName.boxOptions_Video_Active).SetIsVisible(false);
                GetChildGroupByName(MenuGroupName.options).GetChildControlByName(MenuControlName.boxOptions_Audio_Active).SetIsVisible(false);
                GetChildGroupByName(MenuGroupName.options).GetChildControlByName(MenuControlName.boxOptions_Controls_Active).SetIsVisible(true);

                MenuControl_OptionsControls_MouseSensitivitySlider.sliderCurValue = CustomInputManager.sensitivityX;
                MenuControl_OptionsControls_MouseSensitivityText.text = GetIntTextFromFloat(CustomInputManager.sensitivityX);
                MenuControl_OptionsControls_InvertMouseToggle.toggleValue = CustomInputManager.invertMouse;
                MenuControl_OptionsControls_UseMouseWheelToChangeWeaponToggle.toggleValue = CustomInputManager.useMouseWheelToChangeWeapon;
                break;

            case ActiveOptionsPage.Audio:
                GetChildGroupByName(MenuGroupName.options_Video).SetEnabled(false);
                GetChildGroupByName(MenuGroupName.options_Audio).SetEnabled(true);
                GetChildGroupByName(MenuGroupName.options_Controls).SetEnabled(false);

                GetChildGroupByName(MenuGroupName.options).GetChildControlByName(MenuControlName.btnOptionsVideo).SetIsActive(true);
                GetChildGroupByName(MenuGroupName.options).GetChildControlByName(MenuControlName.btnOptionsAudio).SetIsActive(false);
                GetChildGroupByName(MenuGroupName.options).GetChildControlByName(MenuControlName.btnOptionsControls).SetIsActive(true);

                GetChildGroupByName(MenuGroupName.options).GetChildControlByName(MenuControlName.boxOptions_Video_Active).SetIsVisible(false);
                GetChildGroupByName(MenuGroupName.options).GetChildControlByName(MenuControlName.boxOptions_Audio_Active).SetIsVisible(true);
                GetChildGroupByName(MenuGroupName.options).GetChildControlByName(MenuControlName.boxOptions_Controls_Active).SetIsVisible(false);

                MenuControl_OptionsAudio_MasterVolumeSlider.sliderCurValue = AudioController.GeneralVolume;
                MenuControl_OptionsAudio_MasterVolumeText.text = GetDoRaghamAsharTextFromFloat(AudioController.GeneralVolume);
                MenuControl_OptionsAudio_SFXVolumeSlider.sliderCurValue = AudioController.sfxVolume;
                MenuControl_OptionsAudio_SFXVolumeText.text = GetDoRaghamAsharTextFromFloat(AudioController.sfxVolume);
                MenuControl_OptionsAudio_MusicVolumeSlider.sliderCurValue = AudioController.musicVolume;
                MenuControl_OptionsAudio_MusicVolumeText.text = GetDoRaghamAsharTextFromFloat(AudioController.musicVolume);
                break;
        }
    }

    void Options_Controls_ReInitAllKeysTextures()
    {
        Options_Controls_InitKeyTexture(MenuControlName.btnOptionsControls_Key_Action_Pr, CustomInputManager.keys.Action.primary);
        Options_Controls_InitKeyTexture(MenuControlName.btnOptionsControls_Key_Action_Sec, CustomInputManager.keys.Action.secondary);

        Options_Controls_InitKeyTexture(MenuControlName.btnOptionsControls_Key_Aim_Pr, CustomInputManager.keys.Aim.primary);
        Options_Controls_InitKeyTexture(MenuControlName.btnOptionsControls_Key_Aim_Sec, CustomInputManager.keys.Aim.secondary);

        Options_Controls_InitKeyTexture(MenuControlName.btnOptionsControls_Key_ChangeGun_Pr, CustomInputManager.keys.ChangeGun.primary);
        Options_Controls_InitKeyTexture(MenuControlName.btnOptionsControls_Key_ChangeGun_Sec, CustomInputManager.keys.ChangeGun.secondary);

        Options_Controls_InitKeyTexture(MenuControlName.btnOptionsControls_Key_Crouch_Pr, CustomInputManager.keys.Crouch.primary);
        Options_Controls_InitKeyTexture(MenuControlName.btnOptionsControls_Key_Crouch_Sec, CustomInputManager.keys.Crouch.secondary);

        Options_Controls_InitKeyTexture(MenuControlName.btnOptionsControls_Key_Fire_Pr, CustomInputManager.keys.Fire.primary);
        Options_Controls_InitKeyTexture(MenuControlName.btnOptionsControls_Key_Fire_Sec, CustomInputManager.keys.Fire.secondary);

        Options_Controls_InitKeyTexture(MenuControlName.btnOptionsControls_Key_Grenade_SnipeTimeControl_Pr, CustomInputManager.keys.Grenade_SnipeTimeController.primary);
        Options_Controls_InitKeyTexture(MenuControlName.btnOptionsControls_Key_Grenade_SnipeTimeControl_Sec, CustomInputManager.keys.Grenade_SnipeTimeController.secondary);

        Options_Controls_InitKeyTexture(MenuControlName.btnOptionsControls_Key_Jump_Pr, CustomInputManager.keys.Jump.primary);
        Options_Controls_InitKeyTexture(MenuControlName.btnOptionsControls_Key_Jump_Sec, CustomInputManager.keys.Jump.secondary);

        Options_Controls_InitKeyTexture(MenuControlName.btnOptionsControls_Key_Melee_Pr, CustomInputManager.keys.Melee.primary);
        Options_Controls_InitKeyTexture(MenuControlName.btnOptionsControls_Key_Melee_Sec, CustomInputManager.keys.Melee.secondary);

        Options_Controls_InitKeyTexture(MenuControlName.btnOptionsControls_Key_Missions_Pr, CustomInputManager.keys.Missions.primary);
        Options_Controls_InitKeyTexture(MenuControlName.btnOptionsControls_Key_Missions_Sec, CustomInputManager.keys.Missions.secondary);

        Options_Controls_InitKeyTexture(MenuControlName.btnOptionsControls_Key_MoveBackward_Pr, CustomInputManager.keys.MoveBackward.primary);
        Options_Controls_InitKeyTexture(MenuControlName.btnOptionsControls_Key_MoveBackward_Sec, CustomInputManager.keys.MoveBackward.secondary);

        Options_Controls_InitKeyTexture(MenuControlName.btnOptionsControls_Key_MoveForward_Pr, CustomInputManager.keys.MoveForward.primary);
        Options_Controls_InitKeyTexture(MenuControlName.btnOptionsControls_Key_MoveForward_Sec, CustomInputManager.keys.MoveForward.secondary);

        Options_Controls_InitKeyTexture(MenuControlName.btnOptionsControls_Key_MoveLeft_Pr, CustomInputManager.keys.MoveLeft.primary);
        Options_Controls_InitKeyTexture(MenuControlName.btnOptionsControls_Key_MoveLeft_Sec, CustomInputManager.keys.MoveLeft.secondary);

        Options_Controls_InitKeyTexture(MenuControlName.btnOptionsControls_Key_MoveRight_Pr, CustomInputManager.keys.MoveRight.primary);
        Options_Controls_InitKeyTexture(MenuControlName.btnOptionsControls_Key_MoveRight_Sec, CustomInputManager.keys.MoveRight.secondary);

        Options_Controls_InitKeyTexture(MenuControlName.btnOptionsControls_Key_Reload_Pr, CustomInputManager.keys.Reload.primary);
        Options_Controls_InitKeyTexture(MenuControlName.btnOptionsControls_Key_Reload_Sec, CustomInputManager.keys.Reload.secondary);

        Options_Controls_InitKeyTexture(MenuControlName.btnOptionsControls_Key_Sprint_SnipeSteady_Pr, CustomInputManager.keys.Sprint_SnipeSteady.primary);
        Options_Controls_InitKeyTexture(MenuControlName.btnOptionsControls_Key_Sprint_SnipeSteady_Sec, CustomInputManager.keys.Sprint_SnipeSteady.secondary);
    }

    void Options_Controls_InitKeyTexture(MenuControlName _menuCtrlName, KeyCode _keyCode)
    {
        MenuControlName menuCtrlName = _menuCtrlName;
        KeyCode keyCode = _keyCode;

        MenuControl menuCtrl = MenuGroup_OptionsControls.GetChildControlByName(menuCtrlName);

        KeyPropsInfo keyPropsInf = allKeyProps.GetKeyPropsInfoByKeyCode(keyCode);

        menuCtrl.guiSkin.button.normal.background = keyPropsInf.texture_Normal;
        menuCtrl.guiSkin.button.hover.background = keyPropsInf.texture_Over;
        menuCtrl.guiSkin.button.active.background = keyPropsInf.texture_Active;
    }

    void Options_Controls_SetPlayerRequstedKeyToChange(MenuControlName _menuCtrlName, GameKeyInfo _gameKeyInf, bool _isPrimary)
    {
        //playerEnteredCustomKey = KeyCode.None;

        playerRequestedKey_MenuControlName = _menuCtrlName;

        playerRequestedKeyInfoToChange = _gameKeyInf;

        playerRequestedKeyInfo_IsPrimary = _isPrimary;
    }

    string GetDoRaghamAsharTextFromFloat(float _val)
    {
        float val = _val;

        val *= 100;

        int intVal = (int)(val);

        val = ((float)(intVal)) / 100f;

        string res = val.ToString();

        if (val >= 0)
        {
            if (res.Length == 1)
            {
                res += ".00";
            }

            if (res.Length == 3)
            {
                res += "0";
            }
        }
        else
        {
            if (res.Length == 2)
            {
                res += ".00";
            }

            if (res.Length == 4)
            {
                res += "0";
            }
        }

        return res;
    }

    string GetIntTextFromFloat(float _val)
    {
        float val = _val;

        int intVal = (int)(val);

        string res = intVal.ToString();

        return res;
    }

    bool CheckOpenComboBoxInterlace(Rect _menuCtrlRect)
    {
        Rect menuCtrlRect = _menuCtrlRect;

        if (!aComboBoxIsOpen)
            return false;

        if (!DoRectsInterlace(menuCtrlRect, curOpenComboBoxFinalRect))
            return false;

        return true;
    }

    bool DoRectsInterlace(Rect _rectA, Rect _rectB)
    {
        Rect rectA = _rectA;
        Rect rectB = _rectB;

        if ((rectA.x < rectB.x) && ((rectA.x + rectA.width) < rectB.x))
            return false;

        if ((rectA.x > (rectB.x + rectB.width)) && ((rectA.x + rectA.width) > (rectB.x + rectB.width)))
            return false;

        if ((rectA.y < rectB.y) && ((rectA.y + rectA.height) < rectB.y))
            return false;

        if ((rectA.y > (rectB.y + rectB.height)) && ((rectA.y + rectA.height) > (rectB.y + rectB.height)))
            return false;

        return true;
    }

    void CopyGUIStyleContent(GUIStyle _newStyle, GUIStyle _oldStyle)
    {
        GUIStyle newStyle = _newStyle;
        GUIStyle oldStyle = _oldStyle;

        newStyle.active = oldStyle.active;
        newStyle.alignment = oldStyle.alignment;
        newStyle.border = oldStyle.border;
        newStyle.clipOffset = oldStyle.clipOffset;
        newStyle.clipping = oldStyle.clipping;
        newStyle.contentOffset = oldStyle.contentOffset;
        newStyle.fixedHeight = oldStyle.fixedHeight;
        newStyle.fixedWidth = oldStyle.fixedWidth;
        newStyle.focused = oldStyle.focused;
        newStyle.font = oldStyle.font;
        newStyle.fontSize = oldStyle.fontSize;
        newStyle.fontStyle = oldStyle.fontStyle;
        newStyle.hover = oldStyle.hover;
        newStyle.imagePosition = oldStyle.imagePosition;
        newStyle.margin = oldStyle.margin;
        newStyle.name = oldStyle.name;
        newStyle.normal = oldStyle.normal;
        newStyle.onActive = oldStyle.onActive;
        newStyle.onFocused = oldStyle.onFocused;
        newStyle.onHover = oldStyle.onHover;
        newStyle.onNormal = oldStyle.onNormal;
        newStyle.overflow = oldStyle.overflow;
        newStyle.padding = oldStyle.padding;
        newStyle.stretchHeight = oldStyle.stretchHeight;
        newStyle.stretchWidth = oldStyle.stretchWidth;
        newStyle.wordWrap = oldStyle.wordWrap;
    }

    void SetAnyVideoSettingsControlChanged(bool _val)
    {
        isAnyVideoSettingsControlChanged = _val;
    }

    int GetTextureQualityIndexByItsName(TextureQual _textureName)
    {
        TextureQual textureName = _textureName;
        switch (textureName)
        {
            case TextureQual.quarter:
                return 0;

            case TextureQual.half:
                return 1;

            case TextureQual.full:
                return 2;
        }

        return 0;
    }

    TextureQual GetTextureQualityNameByItsIndex(int _index)
    {
        int index = _index;
        switch (index)
        {
            case 0:
                return TextureQual.quarter;

            case 1:
                return TextureQual.half;

            case 2:
                return TextureQual.full;
        }

        return TextureQual.quarter;
    }

    int GetOverallQualityIndexByItsName(VideoSettingTypes _overallQualityName)
    {
        VideoSettingTypes overallQualityName = _overallQualityName;

        switch (overallQualityName)
        {
            case VideoSettingTypes.Low:
                return 0;

            case VideoSettingTypes.Medium:
                return 1;

            case VideoSettingTypes.High:
                return 2;

            case VideoSettingTypes.VeryHigh:
                return 3;

            case VideoSettingTypes.Ultra:
                return 4;

            case VideoSettingTypes.Custom:
                return 5;
        }

        return 0;
    }

    VideoSettingTypes GetOverallQualityNameByItsIndex(int _index)
    {
        int index = _index;
        switch (index)
        {
            case 0:
                return VideoSettingTypes.Low;

            case 1:
                return VideoSettingTypes.Medium;

            case 2:
                return VideoSettingTypes.High;

            case 3:
                return VideoSettingTypes.VeryHigh;

            case 4:
                return VideoSettingTypes.Ultra;

            case 5:
                return VideoSettingTypes.Custom;
        }

        return VideoSettingTypes.Low;
    }

    int GetShadowQualityIndexByItsName(ShadowQual _shadowName)
    {
        ShadowQual shadowName = _shadowName;
        switch (shadowName)
        {
            case ShadowQual.low:
                return 0;

            case ShadowQual.medium:
                return 1;

            case ShadowQual.high:
                return 2;
        }

        return 0;
    }

    ShadowQual GetShadowQualityNameByItsIndex(int _index)
    {
        int index = _index;
        switch (index)
        {
            case 0:
                return ShadowQual.low;

            case 1:
                return ShadowQual.medium;

            case 2:
                return ShadowQual.high;
        }

        return ShadowQual.low;
    }

    void ChangeVideoPresetToCustom()
    {
        vidSettings_Selected_VidSetType = VideoSettingTypes.Custom;
        MenuControl_OptionsVideo_OverallQualityCombo.comboSelectedIndex = GetOverallQualityIndexByItsName(vidSettings_Selected_VidSetType);
    }

    //

    void ChangeControlsAndNeedValuesToPreset(VideoSettingTypes _overallQualityName)
    {
        VideoSettingTypes overallQualityName = _overallQualityName;

        switch (overallQualityName)
        {
            case VideoSettingTypes.Low:
                vidSettings_Selected_SSAO = VideoSettingsController.default_Low_UseSSAO;
                vidSettings_Selected_Bloom = VideoSettingsController.default_Low_UseBloom;
                vidSettings_Selected_Shadows = VideoSettingsController.default_Low_ShadowOn;
                vidSettings_Selected_ShadowQual = VideoSettingsController.default_Low_ShadowQuality;
                vidSettings_Selected_ShadowDistance = VideoSettingsController.default_Low_ShadowDistance;
                vidSettings_Selected_TextureQual = VideoSettingsController.default_Low_TextureQual;
                vidSettings_Selected_VidSetType = VideoSettingsController.default_Low_VideoSettingType;
                vidSettings_Selected_Anisotropic = VideoSettingsController.default_Low_UseAnisotropic;
                break;

            case VideoSettingTypes.Medium:
                vidSettings_Selected_SSAO = VideoSettingsController.default_Medium_UseSSAO;
                vidSettings_Selected_Bloom = VideoSettingsController.default_Medium_UseBloom;
                vidSettings_Selected_Shadows = VideoSettingsController.default_Medium_ShadowOn;
                vidSettings_Selected_ShadowQual = VideoSettingsController.default_Medium_ShadowQuality;
                vidSettings_Selected_ShadowDistance = VideoSettingsController.default_Medium_ShadowDistance;
                vidSettings_Selected_TextureQual = VideoSettingsController.default_Medium_TextureQual;
                vidSettings_Selected_VidSetType = VideoSettingsController.default_Medium_VideoSettingType;
                vidSettings_Selected_Anisotropic = VideoSettingsController.default_Medium_UseAnisotropic;
                break;

            case VideoSettingTypes.High:
                vidSettings_Selected_SSAO = VideoSettingsController.default_High_UseSSAO;
                vidSettings_Selected_Bloom = VideoSettingsController.default_High_UseBloom;
                vidSettings_Selected_Shadows = VideoSettingsController.default_High_ShadowOn;
                vidSettings_Selected_ShadowQual = VideoSettingsController.default_High_ShadowQuality;
                vidSettings_Selected_ShadowDistance = VideoSettingsController.default_High_ShadowDistance;
                vidSettings_Selected_TextureQual = VideoSettingsController.default_High_TextureQual;
                vidSettings_Selected_VidSetType = VideoSettingsController.default_High_VideoSettingType;
                vidSettings_Selected_Anisotropic = VideoSettingsController.default_High_UseAnisotropic;
                break;

            case VideoSettingTypes.VeryHigh:
                vidSettings_Selected_SSAO = VideoSettingsController.default_VeryHigh_UseSSAO;
                vidSettings_Selected_Bloom = VideoSettingsController.default_VeryHigh_UseBloom;
                vidSettings_Selected_Shadows = VideoSettingsController.default_VeryHigh_ShadowOn;
                vidSettings_Selected_ShadowQual = VideoSettingsController.default_VeryHigh_ShadowQuality;
                vidSettings_Selected_ShadowDistance = VideoSettingsController.default_VeryHigh_ShadowDistance;
                vidSettings_Selected_TextureQual = VideoSettingsController.default_VeryHigh_TextureQual;
                vidSettings_Selected_VidSetType = VideoSettingsController.default_VeryHigh_VideoSettingType;
                vidSettings_Selected_Anisotropic = VideoSettingsController.default_VeryHigh_UseAnisotropic;
                break;

            case VideoSettingTypes.Ultra:
                vidSettings_Selected_SSAO = VideoSettingsController.default_Ultra_UseSSAO;
                vidSettings_Selected_Bloom = VideoSettingsController.default_Ultra_UseBloom;
                vidSettings_Selected_Shadows = VideoSettingsController.default_Ultra_ShadowOn;
                vidSettings_Selected_ShadowQual = VideoSettingsController.default_Ultra_ShadowQuality;
                vidSettings_Selected_ShadowDistance = VideoSettingsController.default_Ultra_ShadowDistance;
                vidSettings_Selected_TextureQual = VideoSettingsController.default_Ultra_TextureQual;
                vidSettings_Selected_VidSetType = VideoSettingsController.default_Ultra_VideoSettingType;
                vidSettings_Selected_Anisotropic = VideoSettingsController.default_Ultra_UseAnisotropic;
                break;

            case VideoSettingTypes.Custom:
                vidSettings_Selected_VidSetType = VideoSettingTypes.Custom;
                break;
        }

        MenuControl_OptionsVideo_OverallQualityCombo.comboSelectedIndex = GetOverallQualityIndexByItsName(vidSettings_Selected_VidSetType);

        if (overallQualityName != VideoSettingTypes.Custom)
        {
            MenuControl_OptionsVideo_TexturesQualityCombo.comboSelectedIndex = GetTextureQualityIndexByItsName(vidSettings_Selected_TextureQual);
            MenuControl_OptionsVideo_AnisotropicToggle.toggleValue = vidSettings_Selected_Anisotropic;
            MenuControl_OptionsVideo_SSAOToggle.toggleValue = vidSettings_Selected_SSAO;
            MenuControl_OptionsVideo_BloomToggle.toggleValue = vidSettings_Selected_Bloom;
            MenuControl_OptionsVideo_ShadowOnToggle.toggleValue = vidSettings_Selected_Shadows;
            //MenuControl_OptionsVideo_ShadowDistanceSlider.sliderCurValue = vidSettings_Selected_ShadowDistance;
            //MenuControl_OptionsVideo_ShadowDistanceText.text = GetIntTextFromFloat(vidSettings_Selected_ShadowDistance);
            MenuControl_OptionsVideo_VSyncToggle.toggleValue = vidSettings_Selected_VSync;
            MenuControl_OptionsVideo_ShadowQualityCombo.comboSelectedIndex = GetShadowQualityIndexByItsName(vidSettings_Selected_ShadowQual);
        }
    }

    void ApplyVideoSettings()
    {
        MenuControl_OptionsVideo_ApplyButton.SetIsActive(false);

        SetAnyVideoSettingsControlChanged(false);

        isReloadingVideoSettingsControlsNeeded = true;

        VideoSettingsController.SetResolution(vidSettings_Selected_ResolutionIndex, true);

        VideoSettingsController.SetBightness(vidSettings_Selected_Brightness);

        VideoSettingsController.Unapplied_SetTextureQuality(vidSettings_Selected_TextureQual);

        VideoSettingsController.SetVideoSettingType(vidSettings_Selected_VidSetType);

        VideoSettingsController.SetUseAnisotropic(vidSettings_Selected_Anisotropic);

        VideoSettingsController.SetUseSSAO(vidSettings_Selected_SSAO);

        VideoSettingsController.SetUseBloom(vidSettings_Selected_Bloom);

        VideoSettingsController.Unapplied_SetIsShadowOn(vidSettings_Selected_Shadows);

        //VideoSettingsController.SetShadowDistance(vidSettings_Selected_ShadowDistance);

        VideoSettingsController.Unapplied_SetIsVSyncOn(vidSettings_Selected_VSync);

        VideoSettingsController.Unapplied_SetShadowQuality(vidSettings_Selected_ShadowQual);

        VideoSettingsController.ApplyRelativeAppliableSettings();
    }

    void UnapplyVideoSettingsToOld()
    {
        MenuControl_OptionsVideo_ApplyButton.SetIsActive(false);

        SetAnyVideoSettingsControlChanged(false);

        isReloadingVideoSettingsControlsNeeded = true;

        VideoSettingsController.SetResolution(vidSettings_Selected_ResolutionIndex_Old, true);

        VideoSettingsController.SetBightness(vidSettings_Selected_Brightness_Old);

        VideoSettingsController.Unapplied_SetTextureQuality(vidSettings_Selected_TextureQual_Old);

        VideoSettingsController.SetVideoSettingType(vidSettings_Selected_VidSetType_Old);

        VideoSettingsController.SetUseAnisotropic(vidSettings_Selected_Anisotropic_Old);

        VideoSettingsController.SetUseSSAO(vidSettings_Selected_SSAO_Old);

        VideoSettingsController.SetUseBloom(vidSettings_Selected_Bloom_Old);

        VideoSettingsController.Unapplied_SetIsShadowOn(vidSettings_Selected_Shadows_Old);

        //VideoSettingsController.SetShadowDistance(vidSettings_Selected_ShadowDistance_Old);

        VideoSettingsController.Unapplied_SetIsVSyncOn(vidSettings_Selected_VSync_Old);

        VideoSettingsController.Unapplied_SetShadowQuality(vidSettings_Selected_ShadowQual_Old);

        VideoSettingsController.ApplyRelativeAppliableSettings();
    }
}