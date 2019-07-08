using UnityEngine;
using System.Collections;

public class DecalHandler : MonoBehaviour
{
    public void GenerateDecal(Texture2D hitTexture, GameObject affectedObj)
    {
        transform.Rotate(new Vector3(0, 0, Random.Range(-180.0f, 180.0f)));

        //<<91-04-14>>
        transform.localScale *= Random.Range(0.7f, 1.5f);
        //<<\91-04-14>>

        Decal.dCount++;
        Decal decal = gameObject.GetComponent<Decal>();
        decal.affectedObjects = new GameObject[1];
        decal.affectedObjects[0] = affectedObj;
        decal.decalMode = DecalMode.MESH_COLLIDER;
        decal.pushDistance = 0.08f + DecalManager.Add(gameObject);

        //decal.pushDistance = 0.019f;

        Material mat = new Material(decal.decalMaterial);
        mat.mainTexture = hitTexture;
        decal.decalMaterial = mat;
        decal.CalculateDecal();
        decal.transform.parent = affectedObj.transform;
    }
}
