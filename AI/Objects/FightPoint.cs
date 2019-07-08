using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FightPoint : MonoBehaviour {
    public int lane = 0;

    public FightSideEnum FightSide = FightSideEnum.Ally;

    public SoldierFightInPointInfo[] fightInfos;

    public SoldierFightInPointInfo defaultfightInfo;

    public bool isInitialPoint = false;

    public float ratingCoef = 1;

    public bool isLowRate = false;

    //<Lean>

    public bool canLeanLeft = false;

    public bool canLeanRight = false;

    public float startLeanChance = 0.5f;

    //</Lean>

    public bool grenadeEnabled = false;

    public GrenadeLaunchInfo[] grenadeLaunchInfos;

    public bool IsGenerallyOkForGrenade()
    {
        if (!grenadeEnabled)
            return false;

        if (grenadeLaunchInfos == null)
            return false;

        if (grenadeLaunchInfos.Length == 0)
            return false;

        if (!GetRandomReadyGrenadeLaunchInfo())
            return false;

        return true;
    }

    public GrenadeLaunchInfo GetRandomReadyGrenadeLaunchInfo()
    {
        List<GrenadeLaunchInfo> okLaunchInfos = new List<GrenadeLaunchInfo>();

        foreach (GrenadeLaunchInfo grLaunchInf in grenadeLaunchInfos)
        {
            if (grLaunchInf.IsReady())
            {
                okLaunchInfos.Add(grLaunchInf);
            }
        }

        if (okLaunchInfos.Count == 0)
            return null;

        return okLaunchInfos[Random.Range(0, okLaunchInfos.Count)];
    }

    public AnimsList onlyDoAnimOnThisPoint;
}
