using UnityEngine;
using System.Collections;

public class SoldierMovementInfo : MonoBehaviour
{
    public MovementTypeEnum movementType;
    public float maxSpeed;
    public float firstAccelerationTime;
    public float endAccelerationTime;
    public float startMovingCrossfadeTime;
    public float endMovingCrossfadeTime;
    public AnimsList animsMove = null;
    public SoldierDamageAnimPack animPackMoveDamage = null;
    public bool moveFightEnabled = false;

    //mf
    public AnimsList anim_UpBody_Move_Mid;
    public AnimsList anim_UpBody_Move_Legs;
    public AnimsList anim_UpBody_MoveShootIdle_Left;
    public AnimsList anim_UpBody_MoveShootIdle_Mid;
    public AnimsList anim_UpBody_MoveShootIdle_Right;
    public AnimsList anim_UpBody_MoveReload_Left;
    public AnimsList anim_UpBody_MoveReload_Mid;
    public AnimsList anim_UpBody_MoveReload_Right;
    public AnimsList anim_UpBody_MoveShoot_Left;
    public AnimsList anim_UpBody_MoveShoot_Mid;
    public AnimsList anim_UpBody_MoveShoot_Right;

    public AnimsList anims_UpBody_MoveDamage_Left;
    public AnimsList anims_UpBody_MoveDamage_Mid;
    public AnimsList anims_UpBody_MoveDamage_Right;
    //~mf
}
