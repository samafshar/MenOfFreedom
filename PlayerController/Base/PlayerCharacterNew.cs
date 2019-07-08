using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HUD_3DObj
{
    public bool isActive = false;
    public Transform source;
    public Vector3 sourcePos;
    public string name;
    public bool showSideFeleshs = true;

    float baseWCoef = 1;
    float wCoef = 1;

    bool shouldBlink = false;
    float blinkAdditionalWCoef = 1;
    float blinkCounter = 0;
    float blinkSpeed = 4;

    public void Start(Transform _source, Vector3 _sourcePos, string _name, bool _showSideFeleshes)
    {
        isActive = true;

        name = _name;
        source = _source;
        sourcePos = _sourcePos;
        showSideFeleshs = _showSideFeleshes;
    }

    public void Run()
    {
        if (source != null)
            sourcePos = source.position;

        if (shouldBlink)
        {
            blinkCounter += blinkSpeed * Time.deltaTime;
            blinkCounter = Mathf.Clamp(blinkCounter, 0, 2);

            wCoef = baseWCoef + Mathf.Sin(blinkCounter * (Mathf.PI / 2)) * blinkAdditionalWCoef;

            if (blinkCounter == 2)
            {
                shouldBlink = false;
                blinkCounter = 0;
                wCoef = baseWCoef;
            }
        }
    }

    public void End()
    {
        isActive = false;
    }

    public void Blink()
    {
        shouldBlink = true;
    }

    public float GetWCoef()
    {
        return wCoef;
    }
}

public class HUD_DamageSide
{
    public bool isActive = false;
    public AnimationCurve alphaAnimCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.17f, 1), new Keyframe(1.5f, 1), new Keyframe(3f, 0));
    public float timeCounter = 0f;
    float maxTime = 4;
    public Transform sourceEnemy;
    public Vector3 sourcePos;
    public float curAlpha = 0;

    public void Restart(Transform _sourceEnemy, Vector3 _sourcePos)
    {
        isActive = true;

        sourceEnemy = _sourceEnemy;
        sourcePos = _sourcePos;
        timeCounter = 0;
    }

    public void Run()
    {
        if (isActive)
        {
            timeCounter += Time.deltaTime;

            curAlpha = alphaAnimCurve.Evaluate(timeCounter);

            if (timeCounter >= maxTime)
            {
                isActive = false;
            }
        }
    }

}

public enum HUDAmmoPickInfoType
{
    GrenadesAdded,
    GrenadesFull,
    GunAmmoAdded,
    GunAmmoFull,
}

public enum SnipeTimeScaleStatus
{
    Off,
    Starting,
    On,
    Ending,
}

public enum MissionFailType
{
    AlliesNotSupported,
    YouLeftFightArea,
    YouAreDetectedByEnemies,
    YouLeftAreaWithoutPlantingDynamites,
    DynamteHasBeenExplodedBeforeCommunicationBreakdown,
    EnemyHeardYourFire,
    EnemySawHisMateNash,
}

public enum PlayerFootEnum
{
    Left,
    Right,
}

public enum PlayerStateEnum
{
    Idle_Init,
    Idle_Update,

    Walk_Init,
    Walk_Update,

    Run_Init,
    Run_Update,

    IdleToRun_Init,
    IdleToRun_Update,

    RunToIdle_Init,
    RunToIdle_Update,

    Fire_Init,
    Fire_Update,

    Reload_Init,
    Reload_Update,

    DundunReload_Start_Init,
    DundunReload_Start_Update,

    DundunReload_Loop_Init,
    DundunReload_Loop_Update,

    DundunReload_End_Init,
    DundunReload_End_Update,

    GunDown_Init,
    GunDown_Update,

    GunUp_Init,
    GunUp_Update,

    IdleToAim_Init,
    IdleToAim_Update,

    AimToIdle_Init,
    AimToIdle_Update,

    Aim_Init,
    Aim_Update,

    AimWalk_Init,
    AimWalk_Update,

    AimFire_Init,
    AimFire_Update,

    Melee_Init,
    Melee_Update,

    GrenadeGunDown_Init,
    GrenadeGunDown_Update,

    GrenadeZamen_Init,
    GrenadeZamen_Update,

    GrenadePartab_Init,
    GrenadePartab_Update,

    GrenadeGunUp_Init,
    GrenadeGunUp_Update,

    Die_Init,
    Die_Update,

    MissionFailedByOutMistake_Init,
    MissionFailedByOutMistake_Update,

    SnipeIdleToAim_Init,
    SnipeIdleToAim_Update,

    SnipeAimToIdle_Init,
    SnipeAimToIdle_Update,

    SnipeAim_Init,
    SnipeAim_Update,

    PickingNewGun_Init,
    PickingNewGun_Update,

    Camp_Idle_Init,
    Camp_Idle_Update,

    Camp_FastWalk_Init,
    Camp_FastWalk_Update,

    Camp_Knife_Init,
    Camp_Knife_Update,

    KnifeGunDown_Init,
    KnifeGunDown_Update,

    KnifeAttack_Init,
    KnifeAttack_Update,

    KnifeGunUp_Init,
    KnifeGunUp_Update,

    FU_Init,
    FU_Update,
}

public enum PlayerHorizMovementStateEnum
{
    NoMove,
    SlowMove,
    NormalMove,
    FastMove,
}

public enum PlayerVertMovementStateEnum
{
    Stand,
    Sit,
    Jump,
    StandToSit_Init,
    StandToSit,
    SitToStand_Init,
    SitToStand,
}

public class MultiDamageController
{
    public float curFrameTime = -1;
    public List<DamageInfo> curFrameAppliedDamages = new List<DamageInfo>();

    public void AddDamage(DamageInfo _dmg)
    {
        DamageInfo dmg = _dmg;

        Refresh();

        if (dmg == null)
            return;

        if (!curFrameAppliedDamages.Contains(dmg))
        {
            curFrameAppliedDamages.Add(dmg);
        }
    }

    public bool IsDamageAppliedBefore(DamageInfo _dmg)
    {
        DamageInfo dmg = _dmg;

        Refresh();

        if (dmg == null)
            return false;

        return curFrameAppliedDamages.Contains(dmg);
    }

    public void Refresh()
    {
        if (curFrameTime != Time.time)
        {
            curFrameTime = Time.time;

            curFrameAppliedDamages.Clear();
        }
    }
}

public class PlayerKeys
{
    public string moveHorizontal = "Horizontal";
    public string moveVertical = "Vertical";
    public string fire = "Fire1";
    public string aim = "Fire2";
    public string jump = "Jump";
    public string mouseX = "Mouse X";
    public string mouseY = "Mouse Y";
    public string sprint = "Sprint";
    public string crouch = "Crouch";
    public string changeGun = "ChangeGun";
    public string grenade = "Grenade";
    public string melee = "Melee";
    public string action = "Action";
    public string reload = "Reload";
    //public string snipeTimeSpeedController = "Fire3";
    public string tab = "Tab";
}

public class PlayerCharacterNew : MonoBehaviour
{

    public static PlayerCharacterNew Instance;

    //

    public bool isCampPlayer = false;

    public PlayerGun gun1;
    public PlayerGun gun2;

    public PlayerGun[] guns;

    public GameObject knifeMeshObject = null;

    public int initialNumOfGrenades = 3;
    public int maxGrenadeCapacity = 4;

    public float snipeMaxSteadyTime = 5;

    public Camera mainCam;
    public Camera fpsCamera;

    public GameObject bulletWhizzLot;

    public float maxNormalMoveSpeed;
    public float maxNormalSideMoveSpeed;
    public float maxFastMoveSpeed;
    public float maxFastSideMoveSpeed;
    public float maxSlowMoveSpeed;
    public float maxSlowSideMoveSpeed;

    public float maxRunningTime = 15;
    public float maxDelayTimeBetweenRuns = 2;
    public float runningRegenCoef = 1.6f;

    public float sittingSpeed = 30;

    public float meleeAttackMaxDelay = 0.2f;
    public float campMeleeAttackMaxDelay = 0.35f;
    public float campMaxTimeBetweenKnifes = 1.5f;

    public float minNormalMovementVelocity = 0.5f;

    public float standHeight = 2f;
    public float sitHeight = 1f;

    public PlayerSounds soundsList;
    public FootStepSoundList footStepSoundList;
    public AudioInfo audioInfo;
    public AudioInfo audioInfo_Act;
    public AudioInfo audioInfo_BulletImpact;
    public AudioInfo audioInfo_SniperHeartBit;
    public AudioInfo audioInfo_FootStep_Right;
    public AudioInfo audioInfo_FootStep_Left;
    public AudioInfo audioInfo_FootStep_Landing;
    public AudioInfo audioInfo_Knife;
    public AudioInfo audioInfo_KnifeHit;

    public GameObject generalInfoObject;

    public HUDBLoodBili hud_DirectionalBlood;
    public GUITexture hud_SnipeCross;

    public Transform movingObjsRoot;
    public Transform camerasRoot;

    public float meleeAttackDamage = 10000;
    public float meleeAttackImpulse = 10;
    public GameObject meleeAttackHelper_Start;
    public GameObject meleeAttackHelper_End;

    public float maxDamageDecreasementCoef = 0.35f;
    public float curHealthCoefForMaxDamageDecreasement = 0.35f;

    public float lowHPCoef = 0.6f;
    public float criticalHPCoef = 0.3f;
    public float criticalHPWhileGettingBackCoef = 0.2f;

    public float normalHPRegen = 5;
    public float criticalHPRegen = 2;
    public float gettingBackFromCriticalHPRegen = 10;
    public float recievingDamageHPRegen = 0.5f;

    public float critical_Time = 5;
    public float gettingBackFromCritical_Time = 3;
    public float recievingDamageHPRegen_Time = 2;

    public float criticalAudioEffectStartingTime = 0.8f;
    public float criticalAudioEffectEndingTime = 3f;

    public float criticalImageEffectStartingTime = 0.3f;
    public float criticalImageEffectEndingTime = 3f;

    public float bulletHitDangImageEffect_StartingTime = 0.07f;
    public float bulletHitDangImageEffect_DurationTime = 0.1f;
    public float bulletHitDangImageEffect_EndingTime = 0.12f;

    public GameObject grenadeHandMeshObject;
    public GameObject grenade;
    public Transform grenadeStartPos;
    public Transform grenadeDirEndPos;
    public float grenadeSpeed = 120;


    public PlayerExplosionCamCurve[] explosionCamCurves;

    public Transform underFootRaycastStartPos;
    public Transform underFootRaycastEndPos;

    public AnimationCurve footStepDelayGraph;

    public ObjectDetector forwardObjectDetector;

    public Transform[] campViewRaysactTargets;

    public GameObject campKnifeMeshObject = null;

    public float campStandMovingSpeedCoef = 1.2f;

    public PlayerCharacterNew campToNormalReferencePlayer;

    public PlayerCharacterNew normalToCampReferencePlayer;

    public ParticleEmitter campRain;

    public float maxDistanceView = 400f;

    public ObjectDetector bottomObjectDetector;

    public Transform soldNashDetectorTr;

    public MeleeAttack_SoldierDetector meleeAttack_SoldierDetector;

    //public Transform leftViewTr;
    //public Transform rightViewTr;

    //

    [HideInInspector]
    public MapLogic mapLogic;

    [HideInInspector]
    public PlayerStateEnum state = PlayerStateEnum.Idle_Init;

    [HideInInspector]
    public PlayerHorizMovementStateEnum horizMovementState = PlayerHorizMovementStateEnum.NoMove;

    [HideInInspector]
    public PlayerVertMovementStateEnum vertMovementState = PlayerVertMovementStateEnum.Stand;

    [HideInInspector]
    public PlayerKeys keys = new PlayerKeys();

    [HideInInspector]
    public InputControllerNew inputController;

    [HideInInspector]
    public bool canRun = true;

    [HideInInspector]
    public bool sprintingOK = false;

    [HideInInspector]
    public bool firingOK = false;

    [HideInInspector]
    public int currentNumOfGrenades = 0;

    [HideInInspector]
    public bool canRefreshSprintKey = true;

    [HideInInspector]
    public bool canRefreshFireKey = true;

    [HideInInspector]
    public float snipeCurLarzeshX = 0;

    [HideInInspector]
    public float snipeCurLarzeshY = 0;

    [HideInInspector]
    public float snipeModeMouseSensitivityReductionCoef = 0.35f;

    [HideInInspector]
    public float snipeModeBulletHitPareshReductionCoef = 0.25f;

    [HideInInspector]
    public float snipeModeExplosionCameraShakeReductionCoef = 0.3f;

    [HideInInspector]
    public bool isOnSnipeMode = false;

    [HideInInspector]
    public bool doSnipeLarzeshing = false;

    [HideInInspector]
    public float movingSnipeX = 0;

    [HideInInspector]
    public float movingSnipeY = 0;

    [HideInInspector]
    public bool doSnipeMovingCamShake = false;

    [HideInInspector]
    public float snipeShootX = 0;

    [HideInInspector]
    public float snipeShootY = 0;

    [HideInInspector]
    public bool doSnipeShootCamShake = false;

    [HideInInspector]
    public float camExplosionShakeY = 0;

    [HideInInspector]
    public float camExplosionShakeZ = 0;

    [HideInInspector]
    public bool isCameraShakingByExplosion = false;

    [HideInInspector]
    public HUDTextures hudTextures;

    //

    float mainAnimRemainingTime;
    float mainAnimCrossfadeTimeCounter = 0;
    float mainAnimTimeCounter = 0;
    float mainAnimLength = 0;

    string mainAnim;

    CharacterMotor characterMotor;

    CharacterController charControl;

    CharacterInfo charInfo;

    GameGeneralInfo generalInfoHandler;

    BulletDirection bulletDirection;

    PlayerRotationX playerRotationX;

    MovingObjsRotation movingObjsRotation;

    PlayerGun curActiveGun = null;

    GameObject curGunMeshObject = null;

    PlayerSnipeGunInfo curActiveGun_SnipeInfo = null;

    AudioLowPassFilter audioLowPassFilter;

    AnimationCurve audioLowPassEffectCurve;

    BlurEffect missionFailBlurEffect;

    PlayerCriticalStateImageEffect criticalScreenImageEffect;

    PlayerExplosionDirtEffect explosionDirtEffect;

    float curFOV;

    float curGunNormalFOV;

    float curGunAimFOV;

    float fovSwitchingSpeed;

    //enum AnimStateEnum
    //{
    //    None,
    //    Idle,
    //    Walk,
    //    Run,
    //    Fire,
    //    Reload,
    //    IdleToRun,
    //    RunToIdle,
    //    IdleToAim,
    //    AimToIdle,
    //    AimIdle,
    //    AimFire,
    //    AimWalk,
    //    Melee,
    //    Grenade,
    //    GunDown,
    //    GunUp,
    //    DundunReload_Start,
    //    DundunReload_Loop,
    //    DundunReload_End,
    //}

    string curAnimName;

    class PlayerAnimations
    {
        public string none = " ";

        public string idle = "idleRelax";
        public string walk = "walk";
        public string run = "run";

        public string fire = "shoot";
        public string reload = "reload";

        public string idleToRun = "idleToRun";
        public string runToIdle = "runToIdle";

        public string idleToAim = "idleToAim";
        public string aimToIdle = "aimToIdle";

        public string aimIdle = "aimIdle";
        public string aimFire = "aimShoot";
        public string aimWalk = "aimWalk";

        public string melee = "melee";

        public string grenadeGunDown = "grenadeGunDown";
        public string grenadeZamen = "grenadeZamen";
        public string grenadePartab = "grenadePartab";
        public string grenadeGunUp = "grenadeGunUp";

        public string gunUp = "gunUp";
        public string gunDown = "gunDown";

        public string dundunReload_Start = "dundunReloadStart";
        public string dundunReload_Loop = "dundunReloadLoop";
        public string dundunReload_End = "dundunReloadEnd";

        public string camp_Idle = "idle";
        public string camp_WalkFast = "walk";
        //public string camp_Knife = "melee";

        public string knifeAttack01 = "knifeAttack01";
        public string knifeAttack02 = "knifeAttack02";
        public string knifeAttack03 = "knifeAttack03";

        public string fu = "FU";
    }

    PlayerAnimations playerAnimationsList = new PlayerAnimations();

    BlurEffect blurEffect;

    bool needsToChangeGun = false;
    bool needsToAim = false;

    float reloadTimeCounter = 0;

    bool didReloadingDone = false;

    bool isZoomingFOV = false;

    bool isUnzoomingFOV = false;

    float maxCFTimeCoef = 0.25f;

    float minEndCFTimeCoef = 0.5f;

    enum PlayerHPStateEnum
    {
        Normal_Init,
        Normal_Update,
        Low_Init,
        Low_Update,
        Critical_Init,
        Critical_Update,
        GettingBackFromCritical_Init,
        GettingBackFromCritical_Update,
    }

    PlayerHPStateEnum hpState = PlayerHPStateEnum.Normal_Init;

    bool isPlayerStopped = false;

    bool isGamePaused = false;

    float bloodAlpha = 0;

    float criticalAudioEffectIntensity = 0;

    float criticalImageEffectIntensity = 0;

    PlayerBloodEffect playerBloodEffect;

    float curHPRegen = 0;

    float criticalHpStateTimeCounter = 0;

    float gettingBackFromCriticalHpStateTimeCounter = 0;

    float recieveDamageHPRegenTimeCounter = 0;

    float additionalCoef = 1;

    float criticalAudioEffectChangeDurationTime = 1;

    bool isIncreasingCriticalAudioEffect = false;
    bool isDecreasingCriticalAudioEffect = false;

    float criticalImageEffectChangeDurationTime = 1;

    bool isIncreasingCriticalImageEffect = false;
    bool isDecreasingCriticalImageEffect = false;

    bool isPhysicsStandToSit = false;
    bool isPhysicsSitToStand = false;

    bool isMovingObjStandToSit = false;
    bool isMovingObjSitToStand = false;

    float currentPosInSitStand = 0f;

    float bulletHitDangImageEffectTimeCounter = 0f;
    int bulletHitDangState = 0;

    float bulletHitDangAlpha = 0;

    PlayerBulletHitImageEffect bulletHitDangImageEffect;

    float curBulletHitChance = 1;
    float bulletHitChanceMaxDuration = 1.1f;
    float bulletHitChanceTimeCounter = 0;
    int bulletHitChanceMaxStacks = 4;
    int bulletHitChanceCurNumOfStacks = 0;
    float bulletHitChance_DecreasementValue = 0.09f;

    bool shouldContinueDundunReload = true;

    float meleeAttackDelayTimeCounter = 0;

    bool didMeleeAttackDone = false;

    bool shouldThrowGrenadeNow = false;


    float throwGrenadeDelayOnPartabAnim = 0.2f;

    float throwGrenadeDelayOnPartabAnim_TimeCounter;

    bool didGrenadeThrown = false;

    MultiDamageController multiDmgCtrl = new MultiDamageController();

    float snipeLarzXSinMot = 0;
    float snipeLarzYSinMot = 0;

    float snipeCurSteadyAimValue = 1;
    float snipeNormalAimToSteadyAimSpeed = 10;

    float snipeSteadyTimeCounter = 0;
    float snipeSteadyTimeCoef = 0.8f;

    bool snipe_IsSteady = false;
    bool snipe_ShadidBreathingAfterSteady = false;

    float snipeMellowBreathingMinNeededTime = 0.5f;
    bool shouldPlaySnipeBreathVoice = false;

    float snipeTimeSpeedDelayTimeCounter = 0;
    float snipeTimeSpeedDelayMaxTime = 5;

    float snipeTimeSpeedTimeCounter = 0;
    float snipeTimeSpeedMaxTime = 2.5f;

    SnipeTimeScaleStatus snipeTimeScaleStat = SnipeTimeScaleStatus.Off;

    float snipeTimeSpeed_ChangeSpeed = 2.5f;

    TimeScaleCoef snipeTimeScaleCoef;

    float snipeTimeScaleCoefMin = 0.25f;

    float snipeBetweenOnAndOffDelayCounter = 0;
    float snipeBetweenOnAndOffDelayMaxTime = 1;

    float snipeAdditionalFireTimeCoefInSlowSpeedMode = 1.9f;

    //

    bool isPlayerMovingWithAimedSnipe = false;

    float movingSnipeXMaxVal = 1.15f;
    float movingSnipeYMaxVal = 0.55f;

    float movingSnipeXSpeed = 6.7f;
    float movingSnipeYSpeed = 6.7f;

    float movingSnipeXFactor = 0;
    float movingSnipeYFactor = 0;

    float stopMovingSnipeSpeed = 6;

    //

    bool isSnipeShooting = false;

    float shootingSnipeXMaxVal = 1.5f;
    float shootingSnipeYMaxVal = 0.6f;

    float shootingSnipeXSpeed = 15.5f;
    float shootingSnipeYSpeed = 12f;

    float shootingSnipeXFactor = 0;
    float shootingSnipeYFactor = 0;

    bool snipeShootingXDone = false;
    bool snipeShootingYDone = false;

    float camExplosionCurrentShakeSheddat = 0;
    float camExplosionInitialSheddat = 0;
    float camExplosionShakeTimeCounter = 0;
    float camExplosionShakeMaxTime = 0.7f;
    float camExplosionShakeMaxY = 5;
    float camExplosionShakeMaxZ = 10;

    PlayerExplosionCamCurve selectedExplosionCamShakeInfo;

    bool shouldPlayLandingSound = false;

    float landingSoundMinNeededTime = 0.3f;
    float landingSoundTimeCounter = 0.3f;

    float gentlyGetUnderFootSurfaceMaterial_TimeCounter = 0;
    float gentlyGetUnderFootSurfaceMaterial_Max = 0.15f;

    string lastUnderfootSurfaceMaterial = "";

    float footStep_MinNeedVelocity = 0.4f;
    float footStep_TimeCounterInitialValue = 0.15f;
    float footStep_TimeCounter = 0.2f;

    int footStep_LastAudioIndex = 0;

    PlayerFootEnum curFoot = PlayerFootEnum.Right;

    float footStepSoundMaxPitch = 1.11f;
    float footStepSoundMinPitch = 0.89f;

    float campLevelDefaultCustomVolume = 0.65f;
    float campLevelSlowCustomVolume = 0.37f;


    float explosionDirtStartMaxTime = 2.5f;
    float explosionDirtFadeSpeed = 0.3f;
    float explosionDirtStartTimeCounter = 0f;
    float curExplosionDirtEffectAlpha = 0;

    bool isOnExplosionDirtEffect = false;

    SkinnedMeshRenderer[] grenadeHandSkinnedRenderers = null;

    float dieBlurEffectBetweenIterationMaxTime = 0.1f;
    float dieBlurEffectBetweenIterationTimeCounter = 0.1f;
    int missionFailBlurEffectMaxIteration = 6;
    int missionFailBlurEffectCurIteration = 0;

    float missionFailBlurEffectMaxIntensity = 3.5f;
    float missionFailBlurEffectCurIntensity = 0;
    float missionFailBlurEffectIntensitySpeed = 0.5f;

    bool isMissionFailBlurStarted = false;

    TimeScaleCoef missionFailTimeScaleCoef;
    float missionFailTimeScaleCoefSpeed = 0.38f;
    float missionFailTimeScaleCoefMin = 0.33f;

    bool isMissionFailTimeScaleStarted = false;

    float nearMaxDistanceToTakeMoreDamageFromSolds = 20f; //12.5f
    float nearMinDistanceToTakeMoreDamageFromSolds = 6f; //4.5f
    float nearDamageMaxCoef = 3.5f; //2.8f

    Compass compass;

    float camp_AnimToCampIdleCFTime = 0.37f;
    float camp_AnimToCampMoveFastCFTime = 0.28f;
    float camp_AnimToCampKnifeCFTime = 0.3f;

    float camp_AttackToCampIdleCFTime = 0.46f;
    bool isCampKnifeAttackedNow = false;

    //



    //<Test> TESSSSSSSSSSSSSSTTTTTTTTTTTTTTTTTTTTTTTT!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    float testCounter = 0.5f;
    //</Test>

    //---------------

    float curAnimCFTime = 0;

    float endCFTime;

    float animCFTime_Default = 0.25f;
    float animCFTime_NoneToIdle = 0.05f;
    float minAnimCFTime = 0.14f;

    float animCFTime_Idle_To_IdleToAim = 0.09f;
    float animCFTime_IdleToAim_To_Aim = 0.09f;
    float animCFTime_IdleToAim_To_Idle = 0.09f;
    float animCFTime_Aim_To_IdleToAim = 0.09f;
    float animCFTime_Walk_To_IdleToAim = 0.09f;

    float forwardObjectDetectionTimeCounter = 0;
    float forwardObjectDetectionDelay = 0.5f;

    Item newGunItemToPick;

    int maxNumOfGuns = 2;

    bool isMissionFailedByOutMistake = false;

    MissionFailType missionFailType;

    float runningTimeCounter = 0;
    float betweenRunsDelayTimeCounter = 0;

    bool runningTimeIncreasedInLastUpdate = false;

    bool needToFadeCross = false;
    bool showHUDCross = true;

    float hudCrossAlpha = 1f;

    float fadeCrossSpeed = 10f;
    float fadeoutCrossSpeed = 3f;

    public HUDCrossTextures crossTextures;

    float hudCrossScreenHeightSize = 0.02f;
    float hudCross_DispersionToScreenCoef = 0.05f;

    float timerForDisEnableVignet = 0f;
    float maxTimeForChangeVignet = 1.5f;

    bool needToChangeVignet = true;
    bool needToDisableVignet = true;
    bool needToEnableVignet = false;

    float bottomObjectDetectorTimeCounter = 0.2f;
    float bottomObjectDetectorMaxTime = 0.2f;

    //HUD

    HUDParent hudParent;

    HUDGroup oldGun_HUDGroup_Bullet;

    HUDGroup currentActiveGun_HUDGroup_Bullet;
    List<HUDControl> currentActiveGun_HUDControls_Bullets = new List<HUDControl>();
    HUDGroup currentActiveGun_HUDGroup_GunShape;
    HUDControl currentActiveGun_HUDControl_GunShape;

    HUDControl hudControl_Grenade;
    HUDControl hudControl_BulletCount_L;
    HUDControl hudControl_BulletCount_R;
    HUDControl hudControl_BulletCount_M;
    HUDControl hudControl_SitStand;

    float hudShowForAWhile_Time = 7.5f;
    float hudShowForAWhile_StartAlphaSpeed = 4;
    float hudShowForAWhile_EndAlphaSpeed = 1;

    float hudGunShape_Time = 3.5f;
    float hudGunShape_StartAlphaSpeed = 6;
    float hudGunShape_EndAlphaSpeed = 2f;

    float hudSitStand_Time = 1.5f;
    float hudSitStand_StartAlphaSpeed = 4;
    float hudSitStand_EndAlphaSpeed = 1f;

    float hudAlphaIncSpeed_Fast = 7;
    float hudAlphaDecSpeed_Fast = 7;

    float hud_MinimapDistCoef = 0.00175f;

    float hud_MinimapSoldierW_ScreenCoef = 0.03f;
    float hud_MinimapObjectiveW_ScreenCoef = 0.04f;


    float hud_Text_DefaultIncAlphaSpeed = 3.5f;
    float hud_Text_DefaultDecAlphaSpeed = 2;

    float hud_Text_FastIncAlphaSpeed = 5.5f;
    float hud_Text_FastDecAlphaSpeed = 3.2f;

    float hud_Text_DefaultDecWHSpeed = 0.2f;

    float hud_YouGetDamaged_GettingBackFromCrit_Coef = 0.6f;

    HUDControl hudCtrl_Text_YouGetDamaged_TakeCover;
    AnimationCurve hudAnimCurve_YouGetDamaged_W_H;

    HUDControl hudCtrl_Reload_NoAmmo;
    AnimationCurve hudAnimCurve_Reload_NoAmmo_W_H;

    float hud_Reload_NoAmmo_MagazineCoef = 0.2f;

    float GUIOldTime = 0;

    bool canShowHUD_PressActKeyToGrabGun = false;
    bool canShowHUD_PressActRefillAmmo = false;
    bool canShowHUD_PressActKeyToDoTheAction = false;

    bool canShowHUD_AmmoGunPickInfo = false;

    PlayerGunName nameOfGrabbableGun = PlayerGunName.Springfield;

    HUDGroup hudGroup_ActKeyRelated;
    HUDControl hudControl_PressActKeyToGrabGun;
    HUDControl hudControl_PressActKeyToRefillAmmo;
    HUDControl hudControl_PressActKeyToDoAct;
    HUDControl hudControl_GrabbableGun;

    HUDControl hudControl_AmmoPickInfo;
    bool canShowHUD_AmmoPickInfo = false;
    float hud_AmmoPickInfo_Duration = 1f;
    float hud_AmmoPickInfo_HideSpeed_Slow = 0.5f;
    float hud_AmmoPickInfo_HideSpeed_Fast = 4;
    AnimationCurve hudAnimCurve_AmmoPickInfo_W_H;

    HUDControl hudControl_PressShiftToHoldBreath;
    HUDControl hudControl_SnipeHoldBreathKey1;
    HUDControl hudControl_SnipeFocusKey1;
    HUDControl hudControl_SnipeCancelFocusKey1;
    //HUDControl hudControl_SnipeHoldBreathKey2;
    HUDControl hudControl_PressMidMouseForFocusMode;
    HUDControl hudControl_PressMidMouseToCancelFocusMode;

    bool hud_ShouldShow_SnipeHint_PressShiftToHoldBreath = false;
    bool hud_ShouldShow_SnipeHint_PressMidMouseForFocusMode = false;
    bool hud_ShouldShow_SnipeHint_PressMidMouseToCancelFocusMode = false;

    HUDControl hudControl_SnipeHint_TimeScaleBar;
    HUDControl hudControl_SnipeHint_TimeScaleBar_Disabled;
    HUDControl hudControl_SnipeHint_TimeScaleBar_Limit;
    HUDControl hudControl_SnipeHint_TimeScaleBar_Icon;

    float hud_SnipeHint_TimeScaleBar_MaxW = 700;

    float hud_SnipeHint_TimeScaleBar_ShowSpeed = 2;
    float hud_SnipeHint_TimeScaleBar_HideSpeed = 2;

    float hud_SnipeHint_TimeScaleBar_FastDecSpeed = 1500;

    AnimationCurve hudAnimCurve_MissionFailAlpha;
    bool hud_ShouldShow_MissionFail = false;

    HUDGroup hudGroup_MissionFail;

    HUDControl hudControl_SelectedMissionFailText;

    HUD_DamageSide[] hud_DamageSides = new HUD_DamageSide[8];

    HUDControl hudControl_DamageSide;

    HUDControl hudControl_GrenadeIcon;
    HUDControl hudControl_GrenadeIconFelesh;

    float hud_GrenadeDetectionRange = 11;
    float hud_GrenadeDetectionMaxDistToShowFullAlpha = 4.5f;
    float hud_GrenadeNeededPassTime = 0.6f;

    float hud_3DObjFelesh_CornerAngleCoef = 0.4f;
    float hud_3DObjFelesh_MarginCoef = 0.9f;
    float hud_3DObjFelesh_InReductionRange_DatCoef = 2f;
    float hud_3DObjFelesh_InReductionRange_DatPow = 2;
    HUDControl hudControl_3DObj_Felesh;

    bool hud_ShouldShow_LvlCampSneakingHints = false;
    int hud_SneakingHint_CurIndex = 0;
    float hud_SneakingHint_ShowSpeed = 2.5f;
    float hud_SneakingHint_HideSpeed = 1.8f;
    float hud_SneakingHint_BetweenHUDsDelayMaxTime = 0.2f;
    float hud_SneakingHint_DurationWhenCompletedMaxTime = 14f;
    float hud_SneakingHint_TimeCounter = 0;
    bool hud_SneakingHint_IsCountingDelayBetweenHintBoxes = false;
    bool hud_SneakingHint_IsShowingHintBoxesDone = false;
    bool hud_SneakingHint_IsShowingAllHintPartsDone = false;
    bool hud_SneakingHint_IsHidingAll = false;
    bool hud_SneakingHint_InitialDelayIsDone = false;
    float hud_SneakingHint_InitialDelayMaxTime = 2;

    HUDGroup hudGroup_SneakingHints;
    HUDGroup hudGroup_SneakingHintsPart2;
    HUDControl hudControl_SneakingHints_LastSentence;

    List<HUD_3DObj> hud_3DObjs = new List<HUD_3DObj>();

    HUDGroup hudGroup_LvlCamp;

    HUDControl hudControl_LvlCamp_ClockBG;
    HUDControl hudControl_LvlCamp_ClockBGRed;
    HUDControl hudControl_LvlCamp_ClockHandle;

    bool hud_ShouldShow_LvlCampClock = false;

    float hud_LvlCamp_ClockMaxTime = 180;
    [HideInInspector]
    public float hud_LvlCamp_ClockTimeCounter = 180;
    float hud_LvlCamp_TimeToStartBlinkingRedClock = 60;
    float hud_LvlCamp_TimeToDoMaxBlinkingSpeedForRedClock = 15;
    float hud_LvlCamp_RedClockBlinkingSpeed_Min = 0.3f;
    float hud_LvlCamp_RedClockBlinkingSpeed_Max = 1f;
    float hud_LvlCamp_RedClockBlinkingAlpha_Min = 0.3f;
    float hud_LvlCamp_RedClockBlinkingAlpha_Max = 1f;
    float hud_LvlCamp_RedClockBlinkTimeCounter = 0;
    float hud_LvlCamp_ClockHideSpeed = 2f;
    AnimationCurve hud_LvlCamp_AnimCurve_RedClock;


    //~HUD

    //<Test>

    //string gunPickGUIString = "";
    //float gunPickGUICounter = 0;
    //float gunPickGUICounterMaxVal = 3;
    //GUIStyle gunPickGUIStyle;

    //</Test>

    //bool showMissionFailedByOutMistakeGUI = false;
    string missionFailByOutMistakeGUIString = "";

    [HideInInspector]
    public bool playerShouldTakeABreathFromRunning = false;
    int runningBreakState = 0;

    float campTimeBetweenKnifesCounter = 0;

    [HideInInspector]
    public bool camp_DidPlayerMakeALoudMovingSound = false;
    [HideInInspector]
    public bool camp_DidPlayerMakeALoudLandingSound = false;

    bool shouldRestartFromLastCheckpoint = false;
    float timeCounterToRestartFromLastCheckpoint = 1.7f;

    bool isPlayerStopForCutscene = false;

    string curKnifeAnimName = "";
    //string curCampKnifeAnimName = "";

    GunBonesForPosAndRot campKnifePosRotBones;
    GunBonesForPosAndRot knifePosRotBones;

    [HideInInspector]
    public Vector3 campKnife_PosBoneInitialLocalPosition;

    [HideInInspector]
    public Quaternion campKnife_RotBoneInitialLocalRotation;


    void Awake()
    {
        Instance = this;

        for (int i = 0; i < hud_DamageSides.Length; i++)
        {
            hud_DamageSides[i] = new HUD_DamageSide();
        }

    }

