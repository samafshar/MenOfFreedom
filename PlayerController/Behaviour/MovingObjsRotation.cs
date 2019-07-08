using UnityEngine;
using System.Collections;

public enum BulletHitCameraRotState
{
    None,
    Rafting_Start,
    Rafting_Update,
    Bargashting_Start,
    Bargashting_Update,
}

public class MovingObjsRotation : MonoBehaviour
{
    public float sensitivityY = 15f;

    public float minimumY = -60F;
    public float maximumY = 60F;

    [HideInInspector]
    public Transform RotationBone;
    [HideInInspector]
    public Transform PositionBone;

    float rotTolerance = 0.0f;

    PlayerCharacterNew player;

    float bulHit_MaxDistFactor = 50f;

    float bulHit_MaxDecreasementByDist = 0.5f;

    float bulHit_RaftAxolamalTime = 0.07f;
    float bulHit_BargashtAxolamalTime = 0.14f;

    float bulHit_MotaghayyerSpeedToghsValue = 0.7f;

    float bulHit_ZaribAngle = 90;

    BulletHitCameraRotState bulHit_StateY = BulletHitCameraRotState.None;
    float bulHit_NewBulletMaxAdditionalY = 0.035f;
    float bulHit_MaxRotY = 0.035f;
    float bulHit_CurRotY = 0;
    float bulHit_TargetRotY = 0;
    float bulHit_CurSpeedY = 0;
    float bulHit_MaxSpeedY = 1000f;
    float bulHit_InitialCurRotYInBargasht = 0;
    float rotationY = 0;


    BulletHitCameraRotState bulHit_StateZ = BulletHitCameraRotState.None;
    float bulHit_NewBulletMaxAdditionalZ = 0.05f;
    float bulHit_MaxRotZ = 0.05f;
    float bulHit_CurRotZ = 0;
    float bulHit_TargetRotZ = 0;
    float bulHit_CurSpeedZ = 0;
    float bulHit_MaxSpeedZ = 1000f;
    float bulHit_InitialCurRotZInBargasht = 0;
    float rotationZ = 0;

    bool fireShouldCount = false;
    float kick = 0f;
    float kickChangeAmount = 0;

    float raftKickCoef = 12;
    float bargashtKickCoef = 8;

    Transform camerasRoot;

    void Awake()
    {

    }

    // Use this for initialization
    void Start()
    {
        player = PlayerCharacterNew.Instance;

        camerasRoot = player.camerasRoot;
    }

