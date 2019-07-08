using UnityEngine;
using System.Collections;

public class LvlB2_End : MonoBehaviour
{
    public AudioInfo[] audInfosToDecreaseVol;
    public float audInfsDecVolSpeed = 0.3f;

    public GameObject AnimPack01;
    public GameObject AnimPack02;
    public GameObject AnimPack03;
    public GameObject AnimPack04;
    public GameObject AnimPack05;
    public GameObject AnimPack06;
    public GameObject AnimPack07;
    public GameObject AnimPack08;
    public GameObject AnimPack09;
    public GameObject AnimPack10;

    public float DatPack02StartingAnimTime;
    public float DatPack03StartingAnimTime;
    public float DatPack04StartingAnimTime;
    public float DatPack05StartingAnimTime;
    public float DatPack06StartingAnimTime;
    public float DatPack07StartingAnimTime;
    public float DatPack08StartingAnimTime;
    public float DatPack09StartingAnimTime;
    public float DatPack10StartingAnimTime;

    public float LastPicsBGAlphaSpeed = 0.5f;
    public float delayTimeToStartFirstPicAfterBlackBG = 1;

    public float eachPicShowTime = 4f;
    public float eachPicAlphaSpeed = 1f;
    public float delayBetweenPics = 6.1f;

    public float lastPicShowTime = 8f;
    public float lastPicStartAlphaSpeed = 0.5f;
    public float lastPicEndAlphaSpeed = 0.33f;
    public float lastPicTotalTime = 16f;

    public float FALogoShowTime = 4f;
    public float FALogoStartAlphaSpeed = 1f;
    public float FALogoEndAlphaSpeed = 2f;
    public float FALogoTotalTime = 7f;



    int curPicIndex = 1;

    float timeCounter = 0;

    float showLastPicsState = -1;

    HUDGroup hud_Group_LastPics;

    bool isDecreasingVolOfAudInfos = false;

	// Use this for initialization
	void Start () {
        AnimPack01.SetActiveRecursively(true);

        hud_Group_LastPics = MapLogic.Instance.mapHUDParent.GetChildGroupByName(HUDGroupName.LvlBushehr02_LastPics);
	}
	
	// Update is called once per frame
	void Update () {

        if (isDecreasingVolOfAudInfos)
        {
            for (int i = 0; i < audInfosToDecreaseVol.Length; i++)
            {
                audInfosToDecreaseVol[i].SetCustomVolume(audInfosToDecreaseVol[i].customVolume - audInfsDecVolSpeed * Time.deltaTime);
            }
        }

        #region LastPics 0 Start BG
        if (showLastPicsState == 0)
        {
            hud_Group_LastPics.hudControls[0].SetAlpha(0);
            hud_Group_LastPics.hudControls[0].StartIncreasingAlpha(LastPicsBGAlphaSpeed);
            hud_Group_LastPics.hudControls[0].SetIsVisible(true);

            timeCounter = 1 / LastPicsBGAlphaSpeed + delayTimeToStartFirstPicAfterBlackBG;

            showLastPicsState = 1;
        } 
        #endregion

        #region LastPics 1 W8 for BG
        if (showLastPicsState == 1)
        {
            timeCounter = MathfPlus.DecByDeltatimeToZero(timeCounter);

            if (timeCounter == 0)
            {
                showLastPicsState = 2;
            }
        }
        #endregion

        #region LastPics 2 Choosing pic
        if (showLastPicsState == 2)
        {
            if (hud_Group_LastPics.hudControls[curPicIndex+1].controlName != HUDControlName.LvlFlashback_FA_Logo)
            {
                timeCounter = delayBetweenPics;

                hud_Group_LastPics.hudControls[curPicIndex].SetAlpha(0);
                hud_Group_LastPics.hudControls[curPicIndex].SetIsVisible(true);

                hud_Group_LastPics.hudControls[curPicIndex].ShowForAWhile(eachPicShowTime, eachPicAlphaSpeed, eachPicAlphaSpeed);
                curPicIndex++;

                showLastPicsState = 3;
            }
            else
            {
                timeCounter = lastPicTotalTime;

                hud_Group_LastPics.hudControls[curPicIndex].SetAlpha(0);
                hud_Group_LastPics.hudControls[curPicIndex].SetIsVisible(true);

                hud_Group_LastPics.hudControls[curPicIndex].ShowForAWhile(lastPicShowTime, lastPicStartAlphaSpeed, lastPicEndAlphaSpeed);
                showLastPicsState = 4;
            }
        }
        #endregion

        #region LastPics 3 Showing selected Pic
        if (showLastPicsState == 3)
        {
            timeCounter = MathfPlus.DecByDeltatimeToZero(timeCounter);

            if (timeCounter == 0)
                showLastPicsState = 2;
        }
        #endregion

        #region LastPics 4 Showing Last Pic
        if (showLastPicsState == 4)
        {
            timeCounter = MathfPlus.DecByDeltatimeToZero(timeCounter);

            if (timeCounter == 0)
                showLastPicsState = 5;
        }
        #endregion

        #region LastPics 5 Start FA Logo
        if (showLastPicsState == 5)
        {
                timeCounter = FALogoTotalTime;

                HUDControl hudControl_FA = hud_Group_LastPics.GetChildControlByName(HUDControlName.LvlFlashback_FA_Logo);

                hudControl_FA.SetAlpha(0);
                hudControl_FA.SetIsVisible(true);

                hudControl_FA.ShowForAWhile(FALogoShowTime, FALogoStartAlphaSpeed, FALogoEndAlphaSpeed);

                showLastPicsState = 6;
        }
        #endregion

        #region LastPics 6 Showing FA Logo
        if (showLastPicsState == 6)
        {
            timeCounter = MathfPlus.DecByDeltatimeToZero(timeCounter);

            if (timeCounter == 0)
                showLastPicsState = 7;
        }
        #endregion
    }

