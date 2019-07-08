using UnityEngine;
using System.Collections;

public class HUDBLoodBili : MonoBehaviour
{
    public Texture2D TextureBloodUp;
    public Texture2D TextureBloodDown;
    public Texture2D TextureBloodLeft;
    public Texture2D TextureBloodRight;
    public Texture2D TextureBloodUpLeft;
    public Texture2D TextureBloodUpRight;
    public Texture2D TextureBloodDownLeft;
    public Texture2D TextureBloodDownRight;
    public Texture2D TextureBloodCritical;
    public float FadingInTextureVelocity;
    public float FadingOutTextureVelocity;

    public GUISkin SkinBloodUp;
    public GUISkin SkinBloodDown;
    public GUISkin SkinBloodLeft;
    public GUISkin SkinBloodRight;
    public GUISkin SkinBloodUpLeft;
    public GUISkin SkinBloodUpRight;
    public GUISkin SkinBloodDownLeft;
    public GUISkin SkinBloodDownRight;
    public GUISkin SkinBloodCritical;
    private Color upColor;
    private Color downColor;
    private Color leftColor;
    private Color rightColor;
    private Color upLeftColor;
    private Color upRightColor;
    private Color downLeftColor;
    private Color downRightColor;
    private Color criticalColor;

    private float upAlfa = 0;
    private float upLeftAlfa = 0;
    private float upRightAlfa = 0;
    private float leftAlfa = 0;
    private float rightAlfa = 0;
    private float downAlfa = 0;
    private float downLeftAlfa = 0;
    private float downRightAlfa = 0;
    private float criticalAlfa = 0;
    private bool[] StateBoolian = new bool[9];
    private bool[] increasingAlfa = new bool[9];

    void Awake()
    {
        for (int i = 0; i <= 8; i++)
        {
            StateBoolian[i] = false;
            increasingAlfa[i] = false;

        }
    }

    public void SetTextureStateForHUD(int OurState)
    {
  
        currentState = OurState;
  

        StateBoolian[OurState] = true;
        increasingAlfa[OurState] = true;

        if (OurState == 8)
        {
            for (int i = 0; i <= 7; i++)
            {
                StateBoolian[i] = false;
            }
        }

    }

