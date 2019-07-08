using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum MenuRectLayoutX
{
    Left,
    Right,
    Center,
}

public enum MenuRectLayoutY
{
    Top,
    Bottom,
    Center,
}

public class MenuRect
{
    public MenuRectLayoutX xLayout = MenuRectLayoutX.Center;
    public float x = 0;

    public MenuRectLayoutY yLayout = MenuRectLayoutY.Center;
    public float y = 0;

    public float w = 0;

    public float h = 0;

    public float scale = 1;

    public void SetLayout_X(MenuRectLayoutX _xLayout)
    {
        xLayout = _xLayout;
    }

    public void SetLayout_Y(MenuRectLayoutY _yLayout)
    {
        yLayout = _yLayout;
    }

    public void SetScale(float _scale)
    {
        scale = _scale;
    }

    public void SetDefaultModeValues(float _x, float _y, float _w, float _h)
    {
        w = _w * scale;
        h = _h * scale;

        float scaledOldX = _x * scale;
        float scaledOldY = _y * scale;

        switch (xLayout)
        {
            case MenuRectLayoutX.Center:
                x = Screen.width / 2 - w / 2 + scaledOldX;
                break;

            case MenuRectLayoutX.Left:
                x = scaledOldX;
                break;

            case MenuRectLayoutX.Right:
                x = Screen.width - w - scaledOldX;
                break;
        }

        switch (yLayout)
        {
            case MenuRectLayoutY.Center:
                y = Screen.height / 2 - h / 2 + scaledOldY;
                break;

            case MenuRectLayoutY.Top:
                y = scaledOldY;
                break;

            case MenuRectLayoutY.Bottom:
                y = Screen.height - h - scaledOldY;
                break;
        }

    }
}

public enum MenuControlType
{
    Button,
    PictureBox,
    Toggle,
    TextBox,
    Slider,
    ComboBox,
}

public enum MenuControlName
{
    _noName,
    btnLevel01,
    btnLevel02,
    btnLevel03,
    btnLevel04,
    btnLevel05,
    btnLevel06,
    btnLevel07,
    btnLevel08,
    btnStart,
    btnBack,
    boxLevel01Selected,
    boxLevel02Selected,
    boxLevel03Selected,
    boxLevel04Selected,
    boxLevel05Selected,
    boxLevel06Selected,
    boxLevel07Selected,
    boxLevel08Selected,
    boxLevelScreenshot,
    btnMedium,
    btnHard,
    boxDifficultyDialogue,
    btnNewGame,
    btnContinue,
    btnMissionSelect,
    btnOptions,
    btnCredits,
    btnQuit,
    btnYes,
    btnNo,
    boxQuitDialogue,
    boxLastCheckpointWillBeLostDialogue,
    btnRestart,
    btnLastCheckPoint,
    boxLastCheckpointBG,
    boxRestartMissionBG,
    boxSaveAndQuitBG,
    btnLevel09,
    btnLevel10,
    boxInGameBG,
    boxOptionsBG,
    btnOptionsVideo,
    btnOptionsAudio,
    btnOptionsControls,
    boxOptionsAudio_Label_MasterVol,
    boxOptionsAudio_Label_SfxVol,
    boxOptionsAudio_Label_MusicVol,
    boxOptionsVideo_Label_Resolution,
    boxOptionsVideo_Label_SSAO,
    boxOptionsControls_Label,
    btnOptionsControls_Key_Action_Pr,
    btnOptionsControls_Key_Action_Sec,
    btnOptionsControls_Key_Crouch_Pr,
    btnOptionsControls_Key_Crouch_Sec,
    btnOptionsControls_Key_Jump_Pr,
    btnOptionsControls_Key_Jump_Sec,
    btnOptionsControls_Key_MoveBackward_Pr,
    btnOptionsControls_Key_MoveBackward_Sec,
    btnOptionsControls_Key_MoveForward_Pr,
    btnOptionsControls_Key_MoveForward_Sec,
    btnOptionsControls_Key_MoveLeft_Pr,
    btnOptionsControls_Key_MoveLeft_Sec,
    btnOptionsControls_Key_MoveRight_Pr,
    btnOptionsControls_Key_MoveRight_Sec,
    btnOptionsControls_Key_Sprint_SnipeSteady_Pr,
    btnOptionsControls_Key_Sprint_SnipeSteady_Sec,
    btnOptionsControls_Key_Aim_Pr,
    btnOptionsControls_Key_Aim_Sec,
    btnOptionsControls_Key_ChangeGun_Pr,
    btnOptionsControls_Key_ChangeGun_Sec,
    btnOptionsControls_Key_Fire_Pr,
    btnOptionsControls_Key_Fire_Sec,
    btnOptionsControls_Key_Grenade_SnipeTimeControl_Pr,
    btnOptionsControls_Key_Grenade_SnipeTimeControl_Sec,
    btnOptionsControls_Key_Melee_Pr,
    btnOptionsControls_Key_Melee_Sec,
    btnOptionsControls_Key_Missions_Pr,
    btnOptionsControls_Key_Missions_Sec,
    btnOptionsControls_Key_Reload_Pr,
    btnOptionsControls_Key_Reload_Sec,
    boxOptionsControls_PressAKey,
    boxOptionsControls_EnteredkeyIsIncorrect,
    btnOptionsDefaults,
    boxOptions_Video_Active,
    boxOptions_Audio_Active,
    boxOptions_Controls_Active,
    boxDialogue,
    sliderOptionsControlsMouseSensitivity,
    txtOptionsControlsMouseSensitivityText,
    togOptionsControlsInvertMouse,
    togOptionsControlsUseMouseWheelToChangeWeapons,
    sliderOptionsAudioMasterVolume,
    txtOptionsAudioMasterVolumeText,
    sliderOptionsAudioSFXVolume,
    txtOptionsAudioSFXVolumeText,
    sliderOptionsAudioMusicVolume,
    txtOptionsAudioMusicVolumeText,
    comboOptionsVideoResolutions,
    btnApply,
    sliderOptionsVideoBrightness,
    txtOptionsVideoBrightness,
    comboOptionsVideoTexturesQuality,
    comboOptionsVideoOverallQuality,
    togOptionsVideoAnisotropic,
    togOptionsVideo_SSAOToggle,
    togOptionsVideo_BloomToggle,
    togOptionsVideo_ShadowOnToggle,
    sliderOptionsVideo_ShadowDistanceSlider,
    txtOptionsVideo_ShadowDistanceText,
    comboOptionsVideo_ShadowQualityCombo,
    togOptionsVideo_VSyncToggle,
    lblOptionsVideo_Shadow,
    lblOptionsVideo_ShadowQuality,
    lblOptionsVideo_ShadowDistance,
    boxBG,
    btnEasy,
    boxLevelsNumsBack,
    txtVersion,
}

