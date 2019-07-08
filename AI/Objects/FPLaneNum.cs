using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class FPLaneNum : MonoBehaviour {

    public FightPoint fp;
    public Material[] numMats;

    void OnRenderObject()
    {
        renderer.material = numMats[fp.lane];
    }
}