    void OnGUI()
    {
        //SetTextureStateForHUD(0);
        //SetTextureStateForHUD(1);
        //SetTextureStateForHUD(2);
        //SetTextureStateForHUD(3);
        //SetTextureStateForHUD(4);
        //SetTextureStateForHUD(5);
        //SetTextureStateForHUD(6);
        //SetTextureStateForHUD(7);
        //SetTextureStateForHUD(8);


        if (StateBoolian[0] == true)
        {
            GUI.skin = SkinBloodLeft;
            leftColor = GUI.color;
            Color currentColor = leftColor;
            currentColor.a = leftAlfa;
            GUI.color = currentColor;
            if (increasingAlfa[0] == true)
            {
                leftAlfa += Time.deltaTime * FadingInTextureVelocity;
                if (leftAlfa > 1)
                {
                    increasingAlfa[0] = false;

                }

            }
            if (increasingAlfa[0] == false)
            {
                leftAlfa -= Time.deltaTime * FadingOutTextureVelocity;
                if (downRightAlfa <= 0)
                {
                    StateBoolian[0] = false;
                }

            }
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), TextureBloodLeft);
        }


        //Right
        if (StateBoolian[1] == true)
        {
            GUI.skin = SkinBloodRight;
            rightColor = GUI.color;
            Color currentColor = rightColor;
            currentColor.a = rightAlfa;
            GUI.color = currentColor;
            if (increasingAlfa[1] == true)
            {
                rightAlfa += Time.deltaTime * FadingInTextureVelocity;
                if (rightAlfa > 1)
                {
                    increasingAlfa[1] = false;

                }

            }
            if (increasingAlfa[1] == false)
            {
                rightAlfa -= Time.deltaTime * FadingOutTextureVelocity;
                if (rightAlfa <= 0)
                {
                    StateBoolian[1] = false;
                }

            }
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), TextureBloodRight);
        }

        //Up

        if (StateBoolian[2] == true)
        {
            GUI.skin = SkinBloodUp;
            upColor = GUI.color;
            Color currentColor = upColor;
            currentColor.a = upAlfa;
            GUI.color = currentColor;
            if (increasingAlfa[2] == true)
            {
                upAlfa += Time.deltaTime * FadingInTextureVelocity;
                if (upAlfa > 1)
                {
                    increasingAlfa[2] = false;

                }

            }
            if (increasingAlfa[2] == false)
            {
                upAlfa -= Time.deltaTime * FadingOutTextureVelocity;
                if (upAlfa <= 0)
                {
                    StateBoolian[2] = false;
                }

            }
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), TextureBloodUp);
        }

        //down
        if (StateBoolian[3] == true)
        {
            GUI.skin = SkinBloodDown;
            downColor = GUI.color;
            Color currentColor = downColor;
            currentColor.a = downAlfa;
            GUI.color = currentColor;
            if (increasingAlfa[3] == true)
            {
                downAlfa += Time.deltaTime * FadingInTextureVelocity;
                if (downAlfa > 1)
                {
                    increasingAlfa[3] = false;

                }

            }
            if (increasingAlfa[3] == false)
            {
                downAlfa -= Time.deltaTime * FadingOutTextureVelocity;
                if (downAlfa <= 0)
                {
                    StateBoolian[3] = false;
                }

            }
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), TextureBloodDown);
        }

        //upperLeft


        if (StateBoolian[4] == true)
        {
            GUI.skin = SkinBloodUpLeft;
            upLeftColor = GUI.color;
            Color currentColor = upLeftColor;
            currentColor.a = upLeftAlfa;
            GUI.color = currentColor;
            if (increasingAlfa[4] == true)
            {
                upLeftAlfa += Time.deltaTime * FadingInTextureVelocity;
                if (upLeftAlfa > 1)
                {
                    increasingAlfa[4] = false;

                }

            }
            if (increasingAlfa[4] == false)
            {
                upLeftAlfa -= Time.deltaTime * FadingOutTextureVelocity;
                if (upLeftAlfa <= 0)
                {
                    StateBoolian[4] = false;
                }

            }
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), TextureBloodUpLeft);
        }



        // upRight


        if (StateBoolian[5] == true)
        {
            GUI.skin = SkinBloodUpRight;
            upRightColor = GUI.color;
            Color currentColor = upRightColor;
            currentColor.a = upRightAlfa;
            GUI.color = currentColor;
            if (increasingAlfa[5] == true)
            {
                upRightAlfa += Time.deltaTime * FadingInTextureVelocity;
                if (upRightAlfa > 1)
                {
                    increasingAlfa[5] = false;

                }

            }
            if (increasingAlfa[5] == false)
            {
                upRightAlfa -= Time.deltaTime * FadingOutTextureVelocity;
                if (upRightAlfa <= 0)
                {
                    StateBoolian[5] = false;
                }

            }
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), TextureBloodUpRight);
        }

        //LowerLft

        if (StateBoolian[6] == true)
        {
            GUI.skin = SkinBloodDownLeft;
            downLeftColor = GUI.color;
            Color currentColor = downLeftColor;
            currentColor.a = downLeftAlfa;
            GUI.color = currentColor;
            if (increasingAlfa[6] == true)
            {
                downLeftAlfa += Time.deltaTime * FadingInTextureVelocity;
                if (downLeftAlfa > 1)
                {
                    increasingAlfa[6] = false;

                }

            }
            if (increasingAlfa[6] == false)
            {
                downLeftAlfa -= Time.deltaTime * FadingOutTextureVelocity;
                if (downLeftAlfa <= 0)
                {
                    StateBoolian[6] = false;
                }

            }
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), TextureBloodDownLeft);
        }

        //LowerRight

        if (StateBoolian[7] == true)
        {
            GUI.skin = SkinBloodDownRight;
            downRightColor = GUI.color;
            Color currentColor = downRightColor;
            currentColor.a = downRightAlfa;
            GUI.color = currentColor;
            if (increasingAlfa[7] == true)
            {
                downRightAlfa += Time.deltaTime * FadingInTextureVelocity;
                if (downRightAlfa > 1)
                {
                    increasingAlfa[7] = false;

                }

            }
            if (increasingAlfa[7] == false)
            {
                downRightAlfa -= Time.deltaTime * FadingOutTextureVelocity;
                if (downRightAlfa <= 0)
                {
                    StateBoolian[7] = false;
                }

            }
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), TextureBloodDownRight);
        }

        //Critical
        if (StateBoolian[8] == true)
        {
            GUI.skin = SkinBloodCritical;
            criticalColor = GUI.color;
            Color currentColor = criticalColor;
            currentColor.a = criticalAlfa;
            GUI.color = currentColor;
            if (increasingAlfa[8] == true)
            {
                criticalAlfa += Time.deltaTime * FadingInTextureVelocity;
                if (criticalAlfa > 1)
                {
                    increasingAlfa[8] = false;

                }

            }
            if (increasingAlfa[8] == false)
            {
                criticalAlfa -= Time.deltaTime * FadingOutTextureVelocity;
                if (criticalAlfa <= 0)
                {
                    StateBoolian[8] = false;
                }
            }
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), TextureBloodCritical);
        }
    }


    int currentState;
    public void FadeTexture()
    {
        increasingAlfa[currentState] = false;
    }

}
