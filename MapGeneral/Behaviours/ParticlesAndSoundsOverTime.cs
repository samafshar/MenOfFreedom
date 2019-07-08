using UnityEngine;
using System.Collections;

public class ParticlesAndSoundsOverTime : MonoBehaviour
{
    public ParticleSystem particleRoot;
    public AudioInfo particleSounds;

    public AudioInfo soundsToPlay;

    public float startDelayTime = 0f;

    public float particleDuration = 5f;

    public float minTimeBetweenRun = 2f;
    public float maxTimeBetweenRun = 4f;

    //

    float timer = 0f;
    float timerForEnd = 0f;
    float step = 0f;

    bool isParticle = false;
    bool isSound = false;

    AudioClip[] clips;

    void Start()
    {
        SetStep(0f);

        timer = startDelayTime;
        timerForEnd = particleDuration;

        if (particleRoot != null)
        {
            isParticle = true;

            clips = particleSounds.GetComponent<AudioClipList>().audioClips;
        }

        if (soundsToPlay != null)
        {
            isSound = true;

            clips = soundsToPlay.GetComponent<AudioClipList>().audioClips;
        }
    }

    void Update()
    {
        #region Wait For Delay Time
        if (step == 0)
        {
            timer = MathfPlus.DecByDeltatimeToZero(timer);

            if (timer == 0)
            {
                step = 1;
            }
        } 
        #endregion

        #region Start
        if (step == 1)
        {
            if (isParticle)
            {
                particleRoot.Play(true);

                particleSounds.PlayClip(clips);
            }

            if (isSound)
            {
                soundsToPlay.PlayClip(clips);
            }

            timer = Random.Range(minTimeBetweenRun, maxTimeBetweenRun);

            SetStep(2);
        } 
        #endregion

        #region Wait to finish
        if (step == 2)
        {
            if (isParticle)
            {
                timerForEnd = MathfPlus.DecByDeltatimeToZero(timerForEnd);

                if (timerForEnd == 0)
                {
                    SetStep(3);
                }
            }

            if (isSound)
            {
                if (!soundsToPlay.isPlaying)
                {
                    SetStep(3);
                }
            }
        } 
        #endregion

        #region Wait for between time
        if (step == 3)
        {
            timer = MathfPlus.DecByDeltatimeToZero(timer);

            if (timer == 0)
            {
                SetStep(1);

                timerForEnd = particleDuration;
            }
        } 
        #endregion
    }

    void SetStep(float _step)
    {
        float st = _step;

        step = st;
    }
}