public class MenuControl : MonoBehaviour
{
    [HideInInspector]
    public bool isControlActive = false;

    [HideInInspector]
    public bool isControlVisible = false;

    public MenuControlName controlName = MenuControlName._noName;

    public MenuControlType type;

    public MenuRectLayoutX xLayout = MenuRectLayoutX.Center;

    public MenuRectLayoutY yLayout = MenuRectLayoutY.Center;

    public float x = 0;

    public float y = 0;

    public float w = 0;

    public float h = 0;

    public GUISkin guiSkin;

    public string text = "";

    public Texture2D[] textures;

    public int selectedTextureIndex = 0;

    public string __Slider______________________________________________ = "_______________________________";

    public float sliderMaxValue = 1;

    public float sliderMinValue = 0;

    public float sliderCurValue = 0;

    public string __Toggle______________________________________________ = "_______________________________";

    public bool toggleValue = false;

    public string __ComboBox______________________________________________ = "_______________________________";

    public bool comboIsMini = false;

    public bool comboIsInited = false;

    public int comboSelectedIndex = 0;

    public List<ComboBoxItem> comboItems = new List<ComboBoxItem>();

    [HideInInspector]
    public bool comboIsOpen = false;

    [HideInInspector]
    public float comboCurUnlimitedAlpha = 0;

    [HideInInspector]
    public int comboFirstIndex = 0;

    [HideInInspector]
    public bool comboFirstIndexIsInited = false;

    [HideInInspector]
    public bool comboNeedsRoll = false;

    [HideInInspector]
    public bool comboNeedsRollIsInited = false;

    [HideInInspector]
    public int comboRollNumOfButtons = 0;

    // ///////////////////////////////////////////

    [HideInInspector]
    public bool isChanged = false;

    [HideInInspector]
    public MenuRect rect;

    float scale = 1;

    //

    public void ReInitScale(float _scale)
    {
        scale = _scale;
        ReInitMenuRect();
    }

    public void ReInitMenuRect()
    {
        rect = new MenuRect();
        rect.SetScale(scale);
        rect.SetLayout_X(xLayout);
        rect.SetLayout_Y(yLayout);
        rect.SetDefaultModeValues(x, y, w, h);
    }

    public void SetIsActive(bool _value)
    {
        isControlActive = _value;
    }

    public void SetIsVisible(bool _value)
    {
        isControlVisible = _value;
    }

    public void SetSelectedTextureIndex(int _index)
    {
        selectedTextureIndex = _index;
    }

    public void Combo_SetIsOpenOrClose(bool _isOpen)
    {
        bool isOpen = _isOpen;

        comboIsOpen = isOpen;

        if (!comboIsOpen)
            comboCurUnlimitedAlpha = 0;
    }
}
