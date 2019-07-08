using UnityEngine;
using System.Collections;

public class RainArea : MonoBehaviour
{
    public ParticleEmitter[] rains;
    PlayerCharacterNew playerCharNew;

    LogicTrigger logTrig;

    bool myRainsAreActive = false;

    ActionStatus status = ActionStatus.NotStarted;

    float delayToStart = 0.4f;

    // Use this for initialization
    void Start()
    {
        logTrig = GetComponent<LogicTrigger>();
        logTrig.SetEnabled(true);

        playerCharNew = PlayerCharacterNew.Instance;

        foreach (ParticleEmitter pe in rains)
        {
            pe.emit = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (status == ActionStatus.NotStarted)
        {
            delayToStart = MathfPlus.DecByDeltatimeToZero(delayToStart);

            if (delayToStart == 0)
            {
                status = ActionStatus.Running;
            }
        }

        if (status == ActionStatus.Running)
        {
            if (logTrig.IsPlayerIn())
            {
                if (!myRainsAreActive)
                {
                    myRainsAreActive = true;

                    playerCharNew.campRain.emit = false;

                    foreach (ParticleEmitter pe in rains)
                    {
                        pe.emit = true;
                    }
                }
            }
            else
            {
                if (myRainsAreActive)
                {
                    myRainsAreActive = false;

                    playerCharNew.campRain.emit = true;

                    foreach (ParticleEmitter pe in rains)
                    {
                        pe.emit = false;
                    }
                }
            }
        }
    }
}
