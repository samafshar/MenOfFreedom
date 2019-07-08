using UnityEngine;
using System.Collections;

public class LoadingIcon : MonoBehaviour
{

    public float speed = 10;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //float val = (Time.time - ((int)(Time.time / 360)) * 360) * speed;
        //transform.rotation = Quaternion.Euler(val, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);


        //transform.Rotate(Vector3.up, Time.deltaTime * speed);
    }
}
