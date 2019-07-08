using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExecutionArea : MonoBehaviour
{
    public float duration = 60;

    public int skippabaleNumOfInsideAliveSolds = 1;

    public float delayTimeToKillPlayer = 1.5f;

    public Transform killPoint;

    [HideInInspector]
    public float checkPlayerInMaxTime = 0.1f;

    [HideInInspector]
    public float checkPlayerInTimeCounter = 0.1f;

    [HideInInspector]
    public bool isEnabled = false;

    float timeCounter;

    [HideInInspector]
    public bool IsPlayerIn = false;

    bool countPlayerStayTime = false;
    float playerStayingTime = 0;

    [HideInInspector]
    public List<GameObject> insideSolds = new List<GameObject>();

    [HideInInspector]
    public ExeEnemyArea[] enemyAreas;

    [HideInInspector]
    public ExePlayerDeathArea playerDeathArea;                                                            

    [HideInInspector]
    public bool isEnabledFirstTick = false;

    float enabledFixedUpdateCount = 0;

    float time_InObjectsGarbageRemove_Max = 0.5f;
    float time_InObjectsGarbageRemove_Counter = 0.5f;

    [HideInInspector]
    public bool isEverStarted = false;

    void Start()
    {
        enemyAreas = transform.GetComponentsInChildren<ExeEnemyArea>();
        playerDeathArea = transform.GetComponentInChildren<ExePlayerDeathArea>();

        foreach (ExeEnemyArea eea in enemyAreas)
        {
            eea.Init_SetOwner(this);
        }

        playerDeathArea.Init_SetOwner(this);
    }

    void Update()
    {
        if (isEnabled)
        {

            timeCounter = MathfPlus.DecByDeltatimeToZero(timeCounter);

            if (timeCounter == 0)
            {
                EndIt();
                return;
            }

            checkPlayerInTimeCounter = MathfPlus.DecByDeltatimeToZero(checkPlayerInTimeCounter);

            if (checkPlayerInTimeCounter == 0)
            {
                IsPlayerIn = false;
            }

            if (IsPlayerIn)
            {
                countPlayerStayTime = true;
            }
            else
            {
                countPlayerStayTime = false;
                playerStayingTime = 0;
            }

            if (countPlayerStayTime)
            {
                playerStayingTime += Time.deltaTime;

                if (playerStayingTime > delayTimeToKillPlayer)
                {
                    if (IsPlayerIn)
                    {
                        bool isOK = false;

                        if (GetNumOfInsideEnemies() > skippabaleNumOfInsideAliveSolds)
                            isOK = true;

                        if (isOK)
                        {
                            KillPlayer();
                            EndIt();
                        }
                    }
                }
            }
        }
    }

    //void OnTriggerStay(Collider _col)
    //{
    //    if (isEnabled)
    //    {
    //        Collider col = _col;

    //        if (col == null)
    //        {
    //            IsPlayerIn = false;
    //            checkPlayerInTimeCounter = 0;
    //            return;
    //        }

    //        GameObject obj = col.transform.root.gameObject;

    //        if (obj.tag == "Player")
    //        {
    //            IsPlayerIn = true;
    //            checkPlayerInTimeCounter = checkPlayerInMaxTime;
    //            return;
    //        }
    //    }
    //    else
    //    {
    //        IsPlayerIn = false;
    //        checkPlayerInTimeCounter = 0;
    //    }
    //}

    //void OnTriggerEnter(Collider _col)
    //{
    //    if (isEnabled)
    //    {
    //        Collider col = _col;

    //        if (col == null)
    //        {
    //            return;
    //        }

    //        GameObject obj = col.transform.root.gameObject;

    //        if (obj.tag == "Soldier")
    //        {
    //            CharacterInfo soldChInf = obj.GetComponent<CharacterInfo>();

    //            if (soldChInf.FightSide == FightSideEnum.Enemy)
    //            {
    //                if (!insideSolds.Contains(obj))
    //                {
    //                    insideSolds.Add(obj);
    //                    return;
    //                }
    //            }
    //        }
    //    }
    //}

    //void OnTriggerExit(Collider _col)
    //{
    //    if (isEnabled)
    //    {
    //        Collider col = _col;

    //        if (col == null)
    //        {
    //            return;
    //        }

    //        GameObject obj = col.transform.root.gameObject;

    //        if (insideSolds.Contains(obj))
    //        {
    //            insideSolds.Remove(obj);
    //        }
    //    }
    //}

    void KillPlayer()
    {
        if (PlayerCharacterNew.Instance != null)
        {
            DamageInfo dmgInf = new DamageInfo();

            dmgInf.damageSource = killPoint.gameObject;
            dmgInf.damageSourcePosition = killPoint.transform.position;
            dmgInf.damageAmount = 100000000;

            PlayerCharacterNew.Instance.ApplyDamage(dmgInf);
        }
    }

    public void StartIt()
    {
        isEverStarted = true;

        isEnabled = true;
        IsPlayerIn = false;
        countPlayerStayTime = false;
        playerStayingTime = 0;
        checkPlayerInTimeCounter = 0;
        timeCounter = duration;

        insideSolds.Clear();

        isEnabledFirstTick = true;
        enabledFixedUpdateCount = 0;
    }

    public void StartItIfItsNotStartedBefore()
    {
        if (isEverStarted)
            return;

        StartIt();
    }

    public void EndIt()
    {
        isEnabled = false;
        IsPlayerIn = false;
        countPlayerStayTime = false;
        playerStayingTime = 0;

        insideSolds.Clear();
    }

    public int GetNumOfInsideEnemies()
    {
        return insideSolds.Count;
    }

    void FixedUpdate()
    {
        if (isEnabled)
        {
            if (isEnabledFirstTick)
            {
                enabledFixedUpdateCount++;

                if (enabledFixedUpdateCount == 3)
                    isEnabledFirstTick = false;
            }

            time_InObjectsGarbageRemove_Counter = MathfPlus.DecByDeltatimeToZero(time_InObjectsGarbageRemove_Counter);

            if (time_InObjectsGarbageRemove_Counter == 0)
            {
                time_InObjectsGarbageRemove_Counter = time_InObjectsGarbageRemove_Max;

                int i = 0;

                while (i < insideSolds.Count)
                {
                    if (insideSolds[i] == null)
                    {
                        insideSolds.RemoveAt(i);
                        continue;
                    }

                    i++;
                }
            }
        }
    }
}
