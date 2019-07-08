using UnityEngine;
using System.Collections;

public enum DamageType
{
    Bullet,
    Explosion,
    Fire,
}

public enum SoldierBodyPart
{
    UpFront,
    UpBack,
    Down,
    Head,
}

public class DamageInfo
{
    public GameObject damageSource = null;
    public Vector3 damageSourcePosition;
    public SoldierBodyPart bodyPart = SoldierBodyPart.UpFront;
    public DamageType damageType = DamageType.Bullet;
    public float damageAmount = 0;
    public Vector3 BulletDirection;
    public Vector3 HitPoint;
    public float Impulse;
    public string ownerTag;

    public float GetDamageCoefBySoldierBodyPart()
    {
        switch (bodyPart)
        {
            case SoldierBodyPart.UpFront:
            case SoldierBodyPart.UpBack:
                return 1;

            case SoldierBodyPart.Down:
                return 0.67f;

            case SoldierBodyPart.Head:
                return 1000000f;
        }

        return 1;
    }
}
