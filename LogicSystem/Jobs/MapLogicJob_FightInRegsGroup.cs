using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapLogicJob_FightInRegsGroup : MapLogicJob
{
    public MapLogicJob_FightInReg[] fightInRegs;

    public CriticalArea[] criticalAreas;

    public float initialDelay = 0;

    public string __Grenade__ = "___________________________";

    public bool grenadeEnabled = true;
    public int grenadeCount = 0;
    public float grenadeStartDelayTimeMin = 10;
    public float grenadeStartDelayTimeMax = 30;
    public float grenadeNextDelayTimeMin = 15;
    public float grenadeNextDelayTimeMax = 45;
    public float grenadeTimeCoefForEveryFightInReg = 0.92f;

    [HideInInspector]
    public int countOfRemainingSolds = 0;

    float grenadeDelayTimeCounter = 0f;

    float childGrenadeCheckMaxTime = 0.5f;
    float childGrenadeCheckTimeCounter = 0.5f;


    public override void StartIt()
    {
        base.StartIt();

        foreach (MapLogicJob_FightInReg fInReg in fightInRegs)
        {
            fInReg.ownerGroup = this;

            if (fInReg.controlledSoldier != null || fInReg.initialSoldier!=null)
            {
                if (fInReg.fightReg.respawnPointCollection == null || (fInReg.fightReg.respawnPointCollection != null && fInReg.fightReg.respawnPointCollection.RespawnPointType == RespawnPointTypeEnum.Counter))
                {
                    countOfRemainingSolds++;
                }
            }

            if (fInReg.fightReg.respawnPointCollection != null && fInReg.fightReg.respawnPointCollection.RespawnPointType == RespawnPointTypeEnum.Counter)
            {
                countOfRemainingSolds += (int)(fInReg.fightReg.respawnPointCollection.MaxCount);
            }
        }

        foreach (CriticalArea critArea in criticalAreas)
        {
            if (critArea != null)
            {
                critArea.Init_SetRelatedFightInRegsGroup(this);
                critArea.StartIt();
            }
        }

        grenadeDelayTimeCounter = Random.Range(grenadeStartDelayTimeMin, grenadeStartDelayTimeMax) + initialDelay;
    }

    public override void RunIt()
    {
        base.RunIt();

    StartSteps:

        if (step == 1)
        {
            foreach (MapLogicJob_FightInReg fInReg in fightInRegs)
            {
                fInReg.Init_AddInitialDelay(initialDelay);
                fInReg.StartIt();
            }

            SetStep(2);
        }

        if (step == 2)
        {
            if (needsToBeFinished)
            {
                foreach (MapLogicJob_FightInReg fInReg in fightInRegs)
                {
                    SetChildNeedsToBeFinished(fInReg);
                }

                SetStep(3);
                goto StartSteps;
            }

            foreach (MapLogicJob_FightInReg fInReg in fightInRegs)
            {
                fInReg.RunIt();
            }
        }

        if (step == 3)
        {
            //<Alpha>
            if (needsToBeFinished)
            {
                foreach (MapLogicJob_FightInReg fInReg in fightInRegs)
                {
                    SetChildNeedsToBeFinished(fInReg);
                }
            }
            //</Alpha>

            foreach (MapLogicJob_FightInReg fInReg in fightInRegs)
            {
                fInReg.RunIt();
            }

            bool allFinished = true;

            foreach (MapLogicJob_FightInReg fInReg in fightInRegs)
            {
                if (fInReg.status != LogicJobStatus.Finished)
                    allFinished = false;
            }

            if (allFinished)
            {
                SetFinished(true);
                return;
            }
        }

    EndSteps: ;

        if (grenadeEnabled)
        {
            if (grenadeCount > 0)
            {
                grenadeDelayTimeCounter = MathfPlus.DecByDeltatimeToZero(grenadeDelayTimeCounter);

                if (IsReadyToSetAChildFightRegGrenade())
                {
                    SlowlyTrySetAChildFightRegGrenade();
                }
            }
        }
    }

    public void ResetCounterOfCreatedSoldiers()
    {
        foreach (MapLogicJob_FightInReg fInReg in fightInRegs)
        {
            fInReg.ResetCounterOfCreatedSoldiers();
        }
    }

    public int GetNumOfCreatedSoldiers()
    {
        int i = 0;

        foreach (MapLogicJob_FightInReg fInReg in fightInRegs)
        {
            i += fInReg.counterOfCreatedSoldiers;
        }

        return i;
    }

    public void StopCreatingMoreSoldiers()
    {
        foreach (MapLogicJob_FightInReg fInReg in fightInRegs)
        {
            fInReg.StopCreatingMoreSoldiers();
        }

        EndCriticalAreas();
    }

    public void StopCreatingMoreSoldiersAndMakeAliveSoldiersSoWeak()
    {
        foreach (MapLogicJob_FightInReg fInReg in fightInRegs)
        {
            fInReg.StopCreatingMoreSoldiersAndMakeAliveSoldierSoWeak();
        }

        EndCriticalAreas();
    }

    void EndCriticalAreas()
    {
        foreach (CriticalArea critArea in criticalAreas)
        {
            if (critArea != null)
            {
                critArea.EndIt();
            }
        }
    }

    public bool IsCreatingSoldiersStoppedAndAllSoldsDead()
    {
        foreach (MapLogicJob_FightInReg fInReg in fightInRegs)
        {
            if (!fInReg.IsCreatingSoldiersStoppedAndAllSoldsDead())
                return false;
        }

        return true;
    }

    public void DecreaseCountOfCreatedSoldiers_ForCountRespawnStyle()
    {
        countOfRemainingSolds--;
    }

    public override void SetFinished(bool _isFinishedOK)
    {
        base.SetFinished(_isFinishedOK);

        foreach (CriticalArea critArea in criticalAreas)
        {
            if (critArea != null)
            {
                critArea.EndIt();
            }
        }
    }

    //public MapLogicJob_FightInReg SelectBestFightInRegForGrenade()
    //{
    //    List<MapLogicJob_FightInReg> okRegs = new List<MapLogicJob_FightInReg>();

    //    foreach (MapLogicJob_FightInReg fInReg in fightInRegs)
    //    {

    //    }
    //}

    public bool IsReadyToSetAChildFightRegGrenade()
    {
        if (grenadeDelayTimeCounter > 0)
            return false;

        if (!grenadeEnabled)
            return false;

        if (grenadeCount == 0)
            return false;

        return true;
    }

    void SlowlyTrySetAChildFightRegGrenade()
    {
        childGrenadeCheckTimeCounter = MathfPlus.DecByDeltatimeToZero(childGrenadeCheckTimeCounter);

        if (childGrenadeCheckTimeCounter == 0)
        {
            childGrenadeCheckTimeCounter = childGrenadeCheckMaxTime;
            TrySetAChildFightRegGrenade();
        }
    }

    void TrySetAChildFightRegGrenade()
    {
        foreach (MapLogicJob_FightInReg fInReg in fightInRegs)
        {
            if(IsChildFightRegReadyForSettingGrenade(fInReg))
            {
                SetChildFightInRegGrenade(fInReg);
                return;
            }
        }
    }

    bool IsChildFightRegReadyForSettingGrenade(MapLogicJob_FightInReg _childFInReg)
    {
        MapLogicJob_FightInReg childFInReg = _childFInReg;

        if (!childFInReg.grenadeIsControlledByOwnerGroup)
            return false;

        if (childFInReg.nowReadyForGreandeLaunch)
            return false;

        if (childFInReg.isOwnerRequestedAGrenade)
            return false;

        if (!childFInReg.CanSetReadyForLaunchGrenade())
            return false;

        return true;
    }

    public void SetChildFightInRegGrenade(MapLogicJob_FightInReg _childFInReg)
    {
        MapLogicJob_FightInReg childFInReg = _childFInReg;

        grenadeDelayTimeCounter = Random.Range(grenadeNextDelayTimeMin, grenadeNextDelayTimeMax);

        if (fightInRegs.Length > 1)
        {
            for (int i = 1; i < fightInRegs.Length; i++)
            {
                grenadeDelayTimeCounter *= grenadeTimeCoefForEveryFightInReg;
            }
        }

        grenadeCount--;
        grenadeCount = Mathf.Clamp(grenadeCount, 0, int.MaxValue);

        childFInReg.SetItsNowReadyForLaunchGrenade();
        childFInReg.SetOwnerRequestedAGrenade();
    }

    public void SetAChildFinishedWithoutThrowingRequestedGrenade()
    {
        grenadeCount++;
    }
}
