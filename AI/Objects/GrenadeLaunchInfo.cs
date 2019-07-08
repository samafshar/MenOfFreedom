using UnityEngine;
using System.Collections;

public class GrenadeLaunchInfo : MonoBehaviour
{

    public GameObject grenadeDirHandler;
    public GameObject speedController;
    public GameObject minValLine;
    public GameObject maxValLine;

    float minSpeed = 5;
    float maxSpeed = 40;
    float speedRandomCoef = 0.05f;
    float dirRandomVal = 0.06f;

    public bool IsReady()
    {
        return true;
    }

    public Vector3 GetGrenadeDirection()
    {
        Vector3 res = grenadeDirHandler.transform.forward;
        res += new Vector3(Random.Range(-dirRandomVal, dirRandomVal),
                           Random.Range(-dirRandomVal, dirRandomVal),
                           Random.Range(-dirRandomVal, dirRandomVal));

        res = res.normalized;

        return res;
    }

    public float GetGrenadeSpeed()
    {
        float speedControllerLocalZ = speedController.transform.localPosition.z;
        float minValLineLocalZ = minValLine.transform.localPosition.z;
        float maxValLineLocalZ = maxValLine.transform.localPosition.z;

        float sp = speedControllerLocalZ - minValLineLocalZ;
        float mx = maxValLineLocalZ - minValLineLocalZ;

        float result = (sp / mx) * 40;
        result = Mathf.Clamp(result, minSpeed, maxSpeed);
        result *= (1 + Random.Range(-speedRandomCoef, speedRandomCoef));

        return result;

    }
}
