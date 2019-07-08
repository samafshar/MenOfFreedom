using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ActMachineGunStep
{
    Start01,
    Start02,
    GoingToPos01,
    GoingToPos02,
    FirstSoldierRotation01,
    FirstSoldierRotation02,
    WaitForFirstAnim,
    UsingMachineGun01_Init,
    UsingMachineGun02_Restart,
    UsingMachineGun03_Using,
    Finishing_MovementAct01,
    Finishing_MovementAct02,
    Finishing_UsingMachineGun01,
    Finishing_UsingMachineGun02,
}

public class SoldierAction_MachineGun : SoldierAction
{

    ActMachineGunStep step;

    SoldierAction_Movement movementAct;

    float maxAngle = 70;

    float range;

    float currentAngle = 0;

    float midWeight = 1f;
    float leftWeight = 0f;
    float rightWeight = 0f;

    float rotateSpeed = 200;

    float machineGunShootCounter;

    float shootDurationMin = 10;
    float shootDurationMax = 15;
    float shootDurationCounter;

    float idleTimeMin = 0.5f;
    float idleTimeMax = 1;
    float idleTimeCounter;

    float lockTargetMin = 1;
    float lockTargetMax = 2.5f;
    float lockTargetCounter;

    float recheckTargetMin = 0.3f;
    float recheckTargetMax = 0.3f;
    float recheckTargetCounter;

    public List<GameObject> initialEnemies = null;

    SoldierMachineGun machineGun;

    SoldierMachineGunInfo machineGunInfo;

    MovementTypeEnum currentMovementType = MovementTypeEnum.RunFast;

    string anim_SoldierOnMachineGun_Mid = "";
    string anim_SoldierOnMachineGun_Left = "";
    string anim_SoldierOnMachineGun_Right = "";

    CharRaycastResult target = null;

    bool isIdle = false;

    bool setSoldierExactPos = false;

    //

    public void Init_MachineGun(SoldierMachineGun _machineGun)
    {
        machineGun = _machineGun;

        machineGunInfo = soldInfo.soldierGeneralInfo.GetMachineGunInfoByType(machineGun.machineGunType);

        //machineGun.gun.Set_ControlledSoldier(controlledSoldier.gameObject);

        anim_SoldierOnMachineGun_Mid = machineGunInfo.animsList_SoldierOnMachineGun_Mid.GetRandomAnimName();
        anim_SoldierOnMachineGun_Left = machineGunInfo.animsList_SoldierOnMachineGun_Left.GetRandomAnimName();
        anim_SoldierOnMachineGun_Right = machineGunInfo.animsList_SoldierOnMachineGun_Right.GetRandomAnimName();
    }

    public void Init_MovementType(MovementTypeEnum _movementType)
    {
        currentMovementType = _movementType;
    }

    //

    public override void Init(Transform contSoldier)
    {
        base.Init(contSoldier);

        range = soldInfo.fightRange;
    }

    public override void StartAct()
    {
        base.StartAct();

        step = ActMachineGunStep.Start01;
    }

