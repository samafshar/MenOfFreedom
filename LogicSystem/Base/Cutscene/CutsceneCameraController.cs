using UnityEngine;
using System.Collections;

public class CutsceneCameraController : MonoBehaviour
{
    public AnimationClip animClip;
    public bool autoPlayAnimation = true;
    public float initial_FoV_ForNOautoplay = 46;

    [HideInInspector]
    public Camera cam;
    [HideInInspector]
    public BlurEffect camBlurEffect;
    [HideInInspector]
    public PlayerBloodEffect camBloodEffect;

    [HideInInspector]
    public float fov = 46f;
    [HideInInspector]
    public float blurIntensity = 0;
    [HideInInspector]
    public float bloodAlpha = 0;

    [HideInInspector]
    public CutsceneController parentCutsceneController;

    // bool isAnimTriedToBePlayed = false;

    bool isActive = false;

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            //if (!isAnimTriedToBePlayed)
            //{
            //    if (!autoPlayAnimation)
            //    {
            //        PlayAnimation();
            //    }

            //    isAnimTriedToBePlayed = true;
            //}

            KeepCamActive();
        }
    }

    public void StartIt(Camera _cam)
    {
        if (autoPlayAnimation)
        {
            if (animClip != null)
                animation.Play(animClip.name);

            //isAnimTriedToBePlayed = true;
        }

        cam = _cam;
        camBlurEffect = cam.GetComponent<BlurEffect>();
        camBloodEffect = cam.GetComponent<PlayerBloodEffect>();
    }

    public void PlayAnimation()
    {
        if (animClip != null)
            animation.Play(animClip.name);
    }

    public void SetActive()
    {
        isActive = true;

        KeepCamActive();
    }

    public void SetDeactive()
    {
        isActive = false;
    }

    public void NextSequence()
    {
        parentCutsceneController.NextSequence();
    }

    public void KeepCamActive()
    {
        cam.transform.position = transform.position;
        cam.transform.rotation = transform.rotation;
        cam.fov = fov;

        if (camBlurEffect != null)
        {
            camBlurEffect.blurSpread = blurIntensity;
        }

        if (camBloodEffect != null)
        {
            camBloodEffect.SetBloodAlpha(bloodAlpha);
        }
    }
}
