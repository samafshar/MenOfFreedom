using UnityEngine;
using System.Collections;

public class FootStepSoundList : MonoBehaviour
{
    public AudioClip[] FootStep_Right_Default;
    public AudioClip[] FootStep_Right_Wood;
    public AudioClip[] FootStep_Right_Concrete;
    public AudioClip[] FootStep_Right_Water;
    public AudioClip[] FootStep_Right_Metal;
    public AudioClip[] FootStep_Right_Mud;

    public AudioClip[] FootStep_Left_Default;
    public AudioClip[] FootStep_Left_Wood;
    public AudioClip[] FootStep_Left_Concrete;
    public AudioClip[] FootStep_Left_Water;
    public AudioClip[] FootStep_Left_Metal;
    public AudioClip[] FootStep_Left_Mud;

    public AudioClip[] Land_Default;
    public AudioClip[] Land_Wood;
    public AudioClip[] Land_Concrete;
    public AudioClip[] Land_Water;
    public AudioClip[] Land_Metal;
    public AudioClip[] Land_Mud;

    public AudioClip[] GetLandAudio(string _surfaceMaterial)
    {
        string surfaceMaterial = _surfaceMaterial.ToLower();

        if (surfaceMaterial == SurfaceMaterials.Concrete)
            return Land_Concrete;

        if (surfaceMaterial == SurfaceMaterials.Metal)
            return Land_Metal;

        if (surfaceMaterial == SurfaceMaterials.Mud)
            return Land_Mud;

        if (surfaceMaterial == SurfaceMaterials.Water)
            return Land_Water;

        if (surfaceMaterial == SurfaceMaterials.Wood)
            return Land_Wood;

        return Land_Default;
    }

    public AudioClip[] GetFootStepAudio(string _surfaceMaterial, PlayerFootEnum _foot)
    {
        PlayerFootEnum foot = _foot;
        string surfaceMaterial = _surfaceMaterial.ToLower();

        if (foot == PlayerFootEnum.Right)
        {
            if (surfaceMaterial == SurfaceMaterials.Concrete)
                return FootStep_Right_Concrete;

            if (surfaceMaterial == SurfaceMaterials.Metal)
                return FootStep_Right_Metal;

            if (surfaceMaterial == SurfaceMaterials.Mud)
                return FootStep_Right_Mud;

            if (surfaceMaterial == SurfaceMaterials.Water)
                return FootStep_Right_Water;

            if (surfaceMaterial == SurfaceMaterials.Wood)
                return FootStep_Right_Wood;

            return FootStep_Right_Default;
        }

        if (foot == PlayerFootEnum.Left)
        {
            if (surfaceMaterial == SurfaceMaterials.Concrete)
                return FootStep_Left_Concrete;

            if (surfaceMaterial == SurfaceMaterials.Metal)
                return FootStep_Left_Metal;

            if (surfaceMaterial == SurfaceMaterials.Mud)
                return FootStep_Left_Mud;

            if (surfaceMaterial == SurfaceMaterials.Water)
                return FootStep_Left_Water;

            if (surfaceMaterial == SurfaceMaterials.Wood)
                return FootStep_Left_Wood;

            return FootStep_Left_Default;
        }

        return FootStep_Right_Default;
    }
}
