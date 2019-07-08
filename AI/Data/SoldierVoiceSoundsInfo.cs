using UnityEngine;
using System.Collections;

public enum SoldierVoiceSoundTypeEnum
{
    Type1,
    Type2,
    Type3,
}

public class SoldierVoiceSoundsInfo : MonoBehaviour
{
    public SoldierVoiceSoundTypeEnum voiceType;

    public AudioClip[] BulletDamage;

    public AudioClip[] ExplosionDamage;

    public AudioClip[] Die;

    public AudioClip[] CampModeDie;

    public AudioClip[] ImReloading;

    public AudioClip[] Agressive;

    public AudioClip[] AgressiveMovement;

    public AudioClip[] BehindTheWall;

    public AudioClip[] IntoThatBuilding;

    public AudioClip[] Camp_EnemyDetected;

    public AudioClip GetAudioClip_Damage(DamageType _damageType)
    {
        AudioClip[] audios = null;

        switch (_damageType)
        {
            case DamageType.Bullet:
                audios = BulletDamage;
                break;

            case DamageType.Explosion:
                audios = ExplosionDamage;
                break;
        }

        return audios[Random.Range(0, audios.Length)];
    }
}