    // Update is called once per frame
    void Update()
    {

        if (!PlayerCharacterNew.Instance.IsGamePaused())
        {
            if (!PlayerCharacterNew.Instance.IsPlayerStopped())
            {
                float mouseRotY = CustomInputManager.GetPlayerCamAxisY(); //Input.GetAxis("Mouse Y") * sensitivityY;

                if (player.isOnSnipeMode)
                    mouseRotY *= player.snipeModeMouseSensitivityReductionCoef;

                rotationY += mouseRotY;

                #region FireKick
                if (!fireShouldCount)
                {
                    if (kickChangeAmount > 0)
                    {
                        if (kickChangeAmount - Time.deltaTime * bargashtKickCoef > 0)
                        {
                            rotationY -= Time.deltaTime * bargashtKickCoef;
                            kickChangeAmount -= Time.deltaTime * bargashtKickCoef;
                        }
                        else
                        {
                            rotationY -= kickChangeAmount;
                            kickChangeAmount = 0;
                        }
                    }
                    else
                        kickChangeAmount = 0;
                }

                if (fireShouldCount)
                {
                    rotationY += Time.deltaTime * raftKickCoef;
                    kickChangeAmount += Time.deltaTime * raftKickCoef;
                    kick -= Time.deltaTime * raftKickCoef;
                    if (kick < 0)
                        fireShouldCount = false;
                }
                #endregion

                #region Y Rafting_Start
                if (bulHit_StateY == BulletHitCameraRotState.Rafting_Start)
                {
                    bulHit_StateY = BulletHitCameraRotState.Rafting_Update;
                }
                #endregion

                #region Y Rafting_Update
                if (bulHit_StateY == BulletHitCameraRotState.Rafting_Update)
                {
                    float oldDif = bulHit_TargetRotY - bulHit_CurRotY;

                    if (IsValueReached(oldDif))
                    {
                        bulHit_CurRotY = bulHit_TargetRotY;

                        bulHit_StateY = BulletHitCameraRotState.Bargashting_Start;
                        goto EndBulHitRotY;
                    }

                    float motSpeed = GetMotaghayyerSpeed(bulHit_TargetRotY - bulHit_CurRotY, bulHit_RaftAxolamalTime, bulHit_MaxSpeedY);

                    bulHit_CurRotY += (bulHit_CurSpeedY + motSpeed) * Time.deltaTime;

                    float newDif = bulHit_TargetRotY - bulHit_CurRotY;

                    if (IsValueReached(newDif))
                    {
                        bulHit_CurRotY = bulHit_TargetRotY;

                        bulHit_StateY = BulletHitCameraRotState.Bargashting_Start;
                        goto EndBulHitRotY;
                    }

                    if ((newDif > 0 && oldDif < 0) || (newDif < 0 && oldDif > 0))
                    {
                        bulHit_CurRotY = bulHit_TargetRotY;

                        bulHit_StateY = BulletHitCameraRotState.Bargashting_Start;
                        goto EndBulHitRotY;
                    }
                }
                #endregion

                #region Y Bargashting_Start
                if (bulHit_StateY == BulletHitCameraRotState.Bargashting_Start)
                {
                    bulHit_CurRotY = bulHit_TargetRotY;

                    bulHit_TargetRotY = 0;

                    bulHit_CurSpeedY = GetNewRotSpeed(bulHit_CurRotY, bulHit_TargetRotY, bulHit_BargashtAxolamalTime, bulHit_MaxSpeedY);

                    bulHit_InitialCurRotYInBargasht = bulHit_CurRotY;

                    bulHit_StateY = BulletHitCameraRotState.Bargashting_Update;
                }
                #endregion

                #region Y Bargashting_Update
                if (bulHit_StateY == BulletHitCameraRotState.Bargashting_Update)
                {
                    float oldDif = bulHit_TargetRotY - bulHit_CurRotY;

                    if (IsValueReached(oldDif))
                    {
                        bulHit_CurRotY = 0;

                        bulHit_StateY = BulletHitCameraRotState.None;
                        goto EndBulHitRotY;
                    }

                    float motSpeed = GetMotaghayyerSpeed(bulHit_CurRotY - bulHit_InitialCurRotYInBargasht, bulHit_BargashtAxolamalTime, bulHit_MaxSpeedY);

                    bulHit_CurRotY += (bulHit_CurSpeedY + motSpeed) * Time.deltaTime;

                    float newDif = bulHit_TargetRotY - bulHit_CurRotY;

                    if (IsValueReached(newDif))
                    {
                        bulHit_CurRotY = 0;

                        bulHit_StateY = BulletHitCameraRotState.None;
                        goto EndBulHitRotY;
                    }

                    if ((newDif > 0 && oldDif < 0) || (newDif < 0 && oldDif > 0))
                    {
                        bulHit_CurRotY = 0;

                        bulHit_StateY = BulletHitCameraRotState.None;
                        goto EndBulHitRotY;
                    }
                }
                #endregion

            EndBulHitRotY:

                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                float rotY = bulHit_CurRotY * bulHit_ZaribAngle;

                //---Z----------------------------------------------------------

                #region Z Rafting_Start
                if (bulHit_StateZ == BulletHitCameraRotState.Rafting_Start)
                {
                    bulHit_StateZ = BulletHitCameraRotState.Rafting_Update;
                }
                #endregion

                #region Z Rafting_Update
                if (bulHit_StateZ == BulletHitCameraRotState.Rafting_Update)
                {
                    float oldDif = bulHit_TargetRotZ - bulHit_CurRotZ;

                    if (IsValueReached(oldDif))
                    {
                        bulHit_CurRotZ = bulHit_TargetRotZ;

                        bulHit_StateZ = BulletHitCameraRotState.Bargashting_Start;
                        goto EndBulHitRotZ;
                    }

                    float motSpeed = GetMotaghayyerSpeed(bulHit_TargetRotZ - bulHit_CurRotZ, bulHit_RaftAxolamalTime, bulHit_MaxSpeedZ);

                    bulHit_CurRotZ += (bulHit_CurSpeedZ + motSpeed) * Time.deltaTime;

                    float newDif = bulHit_TargetRotZ - bulHit_CurRotZ;

                    if (IsValueReached(newDif))
                    {
                        bulHit_CurRotZ = bulHit_TargetRotZ;

                        bulHit_StateZ = BulletHitCameraRotState.Bargashting_Start;
                        goto EndBulHitRotZ;
                    }

                    if ((newDif > 0 && oldDif < 0) || (newDif < 0 && oldDif > 0))
                    {
                        bulHit_CurRotZ = bulHit_TargetRotZ;

                        bulHit_StateZ = BulletHitCameraRotState.Bargashting_Start;
                        goto EndBulHitRotZ;
                    }
                }
                #endregion

                #region Z Bargashting_Start
                if (bulHit_StateZ == BulletHitCameraRotState.Bargashting_Start)
                {
                    bulHit_CurRotZ = bulHit_TargetRotZ;

                    bulHit_TargetRotZ = 0;

                    bulHit_CurSpeedZ = GetNewRotSpeed(bulHit_CurRotZ, bulHit_TargetRotZ, bulHit_BargashtAxolamalTime, bulHit_MaxSpeedZ);

                    bulHit_InitialCurRotZInBargasht = bulHit_CurRotZ;

                    bulHit_StateZ = BulletHitCameraRotState.Bargashting_Update;
                }
                #endregion

                #region Z Bargashting_Update
                if (bulHit_StateZ == BulletHitCameraRotState.Bargashting_Update)
                {
                    float oldDif = bulHit_TargetRotZ - bulHit_CurRotZ;

                    if (IsValueReached(oldDif))
                    {
                        bulHit_CurRotZ = 0;

                        bulHit_StateZ = BulletHitCameraRotState.None;
                        goto EndBulHitRotZ;
                    }

                    float motSpeed = GetMotaghayyerSpeed(bulHit_CurRotZ - bulHit_InitialCurRotZInBargasht, bulHit_BargashtAxolamalTime, bulHit_MaxSpeedZ);

                    bulHit_CurRotZ += (bulHit_CurSpeedZ + motSpeed) * Time.deltaTime;

                    float newDif = bulHit_TargetRotZ - bulHit_CurRotZ;

                    if (IsValueReached(newDif))
                    {
                        bulHit_CurRotZ = 0;

                        bulHit_StateZ = BulletHitCameraRotState.None;
                        goto EndBulHitRotZ;
                    }

                    if ((newDif > 0 && oldDif < 0) || (newDif < 0 && oldDif > 0))
                    {
                        bulHit_CurRotZ = 0;

                        bulHit_StateZ = BulletHitCameraRotState.None;
                        goto EndBulHitRotZ;
                    }
                }
                #endregion

            EndBulHitRotZ:

                float rotZ = bulHit_CurRotZ * bulHit_ZaribAngle;

                transform.localEulerAngles = new Vector3(-rotY - rotationY, transform.localEulerAngles.y, rotZ + rotationZ);

                //ExplosionCamShake------------------

                if (player.isCameraShakingByExplosion)
                {
                    float camExpShakeY = player.camExplosionShakeY;

                    if (player.isOnSnipeMode)
                    {
                        camExpShakeY *= player.snipeModeExplosionCameraShakeReductionCoef;
                    }

                    float camExpShakeZ = player.camExplosionShakeZ;

                    if (player.isOnSnipeMode)
                    {
                        camExpShakeZ *= player.snipeModeExplosionCameraShakeReductionCoef;
                    }

                    transform.localEulerAngles += new Vector3(player.camExplosionShakeY, 0, player.camExplosionShakeZ);
                }

                //~ExplosionCamShake-----------------


                //<Cameras>-----------------------------------------------------------------------------------------------------------

                PlayerGun curActiveGun = player.GetActiveGun();

                if (curActiveGun != null)
                {
                    Vector3 initialPos = curActiveGun.posBoneInitialLocalPosition;
                    Quaternion initialRot = curActiveGun.rotBoneInitialLocalRotation;

                    Vector3 camsAdditionalPos = PositionBone.localPosition - initialPos;

                    //float camsAdditionalRotX = -(Mathf.DeltaAngle(RotationBone.localEulerAngles.y, initialRot.eulerAngles.y));
                    //float camsAdditionalRotY = (RotationBone.localEulerAngles.x - initialRot.eulerAngles.x);
                    //float camsAdditionalRotZ = (RotationBone.localEulerAngles.z - initialRot.eulerAngles.z);

                    camerasRoot.localPosition = camsAdditionalPos;
                    //camerasRoot.localEulerAngles = new Vector3(camsAdditionalRotX, camsAdditionalRotY, camsAdditionalRotZ);

                    //<Test>
                    float camsAdditionalRotX = (RotationBone.localEulerAngles.z - initialRot.eulerAngles.z);
                    float camsAdditionalRotY = -(Mathf.DeltaAngle(RotationBone.localEulerAngles.y, initialRot.eulerAngles.y));
                    float camsAdditionalRotZ = (RotationBone.localEulerAngles.x - initialRot.eulerAngles.x);

                    camerasRoot.localEulerAngles = new Vector3(camsAdditionalRotX, camsAdditionalRotY, camsAdditionalRotZ);
                    //</Test>

                    //Snipe

                    if (player.doSnipeLarzeshing)
                    {
                        camerasRoot.localEulerAngles += new Vector3(player.snipeCurLarzeshX, player.snipeCurLarzeshY, 0);
                    }

                    if (player.doSnipeMovingCamShake)
                    {
                        camerasRoot.localEulerAngles += new Vector3(player.movingSnipeX, player.movingSnipeY, 0);
                    }

                    if (player.doSnipeShootCamShake)
                    {
                        camerasRoot.localEulerAngles += new Vector3(-player.snipeShootX, player.snipeShootY, 0);
                    }

                    //~Snipe
                }
                else
                {
                    if (player.isCampPlayer)
                    {
                        Vector3 initialPos = player.campKnife_PosBoneInitialLocalPosition;
                        Quaternion initialRot = player.campKnife_RotBoneInitialLocalRotation;

                        Vector3 camsAdditionalPos = PositionBone.localPosition - initialPos;

                        camerasRoot.localPosition = camsAdditionalPos;

                        //<Test>
                        float camsAdditionalRotX = (RotationBone.localEulerAngles.z - initialRot.eulerAngles.z);
                        float camsAdditionalRotY = -(Mathf.DeltaAngle(RotationBone.localEulerAngles.y, initialRot.eulerAngles.y));
                        float camsAdditionalRotZ = (RotationBone.localEulerAngles.x - initialRot.eulerAngles.x);

                        camerasRoot.localEulerAngles = new Vector3(camsAdditionalRotX, camsAdditionalRotY, camsAdditionalRotZ);
                        //</Test>
                    }
                }

                //</Cameras>----------------------------------------------------------------------------------------------------------
            }
        }
    }

