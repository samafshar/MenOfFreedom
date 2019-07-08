using UnityEngine;
using System.Collections;

public class LvlNakhl2_End : MonoBehaviour {

    public GameObject zakhmiAllyPackToEnable;
    public float delayTimeToEnableZakhmiAllyPack;

    float timeCounter;

	// Use this for initialization
	void Start () {
        timeCounter = delayTimeToEnableZakhmiAllyPack;
	}
	
	// Update is called once per frame
	void Update () {

        timeCounter = MathfPlus.DecByDeltatimeToZero(timeCounter);

        if (timeCounter == 0)
        {
            timeCounter = 100000000;
            EnableZakhmiAllyPack();
        }
	
	}

    void EnableZakhmiAllyPack()
    {
        zakhmiAllyPackToEnable.SetActiveRecursively(true);
    }
}
