using UnityEngine;
using System.Collections;

public class LvlB2_BeforeEnd : MonoBehaviour
{
    public GameObject AnimPack01;

   // public float timeToHidePack;


	// Use this for initialization
	void Start () {
        AnimPack01.SetActiveRecursively(true);
	}

    void Update()
    {
        //timeToHidePack = MathfPlus.DecByDeltatimeToZero(timeToHidePack);

        //if (timeToHidePack == 0)
        //{
        //    timeToHidePack = 1000000f;

        //    AnimPack01.SetActiveRecursively(false);
        //}
    }
}
