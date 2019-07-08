using UnityEngine;
using System.Collections;

public class Brightness : MonoBehaviour
{

    public static Brightness Instance;

    void Awake()
    {
        Instance = this;

    }

    public void SetBrighness(float intensity)
    {

        //ScreenOverlay currentmainScreenOverlay =Camera.mainCamera.GetComponent<ScreenOverlay>();
        //currentmainScreenOverlay.intensity = intensity;
        //ScreenOverlay sFXScreenOverlay = Camera.allCameras[1].GetComponent<ScreenOverlay>();
        //sFXScreenOverlay.intensity = intensity;

        foreach (Camera cam in Camera.allCameras)
        {
            BrightnessScreenOverlay so = cam.GetComponent<BrightnessScreenOverlay>();

            if (so != null)
                so.intensity = intensity;
        }
    }
}
