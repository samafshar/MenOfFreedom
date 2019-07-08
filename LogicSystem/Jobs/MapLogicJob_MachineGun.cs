using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class MapLogicJob_MachineGun : MapLogicJob
{

    public GameObject initialSoldier;
    public SoldierMachineGun machineGun;

    public MovementTypeEnum movementType = MovementTypeEnum.RunFast;

    public float initialDelay = 0; //new

    SoldierAction_MachineGun actMachineGun;

    public RespawnPointCollection respawnPointCollection = null; //new

    public string __CustomProperties_________________________ = "_______________________________";
    public bool customPropsAreUsed = false;
    public float customRecievingDamageCoef = 1;
    public bool onlyGetDamageFromPlayer = false;
    public LogicTrigger gettingDamageArea;

    //[HideInInspector]
    //public GameObject currentSoldier; //new

    RespawnPoint_New[] rPs;

    bool canAddGettingDamageAreaToNewSolds = true;

    public override void StartIt()
    {
        base.StartIt();

        //if (customPropsAreUsed)
        //{
        //    if (gettingDamageArea)
        //        gettingDamageArea.SetEnabled(true);
        //}

        if (initialSoldier != null)
            Init_SetControlledSoldier(initialSoldier);

        if (respawnPointCollection != null)
        {
            respawnPointCollection.Init_SetInitialDelay(initialDelay);
            respawnPointCollection.Init_Start();
        }

        if (controlledSoldier != null)
        {
            //currentSoldier = controlledSoldier;

            if (respawnPointCollection != null)
                respawnPointCollection.SetInitialSoldier(controlledSoldier);
        }
    }

    public override void RunIt()
    {
        base.RunIt();

    StartSteps:

        #region 1 Start
        if (step == 1)
        {
            if (needsToBeFinished)
            {
                SetStep(2);
                goto StartSteps;
            }

            if (controlledSoldier != null)
                AddMachineGunActToSoldier(controlledSoldier);

            SetStep(1.1f);
        }
        #endregion

        #region 1.1 Run
        if (step == 1.1f)
        {
            if (needsToBeFinished)
            {
                SetStep(2);
                goto StartSteps;
            }

            if (respawnPointCollection != null)
            {
                if (IsCreatingSoldiersStoppedAndAllSoldsDead())
                {
                    SetFinished(true);
                    return;
                }

                if (respawnPointCollection.IsReady())
                    CreateAndInitSoldier();
            }
        }
        #endregion

        #region 2 Start Finishing
        if (step == 2) //StartFinishing
        {
            StopCreatingMoreSoldiers();

            if (!GeneralStats.IsCharacterAlive(controlledSoldier))
            {
                SetFinished(true);
                return;
            }

            if (actMachineGun == null)
            {
                SetFinished(true);
                return;
            }

            actMachineGun.SetNeedsToBeFinished(evenStopMovingForFinish);

            SetStep(2.1f);
        }
        #endregion

        #region 2.1 Check Finished
        if (step == 2.1f) //Check Finished
        {
            if (!GeneralStats.IsCharacterAlive(controlledSoldier))
            {
                SetFinished(true);
                return;
            }
            else
            {
                //<Alpha>
                if (needsToBeFinished)
                    actMachineGun.SetNeedsToBeFinished(evenStopMovingForFinish);
                //</Alpha>

                if (actMachineGun.status == SoldierAction.ActionStatusEnum.Finished)
                {
                    SetFinished(true);
                    return;
                }
            }
        }
        #endregion
    }

    void CreateAndInitSoldier()
    {
        RespawnPointCollection resPointCol = respawnPointCollection;

        GameObject sold = resPointCol.CreateSoldier();

        Init_SetControlledSoldier(sold);

        AddMachineGunActToSoldier(controlledSoldier);
    }

    void AddMachineGunActToSoldier(GameObject sold)
    {
        actMachineGun = sold.AddComponent<SoldierAction_MachineGun>();

        actMachineGun.Init(sold.transform);
        actMachineGun.Init_MachineGun(machineGun);
        actMachineGun.Init_MovementType(movementType);

        actMachineGun.StartAct();

        if (customPropsAreUsed)
        {
            soldCharInfo.SetRecievedDamageCoef(customRecievingDamageCoef);

            soldInfo.SetShouldOnlyTakeDamageFromPlayer(onlyGetDamageFromPlayer);

            if (gettingDamageArea)
                if (canAddGettingDamageAreaToNewSolds)
                    soldInfo.SetGettingDamageArea(gettingDamageArea);
        }
    }

    public void StopCreatingMoreSoldiers()
    {
        if (respawnPointCollection != null)
        {
            respawnPointCollection.SetFinished();
        }
    }

    public bool IsCreatingSoldiersStoppedAndAllSoldsDead()
    {
        if (respawnPointCollection != null)
        {
            if (!respawnPointCollection.isFinished)
            {
                return false;
            }
        }

        if (controlledSoldier != null)
        {
            if (!soldCharInfo.IsDeadOrDisabled())
                return false;
        }

        return true;
    }

    public override void SetFinished(bool _isFinishedOK)
    {
        base.SetFinished(_isFinishedOK);

        if (controlledSoldier != null)
        {
            if (actMachineGun != null)
                Destroy(actMachineGun);
        }

        if (customPropsAreUsed)
        {
            if (gettingDamageArea)
            {
                //gettingDamageArea.SetEnabled(false);

                if (soldInfo != null)
                    soldInfo.SetGettingDamageArea(null);
            }
        }
    }

    void Update()
    {
        DrawDebugLines();
    }

    void DrawDebugLines()
    {
        rPs = transform.GetComponentsInChildren<RespawnPoint_New>();

        if (rPs != null && rPs.Length > 1)
        {
            for (int i = 0; i < rPs.Length - 1; i++)
            {
                Debug.DrawLine(rPs[i].transform.position, rPs[i + 1].transform.position, new Color(0.88f, 0.71f, 0.18f));
            }
        }

        if (rPs != null && rPs.Length > 0)
        {
            Debug.DrawLine(machineGun.transform.position, rPs[0].transform.position, new Color(0.88f, 0.71f, 0.18f));
        }
    }

    public void DisableGettingDamageAreaIfExists()
    {
        if (customPropsAreUsed)
        {
            //if (gettingDamageArea)
            //    gettingDamageArea.SetEnabled(false);

            if (soldInfo != null)
                soldInfo.SetGettingDamageArea(null);

            canAddGettingDamageAreaToNewSolds = false;
        }
    }
}