    void Start()
    {
        hudTextures = GameObject.FindObjectOfType(typeof(HUDTextures)) as HUDTextures;

        HideSnipeHUD();

        mapLogic = MapLogic.Instance;
        mapLogic.AddChar(gameObject);

        hudParent = mapLogic.mapHUDParent;

        hudControl_Grenade = hudParent.GetChildGroupByName(HUDGroupName.Grenade).GetChildControlByName(HUDControlName.GrenadeShape);
        hudControl_BulletCount_L = hudParent.GetChildGroupByName(HUDGroupName.AmmoCount).GetChildControlByName(HUDControlName.AmmoCount_Left);
        hudControl_BulletCount_R = hudParent.GetChildGroupByName(HUDGroupName.AmmoCount).GetChildControlByName(HUDControlName.AmmoCount_Right);
        hudControl_BulletCount_M = hudParent.GetChildGroupByName(HUDGroupName.AmmoCount).GetChildControlByName(HUDControlName.AmmoCount_Mid);
        hudControl_SitStand = hudParent.GetChildGroupByName(HUDGroupName.SitStand).GetChildControlByName(HUDControlName.SitStand);

        hudCtrl_Text_YouGetDamaged_TakeCover = hudParent.GetChildGroupByName(HUDGroupName.Text_YouGetDamagedTakeCover).GetChildControlByName(HUDControlName.Text_YouGetDamagedTakeCover);

        hudAnimCurve_YouGetDamaged_W_H = new AnimationCurve(new Keyframe(0, 1), new Keyframe(0.25f, 1.4f), new Keyframe(0.9f, 1));
        hudAnimCurve_YouGetDamaged_W_H.postWrapMode = WrapMode.Loop;

        hudCtrl_Reload_NoAmmo = hudParent.GetChildGroupByName(HUDGroupName.Text_Reload_NoAmmo).GetChildControlByName(HUDControlName.Text_Reload_NoAmmo);
        hudAnimCurve_Reload_NoAmmo_W_H = new AnimationCurve(new Keyframe(0, 1), new Keyframe(0.55f, 1.2f), new Keyframe(1.1f, 1));
        hudAnimCurve_Reload_NoAmmo_W_H.postWrapMode = WrapMode.Loop;

        hudGroup_ActKeyRelated = hudParent.GetChildGroupByName(HUDGroupName.ActKeyRelated);

        hudControl_PressActKeyToGrabGun = hudGroup_ActKeyRelated.GetChildControlByName(HUDControlName.PressActKeyToGrabGun);
        hudControl_PressActKeyToRefillAmmo = hudGroup_ActKeyRelated.GetChildControlByName(HUDControlName.PressActKeyToRefillAmmo);
        hudControl_PressActKeyToDoAct = hudGroup_ActKeyRelated.GetChildControlByName(HUDControlName.PressActKeyToDoAct);
        hudControl_GrabbableGun = hudGroup_ActKeyRelated.GetChildControlByName(HUDControlName.GrabbableGun);

        hudControl_AmmoPickInfo = hudParent.GetChildGroupByName(HUDGroupName.AmmoPickInfo).GetChildControlByName(HUDControlName.AmmoPickInfo);
        hudAnimCurve_AmmoPickInfo_W_H = new AnimationCurve(new Keyframe(0, 1), new Keyframe(0.25f, 1.2f), new Keyframe(0.5f, 1));
        hudAnimCurve_AmmoPickInfo_W_H.postWrapMode = WrapMode.Once;

        hudControl_PressShiftToHoldBreath = hudParent.GetChildGroupByName(HUDGroupName.SnipeHints).GetChildControlByName(HUDControlName.SnipeHint_PressShiftToHoldBreath);
        hudControl_SnipeHoldBreathKey1 = hudParent.GetChildGroupByName(HUDGroupName.SnipeHints).GetChildControlByName(HUDControlName.SnipeBreath1);
        //hudControl_SnipeHoldBreathKey2 = hudParent.GetChildGroupByName(HUDGroupName.SnipeHints).GetChildControlByName(HUDControlName.SnipeBreath2);
        hudControl_SnipeFocusKey1 = hudParent.GetChildGroupByName(HUDGroupName.SnipeHints).GetChildControlByName(HUDControlName.SnipeFocusKey1);
        hudControl_SnipeCancelFocusKey1 = hudParent.GetChildGroupByName(HUDGroupName.SnipeHints).GetChildControlByName(HUDControlName.SnipeCancelFocusKey1);
        hudControl_PressMidMouseForFocusMode = hudParent.GetChildGroupByName(HUDGroupName.SnipeHints).GetChildControlByName(HUDControlName.SnipeHint_ClickMouseMidToStartFocusMode);
        hudControl_PressMidMouseToCancelFocusMode = hudParent.GetChildGroupByName(HUDGroupName.SnipeHints).GetChildControlByName(HUDControlName.SnipeHint_ClickMouseMidToCancelFocusMode);

        hudControl_SnipeHint_TimeScaleBar = hudParent.GetChildGroupByName(HUDGroupName.SnipeHints).GetChildControlByName(HUDControlName.SnipeHint_TimeScaleBar);
        hudControl_SnipeHint_TimeScaleBar_Disabled = hudParent.GetChildGroupByName(HUDGroupName.SnipeHints).GetChildControlByName(HUDControlName.SnipeHint_TimeScaleBar_Disabled);
        hudControl_SnipeHint_TimeScaleBar_Limit = hudParent.GetChildGroupByName(HUDGroupName.SnipeHints).GetChildControlByName(HUDControlName.SnipeHint_TimeScaleBar_Limit);
        hudControl_SnipeHint_TimeScaleBar_Icon = hudParent.GetChildGroupByName(HUDGroupName.SnipeHints).GetChildControlByName(HUDControlName.SnipeHint_TimeScaleBar_Icon);

        hudGroup_MissionFail = hudParent.GetChildGroupByName(HUDGroupName.MissionFail);
        hudAnimCurve_MissionFailAlpha = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1.1f, 0f), new Keyframe(1.9f, 1));
        hudAnimCurve_MissionFailAlpha.postWrapMode = WrapMode.Once;

        hudControl_DamageSide = hudParent.GetChildGroupByName(HUDGroupName.DamageSide).GetChildControlByName(HUDControlName.DamageSide);

        hudControl_GrenadeIcon = hudParent.GetChildGroupByName(HUDGroupName.GrenadeIcon).GetChildControlByName(HUDControlName.GrenadeIcon);
        hudControl_GrenadeIconFelesh = hudParent.GetChildGroupByName(HUDGroupName.GrenadeIcon).GetChildControlByName(HUDControlName.GrenadeIconFelesh);

        hudControl_3DObj_Felesh = hudParent.GetChildGroupByName(HUDGroupName.the3DObjective).GetChildControlByName(HUDControlName.the3DObjectiveFelesh);

        hudGroup_SneakingHints = hudParent.GetChildGroupByName(HUDGroupName.SneakingHints);
        hudGroup_SneakingHintsPart2 = hudParent.GetChildGroupByName(HUDGroupName.SneakingHintsPart2);

        hudControl_SneakingHints_LastSentence = hudGroup_SneakingHintsPart2.GetChildControlByName(HUDControlName.SneakingHint_EffectEnemies);

        hudGroup_LvlCamp = hudParent.GetChildGroupByName(HUDGroupName.LvlCamp);
        hudControl_LvlCamp_ClockBG = hudGroup_LvlCamp.GetChildControlByName(HUDControlName.LvlCamp_ClockBG);
        hudControl_LvlCamp_ClockBGRed = hudGroup_LvlCamp.GetChildControlByName(HUDControlName.LvlCamp_ClockBGRed);
        hudControl_LvlCamp_ClockHandle = hudGroup_LvlCamp.GetChildControlByName(HUDControlName.LvlCamp_ClockHandle);

        hud_LvlCamp_AnimCurve_RedClock = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1f, 1f), new Keyframe(1f, 1), new Keyframe(1f, 1), new Keyframe(2f, 0));
        hud_LvlCamp_AnimCurve_RedClock.postWrapMode = WrapMode.Loop;

        MakePlayerCommonHUDsVisible();

        //


        knifePosRotBones = knifeMeshObject.GetComponent<GunBonesForPosAndRot>();

        campKnifePosRotBones = campKnifeMeshObject.GetComponent<GunBonesForPosAndRot>();

        if (grenadeHandMeshObject != null)
            grenadeHandSkinnedRenderers = grenadeHandMeshObject.GetComponentsInChildren<SkinnedMeshRenderer>();

        charInfo = GetComponent<CharacterInfo>();

        playerRotationX = GetComponent<PlayerRotationX>();

        movingObjsRotation = GetComponentInChildren<MovingObjsRotation>();

        SetHPState(PlayerHPStateEnum.Normal_Init);

        bulletDirection = GetComponent<BulletDirection>();

        foreach (PlayerGun plGun in guns)
        {
            plGun.Init();

            MakeGunHidden(plGun);
        }

        MakeGrenadeHidden();

        MakeCampKnifeHidden();

        SetCampKnifeHandInitialPosAndRot();

        MakeKnifeHidden();

        SetKnifeHandInitialPosAndRot();

        LocalInit_SetGuns();

        SetNumOfCurrentGrenades(initialNumOfGrenades);

        SetGrenadeHandInitialPosAndRot();

        curAnimName = playerAnimationsList.none;

        characterMotor = GetComponent<CharacterMotor>();

        LocalInit_SetInitialCharMotorSpeeds();

        inputController = GetComponent<InputControllerNew>();

        charControl = GetComponent<CharacterController>();
        charControl.height = standHeight;

        generalInfoHandler = GameGeneralInfo.Instance;
        if (generalInfoHandler == null)
            Debug.LogError("General Info nulle bishour!!!");

        audioLowPassFilter = mainCam.GetComponent<AudioLowPassFilter>();
        audioLowPassEffectCurve = new AnimationCurve(new Keyframe(0f, 20000f), new Keyframe(0.2f, 6000f), new Keyframe(0.4f, 2500f), new Keyframe(0.6f, 1300f), new Keyframe(0.8f, 600f), new Keyframe(1f, 300f));

        missionFailBlurEffect = fpsCamera.GetComponent<BlurEffect>();

        playerBloodEffect = fpsCamera.GetComponent<PlayerBloodEffect>();

        bulletHitDangImageEffect = fpsCamera.GetComponent<PlayerBulletHitImageEffect>();

        criticalScreenImageEffect = fpsCamera.GetComponent<PlayerCriticalStateImageEffect>();

        explosionDirtEffect = fpsCamera.GetComponent<PlayerExplosionDirtEffect>();

        LocalInit_SetInitialState();

        LocalInit_SetInitialCustomVolumes();

        missionFailTimeScaleCoef = new TimeScaleCoef();
        missionFailTimeScaleCoef.coefSource = TimeScaleCoefSource.PlayerMissionFail;
        missionFailTimeScaleCoef.currentValue = 1f;

        GameController.AddTimeScaleCoef(missionFailTimeScaleCoef);

        snipeTimeScaleCoef = new TimeScaleCoef();
        snipeTimeScaleCoef.coefSource = TimeScaleCoefSource.PlayerSnipe;
        snipeTimeScaleCoef.currentValue = 1f;

        GameController.AddTimeScaleCoef(snipeTimeScaleCoef);

        compass = GetComponent<Compass>();

        charInfo.MaxHealth *= GameController.playerInitialHPCoef;
        charInfo.CurrentHealth *= GameController.playerInitialHPCoef;

        timerForDisEnableVignet = maxTimeForChangeVignet;

        if (!isCampPlayer)
        {
            ReSetGunInfoHUD();
        }

        if (isCampPlayer)
        {
            ResetBones(campKnifePosRotBones.BonePosition, campKnifePosRotBones.BoneRotation);
        }
    }

    void Update()
    {
        if (!IsGamePaused())
        {
            DecAnimCounters();

            #region Camp
            if (isCampPlayer)
            {
                campTimeBetweenKnifesCounter = MathfPlus.DecByDeltatimeToZero(campTimeBetweenKnifesCounter);
                camp_DidPlayerMakeALoudLandingSound = false;
                camp_DidPlayerMakeALoudMovingSound = false;
            }
            #endregion

            #region Time Counters

            recieveDamageHPRegenTimeCounter = MathfPlus.DecByDeltatimeToZero(recieveDamageHPRegenTimeCounter);

            criticalHpStateTimeCounter = MathfPlus.DecByDeltatimeToZero(criticalHpStateTimeCounter);

            gettingBackFromCriticalHpStateTimeCounter = MathfPlus.DecByDeltatimeToZero(gettingBackFromCriticalHpStateTimeCounter);

            forwardObjectDetectionTimeCounter = MathfPlus.DecByDeltatimeToZero(forwardObjectDetectionTimeCounter);

            //<Test>
            //gunPickGUICounter = MathfPlus.DecByDeltatimeToZero(gunPickGUICounter);
            //</Test>

            #endregion

            #region Critical Audio Effect

            if (isIncreasingCriticalAudioEffect)
            {
                isDecreasingCriticalAudioEffect = false;

                if (criticalAudioEffectIntensity < 1)
                {
                    SetCriticalAudioEffectIntensity(criticalAudioEffectIntensity + (1 / criticalAudioEffectChangeDurationTime) * Time.deltaTime);
                }
            }

            if (isDecreasingCriticalAudioEffect)
            {
                isIncreasingCriticalAudioEffect = false;

                if (criticalAudioEffectIntensity > 0)
                {
                    SetCriticalAudioEffectIntensity(criticalAudioEffectIntensity - (1 / criticalAudioEffectChangeDurationTime) * Time.deltaTime);
                }
            }

            #endregion

            #region Critical Image Effect

            if (isIncreasingCriticalImageEffect)
            {
                isDecreasingCriticalImageEffect = false;

                if (criticalImageEffectIntensity < 1)
                {
                    SetCriticalImageEffectIntensity(criticalImageEffectIntensity + (1 / criticalImageEffectChangeDurationTime) * Time.deltaTime);
                }
            }

            if (isDecreasingCriticalImageEffect)
            {
                isIncreasingCriticalImageEffect = false;

                if (criticalImageEffectIntensity > 0)
                {
                    SetCriticalImageEffectIntensity(criticalImageEffectIntensity - (1 / criticalImageEffectChangeDurationTime) * Time.deltaTime);
                }
            }

            #endregion

            #region FOV

            if (isZoomingFOV)
            {
                isUnzoomingFOV = false;

                float newFOV = curFOV - fovSwitchingSpeed * Time.deltaTime * (curGunNormalFOV - curGunAimFOV);
                newFOV = Mathf.Clamp(newFOV, curGunAimFOV, curGunNormalFOV);

                if (newFOV == curGunAimFOV)
                    isZoomingFOV = false;

                SetCurrentFOV(newFOV);
            }

            if (isUnzoomingFOV)
            {
                isZoomingFOV = false;

                float newFOV = curFOV + fovSwitchingSpeed * Time.deltaTime * (curGunNormalFOV - curGunAimFOV);
                newFOV = Mathf.Clamp(newFOV, curGunAimFOV, curGunNormalFOV);

                if (newFOV == curGunNormalFOV)
                    isUnzoomingFOV = false;

                SetCurrentFOV(newFOV);
            }

            #endregion

            #region Blood Effect

            float bldAl;

            bldAl = (charInfo.MaxHealth - charInfo.CurrentHealth) / ((1 - criticalHPCoef) * charInfo.MaxHealth);
            bldAl = Mathf.Clamp01(bldAl);

            SetBloodAlpha(bldAl);

            #endregion

            #region HPRegen

            ChangeHealth(curHPRegen * charInfo.MaxHealth * Time.deltaTime);

            #endregion

            #region SitStandMovingObj
            if (isMovingObjStandToSit)
            {
                isMovingObjSitToStand = false;

                float oldC = currentPosInSitStand;

                currentPosInSitStand += Time.deltaTime * sittingSpeed;
                currentPosInSitStand = Mathf.Clamp(currentPosInSitStand, 0, (standHeight - sitHeight) / 2);

                float dif = currentPosInSitStand - oldC;

                if (dif > 0)
                {
                    movingObjsRoot.localPosition = new Vector3(movingObjsRoot.localPosition.x,
                                                               movingObjsRoot.localPosition.y - dif,
                                                               movingObjsRoot.localPosition.z);
                }
                else
                {
                    isMovingObjStandToSit = false;
                }
            }

            if (isMovingObjSitToStand)
            {
                isMovingObjStandToSit = false;

                float oldC = currentPosInSitStand;

                currentPosInSitStand -= Time.deltaTime * sittingSpeed;
                currentPosInSitStand = Mathf.Clamp(currentPosInSitStand, 0, (standHeight - sitHeight) / 2);

                float dif = oldC - currentPosInSitStand;

                if (dif > 0)
                {
                    movingObjsRoot.localPosition = new Vector3(movingObjsRoot.localPosition.x,
                                                               movingObjsRoot.localPosition.y + dif,
                                                               movingObjsRoot.localPosition.z);
                }
                else
                {
                    isMovingObjSitToStand = false;
                }
            }
            #endregion

            #region BulletHitDang

            if (bulletHitDangState == 1) //Start
            {
                SetBulletHitDangAlpha(0);
                bulletHitDangImageEffectTimeCounter = bulletHitDangImageEffect_StartingTime;

                bulletHitDangState = 2;
            }

            if (bulletHitDangState == 2) //Increasing ALpha
            {
                bulletHitDangImageEffectTimeCounter = MathfPlus.DecByDeltatimeToZero(bulletHitDangImageEffectTimeCounter);

                SetBulletHitDangAlpha(1 - (bulletHitDangImageEffectTimeCounter / bulletHitDangImageEffect_StartingTime));

                if (bulletHitDangImageEffectTimeCounter == 0)
                {
                    bulletHitDangState = 3;
                }
            }

            if (bulletHitDangState == 3) //Start Duration
            {
                SetBulletHitDangAlpha(1);
                bulletHitDangImageEffectTimeCounter = bulletHitDangImageEffect_DurationTime;

                bulletHitDangState = 4;
            }

            if (bulletHitDangState == 4) //Duration
            {
                bulletHitDangImageEffectTimeCounter = MathfPlus.DecByDeltatimeToZero(bulletHitDangImageEffectTimeCounter);

                if (bulletHitDangImageEffectTimeCounter == 0)
                {
                    bulletHitDangState = 5;
                }
            }

            if (bulletHitDangState == 5) //Start Ending
            {
                bulletHitDangImageEffectTimeCounter = bulletHitDangImageEffect_EndingTime;

                bulletHitDangState = 6;
            }

            if (bulletHitDangState == 6) //Ending
            {
                bulletHitDangImageEffectTimeCounter = MathfPlus.DecByDeltatimeToZero(bulletHitDangImageEffectTimeCounter);

                SetBulletHitDangAlpha(bulletHitDangImageEffectTimeCounter / bulletHitDangImageEffect_EndingTime);

                if (bulletHitDangImageEffectTimeCounter == 0)
                {
                    bulletHitDangState = 7;
                }
            }

            if (bulletHitDangState == 7) //Finish
            {
                FinishBulletHitDang();
            }

            #endregion

            #region BulletHitChance

            if (bulletHitChanceTimeCounter > 0)
            {
                bulletHitChanceTimeCounter = MathfPlus.DecByDeltatimeToZero(bulletHitChanceTimeCounter);
            }
            else
            {
                SetCurBulletHitChance(1);
                bulletHitChanceCurNumOfStacks = 0;
            }

            #endregion

            #region Snipe

            if (doSnipeLarzeshing)
            {
                snipeLarzXSinMot += curActiveGun_SnipeInfo.SnipeIdleLarzeshXSpeed * Time.deltaTime;
                snipeCurLarzeshX = snipeCurSteadyAimValue * (1 + (snipeSteadyTimeCoef * snipeSteadyTimeCounter)) * (Mathf.Sin(snipeLarzXSinMot) * curActiveGun_SnipeInfo.SnipeIdleLarzeshXMax);

                snipeLarzYSinMot += curActiveGun_SnipeInfo.SnipeIdleLarzeshYSpeed * Time.deltaTime;
                snipeCurLarzeshY = snipeCurSteadyAimValue * (1 + (snipeSteadyTimeCoef * snipeSteadyTimeCounter)) * (Mathf.Sin(snipeLarzYSinMot) * curActiveGun_SnipeInfo.SnipeIdleLarzeshYMax);
            }

            if (isPlayerMovingWithAimedSnipe)
            {
                movingSnipeXFactor += movingSnipeXSpeed * Time.deltaTime;
                movingSnipeX = Mathf.Abs(Mathf.Sin(movingSnipeXFactor)) * movingSnipeXMaxVal;

                movingSnipeYFactor += movingSnipeYSpeed * Time.deltaTime;
                movingSnipeY = Mathf.Sin(movingSnipeYFactor) * movingSnipeYMaxVal;
            }
            else
            {
                movingSnipeXFactor = 0;
                movingSnipeYFactor = 0;

                movingSnipeX = (Mathf.Lerp(movingSnipeX, 0, stopMovingSnipeSpeed * Time.deltaTime));
                movingSnipeY = (Mathf.Lerp(movingSnipeY, 0, stopMovingSnipeSpeed * Time.deltaTime));
            }

            if (isSnipeShooting)
            {
                if (!snipeShootingXDone)
                {
                    shootingSnipeXFactor += shootingSnipeXSpeed * Time.deltaTime;
                    snipeShootX = Mathf.Clamp((Mathf.Sin(shootingSnipeXFactor)) * shootingSnipeXMaxVal, 0, 1000);

                    if (snipeShootX == 0)
                    {
                        snipeShootingXDone = true;
                    }
                }

                if (!snipeShootingYDone)
                {
                    shootingSnipeYFactor += shootingSnipeYSpeed * Time.deltaTime;
                    snipeShootY = Mathf.Clamp((Mathf.Sin(shootingSnipeYFactor)) * shootingSnipeYMaxVal, 0, 1000);

                    if (snipeShootY == 0)
                    {
                        snipeShootingYDone = true;
                    }
                }

                if (snipeShootingXDone && snipeShootingYDone)
                {
                    SetSnipeIsShooting(false);
                }
            }
            else
            {
                shootingSnipeXFactor = 0;
                shootingSnipeYFactor = 0;

                snipeShootX = 0;
                snipeShootY = 0;
            }

            if (snipe_IsSteady)
            {
                PlaySnipeVoice_SteadyHeartBeat();

                SetSnipeSteadyTimeCounter(snipeSteadyTimeCounter + Time.deltaTime);

                SetSnipeCurSteadyAimValue(Mathf.Lerp(snipeCurSteadyAimValue, 0, snipeNormalAimToSteadyAimSpeed * Time.deltaTime));

                //SetSnipeCurSteadyAimValue(snipeCurSteadyAimValue - snipeNormalAimToSteadyAimSpeed * Time.deltaTime);

                if (snipeSteadyTimeCounter == snipeMaxSteadyTime)
                {
                    SetSnipeIsSteady(false);
                    SetSnipeShadidBreathingAfterSteady(true);
                }
            }
            else
            {
                SetSnipeSteadyTimeCounter(snipeSteadyTimeCounter - Time.deltaTime);

                SetSnipeCurSteadyAimValue(Mathf.Lerp(snipeCurSteadyAimValue, 1, (snipeNormalAimToSteadyAimSpeed / 2) * Time.deltaTime));

                //SetSnipeCurSteadyAimValue(snipeCurSteadyAimValue + snipeNormalAimToSteadyAimSpeed * Time.deltaTime);

                if (CanPlaySnipeMellowBreathing())
                {
                    SetShouldPlaySnipeBreathVoice(false);

                    PlaySnipeVoice_NafasNafas_Mellow();
                }
            }

            if (CanPlaySnipeShadidBreathing())
            {
                SetSnipeShadidBreathingAfterSteady(false);

                SetShouldPlaySnipeBreathVoice(false);

                PlaySnipeVoice_NafasNafas_Shadid();
            }
            #endregion

            #region Snipe Time Speed Controller

            snipeBetweenOnAndOffDelayCounter = MathfPlus.DecByDeltatimeToZero(snipeBetweenOnAndOffDelayCounter);

            if (snipeTimeScaleStat == SnipeTimeScaleStatus.Off)
            {
                snipeTimeSpeedDelayTimeCounter = MathfPlus.DecByDeltatimeToZero(snipeTimeSpeedDelayTimeCounter);
            }

            if (snipeTimeScaleStat == SnipeTimeScaleStatus.Starting)
            {
                SetSnipeTimeScaleCoefValue(snipeTimeScaleCoef.currentValue - snipeTimeSpeed_ChangeSpeed * Time.deltaTime);

                if (snipeTimeScaleCoef.currentValue == snipeTimeScaleCoefMin)
                {
                    StartStayingSnipeTimeSpeedController();
                }
            }

            if (snipeTimeScaleStat == SnipeTimeScaleStatus.On)
            {
                snipeTimeSpeedTimeCounter = MathfPlus.DecByDeltatimeToZero(snipeTimeSpeedTimeCounter);

                if (snipeTimeSpeedTimeCounter == 0)
                {
                    StartEndingSnipeTimeSpeedController();
                }
            }

            if (snipeTimeScaleStat == SnipeTimeScaleStatus.Ending)
            {
                SetSnipeTimeScaleCoefValue(snipeTimeScaleCoef.currentValue + snipeTimeSpeed_ChangeSpeed * Time.deltaTime);

                if (snipeTimeScaleCoef.currentValue == 1)
                {
                    EndSnipeTimeSpeedControllerNow();
                }
            }

            #endregion

            #region ExplosionCamShake
            if (isCameraShakingByExplosion)
            {
                camExplosionShakeTimeCounter = MathfPlus.DecByDeltatimeToZero(camExplosionShakeTimeCounter);

                camExplosionCurrentShakeSheddat = (camExplosionShakeTimeCounter / camExplosionShakeMaxTime) * camExplosionInitialSheddat;

                float eval = camExplosionShakeMaxTime - camExplosionShakeTimeCounter;
                float zarib = camExplosionCurrentShakeSheddat;

                camExplosionShakeY = selectedExplosionCamShakeInfo.camCurve_Y.Evaluate(eval) * zarib * camExplosionShakeMaxY;
                camExplosionShakeZ = selectedExplosionCamShakeInfo.camCurve_Z.Evaluate(eval) * zarib * camExplosionShakeMaxZ;

                if (camExplosionShakeTimeCounter == 0)
                {
                    EndCamExplosionShake();
                }
            }
            #endregion

            #region Foot steps and landing sounds

            footStep_TimeCounter = MathfPlus.DecByDeltatimeToZero(footStep_TimeCounter);

            gentlyGetUnderFootSurfaceMaterial_TimeCounter = MathfPlus.DecByDeltatimeToZero(gentlyGetUnderFootSurfaceMaterial_TimeCounter);

            if (IsPlayerGrounded())
            {
                landingSoundTimeCounter = landingSoundMinNeededTime;

                if (shouldPlayLandingSound)
                {
                    shouldPlayLandingSound = false;

                    PlayLandingSound(GetUnderFootSurfaceMaterial());

                    if (isCampPlayer)
                        camp_DidPlayerMakeALoudLandingSound = true;
                }
                else
                {
                    if (ShouldResetFootstepTimeCounter())
                        footStep_TimeCounter = footStep_TimeCounterInitialValue;
                    else
                    {
                        if (ShouldPlayFootstepSound())
                        {
                            float vel = GetHorizVelocity();

                            footStep_TimeCounter = footStepDelayGraph.Evaluate(vel);

                            PlayFootStepSound(GentlyGetUnderFootSurfaceMaterial(), curFoot);

                            if (isCampPlayer)
                            {
                                if (IsPlayerRunning())
                                    camp_DidPlayerMakeALoudMovingSound = true;
                            }

                            if (curFoot == PlayerFootEnum.Right)
                                curFoot = PlayerFootEnum.Left;
                            else
                                curFoot = PlayerFootEnum.Right;
                        }
                    }
                }
            }
            else
            {
                landingSoundTimeCounter = MathfPlus.DecByDeltatimeToZero(landingSoundTimeCounter);

                if (landingSoundTimeCounter == 0)
                    shouldPlayLandingSound = true;
            }
            #endregion

            #region Explosion Dirt Effect
            if (isOnExplosionDirtEffect)
            {
                explosionDirtStartTimeCounter = MathfPlus.DecByDeltatimeToZero(explosionDirtStartTimeCounter);

                if (explosionDirtStartTimeCounter == 0)
                {
                    SetExplosionDirtEffectAlpha(curExplosionDirtEffectAlpha - explosionDirtFadeSpeed * Time.deltaTime);

                    if (curExplosionDirtEffectAlpha == 0)
                        EndExplosionDirtEffect();
                }
            }
            #endregion

            #region Mission fail blur effect
            if (isMissionFailBlurStarted)
            {
                dieBlurEffectBetweenIterationTimeCounter = MathfPlus.DecByDeltatimeToZero(dieBlurEffectBetweenIterationTimeCounter);

                if (dieBlurEffectBetweenIterationTimeCounter == 0)
                {
                    dieBlurEffectBetweenIterationTimeCounter = dieBlurEffectBetweenIterationMaxTime;

                    SetMissionFailBlurEffectIteration(missionFailBlurEffectCurIteration + 1);
                }

                SetMissionFailBlurEffectIntensity(missionFailBlurEffectCurIntensity + missionFailBlurEffectIntensitySpeed * Time.deltaTime);

            }
            #endregion

            #region Mission fail time scale
            if (isMissionFailTimeScaleStarted)
            {
                SetMissionFailTimeScaleCoefValue(missionFailTimeScaleCoef.currentValue - missionFailTimeScaleCoefSpeed * Time.deltaTime);

            }
            #endregion

            #region Compass
            if (compass && compass.enabled)
                compass.SetShouldShowTextures(ShouldShowCompass());
            #endregion

            #region Running

            DecreaseRunningTime();

            if (runningBreakState == 1)
            {
                PlayRunningBreakVoice();
                SetBetweenRunsDelayTime(maxDelayTimeBetweenRuns);

                runningBreakState = 2;
            }

            if (runningBreakState == 2)
            {
                DecreaseBetweenRunsDelayTime();

                if (betweenRunsDelayTimeCounter == 0)
                {
                    SetPlayerBreathingIsDoneForRunning();
                }
            }

            #endregion

            #region RestartFromLastCheckpoint
            if (shouldRestartFromLastCheckpoint)
            {
                timeCounterToRestartFromLastCheckpoint = MathfPlus.DecByDeltatimeToZero(timeCounterToRestartFromLastCheckpoint);

                if (timeCounterToRestartFromLastCheckpoint == 0)
                {
                    shouldRestartFromLastCheckpoint = false;
                    GameController.LoadCurrentLevelLastCheckpoint();
                }
            }
            #endregion

            #region Dispersion For Current Gun
            if (curActiveGun != null)
            {
                curActiveGun.SetPlayerCurMovementSpeed(GetHorizVelocity());

                curActiveGun.SetPlayerIfOnAir(IsPlayerOnAir());
            }
            #endregion

            #region Vinget Test
            if (needToChangeVignet && fpsCamera.GetComponent<Vignetting>())
            {
                timerForDisEnableVignet = MathfPlus.DecByDeltatimeToZero(timerForDisEnableVignet);

                if (timerForDisEnableVignet == 0)
                {
                    if (needToEnableVignet)
                    {
                        fpsCamera.GetComponent<Vignetting>().enabled = true;

                        needToDisableVignet = false;
                        needToEnableVignet = false;
                        needToChangeVignet = false;
                    }

                    if (needToDisableVignet)
                    {
                        fpsCamera.GetComponent<Vignetting>().enabled = false;

                        needToDisableVignet = false;
                        needToEnableVignet = true;

                        timerForDisEnableVignet = maxTimeForChangeVignet;
                    }
                }
            }
            #endregion

            #region BottomObjectDetector
            bottomObjectDetectorTimeCounter = MathfPlus.DecByDeltatimeToZero(bottomObjectDetectorTimeCounter);

            if (bottomObjectDetectorTimeCounter == 0)
            {
                bottomObjectDetectorTimeCounter = bottomObjectDetectorMaxTime;

                if (!isCampPlayer)
                    CheckBottomObjectDetector();
            }
            #endregion

            #region HUD

            #region Pressing Tab = Objs Page
            if (CanShowHUD_ObjectivesPage())
            {
                mapLogic.hudGroup_ObjectivesPage.SetVisibilityOfAllChilds(true);
                mapLogic.hudGroup_ObjectivesPageBG.SetVisibilityOfAllChilds(true);

                mapLogic.isShowingObjectivesPageByHoldingTabKeyByPlayer = true;
            }
            else
            {
                mapLogic.hudGroup_ObjectivesPage.SetVisibilityOfAllChilds(false);
                mapLogic.hudGroup_ObjectivesPageBG.SetVisibilityOfAllChilds(false);

                mapLogic.isShowingObjectivesPageByHoldingTabKeyByPlayer = false;
            }
            #endregion

            #region HUD_YouGetDamaged_TakeCover
            if (CanShowHUD_YouGetDamaged_TakeCover())
            {

                #region Start
                if (hudCtrl_Text_YouGetDamaged_TakeCover.IsOutStep(HUDOutStep.NotStarted))
                {
                    hudCtrl_Text_YouGetDamaged_TakeCover.SetIsVisible(true);
                    hudCtrl_Text_YouGetDamaged_TakeCover.StartIncreasingAlpha(hud_Text_DefaultIncAlphaSpeed);
                    hudCtrl_Text_YouGetDamaged_TakeCover.SetOutCounter(0);


                    hudCtrl_Text_YouGetDamaged_TakeCover.SetOutStep(HUDOutStep.Starting);
                }
                #endregion

                #region Starting
                if (hudCtrl_Text_YouGetDamaged_TakeCover.IsOutStep(HUDOutStep.Starting))
                {
                    if (hudCtrl_Text_YouGetDamaged_TakeCover.alphaStatus == HUDAlphaStat.Full)
                    {
                        hudCtrl_Text_YouGetDamaged_TakeCover.SetOutStep(HUDOutStep.RunningA);
                    }
                }
                #endregion

                #region RunningA
                if (hudCtrl_Text_YouGetDamaged_TakeCover.IsOutStep(HUDOutStep.RunningA))
                {
                    hudCtrl_Text_YouGetDamaged_TakeCover.IncOutCounterByTime();

                    hudCtrl_Text_YouGetDamaged_TakeCover.wCoef = hudAnimCurve_YouGetDamaged_W_H.Evaluate(hudCtrl_Text_YouGetDamaged_TakeCover.outCounter);
                    hudCtrl_Text_YouGetDamaged_TakeCover.hCoef = hudAnimCurve_YouGetDamaged_W_H.Evaluate(hudCtrl_Text_YouGetDamaged_TakeCover.outCounter);

                    hudCtrl_Text_YouGetDamaged_TakeCover.ReInitRect();
                }
                #endregion
            }
            else
            {
                hudCtrl_Text_YouGetDamaged_TakeCover.SetOutStep(HUDOutStep.NotStarted);

                if (hudCtrl_Text_YouGetDamaged_TakeCover.isControlVisible)
                {
                    hudCtrl_Text_YouGetDamaged_TakeCover.SetAlpha(hudCtrl_Text_YouGetDamaged_TakeCover.alpha - hud_Text_FastDecAlphaSpeed * Time.deltaTime);

                    hudCtrl_Text_YouGetDamaged_TakeCover.wCoef -= hud_Text_DefaultDecWHSpeed * Time.deltaTime;
                    hudCtrl_Text_YouGetDamaged_TakeCover.wCoef = Mathf.Clamp(hudCtrl_Text_YouGetDamaged_TakeCover.wCoef, 1, float.MaxValue);

                    hudCtrl_Text_YouGetDamaged_TakeCover.hCoef -= hud_Text_DefaultDecWHSpeed * Time.deltaTime;
                    hudCtrl_Text_YouGetDamaged_TakeCover.hCoef = Mathf.Clamp(hudCtrl_Text_YouGetDamaged_TakeCover.hCoef, 1, float.MaxValue);

                    hudCtrl_Text_YouGetDamaged_TakeCover.ReInitRect();

                    if (hudCtrl_Text_YouGetDamaged_TakeCover.alphaStatus == HUDAlphaStat.Zero)
                    {
                        hudCtrl_Text_YouGetDamaged_TakeCover.SetIsVisible(false);

                        hudCtrl_Text_YouGetDamaged_TakeCover.wCoef = 1;
                        hudCtrl_Text_YouGetDamaged_TakeCover.hCoef = 1;

                        hudCtrl_Text_YouGetDamaged_TakeCover.ReInitRect();

                        hudCtrl_Text_YouGetDamaged_TakeCover.SetOutCounter(0);
                    }
                }
            }
            #endregion

            #region HUD_Reload_NoAmmo
            if (CanShowHUD_Reload_NoAmmo())
            {
                #region Start
                if (hudCtrl_Reload_NoAmmo.IsOutStep(HUDOutStep.NotStarted))
                {
                    hudCtrl_Reload_NoAmmo.SetIsVisible(true);
                    hudCtrl_Reload_NoAmmo.SetAlpha(1);
                    hudCtrl_Reload_NoAmmo.SetOutCounter(0);

                    hudCtrl_Reload_NoAmmo.SetOutStep(HUDOutStep.RunningA);
                }
                #endregion

                #region RunningA
                if (hudCtrl_Reload_NoAmmo.IsOutStep(HUDOutStep.RunningA))
                {
                    if ((curActiveGun.GetCurrentBulletCount() + curActiveGun.GetCurrentMagazineCount()) == 0)
                        hudCtrl_Reload_NoAmmo.SetSelectedTextureIndex(1);
                    else
                        hudCtrl_Reload_NoAmmo.SetSelectedTextureIndex(0);

                    hudCtrl_Reload_NoAmmo.IncOutCounterByTime();

                    hudCtrl_Reload_NoAmmo.wCoef = hudAnimCurve_Reload_NoAmmo_W_H.Evaluate(hudCtrl_Reload_NoAmmo.outCounter);
                    hudCtrl_Reload_NoAmmo.hCoef = hudAnimCurve_Reload_NoAmmo_W_H.Evaluate(hudCtrl_Reload_NoAmmo.outCounter);

                    hudCtrl_Reload_NoAmmo.ReInitRect();
                }
                #endregion
            }
            else
            {
                hudCtrl_Reload_NoAmmo.SetOutStep(HUDOutStep.NotStarted);

                if (hudCtrl_Reload_NoAmmo.isControlVisible)
                {
                    hudCtrl_Reload_NoAmmo.SetAlpha(hudCtrl_Reload_NoAmmo.alpha - hud_Text_FastDecAlphaSpeed * Time.deltaTime);

                    hudCtrl_Reload_NoAmmo.wCoef -= hud_Text_DefaultDecWHSpeed * Time.deltaTime;
                    hudCtrl_Reload_NoAmmo.wCoef = Mathf.Clamp(hudCtrl_Reload_NoAmmo.wCoef, 1, float.MaxValue);

                    hudCtrl_Reload_NoAmmo.hCoef -= hud_Text_DefaultDecWHSpeed * Time.deltaTime;
                    hudCtrl_Reload_NoAmmo.hCoef = Mathf.Clamp(hudCtrl_Reload_NoAmmo.hCoef, 1, float.MaxValue);

                    hudCtrl_Reload_NoAmmo.ReInitRect();

                    if (hudCtrl_Reload_NoAmmo.alphaStatus == HUDAlphaStat.Zero)
                    {
                        hudCtrl_Reload_NoAmmo.SetIsVisible(false);

                        hudCtrl_Reload_NoAmmo.wCoef = 1;
                        hudCtrl_Reload_NoAmmo.hCoef = 1;

                        hudCtrl_Reload_NoAmmo.ReInitRect();

                        hudCtrl_Reload_NoAmmo.SetOutCounter(0);
                    }
                }
            }
            #endregion

            #region HUD_ActKeyRelated
            CheckAndInitActionKeyHUD();

            bool isActKeyRelatedHUDShown = false;

            #region HUD_PressActKeyToDoTheAction
            if (!isActKeyRelatedHUDShown && CanShowHUD_PressActKeyToDoTheAction())
            {
                hudControl_PressActKeyToDoAct.SetAlpha(1);
                hudControl_PressActKeyToDoAct.SetIsVisible(true);

                isActKeyRelatedHUDShown = true;
            }
            else
            {
                hudControl_PressActKeyToDoAct.SetAlpha(0);
                hudControl_PressActKeyToDoAct.SetIsVisible(false);
            }
            #endregion

            #region HUD_PressActKeyToGrabGun
            if (!isActKeyRelatedHUDShown && CanShowHUD_PressActKeyToGrabGun())
            {
                hudControl_PressActKeyToGrabGun.SetAlpha(1);
                hudControl_PressActKeyToGrabGun.SetIsVisible(true);

                hudControl_GrabbableGun.SetAlpha(1);
                hudControl_GrabbableGun.SetIsVisible(true);

                isActKeyRelatedHUDShown = true;
            }
            else
            {
                hudControl_PressActKeyToGrabGun.SetAlpha(0);
                hudControl_PressActKeyToGrabGun.SetIsVisible(false);

                hudControl_GrabbableGun.SetAlpha(0);
                hudControl_GrabbableGun.SetIsVisible(false);
            }
            #endregion

            #region HUD_PressActKeyToRefillAmmo
            if (!isActKeyRelatedHUDShown && CanShowHUD_PressActKeyToRefillAmmo())
            {
                hudControl_PressActKeyToRefillAmmo.SetAlpha(1);
                hudControl_PressActKeyToRefillAmmo.SetIsVisible(true);

                isActKeyRelatedHUDShown = true;
            }
            else
            {
                hudControl_PressActKeyToRefillAmmo.SetAlpha(0);
                hudControl_PressActKeyToRefillAmmo.SetIsVisible(false);
            }
            #endregion

            #endregion

            #region HUD_AmmoPickInfo
            if (CanShowHUD_AmmoPickInfo())
            {
                #region Starting
                if (hudControl_AmmoPickInfo.IsOutStep(HUDOutStep.Starting))
                {
                    hudControl_AmmoPickInfo.SetIsVisible(true);
                    hudControl_AmmoPickInfo.SetAlpha(1);
                    hudControl_AmmoPickInfo.SetOutCounter(0);

                    hudControl_AmmoPickInfo.SetOutStep(HUDOutStep.RunningA);
                }
                #endregion

                #region RunningA
                if (hudControl_AmmoPickInfo.IsOutStep(HUDOutStep.RunningA))
                {
                    hudControl_AmmoPickInfo.IncOutCounterByTime();

                    hudControl_AmmoPickInfo.wCoef = hudAnimCurve_AmmoPickInfo_W_H.Evaluate(hudControl_AmmoPickInfo.outCounter);
                    hudControl_AmmoPickInfo.hCoef = hudAnimCurve_AmmoPickInfo_W_H.Evaluate(hudControl_AmmoPickInfo.outCounter);

                    hudControl_AmmoPickInfo.ReInitRect();

                    if (hudControl_AmmoPickInfo.outCounter >= hud_AmmoPickInfo_Duration)
                    {
                        hudControl_AmmoPickInfo.SetOutStep(HUDOutStep.Finishing);
                    }
                }
                #endregion

                #region Finishing
                if (hudControl_AmmoPickInfo.IsOutStep(HUDOutStep.Finishing))
                {
                    hudControl_AmmoPickInfo.SetAlpha(hudControl_AmmoPickInfo.alpha - hud_AmmoPickInfo_HideSpeed_Slow * Time.deltaTime);

                    if (hudControl_AmmoPickInfo.alphaStatus == HUDAlphaStat.Zero)
                    {
                        hudControl_AmmoPickInfo.SetOutStep(HUDOutStep.Finished);

                        canShowHUD_AmmoPickInfo = false;

                        hudControl_AmmoPickInfo.SetIsVisible(false);
                        hudControl_AmmoPickInfo.SetAlpha(0);

                        hudControl_AmmoPickInfo.wCoef = 1;
                        hudControl_AmmoPickInfo.hCoef = 1;

                        hudControl_AmmoPickInfo.ReInitRect();

                        hudControl_AmmoPickInfo.SetOutCounter(0);
                    }
                }
                #endregion
            }
            else
            {
                if (hudControl_AmmoPickInfo.isControlVisible)
                {
                    hudControl_AmmoPickInfo.SetAlpha(hudControl_AmmoPickInfo.alpha - hud_AmmoPickInfo_HideSpeed_Fast * Time.deltaTime);

                    hudControl_AmmoPickInfo.wCoef -= hud_Text_DefaultDecWHSpeed * Time.deltaTime;
                    hudControl_AmmoPickInfo.wCoef = Mathf.Clamp(hudControl_AmmoPickInfo.wCoef, 1, float.MaxValue);

                    hudControl_AmmoPickInfo.hCoef -= hud_Text_DefaultDecWHSpeed * Time.deltaTime;
                    hudControl_AmmoPickInfo.hCoef = Mathf.Clamp(hudControl_AmmoPickInfo.hCoef, 1, float.MaxValue);

                    hudControl_AmmoPickInfo.ReInitRect();

                    if (hudControl_AmmoPickInfo.alphaStatus == HUDAlphaStat.Zero)
                    {
                        hudControl_AmmoPickInfo.SetOutStep(HUDOutStep.Finished);

                        canShowHUD_AmmoPickInfo = false;

                        hudControl_AmmoPickInfo.SetIsVisible(false);
                        hudControl_AmmoPickInfo.SetAlpha(0);

                        hudControl_AmmoPickInfo.wCoef = 1;
                        hudControl_AmmoPickInfo.hCoef = 1;

                        hudControl_AmmoPickInfo.ReInitRect();

                        hudControl_AmmoPickInfo.SetOutCounter(0);
                    }
                }
            }
            #endregion

            #region HUD_SnipeHint_PressShiftToHoldBreath

            if (snipeSteadyTimeCounter == 0)
                hud_ShouldShow_SnipeHint_PressShiftToHoldBreath = true;
            else
                hud_ShouldShow_SnipeHint_PressShiftToHoldBreath = false;

            if (CanShowHUD_SnipeHint_PressShiftToHoldBreath())
            {
                hudControl_PressShiftToHoldBreath.SetIsVisible(true);
                hudControl_PressShiftToHoldBreath.SetAlpha(1);

                hudControl_SnipeHoldBreathKey1.SetIsVisible(true);
                hudControl_SnipeHoldBreathKey1.SetAlpha(1);
                KeyCode keyCode = CustomInputManager.keys.Sprint_SnipeSteady.primary;
                KeyPropsInfo keyPropsInf = mapLogic.ingameMenu.allKeyProps.GetKeyPropsInfoByKeyCode(keyCode);
                hudControl_SnipeHoldBreathKey1.textures[0] = keyPropsInf.texture_Normal;

                //hudControl_SnipeHoldBreathKey2.SetIsVisible(true);
                //hudControl_SnipeHoldBreathKey2.SetAlpha(1);
                //keyCode = CustomInputManager.keys.Sprint_SnipeSteady.secondary;
                //keyPropsInf = mapLogic.ingameMenu.allKeyProps.GetKeyPropsInfoByKeyCode(keyCode);
                //hudControl_SnipeHoldBreathKey2.textures[0] = keyPropsInf.texture_Normal;
            }
            else
            {
                hudControl_PressShiftToHoldBreath.SetIsVisible(false);
                hudControl_PressShiftToHoldBreath.SetAlpha(0);
                hudControl_SnipeHoldBreathKey1.SetIsVisible(false);
                hudControl_SnipeHoldBreathKey1.SetAlpha(0);
                //hudControl_SnipeHoldBreathKey2.SetIsVisible(false);
                //hudControl_SnipeHoldBreathKey2.SetAlpha(0);
            }
            #endregion

            #region HUD_SnipeHint_PressMidMouseForFocusMode

            if (snipeTimeSpeedDelayTimeCounter == 0)
                hud_ShouldShow_SnipeHint_PressMidMouseForFocusMode = true;
            else
                hud_ShouldShow_SnipeHint_PressMidMouseForFocusMode = false;

            if (CanShowHUD_SnipeHint_PressMidMouseForFocusMode())
            {
                hudControl_PressMidMouseForFocusMode.SetIsVisible(true);
                hudControl_PressMidMouseForFocusMode.SetAlpha(1);

                hudControl_SnipeFocusKey1.SetIsVisible(true);
                hudControl_SnipeFocusKey1.SetAlpha(1);
                KeyCode keyCode = CustomInputManager.keys.Grenade_SnipeTimeController.primary;
                KeyPropsInfo keyPropsInf = mapLogic.ingameMenu.allKeyProps.GetKeyPropsInfoByKeyCode(keyCode);
                hudControl_SnipeFocusKey1.textures[0] = keyPropsInf.texture_Normal;
            }
            else
            {
                hudControl_PressMidMouseForFocusMode.SetIsVisible(false);
                hudControl_PressMidMouseForFocusMode.SetAlpha(0);

                hudControl_SnipeFocusKey1.SetIsVisible(false);
                hudControl_SnipeFocusKey1.SetAlpha(0);
            }

            #endregion

            #region HUD_SnipeHint_PressMidMouseToCancelFocusMode

            if (snipeTimeScaleStat == SnipeTimeScaleStatus.On && snipeBetweenOnAndOffDelayCounter == 0)
                hud_ShouldShow_SnipeHint_PressMidMouseToCancelFocusMode = true;
            else
                hud_ShouldShow_SnipeHint_PressMidMouseToCancelFocusMode = false;

            if (CanShowHUD_SnipeHint_PressMidMouseToCancelFocusMode())
            {
                hudControl_PressMidMouseToCancelFocusMode.SetIsVisible(true);
                hudControl_PressMidMouseToCancelFocusMode.SetAlpha(1);

                hudControl_SnipeCancelFocusKey1.SetIsVisible(true);
                hudControl_SnipeCancelFocusKey1.SetAlpha(1);
                KeyCode keyCode = CustomInputManager.keys.Grenade_SnipeTimeController.primary;
                KeyPropsInfo keyPropsInf = mapLogic.ingameMenu.allKeyProps.GetKeyPropsInfoByKeyCode(keyCode);
                hudControl_SnipeCancelFocusKey1.textures[0] = keyPropsInf.texture_Normal;
            }
            else
            {
                hudControl_PressMidMouseToCancelFocusMode.SetIsVisible(false);
                hudControl_PressMidMouseToCancelFocusMode.SetAlpha(0);

                hudControl_SnipeCancelFocusKey1.SetIsVisible(false);
                hudControl_SnipeCancelFocusKey1.SetAlpha(0);
            }

            #endregion

            #region HUD_SnipeHint_TimeScaleBars
            if (CanShowHUD_SnipeHint_TimeScaleBar())
            {
                hudControl_SnipeHint_TimeScaleBar.SetIsVisible(true);
                hudControl_SnipeHint_TimeScaleBar_Disabled.SetIsVisible(true);
                hudControl_SnipeHint_TimeScaleBar_Limit.SetIsVisible(true);
                hudControl_SnipeHint_TimeScaleBar_Icon.SetIsVisible(true);

                #region Off
                if (snipeTimeScaleStat == SnipeTimeScaleStatus.Off)
                {
                    hudControl_SnipeHint_TimeScaleBar_Limit.SetAlpha(0);

                    hudControl_SnipeHint_TimeScaleBar_Icon.SetAlpha(hudControl_SnipeHint_TimeScaleBar_Icon.alpha - hud_SnipeHint_TimeScaleBar_HideSpeed * Time.deltaTime);

                    if (snipeTimeSpeedDelayTimeCounter > 0)
                    {
                        hudControl_SnipeHint_TimeScaleBar.SetAlpha(hudControl_SnipeHint_TimeScaleBar.alpha - hud_SnipeHint_TimeScaleBar_HideSpeed * Time.deltaTime);

                        hudControl_SnipeHint_TimeScaleBar_Disabled.SetAlpha(hudControl_SnipeHint_TimeScaleBar_Disabled.alpha + hud_SnipeHint_TimeScaleBar_ShowSpeed * Time.deltaTime);
                        hudControl_SnipeHint_TimeScaleBar_Disabled.w = ((snipeTimeSpeedDelayMaxTime - snipeTimeSpeedDelayTimeCounter) / snipeTimeSpeedDelayMaxTime) * hud_SnipeHint_TimeScaleBar_MaxW;
                        hudControl_SnipeHint_TimeScaleBar_Disabled.ReInitRect();
                    }
                    else
                    {
                        hudControl_SnipeHint_TimeScaleBar.SetAlpha(hudControl_SnipeHint_TimeScaleBar.alpha + hud_SnipeHint_TimeScaleBar_ShowSpeed * Time.deltaTime);
                        hudControl_SnipeHint_TimeScaleBar.w = hud_SnipeHint_TimeScaleBar_MaxW;
                        hudControl_SnipeHint_TimeScaleBar.ReInitRect();

                        hudControl_SnipeHint_TimeScaleBar_Disabled.SetAlpha(hudControl_SnipeHint_TimeScaleBar_Disabled.alpha - hud_SnipeHint_TimeScaleBar_HideSpeed * Time.deltaTime);
                    }
                }
                else
                {
                    hudControl_SnipeHint_TimeScaleBar_Disabled.SetAlpha(hudControl_SnipeHint_TimeScaleBar_Disabled.alpha - hud_SnipeHint_TimeScaleBar_HideSpeed * Time.deltaTime);
                }
                #endregion

                #region Starting
                if (snipeTimeScaleStat == SnipeTimeScaleStatus.Starting)
                {
                    hudControl_SnipeHint_TimeScaleBar_Limit.SetAlpha(hudControl_SnipeHint_TimeScaleBar_Limit.alpha + hud_SnipeHint_TimeScaleBar_ShowSpeed * Time.deltaTime);

                    hudControl_SnipeHint_TimeScaleBar_Icon.SetAlpha(hudControl_SnipeHint_TimeScaleBar_Icon.alpha + hud_SnipeHint_TimeScaleBar_ShowSpeed * Time.deltaTime);

                    hudControl_SnipeHint_TimeScaleBar.SetAlpha(1);

                    hudControl_SnipeHint_TimeScaleBar_Limit.SetAlpha(1);

                    hudControl_SnipeHint_TimeScaleBar.w = hud_SnipeHint_TimeScaleBar_MaxW;
                    hudControl_SnipeHint_TimeScaleBar.ReInitRect();

                }
                #endregion

                #region On
                if (snipeTimeScaleStat == SnipeTimeScaleStatus.On)
                {
                    hudControl_SnipeHint_TimeScaleBar.SetAlpha(1);

                    hudControl_SnipeHint_TimeScaleBar_Icon.SetAlpha(hudControl_SnipeHint_TimeScaleBar_Icon.alpha + hud_SnipeHint_TimeScaleBar_ShowSpeed * Time.deltaTime);


                    if (snipeTimeSpeedTimeCounter > 0)
                    {
                        hudControl_SnipeHint_TimeScaleBar.w = ((snipeTimeSpeedTimeCounter / snipeTimeSpeedMaxTime) * hud_SnipeHint_TimeScaleBar_MaxW);
                        hudControl_SnipeHint_TimeScaleBar.ReInitRect();
                    }
                    else
                    {
                        hudControl_SnipeHint_TimeScaleBar.w = 0;
                        hudControl_SnipeHint_TimeScaleBar.ReInitRect();
                    }

                    if (snipeBetweenOnAndOffDelayCounter == 0)
                    {
                        hudControl_SnipeHint_TimeScaleBar_Limit.SetAlpha(hudControl_SnipeHint_TimeScaleBar_Limit.alpha - hud_SnipeHint_TimeScaleBar_HideSpeed * Time.deltaTime);
                    }
                }
                #endregion

                #region Ending
                if (snipeTimeScaleStat == SnipeTimeScaleStatus.Ending)
                {
                    hudControl_SnipeHint_TimeScaleBar_Icon.SetAlpha(hudControl_SnipeHint_TimeScaleBar_Icon.alpha - hud_SnipeHint_TimeScaleBar_HideSpeed * Time.deltaTime);

                    hudControl_SnipeHint_TimeScaleBar_Limit.SetAlpha(0);

                    hudControl_SnipeHint_TimeScaleBar.SetAlpha(hudControl_SnipeHint_TimeScaleBar.alpha - hud_SnipeHint_TimeScaleBar_HideSpeed * Time.deltaTime);

                    hudControl_SnipeHint_TimeScaleBar.w = hudControl_SnipeHint_TimeScaleBar.w - hud_SnipeHint_TimeScaleBar_FastDecSpeed * Time.deltaTime;
                    hudControl_SnipeHint_TimeScaleBar.w = Mathf.Clamp(hudControl_SnipeHint_TimeScaleBar.w, 0, float.MaxValue);
                    hudControl_SnipeHint_TimeScaleBar.ReInitRect();
                }
                #endregion
            }
            else
            {
                hudControl_SnipeHint_TimeScaleBar.SetIsVisible(false);
                hudControl_SnipeHint_TimeScaleBar.SetAlpha(0);

                hudControl_SnipeHint_TimeScaleBar_Disabled.SetIsVisible(false);
                hudControl_SnipeHint_TimeScaleBar_Disabled.SetAlpha(0);

                hudControl_SnipeHint_TimeScaleBar_Limit.SetIsVisible(false);
                hudControl_SnipeHint_TimeScaleBar_Disabled.SetAlpha(0);

                hudControl_SnipeHint_TimeScaleBar_Icon.SetIsVisible(false);
                hudControl_SnipeHint_TimeScaleBar_Icon.SetAlpha(0);
            }
            #endregion

            #region HUD_ShouldShow_MissionFail
            if (hud_ShouldShow_MissionFail)
            {
                hudControl_SelectedMissionFailText.IncOutCounterByTime();

                hudControl_SelectedMissionFailText.SetAlpha(hudAnimCurve_MissionFailAlpha.Evaluate(hudControl_SelectedMissionFailText.outCounter));
            }
            #endregion

            #region HUD_DamageSides
            for (int i = 0; i < hud_DamageSides.Length; i++)
            {
                HUD_DamageSide dmgSide = hud_DamageSides[i];

                if (dmgSide.isActive)
                    dmgSide.Run();
            }
            #endregion

            #region HUD_3DObjs
            for (int i = 0; i < hud_3DObjs.Count; i++)
            {
                HUD_3DObj dat3DObj = hud_3DObjs[i];

                if (dat3DObj.isActive)
                {
                    dat3DObj.Run();
                }
            }

            #endregion

            #region HUD_SneakingHints
            if (CanShowHUD_SneakingHints())
            {
                if (!hud_SneakingHint_InitialDelayIsDone)
                {
                    hud_SneakingHint_TimeCounter = MathfPlus.DecByDeltatimeToZero(hud_SneakingHint_TimeCounter);

                    if (hud_SneakingHint_TimeCounter == 0)
                    {
                        hud_SneakingHint_InitialDelayIsDone = true;
                    }
                }
                else
                {
                    if (!hud_SneakingHint_IsShowingAllHintPartsDone)
                    {
                        if (!hud_SneakingHint_IsShowingHintBoxesDone)
                        {
                            if (!hud_SneakingHint_IsCountingDelayBetweenHintBoxes)
                            {
                                hudGroup_SneakingHints.hudControls[hud_SneakingHint_CurIndex].SetAlpha(hudGroup_SneakingHints.hudControls[hud_SneakingHint_CurIndex].alpha + Time.deltaTime * hud_SneakingHint_ShowSpeed);

                                if (hudGroup_SneakingHints.hudControls[hud_SneakingHint_CurIndex].alphaStatus == HUDAlphaStat.Full)
                                {
                                    hud_SneakingHint_TimeCounter = hud_SneakingHint_BetweenHUDsDelayMaxTime;
                                    hud_SneakingHint_IsCountingDelayBetweenHintBoxes = true;
                                }
                            }
                            else
                            {
                                hud_SneakingHint_TimeCounter = MathfPlus.DecByDeltatimeToZero(hud_SneakingHint_TimeCounter);

                                if (hud_SneakingHint_TimeCounter == 0)
                                {
                                    hud_SneakingHint_CurIndex++;

                                    if (hud_SneakingHint_CurIndex < hudGroup_SneakingHints.hudControls.Length)
                                    {
                                        hud_SneakingHint_IsCountingDelayBetweenHintBoxes = false;
                                    }
                                    else
                                    {
                                        hud_SneakingHint_IsShowingHintBoxesDone = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            hudControl_SneakingHints_LastSentence.SetAlpha(hudControl_SneakingHints_LastSentence.alpha + Time.deltaTime * hud_SneakingHint_ShowSpeed);

                            if (hudControl_SneakingHints_LastSentence.alphaStatus == HUDAlphaStat.Full)
                            {
                                hud_SneakingHint_TimeCounter = hud_SneakingHint_DurationWhenCompletedMaxTime;

                                hud_SneakingHint_IsShowingAllHintPartsDone = true;
                            }
                        }
                    }
                    else
                    {
                        if (!hud_SneakingHint_IsHidingAll)
                        {
                            hud_SneakingHint_TimeCounter = MathfPlus.DecByDeltatimeToZero(hud_SneakingHint_TimeCounter);

                            if (hud_SneakingHint_TimeCounter == 0)
                            {
                                hud_SneakingHint_IsHidingAll = true;
                            }
                        }
                        else
                        {
                            hudGroup_SneakingHints.SetAlphaOfAllChilds(hudGroup_SneakingHints.hudControls[0].alpha - hud_SneakingHint_HideSpeed * Time.deltaTime);
                            hudControl_SneakingHints_LastSentence.SetAlpha(hudControl_SneakingHints_LastSentence.alpha - hud_SneakingHint_HideSpeed * Time.deltaTime);

                            if (hudControl_SneakingHints_LastSentence.alphaStatus == HUDAlphaStat.Zero)
                            {
                                hud_ShouldShow_LvlCampSneakingHints = false;

                                hudGroup_SneakingHints.SetAlphaOfAllChilds(0);
                                hudGroup_SneakingHints.SetVisibilityOfAllChilds(false);

                                hudControl_SneakingHints_LastSentence.SetAlpha(0);
                                hudControl_SneakingHints_LastSentence.SetIsVisible(false);
                            }
                        }
                    }
                }
            }
            else
            {
                if (hudGroup_SneakingHints.hudControls[0].alpha > 0 || hudGroup_SneakingHints.hudControls[0].isControlVisible)
                {
                    hudGroup_SneakingHints.SetAlphaOfAllChilds(0);
                    hudGroup_SneakingHints.SetVisibilityOfAllChilds(false);

                    hudControl_SneakingHints_LastSentence.SetAlpha(0);
                    hudControl_SneakingHints_LastSentence.SetIsVisible(false);
                }
            }

            #endregion

            #region HUD_LvlCampCounterClock
            if (CanShowHUD_LvlCampCounterClock())
            {
                hud_LvlCamp_ClockTimeCounter = MathfPlus.DecByDeltatimeToZero(hud_LvlCamp_ClockTimeCounter);

                if (hud_LvlCamp_ClockTimeCounter < hud_LvlCamp_TimeToStartBlinkingRedClock)
                {
                    float minMaxDif = hud_LvlCamp_TimeToStartBlinkingRedClock - hud_LvlCamp_TimeToDoMaxBlinkingSpeedForRedClock;
                    float time_Min_Dif = hud_LvlCamp_ClockTimeCounter - hud_LvlCamp_TimeToDoMaxBlinkingSpeedForRedClock;
                    time_Min_Dif = Mathf.Clamp(time_Min_Dif, 0, float.MaxValue);

                    float redClockOppVal = (minMaxDif - time_Min_Dif) / minMaxDif;
                    redClockOppVal = Mathf.Clamp01(redClockOppVal);

                    float timeSpeedCoef = hud_LvlCamp_RedClockBlinkingSpeed_Min + redClockOppVal * (hud_LvlCamp_RedClockBlinkingSpeed_Max - hud_LvlCamp_RedClockBlinkingSpeed_Min);
                    float maxAlph = hud_LvlCamp_RedClockBlinkingAlpha_Min + redClockOppVal * (hud_LvlCamp_RedClockBlinkingAlpha_Max - hud_LvlCamp_RedClockBlinkingAlpha_Min);

                    hud_LvlCamp_RedClockBlinkTimeCounter += timeSpeedCoef * Time.deltaTime;
                    float alph = hud_LvlCamp_AnimCurve_RedClock.Evaluate(hud_LvlCamp_RedClockBlinkTimeCounter) * maxAlph;

                    hudControl_LvlCamp_ClockBGRed.SetAlpha(alph);
                }

                if (hud_LvlCamp_ClockTimeCounter == 0)
                {
                    HUD_StopShowingLvlCampCounterClock();
                }
            }
            else
            {
                if (hudControl_LvlCamp_ClockBG.alpha > 0)
                {
                    hudControl_LvlCamp_ClockBG.SetAlpha(hudControl_LvlCamp_ClockBG.alpha - Time.deltaTime * hud_LvlCamp_ClockHideSpeed);
                    hudControl_LvlCamp_ClockBGRed.SetAlpha(hudControl_LvlCamp_ClockBGRed.alpha - Time.deltaTime * hud_LvlCamp_ClockHideSpeed);
                    hudControl_LvlCamp_ClockHandle.SetAlpha(hudControl_LvlCamp_ClockHandle.alpha - Time.deltaTime * hud_LvlCamp_ClockHideSpeed);

                    if (hudControl_LvlCamp_ClockBG.alpha == 0)
                    {
                        hudControl_LvlCamp_ClockBG.SetAlpha(0);
                        hudControl_LvlCamp_ClockBG.SetIsVisible(false);

                        hudControl_LvlCamp_ClockBGRed.SetAlpha(0);
                        hudControl_LvlCamp_ClockBGRed.SetIsVisible(false);

                        hudControl_LvlCamp_ClockHandle.SetAlpha(0);
                        hudControl_LvlCamp_ClockHandle.SetIsVisible(false);
                    }
                }
            }

            #endregion

            #endregion

            //

            if (!IsPlayerStopped() || (IsPlayerStopped() && !isPlayerStopForCutscene))
            {
                #region Player States

            StartStates:

                #region Idle_Init
                if (IsState(PlayerStateEnum.Idle_Init))
                {
                    needToFadeCross = false;

                    canRun = true;

                    bool ndsChngGun = needsToChangeGun;
                    SetNeedsToChangeGun(false);

                    bool ndsAim = needsToAim;
                    SetNeedsToAim(false);

                    if (ShouldAutomaticallyReload())
                    {
                        SetState(GetReloadStateForCurrentGun());
                        goto EndStates;
                    }

                    if (ndsChngGun)
                    {
                        SetState(PlayerStateEnum.GunDown_Init);
                        goto EndStates;
                    }

                    if (ndsAim)
                    {
                        SetState(GetIdleToAimStateForCurrentGun());
                        goto EndStates;
                    }

                    if (IsPayerMovingSlowOrNormal())
                    {
                        SetState(PlayerStateEnum.Walk_Init);
                        goto EndStates;
                    }

                    if (IsPlayerRunning())
                    {
                        SetState(PlayerStateEnum.IdleToRun_Init);
                        goto EndStates;
                    }

                    curAnimCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.idle);

                    StartNewMainAnimWithCrossfadeTime(playerAnimationsList.idle, curAnimCFTime);

                    SetState(PlayerStateEnum.Idle_Update);
                }
                #endregion

                #region Idle_Update
                if (IsState(PlayerStateEnum.Idle_Update))
                {
                    if (ShouldAutomaticallyReload())
                    {
                        SetState(GetReloadStateForCurrentGun());
                        goto EndStates;
                    }

                    if (IsPayerMovingSlowOrNormal())
                    {
                        SetState(PlayerStateEnum.Walk_Init);
                        goto EndStates;
                    }

                    if (IsPlayerRunning())
                    {
                        SetState(PlayerStateEnum.IdleToRun_Init);
                        goto EndStates;
                    }

                    //<Keys>

                    #region action
                    if (CheckActionRequestAndShouldPickNewGun())
                    {
                        SetState(PlayerStateEnum.PickingNewGun_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region grenade
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Grenade()) //(GetButtonDown(keys.grenade))
                    {
                        if (CanThrowGrenade())
                        {
                            SetState(PlayerStateEnum.GrenadeGunDown_Init);
                            goto EndStates;
                        }
                    }
                    #endregion

                    #region melee
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Melee()) //(GetButtonDown(keys.melee))
                    {
                        SetState(PlayerStateEnum.Melee_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region changeGun
                    if (IsChangeGunKeyPressed())
                    {
                        SetState(PlayerStateEnum.GunDown_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region fire
                    if (NeedsToFire())
                    {
                        SetState(PlayerStateEnum.Fire_Init);
                        goto EndStates;
                    }
                    else
                    {
                        if (ShouldPlayGunEmptySound())
                        {
                            SetFiringIsNotOk();
                            curActiveGun.PlaySound_Empty();
                        }
                    }
                    #endregion

                    #region reload
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Reload()) //(GetButtonDown(keys.reload))
                    {
                        if (CanReload())
                        {
                            SetState(GetReloadStateForCurrentGun());
                            goto EndStates;
                        }
                    }
                    #endregion

                    #region aim
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Aim()) //(GetButtonDown(keys.aim))
                    {
                        SetState(GetIdleToAimStateForCurrentGun());
                        goto EndStates;
                    }
                    #endregion

                    #region FU
                    //if (Is_FU_KeyDown())
                    //{
                    //    if (curActiveGun.gunName == PlayerGunName.Springfield)
                    //    {
                    //        SetState(PlayerStateEnum.FU_Init);
                    //        goto EndStates;
                    //    }
                    //}
                    #endregion

                    //</Keys>
                }
                #endregion

                #region Walk_Init
                if (IsState(PlayerStateEnum.Walk_Init))
                {
                    canRun = true;

                    if (ShouldAutomaticallyReload())
                    {
                        SetState(GetReloadStateForCurrentGun());
                        goto EndStates;
                    }

                    curAnimCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.walk);

                    StartNewMainAnimWithCrossfadeTime(playerAnimationsList.walk, curAnimCFTime);

                    SetState(PlayerStateEnum.Walk_Update);
                }
                #endregion

                #region Walk_Update
                if (IsState(PlayerStateEnum.Walk_Update))
                {
                    if (ShouldAutomaticallyReload())
                    {
                        SetState(GetReloadStateForCurrentGun());
                        goto EndStates;
                    }

                    if (IsPlayerRunning())
                    {
                        SetState(PlayerStateEnum.IdleToRun_Init);
                        goto EndStates;
                    }

                    if (!IsPayerMovingSlowOrNormal()) // <-- NOT!
                    {
                        SetState(PlayerStateEnum.Idle_Init);
                        goto EndStates;
                    }

                    //<Keys>

                    #region action
                    if (CheckActionRequestAndShouldPickNewGun())
                    {
                        SetState(PlayerStateEnum.PickingNewGun_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region grenade
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Grenade()) //(GetButtonDown(keys.grenade))
                    {
                        if (CanThrowGrenade())
                        {
                            SetState(PlayerStateEnum.GrenadeGunDown_Init);
                            goto EndStates;
                        }
                    }
                    #endregion

                    #region melee
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Melee()) //(GetButtonDown(keys.melee))
                    {
                        SetState(PlayerStateEnum.Melee_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region changeGun
                    if (IsChangeGunKeyPressed())
                    {
                        SetState(PlayerStateEnum.GunDown_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region fire
                    if (NeedsToFire())
                    {
                        SetState(PlayerStateEnum.Fire_Init);
                        goto EndStates;
                    }
                    else
                    {
                        if (ShouldPlayGunEmptySound())
                        {
                            SetFiringIsNotOk();
                            curActiveGun.PlaySound_Empty();
                        }
                    }
                    #endregion

                    #region reload
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Reload()) //(GetButtonDown(keys.reload))
                    {
                        if (CanReload())
                        {
                            SetState(GetReloadStateForCurrentGun());
                            goto EndStates;
                        }
                    }
                    #endregion

                    #region aim
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Aim()) //(GetButtonDown(keys.aim))
                    {
                        SetState(GetIdleToAimStateForCurrentGun());
                        goto EndStates;
                    }
                    #endregion

                    #region FU
                    //if (Is_FU_KeyDown())
                    //{
                    //    if (curActiveGun.gunName == PlayerGunName.Springfield)
                    //    {
                    //        SetState(PlayerStateEnum.FU_Init);
                    //        goto EndStates;
                    //    }
                    //}
                    #endregion

                    //</Keys>
                }
                #endregion

                #region IdleToRun_Init
                if (IsState(PlayerStateEnum.IdleToRun_Init))
                {
                    canRun = true;

                    curAnimCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.idleToRun);

                    StartNewMainAnimWithCrossfadeTime(playerAnimationsList.idleToRun, curAnimCFTime);

                    SetState(PlayerStateEnum.IdleToRun_Update);
                }
                #endregion

                #region IdleToRun_Update
                if (IsState(PlayerStateEnum.IdleToRun_Update))
                {
                    if (IsPlayerRunning())
                    {
                        endCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.run);

                        if (CheckMainAnimIsFinished(endCFTime))
                        {
                            SetState(PlayerStateEnum.Run_Init);
                            goto EndStates;
                        }
                    }
                    else
                    {
                        SetSprintingIsNotOk();
                        SetState(PlayerStateEnum.RunToIdle_Init);
                        goto EndStates;
                    }

                    //<Keys>

                    #region action
                    if (CheckActionRequestAndShouldPickNewGun())
                    {
                        SetState(PlayerStateEnum.PickingNewGun_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region grenade
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Grenade()) //(GetButtonDown(keys.grenade))
                    {
                        SetSprintingIsNotOk();

                        if (CanThrowGrenade())
                        {
                            SetState(PlayerStateEnum.GrenadeGunDown_Init);
                            goto EndStates;
                        }
                    }
                    #endregion

                    #region melee
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Melee()) //(GetButtonDown(keys.melee))
                    {
                        SetSprintingIsNotOk();
                        SetState(PlayerStateEnum.Melee_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region changeGun
                    if (IsChangeGunKeyPressed())
                    {
                        SetState(PlayerStateEnum.GunDown_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region reload
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Reload()) //(GetButtonDown(keys.reload))
                    {
                        SetSprintingIsNotOk();

                        if (CanReload())
                        {
                            SetState(GetReloadStateForCurrentGun());
                            goto EndStates;
                        }
                        else
                        {
                            SetState(PlayerStateEnum.RunToIdle_Init);
                            goto EndStates;
                        }
                    }
                    #endregion

                    #region aim, fire, crouch
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Aim() //(GetButtonDown(keys.aim)
                        || CustomInputManager.KeyDownIfGameIsNotPaused_Fire() //GetButtonDown(keys.fire)
                        || CustomInputManager.KeyDownIfGameIsNotPaused_Crouch()) //GetButtonDown(keys.crouch))
                    {
                        SetSprintingIsNotOk();
                        SetState(PlayerStateEnum.RunToIdle_Init);
                        goto EndStates;
                    }
                    #endregion

                    //</Keys>
                }
                #endregion

                #region RunToIdle_Init
                if (IsState(PlayerStateEnum.RunToIdle_Init))
                {
                    canRun = true;

                    curAnimCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.runToIdle);

                    StartNewMainAnimWithCrossfadeTime(playerAnimationsList.runToIdle, curAnimCFTime);

                    SetState(PlayerStateEnum.RunToIdle_Update);
                }
                #endregion

                #region RunToIdle_Update
                if (IsState(PlayerStateEnum.RunToIdle_Update))
                {
                    endCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.idle);

                    if (CheckMainAnimIsFinished(endCFTime))
                    {
                        SetState(PlayerStateEnum.Idle_Init);
                        goto EndStates;
                    }

                    //<Keys>

                    #region action
                    if (CheckActionRequestAndShouldPickNewGun())
                    {
                        SetState(PlayerStateEnum.PickingNewGun_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region grenade
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Grenade()) //(GetButtonDown(keys.grenade))
                    {
                        SetSprintingIsNotOk();

                        if (CanThrowGrenade())
                        {
                            SetState(PlayerStateEnum.GrenadeGunDown_Init);
                            goto EndStates;
                        }
                    }
                    #endregion

                    #region melee
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Melee()) //(GetButtonDown(keys.melee))
                    {
                        SetSprintingIsNotOk();
                        SetState(PlayerStateEnum.Melee_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region changeGun
                    if (IsChangeGunKeyPressed())
                    {
                        SetState(PlayerStateEnum.GunDown_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region reload
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Reload()) //(GetButtonDown(keys.reload))
                    {
                        SetSprintingIsNotOk();

                        if (CanReload())
                        {
                            SetState(GetReloadStateForCurrentGun());
                            goto EndStates;
                        }
                    }
                    #endregion

                    #region aim
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Aim()) //(GetButtonDown(keys.aim))
                    {
                        SetSprintingIsNotOk();
                        SetState(GetIdleToAimStateForCurrentGun());
                        goto EndStates;
                    }
                    #endregion

                    //</Keys>
                }
                #endregion

                #region Run_Init
                if (IsState(PlayerStateEnum.Run_Init))
                {
                    canRun = true;

                    curAnimCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.run);

                    StartNewMainAnimWithCrossfadeTime(playerAnimationsList.run, curAnimCFTime);

                    SetState(PlayerStateEnum.Run_Update);
                }
                #endregion

                #region Run_Update
                if (IsState(PlayerStateEnum.Run_Update))
                {
                    if (!IsPlayerRunning())
                    {
                        SetSprintingIsNotOk();
                        SetState(PlayerStateEnum.RunToIdle_Init);
                        goto EndStates;
                    }
                    else
                    {
                        IncreaseRunningTime();

                        if (IsPlayerTiredOfRunning())
                        {
                            SetPlayerShouldTakeABreathFromRunning();
                            SetSprintingIsNotOk();
                            SetState(PlayerStateEnum.RunToIdle_Init);
                            goto EndStates;
                        }
                    }

                    //<Keys>

                    #region grenade
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Grenade()) //(GetButtonDown(keys.grenade))
                    {
                        SetSprintingIsNotOk();

                        if (CanThrowGrenade())
                        {
                            SetState(PlayerStateEnum.GrenadeGunDown_Init);
                            goto EndStates;
                        }
                    }
                    #endregion

                    #region melee
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Melee()) //(GetButtonDown(keys.melee))
                    {
                        SetSprintingIsNotOk();
                        SetState(PlayerStateEnum.Melee_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region changeGun
                    if (IsChangeGunKeyPressed())
                    {
                        SetState(PlayerStateEnum.GunDown_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region reload
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Reload()) //(GetButtonDown(keys.reload))
                    {
                        SetSprintingIsNotOk();

                        if (CanReload())
                        {
                            SetState(GetReloadStateForCurrentGun());
                            goto EndStates;
                        }
                        else
                        {
                            SetState(PlayerStateEnum.RunToIdle_Init);
                            goto EndStates;
                        }
                    }
                    #endregion

                    #region aim
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Aim()) //(GetButtonDown(keys.aim))
                    {
                        SetSprintingIsNotOk();
                        SetState(GetIdleToAimStateForCurrentGun());
                        goto EndStates;
                    }
                    #endregion

                    #region fire, crouch
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Fire() //(GetButtonDown(keys.fire)
                        || CustomInputManager.KeyDownIfGameIsNotPaused_Crouch()) //GetButtonDown(keys.crouch))
                    {
                        SetSprintingIsNotOk();
                        SetState(PlayerStateEnum.RunToIdle_Init);
                        goto EndStates;
                    }
                    #endregion

                    //</Keys>
                }
                #endregion

                #region Fire_Init
                if (IsState(PlayerStateEnum.Fire_Init))
                {
                    SetNeedsToAim(false);
                    SetNeedsToChangeGun(false);

                    canRun = false;

                    curAnimCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.fire);

                    StartNewMainAnimWithCrossfadeTime(playerAnimationsList.fire, curAnimCFTime);

                    if (!curActiveGun.isAutomatic)
                        SetFiringIsNotOk();

                    curActiveGun.TryFire();

                    ReSetGunInfoHUD();

                    SetState(PlayerStateEnum.Fire_Update);
                }
                #endregion

                #region Fire_Update
                if (IsState(PlayerStateEnum.Fire_Update))
                {
                    endCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.idle);

                    if (CheckMainAnimIsFinished(endCFTime))
                    {
                        SetState(PlayerStateEnum.Idle_Init);
                        goto EndStates;
                    }

                    //<Keys>

                    #region grenade
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Grenade()) //(GetButtonDown(keys.grenade))
                    {
                        SetNeedsToAim(false);

                        if (CanThrowGrenade())
                        {
                            SetState(PlayerStateEnum.GrenadeGunDown_Init);
                            goto EndStates;
                        }
                    }
                    #endregion

                    #region melee
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Melee()) //(GetButtonDown(keys.melee))
                    {
                        SetNeedsToAim(false);

                        SetState(PlayerStateEnum.Melee_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region changeGun
                    if (IsChangeGunKeyPressed())
                    {
                        SetNeedsToChangeGun(true);
                    }
                    #endregion

                    #region reload
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Reload()) //(GetButtonDown(keys.reload))
                    {
                        if (!needsToChangeGun)
                        {
                            if (CanReload())
                            {
                                SetNeedsToAim(false);

                                SetState(GetReloadStateForCurrentGun());
                                goto EndStates;
                            }
                        }
                    }
                    #endregion

                    #region aim
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Aim()) //(GetButtonDown(keys.aim))
                    {
                        SetNeedsToAim(true);
                    }
                    #endregion

                    //</Keys>
                }
                #endregion

                #region Reload_Init
                if (IsState(PlayerStateEnum.Reload_Init))
                {
                    needToFadeCross = true;

                    canRun = false;

                    curAnimCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.reload);

                    StartNewMainAnimWithCrossfadeTime(playerAnimationsList.reload, curAnimCFTime);

                    reloadTimeCounter = curActiveGun.reloadTimeOnAnim;

                    didReloadingDone = false;

                    curActiveGun.PlaySound_Reload();

                    SetState(PlayerStateEnum.Reload_Update);
                }
                #endregion

                #region Reload_Update
                if (IsState(PlayerStateEnum.Reload_Update))
                {
                    endCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.idle);

                    if (CheckMainAnimIsFinished(endCFTime))
                    {
                        if (!didReloadingDone)
                        {
                            curActiveGun.Reload(PlayerGunReloadMode.Full);
                            ReSetGunInfoHUD();
                        }

                        SetState(PlayerStateEnum.Idle_Init);
                        goto EndStates;
                    }

                    reloadTimeCounter = MathfPlus.DecByDeltatimeToZero(reloadTimeCounter);

                    if (reloadTimeCounter == 0)
                    {
                        if (!didReloadingDone)
                        {
                            didReloadingDone = true;

                            curActiveGun.Reload(PlayerGunReloadMode.Full);
                            ReSetGunInfoHUD();
                        }
                    }

                    if (IsPlayerRunning())
                    {
                        SetState(PlayerStateEnum.IdleToRun_Init);
                        goto EndStates;
                    }

                    //<Keys>

                    #region action
                    if (CheckActionRequestAndShouldPickNewGun())
                    {
                        SetState(PlayerStateEnum.PickingNewGun_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region grenade
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Grenade()) //(GetButtonDown(keys.grenade))
                    {
                        if (CanThrowGrenade())
                        {
                            SetState(PlayerStateEnum.GrenadeGunDown_Init);
                            goto EndStates;
                        }
                    }
                    #endregion

                    #region melee
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Melee()) //(GetButtonDown(keys.melee))
                    {
                        SetState(PlayerStateEnum.Melee_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region changeGun
                    if (IsChangeGunKeyPressed())
                    {
                        SetState(PlayerStateEnum.GunDown_Init);
                        goto EndStates;
                    }
                    #endregion

                    //</Keys>
                }
                #endregion

                #region GunDown_Init
                if (IsState(PlayerStateEnum.GunDown_Init))
                {
                    needToFadeCross = true;

                    canRun = true;

                    curAnimCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.gunDown);

                    StartNewMainAnimWithCrossfadeTime(playerAnimationsList.gunDown, curAnimCFTime);

                    SetState(PlayerStateEnum.GunDown_Update);
                }
                #endregion

                #region GunDown_Update
                if (IsState(PlayerStateEnum.GunDown_Update))
                {
                    endCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.gunUp);

                    if (CheckMainAnimIsFinished(endCFTime))
                    {
                        SetState(PlayerStateEnum.GunUp_Init);
                        goto EndStates;
                    }

                    //<Keys>

                    #region action
                    if (CheckActionRequestAndShouldPickNewGun())
                    {
                        SetState(PlayerStateEnum.PickingNewGun_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region grenade
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Grenade()) //(GetButtonDown(keys.grenade))
                    {
                        SetSprintingIsNotOk();

                        if (CanThrowGrenade())
                        {
                            SetNeedsToChangeGun(true);

                            SetState(PlayerStateEnum.GrenadeGunDown_Init);
                            goto EndStates;
                        }
                    }
                    #endregion

                    //</Keys>
                }
                #endregion

                #region GunUp_Init
                if (IsState(PlayerStateEnum.GunUp_Init))
                {
                    canRun = true;

                    ChangeGun();

                    ShowGunShapeHUDForAWhile();

                    curAnimCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.gunUp);
                    //curAnimCFTime = 0;

                    StartNewMainAnimWithCrossfadeTime(playerAnimationsList.gunUp, curAnimCFTime);

                    SetState(PlayerStateEnum.GunUp_Update);
                }
                #endregion

                #region GunUp_Update
                if (IsState(PlayerStateEnum.GunUp_Update))
                {
                    endCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.idle);

                    if (CheckMainAnimIsFinished(endCFTime))
                    {
                        SetState(PlayerStateEnum.Idle_Init);
                        goto EndStates;
                    }

                    //<Keys>

                    #region action
                    if (CheckActionRequestAndShouldPickNewGun())
                    {
                        SetState(PlayerStateEnum.PickingNewGun_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region grenade
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Grenade()) //(GetButtonDown(keys.grenade))
                    {
                        SetSprintingIsNotOk();

                        if (CanThrowGrenade())
                        {
                            SetState(PlayerStateEnum.GrenadeGunDown_Init);
                            goto EndStates;
                        }
                    }
                    #endregion

                    //</Keys>
                }
                #endregion

                #region IdleToAim_Init
                if (IsState(PlayerStateEnum.IdleToAim_Init))
                {
                    needToFadeCross = true;

                    SetNeedsToAim(false);

                    StartZoomingFOV();

                    canRun = false;

                    curAnimCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.idleToAim);

                    StartNewMainAnimWithCrossfadeTime(playerAnimationsList.idleToAim, curAnimCFTime);

                    SetState(PlayerStateEnum.IdleToAim_Update);
                }
                #endregion

                #region IdleToAim_Update
                if (IsState(PlayerStateEnum.IdleToAim_Update))
                {
                    endCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.aimIdle);

                    if (CheckMainAnimIsFinished(endCFTime))
                    {
                        SetState(PlayerStateEnum.Aim_Init);
                        goto EndStates;
                    }

                    //<Keys>

                    #region action
                    if (CheckActionRequestAndShouldPickNewGun())
                    {
                        SetNeedsToAim(false);
                        StartUnzoomingFOV();

                        SetState(PlayerStateEnum.PickingNewGun_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region grenade
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Grenade()) //(GetButtonDown(keys.grenade))
                    {
                        if (CanThrowGrenade())
                        {
                            SetNeedsToAim(false);

                            StartUnzoomingFOV();

                            SetState(PlayerStateEnum.GrenadeGunDown_Init);
                            goto EndStates;
                        }
                    }
                    #endregion

                    #region melee
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Melee()) //(GetButtonDown(keys.melee))
                    {
                        SetNeedsToAim(true);

                        StartUnzoomingFOV();

                        SetState(PlayerStateEnum.Melee_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region changeGun
                    if (IsChangeGunKeyPressed())
                    {
                        SetNeedsToAim(false);

                        StartUnzoomingFOV();

                        SetState(PlayerStateEnum.GunDown_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region reload
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Reload()) //(GetButtonDown(keys.reload))
                    {
                        if (CanReload())
                        {
                            SetNeedsToAim(false);

                            StartUnzoomingFOV();

                            SetState(GetReloadStateForCurrentGun());
                            goto EndStates;
                        }
                    }
                    #endregion

                    #region aim
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Aim()) //(GetButtonDown(keys.aim))
                    {
                        SetNeedsToAim(false);

                        StartUnzoomingFOV();

                        SetState(GetAimToIdleStateForCurrentGun());
                        goto EndStates;
                    }
                    #endregion

                    #region fire
                    if (NeedsToFire())
                    {
                        SetNeedsToAim(false);

                        SetState(PlayerStateEnum.AimFire_Init);
                        goto EndStates;
                    }
                    else
                    {
                        if (ShouldPlayGunEmptySound())
                        {
                            SetFiringIsNotOk();
                            curActiveGun.PlaySound_Empty();
                        }
                    }
                    #endregion

                    //</Keys>
                }
                #endregion

                #region AimToIdle_Init
                if (IsState(PlayerStateEnum.AimToIdle_Init))
                {
                    StartUnzoomingFOV();

                    canRun = false;

                    curAnimCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.aimToIdle);

                    StartNewMainAnimWithCrossfadeTime(playerAnimationsList.aimToIdle, curAnimCFTime);

                    SetState(PlayerStateEnum.AimToIdle_Update);
                }
                #endregion

                #region AimToIdle_Update
                if (IsState(PlayerStateEnum.AimToIdle_Update))
                {
                    endCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.idle);

                    if (CheckMainAnimIsFinished(endCFTime))
                    {
                        SetState(PlayerStateEnum.Idle_Init);
                        goto EndStates;
                    }

                    //<Keys>

                    #region action
                    if (CheckActionRequestAndShouldPickNewGun())
                    {
                        SetState(PlayerStateEnum.PickingNewGun_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region grenade
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Grenade()) //(GetButtonDown(keys.grenade))
                    {
                        if (CanThrowGrenade())
                        {
                            SetState(PlayerStateEnum.GrenadeGunDown_Init);
                            goto EndStates;
                        }
                    }
                    #endregion

                    #region melee
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Melee()) //(GetButtonDown(keys.melee))
                    {
                        SetState(PlayerStateEnum.Melee_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region changeGun
                    if (IsChangeGunKeyPressed())
                    {
                        SetState(PlayerStateEnum.GunDown_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region reload
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Reload()) //(GetButtonDown(keys.reload))
                    {
                        if (CanReload())
                        {
                            SetState(GetReloadStateForCurrentGun());
                            goto EndStates;
                        }
                    }
                    #endregion

                    #region aim
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Aim()) //(GetButtonDown(keys.aim))
                    {
                        SetState(GetIdleToAimStateForCurrentGun());
                        goto EndStates;
                    }
                    #endregion

                    #region fire
                    if (NeedsToFire())
                    {
                        SetState(PlayerStateEnum.Fire_Init);
                        goto EndStates;
                    }
                    else
                    {
                        if (ShouldPlayGunEmptySound())
                        {
                            SetFiringIsNotOk();
                            curActiveGun.PlaySound_Empty();
                        }
                    }
                    #endregion

                    //</Keys>
                }
                #endregion

                #region Aim_Init
                if (IsState(PlayerStateEnum.Aim_Init))
                {
                    SetNeedsToAim(false);

                    canRun = true;

                    if (ShouldAutomaticallyReload())
                    {
                        StartUnzoomingFOV();

                        SetState(GetReloadStateForCurrentGun());
                        goto EndStates;
                    }

                    curAnimCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.aimIdle);

                    StartNewMainAnimWithCrossfadeTime(playerAnimationsList.aimIdle, curAnimCFTime);

                    SetState(PlayerStateEnum.Aim_Update);
                }
                #endregion

                #region Aim_Update
                if (IsState(PlayerStateEnum.Aim_Update))
                {
                    if (ShouldAutomaticallyReload())
                    {
                        SetNeedsToAim(false);

                        StartUnzoomingFOV();

                        SetState(GetReloadStateForCurrentGun());
                        goto EndStates;
                    }

                    if (IsPayerMovingSlowOrNormal())
                    {
                        SetNeedsToAim(false);

                        SetState(PlayerStateEnum.AimWalk_Init);
                        goto EndStates;
                    }

                    if (IsPlayerRunning())
                    {
                        SetNeedsToAim(false);

                        SetState(GetAimToIdleStateForCurrentGun());
                        goto EndStates;
                    }

                    //<Keys>

                    #region action
                    if (CheckActionRequestAndShouldPickNewGun())
                    {
                        SetNeedsToAim(false);
                        StartUnzoomingFOV();

                        SetState(PlayerStateEnum.PickingNewGun_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region grenade
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Grenade()) //(GetButtonDown(keys.grenade))
                    {
                        if (CanThrowGrenade())
                        {
                            SetNeedsToAim(false);

                            StartUnzoomingFOV();

                            SetState(PlayerStateEnum.GrenadeGunDown_Init);
                            goto EndStates;
                        }
                    }
                    #endregion

                    #region melee
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Melee()) //(GetButtonDown(keys.melee))
                    {
                        SetNeedsToAim(true);

                        StartUnzoomingFOV();

                        SetState(PlayerStateEnum.Melee_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region changeGun
                    if (IsChangeGunKeyPressed())
                    {
                        SetNeedsToAim(false);

                        StartUnzoomingFOV();

                        SetState(PlayerStateEnum.GunDown_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region reload
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Reload()) //(GetButtonDown(keys.reload))
                    {
                        if (CanReload())
                        {
                            SetNeedsToAim(false);

                            StartUnzoomingFOV();

                            SetState(GetReloadStateForCurrentGun());
                            goto EndStates;
                        }
                    }
                    #endregion

                    #region aim
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Aim()) //(GetButtonDown(keys.aim))
                    {
                        SetNeedsToAim(false);

                        StartUnzoomingFOV();

                        SetState(GetAimToIdleStateForCurrentGun());
                        goto EndStates;
                    }
                    #endregion

                    #region fire
                    if (NeedsToFire())
                    {
                        SetNeedsToAim(false);

                        SetState(PlayerStateEnum.AimFire_Init);
                        goto EndStates;
                    }
                    else
                    {
                        if (ShouldPlayGunEmptySound())
                        {
                            SetFiringIsNotOk();
                            curActiveGun.PlaySound_Empty();
                        }
                    }
                    #endregion

                    //</Keys>
                }
                #endregion

                #region AimWalk_Init
                if (IsState(PlayerStateEnum.AimWalk_Init))
                {
                    SetNeedsToAim(false);

                    canRun = true;

                    if (ShouldAutomaticallyReload())
                    {
                        StartUnzoomingFOV();

                        SetState(GetReloadStateForCurrentGun());
                        goto EndStates;
                    }

                    curAnimCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.aimWalk);

                    StartNewMainAnimWithCrossfadeTime(playerAnimationsList.aimWalk, curAnimCFTime);

                    SetState(PlayerStateEnum.AimWalk_Update);
                }
                #endregion

                #region AimWalk_Update
                if (IsState(PlayerStateEnum.AimWalk_Update))
                {
                    if (ShouldAutomaticallyReload())
                    {
                        SetNeedsToAim(false);

                        StartUnzoomingFOV();

                        SetState(GetReloadStateForCurrentGun());
                        goto EndStates;
                    }

                    if (IsPlayerRunning())
                    {
                        SetNeedsToAim(false);

                        SetState(GetAimToIdleStateForCurrentGun());
                        goto EndStates;
                    }

                    if (!IsPayerMovingSlowOrNormal()) // <- NOT!
                    {
                        SetNeedsToAim(false);

                        SetState(PlayerStateEnum.Aim_Init);
                        goto EndStates;
                    }

                    //<Keys>

                    #region action
                    if (CheckActionRequestAndShouldPickNewGun())
                    {
                        SetNeedsToAim(false);
                        StartUnzoomingFOV();

                        SetState(PlayerStateEnum.PickingNewGun_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region grenade
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Grenade()) //(GetButtonDown(keys.grenade))
                    {
                        if (CanThrowGrenade())
                        {
                            SetNeedsToAim(false);

                            StartUnzoomingFOV();

                            SetState(PlayerStateEnum.GrenadeGunDown_Init);
                            goto EndStates;
                        }
                    }
                    #endregion

                    #region melee
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Melee()) //(GetButtonDown(keys.melee))
                    {
                        SetNeedsToAim(true);

                        StartUnzoomingFOV();

                        SetState(PlayerStateEnum.Melee_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region changeGun
                    if (IsChangeGunKeyPressed())
                    {
                        SetNeedsToAim(false);

                        StartUnzoomingFOV();

                        SetState(PlayerStateEnum.GunDown_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region reload
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Reload()) //(GetButtonDown(keys.reload))
                    {
                        if (CanReload())
                        {
                            SetNeedsToAim(false);

                            StartUnzoomingFOV();

                            SetState(GetReloadStateForCurrentGun());
                            goto EndStates;
                        }
                    }
                    #endregion

                    #region aim
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Aim()) //(GetButtonDown(keys.aim))
                    {
                        SetNeedsToAim(false);

                        StartUnzoomingFOV();

                        SetState(GetAimToIdleStateForCurrentGun());
                        goto EndStates;
                    }
                    #endregion

                    #region fire
                    if (NeedsToFire())
                    {
                        SetNeedsToAim(false);

                        SetState(PlayerStateEnum.AimFire_Init);
                        goto EndStates;
                    }
                    else
                    {
                        if (ShouldPlayGunEmptySound())
                        {
                            SetFiringIsNotOk();
                            curActiveGun.PlaySound_Empty();
                        }
                    }
                    #endregion

                    //</Keys>
                }
                #endregion

                #region AimFire_Init
                if (IsState(PlayerStateEnum.AimFire_Init))
                {
                    SetNeedsToAim(false);

                    canRun = false;

                    curAnimCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.aimFire);

                    StartNewMainAnimWithCrossfadeTime(playerAnimationsList.aimFire, curAnimCFTime);

                    if (!curActiveGun.isAutomatic)
                        SetFiringIsNotOk();

                    curActiveGun.TryFire();

                    ReSetGunInfoHUD();

                    SetState(PlayerStateEnum.AimFire_Update);
                }
                #endregion

                #region AimFire_Update
                if (IsState(PlayerStateEnum.AimFire_Update))
                {
                    endCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.aimIdle);

                    if (CheckMainAnimIsFinished(endCFTime))
                    {
                        SetState(PlayerStateEnum.Aim_Init);
                        goto EndStates;
                    }

                    //<Keys>

                    #region grenade
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Grenade()) //(GetButtonDown(keys.grenade))
                    {
                        if (CanThrowGrenade())
                        {
                            SetNeedsToAim(false);

                            StartUnzoomingFOV();

                            SetState(PlayerStateEnum.GrenadeGunDown_Init);
                            goto EndStates;
                        }
                    }
                    #endregion

                    #region melee
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Melee()) //(GetButtonDown(keys.melee))
                    {
                        SetNeedsToAim(true);

                        StartUnzoomingFOV();

                        SetState(PlayerStateEnum.Melee_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region changeGun
                    if (IsChangeGunKeyPressed())
                    {
                        SetNeedsToAim(false);

                        StartUnzoomingFOV();

                        SetState(PlayerStateEnum.GunDown_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region reload
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Reload()) //(GetButtonDown(keys.reload))
                    {
                        if (CanReload())
                        {
                            SetNeedsToAim(false);

                            StartUnzoomingFOV();

                            SetState(GetReloadStateForCurrentGun());
                            goto EndStates;
                        }
                    }
                    #endregion

                    #region aim
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Aim()) //(GetButtonDown(keys.aim))
                    {
                        SetNeedsToAim(false);

                        StartUnzoomingFOV();

                        SetState(GetAimToIdleStateForCurrentGun());
                        goto EndStates;
                    }
                    #endregion

                    //</Keys>
                }
                #endregion

                #region //BRIDGE Melee_Init
                if (IsState(PlayerStateEnum.Melee_Init))
                {
                    SetState(PlayerStateEnum.KnifeGunDown_Init);

                    //needToFadeCross = true;

                    //canRun = false;

                    //curAnimCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.melee);

                    //StartNewMainAnimWithCrossfadeTime(playerAnimationsList.melee, curAnimCFTime);

                    //meleeAttackDelayTimeCounter = meleeAttackMaxDelay;

                    //didMeleeAttackDone = false;

                    //SetState(PlayerStateEnum.Melee_Update);
                }
                #endregion

                #region //OLD Melee_Update
                //if (IsState(PlayerStateEnum.Melee_Update))
                //{
                //    endCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.idle);

                //    if (CheckMainAnimIsFinished(endCFTime))
                //    {
                //        if (!didMeleeAttackDone)
                //        {
                //            TryDoMeleeAttackDamage();
                //        }

                //        SetState(PlayerStateEnum.Idle_Init);
                //        goto EndStates;
                //    }

                //    meleeAttackDelayTimeCounter = MathfPlus.DecByDeltatimeToZero(meleeAttackDelayTimeCounter);

                //    if (meleeAttackDelayTimeCounter == 0)
                //    {
                //        if (!didMeleeAttackDone)
                //        {
                //            didMeleeAttackDone = true;

                //            TryDoMeleeAttackDamage();
                //        }
                //    }

                //    //<Keys>

                //    #region grenade
                //    if (GetButtonDown(keys.grenade))
                //    {
                //        if (CanThrowGrenade())
                //        {
                //            SetNeedsToAim(false);

                //            SetState(PlayerStateEnum.GrenadeGunDown_Init);
                //            goto EndStates;
                //        }
                //    }
                //    #endregion

                //    #region changeGun
                //    if (GetButtonDown(keys.changeGun))
                //    {
                //        SetNeedsToChangeGun(true);
                //    }
                //    #endregion

                //    #region aim
                //    if (GetButtonDown(keys.aim))
                //    {
                //        SetNeedsToAim(true);
                //    }
                //    #endregion

                //    //</Keys>
                //}
                #endregion

                #region GrenadeGunDown_Init
                if (IsState(PlayerStateEnum.GrenadeGunDown_Init))
                {
                    needToFadeCross = true;

                    canRun = false;

                    ReSetGunInfoHUD();

                    curAnimCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.grenadeGunDown);

                    StartNewMainAnimWithCrossfadeTime(playerAnimationsList.grenadeGunDown, curAnimCFTime);

                    SetState(PlayerStateEnum.GrenadeGunDown_Update);
                }
                #endregion

                #region GrenadeGunDown_Update
                if (IsState(PlayerStateEnum.GrenadeGunDown_Update))
                {
                    endCFTime = 0;

                    if (CheckMainAnimIsFinished(endCFTime))
                    {
                        SetState(PlayerStateEnum.GrenadeZamen_Init);
                        goto EndStates;
                    }

                    //<Keys>

                    #region changeGun
                    if (IsChangeGunKeyPressed())
                    {
                        SetNeedsToChangeGun(true);
                    }
                    #endregion

                    #region aim
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Aim()) //(GetButtonDown(keys.aim))
                    {
                        SetNeedsToAim(true);
                    }
                    #endregion

                    //</Keys>
                }
                #endregion]

                #region GrenadeZamen_Init
                if (IsState(PlayerStateEnum.GrenadeZamen_Init))
                {
                    canRun = false;

                    MakeGunHidden(curActiveGun);
                    MakeGrenadeAppear();

                    StartNewAnimForObject(grenadeHandMeshObject, playerAnimationsList.grenadeZamen, 0);

                    shouldThrowGrenadeNow = false;

                    SetState(PlayerStateEnum.GrenadeZamen_Update);
                }
                #endregion

                #region GrenadeZamen_Update
                if (IsState(PlayerStateEnum.GrenadeZamen_Update))
                {
                    if (IsUnloopedAnimFinishedOnObject(grenadeHandMeshObject, playerAnimationsList.grenadeZamen, 0))
                    {
                        if (shouldThrowGrenadeNow)
                        {
                            SetState(PlayerStateEnum.GrenadePartab_Init);
                            goto EndStates;
                        }
                    }

                    //<Keys>

                    #region changeGun
                    if (IsChangeGunKeyPressed())
                    {
                        SetNeedsToChangeGun(true);
                    }
                    #endregion

                    #region aim
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Aim()) //(GetButtonDown(keys.aim))
                    {
                        SetNeedsToAim(true);
                    }
                    #endregion

                    #region grenade button up
                    if (!CustomInputManager.KeyIfGameIsNotPaused_Grenade()) // (!GetButton(keys.grenade))
                    {
                        shouldThrowGrenadeNow = true;
                    }
                    #endregion

                    //</Keys>
                }
                #endregion]

                #region GrenadePartab_Init
                if (IsState(PlayerStateEnum.GrenadePartab_Init))
                {
                    canRun = false;

                    StartNewAnimForObject(grenadeHandMeshObject, playerAnimationsList.grenadePartab, 0);

                    throwGrenadeDelayOnPartabAnim_TimeCounter = throwGrenadeDelayOnPartabAnim;

                    didGrenadeThrown = false;

                    SetState(PlayerStateEnum.GrenadePartab_Update);
                }
                #endregion

                #region GrenadePartab_Update
                if (IsState(PlayerStateEnum.GrenadePartab_Update))
                {
                    throwGrenadeDelayOnPartabAnim_TimeCounter = MathfPlus.DecByDeltatimeToZero(throwGrenadeDelayOnPartabAnim_TimeCounter);

                    if (throwGrenadeDelayOnPartabAnim_TimeCounter == 0)
                    {
                        if (!didGrenadeThrown)
                        {
                            didGrenadeThrown = true;

                            ThrowGrenade();

                            ReSetGunInfoHUD();
                        }
                    }

                    if (IsUnloopedAnimFinishedOnObject(grenadeHandMeshObject, playerAnimationsList.grenadePartab, 0))
                    {
                        SetState(PlayerStateEnum.GrenadeGunUp_Init);
                        goto EndStates;
                    }

                    //<Keys>

                    #region changeGun
                    if (IsChangeGunKeyPressed())
                    {
                        SetNeedsToChangeGun(true);
                    }
                    #endregion

                    #region aim
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Aim()) //(GetButtonDown(keys.aim))
                    {
                        SetNeedsToAim(true);
                    }
                    #endregion

                    //</Keys>
                }
                #endregion]

                #region GrenadeGunUp_Init
                if (IsState(PlayerStateEnum.GrenadeGunUp_Init))
                {
                    canRun = false;

                    MakeGunAppear(curActiveGun);
                    MakeGrenadeHidden();

                    //curAnimCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.grenadeGunUp);

                    curAnimCFTime = 0;

                    StartNewMainAnimWithCrossfadeTime(playerAnimationsList.grenadeGunUp, curAnimCFTime);

                    SetState(PlayerStateEnum.GrenadeGunUp_Update);
                }
                #endregion

                #region GrenadeGunUp_Update
                if (IsState(PlayerStateEnum.GrenadeGunUp_Update))
                {
                    endCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.idle);

                    if (CheckMainAnimIsFinished(endCFTime))
                    {
                        SetState(PlayerStateEnum.Idle_Init);
                        goto EndStates;
                    }

                    //<Keys>

                    #region changeGun
                    if (IsChangeGunKeyPressed())
                    {
                        SetNeedsToChangeGun(true);
                    }
                    #endregion

                    #region aim
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Aim()) //(GetButtonDown(keys.aim))
                    {
                        SetNeedsToAim(true);
                    }
                    #endregion

                    //</Keys>
                }
                #endregion]

                #region Die_Init
                if (IsState(PlayerStateEnum.Die_Init))
                {
                    StartDyingEffects();

                    SetState(PlayerStateEnum.Die_Update);
                }
                #endregion

                #region MissionFailedByOutMistake_Init
                if (IsState(PlayerStateEnum.MissionFailedByOutMistake_Init))
                {
                    StartMissionFailByOutMistakeEffects();

                    SetState(PlayerStateEnum.MissionFailedByOutMistake_Update);
                }
                #endregion

                #region DundunReload_Start_Init
                if (IsState(PlayerStateEnum.DundunReload_Start_Init))
                {
                    needToFadeCross = true;

                    canRun = false;

                    curAnimCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.dundunReload_Start);

                    StartNewMainAnimWithCrossfadeTime(playerAnimationsList.dundunReload_Start, curAnimCFTime);

                    curActiveGun.PlaySound_ReloadChikkiStart();

                    SetState(PlayerStateEnum.DundunReload_Start_Update);
                }
                #endregion

                #region DundunReload_Start_Update
                if (IsState(PlayerStateEnum.DundunReload_Start_Update))
                {
                    endCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.dundunReload_Loop);

                    if (CheckMainAnimIsFinished(endCFTime))
                    {
                        SetState(PlayerStateEnum.DundunReload_Loop_Init);
                        goto EndStates;
                    }

                    if (IsPlayerRunning())
                    {
                        SetState(PlayerStateEnum.IdleToRun_Init);
                        goto EndStates;
                    }

                    //<Keys>

                    #region action
                    if (CheckActionRequestAndShouldPickNewGun())
                    {
                        SetState(PlayerStateEnum.PickingNewGun_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region grenade
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Grenade()) //(GetButtonDown(keys.grenade))
                    {
                        if (CanThrowGrenade())
                        {
                            SetState(PlayerStateEnum.GrenadeGunDown_Init);
                            goto EndStates;
                        }
                    }
                    #endregion

                    #region melee
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Melee()) //(GetButtonDown(keys.melee))
                    {
                        SetState(PlayerStateEnum.Melee_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region changeGun
                    if (IsChangeGunKeyPressed())
                    {
                        SetState(PlayerStateEnum.GunDown_Init);
                        goto EndStates;
                    }
                    #endregion

                    //</Keys>
                }
                #endregion

                #region DundunReload_Loop_Init
                if (IsState(PlayerStateEnum.DundunReload_Loop_Init))
                {
                    canRun = false;

                    curAnimCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.dundunReload_Loop);

                    StartNewMainAnimWithCrossfadeTime(playerAnimationsList.dundunReload_Loop, curAnimCFTime);

                    reloadTimeCounter = curActiveGun.reloadTimeOnAnim;

                    shouldContinueDundunReload = !curActiveGun.IsItLastBulletOfReload();

                    didReloadingDone = false;

                    curActiveGun.PlaySound_Reload();

                    SetState(PlayerStateEnum.DundunReload_Loop_Update);
                }
                #endregion

                #region DundunReload_Loop_Update
                if (IsState(PlayerStateEnum.DundunReload_Loop_Update))
                {
                    if (shouldContinueDundunReload)
                        endCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.dundunReload_Loop);
                    else
                        endCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.dundunReload_End);

                    if (CheckMainAnimIsFinished(endCFTime))
                    {
                        if (!didReloadingDone)
                        {
                            curActiveGun.Reload(PlayerGunReloadMode.Dundun);
                            ReSetGunInfoHUD();
                        }

                        if (CanReload() && shouldContinueDundunReload)
                        {
                            SetState(PlayerStateEnum.DundunReload_Loop_Init);
                            goto EndStates;
                        }
                        else
                        {
                            SetState(PlayerStateEnum.DundunReload_End_Init);
                            goto EndStates;
                        }
                    }

                    reloadTimeCounter = MathfPlus.DecByDeltatimeToZero(reloadTimeCounter);

                    if (reloadTimeCounter == 0)
                    {
                        if (!didReloadingDone)
                        {
                            didReloadingDone = true;

                            curActiveGun.Reload(PlayerGunReloadMode.Dundun);
                            ReSetGunInfoHUD();
                        }
                    }

                    if (IsPlayerRunning())
                    {
                        SetState(PlayerStateEnum.IdleToRun_Init);
                        goto EndStates;
                    }

                    //<Keys>

                    #region action
                    if (CheckActionRequestAndShouldPickNewGun())
                    {
                        SetState(PlayerStateEnum.PickingNewGun_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region grenade
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Grenade()) //(GetButtonDown(keys.grenade))
                    {
                        if (CanThrowGrenade())
                        {
                            SetState(PlayerStateEnum.GrenadeGunDown_Init);
                            goto EndStates;
                        }
                    }
                    #endregion

                    #region melee
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Melee()) //(GetButtonDown(keys.melee))
                    {
                        SetState(PlayerStateEnum.Melee_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region changeGun
                    if (IsChangeGunKeyPressed())
                    {
                        SetState(PlayerStateEnum.GunDown_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region fire
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Fire()) //(GetButtonDown(keys.fire))
                    {
                        shouldContinueDundunReload = false;
                    }
                    #endregion

                    //</Keys>
                }
                #endregion

                #region DundunReload_End_Init
                if (IsState(PlayerStateEnum.DundunReload_End_Init))
                {
                    canRun = false;

                    curAnimCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.dundunReload_End);

                    StartNewMainAnimWithCrossfadeTime(playerAnimationsList.dundunReload_End, curAnimCFTime);

                    curActiveGun.PlaySound_ReloadChikkiEnd();

                    SetState(PlayerStateEnum.DundunReload_End_Update);
                }
                #endregion

                #region DundunReload_End_Update
                if (IsState(PlayerStateEnum.DundunReload_End_Update))
                {
                    endCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.idle);

                    if (CheckMainAnimIsFinished(endCFTime))
                    {
                        SetState(PlayerStateEnum.Idle_Init);
                        goto EndStates;
                    }

                    if (IsPlayerRunning())
                    {
                        SetState(PlayerStateEnum.IdleToRun_Init);
                        goto EndStates;
                    }

                    //<Keys>

                    #region action
                    if (CheckActionRequestAndShouldPickNewGun())
                    {
                        SetState(PlayerStateEnum.PickingNewGun_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region grenade
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Grenade()) //(GetButtonDown(keys.grenade))
                    {
                        if (CanThrowGrenade())
                        {
                            SetState(PlayerStateEnum.GrenadeGunDown_Init);
                            goto EndStates;
                        }
                    }
                    #endregion

                    #region melee
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Melee()) //(GetButtonDown(keys.melee))
                    {
                        SetState(PlayerStateEnum.Melee_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region changeGun
                    if (IsChangeGunKeyPressed())
                    {
                        SetState(PlayerStateEnum.GunDown_Init);
                        goto EndStates;
                    }
                    #endregion

                    //</Keys>
                }
                #endregion

                #region SnipeIdleToAim_Init
                if (IsState(PlayerStateEnum.SnipeIdleToAim_Init))
                {
                    SetNeedsToAim(false);

                    canRun = false;

                    curAnimCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.idleToAim);

                    StartNewMainAnimWithCrossfadeTime(playerAnimationsList.idleToAim, curAnimCFTime);

                    SetState(PlayerStateEnum.SnipeIdleToAim_Update);
                }
                #endregion

                #region SnipeIdleToAim_Update
                if (IsState(PlayerStateEnum.SnipeIdleToAim_Update))
                {
                    endCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.aimIdle);

                    if (CheckMainAnimIsFinished(endCFTime))
                    {
                        SetState(PlayerStateEnum.SnipeAim_Init);
                        goto EndStates;
                    }

                    //<Keys>

                    #region action
                    if (CheckActionRequestAndShouldPickNewGun())
                    {
                        SetState(PlayerStateEnum.PickingNewGun_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region fire
                    if (NeedsToFire())
                    {
                        curActiveGun.TryFire();
                        SetSnipeIsShooting(true);
                        ResetSnipeShootXYSitu();
                    }
                    else
                    {
                        if (ShouldPlayGunEmptySound())
                        {
                            SetFiringIsNotOk();
                            curActiveGun.PlaySound_Empty();
                        }
                    }
                    #endregion

                    #region grenade
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Grenade()) //(GetButtonDown(keys.grenade))
                    {
                        if (CanThrowGrenade())
                        {
                            SetState(PlayerStateEnum.GrenadeGunDown_Init);
                            goto EndStates;
                        }
                    }
                    #endregion

                    #region melee
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Melee()) //(GetButtonDown(keys.melee))
                    {
                        SetState(PlayerStateEnum.Melee_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region changeGun
                    if (IsChangeGunKeyPressed())
                    {
                        SetState(PlayerStateEnum.GunDown_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region reload
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Reload()) //(GetButtonDown(keys.reload))
                    {
                        if (CanReload())
                        {
                            SetState(GetReloadStateForCurrentGun());
                            goto EndStates;
                        }
                    }
                    #endregion

                    #region aim
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Aim()) //(GetButtonDown(keys.aim))
                    {
                        SetState(GetAimToIdleStateForCurrentGun());
                        goto EndStates;
                    }
                    #endregion

                    //</Keys>
                }
                #endregion

                #region SnipeAim_Init
                if (IsState(PlayerStateEnum.SnipeAim_Init))
                {
                    canRun = false;

                    StartSnipeMode();

                    SetState(PlayerStateEnum.SnipeAim_Update);
                }
                #endregion

                #region SnipeAim_Update
                if (IsState(PlayerStateEnum.SnipeAim_Update))
                {
                    if (ShouldAutomaticallyReload())
                    {
                        EndSnipeMode();

                        SetState(GetReloadStateForCurrentGun());
                        goto EndStates;
                    }

                    if (IsPayerMovingSlowOrNormal())
                    {
                        SetMovingWithAimedSnipe(true);
                    }
                    else
                    {
                        SetMovingWithAimedSnipe(false);
                    }

                    //<Keys>

                    #region action
                    if (CheckActionRequestAndShouldPickNewGun())
                    {
                        EndSnipeMode();

                        SetState(PlayerStateEnum.PickingNewGun_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region steady
                    if (CustomInputManager.KeyIfGameIsNotPaused_SnipeSteady()) //(GetButton(keys.sprint))
                    {
                        if (CanSnipeGoSteady())
                        {
                            PlaySnipeVoice_StartHabsingNafas();

                            SetShouldPlaySnipeBreathVoice(true);

                            SetSnipeIsSteady(true);
                        }
                    }
                    else
                    {
                        SetSnipeIsSteady(false);
                    }
                    #endregion

                    #region fire
                    if (NeedsToFire())
                    {
                        curActiveGun.TryFire();
                        SetSnipeIsShooting(true);
                        ResetSnipeShootXYSitu();

                        ReSetGunInfoHUD();
                    }
                    else
                    {
                        if (ShouldPlayGunEmptySound())
                        {
                            SetFiringIsNotOk();
                            curActiveGun.PlaySound_Empty();
                        }
                    }
                    #endregion

                    //#region grenade
                    //if (GetButtonDown(keys.grenade))
                    //{
                    //    if (CanThrowGrenade())
                    //    {
                    //        EndSnipeMode();

                    //        SetState(PlayerStateEnum.GrenadeGunDown_Init);
                    //        goto EndStates;
                    //    }
                    //}
                    //#endregion

                    #region changeGun
                    if (IsChangeGunKeyPressed())
                    {
                        EndSnipeMode();

                        SetState(PlayerStateEnum.GunDown_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region reload
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Reload()) //(GetButtonDown(keys.reload))
                    {
                        if (CanReload())
                        {
                            EndSnipeMode();

                            SetState(GetReloadStateForCurrentGun());
                            goto EndStates;
                        }
                    }
                    #endregion

                    #region aim
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Aim()) //(GetButtonDown(keys.aim))
                    {
                        EndSnipeMode();

                        SetState(GetAimToIdleStateForCurrentGun());
                        goto EndStates;
                    }
                    #endregion

                    #region SnipeTimeSpeedCtrl
                    if (IsStartSnipeTimeSpeedControllerRequested())
                    {
                        StartBeginningSnipeTimeSpeedController();
                    }
                    else
                    {
                        if (IsEndSnipeTimeSpeedControllerRequested())
                        {
                            StartEndingSnipeTimeSpeedController();
                        }
                    }
                    #endregion

                    //</Keys>
                }
                #endregion

                #region SnipeAimToIdle_Init
                if (IsState(PlayerStateEnum.SnipeAimToIdle_Init))
                {
                    canRun = false;

                    //curAnimCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.aimToIdle);

                    curAnimCFTime = 0;

                    StartNewMainAnimWithCrossfadeTime(playerAnimationsList.aimToIdle, curAnimCFTime);

                    SetState(PlayerStateEnum.SnipeAimToIdle_Update);
                }
                #endregion

                #region SnipeAimToIdle_Update
                if (IsState(PlayerStateEnum.SnipeAimToIdle_Update))
                {
                    endCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.idle);

                    if (CheckMainAnimIsFinished(endCFTime))
                    {
                        SetState(PlayerStateEnum.Idle_Init);
                        goto EndStates;
                    }

                    //<Keys>

                    #region action
                    if (CheckActionRequestAndShouldPickNewGun())
                    {
                        SetState(PlayerStateEnum.PickingNewGun_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region fire
                    if (NeedsToFire())
                    {
                        SetState(PlayerStateEnum.Fire_Init);
                        goto EndStates;
                    }
                    else
                    {
                        if (ShouldPlayGunEmptySound())
                        {
                            SetFiringIsNotOk();
                            curActiveGun.PlaySound_Empty();
                        }
                    }
                    #endregion

                    #region grenade
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Grenade()) //(GetButtonDown(keys.grenade))
                    {
                        if (CanThrowGrenade())
                        {
                            SetState(PlayerStateEnum.GrenadeGunDown_Init);
                            goto EndStates;
                        }
                    }
                    #endregion

                    #region melee
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Melee()) //(GetButtonDown(keys.melee))
                    {
                        SetState(PlayerStateEnum.Melee_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region changeGun
                    if (IsChangeGunKeyPressed())
                    {
                        SetState(PlayerStateEnum.GunDown_Init);
                        goto EndStates;
                    }
                    #endregion

                    #region reload
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Reload()) //(GetButtonDown(keys.reload))
                    {
                        if (CanReload())
                        {
                            SetState(GetReloadStateForCurrentGun());
                            goto EndStates;
                        }
                    }
                    #endregion

                    #region aim
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Aim()) //(GetButtonDown(keys.aim))
                    {
                        SetState(GetIdleToAimStateForCurrentGun());
                        goto EndStates;
                    }
                    #endregion

                    //</Keys>
                }
                #endregion

                #region PickingNewGun_Init
                if (IsState(PlayerStateEnum.PickingNewGun_Init))
                {
                    canRun = true;

                    ResetActionDelayTimer();

                    PickNewGun(newGunItemToPick);

                    ReSetGunInfoHUD();

                    ShowGunInfoHUDForAWhile();

                    curAnimCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.gunUp);

                    StartNewMainAnimWithCrossfadeTime(playerAnimationsList.gunUp, curAnimCFTime);

                    SetState(PlayerStateEnum.GunUp_Update);
                }
                #endregion

                #region Camp_Idle_Init
                if (IsState(PlayerStateEnum.Camp_Idle_Init))
                {
                    canRun = true;

                    if (IsPlayerRunning())
                    {
                        SetState(PlayerStateEnum.Camp_FastWalk_Init);
                        goto EndStates;
                    }

                    curAnimCFTime = camp_AnimToCampIdleCFTime;

                    if (isCampKnifeAttackedNow)
                    {
                        isCampKnifeAttackedNow = false;

                        curAnimCFTime = camp_AttackToCampIdleCFTime;
                    }

                    CampMode_StartNewMainAnimWithCrossfadeTime(playerAnimationsList.camp_Idle, curAnimCFTime);

                    SetState(PlayerStateEnum.Camp_Idle_Update);
                }
                #endregion

                #region Camp_Idle_Update
                if (IsState(PlayerStateEnum.Camp_Idle_Update))
                {
                    if (IsPlayerRunning())
                    {
                        SetState(PlayerStateEnum.Camp_FastWalk_Init);
                        goto EndStates;
                    }

                    //<Keys>

                    #region action
                    CampMode_CheckActionRequest();
                    #endregion

                    #region melee
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Melee()) //(GetButtonDown(keys.melee))
                    {
                        if (Camp_CanStartKnifeNow())
                        {
                            SetState(PlayerStateEnum.Camp_Knife_Init);
                            goto EndStates;
                        }
                    }
                    #endregion

                    #region fire
                    if (CampMode_NeedsToFire())
                    {
                        if (Camp_CanStartKnifeNow())
                        {
                            SetState(PlayerStateEnum.Camp_Knife_Init);
                            goto EndStates;
                        }
                    }
                    #endregion

                    //</Keys>
                }
                #endregion

                #region Camp_FastWalk_Init
                if (IsState(PlayerStateEnum.Camp_FastWalk_Init))
                {
                    canRun = true;

                    curAnimCFTime = camp_AnimToCampMoveFastCFTime;

                    CampMode_StartNewMainAnimWithCrossfadeTime(playerAnimationsList.camp_WalkFast, curAnimCFTime);

                    SetState(PlayerStateEnum.Camp_FastWalk_Update);
                }
                #endregion

                #region Camp_FastWalk_Update
                if (IsState(PlayerStateEnum.Camp_FastWalk_Update))
                {
                    if (!IsPlayerRunning())
                    {
                        SetSprintingIsNotOk();
                        SetState(PlayerStateEnum.Camp_Idle_Init);
                        goto EndStates;
                    }

                    //<Keys>

                    #region action
                    CampMode_CheckActionRequest();
                    #endregion

                    #region melee
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Melee()) //(GetButtonDown(keys.melee))
                    {
                        if (Camp_CanStartKnifeNow())
                        {
                            //SetSprintingIsNotOk();
                            SetState(PlayerStateEnum.Camp_Knife_Init);
                            goto EndStates;
                        }
                    }
                    #endregion

                    #region fire
                    if (CampMode_NeedsToFire())
                    {
                        if (Camp_CanStartKnifeNow())
                        {
                            SetState(PlayerStateEnum.Camp_Knife_Init);
                            goto EndStates;
                        }
                    }
                    #endregion

                    #region crouch
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Crouch()) //(GetButtonDown(keys.crouch))
                    {
                        SetSprintingIsNotOk();
                        SetState(PlayerStateEnum.Camp_Idle_Init);
                        goto EndStates;
                    }
                    #endregion

                    //</Keys>
                }
                #endregion

                #region Camp_Knife_Init
                if (IsState(PlayerStateEnum.Camp_Knife_Init))
                {
                    canRun = true;

                    curAnimCFTime = 0.1f;

                    CampMode_StartNewMainAnimWithCrossfadeTime(GetCampKnifeRandomAttackAnimName(), curAnimCFTime);

                    meleeAttackDelayTimeCounter = campMeleeAttackMaxDelay;

                    didMeleeAttackDone = false;

                    Camp_SetTimeBetweenKnifesMax();

                    PlayCampKnifeSound();

                    SetState(PlayerStateEnum.Camp_Knife_Update);
                }
                #endregion

                #region Camp_Knife_Update
                if (IsState(PlayerStateEnum.Camp_Knife_Update))
                {
                    endCFTime = camp_AttackToCampIdleCFTime;

                    if (CheckMainAnimIsFinished(endCFTime))
                    {
                        if (!didMeleeAttackDone)
                        {
                            TryDoMeleeAttackDamage();
                        }

                        //ResetBones(curActiveGun.positionBone, curActiveGun.rotationBone);

                        isCampKnifeAttackedNow = true;

                        SetState(PlayerStateEnum.Camp_Idle_Init);
                        goto EndStates;
                    }

                    meleeAttackDelayTimeCounter = MathfPlus.DecByDeltatimeToZero(meleeAttackDelayTimeCounter);

                    if (meleeAttackDelayTimeCounter == 0)
                    {
                        if (!didMeleeAttackDone)
                        {
                            didMeleeAttackDone = true;

                            TryDoMeleeAttackDamage();
                        }
                    }
                }
                #endregion

                #region KnifeGunDown_Init
                if (IsState(PlayerStateEnum.KnifeGunDown_Init))
                {
                    //needToFadeCross = true;

                    //canRun = false;

                    ////ReSetGunInfoHUD();

                    //curAnimCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.grenadeGunDown);

                    //StartNewMainAnimWithCrossfadeTime(playerAnimationsList.grenadeGunDown, curAnimCFTime);

                    MakeGunHidden(curActiveGun);

                    SetState(PlayerStateEnum.KnifeAttack_Init);
                }
                #endregion

                #region KnifeGunDown_Update
                if (IsState(PlayerStateEnum.KnifeGunDown_Update))
                {
                    endCFTime = 0;

                    if (CheckMainAnimIsFinished(endCFTime))
                    {
                        MakeGunHidden(curActiveGun);

                        SetState(PlayerStateEnum.KnifeAttack_Init);
                        goto EndStates;
                    }

                    //<Keys>

                    #region changeGun
                    if (IsChangeGunKeyPressed())
                    {
                        SetNeedsToChangeGun(true);
                    }
                    #endregion

                    #region aim
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Aim()) //(GetButtonDown(keys.aim))
                    {
                        SetNeedsToAim(true);
                    }
                    #endregion

                    //</Keys>
                }
                #endregion]

                #region KnifeAttack_Init
                if (IsState(PlayerStateEnum.KnifeAttack_Init))
                {
                    needToFadeCross = true;

                    canRun = false;

                    MakeKnifeAppear();

                    PlayKnifeAnim();

                    PlayKnifeSound();

                    meleeAttackDelayTimeCounter = meleeAttackMaxDelay;

                    didMeleeAttackDone = false;

                    ResetBones(knifePosRotBones.BonePosition, knifePosRotBones.BoneRotation);

                    SetState(PlayerStateEnum.KnifeAttack_Update);
                }
                #endregion

                #region KnifeAttack_Update
                if (IsState(PlayerStateEnum.KnifeAttack_Update))
                {
                    if (IsKnifeAnimFinished())
                    {
                        if (!didMeleeAttackDone)
                        {
                            TryDoMeleeAttackDamage();
                        }

                        MakeKnifeHidden();

                        ResetBones(curActiveGun.positionBone, curActiveGun.rotationBone);

                        SetState(PlayerStateEnum.KnifeGunUp_Init);
                        goto EndStates;
                    }

                    meleeAttackDelayTimeCounter = MathfPlus.DecByDeltatimeToZero(meleeAttackDelayTimeCounter);

                    if (meleeAttackDelayTimeCounter == 0)
                    {
                        if (!didMeleeAttackDone)
                        {
                            didMeleeAttackDone = true;

                            TryDoMeleeAttackDamage();
                        }
                    }

                    //<Keys>

                    #region changeGun
                    if (IsChangeGunKeyPressed())
                    {
                        SetNeedsToChangeGun(true);
                    }
                    #endregion

                    #region aim
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Aim()) //(GetButtonDown(keys.aim))
                    {
                        SetNeedsToAim(true);
                    }
                    #endregion

                    //</Keys>
                }
                #endregion

                #region KnifeGunUp_Init
                if (IsState(PlayerStateEnum.KnifeGunUp_Init))
                {
                    canRun = false;

                    MakeGunAppear(curActiveGun);
                    //MakeGrenadeHidden();

                    //curAnimCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.grenadeGunUp);

                    curAnimCFTime = 0;

                    StartNewMainAnimWithCrossfadeTime(playerAnimationsList.grenadeGunUp, curAnimCFTime);

                    SetState(PlayerStateEnum.KnifeGunUp_Update);
                }
                #endregion

                #region KnifeGunUp_Update
                if (IsState(PlayerStateEnum.KnifeGunUp_Update))
                {
                    endCFTime = GetAnimCrossfadeTime(curAnimName, playerAnimationsList.idle);

                    if (CheckMainAnimIsFinished(endCFTime))
                    {
                        SetState(PlayerStateEnum.Idle_Init);
                        goto EndStates;
                    }

                    //<Keys>

                    #region changeGun
                    if (IsChangeGunKeyPressed())
                    {
                        SetNeedsToChangeGun(true);
                    }
                    #endregion

                    #region aim
                    if (CustomInputManager.KeyDownIfGameIsNotPaused_Aim()) //(GetButtonDown(keys.aim))
                    {
                        SetNeedsToAim(true);
                    }
                    #endregion

                    //</Keys>
                }
                #endregion]

                #region FU_Init
                if (IsState(PlayerStateEnum.FU_Init))
                {
                    canRun = false;

                    curAnimCFTime = 0.4f;

                    StartNewMainAnimWithCrossfadeTime(playerAnimationsList.fu, curAnimCFTime);

                    SetState(PlayerStateEnum.FU_Update);
                }
                #endregion

                #region FU_Update
                if (IsState(PlayerStateEnum.FU_Update))
                {
                    endCFTime = 0.45f;

                    if (CheckMainAnimIsFinished(endCFTime))
                    {
                        SetState(PlayerStateEnum.Idle_Init);
                        goto EndStates;
                    }
                }
                #endregion

            EndStates: ;

                #endregion
            }

            #region Horiz
        StartHorizMoveStates:

            #region NoMove
            if (IsHorizMovementState(PlayerHorizMovementStateEnum.NoMove))
            {
                SetCharacterMotorMovementSpeeds(0, 0);
            }
            #endregion

            #region SlowMove
            if (IsHorizMovementState(PlayerHorizMovementStateEnum.SlowMove))
            {
                SetCharacterMotorMovementSpeeds(maxSlowMoveSpeed, maxSlowSideMoveSpeed);
            }
            #endregion

            #region NormalMove
            if (IsHorizMovementState(PlayerHorizMovementStateEnum.NormalMove))
            {
                if (!isCampPlayer)
                    SetCharacterMotorMovementSpeeds(maxNormalMoveSpeed, maxNormalSideMoveSpeed);
                else
                {
                    float spCoef = 1;

                    if (IsVertMovementState(PlayerVertMovementStateEnum.Stand))
                    {
                        spCoef = campStandMovingSpeedCoef;
                    }

                    SetCharacterMotorMovementSpeeds(spCoef * maxSlowMoveSpeed, spCoef * maxSlowSideMoveSpeed);
                }
            }
            #endregion

            #region FastMove
            if (IsHorizMovementState(PlayerHorizMovementStateEnum.FastMove))
            {
                if (!isCampPlayer)
                    SetCharacterMotorMovementSpeeds(maxFastMoveSpeed, maxFastSideMoveSpeed);
                else
                    SetCharacterMotorMovementSpeeds(maxNormalMoveSpeed, maxNormalSideMoveSpeed);
            }
            #endregion

        EndHorizMoveStates: ;
            #endregion

            #region Vert
        StartVertMoveStates:

            #region Jump
            if (IsVertMovementState(PlayerVertMovementStateEnum.Jump))
            {
                if (characterMotor.IsGrounded())
                {
                    SetVertMovementState(PlayerVertMovementStateEnum.Stand);
                }
            }
            #endregion

            #region StandToSit_Init
            if (IsVertMovementState(PlayerVertMovementStateEnum.StandToSit_Init))
            {
                characterMotor.jumping.enabled = false;

                StartStandToSitProcess();

                SetVertMovementState(PlayerVertMovementStateEnum.StandToSit);
            }
            #endregion

            #region StandToSit
            if (IsVertMovementState(PlayerVertMovementStateEnum.StandToSit))
            {
                characterMotor.jumping.enabled = false;

                if (IsStandToSitProcessFinished())
                {
                    SetVertMovementState(PlayerVertMovementStateEnum.Sit);
                    ReSetSitStandHUD();
                }
            }
            #endregion

            #region SitToStand_Init
            if (IsVertMovementState(PlayerVertMovementStateEnum.SitToStand_Init))
            {
                characterMotor.jumping.enabled = false;

                StartSitToStandProcess();

                SetVertMovementState(PlayerVertMovementStateEnum.SitToStand);
            }
            #endregion

            #region SitToStand
            if (IsVertMovementState(PlayerVertMovementStateEnum.SitToStand))
            {
                characterMotor.jumping.enabled = false;

                if (IsSitToStandProcessFinished())
                {
                    SetVertMovementState(PlayerVertMovementStateEnum.Stand);
                    ReSetSitStandHUD();
                }
            }
            #endregion

            #region Sit
            if (IsVertMovementState(PlayerVertMovementStateEnum.Sit))
            {
                //<Test> TESSSSSSSSSSSSSSTTTTTTTTTTTTTTTTTTTTTTTT!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

                characterMotor.jumping.enabled = false;

                //</Test>
            }
            #endregion

            #region Stand
            if (IsVertMovementState(PlayerVertMovementStateEnum.Stand))
            {
                //<Test> TESSSSSSSSSSSSSSTTTTTTTTTTTTTTTTTTTTTTTT!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

                characterMotor.jumping.enabled = true;

                //</Test>
            }
            #endregion

        EndVertMoveStates: ;
            #endregion

            #region HPState

        StartHPState:

            #region Normal_Init
            if (IsHPState(PlayerHPStateEnum.Normal_Init))
            {
                SetHPState(PlayerHPStateEnum.Normal_Update);
            }
            #endregion

            #region Normal_Update
            if (IsHPState(PlayerHPStateEnum.Normal_Update))
            {
                if (NeedsToChangeHPRegBecauseDamageRecieved())
                    SetCurrentHPRegen(recievingDamageHPRegen);
                else
                    SetCurrentHPRegen(normalHPRegen);

                PlayerHPStateEnum newHPState = CheckAndGetNewHPState();

                if (newHPState != hpState)
                {
                    SetHPState(newHPState);
                }
            }
            #endregion

            #region Low_Init
            if (IsHPState(PlayerHPStateEnum.Low_Init))
            {
                SetHPState(PlayerHPStateEnum.Low_Update);
            }
            #endregion

            #region Low_Update
            if (IsHPState(PlayerHPStateEnum.Low_Update))
            {
                PlayLowHPBreathingVoice();

                if (NeedsToChangeHPRegBecauseDamageRecieved())
                    SetCurrentHPRegen(recievingDamageHPRegen);
                else
                    SetCurrentHPRegen(normalHPRegen);

                PlayerHPStateEnum newHPState = CheckAndGetNewHPState();

                if (newHPState != hpState)
                {
                    SetHPState(newHPState);
                }
            }
            #endregion

            #region Critical_Init
            if (IsHPState(PlayerHPStateEnum.Critical_Init))
            {
                SetMaxCriticalHPStateTime();

                StartIncreasingCriticalAudioEffect(criticalAudioEffectStartingTime);

                StartIncreasingCriticalImageEffect(criticalImageEffectStartingTime);

                SetHPState(PlayerHPStateEnum.Critical_Update);
            }
            #endregion

            #region Critical_Update
            if (IsHPState(PlayerHPStateEnum.Critical_Update))
            {
                PlayCriticalHPBreathingVoice();

                if (NeedsToChangeHPRegBecauseDamageRecieved())
                    SetCurrentHPRegen(recievingDamageHPRegen);
                else
                    SetCurrentHPRegen(criticalHPRegen);

                PlayerHPStateEnum newHPState = CheckAndGetNewHPState();

                if (newHPState != hpState)
                {
                    SetHPState(newHPState);
                }
            }
            #endregion

            #region GettingBackFromCritical_Init
            if (IsHPState(PlayerHPStateEnum.GettingBackFromCritical_Init))
            {
                SetMaxGettingBackFromCriticalHpStateTime();

                StartDecreasingCriticalAudioEffect(criticalAudioEffectEndingTime);

                StartDecreasingCriticalImageEffect(criticalImageEffectEndingTime);

                SetHPState(PlayerHPStateEnum.GettingBackFromCritical_Update);
            }
            #endregion

            #region GettingBackFromCritical_Update
            if (IsHPState(PlayerHPStateEnum.GettingBackFromCritical_Update))
            {
                PlayLowHPBreathingVoice();

                SetCurrentHPRegen(gettingBackFromCriticalHPRegen);

                PlayerHPStateEnum newHPState = CheckAndGetNewHPState();

                if (newHPState != hpState)
                {
                    SetHPState(newHPState);
                }
            }
            #endregion



            #endregion

        EndHPState: ;

        }

    }

    bool IsState(PlayerStateEnum _state)
    {
        return state == _state;
    }

    void SetState(PlayerStateEnum _state)
    {
        state = _state;
    }

    bool IsCurrentAnimName(string _animName)
    {
        return curAnimName == _animName;
    }

    void SetCurrentAnimName(string _animName)
    {
        curAnimName = _animName;
    }

    public bool IsHorizMovementState(PlayerHorizMovementStateEnum _state)
    {
        return horizMovementState == _state;
    }

    public void SetHorizMovementState(PlayerHorizMovementStateEnum _state)
    {
        horizMovementState = _state;
    }

    public bool IsVertMovementState(PlayerVertMovementStateEnum _state)
    {
        return vertMovementState == _state;
    }

    public void SetVertMovementState(PlayerVertMovementStateEnum _state)
    {
        vertMovementState = _state;


    }

    public void SetGunAvailable(PlayerGunName _gunName)
    {
        PlayerGunName gunName = _gunName;

        foreach (PlayerGun plGun in guns)
        {
            if (plGun.gunName == gunName)
            {
                plGun.SetAvailable();

                return;
            }
        }
    }

    public void SetGunUnavailable(PlayerGunName _gunName)
    {
        PlayerGunName gunName = _gunName;

        foreach (PlayerGun plGun in guns)
        {
            if (plGun.gunName == gunName)
            {
                plGun.SetUnavailable();

                return;
            }
        }
    }

    public bool IsGunAvailable(PlayerGunName _gunName)
    {
        PlayerGunName gunName = _gunName;

        foreach (PlayerGun plGun in guns)
        {
            if (plGun.gunName == gunName)
            {
                if (plGun.isAvailable)
                    return true; ;
            }
        }

        return false;
    }

    public void SetActiveGun(PlayerGunName _gunName)
    {
        DeactiveCurrentActiveGun();

        PlayerGunName gunName = _gunName;

        foreach (PlayerGun plGun in guns)
        {
            if (plGun.gunName == gunName)
            {
                curActiveGun = plGun;

                curActiveGun.SetActive();

                curGunMeshObject = curActiveGun.meshObject;

                curGunNormalFOV = curActiveGun.normalFOV;

                curGunAimFOV = curActiveGun.aimFOV;

                SetCurrentFOV(curGunNormalFOV);

                if (curActiveGun.isSnipe)
                {
                    curActiveGun_SnipeInfo = curActiveGun.snipeInfo;
                }
                else
                {
                    curActiveGun_SnipeInfo = null;
                }

                if (curActiveGun.fovSwitchingSpeed != 0)
                    fovSwitchingSpeed = curActiveGun.fovSwitchingSpeed;
                else
                    fovSwitchingSpeed = (1 / curGunMeshObject.animation[playerAnimationsList.idleToAim].length);

                MakeGunAppear(curActiveGun);
                //curActiveGun.gunMesh.enabled = true;

                ResetBones(curActiveGun.positionBone, curActiveGun.rotationBone);

                ReInitGunInfoHUDForNewGun();

                ReSetGunInfoHUD();

                ShowGunInfoHUDForAWhile();

                ShowGunShapeHUDForAWhile();

                return;
            }
        }
    }

    public void DeactiveCurrentActiveGun()
    {
        if (curActiveGun != null)
        {
            MakeGunHidden(curActiveGun);

            curActiveGun.meshObject.animation.Stop();

            curActiveGun.SetDeactive();

            curActiveGun = null;
        }
    }

    void ChangeGun()
    {
        PlayerGun newGun = null;

        foreach (PlayerGun plGun in guns)
        {
            if (plGun.isAvailable && !plGun.isActive)
            {
                newGun = plGun;
                break;
            }
        }

        if (newGun == null)
            newGun = curActiveGun;

        DeactiveCurrentActiveGun();

        SetActiveGun(newGun.gunName);
    }

    void MakeGunHidden(PlayerGun _gun)
    {
        PlayerGun gn = _gun;

        SkinnedMeshRenderer[] sknRenderer = gn.meshObject.GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (SkinnedMeshRenderer skrn in sknRenderer)
        {
            skrn.enabled = false;
        }
        //gn.gunMesh.enabled = false;
    }

    void MakeGunAppear(PlayerGun _gun)
    {
        PlayerGun gn = _gun;

        SkinnedMeshRenderer[] sknRenderer = gn.meshObject.GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (SkinnedMeshRenderer skrn in sknRenderer)
        {
            skrn.enabled = true;
        }

        StartNewMainAnimWithCrossfadeTime(playerAnimationsList.idle, 0);
    }

    void MakeGrenadeHidden()
    {
        if (grenadeHandSkinnedRenderers != null)
        {
            foreach (SkinnedMeshRenderer skrn in grenadeHandSkinnedRenderers)
            {
                skrn.enabled = false;
            }
        }
    }

    void MakeGrenadeAppear()
    {
        if (grenadeHandSkinnedRenderers != null)
        {
            foreach (SkinnedMeshRenderer skrn in grenadeHandSkinnedRenderers)
            {
                skrn.enabled = true;
            }
        }
    }

    void MakeCampKnifeHidden()
    {
        if (campKnifeMeshObject)
        {
            SkinnedMeshRenderer[] sknRenderer = campKnifeMeshObject.GetComponentsInChildren<SkinnedMeshRenderer>();

            foreach (SkinnedMeshRenderer skrn in sknRenderer)
            {
                skrn.enabled = false;
            }
        }
    }

    void MakeCampKnifeAppear()
    {
        SkinnedMeshRenderer[] sknRenderer = campKnifeMeshObject.GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (SkinnedMeshRenderer skrn in sknRenderer)
        {
            skrn.enabled = true;
        }
    }

    void MakeKnifeHidden()
    {
        if (knifeMeshObject)
        {
            SkinnedMeshRenderer[] sknRenderer = knifeMeshObject.GetComponentsInChildren<SkinnedMeshRenderer>();

            foreach (SkinnedMeshRenderer skrn in sknRenderer)
            {
                skrn.enabled = false;
            }
        }
    }

    void MakeKnifeAppear()
    {
        SkinnedMeshRenderer[] sknRenderer = knifeMeshObject.GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (SkinnedMeshRenderer skrn in sknRenderer)
        {
            skrn.enabled = true;
        }
    }

    void SetCurrentFOV(float _value)
    {
        curFOV = _value;

        mainCam.fov = curFOV;
        fpsCamera.fov = curFOV;
    }

    void ResetBones(Transform _posBone, Transform _rotBone)
    {
        Transform posBone = _posBone;
        Transform rotBone = _rotBone;

        playerRotationX.SetBonesFromGuns(rotBone);

        movingObjsRotation.SetBonesFromGuns(posBone, rotBone);
    }

    void SetDefaultGuns()
    {
        List<PlayerGunName> stGuns = new List<PlayerGunName>();

        //stGuns.Add(PlayerGunName.Springfield);
        //stGuns.Add(PlayerGunName.Winchester);
        //stGuns.Add(PlayerGunName.MP18);
        //stGuns.Add(PlayerGunName.Luggar);

        if (gun1 == null)
        {
            Debug.LogError("Gun 1 is not set for player!");
            return;
        }

        stGuns.Add(gun1.gunName);

        if (gun2 != null)
        {
            stGuns.Add(gun2.gunName);
        }

        SetGuns(stGuns, gun1.gunName);
    }

    void RemoveAllGuns()
    {
        foreach (PlayerGun plGun in guns)
        {
            SetGunUnavailable(plGun.gunName);
        }

        DeactiveCurrentActiveGun();
    }

    public void SetGuns(List<PlayerGunName> _availableGuns, PlayerGunName _activeGun)
    {
        List<PlayerGunName> avGuns = _availableGuns;
        PlayerGunName acGun = _activeGun;

        foreach (PlayerGun plGun in guns)
        {
            if (avGuns.Contains(plGun.gunName))
            {
                SetGunAvailable(plGun.gunName);
            }
            else
            {
                SetGunUnavailable(plGun.gunName);
            }
        }

        SetActiveGun(acGun);
    }

    #region Anim Functions

    public void StartNewMainAnimWithCrossfadeTime(string _anim, float _startCrossfadeTime)
    {
        float cfTime = _startCrossfadeTime;

        mainAnim = _anim;
        SetCurrentAnimName(mainAnim);
        curGunMeshObject.animation[mainAnim].time = 0f;
        curGunMeshObject.animation.CrossFade(mainAnim, cfTime);
        mainAnimTimeCounter = Mathf.Max(curGunMeshObject.animation[mainAnim].length, cfTime);
        mainAnimLength = mainAnimTimeCounter;

        mainAnimCrossfadeTimeCounter = cfTime;
    }

    public void CampMode_StartNewMainAnimWithCrossfadeTime(string _anim, float _startCrossfadeTime)
    {
        float cfTime = _startCrossfadeTime;

        mainAnim = _anim;
        SetCurrentAnimName(mainAnim);

        campKnifeMeshObject.animation[mainAnim].time = 0;
        campKnifeMeshObject.animation.CrossFade(mainAnim, cfTime);
        mainAnimTimeCounter = Mathf.Max(campKnifeMeshObject.animation[mainAnim].length, cfTime);
        mainAnimLength = mainAnimTimeCounter;

        mainAnimCrossfadeTimeCounter = cfTime;
    }

    public bool IsFullyInNewMainAnim()
    {
        return mainAnimCrossfadeTimeCounter == 0;
    }

    public bool CheckMainAnimIsFinished(float _endCrossfadeTime)
    {
        float endCFTime = Mathf.Min(_endCrossfadeTime, mainAnimLength * minEndCFTimeCoef);

        if (mainAnimTimeCounter <= endCFTime)
        {
            mainAnimRemainingTime = mainAnimTimeCounter;
            return true;
        }

        mainAnimRemainingTime = mainAnimTimeCounter;
        return false;
    }

    void PlayKnifeAnim()
    {
        curKnifeAnimName = GetKnifeRandomAttackAnimName();

        knifeMeshObject.animation[curKnifeAnimName].time = 0;
        knifeMeshObject.animation.Play(curKnifeAnimName);
    }

    //void PlayCampKnifeAnim()
    //{
    //    curCampKnifeAnimName = GetCampKnifeRandomAttackAnimName();

    //    campKnifeMeshObject.animation[curCampKnifeAnimName].time = 0;
    //    campKnifeMeshObject.animation.Play(curCampKnifeAnimName);
    //}

    bool IsKnifeAnimFinished()
    {
        return !knifeMeshObject.animation.isPlaying;
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

    void StartNewAnimForObject(GameObject _obj, string _anim, float _startCrossfadeTime)
    {
        GameObject obj = _obj;
        string anim = _anim;
        float cfTime = _startCrossfadeTime;

        obj.animation[anim].time = 0;
        obj.animation.CrossFade(anim, cfTime);
    }

    bool IsUnloopedAnimFinishedOnObject(GameObject _obj, string _anim, float _endCFTime, out float _remainingEndTime)
    {
        return GeneralStats.IsUnloopedAnimFinishedOnObject(_obj, _anim, _endCFTime, out _remainingEndTime);
    }

    bool IsUnloopedAnimFinishedOnObject(GameObject _obj, string _anim, float _endCFTime)
    {
        return GeneralStats.IsUnloopedAnimFinishedOnObject(_obj, _anim, _endCFTime);
    }

    #endregion

    float GetAnimCrossfadeTime(string _oldAnimName, string _newAnimName)
    {
        string oldAnimName = _oldAnimName;
        string newAnimName = _newAnimName;

        float def = animCFTime_Default;

        if (newAnimName == oldAnimName)
        {
            return 0;
        }

        #region gunUp
        if (oldAnimName == playerAnimationsList.none)
        {
            if (newAnimName == playerAnimationsList.idle)
            {
                return CorrectCFTime(animCFTime_NoneToIdle, oldAnimName, newAnimName);
            }
        }
        #endregion

        if (newAnimName == playerAnimationsList.gunUp) // <- New!
        {
            return 0;
        }

        if (oldAnimName == playerAnimationsList.fu)
        {
            return 0.45f;
        }

        #region idle
        if (oldAnimName == playerAnimationsList.idle)
        {
            if (newAnimName == playerAnimationsList.idleToAim)
            {
                return animCFTime_Idle_To_IdleToAim;
            }
        }
        #endregion

        #region idleToAim
        if (oldAnimName == playerAnimationsList.idleToAim)
        {
            if (newAnimName == playerAnimationsList.aimIdle)
            {
                return animCFTime_IdleToAim_To_Aim;
            }

            if (newAnimName == playerAnimationsList.idle)
            {
                return animCFTime_IdleToAim_To_Idle;
            }
        }
        #endregion

        #region aimIdle
        if (oldAnimName == playerAnimationsList.aimIdle)
        {
            if (newAnimName == playerAnimationsList.idleToAim)
            {
                return animCFTime_Aim_To_IdleToAim;
            }
        }
        #endregion

        #region walk
        if (oldAnimName == playerAnimationsList.walk)
        {
            if (newAnimName == playerAnimationsList.idleToAim)
            {
                return animCFTime_Walk_To_IdleToAim;
            }
        }
        #endregion

        //#region run
        //if (oldAnimName == playerAnimationsList.run)
        //{
        //    if (newAnimName == playerAnimationsList.runToIdle)
        //    {
        //        return 0.15f;
        //    }
        //}

        //if (oldAnimName == playerAnimationsList.runToIdle)
        //{
        //    if (newAnimName == playerAnimationsList.run)
        //    {
        //        return 0.15f;
        //    }
        //}
        //#endregion

        return CorrectCFTime(def, oldAnimName, newAnimName);
    }

    void SetCharacterMotorMovementSpeeds(float _forwardSpeed, float _sideSpeed)
    {
        float forwSp = _forwardSpeed;
        float sideSp = _sideSpeed;

        characterMotor.movement.maxForwardSpeed = forwSp;
        characterMotor.movement.maxBackwardsSpeed = forwSp;
        characterMotor.movement.maxSidewaysSpeed = sideSp;
    }

    public float GetHorizVelocity()
    {
        Vector2 vel = new Vector2(characterMotor.movement.velocity.x, characterMotor.movement.velocity.z);

        return vel.magnitude;
    }

    public float GetTotalVelocity()
    {
        Vector3 vel = new Vector3(characterMotor.movement.velocity.x, characterMotor.movement.velocity.y, characterMotor.movement.velocity.z);

        return vel.magnitude;
    }

    public bool IsPlayerOnAir()
    {
        return !characterMotor.IsGrounded();
    }

    bool IsPayerOnlyMovingSlow()
    {
        float vel = GetHorizVelocity();
        return ((horizMovementState == PlayerHorizMovementStateEnum.SlowMove) && (vel >= minNormalMovementVelocity));
    }

    bool IsPayerOnlyMovingNormal()
    {
        float vel = GetHorizVelocity();
        return ((horizMovementState == PlayerHorizMovementStateEnum.NormalMove) && (vel >= minNormalMovementVelocity));
    }

    bool IsPayerMovingSlowOrNormal()
    {
        float vel = GetHorizVelocity();
        return ((horizMovementState == PlayerHorizMovementStateEnum.NormalMove || horizMovementState == PlayerHorizMovementStateEnum.SlowMove) && (vel >= minNormalMovementVelocity));
    }

    bool IsPlayerRunning()
    {
        return horizMovementState == PlayerHorizMovementStateEnum.FastMove;
    }

    //bool GetButton(string _buttonName)
    //{
    //    string buttonName = _buttonName;

    //    return inputController.GetButton(buttonName);
    //}

    //bool GetButtonDown(string _buttonName)
    //{
    //    string buttonName = _buttonName;

    //    return inputController.GetButtonDown(buttonName);
    //}

    //bool GetKey(KeyCode _keyCode)
    //{
    //    KeyCode keyCode = _keyCode;

    //    return inputController.GetKey(keyCode);
    //}

    //bool GetKeyDown(KeyCode _keyCode)
    //{
    //    KeyCode keyCode = _keyCode;

    //    return inputController.GetKeyDown(keyCode);
    //}

    bool NeedsReload()
    {
        return curActiveGun.NeedsReload();
    }

    bool CanReload()
    {
        return curActiveGun.CanReload();
    }

    bool IsShootingTimeFinished()
    {
        return curActiveGun.IsShootingTimeFinished();
    }

    bool ShouldAutomaticallyReload()
    {
        return (CanReload() && NeedsReload() && IsShootingTimeFinished());
    }

    bool CanThrowGrenade()
    {
        return currentNumOfGrenades > 0;
    }

    PlayerStateEnum GetReloadStateForCurrentGun()
    {
        if (curActiveGun.dundunReloadMode)
            return PlayerStateEnum.DundunReload_Start_Init;
        else
            return PlayerStateEnum.Reload_Init;
    }

    PlayerStateEnum GetIdleToAimStateForCurrentGun()
    {
        if (curActiveGun.isSnipe)
            return PlayerStateEnum.SnipeIdleToAim_Init;
        else
            return PlayerStateEnum.IdleToAim_Init;
    }

    PlayerStateEnum GetAimToIdleStateForCurrentGun()
    {
        if (curActiveGun.isSnipe)
            return PlayerStateEnum.SnipeAimToIdle_Init;
        else
            return PlayerStateEnum.AimToIdle_Init;
    }

    public void SetSprintingIsNotOk()
    {
        canRefreshSprintKey = false;
        sprintingOK = false;
    }

    public void SetFiringIsNotOk()
    {
        canRefreshFireKey = false;
        firingOK = false;
    }

    void SetNeedsToAim(bool _value)
    {
        needsToAim = _value;
    }

    void SetNeedsToChangeGun(bool _value)
    {
        needsToChangeGun = _value;
    }

    public void SetNumOfCurrentGrenades(int _quantity)
    {
        int quant = _quantity;

        currentNumOfGrenades = Mathf.Clamp(quant, 0, maxGrenadeCapacity);
    }

    public void AddGrenade(int _num)
    {
        int num = _num;
        SetNumOfCurrentGrenades(currentNumOfGrenades + num);
    }

    public void RemoveGrenade(int _num)
    {
        int num = _num;
        SetNumOfCurrentGrenades(currentNumOfGrenades - num);
    }

    float CorrectCFTime(float _value, string _oldAnimName, string _newAnimName)
    {
        string oldAnimName = _oldAnimName;
        string newAnimName = _newAnimName;
        float value = _value;

        if (oldAnimName == playerAnimationsList.none || newAnimName == playerAnimationsList.none)
            return 0;

        value = Mathf.Clamp(value, 0, maxCFTimeCoef * curGunMeshObject.animation[oldAnimName].length);
        value = Mathf.Clamp(value, 0, maxCFTimeCoef * curGunMeshObject.animation[newAnimName].length);
        value = Mathf.Clamp(value, minAnimCFTime, float.MaxValue);
        return value;
    }

    bool NeedsToFire()
    {
        if (firingOK && CustomInputManager.KeyIfGameIsNotPaused_Fire() && curActiveGun.IsReady()) //if (firingOK && GetButton(keys.fire) && curActiveGun.IsReady())
            return true;

        return false;
    }

    bool ShouldPlayGunEmptySound()
    {
        if (curActiveGun == null)
            return false;

        if (!firingOK)
            return false;

        if (!CustomInputManager.KeyIfGameIsNotPaused_Fire()) //(!GetButton(keys.fire))
            return false;

        if ((curActiveGun.GetCurrentBulletCount() + curActiveGun.GetCurrentMagazineCount()) == 0)
            return true;

        return false;
    }

    bool CampMode_NeedsToFire()
    {
        if (CustomInputManager.KeyIfGameIsNotPaused_Fire()) //(GetButton(keys.fire))
            return true;

        return false;
    }

    void StartZoomingFOV()
    {
        isZoomingFOV = true;
        isUnzoomingFOV = false;

        SetFOVIsZoomed();
    }

    void StartUnzoomingFOV()
    {
        isZoomingFOV = false;
        isUnzoomingFOV = true;

        SetFOVIsUnzoomed();
    }

    void StartSnipeFOV()
    {
        SetCurrentFOV(curActiveGun_SnipeInfo.SnipeModeFOV);

        SetFOVIsZoomed();
    }

    void EndSnipeFOV()
    {
        SetCurrentFOV(curActiveGun.normalFOV);

        SetFOVIsUnzoomed();
    }

    void SetFOVIsZoomed()
    {
        if (curActiveGun != null)
            curActiveGun.SetIsAimed(true);
    }

    void SetFOVIsUnzoomed()
    {
        if (curActiveGun != null)
            curActiveGun.SetIsAimed(false);
    }

    public void ApplyDamage(DamageInfo _dmg)
    {
        if (IsMissionFailed())
            return;

        DamageInfo dmg = _dmg;

        if (dmg.damageSource != null && dmg.damageSource.transform.root.tag.ToLower() == GeneralStats.allyTagName_ToLower)
        {
            return;
        }

        if (multiDmgCtrl.IsDamageAppliedBefore(dmg))
            return;

        multiDmgCtrl.AddDamage(dmg);

        float dmgAmount = -CorrectDamageToApply(dmg.damageAmount);

        dmgAmount *= GetNearBulletDamageCoef(dmg);

        ChangeHealth(dmgAmount);

        ShowDirectionalBlood(dmg.damageSourcePosition);

        SetMaxRecievedDamageHPRegTime();

        SetMaxCriticalHPStateTime();

        //StartBulletHitDang();

        HUD_AddDamageSide(dmg.damageSource.transform, dmg.damageSourcePosition);

        if (dmg.damageType == DamageType.Bullet)
        {
            ReSetBulletHitChance();
            PlayBulletImpactSound();

            BroadcastMessage("PlayerHitByBullet", dmg);
        }
    }

    public float CorrectDamageToApply(float _value)
    {
        float dmg = _value;

        float dmgDecCoef = (charInfo.MaxHealth - charInfo.CurrentHealth) / (charInfo.MaxHealth - curHealthCoefForMaxDamageDecreasement * charInfo.MaxHealth);

        dmgDecCoef = Mathf.Clamp01(dmgDecCoef);

        dmgDecCoef *= maxDamageDecreasementCoef;

        dmg -= dmgDecCoef * dmg;

        return dmg;
    }

    public void ChangeHealth(float _value)
    {
        float value = _value;

        charInfo.CurrentHealth += value;
        charInfo.CurrentHealth = Mathf.Clamp(charInfo.CurrentHealth, 0, charInfo.MaxHealth);

        float curHP = charInfo.CurrentHealth;
        float maxHP = charInfo.MaxHealth;

        if (curHP == 0)
            KillPlayer();

    }

    void SetHPState(PlayerHPStateEnum _state)
    {
        hpState = _state;
    }

    bool IsHPState(PlayerHPStateEnum _state)
    {
        return hpState == _state;
    }

    void ShowDirectionalBlood(Vector3 _dmgPos)
    {
        Vector3 dmgPos = _dmgPos;

        BulletDirection.DirState dirState = bulletDirection.CalculateBulletDir(dmgPos);

        int index = 0;

        switch (dirState)
        {
            case BulletDirection.DirState.West:
                index = 0;
                break;

            case BulletDirection.DirState.East:
                index = 1;
                break;

            case BulletDirection.DirState.North:
                index = 2;
                break;

            case BulletDirection.DirState.South:
                index = 3;
                break;

            case BulletDirection.DirState.NorthWest:
                index = 4;
                break;

            case BulletDirection.DirState.NorthEast:
                index = 5;
                break;

            case BulletDirection.DirState.SouthWest:
                index = 6;
                break;

            case BulletDirection.DirState.SouthEast:
                index = 7;
                break;
        }

        hud_DirectionalBlood.SetTextureStateForHUD(index);
    }

    public void KillPlayer()
    {
        if (IsMissionFailed())
            return;

        //<Temp>
        audioInfo.Stop();
        //</Temp>

        SetState(PlayerStateEnum.Die_Init);

        charInfo.IsDead = true;

        ShowHUD_MissionFail(true, MissionFailType.YouLeftFightArea);

        DoCommonMissionFailThings();
    }

    public void SetMissionFailedByOutMistake(MissionFailType _failType)
    {
        if (IsMissionFailed())
            return;

        missionFailType = _failType;

        isMissionFailedByOutMistake = true;

        SetState(PlayerStateEnum.MissionFailedByOutMistake_Init);

        DoCommonMissionFailThings();
    }

    void DoCommonMissionFailThings()
    {
        if (isOnSnipeMode)
            EndSnipeForDie();

        if (!isCampPlayer)
            MakeGunHidden(curActiveGun);
        else
            MakeCampKnifeHidden();

        MakeGrenadeHidden();

        StopPlayer(false);

        SetShouldRestartFromLastCheckpoint();

        MakeCommonHUDsInvisible();
    }

    void StartDyingEffects()
    {
        if (IsPlayer_Not_Standing())
            mainCam.animation.Play("SitDie");
        else
            mainCam.animation.Play("Die");

        StartMissionFailBlurEffect();
        StartMissionFailTimeScale();

        if (soundsList.Audio_Die != null && soundsList.Audio_Die.Length > 0)
            audioInfo.PlayClip(soundsList.Audio_Die);

        audioInfo.KillYourself();
    }

    void StartMissionFailByOutMistakeEffects()
    {
        //<Test>
        //showMissionFailedByOutMistakeGUI = true;

        //switch (missionFailType)
        //{
        //    case MissionFailType.AlliesNotSupported:
        //        missionFailByOutMistakeGUIString = "Mission failed. You didn't support your allies!";
        //        break;

        //    case MissionFailType.YouLeftFightArea:
        //        missionFailByOutMistakeGUIString = "Mission failed. You left the fight area!";
        //        break;

        //    case MissionFailType.YouAreDetectedByEnemies:
        //        missionFailByOutMistakeGUIString = "Mission failed. You are detected by enemies!";
        //        break;
        //}
        //</Test>

        ShowHUD_MissionFail(false, missionFailType);

        StartMissionFailBlurEffect();
        StartMissionFailTimeScale();
    }

    void StartMissionFailBlurEffect()
    {
        isMissionFailBlurStarted = true;
        missionFailBlurEffect.enabled = true;
        SetMissionFailBlurEffectIteration(0);
        SetMissionFailBlurEffectIntensity(0);
    }

    void SetMissionFailBlurEffectIteration(int _val)
    {
        missionFailBlurEffectCurIteration = Mathf.Clamp(_val, 0, missionFailBlurEffectMaxIteration);
        missionFailBlurEffect.iterations = missionFailBlurEffectCurIteration;
    }

    void SetMissionFailBlurEffectIntensity(float _val)
    {
        missionFailBlurEffectCurIntensity = Mathf.Clamp(_val, 0, missionFailBlurEffectMaxIntensity);
        missionFailBlurEffect.blurSpread = missionFailBlurEffectCurIntensity;
    }

    void StartMissionFailTimeScale()
    {
        isMissionFailTimeScaleStarted = true;
        missionFailTimeScaleCoef.currentValue = 1;
    }

    void SetMissionFailTimeScaleCoefValue(float _val)
    {
        missionFailTimeScaleCoef.currentValue = Mathf.Clamp(_val, missionFailTimeScaleCoefMin, 1);
        GameController.UpdateTimeScaleCoef(missionFailTimeScaleCoef);
    }

    public void StopPlayer(bool _isForCutscene)
    {
        FinishBulletHitDang();

        isPlayerStopped = true;

        characterMotor.enabled = false;

        isPlayerStopForCutscene = _isForCutscene;

        if (isPlayerStopForCutscene)
        {
            mainCam.enabled = false;
            fpsCamera.enabled = false;

            MutePlayerAudios();

            ChangeHealth(float.MaxValue);
            SetBloodAlpha(0);
            SetCurrentHPRegen(normalHPRegen);

            //if (isOnSnipeMode)
            //    EndSnipeModeForCutscene();

            if (!isCampPlayer)
                MakeGunHidden(curActiveGun);
            else
                MakeCampKnifeHidden();

            MakeGrenadeHidden();

            MakeCommonHUDsInvisible();

            if (isOnSnipeMode)
            {
                EndSnipeModeForCutscene();
            }
        }

    }

    public void RestartPlayer()
    {
        isPlayerStopped = false;

        characterMotor.enabled = true;

        mainCam.enabled = true;
        fpsCamera.enabled = true;

        UnMuePlayerAudios();

        if (!isCampPlayer)
            MakeGunAppear(curActiveGun);
        else
            MakeCampKnifeAppear();

        MakeGrenadeAppear();

        MakeCommonHUDsVisible();
    }

    public bool IsPlayerStopped()
    {
        return isPlayerStopped;
    }

    public void OnGamePause()
    {
        FinishBulletHitDang();

        isGamePaused = true;
    }

    public void OnGameUnpause()
    {
        isGamePaused = false;
    }

    public bool IsGamePaused()
    {
        return isGamePaused;
    }

    void SetBloodAlpha(float _value)
    {
        bloodAlpha = _value;

        playerBloodEffect.SetBloodAlpha(bloodAlpha);
    }

    void SetCriticalAudioEffectIntensity(float _value)
    {
        float val = Mathf.Clamp01(_value);

        criticalAudioEffectIntensity = val;

        SetAudioFilterLowPass(criticalAudioEffectIntensity);
    }

    void SetAudioFilterLowPass(float _value)
    {
        float val = _value;

        audioLowPassFilter.cutoffFrequency = audioLowPassEffectCurve.Evaluate(val);
    }

    void StartIncreasingCriticalAudioEffect(float _duration)
    {
        criticalAudioEffectChangeDurationTime = _duration;

        isIncreasingCriticalAudioEffect = true;
        isDecreasingCriticalAudioEffect = false;
    }

    void StartDecreasingCriticalAudioEffect(float _duration)
    {
        criticalAudioEffectChangeDurationTime = _duration;

        isIncreasingCriticalAudioEffect = false;
        isDecreasingCriticalAudioEffect = true;
    }

    void SetCriticalImageEffectIntensity(float _value)
    {
        float val = Mathf.Clamp01(_value);

        criticalImageEffectIntensity = val;

        criticalScreenImageEffect.SetAlpha(val);
    }

    void StartIncreasingCriticalImageEffect(float _duration)
    {
        criticalImageEffectChangeDurationTime = _duration;

        isIncreasingCriticalImageEffect = true;
        isDecreasingCriticalImageEffect = false;
    }

    void StartDecreasingCriticalImageEffect(float _duration)
    {
        criticalImageEffectChangeDurationTime = _duration;

        isIncreasingCriticalImageEffect = false;
        isDecreasingCriticalImageEffect = true;
    }

    void SetMaxRecievedDamageHPRegTime()
    {
        recieveDamageHPRegenTimeCounter = recievingDamageHPRegen_Time;
    }

    void SetMaxCriticalHPStateTime()
    {
        criticalHpStateTimeCounter = critical_Time;
    }

    void SetMaxGettingBackFromCriticalHpStateTime()
    {
        gettingBackFromCriticalHpStateTimeCounter = gettingBackFromCritical_Time;
    }

    bool NeedsToChangeHPRegBecauseDamageRecieved()
    {
        return recieveDamageHPRegenTimeCounter > 0;
    }

    void SetCurrentHPRegen(float _value)
    {
        curHPRegen = _value;
    }

    bool IsHPCriticalStateTimeFinished()
    {
        return criticalHpStateTimeCounter == 0;
    }

    bool IsGettingBackFromCriticalStateTimeFinished()
    {
        return gettingBackFromCriticalHpStateTimeCounter == 0;
    }

    PlayerHPStateEnum CheckAndGetNewHPState()
    {
        bool curHP_Low = false;
        bool curHP_Critical = false;
        bool curHP_WhileGettingBackCritical = false;

        float maxHP = charInfo.MaxHealth;
        float curHP = charInfo.CurrentHealth;

        float lowHP = maxHP * lowHPCoef;
        float critHP = maxHP * criticalHPCoef;
        float critHPWhileGettingBack = maxHP * criticalHPWhileGettingBackCoef;

        if (curHP < lowHP)
        {
            curHP_Low = true;
        }

        if (curHP < critHP)
        {
            curHP_Critical = true;
        }

        if (curHP < critHPWhileGettingBack)
        {
            curHP_WhileGettingBackCritical = true;
        }

        switch (hpState)
        {
            case PlayerHPStateEnum.Normal_Init:
            case PlayerHPStateEnum.Normal_Update:

                if (curHP_Critical)
                    return PlayerHPStateEnum.Critical_Init;

                if (curHP_Low)
                    return PlayerHPStateEnum.Low_Init;

                break;

            case PlayerHPStateEnum.Low_Init:
            case PlayerHPStateEnum.Low_Update:

                if (curHP_Critical)
                    return PlayerHPStateEnum.Critical_Init;

                if (!curHP_Low)
                    return PlayerHPStateEnum.Normal_Init;

                break;

            case PlayerHPStateEnum.Critical_Init:
            case PlayerHPStateEnum.Critical_Update:

                if (!curHP_Critical && IsHPCriticalStateTimeFinished())
                    return PlayerHPStateEnum.GettingBackFromCritical_Init;

                break;

            case PlayerHPStateEnum.GettingBackFromCritical_Init:
            case PlayerHPStateEnum.GettingBackFromCritical_Update:

                if (curHP_WhileGettingBackCritical)
                    return PlayerHPStateEnum.Critical_Init;

                if (IsGettingBackFromCriticalStateTimeFinished())
                {
                    if (curHP_Critical)
                        return PlayerHPStateEnum.Critical_Init;

                    if (curHP_Low)
                        return PlayerHPStateEnum.Low_Init;

                    return PlayerHPStateEnum.Normal_Init;
                }

                break;
        }

        return hpState;
    }

    void PlayLowHPBreathingVoice()
    {
        audioInfo.PlayClipIfReady(soundsList.Audio_PlayerBeDamagedLow);
    }

    void PlayCriticalHPBreathingVoice()
    {
        audioInfo.PlayClipIfReady(soundsList.Audio_PlayerBeDamagedCritical);
    }

    void PlayBulletImpactSound()
    {
        audioInfo_BulletImpact.PlayClip_OneShot(soundsList.Audio_BulletImpact);
    }

    void StartStandToSitProcess()
    {
        isPhysicsStandToSit = true;
        isMovingObjStandToSit = true;

        isPhysicsSitToStand = false;
        isMovingObjSitToStand = false;
    }

    void StartSitToStandProcess()
    {
        isPhysicsStandToSit = false;
        isMovingObjStandToSit = false;

        isPhysicsSitToStand = true;
        isMovingObjSitToStand = true;
    }

    void FixedUpdate()
    {
        if (isPhysicsStandToSit)
        {
            isPhysicsSitToStand = false;

            if (charControl.height != sitHeight)
                charControl.height = sitHeight;

            isPhysicsStandToSit = false;
        }

        if (isPhysicsSitToStand)
        {
            isPhysicsStandToSit = false;

            if (charControl.height < standHeight)
            {
                transform.position = new Vector3(transform.position.x,
                                                 transform.position.y + Time.deltaTime * (sittingSpeed / 2),
                                                 transform.position.z);

                charControl.height += Time.deltaTime * sittingSpeed;

                if (charControl.height >= standHeight)
                {
                    charControl.height = standHeight;

                    isPhysicsSitToStand = false;
                }
            }
            else
            {
                isPhysicsSitToStand = false;
            }
        }
    }

    bool IsStandToSitProcessFinished()
    {
        return (isPhysicsStandToSit == false && isMovingObjStandToSit == false);
    }

    bool IsSitToStandProcessFinished()
    {
        return (isPhysicsSitToStand == false && isMovingObjSitToStand == false);
    }

    void StartBulletHitDang()
    {
        bulletHitDangState = 1;
    }

    void FinishBulletHitDang()
    {
        bulletHitDangImageEffectTimeCounter = 0;

        SetBulletHitDangAlpha(0);

        bulletHitDangState = 0;
    }

    void SetBulletHitDangAlpha(float _value)
    {
        bulletHitDangAlpha = _value;
        bulletHitDangImageEffect.intensity = bulletHitDangAlpha;
    }

    void ReSetBulletHitChance()
    {
        bulletHitChanceTimeCounter = bulletHitChanceMaxDuration;
        bulletHitChanceCurNumOfStacks += 1;
        bulletHitChanceCurNumOfStacks = Mathf.Clamp(bulletHitChanceCurNumOfStacks, 0, bulletHitChanceMaxStacks);
        SetCurBulletHitChance(1 - bulletHitChanceCurNumOfStacks * bulletHitChance_DecreasementValue);
    }

    void SetCurBulletHitChance(float _val)
    {
        curBulletHitChance = _val;
    }

    public float GetCurBulletHitChance()
    {
        return curBulletHitChance;
    }

    void TryDoMeleeAttackDamage()
    {
        Vector3 startPos = meleeAttackHelper_Start.transform.position;
        Vector3 endPos = meleeAttackHelper_End.transform.position;

        Vector3 direction = endPos - startPos;

        float rayMagnitude = direction.magnitude;
        direction.Normalize();
        Ray ray = new Ray(startPos, direction);

        RaycastHit[] hits;

        GameObject ply = gameObject;

        hits = Physics.RaycastAll(ray, rayMagnitude, generalInfoHandler.HitLayer);

        if (hits != null && hits.Length > 0)
        {
            List<RaycastHit> sortedHits = new List<RaycastHit>();

            List<RaycastHit> unsortedHits = new List<RaycastHit>();

            foreach (RaycastHit rh in hits)
            {
                unsortedHits.Add(rh);
            }

            while (true)
            {
                int lowestInd = -1;
                float lowestDist = float.MaxValue;

                for (int j = 0; j < unsortedHits.Count; j++)
                {
                    float dist = Vector3.Magnitude(unsortedHits[j].point - startPos);

                    if (dist <= lowestDist)
                    {
                        lowestInd = j;
                        lowestDist = dist;
                    }
                }

                sortedHits.Add(unsortedHits[lowestInd]);
                unsortedHits.RemoveAt(lowestInd);

                if (unsortedHits.Count == 0)
                    break;
            }

            int i = 0;
            while (i < sortedHits.Count)
            {
                RaycastHit hitInfo = sortedHits[i];

                GameObject hittedObj = hitInfo.collider.transform.root.gameObject;

                if (GameObject.Equals(hittedObj, ply))
                {
                    i++;
                    continue;
                }

                if (hittedObj.tag.ToLower() != GeneralStats.enemyTagName_ToLower)
                {
                    goto End;
                }
                else
                {
                    break;
                }
            }
        }

        bool isKnifeHitAudioPlayed = false;

        foreach (GameObject go in meleeAttack_SoldierDetector.insideSolds)
        {
            if (go)
            {
                if (!isKnifeHitAudioPlayed)
                {
                    isKnifeHitAudioPlayed = true;

                    PlayKnifeHit();
                }

                SoldierInfo si = go.GetComponent<SoldierInfo>();

                DamageInfo dmgInfo = new DamageInfo();

                dmgInfo.bodyPart = SoldierBodyPart.UpFront;
                dmgInfo.damageSource = ply;
                dmgInfo.damageSourcePosition = startPos;
                dmgInfo.damageType = DamageType.Bullet;
                dmgInfo.damageAmount = meleeAttackDamage;
                dmgInfo.Impulse = meleeAttackImpulse;
                dmgInfo.BulletDirection = direction;
                dmgInfo.HitPoint = si.bodyInfo.soldierHeadTr.transform.position;

                go.SendMessageUpwards("ApplyDamage", dmgInfo);
            }
        }

    End: ;

    }

    void ThrowGrenade()
    {
        RemoveGrenade(1);

        GameObject gObj = Instantiate(grenade) as GameObject;

        gObj.transform.position = grenadeStartPos.position;

        Vector3 gDir = (grenadeDirEndPos.position - grenadeStartPos.position).normalized;

        Rigidbody gBody = gObj.transform.GetComponentInChildren<Rigidbody>();

        gBody.AddForceAtPosition(gDir * grenadeSpeed, gObj.transform.position, ForceMode.Impulse);

        float torqueX = Random.Range(-GeneralStats.grenadeMaxTorqueRangeX, GeneralStats.grenadeMaxTorqueRangeX);
        float torqueY = Random.Range(-GeneralStats.grenadeMaxTorqueRangeY, GeneralStats.grenadeMaxTorqueRangeY);
        float torqueZ = Random.Range(-GeneralStats.grenadeMaxTorqueRangeZ, GeneralStats.grenadeMaxTorqueRangeZ);

        Vector3 torque = new Vector3(torqueX, torqueY, torqueZ);
        gBody.AddTorque(torque, ForceMode.Impulse);
    }

    void SetGrenadeHandInitialPosAndRot()
    {
        GunBonesForPosAndRot initialgbfpar = grenadeHandMeshObject.GetComponent<GunBonesForPosAndRot>();
        Transform initialBonePos = initialgbfpar.BonePosition;

        grenadeHandMeshObject.transform.localPosition = new Vector3(-initialBonePos.localPosition.x, -initialBonePos.localPosition.y, -initialBonePos.localPosition.z);
        grenadeHandMeshObject.transform.localRotation = Quaternion.identity;
    }

    void SetCampKnifeHandInitialPosAndRot()
    {
        if (campKnifeMeshObject)
        {
            GunBonesForPosAndRot initialgbfpar = campKnifeMeshObject.GetComponent<GunBonesForPosAndRot>();
            Transform initialBonePos = initialgbfpar.BonePosition;

            campKnifeMeshObject.transform.localPosition = new Vector3(-initialBonePos.localPosition.x, -initialBonePos.localPosition.y, -initialBonePos.localPosition.z);
            campKnifeMeshObject.transform.localRotation = Quaternion.identity;
        }
    }

    void SetKnifeHandInitialPosAndRot()
    {
        if (knifeMeshObject)
        {
            GunBonesForPosAndRot initialgbfpar = knifeMeshObject.GetComponent<GunBonesForPosAndRot>();
            Transform initialBonePos = initialgbfpar.BonePosition;

            knifeMeshObject.transform.localPosition = new Vector3(-initialBonePos.localPosition.x, -initialBonePos.localPosition.y, -initialBonePos.localPosition.z);
            knifeMeshObject.transform.localRotation = Quaternion.identity;
        }
    }

    public void StartCustomExplosionCamShake(Transform _explosionTr, float _explosionRange, float _minPlayerDistToDoMaxShake, bool _showCameraDirtEffect)
    {
        Transform explosionTr = _explosionTr;
        float playerCameraShakeRange = _explosionRange;
        float playerCameraShake_MinDistToDoMaxShakeEee = _minPlayerDistToDoMaxShake;
        bool showCameraDirtEffect = _showCameraDirtEffect;

        Vector3 explosionPos = explosionTr.position;

        DamageInfo dmg = new DamageInfo();

        dmg.damageSource = explosionTr.gameObject;
        dmg.damageSourcePosition = explosionPos;
        dmg.damageType = DamageType.Explosion;

        PlayerCharacterNew player = this;

        float playerDist = (player.transform.position - explosionPos).magnitude;

        float makhraj = playerCameraShakeRange - playerCameraShake_MinDistToDoMaxShakeEee;
        float sourat = playerCameraShakeRange - playerDist;

        float result = Mathf.Clamp01(sourat / makhraj);

        ExplosionDamageInfo expDmgInf = new ExplosionDamageInfo();
        expDmgInf.damageInfo = dmg;
        expDmgInf.playerDamageSheddat = result;

        player.SetExplosionEffectIsNeeded(expDmgInf, showCameraDirtEffect);
    }

    public void SetExplosionEffectIsNeeded(ExplosionDamageInfo _expDmgInfo, bool _showDirtEffect)
    {
        ExplosionDamageInfo expDmgInfo = _expDmgInfo;

        float sheddat = expDmgInfo.playerDamageSheddat;

        if (sheddat == 0)
            return;

        if (sheddat < camExplosionCurrentShakeSheddat)
            return;

        if (_showDirtEffect)
            StartExplosionDirtEffect(sheddat, explosionDirtStartMaxTime);

        camExplosionInitialSheddat = Mathf.Clamp01(sheddat);
        camExplosionCurrentShakeSheddat = camExplosionInitialSheddat;

        camExplosionShakeTimeCounter = camExplosionShakeMaxTime;

        int i = 0;

        i = Random.Range(0, explosionCamCurves.Length);

        selectedExplosionCamShakeInfo = explosionCamCurves[i];

        StartCamExplosionShake();

        BroadcastMessage("PlayerHitByExplosion", expDmgInfo);
    }

    void StartCamExplosionShake()
    {
        isCameraShakingByExplosion = true;
    }

    void EndCamExplosionShake()
    {
        isCameraShakingByExplosion = false;

        camExplosionShakeTimeCounter = 0;
        camExplosionCurrentShakeSheddat = 0;

        camExplosionShakeY = 0;
        camExplosionShakeZ = 0;
    }

    public PlayerGun GetActiveGun()
    {
        return curActiveGun;
    }

    public void CheckPointLoadTransition()
    {
        if (PlayerController.LastCheckPoint_IsLvlCampPlayer)
        {
            ResetPlayerCampStatus(true);
        }
        else
        {
            ResetPlayerCampStatus(false);

            PlayerGunName primGunName = PlayerController.LastCheckPoint_PrimGun;
            PlayerGunName secGunName = PlayerController.LastCheckPoint_SecGun;

            List<PlayerGunName> stGuns = new List<PlayerGunName>();

            stGuns.Add(primGunName);

            if (PlayerController.LastCheckPoint_HaveSecGun)
                stGuns.Add(secGunName);

            SetGuns(stGuns, primGunName);

            foreach (PlayerGun plGun in guns)
            {
                if (plGun.gunName == primGunName)
                {
                    plGun.SetCurrentBulletCount(PlayerController.LastCheckPoint_PrimGunCurBulletCount);
                    plGun.SetCurrentMagazineCount(PlayerController.LastCheckPoint_PrimGunCurMagCount);
                }
                else
                {
                    if (PlayerController.LastCheckPoint_HaveSecGun)
                    {
                        if (plGun.gunName == secGunName)
                        {
                            plGun.SetCurrentBulletCount(PlayerController.LastCheckPoint_SecGunCurBulletCount);
                            plGun.SetCurrentMagazineCount(PlayerController.LastCheckPoint_SecGunCurMagCount);
                        }
                    }
                }
            }
        }

        SetNumOfCurrentGrenades(PlayerController.LastCheckPoint_CurGrenadeCount);

        if (!isCampPlayer)
            ReSetGunInfoHUD();
    }

    public void CheckPointSaveTransition()
    {
        PlayerController.LastCheckPoint_IsLvlCampPlayer = isCampPlayer;

        if (!isCampPlayer)
        {
            PlayerController.LastCheckPoint_PrimGun = curActiveGun.gunName;
            PlayerController.LastCheckPoint_PrimGunCurBulletCount = curActiveGun.GetCurrentBulletCount();
            PlayerController.LastCheckPoint_PrimGunCurMagCount = curActiveGun.GetCurrentMagazineCount();

            PlayerGun secGun = null;

            foreach (PlayerGun plGun in guns)
            {
                if (plGun.isAvailable && !plGun.isActive)
                {
                    secGun = plGun;
                    break;
                }
            }

            if (secGun == null)
            {
                PlayerController.LastCheckPoint_HaveSecGun = false;

                PlayerController.LastCheckPoint_SecGun = PlayerGunName.FourLool;
                PlayerController.LastCheckPoint_SecGunCurBulletCount = 0;
                PlayerController.LastCheckPoint_SecGunCurMagCount = 0;
            }
            else
            {
                PlayerController.LastCheckPoint_HaveSecGun = true;

                PlayerController.LastCheckPoint_SecGun = secGun.gunName;
                PlayerController.LastCheckPoint_SecGunCurBulletCount = secGun.GetCurrentBulletCount();
                PlayerController.LastCheckPoint_SecGunCurMagCount = secGun.GetCurrentMagazineCount();
            }
        }

        PlayerController.LastCheckPoint_CurGrenadeCount = currentNumOfGrenades;
    }

    void StartSnipeMode()
    {
        isOnSnipeMode = true;

        MakeGunHidden(curActiveGun);

        StartSnipeFOV();

        ShowSnipeHUD();

        ResetSnipeLarzeshXY();

        doSnipeLarzeshing = true;

        doSnipeMovingCamShake = true;

        doSnipeShootCamShake = true;
    }

    void EndSnipeMode()
    {
        SetSnipeIsSteady(false);

        isOnSnipeMode = false;

        MakeGunAppear(curActiveGun);

        EndSnipeFOV();

        HideSnipeHUD();

        ResetSnipeLarzeshXY();

        doSnipeLarzeshing = false;

        doSnipeMovingCamShake = false;

        doSnipeShootCamShake = false;

        if (CanEndSnipeTimeSpeedController())
        {
            StartEndingSnipeTimeSpeedController();
        }
    }

    void EndSnipeForDie()
    {
        SetSnipeIsSteady(false);

        isOnSnipeMode = false;

        EndSnipeFOV();

        HideSnipeHUD();

        if (CanEndSnipeTimeSpeedController())
        {
            EndSnipeTimeSpeedControllerNow();
        }
    }

    void EndSnipeModeForCutscene()
    {
        SetSnipeIsSteady(false);

        isOnSnipeMode = false;

        //MakeGunAppear(curActiveGun);

        EndSnipeFOV();

        HideSnipeHUD();

        ResetSnipeLarzeshXY();

        doSnipeLarzeshing = false;

        doSnipeMovingCamShake = false;

        doSnipeShootCamShake = false;

        if (CanEndSnipeTimeSpeedController())
        {
            EndSnipeTimeSpeedControllerNow();
        }

        SetState(PlayerStateEnum.Idle_Init);

    }

    void ShowSnipeHUD()
    {
        hud_SnipeCross.enabled = true;
    }

    void HideSnipeHUD()
    {
        hud_SnipeCross.enabled = false;
    }

    void ResetSnipeLarzeshXY()
    {
        snipeLarzXSinMot = 0;
        snipeLarzYSinMot = 0;
    }

    bool CanSnipeGoSteady()
    {
        return snipeSteadyTimeCounter == 0;
    }

    void SetSnipeIsSteady(bool _value)
    {
        snipe_IsSteady = _value;
    }

    void SetMovingWithAimedSnipe(bool _value)
    {
        isPlayerMovingWithAimedSnipe = _value;
    }

    void SetSnipeIsShooting(bool _value)
    {
        isSnipeShooting = _value;
    }

    void SetSnipeSteadyTimeCounter(float _value)
    {
        snipeSteadyTimeCounter = Mathf.Clamp(_value, 0, snipeMaxSteadyTime);
    }

    void SetSnipeCurSteadyAimValue(float _value)
    {
        snipeCurSteadyAimValue = Mathf.Clamp(_value, 0, 1);
    }

    void SetSnipeShadidBreathingAfterSteady(bool _value)
    {
        snipe_ShadidBreathingAfterSteady = _value;
    }

    bool CanPlaySnipeShadidBreathing()
    {
        if (charInfo.IsDeadOrDisabled())
            return false;

        return snipe_ShadidBreathingAfterSteady;
    }

    void SetShouldPlaySnipeBreathVoice(bool _value)
    {
        shouldPlaySnipeBreathVoice = _value;
    }

    bool CanPlaySnipeMellowBreathing()
    {
        if (charInfo.IsDeadOrDisabled())
            return false;

        return (shouldPlaySnipeBreathVoice && (snipeSteadyTimeCounter >= snipeMellowBreathingMinNeededTime));
    }

    void PlaySnipeVoice_StartHabsingNafas()
    {
        audioInfo.PlayClipIfReady(soundsList.Audio_SnipeNafasHabs);
    }

    void PlaySnipeVoice_NafasNafas_Shadid()
    {
        audioInfo.PlayClipIfReady(soundsList.Audio_SnipeShadidBreathing);
    }

    void PlaySnipeVoice_NafasNafas_Mellow()
    {
        audioInfo.PlayClipIfReady(soundsList.Audio_SnipeMellowBreathing);
    }

    void PlaySnipeVoice_SteadyHeartBeat()
    {
        audioInfo_SniperHeartBit.PlayClipIfReady(soundsList.Audio_SnipeHeartBeat);
    }

    void ResetSnipeShootXYSitu()
    {
        snipeShootX = 0;
        snipeShootY = 0;

        snipeShootingXDone = false;
        snipeShootingYDone = false;
    }

    bool IsPlayerGrounded()
    {
        return charControl.isGrounded;
    }

    void PlayLandingSound(string _surfaceMaterial)
    {
        audioInfo_FootStep_Landing.PlayClip_OneShot(footStepSoundList.GetLandAudio(_surfaceMaterial));
    }

    void PlayFootStepSound(string _surfaceMaterial, PlayerFootEnum _foot)
    {
        string surfaceMaterial = _surfaceMaterial;
        PlayerFootEnum foot = _foot;

        AudioClip[] audios = footStepSoundList.GetFootStepAudio(surfaceMaterial, foot);

        //footStep_LastAudioIndex++;

        //if (footStep_LastAudioIndex >= audios.Length)
        //    footStep_LastAudioIndex = 0;

        if (foot == PlayerFootEnum.Right)
        {
            audioInfo_FootStep_Right.SetCustomPitch(Random.Range(footStepSoundMinPitch, footStepSoundMaxPitch));
            audioInfo_FootStep_Right.PlayClip_OneShot(audios);
        }
        else
        {
            audioInfo_FootStep_Left.SetCustomPitch(Random.Range(footStepSoundMinPitch, footStepSoundMaxPitch));
            audioInfo_FootStep_Left.PlayClip_OneShot(audios);
        }
    }

    string GentlyGetUnderFootSurfaceMaterial()
    {
        if (gentlyGetUnderFootSurfaceMaterial_TimeCounter == 0)
        {
            gentlyGetUnderFootSurfaceMaterial_TimeCounter = gentlyGetUnderFootSurfaceMaterial_Max;

            string mat = GetUnderFootSurfaceMaterial();

            return mat;
        }

        print(lastUnderfootSurfaceMaterial);

        return lastUnderfootSurfaceMaterial;
    }

    string GetUnderFootSurfaceMaterial()
    {
        lastUnderfootSurfaceMaterial = "";

        Vector3 startPos = underFootRaycastStartPos.position;
        Vector3 endPos = underFootRaycastEndPos.position;

        Vector3 direction = endPos - startPos;
        float rayMagnitude = direction.magnitude;
        direction.Normalize();
        Ray ray = new Ray(startPos, direction);

        RaycastHit[] hits;

        GameObject ply = gameObject;

        hits = Physics.RaycastAll(ray, rayMagnitude, generalInfoHandler.UnderFootSurfaceLayer);


        List<RaycastHit> sortedHits = new List<RaycastHit>();
        List<RaycastHit> unsortedHits = new List<RaycastHit>();

        if (hits != null && hits.Length > 0)
        {

            foreach (RaycastHit rh in hits)
            {
                unsortedHits.Add(rh);
            }

            while (true)
            {
                int lowestInd = -1;
                float lowestDist = float.MaxValue;

                for (int j = 0; j < unsortedHits.Count; j++)
                {
                    float dist = Vector3.Magnitude(unsortedHits[j].point - startPos);

                    if (dist <= lowestDist)
                    {
                        lowestInd = j;
                        lowestDist = dist;
                    }
                }

                sortedHits.Add(unsortedHits[lowestInd]);
                unsortedHits.RemoveAt(lowestInd);

                if (unsortedHits.Count == 0)
                    break;
            }
        }


        int i = 0;
        while (i < sortedHits.Count)
        {
            RaycastHit hitInfo = hits[i];

            GameObject hittedObj = hitInfo.collider.gameObject;

            GameObject hittedObjRoot = hitInfo.collider.transform.root.gameObject;

            if (GameObject.Equals(hittedObjRoot, ply))
            {
                i++;
                continue;
            }

            lastUnderfootSurfaceMaterial = hittedObj.tag.ToLower();

            //print(hittedObjRoot);
            return lastUnderfootSurfaceMaterial;
        }

        //print(lastUnderfootSurfaceMaterial);

        return lastUnderfootSurfaceMaterial;
    }

    bool ShouldPlayFootstepSound()
    {
        bool timeIsOk = (footStep_TimeCounter == 0);
        float vel = GetHorizVelocity();

        if (timeIsOk && (vel >= footStep_MinNeedVelocity))
        {
            if (!isCampPlayer)
                return true;
            else
                return IsPlayerRunning();
        }

        return false;
    }

    bool ShouldResetFootstepTimeCounter()
    {
        float vel = GetHorizVelocity();
        return (vel < footStep_MinNeedVelocity);
    }

    void StartExplosionDirtEffect(float _alpha, float _time)
    {
        isOnExplosionDirtEffect = true;

        explosionDirtEffect.enabled = true;

        SetExplosionDirtEffectAlpha(_alpha);
        explosionDirtStartTimeCounter = _time;
    }

    void EndExplosionDirtEffect()
    {
        isOnExplosionDirtEffect = false;

        explosionDirtEffect.enabled = false;

        SetExplosionDirtEffectAlpha(0);
        explosionDirtStartTimeCounter = 0;
    }

    void SetExplosionDirtEffectAlpha(float _value)
    {
        curExplosionDirtEffectAlpha = _value;
        explosionDirtEffect.SetAlpha(curExplosionDirtEffectAlpha);
    }

    bool IsPlayer_Not_Standing()
    {
        return (vertMovementState == PlayerVertMovementStateEnum.Sit || vertMovementState == PlayerVertMovementStateEnum.SitToStand || vertMovementState == PlayerVertMovementStateEnum.StandToSit);
    }

    float GetNearBulletDamageCoef(DamageInfo _damage)
    {
        DamageInfo dmg = _damage;

        if (dmg.damageType != DamageType.Bullet)
            return 1;

        float dist = Vector3.Distance(dmg.HitPoint, dmg.damageSourcePosition);

        if (dist > nearMaxDistanceToTakeMoreDamageFromSolds)
            return 1;

        if (dist < nearMinDistanceToTakeMoreDamageFromSolds)
            return nearDamageMaxCoef;

        float x = (dist - nearMinDistanceToTakeMoreDamageFromSolds) / (nearMaxDistanceToTakeMoreDamageFromSolds - nearMinDistanceToTakeMoreDamageFromSolds);

        return (nearDamageMaxCoef - x * (nearDamageMaxCoef - 1));
    }

    public bool ShouldShowCompass()
    {
        if (!compass)
            return false;

        if (!compass.enabled)
            return false;

        if (mapLogic.isCutsceneMode)
            return false;

        if (IsMissionFailed())
            return false;

        return true;
    }

    //

    bool IsActionRequested()
    {
        if (CustomInputManager.KeyDownIfGameIsNotPaused_Action()) //(GetButtonDown(keys.action))
        {
            if (IsForwardObjectDetectorReadyAndContainsSomething())
            {
                return true;
            }
        }

        return false;
    }

    bool IsForwardObjectDetectorReadyAndContainsSomething()
    {
        return IsForwardObjectDetectorReadyToCheck() && forwardObjectDetector.DoesContainAnything();
    }

    bool IsForwardObjectDetectorReadyToCheck()
    {
        return forwardObjectDetectionTimeCounter == 0;
    }

    bool ISRequestedActionAnObjective()
    {
        return forwardObjectDetector.DoesContainAnyLogicObjectives();
    }

    bool ISRequestedActionAnItem()
    {
        return (!ISRequestedActionAnObjective() && forwardObjectDetector.DoesContainAnyItem());
    }

    void ResetActionDelayTimer()
    {
        forwardObjectDetectionTimeCounter = forwardObjectDetectionDelay;
    }

    LogicObjective GetLogicObjectiveFromList()
    {
        List<LogicObjective> list = forwardObjectDetector.InsideLogicObjectives;

        foreach (LogicObjective lo in list)
        {
            if (lo.IsActive)
            {
                return lo;
            }
        }

        return null;
    }

    void SetLogicObjectiveIsDone(LogicObjective _logObj)
    {
        LogicObjective logObj = _logObj;

        logObj.SetDone();
        ResetActionDelayTimer();
    } //forwardDetector Dorost nashode!!!!!!!!!!!!!!!!!

    bool IsReqItemPickableAmmo(Item _item)
    {
        Item item = _item;

        //    if (((item.itemType == ItemType.Bullet || item.itemType == ItemType.Gun) && IsGunAvailable(item.gunName))
        //      || item.itemType == ItemType.Grenade)
        //    {
        //        return true;
        //    }


        if (((item.itemType == ItemType.Bullet) && IsGunAvailable(item.gunName))
            || item.itemType == ItemType.Grenade)
        {
            return true;
        }

        return false;
    }

    bool IsReqItemAmmoButNotPickable(Item _item)
    {
        Item item = _item;

        return (item.itemType == ItemType.Bullet && !IsGunAvailable(item.gunName));
    }

    void AddAmmoFromItem(Item _ammoItem)
    {
        Item ammoItem = _ammoItem;

        if (ammoItem.itemType == ItemType.Grenade)
        {
            int countBeforeAdd = currentNumOfGrenades;

            AddGrenade(ammoItem.ammoCount);

            int countDifAfterAdd = currentNumOfGrenades - countBeforeAdd;

            UpdateItemAmmoCount(ammoItem, ammoItem.ammoCount - countDifAfterAdd);

            if (countDifAfterAdd > 0)
            {
                ShowHUD_AmmoPickInfo(HUDAmmoPickInfoType.GrenadesAdded, PlayerGunName.FourLool);
                PlaySound_PickUpAmmo();
            }
            else
            {
                ShowHUD_AmmoPickInfo(HUDAmmoPickInfoType.GrenadesFull, PlayerGunName.FourLool);
                PlaySound_PickUpZeroAmmo();
            }

            ReSetGunInfoHUD();
            return;
        }

        if (ammoItem.itemType == ItemType.Bullet) // || ammoItem.itemType == ItemType.Gun)
        {
            PlayerGunName gunName = ammoItem.gunName;

            foreach (PlayerGun plGun in guns)
            {
                if (plGun.gunName == gunName)
                {
                    int countBeforeAdd = plGun.GetCurrentBulletCount();

                    plGun.SetCurrentBulletCount(countBeforeAdd + ammoItem.ammoCount);

                    int countDifAfterAdd = plGun.GetCurrentBulletCount() - countBeforeAdd;

                    UpdateItemAmmoCount(ammoItem, ammoItem.ammoCount - countDifAfterAdd);

                    if (countDifAfterAdd > 0)
                    {
                        ShowHUD_AmmoPickInfo(HUDAmmoPickInfoType.GunAmmoAdded, gunName);
                        PlaySound_PickUpAmmo();
                    }
                    else
                    {
                        ShowHUD_AmmoPickInfo(HUDAmmoPickInfoType.GunAmmoFull, gunName);
                        PlaySound_PickUpZeroAmmo();
                    }

                    ReSetGunInfoHUD();
                    return;
                }
            }
        }
    }

    bool IsReqItemANewGun(Item _item)
    {
        Item item = _item;

        if (!(item.itemType == ItemType.Gun))
            return false;

        return !IsGunAvailable(item.gunName);
    }

    bool CheckActionRequestAndShouldPickNewGun()
    {
        if (IsActionRequested())
        {
            if (ISRequestedActionAnObjective())
            {
                LogicObjective logObj = GetLogicObjectiveFromList();
                SetLogicObjectiveIsDone(logObj);
                ResetActionDelayTimer();
            }
            else
            {
                if (ISRequestedActionAnItem())
                {
                    Item itm = forwardObjectDetector.GetFirstOKItemFromList();

                    if (IsReqItemPickableAmmo(itm))
                    {
                        AddAmmoFromItem(itm);
                        ResetActionDelayTimer();
                    }
                    else
                    {
                        if (IsReqItemAmmoButNotPickable(itm))
                        {
                            //HUD_UnavailableAmmoRequestedFromItem(itm);
                        }
                        else
                        {
                            if (IsReqItemANewGun(itm))
                            {
                                newGunItemToPick = itm;
                                return true;
                            }
                        }
                    }
                }
            }
        }

        return false;
    }

    void CheckBottomObjectDetector()
    {
        if (DoesBottomObjectDetectorContainItem())
        {
            Item itm = bottomObjectDetector.GetFirstOKItemFromList();

            if ((itm.itemType == ItemType.Gun) && IsGunAvailable(itm.gunName))
            {
                PlayerGunName gunName = itm.gunName;

                foreach (PlayerGun plGun in guns)
                {
                    if (plGun.gunName == gunName)
                    {
                        int countBeforeAdd = plGun.GetCurrentBulletCount();

                        plGun.SetCurrentBulletCount(countBeforeAdd + itm.ammoCount);

                        int countDifAfterAdd = plGun.GetCurrentBulletCount() - countBeforeAdd;

                        if (countDifAfterAdd > 0)
                        {
                            itm.UpdateAmmoCount(0);

                            //HUD_GunAmmoAdded(gunName, countDifAfterAdd);

                            ShowHUD_AmmoPickInfo(HUDAmmoPickInfoType.GunAmmoAdded, gunName);

                            ReSetGunInfoHUD();

                            PlaySound_PickUpAmmo();
                        }

                        return;
                    }
                }
            }
        }
    }

    void CampMode_CheckActionRequest()
    {
        if (IsActionRequested())
        {
            if (ISRequestedActionAnObjective())
            {
                LogicObjective logObj = GetLogicObjectiveFromList();
                SetLogicObjectiveIsDone(logObj);
                ResetActionDelayTimer();
            }
        }
    }

    void UpdateItemAmmoCount(Item _item, int _newAmmoCount)
    {
        Item item = _item;
        int newAmmoCount = _newAmmoCount;
        item.UpdateAmmoCount(newAmmoCount);
    }

    void PickNewGun(Item _gunItem)
    {
        Item gunItem = _gunItem;
        int gunItemAmmoCount = gunItem.ammoCount;
        PlayerGunName newGun = gunItem.gunName;

        bool shouldEjectOldGun = (GetNumOfAvaiableGuns() == maxNumOfGuns);

        //

        GameObject newGunItemGO = gunItem.GetRootGameObject();
        ItemRootObject newGunItemGO_ItemRootObj = newGunItemGO.GetComponent<ItemRootObject>();

        if (shouldEjectOldGun)
        {
            GameObject oldGunItemGO = GameObject.Instantiate(curActiveGun.itemGameObject, newGunItemGO.transform.position, newGunItemGO.transform.rotation) as GameObject;
            Item oldGunItem = oldGunItemGO.GetComponent<ItemRootObject>().childItem;

            oldGunItem.ammoCount = curActiveGun.GetCurrentBulletCount() + curActiveGun.GetCurrentMagazineCount();
        }

        newGunItemGO_ItemRootObj.SetFinished();

        //

        if (shouldEjectOldGun)
        {
            SetGunUnavailable(curActiveGun.gunName);
        }

        DeactiveCurrentActiveGun();
        SetGunAvailable(newGun);
        SetActiveGun(newGun);

        curActiveGun.SetCurrentMagazineCount(gunItemAmmoCount);
        curActiveGun.SetCurrentBulletCount(gunItemAmmoCount - curActiveGun.GetCurrentMagazineCount());

        //HUD_GunPicked(curActiveGun.gunName);

        PlaySound_PickUpGun();
    }

    public void ReplaceGun(PlayerGunName _newGun, int _ammoCount, PlayerGunName _oldGun)
    {
        int gunItemAmmoCount = _ammoCount;

        //bool shouldEjectOldGun = (GetNumOfAvaiableGuns() == maxNumOfGuns);

        //

        //GameObject newGunItemGO = gunItem.GetRootGameObject();
        //ItemRootObject newGunItemGO_ItemRootObj = newGunItemGO.GetComponent<ItemRootObject>();

        //if (shouldEjectOldGun)
        //{
        //    GameObject oldGunItemGO = GameObject.Instantiate(curActiveGun.itemGameObject, newGunItemGO.transform.position, newGunItemGO.transform.rotation) as GameObject;
        //    Item oldGunItem = oldGunItemGO.GetComponent<ItemRootObject>().childItem;

        //    oldGunItem.ammoCount = curActiveGun.GetCurrentBulletCount() + curActiveGun.GetCurrentMagazineCount();
        //}

        //newGunItemGO_ItemRootObj.SetFinished();

        //

        //if (shouldEjectOldGun)
        //{
        SetGunUnavailable(_oldGun);
        //}

        DeactiveCurrentActiveGun();
        SetGunAvailable(_newGun);
        SetActiveGun(_newGun);

        curActiveGun.SetCurrentMagazineCount(gunItemAmmoCount);
        curActiveGun.SetCurrentBulletCount(gunItemAmmoCount - curActiveGun.GetCurrentMagazineCount());

        ReSetGunInfoHUD();

        //HUD_GunPicked(curActiveGun.gunName);

        //PlaySound_PickUpGun();
    }

    void PlaySound_PickUpGun()
    {
        audioInfo_Act.PlayClip_OneShot(soundsList.Audio_PickUpGun);
    }

    void PlaySound_PickUpAmmo()
    {
        audioInfo_Act.PlayClip_OneShot(soundsList.Audio_PickUpAmmo);
    }

    void PlaySound_PickUpZeroAmmo()
    {
        audioInfo_Act.PlayClip_OneShot(soundsList.Audio_ZeroAmmoPicked);
    }

    bool DoesBottomObjectDetectorContainItem()
    {
        return bottomObjectDetector.DoesContainAnyItem();
    }

    //

    //void HUD_UnavailableAmmoRequestedFromItem(Item _ammoItem)
    //{
    //    //<Test>
    //    GUIStyle st = new GUIStyle();
    //    st.fontSize = 50;
    //    st.fontStyle = FontStyle.Bold;
    //    StartGUIGunPickText("No " + _ammoItem.gunName + " to add ammo!!!", st);
    //    //</Test>
    //}

    //void HUD_GrenadesAdded(int _count)
    //{
    //    //<Test>
    //    GUIStyle st = new GUIStyle();
    //    st.fontSize = 30;
    //    st.fontStyle = FontStyle.Italic;
    //    StartGUIGunPickText(_count + " grenades added.", st);
    //    //</Test>
    //}

    //void HUD_GunAmmoAdded(PlayerGunName _gunName, int _count)
    //{
    //    //<Test>
    //    GUIStyle st = new GUIStyle();
    //    if (_count > 0)
    //    {
    //        st.fontSize = 30;
    //        st.fontStyle = FontStyle.Italic;

    //        StartGUIGunPickText(_count + " ammo added to " + _gunName + ".", st);
    //    }
    //    else
    //    {
    //        st.fontSize = 50;
    //        st.fontStyle = FontStyle.Bold;

    //        StartGUIGunPickText(_gunName + " is full of ammo!!!", st);
    //    }

    //    //</Test>
    //}

    //void HUD_GunPicked(PlayerGunName _gunName)
    //{
    //    //<Test>
    //    GUIStyle st = new GUIStyle();

    //    st.fontSize = 35;
    //    st.fontStyle = FontStyle.BoldAndItalic;

    //    StartGUIGunPickText(_gunName + " picked.", st);
    //    //</Test>
    //}

    int GetNumOfAvaiableGuns()
    {
        int numOfGuns = 0;

        for (int i = 0; i < guns.Length; i++)
        {
            if (guns[i].isAvailable)
                numOfGuns++;
        }

        return numOfGuns;
    }

    void OnGUI()
    {
        GUI.depth = -850;

        if (ShouldShowCompass())
            ShowHUD_ObjsInMiniMap();

        if (CanShowHUD_DamageSides())
            ShowHUD_DamageSides();

        if (CanShowHUD_NearGrenades())
            ShowHUD_NearGrenades();

        if (CanShowHUD_3DObjectives())
            ShowHUD_3DObjectives();

        if (CanShowHUD_LvlCamp_OnGUIClockHandle())
            ShowHUD_LvlCamp_ClockHandle();

        //if (gunPickGUICounter > 0)
        //{
        //    float additionalFontSize = Mathf.Clamp((gunPickGUICounter - 1.85f) * Mathf.Abs((gunPickGUICounter - 1.85f)), 1f, 1000f);
        //    GUIStyle st = new GUIStyle();
        //    st.fontSize = (int)(gunPickGUIStyle.fontSize * additionalFontSize);
        //    st.fontStyle = gunPickGUIStyle.fontStyle;
        //    GUI.TextField(new Rect((Screen.width * 0.17f), Screen.height * 0.84f, 1200, 400), gunPickGUIString, st);
        //}

        //if (showMissionFailedByOutMistakeGUI)
        //{
        //    GUIStyle st = new GUIStyle();
        //    st.fontSize = 40;
        //    st.fontStyle = FontStyle.Bold;
        //    GUI.TextField(new Rect((Screen.width * 0.2f), Screen.height * 0.45f, 1200, 400), missionFailByOutMistakeGUIString, st);
        //}



        if (showHUDCross && !IsGamePaused() && !mapLogic.isCutsceneMode && !IsMissionFailed())
        {
            if (curActiveGun != null && curActiveGun.doesContainTarget)
            {
                if (needToFadeCross)
                {
                    Color c = GUI.color;
                    c.a = hudCrossAlpha;
                    GUI.color = c;

                    hudCrossAlpha -= Time.deltaTime * fadeCrossSpeed;
                    hudCrossAlpha = Mathf.Clamp(hudCrossAlpha, 0f, 1f);
                }
                else
                {
                    Color c = GUI.color;
                    c.a = hudCrossAlpha;
                    GUI.color = c;

                    hudCrossAlpha += Time.deltaTime * fadeoutCrossSpeed;
                    hudCrossAlpha = Mathf.Clamp(hudCrossAlpha, 0f, 1f);
                }

                float wth = Screen.height * hudCrossScreenHeightSize;
                float valFromDispAng = curActiveGun.currentDispersionAngle * hudCross_DispersionToScreenCoef * Screen.height;

                Rect rightRect = new Rect(Screen.width / 2 - wth / 2 + valFromDispAng, Screen.height / 2 - wth / 2, wth, wth);
                Rect leftRect = new Rect(Screen.width / 2 - wth / 2 - valFromDispAng, Screen.height / 2 - wth / 2, wth, wth);
                Rect topRect = new Rect(Screen.width / 2 - wth / 2, Screen.height / 2 - wth / 2 - valFromDispAng, wth, wth);
                Rect botRect = new Rect(Screen.width / 2 - wth / 2, Screen.height / 2 - wth / 2 + valFromDispAng, wth, wth);

                Texture2D Cross_Top = crossTextures.HUD_Cross_Normal_Top;
                Texture2D Cross_Bot = crossTextures.HUD_Cross_Normal_Bot;
                Texture2D Cross_Right = crossTextures.HUD_Cross_Normal_Right;
                Texture2D Cross_Left = crossTextures.HUD_Cross_Normal_Left;

                Vector3 start = PlayerCharacterNew.Instance.mainCam.ScreenToWorldPoint(new Vector3(Screen.width / 2,
                                                            Screen.height / 2, PlayerCharacterNew.Instance.mainCam.nearClipPlane));

                Ray camRay = new Ray(start, PlayerCharacterNew.Instance.mainCam.transform.forward);
                RaycastHit hitInfo;

                if (Physics.Raycast(camRay, out hitInfo, maxDistanceView, generalInfoHandler.HitLayer))
                {
                    string rootTag = hitInfo.collider.transform.root.tag.ToLower();

                    if (rootTag == GeneralStats.enemyTagName_ToLower)
                    {
                        Cross_Bot = crossTextures.HUD_Cross_Enemy_Bot;
                        Cross_Top = crossTextures.HUD_Cross_Enemy_Top;
                        Cross_Right = crossTextures.HUD_Cross_Enemy_Right;
                        Cross_Left = crossTextures.HUD_Cross_Enemy_Left;
                    }

                    if (rootTag == GeneralStats.allyTagName_ToLower)
                    {
                        Cross_Bot = crossTextures.HUD_Cross_Ally_Bot;
                        Cross_Top = crossTextures.HUD_Cross_Ally_Top;
                        Cross_Right = crossTextures.HUD_Cross_Ally_Right;
                        Cross_Left = crossTextures.HUD_Cross_Ally_Left;
                    }
                }

                GUI.DrawTexture(rightRect, Cross_Right);
                GUI.DrawTexture(leftRect, Cross_Left);
                GUI.DrawTexture(topRect, Cross_Top);
                GUI.DrawTexture(botRect, Cross_Bot);
            }
        }

        GUIOldTime = Time.time;
    }

    //void StartGUIGunPickText(string _txt, GUIStyle _Style)
    //{
    //    gunPickGUICounter = gunPickGUICounterMaxVal;
    //    gunPickGUIString = _txt;
    //    gunPickGUIStyle = _Style;
    //}

    public bool IsMissionFailed()
    {
        if (charInfo.IsDead)
            return true;

        if (isMissionFailedByOutMistake)
            return true;

        return false;
    }

    //

    bool IsStartSnipeTimeSpeedControllerRequested()
    {
        if (!IsSnipeTimeSpeedControllerReadyToStart())
            return false;

        //if (GetButtonDown(keys.snipeTimeSpeedController))
        //    return true;

        if (CustomInputManager.KeyDownIfGameIsNotPaused_SnipeTimeControl())//(GetKeyDown(KeyCode.Mouse2))
            return true;

        return false;
    }

    bool IsEndSnipeTimeSpeedControllerRequested()
    {
        if (!CanEndSnipeTimeSpeedController())
            return false;

        if (snipeBetweenOnAndOffDelayCounter != 0)
            return false;

        //if (GetButtonDown(keys.snipeTimeSpeedController))
        //    return true;

        if (CustomInputManager.KeyDownIfGameIsNotPaused_SnipeTimeControl()) //(GetKeyDown(KeyCode.Mouse2))
            return true;

        return false;
    }

    bool IsSnipeTimeSpeedControllerReadyToStart()
    {
        if (snipeTimeScaleStat != SnipeTimeScaleStatus.Off)
            return false;

        if (snipeTimeSpeedDelayTimeCounter != 0)
            return false;

        return true;
    }

    bool CanEndSnipeTimeSpeedController()
    {
        if (snipeTimeScaleStat == SnipeTimeScaleStatus.Off)
            return false;

        return true;
    }

    void StartBeginningSnipeTimeSpeedController()
    {
        GameController.isMouseMiddleClickUsedInSnipe = true;

        snipeTimeScaleStat = SnipeTimeScaleStatus.Starting;
        snipeTimeSpeedDelayTimeCounter = 1000;
        snipeTimeSpeedTimeCounter = snipeTimeSpeedMaxTime;

        snipeBetweenOnAndOffDelayCounter = snipeBetweenOnAndOffDelayMaxTime;
    }

    void StartStayingSnipeTimeSpeedController()
    {
        snipeTimeScaleStat = SnipeTimeScaleStatus.On;
        snipeTimeSpeedDelayTimeCounter = 1000;
        snipeTimeSpeedTimeCounter = snipeTimeSpeedMaxTime;

        SetSnipeAdditionalFireTimeCoef(snipeAdditionalFireTimeCoefInSlowSpeedMode);
    }

    void StartEndingSnipeTimeSpeedController()
    {
        snipeTimeScaleStat = SnipeTimeScaleStatus.Ending;
        snipeTimeSpeedDelayTimeCounter = 1000;
        snipeTimeSpeedTimeCounter = snipeTimeSpeedMaxTime;

        SetSnipeAdditionalFireTimeCoef(1);
    }

    void EndSnipeTimeSpeedControllerNow()
    {
        SetSnipeTimeScaleCoefValue(1);

        snipeTimeScaleStat = SnipeTimeScaleStatus.Off;
        snipeTimeSpeedDelayTimeCounter = snipeTimeSpeedDelayMaxTime;
        snipeTimeSpeedTimeCounter = 0;

        SetSnipeAdditionalFireTimeCoef(1);
    }

    void SetSnipeTimeScaleCoefValue(float _val)
    {
        snipeTimeScaleCoef.currentValue = Mathf.Clamp(_val, snipeTimeScaleCoefMin, 1);
        GameController.UpdateTimeScaleCoef(snipeTimeScaleCoef);
    }

    void SetSnipeAdditionalFireTimeCoef(float _val)
    {
        curActiveGun.SetCurFireTimeAdditionalCoef(_val);
    }

    void IncreaseRunningTime()
    {
        runningTimeIncreasedInLastUpdate = true;
        SetRunningTime(runningTimeCounter + Time.deltaTime);
    }

    void DecreaseRunningTime()
    {
        if (runningTimeIncreasedInLastUpdate)
            runningTimeIncreasedInLastUpdate = false;
        else
            SetRunningTime(runningTimeCounter - Time.deltaTime * runningRegenCoef);
    }

    void SetRunningTime(float _val)
    {
        runningTimeCounter = _val;
        runningTimeCounter = Mathf.Clamp(runningTimeCounter, 0, maxRunningTime);
    }

    void DecreaseBetweenRunsDelayTime()
    {
        SetBetweenRunsDelayTime(betweenRunsDelayTimeCounter - Time.deltaTime);
    }

    void SetBetweenRunsDelayTime(float _val)
    {
        betweenRunsDelayTimeCounter = _val;
        betweenRunsDelayTimeCounter = Mathf.Clamp(betweenRunsDelayTimeCounter, 0, maxDelayTimeBetweenRuns);
    }

    bool IsPlayerTiredOfRunning()
    {
        return (runningTimeCounter == maxRunningTime);
    }

    void SetPlayerShouldTakeABreathFromRunning()
    {
        playerShouldTakeABreathFromRunning = true;
        runningBreakState = 1;
    }

    void SetPlayerBreathingIsDoneForRunning()
    {
        playerShouldTakeABreathFromRunning = false;
        runningBreakState = 0;
        SetRunningTime(0);
    }

    void PlayRunningBreakVoice()
    {
        audioInfo.PlayClipIfReady(soundsList.Audio_RunningBreakBreathing);
    }

    bool Camp_CanStartKnifeNow()
    {
        return campTimeBetweenKnifesCounter == 0;
    }

    void Camp_SetTimeBetweenKnifesMax()
    {
        campTimeBetweenKnifesCounter = campMaxTimeBetweenKnifes;
    }

    public void ResetPlayerCampStatus(bool _isCampPlayer)
    {
        bool oldCampSitu = isCampPlayer;

        isCampPlayer = _isCampPlayer;

        if (oldCampSitu == isCampPlayer)
            return;

        //Specific!!!!!!
        if (oldCampSitu && !isCampPlayer) // Camp -> Normal
        {
            CampNormalSwitch_CopyFromReference(false);
            MakeCampKnifeHidden();
        }
        else                              // Normal -> Camp
        {
            CampNormalSwitch_CopyFromReference(true);
            RemoveAllGuns();

            ResetBones(campKnifePosRotBones.BonePosition, campKnifePosRotBones.BoneRotation);
        }

        //General!!!!!!!
        campTimeBetweenKnifesCounter = 0;
        camp_DidPlayerMakeALoudLandingSound = false;
        camp_DidPlayerMakeALoudMovingSound = false;

        LocalInit_SetGuns();
        LocalInit_SetInitialCharMotorSpeeds();
        LocalInit_SetInitialState();
        LocalInit_SetInitialCustomVolumes();
    }

    void LocalInit_SetGuns()
    {
        if (!isCampPlayer)
        {
            SetDefaultGuns();
        }
        else
        {
            MakeCampKnifeAppear();
        }
    }

    void LocalInit_SetInitialCharMotorSpeeds()
    {
        if (!isCampPlayer)
        {
            characterMotor.movement.maxForwardSpeed = maxNormalMoveSpeed;
            characterMotor.movement.maxSidewaysSpeed = maxNormalSideMoveSpeed;
        }
        else
        {
            characterMotor.movement.maxForwardSpeed = maxSlowMoveSpeed;
            characterMotor.movement.maxSidewaysSpeed = maxSlowSideMoveSpeed;
        }
    }

    void LocalInit_SetInitialState()
    {
        if (!isCampPlayer)
            SetState(PlayerStateEnum.Idle_Init);
        else
            SetState(PlayerStateEnum.Camp_Idle_Init);
    }

    void LocalInit_SetInitialCustomVolumes()
    {
        if (!isCampPlayer)
        {
            audioInfo_FootStep_Right.SetCustomVolume(1);
            audioInfo_FootStep_Left.SetCustomVolume(1);
            audioInfo_FootStep_Landing.SetCustomVolume(1);
        }
        else
        {
            audioInfo_FootStep_Right.SetCustomVolume(campLevelDefaultCustomVolume);
            audioInfo_FootStep_Left.SetCustomVolume(campLevelDefaultCustomVolume);
            audioInfo_FootStep_Landing.SetCustomVolume(campLevelDefaultCustomVolume);
        }
    }

    void CampNormalSwitch_CopyFromReference(bool _normalToCamp)
    {
        bool normalToCamp = _normalToCamp;

        PlayerCharacterNew refPlayer;

        if (normalToCamp)
        {
            refPlayer = normalToCampReferencePlayer;
        }
        else
        {
            refPlayer = campToNormalReferencePlayer;
        }

        maxFastMoveSpeed = refPlayer.maxFastMoveSpeed;
        maxFastSideMoveSpeed = refPlayer.maxFastSideMoveSpeed;
        maxNormalMoveSpeed = refPlayer.maxNormalMoveSpeed;
        maxNormalSideMoveSpeed = refPlayer.maxNormalSideMoveSpeed;
        maxSlowMoveSpeed = refPlayer.maxSlowMoveSpeed;
        maxSlowSideMoveSpeed = refPlayer.maxSlowSideMoveSpeed;

        footStepDelayGraph = refPlayer.footStepDelayGraph;
    }

    void SetShouldRestartFromLastCheckpoint()
    {
        shouldRestartFromLastCheckpoint = true;
    }

    string GetKnifeRandomAttackAnimName()
    {

        return playerAnimationsList.knifeAttack01;

    }

    string GetCampKnifeRandomAttackAnimName()
    {
        //int rnd = Random.Range((int)1, (int)3);

        //switch (rnd)
        //{
        //    case 1:
        //        return playerAnimationsList.knifeAttack01;

        //    case 2:
        //        return playerAnimationsList.knifeAttack02;

        //    //case 3:
        //    //    return playerAnimationsList.knifeAttack03;
        //}

        return playerAnimationsList.knifeAttack02;
    }

    void PlayKnifeSound()
    {
        audioInfo_Knife.PlayClipIfReady(soundsList.Audio_Knife);
    }

    void PlayCampKnifeSound()
    {
        audioInfo_Knife.PlayClipIfReady(soundsList.Audio_CampKnife);
    }

    void PlayKnifeHit()
    {
        audioInfo_Knife.PlayClip_OneShot(soundsList.Audio_KnifeHit);
    }

    void MakePlayerCommonHUDsVisible()
    {
        hudParent.GetChildGroupByName(HUDGroupName.AmmoCount).SetVisibilityOfAllChilds(true);
        hudParent.GetChildGroupByName(HUDGroupName.Grenade).SetVisibilityOfAllChilds(true);
        hudParent.GetChildGroupByName(HUDGroupName.GunShape).SetVisibilityOfAllChilds(true);
        hudParent.GetChildGroupByName(HUDGroupName.LugerBullets).SetVisibilityOfAllChilds(true);
        hudParent.GetChildGroupByName(HUDGroupName.MP18Bullets).SetVisibilityOfAllChilds(true);
        hudParent.GetChildGroupByName(HUDGroupName.SitStand).SetVisibilityOfAllChilds(true);
        hudParent.GetChildGroupByName(HUDGroupName.SnipeBullets).SetVisibilityOfAllChilds(true);
        hudParent.GetChildGroupByName(HUDGroupName.SpringfieldBullets).SetVisibilityOfAllChilds(true);
        hudParent.GetChildGroupByName(HUDGroupName.WinchesterBullets).SetVisibilityOfAllChilds(true);
        hudParent.GetChildGroupByName(HUDGroupName.SitStand).SetVisibilityOfAllChilds(true);
    }

    void MakeCommonHUDsInvisible()
    {
        hudParent.GetChildGroupByName(HUDGroupName.AmmoCount).SetVisibilityOfAllChilds(false);
        hudParent.GetChildGroupByName(HUDGroupName.Grenade).SetVisibilityOfAllChilds(false);
        hudParent.GetChildGroupByName(HUDGroupName.GunShape).SetVisibilityOfAllChilds(false);
        hudParent.GetChildGroupByName(HUDGroupName.LugerBullets).SetVisibilityOfAllChilds(false);
        hudParent.GetChildGroupByName(HUDGroupName.MP18Bullets).SetVisibilityOfAllChilds(false);
        hudParent.GetChildGroupByName(HUDGroupName.SnipeBullets).SetVisibilityOfAllChilds(false);
        hudParent.GetChildGroupByName(HUDGroupName.SpringfieldBullets).SetVisibilityOfAllChilds(false);
        hudParent.GetChildGroupByName(HUDGroupName.WinchesterBullets).SetVisibilityOfAllChilds(false);
        hudParent.GetChildGroupByName(HUDGroupName.SitStand).SetVisibilityOfAllChilds(false);
    }

    void MakeCommonHUDsVisible()
    {
        hudParent.GetChildGroupByName(HUDGroupName.AmmoCount).SetVisibilityOfAllChilds(true);
        hudParent.GetChildGroupByName(HUDGroupName.Grenade).SetVisibilityOfAllChilds(true);
        hudParent.GetChildGroupByName(HUDGroupName.GunShape).SetVisibilityOfAllChilds(true);
        hudParent.GetChildGroupByName(HUDGroupName.LugerBullets).SetVisibilityOfAllChilds(true);
        hudParent.GetChildGroupByName(HUDGroupName.MP18Bullets).SetVisibilityOfAllChilds(true);
        hudParent.GetChildGroupByName(HUDGroupName.SnipeBullets).SetVisibilityOfAllChilds(true);
        hudParent.GetChildGroupByName(HUDGroupName.SpringfieldBullets).SetVisibilityOfAllChilds(true);
        hudParent.GetChildGroupByName(HUDGroupName.WinchesterBullets).SetVisibilityOfAllChilds(true);
        hudParent.GetChildGroupByName(HUDGroupName.SitStand).SetVisibilityOfAllChilds(true);
    }

    void ReInitGunInfoHUDForNewGun()
    {
        oldGun_HUDGroup_Bullet = currentActiveGun_HUDGroup_Bullet;

        //

        currentActiveGun_HUDGroup_Bullet = hudParent.GetChildGroupByName(curActiveGun.hudBullet_GroupName);

        currentActiveGun_HUDControls_Bullets = new List<HUDControl>();
        foreach (HUDControl hc in currentActiveGun_HUDGroup_Bullet.hudControls)
        {
            currentActiveGun_HUDControls_Bullets.Add(hc);
        }

        currentActiveGun_HUDGroup_GunShape = hudParent.GetChildGroupByName(HUDGroupName.GunShape);

        currentActiveGun_HUDControl_GunShape = currentActiveGun_HUDGroup_GunShape.GetChildControlByName(curActiveGun.hudGun_ControlName);

        //

        if (oldGun_HUDGroup_Bullet != null)
            oldGun_HUDGroup_Bullet.StartDecreasingAlphaOfAllChilds(hudAlphaDecSpeed_Fast);

        currentActiveGun_HUDGroup_GunShape.StartDecreasingAlphaOfAllChilds(hudAlphaDecSpeed_Fast);
        currentActiveGun_HUDControl_GunShape.StartIncreasingAlpha(hudAlphaIncSpeed_Fast);

        ReSetGunInfoHUD();
    }

    void ReSetGunInfoHUD()
    {
        currentActiveGun_HUDGroup_Bullet.SetChildsTextureIndex(0, curActiveGun.GetCurrentMagazineCount() - 1, 1);
        currentActiveGun_HUDGroup_Bullet.SetChildsTextureIndex(curActiveGun.GetCurrentMagazineCount(), curActiveGun.magazineCapacity - 1, 0);

        hudControl_Grenade.SetSelectedTextureIndex(currentNumOfGrenades);

        ShowGunInfoHUDForAWhile();
    }

    void ShowGunInfoHUDForAWhile()
    {
        currentActiveGun_HUDGroup_Bullet.ShowChildsForAWhile(hudShowForAWhile_Time, hudShowForAWhile_StartAlphaSpeed, hudShowForAWhile_EndAlphaSpeed);
        hudControl_Grenade.ShowForAWhile(hudShowForAWhile_Time, hudShowForAWhile_StartAlphaSpeed, hudShowForAWhile_EndAlphaSpeed);
        hudControl_BulletCount_L.ShowForAWhile(hudShowForAWhile_Time, hudShowForAWhile_StartAlphaSpeed, hudShowForAWhile_EndAlphaSpeed);
        hudControl_BulletCount_R.ShowForAWhile(hudShowForAWhile_Time, hudShowForAWhile_StartAlphaSpeed, hudShowForAWhile_EndAlphaSpeed);
        hudControl_BulletCount_M.ShowForAWhile(hudShowForAWhile_Time, hudShowForAWhile_StartAlphaSpeed, hudShowForAWhile_EndAlphaSpeed);

        ReSetAmmoCountHUD();
    }

    void ReSetSitStandHUD()
    {
        if (vertMovementState == PlayerVertMovementStateEnum.Sit)
        {
            hudControl_SitStand.SetSelectedTextureIndex(1);
        }

        if (vertMovementState == PlayerVertMovementStateEnum.Stand)
        {
            hudControl_SitStand.SetSelectedTextureIndex(0);
        }

        ShowSitStandHUDForAWhile();
    }

    void ShowSitStandHUDForAWhile()
    {
        hudControl_SitStand.ShowForAWhile(hudSitStand_Time, hudSitStand_StartAlphaSpeed, hudSitStand_EndAlphaSpeed);
    }

    void ShowGunShapeHUDForAWhile()
    {
        currentActiveGun_HUDControl_GunShape.ShowForAWhile(hudGunShape_Time, hudGunShape_StartAlphaSpeed, hudGunShape_EndAlphaSpeed);
    }

    void ReSetAmmoCountHUD()
    {
        if (curActiveGun != null)
        {
            int curAmmoCount = curActiveGun.GetCurrentBulletCount();

            int l_Index = curAmmoCount / 100;
            int m_Index = (curAmmoCount - l_Index * 100) / 10;
            int r_Index = curAmmoCount - l_Index * 100 - m_Index * 10;

            if (l_Index == 0)
            {
                hudControl_BulletCount_L.SetSelectedTextureIndex(10);
            }
            else
            {
                hudControl_BulletCount_L.SetSelectedTextureIndex(l_Index);
            }

            if (m_Index == 0 && l_Index == 0)
            {
                hudControl_BulletCount_M.SetSelectedTextureIndex(10);
            }
            else
            {
                hudControl_BulletCount_M.SetSelectedTextureIndex(m_Index);
            }

            hudControl_BulletCount_R.SetSelectedTextureIndex(r_Index);
        }
        //hudControl_BulletCount_L
    }

    void ShowHUD_ObjsInMiniMap()
    {
        if (!ShouldShowCompass())
            return;

        float minimapX = compass.texture_X_Coef * Screen.height + (compass.texture_W_Coef / 2) * Screen.height;
        float minimapY = compass.texture_Y_Coef * Screen.height + (compass.texture_H_Coef / 2) * Screen.height;
        float minimapRadius = (compass.texture_W_Coef / 2.5f) * Screen.height;

        Vector3 playerPos = transform.position;
        Vector2 playerPos2D = new Vector2(playerPos.x, playerPos.z);

        Vector3 playerForward = transform.forward;
        Vector2 playerForward2D = new Vector2(playerForward.x, playerForward.z);

        foreach (GameObject sold in mapLogic.mapEnemyChars)
        {
            if (sold != null && sold.GetComponent<SoldierAction>() != null)
            {
                Vector3 soldPos = sold.transform.position;

                ShowPointOnMinimap(minimapX, minimapY, minimapRadius, playerPos, playerPos2D, playerForward, playerForward2D, soldPos, hudTextures.minimapEnemyWCoef, hudTextures.minimapEnemy);
            }
        }

        foreach (GameObject sold in mapLogic.mapAllyChars)
        {
            if (sold != null && sold.GetComponent<SoldierAction>() != null)
            {
                Vector3 soldPos = sold.transform.position;

                ShowPointOnMinimap(minimapX, minimapY, minimapRadius, playerPos, playerPos2D, playerForward, playerForward2D, soldPos, hudTextures.minimapAllyWCoef, hudTextures.minimapAlly);
            }
        }

        foreach (HUD_3DObj dat3DObj in hud_3DObjs)
        {
            if (dat3DObj != null && dat3DObj.isActive)
            {
                ShowPointOnMinimap(minimapX, minimapY, minimapRadius, playerPos, playerPos2D, playerForward, playerForward2D, dat3DObj.sourcePos, hudTextures.minimapObjectiveWCoef * dat3DObj.GetWCoef(), hudTextures.minimapObjective);
            }
        }
    }

    void ShowPointOnMinimap(float _minimapX, float _minimapY, float _minimapRadius, Vector3 _playerPos, Vector2 _playerPos2D, Vector3 _playerForward, Vector2 _playerForward2D, Vector3 _objPos, float _wCoef, Texture2D _texture)
    {
        float minimapX = _minimapX;
        float minimapY = _minimapY;
        float minimapRadius = _minimapRadius;

        Vector3 playerPos = _playerPos;
        Vector2 playerPos2D = _playerPos2D;
        Vector3 playerForward = _playerForward;
        Vector2 playerForward2D = _playerForward2D;

        Vector3 pos = _objPos;
        Vector2 pos2D = new Vector2(pos.x, pos.z);

        float wCoef = _wCoef;
        float w = wCoef * Screen.height;

        Texture2D texch = _texture;

        Vector2 distVec2D = pos2D - playerPos2D;
        float dist2DToPlayer = distVec2D.magnitude;
        float coefed_Dist = dist2DToPlayer * hud_MinimapDistCoef * Screen.height;
        coefed_Dist = Mathf.Clamp(coefed_Dist, 0, minimapRadius);

        float datAngle = MathfPlus.GetDeltaAngle(playerForward, playerPos, pos);

        GUIUtility.RotateAroundPivot(datAngle, new Vector2(minimapX, minimapY));

        GUI.DrawTexture(new Rect(minimapX - w / 2, minimapY - w / 2 - coefed_Dist, w, w), texch);

        GUIUtility.RotateAroundPivot(-datAngle, new Vector2(minimapX, minimapY));
    }

    void ShowRotatingHUD(float _NcenterX, float _NcenterY, float _Nradius, Vector3 _playerPos, Vector2 _playerPos2D, Vector3 _playerForward, Vector2 _playerForward2D, Vector3 _objPos, float _Nw, float _Nh, Texture2D _texture, float _alpha)
    {
        float centerX = _NcenterX;
        float centerY = _NcenterY;
        float radius = _Nradius;

        Vector3 playerPos = _playerPos;
        Vector2 playerPos2D = _playerPos2D;
        Vector3 playerForward = _playerForward;
        Vector2 playerForward2D = _playerForward2D;

        Vector3 pos = _objPos;
        Vector2 pos2D = new Vector2(pos.x, pos.z);

        float alph = _alpha;

        float w = _Nw;
        float h = _Nh;

        Texture2D texch = _texture;

        float datAngle = MathfPlus.GetDeltaAngle(playerForward, playerPos, pos);

        GUIUtility.RotateAroundPivot(datAngle, new Vector2(centerX, centerY));

        Color oldClr = GUI.color;

        Color clr = new Color(1, 1, 1, alph);

        GUI.color = clr;

        GUI.DrawTexture(new Rect(centerX - w / 2, centerY - h / 2 - radius, w, h), texch);

        GUI.color = oldClr;

        GUIUtility.RotateAroundPivot(-datAngle, new Vector2(centerX, centerY));
    }

    void ShowRotatingHUD(float _NcenterX, float _NcenterY, float _Nradius, float _angle, float _Nw, float _Nh, Texture2D _texture, float _alpha)
    {
        float centerX = _NcenterX;
        float centerY = _NcenterY;
        float radius = _Nradius;

        float alph = _alpha;

        float w = _Nw;
        float h = _Nh;

        Texture2D texch = _texture;

        float datAngle = _angle;

        GUIUtility.RotateAroundPivot(datAngle, new Vector2(centerX, centerY));

        Color oldClr = GUI.color;

        Color clr = new Color(1, 1, 1, alph);

        GUI.color = clr;

        GUI.DrawTexture(new Rect(centerX - w / 2, centerY - h / 2 - radius, w, h), texch);

        GUI.color = oldClr;

        GUIUtility.RotateAroundPivot(-datAngle, new Vector2(centerX, centerY));
    }

    bool IsGenerallyOkToShowNormalHUDs()
    {
        return mapLogic.CanGenerallyShowNormalHUD();
    }

    bool CanShowHUD_YouGetDamaged_TakeCover()
    {
        if (!IsGenerallyOkToShowNormalHUDs())
            return false;

        if (IsHPState(PlayerHPStateEnum.Critical_Update) || (IsHPState(PlayerHPStateEnum.GettingBackFromCritical_Update) && gettingBackFromCriticalHpStateTimeCounter > gettingBackFromCritical_Time * hud_YouGetDamaged_GettingBackFromCrit_Coef))
            return true;

        return false;
    }

    bool CanShowHUD_Reload_NoAmmo()
    {
        if (!IsGenerallyOkToShowNormalHUDs())
            return false;

        if (curActiveGun == null)
            return false;

        if (isCampPlayer)
            return false;

        if ((((curActiveGun.GetCurrentBulletCount() + curActiveGun.GetCurrentMagazineCount()) > curActiveGun.magazineCapacity) && (curActiveGun.GetCurrentMagazineCount() <= hud_Reload_NoAmmo_MagazineCoef * curActiveGun.magazineCapacity))
            ||
            (curActiveGun.GetCurrentBulletCount() + curActiveGun.GetCurrentMagazineCount()) == 0)
            return true;

        return false;
    }

    bool CanShowHUD_ObjectivesPage()
    {
        if (!IsGenerallyOkToShowNormalHUDs())
            return false;

        if (CustomInputManager.KeyIfGameIsNotPaused_Missions()) //(GetButton(keys.tab))
            return true;

        return false;
    }

    bool CanShowHUD_PressActKeyToGrabGun()
    {
        if (!IsGenerallyOkToShowNormalHUDs())
            return false;

        if (isCampPlayer)
            return false;

        if (canShowHUD_PressActKeyToGrabGun)
            return true;

        return false;
    }

    bool CanShowHUD_PressActKeyToRefillAmmo()
    {
        if (!IsGenerallyOkToShowNormalHUDs())
            return false;

        if (isCampPlayer)
            return false;

        if (canShowHUD_PressActRefillAmmo)
            return true;

        return false;
    }

    bool CanShowHUD_PressActKeyToDoTheAction()
    {
        if (!IsGenerallyOkToShowNormalHUDs())
            return false;

        if (canShowHUD_PressActKeyToDoTheAction)
            return true;

        return false;
    }

    void CheckAndInitActionKeyHUD()
    {
        canShowHUD_PressActKeyToGrabGun = false;
        canShowHUD_PressActRefillAmmo = false;
        canShowHUD_PressActKeyToDoTheAction = false;

        if (IsForwardObjectDetectorReadyAndContainsSomething())
        {
            if (forwardObjectDetector.DoesContainAnyLogicObjectives())
            {
                canShowHUD_PressActKeyToDoTheAction = true;
                return;
            }

            if (!isCampPlayer)
            {
                if (forwardObjectDetector.DoesContainAnyItem())
                {
                    Item item = forwardObjectDetector.GetFirstOKItemFromList();

                    if (IsReqItemPickableAmmo(item))
                    {
                        canShowHUD_PressActRefillAmmo = true;
                        return;
                    }
                    else
                    {
                        if (IsReqItemANewGun(item))
                        {
                            canShowHUD_PressActKeyToGrabGun = true;
                            nameOfGrabbableGun = item.gunName;

                            int ind = 0;

                            foreach (PlayerGun plGun in guns)
                            {
                                if (plGun.gunName == nameOfGrabbableGun)
                                {
                                    ind = plGun.shapeHUDIndex;

                                    break;
                                }
                            }

                            hudControl_GrabbableGun.SetSelectedTextureIndex(ind);
                            return;
                        }
                    }
                }
            }
        }
    }

    void ShowHUD_AmmoPickInfo(HUDAmmoPickInfoType _infoType, PlayerGunName _gunName)
    {
        HUDAmmoPickInfoType infoType = _infoType;
        PlayerGunName gName = _gunName;

        int gHUDInd = 0;

        foreach (PlayerGun plGun in guns)
        {
            if (plGun.gunName == gName)
            {
                gHUDInd = plGun.shapeHUDIndex;
                break;
            }
        }

        switch (infoType)
        {
            case HUDAmmoPickInfoType.GrenadesAdded:
                hudControl_AmmoPickInfo.SetSelectedTextureIndex(0);
                break;

            case HUDAmmoPickInfoType.GrenadesFull:
                hudControl_AmmoPickInfo.SetSelectedTextureIndex(1);
                break;

            case HUDAmmoPickInfoType.GunAmmoAdded:
                hudControl_AmmoPickInfo.SetSelectedTextureIndex(2 + 2 * gHUDInd);
                break;

            case HUDAmmoPickInfoType.GunAmmoFull:
                hudControl_AmmoPickInfo.SetSelectedTextureIndex(2 + 2 * gHUDInd + 1);
                break;
        }

        canShowHUD_AmmoPickInfo = true;

        hudControl_AmmoPickInfo.SetIsVisible(true);
        hudControl_AmmoPickInfo.SetAlpha(0);

        hudControl_AmmoPickInfo.wCoef = 1;
        hudControl_AmmoPickInfo.hCoef = 1;

        hudControl_AmmoPickInfo.ReInitRect();

        hudControl_AmmoPickInfo.SetOutCounter(0);

        hudControl_AmmoPickInfo.SetOutStep(HUDOutStep.Starting);
    }

    bool CanShowHUD_AmmoPickInfo()
    {
        if (!IsGenerallyOkToShowNormalHUDs())
            return false;

        if (canShowHUD_AmmoPickInfo)
            return true;

        return false;
    }

    bool CanShowHUD_SnipeHint_PressShiftToHoldBreath()
    {
        if (!IsGenerallyOkToShowNormalHUDs())
            return false;

        if (!isOnSnipeMode)
            return false;

        return hud_ShouldShow_SnipeHint_PressShiftToHoldBreath;
    }

    bool CanShowHUD_SnipeHint_PressMidMouseForFocusMode()
    {
        if (!IsGenerallyOkToShowNormalHUDs())
            return false;

        if (!isOnSnipeMode)
            return false;

        return hud_ShouldShow_SnipeHint_PressMidMouseForFocusMode;
    }

    bool CanShowHUD_SnipeHint_PressMidMouseToCancelFocusMode()
    {
        if (!IsGenerallyOkToShowNormalHUDs())
            return false;

        if (!isOnSnipeMode)
            return false;

        return hud_ShouldShow_SnipeHint_PressMidMouseToCancelFocusMode;
    }

    bool CanShowHUD_SnipeHint_TimeScaleBar()
    {
        if (!IsGenerallyOkToShowNormalHUDs())
            return false;

        if (!isOnSnipeMode)
            return false;

        return true;
    }

    void ShowHUD_MissionFail(bool _isPlayerDead, MissionFailType _failType)
    {
        bool isPlayerDead = _isPlayerDead;
        MissionFailType failType = _failType;

        hud_ShouldShow_MissionFail = true;

        if (isPlayerDead)
        {
            hudControl_SelectedMissionFailText = hudGroup_MissionFail.GetChildControlByName(HUDControlName.MissionFail_YouDied);
        }
        else
        {
            switch (failType)
            {
                case MissionFailType.AlliesNotSupported:
                    hudControl_SelectedMissionFailText = hudGroup_MissionFail.GetChildControlByName(HUDControlName.MissionFail_AlliesNotSupported);
                    break;

                case MissionFailType.YouAreDetectedByEnemies:
                    hudControl_SelectedMissionFailText = hudGroup_MissionFail.GetChildControlByName(HUDControlName.MissionFail_YouAreDetectedByEnemies);
                    break;

                case MissionFailType.YouLeftFightArea:
                    hudControl_SelectedMissionFailText = hudGroup_MissionFail.GetChildControlByName(HUDControlName.MissionFail_YouLeftFightArea);
                    break;

                case MissionFailType.YouLeftAreaWithoutPlantingDynamites:
                    hudControl_SelectedMissionFailText = hudGroup_MissionFail.GetChildControlByName(HUDControlName.MissionFail_YouLeftAreaWithoutPlantingDynamites);
                    break;

                case MissionFailType.DynamteHasBeenExplodedBeforeCommunicationBreakdown:
                    hudControl_SelectedMissionFailText = hudGroup_MissionFail.GetChildControlByName(HUDControlName.MissionFail_DynamteHasBeenExplodedBeforeCommunicationBreakdown);
                    break;

                case MissionFailType.EnemyHeardYourFire:
                    hudControl_SelectedMissionFailText = hudGroup_MissionFail.GetChildControlByName(HUDControlName.MissionFail_EnemyHeardYourFire);
                    break;

                case MissionFailType.EnemySawHisMateNash:
                    hudControl_SelectedMissionFailText = hudGroup_MissionFail.GetChildControlByName(HUDControlName.MissionFail_EnemySawHisMateNash);
                    break;
            }
        }

        hudControl_SelectedMissionFailText.SetIsVisible(true);
        hudControl_SelectedMissionFailText.SetAlpha(0);

        hudControl_SelectedMissionFailText.SetOutCounter(0);
    }

    void HUD_AddDamageSide(Transform _sourceEnemy, Vector3 _sourcePos)
    {
        Transform sourceEnemy = _sourceEnemy;
        Vector3 sourcePos = _sourcePos;

        //if (sourceEnemy != null)
        //{
        //    for (int i = 0; i < hud_DamageSides.Length; i++)
        //    {
        //        HUD_DamageSide dmgSide = hud_DamageSides[i];

        //        if (dmgSide.isActive && dmgSide.sourceEnemy != null && dmgSide.sourceEnemy == sourceEnemy)
        //        {
        //            dmgSide.Restart(sourceEnemy, sourcePos);
        //            return;
        //        }
        //    }
        //}

        for (int i = 0; i < hud_DamageSides.Length; i++)
        {
            HUD_DamageSide dmgSide = hud_DamageSides[i];

            if (!dmgSide.isActive)
            {
                dmgSide.Restart(sourceEnemy, sourcePos);
                return;
            }
        }

        //int bestDmgSideIndex = 0;
        //float bestDmgSideTimeCounter = 0;

        //for (int i = 0; i < hud_DamageSides.Length; i++)
        //{
        //    HUD_DamageSide dmgSide = hud_DamageSides[i];

        //    if (dmgSide.timeCounter > bestDmgSideTimeCounter)
        //    {
        //        bestDmgSideTimeCounter = dmgSide.timeCounter;
        //        bestDmgSideIndex = i;
        //    }
        //}

        //hud_DamageSides[bestDmgSideIndex].Restart(sourceEnemy, sourcePos);
    }

    bool CanShowHUD_DamageSides()
    {
        if (!IsGenerallyOkToShowNormalHUDs())
            return false;

        return true;
    }

    void ShowHUD_DamageSides()
    {
        float centerX = 0.5f * Screen.width;
        float centerY = 0.5f * Screen.height;
        float radius = hudControl_DamageSide.rect.y - 0.5f * Screen.height;
        float w = hudControl_DamageSide.rect.w;
        float h = hudControl_DamageSide.rect.h;
        Texture2D texch = hudControl_DamageSide.textures[0];

        Vector3 playerPos = transform.position;
        Vector2 playerPos2D = new Vector2(playerPos.x, playerPos.z);

        Vector3 playerForward = transform.forward;
        Vector2 playerForward2D = new Vector2(playerForward.x, playerForward.z);

        for (int i = 0; i < hud_DamageSides.Length; i++)
        {
            HUD_DamageSide dmgSide = hud_DamageSides[i];

            if (dmgSide.isActive)
            {
                Vector3 datPos = dmgSide.sourcePos;

                float alph = dmgSide.curAlpha;

                ShowRotatingHUD(centerX, centerY, radius, playerPos, playerPos2D, playerForward, playerForward2D, datPos, w, h, texch, alph);
            }
        }
    }

    bool CanShowHUD_NearGrenades()
    {
        if (!IsGenerallyOkToShowNormalHUDs())
            return false;

        return true;
    }

    void ShowHUD_NearGrenades()
    {
        float centerX = 0.5f * Screen.width;
        float centerY = hudControl_GrenadeIcon.rect.y;

        float radius = hudControl_GrenadeIconFelesh.rect.y - 0.5f * Screen.height;

        float feleshW = hudControl_GrenadeIconFelesh.rect.w;
        float feleshH = hudControl_GrenadeIconFelesh.rect.h;
        Texture2D feleshTexch = hudControl_GrenadeIconFelesh.textures[0];

        float grnW = hudControl_GrenadeIcon.rect.w;
        float grnH = hudControl_GrenadeIcon.rect.h;
        Texture2D grnTexch = hudControl_GrenadeIcon.textures[0];

        Vector3 playerPos = transform.position;
        Vector2 playerPos2D = new Vector2(playerPos.x, playerPos.z);

        Vector3 playerForward = transform.forward;
        Vector2 playerForward2D = new Vector2(playerForward.x, playerForward.z);

        for (int i = 0; i < mapLogic.mapActiveGrenades.Count; i++)
        {
            PlayerGrenade plGrn = mapLogic.mapActiveGrenades[i];

            float grnDistToPlayer = Vector3.Distance(transform.position, plGrn.transform.position);

            if (plGrn != null && (plGrn.time - plGrn.timeCounter >= hud_GrenadeNeededPassTime) && (grnDistToPlayer <= hud_GrenadeDetectionRange))
            {
                float alph;

                if (grnDistToPlayer < hud_GrenadeDetectionMaxDistToShowFullAlpha)
                    alph = 1;
                else
                {
                    alph = (hud_GrenadeDetectionRange - grnDistToPlayer) / (hud_GrenadeDetectionRange - hud_GrenadeDetectionMaxDistToShowFullAlpha);
                    alph = Mathf.Clamp01(alph);
                }

                Color oldClr = GUI.color;

                Color clr = new Color(1, 1, 1, alph);

                GUI.color = clr;

                GUI.DrawTexture(new Rect(centerX - grnW / 2, centerY - grnH / 2, grnW, grnH), grnTexch);

                GUI.color = oldClr;


                ShowRotatingHUD(centerX, centerY, radius, playerPos, playerPos2D, playerForward, playerForward2D, plGrn.transform.position, feleshW, feleshH, feleshTexch, alph);
            }
        }
    }

    public void HUD_Add3DObj(Transform _sourceTr, Vector3 _sourcePos, string _name, bool _showSideFeleshes)
    {
        for (int i = 0; i < hud_3DObjs.Count; i++)
        {
            if (hud_3DObjs[i].name == _name)
            {
                Debug.LogError("Can not add hud_3DObj. '" + _name + "' is used bafore!");
                return;
            }
        }

        HUD_3DObj hud3DObj = new HUD_3DObj();

        hud3DObj.Start(_sourceTr, _sourcePos, _name, _showSideFeleshes);

        hud3DObj.Blink();

        hud_3DObjs.Add(hud3DObj);
    }

    public void HUD_Remove3DObj(string _name)
    {
        for (int i = 0; i < hud_3DObjs.Count; i++)
        {
            if (hud_3DObjs[i].name == _name)
            {
                hud_3DObjs[i].End();
                hud_3DObjs.RemoveAt(i);
            }
        }
    }

    public HUD_3DObj HUD_Get3DObjByName(string _name)
    {
        for (int i = 0; i < hud_3DObjs.Count; i++)
        {
            if (hud_3DObjs[i].name == _name)
            {
                return hud_3DObjs[i];
            }
        }

        Debug.LogError("No hud3DObj found with name: '" + _name + "'!");
        return null;
    }

    bool CanShowHUD_3DObjectives()
    {
        if (!IsGenerallyOkToShowNormalHUDs())
            return false;

        return true;
    }

    void ShowHUD_3DObjectives()
    {
        float centerX = 0.5f * Screen.width;
        float centerY = 0.5f * Screen.height;

        float w = hudControl_3DObj_Felesh.rect.w;
        float h = hudControl_3DObj_Felesh.rect.h;
        Texture2D texch = hudControl_3DObj_Felesh.textures[0];

        Vector3 playerPos = transform.position;
        Vector2 playerPos2D = new Vector2(playerPos.x, playerPos.z);

        Vector3 playerForward = transform.forward;
        Vector2 playerForward2D = new Vector2(playerForward.x, playerForward.z);

        for (int i = 0; i < hud_3DObjs.Count; i++)
        {
            HUD_3DObj dat3DObj = hud_3DObjs[i];

            if (dat3DObj.isActive && dat3DObj.showSideFeleshs)
            {
                Vector3 datPos = dat3DObj.sourcePos;

                float radius = HUD_GetHUDRadiusFor3DObj(dat3DObj);

                ShowRotatingHUD(centerX, centerY, radius, playerPos, playerPos2D, playerForward, playerForward2D, datPos, w, h, texch, 1);
            }
        }
    }

    float HUD_GetHUDRadiusFor3DObj(HUD_3DObj _dat3DObj)
    {
        HUD_3DObj dat3DObj = _dat3DObj;

        float scW = Screen.width;
        float scH = Screen.height;

        Vector3 playerPos = transform.position;
        Vector3 playerForward = transform.forward;

        float datAngle = MathfPlus.GetDeltaAngle(playerForward, playerPos, dat3DObj.sourcePos);
        float standardDatAngle = -datAngle + 90;

        if (standardDatAngle >= 180)
            standardDatAngle -= 360;

        float standardDatAngleInRadian = Mathf.Deg2Rad * standardDatAngle;

        float cornerLowerCoef = 1 - hud_3DObjFelesh_CornerAngleCoef;
        float cornerHigherCoef = 1 + hud_3DObjFelesh_CornerAngleCoef;


        float cornerA_AngleInRadian = Mathf.Atan(scH / scW);
        float cornerA_AngleInRadian_Lower = cornerA_AngleInRadian * cornerLowerCoef;
        float cornerA_AngleInRadian_Higher = cornerA_AngleInRadian * cornerHigherCoef;

        float cornerB_AngleInRadian = Mathf.PI - cornerA_AngleInRadian;
        float cornerB_AngleInRadian_Lower = Mathf.PI - cornerA_AngleInRadian_Higher;
        float cornerB_AngleInRadian_Higher = Mathf.PI - cornerA_AngleInRadian_Lower;

        float cornerC_AngleInRadian = -cornerB_AngleInRadian;
        float cornerC_AngleInRadian_Lower = -cornerB_AngleInRadian_Higher;
        float cornerC_AngleInRadian_Higher = -cornerB_AngleInRadian_Lower;

        float cornerD_AngleInRadian = -cornerA_AngleInRadian;
        float cornerD_AngleInRadian_Lower = -cornerA_AngleInRadian_Higher;
        float cornerD_AngleInRadian_Higher = -cornerA_AngleInRadian_Lower;


        float firstRadius = 0;

        if (standardDatAngleInRadian >= cornerA_AngleInRadian && standardDatAngleInRadian < Mathf.PI / 2)
        {
            float x = scH / 2;
            float y;
            float z;
            float beta = Mathf.PI / 2 - standardDatAngleInRadian;
            y = Mathf.Tan(beta) * x;
            z = Mathf.Sqrt(x * x + y * y);

            firstRadius = z;

            goto ZFound;
        }

        if (standardDatAngleInRadian >= Mathf.PI / 2 && standardDatAngleInRadian < cornerB_AngleInRadian)
        {
            float x = scH / 2;
            float y;
            float z;
            float beta = standardDatAngleInRadian - Mathf.PI / 2;
            y = Mathf.Tan(beta) * x;
            z = Mathf.Sqrt(x * x + y * y);

            firstRadius = z;

            goto ZFound;
        }

        if (standardDatAngleInRadian >= cornerB_AngleInRadian && standardDatAngleInRadian < Mathf.PI)
        {
            float x = scW / 2;
            float y;
            float z;
            float beta = Mathf.PI - standardDatAngleInRadian;
            y = Mathf.Tan(beta) * x;
            z = Mathf.Sqrt(x * x + y * y);

            firstRadius = z;

            goto ZFound;
        }

        if (standardDatAngleInRadian >= -Mathf.PI && standardDatAngleInRadian < cornerC_AngleInRadian)
        {
            float x = scW / 2;
            float y;
            float z;
            float beta = Mathf.PI + standardDatAngleInRadian;
            y = Mathf.Tan(beta) * x;
            z = Mathf.Sqrt(x * x + y * y);

            firstRadius = z;

            goto ZFound;
        }

        if (standardDatAngleInRadian >= cornerC_AngleInRadian && standardDatAngleInRadian < -Mathf.PI / 2)
        {
            float x = scH / 2;
            float y;
            float z;
            float beta = -standardDatAngleInRadian - Mathf.PI / 2;
            y = Mathf.Tan(beta) * x;
            z = Mathf.Sqrt(x * x + y * y);

            firstRadius = z;

            goto ZFound;
        }

        if (standardDatAngleInRadian >= -Mathf.PI / 2 && standardDatAngleInRadian < cornerD_AngleInRadian)
        {
            float x = scH / 2;
            float y;
            float z;
            float beta = Mathf.PI / 2 + standardDatAngleInRadian;
            y = Mathf.Tan(beta) * x;
            z = Mathf.Sqrt(x * x + y * y);

            firstRadius = z;

            goto ZFound;
        }

        if (standardDatAngleInRadian >= cornerD_AngleInRadian && standardDatAngleInRadian < 0)
        {
            float x = scW / 2;
            float y;
            float z;
            float beta = -standardDatAngleInRadian;
            y = Mathf.Tan(beta) * x;
            z = Mathf.Sqrt(x * x + y * y);

            firstRadius = z;

            goto ZFound;
        }

        if (standardDatAngleInRadian >= 0 && standardDatAngleInRadian < cornerA_AngleInRadian)
        {
            float x = scW / 2;
            float y;
            float z;
            float beta = standardDatAngleInRadian;
            y = Mathf.Tan(beta) * x;
            z = Mathf.Sqrt(x * x + y * y);

            firstRadius = z;

            goto ZFound;
        }

    ZFound:

        float finalRadius = firstRadius;
        float coefToDecreaseFromFinalRadius = 0;

        bool angleIsInReductionRange = false;

        float reductionRangeDiffValInRadian = 0;

        if (standardDatAngleInRadian >= cornerA_AngleInRadian_Lower && standardDatAngleInRadian < cornerA_AngleInRadian)
        {
            reductionRangeDiffValInRadian = standardDatAngleInRadian - cornerA_AngleInRadian_Lower;

            angleIsInReductionRange = true;
            goto EndOfDat;
        }

        if (standardDatAngleInRadian >= cornerA_AngleInRadian && standardDatAngleInRadian < cornerA_AngleInRadian_Higher)
        {
            reductionRangeDiffValInRadian = cornerA_AngleInRadian_Higher - standardDatAngleInRadian;

            angleIsInReductionRange = true;
            goto EndOfDat;
        }

        if (standardDatAngleInRadian >= cornerB_AngleInRadian_Lower && standardDatAngleInRadian < cornerB_AngleInRadian)
        {
            reductionRangeDiffValInRadian = standardDatAngleInRadian - cornerB_AngleInRadian_Lower;

            angleIsInReductionRange = true;
            goto EndOfDat;
        }

        if (standardDatAngleInRadian >= cornerB_AngleInRadian && standardDatAngleInRadian < cornerB_AngleInRadian_Higher)
        {
            reductionRangeDiffValInRadian = cornerB_AngleInRadian_Higher - standardDatAngleInRadian;

            angleIsInReductionRange = true;
            goto EndOfDat;
        }

        if (standardDatAngleInRadian >= cornerC_AngleInRadian_Lower && standardDatAngleInRadian < cornerC_AngleInRadian)
        {
            reductionRangeDiffValInRadian = standardDatAngleInRadian - cornerC_AngleInRadian_Lower;

            angleIsInReductionRange = true;
            goto EndOfDat;
        }

        if (standardDatAngleInRadian >= cornerC_AngleInRadian && standardDatAngleInRadian < cornerC_AngleInRadian_Higher)
        {
            reductionRangeDiffValInRadian = cornerC_AngleInRadian_Higher - standardDatAngleInRadian;

            angleIsInReductionRange = true;
            goto EndOfDat;
        }

        if (standardDatAngleInRadian >= cornerD_AngleInRadian_Lower && standardDatAngleInRadian < cornerD_AngleInRadian)
        {
            reductionRangeDiffValInRadian = standardDatAngleInRadian - cornerD_AngleInRadian_Lower;

            angleIsInReductionRange = true;
            goto EndOfDat;
        }

        if (standardDatAngleInRadian >= cornerD_AngleInRadian && standardDatAngleInRadian < cornerD_AngleInRadian_Higher)
        {
            reductionRangeDiffValInRadian = cornerD_AngleInRadian_Higher - standardDatAngleInRadian;

            angleIsInReductionRange = true;
            goto EndOfDat;
        }

    EndOfDat:

        if (angleIsInReductionRange)
        {
            coefToDecreaseFromFinalRadius = Mathf.Pow(reductionRangeDiffValInRadian, hud_3DObjFelesh_InReductionRange_DatPow) * hud_3DObjFelesh_InReductionRange_DatCoef;

            //float tmp = (scH / 2) / (Mathf.Tan(cornerHigherCoef * cornerA_AngleInRadian));
            //float r = (scW / 2) - tmp;

            //float scRd = Mathf.Sqrt((scW / 2) * (scW / 2) + (scH / 2) * (scH / 2));

            //finalRadius = Mathf.Clamp(finalRadius, 0, scRd - r);
        }

        finalRadius *= (1 - coefToDecreaseFromFinalRadius);
        finalRadius *= hud_3DObjFelesh_MarginCoef;

        return finalRadius;
    }

    bool CanShowHUD_SneakingHints()
    {
        if (!IsGenerallyOkToShowNormalHUDs())
            return false;

        if (!hud_ShouldShow_LvlCampSneakingHints)
            return false;

        return true;
    }

    public void HUD_TryToStartShowingSneakingHints()
    {
        if (GameController.canShowSneakingHints)
        {
            GameController.canShowSneakingHints = false;

            hud_SneakingHint_TimeCounter = hud_SneakingHint_InitialDelayMaxTime;

            hudGroup_SneakingHints.SetAlphaOfAllChilds(0);
            hudGroup_SneakingHints.SetVisibilityOfAllChilds(true);

            hudControl_SneakingHints_LastSentence.SetAlpha(0);
            hudControl_SneakingHints_LastSentence.SetIsVisible(true);

            hud_ShouldShow_LvlCampSneakingHints = true;
        }
    }

    bool CanShowHUD_LvlCampCounterClock()
    {
        if (!IsGenerallyOkToShowNormalHUDs())
            return false;

        if (!hud_ShouldShow_LvlCampClock)
            return false;

        return true;
    }

    public void HUD_StartShowingLvlCampCounterClock()
    {
        if (hud_ShouldShow_LvlCampClock)
            return;

        hud_ShouldShow_LvlCampClock = true;

        hudControl_LvlCamp_ClockBG.SetAlpha(1f);
        hudControl_LvlCamp_ClockBG.SetIsVisible(true);

        hudControl_LvlCamp_ClockBGRed.SetAlpha(0f);
        hudControl_LvlCamp_ClockBGRed.SetIsVisible(true);

        hudControl_LvlCamp_ClockHandle.SetAlpha(1f);
        hudControl_LvlCamp_ClockHandle.SetIsVisible(false);

        hud_LvlCamp_ClockTimeCounter = hud_LvlCamp_ClockMaxTime;
    }

    public void HUD_StopShowingLvlCampCounterClock()
    {
        hud_ShouldShow_LvlCampClock = false;
    }

    bool CanShowHUD_LvlCamp_OnGUIClockHandle()
    {
        if (hudControl_LvlCamp_ClockHandle.alpha > 0)
            return true;

        return false;
    }

    void ShowHUD_LvlCamp_ClockHandle()
    {
        float centerX = hudControl_LvlCamp_ClockBG.rect.x + 0.5f * hudControl_LvlCamp_ClockBG.rect.w;
        float centerY = hudControl_LvlCamp_ClockBG.rect.y + 0.5f * hudControl_LvlCamp_ClockBG.rect.h + hudControl_LvlCamp_ClockHandle.rect.h * hudControl_LvlCamp_ClockHandle.additionalNums[1];

        float radius = +hudControl_LvlCamp_ClockHandle.rect.h * hudControl_LvlCamp_ClockHandle.additionalNums[0];

        float w = hudControl_LvlCamp_ClockHandle.rect.w;
        float h = hudControl_LvlCamp_ClockHandle.rect.h;

        Texture2D texch = hudControl_LvlCamp_ClockHandle.textures[0];

        float alph = hudControl_LvlCamp_ClockHandle.alpha;

        float ang = hud_LvlCamp_ClockTimeCounter * 1.5f;

        ShowRotatingHUD(centerX, centerY, radius, ang, w, h, texch, alph);
    }

    void MutePlayerAudios()
    {
        audioInfo.SetCustomVolume(0);
        audioInfo_Act.SetCustomVolume(0);
        audioInfo_BulletImpact.SetCustomVolume(0);
        audioInfo_SniperHeartBit.SetCustomVolume(0);
        audioInfo_FootStep_Right.SetCustomVolume(0);
        audioInfo_FootStep_Left.SetCustomVolume(0);
        audioInfo_FootStep_Landing.SetCustomVolume(0);
        audioInfo_Knife.SetCustomVolume(0);
        audioInfo_KnifeHit.SetCustomVolume(0);
    }

    void UnMuePlayerAudios()
    {
        audioInfo.SetCustomVolume(1);
        audioInfo_Act.SetCustomVolume(1);
        audioInfo_BulletImpact.SetCustomVolume(1);
        audioInfo_SniperHeartBit.SetCustomVolume(1);
        audioInfo_FootStep_Right.SetCustomVolume(1);
        audioInfo_FootStep_Left.SetCustomVolume(1);
        audioInfo_FootStep_Landing.SetCustomVolume(1);
        audioInfo_Knife.SetCustomVolume(1);
        audioInfo_KnifeHit.SetCustomVolume(1);
    }

    public void TryStartSitting()
    {
        if (vertMovementState == PlayerVertMovementStateEnum.Stand)
        {
            SetSprintingIsNotOk();
            SetVertMovementState(PlayerVertMovementStateEnum.StandToSit_Init);
            SetHorizMovementState(PlayerHorizMovementStateEnum.SlowMove);
        }
    }

    bool Is_FU_KeyDown()
    {
        //return GetKeyDown(KeyCode.Backspace);

        return CustomInputManager.KeyDownIfGameIsNotPaused_FU();
    }

    public CharacterController GetCharController()
    {
        return charControl;
    }

    bool IsChangeGunKeyPressed()
    {
        //return (Input.GetAxis("Mouse ScrollWheel") > 0) || (Input.GetAxis("Mouse ScrollWheel") < 0) || GetButtonDown(keys.changeGun);

        return CustomInputManager.KeyDownIfGameIsNotPaused_ChangeGun();
    }
}
