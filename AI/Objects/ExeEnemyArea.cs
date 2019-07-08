using UnityEngine;
using System.Collections;

public class ExeEnemyArea : MonoBehaviour
{
    [HideInInspector]
    public ExecutionArea owner;

    public void Init_SetOwner(ExecutionArea _owner)
    {
        owner = _owner;
    }

    void OnTriggerEnter(Collider _col)
    {
        CheckAndAddEnemies(_col);
    }

    void OnTriggerStay(Collider _col)
    {
        if (owner.isEnabled && owner.isEnabledFirstTick)
            CheckAndAddEnemies(_col);
    }

    void OnTriggerExit(Collider _col)
    {
        if (owner.isEnabled)
        {
            Collider col = _col;

            if (col == null)
            {
                return;
            }

            GameObject obj = col.transform.root.gameObject;

            if (owner.insideSolds.Contains(obj))
            {
                owner.insideSolds.Remove(obj);
            }
        }
    }

    void CheckAndAddEnemies(Collider _col)
    {
        if (owner.isEnabled)
        {
            Collider col = _col;

            if (col == null)
            {
                return;
            }

            GameObject obj = col.transform.root.gameObject;

            if (obj.tag.ToLower() == GeneralStats.enemyTagName_ToLower)
            {
                if (!owner.insideSolds.Contains(obj))
                {
                    owner.insideSolds.Add(obj);
                    return;
                }
            }
        }
    }
}
