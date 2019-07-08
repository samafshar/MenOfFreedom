using UnityEngine;
using System.Collections;

public class TargetHud : MonoBehaviour
{
    public float devidedScreenMinDistanceTarget;
    public float devidedScreenMaxDistanceTarget;
    public float devidedScreenMoveLimit;

    public float FadingInTargetFade;
    public float FadingOutTargetV;
    public float FadingOutTargetAimV;
    public float devidedScreenSizeWidthVerticalTexture;
    public float LenghAspect;
    public float PositiveSpeed;
    public float NegativeSpeed;
    public float PRunningTargetSpeed;
    public float NRunningTargetSpeed;
    public float NAimTargetSpeed;
    public float PAimTargetSpeed;
    public GUISkin TargetSkin;

    public Texture2D WUpTexture;
    public Texture2D WDownTexture;
    public Texture2D WLeftTexture;
    public Texture2D WRightTexture;
    public Texture2D GUpTexture;
    public Texture2D GDownTexture;
    public Texture2D GLeftTexture;
    public Texture2D GRightTexture;
    public Texture2D RUpTexture;
    public Texture2D RDownTexture;
    public Texture2D RLeftTexture;
    public Texture2D RRightTexture;
    private Vector2 sizeVerticalTexture;
    private Vector2 sizeHorizentalTexture;
    private float maxDistance;
    private float minDistance;
    private float moveLimit;
    private float singelMovementMagnitude = 0;
    private float totalMoveMag = 0;
    private float targetAlfa = 1;

    private bool TargetMoveing = false;
    private bool Increase = false;
    private int MovingCount = 0;
    private bool walking = false;
    private bool Running = false;
    private bool decreasingAlfa = false;
    private bool stopRunning = false;
    private bool aim = false;
    private bool LastWasAim = false;
    private bool startGUI = true;
    private bool stopAim = false;
    private Color targetColor;
    public Texture2D currentUpTexture;
    public Texture2D currentDownTexture;
    public Texture2D currentLeftTexture;
    public Texture2D currentRightTexture;


    void Awake()
    {
        SetSizeAndPos();
       
    }

    void Update()
    {
    }

