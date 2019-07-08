using UnityEngine;
using System.Collections;

public enum HUDControlName
{
    _noName,
    GunShape_Springfield,
    GunShape_MP18,
    GunShape_Luger,
    GunShape_Snipe,
    GunShape_Winchester,
    GunBullet_Springfield,
    GunBullet_MP18,
    GunBullet_Luger,
    GunBullet_Snipe,
    GunBullet_Winchester,
    AmmoCount_Left,
    AmmoCount_Mid,
    AmmoCount_Right,
    GrenadeShape,
    SitStand,
    Text_YouGetDamagedTakeCover,
    Text_Reload_NoAmmo,
    LevelObjective01,
    LevelObjective02,
    LevelObjective03,
    LevelObjective04,
    LevelObjective05,
    LevelObjective06,
    LevelObjective07,
    LevelObjective08,
    LevelObjective09,
    LevelObjective10,
    ObjectivesPageBG,
    ObjectivesPageBGText,
    GameSaved,
    CurrentMission,
    MissionComplete,
    MainMission,
    ObjectivePageBGMainObjective,
    PressActKeyToGrabGun,
    PressActKeyToRefillAmmo,
    PressActKeyToDoAct,
    GrabbableGun,
    AmmoPickInfo,
    SnipeHint_PressShiftToHoldBreath,
    SnipeHint_ClickMouseMidToStartFocusMode,
    SnipeHint_TimeScaleBar,
    SnipeHint_TimeScaleBar_Disabled,
    SnipeHint_TimeScaleBar_Limit,
    SnipeHint_TimeScaleBar_Icon,
    SnipeHint_ClickMouseMidToCancelFocusMode,
    MissionFail_YouDied,
    MissionFail_AlliesNotSupported,
    MissionFail_BombNotPlanted,
    MissionFail_YouLeftFightArea,
    MissionFail_YouAreDetectedByEnemies,
    DamageSide,
    GrenadeIcon,
    GrenadeIconFelesh,
    the3DObjectiveFelesh,
    SneakingHint_View,
    SneakingHint_SitStand,
    SneakingHint_MoveSpeed,
    SneakingHint_Grass,
    SneakingHint_Sound,
    SneakingHint_EffectEnemies,
    MissionFail_YouLeftAreaWithoutPlantingDynamites,
    LvlCamp_ClockBG,
    LvlCamp_ClockBGRed,
    LvlCamp_ClockHandle,
    MissionFail_DynamteHasBeenExplodedBeforeCommunicationBreakdown,
    LvlFlashback_FA_Logo,
    SneakingHint_Light,
    LvlFlashback_FA_PejvakStudio,
    LvlFlashback_FA_Presents,
    MissionFail_EnemyHeardYourFire,
    MissionFail_EnemySawHisMateNash,
    LvlBushehr02Hint_Shooting,
    LvlBushehr02Hint_SnipeTimeScale,
    BlackBG,
    LvlBushehr02_TheEndPic01,
    LvlBushehr02_TheEndPic02,
    LvlBushehr02_TheEndPic03,
    LvlBushehr02_TheEndPic04,
    LvlBushehr02_TheEndPic05,
    LvlBushehr02_TheEndPic06,
    LvlBushehr02_TheEndPic07,
    LvlBushehr02_TheEndPic08,
    LvlBushehr02_TheEndPic09,
    LvlBushehr02_TheEndPic10,
    SnipeFocusKey1,
    SnipeFocusKey2,
    SnipeBreath1,
    SnipeBreath2,
    SnipeCancelFocusKey1,
    SnipeCancelFocusKey2,
}

public enum HUDOutStep
{
    NotStarted,
    Starting,
    RunningA,
    RunningB,
    Finishing,
    Finished,
}

public enum HUDAlphaStat
{
    Mid,
    Full,
    Increasing,
    Decreasing,
    Zero,
}

public enum ShowForAWhileStep
{
    Idle,
    FirstAlpha,
    Showing,
    EndAlpha,
}

public class HUDControl : MonoBehaviour
{
    [HideInInspector]
    public bool isControlVisible = false;

    public HUDControlName controlName = HUDControlName._noName;

    public MenuRectLayoutX xLayout = MenuRectLayoutX.Center;

    public MenuRectLayoutY yLayout = MenuRectLayoutY.Center;

    public float x = 0;

    public float y = 0;

    public float w = 0;

    public float h = 0;

    public Texture2D[] textures;

    public int selectedTextureIndex = 0;

    public float[] additionalNums;

    ShowForAWhileStep showForAWhile_Step = ShowForAWhileStep.Idle;
    float showForAWhile_StartingAlphaSpeed = 0;
    float showForAWhile_EndAlphaSpeed = 0;
    float showForAWhile_DurationCounter = 0;

    //

    [HideInInspector]
    public MenuRect rect;

    [HideInInspector]
    public float alpha = 1;

