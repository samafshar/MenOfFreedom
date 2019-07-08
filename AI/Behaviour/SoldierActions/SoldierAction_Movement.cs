//<91-04-12>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum MovementTypeEnum
{
    Walk,
    RunNormal,
    RunFast,
}

public class SoldierAction_Movement : SoldierAction
{
    enum VChangeStepEnum
    {
        accFirst01,
        accFirst02,
        mid01,
        mid02,
        stopping01,
        stopping02,
        stopping03,
        accEnd01,
        accEnd02,
        accEnd03,
    }
    enum SoldStatusEnum
    {
        FindingPath_FirstCheck,
        FindingPath_StartAStar,
        FindingPath_WaitingForAStarResult,
        FindingPath_WaitingForRestartAStar,
        MoveInit01,
        StartingNormal01,
        StartingNormal02,
        MovingNormal01,
        MovingNormal02,
        StoppingNormal01,
        StoppingNormal02,
        EndingNormal01,
        EndingNormal02,
        EndingNormal03,
        GettingDamageNormal01,
        GettingDamageNormal02,
        //mf
        MovingNormalToMoveFight01,
        MovingNormalToMoveFight02,
        MoveFightToMovingNormal01,
        MoveFightToMovingNormal02,
        MoveFight01,
        MoveFight02,
        //MoveFight_Moving01,
        //MoveFight_Moving02,
        MoveFight_ShootIdle01,
        MoveFight_ShootIdle02,
        MoveFight_MovingToShootIdle01,
        MoveFight_MovingToShootIdle02,
        MoveFight_ShootIdleToMoving01,
        MoveFight_ShootIdleToMoving02,

        MoveFight_Damage01,
        MoveFight_Damage02,
        MoveFight_ShootIdleToDamage01,
        MoveFight_ShootIdleToDamage02,
        MoveFight_DamageToShootIdle01,
        MoveFight_DamageToShootIdle02,

        MoveFight_Reload01,
        MoveFight_Reload02,
        MoveFight_ShootIdleToReload01,
        MoveFight_ShootIdleToReload02,
        MoveFight_ReloadToShootIdle01,
        MoveFight_ReloadToShootIdle02,

        MoveFight_Shoot01,
        MoveFight_Shoot02,
        //~mf

        EarlyEnd01,
        EarlyEnd02,
    }

    //----------------------------------------------------------------------------------------

    // /////////

    public MovementTypeEnum movementType = MovementTypeEnum.RunFast;

    //public float rotSpeedUpperBody = 450;
    public float rotSpeedAllBody = 320;

    public float maxRotOnFrame = 25;

    public float v0 = 0;
    public float vMid = 8;
    public float vEnd = 0;

    public float tFirstAcc = 1f;
    public float tEndAcc = 1f;

    public float startMovingCrossfadeTime = 0.8f;
    public float endMovingCrossfadeTime = 0.8f;

    public List<GameObject> initialEnemies;

    public string anim_Start = "";
    public string anim_Move = "";
    public string anim_End = "";

    //public Transform soldierRoot;
    //public Transform soldierUpperBodyRoot;
    //public Transform soldierGunRoot;

    // ////////

    //Just get (No set)

    public float remainingPathLength;
    public bool isEndingV = false;
    public bool isBeforeEndAnimRun = false;
    public Vector3[] path;

    public string anim_End_ToCrossfadeForNextAct = null;

    //

    bool fightWhileMove = false;

    bool pathIsSet = false;

    SoldierGun soldGun;
    bool nowIn_Moving_MoveFight_Switching = false;
    bool nowInMoveFight = false;

    float moveFightHalfAngle = 60;

    float maxUpbodyAnimHalfAngle = 90;

    float moveFight_RotSpeed = 160;

    float minNeededTimeBeforeEndingToStartMoveFight = 1.9f;
    float minTimeBeforeEndingToStopMoveFight = 0.75f;

    float time_MoveFightStartDelayTime_Min = 0.28f;
    float time_MoveFightStartDelayTime_Max = 0.48f;

    float time_MoveFightStartDelayTime_Counter;
    SoldierFightInPointInfo moveFightInfo;

    Transform soldierLeftFootTr;
    Transform soldierRightFootTr;

    CharRaycastResult moveFightTarget = null;

    float time_MoveFightTargetRecheck_Max = 0.1f;
    float time_MoveFightTargetRecheck_Counter = 0.1f;

    //

    float currentUpBodyAngle = 0;

    float moveFight_Weight_Right = 0;
    float moveFight_Weight_Mid = 0;
    float moveFight_Weight_Left = 0;

    float moveFight_UpBodyWeight_Move = 0;
    float moveFight_UpBodyWeight_MoveShootIdle = 0;
    float moveFight_UpBodyWeight_MoveShoot = 0;
    float moveFight_UpBodyWeight_MoveReload = 0;
    float moveFight_UpBodyWeight_MoveDamage = 0;

    float moveFight_CrossfadeTime_MoveToShootIdle = 0.28f;
    float moveFight_CrossfadeTime_ShootIdleToMove = 0.28f;
    float moveFight_CrossfadeTime_DamageToShootIdle = 0.28f;
    float moveFight_CrossfadeTime_ShootIdleToDamage = 0.28f;
    float moveFight_CrossfadeTime_ReloadToShootIdle = 0.37f;
    float moveFight_CrossfadeTime_ShootIdleToReload = 0.37f;

    float actualCFTime;

    string anim_UpBody_Move_Mid = "";
    string anim_UpBody_Move_Legs = "";
    string anim_UpBody_MoveShootIdle_Left = "";
    string anim_UpBody_MoveShootIdle_Mid = "";
    string anim_UpBody_MoveShootIdle_Right = "";
    string anim_UpBody_MoveReload_Left = "";
    string anim_UpBody_MoveReload_Mid = "";
    string anim_UpBody_MoveReload_Right = "";
    string anim_UpBody_MoveShoot_Left = "";
    string anim_UpBody_MoveShoot_Mid = "";
    string anim_UpBody_MoveShoot_Right = "";

    string[] anims_UpBody_MoveDamage_Left;
    string[] anims_UpBody_MoveDamage_Mid;
    string[] anims_UpBody_MoveDamage_Right;

    int animUpBodyMoveDmgSelectedIndex = 0;

    bool lockRotationOnTarget = false;

    bool gentlyRotateUpBodyToMid = false;

    float animTimeCounter;
    float minAnimCrossfadeTime = 0.1f;

    float time_MoveFightShoot_Counter = 0.1f;
    float time_MoveFightShoot_Max = 0.1f;

    Transform transformToRotateAfterStop = null;

    Vector3 destPosToFindPath;

    //----------------------------------------------------------------------------------------

    float gravity = SoldierStats.SoldierGravity;
    float pathPosRadius = GeneralStats.PointRadius;

    SoldStatusEnum soldStatus;
    VChangeStepEnum vChangeStep = VChangeStepEnum.accFirst01;
    bool normalRotation;
    int curTargetIndex = 0;
    float accFirst;
    float accEnd;
    float dxFirst;
    float dxMid;
    float dxEnd;
    float v;
    float vOld;
    float pathLength;

    Vector3 soldierUpperBodyLookTarget;
    Vector3 soldierLowerBodyLookTarget;
    bool isStartingV = false;
    bool isMidV = false;

    DamageInfo dmg = null;
    string selectedDmgAnim;
    float selectedDmgAnimNeededDX;
    float animToAnimMovingCrossfadeTime;
    float animEndFinalTime;
    float animEndTimeCounter;
    SoldierDamageAnimPack anim_Damage_Move = null;
    bool moveFightEnabled = false;

    Transform endRotLookTargetTransform; //This or ...
    Vector3 endRotNormal = Vector3.zero; //... this
    bool isCustomEndRot = false;

    float animToAnimStartCrossfadeTime = 0.2f;
    float animMovingToAnimDamageCrossfadeTime = 0.23f;
    float animDamageToAnimMovingCrossfadeTime = 0.33f;
    float animMovingToAnimEndCrossfadeTime = 0.2f;

    bool needsToStop = false;
    float stopTEndAcc;
    float time_StopTEndAcc_Counter;
    string stopEndAnimNameToCrossfade = "";

    float aStarResultMaxTime = 0.4f;
    float aStarResultTimeCounter = 0.4f;

    float restartAStarMaxTime = 0.5f;
    float restartAStarTimeCounter = 0.5f;

    float rotAmountOnFrame = 0;

    float footStep_MinNeedVelocity = 0.4f;
    float footStep_TimeCounterInitialValue = 0.15f;
    float footStep_TimeCounter = 0.2f;

    float movingToMoveFightMaxTime = 0.36f;
    //-----------------------------------------------------------------------------------------

    //float playerInWayRecheckMaxTime = 0.01f;
    //float playerInWayRecheckTimeCounter = 0.01f;

    float playerHitRadius = 0;

    public void InitDefaultParams(MovementTypeEnum _movementType)
    {
        movementType = _movementType;
        SoldierMovementInfo mi = soldInfo.GetMovementInfoByType(movementType);
        vMid = mi.maxSpeed;
        tFirstAcc = mi.firstAccelerationTime;
        tEndAcc = mi.endAccelerationTime;
        startMovingCrossfadeTime = mi.startMovingCrossfadeTime;
        endMovingCrossfadeTime = mi.endMovingCrossfadeTime;
        anim_Move = mi.animsMove.GetRandomAnimName();
        anim_Damage_Move = mi.animPackMoveDamage;
        moveFightEnabled = mi.moveFightEnabled;

        //mf
        if (moveFightEnabled)
        {
            anim_UpBody_Move_Mid = mi.anim_UpBody_Move_Mid.GetRandomAnimName();
            anim_UpBody_Move_Legs = mi.anim_UpBody_Move_Legs.GetRandomAnimName();
            anim_UpBody_MoveShootIdle_Left = mi.anim_UpBody_MoveShootIdle_Left.GetRandomAnimName();
            anim_UpBody_MoveShootIdle_Mid = mi.anim_UpBody_MoveShootIdle_Mid.GetRandomAnimName();
            anim_UpBody_MoveShootIdle_Right = mi.anim_UpBody_MoveShootIdle_Right.GetRandomAnimName();
            anim_UpBody_MoveReload_Left = mi.anim_UpBody_MoveReload_Left.GetRandomAnimName();
            anim_UpBody_MoveReload_Mid = mi.anim_UpBody_MoveReload_Mid.GetRandomAnimName();
            anim_UpBody_MoveReload_Right = mi.anim_UpBody_MoveReload_Right.GetRandomAnimName();
            anim_UpBody_MoveShoot_Left = mi.anim_UpBody_MoveShoot_Left.GetRandomAnimName();
            anim_UpBody_MoveShoot_Mid = mi.anim_UpBody_MoveShoot_Mid.GetRandomAnimName();
            anim_UpBody_MoveShoot_Right = mi.anim_UpBody_MoveShoot_Right.GetRandomAnimName();

            anims_UpBody_MoveDamage_Left = mi.anims_UpBody_MoveDamage_Left.GetAnimNames();
            anims_UpBody_MoveDamage_Mid = mi.anims_UpBody_MoveDamage_Mid.GetAnimNames();
            anims_UpBody_MoveDamage_Right = mi.anims_UpBody_MoveDamage_Right.GetAnimNames();
        }
        //~mf
    }