    public void PlayerHitByBullet(DamageInfo _dmg)
    {
        DamageInfo dmg = _dmg;

        if (bulHit_StateY == BulletHitCameraRotState.None)
            InitBulRotY(dmg, 1);

        if (bulHit_StateZ == BulletHitCameraRotState.None)
            InitBulRotZ(dmg, 1);
    }

    public void PlayerHitByExplosion(ExplosionDamageInfo _expDmg)
    {
        ExplosionDamageInfo expDmg = _expDmg;

        DamageInfo dmg = expDmg.damageInfo;

        float exCoef = expDmg.playerDamageSheddat;

        if (bulHit_StateY == BulletHitCameraRotState.None)
            InitBulRotY(dmg, exCoef);

        if (bulHit_StateZ == BulletHitCameraRotState.None)
            InitBulRotZ(dmg, exCoef);
    }

    void InitBulRotY(DamageInfo _dmg, float _ExtraCoef)
    {
        DamageInfo dmg = _dmg;
        float exCoef = _ExtraCoef;

        Vector3 bulPos = dmg.damageSourcePosition;
        Vector3 playerPos = player.transform.position;

        Vector3 playerForward = player.transform.forward;

        playerForward = new Vector3(playerForward.x, 0, playerForward.z);

        Vector3 dist = new Vector3(bulPos.x - playerPos.x, 0, bulPos.z - playerPos.z);

        float dist3DMag = (bulPos - playerPos).magnitude;

        dist3DMag = Mathf.Clamp(dist3DMag, 0, bulHit_MaxDistFactor);

        float ang = Vector3.Angle(dist, playerForward);

        float newBulRotY = (1 - (ang / 90)) * bulHit_NewBulletMaxAdditionalY;

        newBulRotY *= (1 - ((dist3DMag / bulHit_MaxDistFactor) * bulHit_MaxDecreasementByDist));

        if (player.isOnSnipeMode)
            newBulRotY *= player.snipeModeBulletHitPareshReductionCoef;

        newBulRotY *= exCoef;

        bulHit_TargetRotY += newBulRotY;

        bulHit_TargetRotY = Mathf.Clamp(bulHit_TargetRotY, -bulHit_MaxRotY, bulHit_MaxRotY);

        bulHit_StateY = BulletHitCameraRotState.Rafting_Start;

        bulHit_CurSpeedY = GetNewRotSpeed(bulHit_CurRotY, bulHit_TargetRotY, bulHit_RaftAxolamalTime, bulHit_MaxSpeedY);
    }

