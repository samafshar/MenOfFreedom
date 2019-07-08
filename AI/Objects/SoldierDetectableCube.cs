using UnityEngine;
using System.Collections;

public class SoldierDetectableCube : MonoBehaviour
{
    bool isStarted = false;

    float minLifeTimeCounter;
    float maxLifeTimeCounter;

    void Start()
    {
        minLifeTimeCounter = GeneralStats.deadNash_minLifeTime;
        maxLifeTimeCounter = GeneralStats.deadNash_maxLifeTime;
    }

    void Update()
    {
        if (IsStarted())
        {
            minLifeTimeCounter = MathfPlus.DecByDeltatimeToZero(minLifeTimeCounter);

            if (minLifeTimeCounter == 0)
            {
                if (!IsInPlayerView())
                {
                    DestroyIt();
                    return;
                }
            }

            maxLifeTimeCounter = MathfPlus.DecByDeltatimeToZero(maxLifeTimeCounter);

            if (maxLifeTimeCounter == 0)
            {
                DestroyIt();
                return;
            }
        }
    }

    public void StartIfItsNotStarted()
    {
        if (!isStarted)
        {
            isStarted = true;
        }
    }

    public bool IsStarted()
    {
        return isStarted;
    }

    bool IsInPlayerView()
    {
        return GeneralStats.IsVecInView(transform.position, PlayerCharacterNew.Instance.soldNashDetectorTr.position,
                                            PlayerCharacterNew.Instance.soldNashDetectorTr.rotation, -100, 100, 120);
    }

    void DestroyIt()
    {
        isStarted = false;

        Destroy(gameObject);
    }
}
