using UnityEngine;
using System.Collections;

public class MainMenuLogic : MonoBehaviour {

    public MenuParent mainMenuParent;

    public float menuTimeScale = 0.03f;

	// Use this for initialization
	void Awake () {
        GameController.ResetSettingsForNewScene(false);

        Time.timeScale = menuTimeScale;

        mainMenuParent.ActivateIt(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
