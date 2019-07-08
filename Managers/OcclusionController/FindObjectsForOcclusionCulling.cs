using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class FindObjectsForOcclusionCulling : MonoBehaviour
{
    GameObject[] allGameObjects;

    public OcclusionObjectsList OccList;

    public void FindRelativeObjects()
    {
        OccList.ClearAllLists();

        allGameObjects = GameObject.FindSceneObjectsOfType(typeof(GameObject)) as GameObject[];

        foreach (GameObject gobj in allGameObjects)
        {
            if (gobj.GetComponent<OcclusionObjectsTypes>())
            {
                OcclusionObjectsTypes gObjOccType = gobj.GetComponent<OcclusionObjectsTypes>();

                HideObjects(gobj);

                foreach (OcclusionType occType in gObjOccType.types)
                {
                    OccList.GetProperListOfGameObject(occType).Add(gobj);
                }
            }
        }

        OccList.SetMeshRenderers();
    }

    public void RemoveOcclusionTypeComponent()
    {
        allGameObjects = GameObject.FindSceneObjectsOfType(typeof(GameObject)) as GameObject[];

        foreach (GameObject gobj in allGameObjects)
        {
            if (gobj.GetComponent<OcclusionObjectsTypes>())
            {
                DestroyImmediate(gobj.GetComponent<OcclusionObjectsTypes>());
            }
        }
    }

    public void RestoreHiddenObjects()
    {
        OcclusionType type = OcclusionType.A;

        while (type != OcclusionType.End)
        {
            List<MeshRenderer> templist = OccList.GetProperListOfMeshRenderer(type);

            if (templist.Count > 0)
            {
                foreach (MeshRenderer mr in templist)
                {
                    mr.enabled = true;
                }
            }

            type++;
        }
    }

    void HideObjects(GameObject _object)
    {
        GameObject gObj = _object;
        
        MeshRenderer[] mrs = gObj.GetComponentsInChildren<MeshRenderer>();

        if (mrs != null)
        {
            foreach (MeshRenderer mr in mrs)
            {
                mr.enabled = false;
            }
        }
    }
}
