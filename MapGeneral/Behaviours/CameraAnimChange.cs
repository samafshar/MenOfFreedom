using UnityEngine;
using System.Collections;

public class CameraAnimChange : MonoBehaviour
{
    public GameObject camera;

    public AnimationClip animMain;

    public AnimationClip animEnd;

    public float switchingAnimSpeed = 0.5f;

    bool isSwitchingAnims = false;

    void Update()
    {
        if (isSwitchingAnims)
        {
            camera.animation[animMain.name].weight = MathfPlus.DecByDeltatimeToZeroWithAdditionalCoef(camera.animation[animMain.name].weight, switchingAnimSpeed);

            camera.animation[animEnd.name].weight = 1 - camera.animation[animMain.name].weight;

            if (camera.animation[animEnd.name].weight == 1)
                isSwitchingAnims = false;
        }
    }

    public void StartAnimation()
    {
        camera.animation[animMain.name].weight = 1;
        camera.animation[animMain.name].layer = 1;
        camera.animation[animMain.name].enabled = true;

        camera.animation[animEnd.name].weight = 0;
        camera.animation[animEnd.name].layer = 2;
        camera.animation[animEnd.name].enabled = true;
    }

    public void StartSwitchingAnims()
    {
        isSwitchingAnims = true;
    }
}
