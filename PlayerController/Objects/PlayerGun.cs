using UnityEngine;
using System.Collections;

public enum PlayerGunName
{
    Springfield,
    LeeEnfield,
    MP18,
    Snipe,
    FourLool,
    Winchester,
    Luggar,
}

public enum PlayerGunReloadMode
{
    Full,
    Dundun,
}

public class PlayerGun : MonoBehaviour
{

    public PlayerGunName gunName;

    public bool isAutomatic = false;

    public bool dundunReloadMode = false;

    public bool doesContainTarget = true;

    public int bulletCapacity = 50;

    public int magazineCapacity = 10;

    public int bulletExpense = 1;

    public float maxBetweenFireTime = 0.5f;

    public float reloadTimeOnAnim = 0.4f;

    public float dispersionAngle_Noraml_Min = 1;

    public float dispersionAngle_Noraml_Max = 4;

    public float dispersionAngle_Noraml_BulletInc = 1;

    public float dispersionAngle_Aim_Min = 0.5f;

    public float dispersionAngle_Aim_Max = 1;

    public float dispersionAngle_Aim_BulletInc = 0.4f;
        
    public float normalAccurateShotChance = 0.2f;

    public float aimAccurateShotChance = 0.6f;

    public float normalFOV = 46;

    public float aimFOV = 36;

    public float fovSwitchingSpeed = 0; // 0 means using anim speed.

    public float dispersionAngleDecSpeed = 10;

    public float dispersionAngleDecSpeedWhenOnAir = 6;

    public bool useCustomOffset = false;

    public Transform customOffsetTransform;

    public GameObject meshObject;

    public GameObject bulletType;

    public FireObjects[] fireObjects;

    public GameObject shellObject;

    public float shellEjectSpin = 0.5f;

    public float shellEjectSpeed = 0.25f;

    public AudioInfo audioInfo_Main;

    public Weapon_SoundList soundList;

    public bool isSnipe = false;

    public PlayerSnipeGunInfo snipeInfo;

    public GameObject itemGameObject;

    public HUDGroupName hudBullet_GroupName;

    public HUDControlName hudGun_ControlName;

    public int shapeHUDIndex = 0;

    //

    int curBulletCount;
    int curMagazineCount;

    float spreadAmountCoef = 0.1f;

    float curFireTime = 0;

    bool isInited = false;

    float soundMaxPitch = 1.1f;
    float soundMinPitch = 0.91f;

    //

    [HideInInspector]
    public float currentDispersionAngle = 0;

    [HideInInspector]
    public bool isAvailable = false;

    [HideInInspector]
    public bool isActive = false;

    [HideInInspector]
    public Transform positionBone;

    [HideInInspector]
    public Transform rotationBone;

    [HideInInspector]
    public bool isAimed = false;

    [HideInInspector]
    public Vector3 posBoneInitialLocalPosition;

    [HideInInspector]
    public Quaternion rotBoneInitialLocalRotation;

    [HideInInspector]
    public float curFireTimeDecreasementAdditionalCoef = 1;
    
    bool isPlayerOnAir = false;
    bool needToDecreaseDispersionOnAir = false;

    float playerMovementSpeed = 0f;
    
    float totalDispAng = 0;

    float playerMvSpeedIncCoefForDispersion = 3.5f;

    float dispersionAngleFromShooting = 0;

    float makeDispersionAngleMoreRealCoef = 0.1f;

    //

    void Start()
    {
        Init();
    }

    void Update()
    {
        curFireTime = MathfPlus.DecByDeltatimeToZeroWithAdditionalCoef(curFireTime, curFireTimeDecreasementAdditionalCoef);

        #region Dispersion Angle

        #region By Movement
        if (isPlayerOnAir)
        {
            if (totalDispAng <= dispersionAngle_Noraml_Max && !needToDecreaseDispersionOnAir)
            {
                totalDispAng += GetDispersionAngleWhenPlayerBeOnAir();

                if (totalDispAng > dispersionAngle_Noraml_Max)
                    needToDecreaseDispersionOnAir = true;
            }
            else
            {
                if (GetDispersionAngleByPlayerMovementSpeed() == 0)
                    totalDispAng = MathfPlus.DecByDeltatimeToZeroWithAdditionalCoef(totalDispAng, dispersionAngleDecSpeedWhenOnAir);
            }
        }
        else
        {
            totalDispAng += GetDispersionAngleByPlayerMovementSpeed();

            if (GetDispersionAngleByPlayerMovementSpeed() == 0)
            {
                totalDispAng = MathfPlus.DecByDeltatimeToZeroWithAdditionalCoef(totalDispAng, dispersionAngleDecSpeed);
            }

            needToDecreaseDispersionOnAir = false;
        }
        #endregion

        #region By Shooting
        totalDispAng += dispersionAngleFromShooting;

        dispersionAngleFromShooting = MathfPlus.DecByDeltatimeToZeroWithAdditionalCoef(dispersionAngleFromShooting, dispersionAngleDecSpeed);
        #endregion

        totalDispAng = ClampTotalDispersionAngle(totalDispAng);

        SetCurrentDispersionAngle(totalDispAng); 

        #endregion
    }

