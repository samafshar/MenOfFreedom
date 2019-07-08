using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeleeAttack_SoldierDetector : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> insideSolds = new List<GameObject>();

    void OnTriggerEnter(Collider _col)
    {
        CheckAndAddEnemies(_col);
    }

    void OnTriggerStay(Collider _col)
    {
        CheckAndAddEnemies(_col);
    }

    void OnTriggerExit(Collider _col)
    {
        Collider col = _col;

        if (col == null)
        {
            return;
        }

        GameObject obj = col.transform.root.gameObject;

        if (insideSolds.Contains(obj))
        {
            insideSolds.Remove(obj);
        }
    }

    void CheckAndAddEnemies(Collider _col)
    {
        Collider col = _col;

        if (col == null)
        {
            return;
        }

        GameObject obj = col.transform.root.gameObject;

        if (obj.tag.ToLower() == GeneralStats.enemyTagName_ToLower)
        {
            if (!insideSolds.Contains(obj))
            {
                insideSolds.Add(obj);
                return;
            }
        }
    }
}