    public void Init_PosToFindPath(Vector3 _destPos)
    {
        destPosToFindPath = _destPos;
    }

    public void Init_SetPath(Vector3[] _path)
    {
        path = _path;
        pathIsSet = true;
    }

    public void Init_DoFightWhileMove(bool _value)
    {
        fightWhileMove = _value;

        if (!moveFightEnabled)
        {
            if (fightWhileMove)
            {
                fightWhileMove = false;
                Debug.LogError("Move fight is required for a fight reg, while it's movement type doesn't support move fight!");
            }
        }
    }

    //

    public void SetNextActAnimToCrossfade(string _animName)
    {
        anim_End_ToCrossfadeForNextAct = _animName;
    }

    public void SetEndingRotLookTarget(Transform _rotLookTarget)
    {
        isCustomEndRot = true;
        endRotLookTargetTransform = _rotLookTarget;
    }

    public void SetEndingRotNormal(Vector3 _rotNormal)
    {
        isCustomEndRot = true;
        endRotNormal = _rotNormal;
    }

    public void SetNeedsToStop(float _tEndAcc, string _endAnimNameToCrossfade, Transform _trToRotateTo)
    {
        needsToStop = true;
        stopTEndAcc = _tEndAcc;
        time_StopTEndAcc_Counter = stopTEndAcc;
        stopEndAnimNameToCrossfade = _endAnimNameToCrossfade;
        transformToRotateAfterStop = _trToRotateTo;
    }

    public void SetNeedsToStop(float _tEndAcc, string _endAnimNameToCrossfade)
    {
        SetNeedsToStop(_tEndAcc, _endAnimNameToCrossfade, null);
    }

    //

    public override void Init(Transform contSoldier)
    {
        base.Init(contSoldier);
        //InitMainSoundPlay(soldInfo.footStepsAudioSource);
        //mf
        soldierLeftFootTr = soldBodyInfo.soldierLeftFootTr;
        soldierRightFootTr = soldBodyInfo.soldierRightFootTr;

        moveFightInfo = soldInfo.soldierGeneralInfo.defaultOnPosFightInPointInfo;
        soldGun = soldInfo.gun;
        //~mf

        playerHitRadius = (playerCharNew.GetCharController().radius + soldInfo.charController.radius)* 1.2f;
    }

    public override void StartAct()
    {
        base.StartAct();

        soldInfo.SetIsMoving(true);

        if (pathIsSet)
        {
            soldStatus = SoldStatusEnum.MoveInit01;
        }
        else
        {
            soldStatus = SoldStatusEnum.FindingPath_FirstCheck;
        }

        //if (path.Length == 0)
        //{
        //    SetFinished(true);
        //    return;
        //}

        //pathLength = CalcPathLength(0, path.Length - 1);
        //remainingPathLength = pathLength;

        //if (pathLength <= pathPosRadius)
        //{
        //    SetFinished(true);
        //    return;
        //}

        //accFirst = PhysPlus.GetAccelerationByParams(vMid, v0, tFirstAcc);
        //accEnd = PhysPlus.GetAccelerationByParams(vEnd, vMid, tEndAcc);

        //dxFirst = PhysPlus.GetDXByParams(accFirst, tFirstAcc, v0);
        //dxEnd = PhysPlus.GetDXByParams(accEnd, tEndAcc, vMid);
        //dxMid = pathLength - dxFirst - dxEnd;

        //v = v0;

        //animEndFinalTime = endMovingCrossfadeTime;
        //if (anim_End != "")
        //{
        //    animEndFinalTime = soldAnimObj.animation[anim_End].length;
        //}
        //animEndFinalTime = Mathf.Clamp(animEndFinalTime, 0, tEndAcc);

        //animEndTimeCounter = tEndAcc;

        ////mf
        //Reset_MoveFightStartDelayTime();
        ////~mf

        ////<Temp>
        //soldStatus = SoldStatusEnum.StartingNormal01;
        ////</Temp>
    }

    //<Test>
    List<GameObject> sph = new List<GameObject>();
    //</Test>

