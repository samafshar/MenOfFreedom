using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class FPLowRate : MonoBehaviour {

    public FightPoint fp;

    void OnRenderObject()
    {
        if (fp.isLowRate)
            renderer.enabled = true;
        else
            renderer.enabled = false;
    }
}
