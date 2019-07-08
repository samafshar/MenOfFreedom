using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum VideoSettingTypes
{
    Low,
    Medium,
    High,
    VeryHigh,
    Ultra,
    Custom,
}

public enum ShadowQual
{
    low,
    medium,
    high,
}

public enum TextureQual
{
    quarter,
    half,
    full,
}

public static class VideoSettingsController
{
    public static float default_LODBias = 4f;

    public static VideoSettingTypes default_Low_VideoSettingType = VideoSettingTypes.Low;
    public static bool default_Low_ShadowOn = false;
    public static ShadowQual default_Low_ShadowQuality = ShadowQual.medium;
    public static float default_Low_ShadowDistance = 40;
    public static TextureQual default_Low_TextureQual = TextureQual.quarter;
    public static bool default_Low_UseSSAO = false;
    public static bool default_Low_UseAnisotropic = false;
    public static bool default_Low_UseBloom = false;

    public static VideoSettingTypes default_Medium_VideoSettingType = VideoSettingTypes.Medium;
    public static bool default_Medium_ShadowOn = false;
    public static ShadowQual default_Medium_ShadowQuality = ShadowQual.medium;
    public static float default_Medium_ShadowDistance = 40;
    public static TextureQual default_Medium_TextureQual = TextureQual.half;
    public static bool default_Medium_UseSSAO = false;
    public static bool default_Medium_UseAnisotropic = true;
    public static bool default_Medium_UseBloom = false;

    public static VideoSettingTypes default_High_VideoSettingType = VideoSettingTypes.High;
    public static bool default_High_ShadowOn = false;
    public static ShadowQual default_High_ShadowQuality = ShadowQual.medium;
    public static float default_High_ShadowDistance = 40;
    public static TextureQual default_High_TextureQual = TextureQual.full;
    public static bool default_High_UseSSAO = false;
    public static bool default_High_UseAnisotropic = true;
    public static bool default_High_UseBloom = false;

    public static VideoSettingTypes default_VeryHigh_VideoSettingType = VideoSettingTypes.VeryHigh;
    public static bool default_VeryHigh_ShadowOn = true;
    public static ShadowQual default_VeryHigh_ShadowQuality = ShadowQual.medium;
    public static float default_VeryHigh_ShadowDistance = 40;
    public static TextureQual default_VeryHigh_TextureQual = TextureQual.full;
    public static bool default_VeryHigh_UseSSAO = false;
    public static bool default_VeryHigh_UseAnisotropic = true;
    public static bool default_VeryHigh_UseBloom = true;

    public static VideoSettingTypes default_Ultra_VideoSettingType = VideoSettingTypes.Ultra;
    public static bool default_Ultra_ShadowOn = true;
    public static ShadowQual default_Ultra_ShadowQuality = ShadowQual.high;
    public static float default_Ultra_ShadowDistance = 60;
    public static TextureQual default_Ultra_TextureQual = TextureQual.full;
    public static bool default_Ultra_UseSSAO = true;
    public static bool default_Ultra_UseAnisotropic = true;
    public static bool default_Ultra_UseBloom = true;

    public static VideoSettingTypes curVideoSettingType;

    public static int curResolutionIndex = 0;

    public static int curUnityQualManagerIndex = 0;

    public static bool curIsShadowOn = false;

    public static ShadowQual curShadowQual = ShadowQual.low;

    public static TextureQual curTextureQual = TextureQual.full;

    public static bool curIsVSyncOn = false;

    public static float curShadowDistance = 50f;

    public static float curBrightness = 0;

    public static bool curUseSSAO = false;

    public static bool curUseAnisotropic = false;

    public static float curLODBias = 3f;

    public static bool curUseBloom = false;

    public static bool gameOneTimeResolutionIsSet = false;

    //

    public static void Init()
    {
        InitDefaultVideoSetting(false);
    }

