using UnityEngine;
using System.Collections;

public class CutsceneAudioInfoController : MonoBehaviour
{
    public AudioInfo audioInfo;
    public AudioClip[] audioClips;
    public float[] audioClipTimes;

    float timeCounter = 0;
    int nextIndex = 0;
    bool isPlaying = false;

    void Update()
    {
        if (isPlaying)
        {
            timeCounter = MathfPlus.DecByDeltatimeToZero(timeCounter);

            if (timeCounter == 0)
            {
                if (nextIndex >= audioClips.Length)
                    isPlaying = false;
                else
                {
                    timeCounter = audioClipTimes[nextIndex];

                    if (audioClips[nextIndex] != null)
                    {
                        audioInfo.PlayClip(audioClips[nextIndex]);
                    }

                    nextIndex++;
                }
            }
        }
    }

    public void StartIt()
    {
        timeCounter = 0;
        nextIndex = 0;
        isPlaying = true;
    }
}
