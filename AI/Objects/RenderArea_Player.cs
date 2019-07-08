using UnityEngine;
using System.Collections;

public class RenderArea_Player : MonoBehaviour
{
    public MapLogicJob_FightInReg[] relatedSoldFightInRegJobs;
    public Light[] relatedLights;

    LogicTrigger logTrig;

    public bool IsPlayerIn()
    {
        return logTrig.IsPlayerIn();
    }

    public void ShowSoldiers()
    {
        foreach (MapLogicJob_FightInReg fInRegJob in relatedSoldFightInRegJobs)
        {
            if (fInRegJob)
            {
                if (fInRegJob.controlledSoldier)
                {
                    SoldierInfo sInf = fInRegJob.controlledSoldier.GetComponent<SoldierInfo>();

                    sInf.ShowChildSkinnedMeshRenderers();
                }
            }
            else
            {
                print("FightInReg job that is assigned to a RenderAreaRelation is missing or null!");
            }
        }

        foreach (Light lite in relatedLights)
        {
            if (lite)
            {
                lite.gameObject.active = true;
                lite.enabled = true;
            }
            else
            {
                print("Light that is assigned to a RenderAreaRelation is missing or null!");
            }
        }
    }

    public void HideSoldiers()
    {
        foreach (MapLogicJob_FightInReg fInRegJob in relatedSoldFightInRegJobs)
        {
            if (fInRegJob)
            {
                if (fInRegJob.controlledSoldier)
                {
                    SoldierInfo sInf = fInRegJob.controlledSoldier.GetComponent<SoldierInfo>();

                    sInf.HideChildSkinnedMeshRenderers();
                }
            }
            else
            {
                print("FightInReg job that is assigned to a RenderAreaRelation is missing or null!");
            }
        }

        foreach (Light lite in relatedLights)
        {
            if (lite)
            {
                lite.gameObject.active = false;
                lite.enabled = false;
            }
            else
            {
                print("Light that is assigned to a RenderAreaRelation is missing or null!");
            }
        }
    }

    public void StartIt()
    {
        logTrig = GetComponent<LogicTrigger>();
        logTrig.SetEnabled(true);
	}
}
