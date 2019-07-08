using UnityEngine;
//using UnityEditor;
using System.Collections;

[ExecuteInEditMode]
public class SoldierUpdateAnims : MonoBehaviour
{
    public AnimType animType;

    public string animPath = "Assets/AICharacters/Anims/";

    public void UpdateAnims()
    {
        GeneralStats.EmptyAnimationStateArray(gameObject);

        foreach (AnimationClip anClip in animType.animClips)
        {
            if (anClip != null)
            {
                animation.AddClip(anClip, anClip.name);
            }
            else
                print("Nulle!!!!!");
        }
    }
}
