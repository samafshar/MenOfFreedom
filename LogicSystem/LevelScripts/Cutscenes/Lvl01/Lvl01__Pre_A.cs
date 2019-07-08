using UnityEngine;
using System.Collections;

public class Lvl01__Pre_A : MonoBehaviour
{
    float timeCounter = 0;
    float timeToShowPejakStudioText = 11f;
    float timeToShowPresentsText = 12.2f;
    float textsDuration = 3.0f;

    float textsStartAlphaSpeed = 1.1f;
    float textsEndAlphaSpeed = 0.9f;

    bool isShowingPejvakStudioTextStarted = false;
    bool isShowingPresentsStudioTextStarted = false;

    HUDControl hudControl_PejvakStudio;
    HUDControl hudControl_Presents;

    bool isInited = false;

    void Start()
    {

    }

    void Update()
    {
        if (!isInited)
        {
            isInited = true;

            hudControl_PejvakStudio = MapLogic.Instance.mapHUDParent.GetChildGroupByName(HUDGroupName.LvlFlashback).GetChildControlByName(HUDControlName.LvlFlashback_FA_PejvakStudio);
            hudControl_Presents = MapLogic.Instance.mapHUDParent.GetChildGroupByName(HUDGroupName.LvlFlashback).GetChildControlByName(HUDControlName.LvlFlashback_FA_Presents);
        }
        else
        {
            timeCounter += Time.deltaTime;

            if (!isShowingPejvakStudioTextStarted)
            {
                if (timeCounter >= timeToShowPejakStudioText)
                {
                    isShowingPejvakStudioTextStarted = true;

                    hudControl_PejvakStudio.SetIsVisible(true);
                    hudControl_PejvakStudio.ShowForAWhile(textsDuration, textsStartAlphaSpeed, textsEndAlphaSpeed);
                }
            }

            if (!isShowingPresentsStudioTextStarted)
            {
                if (timeCounter >= timeToShowPresentsText)
                {
                    isShowingPresentsStudioTextStarted = true;

                    hudControl_Presents.SetIsVisible(true);
                    hudControl_Presents.ShowForAWhile(textsDuration - (timeToShowPresentsText - timeToShowPejakStudioText), textsStartAlphaSpeed, textsEndAlphaSpeed);
                }
            }
        }
    }

    //
}
