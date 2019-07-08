using UnityEngine;
using System.Collections;

public class DieInTime : MonoBehaviour {

    public float time = 1;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, time);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
