using UnityEngine;
using System.Collections;

public enum MusicSong
{
    Action_A,
    Action_B,
    Action_C,
    Action_D,
    Action_E,
    Ambient_A,
    Ambient_B,
    Stress_A,
    None,
}

public enum MusicFadeType
{
    Slow,
    Normal,
    Fast,
    VeryFast,
}

public enum MusicPlayingStatus
{
    Idle,
    PassingDelay,
    Playing,
    FadingToNewMusic,
    FadingToNone,
}

public class MusicController : MonoBehaviour
{
    public static MusicController Instance;

    public AudioClip Action_A;
    public AudioClip Action_B;
    public AudioClip Action_C;
    public AudioClip Action_D;
    public AudioClip Action_E;
    public AudioClip Ambient_A;
    public AudioClip Ambient_B;
    public AudioClip Stress_A;


    MusicSong curPlayingSong = MusicSong.None;
    MusicSong queuedSong = MusicSong.None;
    float newSongStartDelay = 0;

    float curFadeSpeed = 1;

    //

    void Update()
    {
        #region PassingDelay
        if (IsStatus(MusicPlayingStatus.PassingDelay))
        {
            newSongStartDelay = MathfPlus.DecByDeltatimeToZero(newSongStartDelay);

            if (newSongStartDelay == 0)
            {
                MapLogic.Instance.audioInfo_Music.SetCustomVolume(1);
                MapLogic.Instance.audioInfo_Music.PlayClip(GetAudioClipBySongType(curPlayingSong));

                SetStatus(MusicPlayingStatus.Playing);
            }
        } 
        #endregion

        #region FadingToNewMusic
        if (IsStatus(MusicPlayingStatus.FadingToNewMusic))
        {
            MapLogic.Instance.audioInfo_Music.SetCustomVolume(MapLogic.Instance.audioInfo_Music.customVolume - Time.deltaTime * curFadeSpeed);

            if (MapLogic.Instance.audioInfo_Music.customVolume == 0)
            {
                curPlayingSong = queuedSong;

                queuedSong = MusicSong.None;

                SetStatus(MusicPlayingStatus.PassingDelay);
            }
        }
        #endregion

        #region FadingToNone
        if (IsStatus(MusicPlayingStatus.FadingToNone))
        {
            MapLogic.Instance.audioInfo_Music.SetCustomVolume(MapLogic.Instance.audioInfo_Music.customVolume - Time.deltaTime * curFadeSpeed);

            if (MapLogic.Instance.audioInfo_Music.customVolume == 0)
            {
                curPlayingSong =  MusicSong.None;

                MapLogic.Instance.audioInfo_Music.Stop();

                SetStatus(MusicPlayingStatus.Idle);
            }
        }
        #endregion
    }

    void Awake()
    {
        Instance = this;
    }

    //

    MusicPlayingStatus status = MusicPlayingStatus.Idle;

    public void PlayMusic(MusicSong _song, float _delay)
    {
        MusicSong newSong = _song;
        float delay = _delay;

        if (curPlayingSong == newSong)
            return;

        if (curPlayingSong == MusicSong.None)
        {
            curPlayingSong = newSong;
            newSongStartDelay = delay;

            SetStatus(MusicPlayingStatus.PassingDelay);

            return;
        }

        EndMusicWithFadeAndStartNewSong(MusicFadeType.Fast, newSong, delay);
    }

    public void EndMusicWithFade(MusicFadeType _fadeType)
    {
        if (IsStatus(MusicPlayingStatus.Idle))
            return;

        MusicFadeType fadeType = _fadeType;

        curFadeSpeed = GetFadeSpeedByType(fadeType);

        SetStatus(MusicPlayingStatus.FadingToNone);
    }

    public void EndMusicWithFadeAndStartNewSong(MusicFadeType _fadeType, MusicSong _newSong, float _delay )
    {
        MusicFadeType fadeType = _fadeType;

        curFadeSpeed = GetFadeSpeedByType(fadeType);

        queuedSong = _newSong;

        newSongStartDelay = _delay;

        SetStatus(MusicPlayingStatus.FadingToNewMusic);
    }

    float GetFadeSpeedByType(MusicFadeType _fadeType)
    {
        MusicFadeType fadeType = _fadeType;

        switch (fadeType)
        {
            case MusicFadeType.Slow:
                return 0.10f;

            case MusicFadeType.Normal:
                return 0.15f;

            case MusicFadeType.Fast:
                return 0.3f;

            case MusicFadeType.VeryFast:
                return 0.6f;
        }

        return 1;
    }

    void SetStatus(MusicPlayingStatus _status)
    {
        status = _status;
    }

    bool IsStatus(MusicPlayingStatus _status)
    {
        return status == _status;
    }

    public AudioClip GetAudioClipBySongType(MusicSong _songType)
    {
        MusicSong songType = _songType;

        switch (songType)
        {
            case MusicSong.Action_A:
                return Action_A;

            case MusicSong.Action_B:
                return Action_B;

            case MusicSong.Action_C:
                return Action_C;

            case MusicSong.Action_D:
                return Action_D;

            case MusicSong.Action_E:
                return Action_E;

            case MusicSong.Ambient_A:
                return Ambient_A;

            case MusicSong.Ambient_B:
                return Ambient_B;

            case MusicSong.Stress_A:
                return Stress_A;
        }

        return null;
    }
}
