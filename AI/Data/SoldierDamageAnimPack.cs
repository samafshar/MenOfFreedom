using UnityEngine;
using System.Collections;

public class SoldierDamageAnimPack : MonoBehaviour {
    public string name;

    public AnimsList Anims_Bullet_UpFront;
    public AnimsList Anims_Bullet_UpBack;
    public AnimsList Anims_Bullet_Down;

    public string GetRandomAnim(DamageInfo dmg)
    {
        return GetRandomAnim(dmg.damageType, dmg.bodyPart);
    }
    public string GetRandomAnim(DamageType dmgType, SoldierBodyPart bodyPart)
    {
        AnimsList anims = GetAnimList(dmgType, bodyPart);
        return anims.GetRandomAnimName();
    }

    public AnimsList GetAnimList(DamageType dmgType, SoldierBodyPart bodyPart)
    {
        switch (dmgType)
        {
            case DamageType.Bullet:
                switch (bodyPart)
                {
                    case SoldierBodyPart.UpFront:
                        return Anims_Bullet_UpFront;

                    case SoldierBodyPart.UpBack:
                        return Anims_Bullet_UpBack;

                    case SoldierBodyPart.Down:
                        return Anims_Bullet_Down;
                }
                break;
        }

        return Anims_Bullet_UpFront;
    }
}
