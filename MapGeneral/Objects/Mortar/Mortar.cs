using UnityEngine;
using System.Collections;

public enum MortarPriorityGroup
{
    A = 0,
    B = 1,
    C = 2,
    KillingPlayer = 3,
    none = 4,
}

public class Mortar : MonoBehaviour
{
    public GameObject particleRoot;
    public AudioInfo audioInfo;
    public float delayTime = 0f;
    public MortarPriorityGroup priorityGroup = MortarPriorityGroup.A;

    //
    AudioClip[] clips;
    int step;

    bool isFinished = false;
    bool isReady = true;

    ParticleSystem curPar;

    void Start()
    {
        clips = audioInfo.GetComponent<AudioClipList>().audioClips;
    }

    void Update()
    {
        if (step == 1)
        {
            if (delayTime > 0)
            {
                delayTime = MathfPlus.DecByDeltatimeToZero(delayTime);

                if (delayTime == 0)
                {
                    SetStep(2);
                }
            }
            else
                SetStep(2);
        }

        if (step == 2)
        {
            PlayExplosionEffects();

            GetComponent<Explosion>().Explode();

            SetReady(false);

            SetStep(3);
        }

        if (step == 3)
        {
            if (!curPar.isPlaying)
            {
                Done();
            }
        }
    }

    void SetFinished(bool _isFinished)
    {
        isFinished = _isFinished;
    }

    void SetReady(bool _isReady)
    {
        isReady = _isReady;
    }

    void Done()
    {
        StopExplosionEffects();

        SetReady(true);
        SetFinished(true);
        SetStep(1000);
    }

    void SetStep(int _step)
    {
        step = _step;
    }

    void PlayExplosionEffects()
    {
        particleRoot.SetActiveRecursively(true);

        curPar = particleRoot.GetComponent<ParticleSystem>();

        audioInfo.PlayClip(clips);
    }

    void StopExplosionEffects()
    {
        particleRoot.SetActiveRecursively(false);
    }

    public bool IsFinished()
    {
        return isFinished;
    }

    public bool IsReady()
    {
        return isReady;
    }

    public void Refresh()
    {
        SetReady(true);
        SetFinished(false);
    }

    public void StartIt()
    {
        SetStep(1);
    }
}
