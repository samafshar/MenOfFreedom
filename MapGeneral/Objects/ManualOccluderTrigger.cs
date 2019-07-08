using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum OcclusionType
{
    A = 0,
    B = 1,
    C = 2,
    D = 3,
    E = 4,
    F = 5,
    G = 6,
    H = 7,
    I = 8,
    J = 9,
    K = 10,
    L = 11,
    M = 12,
    N = 13,
    O = 14,
    P = 15,
    End = 16,
}

public class ManualOccluderTrigger : MonoBehaviour
{
    public List<OcclusionType> occlusionType = new List<OcclusionType>();

    OcclusionObjectsList occObjList;

    bool entered = false;
    bool exited = false;
    bool playerEntered = false;

    List<MeshRenderer> meshRenderers = new List<MeshRenderer>();

    static List<ManualOccluderTrigger> currentActiveOccluder = new List<ManualOccluderTrigger>();

    void Start()
    {
        OcclusionObjectsList ol = FindObjectOfType(typeof(OcclusionObjectsList)) as OcclusionObjectsList;

        if (ol != null)
        {
            occObjList = ol;
        }
        else
        {
            Debug.LogError("Manual Occlusion Culling cant find OcclusionList");
        }

        SetMeshRenderers();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag.ToLower() == GeneralStats.playerTagName_ToLower)
        {
            if (!entered)
            {
                entered = true;
                exited = false;

                ShowObjectsOfThisType(true);

                Enter();
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag.ToLower() == GeneralStats.playerTagName_ToLower)
        {
            if (!exited)
            {
                entered = false;
                exited = true;

                ShowObjectsOfThisType(false);

                Exit();
            }
        }
    }

    void ShowObjectsOfThisType(bool status)
    {
        foreach (MeshRenderer mr in meshRenderers)
        {
            if (mr != null)
                mr.enabled = status;
        }
    }

    void SetMeshRenderers()
    {
        foreach (OcclusionType occType in occlusionType)
        {
            List<MeshRenderer> tempList = occObjList.GetProperListOfMeshRenderer(occType);

            foreach (MeshRenderer mr in tempList)
            {
                meshRenderers.Add(mr);
            }
        }
    }

    void Enter()
    {
        currentActiveOccluder.Add(this);

        playerEntered = true;
    }

    void Exit()
    {
        currentActiveOccluder.Remove(this);

        if (currentActiveOccluder.Count > 0)
        {
            foreach (ManualOccluderTrigger mot in currentActiveOccluder)
            {
                mot.ShowObjectsOfThisType(true);
            }
        }

        playerEntered = false;
    }

    public void StartForCutscene()
    {
        ShowObjectsOfThisType(true);
    }

    public void EndForCutscene()
    {
        ShowObjectsOfThisType(false);

        if (currentActiveOccluder.Count > 0)
        {
            foreach (ManualOccluderTrigger mot in currentActiveOccluder)
            {
                mot.ShowObjectsOfThisType(true);
            }
        }
    }
}
