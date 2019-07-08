using UnityEngine;
using System.Collections;

public enum InfluenceType
{
    DamageOverTime,
}

public class DamageInfluence : MonoBehaviour
{
    public Vector3 InfluencePosition;
    public float AmountOfLoseDamage;
    public float LoseHealthTime = 1f;
    public InfluenceType InfluenceType;
    public GameObject Particle_Influence;

    private DynamicObject dynamicHandle;
    private bool influenceStart = false;

    // Use this for initialization
    void Start()
    {
        dynamicHandle = GetComponent<DynamicObject>();
        if (dynamicHandle == null)
        {
            Debug.LogError("influence object should be dynamic"); 
        }
    }

    private float timer = 0f;
    // Update is called once per frame
    void Update()
    {
        switch (InfluenceType)
        {
            case InfluenceType.DamageOverTime:
                if (influenceStart)
                {
                    timer += Time.deltaTime;
                    if (timer > LoseHealthTime)
                    {
                        timer = 0f;
                        dynamicHandle.ChangeHealth(-AmountOfLoseDamage);
                    }                    
                }
                break;
        }
    }

    public void DamageOccur()
    {
        if (!influenceStart)
        {
            influenceStart = true;
            if (Particle_Influence != null)
            {
                GameObject gObj = Instantiate(Particle_Influence) as GameObject;
                gObj.transform.parent = transform;
                gObj.transform.localPosition = InfluencePosition;
            }
        }
    }
}
