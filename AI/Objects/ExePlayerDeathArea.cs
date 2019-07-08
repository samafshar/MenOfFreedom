using UnityEngine;
using System.Collections;

public class ExePlayerDeathArea : MonoBehaviour {

    [HideInInspector]
    public ExecutionArea owner;

    public void Init_SetOwner(ExecutionArea _owner)
    {
        owner = _owner;
    }

    void OnTriggerStay(Collider _col)
    {
        if (owner.isEnabled)
        {
            Collider col = _col;

            if (col == null)
            {
                owner.IsPlayerIn = false;
                owner.checkPlayerInTimeCounter = 0;
                return;
            }

            GameObject obj = col.transform.root.gameObject;

            if (obj.tag.ToLower() == GeneralStats.playerTagName_ToLower)
            {
                owner.IsPlayerIn = true;
                owner.checkPlayerInTimeCounter = owner.checkPlayerInMaxTime;
                return;
            }
        }
        else
        {
            owner.IsPlayerIn = false;
            owner.checkPlayerInTimeCounter = 0;
        }
    }
}
