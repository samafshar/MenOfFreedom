using UnityEngine;
using System.Collections;

public enum AudioType
{
    SFX,
    Voice,
    Music,
}

public class AudioInfo : MonoBehaviour
{

    public AudioSource audioSource;

    public AudioType audioType = AudioType.SFX;

    public bool continuePlayingAfterDie = false;

    //

    [HideInInspector]
    public bool wasPlayingBeforePause = false;

    [HideInInspector]
    public bool isGamePaused = false;

    [HideInInspector]
    public bool isPlaying = false;

    [HideInInspector]
    public float time = 0;

    [HideInInspector]
    public bool isItADeadAudioInfo = false;

    //

    [HideInInspector]
    public float customVolume = 1f;

    [HideInInspector]
    public float customPitch = 1;

    [HideInInspector]
    public float initialVolume = -1;

    [HideInInspector]
    public float initialPitch;

    bool shouldDecreaseVolumeToEnd = false;
    float decreasingCustomVolumeTime = 0;

    float curAudioTimeCounter = 0;

    bool isInitedBefore = false;

    //

    public bool IsReady()
    {
        if (isGamePaused)
            return false;

        if (audioSource.isPlaying)
            return false;

        return true;
    }

    public void PlayClip_OneShot(AudioClip _audioClip)
    {
        audioSource.PlayOneShot(_audioClip);
        SetCurAudioTimeCounter(_audioClip);
    }

    public void PlayClip_OneShot(AudioClip[] _audioClips)
    {
        AudioClip audCl = _audioClips[Random.Range(0, _audioClips.Length)];
        PlayClip_OneShot(audCl);
    }

    public void PlayClipIfReady(AudioClip _audioClip)
    {
        if (IsReady())
            PlayClip(_audioClip);
    }

    public void PlayClipIfReady(AudioClip[] _audioClips)
    {
        PlayClipIfReady(_audioClips[Random.Range(0, _audioClips.Length)]);
    }

    public void PlayClip(AudioClip _audioClip)
    {
        audioSource.Stop();
        audioSource.clip = _audioClip;
        audioSource.Play();

        SetCurAudioTimeCounter(audioSource.clip);

        if (isGamePaused)
        {
            wasPlayingBeforePause = true;
            audioSource.Pause();
        }

        isPlaying = !isGamePaused;
    }

    public void PlayClip(AudioClip[] _audioClips)
    {
        PlayClip(_audioClips[Random.Range(0, _audioClips.Length)]);
    }

    public void PlayIfReady()
    {
        if (IsReady())
            Play();
    }

    public void Play()
    {
        audioSource.Stop();
        audioSource.Play();

        SetCurAudioTimeCounter(audioSource.clip);

        if (isGamePaused)
        {
            wasPlayingBeforePause = true;
            audioSource.Pause();
        }

        isPlaying = !isGamePaused;
    }

    //public void PlayAsDead()
    //{
    //    audioSource.Play();

    //    if (isPaused)
    //    {
    //        wasPlayingBeforePause = true;
    //        audioSource.Pause();
    //    }

    //    isPlaying = !isPaused;
    //}

    public void GamePause()
    {
        if (!isGamePaused)
        {
            wasPlayingBeforePause = audioSource.isPlaying;

            isPlaying = false;

            isGamePaused = true;

            audioSource.Pause();
        }
    }

    public void GameUnPause()
    {
        if (isGamePaused)
        {
            isGamePaused = false;

            if (wasPlayingBeforePause)
            {
                isPlaying = true;
                audioSource.Play();
            }
        }
    }

    public void SetCustomVolume(float _customVolume)
    {
        customVolume = Mathf.Clamp01(_customVolume);

        UpdateVolume();
    }

    public void SetCustomPitch(float _customPitch)
    {
        customPitch = Mathf.Clamp(_customPitch, 0, 3);

        UpdatePitch();
    }

    public void UpdateVolume()
    {
        audioSource.volume = GetFinalVolume(customVolume);
    }

    public void UpdatePitch()
    {
        audioSource.pitch = GetFinalPitch(customPitch);
    }

    public void SetItsADeadAudioInfo()
    {
        isItADeadAudioInfo = true;
    }

    public void StartDecreasingCustomVolumeToEnd(float _time)
    {
        decreasingCustomVolumeTime = _time;

        shouldDecreaseVolumeToEnd = true;
    }

    public void Stop()
    {
        wasPlayingBeforePause = false;
        isPlaying = false;
        audioSource.Stop();
    }

    //

    void Awake()
    {
        Init();
    }

    void Start()
    {

    }

    public void Init()
    {
        if (isInitedBefore)
            return;

        isInitedBefore = true;

        SetInitialVariables();

        if (audioSource.playOnAwake)
            Play();

        AudioController.AddAudioInfo(this);
    }

    void Update()
    {
        curAudioTimeCounter = MathfPlus.DecByDeltatimeToZero(audioSource.pitch);

        isPlaying = audioSource.isPlaying;
        time = audioSource.time;

        if (shouldDecreaseVolumeToEnd)
        {
            SetCustomVolume(customVolume - (Time.deltaTime / decreasingCustomVolumeTime));

            if (customVolume == 0)
                shouldDecreaseVolumeToEnd = false;
        }

        if (isItADeadAudioInfo)
        {
            if (!isGamePaused && !isPlaying)
                Destroy(this.gameObject);
        }
    }

    void OnDestroy()
    {
        AudioController.RemoveAudioInfo(this);

        //if (continuePlayingAfterDie)
        //{
        //    if (isPlaying || (isPaused && wasPlayingBeforePause))
        //    {
        //        AudioController.CreateDeadAudioInfo(gameObject);
        //    }
        //}
    }

    void SetInitialVariables()
    {
        if (initialVolume == -1)
        {
            initialVolume = audioSource.volume;
            initialPitch = audioSource.pitch;
        }
    }

    float GetFinalVolume(float _customVolume)
    {
        SetInitialVariables();

        float newVol = AudioController.GeneralVolume * initialVolume * _customVolume;

        float volCoef = 1;

        switch (audioType)
        {
            case AudioType.SFX:
                volCoef = AudioController.sfxVolume;
                break;

            case AudioType.Voice:
                volCoef = AudioController.voiceVolume;
                break;

            case AudioType.Music:
                volCoef = AudioController.musicVolume;
                break;
        }

        newVol *= volCoef;

        return newVol;
    }

    float GetFinalPitch(float _customPitch)
    {
        SetInitialVariables();

        if (audioType == AudioType.Music)
            return initialPitch * _customPitch;

        float newPitch = AudioController.GeneralPitch * initialPitch * _customPitch;

        return newPitch;
    }

    public void KillYourself()
    {
        if (continuePlayingAfterDie)
        {
            if (isPlaying || (isGamePaused && wasPlayingBeforePause))
            {
                gameObject.transform.parent = null;

                audioSource.loop = false;

                continuePlayingAfterDie = false;

                SetItsADeadAudioInfo();
            }
        }
    }

    void SetCurAudioTimeCounter(AudioClip _audClip)
    {
        curAudioTimeCounter = _audClip.length;
    }

    public bool IsCurAudioFinished()
    {
        return curAudioTimeCounter == 0;
    }
}
