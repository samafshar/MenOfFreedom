using UnityEngine;
using System.Collections;

public class HitEffectManager : MonoBehaviour
{
    public HitEffect Wood;
    public HitEffect Metal;
    public HitEffect Mud;
    public HitEffect Concrete;
    public HitEffect Water;
    public HitEffect Meat;
    public HitEffect Default;

    public HitEffect GetRelativeHitEffect(string hitTag)
    {
        HitEffect effect;
        switch (hitTag)
        {
            case SurfaceMaterials.Concrete:
                effect = Concrete;
                break;

            case SurfaceMaterials.Wood:
                effect = Wood;
                break;

            case SurfaceMaterials.Metal:
                effect = Metal;
                break;

            case SurfaceMaterials.Mud:
                effect = Mud;
                break;

            case SurfaceMaterials.Water:
                effect = Water;
                break;

            case BodyParts.head:
            case BodyParts.rightHand:
            case BodyParts.leftHand:
            case BodyParts.rightArm:
            case BodyParts.leftArm:
            case BodyParts.rightFoot:
            case BodyParts.leftFoot:
            case BodyParts.chest:
            case BodyParts.back:
                effect = Meat;
                break;

            default:
                effect = Default;
                break;
        }
        return effect;
    }
}
