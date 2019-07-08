using UnityEngine;
using System.Collections;

public class RespawnPoint_New : MonoBehaviour {

    public RespawnPointGroup_New group;

    public float weight = 1;

    float groupDelayCounter = 0;

    //

    public void SetGroupDelay(float _delay)
    {
        groupDelayCounter = _delay;
    }

    public bool IsReady()
    {
        if (groupDelayCounter > 0)
            return false;

        return true;
    }

    //

    void Awake()
    {
        if (group != null)
            group.AddRespawnPoint(this);
    }

    void Update()
    {
        if (groupDelayCounter > 0)
            groupDelayCounter = MathfPlus.DecByDeltatimeToZero(groupDelayCounter);
    }
}
