using UnityEngine;
using System.Collections;

public class SoldierFightInPointInfo : MonoBehaviour {

    public string name;
    public FightInPointTypeEnum fightType;
    public FightInPointStyleEnum fightStyle;
    public SoldierFightInPointInfo playerCriticalFightInfoInCover;
    public SoldierFightInPointInfo playerCriticalFightInfoInShoot; //Not for normal shooting acts

    //Gun offsets
    public Vector3 SpringFieldRaycastOffset;
    public Vector3 MP18RaycastOffset;

    public float coveringAngleYToFightPosRotation = 0;
    public float actToCoverRotationSpeed;
    public float fightRotationSpeed;
    public float coveringMinTime;
    public float coveringMaxTime;
    public float shootingMinTime;
    public float shootingMaxTime;
    public float shootingStartAngle;
    public float shootingEndAngle;

    //<Lean>

    public bool canLeanLeft = false;
    public Vector3 leanLeftRaycastOffset;
    public bool canLeanRight = false;
    public Vector3 leanRightRaycastOffset;

    public float leanHalfAngle = 25;

    //</Lean>

    public bool grenadeEnabled = false;
    public float grenadeCreationDelayInAnim = 0.7f;

    public AnimsList animCoveringRelax;
    public AnimsList animCoveringInFight;
    public AnimsList animIdleRelax; //NoCover
    public AnimsList animIdleInFight; //NoCover
    public AnimsList animCoveringReload;
    public AnimsList animIdleReload; //NoCover

    public AnimsList animShootAllBody_Forward;
    public AnimsList animShootAllBody_Down;
    public AnimsList animShootAllBody_Up;
    public AnimsList GetAnimList_ShootAllBody(SoldierGunDirectionEnum _gunDir)
    {
        switch (_gunDir)
        {
            case SoldierGunDirectionEnum.Downward:
                return animShootAllBody_Down;

            case SoldierGunDirectionEnum.Forward:
                return animShootAllBody_Forward;

            case SoldierGunDirectionEnum.Upward:
                return animShootAllBody_Up;
        }

        return null;
    }

    public AnimsList animFightLookAllBody_Forward;
    public AnimsList animFightLookAllBody_Down;
    public AnimsList animFightLookAllBody_Up;
    public AnimsList GetAnimList_FightLookAllBody(SoldierGunDirectionEnum _gunDir)
    {
        switch (_gunDir)
        {
            case SoldierGunDirectionEnum.Downward:
                return animFightLookAllBody_Down;

            case SoldierGunDirectionEnum.Forward:
                return animFightLookAllBody_Forward;

            case SoldierGunDirectionEnum.Upward:
                return animFightLookAllBody_Up;
        }

        return null;
    }

    public AnimsList animCoveringToShootAllBody_Forward;
    public AnimsList animCoveringToShootAllBody_Down;
    public AnimsList animCoveringToShootAllBody_Up;
    public AnimsList GetAnimList_CoveringToShootAllBody(SoldierGunDirectionEnum _gunDir)
    {
        switch (_gunDir)
        {
            case SoldierGunDirectionEnum.Downward:
                return animCoveringToShootAllBody_Down;

            case SoldierGunDirectionEnum.Forward:
                return animCoveringToShootAllBody_Forward;

            case SoldierGunDirectionEnum.Upward:
                return animCoveringToShootAllBody_Up;
        }

        return null;
    }

    public AnimsList animShootToCoveringAllBody_Forward;
    public AnimsList animShootToCoveringAllBody_Down;
    public AnimsList animShootToCoveringAllBody_Up;
    public AnimsList GetAnimList_ShootToCoveringAllBody(SoldierGunDirectionEnum _gunDir)
    {
        switch (_gunDir)
        {
            case SoldierGunDirectionEnum.Downward:
                return animShootToCoveringAllBody_Down;

            case SoldierGunDirectionEnum.Forward:
                return animShootToCoveringAllBody_Forward;

            case SoldierGunDirectionEnum.Upward:
                return animShootToCoveringAllBody_Up;
        }

        return null;
    }

    public AnimsList animIdleToShootAllBody_Forward; //NoCover
    public AnimsList animIdleToShootAllBody_Down; //NoCover
    public AnimsList animIdleToShootAllBody_Up; //NoCover
    public AnimsList GetAnimList_IdleToShootAllBody(SoldierGunDirectionEnum _gunDir)
    {
        switch (_gunDir)
        {
            case SoldierGunDirectionEnum.Downward:
                return animIdleToShootAllBody_Down;

            case SoldierGunDirectionEnum.Forward:
                return animIdleToShootAllBody_Forward;

            case SoldierGunDirectionEnum.Upward:
                return animIdleToShootAllBody_Up;
        }

        return null;
    }

    public AnimsList animShootToIdleAllBody_Forward; //NoCover
    public AnimsList animShootToIdleAllBody_Down; //NoCover
    public AnimsList animShootToIdleAllBody_Up; //NoCover
    public AnimsList GetAnimList_ShootToIdleAllBody(SoldierGunDirectionEnum _gunDir)
    {
        switch (_gunDir)
        {
            case SoldierGunDirectionEnum.Downward:
                return animShootToIdleAllBody_Down;

            case SoldierGunDirectionEnum.Forward:
                return animShootToIdleAllBody_Forward;

            case SoldierGunDirectionEnum.Upward:
                return animShootToIdleAllBody_Up;
        }

        return null;
    }

    public SoldierDamageAnimPack animDamageCoveringRelax;
    public SoldierDamageAnimPack animDamageCoveringInFight;
    public SoldierDamageAnimPack animDamageIdleRelax; //NoCover
    public SoldierDamageAnimPack animDamageIdleInFight; //NoCover

    public SoldierDamageAnimPack animDamageShootAllBody_Forward;
    public SoldierDamageAnimPack animDamageShootAllBody_Down;
    public SoldierDamageAnimPack animDamageShootAllBody_Up;
    public SoldierDamageAnimPack GetAnimDamagePack_ShootAllBodyDamage(SoldierGunDirectionEnum _gunDir)
    {
        switch (_gunDir)
        {
            case SoldierGunDirectionEnum.Downward:
                return animDamageShootAllBody_Down;

            case SoldierGunDirectionEnum.Forward:
                return animDamageShootAllBody_Forward;

            case SoldierGunDirectionEnum.Upward:
                return animDamageShootAllBody_Up;
        }

        return null;
    }

    //<Lean>

    public AnimsList animCoveringToLeanLeft;
    public AnimsList animLeanLeftIdle;
    public AnimsList animLeanLeftShoot;
    public AnimsList animLeanLeftToCover;

    public SoldierDamageAnimPack animDamageLeanLeft;

    //</Lean>

    public AnimsList animGrenade;

    //

    public Vector3 GetRaycastOffsetForGun(SoldierGunsName _gunName)
    {
        switch (_gunName)
        {
            case SoldierGunsName.SpringField:
                return SpringFieldRaycastOffset;

            case SoldierGunsName.MP18:
                return MP18RaycastOffset;
        }

        return Vector3.zero;
    }

    public string GetAStartAnim()
    {
        if (fightType == FightInPointTypeEnum.NoCover)
            return animIdleInFight.GetRandomAnimName();

        return animCoveringInFight.GetRandomAnimName();
    }

    //<Lean>

    public Vector3 GetLeanLeftRaycastOffsetForGun(SoldierGunsName _gunName)
    {
        return leanLeftRaycastOffset;
    }

    public Vector3 GetLeanRightRaycastOffsetForGun(SoldierGunsName _gunName)
    {
        return leanRightRaycastOffset;
    }

    //</Lean>
}
