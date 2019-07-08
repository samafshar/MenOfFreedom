using UnityEngine;
using System.Collections;

public class Lvl01_B : MonoBehaviour
{
    public GameObject killerEnemyGunFireParticle;
    public GameObject killerEnemy;

    float timeCounter = 0;
    float timeToShowKillerEnemy = 40;
    float timeToShowKillerEnemyFireParticle = 43.7f;
    float timeToShowFALogo = 47.5f;
    float FALogoDuration = 3.5f;
    float FALogoStartAlphaSpeed = 0.5f;
    float FALogoEndAlphaSpeed = 0.5f;
    float FALogoScaleIncSpeed = 0.0f;
    float FAScale = 1;

    bool isKillerEnemyShown = false;
    bool isKillerEnemyFireParticleShown = false;
    bool isShowingFALogoStarted = false;

    HUDControl hudControl_FA;

    public AudioInfo[] audioInfosToDecreaseVolume;
    float decVolSpeed = 3f;

    bool shouldDecreaseVols = false;

    void Start()
    {
        hudControl_FA = MapLogic.Instance.mapHUDParent.GetChildGroupByName(HUDGroupName.LvlFlashback).GetChildControlByName(HUDControlName.LvlFlashback_FA_Logo);
    }

    void Update()
    {

        timeCounter += Time.deltaTime;

        if (!isKillerEnemyShown)
        {
            if (timeCounter >= timeToShowKillerEnemy)
            {
                isKillerEnemyShown = true;

                killerEnemy.SetActiveRecursively(true);
                killerEnemyGunFireParticle.SetActiveRecursively(false);
            }
        }

        if (!isKillerEnemyFireParticleShown)
        {
            if (timeCounter >= timeToShowKillerEnemyFireParticle)
            {
                isKillerEnemyFireParticleShown = true;

                killerEnemyGunFireParticle.SetActiveRecursively(true);
                killerEnemyGunFireParticle.GetComponent<ParticleSystem>().Stop();
                killerEnemyGunFireParticle.GetComponent<ParticleSystem>().Play();

                shouldDecreaseVols = true;
            }
        }

        if (shouldDecreaseVols)
        {
            foreach (AudioInfo audInf in audioInfosToDecreaseVolume)
            {
                audInf.SetCustomVolume(audInf.customVolume - Time.deltaTime * decVolSpeed);
            }
        }

        if (!isShowingFALogoStarted)
        {
            if (timeCounter >= timeToShowFALogo)
            {
                isShowingFALogoStarted = true;

                hudControl_FA.SetIsVisible(true);
                hudControl_FA.ShowForAWhile(FALogoDuration, FALogoStartAlphaSpeed, FALogoEndAlphaSpeed);
            }
        }

        if (isShowingFALogoStarted)
        {
            FAScale += Time.deltaTime * FALogoScaleIncSpeed;
            hudControl_FA.rect.SetScale(FAScale);
            hudControl_FA.rect.SetDefaultModeValues(hudControl_FA.x, hudControl_FA.y, hudControl_FA.w, hudControl_FA.h);
        }

    }

    //

    public void PlayKillerGunFireParticle()
    {
        killerEnemyGunFireParticle.SetActiveRecursively(true);
        killerEnemyGunFireParticle.GetComponent<ParticleSystem>().Play();
    }

    //
}
