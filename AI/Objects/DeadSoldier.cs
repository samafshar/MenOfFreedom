using UnityEngine;
using System.Collections;

public class DeadSoldInitialInfo
{
    public DamageInfo damageInfo;
    public DeadSoldAnim deadSoldAnim = null;
    public string curRunningSoldAnim = "";
    public float curRunningSoldAnimTime = 0;
    public Vector3 initialPos;
    public Quaternion initialRot;
    public Vector3 offset;
    public SoldierVoiceSoundsInfo voiceInfo;
}

public class DeadSoldAnim
{
    public string animName = "";
    public float initialCrossfadeTime = 0.5f;
}

public class DeadSoldier : MonoBehaviour
{
    public AudioInfo dieAudioInfo;

    DeadSoldInitialInfo initialInfo;
    float delayToStartDeadAnim = 0f;
    float timeCounter = 0;

    bool isEnabled = false;
    bool startingAnimDone = false;

    MapLogic mapLogic;

    PlayerCharacterNew playerCharNew;

    RagdollCreator rdCreator;

    AudioClip[] dieVoicesToPlay;

    bool isFirstRun = true;

    bool shouldPlayVoice = true;

    // Use this for initialization
    void Start()
    {
        mapLogic = MapLogic.Instance;

        playerCharNew = PlayerCharacterNew.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFirstRun)
        {
            if (shouldPlayVoice)
            {
                shouldPlayVoice = false;

                if (playerCharNew.isCampPlayer)
                {
                    dieVoicesToPlay = initialInfo.voiceInfo.CampModeDie;
                }
                else
                {
                    dieVoicesToPlay = initialInfo.voiceInfo.Die;
                }

                AudioInfo dieNewAudInf = GameObject.Instantiate(dieAudioInfo, transform.position, transform.rotation) as AudioInfo;
                dieNewAudInf.Init();
                dieNewAudInf.PlayClip(dieVoicesToPlay);

                DieInTime dieAudInfDieInTime = dieNewAudInf.gameObject.AddComponent<DieInTime>();
                dieAudInfDieInTime.time = SoldierStats.MaxTimeOfDeadSoldVoice;
            }
        }

        if (isFirstRun)
            isFirstRun = false;

        if (isEnabled)
        {
            if (!startingAnimDone)
            {
                timeCounter += Time.deltaTime;
                if (timeCounter > delayToStartDeadAnim)
                {
                    startingAnimDone = true;

                    animation[initialInfo.deadSoldAnim.animName].time = 0;
                    animation.CrossFade(initialInfo.deadSoldAnim.animName, initialInfo.deadSoldAnim.initialCrossfadeTime);
                }
            }
            else
            {
                if (GeneralStats.IsUnloopedAnimFinishedOnObject(gameObject, initialInfo.deadSoldAnim.animName, 0))
                {
                    StopThisZombie();
                }
            }
        }
    }

    public void StartThisZombie(DeadSoldInitialInfo _initialInfo)
    {
        initialInfo = _initialInfo;

        transform.position = initialInfo.initialPos;
        transform.rotation = initialInfo.initialRot;

        transform.Translate(initialInfo.offset);

        animation.Play(initialInfo.curRunningSoldAnim);
        animation[initialInfo.curRunningSoldAnim].time = Mathf.Clamp(initialInfo.curRunningSoldAnimTime, 0, animation[initialInfo.curRunningSoldAnim].length);

        rdCreator = GetComponent<RagdollCreator>();

        isEnabled = true;
    }

    public void StopThisZombie()
    {
        isEnabled = false;

        rdCreator.MakeRagdoll(initialInfo.damageInfo, initialInfo, false);
    }
}
