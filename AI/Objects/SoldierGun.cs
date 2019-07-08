using UnityEngine;
using System.Collections;

public enum SoldierGunsName
{
    SpringField,
    MP18,
    MachineGun,
}

public class SoldierGun : MonoBehaviour
{

    enum stepEnum
    {
        Start,
        FirstDelayAfterReload,
        Firing,
        BetweenFireLoops,
        Ready,
    }

    public string Note = "For every new gun, fight in point infos must be edited in code and editor";
    public SoldierGunsName name;
    public GameObject bullet;
    public float range;

    public int magazineCountMin;
    public int magaznieCountMax;
    public int fireLoopBulletCountMin;
    public int fireLoopBulletCountMax;
    public float fireTimeMin;
    public float fireTimeMax;
    public float betweenFireLoopsTimeMin;
    public float betweenFireLoopsTimeMax;

    //AudioSource audioSource;
    AudioInfo audioInfo;

    public bool playFireSoundAsOneShot = false;

    [HideInInspector]
    public float criticalDamageChance = 1;

    float criticalDamageCoef = 2.5f;

    public AudioClip[] soundsFire;
    public AudioClip[] soundsReload;

    // SoundPlay soundPlay = new SoundPlay();

    float fireSoundMinPitch = 0.92f;
    float fireSoundMaxPitch = 1.08f;

    float firstShotAfterReloadDelayTimeMin = 0.1f;
    float firstShotAfterReloadDelayTimeMax = 0.3f;

    stepEnum step = stepEnum.Start;

    [HideInInspector]
    public SoldierInfo soldInfo;
    Transform DefaultFireTr;

    int currentMagazieCount;
    int currentFireLoopCount;

    float fireTimeCounter;
    float betweenFireLoopsTimeCounter;
    float firstShotAfterReloadDelayTimeCounter;

    Vector3 selectedFirePos;
    Vector3 targetFirePos;

    Vector3 bulletDir;

    bool doCriticalDamage = false;

    bool applyExternalLogicDamageCoef = false;
    float externalLogicDamageCoef;

    float allyDamageCoef = 0.75f;

    public void InitGun(SoldierGun gunInfo)
    {
        name = gunInfo.name;
        bullet = gunInfo.bullet;
        range = gunInfo.range;
        magazineCountMin = gunInfo.magazineCountMin;
        magaznieCountMax = gunInfo.magaznieCountMax;
        fireTimeMin = gunInfo.fireTimeMin;
        fireTimeMax = gunInfo.fireTimeMax;
        fireLoopBulletCountMin = gunInfo.fireLoopBulletCountMin;
        fireLoopBulletCountMax = gunInfo.fireLoopBulletCountMax;
        betweenFireLoopsTimeMin = gunInfo.betweenFireLoopsTimeMin;
        betweenFireLoopsTimeMax = gunInfo.betweenFireLoopsTimeMax;
        soundsFire = gunInfo.soundsFire;
        soundsReload = gunInfo.soundsReload;
    }

    //public void InitAudioSource(AudioSource _audSource)
    //{
    //    audioSource = _audSource;
    //}

    public void InitAudioInfo(AudioInfo _audInf)
    {
        audioInfo = _audInf;
    }

    //--------------------------------------------

    public void Reload()
    {
        ResetSteps();
    }

    public bool TryFire(Vector3 TargetPos)
    {
        selectedFirePos = DefaultFireTr.position;
        targetFirePos = TargetPos;
        SetBulletDir();
        return FireIfCan();
    }

    public bool TryFire(Vector3 SourcePos, Vector3 TargetPos)
    {
        selectedFirePos = SourcePos;
        targetFirePos = TargetPos;
        SetBulletDir();
        return FireIfCan();
    }

    public bool NeedsReload()
    {
        return (currentMagazieCount == 0);
    }

    public bool IsReady()
    {
        StartGunProcessIfItsNot();

        return step == stepEnum.Ready;
    }

    public void PlayReloadSound()
    {
        audioInfo.SetCustomPitch(1);
        audioInfo.continuePlayingAfterDie = false;
        audioInfo.PlayClip(soundsReload);
    }

    public void PlayShootSound()
    {
        audioInfo.SetCustomPitch(Random.Range(fireSoundMinPitch, fireSoundMaxPitch));
        audioInfo.continuePlayingAfterDie = true;

        if (!playFireSoundAsOneShot)
            audioInfo.PlayClip(soundsFire);
        else
            audioInfo.PlayClip_OneShot(soundsFire);
    }

    bool FireIfCan()
    {
        if (NeedsReload())
            return false;

        StartGunProcessIfItsNot();

        if (step == stepEnum.Ready)
        {
            currentMagazieCount--;
            currentFireLoopCount--;
            //soundPlay.PlaySound_FX(soundsFire, Random.Range(fireSoundMinPitch, fireSoundMaxPitch));

            PlayShootSound();

            CreateBullet();
            step = stepEnum.Firing;
            return true;
        }

        return false;
    }

