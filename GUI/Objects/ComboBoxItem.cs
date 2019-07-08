using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ComboBoxItem : MonoBehaviour {

    public string name;
    public GUISkin guiSkin;
    public GUISkin guiSkin_Active;
    public GUISkin guiSkin_OnTop;
    public string text;
    public List<float> additionalNums = new List<float>();

    [HideInInspector]
    public int id = -1;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
