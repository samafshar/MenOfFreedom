using UnityEngine;
using System.Collections;

public class DieOverTime : MonoBehaviour
{
    public float DieTime = 5f;
    // Use this for initialization
    void Start()
    {
        timer = DieTime;
    }

    float timer;
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
            Destroy(gameObject);
    }
}
