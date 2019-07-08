using UnityEngine;
using System.Collections;

public class LogicObjective : MonoBehaviour
{
    public bool IsActive = true;
    public bool shouldActiveByDamage = false;
    
    [HideInInspector]
    public bool IsDone = false;
    
    MultiDamageController multiDmgCtrl = new MultiDamageController();

    public void SetDone()
    {
        IsActive = false;
        IsDone = true;
    }

    public void ApplyDamage(DamageInfo _dmg)
    {
        if (multiDmgCtrl.IsDamageAppliedBefore(_dmg))
            return;

        multiDmgCtrl.AddDamage(_dmg);

        if (IsActive && !IsDone)
        {
            if (shouldActiveByDamage)
                SetDone();
        }
    }
}
