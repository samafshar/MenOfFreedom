using UnityEngine;
using System.Collections;

public class AlarmLamp : MonoBehaviour {

    public GameObject lightObject;
    public AnimationClip anim;
    Light light;
    

	// Use this for initialization
	void Start () {
        light = lightObject.GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartIt()
    {
        light.gameObject.active = true;
        light.enabled = true;
        lightObject.animation.Play(anim.name);
    }

    public void StopIt()
    {
        light.enabled = false;
        lightObject.animation.Stop();
    }
}
