using UnityEngine;
using System.Collections;

public class SoldierDetectorCube : MonoBehaviour
{
    public SoldierDetectableCube detectableCube;

    public SoldierDetectableCube[] checkableDudes;

    public float startAngle;
    public float endAngle;
    public float viewRange;

    int step = -1;

    bool isStarted = false;
    bool isDudeNashSeen = false;

    void Update()
    {
        if (step == 1)
        {
            foreach (SoldierDetectableCube dude in checkableDudes)
            {
                if (dude.IsStarted())
                {
                    if (GeneralStats.IsVecInView(dude.transform.position, transform.position,
                                                        transform.rotation, startAngle, endAngle, viewRange))
                    {
                        Vector3 startPoint = transform.position;
                        Vector3 direction = dude.transform.position - startPoint;

                        float rayMagnitude = direction.magnitude;

                        bool isInView = false;

                        RaycastHit hit = new RaycastHit();
                        if (Physics.Raycast(startPoint, direction, out hit, viewRange, GameGeneralInfo.Instance.SoldierViewRaycastLayer))
                        {
                            if (hit.transform.name == dude.transform.name)
                                isInView = true;
                        }

                        if (isInView)
                        {
                            SetDudeNashSeen();
                        }
                    }
                }
            }
        }
    }

    public void StartIfItsNotStarted()
    {
        if (!isStarted)
        {
            step = 1;

            isStarted = true;
        }
    }

    public bool IsStarted()
    {
        return isStarted;
    }

    public bool IsDudeNashSeen()
    {
        return isDudeNashSeen;
    }

    void SetDudeNashSeen()
    {
        isDudeNashSeen = true;
    }

    void OnDestroy()
    {
        detectableCube.transform.position = transform.position;

        detectableCube.StartIfItsNotStarted();
    }
}
