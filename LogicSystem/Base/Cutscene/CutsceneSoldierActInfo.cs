using UnityEngine;
using System.Collections;

public class CutsceneSoldierActInfo : MonoBehaviour
{
    public GameObject fakeSoldier;

    public CampCurveInfo[] campCurveInfos;

    CutsceneSoldierAct cutsceneAct = new CutsceneSoldierAct();

    [HideInInspector]
    public ActionStatus status = ActionStatus.NotStarted;

    SkinnedMeshRenderer[] soldierSkinnedMeshRenderers;

    MeshRenderer[] soldierMeshRenderers;

    // Use this for initialization
    void Start()
    {
        soldierSkinnedMeshRenderers = fakeSoldier.GetComponentsInChildren<SkinnedMeshRenderer>();

        soldierMeshRenderers = fakeSoldier.GetComponentsInChildren<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (status == ActionStatus.Running)
        {
            cutsceneAct.UpdateAct();
        }
    }

    public void StartIt()
    {
        ShowCharacters();

        cutsceneAct.Init(fakeSoldier.transform);
        cutsceneAct.InitCampCurveInfos(campCurveInfos);
        cutsceneAct.StartAct();

        status = ActionStatus.Running;
    }

    public void StopIt()
    {
        HideCharacters();

        status = ActionStatus.Finished;
    }

    void ShowCharacters()
    {
        SkinnedMeshRenderer[] smrs = soldierSkinnedMeshRenderers;

        for (int j = 0; j < smrs.Length; j++)
        {
            if (smrs[j] != null)
            {
                smrs[j].enabled = true;
            }
        }


        MeshRenderer[] mrs = soldierMeshRenderers;

        for (int j = 0; j < mrs.Length; j++)
        {
            if (mrs[j] != null)
            {
                mrs[j].enabled = true;
            }
        }
    }

    void HideCharacters()
    {
        SkinnedMeshRenderer[] smrs = soldierSkinnedMeshRenderers;

        for (int j = 0; j < smrs.Length; j++)
        {
            if (smrs[j] != null)
            {
                smrs[j].enabled = false;
            }
        }


        MeshRenderer[] mrs = soldierMeshRenderers;

        for (int j = 0; j < mrs.Length; j++)
        {
            if (mrs[j] != null)
            {
                mrs[j].enabled = false;
            }
        }
    }
}
