using UnityEngine;
using System.Collections;

public class SunShaftTransform : MonoBehaviour
{
    Vector3 oldPos;
    void Start()
    {
        oldPos = PlayerCharacterNew.Instance.transform.position;
    }

    void Update()
    {
        Vector3 newPos = PlayerCharacterNew.Instance.transform.position;

        Vector3 diff = newPos - oldPos;

        oldPos = newPos;

        transform.Translate(diff);
    }
}
