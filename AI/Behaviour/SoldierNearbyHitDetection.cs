using UnityEngine;
using System.Collections;

public class SoldierNearbyHitDetection : MonoBehaviour
{
    bool isBulletHitNearbySeen = false;

    float maxDistance;

    void Start()
    {
        float max = transform.localScale.x;

        if (max < transform.localScale.y)
            max = transform.localScale.y;

        if (max < transform.localScale.z)
            max = transform.localScale.z;

        maxDistance = max * 2;
    }

    public bool IsBulletHitNearbySeen()
    {
        return isBulletHitNearbySeen;
    }

    public void SetBulletHitNearby(RaycastHit hitInfo, Ray ray, Bullet bullet)
    {
        isBulletHitNearbySeen = true;

        Vector3 startPoint = hitInfo.point + (ray.direction * 0.01f);

        Ray soldierHitRay = new Ray(startPoint, ray.direction);

        RaycastHit hit;

        if (Physics.Raycast(soldierHitRay, out hit, maxDistance, GameGeneralInfo.Instance.HitLayer))
        {
            if (hit.collider.transform.root.tag == transform.root.tag)
            {
                float soldierHP = transform.parent.GetComponent<CharacterInfo>().CurrentHealth;

                if (bullet.Damage >= soldierHP)
                {
                    isBulletHitNearbySeen = false;
                }
            }
        }
    }
}
