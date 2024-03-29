using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Menu : MonoBehaviour
{
    private class DoubleStr
    {
        private string firstStr;

        public string FirstStr
        {
            get { return firstStr; }
            set { firstStr = value; }
        }
        private string secondStr;

        public string SecondStr
        {
            get { return secondStr; }
            set { secondStr = value; }
        }
        public bool HavingDoubleStr;



    }
    public static Menu Instance;

    public float DevideScreenButtonSizeX;
    public float AspectButtonSizeY;
    public Vector2 DevideScreenGroup1Pos;
    public Vector2 DevideScreenWarningPos;
    public Vector2 DevideScreenWarningGroupPos;
    public float DevideScreenWarningSizeX;
    public float AspectWarningSizeY;
    public Vector2 DevideScreenGroupOptionsNamePos;
    public Vector2 DevideScreenControlNamePos;
    public Vector2 DevideScreenOptionNamePos;
    public Vector2 DevideScreenShowApplySettingPos;
    public Vector2 DevideScreenShowApplySettingGroupPos;
    public Vector2 DevideScreenMovementGroupPos;
    public Vector2 DevideScreenMovementTextGroupPos;
    public Vector2 DevideScreenActionGroupPos;
    public Vector2 DevideScreenActionTextGroupPos;
    public Vector2 DevideScreenLookGroupPos;
    public Vector2 DevideScreenLookTextGroupPos;
    public Vector2 DevideScreenGroupInGamePos;
    public Vector2 DevideScreenGroupInGameNamePos;
    public float DevideScreenShowApplySettingSizeX;
    public float AspectShowApplySettingSizeY;
    public float DevideScreenShowSecondTexrureSizeX;
    public float AspectShowSecondTexrureSizeY;
    public Vector2 DevideScreenShowSecondTexrurePos;
    public Vector2 DevideScreenGroupOptionPos;
    public Vector2 DevideScreenGroupControlPos;
    public Vector2 DevideScreenGroupVideoPos;
    public Vector2 DevideScreenGroupVideoTextPos;
    public Vector2 DevideScreenGroupBackPos;
    public Vector2 DevideScreenResetPos;
    public float DevideScreenBoxSizeX;
    public float AspectBoxSizeY;
    public float DevideScreenSliderSizeX;
    public float AspectSliderSizeY;
    public Vector2 DevideScreenGroupAdvanceButtonPos;
    public Vector2 DevideScreenGroupAdvanceTextPos;
    public float DevideScreenLableSizeX;
    public float AspectLableSizeY;
    public float devScrButMargin;
    public float devScrButMargRight;
    public float devScrMargBox;
    public float DevScrSliderMargin;
    public float DevScrSliderMarginToButton;
    public float DevScrLableMargin;
    public float MenuBlurSpread;
    public int MenuBlurIteration;
    public bool InGameMenu;


    //public GUISkin GUISkin;
    public Texture2D ApplySettingTexture;
    public Texture2D TransparentTexOption;
    public Texture2D TransparentTexPuaseMenu;
    public Texture2D ExitTex;
    public Texture2D WarningTexure;


    public GUISkin skinGroup1CheckPoint;
    public GUISkin skinGroup1Quit;
    public GUISkin skinGroup1Resume;
    public GUISkin skinGroup1Option;
    public GUISkin skinGroup1Restart;
    public GUISkin skinGroup1Pouse;
    public GUISkin SkinJustEnglish;
    public GUISkin SkinBackBut;
    public GUISkin SkinOptionText;
    public GUISkin SkinVideoButInactive;
    public GUISkin SkinVideoButActive;
    public GUISkin SkinAudioButInactive;
    public GUISkin SkinAudioButActive;
    public GUISkin SkinControlsBut;
    public GUISkin SkinResText;
    public GUISkin SkinBriText;
    public GUISkin skinCostumAdvanceOnNormal;
    public GUISkin skinLowVideo;
    public GUISkin skinMediumVideo;
    public GUISkin skinHighVideo;

    public GUISkin SkinNoBut;
    public GUISkin SkinYesBut;
    public GUISkin SkinSubtitleText;
    public GUISkin SkinSpeakerBut;
    public GUISkin SkinSpeakerText;
    public GUISkin SkinGeneralVulomeText;
    public GUISkin SkinMusicVulomeText;
    public GUISkin SkinVoiceVulomeText;
    public GUISkin SkinSFXVulomeText;
    public GUISkin SkinForwardText;
    public GUISkin SkinBackwardText;
    public GUISkin SkinLeftText;
    public GUISkin SkinRightText;
    public GUISkin SkinStandText;
    public GUISkin SkinCrouchText;
    public GUISkin SkinBrithandHoldBText;
    public GUISkin SkinMovementInactiveBut;
    public GUISkin SkinMovementActiveBut;
    public GUISkin SkinActionInactiveBut;
    public GUISkin SkinActionActiveBut;
    public GUISkin SkinLookInactiveBut;
    public GUISkin SkinLookActiveBut;
    public GUISkin SkinControlsButActive;
    public GUISkin SkinControlsButInactive;
    public GUISkin SkinYesForInvertMouse;
    public GUISkin SkinNoForINvertMouse;
    public GUISkin SkinFireText;
    public GUISkin SkinAimText;
    public GUISkin SkinHoldAimText;
    public GUISkin SkinReloadText;
    public GUISkin SkinSwichWeaponText;
    public GUISkin SkinMeleeText;
    public GUISkin SkinUseText;
    public GUISkin SkinThrowSpatialGrandText;
    public GUISkin SkinApplyControlBut;
    public GUISkin SkinBackApplyControlBut;
    public GUISkin SkinShowObjectiveText;
    public GUISkin SkinMouseSensivityText;
    public GUISkin SkinMouseInvertText;
    public GUISkin skinRecetContolBut;
    public GUISkin skinNewGame;
    public GUISkin skinContinue;


    public string[] SpeakerConfigStr;

    public string[] DefaultMovementStr;
    public string[] DefaultMovementSecondaryStr;
    public string[] DefaultActionStr;
    public string[] DefaultActionSecondaryStr;

    public string[] ShadowStr;
    public string[] TextureQualityStr;
    public string[] AnisotropicTextureStr;
    public string[] AntiAlasingStr;
    public string[] VSyncStr;




    public float MouseSensivityDefault;

    private List<int> ResX = new List<int>();
    private List<int> ResY = new List<int>();
    private List<string> ResolutionStr = new List<string>();
    private List<int> movementCount = new List<int>();
    private string KeyboardStr;
    private string[] SavedActionStr;
    private List<string> movementStr = new List<string>();
    private List<DoubleStr> movementDoubleStr = new List<DoubleStr>();
    private List<DoubleStr> actionDoubleStr = new List<DoubleStr>();
    private List<string> oldActionStr = new List<string>();
    private List<string> oldMovementStr = new List<string>();
    private List<string> actionStr = new List<string>();
    private Vector2 group1Pos;
    private Vector2 groupInGamePos;
    private Vector2 resetPos;
    private Vector2 groupOptionPos;
    private Vector2 groupBackPos;
    private Vector2 groupVideoPos;
    private Vector2 groupMovementPos;
    private Vector2 groupActionPos;
    private Vector2 groupLookPos;
    private Vector2 groupAudioPos;
    private Vector2 groupControlPos;
    private Vector2 advanceButtonPos;
    private Vector2 groupAdvanceVPos;
    private Vector2 groupYesNOPos;
    private Vector2 groupWarningPos;
    private Vector2 buttonSize;
    private Vector2 boxSize;
    private Vector2 showWarningPos;
    private Vector2 showWarningSize;
    private Vector2 sliderSize;
    private Vector2 groupInGameNamePos;
    private Vector2 groupLookText;
    private Vector2 TransParTexPos;
    private Vector2 TransParTexSize;
    private Vector2 groupActionText;
    private Vector2 showExitPos;
    private Vector2 showExitSize;
    private Vector2 groupMovementText;
    private Vector2 controlName;
    private Vector2 groupPos;
    private Vector2 groupVideoTextPos;
    private Vector2 GOptionsNamePos;
    private Vector2 advanceTextPos;
    private Vector2 optionName;
    private Vector2 showApplySettingPos;
    private Vector2 showApplySettingSize;
    private float time;
    private float margin;
    private float marginSlider;
    private float marginSliderToButton;
    private float interval;
    private float marginBox;
    private float marginRight;
    private float lableMargin;
    private float lableHeight;
    private float lableWide;
    private float horizentalButtonInterval;
    private int resolutionTempIndex = 0;
    private int shadowTempIndex;
    private int textureQualityTempIndex;
    private int anisotropicTextureTempIndex;
    private int antiAliasingTempIndex;
    private int vSyncTempIndex;
    private int bloomTempIndex;
    private int dOFTempIndex;
    private int motionBlurTempIndex;
    private int sSAOTempIndex;

    private int shadowIndex = 0;
    private int textureQualityIndex = 0;
    private int anisotropicTextureIndex = 0;
    private int antiAliasingIndex = 0;
    private int vSyncIndex = 1;
    private int bloomIndex = 1;
    private int dOFIndex = 0;
    private int motionBlurIndex = 0;
    private int sSAOIndex = 0;

    private int videoSettingTempIndex = 0;
    private int videoSettingIndex = 0;
    private int subTitleIndex = 1;
    private int subTitleOldIndex;

    private int speakerConfigIndex = 0;
    private int speakerConfigOldIndex = 0;
    private int resolutionIndex = 0;
    private int onHoverCount = 0;
    private int buttonNumber = 1;
    private int buttonNumberOnset;
    private int invertMouseCount = 1;
    private int invertMouseOldCount = 1;
    private float brightnessSlider = 0f;
    private float brightnessSetslide = 0f;
    private float brightnessOldSlider = 0f;
    private float mouseSensivity = 0f;

    private float mouseOldSensivity = .6f;
    private float generalVolumeSetslide;
    private float generalVolumeSlider = 0.5f;
    private float generalVolumeOldSlider;
    private float voiceVolumeSetslide;
    private float voiceVolumeSlider = 0.5f;
    private float voiceVolumeOldSlider;
    private float sFXVolumeSetslide;
    private float sFXVolumeSlider = 0.5f;
    private float sFXVolumeOldSlider;
    private float musicVolumeSetslide;
    private float musicVolumeSlider = 0.5f;
    private float musicVolumeOldSlider;
    private bool showMainBackGroundPauseMenu = false;
    private bool buttonOnHover = false;
    private bool buttonGroup1 = true;
    private bool buttonGroupInGame = false;
    private bool ButtonNewGame = false;
    private bool ButtonCountinue = false;
    private bool buttonOption = false;
    private bool buttonVideo = false;
    private bool buttonAudio = false;
    private bool costumAdvanceVideo = false;
    private bool buttonMovement = false;
    private bool buttonAction = false;
    private bool buttonLook = false;
    private bool halfButsize = false;
    private bool checkOnVideo = false;
    private bool checkOnAudio = false;
    private bool checkOnMovement = false;
    private bool checkOnLook = false;
    private bool changeInControls = false;
    private bool buttonControl = false;
    private bool ButtonExit = false;
    private bool ShowTransparentTex = false;
    private bool showApplySettingForControls = false;
    //private bool showApplySettingForAdvanceV = false;
    private bool showBack = false;
    private bool hoverOnBack = false;
    private bool hoverOnShowWarning = false;
    private bool hoverOnShowApplySettingForControls = false;
    private bool onActionKeySet = false;
    private bool hoverOnResOrBri = false;
    private bool hoverOnOption = false;
    private bool hoverOnApplysetting = false;
    private bool hoverOnLook = false;
    private bool hoverOnGroupAdvaceV = false;
    private bool hoverOnReset = false;
    private bool hoverOnControls = false;
    private bool hoverOnMovement = false;
    private bool hoverOnAction = false;
    private bool hoverOnAdvanceVideo = false;
    private bool hoverOnAudio = false;
    private bool hoverOnInGame = false;
    private bool hoverOnExit = false;
    private bool InGameOtherButton = false;
    private bool showApplySetting = false;
    private bool showWarning = false;
    private bool changeForAdvanceV = false;
    private float sliderHeight;
    private float sliderWide;
    private float oldBlurSpread;
    private int oldBlurIteration;
    private float oldBlurSpreadFPS;
    private int oldBlurIterationFPS;
    private Resolution monitorPreRES;
    private Event keyboardevent;
    private bool alamatSoalExistOnM;
    private bool alamatSoalExistOnA;
    private bool pause = false;
    private bool escapeInsteadOfBack = false;
    private bool isEscapeKeyUp = false;
    private bool usedEscapeForBack = false;
    private bool applyBoolian = false;
    private bool escapeInsteadOfYesApply = false;
   
    void Awake()
    {
        Instance = this;
        SetInitialParametr();
        Resolution[] resolutions = Screen.resolutions;
        monitorPreRES = Screen.currentResolution;
        foreach (Resolution res in resolutions)
        {
            ResolutionStr.Add(res.width + " x " + res.height);
            ResX.Add(res.width);
            ResY.Add(res.height);
            if (Screen.currentResolution.width == res.width && Screen.currentResolution.height == res.height)
            {
                resolutionIndex = ResolutionStr.Count - 1;
            }


        }
        SetAudioSliderToVolumeControler(generalVolumeSlider);
        SetAudioSliderToVolumeControler(musicVolumeSlider);
        SetAudioSliderToVolumeControler(voiceVolumeSlider);
        SetAudioSliderToVolumeControler(sFXVolumeSlider);

        resolutionTempIndex = resolutionIndex;
        
        //ApplyControlsButtonSet(primeryMovementStr, primeryActionStr, seconderyMovementStr, seconderyActionStr, movementCount, ActionCount);

    }
    void Start()
    {
        if (InGameMenu)
        {
            Brightness.Instance.SetBrighness(brightnessSlider);
        }
        if (ControlActionPrimier.AnyChangeControlSave != true)
        {
            for (int i = 0; i < DefaultActionStr.Length; i++)
            {
                DoubleStr CurrentdoubleStr = new DoubleStr();

                if (DefaultActionSecondaryStr[i] != "")
                {
                    CurrentdoubleStr.FirstStr = DefaultActionStr[i];
                    CurrentdoubleStr.SecondStr = DefaultActionSecondaryStr[i];
                    CurrentdoubleStr.HavingDoubleStr = true;
                    actionDoubleStr.Add(CurrentdoubleStr);
                }
                else
                {
                    CurrentdoubleStr.FirstStr = DefaultActionStr[i];
                    CurrentdoubleStr.SecondStr = "";
                    CurrentdoubleStr.HavingDoubleStr = false;
                    actionDoubleStr.Add(CurrentdoubleStr);
                }
                actionStr.Add(MakingFullStrToShow(CurrentdoubleStr));
                oldActionStr.Add(MakingFullStrToShow(CurrentdoubleStr));

            }


            for (int i = 0; i < DefaultMovementStr.Length; i++)
            {
                DoubleStr CurrentdoubleStr = new DoubleStr();

                if (DefaultMovementSecondaryStr[i] != "")
                {


                    CurrentdoubleStr.FirstStr = DefaultMovementStr[i];
                    CurrentdoubleStr.SecondStr = DefaultMovementSecondaryStr[i];
                    CurrentdoubleStr.HavingDoubleStr = true;
                    movementDoubleStr.Add(CurrentdoubleStr);


                }
                else
                {


                    CurrentdoubleStr.FirstStr = DefaultMovementStr[i];
                    CurrentdoubleStr.SecondStr = "";
                    CurrentdoubleStr.HavingDoubleStr = false;
                    movementDoubleStr.Add(CurrentdoubleStr);


                }
                movementStr.Add(MakingFullStrToShow(CurrentdoubleStr));
                oldMovementStr.Add(MakingFullStrToShow(CurrentdoubleStr));
            }
        }
        else
        {
            DontSetControlBut(movementDoubleStr, actionDoubleStr, movementStr, actionStr);
        }
        if (AdvanceVideoParameter.AnyChangeOnAdvanceVSave == false)
        {
            SetApplyAdvanceVForSave();
        }
        else
        {
            DontApplyAdvanceV();
        }

        if (otherOptionParameter.AnyChangeOnOtherParameter == false)
        {
            ApplyAdvanceV();
        }
        else
        {
            DontApplyAdvanceV();
        }
        SetSizeAndPos();
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && pause && isEscapeKeyUp)
        {

            if (InGameMenu && buttonGroupInGame && !applyBoolian)
            {
                StopInGameMenu(true);
                showMainBackGroundPauseMenu = false;
                buttonGroupInGame = false;
            }
            else if (!usedEscapeForBack)
            {
                escapeInsteadOfBack = true;
                HandelInput();
            }
            else if (applyBoolian)
            {
                escapeInsteadOfYesApply = true;
                HandelInput();
                usedEscapeForBack = false;
                escapeInsteadOfYesApply = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            isEscapeKeyUp = true;
        }

        if (InGameMenu)
        {
            buttonGroup1 = false;
        }


        if (InGameMenu && pause == true && InGameOtherButton == false)
        {
            buttonGroupInGame = true;
            showMainBackGroundPauseMenu = true;
            buttonGroup1 = false;
        }

        if (brightnessSlider != brightnessSetslide)
        {
            SetBrightness(brightnessSlider);
            showApplySetting = true;
        }
        if (generalVolumeSlider != generalVolumeSetslide)
        {
            generalVolumeSetslide = SetVolumeOnButton(generalVolumeSlider, generalVolumeSetslide);

            showApplySetting = true;
        }
        if (voiceVolumeSlider != voiceVolumeSetslide)
        {
            voiceVolumeSetslide = SetVolumeOnButton(voiceVolumeSlider, voiceVolumeSetslide);

            showApplySetting = true;
        }
        if (musicVolumeSlider != musicVolumeSetslide)
        {
            musicVolumeSetslide = SetVolumeOnButton(musicVolumeSlider, musicVolumeSetslide);
            showApplySetting = true;
        }
        if (sFXVolumeSlider != sFXVolumeSetslide)
        {
            sFXVolumeSetslide = SetVolumeOnButton(sFXVolumeSlider, sFXVolumeSetslide);
            showApplySetting = true;
        }

        buttonOnHover = false;
        if ((buttonGroup1 && ButtonExit == false) || showWarning)
        {
            if (showApplySetting == false)
            {
                CheckMousePos(group1Pos, 4);
                hoverOnApplysetting = false;
            }
            else
            {
                halfButsize = true;
                if (showWarning)
                {
                    CheckMousePos(groupWarningPos, 2);
                }
                else
                {
                    CheckMousePos(groupYesNOPos, 2);
                }
                if (buttonOnHover)
                {
                    hoverOnApplysetting = true;
                }
                else
                {
                    hoverOnApplysetting = false;
                }
                halfButsize = false;
            }
        }

        else if ((buttonGroupInGame && ButtonExit == false) || showWarning)
        {

            if (showApplySetting == false)
            {
                CheckMousePos(groupInGamePos, 5);
                if (buttonOnHover)
                {
                    hoverOnInGame = true;
                }
                else
                    hoverOnInGame = false;
            }
            else
            {
                halfButsize = true;
                if (showWarning)
                {
                    CheckMousePos(groupWarningPos, 2);
                }
                else
                {
                    CheckMousePos(groupYesNOPos, 2);
                }
                if (buttonOnHover)
                {
                    hoverOnApplysetting = true;
                }
                else
                {
                    hoverOnApplysetting = false;
                }
                halfButsize = false;
            }
        }
        else if (buttonOption)
        {

            CheckMousePos(groupOptionPos, 3);
            if (buttonOnHover)
            {
                hoverOnOption = true;
            }
            else
                hoverOnOption = false;




        }
        if (ButtonExit && buttonOnHover == false)
        {
            halfButsize = true;
            CheckMousePos(groupWarningPos, 2);
            if (buttonOnHover)
            {
                hoverOnExit = true;
            }
            else
            {
                hoverOnExit = false;
            }
            halfButsize = false;
        }

        if (showBack && buttonOnHover == false && showWarning == false)
        {

            halfButsize = true;
            CheckMousePos(groupBackPos, 1);
            if (buttonOnHover)
            {
                hoverOnBack = true;
            }
            else
            {
                hoverOnBack = false;
            }
            halfButsize = false;
        }
        checkOnVideo = true;
        if (buttonVideo && buttonOnHover == false)
        {
            CheckMousePos(groupVideoPos, 2);

            if (buttonOnHover)
            {
                hoverOnResOrBri = true;
            }
            else
                hoverOnResOrBri = false;


        }

        checkOnVideo = false;
        if (buttonVideo && buttonOnHover == false)
        {
            CheckMousePos(advanceButtonPos, 1);
            if (buttonOnHover)
            {
                hoverOnAdvanceVideo = true;
            }
            else
                hoverOnAdvanceVideo = false;
        }
        checkOnAudio = true;
        if (buttonAudio && buttonOnHover == false)
        {
            CheckMousePos(groupAudioPos, 6);
            if (buttonOnHover)
            {
                hoverOnAudio = true;
            }
            else
                hoverOnAudio = false;

        }
        checkOnAudio = false;
        halfButsize = true;
        if (buttonControl && buttonOnHover == false)
        {
            CheckMousePos(resetPos, 1);
            if (buttonOnHover)
            {
                hoverOnReset = true;
            }
            else
                hoverOnReset = false;
        }
        halfButsize = false;
        if (buttonControl && buttonOnHover == false)
        {
            CheckHorizentalMousePos(groupControlPos, 3);
            hoverOnAction = false;
            hoverOnMovement = false;
            if (buttonOnHover)
            {
                hoverOnControls = true;
            }
            else
                hoverOnControls = false;


        }

        checkOnMovement = true;
        if (buttonMovement && buttonOnHover == false && showWarning == false && showApplySettingForControls == false)
        {

            CheckMousePos(groupMovementPos, 7);
            if (buttonOnHover)
            {
                hoverOnMovement = true;

            }
            else
                hoverOnMovement = false;

        }
        if (buttonAction && buttonOnHover == false && showWarning == false && showApplySettingForControls == false)
        {
            CheckMousePos(groupActionPos, 11);
            if (buttonOnHover)
            {
                hoverOnAction = true;
            }
            else
                hoverOnAction = false;

        }
        checkOnLook = true;
        if (buttonLook && buttonOnHover == false && showWarning == false && showApplySettingForControls == false)
        {
            CheckMousePos(groupLookPos, 2);
            if (buttonOnHover)
            {
                hoverOnLook = true;
            }
            else
                hoverOnLook = false;

        }
        checkOnLook = false;
        if (costumAdvanceVideo && buttonOnHover == false)
        {
            CheckMousePos(groupAdvanceVPos, 10);
            if (buttonOnHover)
            {
                hoverOnGroupAdvaceV = true;
            }
            else
                hoverOnGroupAdvaceV = false;
        }
        checkOnMovement = false;


        if (buttonOnHover == true)
        {
            OnHoverSound();
            HandelInput();
        }
        else
        {
            onHoverCount = 0;
        }



    }
    void OnGUI()
    {

        //SetSizeAndPos();
        if (showMainBackGroundPauseMenu)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), TransparentTexPuaseMenu);
        }

        if ((buttonMovement || buttonAction) && onActionKeySet == false)
        {

            alamatSoalExistOnM = CheckForAlamatesoal(movementStr);

            alamatSoalExistOnA = CheckForAlamatesoal(actionStr);

            if (Input.anyKey)
            {
                if (hoverOnMovement || hoverOnAction || alamatSoalExistOnA || alamatSoalExistOnM)
                {
                    keyboardevent = Event.current;

                    if (keyboardevent.isKey || keyboardevent.shift || keyboardevent.isMouse)
                    {


                        if (keyboardevent.isKey)
                        {
                            KeyboardStr = (keyboardevent.keyCode.ToString());
                            KeyboardStr = SetAlphaButtonToNumber(KeyboardStr);



                        }
                        if (keyboardevent.shift)
                        {
                            KeyboardStr = "Shift";
                        }
                        if (keyboardevent.isMouse)
                        {

                            if (keyboardevent.button == 0)
                            {
                                KeyboardStr = "Left Mouse";
                            }
                            if (keyboardevent.button == 1)
                            {
                                KeyboardStr = "Right Mouse";
                            }
                            if (keyboardevent.button == 2)
                            {
                                KeyboardStr = "Middel Mouse";
                            }


                        }
                        if (KeyboardStr != "null" && KeyboardStr != "None")
                        {
                            onActionKeySet = true;
                            HandelInput();
                        }

                    }
                }
            }


        }

        if (buttonGroupInGame)
        {
            GUI.skin = skinGroup1Pouse;
            GUI.Box(new Rect(groupInGameNamePos.x, groupInGameNamePos.y, boxSize.x, boxSize.y), "");

            GUI.skin = skinGroup1Resume;
            GUI.Button(new Rect(groupInGamePos.x, groupInGamePos.y, buttonSize.x, buttonSize.y), "");
            GUI.skin = skinGroup1Option;
            GUI.Button(new Rect(groupInGamePos.x, groupInGamePos.y + 1 * interval, buttonSize.x, buttonSize.y), "");
            GUI.skin = skinGroup1CheckPoint;
            GUI.Button(new Rect(groupInGamePos.x, groupInGamePos.y + 2 * interval, buttonSize.x, buttonSize.y), "");
            GUI.skin = skinGroup1Restart;
            GUI.Button(new Rect(groupInGamePos.x, groupInGamePos.y + 3 * interval, buttonSize.x, buttonSize.y), "");

            GUI.skin = skinGroup1Quit;
            GUI.Button(new Rect(groupInGamePos.x, groupInGamePos.y + 4 * interval, buttonSize.x, buttonSize.y), " ");


        }
        if (buttonGroup1)
        {


            GUI.skin = skinNewGame;
            GUI.Button(new Rect(group1Pos.x, group1Pos.y, buttonSize.x, buttonSize.y), "");
            GUI.skin = skinContinue;
            GUI.Button(new Rect(group1Pos.x, group1Pos.y + 1 * interval, buttonSize.x, buttonSize.y), "");
            GUI.skin = skinGroup1Option;
            GUI.Button(new Rect(group1Pos.x, group1Pos.y + 2 * interval, buttonSize.x, buttonSize.y), "");
            GUI.skin = skinGroup1Quit;
            GUI.Button(new Rect(group1Pos.x, group1Pos.y + 3 * interval, buttonSize.x, buttonSize.y), "");



        }

        if (ShowTransparentTex)
        {

            GUI.DrawTexture(new Rect(TransParTexPos.x, TransParTexPos.y, TransParTexSize.x, TransParTexSize.y), TransparentTexOption);


        }
        if (ButtonExit)
        {

            GUI.DrawTexture(new Rect(showExitPos.x, showExitPos.y, showExitSize.x, showExitSize.y), ExitTex);
            GUI.skin = SkinYesBut;
            GUI.Button(new Rect(groupWarningPos.x, groupWarningPos.y, buttonSize.x / 2, buttonSize.y), "");
            GUI.skin = SkinNoBut;
            GUI.Button(new Rect(groupWarningPos.x, groupWarningPos.y + interval, buttonSize.x / 2, buttonSize.y), "");
        }
        if (((showApplySetting) && (buttonGroup1 || buttonGroupInGame)) || showWarning)
        {


            if (showWarning)
            {
                GUI.DrawTexture(new Rect(showWarningPos.x, showWarningPos.y, showWarningSize.x, showWarningSize.y), WarningTexure);
                GUI.skin = SkinApplyControlBut;
                GUI.Button(new Rect(groupWarningPos.x, groupWarningPos.y, buttonSize.x / 2, buttonSize.y), "");
                GUI.skin = SkinBackApplyControlBut;
                GUI.Button(new Rect(groupWarningPos.x, groupWarningPos.y + interval, buttonSize.x / 2, buttonSize.y), "");
            }
            else
            {
                GUI.DrawTexture(new Rect(showApplySettingPos.x, showApplySettingPos.y, showApplySettingSize.x, showApplySettingSize.y), ApplySettingTexture);

                GUI.skin = SkinYesBut;
                GUI.Button(new Rect(groupYesNOPos.x, groupYesNOPos.y, buttonSize.x / 2, buttonSize.y), "");
                GUI.skin = SkinNoBut;
                GUI.Button(new Rect(groupYesNOPos.x, groupYesNOPos.y + interval, buttonSize.x / 2, buttonSize.y), "");
            }
        }

        if (buttonOption)
        {


            GUI.skin = SkinOptionText;
            GUI.Box(new Rect(optionName.x, optionName.y, boxSize.x, boxSize.y), "");
            if (buttonVideo == false)
            {
                GUI.skin = SkinVideoButInactive;
            }
            else
            {
                GUI.skin = SkinVideoButActive;
            }
            GUI.Button(new Rect(groupOptionPos.x, groupOptionPos.y, buttonSize.x, buttonSize.y), " ");
            if (buttonAudio == false)
            {
                GUI.skin = SkinAudioButInactive;
            }
            else
            {
                GUI.skin = SkinAudioButActive;
            }
            GUI.Button(new Rect(groupOptionPos.x, groupOptionPos.y + interval, buttonSize.x, buttonSize.y), "");
            if (buttonControl == false)
            {
                GUI.skin = SkinControlsButInactive;
            }
            else
            {
                GUI.skin = SkinControlsButActive;
            }
            GUI.Button(new Rect(groupOptionPos.x, groupOptionPos.y + 2 * interval, buttonSize.x, buttonSize.y), "");



            showBack = true;



        }
        if (showBack)
        {


            GUI.skin = SkinBackBut;
            GUI.Button(new Rect(groupBackPos.x, groupBackPos.y, buttonSize.x / 2, buttonSize.y), "");

        }
        if (costumAdvanceVideo)
        {


            float intervalBox = marginBox + boxSize.y;

            GUI.skin = SkinJustEnglish;

            GUI.Box(new Rect(advanceTextPos.x, advanceTextPos.y, boxSize.x, boxSize.y), "Anti Aliasing");
            GUI.Box(new Rect(advanceTextPos.x, advanceTextPos.y + intervalBox, boxSize.x, boxSize.y), "Sync Every Frame");
            GUI.Box(new Rect(advanceTextPos.x, advanceTextPos.y + 2 * intervalBox, boxSize.x, boxSize.y), "Shadow");

            GUI.Box(new Rect(advanceTextPos.x, advanceTextPos.y + 3 * intervalBox, boxSize.x, boxSize.y), "Motion Blur");
            GUI.Box(new Rect(advanceTextPos.x, advanceTextPos.y + 4 * intervalBox, boxSize.x, boxSize.y), "Depth Of Field");
            GUI.Box(new Rect(advanceTextPos.x, advanceTextPos.y + 5 * intervalBox, boxSize.x, boxSize.y), "Bloom");
            GUI.Box(new Rect(advanceTextPos.x, advanceTextPos.y + 6 * intervalBox, boxSize.x, boxSize.y), "SSAO");
            GUI.Box(new Rect(advanceTextPos.x, advanceTextPos.y + 7 * intervalBox, boxSize.x, boxSize.y), "Texture Quality");
            GUI.Box(new Rect(advanceTextPos.x, advanceTextPos.y + 8 * intervalBox, boxSize.x, boxSize.y), "Anisotropic Texture");
            GUI.Button(new Rect(groupAdvanceVPos.x, groupAdvanceVPos.y, buttonSize.x, buttonSize.y), AntiAlasingStr[antiAliasingTempIndex]);
            GUI.Button(new Rect(groupAdvanceVPos.x, groupAdvanceVPos.y + interval, buttonSize.x, buttonSize.y), VSyncStr[vSyncTempIndex]);
            GUI.Button(new Rect(groupAdvanceVPos.x, groupAdvanceVPos.y + 2 * interval, buttonSize.x, buttonSize.y), ShadowStr[shadowTempIndex]);

            GUI.Button(new Rect(groupAdvanceVPos.x, groupAdvanceVPos.y + 3 * interval, buttonSize.x, buttonSize.y), VSyncStr[motionBlurTempIndex]);
            GUI.Button(new Rect(groupAdvanceVPos.x, groupAdvanceVPos.y + 4 * interval, buttonSize.x, buttonSize.y), VSyncStr[dOFTempIndex]);
            GUI.Button(new Rect(groupAdvanceVPos.x, groupAdvanceVPos.y + 5 * interval, buttonSize.x, buttonSize.y), VSyncStr[bloomTempIndex]);
            GUI.Button(new Rect(groupAdvanceVPos.x, groupAdvanceVPos.y + 6 * interval, buttonSize.x, buttonSize.y), VSyncStr[sSAOTempIndex]);
            GUI.Button(new Rect(groupAdvanceVPos.x, groupAdvanceVPos.y + 7 * interval, buttonSize.x, buttonSize.y), TextureQualityStr[textureQualityTempIndex]);
            GUI.Button(new Rect(groupAdvanceVPos.x, groupAdvanceVPos.y + 8 * interval, buttonSize.x, buttonSize.y), AnisotropicTextureStr[anisotropicTextureTempIndex]);
        }

        if (buttonAudio || buttonControl || buttonVideo)
        {

            float intervalBox = marginBox + boxSize.y;





            if (buttonVideo)
            {
                groupVideoPos = groupPos;


                GUI.skin = SkinJustEnglish;
                GUI.Button(new Rect(groupVideoPos.x, groupVideoPos.y, buttonSize.x, buttonSize.y), ResolutionStr[resolutionTempIndex]);

                brightnessSlider = GUI.HorizontalSlider(new Rect(groupVideoPos.x, groupVideoPos.y + interval, sliderWide, sliderHeight), brightnessSlider, -.5f, .5f);
                GUI.skin = SkinResText;
                GUI.Box(new Rect(groupVideoTextPos.x, groupVideoTextPos.y, boxSize.x, boxSize.y), "");
                GUI.skin = SkinBriText;
                GUI.Box(new Rect(groupVideoTextPos.x, groupVideoTextPos.y + intervalBox, boxSize.x, boxSize.y), "");
                if (videoSettingTempIndex == 0)
                {
                    GUI.skin = skinLowVideo;
                }
                else if (videoSettingTempIndex == 1)
                {
                    GUI.skin = skinMediumVideo;
                }
                else if (videoSettingTempIndex == 2)
                {
                    GUI.skin = skinHighVideo;
                }
                else if (videoSettingTempIndex == 3)
                {
                    GUI.skin = skinCostumAdvanceOnNormal;
                }

                GUI.Button(new Rect(groupVideoTextPos.x, groupVideoTextPos.y + 2 * intervalBox, buttonSize.x, buttonSize.y), "");

                advanceButtonPos = groupVideoTextPos;
                advanceButtonPos.y = groupVideoTextPos.y + 2 * intervalBox;

            }
            if (buttonAudio)
            {

                groupAudioPos = groupPos;


                float hSliderInterval = marginSlider + sliderHeight;
                generalVolumeSlider = GUI.HorizontalSlider(new Rect(groupAudioPos.x, groupAudioPos.y, sliderWide, sliderHeight), generalVolumeSlider, 0f, 1f);
                musicVolumeSlider = GUI.HorizontalSlider(new Rect(groupAudioPos.x, groupAudioPos.y + hSliderInterval, sliderWide, sliderHeight), musicVolumeSlider, 0f, 1f);
                voiceVolumeSlider = GUI.HorizontalSlider(new Rect(groupAudioPos.x, groupAudioPos.y + 2 * hSliderInterval, sliderWide, sliderHeight), voiceVolumeSlider, 0f, 1f);
                sFXVolumeSlider = GUI.HorizontalSlider(new Rect(groupAudioPos.x, groupAudioPos.y + 3 * hSliderInterval, sliderWide, sliderHeight), sFXVolumeSlider, 0f, 1f);
                GUI.skin = SkinSpeakerBut;
                GUI.Button(new Rect(groupAudioPos.x, groupAudioPos.y + 3 * hSliderInterval + marginSliderToButton, buttonSize.x, buttonSize.y), SpeakerConfigStr[speakerConfigIndex]);
                if (subTitleIndex == 0)
                {
                    GUI.skin = SkinNoForINvertMouse;
                }
                else
                    GUI.skin = SkinYesForInvertMouse;
                GUI.Button(new Rect(groupAudioPos.x, groupAudioPos.y + 3 * hSliderInterval + marginSliderToButton + interval, buttonSize.x, buttonSize.y), "");
                GUI.skin = SkinGeneralVulomeText;
                GUI.Box(new Rect(groupVideoTextPos.x, groupVideoTextPos.y, boxSize.x, boxSize.y), "");
                GUI.skin = SkinMusicVulomeText;
                GUI.Box(new Rect(groupVideoTextPos.x, groupVideoTextPos.y + intervalBox, boxSize.x, boxSize.y), "");
                GUI.skin = SkinVoiceVulomeText;
                GUI.Box(new Rect(groupVideoTextPos.x, groupVideoTextPos.y + 2 * intervalBox, boxSize.x, boxSize.y), "");
                GUI.skin = SkinSFXVulomeText;
                GUI.Box(new Rect(groupVideoTextPos.x, groupVideoTextPos.y + 3 * intervalBox, boxSize.x, boxSize.y), "");
                GUI.skin = SkinSpeakerText;
                GUI.Box(new Rect(groupVideoTextPos.x, groupVideoTextPos.y + 4 * intervalBox, boxSize.x, boxSize.y), " ");
                GUI.skin = SkinSubtitleText;
                GUI.Box(new Rect(groupVideoTextPos.x, groupVideoTextPos.y + 5 * intervalBox, boxSize.x, boxSize.y), " ");

            }
            if (buttonControl)
            {

                horizentalButtonInterval = buttonSize.x + marginRight;


                if (buttonMovement)
                {

                    GUI.skin = SkinMovementActiveBut;
                    GUI.Button(new Rect(groupControlPos.x, groupControlPos.y, buttonSize.x, buttonSize.y), "");
                    //CheckforExistName(KeyboardStr, actionStr, actionDoubleButton, primeryActionStr, seconderyActionStr);


                }
                else
                {
                    GUI.skin = SkinMovementInactiveBut;
                    GUI.Button(new Rect(groupControlPos.x, groupControlPos.y, buttonSize.x, buttonSize.y), "");

                }
                if (buttonAction)
                {
                    GUI.skin = SkinActionActiveBut;
                    GUI.Button(new Rect(groupControlPos.x + horizentalButtonInterval, groupControlPos.y, buttonSize.x, buttonSize.y), "");
                    //CheckforUnName(movementStr, primeryMovementStr, seconderyMovementStr, tempSeconderyMovementStr, movementDoubleButton, movementCount);

                }
                else
                {
                    GUI.skin = SkinActionInactiveBut;
                    GUI.Button(new Rect(groupControlPos.x + horizentalButtonInterval, groupControlPos.y, buttonSize.x, buttonSize.y), "");
                }
                if (buttonLook)
                {
                    GUI.skin = SkinLookActiveBut;
                    GUI.Button(new Rect(groupControlPos.x + 2 * horizentalButtonInterval, groupControlPos.y, buttonSize.x, buttonSize.y), "");
                    //CheckforUnName(movementStr, primeryMovementStr, seconderyMovementStr, tempSeconderyMovementStr, movementDoubleButton, movementCount);
                    //CheckforExistName(KeyboardStr, actionStr, actionDoubleButton, primeryActionStr, seconderyActionStr);
                }
                else
                {
                    GUI.skin = SkinLookInactiveBut;
                    GUI.Button(new Rect(groupControlPos.x + 2 * horizentalButtonInterval, groupControlPos.y, buttonSize.x, buttonSize.y), "");
                }


                GUI.skin = skinRecetContolBut;
                GUI.Button(new Rect(resetPos.x, resetPos.y, buttonSize.x / 2, buttonSize.y), "");

            }


        }
        if (buttonMovement || buttonAction || buttonLook)
        {






            float intervalBox = marginBox + boxSize.y;



            float lableInterval = lableMargin + lableHeight;

            if (buttonMovement)
            {

                GUI.skin = SkinForwardText;
                GUI.Box(new Rect(groupMovementText.x, groupMovementText.y, boxSize.x, boxSize.y), "");
                GUI.skin = SkinBackwardText;
                GUI.Box(new Rect(groupMovementText.x, groupMovementText.y + intervalBox, boxSize.x, boxSize.y), "");
                GUI.skin = SkinLeftText;
                GUI.Box(new Rect(groupMovementText.x, groupMovementText.y + 2 * intervalBox, boxSize.x, boxSize.y), "");
                GUI.skin = SkinRightText;
                GUI.Box(new Rect(groupMovementText.x, groupMovementText.y + 3 * intervalBox, boxSize.x, boxSize.y), "");
                GUI.skin = SkinStandText;
                GUI.Box(new Rect(groupMovementText.x, groupMovementText.y + 4 * intervalBox, boxSize.x, boxSize.y), "");
                GUI.skin = SkinCrouchText;
                GUI.Box(new Rect(groupMovementText.x, groupMovementText.y + 5 * intervalBox, boxSize.x, boxSize.y), "");
                GUI.skin = SkinBrithandHoldBText;
                GUI.Box(new Rect(groupMovementText.x, groupMovementText.y + 6 * intervalBox, boxSize.x, boxSize.y), "");
                GUI.skin = SkinJustEnglish;
                GUI.Label(new Rect(groupMovementPos.x, groupMovementPos.y, lableWide, lableHeight), movementStr[1]);
                GUI.Label(new Rect(groupMovementPos.x, groupMovementPos.y + lableInterval, lableWide, lableHeight), movementStr[2]);
                GUI.Label(new Rect(groupMovementPos.x, groupMovementPos.y + 2 * lableInterval, lableWide, lableHeight), movementStr[3]);
                GUI.Label(new Rect(groupMovementPos.x, groupMovementPos.y + 3 * lableInterval, lableWide, lableHeight), movementStr[4]);
                GUI.Label(new Rect(groupMovementPos.x, groupMovementPos.y + 4 * lableInterval, lableWide, lableHeight), movementStr[5]);
                GUI.Label(new Rect(groupMovementPos.x, groupMovementPos.y + 5 * lableInterval, lableWide, lableHeight), movementStr[6]);
                GUI.Label(new Rect(groupMovementPos.x, groupMovementPos.y + 6 * lableInterval, lableWide, lableHeight), movementStr[7]);
            }

            if (buttonAction)
            {

                GUI.skin = SkinFireText;
                GUI.Box(new Rect(groupActionText.x, groupActionText.y, boxSize.x, boxSize.y), "");
                GUI.skin = SkinAimText;
                GUI.Box(new Rect(groupActionText.x, groupActionText.y + intervalBox, boxSize.x, boxSize.y), "");
                GUI.skin = SkinHoldAimText;
                GUI.Box(new Rect(groupActionText.x, groupActionText.y + 2 * intervalBox, boxSize.x, boxSize.y), "");
                GUI.skin = SkinReloadText;
                GUI.Box(new Rect(groupActionText.x, groupActionText.y + 3 * intervalBox, boxSize.x, boxSize.y), "");
                GUI.skin = SkinSwichWeaponText;
                GUI.Box(new Rect(groupActionText.x, groupActionText.y + 4 * intervalBox, boxSize.x, boxSize.y), "");
                GUI.skin = SkinMeleeText;
                GUI.Box(new Rect(groupActionText.x, groupActionText.y + 5 * intervalBox, boxSize.x, boxSize.y), "");
                GUI.skin = SkinUseText;
                GUI.Box(new Rect(groupActionText.x, groupActionText.y + 6 * intervalBox, boxSize.x, boxSize.y), "");
                GUI.skin = SkinThrowSpatialGrandText;
                GUI.Box(new Rect(groupActionText.x, groupActionText.y + 7 * intervalBox, boxSize.x, boxSize.y), "");


                GUI.skin = SkinShowObjectiveText;
                GUI.Box(new Rect(groupActionText.x, groupActionText.y + 8 * intervalBox, boxSize.x, boxSize.y), "");
                GUI.skin = SkinJustEnglish;
                GUI.Label(new Rect(groupActionPos.x, groupActionPos.y, lableWide, lableHeight), actionStr[1]);
                GUI.Label(new Rect(groupActionPos.x, groupActionPos.y + lableInterval, lableWide, lableHeight), actionStr[2]);
                GUI.Label(new Rect(groupActionPos.x, groupActionPos.y + 2 * lableInterval, lableWide, lableHeight), actionStr[3]);
                GUI.Label(new Rect(groupActionPos.x, groupActionPos.y + 3 * lableInterval, lableWide, lableHeight), actionStr[4]);
                GUI.Label(new Rect(groupActionPos.x, groupActionPos.y + 4 * lableInterval, lableWide, lableHeight), actionStr[5]);
                GUI.Label(new Rect(groupActionPos.x, groupActionPos.y + 5 * lableInterval, lableWide, lableHeight), actionStr[6]);
                GUI.Label(new Rect(groupActionPos.x, groupActionPos.y + 6 * lableInterval, lableWide, lableHeight), actionStr[7]);
                GUI.Label(new Rect(groupActionPos.x, groupActionPos.y + 7 * lableInterval, lableWide, lableHeight), actionStr[8]);

                GUI.Label(new Rect(groupActionPos.x, groupActionPos.y + 8 * lableInterval, lableWide, lableHeight), actionStr[9]);
            }
            if (buttonLook)
            {

                GUI.skin = SkinMouseSensivityText;
                GUI.Box(new Rect(groupLookText.x, groupLookText.y, boxSize.x, boxSize.y), "");
                GUI.skin = SkinMouseInvertText;
                GUI.Box(new Rect(groupLookText.x, groupLookText.y + intervalBox, boxSize.x, boxSize.y), "");

                mouseSensivity = GUI.HorizontalSlider(new Rect(groupLookPos.x, groupLookPos.y, sliderWide, sliderHeight), mouseSensivity, 0f, 1f);
                if (invertMouseCount == 1)
                {
                    GUI.skin = SkinYesForInvertMouse;
                }
                else
                {
                    GUI.skin = SkinNoForINvertMouse;
                }
                GUI.Button(new Rect(groupLookPos.x, groupLookPos.y + marginSliderToButton, buttonSize.x, buttonSize.y), "");
            }



        }




    }
    void SetSizeAndPos()
    {
        group1Pos = CalculatePosition(DevideScreenGroup1Pos);
        showWarningPos = CalculatePosition(DevideScreenWarningPos);
        showWarningSize = CalculateSize(DevideScreenWarningSizeX, AspectWarningSizeY);
        groupWarningPos = CalculatePosition(DevideScreenWarningGroupPos);
        groupInGamePos = CalculatePosition(DevideScreenGroupInGamePos);
        groupInGameNamePos = CalculatePosition(DevideScreenGroupInGameNamePos);
        TransParTexPos = CalculatePosition(DevideScreenShowSecondTexrurePos);
        TransParTexSize = CalculateSize(DevideScreenShowSecondTexrureSizeX, AspectShowSecondTexrureSizeY);
        groupLookPos = CalculatePosition(DevideScreenLookGroupPos);
        groupLookText = CalculatePosition(DevideScreenLookTextGroupPos);
        showExitPos = CalculatePosition(DevideScreenWarningPos);
        showExitSize = CalculateSize(DevideScreenWarningSizeX, AspectWarningSizeY);
        groupWarningPos = CalculatePosition(DevideScreenWarningGroupPos);
        groupActionPos = CalculatePosition(DevideScreenActionGroupPos);
        groupActionText = CalculatePosition(DevideScreenActionTextGroupPos);
        groupMovementPos = CalculatePosition(DevideScreenMovementGroupPos);
        groupMovementText = CalculatePosition(DevideScreenMovementTextGroupPos);
        resetPos = CalculatePosition(DevideScreenResetPos);
        controlName = CalculatePosition(DevideScreenControlNamePos);
        groupControlPos = CalculatePosition(DevideScreenGroupControlPos);
        groupPos = CalculatePosition(DevideScreenGroupVideoPos);
        groupVideoTextPos = CalculatePosition(DevideScreenGroupVideoTextPos);
        GOptionsNamePos = CalculatePosition(DevideScreenGroupOptionsNamePos);
        advanceTextPos = CalculatePosition(DevideScreenGroupAdvanceTextPos);
        groupBackPos = CalculatePosition(DevideScreenGroupBackPos);
        groupAdvanceVPos = CalculatePosition(DevideScreenGroupAdvanceButtonPos);
        groupOptionPos = CalculatePosition(DevideScreenGroupOptionPos);
        optionName = CalculatePosition(DevideScreenOptionNamePos);
        showApplySettingPos = CalculatePosition(DevideScreenShowApplySettingPos);
        showApplySettingSize = CalculateSize(DevideScreenShowApplySettingSizeX, AspectShowApplySettingSizeY);
        groupYesNOPos = CalculatePosition(DevideScreenShowApplySettingGroupPos);
        buttonSize = CalculateSize(DevideScreenButtonSizeX, AspectButtonSizeY);
        sliderSize = CalculateSize(DevideScreenSliderSizeX, AspectSliderSizeY);
        sliderHeight = 10;
        sliderWide = sliderSize.x;
        Vector2 marginSliderVec;
        marginSliderVec.x = DevScrSliderMargin;
        marginSliderVec.y = 0;
        marginSliderVec = CalculateSize(marginSliderVec.x, 0);
        marginSlider = marginSliderVec.x;
        Vector2 marginSliderToButtonVec;
        marginSliderToButtonVec.x = DevScrSliderMarginToButton;
        marginSliderToButtonVec.y = 0;
        marginSliderToButtonVec = CalculateSize(marginSliderToButtonVec.x, 0);
        marginSliderToButton = marginSliderToButtonVec.x;
        boxSize = CalculateSize(DevideScreenBoxSizeX, AspectBoxSizeY);
        Vector2 marginVec;
        Vector2 marginSize;
        marginVec.x = devScrButMargin;
        marginSize = CalculateSize(marginVec.x, 0);
        margin = marginSize.x;

        Vector2 marginBoxVec;
        marginBoxVec.x = devScrMargBox;
        marginBoxVec = CalculateSize(marginBoxVec.x, 0);
        marginBox = marginBoxVec.x;
        interval = margin + buttonSize.y;

        Vector2 marginRVec;
        marginRVec.x = devScrButMargRight;
        marginRVec = CalculateSize(marginRVec.x, 0);
        marginRight = marginRVec.x;

        Vector2 lableSize;
        lableSize = CalculateSize(DevideScreenLableSizeX, AspectLableSizeY);
        lableWide = lableSize.x;
        lableHeight = lableSize.y;

        Vector2 marginLableVec;
        marginLableVec.x = DevScrLableMargin;
        marginLableVec = CalculateSize(marginLableVec.x, 0);
        lableMargin = marginLableVec.x;



    }
    Vector2 CalculatePosition(Vector2 divide)
    {
        Vector2 position;
        position.x = Screen.width - Screen.width / divide.x;
        position.y = Screen.height - Screen.height / divide.y;
        return (position);
    }
    Vector2 CalculateSize(float divideX, float aspectY)
    {
        Vector2 size;
        size.x = Screen.width / divideX;
        size.y = size.x * aspectY;
        return (size);
    }
    void CheckMousePos(Vector2 groupPos, int ButtonCount)
    {
        buttonNumber = 0;
        float mouseY = Screen.height - CustomInputManager.GetMouseY(); //Input.mousePosition.y;
        float mouseX = CustomInputManager.GetMouseX(); //Input.mousePosition.x;
        float min = groupPos.y;
        float max = groupPos.y + buttonSize.y;
        float minX = groupPos.x;
        float maxX = groupPos.x + buttonSize.x;
        if (halfButsize == true)
        {

            maxX = groupPos.x + buttonSize.x / 2;
        }


        if (checkOnAudio || checkOnLook)
        {
            max = sliderHeight + groupPos.y;
        }

        if (checkOnMovement && (buttonMovement || buttonAction))
        {
            max = lableHeight + groupPos.y;
            maxX = groupPos.x + lableWide;


        }

        if (minX <= mouseX && mouseX <= maxX)
        {

            for (int i = 1; i <= ButtonCount; i++)
            {

                if (min <= mouseY && mouseY <= max)
                {
                    buttonOnHover = true;
                    buttonNumber = i;
                    i = ButtonCount;

                }
                else
                {
                    buttonOnHover = false;
                }
                if (checkOnAudio && i <= 3)
                {
                    min += sliderHeight + marginSlider;
                }
                else if (checkOnAudio && i == 4 || ((checkOnLook) && i == 1))
                {
                    min += marginSliderToButton;
                }
                else if (checkOnMovement && (buttonAction || buttonMovement))
                {
                    min += lableHeight + lableMargin;

                }
                else
                {
                    min += buttonSize.y + margin;
                }

                if (checkOnVideo)
                {
                    max += margin + sliderHeight;

                }

                else if (checkOnAudio && i <= 3)
                {
                    max += marginSlider + sliderHeight;
                }
                else if ((checkOnAudio && i == 4) || (checkOnLook) && i == 1)
                {

                    max += marginSliderToButton + buttonSize.y - sliderHeight;
                }
                else if (checkOnMovement && (buttonMovement || buttonAction))
                {
                    max += lableMargin + lableHeight;
                }

                else
                {
                    max += buttonSize.y + margin;
                }



            }
        }
        else
        {
            buttonOnHover = false;
        }


    }
    void CheckHorizentalMousePos(Vector2 groupPos, int ButtonCount)
    {
        buttonNumber = 0;
        float mouseY = Screen.height - CustomInputManager.GetMouseY(); //Input.mousePosition.y;
        float mouseX = CustomInputManager.GetMouseX(); //Input.mousePosition.x;
        float min = groupPos.y;
        float max = groupPos.y + buttonSize.y;
        float minX = groupPos.x;
        float maxX = groupPos.x + buttonSize.x;
        if (min <= mouseY && mouseY <= max)
        {

            for (int i = 1; i <= ButtonCount; i++)
            {

                if (minX <= mouseX && mouseX <= maxX)
                {
                    buttonOnHover = true;
                    buttonNumber = i;
                    i = ButtonCount;

                }
                else
                {
                    buttonOnHover = false;

                    minX += horizentalButtonInterval;

                    maxX += horizentalButtonInterval;
                }



            }
        }
        else
        {
            buttonOnHover = false;
        }

    }
    void OnHoverSound()
    {
        if (onHoverCount == 0)
        {
            onHoverCount++;
            audio.Play();
        }


    }
    void HandelInput()
    {
        if (CustomInputManager.GetMouse_LeftButton_Up() || escapeInsteadOfBack || escapeInsteadOfYesApply) //(Input.GetMouseButtonUp(0) || escapeInsteadOfBack || escapeInsteadOfYesApply)
        {
            if (!escapeInsteadOfBack && !escapeInsteadOfYesApply)
            {
                if (buttonGroup1)
                {
                    if (showApplySetting == false)
                    {
                        buttonGroup1 = false;
                        ShowTransparentTex = true;
                        showMainBackGroundPauseMenu = false;
                        if (buttonNumber == 1)
                        {
                            ButtonNewGame = true;
                        }
                        if (buttonNumber == 2)
                        {
                            ButtonCountinue = true;
                        }
                        if (buttonNumber == 3)
                        {
                            buttonOption = true;
                        }
                        if (buttonNumber == 4)
                        {
                            ButtonExit = true;
                            buttonGroup1 = true;
                        }
                    }

                }
                if (hoverOnInGame)
                {
                    if (showApplySetting == false)
                    {

                        if (buttonNumber == 1)
                        {
                            StopInGameMenu(false);
                            showMainBackGroundPauseMenu = false;
                            buttonGroupInGame = false;
                        }
                        else
                        {

                            buttonGroupInGame = false;
                            hoverOnInGame = false;
                            InGameOtherButton = true;
                            if (buttonNumber == 2)
                            {
                                buttonOption = true;
                                ShowTransparentTex = true;
                            }
                            if (buttonNumber == 5)
                            {
                                ButtonExit = true;
                                buttonGroupInGame = true;
                            }
                        }
                    }
                }
                if (hoverOnExit)
                {
                    ButtonExit = false;
                    hoverOnExit = false;
                    if (buttonNumber == 1)
                    {
                        Screen.SetResolution(monitorPreRES.width, monitorPreRES.height, true);
                        Application.Quit();

                    }
                    if (buttonNumber == 2)
                    {
                        ShowTransparentTex = false;
                        InGameOtherButton = false;
                    }

                }
                if (hoverOnOption)
                {
                    hoverOnOption = false;



                    if (buttonNumber == 1)
                    {
                        buttonVideo = true;
                        buttonAudio = false;
                        buttonControl = false;
                        buttonMovement = false;
                        buttonAction = false;
                        buttonLook = false;
                        if (videoSettingTempIndex == 3)
                        {
                            costumAdvanceVideo = true;
                        }
                        else
                            costumAdvanceVideo = false;
                    }

                    if (buttonNumber == 2)
                    {
                        buttonAudio = true;
                        buttonControl = false;
                        buttonMovement = false;
                        buttonAction = false;
                        buttonLook = false;
                        buttonVideo = false;
                        costumAdvanceVideo = false;
                    }
                    if (buttonNumber == 3)
                    {
                        buttonControl = true;
                        buttonMovement = true;
                        buttonVideo = false;
                        buttonAudio = false;

                        costumAdvanceVideo = false;
                    }
                }

                if (hoverOnResOrBri)
                {

                    if (costumAdvanceVideo && changeForAdvanceV == false)
                    {

                        costumAdvanceVideo = false;
                        buttonOption = true;

                    }
                    showApplySetting = true;
                    if (buttonNumber == 1)
                    {
                        SetResolutionOnButton(1);
                        costumAdvanceVideo = false;
                    }
                    if (buttonNumber == 2)
                    {
                        SetBrightness(brightnessSlider);
                        costumAdvanceVideo = false;

                    }

                }
                if (hoverOnGroupAdvaceV)
                {
                    hoverOnGroupAdvaceV = false;
                    changeForAdvanceV = true;
                    SetAdvanceVOnButton(buttonNumber, 1);
                }

                if (hoverOnAudio)
                {
                    showApplySetting = true;
                    if (buttonNumber == 1)
                    {
                        generalVolumeSetslide = SetVolumeOnButton(generalVolumeSlider, generalVolumeSetslide);

                    }
                    if (buttonNumber == 2)
                    {
                        musicVolumeSetslide = SetVolumeOnButton(musicVolumeSlider, musicVolumeSetslide);
                    }
                    if (buttonNumber == 3)
                    {
                        voiceVolumeSetslide = SetVolumeOnButton(voiceVolumeSlider, voiceVolumeSetslide);

                    }
                    if (buttonNumber == 4)
                    {
                        sFXVolumeSetslide = SetVolumeOnButton(sFXVolumeSlider, sFXVolumeSetslide);
                    }
                    if (buttonNumber == 5)
                    {
                        SetSpeakerConfigOnButton();
                    }
                    if (buttonNumber == 6)
                    {
                        SetSubtitleOnButton();
                    }
                }
                if (hoverOnControls)
                {

                    hoverOnControls = false;

                    if (buttonNumber == 1)
                    {
                        buttonMovement = true;
                        buttonLook = false;
                        buttonAction = false;

                    }
                    if (buttonNumber == 2)
                    {
                        buttonAction = true;
                        buttonMovement = false;
                        buttonLook = false;
                    }
                    if (buttonNumber == 3)
                    {
                        buttonLook = true;
                        buttonAction = false;
                        buttonMovement = false;
                    }

                }

                if (hoverOnReset)
                {
                    Reset();
                }



                if (hoverOnAdvanceVideo)
                {
                    hoverOnAdvanceVideo = false;
                    changeForAdvanceV = true;

                    SetVideoSettingButton(1);
                    if (videoSettingTempIndex == 3)
                    {
                        costumAdvanceVideo = true;

                    }
                    else
                        costumAdvanceVideo = false;
                    buttonOption = true;
                    buttonVideo = true;

                }

                if (hoverOnLook)
                {
                    changeInControls = true;
                    if (buttonNumber == 2)
                    {
                        SetMouseInvert();
                    }
                }
            }
            if (hoverOnApplysetting || escapeInsteadOfYesApply)
            {

                if (buttonNumber == 1 || escapeInsteadOfYesApply)
                {
                    ApplyControlsButtonSet(movementDoubleStr, actionDoubleStr);
                    buttonAction = false;
                    buttonMovement = false;
                    buttonLook = false;
                    buttonControl = false;
                    buttonOption = false;
                    if (InGameMenu)
                    {
                        buttonGroupInGame = true;
                    }
                    else
                    {
                        buttonGroup1 = true;
                    }
                    showBack = false;

                    ApplySetting();
                    ApplyAdvanceV();
                    SetApplyAdvanceVForSave();
                }
                if (buttonNumber == 2 && !escapeInsteadOfYesApply)
                {
                    if (showApplySettingForControls)
                    {
                        
                        buttonAction = false;
                        buttonMovement = false;
                        buttonLook = false;
                        buttonOption = false;
                        DontApplySetting();
                        DontApplyAdvanceV();
                    }
                    if (showWarning)
                    {
                        buttonAction = false;
                        buttonControl = true;
                        buttonMovement = true;
                        buttonLook = false;
                        buttonOption = true;
                        if (InGameMenu)
                        {
                            buttonGroupInGame = false;
                            InGameOtherButton = true;
                        }
                        else
                        {
                            buttonGroup1 = false;

                        }

                    }
                    else
                    {
                        buttonAction = false;
                        buttonMovement = false;
                        buttonLook = false;
                        buttonOption = false;
                        DontApplySetting();
                        DontApplyAdvanceV();
                    }

                }

                showApplySettingForControls = false;
                showWarning = false;
                showApplySetting = false;
                ShowTransparentTex = false;
                hoverOnApplysetting = false;

                changeForAdvanceV = false;
                costumAdvanceVideo = false;
                applyBoolian = false;
                usedEscapeForBack = false;
                escapeInsteadOfYesApply = false;
            }
            if (hoverOnBack || escapeInsteadOfBack)
            {
                if (escapeInsteadOfBack)
                {
                    usedEscapeForBack = true;
                }
                escapeInsteadOfBack = false;
                hoverOnBack = false;
                if (buttonOption == true)
                {
                    if (changeForAdvanceV)
                    {
                        changeForAdvanceV = false;
                        showApplySetting = true;

                    }
                    if (changeInControls == true)
                    {
                        showApplySetting = true;
                        changeInControls = false;
                        bool unboundAction = CheckForUnbound(actionStr);
                        bool unboundMovement = CheckForUnbound(movementStr);

                        if (unboundAction == true || unboundMovement == true)
                        {
                            showWarning = true;
                            unboundAction = false;
                            unboundMovement = false;

                        }
                        else
                        {
                            showApplySettingForControls = true;
                        }

                    }
                    if (InGameMenu == false)
                    {
                        buttonGroup1 = true;
                        buttonOption = false;
                        buttonAudio = false;
                        buttonVideo = false;
                        buttonControl = false;
                        buttonLook = false;
                        buttonMovement = false;
                        buttonAction = false;
                        costumAdvanceVideo = false;

                        showBack = false;
                        if (showApplySetting == false)
                        {
                            ShowTransparentTex = false;
                        }
                    }
                    else
                    {
                        buttonGroupInGame = true;
                        InGameOtherButton = false;
                        buttonOption = false;
                        buttonAudio = false;
                        buttonVideo = false;
                        buttonControl = false;
                        buttonMovement = false;
                        buttonAction = false;
                        buttonLook = false;
                        costumAdvanceVideo = false;
                        showBack = false;
                        if (showApplySetting == false)
                        {
                            ShowTransparentTex = false;
                        }
                    }
                }
                if (!showApplySetting && !showWarning && !showApplySettingForControls)
                {
                    usedEscapeForBack = false;
                }
                else
                {
                    applyBoolian = true;
                }
            }
        }
        if (CustomInputManager.GetMouse_RightButton_Down())//(Input.GetMouseButtonDown(1))
        {
            if (hoverOnResOrBri)
            {
                showApplySetting = true;
                if (buttonNumber == 1)
                {
                    SetResolutionOnButton(2);
                }
            }
            if (hoverOnGroupAdvaceV)
            {
                showApplySetting = true;
                SetAdvanceVOnButton(buttonNumber, 2);
            }
            if (hoverOnAdvanceVideo)
            {
                hoverOnAdvanceVideo = false;
                changeForAdvanceV = true;

                SetVideoSettingButton(2);
                if (videoSettingTempIndex == 3)
                {
                    costumAdvanceVideo = true;

                }
                else
                {
                    costumAdvanceVideo = false;
                }
                buttonOption = true;
                buttonVideo = true;

            }
        }
        if (onActionKeySet == true)
        {






            if (alamatSoalExistOnA)
            {
                ControlsButtonSet(actionStr, actionDoubleStr);
                hoverOnAction = false;

            }
            else if (alamatSoalExistOnM)
            {
                ControlsButtonSet(movementStr, movementDoubleStr);
                hoverOnMovement = false;

            }
            else if (buttonMovement)
            {

                ControlsButtonSet(movementStr, movementDoubleStr);
                hoverOnMovement = false;


            }
            else if (buttonAction)
            {

                ControlsButtonSet(actionStr, actionDoubleStr);
                hoverOnAction = false;


            }



            onActionKeySet = false;


        }

    }
    void DontSetControlBut(List<DoubleStr> strMove, List<DoubleStr> strAction, List<string> str1Move, List<string> str1Action)
    {
        strMove[1].FirstStr = ControlMovementPrimier.Forward;
        strMove[2].FirstStr = ControlMovementPrimier.MoveBack;
        strMove[3].FirstStr = ControlMovementPrimier.MoveLeft;

        strMove[4].FirstStr = ControlMovementPrimier.MoveRight;
        strMove[5].FirstStr = ControlMovementPrimier.StandOrJump;
        strMove[6].FirstStr = ControlMovementPrimier.Crouch;
        strMove[7].FirstStr = ControlMovementPrimier.SprintOrHoldBreath;

        strAction[1].FirstStr = ControlActionPrimier.FireWeapon;
        strAction[2].FirstStr = ControlActionPrimier.AimDownTheSight;
        strAction[3].FirstStr = ControlActionPrimier.HoldAimDownTheSight;
        strAction[4].FirstStr = ControlActionPrimier.Reloal;
        strAction[5].FirstStr = ControlActionPrimier.SwichWeapon;
        strAction[6].FirstStr = ControlActionPrimier.Melee;
        strAction[7].FirstStr = ControlActionPrimier.Use;
        strAction[8].FirstStr = ControlActionPrimier.TrowGrenade;
        strAction[9].FirstStr = ControlActionPrimier.ShowObjective;

        strMove[1].SecondStr = ControlMovementSecondery.Forward;
        strMove[2].SecondStr = ControlMovementSecondery.MoveBack;
        strMove[3].SecondStr = ControlMovementSecondery.MoveLeft;
        strMove[4].SecondStr = ControlMovementSecondery.MoveRight;
        strMove[5].SecondStr = ControlMovementSecondery.StandOrJump;
        strMove[6].SecondStr = ControlMovementSecondery.Crouch;
        strMove[7].SecondStr = ControlMovementSecondery.SprintOrHoldBreath;


        strAction[1].SecondStr = ControlActionSecondery.FireWeapon;
        strAction[2].SecondStr = ControlActionSecondery.AimDownTheSight;
        strAction[3].SecondStr = ControlActionSecondery.HoldAimDownTheSight;
        strAction[4].SecondStr = ControlActionSecondery.Reloal;
        strAction[5].SecondStr = ControlActionSecondery.SwichWeapon;
        strAction[6].SecondStr = ControlActionSecondery.Melee;
        strAction[7].SecondStr = ControlActionSecondery.Use;
        strAction[8].SecondStr = ControlActionSecondery.TrowGrenade;
        strAction[9].SecondStr = ControlActionSecondery.ShowObjective;

        for (int i = 1; i <= 9; i++)
        {

            if (strAction[i].SecondStr != "")
            {
                strAction[i].HavingDoubleStr = true;

            }
            else
            {
                strAction[i].HavingDoubleStr = false;

            }
            str1Action[i] = MakingFullStrToShow(strAction[i]);

        }
        for (int i = 1; i <= 7; i++)
        {

            if (strMove[i].SecondStr != "")
            {
                strMove[i].HavingDoubleStr = true;

            }
            else
            {
                strMove[i].HavingDoubleStr = false;

            }
            str1Move[i] = MakingFullStrToShow(strMove[i]);
        }

        if (ControlLook.MouseInvert == true)
        {
            invertMouseOldCount = 1;
        }
        if (ControlLook.MouseInvert == false)
        {
            invertMouseOldCount = 0;
        }
        invertMouseCount = invertMouseOldCount;
        mouseOldSensivity = ControlLook.MouseSensivity;
        mouseSensivity = mouseOldSensivity;
    }
    void SetResolutionOnButton(int state)
    {
        if (state == 1)
        {
            if (resolutionTempIndex < ResolutionStr.Count - 1)
            {
                resolutionTempIndex++;
            }
            else
                resolutionTempIndex = 0;


        }
        if (state == 2)
        {
            if (resolutionTempIndex > 0)
            {
                resolutionTempIndex--;
            }
            else
                resolutionTempIndex = ResolutionStr.Count - 1;


        }

        onHoverCount = 0;
        OnHoverSound();


    }
    float SetVolumeOnButton(float vSlider, float setVSlider)
    {
        setVSlider = vSlider;

        SetAudioSliderToVolumeControler(vSlider);

        onHoverCount = 0;
        OnHoverSound();
        return (setVSlider);

    }
    void SetSpeakerConfigOnButton()
    {
        if (speakerConfigIndex < SpeakerConfigStr.Length - 1)
        {
            speakerConfigIndex++;
        }
        else
            speakerConfigIndex = 0;

        ApplySpeakerConfig(speakerConfigIndex);
        onHoverCount = 0;
        OnHoverSound();
    }
    void SetSubtitleOnButton()
    {
        if (subTitleIndex < 1)
        {
            subTitleIndex++;
        }
        else
            subTitleIndex = 0;
        onHoverCount = 0;
        OnHoverSound();
    }
    void SetVideoSettingButton(int state)
    {
        //state 1==right mouse
        //state 2==left mouse
        if (state == 1)
        {
            if (videoSettingTempIndex < 3)
            {
                videoSettingTempIndex++;
            }
            else
            {
                videoSettingTempIndex = 0;
            }
        }
        if (state == 2)
        {
            if (videoSettingTempIndex > 0)
            {
                videoSettingTempIndex--;
            }
            else
            {
                videoSettingTempIndex = 3;
            }
        }
        onHoverCount = 0;
        OnHoverSound();
    }
    void ApplySetting()
    {

        resolutionIndex = resolutionTempIndex;
        Screen.SetResolution(ResX[resolutionIndex], ResY[resolutionIndex], true);
        generalVolumeOldSlider = generalVolumeSlider;
        voiceVolumeOldSlider = voiceVolumeSlider;
        musicVolumeOldSlider = musicVolumeSlider;
        sFXVolumeOldSlider = sFXVolumeSlider;
        SetAudioSliderToVolumeControler(generalVolumeSlider);
        SetAudioSliderToVolumeControler(voiceVolumeSlider);
        SetAudioSliderToVolumeControler(musicVolumeSlider);
        SetAudioSliderToVolumeControler(sFXVolumeSlider);
        speakerConfigOldIndex = speakerConfigIndex;
        brightnessOldSlider = brightnessSlider;
        subTitleOldIndex = subTitleIndex;
        Brightness.Instance.SetBrighness(brightnessSlider);
        ApplySpeakerConfig(speakerConfigIndex);
        otherOptionParameter.Brightness = brightnessSlider;
        otherOptionParameter.ResolutionIndex = resolutionIndex;
        otherOptionParameter.SubTitleIndex = subTitleIndex;
        otherOptionParameter.SpeakerConfigIndex = speakerConfigIndex;
        otherOptionParameter.VoiceVolume = voiceVolumeSlider;
        otherOptionParameter.GeneralVolume = generalVolumeSlider;
        otherOptionParameter.SFXVolume = sFXVolumeSlider;
        otherOptionParameter.MusicVolume = musicVolumeSlider;
        otherOptionParameter.AnyChangeOnOtherParameter = true;
    }
    void DontApplySetting()
    {
        brightnessSlider = otherOptionParameter.Brightness;
        resolutionIndex = otherOptionParameter.ResolutionIndex;
        subTitleIndex = otherOptionParameter.SubTitleIndex;
        speakerConfigIndex = otherOptionParameter.SpeakerConfigIndex;
        voiceVolumeSlider = otherOptionParameter.VoiceVolume;
        generalVolumeSlider = otherOptionParameter.GeneralVolume;
        sFXVolumeSlider = otherOptionParameter.SFXVolume;
        musicVolumeSlider = otherOptionParameter.MusicVolume;
        resolutionTempIndex = resolutionIndex;
        //generalVolume
        generalVolumeSlider = generalVolumeOldSlider;
        generalVolumeSetslide = generalVolumeOldSlider;
        SetAudioSliderToVolumeControler(generalVolumeSlider);
        //voiceVolume
        voiceVolumeSlider = voiceVolumeOldSlider;
        voiceVolumeSetslide = voiceVolumeOldSlider;
        SetAudioSliderToVolumeControler(voiceVolumeSlider);
        //SFX Volume
        sFXVolumeSlider = sFXVolumeOldSlider;
        sFXVolumeSetslide = sFXVolumeOldSlider;
        SetAudioSliderToVolumeControler(sFXVolumeSlider);
        //music Volume
        musicVolumeSlider = musicVolumeOldSlider;
        musicVolumeSetslide = musicVolumeOldSlider;
        SetAudioSliderToVolumeControler(musicVolumeSlider);
        //
        ApplySpeakerConfig(speakerConfigOldIndex);
        speakerConfigIndex = speakerConfigOldIndex;
        brightnessSlider = brightnessOldSlider;
        brightnessSetslide = brightnessOldSlider;
        subTitleIndex = subTitleOldIndex;
        Brightness.Instance.SetBrighness(brightnessSlider);
        Screen.SetResolution(ResX[resolutionIndex], ResY[resolutionIndex], true);

    }
    void ApplySpeakerConfig(int index)
    {
        if (index == 0)
        {
            AudioSettings.speakerMode = AudioSpeakerMode.Stereo;
        }
        if (index == 1)
        {
            AudioSettings.speakerMode = AudioSpeakerMode.Mono;
        }

        if (index == 2)
        {
            AudioSettings.speakerMode = AudioSpeakerMode.Quad;
        }
        if (index == 3)
        {
            AudioSettings.speakerMode = AudioSpeakerMode.Mode5point1;
        }
        if (index == 4)
        {
            AudioSettings.speakerMode = AudioSpeakerMode.Mode7point1;
        }
    }
    void Reset()
    {

        changeInControls = true;
        for (int i = 0; i < DefaultActionStr.Length; i++)
        {

            DoubleStr CurrentdoubleStr = new DoubleStr();
            //ActionCount.Add(0);

            if (DefaultActionSecondaryStr[i] != "")
            {
                CurrentdoubleStr.FirstStr = DefaultActionStr[i];
                CurrentdoubleStr.SecondStr = DefaultActionSecondaryStr[i];
                CurrentdoubleStr.HavingDoubleStr = true;
                actionDoubleStr[i] = (CurrentdoubleStr);
            }
            else
            {
                CurrentdoubleStr.FirstStr = DefaultActionStr[i];
                CurrentdoubleStr.SecondStr = "";
                CurrentdoubleStr.HavingDoubleStr = false;
                actionDoubleStr[i] = (CurrentdoubleStr);

            }
            actionStr[i] = (MakingFullStrToShow(CurrentdoubleStr));
            oldActionStr[i] = (MakingFullStrToShow(CurrentdoubleStr));

        }


        for (int i = 0; i < DefaultMovementStr.Length; i++)
        {
            DoubleStr CurrentdoubleStr = new DoubleStr();

            if (DefaultMovementSecondaryStr[i] != "")
            {


                CurrentdoubleStr.FirstStr = DefaultMovementStr[i];
                CurrentdoubleStr.SecondStr = DefaultMovementSecondaryStr[i];
                CurrentdoubleStr.HavingDoubleStr = true;
                movementDoubleStr[i] = (CurrentdoubleStr);


            }
            else
            {


                CurrentdoubleStr.FirstStr = DefaultMovementStr[i];
                CurrentdoubleStr.SecondStr = "";
                CurrentdoubleStr.HavingDoubleStr = false;
                movementDoubleStr[i] = (CurrentdoubleStr);


            }
            movementStr[i] = (MakingFullStrToShow(CurrentdoubleStr));
            oldMovementStr[i] = (MakingFullStrToShow(CurrentdoubleStr));
        }
        mouseSensivity = MouseSensivityDefault;
        invertMouseCount = 0;


    }
    void ApplyControlsButtonSet(List<DoubleStr> strMove, List<DoubleStr> strAction)
    {
        //oldActionStr = strAction;
        //oldMovementStr = strMove;
        mouseOldSensivity = mouseSensivity;
        invertMouseOldCount = invertMouseCount;
        ControlActionPrimier.AnyChangeControlSave = true;
        ControlMovementPrimier.Forward = strMove[1].FirstStr;
        ControlMovementPrimier.MoveBack = strMove[2].FirstStr;
        ControlMovementPrimier.MoveLeft = strMove[3].FirstStr;
        ControlMovementPrimier.MoveRight = strMove[4].FirstStr;

        ControlMovementPrimier.StandOrJump = strMove[5].FirstStr;
        ControlMovementPrimier.Crouch = strMove[6].FirstStr;
        ControlMovementPrimier.SprintOrHoldBreath = strMove[7].FirstStr;

        ControlActionPrimier.FireWeapon = strAction[1].FirstStr;
        ControlActionPrimier.AimDownTheSight = strAction[2].FirstStr;
        ControlActionPrimier.HoldAimDownTheSight = strAction[3].FirstStr;
        ControlActionPrimier.Reloal = strAction[4].FirstStr;
        ControlActionPrimier.SwichWeapon = strAction[5].FirstStr;
        ControlActionPrimier.Melee = strAction[6].FirstStr;
        ControlActionPrimier.Use = strAction[7].FirstStr;
        ControlActionPrimier.TrowGrenade = strAction[8].FirstStr;
        ControlActionPrimier.ShowObjective = strAction[9].FirstStr;

        ControlMovementSecondery.Forward = strMove[1].SecondStr;
        ControlMovementSecondery.MoveBack = strMove[2].SecondStr;
        ControlMovementSecondery.MoveLeft = strMove[3].SecondStr;
        ControlMovementSecondery.MoveRight = strMove[4].SecondStr;
        ControlMovementSecondery.StandOrJump = strMove[5].SecondStr;
        ControlMovementSecondery.Crouch = strMove[6].SecondStr;
        ControlMovementSecondery.SprintOrHoldBreath = strMove[7].SecondStr;

        ControlActionSecondery.FireWeapon = strAction[1].SecondStr;
        ControlActionSecondery.AimDownTheSight = strAction[2].SecondStr;
        ControlActionSecondery.HoldAimDownTheSight = strAction[3].SecondStr;
        ControlActionSecondery.Reloal = strAction[4].SecondStr;
        ControlActionSecondery.SwichWeapon = strAction[5].SecondStr;
        ControlActionSecondery.Melee = strAction[6].SecondStr;
        ControlActionSecondery.Use = strAction[7].SecondStr;
        ControlActionSecondery.TrowGrenade = strAction[8].SecondStr;
        ControlActionSecondery.ShowObjective = strAction[9].SecondStr;
        if (invertMouseCount == 1)
        {
            ControlLook.MouseInvert = true;
        }
        if (invertMouseCount == 0)
        {
            ControlLook.MouseInvert = false;
        }
        ControlLook.MouseSensivity = mouseSensivity;
        mouseOldSensivity = mouseSensivity;

    }
    void ControlsButtonSet(List<string> str, List<DoubleStr> currentDoubleStr)
    {
        bool alamatSolalExist = CheckForAlamatesoal(str);
        if (alamatSolalExist == false && KeyboardStr == "Left Mouse")
        {
            str[buttonNumber] = "???";
            buttonNumberOnset = buttonNumber;
            time = Time.time;
            changeInControls = true;

        }
        else if (alamatSolalExist == true)
        {

            str[buttonNumberOnset] = MakingNewStrAfterInput(currentDoubleStr[buttonNumberOnset], KeyboardStr);


        }

    }
    void CheckforExistName(string currentStr, List<string> g1Str, List<DoubleStr> g1)
    {
        for (int i = 1; i < g1.Count; i++)
        {

            if (g1[i].HavingDoubleStr == false)
            {

                if (currentStr == g1[i].FirstStr)
                {

                    g1[i].FirstStr = "Unbound";
                    g1[i].SecondStr = "";
                    g1[i].HavingDoubleStr = false;
                    g1Str[i] = MakingFullStrToShow(g1[i]);

                }
            }
            else
            {

                if (currentStr == g1[i].SecondStr)
                {

                    g1[i].SecondStr = "";
                    g1[i].HavingDoubleStr = false;
                    g1Str[i] = MakingFullStrToShow(g1[i]);
                }
                else if (currentStr == g1[i].FirstStr)
                {

                    g1[i].FirstStr = g1[i].SecondStr;
                    g1[i].SecondStr = "";
                    g1[i].HavingDoubleStr = false;
                    g1Str[i] = MakingFullStrToShow(g1[i]);

                }
            }
        }

    }
    bool CheckForUnbound(List<string> g1)
    {
        bool unbound = false;
        for (int i = 1; i < g1.Count; i++)
        {

            if ("Unbound" == g1[i])
            {
                unbound = true;
            }
        }
        return unbound;


    }
    bool CheckForAlamatesoal(List<string> g1)
    {
        bool alamat = false;
        for (int i = 1; i < g1.Count; i++)
        {

            if ("???" == g1[i])
            {
                alamat = true;
            }
        }
        return alamat;


    }
    void CheckforUnName(List<string> g1, List<string> PriG1, List<string> secG1, List<string> tempSecG1, List<bool> doubleCount, List<int> count)
    {
        for (int i = 1; i < g1.Count; i++)
        {
            if (g1[i] == "???" || g1[i] == "None")
            {
                if (i != buttonNumberOnset)
                {
                    g1[i] = "Unbound";
                    PriG1[i] = "Unbound";
                    doubleCount[i] = false;
                    secG1[i] = "";
                    tempSecG1[i] = "";
                    count[i] = 0;
                }
            }
        }
    }
    void SetMouseInvert()
    {
        if (invertMouseCount < 1)
        {
            invertMouseCount++;
        }
        else
            invertMouseCount = 0;
    }
    void SetBrightness(float brightness)
    {
        if (InGameMenu)
        {
            Brightness.Instance.SetBrighness(brightness);
        }
        brightnessSetslide = brightness;
    }
    void SetAudioSliderToVolumeControler(float vSlider)
    {

        if (vSlider == generalVolumeSlider)
        {
            AudioController.SetVolume_General(generalVolumeSlider);
        }
        else if (vSlider == musicVolumeSlider)
        {
            AudioController.SetVolume_Music(musicVolumeSlider);
        }
        else if (vSlider == voiceVolumeSlider)
        {
            AudioController.SetVolume_Voice(voiceVolumeSlider);
        }
        else if (vSlider == sFXVolumeSlider)
        {
            AudioController.SetVolume_SFX(sFXVolumeSlider);
        }
    }
    string SetAlphaButtonToNumber(string str)
    {
        if (str == "Alpha1")
        {
            str = "1";
        }
        else if (str == "Alpha2")
        {
            str = "2";
        }
        else if (str == "Alpha3")
        {
            str = "3";
        }
        else if (str == "Alpha4")
        {
            str = "4";
        }
        else if (str == "Alpha5")
        {
            str = "5";
        }
        else if (str == "Alpha6")
        {
            str = "6";
        }
        else if (str == "Alpha7")
        {
            str = "7";
        }
        else if (str == "Alpha8")
        {
            str = "8";
        }
        else if (str == "Alpha9")
        {
            str = "9";
        }
        else if (str == "Alpha0")
        {
            str = "0";
        }
        return str;
    }
    void SetAdvanceVOnButton(int buttonNum, int state)
    {
        if (state == 1)
        {
            if (buttonNumber == 1)
            {
                if (antiAliasingTempIndex < AntiAlasingStr.Length - 1)
                {
                    antiAliasingTempIndex++;
                }
                else
                    antiAliasingTempIndex = 0;
            }
            if (buttonNumber == 2)
            {
                if (vSyncTempIndex < VSyncStr.Length - 1)
                {
                    vSyncTempIndex++;
                }
                else
                    vSyncTempIndex = 0;
            }
            if (buttonNumber == 3)
            {
                if (shadowTempIndex < ShadowStr.Length - 1)
                {
                    shadowTempIndex++;
                }
                else
                    shadowTempIndex = 0;
            }

            if (buttonNumber == 4)
            {
                if (motionBlurTempIndex < VSyncStr.Length - 1)
                {
                    motionBlurTempIndex++;
                }
                else
                    motionBlurTempIndex = 0;
            }
            if (buttonNumber == 5)
            {
                if (dOFTempIndex < VSyncStr.Length - 1)
                {
                    dOFTempIndex++;
                }
                else
                    dOFTempIndex = 0;
            }
            if (buttonNumber == 6)
            {
                if (bloomTempIndex < VSyncStr.Length - 1)
                {
                    bloomTempIndex++;
                }
                else
                    bloomTempIndex = 0;
            }
            if (buttonNumber == 7)
            {
                if (sSAOTempIndex < VSyncStr.Length - 1)
                {
                    sSAOTempIndex++;
                }
                else
                    sSAOTempIndex = 0;
            }
            if (buttonNumber == 8)
            {
                if (textureQualityTempIndex < TextureQualityStr.Length - 1)
                {
                    textureQualityTempIndex++;
                }
                else
                    textureQualityTempIndex = 0;
            }
            if (buttonNumber == 9)
            {
                if (anisotropicTextureTempIndex < AnisotropicTextureStr.Length - 1)
                {
                    anisotropicTextureTempIndex++;
                }
                else
                    anisotropicTextureTempIndex = 0;
            }
        }
        if (state == 2)
        {
            if (buttonNumber == 1)
            {
                if (antiAliasingTempIndex > 0)
                {

                    antiAliasingTempIndex--;
                }
                else
                    antiAliasingTempIndex = AntiAlasingStr.Length - 1;
            }
            if (buttonNumber == 2)
            {
                if (vSyncTempIndex > 0)
                {

                    vSyncTempIndex--;
                }
                else
                    vSyncTempIndex = VSyncStr.Length - 1;
            }
            if (buttonNumber == 3)
            {
                if (shadowTempIndex > 0)
                {

                    shadowTempIndex--;
                }
                else
                    shadowTempIndex = ShadowStr.Length - 1;
            }

            if (buttonNumber == 4)
            {
                if (motionBlurTempIndex > 0)
                {
                    motionBlurTempIndex--;
                }
                else
                    motionBlurTempIndex = VSyncStr.Length - 1;

            }
            if (buttonNumber == 5)
            {
                if (dOFTempIndex > 0)
                {
                    dOFTempIndex--;
                }
                else
                    dOFTempIndex = VSyncStr.Length - 1;

            }
            if (buttonNumber == 6)
            {
                if (bloomTempIndex > 0)
                {
                    bloomTempIndex--;
                }
                else
                    bloomTempIndex = VSyncStr.Length - 1;

            }
            if (buttonNumber == 7)
            {
                if (sSAOTempIndex > 0)
                {
                    sSAOTempIndex--;
                }
                else
                    sSAOTempIndex = VSyncStr.Length - 1;
            }
            if (buttonNumber == 8)
            {
                if (textureQualityTempIndex > 0)
                {
                    textureQualityTempIndex--;
                }
                else
                    textureQualityTempIndex = TextureQualityStr.Length - 1;
            }
            if (buttonNumber == 9)
            {
                if (anisotropicTextureTempIndex > 0)
                {
                    anisotropicTextureTempIndex--;
                }
                else
                    anisotropicTextureTempIndex = AnisotropicTextureStr.Length - 1;
            }
        }
    }
    void ApplyAdvanceV()
    {
        videoSettingIndex = videoSettingTempIndex;
        
        if (videoSettingTempIndex == 0)
        {
            //shadowTempIndex=;
            //textureQualityTempIndex=;
            // anisotropicTextureTempIndex=;
            // antiAliasingTempIndex=;

            // vSyncTempIndex=;
            // bloomTempIndex=;
            //dOFTempIndex=;
            // motionBlurTempIndex=;
            // sSAOTempIndex=;
        }

        if (videoSettingTempIndex == 1)
        {
            //shadowTempIndex=;
            //textureQualityTempIndex=;
            // anisotropicTextureTempIndex=;
            // antiAliasingTempIndex=;

            // vSyncTempIndex=;
            // bloomTempIndex=;
            //dOFTempIndex=;
            // motionBlurTempIndex=;
            // sSAOTempIndex=;
        }

        if (videoSettingTempIndex == 2)
        {
            //shadowTempIndex=;
            //textureQualityTempIndex=;
            // anisotropicTextureTempIndex=;
            // antiAliasingTempIndex=;

            // vSyncTempIndex=;
            // bloomTempIndex=;
            //dOFTempIndex=;
            // motionBlurTempIndex=;
            // sSAOTempIndex=;
        }


        //shadowIndex = shadowTempIndex;
        //textureQualityIndex = textureQualityTempIndex;
        //anisotropicTextureIndex = anisotropicTextureTempIndex;
        //antiAliasingIndex = antiAliasingTempIndex;
        //vSyncIndex = vSyncTempIndex;
        //bloomIndex = bloomTempIndex;
        //dOFIndex = dOFTempIndex;
        //motionBlurIndex = motionBlurTempIndex;
        //sSAOIndex = sSAOTempIndex;

        //AdvanceVideo.Instance.SetShadow(shadowIndex);
        //AdvanceVideo.Instance.SetTextureQuality(textureQualityIndex);
        //AdvanceVideo.Instance.SetAnisotropicTexture(anisotropicTextureIndex);
        //AdvanceVideo.Instance.SetAntiAliasing(antiAliasingIndex);
        //AdvanceVideo.Instance.SetVSync(vSyncIndex);
        //AdvanceVideo.Instance.SetDOf(dOFIndex);
        //AdvanceVideo.Instance.SetBloom(bloomIndex);
        //AdvanceVideo.Instance.SetMotionBlur(motionBlurIndex);
        //AdvanceVideo.Instance.SetSSAO(sSAOIndex);
        
    }
    void SetApplyAdvanceVForSave()
    {
        AdvanceVideoParameter.AnyChangeOnAdvanceVSave = true;
        AdvanceVideoParameter.VideoSettingIndex = videoSettingIndex;
        AdvanceVideoParameter.SSAOIndex = sSAOIndex;
        AdvanceVideoParameter.MotionBlurIndex = motionBlurIndex;
        AdvanceVideoParameter.DOFIndex = dOFIndex;
        AdvanceVideoParameter.BloomIndex = bloomIndex;
        AdvanceVideoParameter.VSYNCIndex = vSyncIndex;
        AdvanceVideoParameter.AntiAlisingIndex = antiAliasingIndex;
        AdvanceVideoParameter.AnisoTropicTextureIndex = anisotropicTextureIndex;
        AdvanceVideoParameter.TextureQualityIndex = textureQualityIndex;
        AdvanceVideoParameter.ShadowIndex = shadowIndex;

    }
    void DontApplyAdvanceV()
    {

        videoSettingIndex = AdvanceVideoParameter.VideoSettingIndex;
        sSAOIndex = AdvanceVideoParameter.SSAOIndex;
        motionBlurIndex = AdvanceVideoParameter.MotionBlurIndex;
        dOFIndex = AdvanceVideoParameter.DOFIndex;
        bloomIndex = AdvanceVideoParameter.BloomIndex;
        vSyncIndex = AdvanceVideoParameter.VSYNCIndex;
        antiAliasingIndex = AdvanceVideoParameter.AntiAlisingIndex;
        anisotropicTextureIndex = AdvanceVideoParameter.AnisoTropicTextureIndex;
        textureQualityIndex = AdvanceVideoParameter.TextureQualityIndex;
        shadowIndex = AdvanceVideoParameter.ShadowIndex;
        videoSettingTempIndex = videoSettingIndex;
        shadowTempIndex = shadowIndex;
        textureQualityTempIndex = textureQualityIndex;
        anisotropicTextureTempIndex = anisotropicTextureIndex;
        antiAliasingTempIndex = antiAliasingIndex;

        vSyncTempIndex = vSyncIndex;
        bloomTempIndex = bloomIndex;
        dOFTempIndex = dOFIndex;
        motionBlurTempIndex = motionBlurIndex;
        sSAOTempIndex = sSAOIndex;
    }
    string MakingFullStrToShow(DoubleStr current)
    {
        string MainStr = "";
        if (current.HavingDoubleStr)
        {
            MainStr = current.FirstStr + " OR " + current.SecondStr;

        }
        else
        {
            MainStr = current.FirstStr;
        }
        return MainStr;
    }
    string MakingNewStrAfterInput(DoubleStr current, string str)
    {
        string NewStr = "";
        if (str != current.FirstStr && str != current.SecondStr)
        {
            CheckforExistName(str, actionStr, actionDoubleStr);
            CheckforExistName(str, movementStr, movementDoubleStr);
        }
        if (current.HavingDoubleStr == false && str != current.FirstStr)
        {
            if (current.FirstStr != "Unbound")
            {
                current.SecondStr = current.FirstStr;
                current.FirstStr = str;
                current.HavingDoubleStr = true;
            }
            else
            {
                current.FirstStr = str;
                current.HavingDoubleStr = false;
            }
        }
        else
        {

            current.FirstStr = str;
            current.SecondStr = "";
            current.HavingDoubleStr = false;
        }

        NewStr = MakingFullStrToShow(current);
        return NewStr;
    }
    public void StartInGameMenu()
    {
        pause = true;

        BlurEffect currentBlurEffect = Camera.mainCamera.GetComponent<BlurEffect>();
        currentBlurEffect.enabled = true;
        oldBlurSpread = currentBlurEffect.blurSpread;
        oldBlurIteration = currentBlurEffect.iterations;
        currentBlurEffect.blurSpread = MenuBlurSpread;
        currentBlurEffect.iterations = MenuBlurIteration;
        BlurEffect currentFPSBlurEffect = Camera.allCameras[1].GetComponent<BlurEffect>();
        currentFPSBlurEffect.enabled = true;
        oldBlurSpreadFPS = currentFPSBlurEffect.blurSpread;
        oldBlurIterationFPS = currentFPSBlurEffect.iterations;
        currentFPSBlurEffect.blurSpread = MenuBlurSpread;
        currentFPSBlurEffect.iterations = MenuBlurIteration;
    }
    public void StopInGameMenu(bool _isEscapeKeyUsed)
    {
        GameController.UnPauseGame(_isEscapeKeyUsed);
        pause = false;
        BlurEffect currentBlurEffect = Camera.mainCamera.GetComponent<BlurEffect>();
        currentBlurEffect.enabled = false;
        currentBlurEffect.blurSpread = oldBlurSpread;
        currentBlurEffect.iterations = oldBlurIteration;
        BlurEffect currentFPSBlurEffect = Camera.allCameras[1].GetComponent<BlurEffect>();
        currentFPSBlurEffect.enabled = false;
        currentFPSBlurEffect.blurSpread = oldBlurSpreadFPS;
        currentFPSBlurEffect.iterations = oldBlurIteration;

        isEscapeKeyUp = false;
    }
    void SetInitialParametr()
    {
        //voice
        musicVolumeOldSlider = musicVolumeSlider;
        musicVolumeSetslide = musicVolumeSlider;

        generalVolumeOldSlider = generalVolumeSlider;
        generalVolumeSetslide = generalVolumeSlider;

        sFXVolumeSetslide = sFXVolumeSlider;
        sFXVolumeOldSlider = sFXVolumeSlider;

        voiceVolumeOldSlider = voiceVolumeSlider;
        voiceVolumeSetslide = voiceVolumeSlider;

        speakerConfigOldIndex = speakerConfigIndex;

        //

        // video
        brightnessOldSlider = brightnessSlider;
        brightnessSetslide = brightnessSlider;

        resolutionTempIndex = resolutionIndex;
        //

        // mouse
        mouseOldSensivity = mouseSensivity;
        invertMouseOldCount = invertMouseCount;
        //

        //subtitle
        subTitleOldIndex = subTitleIndex;
        //

        //advanceVideo
        videoSettingTempIndex = videoSettingIndex;

        sSAOTempIndex = sSAOIndex;
        motionBlurTempIndex = motionBlurIndex;
        dOFTempIndex = dOFIndex;
        bloomTempIndex = bloomIndex;
        vSyncTempIndex = vSyncIndex;
        antiAliasingTempIndex = antiAliasingIndex;
        anisotropicTextureTempIndex = anisotropicTextureIndex;
        textureQualityTempIndex = textureQualityIndex;
        shadowTempIndex = shadowIndex;
        //

    }

}

