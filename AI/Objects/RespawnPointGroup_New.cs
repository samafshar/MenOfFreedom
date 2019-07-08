using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RespawnPointGroup_New : MonoBehaviour {

    public float delayTime_Min = 1.7f;
    public float delayTime_Max = 3.2f;

    [HideInInspector]
    public List<RespawnPoint_New> respawnPoints = new List<RespawnPoint_New>();

    void Awake()
    {
        //for (int i = 0; i < respawnPoints.Length; i++)
        //{
        //    if (respawnPoints[i] != null)
        //        respawnPoints[i].group = this;
        //}
    }

    public void AddRespawnPoint(RespawnPoint_New _rp)
    {
        if (!respawnPoints.Contains(_rp))
            respawnPoints.Add(_rp);
    }

    public void StartDelay(RespawnPoint_New _source)
    {
        for (int i = 0; i < respawnPoints.Count; i++)
        {
            if (respawnPoints[i] != _source)
            {
                respawnPoints[i].SetGroupDelay(Random.Range(delayTime_Min, delayTime_Max));
            }
        }
    }
}
