using UnityEngine;
using System;
using System.Text;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

// === This is the info container class ===
[Serializable()]
public class GameStateData : ISerializable
{
    public int gameLastLevel = -444;
    public int gameCurrentLevelLastCheckPoint = -222;
    public int gameCurrentLevel = -222;

    public GameStateData() { }

    public GameStateData(SerializationInfo info, StreamingContext ctxt)
    {
        gameLastLevel = (int)info.GetValue("gameLastLevel", typeof(int));
        gameCurrentLevelLastCheckPoint = (int)info.GetValue("gameLastCheckPoint", typeof(int));
        gameCurrentLevel = (int)info.GetValue("gameCurrentLevel", typeof(int));
    }

    public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
    {
        info.AddValue("gameLastLevel", (gameLastLevel));
        info.AddValue("gameLastCheckPoint", gameCurrentLevelLastCheckPoint);
        info.AddValue("gameCurrentLevel", gameCurrentLevel);
    }
}

[Serializable()]
public class PlayerStateData : ISerializable
{
    public bool lastCheckPoint_IsLvlCampPlayer = false;
    public bool lastCheckPoint_HaveSecGun = true;

    public PlayerGunName lastCheckPoint_PrimGun;
    public int lastCheckPoint_PrimGunCurBulletCount;
    public int lastCheckPoint_PrimGunCurMagCount;

    public PlayerGunName lastCheckPoint_SecGun;
    public int lastCheckPoint_SecGunCurBulletCount;
    public int lastCheckPoint_SecGunCurMagCount;

    public int lastCheckPoint_CurGrenadeCount;

    public PlayerStateData() { }

    public PlayerStateData(SerializationInfo info, StreamingContext ctxt)
    {
        lastCheckPoint_IsLvlCampPlayer = (bool)info.GetValue("lastCheckPoint_IsLvlCampPlayer", typeof(bool));
        lastCheckPoint_HaveSecGun = (bool)info.GetValue("lastCheckPoint_HaveSecGun", typeof(bool));

        lastCheckPoint_PrimGun = (PlayerGunName)info.GetValue("lastCheckPoint_PrimGun", typeof(PlayerGunName));
        lastCheckPoint_PrimGunCurBulletCount = (int)info.GetValue("lastCheckPoint_PrimGunCurBulletCount", typeof(int));
        lastCheckPoint_PrimGunCurMagCount = (int)info.GetValue("lastCheckPoint_PrimGunCurMagCount", typeof(int));

        lastCheckPoint_SecGun = (PlayerGunName)info.GetValue("lastCheckPoint_SecGun", typeof(PlayerGunName));
        lastCheckPoint_SecGunCurBulletCount = (int)info.GetValue("lastCheckPoint_SecGunCurBulletCount", typeof(int));
        lastCheckPoint_SecGunCurMagCount = (int)info.GetValue("lastCheckPoint_SecGunCurMagCount", typeof(int));

        lastCheckPoint_CurGrenadeCount = (int)info.GetValue("lastCheckPoint_CurGrenadeCount", typeof(int));
    }

    public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
    {
        info.AddValue("lastCheckPoint_IsLvlCampPlayer", (lastCheckPoint_IsLvlCampPlayer));
        info.AddValue("lastCheckPoint_HaveSecGun", (lastCheckPoint_HaveSecGun));

        info.AddValue("lastCheckPoint_PrimGun", (lastCheckPoint_PrimGun));
        info.AddValue("lastCheckPoint_PrimGunCurBulletCount", (lastCheckPoint_PrimGunCurBulletCount));
        info.AddValue("lastCheckPoint_PrimGunCurMagCount", (lastCheckPoint_PrimGunCurMagCount));

        info.AddValue("lastCheckPoint_SecGun", (lastCheckPoint_SecGun));
        info.AddValue("lastCheckPoint_SecGunCurBulletCount", (lastCheckPoint_SecGunCurBulletCount));
        info.AddValue("lastCheckPoint_SecGunCurMagCount", (lastCheckPoint_SecGunCurMagCount));

        info.AddValue("lastCheckPoint_CurGrenadeCount", (lastCheckPoint_CurGrenadeCount));
    }
}

[Serializable()]
public class OptionsData : ISerializable
{
    public float controls_SensitivityX = 0;
    public float controls_SensitivityY = 0;
    public bool controls_InvertMouse = false;
    public bool controls_UseMouseWheelToChangeWeapon = true;

    public KeyCode controls_MoveLeft_Primary = KeyCode.A;
    public KeyCode controls_MoveLeft_Secondary = KeyCode.A;

