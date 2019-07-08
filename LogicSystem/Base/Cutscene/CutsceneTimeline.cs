using UnityEngine;
using System.Collections;

public class CutsceneTimeline : MonoBehaviour {

    public AnimationClip animClip;

    [HideInInspector]
    public CutsceneController parentCutsceneController;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void NextSequence()
    {
        parentCutsceneController.NextSequence();
    }

    public void StartIt()
    {
        if (animClip != null)
            animation.Play(animClip.name);
    }
}
