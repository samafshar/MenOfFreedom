using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class SoldierInfo : MonoBehaviour
{
    //public bool printThisSoldier = false;

    public static SoldierInfo instance;

    public SoldierGeneralInfo soldierGeneralInfo;

    public SoldierGun gunInfo;

    public SoldierVoiceSoundsInfo voiceInfo;

    public float DamageChanceOnShooting = 1f;

    public DeadSoldier deadSoldier = null;

    [HideInInspector]
    public SoldierMovementInfo[] movementInfos;

    [HideInInspector]
    public SoldierIdleInfo[] idleInfos;

    //[HideInInspector]
    //public Transform soldierRoot;

    //[HideInInspector]
    //public Transform soldierUpperBodyRoot;

    //[HideInInspector]
    //public Transform soldierGunRoot;

    [HideInInspector]
    public Transform soldierGunFireTr;

    [HideInInspector]
    public GameObject animObject;

    [HideInInspector]
    public SoldierBodyInfo bodyInfo;

    [HideInInspector]
    public GameObject body;

    //[HideInInspector]
    //public AudioSource gunAudioSource;

    //[HideInInspector]
    //public AudioSource footStepsAudioSource;

    //[HideInInspector]
    //public AudioSource voiceAudioSource;

    [HideInInspector]
    public AudioInfo gunAudioInfo;

    [HideInInspector]
    public AudioInfo footStepsAudioInfo;

    [HideInInspector]
    public AudioInfo voiceAudioInfo;

    // ////////

    [HideInInspector]
    public CharacterInfo charInfo;

    [HideInInspector]
    public CharacterController charController;

    [HideInInspector]
    public MapLogic mapLogic;

    //[HideInInspector]
    //public AnimationClip fullAnimationsClip;

    [HideInInspector]
    public Seeker aStarSeeker;

    [HideInInspector]
    public FunnelModifier aStarFunnelModifier;

    [HideInInspector]
    public SimpleSmoothModifier aStarSimpleSmoothModifier;

    [HideInInspector]
    public SoldierGun gun;

    [HideInInspector]
    public float fightRange;

    [HideInInspector]
    public List<DamageInfo> damagesRecieved = new List<DamageInfo>();

    [HideInInspector]
    public DamageInfo firstDamage = null;

    [HideInInspector]
    public bool isDamageRecievedInThisRun = false;

    //[HideInInspector]
    //public SoundPlay soundPlay_VoiceMain = new SoundPlay();

    [HideInInspector]
    public float mainAnimRemainingTime;

    [HideInInspector]
    public bool shouldPlayDamageSound = true;

    [HideInInspector]
    public Vector3[] aStarLastPath = null;

    [HideInInspector]
    public bool isAStarPathResultRecievedInThisRun = false;

    [HideInInspector]
    public bool isAStarPathError = false;

    [HideInInspector]
    public MapLogicJob_FightInReg owner_FightInReg = null;

    [HideInInspector]
    public bool shouldDecreaseCountOfOwnerFightInRegOnDeath = false;

    [HideInInspector]
    public AnimationCurve footStepDelayGraph;

    [HideInInspector]
    public PlayerFootEnum curFoot = PlayerFootEnum.Right;

    [HideInInspector]
    public int curFightRegLaneNum = 0;

    //[HideInInspector]
    //public AntaresBezierController currentMovingCurve = null;

    //-------------------------------------------------------------------------

    //public SoldierDamageAnimPack[] animDamages;

    //-------------------------------------------------------------------------

    float mainAnimCrossfadeTimeCounter = 0;
    float mainAnimTimeCounter = 0;

    [HideInInspector]
    public string mainAnim;

    [HideInInspector]
    public float voiceBusyTimeCounter;

    [HideInInspector]
    public float voiceBusyTimeInitialVal;

    [HideInInspector]
    public float voiceLengthCounter = 0;

    [HideInInspector]
    public bool shouldOnlyTakeDamageFromPlayer = false;

    [HideInInspector]
    public LogicTrigger gettingDamageArea;

    [HideInInspector]
    public Transform soldierEye;

    float playerInCriticSituRange_Min = SoldierStats.playerInCriticSituRange_Min;
    float playerInCriticSituRange_Max = SoldierStats.playerInCriticSituRange_Max;

    bool isPlayerInCriticSitu = false;
    float time_PlayerInCriticSitu_Min = 2f;
    float time_PlayerInCriticSitu_Max = 4f;
    float time_PlayerInCriticSitu_Counter = 0;

    float time_UpdatePlayerInCriticSitu_Max = 0.3f;
    float time_UpdatePlayerInCriticSitu_Counter = 0f;

    bool needsToUpdatePlayerCriticSitu = false;
    bool needsToSlowlyUpdatePlayerCriticSitu = false;

    float recievedDamageTime;

    bool isInited = false;

    float defaultBusyTime_Min = 0.1f;
    float defaultBusyTime_Max = 0.2f;

    MultiDamageController multiDmgCtrl = new MultiDamageController();

    //

    float gentlyGetUnderFootSurfaceMaterial_TimeCounter = 0;
    float gentlyGetUnderFootSurfaceMaterial_Max = 0.15f;

    string lastUnderfootSurfaceMaterial = "";

    Transform footStepRaycastStartTr;
    Transform footStepRaycastEndTr;

    FootStepSoundList footStepSoundList;

    float footStepSoundMaxPitch = 1.11f;
    float footStepSoundMinPitch = 0.89f;

    float forceAttackPlayerTimeCounter = 0f;
    float forceAttackPlayerMaxTime = 2f;

    float forceAttackPlayerRadius = 9f;

    bool isFirstZocMoveDone = false;
    int zocMoveCount = 3;

    //-------------------------------------------------------------------------

    Transform soldHead;
    Transform plHead;
    //Transform plLeftView;
    //Transform plRightView;

    [HideInInspector]
    public bool isStopping = false;
    //float removingCurActCounter = 0;

    [HideInInspector]
    public bool isMoving = false;

    [HideInInspector]
    public SkinnedMeshRenderer[] childSkinnedMeshRenderers;

    [HideInInspector]
    public MeshRenderer[] childMeshRenderers;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (!isInited)
        {
            Init();
        }
    }

    public void Init()
    {
        childSkinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        childMeshRenderers = GetComponentsInChildren<MeshRenderer>();

        charInfo = GetComponent<CharacterInfo>();
        bodyInfo = GetComponentInChildren<SoldierBodyInfo>();

        body = bodyInfo.gameObject;

        //fullAnimationsClip = bodyInfo.fullAnimsClip;

        charInfo.shootRaycastTargets = bodyInfo.shootRaycastTargets;

        Vector3 oldPos;

        CharacterController soldGenInfoCharController = soldierGeneralInfo.GetComponent<CharacterController>();
        charController = gameObject.AddComponent<CharacterController>();

        charController.center = soldGenInfoCharController.center;
        charController.slopeLimit = soldGenInfoCharController.slopeLimit;
        charController.stepOffset = soldGenInfoCharController.stepOffset;
        charController.radius = soldGenInfoCharController.radius;
        charController.height = soldGenInfoCharController.height;

        //soldierRoot = bodyInfo.soldierRoot;
        //soldierUpperBodyRoot = bodyInfo.soldierUpperBodyRoot;
        //soldierGunRoot = bodyInfo.soldierGunRoot;
        soldierGunFireTr = bodyInfo.soldierGunFireTr;
        animObject = bodyInfo.SoldierAnimObject;

        soldierEye = bodyInfo.soldierEyeTr;

        movementInfos = soldierGeneralInfo.movementInfos;
        idleInfos = soldierGeneralInfo.idleInfos;

        //voiceAudioSource = GameObject.Instantiate(soldierGeneralInfo.AudioSource_Voice) as AudioSource;
        //oldPos = voiceAudioSource.transform.position;
        //voiceAudioSource.transform.parent = gameObject.transform;
        //voiceAudioSource.transform.localPosition = oldPos;

        //footStepsAudioSource = GameObject.Instantiate(soldierGeneralInfo.AudioSource_FootSteps) as AudioSource;
        //oldPos = footStepsAudioSource.transform.position;
        //footStepsAudioSource.transform.parent = gameObject.transform;
        //footStepsAudioSource.transform.localPosition = oldPos;

        //gunAudioSource = GameObject.Instantiate(soldierGeneralInfo.AudioSource_Gun) as AudioSource;
        //oldPos = gunAudioSource.transform.position;
        //gunAudioSource.transform.parent = gameObject.transform;
        //gunAudioSource.transform.localPosition = oldPos;

        if (charInfo.FightSide == FightSideEnum.Ally)
        {
            voiceAudioInfo = GameObject.Instantiate(soldierGeneralInfo.AudioInfo_Ally_Voice) as AudioInfo;
        }
        else
        {
            voiceAudioInfo = GameObject.Instantiate(soldierGeneralInfo.AudioInfo_Voice) as AudioInfo;
        }
        oldPos = voiceAudioInfo.transform.position;
        voiceAudioInfo.transform.parent = gameObject.transform;
        voiceAudioInfo.transform.localPosition = oldPos;

        if (charInfo.FightSide == FightSideEnum.Ally)
        {
            footStepsAudioInfo = GameObject.Instantiate(soldierGeneralInfo.AudioInfo_Ally_FootSteps) as AudioInfo;
        }
        else
        {
            footStepsAudioInfo = GameObject.Instantiate(soldierGeneralInfo.AudioInfo_FootSteps) as AudioInfo;
        }
        oldPos = footStepsAudioInfo.transform.position;
        footStepsAudioInfo.transform.parent = gameObject.transform;
        footStepsAudioInfo.transform.localPosition = oldPos;

        if (charInfo.FightSide == FightSideEnum.Ally)
        {
            gunAudioInfo = GameObject.Instantiate(soldierGeneralInfo.AudioInfo_Ally_Gun) as AudioInfo;
        }
        else
        {
            gunAudioInfo = GameObject.Instantiate(soldierGeneralInfo.AudioInfo_Gun) as AudioInfo;
        }
        oldPos = gunAudioInfo.transform.position;
        gunAudioInfo.transform.parent = gameObject.transform;
        gunAudioInfo.transform.localPosition = oldPos;

        charInfo.fightHaloRaycastTargets = new Transform[soldierGeneralInfo.fightHaloRaycastTargets.Length];

        for (int i = 0; i < soldierGeneralInfo.fightHaloRaycastTargets.Length; i++)
        {
            Transform tr = GameObject.Instantiate(soldierGeneralInfo.fightHaloRaycastTargets[i]) as Transform;
            oldPos = tr.position;
            tr.parent = gameObject.transform;
            tr.localPosition = oldPos;

            charInfo.fightHaloRaycastTargets[i] = tr;
        }

        InitAStar();

        mapLogic = MapLogic.Instance;
        mapLogic.AddChar(gameObject);

        //soundPlay_VoiceMain.Init(voiceAudioSource);
        ResetVoiceBusyTimeCounter();

        //soundPlay_VoiceMain.OnPlay += ResetVoiceBusyTimeCounter;

        gun = gameObject.AddComponent<SoldierGun>();
        gun.InitGun(gunInfo);
        //gun.InitAudioSource(gunAudioSource);
        gun.InitAudioInfo(gunAudioInfo);

        fightRange = Mathf.Min(gun.range, charInfo.Range);

        footStepRaycastStartTr = soldierGeneralInfo.footRayCastStartTr;
        footStepRaycastEndTr = soldierGeneralInfo.footRayCastEndTr;

        footStepDelayGraph = soldierGeneralInfo.footStepDelayGraph;
        footStepSoundList = soldierGeneralInfo.footStepSoundList;

        soldHead = charInfo.characterHead;

        if (PlayerCharacterNew.Instance != null)
        {
            plHead = PlayerCharacterNew.Instance.gameObject.GetComponent<CharacterInfo>().characterHead;
            //plLeftView = PlayerCharacterNew.Instance.leftViewTr;
            //plRightView = PlayerCharacterNew.Instance.rightViewTr;
        }

        isInited = true;
    }

    void OnDestroy()
    {
        mapLogic.RemoveChar(gameObject);
    }

    void Update()
    {
        if (!isFirstZocMoveDone)
        {
            charController.SimpleMove(new Vector3(0, 0, 0));

            zocMoveCount--;

            if (zocMoveCount == 0)
            {
                isFirstZocMoveDone = true;
            }
        }

        if (isStopping)
        {
            SoldierAction curAct = gameObject.GetComponent<SoldierAction>();

            if (curAct != null)
                Destroy(curAct);
            else
            {

                //removingCurActCounter++;

                //if (removingCurActCounter >= 5)
                //{

                //removingCurActCounter = 0;

                charController.enabled = false;

                body.SetActiveRecursively(false);

                Renderer[] rends = gameObject.GetComponentsInChildren<Renderer>();

                for (int i = 0; i < rends.Length; i++)
                {
                    rends[i].enabled = false;
                }

                gameObject.active = false;

                isStopping = false;

                //}
            }
        }

        DecAnimCounters();

        voiceBusyTimeCounter -= Time.deltaTime;
        voiceBusyTimeCounter = Mathf.Clamp(voiceBusyTimeCounter, 0, 1000);

        voiceLengthCounter = MathfPlus.DecByDeltatimeToZero(voiceLengthCounter);

        time_UpdatePlayerInCriticSitu_Counter = MathfPlus.DecByDeltatimeToZero(time_UpdatePlayerInCriticSitu_Counter);
        time_PlayerInCriticSitu_Counter = MathfPlus.DecByDeltatimeToZero(time_PlayerInCriticSitu_Counter);
        forceAttackPlayerTimeCounter = MathfPlus.DecByDeltatimeToZero(forceAttackPlayerTimeCounter);

        if (needsToUpdatePlayerCriticSitu)
        {
            UpdatePlayerCriticState();
        }

        bool isSlowlyUpdated = false;

        if (needsToSlowlyUpdatePlayerCriticSitu)
        {
            SlowlyUpdatePlayerCriticState();
            isSlowlyUpdated = true;
        }

        if (time_PlayerInCriticSitu_Counter > 0)
        {
            if (!isSlowlyUpdated)
                SlowlyUpdatePlayerCriticState();
        }
        else
        {
            SetPlayerIsInCriticSitu(false);
        }

        needsToUpdatePlayerCriticSitu = false;
        needsToSlowlyUpdatePlayerCriticSitu = false;

        if (isDamageRecievedInThisRun)
        {
            if (Time.time > recievedDamageTime)
            {
                isDamageRecievedInThisRun = false;
                damagesRecieved.Clear();
                firstDamage = null;
            }
        }

        gentlyGetUnderFootSurfaceMaterial_TimeCounter = MathfPlus.DecByDeltatimeToZero(gentlyGetUnderFootSurfaceMaterial_TimeCounter);

    }

    void LateUpdate()
    {


        //if (isAStarPathResultRecievedInThisRun)
        //{
        //    isAStarPathResultRecievedInThisRun = false;
        //}
    }

    //public bool RotateSoldierAllBodyToRotation(Quaternion rot, float rotSpeed, float toleranceAngle)
    //{
    //    Quaternion targRot = rot;
    //    Quaternion soldRot = gameObject.transform.rotation;

    //    Quaternion soldOldRot = gameObject.transform.rotation;

    //    targRot.SetEulerAngles(0, targRot.eulerAngles.y * Mathf.Deg2Rad, 0);
    //    soldRot.SetEulerAngles(0, soldRot.eulerAngles.y * Mathf.Deg2Rad, 0);

    //    soldRot = Quaternion.Slerp(soldRot, targRot, rotSpeed * Time.deltaTime);
    //    gameObject.transform.rotation = soldRot;

    //    return (Quaternion.Angle(soldRot, soldOldRot) <= toleranceAngle);
    //}

    public bool RotateSoldierAllBodyToRotation(Quaternion _rot, float _rotSpeed, float _toleranceAngle)
    {
        Quaternion rot = _rot;
        float rotSpeed = _rotSpeed;
        float toleranceAngle = _toleranceAngle;

        float maxStartingAngleToDecreaseTotalRotAmount = 30f;
        float minDecreasedRotAmountCoef = 0.3f;

        Vector3 pos = transform.position + rot * Vector3.forward;

        float rotAmount = rotSpeed * Time.deltaTime;

        Vector3 destRot = pos - transform.position;

        float deltaAngle = Mathf.DeltaAngle(
            MathfPlus.HorizontalAngle(transform.forward),
            MathfPlus.HorizontalAngle(destRot));

        if (Mathf.Abs(deltaAngle) < maxStartingAngleToDecreaseTotalRotAmount)
        {
            rotAmount *= (1 - (((maxStartingAngleToDecreaseTotalRotAmount - Mathf.Abs(deltaAngle)) / maxStartingAngleToDecreaseTotalRotAmount) * (1 - minDecreasedRotAmountCoef)));
        }

        if (deltaAngle < 0)
            rotAmount = -rotAmount;

        if (Mathf.Abs(rotAmount) > Mathf.Abs(deltaAngle))
        {
            transform.rotation = rot;
            return true;
        }

        Quaternion rotVec = Quaternion.Euler(0, rotAmount, 0);

        transform.rotation *= rotVec;
        return false;


        //float deltaAngle = MathfPlus.GetDeltaAngle(transform.forward, transform.position, pos);

        //if (Mathf.Abs(rotAmount) >= Mathf.Abs(deltaAngle))
        //{
        //    transform.rotation = rot;
        //    return true;
        //}

        //float dif = deltaAngle - currentUpBodyAngle;

        //if (dif >= 0)
        //{
        //    currentUpBodyAngle += rotAmount;
        //}
        //else
        //{
        //    currentUpBodyAngle -= rotAmount;
        //}

        //return false;
    }

    public void ApplyDamage(DamageInfo dmg)
    {
        if (shouldOnlyTakeDamageFromPlayer)
        {
            if (dmg.damageSource == null)
                return;

            if (PlayerCharacterNew.Instance == null)
                return;

            if (dmg.damageSource != PlayerCharacterNew.Instance.gameObject)
                return;
        }

        if (gettingDamageArea)
        {
            if (gettingDamageArea.isEnabled)
            {
                if (dmg.damageSource == null)
                    return;

                if (!gettingDamageArea.IsGameObjectIn(dmg.damageSource))
                    return;
            }
        }

        if (multiDmgCtrl.IsDamageAppliedBefore(dmg))
            return;

        multiDmgCtrl.AddDamage(dmg);

        damagesRecieved.Add(dmg);
        recievedDamageTime = Time.time;
        firstDamage = damagesRecieved[0];
        isDamageRecievedInThisRun = true;

        if (!charInfo.IsInvulnerable)
        {
            DamageInfo damage = dmg;
            ChangeHealth(-damage.damageAmount * damage.GetDamageCoefBySoldierBodyPart() * charInfo.ReceivedDamageCoef);
            if (charInfo.CurrentHealth <= 0)
            {
                KillSoldier(dmg);
                return;
            }
        }
    }

    public void KillSoldier(DamageInfo dmg)
    {
        BroadcastMessage("KillYourself", SendMessageOptions.DontRequireReceiver);

        if (owner_FightInReg != null && shouldDecreaseCountOfOwnerFightInRegOnDeath)
        {
            owner_FightInReg.DecreaseCountOfCreatedSoldiers_ForCountRespawnStyle();
        }

        charInfo.IsDead = true;
        mapLogic.RemoveChar(gameObject);

        DeadSoldAnim dsAnim = GetDeadSoldAnimForAnim(mainAnim, dmg);

        DeadSoldInitialInfo dsInitialInfo = new DeadSoldInitialInfo();
        dsInitialInfo.damageInfo = dmg;
        dsInitialInfo.curRunningSoldAnim = mainAnim;
        dsInitialInfo.curRunningSoldAnimTime = bodyInfo.animation[mainAnim].time;
        dsInitialInfo.curRunningSoldAnimTime = 0;
        dsInitialInfo.deadSoldAnim = dsAnim;
        dsInitialInfo.initialPos = transform.position;
        dsInitialInfo.initialRot = transform.rotation;
        dsInitialInfo.offset = body.transform.localPosition;
        dsInitialInfo.voiceInfo = voiceInfo;

        if ((dmg.damageType != DamageType.Explosion) && (Random.Range(0f, 0.9999f) < soldierGeneralInfo.DieAnimChance) && (deadSoldier != null) && (dsAnim != null))
        {
            DeadSoldier newDeadSoldier = Instantiate(deadSoldier, transform.position, transform.rotation) as DeadSoldier;

            newDeadSoldier.StartThisZombie(dsInitialInfo);
            //if(isMoving)

        }
        else
        {
            SoldierBodyInfo sBInfo = gameObject.GetComponentInChildren<SoldierBodyInfo>();

            Transform solBody = sBInfo.gameObject.transform;
            solBody.parent = null;

            RagdollCreator rdCreator = solBody.GetComponent<RagdollCreator>();
            rdCreator.MakeRagdoll(dmg, dsInitialInfo, true);
        }

        Destroy(gameObject);
    }

    public void ChangeHealth(float value)
    {
        charInfo.CurrentHealth += value;
        charInfo.CurrentHealth = Mathf.Clamp(charInfo.CurrentHealth, 0, charInfo.MaxHealth);
    }

    //public SoldierDamageAnimPack GetAnimDamagePackByName(string name)
    //{
    //    foreach (SoldierDamageAnimPack adp in animDamages)
    //    {
    //        if (adp.name == name)
    //            return adp;
    //    }

    //    return null;
    //}

    public SoldierMovementInfo GetMovementInfoByType(MovementTypeEnum _movementType)
    {
        foreach (SoldierMovementInfo mi in movementInfos)
        {
            if (mi.movementType == _movementType)
                return mi;
        }

        return null;
    }
    public SoldierIdleInfo GetIdleInfoByType(IdleActionTypeEnum _idleType)
    {
        foreach (SoldierIdleInfo ii in idleInfos)
        {
            if (ii.idleType == _idleType)
                return ii;
        }

        return null;
    }

    public bool IsEnemyAround()
    {
        List<GameObject> enemySolds = mapLogic.GetMapOppositeSideChars(charInfo.FightSide);

        if (enemySolds.Count == 0)
            return false;

        for (int i = 0; i < enemySolds.Count; i++)
        {
            GameObject obj;
            obj = enemySolds[i];

            if (obj == null)
                continue;

            CharacterInfo chInf = obj.GetComponent<CharacterInfo>();
            if (!chInf.IsAttackable())
                continue;

            if (Vector3.Magnitude(transform.position - obj.transform.position) <= charInfo.Range)
                return true;
        }

        return false;
    }

    public void StartNewMainAnimWithCrossfadeTime(string _anim, float _startCrossfadeTime, float _newAnimStartTimeCoefMin, float _newAnimStartTimeCoefMax)
    {
        float startMin = _newAnimStartTimeCoefMin;
        float startMax = _newAnimStartTimeCoefMax;

        float startTimeCoef = Random.Range(startMin, startMax);

        float cfTime = _startCrossfadeTime;
        mainAnim = _anim;

        float startTime = animObject.animation[mainAnim].length * startTimeCoef;


        animObject.animation[mainAnim].time = startTime;
        animObject.animation.CrossFade(mainAnim, cfTime);
        mainAnimTimeCounter = Mathf.Max(((animObject.animation[mainAnim].length - startTime) / SoldierStats.soldAnimSpeedCoef), (cfTime / SoldierStats.soldAnimSpeedCoef));
        mainAnimCrossfadeTimeCounter = cfTime;
    }

    public void StartNewMainAnimWithCrossfadeTime(string _anim, float _startCrossfadeTime)
    {
        StartNewMainAnimWithCrossfadeTime(_anim, _startCrossfadeTime, 0, 0);
    }

    public bool IsFullyInNewMainAnim()
    {
        return mainAnimCrossfadeTimeCounter == 0;
    }

    public bool CheckMainAnimIsFinished(float _endCrossfadeTime)
    {
        if (mainAnimTimeCounter <= _endCrossfadeTime)
        {
            mainAnimRemainingTime = mainAnimTimeCounter;
            return true;
        }

        mainAnimRemainingTime = mainAnimTimeCounter;
        return false;
    }

    void DecAnimCounters()
    {
        if (mainAnimCrossfadeTimeCounter > 0)
            mainAnimCrossfadeTimeCounter -= Time.deltaTime;
        else
        {
            if (mainAnimCrossfadeTimeCounter < 0)
                mainAnimCrossfadeTimeCounter = 0;
        }

        if (mainAnimTimeCounter > 0)
            mainAnimTimeCounter -= Time.deltaTime;
        else
        {
            if (mainAnimTimeCounter < 0)
                mainAnimTimeCounter = 0;
        }
    }


    public void SetVoiceBusyTime(float _min, float _max)
    {
        voiceBusyTimeCounter = Random.Range(_min, _max);
        voiceBusyTimeInitialVal = voiceBusyTimeCounter;
    }

    public bool IsVoiceOnBusyTimer()
    {
        return voiceBusyTimeCounter > 0;
    }

    void ResetVoiceBusyTimeCounter()
    {
        voiceBusyTimeCounter = 0;
    }

    public bool DidVoiceBusyTimeCounterPassATime(float _time)
    {
        return (voiceBusyTimeInitialVal - voiceBusyTimeCounter) >= _time;
    }

    public void PlayVoiceWithAdditionalBusyTime(AudioClip[] _audioClips, float _minBusyTime, float _maxBusyTime)
    {
        AudioClip clip = _audioClips[Random.Range(0, _audioClips.Length)];
        PlayVoiceWithAdditionalBusyTime(clip, _minBusyTime, _maxBusyTime);
    }

    public void PlayVoiceWithAdditionalBusyTime(AudioClip _audioClip, float _minBusyTime, float _maxBusyTime)
    {
        AudioClip clip = _audioClip;
        float minBusyTime = _minBusyTime + clip.length;
        float maxBusyTime = _maxBusyTime + clip.length;

        //soundPlay_VoiceMain.PlaySound_Voice(clip);
        voiceAudioInfo.PlayClip(clip);

        voiceLengthCounter = clip.length + 0.05f;


        SetVoiceBusyTime(minBusyTime, maxBusyTime);
    }

    public bool IsVoiceLengthCountingFinished()
    {
        return voiceLengthCounter == 0;
    }


    void InitAStar()
    {
        aStarSeeker = gameObject.AddComponent<Seeker>();
        aStarSeeker.startEndModifier.exactStartPoint = StartEndModifier.Exactness.Exact;
        aStarSeeker.startEndModifier.exactEndPoint = StartEndModifier.Exactness.Exact;
        aStarSeeker.startEndModifier.mask = soldierGeneralInfo.pathFindObstacleMask;

        aStarFunnelModifier = gameObject.AddComponent<FunnelModifier>();

        aStarSimpleSmoothModifier = gameObject.AddComponent<SimpleSmoothModifier>();

        aStarSeeker.startEndModifier.priority = 3;
        aStarFunnelModifier.priority = 2;
        aStarSimpleSmoothModifier.priority = 1;
    }

    public void FindNewAStarPath(Vector3 _startPos, Vector3 _endPos)
    {
        isAStarPathResultRecievedInThisRun = false;
        aStarSeeker.StartPath(_startPos, _endPos, RefreshAStarPathfindResult);
    }

    public void RefreshAStarPathfindResult(Pathfinding.Path path)
    {
        isAStarPathResultRecievedInThisRun = true;
        isAStarPathError = false;

        if (path == null || path.error)
        {
            isAStarPathError = true;
            aStarLastPath = null;
            return;
        }

        aStarLastPath = path.vectorPath;
    }

    public bool IsPlayerInView_ForCamp()
    {
        if (PlayerCharacterNew.Instance == null)
        {
            Debug.LogError("Player doesn't exist in map!");
            return false;
        }

        PlayerCharacterNew player = PlayerCharacterNew.Instance;
        GameObject playerObj = PlayerCharacterNew.Instance.gameObject;

        Vector3 rayCastPos = soldierEye.position;
        Quaternion rayCastRot = soldierEye.rotation;
        float viewRange = mapLogic.camp_CurUpdate_SoldsViewRange * mapLogic.campSoldierViewRangeCoef;
        float viewHalfAng = mapLogic.camp_CurUpdate_SoldsViewAngle;

        float distToPlayer = Vector3.Magnitude(playerObj.transform.position - transform.position);

        //Back
        if (player.IsVertMovementState(PlayerVertMovementStateEnum.Stand) || player.IsVertMovementState(PlayerVertMovementStateEnum.Jump))
        {
            if (distToPlayer <= SoldierStats.campSoldier_BackMaxDist)
            {
                Transform targTr = player.campViewRaysactTargets[0];

                if (!GeneralStats.IsVecInView(targTr.position, rayCastPos, rayCastRot, -viewHalfAng, viewHalfAng, viewRange)) //<--- NOT
                {
                    Vector3 raycastDirection = targTr.position - soldierEye.position;
                    float rayDirMag = raycastDirection.magnitude;
                    float range = rayDirMag;

                    if (range > viewRange)
                        return false;

                    Vector3 raycastStart = soldierEye.position;

                    RaycastHit[] hits = Physics.RaycastAll(raycastStart, raycastDirection, range, GameGeneralInfo.Instance.SoldierCampModeViewRaycastLayer);

                    bool isHittedToObs = false;

                    foreach (RaycastHit hit in hits)
                    {
                        if (hit.transform.root == gameObject.transform)
                            continue;

                        if (hit.transform.gameObject.tag.ToLower() == GeneralStats.camWallTagName_ToLower)
                        {
                            CampWall cWall = hit.transform.gameObject.GetComponent<CampWall>();

                            if (mapLogic.CanKeepRaycastingThroughCampWall(cWall, hit.point))
                                continue;
                        }

                        isHittedToObs = true;
                    }

                    if (!isHittedToObs)
                        return true;
                }
            }
        }

        foreach (Transform targTr in player.campViewRaysactTargets)
        {
            if (!GeneralStats.IsVecInView(targTr.position, rayCastPos, rayCastRot, -viewHalfAng, viewHalfAng, viewRange))
                return false;

            Vector3 raycastDirection = targTr.position - soldierEye.position;
            float rayDirMag = raycastDirection.magnitude;
            float range = rayDirMag;

            if (range > viewRange)
                return false;

            Vector3 raycastStart = soldierEye.position;

            RaycastHit[] hits = Physics.RaycastAll(raycastStart, raycastDirection, range, GameGeneralInfo.Instance.SoldierCampModeViewRaycastLayer);

            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.root == gameObject.transform)
                    continue;

                if (hit.transform.gameObject.tag.ToLower() == GeneralStats.camWallTagName_ToLower)
                {
                    CampWall cWall = hit.transform.gameObject.GetComponent<CampWall>();

                    if (mapLogic.CanKeepRaycastingThroughCampWall(cWall, hit.point))
                        continue;
                }

                return false;
            }
        }

        return true;

        //CharRaycastResult charRaycastRes;

        //mapLogic.IsCharacterOkAsTarget(gameObject, playerObj, soldierEye.position, soldierEye.rotation, viewRange, -viewHalfAng, viewHalfAng, out charRaycastRes);

        //return charRaycastRes.isCharacterHitted;
    }

    public bool IsPlayerInView()
    {
        if (plHead == null)
        {
            print("Player's head is not assigned. Do it!");
            return false;
        }

        Vector3 raycastDirection = plHead.position - soldHead.position;
        float rayDirMag = raycastDirection.magnitude;
        float range = rayDirMag;

        if (range > charInfo.Range)
            return false;

        Vector3 raycastStart = soldHead.position;

        RaycastHit[] hits = Physics.RaycastAll(raycastStart, raycastDirection, range, GameGeneralInfo.Instance.SoldierViewRaycastLayer);

        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.root == gameObject.transform)
                continue;

            return false;
        }

        return true;
    }

    public bool IsPlayerInView_ConsideringCampWall()
    {
        if (plHead == null)
        {
            print("Player's head is not assigned. Do it!");
            return false;
        }

        Vector3 raycastDirection = plHead.position - soldHead.position;
        float rayDirMag = raycastDirection.magnitude;
        float range = rayDirMag;

        if (range > charInfo.Range)
            return false;

        Vector3 raycastStart = soldHead.position;

        RaycastHit[] hits = Physics.RaycastAll(raycastStart, raycastDirection, range, GameGeneralInfo.Instance.SoldierCampModeViewRaycastLayer);

        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.root == gameObject.transform)
                continue;

            if (hit.transform.gameObject.tag.ToLower() == GeneralStats.camWallTagName_ToLower)
            {
                CampWall cWall = hit.transform.gameObject.GetComponent<CampWall>();

                if (mapLogic.CanKeepRaycastingThroughCampWall(cWall, hit.point))
                    continue;
            }

            return false;
        }

        return true;
    }

    public bool ArePlayerSidesInView()
    {
        if (plHead == null)
        {
            print("Player's head is not assigned. Do it!");
            return false;
        }

        if (bodyInfo.playerViewTrs == null)
        {
            print("Soldier transforms for checking player views are not assigned. Do it!");
            return false;
        }

        if (bodyInfo.playerViewTrs.Length == 0)
        {
            print("Soldier transforms for checking player views are not assigned. Do it!");
            return false;
        }

        foreach (Transform rayTr in bodyInfo.playerViewTrs)
        {
            Vector3 raycastDirection = plHead.position - rayTr.position;
            float rayDirMag = raycastDirection.magnitude;
            float range = rayDirMag;

            if (range > charInfo.Range)
                return false;

            Vector3 raycastStart = rayTr.position;

            RaycastHit[] hits = Physics.RaycastAll(raycastStart, raycastDirection, range, GameGeneralInfo.Instance.SoldierViewRaycastLayer);

            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.root == gameObject.transform)
                    continue;

                return false;
            }
        }

        return true;
    }


    void UpdatePlayerCriticState()
    {
        float distToPlayer = Vector3.Distance(mapLogic.player.transform.position, transform.position);

        if (mapLogic.player == null)
        {
            SetPlayerIsInCriticSitu(false);
            return;
        }

        if (!mapLogic.playerCharInfo.IsAttackable())
            return;

        if (mapLogic.playerCharInfo.GetEnemyFightSide() != charInfo.FightSide)
        {
            SetPlayerIsInCriticSitu(false);
            return;
        }

        if (distToPlayer < playerInCriticSituRange_Min)
        {
            SetPlayerIsInCriticSitu(true);
            //ForceToAttackPlayerForAWhile_IfNotMoving();
            return;
        }

        if (time_PlayerInCriticSitu_Counter > 0 && ((distToPlayer >= playerInCriticSituRange_Min) && (distToPlayer < playerInCriticSituRange_Max)))
        {
            if (IsPlayerInView())
            {
                SetPlayerIsInCriticSitu(true);
                //ForceToAttackPlayerForAWhile_IfNotMoving();
                return;
            }
        }

        if (forceAttackPlayerTimeCounter > 0)
        {
            SetPlayerIsInCriticSitu(true);
            return;
        }

        if (time_PlayerInCriticSitu_Counter <= 0)
            SetPlayerIsInCriticSitu(false);
    }

    void SlowlyUpdatePlayerCriticState()
    {
        if (time_UpdatePlayerInCriticSitu_Counter == 0)
        {
            UpdatePlayerCriticState();
            time_UpdatePlayerInCriticSitu_Counter = time_UpdatePlayerInCriticSitu_Max;
        }
    }

    void SetPlayerIsInCriticSitu(bool _isHe)
    {
        if (_isHe)
        {
            isPlayerInCriticSitu = true;
            ResetTime_PlayerInCriticSitu();
        }
        else
        {
            isPlayerInCriticSitu = false;
        }
    }

    void ResetTime_PlayerInCriticSitu()
    {
        time_PlayerInCriticSitu_Counter = Random.Range(time_PlayerInCriticSitu_Min, time_PlayerInCriticSitu_Max);
    }

    public bool IsPlayerStillInCriticalState()
    {
        if (mapLogic.player == null)
            return false;

        if (!mapLogic.playerCharInfo.IsAttackable())
            return false;

        return isPlayerInCriticSitu;
    }

    public bool ShouldCheckPlayerCritSitu_Generally()
    {
        if (mapLogic.player == null)
            return false;

        if (!mapLogic.playerCharInfo.IsAttackable())
            return false;

        if (mapLogic.playerCharInfo.GetEnemyFightSide() != charInfo.FightSide)
            return false;

        return true;
    }

    public void UpdatePlayerCritic()
    {
        needsToUpdatePlayerCriticSitu = true;
    }

    public void SlowlyUpdatePlayerCritic()
    {
        needsToSlowlyUpdatePlayerCriticSitu = true;
    }

    public void Stop()
    {
        isStopping = true;
        //removingCurActCounter = 0;
    }

    public void Resume()
    {
        gameObject.active = true;

        charController.enabled = true;

        body.SetActiveRecursively(true);

        Renderer[] rends = gameObject.GetComponentsInChildren<Renderer>();

        for (int i = 0; i < rends.Length; i++)
        {
            rends[i].enabled = true;
        }

        if (isStopping)
        {
            isStopping = false;

            Debug.LogError("Soldier resume called while stopping is not finished!");
        }
    }


    public void SetOwnerFightInReg(MapLogicJob_FightInReg _owner)
    {
        owner_FightInReg = _owner;
    }


    public string GentlyGetUnderFootSurfaceMaterial()
    {
        if (gentlyGetUnderFootSurfaceMaterial_TimeCounter == 0)
        {
            gentlyGetUnderFootSurfaceMaterial_TimeCounter = gentlyGetUnderFootSurfaceMaterial_Max;

            return GetUnderFootSurfaceMaterial();
        }

        return lastUnderfootSurfaceMaterial;
    }

    public string GetUnderFootSurfaceMaterial()
    {
        lastUnderfootSurfaceMaterial = "";

        Vector3 startPos = footStepRaycastStartTr.position;
        Vector3 endPos = footStepRaycastEndTr.position;

        Vector3 direction = endPos - startPos;
        float rayMagnitude = direction.magnitude;
        direction.Normalize();
        Ray ray = new Ray(startPos, direction);

        RaycastHit[] hits;

        GameObject sld = gameObject;

        hits = Physics.RaycastAll(ray, rayMagnitude, GameGeneralInfo.Instance.UnderFootSurfaceLayer);

        int i = 0;
        while (i < hits.Length)
        {
            RaycastHit hitInfo = hits[i];

            GameObject hittedObj = hitInfo.collider.transform.root.gameObject;

            if (GameObject.Equals(hittedObj, sld))
            {
                i++;
                continue;
            }

            lastUnderfootSurfaceMaterial = hittedObj.tag.ToLower();
            return lastUnderfootSurfaceMaterial;
        }

        return lastUnderfootSurfaceMaterial;
    }

    public void PlayFootStepSound(string _surfaceMaterial, PlayerFootEnum _foot)
    {
        string surfaceMaterial = _surfaceMaterial;
        PlayerFootEnum foot = _foot;

        AudioClip[] audios = footStepSoundList.GetFootStepAudio(surfaceMaterial, foot);

        footStepsAudioInfo.SetCustomPitch(Random.Range(footStepSoundMinPitch, footStepSoundMaxPitch));
        footStepsAudioInfo.PlayClip_OneShot(audios);
    }


    public void ForceToAttackPlayerForAWhile()
    {
        if (forceAttackPlayerTimeCounter > 0)
            return;

        forceAttackPlayerTimeCounter = forceAttackPlayerMaxTime;

        List<GameObject> nearMates = mapLogic.FindNearMates(gameObject, forceAttackPlayerRadius, FightSideEnum.Enemy);

        for (int i = 0; i < nearMates.Count; i++)
        {
            if (nearMates[i].transform.root.tag.ToLower() == GeneralStats.enemyTagName_ToLower)
            {
                SoldierInfo mateSoldInf = nearMates[i].GetComponent<SoldierInfo>();
                mateSoldInf.ForceToAttackPlayerForAWhile();
            }
        }
    }

    public void ForceToAttackPlayerForAWhile_IfNotMoving()
    {
        if (isMoving)
            return;

        if (forceAttackPlayerTimeCounter > 0)
            return;

        forceAttackPlayerTimeCounter = forceAttackPlayerMaxTime;

        List<GameObject> nearMates = mapLogic.FindNearMates(gameObject, forceAttackPlayerRadius, FightSideEnum.Enemy);

        for (int i = 0; i < nearMates.Count; i++)
        {
            if (nearMates[i].transform.root.tag.ToLower() == GeneralStats.enemyTagName_ToLower)
            {
                SoldierInfo mateSoldInf = nearMates[i].GetComponent<SoldierInfo>();
                mateSoldInf.ForceToAttackPlayerForAWhile_IfNotMoving();
            }
        }
    }

    DeadSoldAnim GetDeadSoldAnimForAnim(string _curRunningAnim, DamageInfo _damageInfo)
    {
        DeadSoldAnim deadSoldAn = null;
        string curRunningAnim = _curRunningAnim;
        DamageInfo damageInfo = _damageInfo;

        DeadSoldEquivAnimInfo deadAnimEquivInfo = FindDeadAnimEquivInfoForCurRunningAnim(curRunningAnim, damageInfo);

        if (deadAnimEquivInfo != null)
        {
            deadSoldAn = new DeadSoldAnim();
            deadSoldAn.animName = deadAnimEquivInfo.deadSoldierAnimsList.GetRandomAnimName();
            deadSoldAn.initialCrossfadeTime = deadAnimEquivInfo.crossfadeTime;

            return deadSoldAn;
        }

        return null;
    }

    DeadSoldEquivAnimInfo FindDeadAnimEquivInfoForCurRunningAnim(string _curRunningAnim, DamageInfo _damageInfo)
    {
        string curRunningAnim = _curRunningAnim;
        DamageInfo damageInfo = _damageInfo;

        foreach (DeadSoldEquivAnimInfo dsequ in soldierGeneralInfo.deadSoldEquivAnimInfos)
        {
            if (damageInfo.bodyPart == SoldierBodyPart.Head && !dsequ.head)
            {
                return null;
            }

            if (damageInfo.bodyPart == SoldierBodyPart.Down && !dsequ.downBody)
            {
                return null;
            }

            if ((damageInfo.bodyPart == SoldierBodyPart.UpBack || damageInfo.bodyPart == SoldierBodyPart.UpFront) && !dsequ.upBody)
            {
                return null;
            }

            foreach (AnimsList anList in dsequ.equivAnimsLists)
            {
                foreach (AnimInfo anInfo in anList.animInfos)
                {
                    if (anInfo.AnimName == curRunningAnim)
                        return dsequ;
                }
            }
        }

        return null;
    }

    public void SetCurrentFightRegLaneNum(int _laneNum)
    {
        curFightRegLaneNum = _laneNum;
    }

    public void SetShouldOnlyTakeDamageFromPlayer(bool _val)
    {
        shouldOnlyTakeDamageFromPlayer = _val;
    }

    public void SetGettingDamageArea(LogicTrigger _logicTrigger)
    {
        gettingDamageArea = _logicTrigger;
    }

    public void SetIsMoving(bool _val)
    {
        isMoving = _val;
    }

    public float GetGroundY()
    {
        Vector3 raycastDirection = new Vector3(0, -1000, 0);
        float rayDirMag = raycastDirection.magnitude;
        float range = rayDirMag;

        Vector3 raycastStart = transform.position + new Vector3(0, charController.height, 0);

        RaycastHit hit;

        if (Physics.Raycast(raycastStart, raycastDirection, out hit, range, GameGeneralInfo.Instance.OnlyGroundLayer))
        {
            return hit.point.y;
        }

        return -1000;
    }


    //

    public bool TryTalk_Agressive()
    {
        if (IsVoiceOnBusyTimer())
            return false;

        if (voiceInfo.Agressive.Length == 0)
            return false;

        PlayVoiceWithAdditionalBusyTime(voiceInfo.Agressive, defaultBusyTime_Min, defaultBusyTime_Max);
        return true;
    }

    public bool TryTalk_Reloading()
    {
        if (IsVoiceOnBusyTimer())
            return false;

        if (voiceInfo.ImReloading.Length == 0)
            return false;

        PlayVoiceWithAdditionalBusyTime(voiceInfo.ImReloading, defaultBusyTime_Min, defaultBusyTime_Max);
        return true;
    }

    public void Talk_Camp_EnemyDetected()
    {
        PlayVoiceWithAdditionalBusyTime(voiceInfo.Camp_EnemyDetected, defaultBusyTime_Min, defaultBusyTime_Max);
    }

    public void ShowChildSkinnedMeshRenderers()
    {
        for (int i = 0; i < childSkinnedMeshRenderers.Length; i++)
        {
            childSkinnedMeshRenderers[i].enabled = true;
        }

        for (int i = 0; i < childMeshRenderers.Length; i++)
        {
            childMeshRenderers[i].enabled = true;
        }
    }

    public void HideChildSkinnedMeshRenderers()
    {
        for (int i = 0; i < childSkinnedMeshRenderers.Length; i++)
        {
            childSkinnedMeshRenderers[i].enabled = false;
        }

        for (int i = 0; i < childMeshRenderers.Length; i++)
        {
            childMeshRenderers[i].enabled = false;
        }
    }
}