    public override void UpdateAct()
    {
        if (pathIsSet)
        {
            remainingPathLength = CalcPathLength(curTargetIndex, path.Length - 1);
            vOld = v;

            #region VChange

        StartVChange:

            Vector3 pos2D = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 playerPos2D = new Vector3(playerObj.transform.position.x, 0, playerObj.transform.position.z);

            Vector3 dist2DToPlayer = playerPos2D - pos2D;
            float dist2DToPlayerMag = dist2DToPlayer.magnitude;

            if (dist2DToPlayerMag <= playerHitRadius)
            {
                Vector3 curPathTarget2D = new Vector3(path[curTargetIndex].x, 0, path[curTargetIndex].z);

                Vector3 dist2DToCurPathTarget = curPathTarget2D - pos2D;
                Vector3 normalizedDist2DToCurPathTarget = dist2DToCurPathTarget.normalized;

                Vector3 normalizedDist2DToPlayer = dist2DToPlayer.normalized;

                float pushSideSign = -1;

                float angleToPlayer = Mathf.DeltaAngle(
                    MathfPlus.HorizontalAngle(normalizedDist2DToCurPathTarget),
                    MathfPlus.HorizontalAngle(normalizedDist2DToPlayer));

                if (angleToPlayer < 0)
                    pushSideSign = 1;

                Vector3 playerPushVec = new Vector3((normalizedDist2DToCurPathTarget.x - (pushSideSign * normalizedDist2DToCurPathTarget.z)) * 0.7071f * 2f, 0, (normalizedDist2DToCurPathTarget.x + (pushSideSign *normalizedDist2DToCurPathTarget.z)) * 0.7071f * 2f);

                playerCharNew.GetCharController().SimpleMove(playerPushVec);
            }

            #region accFirst01
            if (vChangeStep == VChangeStepEnum.accFirst01)
            {
                isStartingV = true;
                vChangeStep = VChangeStepEnum.accFirst02;
            }
            #endregion

            #region accFirst02
            if (vChangeStep == VChangeStepEnum.accFirst02)
            {

                //if (NeedsToGoToEndAcc())
                //{
                //    isStartingV = false;
                //    vChangeStep = VChangeStepEnum.accEnd01;
                //    goto StartVChange;
                //}


                v += accFirst * Time.deltaTime;

                if (Mathf.Abs(v - vOld) >= Mathf.Abs(vMid - vOld))
                {
                    isStartingV = false;
                    v = vMid;
                    vChangeStep = VChangeStepEnum.mid01;
                    goto StartVChange;
                }
                goto EndVChange;
            }
            #endregion

            #region mid01
            if (vChangeStep == VChangeStepEnum.mid01)
            {
                isMidV = true;
                vChangeStep = VChangeStepEnum.mid02;
            }
            #endregion

            #region mid02
            if (vChangeStep == VChangeStepEnum.mid02)
            {
                //playerInWayRecheckTimeCounter = MathfPlus.DecByDeltatimeToZero(playerInWayRecheckTimeCounter);

                //if (playerInWayRecheckTimeCounter == 0)
                //{
                //    playerInWayRecheckTimeCounter = playerInWayRecheckMaxTime;

                //    if (soldInfo.charController.velocity.magnitude < vMid * 0.5f)
                //    {
                //        Vector3 distToNextPoint = new Vector3(path[curTargetIndex].x, 0, path[curTargetIndex].z) - new Vector3(transform.position.x, 0, transform.position.z);
                //        distToNextPoint.Normalize();

                //        Vector3 distToPlayer = new Vector3(playerObj.transform.position.x, 0, playerObj.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z);
                //        distToPlayer.Normalize();

                //        float datZarib = 1;

                //        float deltaAngle = Mathf.DeltaAngle(
                //             MathfPlus.HorizontalAngle(distToNextPoint),
                //             MathfPlus.HorizontalAngle(distToPlayer));

                //        if (deltaAngle < 0)
                //            datZarib = -1;

                //        Vector3 moveVel = new Vector3(transform.position.x - datZarib * distToNextPoint.z * 1, 0, transform.position.z + datZarib * distToNextPoint.x * 1);

                //        playerCharNew.GetCharController().SimpleMove(0.005f * moveVel);


                //        //Vector3 newPoint = new Vector3(transform.position.x - datZarib * distToNextPoint.z * 1, 14, transform.position.z + datZarib * distToNextPoint.x * 1);

                //        //GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                //        //go.transform.position = newPoint;
                //        //go.collider.enabled = false;

                //        //Vector3[] newPath = new Vector3[path.Length + 1];

                //        //int ind = 0;

                //        //for (int i = 0; i < curTargetIndex; i++)
                //        //{
                //        //    newPath[i] = path[i];
                //        //    ind++;
                //        //}

                //        //newPath[ind] = newPoint;
                //        //ind++;

                //        //for (int i = ind; i < newPath.Length; i++)
                //        //{
                //        //    newPath[i] = path[i-1];
                //        //}

                //        //path = newPath;


                //        // path[curTargetIndex] = new Vector3(path[curTargetIndex].x + distToNextPoint.x * 1, path[curTargetIndex].y, path[curTargetIndex].z + distToNextPoint.z * 1);

                //        //path[curTargetIndex]
                //    }
                //}

                if (NeedsToGoToEndAcc())
                {
                    isMidV = false;
                    vChangeStep = VChangeStepEnum.accEnd01;
                    goto StartVChange;
                }
            }
            #endregion

            #region stopping01
            if (vChangeStep == VChangeStepEnum.stopping01)
            {
                isMidV = false;

                isEndingV = true;

                accEnd = PhysPlus.GetAccelerationByParams(0, v, stopTEndAcc);
                vChangeStep = VChangeStepEnum.stopping02;
            }
            #endregion

            #region stopping02
            if (vChangeStep == VChangeStepEnum.stopping02)
            {
                v += accEnd * Time.deltaTime;

                time_StopTEndAcc_Counter -= Time.deltaTime;

                if (v <= 0 || time_StopTEndAcc_Counter <= 0)
                {
                    vChangeStep = VChangeStepEnum.stopping03;
                    SetFinished(true);
                }
            }
            #endregion

            #region accEnd01
            if (vChangeStep == VChangeStepEnum.accEnd01)
            {
                isEndingV = true;
                vChangeStep = VChangeStepEnum.accEnd02;
            }
            #endregion

            #region accEnd02
            if (vChangeStep == VChangeStepEnum.accEnd02)
            {
                accEnd = PhysPlus.GetAccelerationByParams_NoTime(vEnd, v, remainingPathLength);

                v += accEnd * Time.deltaTime;

                if (Mathf.Abs(v - vOld) >= Mathf.Abs(vEnd - vOld))
                {
                    v = vEnd;
                    vChangeStep = VChangeStepEnum.accEnd03;
                }
            }
            #endregion

            #region accEnd03
            if (vChangeStep == VChangeStepEnum.accEnd03)
            {
                accEnd = 0;
                v = vEnd;
            }
            #endregion

        EndVChange:

            #endregion

            #region FootStep
            footStep_TimeCounter = MathfPlus.DecByDeltatimeToZero(footStep_TimeCounter);

            if (ShouldResetFootstepTimeCounter())
                footStep_TimeCounter = footStep_TimeCounterInitialValue;
            else
            {
                if (ShouldPlayFootstepSound())
                {
                    float vel = GetHorizVelocity();

                    footStep_TimeCounter = soldInfo.footStepDelayGraph.Evaluate(vel);

                    soldInfo.PlayFootStepSound(soldInfo.GentlyGetUnderFootSurfaceMaterial(), soldInfo.curFoot);

                    if (soldInfo.curFoot == PlayerFootEnum.Right)
                        soldInfo.curFoot = PlayerFootEnum.Left;
                    else
                        soldInfo.curFoot = PlayerFootEnum.Right;
                }
            }
            #endregion

            #region MoveAndRotation

        StartMoveAndRotation:

            //for (int i = 0; i < sph.Count; i++)
            //{
            //    Destroy(sph[i]);
            //}

            //sph.Clear();

            ////<Test>
            //for (int i = curTargetIndex; i < path.Length; i++)
            //{
            //    GameObject ss = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //    ss.transform.position = path[i];
            //    ss.GetComponent<Collider>().enabled = false;

            //    sph.Add(ss);
            //}
            ////</Test>

            //<Beta>
            if (normalRotation)
            {
                soldierUpperBodyLookTarget = path[curTargetIndex];
                soldierLowerBodyLookTarget = path[curTargetIndex];
            }

            if (isEndingV && isCustomEndRot)
            {

                if (endRotLookTargetTransform != null)
                {
                    soldierUpperBodyLookTarget = endRotLookTargetTransform.position;
                    soldierLowerBodyLookTarget = endRotLookTargetTransform.position;
                }
                else
                    if (endRotNormal != Vector3.zero)
                    {
                        soldierUpperBodyLookTarget = controlledSoldier.transform.position + endRotNormal;
                        soldierLowerBodyLookTarget = controlledSoldier.transform.position + endRotNormal;
                    }

            }
            //</Beta>

            Vector3 dist3DToCurrentTarget = (path[curTargetIndex] - soldTr.position);
            Vector3 dist2DToCurrentTarget = dist3DToCurrentTarget;
            dist2DToCurrentTarget.y = 0;

            Vector3 moveDirection = dist2DToCurrentTarget.normalized;
            Vector3 moveVelocity = moveDirection * v;

            //<AvvalRotationDada>
            if (dist3DToCurrentTarget.magnitude > 0)
            {
                SetRotAmountOnFrame(rotSpeedAllBody * Time.deltaTime);

                //All body

                Vector3 allBodyDestRot = soldierLowerBodyLookTarget - soldTr.position;
                float allBodyDeltaAngle = Mathf.DeltaAngle(
                    MathfPlus.HorizontalAngle(soldTr.forward),
                    MathfPlus.HorizontalAngle(allBodyDestRot));

                if (allBodyDeltaAngle < 0)
                    SetRotAmountOnFrame(-rotAmountOnFrame);

                if (Mathf.Abs(rotAmountOnFrame) > Mathf.Abs(allBodyDeltaAngle))
                    SetRotAmountOnFrame(allBodyDeltaAngle);

                Quaternion allBodyRotVec = Quaternion.Euler(0, rotAmountOnFrame, 0);

                soldTr.rotation *= allBodyRotVec;


                if (!normalRotation)
                {
                    //if (!normalRotation) ke vallaaaaaaaaaaaaaa...

                    //soldierUpperBodyRoot.RotateAround(Vector3.up, 0.01f);
                    //soldierGunRoot.RotateAround(tr.position, Vector3.up, Mathf.Rad2Deg * 0.01f);
                }
            }
            //</AvvalRotationDada>

            if ((dist2DToCurrentTarget.magnitude <= pathPosRadius) || (moveVelocity.magnitude * Time.deltaTime >= dist2DToCurrentTarget.magnitude))
            {
                //moveVelocity = dist2DToCurrentTarget;
                curTargetIndex++;
                if (curTargetIndex > path.Length - 1)
                {
                    SetFinished(true);
                }
            }

            if (!soldCharController.isGrounded)
                moveVelocity.y += gravity;

            //soldCharController.Move(moveVelocity * Time.deltaTime);
            soldCharController.SimpleMove(moveVelocity);

            #endregion
        }

        #region SoldierControl

    StartControllingSoldStatus:

        #region FindingPath_FirstCheck
        if (soldStatus == SoldStatusEnum.FindingPath_FirstCheck)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            if (IsSoldOnPoint(controlledSoldier.gameObject, destPosToFindPath, pathPosRadius))
            {
                soldStatus = SoldStatusEnum.EarlyEnd01;
                goto StartControllingSoldStatus;
            }
            else
            {
                Vector3[] newPath;

                if (mapLogic.FindCurvePath(controlledSoldier.transform.position, destPosToFindPath, pathPosRadius, out newPath))
                {
                    path = newPath;
                    soldStatus = SoldStatusEnum.MoveInit01;
                    goto StartControllingSoldStatus;
                }
                else
                {
                    NavMeshPath navMeshPath = new NavMeshPath();

                    if (NavMesh.CalculatePath(controlledSoldier.transform.position, destPosToFindPath, -1, navMeshPath))
                    {
                        path = navMeshPath.corners;

                        soldStatus = SoldStatusEnum.MoveInit01;
                        goto StartControllingSoldStatus;
                    }

                    soldStatus = SoldStatusEnum.FindingPath_StartAStar;
                    goto StartControllingSoldStatus;
                }
            }
        }
        #endregion

        #region FindingPath_StartAStar
        if (soldStatus == SoldStatusEnum.FindingPath_StartAStar)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            aStarResultTimeCounter = aStarResultMaxTime;
            restartAStarTimeCounter = restartAStarMaxTime;

            soldInfo.FindNewAStarPath(controlledSoldier.transform.position, destPosToFindPath);

            if (soldInfo.isAStarPathResultRecievedInThisRun)
            {
                if (!soldInfo.isAStarPathError)
                {
                    path = soldInfo.aStarLastPath;
                    soldStatus = SoldStatusEnum.MoveInit01;
                    goto StartControllingSoldStatus;
                }
                else
                {
                    Debug.LogError("No path founded to point!");
                    soldStatus = SoldStatusEnum.FindingPath_WaitingForRestartAStar;
                    goto EndControllingSoldStatus;
                }
            }