    public KeyCode controls_MoveRight_Primary = KeyCode.A;
    public KeyCode controls_MoveRight_Secondary = KeyCode.A;

    public KeyCode controls_MoveForward_Primary = KeyCode.A;
    public KeyCode controls_MoveForward_Secondary = KeyCode.A;

    public KeyCode controls_MoveBackward_Primary = KeyCode.A;
    public KeyCode controls_MoveBackward_Secondary = KeyCode.A;

    public KeyCode controls_Jump_Primary = KeyCode.A;
    public KeyCode controls_Jump_Secondary = KeyCode.A;

    public KeyCode controls_Sprint_SnipeSteady_Primary = KeyCode.A;
    public KeyCode controls_Sprint_SnipeSteady_Secondary = KeyCode.A;

    public KeyCode controls_Crouch_Primary = KeyCode.A;
    public KeyCode controls_Crouch_Secondary = KeyCode.A;

    public KeyCode controls_Action_Primary = KeyCode.A;
    public KeyCode controls_Action_Secondary = KeyCode.A;

    public KeyCode controls_Fire_Primary = KeyCode.A;
    public KeyCode controls_Fire_Secondary = KeyCode.A;

    public KeyCode controls_Aim_Primary = KeyCode.A;
    public KeyCode controls_Aim_Secondary = KeyCode.A;

    public KeyCode controls_ChangeGun_Primary = KeyCode.A;
    public KeyCode controls_ChangeGun_Secondary = KeyCode.A;

    public KeyCode controls_Grenade_SnipeTimeController_Primary = KeyCode.A;
    public KeyCode controls_Grenade_SnipeTimeController_Secondary = KeyCode.A;

    public KeyCode controls_Melee_Primary = KeyCode.A;
    public KeyCode controls_Melee_Secondary = KeyCode.A;

    public KeyCode controls_Reload_Primary = KeyCode.A;
    public KeyCode controls_Reload_Secondary = KeyCode.A;

    public KeyCode controls_Missions_Primary = KeyCode.A;
    public KeyCode controls_Missions_Secondary = KeyCode.A;

    //

    public float audio_GeneralVolume = 1;
    public float audio_MusicVolume = 1;
    public float audio_SFXVolume = 1;
    public float audio_VoiceVolume = 1;

    //

    public int video_ResolutionIndex = 0;
    public bool video_VSync = false;
    public float video_Brightness = 0;
    public float video_LODBias = 0;
    public VideoSettingTypes video_Type;
    public bool video_IsShadowOn = false;
    public ShadowQual video_ShadowQual;
    public float video_ShadowDistance = 0;
    public TextureQual video_TextureQual;
    public bool video_UseSSAO = false;
    public bool video_UseAnisotropic = false;
    public bool video_UseBloom = false;


    public OptionsData() { }

