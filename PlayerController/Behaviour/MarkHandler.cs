using UnityEngine;
using System.Collections;

public class MarkHandler : MonoBehaviour
{
    public void GenerateMark(Texture2D hitTexture, RaycastHit hitInfo)
    {
        transform.Rotate(new Vector3(0, Random.Range(-180.0f, 180.0f), 0));
        transform.localScale *= Random.Range(0.6f, 0.8f);

        transform.position += hitInfo.normal * DecalManager.Add(gameObject);

        transform.parent = hitInfo.collider.transform;

        renderer.material.mainTexture = hitTexture;
    }
}
