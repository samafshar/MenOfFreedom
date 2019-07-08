using UnityEngine;
using System.Collections;

public class Lvl01_A_FootstepSound : MonoBehaviour
{

    public AudioClip footStep_Right01;
    public AudioClip footStep_Right02;
    public AudioClip footStep_Left01;
    public AudioClip footStep_Left02;

    public float minPitch;
    public float maxPitch;

    public AudioInfo audioInfo_AllyA;
    public AudioInfo audioInfo_AllyB;

    public GameObject khalooGunFireParticle;

    float khalooGunParticleCounter=0;

	void Start () {

	}
	
	void Update () {

        if (khalooGunParticleCounter > 0)
        {
            khalooGunParticleCounter = MathfPlus.DecByDeltatimeToZero(khalooGunParticleCounter);

            if (khalooGunParticleCounter == 0)
            {
                khalooGunFireParticle.GetComponent<ParticleSystem>().Stop();
                khalooGunFireParticle.SetActiveRecursively(false);
            }
        }

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

    void AllyB_SetRandomPitch()
    {
        audioInfo_AllyB.SetCustomPitch(Random.Range(minPitch, maxPitch));
    }

    void AllyB_ResetPitch()
    {
        audioInfo_AllyB.SetCustomPitch(1);
    }

    public void PlayKhalooGunFireParticle()
    {
        khalooGunParticleCounter = 0.25f;
        khalooGunFireParticle.SetActiveRecursively(true);
        khalooGunFireParticle.GetComponent<ParticleSystem>().Play();
    }

    //
}
