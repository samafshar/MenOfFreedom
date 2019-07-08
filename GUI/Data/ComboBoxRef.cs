using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ComboBoxRef : MonoBehaviour
{
    public GUISkin guiSkin;
    public GUISkin guiSkin_Active;
    public GUISkin guiSkin_OnTop;

    public Texture2D comboTickTexture;

    public GUISkin comboRollUpButton_Normal_Skin;
    public GUISkin comboRollUpButton_Deactive_Skin;
    public GUISkin comboRollDownButton_Normal_Skin;
    public GUISkin comboRollDownButton_Deactive_Skin;
    public float comboRollButton_H;

    public float comboItemsH;
}
