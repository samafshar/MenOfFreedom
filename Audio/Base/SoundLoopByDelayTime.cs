using UnityEngine;
using System.Collections;

public class SoundLoopByDelayTime : MonoBehaviour
{
    public AudioInfo audio;

    public AudioClip[] soundClips;

    public float minTimeDelay = 3f;
    public float maxTimeDelay = 6f;

    float timer;

    bool isStarted = false;

    bool shouldStop = false;

    void Start()
    {
    }

    void Update()
    {
        if (isStarted)
        {
            if (shouldStop && !audio.isPlaying)
            {
                StopIt();
            }

            if (!audio.isPlaying)
                timer = MathfPlus.DecByDeltatimeToZero(timer);

            if (timer == 0)
            {
                audio.PlayClip(soundClips);

                timer = Random.RandomRange(minTimeDelay, maxTimeDelay);
            }
        }
    }

    public void StartIt()
    {
        if (!isStarted)
        {
            timer = Random.RandomRange(minTimeDelay, maxTimeDelay);

            isStarted = true;
        }
    }

    public void EndIt()
    {
        shouldStop = true;
    }

    void StopIt()
    {
        isStarted = false;

        audio.Stop();
    }
}
