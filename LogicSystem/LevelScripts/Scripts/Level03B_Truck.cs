using UnityEngine;
using System.Collections;

public class Level03B_Truck : MonoBehaviour
{

    float step = 0;

    //

    public float step01_Driving_Time = 30;

    public AudioInfo woodDropAudInfo;

    public float timeToPlayWoodDrop = 10;

    public float step02_Dezhabni = 20;

    public GameObject soldier;

    public Light soldierLight;

    public GameObject palyerObjectForFearAnim;

    public GameObject palyerCameraToSlerp;

    public Animation[] animsToChangeWeight;

    public AudioInfo[] audioInfosToChangeVolume;

    public float animsWeightIncDecSpeed = 1.7f;
    public float animPlayerWeightIncDecSpeed = 1f;
    public float playerRotateLerpSpeed = 70;
    public float audioDecIncSpeed = 1;

    public float timeToRunSoldier = 10;
    public float timeToEndSoldier = 25;

    public float timeToStartSoldierLight = 15;
    public float timeToEndSoldierLight = 20;

    public float timeToStartPlayerAnim = 14;
    public float timeToEndPlayerAnim = 28;

    public float timeToStartPlayerRotateLerp = 13;
    public float timeToStopPlayerRotateLerp = 14;

    public float timeToDecAudiosVolume = 0;
    public float timeToIncAudiosVolumeAgain = 44;

    public float timeToDecAnimsWeight = 0;
    public float timeToIncAnimsWeightAgain = 44;

    //

    float timeCounter = 0;

    bool isWoodDropSoundPlayed = false;

    bool isSoldierRun = false;
    bool isSoldierLightStarted = false;
    bool isPlayerAnimStarted = false;

    bool isPlayerAnimFinished = false;
    bool isSoldierLightStopped = false;
    bool isSoldierStopped = false;

    //

    void Start()
    {

    }

