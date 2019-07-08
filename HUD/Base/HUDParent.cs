using UnityEngine;
using System.Collections;

public class HUDParent : MonoBehaviour
{
    public bool autoStart = true;

    public HUDGroup[] hudGroups;

    float defWidth = 1920;
    float defHeight = 1080;

    float scale = 1;

    bool isActive = false;

    //

    int curScreenHight;
    int curScreenWidth;

    //

    void Start()
    {
        if (autoStart)
            ActivateIt();

        foreach (HUDGroup hg in hudGroups)
        {
            hg.SetVisibilityOfAllChilds(false);
        }

        foreach (HUDGroup hg in hudGroups)
        {
            hg.SetAlphaOfAllChilds(0);
        }
    }

    void Update()
    {
        if (isActive)
        {
            if (curScreenHight != Screen.height || curScreenWidth != Screen.width)
            {
                CalcCurHeightAndWidthAndScale();
                ReInitScale(scale);
            }
        }
    }

    void OnGUI()
    {
        GUI.depth = -800;

        foreach (HUDGroup hg in hudGroups)
        {
            foreach (HUDControl hc in hg.hudControls)
            {
                if (hc.isControlVisible)
                {
                    GUI.color = new Color(1, 1, 1, hc.alpha);

                    GUI.DrawTexture(new Rect(hc.rect.x, hc.rect.y, hc.rect.w, hc.rect.h), hc.textures[hc.selectedTextureIndex]);

                    GUI.color = new Color(1, 1, 1, 1f);
                }
            }
        }
    }

    //

    void Init()
    {
        CalcCurHeightAndWidthAndScale();

        foreach (HUDGroup hg in hudGroups)
        {
            hg.Init(scale);
        }
    }

    public void ReInitScale(float _scale)
    {
        scale = _scale;

        foreach (HUDGroup hg in hudGroups)
        {
            hg.ReInitScale(scale);
        }
    }

    public HUDGroup GetChildGroupByName(HUDGroupName _groupName)
    {
        HUDGroupName groupName = _groupName;

        for (int i = 0; i < hudGroups.Length; i++)
        {
            if (hudGroups[i].groupName == groupName)
                return hudGroups[i];
        }

        Debug.LogError("HUD group: '" + groupName + "' not founded!");
        return null;
    }

    public HUDControl GetControlInGroup(HUDGroupName _groupName, HUDControlName _controlName)
    {
        HUDGroup hg = GetChildGroupByName(_groupName);
        return hg.GetChildControlByName(_controlName);
    }

    void CalcCurHeightAndWidthAndScale()
    {
        curScreenHight = Screen.height;
        curScreenWidth = Screen.width;

        scale = curScreenHight / defHeight;
    }

    //

    public void ActivateIt()
    {
        isActive = true;

        Init();
    }

    public void DeactivateIt()
    {
        isActive = false;

        foreach (HUDGroup hg in hudGroups)
        {
            hg.SetVisibilityOfAllChilds(false);
        }
    }
}