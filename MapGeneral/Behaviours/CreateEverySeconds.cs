using UnityEngine;
using System.Collections;

public class CreateEverySeconds : MonoBehaviour {

    public GameObject objToCreate;

    public float maxTime = 3;
    float ctr;

	// Use this for initialization
	void Start () {
        ctr = maxTime;
	}
	
	// Update is called once per frame
	void Update () {
        ctr = MathfPlus.DecByDeltatimeToZero(ctr);

        if (ctr == 0)
        {
            Instantiate(objToCreate, transform.position, transform.rotation);
            ctr = maxTime;
        }
	}
}
