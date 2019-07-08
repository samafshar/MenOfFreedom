//<91-04-12>

using UnityEngine;
using System.Collections;

public class MapLogicJob_VoiceCommand : MapLogicJob
{

    public AudioClip[] audioClips;
    public float time_Delay_Min = 3;
    public float time_Delay_Max = 6;
    public bool setDoneAfterOneTalk = true;
    public float startDelayTime = 0;

    //SoldierInfo soldInfo;



    float timeCounter = 0;


    //[HideInInspector]
    //public bool isDone = false;

    public void Init_SetDoneAfterOneTalk(bool _value)
    {
        setDoneAfterOneTalk = _value;
    }

    public void Init_StartDelay(float _delayTime)
    {
        startDelayTime = _delayTime;
    }

    public override void StartIt()
    {
        base.StartIt();

        shouldStopOnLogicStop = false;

        //soldInfo = controlledSoldier.GetComponent<SoldierInfo>();
    }

    public override void RunIt()
    {
        base.RunIt();

        if (step == 1)
        {
            if (needsToBeFinished)
            {
                if (!soldInfo.IsVoiceOnBusyTimer())
                {
                    SetFinished(false);
                    return;
                }
            }

            if (startDelayTime > 0)
            {
                startDelayTime = MathfPlus.DecByDeltatimeToZero(startDelayTime);
            }

            if (startDelayTime == 0)
            {
                if (!soldInfo.IsVoiceOnBusyTimer())
                {
                    soldInfo.PlayVoiceWithAdditionalBusyTime(audioClips, time_Delay_Min, time_Delay_Max);
                    if (setDoneAfterOneTalk)
                    {
                        timeCounter = soldInfo.voiceBusyTimeCounter;
                        step = 2;
                    }
                    return;
                }
            }
        }

        if (step == 2)
        {
            timeCounter = MathfPlus.DecByDeltatimeToZero(timeCounter);
            if (timeCounter == 0)
            {
                SetFinished(true);
                return;
            }
        }
    }

    //public void SetDone()
    //{
    //    step = 1000;

    //    isDone = true;
    //}

}