    void StartDatPack02()
    {
        AnimPack01.SetActiveRecursively(false);
        AnimPack02.SetActiveRecursively(true);

        AnimPack02.animation.Play();
        AnimPack02.animation[AnimPack02.animation.clip.name].time = DatPack02StartingAnimTime;
    }

    void StartDatPack03()
    {
        AnimPack02.SetActiveRecursively(false);
        AnimPack03.SetActiveRecursively(true);

        AnimPack03.animation.Play();
        AnimPack03.animation[AnimPack03.animation.clip.name].time = DatPack03StartingAnimTime;
    }

    void StartDatPack04()
    {
        AnimPack03.SetActiveRecursively(false);
        AnimPack04.SetActiveRecursively(true);

        AnimPack04.animation.Play();
        AnimPack04.animation[AnimPack04.animation.clip.name].time = DatPack04StartingAnimTime;
    }

    void StartDatPack05()
    {
        AnimPack04.SetActiveRecursively(false);
        AnimPack05.SetActiveRecursively(true);

        AnimPack05.animation.Play();
        AnimPack05.animation[AnimPack05.animation.clip.name].time = DatPack05StartingAnimTime;
    }

    void StartDatPack06()
    {
        AnimPack05.SetActiveRecursively(false);
        AnimPack06.SetActiveRecursively(true);

        AnimPack06.animation.Play();
        AnimPack06.animation[AnimPack06.animation.clip.name].time = DatPack06StartingAnimTime;
    }

    void StartDatPack07()
    {
        AnimPack06.SetActiveRecursively(false);
        AnimPack07.SetActiveRecursively(true);

        AnimPack07.animation.Play();
        AnimPack07.animation[AnimPack07.animation.clip.name].time = DatPack07StartingAnimTime;
    }

    void StartDatPack08()
    {
        AnimPack07.SetActiveRecursively(false);
        AnimPack08.SetActiveRecursively(true);

        AnimPack08.animation.Play();
        AnimPack08.animation[AnimPack08.animation.clip.name].time = DatPack08StartingAnimTime;
    }

    void StartDatPack09And10()
    {
        AnimPack08.SetActiveRecursively(false);
        AnimPack09.SetActiveRecursively(true);

        AnimPack09.animation.Play();
        AnimPack09.animation[AnimPack09.animation.clip.name].time = DatPack09StartingAnimTime;

        AnimPack10.SetActiveRecursively(true);

        AnimPack10.animation.Play();
        AnimPack10.animation[AnimPack10.animation.clip.name].time = DatPack10StartingAnimTime;
    }

    void StartShowingLastPics()
    {
        showLastPicsState = 0;
    }

    void StartDecreasingAudioVols()
    {
        isDecreasingVolOfAudInfos = true;
    }
}
