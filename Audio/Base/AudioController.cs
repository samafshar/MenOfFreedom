using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class AudioController{

    public static float GeneralVolume = 1;
    public static float GeneralPitch = 1;

    public static float sfxVolume = 1;
    public static float voiceVolume = 1;
    public static float musicVolume = 1;

    public static List<AudioInfo> audioInfos = new List<AudioInfo>();

    public static void Init()
    {
        InitDefaultVolumes();
    }

    public static void InitDefaultVolumes()
    {
        SetVolume_General(1);
        SetVolume_SFX(1);
        SetVolume_Music(1);
        SetVolume_Voice(1);
    }


    public static void AddAudioInfo(AudioInfo _audioInfo)
    {
        AudioInfo audInf = _audioInfo;

        audInf.UpdateVolume();
        audInf.UpdatePitch();



        if (!audioInfos.Contains(audInf))
        {
            audioInfos.Add(audInf);
        }
    }

    public static void RemoveAudioInfo(AudioInfo _audioInfo)
    {
        AudioInfo audInf = _audioInfo;

        if (audioInfos.Contains(audInf))
            audioInfos.Remove(audInf);
    }

    public static void RemoveAllAudioInfos()
    {
        audioInfos.Clear();
    }

    public static void UpdateVolume()
    {
        foreach (AudioInfo audInf in audioInfos)
        {
            audInf.UpdateVolume();
        }
    }

    public static void UpdatePitch()
    {
        foreach (AudioInfo audInf in audioInfos)
        {
            audInf.UpdatePitch();
        }
    }

    public static void Pause()
    {
        foreach (AudioInfo audInf in audioInfos)
        {
            audInf.GamePause();
        }
    }

    public static void UnPause()
    {
        foreach (AudioInfo audInf in audioInfos)
        {
            audInf.GameUnPause();
        }
    }

    public static void SetVolume_General(float _vol)
    {
        GeneralVolume = Mathf.Clamp01(_vol);
        UpdateVolume();
    }

    public static void SetVolume_SFX(float _vol)
    {
        sfxVolume = Mathf.Clamp01(_vol);
        UpdateVolume();
    }

    public static void SetVolume_Voice(float _vol)
    {
        voiceVolume = Mathf.Clamp01(_vol);
        UpdateVolume();
    }

    public static void SetVolume_Music(float _vol)
    {
        musicVolume = Mathf.Clamp01(_vol);
        UpdateVolume();
    }

    public static void SetGeneralPitch(float _pitch)
    {
        GeneralPitch = Mathf.Clamp(_pitch, 0, 3);
        UpdatePitch();
    }

    //public static void CreateDeadAudioInfo(GameObject _audioInfoGameObject)
    //{
    //    GameObject deadAudInfObj = GameObject.Instantiate(_audioInfoGameObject, _audioInfoGameObject.transform.position, _audioInfoGameObject.transform.rotation) as GameObject;

    //    AudioInfo sourceAudInf = _audioInfoGameObject.GetComponent<AudioInfo>();
    //    AudioInfo deadAudInf = deadAudInfObj.GetComponent<AudioInfo>();


    //    deadAudInf.audioSource.playOnAwake = false;
    //    deadAudInf.audioSource.loop = false;
    //    deadAudInf.audioSource.time = sourceAudInf.time;

    //    deadAudInf.continuePlayingAfterDie = false;
    //    deadAudInf.wasPlayingBeforePause = sourceAudInf.wasPlayingBeforePause;
    //    deadAudInf.isPaused = false;
    //    deadAudInf.isPlaying = false;
    //    deadAudInf.customVolume = sourceAudInf.customVolume;
    //    deadAudInf.customPitch = sourceAudInf.customPitch;
    //    deadAudInf.initialVolume = sourceAudInf.initialVolume;
    //    deadAudInf.initialPitch = sourceAudInf.initialPitch;
    //    deadAudInf.time = sourceAudInf.time;

    //    deadAudInf.SetItsADeadAudioInfo();

    //    deadAudInf.audioSource.enabled = true;
    //    deadAudInf.enabled = true;

    //    deadAudInf.PlayAsDead();

    //    if (sourceAudInf.isPaused)
    //    {
    //        deadAudInf.Pause();
    //    }
    //}
}