    void OnDestroy()
    {
        if (audioInfo_Main != null)
            Destroy(audioInfo_Main.gameObject);
    }

    public bool TryFire()
    {
        bool permit = IsReady();

        if (!permit)
        {
            if (curBulletCount == 0 && curMagazineCount == 0)
            {
                PlaySound_Empty();
            }

            return false;
        }

        int fireCount = Mathf.Min(bulletExpense, curMagazineCount);

        Fire(fireCount);

        return true;
    }

    public void SetAvailable()
    {
        isAvailable = true;
    }

    public void SetUnavailable()
    {
        isAvailable = false;

        SetCurrentBulletCount(0);
        SetCurrentMagazineCount(0);
    }

    public void SetActive()
    {
        isActive = true;

        SetIsAimed(false);

        currentDispersionAngle = dispersionAngle_Noraml_Min;

        curFireTime = 0;
    }

    public void SetDeactive()
    {
        isActive = false;
    }

    void Fire(int _numOfBullets)
    {
        int numOfBullets = _numOfBullets;

        curFireTime += maxBetweenFireTime;

        OnFire();

        CreateBullet(numOfBullets);

        SetCurrentMagazineCount(curMagazineCount - numOfBullets);
    }

    void OnFire()
    {
        PlaySound_Fire();

        foreach (FireObjects obj in fireObjects)
        {
            obj.SetRandomObject();

            GameObject gObj = Instantiate(obj.ChoosenObject) as GameObject;

            Transform bipTransform = transform.GetComponentInChildren<WeaponBipInfo>().transform;

            gObj.transform.parent = bipTransform;
            gObj.transform.localPosition = obj.ChoosenTransform.localPosition;
            gObj.transform.localRotation = obj.ChoosenTransform.localRotation;
        }

        CreateShell();
    }

    void CreateShell()
    {
        if (shellObject == null)
            return;

        GameObject gObj = Instantiate(shellObject) as GameObject;

        WeaponBipInfo bipHandler = GetComponentInChildren<WeaponBipInfo>();

        gObj.transform.position = bipHandler.ShellTransform.position;
        //gObj.transform.rotation = bipHandler.ShellTransform.rotation;

        int rnd = Random.Range(0, bipHandler.ShellHelper.Length);

        Vector3 shellEjectDirection = (bipHandler.ShellHelper[rnd].position - bipHandler.ShellTransform.position).normalized;

        Rigidbody shellBody = gObj.transform.GetComponentInChildren<Rigidbody>();

        shellBody.AddForceAtPosition(shellEjectDirection * shellEjectSpeed, gObj.transform.position, ForceMode.Impulse);

        if (Random.value > 0.5f)
            shellBody.AddRelativeTorque(-Random.rotation.eulerAngles * shellEjectSpin);
        else
            shellBody.AddRelativeTorque(Random.rotation.eulerAngles * shellEjectSpin);
    }

    void CreateBullet(int _numOfBullets)
    {
        int numOfBullets = _numOfBullets;

        if (!isAimed)
        {
            AddDispersionAngleFromShooting(false);
        }
        else
        {
            AddDispersionAngleFromShooting(true);
        }

        transform.root.BroadcastMessage("BulletFire");

        for (int i = 0; i < numOfBullets; i++)
        {
            GameObject obj = GameObject.Instantiate(bulletType, GetFirePosition(), transform.rotation) as GameObject;
            Bullet bul = obj.GetComponent<Bullet>();

            float spreadAmount = 0;

            float chance = Random.Range(0f, 1f);

            if (!isAimed)
            {
                if (chance > normalAccurateShotChance)
                {
                    spreadAmount = currentDispersionAngle * makeDispersionAngleMoreRealCoef;
                }
            }
            else
            {
                if (chance > aimAccurateShotChance)
                {
                    spreadAmount = currentDispersionAngle * makeDispersionAngleMoreRealCoef;
                }
            }

            float vx = (1 - 2 * Random.value) * spreadAmount * spreadAmountCoef;
            float vy = (1 - 2 * Random.value) * spreadAmount * spreadAmountCoef;
            float vz = 1.0f;

            Vector3 spreadVector = PlayerCharacterNew.Instance.mainCam.transform.TransformDirection(new Vector3(vx, vy, vz));

            bul.InitBulletProp(spreadVector, PlayerCharacterNew.Instance.gameObject);
        }
    }

