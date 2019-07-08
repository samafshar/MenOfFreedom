using UnityEngine;
using System.Collections;

public class LogicSignalingObject : MonoBehaviour {

    public Material normalMaterial;
    public Material signalMaterial;

    public bool shouldActiveByDamage = false;

    [HideInInspector]
    public bool isDone = false;

    [HideInInspector]
    public bool isSignaling = false;

    MultiDamageController multiDmgCtrl = new MultiDamageController();

    void SetSignaling(bool _value)
    {
        isSignaling = _value;

        if (_value)
        {
            Renderer[] rends = transform.GetComponentsInChildren<Renderer>();
            foreach (Renderer rnd in rends)
            {
                rnd.material = signalMaterial;
            }
        }
        else
        {
            Renderer[] rends = transform.GetComponentsInChildren<Renderer>();
            foreach (Renderer rnd in rends)
            {
                rnd.material = normalMaterial;
            }
        }
    }

    void ApplyDamage(DamageInfo _dmg)
    {
        if (multiDmgCtrl.IsDamageAppliedBefore(_dmg))
            return;

        multiDmgCtrl.AddDamage(_dmg);

        if (isSignaling && !isDone)
        {
            if (shouldActiveByDamage)
                SetActivationDone();
        }
    }

    public void StartSignaling()
    {
        SetSignaling(true);
    }

    public void SetActivationDone()
    {
        SetSignaling(false);
        isDone = true;
    }
}
