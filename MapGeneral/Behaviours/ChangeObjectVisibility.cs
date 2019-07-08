using UnityEngine;
using System.Collections;

public class ChangeObjectVisibility : MonoBehaviour
{
    public GameObject mesh;

    bool isVisible = true;

    public void SetVisibility(bool _isVisible)
    {
        isVisible = _isVisible;

        mesh.renderer.enabled = isVisible;
    }

    public bool IsVisible()
    {
        return isVisible;
    }
}
