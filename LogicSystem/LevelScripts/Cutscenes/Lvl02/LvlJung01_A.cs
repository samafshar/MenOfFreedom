using UnityEngine;
using System.Collections;

public class LvlJung01_A : MonoBehaviour
{

    public AudioClip footStep_Right01;
    public AudioClip footStep_Right02;
    public AudioClip footStep_Left01;
    public AudioClip footStep_Left02;

    public AudioClip footStep_RunningRight01;
    public AudioClip footStep_RunningRight02;
    public AudioClip footStep_RunningLeft01;
    public AudioClip footStep_RunningLeft02;

    public AudioClip footStep_Land;

    public AudioClip allyA_footStep_Land;
    public AudioClip allyB_footStep_Land;

    public float minPitch;
    public float maxPitch;

    public AudioInfo audioInfo;

    public AudioInfo audioInfo_AllyA;
    public AudioInfo audioInfo_AllyB;

    public SkinnedMeshRenderer oldSoldierA;
    public SkinnedMeshRenderer oldSoldierA_Head;
    public SkinnedMeshRenderer newSoldierA;
    public SkinnedMeshRenderer newSoldierA_Head;

    public GameObject DatEnemyObj;

	// Use this for initialization
	void Start () {
        newSoldierA.enabled = false;
        newSoldierA_Head.enabled = false;

        SkinnedMeshRenderer[] sknRenderer = DatEnemyObj.GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (SkinnedMeshRenderer skrn in sknRenderer)
        {
            skrn.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayLeftFoot01()
    {
        SetRandomPitch();
        audioInfo.PlayClip(footStep_Left01);
    }

    public void PlayLeftFoot02()
    {
        SetRandomPitch();
        audioInfo.PlayClip(footStep_Left02);
    }

    public void PlayRightFoot01()
    {
        SetRandomPitch();
        audioInfo.PlayClip(footStep_Right01);
    }

    public void PlayRightFoot02()
    {
        SetRandomPitch();
        audioInfo.PlayClip(footStep_Right02);
    }

    public void PlayRunningLeftFoot01()
    {
        ResetPitch();
        audioInfo.PlayClip(footStep_RunningLeft01);
    }

    public void PlayRunningLeftFoot02()
    {
        ResetPitch();
        audioInfo.PlayClip(footStep_RunningLeft02);
    }

    public void PlayRunningRightFoot01()
    {
        ResetPitch();
        audioInfo.PlayClip(footStep_RunningRight01);
    }

    public void PlayRunningRightFoot02()
    {
        ResetPitch();
        audioInfo.PlayClip(footStep_RunningRight02);
    }

    public void PlayLand()
    {
        ResetPitch();
        audioInfo.PlayClip(footStep_Land);
    }

    void SetRandomPitch()
    {
        audioInfo.SetCustomPitch(Random.Range(minPitch, maxPitch));
    }

    void ResetPitch()
    {
        audioInfo.SetCustomPitch(1);
    }

    //

    public void AllyA_PlayLeftFoot01()
    {
        AllyA_SetRandomPitch();
        audioInfo_AllyA.PlayClip(footStep_Left01);
    }

    public void AllyA_PlayLeftFoot02()
    {
        AllyA_SetRandomPitch();
        audioInfo_AllyA.PlayClip(footStep_Left02);
    }

    public void AllyA_PlayRightFoot01()
    {
        AllyA_SetRandomPitch();
        audioInfo_AllyA.PlayClip(footStep_Right01);
    }

    public void AllyA_PlayRightFoot02()
    {
        AllyA_SetRandomPitch();
        audioInfo_AllyA.PlayClip(footStep_Right02);
    }

    public void AllyA_PlayRunningLeftFoot01()
    {
        AllyA_ResetPitch();
        audioInfo_AllyA.PlayClip(footStep_RunningLeft01);
    }

    public void AllyA_PlayRunningLeftFoot02()
    {
        AllyA_ResetPitch();
        audioInfo_AllyA.PlayClip(footStep_RunningLeft02);
    }

    public void AllyA_PlayRunningRightFoot01()
    {
        AllyA_ResetPitch();
        audioInfo_AllyA.PlayClip(footStep_RunningRight01);
    }

    public void AllyA_PlayRunningRightFoot02()
    {
        AllyA_ResetPitch();
        audioInfo_AllyA.PlayClip(footStep_RunningRight02);
    }

    public void AllyA_PlayLand()
    {
        AllyA_ResetPitch();
        audioInfo_AllyA.PlayClip(footStep_Land);
    }

    void AllyA_SetRandomPitch()
    {
        audioInfo_AllyA.SetCustomPitch(Random.Range(minPitch, maxPitch));
    }

    void AllyA_ResetPitch()
    {
        audioInfo_AllyA.SetCustomPitch(1);
    }

    //

    public void AllyB_PlayLeftFoot01()
    {
        AllyB_SetRandomPitch();
        audioInfo_AllyB.PlayClip(footStep_Left01);
    }

    public void AllyB_PlayLeftFoot02()
    {
        AllyB_SetRandomPitch();
        audioInfo_AllyB.PlayClip(footStep_Left02);
    }

    public void AllyB_PlayRightFoot01()
    {
        AllyB_SetRandomPitch();
        audioInfo_AllyB.PlayClip(footStep_Right01);
    }

    public void AllyB_PlayRightFoot02()
    {
        AllyB_SetRandomPitch();
        audioInfo_AllyB.PlayClip(footStep_Right02);
    }

    public void AllyB_PlayRunningLeftFoot01()
    {
        AllyB_ResetPitch();
        audioInfo_AllyB.PlayClip(footStep_RunningLeft01);
    }

    public void AllyB_PlayRunningLeftFoot02()
    {
        AllyB_ResetPitch();
        audioInfo_AllyB.PlayClip(footStep_RunningLeft02);
    }

    public void AllyB_PlayRunningRightFoot01()
    {
        AllyB_ResetPitch();
        audioInfo_AllyB.PlayClip(footStep_RunningRight01);
    }

    public void AllyB_PlayRunningRightFoot02()
    {
        AllyB_ResetPitch();
        audioInfo_AllyB.PlayClip(footStep_RunningRight02);
    }

    public void AllyB_PlayLand()
    {
        AllyB_ResetPitch();
        audioInfo_AllyB.PlayClip(footStep_Land);
    }

    void AllyB_SetRandomPitch()
    {
        audioInfo_AllyB.SetCustomPitch(Random.Range(minPitch, maxPitch));
    }

    void AllyB_ResetPitch()
    {
        audioInfo_AllyB.SetCustomPitch(1);
    }

    //

    public void RefreshSoldierA()
    {
        oldSoldierA.enabled = false;
        oldSoldierA_Head.enabled = false;

        newSoldierA.enabled = true;
        newSoldierA_Head.enabled = true;
    }

    public void EnableDatEnemy()
    {
        SkinnedMeshRenderer[] sknRenderer = DatEnemyObj.GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (SkinnedMeshRenderer skrn in sknRenderer)
        {
            skrn.enabled = true;
        }

        DatEnemyObj.animation[DatEnemyObj.animation.clip.name].time = 0;
    }
}