    void CreateBullet()
    {
        GameObject bulObj = GameObject.Instantiate(bullet, selectedFirePos, Quaternion.LookRotation(bulletDir)) as GameObject;
        Bullet bul = bulObj.GetComponent<Bullet>();

        bul.InitBulletProp(bulletDir, soldInfo.gameObject);

        if (IsCriticalDamage())
        {
            float num = Random.Range(0, 1);

            if (num < criticalDamageChance)
            {
                bul.SetDamage(bul.Damage * criticalDamageCoef);
            }
        }

        if (applyExternalLogicDamageCoef)
        {
            bul.SetDamage(bul.Damage * externalLogicDamageCoef);
        }

        if (!IsCriticalDamage() && !applyExternalLogicDamageCoef)
        {
            if (soldInfo.charInfo.FightSide == FightSideEnum.Ally)
            {
                bul.SetDamage(bul.Damage * allyDamageCoef);

                if (soldInfo.mapLogic.playerCharInfo != null && soldInfo.mapLogic.playerCharInfo.IsAttackable())
                {
                    if (soldInfo.mapLogic.isPlayerHidden)
                    {
                        bul.SetDamage(0);
                    }
                }
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        soldInfo = gameObject.GetComponent<SoldierInfo>();

        if (soldInfo != null)
            DefaultFireTr = soldInfo.soldierGunFireTr;
        else
            DefaultFireTr = transform;

        //soundPlay.Init(audioSource);
        Reload();
    }

    // Update is called once per frame
    void Update()
    {
        if (NeedsReload())
            return;

        if (step == stepEnum.FirstDelayAfterReload)
        {
            firstShotAfterReloadDelayTimeCounter -= Time.deltaTime;

            if (firstShotAfterReloadDelayTimeCounter <= 0)
            {
                ResetFirstShotAfterReloadDelayTimeCounter();
                step = stepEnum.Ready;
                goto End;
            }
        }

        if (step == stepEnum.Firing)
        {
            fireTimeCounter -= Time.deltaTime;

            if (fireTimeCounter <= 0)
            {
                ResetFireTimeCounter();

                if (currentFireLoopCount == 0)
                {
                    step = stepEnum.BetweenFireLoops;
                    goto End;
                }

                step = stepEnum.Ready;
                goto End;
            }
        }

        if (step == stepEnum.BetweenFireLoops)
        {
            betweenFireLoopsTimeCounter -= Time.deltaTime;

            if (betweenFireLoopsTimeCounter <= 0)
            {
                ResetBetwennFireLoopsTimeCounter();
                ResetCurrentFireLoopCount();
                step = stepEnum.Ready;
                goto End;
            }
        }

    End:
        return;
    }

    void ResetCurrentMagazineCount()
    {
        currentMagazieCount = Random.Range(magazineCountMin, magaznieCountMax);
    }
    void ResetCurrentFireLoopCount()
    {
        currentFireLoopCount = Random.Range(fireLoopBulletCountMin, fireLoopBulletCountMax);

        if (currentFireLoopCount > currentMagazieCount)
            currentFireLoopCount = currentMagazieCount;
    }
    void ResetFireTimeCounter()
    {
        fireTimeCounter = Random.Range(fireTimeMin, fireTimeMax);
    }
    void ResetBetwennFireLoopsTimeCounter()
    {
        betweenFireLoopsTimeCounter = Random.Range(betweenFireLoopsTimeMin, betweenFireLoopsTimeMax);
    }
    void ResetFirstShotAfterReloadDelayTimeCounter()
    {
        firstShotAfterReloadDelayTimeCounter = Random.Range(firstShotAfterReloadDelayTimeMin, firstShotAfterReloadDelayTimeMax);
    }

    void ResetSteps()
    {
        step = stepEnum.Start;

        ResetCurrentMagazineCount();
        ResetCurrentFireLoopCount();
        ResetFireTimeCounter();
        ResetBetwennFireLoopsTimeCounter();
        ResetFirstShotAfterReloadDelayTimeCounter();
    }

    void StartGunProcessIfItsNot()
    {
        if (step == stepEnum.Start)
        {
            step = stepEnum.FirstDelayAfterReload;
        }
    }

    void SetBulletDir()
    {
        bulletDir = targetFirePos - selectedFirePos;
    }

    bool IsCriticalDamage()
    {
        return doCriticalDamage;
    }

    public void Set_ControlledSoldier(GameObject _controlledSoldier)
    {
        if (_controlledSoldier == null)
        {
            soldInfo = null;
            return;
        }

        soldInfo = _controlledSoldier.GetComponent<SoldierInfo>();
    }

    public void DoCriticalDamage(bool _doCriticalDamage)
    {
        bool doCritic = _doCriticalDamage;

        doCriticalDamage = doCritic;
    }

    public void ApplyExternalLogicDamageCoef(bool _apply, float _coef)
    {
        bool apply = _apply;
        float coef = _coef;

        applyExternalLogicDamageCoef = apply;
        externalLogicDamageCoef = coef;
    }
}
