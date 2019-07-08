using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum HUDGroupName
{
    _noName,
    AmmoCount,
    Grenade,
    GunShape,
    SitStand,
    LugerBullets,
    MP18Bullets,
    SnipeBullets,
    SpringfieldBullets,
    WinchesterBullets,
    Text_YouGetDamagedTakeCover,
    Text_Reload_NoAmmo,
    LevelObjectives,
    LevelObjectivesBG,
    GameSaved,
    CurrentMission,
    MissionComplete,
    MainMission,
    ActKeyRelated,
    AmmoPickInfo,
    SnipeHints,
    MissionFail,
    DamageSide,
    GrenadeIcon,
    the3DObjective,
    SneakingHints,
    SneakingHintsPart2,
    LvlCamp,
    LvlFlashback,
    LvlBushehr02,
    LvlBushehr02_LastPics,
}

public class HUDGroup : MonoBehaviour
{
    public HUDGroupName groupName = HUDGroupName._noName;

    public HUDControl[] hudControls;

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

        for (int i = 0; i < hudControls.Length; i++)
        {
            hudControls[i].x += x;
            hudControls[i].y += y;
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

        foreach (HUDControl hc in hudControls)
        {
            hc.ReInitScale(scale);
        }
    }

    void SetControlsUnion()
    {
        int j = 0;

        for (int i = 0; i < hudControls.Length; i++)
        {
            hudControls[i].xLayout = unionXLayout;
            hudControls[i].yLayout = unionYLayout;

            hudControls[i].x = unionX + x + (j * unionXDist);
            hudControls[i].y = unionY + y + (j * unionYDist);
            hudControls[i].w = unionW;
            hudControls[i].h = unionH;

            hudControls[i].ReInitRect();

            j++;

            if (repeatUnionAfterHowManyItems > 0)
            {
                if (j == repeatUnionAfterHowManyItems)
                    j = 0;
            }
        }
    }

    public void SetVisibilityOfAllChilds(bool _value)
    {
        for (int i = 0; i < hudControls.Length; i++)
        {
            hudControls[i].SetIsVisible(_value);
        }
    }

    public HUDControl GetChildControlByName(HUDControlName _ctrlName)
    {
        HUDControlName ctrlName = _ctrlName;

        for (int i = 0; i < hudControls.Length; i++)
        {
            if (hudControls[i].controlName == ctrlName)
                return hudControls[i];
        }

        Debug.LogError("HUD control: '" + ctrlName + "' not founded in HUD group: '" + this + "'.");
        return null;
    }

    public void StartDecreasingAlphaOfAllChilds(float _speed)
    {
        for (int i = 0; i < hudControls.Length; i++)
        {
            hudControls[i].StartDecreasingAlpha(_speed);
        }
    }

    public void StartIncreasingAlphaOfAllChilds(float _speed)
    {
        for (int i = 0; i < hudControls.Length; i++)
        {
            hudControls[i].StartIncreasingAlpha(_speed);
        }
    }

    public void SetAlphaOfAllChilds(float _alpha)
    {
        for (int i = 0; i < hudControls.Length; i++)
        {
            hudControls[i].SetAlpha(_alpha);
        }
    }

    public void SetChildsTextureIndex(int _childStartIndex, int _childEndIndex, int _textureIndex)
    {
        if (_childStartIndex > _childEndIndex)
            return;

        for (int i = _childStartIndex; i <= _childEndIndex; i++)
        {
            hudControls[i].selectedTextureIndex = _textureIndex;
        }
    }

    public void ShowChildsForAWhile(float _duration, float _startAlphaSpeed, float _endAlphaSpeed)
    {
        for (int i = 0; i < hudControls.Length; i++)
        {
            hudControls[i].ShowForAWhile(_duration, _startAlphaSpeed, _endAlphaSpeed);
        }
    }
}