    void OnGUI()
    {
        if (startGUI)
        {
            GUI.skin = TargetSkin;
            startGUI = false;
        }
      
        if (TargetMoveing)
        {
            if (Increase)
            {
                singelMovementMagnitude += Time.deltaTime * PositiveSpeed;

                if (singelMovementMagnitude >= moveLimit && MovingCount == 0)
                {
                    Increase = false;
                }
                else if (singelMovementMagnitude >= totalMoveMag + moveLimit && MovingCount != 0)
                {
                    Increase = false;
                }

            }
            else
            {
                singelMovementMagnitude -= Time.deltaTime * NegativeSpeed;
                if (singelMovementMagnitude <= 0)
                {
                    TargetMoveing = false;
                    singelMovementMagnitude = 0;
                    MovingCount = 0;
                    totalMoveMag = 0;

                }
            }

            if (targetAlfa < 1)
            {
                //GUI.skin = TargetSkin;
                targetColor = GUI.color;
                Color currentColor = targetColor;
                currentColor.a = targetAlfa;
                GUI.color = currentColor;

                {
                    targetAlfa += Time.deltaTime * FadingInTargetFade;
                }

                if (targetAlfa > 1)
                {

                    targetAlfa = 1;

                }
            }
        }


        if (Running || walking)
        {
            if (singelMovementMagnitude <= maxDistance)
            {
                singelMovementMagnitude += Time.deltaTime * PRunningTargetSpeed;
                if (Running)
                {
                    decreasingAlfa = true;
                }
                if (singelMovementMagnitude >= maxDistance)
                {
                    singelMovementMagnitude = maxDistance;

                }

            }


            if (decreasingAlfa)
            {
                //GUI.skin = TargetSkin;
                targetColor = GUI.color;
                Color currentColor = targetColor;
                currentColor.a = targetAlfa;
                GUI.color = currentColor;
                targetAlfa -= Time.deltaTime * FadingOutTargetV;


                if (targetAlfa < 0)
                {
                    decreasingAlfa = false;

                    targetAlfa = 0;

                }
            }

        }
        if (stopRunning || stopAim)
        {

            if (stopRunning)
            {
                singelMovementMagnitude -= Time.deltaTime * NRunningTargetSpeed;
                if (singelMovementMagnitude <= 0)
                {
                    singelMovementMagnitude = 0;

                }
            }
            if (stopAim)
            {
                singelMovementMagnitude += Time.deltaTime * NAimTargetSpeed;
               
                decreasingAlfa = true;
                if (singelMovementMagnitude >= 0)
                {
                    singelMovementMagnitude = 0;
                   

                }


            }

            if (targetAlfa < 1)
            {
                //GUI.skin = TargetSkin;
                targetColor = GUI.color;
                Color currentColor = targetColor;
                currentColor.a = targetAlfa;
                GUI.color = currentColor;

                {
                    targetAlfa += Time.deltaTime * FadingInTargetFade;
                }

                if (targetAlfa > 1)
                {

                    targetAlfa = 1;
                  
                }
            }




        }
        if (aim)
        {
           
            if (singelMovementMagnitude > -minDistance)
            {
                singelMovementMagnitude -= Time.deltaTime * NAimTargetSpeed;
                if(singelMovementMagnitude<0)
                {
                   
                decreasingAlfa = true;
                }

                if (singelMovementMagnitude <= -minDistance)
                {
                    singelMovementMagnitude = -minDistance;

                }

            }


            if (decreasingAlfa )
            {
                //GUI.skin = TargetSkin;
                targetColor = GUI.color;
                Color currentColor = targetColor;
                currentColor.a = targetAlfa;
                GUI.color = currentColor;
                targetAlfa -= Time.deltaTime * FadingOutTargetAimV;
                

                if (targetAlfa < 0)
                {

                    
                    targetAlfa = 0;
                    
                }
            }
        }

        GUI.DrawTexture(new Rect(Screen.width / 2 - sizeVerticalTexture.x / 2, Screen.height / 2 - minDistance - sizeVerticalTexture.y - singelMovementMagnitude, sizeVerticalTexture.x, sizeVerticalTexture.y), currentUpTexture);
        GUI.DrawTexture(new Rect(Screen.width / 2 - sizeVerticalTexture.x / 2, Screen.height / 2 + minDistance + singelMovementMagnitude, sizeVerticalTexture.x, sizeVerticalTexture.y), currentDownTexture);

        GUI.DrawTexture(new Rect(Screen.width / 2 + minDistance + singelMovementMagnitude, Screen.height / 2 - sizeHorizentalTexture.y / 2, sizeHorizentalTexture.x, sizeHorizentalTexture.y), currentRightTexture);
        GUI.DrawTexture(new Rect(Screen.width / 2 - minDistance - sizeHorizentalTexture.x - singelMovementMagnitude, Screen.height / 2 - sizeHorizentalTexture.y / 2, sizeHorizentalTexture.x, sizeHorizentalTexture.y), currentLeftTexture);

    }
    float CalculateDis(float divide)
    {
        float dis;
        dis = Screen.width / divide;
        return (dis);
    }
    Vector2 CalculateSize(float divide,float aspect)
    {
        Vector2 size;
        size.x = Screen.width / divide;
        size.y = size.x*aspect;
        return (size);
    }
    void SetSizeAndPos()
    {
        sizeVerticalTexture = CalculateSize(devidedScreenSizeWidthVerticalTexture,LenghAspect);
        sizeHorizentalTexture.x = sizeVerticalTexture.y;
        sizeHorizentalTexture.y = sizeVerticalTexture.x;
        minDistance = CalculateDis(devidedScreenMinDistanceTarget);
        maxDistance = CalculateDis(devidedScreenMaxDistanceTarget);
        moveLimit = CalculateDis(devidedScreenMoveLimit);
    }
    public void MoveTarget(int state)
    {
        
        // state==5 means Aim
        // state==4 means Walking
        // state ==3 means Stop (Running and Walking and Aim)
        // state ==2 means Running
        //state ==1 means shooting
        
        if (state == 1)
        {
            if (LastWasAim)
            {
                
                MoveTarget(3);
                MovingCount = 0;
            }
            if (walking == false)
            {
                
                if (TargetMoveing == true)
                {
                    MovingCount++;

                    totalMoveMag += moveLimit;
                    //totalMoveMag += singelMovementMagnitude;
                    if (totalMoveMag + moveLimit >= maxDistance)
                    {
                        totalMoveMag = maxDistance - moveLimit;
                    }

                }
               
                else
                {
                    singelMovementMagnitude = 0;
                }
               
                TargetMoveing = true;
                
                Increase = true;
                stopAim = false;
                stopRunning = false;
                Running = false;
            }
        }
        if (state == 2)
        {
            Running = true;
            stopRunning = false;
            stopAim = false;
            LastWasAim = false;
            aim = false;
        }
        if (state == 3)
        {
            Running = false;
            if (LastWasAim)
            {
                stopAim = true;
                LastWasAim = false;
            }
            else
            {
                stopRunning = true;
            }
            walking = false;
            aim = false;
            LastWasAim = false;
            TargetMoveing = false;

        }
        if (state == 4)
        {
            Running = false;
            stopRunning = false;
            stopAim = false;
            walking = true;
            aim = false;
            LastWasAim = false;
        }
        if (state == 5)
        {
            aim = true;
            LastWasAim = true;
            stopAim = false;
            walking = false;
            Running = false;
            TargetMoveing = false;
            stopRunning = false;

        }

        

    }
    public void ColorTarget(int state)
    {
        //state==1 White
        //state==2 Green
        //state==3 red
        if (state == 1)
        {
            currentDownTexture = WDownTexture;
            currentUpTexture = WUpTexture;
            currentLeftTexture = WLeftTexture;
            currentRightTexture = WRightTexture;
        }
        if (state == 2)
        {
            currentDownTexture = GDownTexture;
            currentUpTexture = GUpTexture;
            currentLeftTexture = GLeftTexture;
            currentRightTexture = GRightTexture;
        }
        if (state == 3)
        {
            currentDownTexture = RDownTexture;
            currentUpTexture = RUpTexture;
            currentLeftTexture = RLeftTexture;
            currentRightTexture = RRightTexture;
        }
    }

    public float TargetCurrentDistance()
    {
        return singelMovementMagnitude + minDistance;
    }
}