    public OptionsData(SerializationInfo info, StreamingContext ctxt)
    {
        controls_SensitivityX = (float)info.GetValue("controls_SensitivityX", typeof(float));
        controls_SensitivityY = (float)info.GetValue("controls_SensitivityY", typeof(float));
        controls_InvertMouse = (bool)info.GetValue("controls_InvertMouse", typeof(bool));
        controls_UseMouseWheelToChangeWeapon = (bool)info.GetValue("controls_UseMouseWheelToChangeWeapon", typeof(bool));

        controls_MoveLeft_Primary = (KeyCode)info.GetValue("controls_MoveLeft_Primary", typeof(KeyCode));
        controls_MoveLeft_Secondary = (KeyCode)info.GetValue("controls_MoveLeft_Secondary", typeof(KeyCode));

        controls_MoveRight_Primary = (KeyCode)info.GetValue("controls_MoveRight_Primary", typeof(KeyCode));
        controls_MoveRight_Secondary = (KeyCode)info.GetValue("controls_MoveRight_Secondary", typeof(KeyCode));

        controls_MoveForward_Primary = (KeyCode)info.GetValue("controls_MoveForward_Primary", typeof(KeyCode));
        controls_MoveForward_Secondary = (KeyCode)info.GetValue("controls_MoveForward_Secondary", typeof(KeyCode));

        controls_MoveBackward_Primary = (KeyCode)info.GetValue("controls_MoveBackward_Primary", typeof(KeyCode));
        controls_MoveBackward_Secondary = (KeyCode)info.GetValue("controls_MoveBackward_Secondary", typeof(KeyCode));

        controls_Jump_Primary = (KeyCode)info.GetValue("controls_Jump_Primary", typeof(KeyCode));
        controls_Jump_Secondary = (KeyCode)info.GetValue("controls_Jump_Secondary", typeof(KeyCode));

        controls_Sprint_SnipeSteady_Primary = (KeyCode)info.GetValue("controls_Sprint_SnipeSteady_Primary", typeof(KeyCode));
        controls_Sprint_SnipeSteady_Secondary = (KeyCode)info.GetValue("controls_Sprint_SnipeSteady_Secondary", typeof(KeyCode));

        controls_Crouch_Primary = (KeyCode)info.GetValue("controls_Crouch_Primary", typeof(KeyCode));
        controls_Crouch_Secondary = (KeyCode)info.GetValue("controls_Crouch_Secondary", typeof(KeyCode));

        controls_Action_Primary = (KeyCode)info.GetValue("controls_Action_Primary", typeof(KeyCode));
        controls_Action_Secondary = (KeyCode)info.GetValue("controls_Action_Secondary", typeof(KeyCode));

        controls_Fire_Primary = (KeyCode)info.GetValue("controls_Fire_Primary", typeof(KeyCode));
        controls_Fire_Secondary = (KeyCode)info.GetValue("controls_Fire_Secondary", typeof(KeyCode));

        controls_Aim_Primary = (KeyCode)info.GetValue("controls_Aim_Primary", typeof(KeyCode));
        controls_Aim_Secondary = (KeyCode)info.GetValue("controls_Aim_Secondary", typeof(KeyCode));

        controls_ChangeGun_Primary = (KeyCode)info.GetValue("controls_ChangeGun_Primary", typeof(KeyCode));
        controls_ChangeGun_Secondary = (KeyCode)info.GetValue("controls_ChangeGun_Secondary", typeof(KeyCode));

        controls_Grenade_SnipeTimeController_Primary = (KeyCode)info.GetValue("controls_Grenade_SnipeTimeController_Primary", typeof(KeyCode));
        controls_Grenade_SnipeTimeController_Secondary = (KeyCode)info.GetValue("controls_Grenade_SnipeTimeController_Secondary", typeof(KeyCode));

        controls_Melee_Primary = (KeyCode)info.GetValue("controls_Melee_Primary", typeof(KeyCode));
        controls_Melee_Secondary = (KeyCode)info.GetValue("controls_Melee_Secondary", typeof(KeyCode));

        controls_Reload_Primary = (KeyCode)info.GetValue("controls_Reload_Primary", typeof(KeyCode));
        controls_Reload_Secondary = (KeyCode)info.GetValue("controls_Reload_Secondary", typeof(KeyCode));

        controls_Missions_Primary = (KeyCode)info.GetValue("controls_Missions_Primary", typeof(KeyCode));
        controls_Missions_Secondary = (KeyCode)info.GetValue("controls_Missions_Secondary", typeof(KeyCode));

        //

        audio_GeneralVolume = (float)info.GetValue("audio_GeneralVolume", typeof(float));
        audio_MusicVolume = (float)info.GetValue("audio_MusicVolume", typeof(float));
        audio_SFXVolume = (float)info.GetValue("audio_SFXVolume", typeof(float));
        audio_VoiceVolume = (float)info.GetValue("audio_VoiceVolume", typeof(float));

        //

        video_ResolutionIndex = (int)info.GetValue("video_ResolutionIndex", typeof(int));
        video_VSync = (bool)info.GetValue("video_VSync", typeof(bool));
        video_Brightness = (float)info.GetValue("video_Brightness", typeof(float));
        video_LODBias = (float)info.GetValue("video_LODBias", typeof(float));
        video_Type = (VideoSettingTypes)info.GetValue("video_Type", typeof(VideoSettingTypes));
        video_IsShadowOn = (bool)info.GetValue("video_IsShadowOn", typeof(bool));
        video_ShadowQual = (ShadowQual)info.GetValue("video_ShadowQual", typeof(ShadowQual));
        video_ShadowDistance = (float)info.GetValue("video_ShadowDistance", typeof(float));
        video_TextureQual = (TextureQual)info.GetValue("video_TextureQual", typeof(TextureQual));
        video_UseSSAO = (bool)info.GetValue("video_UseSSAO", typeof(bool));
        video_UseAnisotropic = (bool)info.GetValue("video_UseAnisotropic", typeof(bool));
        video_UseBloom = (bool)info.GetValue("video_UseBloom", typeof(bool));
    }

