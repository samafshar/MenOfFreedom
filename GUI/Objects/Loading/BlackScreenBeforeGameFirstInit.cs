using UnityEngine;
using System.Collections;

public class BlackScreenBeforeGameFirstInit : MonoBehaviour {

    public Texture2D blackBGTexture;

    public Camera camToEnable;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (VideoSettingsController.gameOneTimeResolutionIsSet)
        {
            if (Screen.currentResolution.width == Screen.resolutions[VideoSettingsController.curResolutionIndex].width && Screen.currentResolution.height == Screen.resolutions[VideoSettingsController.curResolutionIndex].height)
            {
                camToEnable.enabled = true;
                enabled = false;
            }
        }
	}

    void OnGUI()
    {
        //GUI.depth = -2000;

        //if (!VideoSettingsController.gameOneTimeResolutionIsSet)
        //    GUI.DrawTexture(new Rect(-10000, -10000, 10000000, 10000000), blackBGTexture);
    }
}
