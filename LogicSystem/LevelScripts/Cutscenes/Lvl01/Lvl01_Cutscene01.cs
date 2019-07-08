using UnityEngine;
using System.Collections;

public class Lvl01_Cutscene01 : CutsceneController
{
    public Camera cam01;
    public float ctr = 5f;

    public override void UpdateMe()
    {
        base.UpdateMe();

        if (step == 1)
        {
            cam01.active = true;
            SetStep(1.1f);
        }

        if (step == 1.1f)
        {
            ctr = MathfPlus.DecByDeltatimeToZero(ctr);

            if (ctr == 0)
            {
                StartEnding(false);
            }
        }
    }

    public override void EndMe()
    {
        base.EndMe();
        cam01.active = false;
    }
}