    public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
    {
        info.AddValue("controls_InvertMouse", (controls_InvertMouse));
        info.AddValue("controls_SensitivityX", (controls_SensitivityX));
        info.AddValue("controls_SensitivityY", (controls_SensitivityY));
        info.AddValue("controls_UseMouseWheelToChangeWeapon", (controls_UseMouseWheelToChangeWeapon));

        info.AddValue("controls_Action_Primary", (controls_Action_Primary));
        info.AddValue("controls_Action_Secondary", (controls_Action_Secondary));

        info.AddValue("controls_Aim_Primary", (controls_Aim_Primary));
        info.AddValue("controls_Aim_Secondary", (controls_Aim_Secondary));

        info.AddValue("controls_ChangeGun_Primary", (controls_ChangeGun_Primary));
        info.AddValue("controls_ChangeGun_Secondary", (controls_ChangeGun_Secondary));

        info.AddValue("controls_Crouch_Primary", (controls_Crouch_Primary));
        info.AddValue("controls_Crouch_Secondary", (controls_Crouch_Secondary));

        info.AddValue("controls_Fire_Primary", (controls_Fire_Primary));
        info.AddValue("controls_Fire_Secondary", (controls_Fire_Secondary));

        info.AddValue("controls_Grenade_SnipeTimeController_Primary", (controls_Grenade_SnipeTimeController_Primary));
        info.AddValue("controls_Grenade_SnipeTimeController_Secondary", (controls_Grenade_SnipeTimeController_Secondary));

        info.AddValue("controls_Jump_Primary", (controls_Jump_Primary));
        info.AddValue("controls_Jump_Secondary", (controls_Jump_Secondary));

        info.AddValue("controls_Melee_Primary", (controls_Melee_Primary));
        info.AddValue("controls_Melee_Secondary", (controls_Melee_Secondary));

        info.AddValue("controls_Missions_Primary", (controls_Missions_Primary));
        info.AddValue("controls_Missions_Secondary", (controls_Missions_Secondary));

        info.AddValue("controls_MoveBackward_Primary", (controls_MoveBackward_Primary));
        info.AddValue("controls_MoveBackward_Secondary", (controls_MoveBackward_Secondary));

        info.AddValue("controls_MoveForward_Primary", (controls_MoveForward_Primary));
        info.AddValue("controls_MoveForward_Secondary", (controls_MoveForward_Secondary));

        info.AddValue("controls_MoveLeft_Primary", (controls_MoveLeft_Primary));
        info.AddValue("controls_MoveLeft_Secondary", (controls_MoveLeft_Secondary));

        info.AddValue("controls_MoveRight_Primary", (controls_MoveRight_Primary));
        info.AddValue("controls_MoveRight_Secondary", (controls_MoveRight_Secondary));

        info.AddValue("controls_Reload_Primary", (controls_Reload_Primary));
        info.AddValue("controls_Reload_Secondary", (controls_Reload_Secondary));

        info.AddValue("controls_Sprint_SnipeSteady_Primary", (controls_Sprint_SnipeSteady_Primary));
        info.AddValue("controls_Sprint_SnipeSteady_Secondary", (controls_Sprint_SnipeSteady_Secondary));

        //

        info.AddValue("audio_GeneralVolume", (audio_GeneralVolume));
        info.AddValue("audio_MusicVolume", (audio_MusicVolume));
        info.AddValue("audio_SFXVolume", (audio_SFXVolume));
        info.AddValue("audio_VoiceVolume", (audio_VoiceVolume));

        //

        info.AddValue("video_Brightness", (video_Brightness));
        info.AddValue("video_IsShadowOn", (video_IsShadowOn));
        info.AddValue("video_LODBias", (video_LODBias));
        info.AddValue("video_ResolutionIndex", (video_ResolutionIndex));
        info.AddValue("video_ShadowDistance", (video_ShadowDistance));
        info.AddValue("video_ShadowQual", (video_ShadowQual));
        info.AddValue("video_TextureQual", (video_TextureQual));
        info.AddValue("video_Type", (video_Type));
        info.AddValue("video_UseAnisotropic", (video_UseAnisotropic));
        info.AddValue("video_UseBloom", (video_UseBloom));
        info.AddValue("video_UseSSAO", (video_UseSSAO));
        info.AddValue("video_VSync", (video_VSync));

    }
}

public static class GameSaveLoadController
{
    public static bool optionsLoadWasOK = false;

    static string saveLoadPath = "";
    static string gameStatePath = "_game.mof";
    static string playerStatePath = "_ply.mof";
    static string optionsPath = "_opts.mof";

    static string GetSaveLoadPath()
    {
        return saveLoadPath;
    }

