using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MortarHeadQuarter : MonoBehaviour
{
    public float maxTimeOfWaitingForNextExplosion = 0.2f;

    public MortarManager[] mortarManagers;

    float timer = 0f;

    int step = -1;

    bool isReadyForNextExplosion = true;

    bool isStarted = false;

    MortarManager selectedMrtMg;

    void Update()
    {
        if (step == 1)
        {
            int rnd = Random.Range(0, mortarManagers.Length);

            selectedMrtMg = mortarManagers[rnd];

            selectedMrtMg.StartItIfNotStarted();

            SetStep(2);
        }

        if (step == 2)
        {
            if (selectedMrtMg.IsReady())
            {
                timer = maxTimeOfWaitingForNextExplosion;

                SetStep(3);
            }
        }

        if (step == 3)
        {
            timer = MathfPlus.DecByDeltatimeToZero(timer);

            if (timer == 0)
            {
                SetStep(1);
            }
        }
    }

    void SetStep(int _step)
    {
        step = _step;
    }

    bool IsStarted()
    {
        return isStarted;
    }

    bool IsReadyForNextExplosion()
    {
        return isReadyForNextExplosion;
    }

    void SetReadyForNextExplosion(bool _isReady)
    {
        isReadyForNextExplosion = _isReady;
    }

    public void StartItIfNotStarted()
    {
        if (!IsStarted())
        {
            SetStep(1);

            isStarted = true;
        }
    }

    public void EndIt()
    {
        SetStep(1000);

        isStarted = false;
    }

    public void SetDeathChance(int _deathChance)
    {
        mortarManagers[0].playerDeathChance = _deathChance;
    }
}