    Vector3 GetFirePosition()
    {
        return PlayerCharacterNew.Instance.mainCam.ScreenToWorldPoint(new Vector3(Screen.width / 2,
                                                            Screen.height / 2, PlayerCharacterNew.Instance.mainCam.nearClipPlane));
    }

    public bool IsReady()
    {
        if (curMagazineCount <= 0)
            return false;

        if (curFireTime > 0)
            return false;

        return true;
    }

    public bool IsShootingTimeFinished()
    {
        return curFireTime == 0;
    }

    void SetCurrentDispersionAngle(float _value)
    {
        currentDispersionAngle = _value;

        if (!isAimed)
            currentDispersionAngle = Mathf.Clamp(currentDispersionAngle, dispersionAngle_Noraml_Min, dispersionAngle_Noraml_Max);
        else
            currentDispersionAngle = Mathf.Clamp(currentDispersionAngle, dispersionAngle_Aim_Min, dispersionAngle_Aim_Max);
    }

    float ClampTotalDispersionAngle(float _value)
    {
        float val = _value;

        if (!isAimed)
            val = Mathf.Clamp(val, dispersionAngle_Noraml_Min, dispersionAngle_Noraml_Max);
        else
            val = Mathf.Clamp(val, dispersionAngle_Aim_Min, dispersionAngle_Aim_Max);

        return val;
    }

    void AddDispersionAngleFromShooting(bool _isAimed)
    {
        bool isA = _isAimed;

        if (isA)
        {
            dispersionAngleFromShooting += dispersionAngle_Aim_BulletInc;

            dispersionAngleFromShooting = Mathf.Clamp(dispersionAngleFromShooting, dispersionAngle_Aim_Min, dispersionAngle_Aim_Max);
        }
        else
        {
            dispersionAngleFromShooting += dispersionAngle_Noraml_BulletInc;

            dispersionAngleFromShooting = Mathf.Clamp(dispersionAngleFromShooting, dispersionAngle_Noraml_Min, dispersionAngle_Noraml_Max);
        }
    }

    public void SetPlayerCurMovementSpeed(float _moveSpeed)
    {
        playerMovementSpeed = _moveSpeed;
    }

    public void SetPlayerIfOnAir(bool _isOnAir)
    {
        isPlayerOnAir = _isOnAir;
    }

    float GetDispersionAngleByPlayerMovementSpeed()
    {
        //return (playerMovementSpeed / playerMovementSpeedMakhraj) * dispersionAngleFromFullMovementSpeed;

        return playerMovementSpeed * playerMvSpeedIncCoefForDispersion * Time.deltaTime;
    }

    float GetDispersionAngleWhenPlayerBeOnAir()
    {
        return PlayerCharacterNew.Instance.maxNormalMoveSpeed * playerMvSpeedIncCoefForDispersion * Time.deltaTime;
    }

    public bool NeedsReload()
    {
        if (curMagazineCount == 0 && curBulletCount > 0)
        {
            return true;
        }

        return false;
    }

    public bool CanReload()
    {
        if (curMagazineCount < magazineCapacity && curBulletCount > 0)
        {
            return true;
        }

        return false;
    }

    public void Reload(PlayerGunReloadMode _mode)
    {
        if (!CanReload())
            return;

        PlayerGunReloadMode mode = _mode;

        switch (mode)
        {
            case PlayerGunReloadMode.Full:

                if (curBulletCount >= (magazineCapacity - curMagazineCount))
                {
                    SetCurrentBulletCount(curBulletCount - (magazineCapacity - curMagazineCount));
                    //curBulletCount -= (magazineCapacity - curMagazineCount);
                    //curMagazineCount = magazineCapacity;
                    SetCurrentMagazineCount(magazineCapacity);
                }
                else
                {
                    SetCurrentMagazineCount(curMagazineCount + curBulletCount);
                    //curMagazineCount += curBulletCount;
                    //curBulletCount = 0;
                    SetCurrentBulletCount(0);
                }

                break;

            case PlayerGunReloadMode.Dundun:

                SetCurrentBulletCount(curBulletCount - 1);
                //curBulletCount--;
                //curMagazineCount++;
                SetCurrentMagazineCount(curMagazineCount + 1);

                break;
        }
    }

