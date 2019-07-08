using UnityEngine;
using System.Collections;

public enum IdleActionTypeEnum
{
    Stand,
    Sit,
}

public class SoldierAction_Idle : SoldierAction
{
    enum StepEnum
    {
        AnimToIdle01,
        AnimToIdle02,
        Idle,
        Damage01,
        Damage02,
    }

    //

    IdleActionTypeEnum idleType;
    StepEnum step;
    AnimsList anims;
    string selectedAnim;
    SoldierDamageAnimPack animPackIdleDamage;
    string selectedDamageAnim;
    DamageInfo dmg;

    float animToAnimIdleCrossfadeTime = 0.4f;
    float animToAnimDmgCrossfadeTime = 0.3f;

    float animToAnimIdleCFTimeFinal;

    //-----------------------------------------------------------------------

    public void InitDefaultParams(IdleActionTypeEnum _type)
    {
        idleType = _type;

        SoldierIdleInfo ii = soldInfo.GetIdleInfoByType(idleType);
        anims = ii.animsIdle;
        animPackIdleDamage = ii.animPackIdleDamage;
    }

    //

    public override void Init(Transform contSoldier)
    {
        base.Init(contSoldier);
    }

    public override void StartAct()
    {
        base.StartAct();
        step = StepEnum.AnimToIdle01;
    }

    public override void UpdateAct()
    {
        base.UpdateAct();


    Start:

        #region AnimToIdle01
        if (step == StepEnum.AnimToIdle01)
        {
            if (needsToBeFinished)
            {
                SetFinished(false);
                return;
            }

            animToAnimIdleCFTimeFinal = animToAnimIdleCrossfadeTime;
            step = StepEnum.AnimToIdle02;
        } 
        #endregion

        #region AnimToIdle02
        if (step == StepEnum.AnimToIdle02)
        {
            if (needsToBeFinished)
            {
                SetFinished(false);
                return;
            }

            if (soldInfo.isDamageRecievedInThisRun)
            {
                dmg = soldInfo.firstDamage;

                if (ShouldTakeDamage(dmg))
                {
                    step = StepEnum.Damage01;
                    goto Start;
                }
            }

            selectedAnim = anims.GetRandomAnimName();
            soldAnimObj.animation[selectedAnim].time = 0;
            soldAnimObj.animation.CrossFade(selectedAnim, animToAnimIdleCFTimeFinal);
            step = StepEnum.Idle;
        } 
        #endregion

        #region Damage01
        if (step == StepEnum.Damage01)
        {
            if (needsToBeFinished)
            {
                SetFinished(false);
                return;
            }

            selectedDamageAnim = animPackIdleDamage.GetRandomAnim(dmg);
            soldAnimObj.animation[selectedDamageAnim].time = 0;
            soldAnimObj.animation.CrossFade(selectedDamageAnim, animToAnimDmgCrossfadeTime);
            step = StepEnum.Damage02;
            goto Finish;
        } 
        #endregion

        #region Damage02
        if (step == StepEnum.Damage02)
        {
            if (needsToBeFinished)
            {
                SetFinished(false);
                return;
            }

            float passedAnimTime = soldAnimObj.animation[selectedDamageAnim].time;
            if (passedAnimTime >= soldAnimObj.animation[selectedDamageAnim].length - animToAnimIdleCrossfadeTime)
            {
                animToAnimIdleCFTimeFinal = soldAnimObj.animation[selectedDamageAnim].length - passedAnimTime;
                step = StepEnum.AnimToIdle02;
                goto Start;
            }

        } 
        #endregion

        #region Idle
        if (step == StepEnum.Idle)
        {
            if (needsToBeFinished)
            {
                SetFinished(true);
                return;
            }

            if (soldInfo.isDamageRecievedInThisRun)
            {
                dmg = soldInfo.firstDamage;

                if (ShouldTakeDamage(dmg))
                {
                    step = StepEnum.Damage01;
                    goto Start;
                }
            }

            float animTime = soldAnimObj.animation[selectedAnim].time;

            if (animTime >= soldAnimObj.animation[selectedAnim].length - animToAnimIdleCrossfadeTime)
            {
                animToAnimIdleCFTimeFinal = soldAnimObj.animation[selectedAnim].length - animTime;
                step = StepEnum.AnimToIdle02;
                goto Start;
            }
        } 
        #endregion

    Finish:
        return;
    }

    public override bool ShouldTakeDamage(DamageInfo dmg)
    {
        if (base.ShouldTakeDamage(dmg))
        {
            return true;
        }

        return false;
    }
}
