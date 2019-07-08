using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OcclusionObjectsList : MonoBehaviour
{
    public List<GameObject> occlusionObj_A = new List<GameObject>();
    public List<GameObject> occlusionObj_B = new List<GameObject>();
    public List<GameObject> occlusionObj_C = new List<GameObject>();
    public List<GameObject> occlusionObj_D = new List<GameObject>();
    public List<GameObject> occlusionObj_E = new List<GameObject>();
    public List<GameObject> occlusionObj_F = new List<GameObject>();
    public List<GameObject> occlusionObj_G = new List<GameObject>();
    public List<GameObject> occlusionObj_H = new List<GameObject>();
    public List<GameObject> occlusionObj_I = new List<GameObject>();
    public List<GameObject> occlusionObj_J = new List<GameObject>();
    public List<GameObject> occlusionObj_K = new List<GameObject>();
    public List<GameObject> occlusionObj_L = new List<GameObject>();
    public List<GameObject> occlusionObj_M = new List<GameObject>();
    public List<GameObject> occlusionObj_N = new List<GameObject>();
    public List<GameObject> occlusionObj_O = new List<GameObject>();
    public List<GameObject> occlusionObj_P = new List<GameObject>();

    public string _____________________________________________________________________________ = "";

    public List<MeshRenderer> mRenderer_A = new List<MeshRenderer>();
    public List<MeshRenderer> mRenderer_B = new List<MeshRenderer>();
    public List<MeshRenderer> mRenderer_C = new List<MeshRenderer>();
    public List<MeshRenderer> mRenderer_D = new List<MeshRenderer>();
    public List<MeshRenderer> mRenderer_E = new List<MeshRenderer>();
    public List<MeshRenderer> mRenderer_F = new List<MeshRenderer>();
    public List<MeshRenderer> mRenderer_G = new List<MeshRenderer>();
    public List<MeshRenderer> mRenderer_H = new List<MeshRenderer>();
    public List<MeshRenderer> mRenderer_I = new List<MeshRenderer>();
    public List<MeshRenderer> mRenderer_J = new List<MeshRenderer>();
    public List<MeshRenderer> mRenderer_K = new List<MeshRenderer>();
    public List<MeshRenderer> mRenderer_L = new List<MeshRenderer>();
    public List<MeshRenderer> mRenderer_M = new List<MeshRenderer>();
    public List<MeshRenderer> mRenderer_N = new List<MeshRenderer>();
    public List<MeshRenderer> mRenderer_O = new List<MeshRenderer>();
    public List<MeshRenderer> mRenderer_P = new List<MeshRenderer>();

    public List<GameObject> GetProperListOfGameObject(OcclusionType _type)
    {
        OcclusionType type = _type;

        switch (type)
        {
            case OcclusionType.A:
                return occlusionObj_A;
                break;

            case OcclusionType.B:
                return occlusionObj_B;
                break;

            case OcclusionType.C:
                return occlusionObj_C;
                break;

            case OcclusionType.D:
                return occlusionObj_D;
                break;

            case OcclusionType.E:
                return occlusionObj_E;
                break;

            case OcclusionType.F:
                return occlusionObj_F;
                break;

            case OcclusionType.G:
                return occlusionObj_G;
                break;

            case OcclusionType.H:
                return occlusionObj_H;
                break;

            case OcclusionType.I:
                return occlusionObj_I;
                break;

            case OcclusionType.J:
                return occlusionObj_J;
                break;

            case OcclusionType.K:
                return occlusionObj_K;
                break;

            case OcclusionType.L:
                return occlusionObj_L;
                break;

            case OcclusionType.M:
                return occlusionObj_M;
                break;

            case OcclusionType.N:
                return occlusionObj_N;
                break;

            case OcclusionType.O:
                return occlusionObj_O;
                break;

            case OcclusionType.P:
                return occlusionObj_P;
                break;
        }

        Debug.LogError("GetProperList In OcclusionObjectList Works Bad");
        return occlusionObj_P;
    }

    public List<MeshRenderer> GetProperListOfMeshRenderer(OcclusionType _type)
    {
        OcclusionType type = _type;

        switch (type)
        {
            case OcclusionType.A:
                return mRenderer_A;
                break;

            case OcclusionType.B:
                return mRenderer_B;
                break;

            case OcclusionType.C:
                return mRenderer_C;
                break;

            case OcclusionType.D:
                return mRenderer_D;
                break;

            case OcclusionType.E:
                return mRenderer_E;
                break;

            case OcclusionType.F:
                return mRenderer_F;
                break;

            case OcclusionType.G:
                return mRenderer_G;
                break;

            case OcclusionType.H:
                return mRenderer_H;
                break;

            case OcclusionType.I:
                return mRenderer_I;
                break;

            case OcclusionType.J:
                return mRenderer_J;
                break;

            case OcclusionType.K:
                return mRenderer_K;
                break;

            case OcclusionType.L:
                return mRenderer_L;
                break;

            case OcclusionType.M:
                return mRenderer_M;
                break;

            case OcclusionType.N:
                return mRenderer_N;
                break;

            case OcclusionType.O:
                return mRenderer_O;
                break;

            case OcclusionType.P:
                return mRenderer_P;
                break;
        }

        Debug.LogError("GetProperListOfMeshRenderers In OcclusionObjectList Works Bad");
        return mRenderer_P;
    }

    public void SetMeshRenderers()
    {
        OcclusionType type = OcclusionType.A;
        
        while (type != OcclusionType.End)
        {
            List<GameObject> templist = GetProperListOfGameObject(type);

            if (templist.Count > 0)
            {
                foreach (GameObject gObj in templist)
                {
                    MeshRenderer[] mrs = gObj.GetComponentsInChildren<MeshRenderer>();

                    if (mrs.Length > 0)
                    {
                        foreach (MeshRenderer mr in mrs)
                        {
                            if (mr.tag != "Helper")
                                GetProperListOfMeshRenderer(type).Add(mr);
                        }
                    }
                }
            }

            type++;
        }
    }

    public void ClearAllLists()
    {
        occlusionObj_A.Clear();
        occlusionObj_B.Clear();
        occlusionObj_C.Clear();
        occlusionObj_D.Clear();
        occlusionObj_E.Clear();
        occlusionObj_F.Clear();
        occlusionObj_G.Clear();
        occlusionObj_H.Clear();
        occlusionObj_I.Clear();
        occlusionObj_J.Clear();
        occlusionObj_K.Clear();
        occlusionObj_L.Clear();
        occlusionObj_M.Clear();
        occlusionObj_N.Clear();
        occlusionObj_O.Clear();
        occlusionObj_P.Clear();

        //

        mRenderer_A.Clear();
        mRenderer_B.Clear();
        mRenderer_C.Clear();
        mRenderer_D.Clear();
        mRenderer_E.Clear();
        mRenderer_F.Clear();
        mRenderer_G.Clear();
        mRenderer_H.Clear();
        mRenderer_I.Clear();
        mRenderer_J.Clear();
        mRenderer_K.Clear();
        mRenderer_L.Clear();
        mRenderer_M.Clear();
        mRenderer_N.Clear();
        mRenderer_O.Clear();
        mRenderer_P.Clear();
    }
}
