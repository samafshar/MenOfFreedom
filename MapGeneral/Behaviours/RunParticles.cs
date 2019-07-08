using UnityEngine;
using System.Collections;

public class RunParticles : MonoBehaviour
{
    public GameObject[] particles;

    public float[] timesForEach;

    float timer = 0f;

    int counter = 0;

    bool started = false;

    void Start()
    {
        counter = 0;

        SetTimer();
    }

    void Update()
    {
        if (started)
        {
            timer = MathfPlus.DecByDeltatimeToZero(timer);

            if (timer == 0)
            {
                if (counter < particles.Length)
                {
                    particles[counter].SetActiveRecursively(true);

                    counter++;

                    SetTimer();
                }
            }
        }
    }

    public void StartIt()
    {
        started = true;
    }

    public void StopIt()
    {
        started = false;
    }

    void SetTimer()
    {
        if (timesForEach.Length > 1 && timesForEach.Length > counter)
        {
            timer = timesForEach[counter];
        }
        else
        {
            timer = timesForEach[0];
        }
    }    
}
