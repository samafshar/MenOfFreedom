using UnityEngine;
using System.Collections;

public class RenderAreasController : MonoBehaviour
{
    public RenderArea_Player[] playerRenderAreas;

    public bool autoStart = true;

    int indexOfCurRelationAreaThatPlayerIsIn = -1;

    float delay = 0.3f;

    bool firstInitIsDone = false;

    [HideInInspector]
    public ActionStatus status = ActionStatus.NotStarted;

    void Start()
    {
        if (autoStart)
            StartIt();
    }

    void Update()
    {
        if (status == ActionStatus.Running)
        {
            if (!firstInitIsDone)
            {
                delay = MathfPlus.DecByDeltatimeToZero(delay);

                if (delay == 0)
                {
                    firstInitIsDone = true;

                    for (int i = 0; i < playerRenderAreas.Length; i++)
                    {
                        playerRenderAreas[i].HideSoldiers();
                    }

                    indexOfCurRelationAreaThatPlayerIsIn = GetIndexOfAreaThatPlayerIsIn();

                    if (indexOfCurRelationAreaThatPlayerIsIn >= 0)
                    {
                        playerRenderAreas[indexOfCurRelationAreaThatPlayerIsIn].ShowSoldiers();
                    }

                }
            }
            else
            {
                CheckAndInitIndices();
            }
        }
    }

    int GetIndexOfAreaThatPlayerIsIn()
    {
        for (int i = 0; i < playerRenderAreas.Length; i++)
        {
            if (playerRenderAreas[i].IsPlayerIn())
            {
                return i;
            }
        }

        return -1;
    }

    void CheckAndInitIndices()
    {
        int oldIndex = indexOfCurRelationAreaThatPlayerIsIn;

        indexOfCurRelationAreaThatPlayerIsIn = GetIndexOfAreaThatPlayerIsIn();

        if (oldIndex < 0 && indexOfCurRelationAreaThatPlayerIsIn >= 0)
        {
            playerRenderAreas[indexOfCurRelationAreaThatPlayerIsIn].ShowSoldiers();
            return;
        }

        if (oldIndex >= 0 && indexOfCurRelationAreaThatPlayerIsIn < 0)
        {
            playerRenderAreas[oldIndex].HideSoldiers();
            return;
        }

        if (oldIndex != indexOfCurRelationAreaThatPlayerIsIn)
        {
            playerRenderAreas[oldIndex].HideSoldiers();
            playerRenderAreas[indexOfCurRelationAreaThatPlayerIsIn].ShowSoldiers();
            return;
        }
    }

    public void StartIt()
    {
        status = ActionStatus.Running;

        for (int i = 0; i < playerRenderAreas.Length; i++)
        {
            playerRenderAreas[i].StartIt();
        }
    }

    public void EndIt()
    {
        status = ActionStatus.Finished;
    }
}