    static string GetGameStatePath()
    {
        return GetSaveLoadPath() + gameStatePath;
    }
    public static void SaveGameState()
    {
        GameStateData data = new GameStateData();
        data.gameCurrentLevelLastCheckPoint = GameController.gameCurrentLevelLastCheckPoint;
        data.gameLastLevel = GameController.gameLastLevel;
        data.gameCurrentLevel = GameController.gameCurrentLevel;

        string filePath = GetGameStatePath();

        Stream stream = null;

        try
        {
            stream = File.Open(filePath, FileMode.Create);

            BinaryFormatter bformatter = new BinaryFormatter();
            bformatter.Binder = new VersionDeserializationBinder();
            bformatter.Serialize(stream, data);
        }
        catch
        {
            //<Test>
            Debug.LogError("Saving game state error!!!");
            //</Test>
        }
        finally
        {
            if (stream != null)
                stream.Close();
        }
    }
    public static void LoadGameState()
    {
        GameStateData data = new GameStateData();

        string filePath = GetGameStatePath();

        Stream stream = null;

        try
        {
            stream = File.Open(filePath, FileMode.Open);

            BinaryFormatter bformatter = new BinaryFormatter();
            bformatter.Binder = new VersionDeserializationBinder();
            data = (GameStateData)bformatter.Deserialize(stream);

            GameController.SetGameCurrentLevel(data.gameCurrentLevel);
            GameController.SetGameCurrentLevelLastCheckPoint(data.gameCurrentLevelLastCheckPoint);
            GameController.SetGameLastLevel(data.gameLastLevel);
        }
        catch
        {
            //<Test>
            Debug.LogError("Loading game state error!!!");
            //</Test>

            ResetGameStateToDefault();
        }
        finally
        {
            if (stream != null)
                stream.Close();
        }
    }
    public static void ResetGameStateToDefault()
    {
        GameController.ResetGameStateToDefault();
    }

    static string GetPlayerStatePath()
    {
        return GetSaveLoadPath() + playerStatePath;
    }
    public static void SavePlayerState()
    {
        PlayerStateData data = new PlayerStateData();

        data.lastCheckPoint_CurGrenadeCount = PlayerController.LastCheckPoint_CurGrenadeCount;
        data.lastCheckPoint_IsLvlCampPlayer = PlayerController.LastCheckPoint_IsLvlCampPlayer;
        data.lastCheckPoint_HaveSecGun = PlayerController.LastCheckPoint_HaveSecGun;
        data.lastCheckPoint_PrimGun = PlayerController.LastCheckPoint_PrimGun;
        data.lastCheckPoint_PrimGunCurBulletCount = PlayerController.LastCheckPoint_PrimGunCurBulletCount;
        data.lastCheckPoint_PrimGunCurMagCount = PlayerController.LastCheckPoint_PrimGunCurMagCount;
        data.lastCheckPoint_SecGun = PlayerController.LastCheckPoint_SecGun;
        data.lastCheckPoint_SecGunCurBulletCount = PlayerController.LastCheckPoint_SecGunCurBulletCount;
        data.lastCheckPoint_SecGunCurMagCount = PlayerController.LastCheckPoint_SecGunCurMagCount;

        string filePath = GetPlayerStatePath();

        Stream stream = null;

        try
        {
            stream = File.Open(filePath, FileMode.Create);

            BinaryFormatter bformatter = new BinaryFormatter();
            bformatter.Binder = new VersionDeserializationBinder();
            bformatter.Serialize(stream, data);
        }
        catch
        {
            //<Test>
            Debug.LogError("Saving player state error!!!");
            //</Test>
        }
        finally
        {
            if (stream != null)
                stream.Close();
        }
    }
    public static void LoadPlayerState()
    {
        PlayerStateData data = new PlayerStateData();

        string filePath = GetPlayerStatePath();

        Stream stream = null;

        try
        {
            stream = File.Open(filePath, FileMode.Open);

            BinaryFormatter bformatter = new BinaryFormatter();
            bformatter.Binder = new VersionDeserializationBinder();
            data = (PlayerStateData)bformatter.Deserialize(stream);

            PlayerController.LastCheckPoint_CurGrenadeCount = data.lastCheckPoint_CurGrenadeCount;
            PlayerController.LastCheckPoint_IsLvlCampPlayer = data.lastCheckPoint_IsLvlCampPlayer;
            PlayerController.LastCheckPoint_HaveSecGun = data.lastCheckPoint_HaveSecGun;
            PlayerController.LastCheckPoint_PrimGun = data.lastCheckPoint_PrimGun;
            PlayerController.LastCheckPoint_PrimGunCurBulletCount = data.lastCheckPoint_PrimGunCurBulletCount;
            PlayerController.LastCheckPoint_PrimGunCurMagCount = data.lastCheckPoint_PrimGunCurMagCount;
            PlayerController.LastCheckPoint_SecGun = data.lastCheckPoint_SecGun;
            PlayerController.LastCheckPoint_SecGunCurBulletCount = data.lastCheckPoint_SecGunCurBulletCount;
            PlayerController.LastCheckPoint_SecGunCurMagCount = data.lastCheckPoint_SecGunCurMagCount;

            PlayerController.LoadWasOK = true;
        }
        catch
        {
            PlayerController.LoadWasOK = false;

            //<Test>
            Debug.LogError("Loading player state error!!!");
            //</Test>
        }
        finally
        {
            if (stream != null)
                stream.Close();
        }
    }




