using UnityEngine;
//using UnityEditor; //<----
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class AnimType : MonoBehaviour
{
    public SoldierGeneralInfo soldierGeneralInfo;
    public SoldierGun gunInfo;
    public AnimationClip fullAnimationsClip;
    public string animFolderPath = "Assets/_AICharacters/Anims";
    public string animFolderName = "";

    [HideInInspector]
    public List<AnimationClip> animClips = new List<AnimationClip>();

    public void CreateAnims() //<--- Dakhelesho gel begir
    {
        //if (animClips.Count > 0)
        //    animClips.RemoveRange(0, animClips.Count);

        //GeneralStats.EmptyAnimationStateArray(gameObject);

        //AssetDatabase.DeleteAsset(animFolderPath + "/" + animFolderName);
        //AssetDatabase.CreateFolder(animFolderPath, animFolderName);

        //for (int i = 0; i < soldierGeneralInfo.SoldierAnimations.Length; i++)
        //{
        //    AnimsList anList = soldierGeneralInfo.SoldierAnimations[i];

        //    AnimInfo[] anInfos = anList.animInfos;

        //    for (int j = 0; j < anInfos.Length; j++)
        //    {
        //        AnimInfo anInf = anInfos[j];
        //        string anName = anInf.AnimName;
        //        Vector2 anFr = anInf.GetFramesByGunName(gunInfo.name);
        //        bool anLoop = anInf.IsLoop;
        //        WrapMode anWrapMode = anInf.wrapMode;

        //        animation.AddClip(fullAnimationsClip, anName, (int)(anFr.x), (int)(anFr.y), anLoop);
        //        animation[anName].wrapMode = anWrapMode;

        //        if (anLoop)
        //            animation[anName].wrapMode = WrapMode.Loop;

        //        AnimationClip animClip = animation.GetClip(anName);

        //        animClip.wrapMode = animation[anName].wrapMode;

        //        animClips.Add(animClip);

        //        AssetDatabase.CreateAsset(animClip, animFolderPath + "/" + animFolderName + "/" + anName + ".anim");
        //    }
        //}
    }
}