    void Update()
    {
        #region Start
        if (step == 0)
        {
            timeCounter = step01_Driving_Time;

            step = 1;
        }
        #endregion

        #region Step01 Driving
        if (step == 1)
        {
            timeCounter = MathfPlus.DecByDeltatimeToZero(timeCounter);

            if (!isWoodDropSoundPlayed)
            {
                if (timeCounter < (step01_Driving_Time - timeToPlayWoodDrop))
                {
                    isWoodDropSoundPlayed = true;
                    woodDropAudInfo.Play();
                }
            }

            if (timeCounter == 0)
            {
                step = 2;
            }
        }
        #endregion

        #region Step02 Dezhbani
        if (step == 2)
        {
            timeCounter = step02_Dezhabni;

            step = 2.1f;
        }
        #endregion

        #region Step 2.1
        if (step == 2.1f)
        {
            timeCounter = MathfPlus.DecByDeltatimeToZero(timeCounter);

            #region AnimsWeight FirstDecreasement
            if ((timeCounter < (step02_Dezhabni - timeToDecAnimsWeight)) && (timeCounter >= (step02_Dezhabni - timeToIncAnimsWeightAgain)))
            {
                foreach (Animation ani in animsToChangeWeight)
                {
                    ani[ani.clip.name].weight = MathfPlus.DecByDeltatimeToZeroWithAdditionalCoef(ani[ani.clip.name].weight, animsWeightIncDecSpeed);
                }
            }
            #endregion

            #region AnimsWeight LastIncreasement
            if (timeCounter < (step02_Dezhabni - timeToIncAnimsWeightAgain))
            {
                foreach (Animation ani in animsToChangeWeight)
                {
                    ani[ani.clip.name].weight += Time.deltaTime * animsWeightIncDecSpeed;
                    ani[ani.clip.name].weight = Mathf.Clamp01(ani[ani.clip.name].weight);
                }
            }
            #endregion

            #region Soldier Start
            if (!isSoldierRun)
            {
                if (timeCounter < (step02_Dezhabni - timeToRunSoldier))
                {
                    isSoldierRun = true;

                    soldier.SetActiveRecursively(true);
                    soldierLight.active = false;
                    soldier.animation.Play();

                }
            }
            #endregion

            #region Soldier End
            if (!isSoldierStopped)
            {
                if (timeCounter < (step02_Dezhabni - timeToEndSoldier))
                {
                    isSoldierStopped = true;

                    soldier.transform.position = new Vector3(1000, 1000, 1000);
                }
            }
            #endregion

            #region SoldierLight Start
            if (!isSoldierLightStarted)
            {
                if (timeCounter < (step02_Dezhabni - timeToStartSoldierLight))
                {
                    isSoldierLightStarted = true;

                    soldierLight.active = true;
                }
            }
            #endregion

            #region SoldierLight End
            if (!isSoldierLightStopped)
            {
                if (timeCounter < (step02_Dezhabni - timeToEndSoldierLight))
                {
                    isSoldierLightStopped = true;

                    soldierLight.active = false;
                }
            }
            #endregion

            #region PlayerAnim Start
            if (!isPlayerAnimStarted)
            {
                if (timeCounter < (step02_Dezhabni - timeToStartPlayerAnim))
                {
                    isPlayerAnimStarted = true;

                    palyerObjectForFearAnim.animation[palyerObjectForFearAnim.animation.clip.name].enabled = true;
                    palyerObjectForFearAnim.animation[palyerObjectForFearAnim.animation.clip.name].weight = 0;

                }
            }
            #endregion

            #region PlayerAnim Run
            if (isPlayerAnimStarted && (timeCounter >= (step02_Dezhabni - timeToEndPlayerAnim)))
            {
                palyerObjectForFearAnim.animation[palyerObjectForFearAnim.animation.clip.name].weight += Time.deltaTime * animPlayerWeightIncDecSpeed;
                palyerObjectForFearAnim.animation[palyerObjectForFearAnim.animation.clip.name].weight = Mathf.Clamp01(palyerObjectForFearAnim.animation[palyerObjectForFearAnim.animation.clip.name].weight);
            }
            #endregion

            #region PlayerAnim End
            if (!isPlayerAnimFinished)
            {
                if (timeCounter < (step02_Dezhabni - timeToEndPlayerAnim))
                {
                    palyerObjectForFearAnim.animation[palyerObjectForFearAnim.animation.clip.name].weight -= Time.deltaTime * animPlayerWeightIncDecSpeed;
                    palyerObjectForFearAnim.animation[palyerObjectForFearAnim.animation.clip.name].weight = Mathf.Clamp01(palyerObjectForFearAnim.animation[palyerObjectForFearAnim.animation.clip.name].weight);

                    if (palyerObjectForFearAnim.animation[palyerObjectForFearAnim.animation.clip.name].weight == 0)
                    {
                        palyerObjectForFearAnim.animation[palyerObjectForFearAnim.animation.clip.name].enabled = false;
                        //palyerCameraToSlerp.GetComponent<MouseLook>().enabled = true;
                        palyerCameraToSlerp.GetComponent<TruckMapMouseLook>().RestartIt();
                        CustomInputManager.ResetInputAxes();
                        isPlayerAnimFinished = true;
                    }
                }
            }
            #endregion

            #region PlayerRotateLerp (All)
            if ((timeCounter < (step02_Dezhabni - timeToStartPlayerRotateLerp)) && timeCounter >= (step02_Dezhabni - timeToStopPlayerRotateLerp))
            {
                //palyerCameraToSlerp.GetComponent<MouseLook>().enabled = false;

                palyerCameraToSlerp.GetComponent<TruckMapMouseLook>().DisableIt();
                Quaternion toQuat = Quaternion.Euler(new Vector3(0, 270, 0));
                palyerCameraToSlerp.transform.localRotation = Quaternion.Slerp(palyerCameraToSlerp.transform.localRotation, toQuat, playerRotateLerpSpeed * Time.deltaTime);
            }
            #endregion

            #region AudiosVol FirstDecreasement
            if ((timeCounter < (step02_Dezhabni - timeToDecAudiosVolume)) && (timeCounter >= (step02_Dezhabni - timeToIncAudiosVolumeAgain)))
            {
                foreach (AudioInfo aud in audioInfosToChangeVolume)
                {
                    aud.SetCustomVolume(aud.customVolume - Time.deltaTime * audioDecIncSpeed);
                }
            }
            #endregion

            #region AudiosVol LastIncreasement
            if (timeCounter < (step02_Dezhabni - timeToIncAudiosVolumeAgain))
            {
                foreach (AudioInfo aud in audioInfosToChangeVolume)
                {
                    aud.SetCustomVolume(aud.customVolume + Time.deltaTime * audioDecIncSpeed);
                }
            }
            #endregion

            if (timeCounter == 0)
                step = 3;
        }
        #endregion

        #region 3 Start screen fading
        if (step == 3f)
        {
            MapLogic.Instance.blackScreenFader.StartFadingOut();

            step = 3.1f;
        }
        #endregion

        #region 3.1 W8 for finish
        if (step == 3.1f)
        {
            if (MapLogic.Instance.blackScreenFader.isFadingFinished)
            {
                SetMissionIsFinished();
                step = 4;
            }
        }
        #endregion
    }

    protected void SetMissionIsFinished()
    {
        int nextLvlNum = GameController.GetNextLevelNumber();
        int oldLvlNum = GameController.gameCurrentLevel;

        if (GameController.gameLastLevel < nextLvlNum)
        {
            GameController.SetGameLastLevel(nextLvlNum);
            GameSaveLoadController.SaveGameState();
        }

        if (nextLvlNum > oldLvlNum)
        {
            GameController.LoadLevelLoadingPage(nextLvlNum, 0);
        }
        else
        {
            GameController.LoadCredits();
        }
    }
}