    public static void InitDefaultVideoSetting(bool _changeResolutionRightNow)
    {
        SetScreenResolutionToHighest(_changeResolutionRightNow);

        Unapplied_SetIsVSyncOn(false);
        SetBightness(0);
        SetLODBias(default_LODBias);

        SetVideoSettingType(VideoSettingTypes.High);
        Unapplied_SetIsShadowOn(default_High_ShadowOn);
        Unapplied_SetShadowQuality(default_High_ShadowQuality);
        //SetShadowDistance(default_High_ShadowDistance);
        Unapplied_SetTextureQuality(default_High_TextureQual);
        SetUseSSAO(default_High_UseSSAO);
        SetUseAnisotropic(default_High_UseAnisotropic);
        SetUseBloom(default_High_UseBloom);

        ApplyRelativeAppliableSettings();
    }

    public static void SetScreenResolutionToHighest(bool _changeResolutionRightNow)
    {
        SetResolution(Screen.resolutions.Length - 1, _changeResolutionRightNow);
    }

    public static void SetResolution(int _resolutionIndex, bool _changeResolutionRightNow)
    {
        bool changeResolutionRightNow = _changeResolutionRightNow;

        curResolutionIndex = _resolutionIndex;

        if (curResolutionIndex > Screen.resolutions.Length - 1)
            curResolutionIndex = Screen.resolutions.Length - 1;

        if (changeResolutionRightNow)
        {
            if ((Screen.currentResolution.width != Screen.resolutions[curResolutionIndex].width) || (Screen.currentResolution.height != Screen.resolutions[curResolutionIndex].height))
                Screen.SetResolution(Screen.resolutions[curResolutionIndex].width, Screen.resolutions[curResolutionIndex].height, true);
        }
    }

    //

    static void ReSetCurUnityQualManagerIndex()
    {
        if (curTextureQual == TextureQual.quarter)
        {
            #region Shadow
            if (!curIsShadowOn)
            {
                #region VSync
                if (!curIsVSyncOn)
                {
                    curUnityQualManagerIndex = 0;
                }
                else
                {
                    curUnityQualManagerIndex = 1;
                }
                #endregion
            }
            else
            {
                #region Low Shadow
                if (curShadowQual == ShadowQual.low)
                {
                    #region VSync
                    if (!curIsVSyncOn)
                    {
                        curUnityQualManagerIndex = 2;
                    }
                    else
                    {
                        curUnityQualManagerIndex = 3;
                    }
                    #endregion
                }
                #endregion

                #region Medium Shadow
                if (curShadowQual == ShadowQual.medium)
                {
                    #region VSync
                    if (!curIsVSyncOn)
                    {
                        curUnityQualManagerIndex = 4;
                    }
                    else
                    {
                        curUnityQualManagerIndex = 5;
                    }
                    #endregion
                }
                #endregion

                #region High Shadow
                if (curShadowQual == ShadowQual.high)
                {
                    #region VSync
                    if (!curIsVSyncOn)
                    {
                        curUnityQualManagerIndex = 6;
                    }
                    else
                    {
                        curUnityQualManagerIndex = 7;
                    }
                    #endregion
                }
                #endregion
            }
            #endregion
        }

        if (curTextureQual == TextureQual.half)
        {
            #region Shadow
            if (!curIsShadowOn)
            {
                #region VSync
                if (!curIsVSyncOn)
                {
                    curUnityQualManagerIndex = 8;
                }
                else
                {
                    curUnityQualManagerIndex = 9;
                }
                #endregion
            }
            else
            {
                #region Low Shadow
                if (curShadowQual == ShadowQual.low)
                {
                    #region VSync
                    if (!curIsVSyncOn)
                    {
                        curUnityQualManagerIndex = 10;
                    }
                    else
                    {
                        curUnityQualManagerIndex = 11;
                    }
                    #endregion
                }
                #endregion

                #region Medium Shadow
                if (curShadowQual == ShadowQual.medium)
                {
                    #region VSync
                    if (!curIsVSyncOn)
                    {
                        curUnityQualManagerIndex = 12;
                    }
                    else
                    {
                        curUnityQualManagerIndex = 13;
                    }
                    #endregion
                }
                #endregion

                #region High Shadow
                if (curShadowQual == ShadowQual.high)
                {
                    #region VSync
                    if (!curIsVSyncOn)
                    {
                        curUnityQualManagerIndex = 14;
                    }
                    else
                    {
                        curUnityQualManagerIndex = 15;
                    }
                    #endregion
                }
                #endregion
            }
            #endregion
        }

        if (curTextureQual == TextureQual.full)
        {
            #region Shadow
            if (!curIsShadowOn)
            {
                #region VSync
                if (!curIsVSyncOn)
                {
                    curUnityQualManagerIndex = 16;
                }
                else
                {
                    curUnityQualManagerIndex = 17;
                }
                #endregion
            }
            else
            {
                #region Low Shadow
                if (curShadowQual == ShadowQual.low)
                {
                    #region VSync
                    if (!curIsVSyncOn)
                    {
                        curUnityQualManagerIndex = 18;
                    }
                    else
                    {
                        curUnityQualManagerIndex = 19;
                    }
                    #endregion
                }
                #endregion

                #region Medium Shadow
                if (curShadowQual == ShadowQual.medium)
                {
                    #region VSync
                    if (!curIsVSyncOn)
                    {
                        curUnityQualManagerIndex = 20;
                    }
                    else
                    {
                        curUnityQualManagerIndex = 21;
                    }
                    #endregion
                }
                #endregion

                #region High Shadow
                if (curShadowQual == ShadowQual.high)
                {
                    #region VSync
                    if (!curIsVSyncOn)
                    {
                        curUnityQualManagerIndex = 22;
                    }
                    else
                    {
                        curUnityQualManagerIndex = 23;
                    }
                    #endregion
                }
                #endregion
            }
            #endregion
        }

        if (curUnityQualManagerIndex != QualitySettings.GetQualityLevel())
            QualitySettings.SetQualityLevel(curUnityQualManagerIndex, true);
    }

