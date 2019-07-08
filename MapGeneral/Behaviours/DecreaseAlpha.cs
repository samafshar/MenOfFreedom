using UnityEngine;
using System.Collections;

public class DecreaseAlpha : MonoBehaviour {

    public float alphaDecSpeed = 3f;
    float alpha = 1;
    Color initialColor;

	// Use this for initialization
	void Start () {
        initialColor = renderer.material.color;
	}
	
	// Update is called once per frame
	void Update () {

        alpha = MathfPlus.DecByDeltatimeToZeroWithAdditionalCoef(alpha, alphaDecSpeed);
        renderer.material.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
	}
}
