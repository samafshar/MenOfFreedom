using UnityEngine;
using System.Collections;

public class LogicDieTrigger : MonoBehaviour {

    public float delay = 0;
    float delayCounter = 0;
    public float timeToStopAfterDelay = 2;
    float stopTimeCounter = 0;
    bool shouldCountStop = false;

    public FightSideEnum victimsFightSide = FightSideEnum.Enemy;

    public string[] victimsValidTags;

    [HideInInspector]
    public bool isEverStarted = false;

    bool started = false;
    bool nowKill = false;

    //

	void Update () {

        if (started)
        {
            delayCounter = MathfPlus.DecByDeltatimeToZero(delayCounter);

            if (delayCounter == 0)
            {
                nowKill = true;
                started = false;
                shouldCountStop = true;
            }
        }

        if (shouldCountStop)
        {
            stopTimeCounter = MathfPlus.DecByDeltatimeToZero(stopTimeCounter);

            if (stopTimeCounter == 0)
            {
                nowKill = false;
                shouldCountStop = false;
            }
        }
	}

    void OnTriggerStay(Collider _col)
    {
        if (nowKill)
        {
            if (_col != null)
            {
                GameObject obj = _col.transform.root.gameObject;

                CharacterInfo objCharInf = obj.GetComponent<CharacterInfo>();

                if (objCharInf == null)
                    return;

                bool tagIsOk = true;

                if (victimsValidTags.Length > 0)
                {
                    tagIsOk = false;

                    for (int i = 0; i < victimsValidTags.Length; i++)
                    {
                        if (obj.tag.ToLower() == victimsValidTags[i].ToLower())
                        {
                            tagIsOk = true;
                            break;
                        }
                    }
                }
                else
                {
                    if (objCharInf.FightSide != victimsFightSide)
                        return;
                }

                if (tagIsOk)
                {
                    SoldierInfo soldInfo = obj.GetComponent<SoldierInfo>();

                    if (soldInfo != null)
                    {
                        DamageInfo dmg = new DamageInfo();

                        dmg.bodyPart = SoldierBodyPart.Head;
                        dmg.BulletDirection = Vector3.down;
                        dmg.damageAmount = 100000;
                        dmg.damageType = DamageType.Bullet;
                        dmg.Impulse = 200;
                        dmg.HitPoint = soldInfo.bodyInfo.soldierHeadTr.position;

                        soldInfo.KillSoldier(dmg);

                        return;
                    }

                    PlayerCharacterNew playerChar = obj.GetComponent<PlayerCharacterNew>();

                    if (playerChar != null)
                    {
                        playerChar.KillPlayer();

                        return;
                    }
                }
            }
        }
    }

    public void StartIt()
    {
        if (!shouldCountStop && !started && !nowKill)
        {
            started = true;
            isEverStarted = true;
            delayCounter = delay;
            stopTimeCounter = timeToStopAfterDelay;
        }
    }

    public void StartItIfItsNotStartedBefore()
    {
        if (isEverStarted)
            return;

        StartIt();
    }
}
