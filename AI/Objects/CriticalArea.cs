using UnityEngine;
using System.Collections;

public enum CriticalAreaStyle
{
    Time,
    Count,
}

public class CriticalArea : MonoBehaviour
{
    public CriticalAreaStyle style = CriticalAreaStyle.Count;

    public float time_Duration = 60;

    public int count_MinNumOfRemainingSoldsToKillPlayer = 3;

    public float delayTimeToKillPlayer = 5f;

    public Transform killPoint;

    [HideInInspector]
    public MapLogicJob_FightInRegsGroup relatedFightInRegsGroup;

    float checkPlayerInMaxTime = 0.1f;
    float checkPlayerInTimeCounter = 0.1f;

    bool isEnabled = false;

    float timeCounter;

    [HideInInspector]
    public bool IsPlayerIn = false;

    bool countPlayerStayTime = false;
    float playerStayingTime = 0;

    public void Init_SetRelatedFightInRegsGroup(MapLogicJob_FightInRegsGroup _fightInRegsGroup)
    {
        relatedFightInRegsGroup = _fightInRegsGroup;
    }

    void Update()
    {
        if (isEnabled)
        {
            if (style == CriticalAreaStyle.Time)
            {
                timeCounter = MathfPlus.DecByDeltatimeToZero(timeCounter);
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

                        if (style == CriticalAreaStyle.Count)
                        {
                            if (relatedFightInRegsGroup != null && relatedFightInRegsGroup.status == LogicJobStatus.Running)
                            {
                                if (relatedFightInRegsGroup.countOfRemainingSolds >= count_MinNumOfRemainingSoldsToKillPlayer)
                                {
                                    isOK = true;
                                }
                            }
                        }
                        else
                        {
                            if (style == CriticalAreaStyle.Time)
                            {
                                if (relatedFightInRegsGroup == null )
                                {
                                    if (timeCounter > 0)
                                        isOK = true;
                                }
                                else
                                {
                                    if (relatedFightInRegsGroup.status == LogicJobStatus.Running)
                                    {
                                        if (timeCounter > 0)
                                            isOK = true;
                                    }
                                }
                            }
                        }

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

    void OnTriggerStay(Collider _col)
    {
        if (isEnabled)
        {
            Collider col = _col;

            if (col == null)
            {
                IsPlayerIn = false;
                checkPlayerInTimeCounter = 0;
                return;
            }

            GameObject obj = col.transform.root.gameObject;

            if (obj.tag.ToLower() == GeneralStats.playerTagName_ToLower)
            {
                IsPlayerIn = true;
                checkPlayerInTimeCounter = checkPlayerInMaxTime;
                return;
            }
        }
        else
        {
            IsPlayerIn = false;
            checkPlayerInTimeCounter = 0;
        }
    }

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
        isEnabled = true;
        IsPlayerIn = false;
        countPlayerStayTime = false;
        playerStayingTime = 0;
        checkPlayerInTimeCounter = 0;

        if (style == CriticalAreaStyle.Time)
        {
            timeCounter = time_Duration;
        }
    }

    public void EndIt()
    {
        isEnabled = false;
        IsPlayerIn = false;
        countPlayerStayTime = false;
        playerStayingTime = 0;
    }
}
