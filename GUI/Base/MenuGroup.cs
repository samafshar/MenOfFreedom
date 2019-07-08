using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum MenuGroupName
{
    _noName,
    missionSelect_levelButtons,
    missionSelect_others,
    difficulty,
    mainMenu_Main,
    quitDialogue,
    lastCheckpointWillBeLostDialogue,
    ingameMenu,
    ingame_lastCheckpointDialogue,
    ingame_restartMissionDialogue,
    ingame_saveAndQuitDialogue,
    options,
    options_Controls,
    options_Video,
    options_Audio,
    options_Controls_WaitingForKey,
    options_Controls_DefaultsDialogue,
    options_Audio_DefaultsDialogue,
    options_Video_DefaultsDialogue,
    options_Video_ApplyDialogue,
    mainMenu_BG,
    missionSelect_numsBack,
    mainMenu_Others,
}

public class MenuGroup : MonoBehaviour
{
    public MenuGroupName groupName = MenuGroupName._noName;

    public MenuControl[] menuControls;

    [HideInInspector]
    public bool isMenuGroupEnabled = false;

    public Texture2D bgTexture = null;

    public float x = 0;

    public float y = 0;

    public string _UNION____________________________________ = "________________________________________________________";

    public bool useUnionSetting = false;

    public float unionX;

    public float unionY;

    public float unionW;

    public float unionH;

    public MenuRectLayoutX unionXLayout = MenuRectLayoutX.Center;

    public MenuRectLayoutY unionYLayout = MenuRectLayoutY.Center;

    public float unionXDist = 0;

    public float unionYDist = 0;

    public int repeatUnionAfterHowManyItems = 0;

    //

    float scale = 1;
    bool isInitedBefore = false;

    public void Init(float _scale)
    {
        if (isInitedBefore)
            return;

        isInitedBefore = true;

        for (int i = 0; i < menuControls.Length; i++)
        {
            menuControls[i].x += x;
            menuControls[i].y += y;
        }

        ReInitScale(_scale);

        if (useUnionSetting)
        {
            SetControlsUnion();
        }
    }

    public void ReInitScale(float _scale)
    {
        scale = _scale;

        foreach (MenuControl mc in menuControls)
        {
            mc.ReInitScale(scale);
        }
    }

    void SetControlsUnion()
    {
        int j = 0;

        for (int i = 0; i < menuControls.Length; i++)
        {
            menuControls[i].xLayout = unionXLayout;
            menuControls[i].yLayout = unionYLayout;

            menuControls[i].x = unionX + x + (j * unionXDist);
            menuControls[i].y = unionY + y + (j * unionYDist);
            menuControls[i].w = unionW;
            menuControls[i].h = unionH;

            menuControls[i].ReInitMenuRect();

            j++;

            if (repeatUnionAfterHowManyItems > 0)
            {
                if (j == repeatUnionAfterHowManyItems)
                    j = 0;
            }
        }
    }

    public void SetEnabled(bool _value)
    {
        isMenuGroupEnabled = _value;

        for (int i = 0; i < menuControls.Length; i++)
        {
            menuControls[i].SetIsActive(isMenuGroupEnabled);
            menuControls[i].SetIsVisible(isMenuGroupEnabled);
        }
    }

    public void SetEnabledTrueButVisibleChildsBecameDeactive()
    {
        isMenuGroupEnabled = true;

        for (int i = 0; i < menuControls.Length; i++)
        {
            if (menuControls[i].isControlVisible)
                menuControls[i].SetIsActive(false);
        }
    }

    public MenuControl GetChildControlByName(MenuControlName _ctrlName)
    {
        MenuControlName ctrlName = _ctrlName;

        for (int i = 0; i < menuControls.Length; i++)
        {
            if (menuControls[i].controlName == ctrlName)
                return menuControls[i];
        }

        Debug.LogError("Menu control: '" + ctrlName + "' not founded in menu group: '" + this + "'.");
        return null;
    }
}