    public override void UpdateAct()
    {
        base.UpdateAct();

        if (setSoldierExactPos)
        {
            Vector3 newPos = Vector3.Lerp(controlledSoldier.position, machineGun.standPosTr.position, Time.deltaTime * 8);
            controlledSoldier.position = new Vector3(newPos.x, controlledSoldier.position.y, newPos.z);
        }

    StartSteps:

        #region Start01
        if (step == ActMachineGunStep.Start01)
        {
            //if (IsSoldOnPos(machineGun.standPosTr.position))
            //{
            //    step = ActMachineGunStep.FirstSoldierRotation01;
            //    goto StartSteps;
            //}
            //else
            //{

            step = ActMachineGunStep.GoingToPos01;
            goto StartSteps;

            //}
        }
        #endregion

        #region GoingToPos01

        if (step == ActMachineGunStep.GoingToPos01)
        {
            movementAct = controlledSoldier.gameObject.AddComponent<SoldierAction_Movement>();
            movementAct.Init(controlledSoldier);
            movementAct.InitDefaultParams(currentMovementType);
            movementAct.Init_PosToFindPath(machineGun.standPosTr.position);
            movementAct.initialEnemies = initialEnemies;

            movementAct.SetNextActAnimToCrossfade(anim_SoldierOnMachineGun_Mid);
            movementAct.SetEndingRotNormal(machineGun.transform.forward);

            movementAct.StartAct();

            step = ActMachineGunStep.GoingToPos02;
        }
        #endregion

        #region GoingToPos02
        if (step == ActMachineGunStep.GoingToPos02)
        {
            if (needsToBeFinished)
            {
                step = ActMachineGunStep.Finishing_MovementAct01;
                goto StartSteps;
            }

            if (movementAct.finishReport == FinishReportEnum.FinishedOK)
            {
                Destroy(movementAct);

                step = ActMachineGunStep.WaitForFirstAnim;
                goto StartSteps;
            }
        }
        #endregion

        #region WaitForFirstAnim
        if (step == ActMachineGunStep.WaitForFirstAnim)
        {
            if (needsToBeFinished)
            {
                SetFinished(true);
                return;
            }

            if (soldInfo.IsFullyInNewMainAnim())
                step = ActMachineGunStep.UsingMachineGun01_Init;
        }
        #endregion

        #region UsingMachineGun01_Init
        if (step == ActMachineGunStep.UsingMachineGun01_Init)
        {
            machineGun.SetControllingSoldier(controlledSoldier.gameObject);

            setSoldierExactPos = true;

            StartAnims();

            machineGunShootCounter = machineGun.shootSpeed;

            ResetShootDuration();
            ResetIdleTime();

            step = ActMachineGunStep.UsingMachineGun02_Restart;
        }
        #endregion

        #region UsingMachineGun02_Restart
        if (step == ActMachineGunStep.UsingMachineGun02_Restart)
        {
            ReselectTarget();
            ResetLockTargetTime();
            ResetRecheckTargetTime();

            step = ActMachineGunStep.UsingMachineGun03_Using;
        }
        #endregion

        #region UsingMachineGun03_Using
        if (step == ActMachineGunStep.UsingMachineGun03_Using)
        {
            if (needsToBeFinished)
            {
                step = ActMachineGunStep.Finishing_UsingMachineGun01;
                goto EndSteps;
            }

            lockTargetCounter = MathfPlus.DecByDeltatimeToZero(lockTargetCounter);

            recheckTargetCounter = MathfPlus.DecByDeltatimeToZero(recheckTargetCounter);

            if (lockTargetCounter == 0)
            {
                step = ActMachineGunStep.UsingMachineGun02_Restart;
                return;
            }

            if (recheckTargetCounter == 0)
            {
                ResetRecheckTargetTime();

                if (!mapLogic.IsCharacterOkAsTarget(gameObject, target.character, machineGun.raycastStartTr.position, machineGun.transform.rotation, range, -maxAngle, maxAngle, out target))
                    ReselectTarget();
            }

            if (!isIdle)
            {
                shootDurationCounter = MathfPlus.DecByDeltatimeToZero(shootDurationCounter);

                if (shootDurationCounter == 0)
                {
                    ResetShootDuration();
                    isIdle = true;
                }

            }
            else
            {
                idleTimeCounter = MathfPlus.DecByDeltatimeToZero(idleTimeCounter);

                if (idleTimeCounter == 0)
                {
                    ResetIdleTime();
                    isIdle = false;
                }
            }

            bool needsToShoot = false;

            if (mapLogic.IsCharacterTotallyFightable(target.character))
            {
                if (GentlySetAngleToTarget())
                {
                    if (!isIdle)
                    {
                        needsToShoot = true;
                    }
                }
            }

            if (needsToShoot)
            {
                //machineGunShootCounter = MathfPlus.DecByDeltatimeToZero(machineGunShootCounter);

                //if (machineGunShootCounter == 0)
                //{
                //    machineGunShootCounter = machineGun.shootSpeed;
                if (machineGun.gun.IsReady())
                {
                    Vector3 targetFirePos;

                    List<Transform> okPoses;

                    if (target.isCharacterHitted)
                    {
                        okPoses = target.characterHittedPoses;
                    }
                    else
                    {
                        okPoses = target.haloHittedPoses;
                    }

                    if (okPoses.Count > 0)
                    {
                        targetFirePos = okPoses[Random.Range(0, okPoses.Count)].position;

                        machineGun.gun.TryFire(machineGun.shootPosTr.position, targetFirePos);

                        //Vector3 bulletDir = targetFirePos - machineGun.shootPosTr.position;

                        //GameObject bulObj = GameObject.Instantiate(machineGun.bullet.gameObject, machineGun.shootPosTr.position, Quaternion.LookRotation(bulletDir)) as GameObject;
                        //Bullet bul = bulObj.GetComponent<Bullet>();
                        //bul.InitBulletProp(bulletDir, gameObject);

                        ////soundPlay.PlaySound_FX(fireSound);
                    }
                }
                //}
            }

            SetActiveAnims();

            SetMachineGunRotatingObjectAngle();
        }
        #endregion

        #region Finishing_MovementAct01
        if (step == ActMachineGunStep.Finishing_MovementAct01)
        {
            movementAct.SetNeedsToBeFinished(evenStopMovingForFinish);
            step = ActMachineGunStep.Finishing_MovementAct02;
            goto EndSteps;
        }
        #endregion

        #region Finishing_MovementAct02
        if (step == ActMachineGunStep.Finishing_MovementAct02)
        {
            //<Alpha>
            if(needsToBeFinished)
                movementAct.SetNeedsToBeFinished(evenStopMovingForFinish);
            //</Alpha>

            if (movementAct.status == ActionStatusEnum.Finished)
            {
                SetFinished(false);
                return;
            }
        }
        #endregion

        #region Finishing_UsingMachineGun01
        if (step == ActMachineGunStep.Finishing_UsingMachineGun01)
        {
            machineGun.SetControllingSoldier(null);
            step = ActMachineGunStep.Finishing_UsingMachineGun02;
        }
        #endregion

        #region Finishing_UsingMachineGun02
        if (step == ActMachineGunStep.Finishing_UsingMachineGun02)
        {
            float endWeightSpeed = machineGun.endRotateTimeCoef;

            midWeight += Time.deltaTime * endWeightSpeed;
            midWeight = Mathf.Clamp01(midWeight);

            leftWeight -= Time.deltaTime * endWeightSpeed;
            leftWeight = Mathf.Clamp01(leftWeight);


            rightWeight -= Time.deltaTime * endWeightSpeed;
            rightWeight = Mathf.Clamp01(rightWeight);

            if (midWeight == 1
                &&
                leftWeight == 0
                &&
                rightWeight == 0)
            {
                EndAnims();
                SetFinished(true);
                return;
            }
        }
        #endregion

    EndSteps: ;
    }

