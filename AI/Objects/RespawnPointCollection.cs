using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ReadyRespawnPointWithWeight
{
    public RespawnPoint_New respawnPoint;
    public float weight;
}

public class RespawnPointCollection : MonoBehaviour {

    public RespawnPointTypeEnum RespawnPointType;
    public float MaxTime;
    public float MaxCount;

    public float MinDelayTime = 5;
    public float MaxDelayTime = 10;

    public GameObject[] soldierTypesToCreate;

    public float initialDelay = 0;

    //

    [HideInInspector]
    public GameObject currentSoldier = null;

    [HideInInspector]
    public bool isFinished = false;

    [HideInInspector]
    public RespawnPoint_New[] respawnPoints;

    //

    CharacterInfo charInfo = null;

    float timeCounter;
    float counter;

    float createSoldierDelayTimeCounter = 0;

    float initialDelayTimeCounter = 0;

    bool isStarted = false;

    //

    public void Init_Start()
    {
        isStarted = true;
        isFinished = false;
        initialDelayTimeCounter = initialDelay;
        createSoldierDelayTimeCounter = 0;
        currentSoldier = null;
        charInfo = null;
        timeCounter = MaxTime;
        counter = MaxCount;
    }

    public void Init_SetInitialDelay(float _delayTime)
    {
        initialDelay = _delayTime;
    }

    //

	// Use this for initialization
	void Awake () {
        respawnPoints = transform.GetComponentsInChildren<RespawnPoint_New>();
	}
	
	// Update is called once per frame
	void Update () {

        if (!isFinished)
        {
            if (isStarted)
            {
                initialDelayTimeCounter = MathfPlus.DecByDeltatimeToZero(initialDelayTimeCounter);

                if (initialDelayTimeCounter == 0)
                {
                    if (currentSoldier == null)
                    {
                        if (createSoldierDelayTimeCounter > 0)
                        {
                            createSoldierDelayTimeCounter -= Time.deltaTime;

                            if (createSoldierDelayTimeCounter < 0)
                                createSoldierDelayTimeCounter = 0;
                        }
                    }

                    if (RespawnPointType == RespawnPointTypeEnum.Time)
                    {
                        timeCounter -= Time.deltaTime;

                        if (timeCounter <= 0)
                        {
                            timeCounter = 0;
                            SetFinished();
                        }
                    }
                }
            }
        }

	}

    public void SetInitialSoldier(GameObject _initialSold)
    {
        currentSoldier = _initialSold;

        if (currentSoldier != null)
        {
            charInfo = currentSoldier.GetComponent<CharacterInfo>();
            SetDelayTimeCounter();
        }
        else
            charInfo = null;
    }

    void SetDelayTimeCounter()
    {
        createSoldierDelayTimeCounter = Random.Range(MinDelayTime, MaxDelayTime);
    }

    public void SetFinished()
    {
        isFinished = true;
    }

    public bool IsReady()
    {
        if (isFinished)
            return false;

        if (!isStarted)
            return false;

        if (initialDelayTimeCounter > 0)
            return false;

        bool areAllPointsNotReady = true;

        for (int i = 0; i < respawnPoints.Length; i++)
        {
            if (respawnPoints[i].IsReady())
                areAllPointsNotReady = false;
        }

        if (areAllPointsNotReady)
            return false;


        if (createSoldierDelayTimeCounter == 0)
        {
            if (currentSoldier == null)
                return true;

            if (charInfo == null)
                return true;

            if (charInfo.IsDead)
                return true;
        }

        return false;
    }

    public GameObject CreateSoldier()
    {
        RespawnPoint_New point = SelectBestRespawnPointToCreateSoldier();

        Vector3 pos = point.gameObject.transform.position;
        pos.y += 0.1f;

        Quaternion rot = point.gameObject.transform.rotation;

        currentSoldier = GameObject.Instantiate(soldierTypesToCreate[Random.Range(0, soldierTypesToCreate.Length)], pos, rot) as GameObject;
        currentSoldier.GetComponent<SoldierInfo>().Init();

        charInfo = currentSoldier.GetComponent<CharacterInfo>();

        if (RespawnPointType == RespawnPointTypeEnum.Counter)
        {
            counter--;

            if (counter == 0)
                SetFinished();
        }

        SetDelayTimeCounter();

        if (point.group != null)
            point.group.StartDelay(point);

        return currentSoldier;
    }

    List<RespawnPoint_New> GetReadyPoints()
    {
        List<RespawnPoint_New> readyPoints = new List<RespawnPoint_New>();

        for (int i = 0; i < respawnPoints.Length; i++)
        {
            if (respawnPoints[i].IsReady())
                readyPoints.Add(respawnPoints[i]);
        }

        return readyPoints;
    }

    RespawnPoint_New SelectBestRespawnPointToCreateSoldier()
    {
        float weightCoefMin = 0.9f;
        float weightCoefMax = 1.1f;

        float totalWeight = 0;

        for (int i = 0; i < respawnPoints.Length; i++)
        {
            totalWeight += respawnPoints[i].weight;
        }

        List<RespawnPoint_New> readyPoints = GetReadyPoints();

        List<ReadyRespawnPointWithWeight> readyPointsWithWeight = new List<ReadyRespawnPointWithWeight>();

        foreach (RespawnPoint_New point in readyPoints)
        {
            ReadyRespawnPointWithWeight pointWithWeight = new ReadyRespawnPointWithWeight();

            pointWithWeight.respawnPoint = point;

            pointWithWeight.weight = (point.weight / totalWeight) * Random.Range(weightCoefMin, weightCoefMax);

            readyPointsWithWeight.Add(pointWithWeight);
        }

        List<ReadyRespawnPointWithWeight> sortedReadyPointsWithWeight = new List<ReadyRespawnPointWithWeight>();

        while (readyPointsWithWeight.Count != 0)
        {
            float maxWeight = float.NegativeInfinity;
            int selectedIndex = -1;

            for (int i = 0; i < readyPointsWithWeight.Count; i++)
            {
                if (readyPointsWithWeight[i].weight > maxWeight)
                {
                    maxWeight = readyPointsWithWeight[i].weight;
                    selectedIndex = i;
                }
            }

            sortedReadyPointsWithWeight.Add(readyPointsWithWeight[selectedIndex]);
            readyPointsWithWeight.RemoveAt(selectedIndex);
        }


        return sortedReadyPointsWithWeight[0].respawnPoint;
    }
}