    public static void Unapplied_SetIsVSyncOn(bool _val)
    {
        curIsVSyncOn = _val;
    }

    public static void Unapplied_SetIsShadowOn(bool _val)
    {
        curIsShadowOn = _val;
    }

    public static void Unapplied_SetShadowQuality(ShadowQual _qual)
    {
        curShadowQual = _qual;
    }

    public static void Unapplied_SetTextureQuality(TextureQual _qual)
    {
        curTextureQual = _qual;
    }

    //

    public static void ApplyRelativeAppliableSettings()
    {
        ReSetCurUnityQualManagerIndex();
    }

    //

    public static void SetShadowDistance(float _val)
    {
        curShadowDistance = _val;

        if (QualitySettings.shadowDistance != curShadowDistance)
            QualitySettings.shadowDistance = curShadowDistance;
    }

    public static void SetBightness(float _val)
    {
        curBrightness = _val;
    }

    public static void SetUseSSAO(bool _val)
    {
        curUseSSAO = _val;
    }

    public static void SetUseAnisotropic(bool _val)
    {
        curUseAnisotropic = _val;

        if (curUseAnisotropic)
        {
            if (QualitySettings.anisotropicFiltering != AnisotropicFiltering.ForceEnable)
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
        }
        else
        {
            if (QualitySettings.anisotropicFiltering != AnisotropicFiltering.Disable)
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
        }
    }

    public static void SetLODBias(float _val)
    {
        curLODBias = _val;

        if (QualitySettings.lodBias != curLODBias)
            QualitySettings.lodBias = curLODBias;
    }

    public static void SetUseBloom(bool _val)
    {
        curUseBloom = _val;
    }

    public static void SetVideoSettingType(VideoSettingTypes _type)
    {
        curVideoSettingType = _type;
    }

    // public static void Try
}
