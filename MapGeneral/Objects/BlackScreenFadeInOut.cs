using UnityEngine;
using System.Collections;

public class BlackScreenFadeInOut : MonoBehaviour
{

    public Texture2D blackTexture;
    public float fadingSpeed = 1;
    public float fadeInStartDelayTime = 0.5f;
    public float fadeOutEndDelayTime = 0.36f;

    float alpha = 1;
    bool isFadingIn = false;
    bool isFadingOut = false;
    float step = 0;

    float newFadingSpeed;
    float newFadeInStartDelayTime;
    float newFadeOutEndDelayTime;

    float timeCounter = 0;

    bool drawBlackScreen = false;

    [HideInInspector]
    public bool isFadingFinished = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        #region FadeIn
        if (isFadingIn)
        {
            if (step == 0)
            {
                timeCounter = newFadeInStartDelayTime;
                step = 1;
            }

            if (step == 1)
            {
                timeCounter = MathfPlus.DecByDeltatimeToZero(timeCounter);

                if (timeCounter == 0)
                    step = 2;
            }

            if (step == 2)
            {
                timeCounter = newFadingSpeed;

                step = 3;
            }

            if (step == 3)
            {
                timeCounter = MathfPlus.DecByDeltatimeToZero(timeCounter);

                alpha = timeCounter / newFadingSpeed;

                if (timeCounter == 0)
                    step = 4;
            }

            if (step == 4)
            {
                isFadingIn = false;
                drawBlackScreen = false;
                isFadingFinished = true;
                step = 1000;
            }
        }
        #endregion

        #region FadeOut
        if (isFadingOut)
        {
            if (step == 0)
            {
                timeCounter = newFadingSpeed;

                step = 1;
            }

            if (step == 1)
            {
                timeCounter = MathfPlus.DecByDeltatimeToZero(timeCounter);

                alpha = 1 - (timeCounter / newFadingSpeed);

                if (timeCounter == 0)
                    step = 2;
            }

            if (step == 2)
            {
                timeCounter = newFadeOutEndDelayTime;
                step = 3;
            }

            if (step == 3)
            {
                timeCounter = MathfPlus.DecByDeltatimeToZero(timeCounter);

                if (timeCounter == 0)
                    step = 4;
            }

            if (step == 4)
            {
                isFadingFinished = true;
                isFadingOut = false;
                step = 1000;
            }
        }
        #endregion
    }

    void OnGUI()
    {
        if (drawBlackScreen)
        {
            GUI.depth = -900;

            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);

            GUI.DrawTexture(new Rect(0, 0, Screen.width * 1.1f, Screen.height), blackTexture, ScaleMode.StretchToFill, true, 10.0f);
        }
    }

    public void StartFadingIn()
    {
        StartFadingIn(fadingSpeed, fadeInStartDelayTime);
    }

    public void StartFadingIn(float _fadingSpeed, float _fadeInStartDelayTime)
    {
        isFadingFinished = false;

        newFadingSpeed = _fadingSpeed;
        newFadeInStartDelayTime = _fadeInStartDelayTime;

        alpha = 1;

        step = 0;

        drawBlackScreen = true;

        isFadingIn = true;
        isFadingOut = false;
    }

    public void StartFadingOut()
    {
        StartFadingOut(fadingSpeed, fadeOutEndDelayTime);
    }

    public void StartFadingOut(float _fadingSpeed, float _fadeOutEndDelayTime)
    {
        isFadingFinished = false;

        newFadingSpeed = _fadingSpeed;
        newFadeOutEndDelayTime = _fadeOutEndDelayTime;

        alpha = 0;

        step = 0;

        drawBlackScreen = true;

        isFadingOut = true;
        isFadingIn = false;
    }

    public bool IsFadingIn()
    {
        return isFadingIn;
    }
}
