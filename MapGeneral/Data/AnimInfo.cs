using UnityEngine;
using System.Collections;

public class AnimInfo : MonoBehaviour {

    public string AnimName;
    public bool IsLoop;
    public WrapMode wrapMode = WrapMode.Default;

    public Vector2 SpringFieldFrames;
    public Vector2 MP18Frames;

    public Vector2 GetFramesByGunName(SoldierGunsName _gunName)
    {
        switch (_gunName)
        {
            case SoldierGunsName.SpringField:
                return SpringFieldFrames;

            case SoldierGunsName.MP18:
                return MP18Frames;
        }
        return new Vector2(0, 0);
    }
}