    static string GetOptionsPath()
    {
        return GetSaveLoadPath() + optionsPath;
    }
    public static void SaveOptions()
    {
        OptionsData data = new OptionsData();

        data.controls_SensitivityX = CustomInputManager.sensitivityX;
        data.controls_SensitivityY = CustomInputManager.sensitivityY;
        data.controls_InvertMouse = CustomInputManager.invertMouse;
        data.controls_UseMouseWheelToChangeWeapon = CustomInputManager.useMouseWheelToChangeWeapon;

        data.controls_Action_Primary = CustomInputManager.keys.Action.primary;
        data.controls_Action_Secondary = CustomInputManager.keys.Action.secondary;

        data.controls_Action_Primary = CustomInputManager.keys.Action.primary;
        data.controls_Action_Secondary = CustomInputManager.keys.Action.secondary;

        data.controls_Aim_Primary = CustomInputManager.keys.Aim.primary;
        data.controls_Aim_Secondary = CustomInputManager.keys.Aim.secondary;

        data.controls_ChangeGun_Primary = CustomInputManager.keys.ChangeGun.primary;
        data.controls_ChangeGun_Secondary = CustomInputManager.keys.ChangeGun.secondary;

        data.controls_Crouch_Primary = CustomInputManager.keys.Crouch.primary;
        data.controls_Crouch_Secondary = CustomInputManager.keys.Crouch.secondary;

        data.controls_Fire_Primary = CustomInputManager.keys.Fire.primary;
        data.controls_Fire_Secondary = CustomInputManager.keys.Fire.secondary;

        data.controls_Grenade_SnipeTimeController_Primary = CustomInputManager.keys.Grenade_SnipeTimeController.primary;
        data.controls_Grenade_SnipeTimeController_Secondary = CustomInputManager.keys.Grenade_SnipeTimeController.secondary;

        data.controls_Jump_Primary = CustomInputManager.keys.Jump.primary;
        data.controls_Jump_Secondary = CustomInputManager.keys.Jump.secondary;

        data.controls_Melee_Primary = CustomInputManager.keys.Melee.primary;
        data.controls_Melee_Secondary = CustomInputManager.keys.Melee.secondary;

        data.controls_Missions_Primary = CustomInputManager.keys.Missions.primary;
        data.controls_Missions_Secondary = CustomInputManager.keys.Missions.secondary;

        data.controls_MoveBackward_Primary = CustomInputManager.keys.MoveBackward.primary;
        data.controls_MoveBackward_Secondary = CustomInputManager.keys.MoveBackward.secondary;

        data.controls_MoveForward_Primary = CustomInputManager.keys.MoveForward.primary;
        data.controls_MoveForward_Secondary = CustomInputManager.keys.MoveForward.secondary;

        data.controls_MoveLeft_Primary = CustomInputManager.keys.MoveLeft.primary;
        data.controls_MoveLeft_Secondary = CustomInputManager.keys.MoveLeft.secondary;

        data.controls_MoveRight_Primary = CustomInputManager.keys.MoveRight.primary;
        data.controls_MoveRight_Secondary = CustomInputManager.keys.MoveRight.secondary;

        data.controls_Reload_Primary = CustomInputManager.keys.Reload.primary;
        data.controls_Reload_Secondary = CustomInputManager.keys.Reload.secondary;

        data.controls_Sprint_SnipeSteady_Primary = CustomInputManager.keys.Sprint_SnipeSteady.primary;
        data.controls_Sprint_SnipeSteady_Secondary = CustomInputManager.keys.Sprint_SnipeSteady.secondary;

        //

        data.audio_GeneralVolume = AudioController.GeneralVolume;
        data.audio_MusicVolume = AudioController.musicVolume;
        data.audio_SFXVolume = AudioController.sfxVolume;
        data.audio_VoiceVolume = AudioController.voiceVolume;

        //

        data.video_ResolutionIndex = VideoSettingsController.curResolutionIndex;
        data.video_VSync = VideoSettingsController.curIsVSyncOn;
        data.video_Brightness = VideoSettingsController.curBrightness;
        data.video_LODBias = VideoSettingsController.curLODBias;
        data.video_Type = VideoSettingsController.curVideoSettingType;
        data.video_IsShadowOn = VideoSettingsController.curIsShadowOn;
        data.video_ShadowQual = VideoSettingsController.curShadowQual;
        data.video_ShadowDistance = VideoSettingsController.curShadowDistance;
        data.video_TextureQual = VideoSettingsController.curTextureQual;
        data.video_UseSSAO = VideoSettingsController.curUseSSAO;
        data.video_UseAnisotropic = VideoSettingsController.curUseAnisotropic;
        data.video_UseBloom = VideoSettingsController.curUseBloom;

        //

        string filePath = GetOptionsPath();

        Stream stream = null;

        try
        {
            stream = File.Open(filePath, FileMode.Create);

            BinaryFormatter bformatter = new BinaryFormatter();
            bformatter.Binder = new VersionDeserializationBinder();
            bformatter.Serialize(stream, data);
        }
        catch
        {
            //<Test>
            Debug.LogError("Saving options error!!!");
            //</Test>
        }
        finally
        {
            if (stream != null)
                stream.Close();
        }
    }
    public static void LoadOptions()
    {
        OptionsData data = new OptionsData();

        string filePath = GetOptionsPath();

        Stream stream = null;

        try
        {
            stream = File.Open(filePath, FileMode.Open);

            BinaryFormatter bformatter = new BinaryFormatter();
            bformatter.Binder = new VersionDeserializationBinder();
            data = (OptionsData)bformatter.Deserialize(stream);

            CustomInputManager.sensitivityX = data.controls_SensitivityX;
            CustomInputManager.sensitivityY = data.controls_SensitivityY;
            CustomInputManager.useMouseWheelToChangeWeapon = data.controls_UseMouseWheelToChangeWeapon;
            CustomInputManager.invertMouse = data.controls_InvertMouse;

            CustomInputManager.AssignKeyToKeyInfo(CustomInputManager.keys.Action, data.controls_Action_Primary, true);
            CustomInputManager.AssignKeyToKeyInfo(CustomInputManager.keys.Action, data.controls_Action_Secondary, false);

            CustomInputManager.AssignKeyToKeyInfo(CustomInputManager.keys.Aim, data.controls_Aim_Primary, true);
            CustomInputManager.AssignKeyToKeyInfo(CustomInputManager.keys.Aim, data.controls_Aim_Secondary, false);

            CustomInputManager.AssignKeyToKeyInfo(CustomInputManager.keys.ChangeGun, data.controls_ChangeGun_Primary, true);
            CustomInputManager.AssignKeyToKeyInfo(CustomInputManager.keys.ChangeGun, data.controls_ChangeGun_Secondary, false);

            CustomInputManager.AssignKeyToKeyInfo(CustomInputManager.keys.Crouch, data.controls_Crouch_Primary, true);
            CustomInputManager.AssignKeyToKeyInfo(CustomInputManager.keys.Crouch, data.controls_Crouch_Secondary, false);

            CustomInputManager.AssignKeyToKeyInfo(CustomInputManager.keys.Fire, data.controls_Fire_Primary, true);
            CustomInputManager.AssignKeyToKeyInfo(CustomInputManager.keys.Fire, data.controls_Fire_Secondary, false);

            CustomInputManager.AssignKeyToKeyInfo(CustomInputManager.keys.Grenade_SnipeTimeController, data.controls_Grenade_SnipeTimeController_Primary, true);
            CustomInputManager.AssignKeyToKeyInfo(CustomInputManager.keys.Grenade_SnipeTimeController, data.controls_Grenade_SnipeTimeController_Secondary, false);

            CustomInputManager.AssignKeyToKeyInfo(CustomInputManager.keys.Jump, data.controls_Jump_Primary, true);
            CustomInputManager.AssignKeyToKeyInfo(CustomInputManager.keys.Jump, data.controls_Jump_Secondary, false);

            CustomInputManager.AssignKeyToKeyInfo(CustomInputManager.keys.Melee, data.controls_Melee_Primary, true);
            CustomInputManager.AssignKeyToKeyInfo(CustomInputManager.keys.Melee, data.controls_Melee_Secondary, false);

            CustomInputManager.AssignKeyToKeyInfo(CustomInputManager.keys.Missions, data.controls_Missions_Primary, true);
            CustomInputManager.AssignKeyToKeyInfo(CustomInputManager.keys.Missions, data.controls_Missions_Secondary, false);

            CustomInputManager.AssignKeyToKeyInfo(CustomInputManager.keys.MoveBackward, data.controls_MoveBackward_Primary, true);
            CustomInputManager.AssignKeyToKeyInfo(CustomInputManager.keys.MoveBackward, data.controls_MoveBackward_Secondary, false);

            CustomInputManager.AssignKeyToKeyInfo(CustomInputManager.keys.MoveForward, data.controls_MoveForward_Primary, true);
            CustomInputManager.AssignKeyToKeyInfo(CustomInputManager.keys.MoveForward, data.controls_MoveForward_Secondary, false);

            CustomInputManager.AssignKeyToKeyInfo(CustomInputManager.keys.MoveLeft, data.controls_MoveLeft_Primary, true);
            CustomInputManager.AssignKeyToKeyInfo(CustomInputManager.keys.MoveLeft, data.controls_MoveLeft_Secondary, false);

            CustomInputManager.AssignKeyToKeyInfo(CustomInputManager.keys.MoveRight, data.controls_MoveRight_Primary, true);
            CustomInputManager.AssignKeyToKeyInfo(CustomInputManager.keys.MoveRight, data.controls_MoveRight_Secondary, false);

            CustomInputManager.AssignKeyToKeyInfo(CustomInputManager.keys.Reload, data.controls_Reload_Primary, true);
            CustomInputManager.AssignKeyToKeyInfo(CustomInputManager.keys.Reload, data.controls_Reload_Secondary, false);

            CustomInputManager.AssignKeyToKeyInfo(CustomInputManager.keys.Sprint_SnipeSteady, data.controls_Sprint_SnipeSteady_Primary, true);
            CustomInputManager.AssignKeyToKeyInfo(CustomInputManager.keys.Sprint_SnipeSteady, data.controls_Sprint_SnipeSteady_Secondary, false);

            //

            AudioController.SetVolume_General(data.audio_GeneralVolume);
            AudioController.SetVolume_Music(data.audio_MusicVolume);
            AudioController.SetVolume_SFX(data.audio_SFXVolume);
            AudioController.SetVolume_Voice(data.audio_VoiceVolume);

            //

            VideoSettingsController.SetResolution(data.video_ResolutionIndex, false);
            VideoSettingsController.Unapplied_SetIsVSyncOn(data.video_VSync);
            VideoSettingsController.SetBightness(data.video_Brightness);
            VideoSettingsController.SetLODBias(data.video_LODBias);

            VideoSettingsController.SetVideoSettingType(data.video_Type);
            VideoSettingsController.Unapplied_SetIsShadowOn(data.video_IsShadowOn);
            VideoSettingsController.Unapplied_SetShadowQuality(data.video_ShadowQual);
            //VideoSettingsController.SetShadowDistance(data.video_ShadowDistance);
            VideoSettingsController.Unapplied_SetTextureQuality(data.video_TextureQual);
            VideoSettingsController.SetUseSSAO(data.video_UseSSAO);
            VideoSettingsController.SetUseAnisotropic(data.video_UseAnisotropic);
            VideoSettingsController.SetUseBloom(data.video_UseBloom);

            VideoSettingsController.ApplyRelativeAppliableSettings();

            //

            optionsLoadWasOK = true;
        }
        catch
        {
            optionsLoadWasOK = false;

            //<Test>
            Debug.LogError("Loading options error!!!");
            //</Test>
        }
        finally
        {
            if (stream != null)
                stream.Close();
        }
    }
}

public sealed class VersionDeserializationBinder : SerializationBinder
{
    public override Type BindToType(string assemblyName, string typeName)
    {
        if (!string.IsNullOrEmpty(assemblyName) && !string.IsNullOrEmpty(typeName))
        {
            Type typeToDeserialize = null;

            assemblyName = Assembly.GetExecutingAssembly().FullName;

            // The following line of code returns the type. 
            typeToDeserialize = Type.GetType(String.Format("{0}, {1}", typeName, assemblyName));

            return typeToDeserialize;
        }

        return null;
    }
}
