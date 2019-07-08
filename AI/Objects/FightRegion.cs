//<91-04-12>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FightRegion : MonoBehaviour
{
    public FightSideEnum fightSide = FightSideEnum.Enemy;

    public MovementTypeEnum movementType = MovementTypeEnum.RunFast;

    [HideInInspector]
    public FightPoint[] fightPoints;

    [HideInInspector]
    public AntaresBezierCurve[] movementCurves;

    [HideInInspector]
    public RespawnPointCollection respawnPointCollection;

    void Awake()
    {
        fightPoints = transform.GetComponentsInChildren<FightPoint>();
        movementCurves = transform.GetComponentsInChildren<AntaresBezierCurve>();
        respawnPointCollection = transform.GetComponentInChildren<RespawnPointCollection>();
    }

    public FightPoint[] GetFightPointsOfLane(int _laneNum)
    {
        int lNum = _laneNum;

        List<FightPoint> fps = new List<FightPoint>();

        foreach (FightPoint fp in fightPoints)
        {
            if (fp.lane == lNum)
            {
                fps.Add(fp);
            }
        }

        if (fps.Count == 0)
        {
            Debug.LogError("No fight point in needed lane found!");
            return fightPoints;
        }

        FightPoint[] result = new FightPoint[fps.Count];

        for (int i = 0; i < fps.Count; i++)
        {
            result[i] = fps[i];
        }

        return result;
    }
}
