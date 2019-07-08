using UnityEngine;
using System.Collections;

public class FireObjects : MonoBehaviour
{
    public GameObject[] Object;
    public Transform[] Trans;

    private GameObject choosenObject;
    private Transform choosenTransform;

    public Transform ChoosenTransform
    {
        get { return choosenTransform; }
        set { choosenTransform = value; }
    }
    public GameObject ChoosenObject
    {
        get { return choosenObject; }
        set { choosenObject = value; }
    }

    public void SetRandomObject()
    {
        //should check for random range
        if(Object.Length == 0)
            return;
        if(Object.Length != Trans.Length)
        {
            Debug.LogError("transform and positions are diffrent in size in fire objects");
            return;
        }

        int rnd = Random.Range(0, Object.Length);
        ChoosenObject = Object[rnd];
        ChoosenTransform = Trans[rnd];
    }
}