    void InitBulRotZ(DamageInfo _dmg, float _ExtraCoef)
    {
        DamageInfo dmg = _dmg;
        float exCoef = _ExtraCoef;

        Vector3 bulPos = dmg.damageSourcePosition;
        Vector3 playerPos = player.transform.position;

        Vector3 playerRight = player.transform.right;

        playerRight = new Vector3(playerRight.x, 0, playerRight.z);

        Vector3 dist = new Vector3(bulPos.x - playerPos.x, 0, bulPos.z - playerPos.z);

        float dist3DMag = (bulPos - playerPos).magnitude;

        dist3DMag = Mathf.Clamp(dist3DMag, 0, bulHit_MaxDistFactor);

        float ang = Vector3.Angle(dist, playerRight);

        float newBulRotZ = (1 - (ang / 90)) * bulHit_NewBulletMaxAdditionalZ;

        newBulRotZ *= (1 - ((dist3DMag / bulHit_MaxDistFactor) * bulHit_MaxDecreasementByDist));

        if (player.isOnSnipeMode)
            newBulRotZ *= player.snipeModeBulletHitPareshReductionCoef;

        newBulRotZ *= exCoef;

        bulHit_TargetRotZ += newBulRotZ;

        bulHit_TargetRotZ = Mathf.Clamp(bulHit_TargetRotZ, -bulHit_MaxRotZ, bulHit_MaxRotZ);

        bulHit_StateZ = BulletHitCameraRotState.Rafting_Start;

        bulHit_CurSpeedZ = GetNewRotSpeed(bulHit_CurRotZ, bulHit_TargetRotZ, bulHit_RaftAxolamalTime, bulHit_MaxSpeedZ);
    }