    public override void SetFinished(bool OK)
    {
        base.SetFinished(OK);

        machineGun.SetControllingSoldier(null);
    }

    //

    bool IsSoldOnPos(Vector3 _pos)
    {
        return SoldierStats.IsSoldierOnPos(controlledSoldier.gameObject, _pos);
    }

    void ResetShootDuration()
    {
        shootDurationCounter = Random.Range(shootDurationMin, shootDurationMax);
    }

    void ResetIdleTime()
    {
        idleTimeCounter = Random.Range(idleTimeMin, idleTimeMax);
    }

    void ResetLockTargetTime()
    {
        lockTargetCounter = Random.Range(lockTargetMin, lockTargetMax);
    }

    void ResetRecheckTargetTime()
    {
        recheckTargetCounter = Random.Range(recheckTargetMin, recheckTargetMax);
    }

    void ReselectTarget()
    {
        List<CharRaycastResult> okTargets = GetOkTargets();

        if (okTargets.Count > 0)
        {
            target = okTargets[0];
            return;
        }

        target = new CharRaycastResult();
    }

    List<CharRaycastResult> GetOkTargets()
    {
        Vector3 rayCastPos = machineGun.raycastStartTr.position;
        Quaternion raycastPosRot = machineGun.transform.rotation;

        List<CharRaycastResult> result;

        if (initialEnemies != null && initialEnemies.Count > 0)
        {
            result = mapLogic.GetAttackableCharsFromList(initialEnemies, controlledSoldier.gameObject, rayCastPos, raycastPosRot, range, -maxAngle, maxAngle);
        }
        else
        {
            result = mapLogic.GetAttackableEnemies(controlledSoldier.gameObject, rayCastPos, raycastPosRot, range, -maxAngle, maxAngle);
        }

        result = mapLogic.RateEnemiesAndSort(result, controlledSoldier.gameObject, rayCastPos);

        return result;
    }


