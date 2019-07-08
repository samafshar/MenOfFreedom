using UnityEngine;
using System.Collections;

public class BlinkingMaterial : MonoBehaviour
{
    public Material normalMat;

    public Material BlinkingMat;

    public GameObject ObjectToBlinking;

    public void StartBlinking()
    {
        ObjectToBlinking.renderer.material = BlinkingMat;
    }
}
