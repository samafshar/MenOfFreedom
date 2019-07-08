using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SoldierGunDirectionEnum
{
    Upward,
    Forward,
    Downward,
}

public static class SoldierStats
{
    public static float soldAnimSpeedCoef = 1.0675f;

    public static float SoldierGravity = -20f;
    //public static float SoldierMaxRange = 150f;
    public static float SoldierRotationAngleToleranceCoef = 0.05f;

    public static float SoldierMaxErrorToPos = 0.5f;

    public static float MaxTimeOfDeadSoldVoice = 7f;

    public static float CurvePointsDistance = 1.03f;

    public static float playerInCriticSituRange_Min = 16;
    public static float playerInCriticSituRange_Max = 19f;


    public static float GunDirectionsDeltaAngle = 16;
    public static float MaxDistForForwardGunDirection = 4f;
    public static float MaxYForForwardGunDirection = 2.2f;

    public static float campSoldier_BackMaxDist = 3.6f;

    public static float campSoldier_BaseViewHalfAngle = 67;
    public static float campSoldier_MaxViewHalfAngle = 78;
    public static float campSoldier_PlayerStandAdditionalViewHalfAngle = 6;
    public static float campSoldier_PlayerHorizSpeedAdditionalViewHalfAnglePer10 = 15;
    public static float campSoldier_ViewRange = 15f;
    public static float campSoldier_PlayerLoudMovingSoundRange = 6f;
    public static float campSoldier_PlayerLoudLandingSoundRange = 8f;
    public static float campSoldier_MaxAdditionalViewRangeFromLights = 20f;
    public static float campSoldier_HiddenAreasMovingEffectToValuePer10 = 22;
    public static float campSoldier_HiddenAreasDefaultEffectValue= 0f;
    public static float campSoldier_PlayerSitCoef = 0.85f;
    public static float campSoldier_PlayerHorizSpeedAdditionalCoefPer10 = 0.6f;
    public static float campSoldier_farViewMinDetectTime = 0.24f;
    public static float campSoldier_farViewMaxDetectTime = 0.36f;
    public static float campSoldier_CampWallMinNeededDist = 5;
    public static float campSoldier_CampWallSpeedMode_MinNeededDist = 3f;
    public static float campSoldier_CampWallSpeedMode_MinNeededSpeed = 0.5f;

 
    public static Vector3 GetShootingRaycastPos(Vector3 _basePos, Quaternion _baseRot, Vector3 _offset)
    {
        Vector3 pos = _basePos;
        Quaternion rot = _baseRot;
        Vector3 offs = _offset;
        offs = MathfPlus.RotateVecAroundYAxisByRotation(offs, rot);
        offs += pos;
        return offs;
    }
    public static Vector3 GetShootingRaycastPos(Vector3 _basePos, Quaternion _baseRot, SoldierFightInPointInfo _info, SoldierGun _gun)
    {
        return GetShootingRaycastPos(_basePos, _baseRot, _info.GetRaycastOffsetForGun(_gun.name));
    }


    public static SoldierGunDirectionEnum GetSoldierGunDirectionForTarget(GameObject _sourceSoldier, GameObject _targetSoldier)
    {
        float deltaAng = GunDirectionsDeltaAngle;

        Vector3 sourcePos = _sourceSoldier.transform.position;
        Vector3 targetPos = _targetSoldier.transform.position;

        if (Mathf.Abs(Vector3.Distance(sourcePos, targetPos)) < MaxDistForForwardGunDirection)
            if (Mathf.Abs(sourcePos.y - targetPos.y) < MaxYForForwardGunDirection)
                return SoldierGunDirectionEnum.Forward;

        float xzDist = (new Vector3(targetPos.x - sourcePos.x,
                                     0,
                                     targetPos.z - sourcePos.z)).magnitude;

        float yDist = targetPos.y - sourcePos.y;

        float angle = Mathf.Atan(yDist / xzDist) * Mathf.Rad2Deg;

        if (angle < -deltaAng)
            return SoldierGunDirectionEnum.Downward;

        if (angle > deltaAng)
            return SoldierGunDirectionEnum.Upward;

        return SoldierGunDirectionEnum.Forward;
    }

    public static bool IsSoldierOnPos(GameObject _soldier, Vector3 _pos)
    {
        return Vector3.Distance(_soldier.transform.position, _pos) <= SoldierMaxErrorToPos;
    }

    //public static float SlopeLimit = 45f;
    //CharacterController charController;

    //bool shouldClearCollisions;

    //List<Collision> collisions = new List<Collision>();

    //public bool IsGrounded()
    //{
    //    if (collisions == null)
    //        return false;

    //    if (collisions.Count == 0)
    //        return false;

    //    GameObject obj = gameObject;
    //    float slopeLimitAngle = SlopeLimit;
    //    CharacterController charCont = charController;

    //    float halfH = charCont.height / 2;
    //    float angle = slopeLimitAngle * Mathf.Deg2Rad;
    //    float extraH = charCont.radius - Mathf.Cos(angle) * charCont.radius;
    //    float maxY = transform.position.y - halfH + extraH;
    //    float minY = transform.position.y - halfH;
    //    minY -= 0.001f;

    //    foreach (Collision collision in collisions)
    //    {
    //        foreach (ContactPoint contact in collision.contacts)
    //        {
    //            if (contact.point.y >= minY && contact.point.y <= maxY)
    //            {
    //                return true;
    //            }
    //        }
    //    }

    //    return false;
    //}

    //void OnCollisionEnter(Collision collision)
    //{
    //    IfCan_ClearCollisionsListAndSetFlags();
    //}

    //void OnCollisionStay(Collision collision)
    //{
    //    IfCan_ClearCollisionsListAndSetFlags();
    //    collisions.Add(collision);
    //}

    //void OnCollisionExit(Collision collision)
    //{
    //    IfCan_ClearCollisionsListAndSetFlags();
    //}

    //void Awake()
    //{
    //    charController = gameObject.GetComponent(typeof(CharacterController)) as CharacterController;
    //}

    //void FixedUpdate()
    //{
    //    shouldClearCollisions = true;
    //}

    //void IfCan_ClearCollisionsListAndSetFlags()
    //{
    //    if (shouldClearCollisions)
    //    {
    //        shouldClearCollisions = false;

    //        if (collisions.Count > 0)
    //            collisions.RemoveRange(0, collisions.Count);
    //    }
    //}
}