    float GetAngleToTarget()
    {
        if (!mapLogic.IsCharacterTotallyFightable(target.character))
            return 0;

        return MathfPlus.GetDeltaAngle(machineGun.transform.forward, machineGun.transform.position, target.character.transform.position);
    }

    bool GentlySetAngleToTarget()
    {
        if (!mapLogic.IsCharacterTotallyFightable(target.character))
        {
            return false;
        }

        float deltaAngle = GetAngleToTarget();

        float rotAmount = rotateSpeed * Time.deltaTime;

        if (Mathf.Abs(rotAmount) >= Mathf.Abs(deltaAngle - currentAngle))
        {
            //currentAngle = deltaAngle;

            currentAngle = Mathf.Clamp(deltaAngle, -maxAngle, maxAngle);

            SetRotateWights();

            return true;
        }

        float dif = deltaAngle - currentAngle;

        if (dif >= 0)
        {
            currentAngle += rotAmount;
        }
        else
        {
            currentAngle -= rotAmount;
        }

        SetRotateWights();

        return false;
    }

    void SetRotateWights()
    {
        midWeight = Mathf.Clamp01((maxAngle - Mathf.Abs(currentAngle)) / maxAngle);

        if (currentAngle >= 0)
            leftWeight = 0;
        else
            leftWeight = Mathf.Clamp01(Mathf.Abs(currentAngle) / maxAngle);

        if (currentAngle <= 0)
            rightWeight = 0;
        else
            rightWeight = Mathf.Clamp01(currentAngle / maxAngle);
    }

    void SetActiveAnims()
    {
        soldInfo.animObject.animation[anim_SoldierOnMachineGun_Mid].weight = midWeight;
        soldInfo.animObject.animation[anim_SoldierOnMachineGun_Left].weight = leftWeight;
        soldInfo.animObject.animation[anim_SoldierOnMachineGun_Right].weight = rightWeight;

        soldInfo.animObject.animation[anim_SoldierOnMachineGun_Mid].enabled = true;
        soldInfo.animObject.animation[anim_SoldierOnMachineGun_Left].enabled = true;
        soldInfo.animObject.animation[anim_SoldierOnMachineGun_Right].enabled = true;
    }

    void StartAnims()
    {
        soldInfo.animObject.animation[anim_SoldierOnMachineGun_Mid].weight = 1;
        soldInfo.animObject.animation[anim_SoldierOnMachineGun_Left].weight = 0;
        soldInfo.animObject.animation[anim_SoldierOnMachineGun_Right].weight = 0;

        soldInfo.animObject.animation[anim_SoldierOnMachineGun_Mid].enabled = true;
        soldInfo.animObject.animation[anim_SoldierOnMachineGun_Left].enabled = true;
        soldInfo.animObject.animation[anim_SoldierOnMachineGun_Right].enabled = true;

        soldInfo.animObject.animation[anim_SoldierOnMachineGun_Mid].layer = 3;
        soldInfo.animObject.animation[anim_SoldierOnMachineGun_Left].layer = 3;
        soldInfo.animObject.animation[anim_SoldierOnMachineGun_Right].layer = 3;
    }

    void EndAnims()
    {
        soldInfo.animObject.animation[anim_SoldierOnMachineGun_Mid].weight = 1;
        soldInfo.animObject.animation[anim_SoldierOnMachineGun_Left].weight = 0;
        soldInfo.animObject.animation[anim_SoldierOnMachineGun_Right].weight = 0;

        soldInfo.animObject.animation[anim_SoldierOnMachineGun_Mid].enabled = true;
        soldInfo.animObject.animation[anim_SoldierOnMachineGun_Left].enabled = false;
        soldInfo.animObject.animation[anim_SoldierOnMachineGun_Right].enabled = false;

        soldInfo.animObject.animation[anim_SoldierOnMachineGun_Mid].layer = 0;
        soldInfo.animObject.animation[anim_SoldierOnMachineGun_Left].layer = 0;
        soldInfo.animObject.animation[anim_SoldierOnMachineGun_Right].layer = 0;
    }

    void SetMachineGunRotatingObjectAngle()
    {
        machineGun.SetRotatingObjectAngle(currentAngle);
    }
}
