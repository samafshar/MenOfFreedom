using UnityEngine;
using System.Collections;

public class MapLogicJob_Anim : MapLogicJob
{
    public LogicJob_Anim_Info anim_Info;

    public float customStartCrossfadeTime = 0;

    //

    string animName = "";

    //

    public void Init_AnimInfo(LogicJob_Anim_Type _animType)
    {
        anim_Info = GameGeneralInfo.Instance.Get_LogicJob_Anim_Info_ByType(_animType);
    }

    public void Init_AnimStartCrossfadeTime(float _time)
    {
        customStartCrossfadeTime = _time;
    }

    public void Init_AnimName(string _animName)
    {
        animName = _animName;
    }

    public override void StartIt()
    {
        base.StartIt();

        shouldStopOnLogicStop = false;

        if (customStartCrossfadeTime <= 0)
            customStartCrossfadeTime = anim_Info.defaultCrossfadeTime;

        if (string.IsNullOrEmpty(animName))
            animName = anim_Info.animsList.GetRandomAnimName();
    }

    public override void RunIt()
    {
        base.RunIt();

        if (step == 1)
        {
            soldInfo.StartNewMainAnimWithCrossfadeTime(animName, customStartCrossfadeTime);
            SetStep(2);
        }

        if (step == 2)
        {
            if (needsToBeFinished)
            {
                SetFinished(true);
            }
        }
    }
}
