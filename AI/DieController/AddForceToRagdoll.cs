using UnityEngine;
using System.Collections;

public class AddForceToRagdoll : MonoBehaviour
{
    private Vector3 hitPosition;
    private Vector3 moveDirection;
    private Rigidbody body;
    private float impulse;

    private bool shouldAddForce = false;
    bool finished = false;

    public float ForceTime = .4f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (shouldAddForce)
        {
            ForceTime -= Time.deltaTime;
            if (ForceTime <= 0)
            {
                shouldAddForce = false;
                finished = true;
            }
            Force();
        }
        if (finished)
            if (body != null)
                body.Sleep();
    }

    private void Force()
    {
        if (body != null)
            body.AddForceAtPosition(moveDirection * impulse, hitPosition, ForceMode.Force);
    }

    public LayerMask layerForRagdoll;

    public void AddForce(Vector3 hitPos, Vector3 direction,float imp)
    {
        Vector3 startPos = hitPos ;
        startPos -= direction * .1f;

        Ray ray = new Ray(startPos, direction);
        RaycastHit hitInfo;

        
        //Should Check for layerMask use distance ? (offset.magnitude)
        if (Physics.Raycast(ray, out hitInfo, direction.magnitude,layerForRagdoll))
        {
            body = hitInfo.collider.rigidbody;
            if (body == null)
            {
                body = hitInfo.transform.parent.rigidbody;
                if (body == null)
                {
                    body = hitInfo.collider.gameObject.GetComponentInChildren<Rigidbody>();
                    if (body == null)
                        Debug.LogError("Couldn't Find Collider In Ragdoll");
                }
            }
            hitPosition = hitPos;
            moveDirection = direction.normalized;
            impulse = imp;
            shouldAddForce = true;
        }
    }
}
