//<91-04-12>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimsList : MonoBehaviour
{
    public AnimInfo[] animInfos;

    public string GetRandomAnimName()
    {
        int index = Random.Range(0, animInfos.Length);

        return animInfos[index].AnimName;
    }

    public string[] GetAnimNames()
    {
        if (animInfos == null || animInfos.Length == 0)
            return null;

        string[] animNames = new string[animInfos.Length];

        for (int i = 0; i < animNames.Length; i++)
        {
            animNames[i] = animInfos[i].AnimName;
        }

        return animNames;
    }
}
