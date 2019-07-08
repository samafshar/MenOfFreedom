using UnityEngine;
using System.Collections;

public class AdvanceVideo : MonoBehaviour
{
    public static AdvanceVideo Instance;
  
    void Awake()
    {
        Instance = this;
    }

    public void SetShadow(int state)
    {
        Light[] lightObjs = GameObject.FindObjectsOfType(typeof(Light)) as Light[];
        if (lightObjs.Length > 0)
        {
            if (state == 0)
            {
                for (int i = 0; i < lightObjs.Length; i++)
                {
                    lightObjs[i].shadows = LightShadows.None;
                }
            }
            if (state == 1)
            {

                for (int i = 0; i < lightObjs.Length; i++)
                {
                    lightObjs[i].shadows = LightShadows.Soft;
                }

            }
            if (state == 2)
            {

                for (int i = 0; i < lightObjs.Length; i++)
                {
                    lightObjs[i].shadows = LightShadows.Hard;
                }

            }
        }
        else
        {
            print("no Light??????????????");
        }

    }
    public void SetTextureQuality(int state)
    {
        if (state == 0)
        {
            QualitySettings.masterTextureLimit = 3;
        }
        if (state == 1)
        {
            QualitySettings.masterTextureLimit = 2;
        }
        if (state == 2)
        {
            QualitySettings.masterTextureLimit = 1;
        }
        if (state == 3)
        {
            QualitySettings.masterTextureLimit = 0;
        }
    }
    public void SetAnisotropicTexture(int state)
    {
        if (state == 0)
        {
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
        }
        if (state == 1)
        {
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
        }

    }
    public void SetAntiAliasing(int state)
    {
        if (state == 0)
        {
            QualitySettings.antiAliasing = 0;
        }
        if (state == 1)
        {
            QualitySettings.antiAliasing = 2;
        }
        if (state == 2)
        {
            QualitySettings.antiAliasing = 4;
        }
        if (state == 3)
        {
            QualitySettings.antiAliasing = 8;
        }
    }
    public void SetVSync(int state)
    {
        if (state == 0)
        {
            QualitySettings.vSyncCount = 0;
        }
        if (state == 1)
        {
            QualitySettings.vSyncCount = 2;
        }
    }
    public void SetDOf(int state)
    {
        DepthOfField34 dof = Camera.mainCamera.GetComponent<DepthOfField34>();
        if (dof !=null)
        {
            if (state == 0)
            {
                dof.enabled = false;
            }
            if (state == 1)
            {
                dof.enabled = true;
            }
        }
        else
        {
            print("No DOF");
        }
    }
    public void SetBloom(int state)
    {
        BloomAndLensFlares bloom = Camera.mainCamera.GetComponent<BloomAndLensFlares>();
        if (bloom != null)
        {
            if (state == 0)
            {
                bloom.enabled = false;
            }
            if (state == 1)
            {
                bloom.enabled = true;
            }

        }
        else
        {
            print("No bloom");
        }
    }
    public void SetMotionBlur(int state)
    {
        MotionBlur motionB = Camera.mainCamera.GetComponent<MotionBlur>();
        if (motionB != null)
        {
            if (state == 0)
            {
                motionB.enabled = false;
            }
            if (state == 1)
            {
                motionB.enabled =true;
            }

        }
        else
        {
            print("No motionBlur");
        }
    }
    public void SetSSAO(int state)
    {
    SSAOEffect sSAO = Camera.mainCamera.GetComponent<SSAOEffect>();
        if (sSAO!= null)
        {
            if (state == 0)
            {
                sSAO.enabled = false;
            }
            if (state == 1)
            {
                sSAO.enabled =true;
            }

        }
        else
        {
            print("No SSAO");
        }
       
    }
}
