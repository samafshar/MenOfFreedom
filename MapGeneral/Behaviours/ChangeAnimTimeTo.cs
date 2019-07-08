using UnityEngine;
using System.Collections;

public class ChangeAnimTimeTo : MonoBehaviour {

    public float delay = 0.25f;
    public float time = 0;

    float delayCounter;

	// Use this for initialization
	void Start () 
    {
        delayCounter = delay;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (delayCounter > 0)
        {
            delayCounter = MathfPlus.DecByDeltatimeToZero(delayCounter);

            if (delayCounter == 0)
            {
                animation[animation.clip.name].time = time;
            }
        }
	}
}
