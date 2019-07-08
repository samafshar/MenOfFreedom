using UnityEngine;
using System.Collections;

public enum The3DObjViewRange
{
    Near,
    Medium,
    Far,
    SoFar,
}

public class The3DObjective : MonoBehaviour
{

    public The3DObjIconType dem3DObjType;

    public MeshRenderer datRenderObj;

    Transform sourceTr;

    float Near_MaxDistForFullAlpha = 16;
    float Near_DistForZeroAlpha = 22;

    float Medium_MaxDistForFullAlpha = 28;
    float Medium_DistForZeroAlpha = 36;

    float Far_MaxDistForFullAlpha = 50;
    float Far_DistForZeroAlpha = 60;

    float SoFar_MaxDistForFullAlpha = 90;
    float SoFar_DistForZeroAlpha = 100;

    float MaxDistForFullAlpha = 0;
    float DistForZeroAlpha = 0;

    PlayerCharacterNew playerCharNew;

    ActionStatus status = ActionStatus.NotStarted;

    float alpha = 0;

    [HideInInspector]
    public string the3DObjName = "";

    float alphaDecSpeed = 2f;
    float yMaxMoveDist = 0.07f;
    float yMoveSpeed = 10f;
    float scaleMaxAmount = 0.11f;
    float scaleChangeSpeed = 4f;
    float counter = 0;

    float scaling_MaxDist = 240;
    float scaling_MinDist = 0;

    float minScale = 0.03f;
    float maxScale = 10f;

    Vector3 lastPos;

    // Use this for initialization
    void Start()
    {
        playerCharNew = PlayerCharacterNew.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (status == ActionStatus.Running)
        {
            if (sourceTr != null)
            {
                transform.position = sourceTr.position;
                transform.LookAt(playerCharNew.fpsCamera.transform);

                lastPos = transform.position;

                float distToPlayer = Vector3.Distance(transform.position, playerCharNew.transform.position);

                float newScale = distToPlayer / (scaling_MaxDist - scaling_MinDist);
                newScale = Mathf.Clamp(newScale, minScale, maxScale);

                datRenderObj.transform.localScale = new Vector3(newScale, newScale, newScale);

                alpha = (distToPlayer - MaxDistForFullAlpha) / (DistForZeroAlpha - MaxDistForFullAlpha);
                alpha = Mathf.Clamp01(alpha);
                alpha = 1 - alpha;

                Color col = datRenderObj.material.color;
                datRenderObj.material.color = new Color(col.r, col.g, col.b, alpha);
            }
            else
            {
                StopIt();
            }
        }

        if (status == ActionStatus.Finished)
        {
            if (alpha > 0)
            {
                alpha = MathfPlus.DecByDeltatimeToZeroWithAdditionalCoef(alpha, alphaDecSpeed);

                Color col = datRenderObj.material.color;
                datRenderObj.material.color = new Color(col.r, col.g, col.b, alpha);
            }
        }

        if (alpha > 0)
        {
            switch (dem3DObjType)
            {
                case The3DObjIconType.FeleshRooBePayin:
                    counter += Time.deltaTime * yMoveSpeed;
                    transform.position = new Vector3(lastPos.x, lastPos.y - Mathf.Sin(counter) * yMaxMoveDist, lastPos.z);
                    break;

                case The3DObjIconType.FeleshRooBeBala:
                    counter += Time.deltaTime * yMoveSpeed;
                    transform.position = new Vector3(lastPos.x, lastPos.y + Mathf.Sin(counter) * yMaxMoveDist, lastPos.z);
                    break;

                case The3DObjIconType.Dot:
                    counter += Time.deltaTime * scaleChangeSpeed;
                    float datAmount = Mathf.Sin(counter) * scaleMaxAmount;
                    transform.localScale = new Vector3(1, 1, 1) + new Vector3(datAmount, datAmount, datAmount);
                    break;
            }
        }
    }

    public void StartIt(Transform _sourceTr, string _3dObjName, The3DObjViewRange _viewRange)
    {
        sourceTr = _sourceTr;

        status = ActionStatus.Running;

        the3DObjName = _3dObjName;

        switch (_viewRange)
        {
            case The3DObjViewRange.Near:
                MaxDistForFullAlpha = Near_MaxDistForFullAlpha;
                DistForZeroAlpha = Near_DistForZeroAlpha;
                break;

            case The3DObjViewRange.Medium:
                MaxDistForFullAlpha = Medium_MaxDistForFullAlpha;
                DistForZeroAlpha = Medium_DistForZeroAlpha;
                break;

            case The3DObjViewRange.Far:
                MaxDistForFullAlpha = Far_MaxDistForFullAlpha;
                DistForZeroAlpha = Far_DistForZeroAlpha;
                break;

            case The3DObjViewRange.SoFar:
                MaxDistForFullAlpha = SoFar_MaxDistForFullAlpha;
                DistForZeroAlpha = SoFar_DistForZeroAlpha;
                break;
        }
    }

    public void StopIt()
    {
        //alpha = 0;

        //Color col = datRenderObj.material.color;
        //datRenderObj.material.color = new Color(col.r, col.g, col.b, alpha);

        status = ActionStatus.Finished;
    }
}
