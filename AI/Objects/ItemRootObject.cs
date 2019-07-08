using UnityEngine;
using System.Collections;

public class ItemRootObject : MonoBehaviour {

    public Item childItem;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetFinished()
    {
        Destroy(gameObject);
    }
}
