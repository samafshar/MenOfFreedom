using UnityEngine;
using System.Collections;

public class MapLogicJob_Move : MapLogicJob
{
    public MovementTypeEnum movementType= MovementTypeEnum.RunFast;

    public GameObject destinationPoint;

    public LogicJob_Anim_Info animInfoForNextActRandomAnim = null;

    public AnimsList animsListForNextActRandomAnim = null;

    Vector3[] movementPath;

    string nextActAnim = "";

    //

    SoldierAction_Movement movementAct;

    float maxErrorToPos;

    float aStarResultMaxTime = 0.4f;
    float aStarResultTimeCounter = 0.4f;

    float restartAStarMaxTime = 0.5f;
    float restartAStarTimeCounter = 0.5f;

    public void Init_MovementType(MovementTypeEnum _movementType)
    {
        movementType = _movementType;
    }
    public void Init_NextActAnim(string _animName)
    {
        nextActAnim = _animName;
    }
    public void Init_CustomPath(Vector3[] _path)
    {
        movementPath = _path;
    }
    public void Init_DestinationPoint(GameObject _destPoint)
    {
        destinationPoint = _destPoint;
    }

    public override void StartIt()
    {
        base.StartIt();

        if (string.IsNullOrEmpty(nextActAnim))
        {
            if (animInfoForNextActRandomAnim != null)
            {
                nextActAnim = animInfoForNextActRandomAnim.animsList.GetRandomAnimName();
            }
            else
            {
                if (animsListForNextActRandomAnim != null)
                    nextActAnim = animsListForNextActRandomAnim.GetRandomAnimName();
            }
        }

    }

    public override void RunIt()
    {
        base.RunIt();

    StartSteps:

        #region 1 Start
        if (step == 1)
        {
            if (needsToBeFinished)
            {
                SetFinished(false);
                return;
            }

            SetStep(3);

            //if (IsSoldOnPoint(controlledSoldier.gameObject, destinationPoint.transform.position, maxErrorToPos))
            //{
            //    SetFinished(true);
            //    return;
            //}
            //else
            //{
            //    if (movementPath != null && movementPath.Length > 0)
            //    {
            //        SetStep(3);
            //        goto StartSteps;
            //    }

            //    Vector3[] newPath;

            //    if (mapLogic.FindCurvePath(controlledSoldier.transform.position, destinationPoint.transform.position, maxErrorToPos, out newPath))
            //    {
            //        movementPath = newPath;
            //        SetStep(3);
            //        goto StartSteps;
            //    }
            //    else
            //    {
            //        SetStep(2);
            //        goto StartSteps;
            //    }
            //}
        }
        #endregion

        #region 2 Start AStar
        if (step == 2)
        {
            aStarResultTimeCounter = aStarResultMaxTime;
            restartAStarTimeCounter = restartAStarMaxTime;

            soldInfo.FindNewAStarPath(controlledSoldier.transform.position, destinationPoint.transform.position);

            if (soldInfo.isAStarPathResultRecievedInThisRun)
            {
                if (!soldInfo.isAStarPathError)
                {
                    movementPath = soldInfo.aStarLastPath;
                    SetStep(3);
                    goto StartSteps;
                }
                else
                {
                    Debug.LogError("No path founded to point!");
                    SetStep(2.2f);
                    goto EndSteps;
                }
            }

            SetStep(2.1f);
            goto StartSteps;
        }
        #endregion

        #region 2.1 Wait for Astar result
        if (step == 2.1f)
        {
            if (needsToBeFinished)
            {
                SetFinished(false);
                return;
            }

            if (soldInfo.isAStarPathResultRecievedInThisRun)
            {
                if (!soldInfo.isAStarPathError)
                {
                    movementPath = soldInfo.aStarLastPath;
                    SetStep(3);
                    goto StartSteps;
                }
                else
                {
                    Debug.LogError("No path founded to point!");
                    SetStep(2.2f);
                    goto EndSteps;
                }
            }

            aStarResultTimeCounter -= Time.deltaTime;
            if (aStarResultTimeCounter <= 0)
            {
                Debug.LogError("No path founded in needed time!");

                SetStep(2);
                goto EndSteps;
            }
        }
        #endregion

        #region 2.2 Waiting to restart Astar
        if (step == 2.2f)
        {
            if (needsToBeFinished)
            {
                SetFinished(false);
                return;
            }

            restartAStarTimeCounter = MathfPlus.DecByDeltatimeToZero(restartAStarTimeCounter);

            if (restartAStarTimeCounter == 0)
            {
                SetStep(2);
                goto StartSteps;
            }
        }
        #endregion

        #region 3 Start Moving
        if (step == 3)
        {
            if (needsToBeFinished)
            {
                SetFinished(false);
                return;
            }

            movementAct = controlledSoldier.gameObject.AddComponent<SoldierAction_Movement>();
            movementAct.Init(controlledSoldier.transform);
            movementAct.InitDefaultParams(movementType);

            movementAct.SetNextActAnimToCrossfade(nextActAnim);
            movementAct.SetEndingRotNormal(destinationPoint.transform.forward);

            movementAct.Init_PosToFindPath(destinationPoint.transform.position);

            //movementAct.Init_SetPath(movementPath);

            movementAct.StartAct();

            SetStep(3.1f);
        }
        #endregion

        #region 3.1 Moving
        if (step == 3.1f)
        {
            if (movementAct.status == SoldierAction.ActionStatusEnum.Finished)
            {
                Destroy(movementAct);
                SetFinished(true);
                return;
            }

            if (needsToBeFinished)
            {
                movementAct.SetNeedsToBeFinished(evenStopMovingForFinish);
                SetStep(4);
                goto StartSteps;
            }
        }
        #endregion

        #region 4 Finishing
        if (step == 4)
        {
            if (movementAct == null)
            {
                SetFinished(false);
                return;
            }

            //<Alpha>
            if (needsToBeFinished)
            {
                movementAct.SetNeedsToBeFinished(evenStopMovingForFinish);
            }
            //</Alpha>

            if (movementAct.status == SoldierAction.ActionStatusEnum.Finished)
            {
                Destroy(movementAct);
                SetFinished(false);
                return;
            }
        }
        #endregion

    EndSteps:
        ;
    }

    bool IsSoldOnPoint(GameObject _soldier, Vector3 _pos, float _maxError)
    {
        return Vector3.Distance(_soldier.transform.position, _pos) <= _maxError;
    }
}