            soldStatus = SoldStatusEnum.FindingPath_WaitingForAStarResult;
            goto StartControllingSoldStatus;
        }
        #endregion

        #region FindingPath_WaitingForAStarResult
        if (soldStatus == SoldStatusEnum.FindingPath_WaitingForAStarResult)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            if (soldInfo.isAStarPathResultRecievedInThisRun)
            {
                if (!soldInfo.isAStarPathError)
                {
                    path = soldInfo.aStarLastPath;
                    soldStatus = SoldStatusEnum.MoveInit01;
                    goto StartControllingSoldStatus;
                }
                else
                {
                    Debug.LogError("No path founded to point!");
                    soldStatus = SoldStatusEnum.FindingPath_WaitingForRestartAStar;
                    goto EndControllingSoldStatus;
                }
            }

            aStarResultTimeCounter -= Time.deltaTime;
            if (aStarResultTimeCounter <= 0)
            {
                Debug.LogError("No path founded in needed time!");

                soldStatus = SoldStatusEnum.FindingPath_StartAStar;
                goto EndControllingSoldStatus;
            }
        }
        #endregion

        #region FindingPath_WaitingForRestartAStar
        if (soldStatus == SoldStatusEnum.FindingPath_WaitingForRestartAStar)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            restartAStarTimeCounter = MathfPlus.DecByDeltatimeToZero(restartAStarTimeCounter);

            if (restartAStarTimeCounter == 0)
            {
                soldStatus = SoldStatusEnum.FindingPath_StartAStar;
                goto StartControllingSoldStatus;
            }
        }
        #endregion

        #region MoveInit01
        if (soldStatus == SoldStatusEnum.MoveInit01)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            pathIsSet = true;

            if (path.Length == 0)
            {
                soldStatus = SoldStatusEnum.EarlyEnd01;
                goto StartControllingSoldStatus;
            }

            pathLength = CalcPathLength(0, path.Length - 1);
            remainingPathLength = pathLength;

            if (pathLength <= pathPosRadius)
            {
                soldStatus = SoldStatusEnum.EarlyEnd01;
                goto StartControllingSoldStatus;
            }

            accFirst = PhysPlus.GetAccelerationByParams(vMid, v0, tFirstAcc);
            accEnd = PhysPlus.GetAccelerationByParams(vEnd, vMid, tEndAcc);

            dxFirst = PhysPlus.GetDXByParams(accFirst, tFirstAcc, v0);
            dxEnd = PhysPlus.GetDXByParams(accEnd, tEndAcc, vMid);
            dxMid = pathLength - dxFirst - dxEnd;

            v = v0;

            animEndFinalTime = endMovingCrossfadeTime;
            if (anim_End != "")
            {
                animEndFinalTime = soldAnimObj.animation[anim_End].length;
            }
            animEndFinalTime = Mathf.Clamp(animEndFinalTime, 0, tEndAcc);

            animEndTimeCounter = tEndAcc;

            //mf
            Reset_MoveFightStartDelayTime();
            //~mf

            //<Temp>
            soldStatus = SoldStatusEnum.StartingNormal01;
            //</Temp>
        }
        #endregion

        #region StartingNormal01
        if (soldStatus == SoldStatusEnum.StartingNormal01)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            normalRotation = true;
            if (anim_Start != "")
            {
                //soldAnimObj.animation[anim_Start.name].time = 0;
                //soldAnimObj.animation.CrossFade(anim_Start.name, animToAnimStartCrossfadeTime);

                soldInfo.StartNewMainAnimWithCrossfadeTime(anim_Start, animToAnimStartCrossfadeTime);

                soldStatus = SoldStatusEnum.StartingNormal02;
                goto EndControllingSoldStatus;
            }
            else
            {
                animToAnimMovingCrossfadeTime = startMovingCrossfadeTime;
                soldStatus = SoldStatusEnum.MovingNormal01;
            }
        }
        #endregion

        #region StartingNormal02
        if (soldStatus == SoldStatusEnum.StartingNormal02)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            //if (soldAnimObj.animation[anim_Start.name].time >= anim_Start.length - startMovingCrossfadeTime)
            //{
            //    animToAnimMovingCrossfadeTime = anim_Start.length - soldAnimObj.animation[anim_Start.name].time;
            //    soldStatus = SoldStatusEnum.MovingNormal01;
            //}

            if (soldInfo.CheckMainAnimIsFinished(startMovingCrossfadeTime))
            {
                startMovingCrossfadeTime = soldInfo.mainAnimRemainingTime;
                soldStatus = SoldStatusEnum.MovingNormal01;
            }
        }
        #endregion

        #region MovingNormal01
        if (soldStatus == SoldStatusEnum.MovingNormal01)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            //soldAnimObj.animation[anim_Move.name].time = 0;
            //soldAnimObj.animation.CrossFade(anim_Move.name, animToAnimMovingCrossfadeTime);

            soldInfo.StartNewMainAnimWithCrossfadeTime(anim_Move, animToAnimMovingCrossfadeTime, 0.1f, 0.7f);

            //mf
            //if (fightWhileMove)
            //    FirstInitAnim_MoveMidRun();
            //~mf

            soldStatus = SoldStatusEnum.MovingNormal02;
            goto EndControllingSoldStatus;
        }
        #endregion

        #region MovingNormal02
        if (soldStatus == SoldStatusEnum.MovingNormal02)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            if (isEndingV)
            {
                soldStatus = SoldStatusEnum.EndingNormal01;
                goto StartControllingSoldStatus;
            }

            if (needsToStop)
            {
                soldStatus = SoldStatusEnum.StoppingNormal01;
                goto StartControllingSoldStatus;
            }

            if (isMidV)
            {
                if (soldInfo.isDamageRecievedInThisRun)
                {
                    dmg = new DamageInfo();
                    dmg = soldInfo.firstDamage;

                    if (ShouldTakeDamage(dmg))
                    {
                        if (anim_Damage_Move != null)
                        {
                            selectedDmgAnim = anim_Damage_Move.GetRandomAnim(dmg);
                            selectedDmgAnimNeededDX = PhysPlus.GetDXByParams(0, soldAnimObj.animation[selectedDmgAnim].length, vMid);
                            float diffDX = remainingPathLength - dxEnd - selectedDmgAnimNeededDX;
                            if (diffDX > 0)
                            {
                                soldStatus = SoldStatusEnum.GettingDamageNormal01;
                                goto StartControllingSoldStatus;
                            }
                        }
                    }
                }
            }

            //mf
            if (soldInfo.IsFullyInNewMainAnim())
            {
                if (fightWhileMove)
                {
                    time_MoveFightStartDelayTime_Counter = MathfPlus.DecByDeltatimeToZero(time_MoveFightStartDelayTime_Counter);

                    if (time_MoveFightStartDelayTime_Counter == 0)
                    {
                        if (IsRemainingDistantOkForMoveFight())
                        {
                            time_MoveFightStartDelayTime_Counter = time_MoveFightStartDelayTime_Min;

                            if (ShouldStartMoveFight())
                            {
                                soldStatus = SoldStatusEnum.MovingNormalToMoveFight01;
                                goto StartControllingSoldStatus;
                            }
                        }
                    }
                }
            }
            //~mf
        }
        #endregion

        //mf

        #region MovingNormalToMoveFight01
        if (soldStatus == SoldStatusEnum.MovingNormalToMoveFight01)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            Start_MovingToMoveFight();

            soldStatus = SoldStatusEnum.MovingNormalToMoveFight02;
        }
        #endregion

        #region MovingNormalToMoveFight02
        if (soldStatus == SoldStatusEnum.MovingNormalToMoveFight02)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            soldAnimObj.animation[anim_UpBody_Move_Mid].enabled = true;
            soldAnimObj.animation[anim_UpBody_Move_Mid].layer = 1;
            soldAnimObj.animation[anim_UpBody_Move_Mid].weight += Time.deltaTime / movingToMoveFightMaxTime;
            soldAnimObj.animation[anim_UpBody_Move_Mid].weight = Mathf.Clamp01(soldAnimObj.animation[anim_UpBody_Move_Mid].weight);
            soldAnimObj.animation[anim_UpBody_Move_Mid].time = soldAnimObj.animation[anim_Move].time;

            soldAnimObj.animation[anim_UpBody_Move_Legs].enabled = true;
            soldAnimObj.animation[anim_UpBody_Move_Legs].layer = 10;
            soldAnimObj.animation[anim_UpBody_Move_Legs].weight = soldAnimObj.animation[anim_UpBody_Move_Mid].weight;
            soldAnimObj.animation[anim_UpBody_Move_Legs].time = soldAnimObj.animation[anim_UpBody_Move_Mid].time;

            if (soldAnimObj.animation[anim_UpBody_Move_Mid].weight == 1)
            {
                nowIn_Moving_MoveFight_Switching = false;

                soldStatus = SoldStatusEnum.MoveFight01;
            }
        }
        #endregion

        #region MoveFight01
        if (soldStatus == SoldStatusEnum.MoveFight01)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            InitMoveFight();
            SetNewMoveFightTarget(GetBestFightTarget());
            soldStatus = SoldStatusEnum.MoveFight02;

            //soldStatus = SoldStatusEnum.aaaaa;
            //goto EndControllingSoldStatus;
        }
        #endregion

        #region MoveFight02
        if (soldStatus == SoldStatusEnum.MoveFight02)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            soldStatus = SoldStatusEnum.MoveFight_MovingToShootIdle01;
        }
        #endregion

        #region MoveFight_MovingToShootIdle01
        if (soldStatus == SoldStatusEnum.MoveFight_MovingToShootIdle01)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            soldStatus = SoldStatusEnum.MoveFight_MovingToShootIdle02;
        }
        #endregion

        #region MoveFight_MovingToShootIdle02
        if (soldStatus == SoldStatusEnum.MoveFight_MovingToShootIdle02)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            GentlySetUpBodyAngleToTarget();

            moveFight_UpBodyWeight_MoveShootIdle += Time.deltaTime / moveFight_CrossfadeTime_MoveToShootIdle;
            moveFight_UpBodyWeight_MoveShootIdle = Mathf.Clamp(moveFight_UpBodyWeight_MoveShootIdle, 0, 1);

            if (moveFight_UpBodyWeight_MoveShootIdle == 1)
            {
                soldStatus = SoldStatusEnum.MoveFight_ShootIdle01;
                goto StartControllingSoldStatus;
            }
        }
        #endregion

        #region MoveFight_ShootIdle01
        if (soldStatus == SoldStatusEnum.MoveFight_ShootIdle01)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            GentlySetUpBodyAngleToTarget();

            if (!IsRemainingDistantOkForMoveFight())
            {
                soldStatus = SoldStatusEnum.MoveFight_ShootIdleToMoving01;
                goto StartControllingSoldStatus;
            }

            if (needsToStop)
            {
                soldStatus = SoldStatusEnum.MoveFight_ShootIdleToMoving01;
                goto StartControllingSoldStatus;
            }

            if (soldInfo.isDamageRecievedInThisRun)
            {
                dmg = new DamageInfo();
                dmg = soldInfo.firstDamage;

                if (ShouldTakeDamage(dmg))
                {
                    soldStatus = SoldStatusEnum.MoveFight_ShootIdleToDamage01;
                    goto StartControllingSoldStatus;
                }
            }

            time_MoveFightTargetRecheck_Counter = MathfPlus.DecByDeltatimeToZero(time_MoveFightTargetRecheck_Counter);

            if (time_MoveFightTargetRecheck_Counter == 0)
            {
                time_MoveFightTargetRecheck_Counter = time_MoveFightTargetRecheck_Max;

                if (!IsTargetOkAsMoveFightTarget(moveFightTarget))
                {
                    CharRaycastResult rayCastRes = GetBestFightTarget();

                    if (!IsTargetOkAsMoveFightTarget(rayCastRes))
                    {
                        soldStatus = SoldStatusEnum.MoveFight_ShootIdleToMoving01;
                        goto StartControllingSoldStatus;
                    }
                    else
                    {
                        SetNewMoveFightTarget(rayCastRes);
                        goto EndControllingSoldStatus;
                    }
                }
            }

            if (NeedsReload())
            {
                soldStatus = SoldStatusEnum.MoveFight_ShootIdleToReload01;
                goto StartControllingSoldStatus;
            }

            time_MoveFightShoot_Counter = MathfPlus.DecByDeltatimeToZero(time_MoveFightShoot_Counter);

            if (time_MoveFightShoot_Counter == 0
                && soldGun.IsReady()
                && lockRotationOnTarget
                && IsTargetOkAsMoveFightTarget(moveFightTarget))
            {
                time_MoveFightShoot_Counter = time_MoveFightShoot_Max;

                soldStatus = SoldStatusEnum.MoveFight_Shoot01;
                goto StartControllingSoldStatus;
            }
        }
        #endregion

        #region MoveFight_ShootIdleToMoving01
        if (soldStatus == SoldStatusEnum.MoveFight_ShootIdleToMoving01)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            soldStatus = SoldStatusEnum.MoveFight_ShootIdleToMoving02;
        }
        #endregion

        #region MoveFight_ShootIdleToMoving02
        if (soldStatus == SoldStatusEnum.MoveFight_ShootIdleToMoving02)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            moveFight_UpBodyWeight_MoveShootIdle -= Time.deltaTime / moveFight_CrossfadeTime_ShootIdleToMove;
            moveFight_UpBodyWeight_MoveShootIdle = Mathf.Clamp(moveFight_UpBodyWeight_MoveShootIdle, 0, 1);

            if (moveFight_UpBodyWeight_MoveShootIdle == 0)
            {
                EndMoveFight();

                soldStatus = SoldStatusEnum.MoveFightToMovingNormal01;
                goto StartControllingSoldStatus;
            }
        }
        #endregion

        #region MoveFight_ShootIdleToDamage01
        if (soldStatus == SoldStatusEnum.MoveFight_ShootIdleToDamage01)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            ResetMoveFightDamageAnims();

            lockRotationOnTarget = false;

            gentlyRotateUpBodyToMid = true;

            soldStatus = SoldStatusEnum.MoveFight_ShootIdleToDamage02;
        }
        #endregion

        #region MoveFight_ShootIdleToDamage02
        if (soldStatus == SoldStatusEnum.MoveFight_ShootIdleToDamage02)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            moveFight_UpBodyWeight_MoveShootIdle -= Time.deltaTime / moveFight_CrossfadeTime_ShootIdleToDamage;
            moveFight_UpBodyWeight_MoveShootIdle = Mathf.Clamp(moveFight_UpBodyWeight_MoveShootIdle, 0, 1);

            moveFight_UpBodyWeight_MoveDamage += Time.deltaTime / moveFight_CrossfadeTime_ShootIdleToDamage;
            moveFight_UpBodyWeight_MoveDamage = Mathf.Clamp(moveFight_UpBodyWeight_MoveDamage, 0, 1);

            if (moveFight_UpBodyWeight_MoveShootIdle == 0 && moveFight_UpBodyWeight_MoveDamage == 1)
            {
                animTimeCounter = moveFight_CrossfadeTime_ShootIdleToDamage;

                soldStatus = SoldStatusEnum.MoveFight_Damage01;
                goto EndControllingSoldStatus;
            }
        }
        #endregion

        #region MoveFight_Damage01
        if (soldStatus == SoldStatusEnum.MoveFight_Damage01)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            soldStatus = SoldStatusEnum.MoveFight_Damage02;
        }
        #endregion

        #region MoveFight_Damage02
        if (soldStatus == SoldStatusEnum.MoveFight_Damage02)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            animTimeCounter += Time.deltaTime;

            if (animTimeCounter >= soldAnimObj.animation[anims_UpBody_MoveDamage_Mid[animUpBodyMoveDmgSelectedIndex]].length - moveFight_CrossfadeTime_DamageToShootIdle)
            {
                actualCFTime = soldAnimObj.animation[anims_UpBody_MoveDamage_Mid[animUpBodyMoveDmgSelectedIndex]].length - animTimeCounter;
                actualCFTime = Mathf.Clamp(actualCFTime, minAnimCrossfadeTime, moveFight_CrossfadeTime_DamageToShootIdle);

                soldStatus = SoldStatusEnum.MoveFight_DamageToShootIdle01;
                goto StartControllingSoldStatus;
            }
        }
        #endregion

        #region MoveFight_DamageToShootIdle01
        if (soldStatus == SoldStatusEnum.MoveFight_DamageToShootIdle01)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            soldStatus = SoldStatusEnum.MoveFight_DamageToShootIdle02;
        }
        #endregion

        #region MoveFight_DamageToShootIdle02
        if (soldStatus == SoldStatusEnum.MoveFight_DamageToShootIdle02)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            moveFight_UpBodyWeight_MoveShootIdle += Time.deltaTime / actualCFTime;
            moveFight_UpBodyWeight_MoveShootIdle = Mathf.Clamp(moveFight_UpBodyWeight_MoveShootIdle, 0, 1);

            moveFight_UpBodyWeight_MoveDamage -= Time.deltaTime / actualCFTime;
            moveFight_UpBodyWeight_MoveDamage = Mathf.Clamp(moveFight_UpBodyWeight_MoveDamage, 0, 1);

            if (moveFight_UpBodyWeight_MoveShootIdle == 1 && moveFight_UpBodyWeight_MoveDamage == 0)
            {
                gentlyRotateUpBodyToMid = false;

                soldStatus = SoldStatusEnum.MoveFight_ShootIdle01;
                goto StartControllingSoldStatus;
            }
        }
        #endregion

        #region MoveFight_ShootIdleToReload01
        if (soldStatus == SoldStatusEnum.MoveFight_ShootIdleToReload01)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            ResetMoveFightReloadAnims();

            lockRotationOnTarget = false;
            gentlyRotateUpBodyToMid = true;

            soldStatus = SoldStatusEnum.MoveFight_ShootIdleToReload02;
        }
        #endregion

        #region MoveFight_ShootIdleToReload02
        if (soldStatus == SoldStatusEnum.MoveFight_ShootIdleToReload02)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            moveFight_UpBodyWeight_MoveShootIdle -= Time.deltaTime / moveFight_CrossfadeTime_ShootIdleToReload;
            moveFight_UpBodyWeight_MoveShootIdle = Mathf.Clamp(moveFight_UpBodyWeight_MoveShootIdle, 0, 1);

            moveFight_UpBodyWeight_MoveReload += Time.deltaTime / moveFight_CrossfadeTime_ShootIdleToReload;
            moveFight_UpBodyWeight_MoveReload = Mathf.Clamp(moveFight_UpBodyWeight_MoveReload, 0, 1);

            if (moveFight_UpBodyWeight_MoveShootIdle == 0 && moveFight_UpBodyWeight_MoveReload == 1)
            {
                animTimeCounter = moveFight_CrossfadeTime_ShootIdleToReload;

                soldStatus = SoldStatusEnum.MoveFight_Reload01;
                goto EndControllingSoldStatus;
            }
        }
        #endregion

        #region MoveFight_Reload01
        if (soldStatus == SoldStatusEnum.MoveFight_Reload01)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            soldGun.Reload();

            soldGun.PlayReloadSound();

            soldStatus = SoldStatusEnum.MoveFight_Reload02;
        }
        #endregion

        #region MoveFight_Reload02
        if (soldStatus == SoldStatusEnum.MoveFight_Reload02)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            animTimeCounter += Time.deltaTime;

            if (animTimeCounter >= soldAnimObj.animation[anim_UpBody_MoveReload_Mid].length - moveFight_CrossfadeTime_ReloadToShootIdle)
            {
                actualCFTime = soldAnimObj.animation[anim_UpBody_MoveReload_Mid].length - animTimeCounter;
                actualCFTime = Mathf.Clamp(actualCFTime, minAnimCrossfadeTime, moveFight_CrossfadeTime_ReloadToShootIdle);

                soldStatus = SoldStatusEnum.MoveFight_ReloadToShootIdle01;
                goto StartControllingSoldStatus;
            }
        }
        #endregion

        #region MoveFight_ReloadToShootIdle01
        if (soldStatus == SoldStatusEnum.MoveFight_ReloadToShootIdle01)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            soldStatus = SoldStatusEnum.MoveFight_ReloadToShootIdle02;
        }
        #endregion

        #region MoveFight_ReloadToShootIdle02
        if (soldStatus == SoldStatusEnum.MoveFight_ReloadToShootIdle02)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            moveFight_UpBodyWeight_MoveShootIdle += Time.deltaTime / actualCFTime;
            moveFight_UpBodyWeight_MoveShootIdle = Mathf.Clamp(moveFight_UpBodyWeight_MoveShootIdle, 0, 1);

            moveFight_UpBodyWeight_MoveReload -= Time.deltaTime / actualCFTime;
            moveFight_UpBodyWeight_MoveReload = Mathf.Clamp(moveFight_UpBodyWeight_MoveReload, 0, 1);

            if (moveFight_UpBodyWeight_MoveShootIdle == 1 && moveFight_UpBodyWeight_MoveReload == 0)
            {
                gentlyRotateUpBodyToMid = false;

                soldStatus = SoldStatusEnum.MoveFight_ShootIdle01;
                goto StartControllingSoldStatus;
            }
        }
        #endregion

        #region MoveFight_Shoot01
        if (soldStatus == SoldStatusEnum.MoveFight_Shoot01)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            ResetMoveFightShootAnims();

            moveFight_UpBodyWeight_MoveShootIdle = 0;
            moveFight_UpBodyWeight_MoveShoot = 1;

            animTimeCounter = soldAnimObj.animation[anim_UpBody_MoveShoot_Mid].length;

            Transform shootTarg;
            List<Transform> okPoses;

            okPoses = moveFightTarget.characterHittedPoses;

            shootTarg = okPoses[Random.Range(0, okPoses.Count)];

            soldGun.TryFire(shootTarg.position);

            soldStatus = SoldStatusEnum.MoveFight_Shoot02;
            goto EndControllingSoldStatus;
        }
        #endregion

        #region MoveFight_Shoot02
        if (soldStatus == SoldStatusEnum.MoveFight_Shoot02)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            GentlySetUpBodyAngleToTarget();

            animTimeCounter = MathfPlus.DecByDeltatimeToZero(animTimeCounter);

            if (animTimeCounter == 0)
            {
                moveFight_UpBodyWeight_MoveShootIdle = 1;
                moveFight_UpBodyWeight_MoveShoot = 0;

                soldStatus = SoldStatusEnum.MoveFight_ShootIdle01;
                goto StartControllingSoldStatus;
            }
        }
        #endregion

        #region MoveFightToMovingNormal01
        if (soldStatus == SoldStatusEnum.MoveFightToMovingNormal01)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            nowIn_Moving_MoveFight_Switching = true;

            soldStatus = SoldStatusEnum.MoveFightToMovingNormal02;
        }
        #endregion

        #region MoveFightToMovingNormal02
        if (soldStatus == SoldStatusEnum.MoveFightToMovingNormal02)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            soldAnimObj.animation[anim_UpBody_Move_Mid].weight -= Time.deltaTime / movingToMoveFightMaxTime;
            soldAnimObj.animation[anim_UpBody_Move_Mid].weight = Mathf.Clamp01(soldAnimObj.animation[anim_UpBody_Move_Mid].weight);
            soldAnimObj.animation[anim_UpBody_Move_Mid].time = soldAnimObj.animation[anim_Move].time;

            soldAnimObj.animation[anim_UpBody_Move_Legs].weight = soldAnimObj.animation[anim_UpBody_Move_Mid].weight;
            soldAnimObj.animation[anim_UpBody_Move_Legs].time = soldAnimObj.animation[anim_UpBody_Move_Mid].time;

            if (soldAnimObj.animation[anim_UpBody_Move_Mid].weight == 0)
            {
                End_Move_MoveFight_Switching();

                soldStatus = SoldStatusEnum.MovingNormal01;

                goto StartControllingSoldStatus;
            }
        }
        #endregion

        //~mf

        #region StoppingNormal01
        if (soldStatus == SoldStatusEnum.StoppingNormal01)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            soldInfo.StartNewMainAnimWithCrossfadeTime(stopEndAnimNameToCrossfade, stopTEndAcc);

            if (transformToRotateAfterStop != null)
            {
                SetEndingRotLookTarget(transformToRotateAfterStop);
            }


            vChangeStep = VChangeStepEnum.stopping01;
            soldStatus = SoldStatusEnum.StoppingNormal02;

            goto EndControllingSoldStatus;
        }
        #endregion

        #region GettingDamageNormal01
        if (soldStatus == SoldStatusEnum.GettingDamageNormal01)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            //soldAnimObj.animation[selectedDmgAnim.name].time = 0;
            //soldAnimObj.animation.CrossFade(selectedDmgAnim.name, animMovingToAnimDamageCrossfadeTime);

            soldInfo.StartNewMainAnimWithCrossfadeTime(selectedDmgAnim, animMovingToAnimDamageCrossfadeTime);

            soldStatus = SoldStatusEnum.GettingDamageNormal02;
            goto EndControllingSoldStatus;
        }
        #endregion

        #region GettingDamageNormal02
        if (soldStatus == SoldStatusEnum.GettingDamageNormal02)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            //if (soldAnimObj.animation[selectedDmgAnim.name].time > (selectedDmgAnim.length - animDamageToAnimMovingCrossfadeTime))
            //{
            //    animToAnimMovingCrossfadeTime = selectedDmgAnim.length - soldAnimObj.animation[selectedDmgAnim.name].time;
            //    soldStatus = SoldStatusEnum.MovingNormal01;
            //    goto StartControllingSoldStatus;
            //}

            if (soldInfo.CheckMainAnimIsFinished(animDamageToAnimMovingCrossfadeTime))
            {
                animToAnimMovingCrossfadeTime = soldInfo.mainAnimRemainingTime;
                soldStatus = SoldStatusEnum.MovingNormal01;
                goto StartControllingSoldStatus;
            }
        }
        #endregion

        #region EndingNormal01
        if (soldStatus == SoldStatusEnum.EndingNormal01)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            animEndTimeCounter -= Time.deltaTime;

            if (animEndTimeCounter <= animEndFinalTime)
            {
                if (!isBeforeEndAnimRun)
                {
                    isBeforeEndAnimRun = true;

                    goto EndControllingSoldStatus;
                }

                if (isBeforeEndAnimRun)
                {
                    isBeforeEndAnimRun = false;

                    soldStatus = SoldStatusEnum.EndingNormal02;
                    goto StartControllingSoldStatus;
                }
            }
        }
        #endregion

        #region EndingNormal02
        if (soldStatus == SoldStatusEnum.EndingNormal02)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            if (anim_End != "")
            {
                //soldAnimObj.animation[anim_End.name].time = 0;
                //soldAnimObj.animation.CrossFade(anim_End.name, animMovingToAnimEndCrossfadeTime);

                soldInfo.StartNewMainAnimWithCrossfadeTime(anim_End, animMovingToAnimEndCrossfadeTime);
            }
            else
            {
                //soldAnimObj.animation[anim_End_ToCrossfadeForNextAct.name].time = 0;
                //soldAnimObj.animation.CrossFade(anim_End_ToCrossfadeForNextAct.name, endMovingCrossfadeTime);

                soldInfo.StartNewMainAnimWithCrossfadeTime(anim_End_ToCrossfadeForNextAct, endMovingCrossfadeTime);
            }

            soldStatus = SoldStatusEnum.EndingNormal03;
            goto EndControllingSoldStatus;
        }
        #endregion

        #region EndingNormal03
        if (soldStatus == SoldStatusEnum.EndingNormal03)
        {
            if (CheckIfShouldFinishSetFinished())
                return;
        }
        #endregion

        #region EarlyEnd01
        if (soldStatus == SoldStatusEnum.EarlyEnd01)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            if (anim_End != "")
            {
                soldInfo.StartNewMainAnimWithCrossfadeTime(anim_End, animMovingToAnimEndCrossfadeTime);
            }
            else
            {
                soldInfo.StartNewMainAnimWithCrossfadeTime(anim_End_ToCrossfadeForNextAct, endMovingCrossfadeTime);
            }

            soldStatus = SoldStatusEnum.EarlyEnd02;
            goto EndControllingSoldStatus;
        }
        #endregion

        #region EarlyEnd02
        if (soldStatus == SoldStatusEnum.EarlyEnd02)
        {
            if (CheckIfShouldFinishSetFinished())
                return;

            SetFinished(true);
            return;
        }
        #endregion

    EndControllingSoldStatus:

        //mf
        if (gentlyRotateUpBodyToMid)
            GentlySetUpBodyAngleToItsForward();

        if (fightWhileMove && nowInMoveFight)
            SetMoveFightAnimsWeight();
        //~mf

        return;
        #endregion
    }

    public override bool ShouldTakeDamage(DamageInfo dmg)
    {
        if (base.ShouldTakeDamage(dmg))
        {
            return true;
        }

        return false;
    }

    public override void SetFinished(bool OK)
    {
        base.SetFinished(OK);

        soldInfo.SetIsMoving(false);

        isBeforeEndAnimRun = false;

        if (nowIn_Moving_MoveFight_Switching)
        {
            End_Move_MoveFight_Switching();
        }

        if (nowInMoveFight)
        {
            EndMoveFight();
            End_Move_MoveFight_Switching();
        }
    }

    //

    float CalcPathLength(int startIndex, int endIndex)
    {
        float length = 0;

        if (path.Length == 0)
            return length;

        int startI = startIndex;
        int endI = endIndex;

        Vector3 dist3D = path[startI] - soldTr.position;
        Vector3 dist2D = new Vector3(dist3D.x, 0, dist3D.z);
        length += dist2D.magnitude;

        for (int i = startI; i < endI; i++)
        {
            dist3D = path[i + 1] - path[i];
            dist2D = new Vector3(dist3D.x, 0, dist3D.z);
            length += dist2D.magnitude;
        }

        return length;
    }

    bool NeedsToGoToEndAcc()
    {
        return remainingPathLength < dxEnd;
    }

    void Reset_MoveFightStartDelayTime()
    {
        time_MoveFightStartDelayTime_Counter = Random.Range(time_MoveFightStartDelayTime_Min, time_MoveFightStartDelayTime_Max);
    }

    bool IsRemainingDistantOkForMoveFight()
    {
        return ((remainingPathLength / v) - tEndAcc - minNeededTimeBeforeEndingToStartMoveFight > 0);
    }

    bool ShouldStartMoveFight()
    {
        if (!fightWhileMove)
            return false;

        if (!soldInfo.IsEnemyAround())
            return false;

        List<CharRaycastResult> attackableChars = GetOkAttackableCharsForMoveFight();

        if (attackableChars.Count == 0)
            return false;

        return true;
    }

    List<CharRaycastResult> GetOkAttackableCharsForMoveFight()
    {
        List<CharRaycastResult> charResults = mapLogic.GetAttackableEnemiesForFightInfo(moveFightInfo, controlledSoldier.gameObject, initialEnemies, controlledSoldier.position, controlledSoldier.rotation);

        List<CharRaycastResult> okChars = new List<CharRaycastResult>();

        foreach (CharRaycastResult charRes in charResults)
        {
            if (IsTargetOkAsMoveFightTarget(charRes))
                okChars.Add(charRes);
        }

        okChars = mapLogic.RateEnemiesAndSort(okChars, controlledSoldier.gameObject, controlledSoldier.position);

        return okChars;
    }

    CharRaycastResult GetBestFightTarget()
    {
        List<CharRaycastResult> charReses = GetOkAttackableCharsForMoveFight();

        if (charReses.Count == 0)
            return null;

        return charReses[0];
    }

    void SetNewMoveFightTarget(CharRaycastResult _target)
    {
        if (moveFightTarget != null && moveFightTarget.character != null)
            moveFightTarget.character.GetComponent<CharacterInfo>().RemoveTargettingEnemy(controlledSoldier.gameObject);

        moveFightTarget = _target;

        if (moveFightTarget != null && moveFightTarget.character != null)
            moveFightTarget.character.GetComponent<CharacterInfo>().AddTargettingEnemy(controlledSoldier.gameObject);

        lockRotationOnTarget = false;
        time_MoveFightTargetRecheck_Counter = time_MoveFightTargetRecheck_Max;
    }

    bool IsTargetOkAsMoveFightTarget(CharRaycastResult _target)
    {
        //CharRaycastResult rayCastRes = new CharRaycastResult();

        if (_target == null)
            return false;

        if (_target.character == null)
            return false;

        if (!mapLogic.IsCharacterOkAsTarget(controlledSoldier.gameObject, _target.character, soldBodyInfo.soldierHeadTr.position, controlledSoldier.rotation, soldInfo.fightRange, -1 * moveFightHalfAngle, moveFightHalfAngle, out _target))
            return false;

        if (!_target.isCharacterHitted)
            return false;

        if (SoldierStats.GetSoldierGunDirectionForTarget(controlledSoldier.gameObject, _target.character) != SoldierGunDirectionEnum.Forward)
            return false;

        if (!GeneralStats.IsVecInView(_target.character.transform.position, controlledSoldier.position, controlledSoldier.rotation, -1 * moveFightHalfAngle, moveFightHalfAngle, soldInfo.fightRange))
            return false;

        return true;
    }

    //

    void InitMoveFight()
    {
        nowInMoveFight = true;

        moveFight_UpBodyWeight_Move = 1;

        soldAnimObj.animation[anim_UpBody_Move_Mid].weight = 1f;
        soldAnimObj.animation[anim_UpBody_Move_Mid].layer = 1;
        soldAnimObj.animation[anim_UpBody_Move_Mid].enabled = true;

        soldAnimObj.animation[anim_UpBody_Move_Legs].weight = 1f;
        soldAnimObj.animation[anim_UpBody_Move_Legs].layer = 10;
        soldAnimObj.animation[anim_UpBody_Move_Legs].enabled = true;

        soldAnimObj.animation[anim_UpBody_Move_Legs].AddMixingTransform(soldierLeftFootTr);
        soldAnimObj.animation[anim_UpBody_Move_Legs].AddMixingTransform(soldierRightFootTr);

        soldAnimObj.animation[anim_UpBody_MoveShootIdle_Left].weight = 0;
        soldAnimObj.animation[anim_UpBody_MoveShootIdle_Right].weight = 0;
        soldAnimObj.animation[anim_UpBody_MoveShootIdle_Mid].weight = 0;
        soldAnimObj.animation[anim_UpBody_MoveShootIdle_Left].enabled = true;
        soldAnimObj.animation[anim_UpBody_MoveShootIdle_Right].enabled = true;
        soldAnimObj.animation[anim_UpBody_MoveShootIdle_Mid].enabled = true;
        soldAnimObj.animation[anim_UpBody_MoveShootIdle_Left].layer = 3;
        soldAnimObj.animation[anim_UpBody_MoveShootIdle_Right].layer = 3;
        soldAnimObj.animation[anim_UpBody_MoveShootIdle_Mid].layer = 3;

        soldAnimObj.animation[anim_UpBody_MoveReload_Left].weight = 0;
        soldAnimObj.animation[anim_UpBody_MoveReload_Right].weight = 0;
        soldAnimObj.animation[anim_UpBody_MoveReload_Mid].weight = 0;
        soldAnimObj.animation[anim_UpBody_MoveReload_Left].enabled = true;
        soldAnimObj.animation[anim_UpBody_MoveReload_Right].enabled = true;
        soldAnimObj.animation[anim_UpBody_MoveReload_Mid].enabled = true;
        soldAnimObj.animation[anim_UpBody_MoveReload_Left].layer = 5;
        soldAnimObj.animation[anim_UpBody_MoveReload_Right].layer = 5;
        soldAnimObj.animation[anim_UpBody_MoveReload_Mid].layer = 5;


        soldAnimObj.animation[anim_UpBody_MoveShoot_Left].weight = 0;
        soldAnimObj.animation[anim_UpBody_MoveShoot_Right].weight = 0;
        soldAnimObj.animation[anim_UpBody_MoveShoot_Mid].weight = 0;
        soldAnimObj.animation[anim_UpBody_MoveShoot_Left].enabled = true;
        soldAnimObj.animation[anim_UpBody_MoveShoot_Right].enabled = true;
        soldAnimObj.animation[anim_UpBody_MoveShoot_Mid].enabled = true;
        soldAnimObj.animation[anim_UpBody_MoveShoot_Left].layer = 5;
        soldAnimObj.animation[anim_UpBody_MoveShoot_Right].layer = 5;
        soldAnimObj.animation[anim_UpBody_MoveShoot_Mid].layer = 5;

        for (int i = 0; i < anims_UpBody_MoveDamage_Mid.Length; i++)
        {

            soldAnimObj.animation[anims_UpBody_MoveDamage_Mid[i]].weight = 0;
            soldAnimObj.animation[anims_UpBody_MoveDamage_Mid[i]].enabled = true;
            soldAnimObj.animation[anims_UpBody_MoveDamage_Mid[i]].layer = 5;

            soldAnimObj.animation[anims_UpBody_MoveDamage_Left[i]].weight = 0;
            soldAnimObj.animation[anims_UpBody_MoveDamage_Left[i]].enabled = true;
            soldAnimObj.animation[anims_UpBody_MoveDamage_Left[i]].layer = 5;

            soldAnimObj.animation[anims_UpBody_MoveDamage_Right[i]].weight = 0;
            soldAnimObj.animation[anims_UpBody_MoveDamage_Right[i]].enabled = true;
            soldAnimObj.animation[anims_UpBody_MoveDamage_Right[i]].layer = 5;
        }

        SetMoveFightAnimsWeight();
    }

    void EndMoveFight()
    {
        soldAnimObj.animation[anim_UpBody_Move_Legs].RemoveMixingTransform(soldierLeftFootTr);
        soldAnimObj.animation[anim_UpBody_Move_Legs].RemoveMixingTransform(soldierRightFootTr);

        Reset_MoveFightStartDelayTime();
        nowInMoveFight = false;
        lockRotationOnTarget = false;
        gentlyRotateUpBodyToMid = false;

        if (moveFightTarget != null && moveFightTarget.character != null)
            moveFightTarget.character.GetComponent<CharacterInfo>().RemoveTargettingEnemy(controlledSoldier.gameObject);

        moveFight_UpBodyWeight_Move = 0;

        soldAnimObj.animation[anim_UpBody_MoveShootIdle_Left].layer = 0;
        soldAnimObj.animation[anim_UpBody_MoveShootIdle_Right].layer = 0;
        soldAnimObj.animation[anim_UpBody_MoveShootIdle_Mid].layer = 0;
        soldAnimObj.animation[anim_UpBody_MoveShootIdle_Left].weight = 0;
        soldAnimObj.animation[anim_UpBody_MoveShootIdle_Right].weight = 0;
        soldAnimObj.animation[anim_UpBody_MoveShootIdle_Mid].weight = 0;
        soldAnimObj.animation[anim_UpBody_MoveShootIdle_Left].enabled = false;
        soldAnimObj.animation[anim_UpBody_MoveShootIdle_Right].enabled = false;
        soldAnimObj.animation[anim_UpBody_MoveShootIdle_Mid].enabled = false;

        soldAnimObj.animation[anim_UpBody_MoveReload_Left].layer = 0;
        soldAnimObj.animation[anim_UpBody_MoveReload_Right].layer = 0;
        soldAnimObj.animation[anim_UpBody_MoveReload_Mid].layer = 0;
        soldAnimObj.animation[anim_UpBody_MoveReload_Left].weight = 0;
        soldAnimObj.animation[anim_UpBody_MoveReload_Right].weight = 0;
        soldAnimObj.animation[anim_UpBody_MoveReload_Mid].weight = 0;
        soldAnimObj.animation[anim_UpBody_MoveReload_Left].enabled = false;
        soldAnimObj.animation[anim_UpBody_MoveReload_Right].enabled = false;
        soldAnimObj.animation[anim_UpBody_MoveReload_Mid].enabled = false;

        soldAnimObj.animation[anim_UpBody_MoveShoot_Left].layer = 0;
        soldAnimObj.animation[anim_UpBody_MoveShoot_Right].layer = 0;
        soldAnimObj.animation[anim_UpBody_MoveShoot_Mid].layer = 0;
        soldAnimObj.animation[anim_UpBody_MoveShoot_Left].weight = 0;
        soldAnimObj.animation[anim_UpBody_MoveShoot_Right].weight = 0;
        soldAnimObj.animation[anim_UpBody_MoveShoot_Mid].weight = 0;
        soldAnimObj.animation[anim_UpBody_MoveShoot_Left].enabled = false;
        soldAnimObj.animation[anim_UpBody_MoveShoot_Right].enabled = false;
        soldAnimObj.animation[anim_UpBody_MoveShoot_Mid].enabled = false;

        for (int i = 0; i < anims_UpBody_MoveDamage_Mid.Length; i++)
        {
            soldAnimObj.animation[anims_UpBody_MoveDamage_Mid[i]].layer = 0;
            soldAnimObj.animation[anims_UpBody_MoveDamage_Mid[i]].weight = 0;
            soldAnimObj.animation[anims_UpBody_MoveDamage_Mid[i]].enabled = false;

            soldAnimObj.animation[anims_UpBody_MoveDamage_Left[i]].layer = 0;
            soldAnimObj.animation[anims_UpBody_MoveDamage_Left[i]].weight = 0;
            soldAnimObj.animation[anims_UpBody_MoveDamage_Left[i]].enabled = false;

            soldAnimObj.animation[anims_UpBody_MoveDamage_Right[i]].layer = 0;
            soldAnimObj.animation[anims_UpBody_MoveDamage_Right[i]].weight = 0;
            soldAnimObj.animation[anims_UpBody_MoveDamage_Right[i]].enabled = false;
        }
    }

    void Start_MovingToMoveFight()
    {
        nowIn_Moving_MoveFight_Switching = true;

        soldAnimObj.animation[anim_UpBody_Move_Mid].weight = 0f;
        soldAnimObj.animation[anim_UpBody_Move_Mid].layer = 1;
        soldAnimObj.animation[anim_UpBody_Move_Mid].enabled = true;


        soldAnimObj.animation[anim_UpBody_Move_Legs].layer = 10;
        soldAnimObj.animation[anim_UpBody_Move_Legs].weight = 0;
        soldAnimObj.animation[anim_UpBody_Move_Legs].enabled = true;
    }

    void End_Move_MoveFight_Switching()
    {
        nowIn_Moving_MoveFight_Switching = false;

        soldAnimObj.animation[anim_UpBody_Move_Mid].weight = 0;
        soldAnimObj.animation[anim_UpBody_Move_Mid].layer = 0;
        soldAnimObj.animation[anim_UpBody_Move_Mid].enabled = false;

        soldAnimObj.animation[anim_UpBody_Move_Legs].weight = 0;
        soldAnimObj.animation[anim_UpBody_Move_Legs].layer = 0;
        soldAnimObj.animation[anim_UpBody_Move_Legs].enabled = false;
    }

    void SetMoveFightAnimsWeight()
    {
        soldAnimObj.animation[anim_UpBody_Move_Mid].weight = moveFight_UpBodyWeight_Move;
        soldAnimObj.animation[anim_UpBody_Move_Mid].enabled = (soldAnimObj.animation[anim_UpBody_Move_Mid].weight > 0);
        if (soldAnimObj.animation[anim_UpBody_Move_Mid].enabled)
            soldAnimObj.animation[anim_UpBody_Move_Mid].time = soldAnimObj.animation[anim_Move].time;

        soldAnimObj.animation[anim_UpBody_Move_Legs].weight = moveFight_UpBodyWeight_Move;
        soldAnimObj.animation[anim_UpBody_Move_Legs].enabled = (soldAnimObj.animation[anim_UpBody_Move_Legs].weight > 0);
        if (soldAnimObj.animation[anim_UpBody_Move_Legs].enabled)
            soldAnimObj.animation[anim_UpBody_Move_Legs].time = soldAnimObj.animation[anim_Move].time;

        soldAnimObj.animation[anim_UpBody_MoveShootIdle_Left].weight = Mathf.Clamp(moveFight_UpBodyWeight_MoveShootIdle * moveFight_Weight_Left, 0, 1);
        //soldAnimObj.animation[anim_UpBody_MoveShootIdle_Left].enabled = (soldAnimObj.animation[anim_UpBody_MoveShootIdle_Left].weight > 0);
        soldAnimObj.animation[anim_UpBody_MoveShootIdle_Left].enabled = true;

        soldAnimObj.animation[anim_UpBody_MoveShootIdle_Right].weight = Mathf.Clamp(moveFight_UpBodyWeight_MoveShootIdle * moveFight_Weight_Right, 0, 1);
        //soldAnimObj.animation[anim_UpBody_MoveShootIdle_Right].enabled = (soldAnimObj.animation[anim_UpBody_MoveShootIdle_Right].weight > 0);
        soldAnimObj.animation[anim_UpBody_MoveShootIdle_Right].enabled = true;

        soldAnimObj.animation[anim_UpBody_MoveShootIdle_Mid].weight = Mathf.Clamp(moveFight_UpBodyWeight_MoveShootIdle * moveFight_Weight_Mid, 0, 1);
        //soldAnimObj.animation[anim_UpBody_MoveShootIdle_Mid].enabled = (soldAnimObj.animation[anim_UpBody_MoveShootIdle_Mid].weight > 0);
        soldAnimObj.animation[anim_UpBody_MoveShootIdle_Mid].enabled = true;

        soldAnimObj.animation[anim_UpBody_MoveReload_Left].weight = Mathf.Clamp(moveFight_UpBodyWeight_MoveReload * moveFight_Weight_Left, 0, 1);
        //soldAnimObj.animation[anim_UpBody_MoveReload_Left].enabled = (soldAnimObj.animation[anim_UpBody_MoveReload_Left].weight > 0);
        soldAnimObj.animation[anim_UpBody_MoveReload_Left].enabled = true;

        soldAnimObj.animation[anim_UpBody_MoveReload_Right].weight = Mathf.Clamp(moveFight_UpBodyWeight_MoveReload * moveFight_Weight_Right, 0, 1);
        //soldAnimObj.animation[anim_UpBody_MoveReload_Right].enabled = (soldAnimObj.animation[anim_UpBody_MoveReload_Right].weight > 0);
        soldAnimObj.animation[anim_UpBody_MoveReload_Right].enabled = true;

        soldAnimObj.animation[anim_UpBody_MoveReload_Mid].weight = Mathf.Clamp(moveFight_UpBodyWeight_MoveReload * moveFight_Weight_Mid, 0, 1);
        //soldAnimObj.animation[anim_UpBody_MoveReload_Mid].enabled = (soldAnimObj.animation[anim_UpBody_MoveReload_Mid].weight > 0);
        soldAnimObj.animation[anim_UpBody_MoveReload_Mid].enabled = true;

        soldAnimObj.animation[anim_UpBody_MoveShoot_Left].weight = Mathf.Clamp(moveFight_UpBodyWeight_MoveShoot * moveFight_Weight_Left, 0, 1);
        //soldAnimObj.animation[anim_UpBody_MoveShoot_Left].enabled = (soldAnimObj.animation[anim_UpBody_MoveShoot_Left].weight > 0);
        soldAnimObj.animation[anim_UpBody_MoveShoot_Left].enabled = true;

        soldAnimObj.animation[anim_UpBody_MoveShoot_Right].weight = Mathf.Clamp(moveFight_UpBodyWeight_MoveShoot * moveFight_Weight_Right, 0, 1);
        //soldAnimObj.animation[anim_UpBody_MoveShoot_Right].enabled = (soldAnimObj.animation[anim_UpBody_MoveShoot_Right].weight > 0);
        soldAnimObj.animation[anim_UpBody_MoveShoot_Right].enabled = true;

        soldAnimObj.animation[anim_UpBody_MoveShoot_Mid].weight = Mathf.Clamp(moveFight_UpBodyWeight_MoveShoot * moveFight_Weight_Mid, 0, 1);
        //soldAnimObj.animation[anim_UpBody_MoveShoot_Mid].enabled = (soldAnimObj.animation[anim_UpBody_MoveShoot_Mid].weight > 0);
        soldAnimObj.animation[anim_UpBody_MoveShoot_Mid].enabled = true;

        soldAnimObj.animation[anims_UpBody_MoveDamage_Left[animUpBodyMoveDmgSelectedIndex]].weight = Mathf.Clamp(moveFight_UpBodyWeight_MoveDamage * moveFight_Weight_Left, 0, 1);
        //soldAnimObj.animation[anims_UpBody_MoveDamage_Left[animUpBodyMoveDmgSelectedIndex]].enabled = (soldAnimObj.animation[anims_UpBody_MoveDamage_Left[animUpBodyMoveDmgSelectedIndex]].weight > 0);
        soldAnimObj.animation[anims_UpBody_MoveDamage_Left[animUpBodyMoveDmgSelectedIndex]].enabled = true;

        soldAnimObj.animation[anims_UpBody_MoveDamage_Right[animUpBodyMoveDmgSelectedIndex]].weight = Mathf.Clamp(moveFight_UpBodyWeight_MoveDamage * moveFight_Weight_Right, 0, 1);
        //soldAnimObj.animation[anims_UpBody_MoveDamage_Right[animUpBodyMoveDmgSelectedIndex]].enabled = (soldAnimObj.animation[anims_UpBody_MoveDamage_Right[animUpBodyMoveDmgSelectedIndex]].weight > 0);
        soldAnimObj.animation[anims_UpBody_MoveDamage_Right[animUpBodyMoveDmgSelectedIndex]].enabled = true;

        soldAnimObj.animation[anims_UpBody_MoveDamage_Mid[animUpBodyMoveDmgSelectedIndex]].weight = Mathf.Clamp(moveFight_UpBodyWeight_MoveDamage * moveFight_Weight_Mid, 0, 1);
        //soldAnimObj.animation[anims_UpBody_MoveDamage_Mid[animUpBodyMoveDmgSelectedIndex]].enabled = (soldAnimObj.animation[anims_UpBody_MoveDamage_Mid[animUpBodyMoveDmgSelectedIndex]].weight > 0);
        soldAnimObj.animation[anims_UpBody_MoveDamage_Mid[animUpBodyMoveDmgSelectedIndex]].enabled = true;
    }

    //

    void ResetMoveFightDamageAnims()
    {
        animUpBodyMoveDmgSelectedIndex = Random.Range(0, anims_UpBody_MoveDamage_Mid.Length);

        soldAnimObj.animation[anims_UpBody_MoveDamage_Left[animUpBodyMoveDmgSelectedIndex]].time = 0;
        soldAnimObj.animation[anims_UpBody_MoveDamage_Right[animUpBodyMoveDmgSelectedIndex]].time = 0;
        soldAnimObj.animation[anims_UpBody_MoveDamage_Mid[animUpBodyMoveDmgSelectedIndex]].time = 0;
    }

    void ResetMoveFightReloadAnims()
    {
        soldAnimObj.animation[anim_UpBody_MoveReload_Left].time = 0;
        soldAnimObj.animation[anim_UpBody_MoveReload_Right].time = 0;
        soldAnimObj.animation[anim_UpBody_MoveReload_Mid].time = 0;
    }

    void ResetMoveFightShootAnims()
    {
        soldAnimObj.animation[anim_UpBody_MoveShoot_Left].time = 0;
        soldAnimObj.animation[anim_UpBody_MoveShoot_Right].time = 0;
        soldAnimObj.animation[anim_UpBody_MoveShoot_Mid].time = 0;
    }

    bool GentlySetUpBodyAngleToPosition(Vector3 _pos)
    {
        Vector3 pos = _pos;
        float rotSpeed = moveFight_RotSpeed;

        float deltaAngle = GetSoldDeltaAngleToPosition(pos);

        float rotAmount = rotSpeed * Time.deltaTime;

        if (Mathf.Abs(rotAmount) >= Mathf.Abs(deltaAngle - currentUpBodyAngle))
        {
            currentUpBodyAngle = Mathf.Clamp(deltaAngle, -moveFightHalfAngle, moveFightHalfAngle);
            SetUpBodyRotateWights();
            return true;
        }

        float dif = deltaAngle - currentUpBodyAngle;

        if (dif >= 0)
        {
            currentUpBodyAngle += rotAmount;
        }
        else
        {
            currentUpBodyAngle -= rotAmount;
        }

        SetUpBodyRotateWights();
        return false;
    }

    bool GentlySetUpBodyAngleToTarget()
    {
        if (moveFightTarget == null)
        {
            lockRotationOnTarget = false;
            return false;
        }

        if (!mapLogic.IsCharacterTotallyFightable(moveFightTarget.character))
        {
            lockRotationOnTarget = false;
            return false;
        }

        float deltaAngle = GetSoldDeltaAngleToMoveTarget();

        if (!lockRotationOnTarget)
        {
            float rotAmount = moveFight_RotSpeed * Time.deltaTime;

            if (Mathf.Abs(rotAmount) >= Mathf.Abs(deltaAngle - currentUpBodyAngle))
            {
                currentUpBodyAngle = Mathf.Clamp(deltaAngle, -moveFightHalfAngle, moveFightHalfAngle);
                SetUpBodyRotateWights();
                lockRotationOnTarget = true;
                return true;
            }

            float dif = deltaAngle - currentUpBodyAngle;

            if (dif >= 0)
            {
                currentUpBodyAngle += rotAmount;
            }
            else
            {
                currentUpBodyAngle -= rotAmount;
            }

            SetUpBodyRotateWights();
            lockRotationOnTarget = false;
            return false;
        }
        else
        {
            currentUpBodyAngle = Mathf.Clamp(deltaAngle, -moveFightHalfAngle, moveFightHalfAngle);
            SetUpBodyRotateWights();
            return true;
        }

        return false;
    }

    bool GentlySetUpBodyAngleToItsForward()
    {
        return GentlySetUpBodyAngleToPosition(controlledSoldier.position + controlledSoldier.forward);
    }

    float GetSoldDeltaAngleToPosition(Vector3 _pos)
    {
        return MathfPlus.GetDeltaAngle(controlledSoldier.forward, controlledSoldier.position, _pos);
    }

    float GetSoldDeltaAngleToMoveTarget()
    {
        return GetSoldDeltaAngleToPosition(moveFightTarget.character.transform.position);
    }

    void SetUpBodyRotateWights()
    {
        moveFight_Weight_Mid = (maxUpbodyAnimHalfAngle - Mathf.Abs(currentUpBodyAngle)) / maxUpbodyAnimHalfAngle;

        if (currentUpBodyAngle >= 0)
            moveFight_Weight_Left = 0;
        else
            moveFight_Weight_Left = Mathf.Abs(currentUpBodyAngle) / maxUpbodyAnimHalfAngle;

        if (currentUpBodyAngle <= 0)
            moveFight_Weight_Right = 0;
        else
            moveFight_Weight_Right = currentUpBodyAngle / maxUpbodyAnimHalfAngle;
    }

    bool NeedsReload()
    {
        return soldGun.NeedsReload();
    }

    bool IsSoldOnPoint(GameObject _soldier, Vector3 _pos, float _maxError)
    {
        return Vector3.Distance(_soldier.transform.position, _pos) <= _maxError;
    }

    void SetRotAmountOnFrame(float _val)
    {
        rotAmountOnFrame = Mathf.Clamp(_val, -maxRotOnFrame, maxRotOnFrame);
    }

    //public void PathFinished(Pathfinding.Path p)
    //{
    //    path = p.vectorPath;
    //}

    //mf

    //void FirstInitAnim_MoveMidRun()
    //{
    //    //soldAnimObj.animation[anim_UpBody_Move_Mid].weight = 1f;
    //    //soldAnimObj.animation[anim_UpBody_Move_Mid].layer = 1;
    //    //soldAnimObj.animation[anim_UpBody_Move_Mid].enabled = true;
    //    //soldAnimObj.animation[anim_UpBody_Move_Mid].time = soldAnimObj.animation[anim_Move].time;
    //}

    bool ShouldPlayFootstepSound()
    {
        bool timeIsOk = (footStep_TimeCounter == 0);
        float vel = GetHorizVelocity();

        return (timeIsOk && (vel >= footStep_MinNeedVelocity));
    }

    float GetHorizVelocity()
    {
        return v;
    }

    bool ShouldResetFootstepTimeCounter()
    {
        float vel = GetHorizVelocity();
        return (vel < footStep_MinNeedVelocity);
    }

    bool ShouldFinish()
    {
        return (needsToBeFinished && evenStopMovingForFinish);
    }

    bool CheckIfShouldFinishSetFinished()
    {
        if (ShouldFinish())
        {
            SetFinished(false);
            return true;
        }

        return false;
    }

    void OnDestroy()
    {
        if (soldInfo != null)
            soldInfo.SetIsMoving(false);
    }
}
