using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterInfo : MonoBehaviour
{
    public FightSideEnum FightSide = FightSideEnum.Enemy;

    public float MaxHealth = 100;

    public float ReceivedDamageCoef = 1;

    public bool IsInvulnerable = false;

    public Transform characterHead;

    public Transform[] shootRaycastTargets;

    public Transform[] fightHaloRaycastTargets;

    [HideInInspector]
    public float Range = 100;

    //

    [HideInInspector]
    public float CurrentHealth;

    [HideInInspector]
    public bool IsDead = false;

    bool isInvulnerableStatusBeforeGoingDisable = false;

    [HideInInspector]
    public List<GameObject> targettingEnemies = new List<GameObject>();

    [HideInInspector]
    public float targettingEnemiesCount = 0;

    float time_RefreshingTargettingEnemies_Max = 0.5f;
    float time_RefreshingTargettingEnemies_Counter = 0.5f;
    //

    void Awake()
    {
        CurrentHealth = MaxHealth;
        isInvulnerableStatusBeforeGoingDisable = IsInvulnerable;
    }

    void Update()
    {
        time_RefreshingTargettingEnemies_Counter = MathfPlus.DecByDeltatimeToZero(time_RefreshingTargettingEnemies_Counter);

        if (time_RefreshingTargettingEnemies_Counter == 0)
        {
            time_RefreshingTargettingEnemies_Counter = time_RefreshingTargettingEnemies_Max;

            RefreshTargettingEnemiesStats();
        }
    }

    void OnEnable()
    {
        IsInvulnerable = isInvulnerableStatusBeforeGoingDisable;
    }

    void OnDisable()
    {
        isInvulnerableStatusBeforeGoingDisable = IsInvulnerable;
        IsInvulnerable = true;
    }

    public FightSideEnum GetEnemyFightSide()
    {
        if (FightSide == FightSideEnum.Ally)
            return FightSideEnum.Enemy;
        else
            return FightSideEnum.Ally;
    }

    public bool IsAttackable()
    {
        return (enabled && !IsInvulnerable && !IsDead);
    }

    public void AddTargettingEnemy(GameObject _enemy)
    {
        GameObject enemy = _enemy;

        if (!targettingEnemies.Contains(enemy))
        {
            targettingEnemies.Add(enemy);
        }

        RefreshTargettingEnemiesStats();
    }

    public void RemoveTargettingEnemy(GameObject _enemy)
    {
        GameObject enemy = _enemy;

        if (targettingEnemies.Contains(enemy))
        {
            targettingEnemies.Remove(enemy);
        }

        RefreshTargettingEnemiesStats();
    }

    public void RefreshTargettingEnemiesStats()
    {
        int i = 0;

        while (i < targettingEnemies.Count)
        {
            if (targettingEnemies[i] == null)
            {
                targettingEnemies.RemoveAt(i);
                continue;
            }

            CharacterInfo chInf = targettingEnemies[i].GetComponent<CharacterInfo>();

            if (chInf.IsDead)
            {
                targettingEnemies.RemoveAt(i);
                continue;
            }

            i++;
        }

        targettingEnemiesCount = i;
    }

    public bool IsDeadOrDisabled()
    {
        return (IsDead || !enabled);
    }

    public void SetRecievedDamageCoef(float _val)
    {
        ReceivedDamageCoef = _val;
    }

    public void SetRecievedDamageCoefZero()
    {
        SetRecievedDamageCoef(0);
    }

    public void SetRecievedDamageCoefMax()
    {
        SetRecievedDamageCoef(1000);
    }
}