    bool IsValueReached(float _value)
    {
        return Mathf.Abs(_value) <= rotTolerance;
    }

    float GetNewRotSpeed(float _cur, float _targ, float _axolamalTime, float _maxSpeed)
    {
        float cur = _cur;
        float targ = _targ;
        float axolamalTime = _axolamalTime;
        float maxSpeed = _maxSpeed;

        float newSpeed = 0;

        newSpeed = (targ - cur) * ((1 - bulHit_MotaghayyerSpeedToghsValue) / axolamalTime);

        newSpeed = Mathf.Clamp(newSpeed, -maxSpeed, maxSpeed);

        return newSpeed;
    }

    float GetMotaghayyerSpeed(float _dist, float _axolamalTime, float _maxSpeed)
    {
        float dist = _dist;
        float axolamalTime = _axolamalTime;
        float maxSpeed = _maxSpeed;

        float newSpeed = 0;

        newSpeed = dist * (bulHit_MotaghayyerSpeedToghsValue / axolamalTime);

        newSpeed = Mathf.Clamp(newSpeed, -maxSpeed, maxSpeed);

        return newSpeed;
    }

    public void SetBonesFromGuns(Transform pos, Transform rot)
    {
        PositionBone = pos;
        RotationBone = rot;
    }

    void BulletFire()
    {
        fireShouldCount = true;
        kick = 1f;
    }
}