using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class FPInitialPoint : MonoBehaviour
{
    public FightPoint fp;

    void OnRenderObject()
    {
        if (fp.isInitialPoint)
            renderer.enabled = true;
        else
            renderer.enabled = false;
    }
}
