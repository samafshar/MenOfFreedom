using UnityEngine;
using System.Collections;

public class ChangeZWhileOnGround : MonoBehaviour {

    public float changeZSpeed = 5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = new Vector3(transform.position.x, GetGroundY(), transform.position.z + changeZSpeed * Time.deltaTime);
	}

    float GetGroundY()
    {
        Vector3 raycastDirection = new Vector3(0, -1000, 0);
        float rayDirMag = raycastDirection.magnitude;
        float range = rayDirMag;

        Vector3 raycastStart = transform.position + new Vector3(0, 2.2f, 0);

        RaycastHit hit;

        if (Physics.Raycast(raycastStart, raycastDirection, out hit, range, GameGeneralInfo.Instance.OnlyGroundLayer))
        {
            return hit.point.y;
        }

        return -1000;
    }
}
