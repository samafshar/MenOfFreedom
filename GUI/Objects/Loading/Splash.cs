using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {

    public GUITexture logoParnian;
    public GUITexture logoBonyad;
    public GUITexture logoPejvak;
    public float initialDelay = 0.5f;
    public float parnianTime = 1;
    public float bonyadTime = 1;
    public float pejvakTime = 1;
    public float betweenLogosDelay = 0.5f;
    public float alphaSpeed = 4f;

    int step = 0;
    float timeCounter = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (step == 0) //Init_FirstDelay
        {
            timeCounter = initialDelay;
            step = 1;
        }

        if (step == 1) //Update_FirstDelay
        {
            timeCounter -= Time.deltaTime;
            if (timeCounter <= 0)
            {
                step = 2;
            }
        }

        if (step == 2) 
        {
            logoParnian.color = new Color(logoParnian.color.r, logoParnian.color.g, logoParnian.color.b, logoParnian.color.a + alphaSpeed * Time.deltaTime);

            if (logoParnian.color.a >= 1)
            {
                timeCounter = parnianTime;
                step = 3;
            }
        }

        if (step == 3) 
        {
            timeCounter -= Time.deltaTime;
            if (timeCounter <= 0)
            {
                step = 4;
            }
        }

        if (step == 4)
        {
            logoParnian.color = new Color(logoParnian.color.r, logoParnian.color.g, logoParnian.color.b, logoParnian.color.a - alphaSpeed * Time.deltaTime);

            if (logoParnian.color.a <= 0)
            {
                timeCounter = betweenLogosDelay;
                step = 5;
            }
        }

        if (step == 5)
        {
            timeCounter -= Time.deltaTime;
            if (timeCounter <= 0)
            {
                step = 6;
            }
        }

        if (step == 6)
        {
            logoBonyad.color = new Color(logoBonyad.color.r, logoBonyad.color.g, logoBonyad.color.b, logoBonyad.color.a + alphaSpeed * Time.deltaTime);

            if (logoBonyad.color.a >= 1)
            {
                timeCounter = bonyadTime;
                step = 7;
            }
        }

        if (step == 7)
        {
            timeCounter -= Time.deltaTime;
            if (timeCounter <= 0)
            {
                step = 8;
            }
        }

        if (step == 8)
        {
            logoBonyad.color = new Color(logoBonyad.color.r, logoBonyad.color.g, logoBonyad.color.b, logoBonyad.color.a - alphaSpeed * Time.deltaTime);

            if (logoBonyad.color.a <= 0)
            {
                timeCounter = betweenLogosDelay;
                step = 9;
            }
        }

        if (step == 9)
        {
            timeCounter -= Time.deltaTime;
            if (timeCounter <= 0)
            {
                step = 10;
            }
        }

        if (step == 10)
        {
            logoPejvak.color = new Color(logoPejvak.color.r, logoPejvak.color.g, logoPejvak.color.b, logoPejvak.color.a + alphaSpeed * Time.deltaTime);

            if (logoPejvak.color.a >= 1)
            {
                timeCounter = pejvakTime;
                step = 11;
            }
        }

        if (step == 11)
        {
            timeCounter -= Time.deltaTime;
            if (timeCounter <= 0)
            {
                step = 12;
            }
        }

        if (step == 12)
        {
            logoPejvak.color = new Color(logoPejvak.color.r, logoPejvak.color.g, logoPejvak.color.b, logoPejvak.color.a - alphaSpeed * Time.deltaTime);

            if (logoPejvak.color.a <= 0)
            {
                timeCounter = betweenLogosDelay;
                step = 13;
            }
        }

        if (step == 13)
        {
            timeCounter -= Time.deltaTime;
            if (timeCounter <= 0)
            {
                GameController.LoadMainMenu();
                step = 14;
            }
        }
	}
}