    [HideInInspector]
    public HUDAlphaStat alphaStatus = HUDAlphaStat.Full;

    [HideInInspector]
    public float alphaChangeSpeed = 1;

    [HideInInspector]
    public HUDOutStep outStep = HUDOutStep.NotStarted;

    [HideInInspector]
    public float xCoef = 1;

    [HideInInspector]
    public float yCoef = 1;

    [HideInInspector]
    public float wCoef = 1;

    [HideInInspector]
    public float hCoef = 1;

    [HideInInspector]
    public float additionalX = 0;

    [HideInInspector]
    public float additionalY = 0;

    [HideInInspector]
    public float additionalW = 0;

    [HideInInspector]
    public float additionalH = 0;

    [HideInInspector]
    public float outCounter = 0;

    float scale = 1;

    //

    void Update()
    {
        #region Show For A While
        if (showForAWhile_Step == ShowForAWhileStep.FirstAlpha)
        {
            StartIncreasingAlpha(showForAWhile_StartingAlphaSpeed);
            showForAWhile_Step = ShowForAWhileStep.Showing;
        }

        if (showForAWhile_Step == ShowForAWhileStep.Showing)
        {
            showForAWhile_DurationCounter = MathfPlus.DecByDeltatimeToZero(showForAWhile_DurationCounter);

            if (showForAWhile_DurationCounter == 0)
            {
                StartDecreasingAlpha(showForAWhile_EndAlphaSpeed);
                showForAWhile_Step = ShowForAWhileStep.EndAlpha;
            }
        }

        if (showForAWhile_Step == ShowForAWhileStep.EndAlpha)
        {
            if (alpha == 0)
            {
                showForAWhile_Step = ShowForAWhileStep.Idle;
            }
        }
        #endregion

        #region Alpha
        if (alphaStatus == HUDAlphaStat.Increasing)
        {
            SetAlpha(alpha + alphaChangeSpeed * Time.deltaTime);

            if (alphaStatus == HUDAlphaStat.Mid)
                alphaStatus = HUDAlphaStat.Increasing;
        }

        if (alphaStatus == HUDAlphaStat.Decreasing)
        {
            SetAlpha(alpha - alphaChangeSpeed * Time.deltaTime);

            if (alphaStatus == HUDAlphaStat.Mid)
                alphaStatus = HUDAlphaStat.Decreasing;
        }
        #endregion
    }

    //

    public void ReInitScale(float _scale)
    {
        scale = _scale;
        ReInitRect();
    }

    public void ReInitRect()
    {
        rect = new MenuRect();
        rect.SetScale(scale);
        rect.SetLayout_X(xLayout);
        rect.SetLayout_Y(yLayout);
        rect.SetDefaultModeValues(x * xCoef + additionalX, y * yCoef + additionalY, w * wCoef + additionalW, h * hCoef + additionalH);
    }

    public void SetIsVisible(bool _value)
    {
        isControlVisible = _value;
    }

    public void SetSelectedTextureIndex(int _index)
    {
        selectedTextureIndex = _index;
    }


    public void StartDecreasingAlpha(float _speed)
    {
        alphaChangeSpeed = _speed;

        alphaStatus = HUDAlphaStat.Decreasing;
    }

    public void StartIncreasingAlpha(float _speed)
    {
        alphaChangeSpeed = _speed;

        alphaStatus = HUDAlphaStat.Increasing;
    }

    public void SetAlpha(float _value)
    {
        alpha = _value;

        alpha = Mathf.Clamp01(alpha);

        if (alpha == 0)
        {
            alphaStatus = HUDAlphaStat.Zero;
        }
        else
        {
            if (alpha == 1)
            {
                alphaStatus = HUDAlphaStat.Full;
            }
            else
            {
                alphaStatus = HUDAlphaStat.Mid;
            }
        }
    }

    public void ShowForAWhile(float _duration, float _startAlphaSpeed, float _endAlphaSpeed)
    {
        showForAWhile_Step = ShowForAWhileStep.FirstAlpha;
        showForAWhile_StartingAlphaSpeed = _startAlphaSpeed;
        showForAWhile_EndAlphaSpeed = _endAlphaSpeed;
        showForAWhile_DurationCounter = _duration;
    }


    public void SetOutStep(HUDOutStep _outStep)
    {
        outStep = _outStep;
    }

    public bool IsOutStep(HUDOutStep _outStep)
    {
        return outStep == _outStep;
    }


    public void SetOutCounter(float _val)
    {
        outCounter = _val;
    }

    public void IncOutCounterByTime()
    {
        SetOutCounter(outCounter + Time.deltaTime);
    }

    public void DecOutCounterByTime()
    {
        SetOutCounter(outCounter - Time.deltaTime);
    }

    public void DecOutCounterByTime_ToZero()
    {
        float oc = outCounter - Time.deltaTime;

        oc = Mathf.Clamp(oc, 0, float.MaxValue);

        SetOutCounter(oc);
    }
}
