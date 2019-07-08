using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleRenderArea : MonoBehaviour
{

    List<GameObject> objects = new List<GameObject>();

    bool isPlayerIn = false;

    bool areObjsHidden = false;

    float delay = 0.5f;

    float yDecreaseSpeed = 3000;

    bool isFirstHideDone = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * yDecreaseSpeed, transform.position.z);

            if (transform.position.y < 0)
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

        delay = MathfPlus.DecByDeltatimeToZero(delay);

        if (delay == 0)
        {
            if (!isFirstHideDone)
            {
                foreach (GameObject go in objects)
                {
                    MeshRenderer[] rends = go.GetComponentsInChildren<MeshRenderer>();

                    foreach (MeshRenderer rend in rends)
                    {
                        rend.enabled = false;
                    }
                }

                areObjsHidden = true;

                isFirstHideDone = true;
            }


            if (!isPlayerIn)
            {
                if (!areObjsHidden)
                {
                    foreach (GameObject go in objects)
                    {
                        MeshRenderer[] rends = go.GetComponentsInChildren<MeshRenderer>();

                        foreach (MeshRenderer rend in rends)
                        {
                            rend.enabled = false;
                        }
                    }

                    areObjsHidden = true;
                }
            }

            if (isPlayerIn)
            {
                if (areObjsHidden)
                {
                    foreach (GameObject go in objects)
                    {
                        MeshRenderer[] rends = go.GetComponentsInChildren<MeshRenderer>();

                        foreach (MeshRenderer rend in rends)
                        {
                            rend.enabled = true;
                        }
                    }

                    areObjsHidden = false;
                }
            }
        }
    }

    void OnTriggerEnter(Collider _col)
    {
        GameObject go = _col.gameObject;

        if (go.transform.root.tag == GeneralStats.allyTagName_ToLower || go.transform.root.tag == GeneralStats.enemyTagName_ToLower)
        {
            return;
        }

        if (go.transform.root.tag == GeneralStats.playerTagName_ToLower)
        {
            isPlayerIn = true;
        }

        if (go.transform.parent != null)
        {
            go = go.transform.parent.gameObject;
        }

        if (!objects.Contains(go))
        {
            objects.Add(go);
        }
    }

    void OnTriggerExit(Collider _col)
    {
        GameObject go = _col.gameObject;

        if (go.transform.root.tag == GeneralStats.allyTagName_ToLower || go.transform.root.tag == GeneralStats.enemyTagName_ToLower)
        {
            return;
        }

        if (go.transform.root.tag == GeneralStats.playerTagName_ToLower)
        {
            isPlayerIn = false;
        }

        if (go.transform.parent != null)
        {
            go = go.transform.parent.gameObject;
        }

        if (objects.Contains(go))
        {
            objects.Remove(go);
        }
    }
}
