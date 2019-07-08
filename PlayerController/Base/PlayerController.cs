using UnityEngine;
using System.Collections;

public static class PlayerController
{
    private static bool lastCheckPoint_IsLvlCampPlayer = false;
    public static bool LastCheckPoint_IsLvlCampPlayer
    {
        get { return PlayerController.lastCheckPoint_IsLvlCampPlayer; }
        set { PlayerController.lastCheckPoint_IsLvlCampPlayer = value; }
    }

    private static bool lastCheckPoint_HaveSecGun = true;
    public static bool LastCheckPoint_HaveSecGun
    {
        get { return PlayerController.lastCheckPoint_HaveSecGun; }
        set { PlayerController.lastCheckPoint_HaveSecGun = value; }
    }


    private static PlayerGunName lastCheckPoint_PrimGun = PlayerGunName.FourLool;
    public static PlayerGunName LastCheckPoint_PrimGun
    {
        get { return PlayerController.lastCheckPoint_PrimGun; }
        set { PlayerController.lastCheckPoint_PrimGun = value; }
    }
    private static int lastCheckPoint_PrimGunCurBulletCount = 0;
    public static int LastCheckPoint_PrimGunCurBulletCount
    {
        get { return PlayerController.lastCheckPoint_PrimGunCurBulletCount; }
        set { PlayerController.lastCheckPoint_PrimGunCurBulletCount = value; }
    }
    private static int lastCheckPoint_PrimGunCurMagCount = 0;
    public static int LastCheckPoint_PrimGunCurMagCount
    {
        get { return PlayerController.lastCheckPoint_PrimGunCurMagCount; }
        set { PlayerController.lastCheckPoint_PrimGunCurMagCount = value; }
    }

    private static PlayerGunName lastCheckPoint_SecGun = PlayerGunName.FourLool;
    public static PlayerGunName LastCheckPoint_SecGun
    {
        get { return PlayerController.lastCheckPoint_SecGun; }
        set { PlayerController.lastCheckPoint_SecGun = value; }
    }
    private static int lastCheckPoint_SecGunCurBulletCount = 0;
    public static int LastCheckPoint_SecGunCurBulletCount
    {
        get { return PlayerController.lastCheckPoint_SecGunCurBulletCount; }
        set { PlayerController.lastCheckPoint_SecGunCurBulletCount = value; }
    }
    private static int lastCheckPoint_SecGunCurMagCount = 0;
    public static int LastCheckPoint_SecGunCurMagCount
    {
        get { return PlayerController.lastCheckPoint_SecGunCurMagCount; }
        set { PlayerController.lastCheckPoint_SecGunCurMagCount = value; }
    }

    private static int lastCheckPoint_CurGrenadeCount = 0;
    public static int LastCheckPoint_CurGrenadeCount
    {
        get { return PlayerController.lastCheckPoint_CurGrenadeCount; }
        set { PlayerController.lastCheckPoint_CurGrenadeCount = value; }
    }

    //

    private static bool loadWasOK = true;
    public static bool LoadWasOK
    {
        get { return PlayerController.loadWasOK; }
        set { PlayerController.loadWasOK = value; }
    }
}