    public bool IsItLastBulletOfReload()
    {
        if ((curMagazineCount == magazineCapacity - 1) || curBulletCount == 1)
        {
            return true;
        }

        return false;
    }

    public void PlaySound_Empty()
    {
        if (soundList.Sound_Empty.Length > 0 && audioInfo_Main.IsReady())
            PlaySound(soundList.Sound_Empty, false);
    }

    void PlaySound_Fire()
    {
        if (soundList.Sound_Fire.Length > 0)
        {
            PlaySound(soundList.Sound_Fire, true);
        }
    }

    public void PlaySound_Reload()
    {
        if (soundList.Sound_Reload.Length > 0)
            PlaySound(soundList.Sound_Reload, false);
    }

    public void PlaySound_ReloadChikkiStart()
    {
        if (soundList.Sound_Reload_Start != null)
            PlaySound(soundList.Sound_Reload_Start, false);
    }

    public void PlaySound_ReloadChikkiEnd()
    {
        if (soundList.Sound_Reload_End != null)
            PlaySound(soundList.Sound_Reload_End, false);
    }

    public void Init()
    {
        if (!isInited)
        {
            isInited = true;

            GunBonesForPosAndRot gunBonesHandler = meshObject.GetComponent<GunBonesForPosAndRot>();
            if (gunBonesHandler == null)
            {
                Debug.LogError("Olagh bone e pos va rot e guno set kon avval!");
                return;
            }

            positionBone = gunBonesHandler.BonePosition;
            rotationBone = gunBonesHandler.BoneRotation;

            posBoneInitialLocalPosition = positionBone.localPosition;
            rotBoneInitialLocalRotation = rotationBone.localRotation;

            SetCurrentBulletCount(bulletCapacity);
            //curBulletCount = bulletCapacity;
            //curMagazineCount = magazineCapacity;
            SetCurrentMagazineCount(magazineCapacity);

            SetIsAimed(false);

            currentDispersionAngle = dispersionAngle_Noraml_Min;

            if (useCustomOffset)
            {
                transform.localPosition = customOffsetTransform.position;
                transform.localRotation = customOffsetTransform.rotation;
            }
            else
            {
                transform.localPosition = new Vector3(-positionBone.localPosition.x, -positionBone.localPosition.y, -positionBone.localPosition.z);
                transform.localRotation = Quaternion.identity;
            }

            dispersionAngleFromShooting = dispersionAngle_Noraml_Min;
        }
    }

    public void SetIsAimed(bool _isAimed)
    {
        isAimed = _isAimed;
    }

    public void SetCurrentBulletCount(int _val)
    {
        curBulletCount = _val;

        curBulletCount = Mathf.Clamp(curBulletCount, 0, bulletCapacity); // + magazineCapacity - curMagazineCount);
    }
    public void SetCurrentMagazineCount(int _val)
    {
        curMagazineCount = _val;

        curMagazineCount = Mathf.Clamp(curMagazineCount, 0, magazineCapacity);
    }

    public int GetCurrentBulletCount()
    {
        return curBulletCount;
    }
    public int GetCurrentMagazineCount()
    {
        return curMagazineCount;
    }

    void PlaySound(AudioClip[] _audios, bool _randomPitch)
    {
        AudioClip[] audios = _audios;
        bool randomPitch = _randomPitch;

        if (randomPitch)
            audioInfo_Main.SetCustomPitch(Random.Range(soundMinPitch, soundMaxPitch));
        else
            audioInfo_Main.SetCustomPitch(1);

        audioInfo_Main.PlayClip(audios);
    }

    void PlaySound(AudioClip _audio, bool _randomPitch)
    {
        AudioClip audio = _audio;
        bool randomPitch = _randomPitch;

        if (randomPitch)
            audioInfo_Main.SetCustomPitch(Random.Range(soundMinPitch, soundMaxPitch));
        else
            audioInfo_Main.SetCustomPitch(1);

        audioInfo_Main.PlayClip(audio);
    }

    public void SetCurFireTimeAdditionalCoef(float _val)
    {
        curFireTimeDecreasementAdditionalCoef = _val;
    }

    public float GetCurrentFireTime()
    {
        return curFireTime;
    }
}
