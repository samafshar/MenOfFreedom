using UnityEngine;
using System.Collections;

public class LogicVoiceInfo : MonoBehaviour
{
    public string voiceName = "";
    public AudioClip[] audioClips;
    public float silenceAfterFinishing = 0.5f;
    public float startDelay = 0;

    public bool repeat = false;
    public float silenceBetweenRepeats_Min = 3;
    public float silenceBetweenRepeats_Max = 6;

    [HideInInspector]
    public int indexInVoiceCollection = -1;

    // Use this for initialization
    void Start()
    {
        if (string.IsNullOrEmpty(voiceName))
            Debug.LogError("Logic voice info name is empty!");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
