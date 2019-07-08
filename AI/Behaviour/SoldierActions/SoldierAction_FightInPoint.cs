using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum FightInPointTypeEnum
{
    NoCover,
    NormalCover,
    LeanSides,
}

public enum FightInPointStyleEnum
{
    Stand,
    Sit,
    LeftStand,
    LeftSit,
    RightStand,
    RightSit,
}

public enum FightCriticWithPlayerPos
{
    Covering,
    NormShoot_Shooting,
}

public enum StepEnum
{

    Start01,

    Covering01,
    Covering02,
    Covering03,
    CoveringInFight00,
    CoveringInFight01_1,
    CoveringInFight01_2,
    CoveringInFight02,
    CoveringRelax00,
    CoveringRelax01_1,
    CoveringRelax01_2,
    CoveringRelax02,
    CoveringInFightDamage01,
    CoveringInFightDamage02,
    CoveringRelaxDamage01,
    CoveringRelaxDamage02,
    CoveringFightCriticalWithPlayer01,
    CoveringFightCriticalWithPlayer02,
    CoveringGettingBackToPosition01,
    CoveringGettingBackToPosition02,
    CoveringReload01,
    CoveringReload02,
    CoveringGrenade01,
    CoveringGrenade02,

    Idle01,
    Idle02,
    IdleInfight01,
    IdleInfight02,
    IdleInfight03,
    IdleInfight04,
    IdleRelax01,
    IdleRelax02,
    IdleRelax03,
    IdleRelax04,
    IdleInfightDamage01,
    IdleInfightDamage02,
    IdleRelaxDamage01,
    IdleRelaxDamage02,
    IdleFightCriticalWithPlayer01,
    IdleFightCriticalWithPlayer02,
    IdleGettingBackToPosition01,
    IdleGettingBackToPosition02,
    IdleReload01,
    IdleReload02,

    NormShoot_ExternalAnimToShoot01,
    //NormShoot_ExternalAnimToShoot02,

    NormalShooting01, //NS
    NormShoot_CoveringToShooting01,
    NormShoot_CoveringToShootingAnim01,
    NormShoot_CoveringToShootingAnim02,
    NormShoot_CoveringToShootingCrossfade01,
    NormShoot_CoveringToShootingCrossfade02,
    NormShoot_IdleToShooting01,
    NormShoot_IdleToShootingAnim01,
    NormShoot_IdleToShootingAnim02,
    NormShoot_IdleToShootingCrossfade01,
    NormShoot_IdleToShootingCrossfade02,
    NormShoot_Shooting01,
    NormShoot_Shooting02,
    NormShoot_Shooting03_1,
    NormShoot_Shooting03_2,
    NormShoot_Shooting02A,
    NormShoot_Shooting03_1A,
    NormShoot_Shooting03_2A,
    NormShoot_Shooting04,
    NormShoot_Shooting05,
    NormShoot_Shooting06_Part1,
    NormShoot_Shooting06_Part2,
    NormShoot_Damage01,
    NormShoot_Damage02,
    NormShoot_FinishShooting01,
    NormShoot_FinishShooting02,
    NormShoot_ShootingToCovering01,
    NormShoot_ShootingToCoveringAnim01,
    NormShoot_ShootingToCoveringAnim02,
    NormShoot_ShootingToCoveringCrossfade01,
    NormShoot_ShootingToCoveringCrossfade02,
    NormShoot_ShootingToIdle01,
    NormShoot_ShootingToIdleAnim01,
    NormShoot_ShootingToIdleAnim02,
    NormShoot_ShootingToIdleCrossfade01,
    NormShoot_ShootingToIdleCrossfade02,
    NormShoot_FightCriticalWithPlayer01,
    NormShoot_FightCriticalWithPlayer02,
    NormShoot_GettingBackToPosition01,
    NormShoot_GettingBackToPosition02,
    NormShoot_GunDirToGunDir01,
    NormShoot_GunDirToGunDir02,
    NormShoot_ShootTarget01,
    NormShoot_ShootTarget02,
    NormShoot_IdleReload01,
    NormShoot_IdleReload02,

    //<Lean>
    LeanLeft01,
    LeanLeft_CoveringToLeanIdle01,
    LeanLeft_CoveringToLean_Anim_01,
    LeanLeft_CoveringToLean_Anim_02,
    LeanLeft_CoveringToLean_NoAnim_01,
    LeanLeft_CoveringToLean_NoAnim_02,
    LeanLeft_Idle_01,
    LeanLeft_Idle_02,
    LeanLeft_Idle_03_1,
    LeanLeft_Idle_03_2,
    LeanLeft_Idle_02_A,
    LeanLeft_Idle_03_1_A,
    LeanLeft_Idle_03_2_A,
    LeanLeft_Idle_04,
    LeanLeft_Idle_05,
    LeanLeft_Idle_06_1,
    LeanLeft_Idle_06_2,
    LeanLeft_Damage_01,
    LeanLeft_Damage_02,
    LeanLeft_LeanIdleToCovering_01,
    LeanLeft_LeanToCovering_Anim_01,
    LeanLeft_LeanToCovering_Anim_02,
    LeanLeft_LeanToCovering_NoAnim_01,
    LeanLeft_LeanToCovering_NoAnim_02,
    LeanLeft_Shoot_01,
    LeanLeft_Shoot_02,

    DoOnlyAnim_01,
    DoOnlyAnim_02,
    //</Lean>
}

public class SoldierAction_FightInPoint : SoldierAction
{
    enum SituationEnum
    {
        unknown,
        covering,
        idle,
        normalFighting,
        leanFighting,
    }

    public SoldierFightInPointInfo[] availableFightInPointInfos;
    public SoldierFightInPointInfo defaultFightInPointInfo;
    public SoldierFightInPointInfo curSelectedFightInPointInfo;
    public SoldierFightInPointInfo playerCriticalFightInfoInShoot; //Not for normal shootings
    public SoldierFightInPointInfo playerCriticalFightInfoInCover;

    public FightInPointTypeEnum fightType;
    public FightInPointStyleEnum fightStyle;

    public List<GameObject> initialEnemies;
    public List<CharRaycastResult> sortedEnemies;

    public float coveringAngleYToFightPosRotation = 0;
    public float actToCoverRotationSpeed;
    public float fightRotationSpeed;
    public float coveringMinTime;
    public float coveringMaxTime;
    public float shootingMinTime;
    public float shootingMaxTime;
    public float shootingStartAngle;
    public float shootingEndAngle;

    public float fightPosRadius = GeneralStats.PointRadius;

    public float actToCoverRotationAngleTolerance;
    public float fightRotationAngleTolerance;

    public Vector3 fightPos;
    public Quaternion fightRot;
    public Quaternion fightPosCoveringRotation;

    public bool shouldConsiderPlayerCriticalSituation = true;
    public bool keepSoldierInPosition = true;

    //

    CharRaycastResult selectedTarget = null;
    SoldierGun soldGun;
    [HideInInspector]
    public StepEnum step;
    SituationEnum situation;
    float range;
    public SoldierGunDirectionEnum currentGunDirection = SoldierGunDirectionEnum.Forward;

    bool isReloadingInFight = false;

    DamageInfo dmg;
    float animLength;
    float passedAnimTime;
    Vector3 gunRaycastOffset;

    public AIVoiceSituation voiceSituation = AIVoiceSituation.Silence;

    float checkGettingBackToPointMaxTime = 1;
    float checkGettingBackToPointTimeCounter = 1;

    float checkGunDirectionMaxTime = 0.3f;
    float checkGunDirectionTimeCounter = 0.3f;

    float checkingIsEnemyAroundMaxTime = 0.5f;
    float checkingIsEnemyAroundTimeCounter = 0.5f;

    float checkingPlayerIsInCriticMaxTime = 0.1f;
    float checkingPlayerIsInCriticMinTime = 0.05f;
    float checkingPlayerIsInCriticTimeCounter = 0.1f;

    float firstCoveringMinTime = 0.0f;
    float firstCoveringMaxTime = 0.1f;

    float newCoveringInfightMinTime = 0.1f;
    float newCoveringInfightMaxTime = 0.2f;

    float coveringInfightTargetRecheckMinTime = 1f;
    float coveringInfightTargetRecheckMaxTime = 2f;

    public float shootingDamageChance = 1f;

    float coveringTimeCounter = 0;
    float shootingTimeCounter;

    float idleTargetCheckTimeCounter;
    float idleTargetCheckMaxTime = 0.1f;
    float idleTargetCheckMinTime = 0.05f;

    bool gentlyRotateAllBodyToTarget;
    bool lockAllBodyRotationOnTarget;

    float animCoveringCrossfadeToFightLookTimeCounter;
    //float animFightLookCrossfadeToCoveringTimeCounter;

    float animIdleCrossfadeToFightLookTimeCounter;
    float animFightLookCrossfadeToIdleTimeCounter;

    float targetLockTimeCounter;
    float targetLockTimeMin = 1f; //Default
    float targetLockTimeMax = 2f; //Default

    float targetRecheckTimeCounter;
    bool targetRecheckTimeReached = false;
    float targetRecheckTimeMin = 0.05f; //Default
    float targetRecheckTimeMax = 0.1f; //Default

    float gunDirToGunDirTimeCounter;

    bool isTargetChecked = false;

    bool lastIsEnemyAroundResult = false;
    //bool lastIsPlayerInCriticResult = false;
    bool nowInCriticalFightWithPlayer = false;
    bool lastIsNotInPosResult = false;
    bool lastIsGunDirNotTrueResult = false;

    float time_TalkingVoiceDelay_Min = 3;
    float time_TalkingVoiceDelay_Max = 9;
    float time_TalkingVoiceDelay_TimeCounter;

    float chanceToTalkImReloadig = 0.8f;

    float defaultCoveringsStartCFTime = 0.4f;
    float defaultCoveringsCFTime = 0.1f;
    float defaultCoveringInFightToCoveringInFightDamageCFTime = 0.23f;
    float defaultCoveringInFightDamageToCoveringInFightCFTime = 0.33f;
    float defaultCoveringInFightToCoveringReloadCFTime = 0.33f;
    float defaultCoveringReloadToCoveringInFightCFTime = 0.33f;
    float defaultCoveringRelaxToCoveringRelaxDamageCFTime = 0.23f;
    float defaultCoveringRelaxDamageToCoveringRelaxCFTime = 0.33f;
    float defaultCurrentCoveringInfightToNewCoveringInfightCFTime = 0.37f;
    float defaultCoveringToFightLookAllBodyCFTime = 0.31f;
    float defaultCovering_To_CoveringToShootAllBodyCFTime = 0.28f;
    float defaultCoveringToFightLookAllBodyCFTimeForNoAnim = 0.37f;
    //float defaultFightLookAllBodyDamageCFTime = 0.01f;
    float defaultFightLookToCoveringAllBodyCFTime = 0.32f;
    float defaultShoot_To_ShootToCoveringAllBodyCFTime = 0.28f;
    float defaultFightLookToCoveringAllBodyCFTimeForNoAnim = 0.37f;
    float defaultCoveringInFightToGrenadeCFTime = 0.28f;
    float defaultGrenadeToCoveringInFightCFTime = 0.28f;

    float defaultIdleStartAllBodyCFTime = 0.4f;
    float defaultIdleAllBodyCFTime = 0.1f;
    float defaultIdleAllBodyCFTime_Slower = 0.4f;
    float defaultIdleInFightToIdleInFightDamageCFTime = 0.23f;
    float defaultIdleInFightDamageToIdleInFightCFTime = 0.33f;
    float defaultIdleInFightToIdleReloadCFTime = 0.33f;
    float defaultIdleReloadToIdleInFightCFTime = 0.33f;
    float defaultIdleRelaxToIdleRelaxDamageCFTime = 0.23f;
    float defaultIdleRelaxDamageToIdleRelaxCFTime = 0.33f;
    float defaultCurrentIdleInfightToNewIdleInfightCFTime = 0.37f;
    float defaultIdleToFightLookAllBodyCFTime = 0.32f;
    float defaultIdle_To_IdleToShootAllBodyCFTime = 0.28f;
    float defaultIdleToFightLookAllBodyCFTimeForNoAnim = 0.37f;
    float defaultFightLookToIdleAllBodyCFTime = 0.28f;
    float defaultShoot_To_ShootToIdleAllBodyCFTime = 0.28f;
    float defaultFightLookToIdleAllBodyCFTimeForNoAnim = 0.37f;

    float defaultFightLookAllBodyCFTime = 0.28f;
    float defaultGunDirToGunDirAllBodyCFTime = 0.25f;
    float defaultFightLookAllBodyDamageCFTime = 0.28f;
    float defaultDamageToFightLookAllBodyCFTime = 0.32f;
    float defaultShootAllBodyCFTime = 0.25f;
    float defaultShootToIdleReloadAllBodyCFTime = 0.34f;
    float defaultIdleReloadToShootAllBodyCFTime = 0.34f;


    float animToAnimCoveringAllBodyCFTime = 0.3f;
    float animToAnimCoveringDamageCFTime = 0.2f;
    float animToAnimCoveringReloadCFTime = 0.2f;
    float animToAnimCoveringToShootAllBodyCFTime = 0.25f;
    float animToAnimShootToCoveringAllBodyCFTime = 0.25f;
    float animToAnimCoveringGrenadeCFTime = 0.3f;

    float animToAnimIdleAllBodyCFTime = 0.2f;
    float animToAnimIdleDamageCFTime = 0.15f;
    float animToAnimIdleReloadCFTime = 0.2f;
    float animToAnimIdleToShootAllBodyCFTime = 0.3f;
    float animToAnimShootToIdleAllBodyCFTime = 0.3f;

    float animToAnimFightLookAllBodyCFTime = 0.35f;
    float animToAnimFightLookAllBodyDamageCFTime = 0.15f;
    float animToAnimShootAllBodyCFTime = 0.15f;
    float animToAnimShootToIdleReloadAllBodyCFTime = 0.4f;


    string selectedAnimCoveringInFight;
    string selectedAnimCoveringInFightDamage;
    string selectedAnimCoveringInFightReload;
    string selectedAnimCoveringRelax;
    string selectedAnimCoveringRelaxDamage;
    string selectedAnimCoveringToShootAllBody;
    string selectedAnimShootToCoveringAllBody;
    string selectedAnimCoveringGrenade;

    string selectedAnimIdleInFight;
    string selectedAnimIdleInFightDamage;
    string selectedAnimIdleInFightReload;
    string selectedAnimIdleRelax;
    string selectedAnimIdleRelaxDamage;
    string selectedAnimIdleToShootAllBody;
    string selectedAnimShootToIdleAllBody;

    string selectedAnimFightLookAllBody;
    string selectedAnimFightLookAllBodyDamage;
    string selectedAnimShootAllBody;

    //<Lean>

    string selectedAnimLeanIdle;
    string selectedAnimLeanShoot;
    string selectedAnimLeanDamage;
    string selectedAnimCoveringToLean;
    string selectedAnimLeanToCovering;

    float default_LeanIdle_CFTime = 0.2f;
    float default_Covering_To_CoveringToLean_CFTime = 0.2f;
    float default_CoveringToLean_To_LeanIdle_CFTime = 0.25f;
    float default_Covering_To_LeanIdle_ForNoAnim_CFTime = 0.45f;
    float default_LeanIdle_To_LeanShoot_CFTime = 0.08f;
    float default_LeanShoot_To_LeanIdle_CFTime = 0.12f;
    float default_LeanIdle_To_LeanDamage_CFTime = 0.08f;
    float default_LeanDamage_To_LeanIdle_CFTime = 0.2f;

    float animToAnim_CoveringToLean_CFTime = 0.25f;
    float animToAnim_LeanIdle_CFTime = 0.2f;
    float animToAnim_LeanShoot_CFTime = 0.2f;
    float animToAnim_LeanDamage_CFTime = 0.2f;
    float animToAnim_LeanToCovering_CFTime = 0.2f;

    float animLeanTimeCounter;

    //</Lean>

    //------------------------------------------------------

    //<Lean>

    bool fightPointCanLeanLeft = false;
    bool fightPointCanLeanRight = false;

    bool canLeanLeft = false;
    bool canLeanRight = false;

    float leanChance = 0;

    //</Lean>

    bool useStartCFTime = true;

    SoldierAction_FightInReg ownerFightInRegAct;

    bool nowReadyForGreandeLaunch = false;

    FightPoint fightPoint = null;

    float grenadeTimeCounter = 0;

    Transform soldierRightHandTr;

    bool shouldOnlyDoAnAnimOnFightPoint = false;
    AnimsList animsListToOnlyDoOnFightPoint = null;

    float onlyAnimCFTime = 1;


    public void InitForFightPoint(FightPoint _fightPoint) //First
    {
        fightPoint = _fightPoint;
        fightPos = _fightPoint.transform.position;
        fightRot = _fightPoint.transform.rotation;
        defaultFightInPointInfo = _fightPoint.defaultfightInfo;
        SetFightPosCoveringRotation();
        availableFightInPointInfos = _fightPoint.fightInfos;

        if (fightPoint.onlyDoAnimOnThisPoint)
        {
            shouldOnlyDoAnAnimOnFightPoint = true;
            animsListToOnlyDoOnFightPoint = fightPoint.onlyDoAnimOnThisPoint;
        }

        //<Lean>

        fightPointCanLeanLeft = _fightPoint.canLeanLeft;
        fightPointCanLeanRight = _fightPoint.canLeanRight;

        leanChance = _fightPoint.startLeanChance;

        //</Lean>

        InitNewFightInPointInfo(defaultFightInPointInfo);
    }

    public void InitForPos(Vector3 _pos, Quaternion _rot, SoldierFightInPointInfo _fightInfo) //First (For no cover types)
    {
        fightPoint = null;
        fightPos = _pos;
        fightRot = _rot;
        defaultFightInPointInfo = _fightInfo;
        SoldierFightInPointInfo[] infos = new SoldierFightInPointInfo[1];
        infos[0] = _fightInfo;
        availableFightInPointInfos = infos;
        InitNewFightInPointInfo(defaultFightInPointInfo);
    }

    public void InitNewFightInPointInfo(SoldierFightInPointInfo _info) //Second
    {
        curSelectedFightInPointInfo = _info;
        fightType = curSelectedFightInPointInfo.fightType;
        fightStyle = curSelectedFightInPointInfo.fightStyle;
        playerCriticalFightInfoInShoot = curSelectedFightInPointInfo.playerCriticalFightInfoInShoot;
        playerCriticalFightInfoInCover = curSelectedFightInPointInfo.playerCriticalFightInfoInCover;
        coveringAngleYToFightPosRotation = curSelectedFightInPointInfo.coveringAngleYToFightPosRotation;
        actToCoverRotationSpeed = curSelectedFightInPointInfo.actToCoverRotationSpeed;
        fightRotationSpeed = curSelectedFightInPointInfo.fightRotationSpeed;
        coveringMinTime = curSelectedFightInPointInfo.coveringMinTime;
        coveringMaxTime = curSelectedFightInPointInfo.coveringMaxTime;
        shootingMinTime = curSelectedFightInPointInfo.shootingMinTime;
        shootingMaxTime = curSelectedFightInPointInfo.shootingMaxTime;
        shootingStartAngle = curSelectedFightInPointInfo.shootingStartAngle;
        shootingEndAngle = curSelectedFightInPointInfo.shootingEndAngle;

        actToCoverRotationAngleTolerance = SoldierStats.SoldierRotationAngleToleranceCoef * actToCoverRotationSpeed;
        fightRotationAngleTolerance = SoldierStats.SoldierRotationAngleToleranceCoef * fightRotationSpeed;

        gunRaycastOffset = curSelectedFightInPointInfo.GetRaycastOffsetForGun(soldGun.name);

        SetFightPosCoveringRotation();

        situation = SituationEnum.unknown;

        //<Lean>

        canLeanLeft = fightPointCanLeanLeft && curSelectedFightInPointInfo.canLeanLeft;
        canLeanRight = fightPointCanLeanRight && curSelectedFightInPointInfo.canLeanRight;

        //</Lean>
    }

    public void InitAIVoiceSitu(AIVoiceSituation _situ)
    {
        voiceSituation = _situ;
    }

    //<Beta>
    public void Init_StartInShootSituForTarget(GameObject _target) //Third
    {
        if (!shouldOnlyDoAnAnimOnFightPoint)
        {
            CharRaycastResult newTarg = new CharRaycastResult();

            mapLogic.IsCharacterOkAsTarget(controlledSoldier.gameObject, _target, GetRaycastPos(), fightRot, range, shootingStartAngle, shootingEndAngle, out newTarg);

            SetNewTarget(newTarg);

            if (fightType == FightInPointTypeEnum.NoCover ||
               fightType == FightInPointTypeEnum.NormalCover)
            {
                step = StepEnum.NormShoot_ExternalAnimToShoot01;
            }
        }
    }
    //</Beta>

    //

    public override void Init(Transform contSoldier)
    {
        base.Init(contSoldier);
        soldGun = soldInfo.gun;
        range = soldInfo.fightRange;
        shootingDamageChance = soldInfo.DamageChanceOnShooting;
        ResetTalkingVoiceDelayCounter();

        soldierRightHandTr = soldBodyInfo.soldierRightHandTr;

        step = StepEnum.Start01;
    }

    public override void StartAct()
    {
        base.StartAct();
    }

    public override void UpdateAct()
    {
        base.UpdateAct();

        #region Pre

        time_TalkingVoiceDelay_TimeCounter = MathfPlus.DecByDeltatimeToZero(time_TalkingVoiceDelay_TimeCounter);

        if (ShouldCheckPlayerCritSitu())
            soldInfo.SlowlyUpdatePlayerCritic();

        if (situation == SituationEnum.covering)
        {
            RotateAllBodyToCovering();

            if (coveringTimeCounter > 0)
            {
                coveringTimeCounter -= Time.deltaTime;
                if (coveringTimeCounter < 0)
                    coveringTimeCounter = 0;
            }
        }

        if (situation == SituationEnum.idle)
        {
            if (idleTargetCheckTimeCounter > 0)
            {
                idleTargetCheckTimeCounter -= Time.deltaTime;
                if (idleTargetCheckTimeCounter < 0)
                    idleTargetCheckTimeCounter = 0;
            }
        }

        if (situation == SituationEnum.normalFighting)
        {
            DoCommonPreShootingThings();

            SetAllBodyRotationInNormalShoot();
        }

        //<Lean>
        if (situation == SituationEnum.leanFighting)
        {
            DoCommonPreShootingThings();
        }
    //</Lean>

        #endregion

    StartSteps:

        // !!!!! Dont forget to set situations !!!!!!!!!!!!!!!!!

        #region Start01
        if (step == StepEnum.Start01)
        {
            StartActInit();

            if (shouldOnlyDoAnAnimOnFightPoint)
            {
                step = StepEnum.DoOnlyAnim_01;
                goto StartSteps;
            }

            if (fightType == FightInPointTypeEnum.NoCover)
            {
                step = StepEnum.Idle01;
                goto StartSteps;
            }
            else
            {
                SetSituation(SituationEnum.covering);

                coveringTimeCounter = Random.Range(firstCoveringMinTime, firstCoveringMaxTime);
                step = StepEnum.Covering02;
                goto StartSteps;
            }
        }
        #endregion

        #region Covering01
        if (step == StepEnum.Covering01)
        {
            SetSituation(SituationEnum.covering);

            coveringTimeCounter = Random.Range(coveringMinTime, coveringMaxTime);
            step = StepEnum.Covering02;
            goto StartSteps;

        }
        #endregion

        #region Covering02
        if (step == StepEnum.Covering02)
        {
            SetSituation(SituationEnum.covering);

            if (useStartCFTime)
            {
                useStartCFTime = false;
                animToAnimCoveringAllBodyCFTime = defaultCoveringsStartCFTime;
            }
            else
                animToAnimCoveringAllBodyCFTime = defaultCoveringsCFTime;

            step = StepEnum.Covering03;
            goto StartSteps;
        }
        #endregion

        #region Covering03
        if (step == StepEnum.Covering03)
        {
            SetSituation(SituationEnum.covering);

            if (IsEnemyAround())
            {
                step = StepEnum.CoveringInFight00;
                goto StartSteps;
            }
            else
            {
                step = StepEnum.CoveringRelax00;
                goto StartSteps;
            }
        }
        #endregion

        #region CoveringInFight00
        if (step == StepEnum.CoveringInFight00)
        {
            SetSituation(SituationEnum.covering);

            selectedAnimCoveringInFight = curSelectedFightInPointInfo.animCoveringInFight.GetRandomAnimName();
            step = StepEnum.CoveringInFight01_1;
            goto StartSteps;
        }
        #endregion

        #region CoveringInFight01_1
        if (step == StepEnum.CoveringInFight01_1)
        {
            SetSituation(SituationEnum.covering);

            //soldAnimObj.animation[selectedAnimCoveringInFight.name].time = 0;
            //soldAnimObj.animation.CrossFade(selectedAnimCoveringInFight.name, animToAnimCoveringAllBodyCFTime);

            soldInfo.StartNewMainAnimWithCrossfadeTime(selectedAnimCoveringInFight, animToAnimCoveringAllBodyCFTime);

            step = StepEnum.CoveringInFight01_2;
        }
        #endregion

        #region CoveringInFight01_2
        if (step == StepEnum.CoveringInFight01_2)
        {
            SetSituation(SituationEnum.covering);

            PlayDamageSoundIfRecieved();

            if (soldInfo.IsFullyInNewMainAnim())
            {
                step = StepEnum.CoveringInFight02;
                goto StartSteps;
            }

            goto EndSteps;
        }
        #endregion

        #region CoveringInFight02
        if (step == StepEnum.CoveringInFight02)
        {
            SetSituation(SituationEnum.covering);

            if (CheckFirstDamage())
            {
                dmg = soldInfo.firstDamage;
                step = StepEnum.CoveringInFightDamage01;
                goto StartSteps;
            }

            if (needsToBeFinished)
            {
                SetFinished(true);
                return;
            }

            //gr
            if (IsReadyToLaunchGrenade())
            {
                step = StepEnum.CoveringGrenade01;
                goto StartSteps;
            }

            if (NeedsReload())
            {
                step = StepEnum.CoveringReload01;
                goto StartSteps;
            }

            if (CheckAndStartPlayerCriticalState(FightCriticWithPlayerPos.Covering))
            {
                step = StepEnum.Idle02;
                animToAnimIdleAllBodyCFTime = defaultIdleAllBodyCFTime_Slower;
                goto StartSteps;
            }

            if (CheckAndEndPlayerCriticalState())
            {
                step = StepEnum.Start01;
                goto StartSteps;
            }

            if (CheckSlowlyNeedsToGetBackToThePoint())
            {
                step = StepEnum.CoveringGettingBackToPosition01;
                goto StartSteps;
            }

            if (!CheckSlowlyIsEnemyAround())
            {
                step = StepEnum.Covering02;
                goto EndSteps;
            }

            if (coveringTimeCounter == 0)
            {
                //<Lean>

                bool leanLeftIsOK = false;

                if (ShouldStartLeanLeft())
                {
                    UpdateEnemiesForLeanLeft();
                    leanLeftIsOK = true;
                }
                else
                    UpdateEnemies();

                //<Lean>

                if (sortedEnemies.Count == 0)
                {
                    SoldierFightInPointInfo newFInfo = SelectNewFightInPointInfo(false);

                    if (newFInfo != curSelectedFightInPointInfo)
                    {
                        InitNewFightInPointInfo(newFInfo);
                        coveringTimeCounter = GetCoveringInfightTargetRecheckTimeForNewFightInfo();
                        animToAnimCoveringAllBodyCFTime = defaultCurrentCoveringInfightToNewCoveringInfightCFTime;
                        step = StepEnum.Covering03;
                        goto EndSteps;
                    }
                    else
                    {
                        coveringTimeCounter = GetCoveringInfightTargetRecheckTimeForCurrentFightInfo();
                    }
                }
                else
                {
                    SetNewTarget(sortedEnemies[0]);

                    //<Lean>

                    if (leanLeftIsOK)
                    {
                        step = StepEnum.LeanLeft01;

                        goto StartSteps;
                    }

                    //</Lean>

                    if (fightType == FightInPointTypeEnum.NormalCover)
                    {
                        step = StepEnum.NormalShooting01;
                    }

                    if (fightType == FightInPointTypeEnum.LeanSides)
                    {
                        //<NotCompleted> Be chizet
                    }

                    goto StartSteps;
                }
            }

            //animLength = soldAnimObj.animation[selectedAnimCoveringInFight.name].length;
            //passedAnimTime = soldAnimObj.animation[selectedAnimCoveringInFight.name].time;

            //if (animLength - passedAnimTime <= defaultCoveringsCFTime)
            //{
            //    animToAnimCoveringAllBodyCFTime = animLength - passedAnimTime;
            //    step = StepEnum.CoveringInFight00;
            //    goto StartSteps;
            //}

            if (soldInfo.CheckMainAnimIsFinished(defaultCoveringsCFTime))
            {
                animToAnimCoveringAllBodyCFTime = soldInfo.mainAnimRemainingTime;
                step = StepEnum.CoveringInFight00;
                goto StartSteps;
            }
        }
        #endregion

        #region CoveringInFightDamage01
        if (step == StepEnum.CoveringInFightDamage01)
        {
            SetSituation(SituationEnum.covering);

            PlayDamageSound(dmg);

            selectedAnimCoveringInFightDamage = curSelectedFightInPointInfo.animDamageCoveringInFight.GetRandomAnim(dmg);
            animToAnimCoveringDamageCFTime = defaultCoveringInFightToCoveringInFightDamageCFTime;

            //soldAnimObj.animation[selectedAnimCoveringInFightDamage.name].time = 0;
            //soldAnimObj.animation.CrossFade(selectedAnimCoveringInFightDamage.name, animToAnimCoveringDamageCFTime);

            soldInfo.StartNewMainAnimWithCrossfadeTime(selectedAnimCoveringInFightDamage, animToAnimCoveringDamageCFTime);

            animToAnimCoveringAllBodyCFTime = defaultCoveringInFightDamageToCoveringInFightCFTime;

            step = StepEnum.CoveringInFightDamage02;
            goto EndSteps;
        }
        #endregion

        #region CoveringInFightDamage02
        if (step == StepEnum.CoveringInFightDamage02)
        {
            SetSituation(SituationEnum.covering);

            //animLength = soldAnimObj.animation[selectedAnimCoveringInFightDamage.name].length;
            //passedAnimTime = soldAnimObj.animation[selectedAnimCoveringInFightDamage.name].time;

            //if (animLength - passedAnimTime <= animToAnimCoveringAllBodyCFTime)
            //{
            //    animToAnimCoveringAllBodyCFTime = animLength - passedAnimTime;
            //    coveringTimeCounter = 0;
            //    step = StepEnum.CoveringInFight00;
            //    goto StartSteps;
            //}

            if (soldInfo.CheckMainAnimIsFinished(animToAnimCoveringAllBodyCFTime))
            {
                animToAnimCoveringAllBodyCFTime = soldInfo.mainAnimRemainingTime;
                coveringTimeCounter = 0;
                step = StepEnum.CoveringInFight00;
                goto StartSteps;
            }
        }
        #endregion

        #region ReloadInCovering01
        if (step == StepEnum.CoveringReload01)
        {
            SetSituation(SituationEnum.covering);

            if (voiceSituation == AIVoiceSituation.Agressive)
                TryPlayVoice_ImReloading();

            if (IsEnemyAround())
            {
                isReloadingInFight = true;
            }

            soldGun.Reload();
            soldGun.PlayReloadSound();

            selectedAnimCoveringInFightReload = curSelectedFightInPointInfo.animCoveringReload.GetRandomAnimName();
            animToAnimCoveringReloadCFTime = defaultCoveringInFightToCoveringReloadCFTime;

            //soldAnimObj.animation[selectedAnimCoveringInFightReload.name].time = 0;
            //soldAnimObj.animation.CrossFade(selectedAnimCoveringInFightReload.name, animToAnimCoveringReloadCFTime);

            soldInfo.StartNewMainAnimWithCrossfadeTime(selectedAnimCoveringInFightReload, animToAnimCoveringReloadCFTime);

            animToAnimCoveringAllBodyCFTime = defaultCoveringReloadToCoveringInFightCFTime;

            step = StepEnum.CoveringReload02;
            goto EndSteps;
        }
        #endregion

        #region ReloadInCovering02
        if (step == StepEnum.CoveringReload02)
        {
            SetSituation(SituationEnum.covering);

            //animLength = soldAnimObj.animation[selectedAnimCoveringInFightReload.name].length;
            //passedAnimTime = soldAnimObj.animation[selectedAnimCoveringInFightReload.name].time;

            //if (animLength - passedAnimTime <= animToAnimCoveringAllBodyCFTime)
            //{
            //    animToAnimCoveringAllBodyCFTime = animLength - passedAnimTime;
            //    step = StepEnum.Covering01;
            //    goto StartSteps;
            //}

            PlayDamageSoundIfRecieved();


            if (soldInfo.CheckMainAnimIsFinished(animToAnimCoveringAllBodyCFTime))
            {
                isReloadingInFight = false;

                animToAnimCoveringAllBodyCFTime = soldInfo.mainAnimRemainingTime;
                step = StepEnum.Covering01;
                goto StartSteps;
            }
        }
        #endregion

        #region CoveringRelax00
        if (step == StepEnum.CoveringRelax00)
        {
            SetSituation(SituationEnum.covering);

            soldGun.Reload();

            step = StepEnum.CoveringRelax01_1;
            goto StartSteps;
        }
        #endregion

        #region CoveringRelax01_1
        if (step == StepEnum.CoveringRelax01_1)
        {
            SetSituation(SituationEnum.covering);

            selectedAnimCoveringRelax = curSelectedFightInPointInfo.animCoveringRelax.GetRandomAnimName();

            //soldAnimObj.animation[selectedAnimCoveringRelax.name].time = 0;
            //soldAnimObj.animation.CrossFade(selectedAnimCoveringRelax.name, animToAnimCoveringAllBodyCFTime);

            soldInfo.StartNewMainAnimWithCrossfadeTime(selectedAnimCoveringRelax, animToAnimCoveringAllBodyCFTime);

            step = StepEnum.CoveringRelax01_2;
        }
        #endregion

        #region CoveringRelax01_2
        if (step == StepEnum.CoveringRelax01_2)
        {
            SetSituation(SituationEnum.covering);

            PlayDamageSoundIfRecieved();

            if (soldInfo.IsFullyInNewMainAnim())
            {
                step = StepEnum.CoveringRelax02;
                goto StartSteps;
            }

            goto EndSteps;
        }
        #endregion

        #region CoveringRelax02
        if (step == StepEnum.CoveringRelax02)
        {
            SetSituation(SituationEnum.covering);

            if (CheckFirstDamage())
            {
                dmg = soldInfo.firstDamage;
                step = StepEnum.CoveringRelaxDamage01;
                goto StartSteps;
            }

            if (needsToBeFinished)
            {
                SetFinished(true);
                return;
            }

            if (CheckSlowlyNeedsToGetBackToThePoint())
            {
                step = StepEnum.CoveringGettingBackToPosition01;
                goto StartSteps;
            }

            if (CheckSlowlyIsEnemyAround())
            {
                step = StepEnum.Covering02;
                goto EndSteps;
            }

            //animLength = soldAnimObj.animation[selectedAnimCoveringRelax.name].length;
            //passedAnimTime = soldAnimObj.animation[selectedAnimCoveringRelax.name].time;

            //if (animLength - passedAnimTime <= defaultCoveringsCFTime)
            //{
            //    animToAnimCoveringAllBodyCFTime = animLength - passedAnimTime;
            //    step = StepEnum.CoveringRelax01_1;
            //    goto StartSteps;
            //}

            if (soldInfo.CheckMainAnimIsFinished(defaultCoveringsCFTime))
            {
                animToAnimCoveringAllBodyCFTime = soldInfo.mainAnimRemainingTime;
                step = StepEnum.CoveringRelax01_1;
                goto StartSteps;
            }
        }
        #endregion

        #region CoveringRelaxDamage01
        if (step == StepEnum.CoveringRelaxDamage01)
        {
            SetSituation(SituationEnum.covering);

            PlayDamageSound(dmg);

            selectedAnimCoveringRelaxDamage = curSelectedFightInPointInfo.animDamageCoveringRelax.GetRandomAnim(dmg);
            animToAnimCoveringDamageCFTime = defaultCoveringRelaxToCoveringRelaxDamageCFTime;

            //soldAnimObj.animation[selectedAnimCoveringRelaxDamage.name].time = 0;
            //soldAnimObj.animation.CrossFade(selectedAnimCoveringRelaxDamage.name, animToAnimCoveringDamageCFTime);

            soldInfo.StartNewMainAnimWithCrossfadeTime(selectedAnimCoveringRelaxDamage, animToAnimCoveringDamageCFTime);

            animToAnimCoveringAllBodyCFTime = defaultCoveringRelaxDamageToCoveringRelaxCFTime;

            step = StepEnum.CoveringRelaxDamage02;
            goto EndSteps;
        }
        #endregion

        #region CoveringRelaxDamage02
        if (step == StepEnum.CoveringRelaxDamage02)
        {
            SetSituation(SituationEnum.covering);

            //animLength = soldAnimObj.animation[selectedAnimCoveringRelaxDamage.name].length;
            //passedAnimTime = soldAnimObj.animation[selectedAnimCoveringRelaxDamage.name].time;

            //if (animLength - passedAnimTime <= animToAnimCoveringAllBodyCFTime)
            //{
            //    animToAnimCoveringAllBodyCFTime = animLength - passedAnimTime;
            //    step = StepEnum.CoveringRelax00;
            //    goto StartSteps;
            //}

            if (soldInfo.CheckMainAnimIsFinished(animToAnimCoveringAllBodyCFTime))
            {
                animToAnimCoveringAllBodyCFTime = soldInfo.mainAnimRemainingTime;
                step = StepEnum.CoveringRelax00;
                goto StartSteps;
            }
        }
        #endregion

        #region CoveringGrenade01
        if (step == StepEnum.CoveringGrenade01)
        {
            SetSituation(SituationEnum.covering);

            grenadeTimeCounter = curSelectedFightInPointInfo.grenadeCreationDelayInAnim;

            selectedAnimCoveringGrenade = curSelectedFightInPointInfo.animGrenade.GetRandomAnimName();
            animToAnimCoveringGrenadeCFTime = defaultCoveringInFightToGrenadeCFTime;

            soldInfo.StartNewMainAnimWithCrossfadeTime(selectedAnimCoveringGrenade, animToAnimCoveringGrenadeCFTime);

            step = StepEnum.CoveringGrenade02;
            goto EndSteps;
        }
        #endregion

        #region CoveringGrenade02
        if (step == StepEnum.CoveringGrenade02)
        {
            SetSituation(SituationEnum.covering);

            grenadeTimeCounter = MathfPlus.DecByDeltatimeToZero(grenadeTimeCounter);

            if (grenadeTimeCounter == 0)
            {
                ThrowGrenade();
                grenadeTimeCounter = 1000000;
            }

            if (soldInfo.CheckMainAnimIsFinished(defaultGrenadeToCoveringInFightCFTime))
            {
                animToAnimCoveringAllBodyCFTime = soldInfo.mainAnimRemainingTime;

                step = StepEnum.CoveringInFight00;
                goto StartSteps;
            }
        }
        #endregion

        //

        #region Idle01
        if (step == StepEnum.Idle01)
        {
            SetSituation(SituationEnum.idle);

            if (useStartCFTime)
            {
                useStartCFTime = false;
                animToAnimIdleAllBodyCFTime = defaultIdleStartAllBodyCFTime;
            }
            else
                animToAnimIdleAllBodyCFTime = defaultIdleAllBodyCFTime;

            step = StepEnum.Idle02;
        }
        #endregion

        #region Idle02
        if (step == StepEnum.Idle02)
        {
            SetSituation(SituationEnum.idle);

            if (IsEnemyAround())
            {
                step = StepEnum.IdleInfight01;
            }
            else
            {
                step = StepEnum.IdleRelax01;
            }
        }
        #endregion

        #region IdleInfight01
        if (step == StepEnum.IdleInfight01)
        {
            SetSituation(SituationEnum.idle);

            selectedAnimIdleInFight = curSelectedFightInPointInfo.animIdleInFight.GetRandomAnimName();
            step = StepEnum.IdleInfight02;
        }
        #endregion

        #region IdleInfight02
        if (step == StepEnum.IdleInfight02)
        {
            SetSituation(SituationEnum.idle);

            soldInfo.StartNewMainAnimWithCrossfadeTime(selectedAnimIdleInFight, animToAnimIdleAllBodyCFTime);

            step = StepEnum.IdleInfight03;
        }
        #endregion

        #region IdleInfight03
        if (step == StepEnum.IdleInfight03)
        {
            SetSituation(SituationEnum.idle);

            PlayDamageSoundIfRecieved();

            if (soldInfo.IsFullyInNewMainAnim())
            {
                step = StepEnum.IdleInfight04;
                goto StartSteps;
            }

            goto EndSteps;
        }
        #endregion

        #region IdleInfight04
        if (step == StepEnum.IdleInfight04)
        {
            SetSituation(SituationEnum.idle);

            if (CheckFirstDamage())
            {
                dmg = soldInfo.firstDamage;
                step = StepEnum.IdleInfightDamage01;
                goto StartSteps;
            }

            if (needsToBeFinished)
            {
                SetFinished(true);
                return;
            }

            if (NeedsReload())
            {
                step = StepEnum.IdleReload01;
                goto StartSteps;
            }

            if (CheckAndStartPlayerCriticalState(FightCriticWithPlayerPos.Covering))
            {
                step = StepEnum.Idle02;
                animToAnimIdleAllBodyCFTime = defaultIdleAllBodyCFTime_Slower;
                goto StartSteps;
            }

            if (CheckAndEndPlayerCriticalState())
            {
                step = StepEnum.Start01;
                goto StartSteps;
            }

            if (CheckSlowlyNeedsToGetBackToThePoint())
            {
                step = StepEnum.IdleGettingBackToPosition01;
                goto StartSteps;
            }

            if (!CheckSlowlyIsEnemyAround())
            {
                step = StepEnum.Idle01;
                goto EndSteps;
            }

            if (idleTargetCheckTimeCounter == 0)
            {
                UpdateEnemies();

                if (sortedEnemies.Count == 0)
                {
                    if (!nowInCriticalFightWithPlayer)
                    {
                        SoldierFightInPointInfo newFInfo = SelectNewFightInPointInfo(false);

                        if (newFInfo != curSelectedFightInPointInfo)
                        {
                            InitNewFightInPointInfo(newFInfo);
                            idleTargetCheckTimeCounter = GetIdleTargetCheckTime();
                            animToAnimIdleAllBodyCFTime = defaultCurrentIdleInfightToNewIdleInfightCFTime;
                            step = StepEnum.Idle02;
                            goto EndSteps;
                        }
                        else
                        {
                            idleTargetCheckTimeCounter = GetIdleTargetCheckTime();
                        }
                    }
                    else
                    {
                        idleTargetCheckTimeCounter = GetIdleTargetCheckTime();
                    }
                }
                else
                {
                    SetNewTarget(sortedEnemies[0]);

                    step = StepEnum.NormalShooting01;
                    goto StartSteps;
                }
            }

            if (soldInfo.CheckMainAnimIsFinished(defaultIdleAllBodyCFTime))
            {
                animToAnimIdleAllBodyCFTime = soldInfo.mainAnimRemainingTime;
                step = StepEnum.IdleInfight01;
                goto StartSteps;
            }
        }
        #endregion

        #region IdleInFightDamage01
        if (step == StepEnum.IdleInfightDamage01)
        {
            SetSituation(SituationEnum.idle);

            PlayDamageSound(dmg);

            selectedAnimIdleInFightDamage = curSelectedFightInPointInfo.animDamageIdleInFight.GetRandomAnim(dmg);
            animToAnimIdleDamageCFTime = defaultIdleInFightToIdleInFightDamageCFTime;

            soldInfo.StartNewMainAnimWithCrossfadeTime(selectedAnimIdleInFightDamage, animToAnimIdleDamageCFTime);

            animToAnimIdleAllBodyCFTime = defaultIdleInFightDamageToIdleInFightCFTime;

            step = StepEnum.IdleInfightDamage02;
            goto EndSteps;
        }
        #endregion

        #region IdleInfightDamage02
        if (step == StepEnum.IdleInfightDamage02)
        {
            SetSituation(SituationEnum.idle);

            if (soldInfo.CheckMainAnimIsFinished(animToAnimIdleAllBodyCFTime))
            {
                animToAnimIdleAllBodyCFTime = soldInfo.mainAnimRemainingTime;
                idleTargetCheckTimeCounter = 0;
                step = StepEnum.IdleInfight01;
                goto StartSteps;
            }
        }
        #endregion

        #region ReloadInIdle01
        if (step == StepEnum.IdleReload01)
        {
            SetSituation(SituationEnum.idle);

            PlayDamageSoundIfRecieved();

            if (voiceSituation == AIVoiceSituation.Agressive)
                TryPlayVoice_ImReloading();

            if (IsEnemyAround())
            {
                isReloadingInFight = true;
            }

            soldGun.Reload();
            soldGun.PlayReloadSound();

            selectedAnimIdleInFightReload = curSelectedFightInPointInfo.animIdleReload.GetRandomAnimName();
            animToAnimIdleReloadCFTime = defaultIdleInFightToIdleReloadCFTime;

            soldInfo.StartNewMainAnimWithCrossfadeTime(selectedAnimIdleInFightReload, animToAnimIdleReloadCFTime);

            animToAnimIdleAllBodyCFTime = defaultIdleReloadToIdleInFightCFTime;

            step = StepEnum.IdleReload02;
            goto EndSteps;
        }
        #endregion

        #region ReloadInIdle02
        if (step == StepEnum.IdleReload02)
        {
            SetSituation(SituationEnum.idle);

            PlayDamageSoundIfRecieved();

            if (soldInfo.CheckMainAnimIsFinished(animToAnimIdleAllBodyCFTime))
            {
                isReloadingInFight = false;

                animToAnimIdleAllBodyCFTime = soldInfo.mainAnimRemainingTime;
                step = StepEnum.Idle01;
                goto StartSteps;
            }
        }
        #endregion

        #region IdleRelax01
        if (step == StepEnum.IdleRelax01)
        {
            SetSituation(SituationEnum.idle);

            soldGun.Reload();

            step = StepEnum.IdleRelax02;
        }
        #endregion

        #region IdleRelax02
        if (step == StepEnum.IdleRelax02)
        {
            SetSituation(SituationEnum.idle);

            selectedAnimIdleRelax = curSelectedFightInPointInfo.animIdleRelax.GetRandomAnimName();

            soldInfo.StartNewMainAnimWithCrossfadeTime(selectedAnimIdleRelax, animToAnimIdleAllBodyCFTime);

            step = StepEnum.IdleRelax03;
        }
        #endregion

        #region IdleRelax03
        if (step == StepEnum.IdleRelax03)
        {
            SetSituation(SituationEnum.idle);

            PlayDamageSoundIfRecieved();

            if (soldInfo.IsFullyInNewMainAnim())
            {
                step = StepEnum.IdleRelax04;
                goto StartSteps;
            }

            goto EndSteps;
        }
        #endregion

        #region IdleRelax04
        if (step == StepEnum.IdleRelax04)
        {
            SetSituation(SituationEnum.idle);

            if (CheckFirstDamage())
            {
                dmg = soldInfo.firstDamage;
                step = StepEnum.IdleRelaxDamage01;
                goto StartSteps;
            }

            if (needsToBeFinished)
            {
                SetFinished(true);
                return;
            }

            if (CheckAndEndPlayerCriticalState())
            {
                step = StepEnum.Start01;
                goto StartSteps;
            }

            if (CheckSlowlyNeedsToGetBackToThePoint())
            {
                step = StepEnum.IdleGettingBackToPosition01;
                goto StartSteps;
            }

            if (CheckSlowlyIsEnemyAround())
            {
                step = StepEnum.Idle01;
                goto EndSteps;
            }

            if (soldInfo.CheckMainAnimIsFinished(defaultIdleAllBodyCFTime))
            {
                animToAnimIdleAllBodyCFTime = soldInfo.mainAnimRemainingTime;
                step = StepEnum.IdleRelax02;
                goto StartSteps;
            }
        }
        #endregion

        #region IdleRelaxDamage01
        if (step == StepEnum.IdleRelaxDamage01)
        {
            SetSituation(SituationEnum.idle);

            PlayDamageSound(dmg);

            selectedAnimIdleRelaxDamage = curSelectedFightInPointInfo.animDamageIdleRelax.GetRandomAnim(dmg);
            animToAnimIdleDamageCFTime = defaultIdleRelaxToIdleRelaxDamageCFTime;

            soldInfo.StartNewMainAnimWithCrossfadeTime(selectedAnimIdleRelaxDamage, animToAnimIdleDamageCFTime);

            animToAnimIdleAllBodyCFTime = defaultIdleRelaxDamageToIdleRelaxCFTime;

            step = StepEnum.IdleRelaxDamage02;
            goto EndSteps;
        }
        #endregion

        #region IdleRelaxDamage02
        if (step == StepEnum.IdleRelaxDamage02)
        {
            SetSituation(SituationEnum.idle);

            if (soldInfo.CheckMainAnimIsFinished(animToAnimIdleAllBodyCFTime))
            {
                animToAnimIdleAllBodyCFTime = soldInfo.mainAnimRemainingTime;
                step = StepEnum.IdleRelax01;
                goto StartSteps;
            }
        }
        #endregion

        //

        #region NormShoot_ExternalAnimToShoot01
        if (step == StepEnum.NormShoot_ExternalAnimToShoot01)
        {
            SetSituation(SituationEnum.normalFighting);

            StartActInit();
            SetGentlyRotateAllBodyToTarget(true);

            step = StepEnum.NormShoot_Shooting03_2;


            goto StartSteps;

        }
        #endregion

        #region NormalShooting01
        if (step == StepEnum.NormalShooting01)
        {
            SetSituation(SituationEnum.normalFighting);

            SetGentlyRotateAllBodyToTarget(true);

            if (fightType == FightInPointTypeEnum.NormalCover)
            {
                step = StepEnum.NormShoot_CoveringToShooting01;
                goto StartSteps;
            }

            if (fightType == FightInPointTypeEnum.NoCover)
            {
                step = StepEnum.NormShoot_IdleToShooting01;
                goto StartSteps;
            }
        }
        #endregion

        #region NormShoot_CoveringToShooting01
        if (step == StepEnum.NormShoot_CoveringToShooting01)
        {
            SetSituation(SituationEnum.normalFighting);

            currentGunDirection = SoldierStats.GetSoldierGunDirectionForTarget(controlledSoldier.gameObject, selectedTarget.character);

            if (curSelectedFightInPointInfo.GetAnimList_CoveringToShootAllBody(currentGunDirection) != null)
            {
                string[] covToShootAnims = curSelectedFightInPointInfo.GetAnimList_CoveringToShootAllBody(currentGunDirection).GetAnimNames();

                if (covToShootAnims != null && covToShootAnims.Length > 0)
                {
                    selectedAnimCoveringToShootAllBody = covToShootAnims[Random.Range(0, covToShootAnims.Length)];
                    animToAnimCoveringToShootAllBodyCFTime = defaultCovering_To_CoveringToShootAllBodyCFTime;

                    step = StepEnum.NormShoot_CoveringToShootingAnim01;
                    goto StartSteps;
                }
            }
            else
            {
                string[] fightLookAnims = curSelectedFightInPointInfo.GetAnimList_FightLookAllBody(currentGunDirection).GetAnimNames();

                selectedAnimCoveringToShootAllBody = fightLookAnims[Random.Range(0, fightLookAnims.Length)];
                animToAnimFightLookAllBodyCFTime = defaultCoveringToFightLookAllBodyCFTimeForNoAnim;

                step = StepEnum.NormShoot_CoveringToShootingCrossfade01;
                goto StartSteps;
            }
        }
        #endregion

        #region NormShoot_CoveringToShootingAnim01
        if (step == StepEnum.NormShoot_CoveringToShootingAnim01)
        {
            SetSituation(SituationEnum.normalFighting);

            //soldAnimObj.animation[selectedAnimCoveringToShootAllBody.name].time = 0;
            //soldAnimObj.animation.CrossFade(selectedAnimCoveringToShootAllBody.name, animToAnimCoveringToShootAllBodyCFTime);

            soldInfo.StartNewMainAnimWithCrossfadeTime(selectedAnimCoveringToShootAllBody, animToAnimCoveringToShootAllBodyCFTime);

            step = StepEnum.NormShoot_CoveringToShootingAnim02;
            goto EndSteps;
        }
        #endregion

        #region NormShoot_CoveringToShootingAnim02
        if (step == StepEnum.NormShoot_CoveringToShootingAnim02)
        {
            SetSituation(SituationEnum.normalFighting);

            //animLength = soldAnimObj.animation[selectedAnimCoveringToShootAllBody.name].length;
            //passedAnimTime = soldAnimObj.animation[selectedAnimCoveringToShootAllBody.name].time;

            //if (animLength - passedAnimTime <= defaultCoveringToFightLookAllBodyCFTime)
            //{
            //    animToAnimFightLookAllBodyCFTime = animLength - passedAnimTime;

            //    step = StepEnum.NormShoot_Shooting02;
            //    goto StartSteps;
            //}

            PlayDamageSoundIfRecieved();

            if (soldInfo.CheckMainAnimIsFinished(defaultCoveringToFightLookAllBodyCFTime))
            {
                animToAnimFightLookAllBodyCFTime = soldInfo.mainAnimRemainingTime;

                step = StepEnum.NormShoot_Shooting02;
                goto StartSteps;
            }
        }
        #endregion

        #region NormShoot_CoveringToShootingCrossfade01
        if (step == StepEnum.NormShoot_CoveringToShootingCrossfade01)
        {
            SetSituation(SituationEnum.normalFighting);

            //soldAnimObj.animation[selectedAnimCoveringToShootAllBody.name].time = 0;
            //soldAnimObj.animation.CrossFade(selectedAnimCoveringToShootAllBody.name, animToAnimFightLookAllBodyCFTime);

            soldInfo.StartNewMainAnimWithCrossfadeTime(selectedAnimCoveringToShootAllBody, animToAnimFightLookAllBodyCFTime);

            //animCoveringCrossfadeToFightLookTimeCounter = defaultCoveringToFightLookAllBodyCFTime;

            step = StepEnum.NormShoot_CoveringToShootingCrossfade02;
            goto EndSteps;
        }
        #endregion

        #region NormShoot_CoveringToShootingCrossfade02
        if (step == StepEnum.NormShoot_CoveringToShootingCrossfade02)
        {
            SetSituation(SituationEnum.normalFighting);

            PlayDamageSoundIfRecieved();

            if (soldInfo.IsFullyInNewMainAnim())
            {
                step = StepEnum.NormShoot_Shooting01;
                goto StartSteps;
            }
        }
        #endregion

        #region NormShoot_IdleToShooting01
        if (step == StepEnum.NormShoot_IdleToShooting01)
        {
            SetSituation(SituationEnum.normalFighting);

            currentGunDirection = SoldierStats.GetSoldierGunDirectionForTarget(controlledSoldier.gameObject, selectedTarget.character);

            if (curSelectedFightInPointInfo.GetAnimList_IdleToShootAllBody(currentGunDirection) != null)
            {
                string[] idleToShootAnims = curSelectedFightInPointInfo.GetAnimList_IdleToShootAllBody(currentGunDirection).GetAnimNames();

                if (idleToShootAnims != null && idleToShootAnims.Length > 0)
                {
                    selectedAnimIdleToShootAllBody = idleToShootAnims[Random.Range(0, idleToShootAnims.Length)];
                    animToAnimIdleToShootAllBodyCFTime = defaultIdle_To_IdleToShootAllBodyCFTime;

                    step = StepEnum.NormShoot_IdleToShootingAnim01;
                    goto StartSteps;
                }
            }
            else
            {
                string[] fightLookAnims = curSelectedFightInPointInfo.GetAnimList_FightLookAllBody(currentGunDirection).GetAnimNames();

                selectedAnimIdleToShootAllBody = fightLookAnims[Random.Range(0, fightLookAnims.Length)];
                animToAnimFightLookAllBodyCFTime = defaultIdleToFightLookAllBodyCFTimeForNoAnim;

                step = StepEnum.NormShoot_IdleToShootingCrossfade01;
                goto StartSteps;
            }
        }
        #endregion

        #region NormShoot_IdleToShootingAnim01
        if (step == StepEnum.NormShoot_IdleToShootingAnim01)
        {
            SetSituation(SituationEnum.normalFighting);

            soldInfo.StartNewMainAnimWithCrossfadeTime(selectedAnimIdleToShootAllBody, animToAnimIdleToShootAllBodyCFTime);

            step = StepEnum.NormShoot_IdleToShootingAnim02;
            goto EndSteps;
        }
        #endregion

        #region NormShoot_IdleToShootingAnim02
        if (step == StepEnum.NormShoot_IdleToShootingAnim02)
        {
            SetSituation(SituationEnum.normalFighting);

            PlayDamageSoundIfRecieved();

            if (soldInfo.CheckMainAnimIsFinished(defaultIdleToFightLookAllBodyCFTime))
            {
                animToAnimFightLookAllBodyCFTime = soldInfo.mainAnimRemainingTime;

                step = StepEnum.NormShoot_Shooting02;
                goto StartSteps;
            }
        }
        #endregion

        #region NormShoot_IdleToShootingCrossfade01
        if (step == StepEnum.NormShoot_IdleToShootingCrossfade01)
        {
            SetSituation(SituationEnum.normalFighting);

            soldInfo.StartNewMainAnimWithCrossfadeTime(selectedAnimIdleToShootAllBody, animToAnimFightLookAllBodyCFTime);

            animIdleCrossfadeToFightLookTimeCounter = defaultIdleToFightLookAllBodyCFTime;

            step = StepEnum.NormShoot_IdleToShootingCrossfade02;
            goto EndSteps;
        }
        #endregion

        #region NormShoot_IdleToShootingCrossfade02
        if (step == StepEnum.NormShoot_IdleToShootingCrossfade02)
        {
            SetSituation(SituationEnum.normalFighting);

            PlayDamageSoundIfRecieved();

            animIdleCrossfadeToFightLookTimeCounter -= Time.deltaTime;
            if (animIdleCrossfadeToFightLookTimeCounter <= 0)
            {
                step = StepEnum.NormShoot_Shooting01;
                goto StartSteps;
            }
        }
        #endregion

        #region NormShoot_Shooting01
        if (step == StepEnum.NormShoot_Shooting01)
        {
            SetSituation(SituationEnum.normalFighting);

            animToAnimFightLookAllBodyCFTime = defaultFightLookAllBodyCFTime;

            step = StepEnum.NormShoot_Shooting02;
            goto StartSteps;
        }
        #endregion

        #region NormShoot_Shooting02
        if (step == StepEnum.NormShoot_Shooting02)
        {
            SetSituation(SituationEnum.normalFighting);

            string[] fightLookAnims = curSelectedFightInPointInfo.GetAnimList_FightLookAllBody(currentGunDirection).GetAnimNames();

            selectedAnimFightLookAllBody = fightLookAnims[Random.Range(0, fightLookAnims.Length)];

            step = StepEnum.NormShoot_Shooting03_1;
            goto StartSteps;
        }
        #endregion

        #region NormShoot_Shooting03_1
        if (step == StepEnum.NormShoot_Shooting03_1)
        {
            SetSituation(SituationEnum.normalFighting);

            //soldAnimObj.animation[selectedAnimFightLookAllBody.name].time = 0;
            //soldAnimObj.animation.CrossFade(selectedAnimFightLookAllBody.name, animToAnimFightLookAllBodyCFTime);

            soldInfo.StartNewMainAnimWithCrossfadeTime(selectedAnimFightLookAllBody, animToAnimFightLookAllBodyCFTime);

            step = StepEnum.NormShoot_Shooting03_2;
        }
        #endregion

        #region NormShoot_Shooting03_2
        if (step == StepEnum.NormShoot_Shooting03_2)
        {
            SetSituation(SituationEnum.normalFighting);

            PlayDamageSoundIfRecieved();

            if (soldInfo.IsFullyInNewMainAnim())
            {
                step = StepEnum.NormShoot_Shooting04;
                goto StartSteps;
            }

            goto EndSteps;
        }
        #endregion

        #region NormShoot_Shooting02A
        if (step == StepEnum.NormShoot_Shooting02A)
        {
            SetSituation(SituationEnum.normalFighting);

            string[] fightLookAnims = curSelectedFightInPointInfo.GetAnimList_FightLookAllBody(currentGunDirection).GetAnimNames();

            selectedAnimFightLookAllBody = fightLookAnims[Random.Range(0, fightLookAnims.Length)];

            step = StepEnum.NormShoot_Shooting03_1A;
            goto StartSteps;
        }
        #endregion

        #region NormShoot_Shooting03_1A
        if (step == StepEnum.NormShoot_Shooting03_1A)
        {
            SetSituation(SituationEnum.normalFighting);

            //soldAnimObj.animation[selectedAnimFightLookAllBody.name].time = 0;
            //soldAnimObj.animation.CrossFade(selectedAnimFightLookAllBody.name, animToAnimFightLookAllBodyCFTime);

            soldInfo.StartNewMainAnimWithCrossfadeTime(selectedAnimFightLookAllBody, animToAnimFightLookAllBodyCFTime);

            step = StepEnum.NormShoot_Shooting03_2A;
        }
        #endregion

        #region NormShoot_Shooting03_2A
        if (step == StepEnum.NormShoot_Shooting03_2A)
        {
            SetSituation(SituationEnum.normalFighting);

            PlayDamageSoundIfRecieved();

            if (soldInfo.IsFullyInNewMainAnim())
            {
                step = StepEnum.NormShoot_Shooting05;
                goto StartSteps;
            }

            goto EndSteps;
        }
        #endregion

        #region NormShoot_Shooting04
        if (step == StepEnum.NormShoot_Shooting04)
        {
            SetSituation(SituationEnum.normalFighting);

            ResetTargetRecheckTime();

            ResetCheckGetBackToPosTimeCounter();

            ResetShootingTime();

            step = StepEnum.NormShoot_Shooting05;
            goto StartSteps;
        }
        #endregion

        #region NormShoot_Shooting05
        if (step == StepEnum.NormShoot_Shooting05)
        {
            SetSituation(SituationEnum.normalFighting);

            SetGentlyRotateAllBodyToTarget(true);

            step = StepEnum.NormShoot_Shooting06_Part1;
            goto StartSteps;
        }
        #endregion

        #region NormShoot_Shooting06
        if (step == StepEnum.NormShoot_Shooting06_Part1)
        {
            SetSituation(SituationEnum.normalFighting);

            if (voiceSituation == AIVoiceSituation.Agressive)
                TryPlayVoice_Agressive();

            isTargetChecked = false;

            if (CheckFirstDamage())
            {
                dmg = soldInfo.firstDamage;
                step = StepEnum.NormShoot_Damage01;
                goto StartSteps;
            }


            if (needsToBeFinished)
            {
                SetFinished(true);
                return;

                //step = StepEnum.NormShoot_FinishShooting01;
                //goto StartSteps;
            }


            if (NeedsReload())
            {
                if (fightType == FightInPointTypeEnum.NoCover)
                {
                    step = StepEnum.NormShoot_IdleReload01;
                    goto StartSteps;
                }
                else
                {
                    step = StepEnum.NormShoot_FinishShooting01;
                    goto StartSteps;
                }
            }

            CheckAndStartPlayerCriticalState(FightCriticWithPlayerPos.NormShoot_Shooting);

            if (CheckAndEndPlayerCriticalState())
            {
                step = StepEnum.Start01;
                goto StartSteps;
            }

            if (IsShootingTimeFinished())
            {
                step = StepEnum.NormShoot_FinishShooting01;
                goto StartSteps;
            }

            if (!lastIsEnemyAroundResult)
            {
                step = StepEnum.NormShoot_FinishShooting01;
                goto StartSteps;
            }

            if (lastIsNotInPosResult)
            {
                step = StepEnum.NormShoot_GettingBackToPosition01;
                goto StartSteps;
            }

            if (!IsTargetTotallyFightable(selectedTarget)
                ||
                IsTargetLockTimeFinished())
            {
                UpdateEnemies();

                if (sortedEnemies.Count == 0)
                {
                    step = StepEnum.NormShoot_FinishShooting01;
                    goto StartSteps;
                }
                else
                {
                    SetNewTarget(sortedEnemies[0]);
                    isTargetChecked = true;
                }
            }

            if (lastIsGunDirNotTrueResult)
            {
                step = StepEnum.NormShoot_GunDirToGunDir01;
                goto StartSteps;
            }

            step = StepEnum.NormShoot_Shooting06_Part2;


            goto StartSteps;

        }

        if (step == StepEnum.NormShoot_Shooting06_Part2)
        {
            if (!isTargetChecked && targetRecheckTimeReached)
            {
                if (IsSelectedTargetOKForShootingNow())
                {
                    isTargetChecked = true;
                }
                else
                {
                    UpdateEnemies();

                    if (sortedEnemies.Count == 0)
                    {
                        step = StepEnum.NormShoot_FinishShooting01;
                        goto StartSteps;
                    }
                    else
                    {
                        SetNewTarget(sortedEnemies[0]);
                        isTargetChecked = true;
                    }
                }
            }

            if (lockAllBodyRotationOnTarget
               &&
               soldGun.IsReady())
            {
                if (!isTargetChecked)
                {
                    isTargetChecked = IsSelectedTargetOKForShootingNow();
                }

                if (isTargetChecked)
                {
                    step = StepEnum.NormShoot_ShootTarget01;
                    goto StartSteps;
                }
            }

            //animLength = soldAnimObj.animation[selectedAnimFightLookAllBody.name].length;
            //passedAnimTime = soldAnimObj.animation[selectedAnimFightLookAllBody.name].time;

            //if (animLength - passedAnimTime <= defaultFightLookAllBodyCFTime)
            //{
            //    animToAnimFightLookAllBodyCFTime = animLength - passedAnimTime;
            //    step = StepEnum.NormShoot_Shooting02A;
            //    goto StartSteps;
            //}

            if (soldInfo.CheckMainAnimIsFinished(defaultFightLookAllBodyCFTime))
            {
                animToAnimFightLookAllBodyCFTime = soldInfo.mainAnimRemainingTime;
                step = StepEnum.NormShoot_Shooting02A;
                goto StartSteps;
            }

            step = StepEnum.NormShoot_Shooting06_Part1;
        }
        #endregion

        #region NormShoot_Damage01
        if (step == StepEnum.NormShoot_Damage01)
        {
            SetSituation(SituationEnum.normalFighting);

            PlayDamageSound(dmg);

            SoldierDamageAnimPack dmgPack = curSelectedFightInPointInfo.GetAnimDamagePack_ShootAllBodyDamage(currentGunDirection);
            selectedAnimFightLookAllBodyDamage = dmgPack.GetRandomAnim(dmg);

            animToAnimFightLookAllBodyDamageCFTime = defaultFightLookAllBodyDamageCFTime;

            //soldAnimObj.animation[selectedAnimFightLookAllBodyDamage.name].time = 0;
            //soldAnimObj.animation.CrossFade(selectedAnimFightLookAllBodyDamage.name, animToAnimFightLookAllBodyDamageCFTime);

            soldInfo.StartNewMainAnimWithCrossfadeTime(selectedAnimFightLookAllBodyDamage, animToAnimFightLookAllBodyDamageCFTime);

            animToAnimFightLookAllBodyCFTime = defaultDamageToFightLookAllBodyCFTime;

            SetGentlyRotateAllBodyToTarget(false);
            SetLockAllBodyRotationOnTarget(false);

            step = StepEnum.NormShoot_Damage02;
            goto EndSteps;
        }
        #endregion

        #region NormShoot_Damage02
        if (step == StepEnum.NormShoot_Damage02)
        {
            SetSituation(SituationEnum.normalFighting);

            //animLength = soldAnimObj.animation[selectedAnimFightLookAllBodyDamage.name].length;
            //passedAnimTime = soldAnimObj.animation[selectedAnimFightLookAllBodyDamage.name].time;

            //if (animLength - passedAnimTime <= animToAnimFightLookAllBodyCFTime)
            //{
            //    animToAnimFightLookAllBodyCFTime = animLength - passedAnimTime;
            //    step = StepEnum.NormShoot_Shooting02A;
            //    goto StartSteps;
            //}

            if (soldInfo.CheckMainAnimIsFinished(animToAnimFightLookAllBodyCFTime))
            {
                animToAnimFightLookAllBodyCFTime = soldInfo.mainAnimRemainingTime;
                step = StepEnum.NormShoot_Shooting02A;
                goto StartSteps;
            }
        }
        #endregion

        #region NormShoot_FinishShooting01
        if (step == StepEnum.NormShoot_FinishShooting01)
        {
            SetSituation(SituationEnum.normalFighting);

            SetGentlyRotateAllBodyToTarget(false);
            SetLockAllBodyRotationOnTarget(false);

            nowInCriticalFightWithPlayer = false;

            if (fightType == FightInPointTypeEnum.NormalCover)
            {
                step = StepEnum.NormShoot_ShootingToCovering01;
                goto StartSteps;
            }

            if (fightType == FightInPointTypeEnum.NoCover)
            {
                step = StepEnum.NormShoot_ShootingToIdle01;
                goto StartSteps;
            }
        }
        #endregion

        #region NormShoot_ShootingToCovering01
        if (step == StepEnum.NormShoot_ShootingToCovering01)
        {
            SetSituation(SituationEnum.covering);

            if (curSelectedFightInPointInfo.GetAnimList_ShootToCoveringAllBody(currentGunDirection) != null)
            {
                string[] shootToCovAnims = curSelectedFightInPointInfo.GetAnimList_ShootToCoveringAllBody(currentGunDirection).GetAnimNames();

                if (shootToCovAnims != null && shootToCovAnims.Length > 0)
                {
                    selectedAnimShootToCoveringAllBody = shootToCovAnims[Random.Range(0, shootToCovAnims.Length)];
                    animToAnimShootToCoveringAllBodyCFTime = defaultShoot_To_ShootToCoveringAllBodyCFTime;

                    step = StepEnum.NormShoot_ShootingToCoveringAnim01;
                    goto StartSteps;
                }
            }
            else
            {
                string[] coveringAnims = curSelectedFightInPointInfo.animCoveringInFight.GetAnimNames();

                selectedAnimShootToCoveringAllBody = coveringAnims[Random.Range(0, coveringAnims.Length)];
                animToAnimCoveringAllBodyCFTime = defaultFightLookToCoveringAllBodyCFTimeForNoAnim;

                step = StepEnum.NormShoot_ShootingToCoveringCrossfade01;
                goto StartSteps;
            }
        }
        #endregion

        #region NormShoot_ShootingToCoveringAnim01
        if (step == StepEnum.NormShoot_ShootingToCoveringAnim01)
        {
            SetSituation(SituationEnum.covering);

            //soldAnimObj.animation[selectedAnimShootToCoveringAllBody.name].time = 0;
            //soldAnimObj.animation.CrossFade(selectedAnimShootToCoveringAllBody.name, animToAnimShootToCoveringAllBodyCFTime);

            soldInfo.StartNewMainAnimWithCrossfadeTime(selectedAnimShootToCoveringAllBody, animToAnimShootToCoveringAllBodyCFTime);

            step = StepEnum.NormShoot_ShootingToCoveringAnim02;
            goto EndSteps;
        }
        #endregion

        #region NormShoot_ShootingToCoveringAnim02
        if (step == StepEnum.NormShoot_ShootingToCoveringAnim02)
        {
            SetSituation(SituationEnum.covering);

            //animLength = soldAnimObj.animation[selectedAnimShootToCoveringAllBody.name].length;
            //passedAnimTime = soldAnimObj.animation[selectedAnimShootToCoveringAllBody.name].time;

            //if (animLength - passedAnimTime <= defaultFightLookToCoveringAllBodyCFTime)
            //{
            //    animToAnimCoveringAllBodyCFTime = animLength - passedAnimTime;

            //    step = StepEnum.Covering01;
            //    goto StartSteps;
            //}

            PlayDamageSoundIfRecieved();

            if (soldInfo.CheckMainAnimIsFinished(defaultFightLookToCoveringAllBodyCFTime))
            {
                animToAnimCoveringAllBodyCFTime = soldInfo.mainAnimRemainingTime;

                step = StepEnum.Covering01;
                goto StartSteps;
            }
        }
        #endregion

        #region NormShoot_ShootingToCoveringCrossfade01
        if (step == StepEnum.NormShoot_ShootingToCoveringCrossfade01)
        {
            SetSituation(SituationEnum.covering);

            //soldAnimObj.animation[selectedAnimShootToCoveringAllBody.name].time = 0;
            //soldAnimObj.animation.CrossFade(selectedAnimShootToCoveringAllBody.name, animToAnimCoveringAllBodyCFTime);

            soldInfo.StartNewMainAnimWithCrossfadeTime(selectedAnimShootToCoveringAllBody, animToAnimCoveringAllBodyCFTime);

            //animFightLookCrossfadeToCoveringTimeCounter = animToAnimCoveringAllBodyCFTime;

            step = StepEnum.NormShoot_ShootingToCoveringCrossfade02;
            goto EndSteps;
        }
        #endregion

        #region NormShoot_ShootingToCoveringCrossfade02
        if (step == StepEnum.NormShoot_ShootingToCoveringCrossfade02)
        {
            SetSituation(SituationEnum.covering);

            PlayDamageSoundIfRecieved();

            if (soldInfo.IsFullyInNewMainAnim())
            {
                //<Bad>
                step = StepEnum.Covering01;
                //</Bad>
                goto StartSteps;
            }

            //animFightLookCrossfadeToCoveringTimeCounter -= Time.deltaTime;
            //if (animFightLookCrossfadeToCoveringTimeCounter <= 0)
            //{
            //    //<Bad>
            //    step = StepEnum.Covering01;
            //    //</Bad>
            //    goto StartSteps;
            //}
        }
        #endregion

        #region NormShoot_ShootingToIdle01
        if (step == StepEnum.NormShoot_ShootingToIdle01)
        {
            SetSituation(SituationEnum.idle);

            if (curSelectedFightInPointInfo.GetAnimList_ShootToIdleAllBody(currentGunDirection) != null)
            {
                string[] shootToIdleAnims = curSelectedFightInPointInfo.GetAnimList_ShootToIdleAllBody(currentGunDirection).GetAnimNames();

                if (shootToIdleAnims != null && shootToIdleAnims.Length > 0)
                {
                    selectedAnimShootToIdleAllBody = shootToIdleAnims[Random.Range(0, shootToIdleAnims.Length)];
                    animToAnimShootToIdleAllBodyCFTime = defaultShoot_To_ShootToIdleAllBodyCFTime;

                    step = StepEnum.NormShoot_ShootingToIdleAnim01;
                    goto StartSteps;
                }
            }
            else
            {
                string[] idleAnims = curSelectedFightInPointInfo.animIdleInFight.GetAnimNames();

                selectedAnimShootToIdleAllBody = idleAnims[Random.Range(0, idleAnims.Length)];
                animToAnimIdleAllBodyCFTime = defaultFightLookToIdleAllBodyCFTimeForNoAnim;

                step = StepEnum.NormShoot_ShootingToIdleCrossfade01;
                goto StartSteps;
            }
        }
        #endregion

        #region NormShoot_ShootingToIdleAnim01
        if (step == StepEnum.NormShoot_ShootingToIdleAnim01)
        {
            SetSituation(SituationEnum.idle);

            soldInfo.StartNewMainAnimWithCrossfadeTime(selectedAnimShootToIdleAllBody, animToAnimShootToIdleAllBodyCFTime);

            step = StepEnum.NormShoot_ShootingToIdleAnim02;
            goto EndSteps;
        }
        #endregion

        #region NormShoot_ShootingToIdleAnim02
        if (step == StepEnum.NormShoot_ShootingToIdleAnim02)
        {
            SetSituation(SituationEnum.idle);

            PlayDamageSoundIfRecieved();

            if (soldInfo.CheckMainAnimIsFinished(defaultFightLookToIdleAllBodyCFTime))
            {
                animToAnimIdleAllBodyCFTime = soldInfo.mainAnimRemainingTime;

                step = StepEnum.Idle01;
                goto StartSteps;
            }
        }
        #endregion

        #region NormShoot_ShootingToIdleCrossfade01
        if (step == StepEnum.NormShoot_ShootingToIdleCrossfade01)
        {
            SetSituation(SituationEnum.idle);

            soldInfo.StartNewMainAnimWithCrossfadeTime(selectedAnimShootToIdleAllBody, animToAnimIdleAllBodyCFTime);

            animFightLookCrossfadeToIdleTimeCounter = animToAnimIdleAllBodyCFTime;

            step = StepEnum.NormShoot_ShootingToIdleCrossfade02;
            goto EndSteps;
        }
        #endregion

        #region NormShoot_ShootingToIdleCrossfade02
        if (step == StepEnum.NormShoot_ShootingToIdleCrossfade02)
        {
            SetSituation(SituationEnum.idle);

            PlayDamageSoundIfRecieved();

            if (soldInfo.IsFullyInNewMainAnim())
            {
                //<Bad>
                step = StepEnum.Idle01;
                //</Bad>
                goto StartSteps;
            }
        }
        #endregion

        #region NormShoot_GunDirToGunDir01
        if (step == StepEnum.NormShoot_GunDirToGunDir01)
        {
            SetSituation(SituationEnum.normalFighting);

            SoldierGunDirectionEnum newGunDir = SoldierGunDirectionEnum.Forward;

            if (selectedTarget.character != null)
                newGunDir = SoldierStats.GetSoldierGunDirectionForTarget(controlledSoldier.gameObject, selectedTarget.character);

            lastIsGunDirNotTrueResult = false;

            if (currentGunDirection == newGunDir)
            {
                step = StepEnum.NormShoot_Shooting06_Part2;
                goto StartSteps;
            }

            currentGunDirection = newGunDir;

            string[] fLookAnims = curSelectedFightInPointInfo.GetAnimList_FightLookAllBody(currentGunDirection).GetAnimNames();

            selectedAnimFightLookAllBody = fLookAnims[Random.Range(0, fLookAnims.Length)];
            animToAnimFightLookAllBodyCFTime = defaultGunDirToGunDirAllBodyCFTime;

            //soldAnimObj.animation[selectedAnimFightLookAllBody.name].time = 0;
            //soldAnimObj.animation.CrossFade(selectedAnimFightLookAllBody.name, animToAnimFightLookAllBodyCFTime);

            soldInfo.StartNewMainAnimWithCrossfadeTime(selectedAnimFightLookAllBody, animToAnimFightLookAllBodyCFTime);

            gunDirToGunDirTimeCounter = animToAnimFightLookAllBodyCFTime;

            step = StepEnum.NormShoot_GunDirToGunDir02;
            goto EndSteps;
        }
        #endregion

        #region NormShoot_GunDirToGunDir02
        if (step == StepEnum.NormShoot_GunDirToGunDir02)
        {
            SetSituation(SituationEnum.normalFighting);

            if (CheckFirstDamage())
            {
                dmg = soldInfo.firstDamage;
                step = StepEnum.NormShoot_Damage01;
                goto StartSteps;
            }

            gunDirToGunDirTimeCounter -= Time.deltaTime;

            if (gunDirToGunDirTimeCounter <= 0)
            {
                step = StepEnum.NormShoot_Shooting05;
                goto StartSteps;
            }
        }
        #endregion

        #region NormShoot_ShootTarget01
        if (step == StepEnum.NormShoot_ShootTarget01)
        {
            SetSituation(SituationEnum.normalFighting);

            Transform shootTarg;

            List<Transform> okPoses;

            if (selectedTarget.isCharacterHitted)
            {
                okPoses = selectedTarget.characterHittedPoses;
            }
            else
            {
                okPoses = selectedTarget.haloHittedPoses;
            }

            shootTarg = okPoses[Random.Range(0, okPoses.Count)];

            soldGun.TryFire(shootTarg.position);

            string[] shootAnims = curSelectedFightInPointInfo.GetAnimList_ShootAllBody(currentGunDirection).GetAnimNames();
            selectedAnimShootAllBody = shootAnims[Random.Range(0, shootAnims.Length)];
            //animToAnimShootAllBodyCFTime = defaultShootAllBodyCFTime;

            //animToAnimFightLookAllBodyCFTime = defaultShootAllBodyCFTime;

            animToAnimShootAllBodyCFTime = Mathf.Min(defaultShootAllBodyCFTime, (soldGun.fireTimeMin / 2.1f));

            animToAnimFightLookAllBodyCFTime = animToAnimShootAllBodyCFTime;

            soldInfo.StartNewMainAnimWithCrossfadeTime(selectedAnimShootAllBody, animToAnimShootAllBodyCFTime);

            //soldAnimObj.animation[selectedAnimShootAllBody.name].time = 0;
            //soldAnimObj.animation.CrossFade(selectedAnimShootAllBody.name, animToAnimShootAllBodyCFTime);

            step = StepEnum.NormShoot_ShootTarget02;
            goto EndSteps;
        }
        #endregion

        #region NormShoot_ShootTarget02
        if (step == StepEnum.NormShoot_ShootTarget02)
        {
            SetSituation(SituationEnum.normalFighting);

            if (CheckShootingDamage())
            {
                dmg = soldInfo.firstDamage;
                step = StepEnum.NormShoot_Damage01;
                goto StartSteps;
            }

            if (targetRecheckTimeReached)
            {
                if (!IsSelectedTargetOKForShootingNow())
                {
                    SetLockAllBodyRotationOnTarget(false);
                }
            }

            if (soldInfo.CheckMainAnimIsFinished(animToAnimFightLookAllBodyCFTime))
            {
                animToAnimFightLookAllBodyCFTime = soldInfo.mainAnimRemainingTime;

                step = StepEnum.NormShoot_Shooting02A;
                goto StartSteps;
            }

            //animLength = soldAnimObj.animation[selectedAnimShootAllBody.name].length;
            //passedAnimTime = soldAnimObj.animation[selectedAnimShootAllBody.name].time;

            //if (animLength - passedAnimTime <= animToAnimFightLookAllBodyCFTime)
            //{
            //    animToAnimFightLookAllBodyCFTime = animLength - passedAnimTime;
            //    step = StepEnum.NormShoot_Shooting02A;
            //    goto StartSteps;
            //}
        }
        #endregion

        #region NormShoot_IdleReload01
        if (step == StepEnum.NormShoot_IdleReload01)
        {
            SetSituation(SituationEnum.normalFighting);

            if (voiceSituation == AIVoiceSituation.Agressive)
                TryPlayVoice_ImReloading();

            isReloadingInFight = true;

            soldGun.Reload();
            soldGun.PlayReloadSound();

            selectedAnimIdleInFightReload = curSelectedFightInPointInfo.animIdleReload.GetRandomAnimName();
            animToAnimShootToIdleReloadAllBodyCFTime = defaultShootToIdleReloadAllBodyCFTime;

            soldInfo.StartNewMainAnimWithCrossfadeTime(selectedAnimIdleInFightReload, animToAnimShootToIdleReloadAllBodyCFTime);

            animToAnimFightLookAllBodyCFTime = defaultIdleReloadToShootAllBodyCFTime;

            step = StepEnum.NormShoot_IdleReload02;
            goto EndSteps;
        }
        #endregion

        #region NormShoot_IdleReload02
        if (step == StepEnum.NormShoot_IdleReload02)
        {
            SetSituation(SituationEnum.normalFighting);

            PlayDamageSoundIfRecieved();

            if (soldInfo.CheckMainAnimIsFinished(animToAnimFightLookAllBodyCFTime))
            {
                isReloadingInFight = false;

                animToAnimFightLookAllBodyCFTime = soldInfo.mainAnimRemainingTime;
                step = StepEnum.NormShoot_Shooting02A;
                goto StartSteps;
            }
        }
        #endregion

        //

        //<Lean>

        #region LeanLeft01
        if (step == StepEnum.LeanLeft01)
        {
            SetSituation(SituationEnum.leanFighting);

            step = StepEnum.LeanLeft_CoveringToLeanIdle01;
        }
        #endregion

        #region LeanLeft_CoveringToLeanIdle01
        if (step == StepEnum.LeanLeft_CoveringToLeanIdle01)
        {
            SetSituation(SituationEnum.leanFighting);

            if (curSelectedFightInPointInfo.animCoveringToLeanLeft != null)
            {
                string[] covToLeanAnims = curSelectedFightInPointInfo.animCoveringToLeanLeft.GetAnimNames();

                if (covToLeanAnims != null && covToLeanAnims.Length > 0)
                {
                    selectedAnimCoveringToLean = covToLeanAnims[Random.Range(0, covToLeanAnims.Length)];
                    animToAnim_CoveringToLean_CFTime = default_Covering_To_CoveringToLean_CFTime;

                    step = StepEnum.LeanLeft_CoveringToLean_Anim_01;
                }
            }
            else
            {
                string[] leanIdleAnims = curSelectedFightInPointInfo.animLeanLeftIdle.GetAnimNames();

                selectedAnimLeanIdle = leanIdleAnims[Random.Range(0, leanIdleAnims.Length)];
                animToAnim_LeanIdle_CFTime = default_Covering_To_LeanIdle_ForNoAnim_CFTime;

                step = StepEnum.LeanLeft_CoveringToLean_NoAnim_01;
            }
        }
        #endregion

        #region LeanLeft_CoveringToLean_Anim_01
        if (step == StepEnum.LeanLeft_CoveringToLean_Anim_01)
        {
            SetSituation(SituationEnum.leanFighting);

            soldInfo.StartNewMainAnimWithCrossfadeTime(selectedAnimCoveringToLean, animToAnim_CoveringToLean_CFTime);

            step = StepEnum.LeanLeft_CoveringToLean_Anim_02;
            goto EndSteps;
        }
        #endregion

        #region LeanLeft_CoveringToLean_Anim_02
        if (step == StepEnum.LeanLeft_CoveringToLean_Anim_02)
        {
            SetSituation(SituationEnum.leanFighting);

            PlayDamageSoundIfRecieved();

            if (soldInfo.CheckMainAnimIsFinished(default_CoveringToLean_To_LeanIdle_CFTime))
            {
                animToAnim_LeanIdle_CFTime = soldInfo.mainAnimRemainingTime;

                step = StepEnum.LeanLeft_Idle_02;
                goto StartSteps;
            }
        }
        #endregion

        #region LeanLeft_CoveringToLean_NoAnim_01
        if (step == StepEnum.LeanLeft_CoveringToLean_NoAnim_01)
        {
            SetSituation(SituationEnum.leanFighting);

            soldInfo.StartNewMainAnimWithCrossfadeTime(selectedAnimLeanIdle, animToAnim_LeanIdle_CFTime);

            animLeanTimeCounter = animToAnim_LeanIdle_CFTime - default_CoveringToLean_To_LeanIdle_CFTime;

            step = StepEnum.LeanLeft_CoveringToLean_NoAnim_02;
            goto EndSteps;
        }
        #endregion

        #region LeanLeft_CoveringToLean_NoAnim_02
        if (step == StepEnum.LeanLeft_CoveringToLean_NoAnim_02)
        {
            SetSituation(SituationEnum.leanFighting);

            PlayDamageSoundIfRecieved();

            animLeanTimeCounter -= Time.deltaTime;

            if (animLeanTimeCounter <= 0)
            {
                step = StepEnum.LeanLeft_Idle_01;
                goto StartSteps;
            }
        }
        #endregion

        #region LeanLeft_Idle_01
        if (step == StepEnum.LeanLeft_Idle_01)
        {
            SetSituation(SituationEnum.leanFighting);

            animToAnim_LeanIdle_CFTime = default_LeanIdle_CFTime;

            step = StepEnum.LeanLeft_Idle_02;
        }
        #endregion

        #region LeanLeft_Idle_02
        if (step == StepEnum.LeanLeft_Idle_02)
        {
            SetSituation(SituationEnum.leanFighting);

            string[] idleAnims = curSelectedFightInPointInfo.animLeanLeftIdle.GetAnimNames();

            selectedAnimLeanIdle = idleAnims[Random.Range(0, idleAnims.Length)];

            step = StepEnum.LeanLeft_Idle_03_1;
        }
        #endregion

        #region LeanLeft_Idle_03_1
        if (step == StepEnum.LeanLeft_Idle_03_1)
        {
            SetSituation(SituationEnum.leanFighting);

            soldInfo.StartNewMainAnimWithCrossfadeTime(selectedAnimLeanIdle, animToAnim_LeanIdle_CFTime);

            step = StepEnum.LeanLeft_Idle_03_2;
        }
        #endregion

        #region LeanLeft_Idle_03_2
        if (step == StepEnum.LeanLeft_Idle_03_2)
        {
            SetSituation(SituationEnum.leanFighting);

            PlayDamageSoundIfRecieved();

            if (soldInfo.IsFullyInNewMainAnim())
            {
                step = StepEnum.LeanLeft_Idle_04;
                goto StartSteps;
            }

            goto EndSteps;
        }
        #endregion

        #region LeanLeft_Idle_02_A
        if (step == StepEnum.LeanLeft_Idle_02_A)
        {
            SetSituation(SituationEnum.leanFighting);

            string[] idleAnims = curSelectedFightInPointInfo.animLeanLeftIdle.GetAnimNames();

            selectedAnimLeanIdle = idleAnims[Random.Range(0, idleAnims.Length)];

            step = StepEnum.LeanLeft_Idle_03_1_A;
        }
        #endregion

        #region LeanLeft_Idle_03_1_A
        if (step == StepEnum.LeanLeft_Idle_03_1_A)
        {
            SetSituation(SituationEnum.leanFighting);

            soldInfo.StartNewMainAnimWithCrossfadeTime(selectedAnimLeanIdle, animToAnim_LeanIdle_CFTime);

            step = StepEnum.LeanLeft_Idle_03_2_A;
        }
        #endregion

        #region LeanLeft_Idle_03_2_A
        if (step == StepEnum.LeanLeft_Idle_03_2_A)
        {
            SetSituation(SituationEnum.leanFighting);

            PlayDamageSoundIfRecieved();

            if (soldInfo.IsFullyInNewMainAnim())
            {
                step = StepEnum.LeanLeft_Idle_05;
                goto StartSteps;
            }

            goto EndSteps;
        }
        #endregion

        #region LeanLeft_Idle_04
        if (step == StepEnum.LeanLeft_Idle_04)
        {
            SetSituation(SituationEnum.leanFighting);

            ResetTargetRecheckTime();

            ResetCheckGetBackToPosTimeCounter();

            ResetShootingTime();

            step = StepEnum.LeanLeft_Idle_05;
        }
        #endregion

        #region LeanLeft_Idle_05
        if (step == StepEnum.LeanLeft_Idle_05)
        {
            SetSituation(SituationEnum.leanFighting);

            step = StepEnum.LeanLeft_Idle_06_1;
        }
        #endregion

        #region LeanLeft_Idle_06 (1 & 2)
        if (step == StepEnum.LeanLeft_Idle_06_1)
        {
            SetSituation(SituationEnum.leanFighting);

            if (voiceSituation == AIVoiceSituation.Agressive)
                TryPlayVoice_Agressive();

            isTargetChecked = false;

            if (CheckFirstDamage())
            {
                dmg = soldInfo.firstDamage;
                step = StepEnum.LeanLeft_Damage_01;
                goto StartSteps;
            }

            if (needsToBeFinished)
            {
                SetFinished(true);
                return;
            }

            if (NeedsReload())
            {
                step = StepEnum.LeanLeft_LeanIdleToCovering_01;
                goto StartSteps;
            }

            if (IsPlayerInCriticalSituation())
            {
                step = StepEnum.LeanLeft_LeanIdleToCovering_01;
                goto StartSteps;
            }

            if (IsShootingTimeFinished())
            {
                step = StepEnum.LeanLeft_LeanIdleToCovering_01;
                goto StartSteps;
            }

            if (!lastIsEnemyAroundResult)
            {
                step = StepEnum.LeanLeft_LeanIdleToCovering_01;
                goto StartSteps;
            }

            if (lastIsNotInPosResult)
            {
                //<Temp>
                //??????????
                //</Temp>
            }

            if (!IsTargetTotallyFightable(selectedTarget)
                ||
                IsTargetLockTimeFinished())
            {
                UpdateEnemiesForLeanLeft();

                if (sortedEnemies.Count == 0)
                {
                    step = StepEnum.LeanLeft_LeanIdleToCovering_01;
                    goto StartSteps;
                }
                else
                {
                    SetNewTarget(sortedEnemies[0]);
                    isTargetChecked = true;
                }
            }

            //if (lastIsGunDirNotTrueResult)
            //{
            //    step = StepEnum.NormShoot_GunDirToGunDir01;
            //    goto StartSteps;
            //}

            step = StepEnum.LeanLeft_Idle_06_2;
        }

        if (step == StepEnum.LeanLeft_Idle_06_2)
        {
            if (!isTargetChecked && targetRecheckTimeReached)
            {
                if (IsSelectedTargetOKForShootingNowForLeanLeft())
                {
                    isTargetChecked = true;
                }
                else
                {
                    UpdateEnemiesForLeanLeft();

                    if (sortedEnemies.Count == 0)
                    {
                        step = StepEnum.LeanLeft_LeanIdleToCovering_01;
                        goto StartSteps;
                    }
                    else
                    {
                        SetNewTarget(sortedEnemies[0]);
                        isTargetChecked = true;
                    }
                }
            }

            if (soldGun.IsReady())
            {
                if (!isTargetChecked)
                {
                    isTargetChecked = IsSelectedTargetOKForShootingNowForLeanLeft();
                }

                if (isTargetChecked)
                {
                    step = StepEnum.LeanLeft_Shoot_01;
                    goto StartSteps;
                }
            }

            if (soldInfo.CheckMainAnimIsFinished(default_LeanIdle_CFTime))
            {
                animToAnim_LeanIdle_CFTime = soldInfo.mainAnimRemainingTime;
                step = StepEnum.LeanLeft_Idle_02_A;
                goto StartSteps;
            }

            step = StepEnum.LeanLeft_Idle_06_1;
        }
        #endregion

        #region LeanLeft_Damage_01
        if (step == StepEnum.LeanLeft_Damage_01)
        {
            SetSituation(SituationEnum.leanFighting);

            PlayDamageSound(dmg);

            SoldierDamageAnimPack dmgPack = curSelectedFightInPointInfo.animDamageLeanLeft;
            selectedAnimLeanDamage = dmgPack.GetRandomAnim(dmg);

            animToAnim_LeanDamage_CFTime = default_LeanIdle_To_LeanDamage_CFTime;

            soldInfo.StartNewMainAnimWithCrossfadeTime(selectedAnimLeanDamage, animToAnim_LeanDamage_CFTime);

            animToAnim_LeanIdle_CFTime = default_LeanDamage_To_LeanIdle_CFTime;

            step = StepEnum.LeanLeft_Damage_02;
            goto EndSteps;
        }
        #endregion

        #region LeanLeft_Damage_02
        if (step == StepEnum.LeanLeft_Damage_02)
        {
            SetSituation(SituationEnum.leanFighting);

            if (soldInfo.CheckMainAnimIsFinished(animToAnim_LeanIdle_CFTime))
            {
                animToAnim_LeanIdle_CFTime = soldInfo.mainAnimRemainingTime;
                step = StepEnum.LeanLeft_Idle_02_A;
                goto StartSteps;
            }
        }
        #endregion

        #region LeanLeft_LeanIdleToCovering_01
        if (step == StepEnum.LeanLeft_LeanIdleToCovering_01)
        {
            SetSituation(SituationEnum.covering);

            nowInCriticalFightWithPlayer = false;

            if (curSelectedFightInPointInfo.animLeanLeftToCover != null)
            {
                string[] leanToCovAnims = curSelectedFightInPointInfo.animLeanLeftToCover.GetAnimNames();

                if (leanToCovAnims != null && leanToCovAnims.Length > 0)
                {
                    selectedAnimLeanToCovering = leanToCovAnims[Random.Range(0, leanToCovAnims.Length)];
                    animToAnim_LeanToCovering_CFTime = default_Covering_To_CoveringToLean_CFTime;

                    step = StepEnum.LeanLeft_LeanToCovering_Anim_01;
                    goto StartSteps;
                }
            }
            else
            {
                string[] coveringAnims = curSelectedFightInPointInfo.animCoveringInFight.GetAnimNames();

                selectedAnimCoveringInFight = coveringAnims[Random.Range(0, coveringAnims.Length)];
                animToAnimCoveringAllBodyCFTime = default_Covering_To_LeanIdle_ForNoAnim_CFTime;

                step = StepEnum.LeanLeft_LeanToCovering_NoAnim_01;
                goto StartSteps;
            }
        }
        #endregion

        #region LeanLeft_LeanToCovering_Anim_01
        if (step == StepEnum.LeanLeft_LeanToCovering_Anim_01)
        {
            SetSituation(SituationEnum.covering);

            soldInfo.StartNewMainAnimWithCrossfadeTime(selectedAnimLeanToCovering, animToAnim_LeanToCovering_CFTime);

            step = StepEnum.LeanLeft_LeanToCovering_Anim_02;
            goto EndSteps;
        }
        #endregion

        #region LeanLeft_LeanToCovering_Anim_02
        if (step == StepEnum.LeanLeft_LeanToCovering_Anim_02)
        {
            SetSituation(SituationEnum.covering);

            PlayDamageSoundIfRecieved();

            if (soldInfo.CheckMainAnimIsFinished(default_CoveringToLean_To_LeanIdle_CFTime))
            {
                animToAnimCoveringAllBodyCFTime = soldInfo.mainAnimRemainingTime;

                step = StepEnum.Covering01;
                goto StartSteps;
            }
        }
        #endregion

        #region LeanLeft_LeanToCovering_NoAnim_01
        if (step == StepEnum.LeanLeft_LeanToCovering_NoAnim_01)
        {
            SetSituation(SituationEnum.covering);

            soldInfo.StartNewMainAnimWithCrossfadeTime(selectedAnimCoveringInFight, animToAnimCoveringAllBodyCFTime);

            step = StepEnum.LeanLeft_LeanToCovering_NoAnim_02;
            goto EndSteps;
        }
        #endregion

        #region LeanLeft_LeanToCovering_NoAnim_02
        if (step == StepEnum.LeanLeft_LeanToCovering_NoAnim_02)
        {
            SetSituation(SituationEnum.covering);

            PlayDamageSoundIfRecieved();

            if (soldInfo.IsFullyInNewMainAnim())
            {
                //<Bad>
                step = StepEnum.Covering01;
                //</Bad>
                goto StartSteps;
            }
        }
        #endregion

        #region LeanLeft_Shoot_01
        if (step == StepEnum.LeanLeft_Shoot_01)
        {
            SetSituation(SituationEnum.leanFighting);

            Transform shootTarg;

            List<Transform> okPoses;

            if (selectedTarget.isCharacterHitted)
            {
                okPoses = selectedTarget.characterHittedPoses;
            }
            else
            {
                okPoses = selectedTarget.haloHittedPoses;
            }

            shootTarg = okPoses[Random.Range(0, okPoses.Count)];

            soldGun.TryFire(shootTarg.position);

            string[] shootAnims = curSelectedFightInPointInfo.animLeanLeftShoot.GetAnimNames();
            selectedAnimLeanShoot = shootAnims[Random.Range(0, shootAnims.Length)];
            animToAnim_LeanShoot_CFTime = default_LeanIdle_To_LeanShoot_CFTime;

            animToAnim_LeanIdle_CFTime = default_LeanShoot_To_LeanIdle_CFTime;

            soldInfo.StartNewMainAnimWithCrossfadeTime(selectedAnimLeanShoot, animToAnim_LeanShoot_CFTime);

            step = StepEnum.LeanLeft_Shoot_02;
            goto EndSteps;
        }
        #endregion

        #region LeanLeft_Shoot_02
        if (step == StepEnum.LeanLeft_Shoot_02)
        {
            SetSituation(SituationEnum.leanFighting);

            if (CheckShootingDamage())
            {
                dmg = soldInfo.firstDamage;
                step = StepEnum.LeanLeft_Damage_01;
                goto StartSteps;
            }

            if (soldInfo.CheckMainAnimIsFinished(animToAnim_LeanIdle_CFTime))
            {
                animToAnim_LeanIdle_CFTime = soldInfo.mainAnimRemainingTime;

                step = StepEnum.LeanLeft_Idle_02_A;
                goto StartSteps;
            }
        }
        #endregion

    //</Lean>

        #region DoOnlyAnim_01
        if (step == StepEnum.DoOnlyAnim_01)
        {
            SetSituation(SituationEnum.covering);

            soldInfo.StartNewMainAnimWithCrossfadeTime(animsListToOnlyDoOnFightPoint.GetRandomAnimName(), onlyAnimCFTime);

            step = StepEnum.DoOnlyAnim_02;
        } 
        #endregion

        #region DoOnlyAnim_02
        if (step == StepEnum.DoOnlyAnim_02)
        {
            SetSituation(SituationEnum.covering);

            if (needsToBeFinished)
            {
                SetFinished(true);
                return;
            }
        } 
        #endregion

    EndSteps:
        return;
    }

    public override bool ShouldTakeDamage(DamageInfo dmg)
    {
        if (base.ShouldTakeDamage(dmg))
        {
            return true;
        }

        return false;
    }

    public override void SetFinished(bool OK)
    {
        base.SetFinished(OK);

        if (selectedTarget != null && selectedTarget.character != null)
            selectedTarget.character.GetComponent<CharacterInfo>().RemoveTargettingEnemy(controlledSoldier.gameObject);

        nowInCriticalFightWithPlayer = false;

        //Finishes in covering or idle situation
    }

    //

    void SetFightPosCoveringRotation()
    {
        fightPosCoveringRotation = fightRot;
        fightPosCoveringRotation.SetEulerAngles(0, (fightPosCoveringRotation.eulerAngles.y + coveringAngleYToFightPosRotation) * Mathf.Deg2Rad, 0);
    }

    bool IsEnemyAround()
    {
        return soldInfo.IsEnemyAround();
    }

    bool CheckSlowlyIsEnemyAround()
    {
        checkingIsEnemyAroundTimeCounter -= Time.deltaTime;
        if (checkingIsEnemyAroundTimeCounter <= 0)
        {
            checkingIsEnemyAroundTimeCounter = checkingIsEnemyAroundMaxTime;
            lastIsEnemyAroundResult = soldInfo.IsEnemyAround();
        }

        return lastIsEnemyAroundResult;
    }

    bool RotateAllBodyToCovering()
    {
        return soldInfo.RotateSoldierAllBodyToRotation(fightPosCoveringRotation, actToCoverRotationSpeed, actToCoverRotationAngleTolerance);
    }

    bool RotateAllBodyToCurrentTarget()
    {
        Quaternion targRot = Quaternion.LookRotation(selectedTarget.character.transform.position - controlledSoldier.position);

        return soldInfo.RotateSoldierAllBodyToRotation(targRot, fightRotationSpeed, fightRotationAngleTolerance);
    }

    void SetAllBodyRotationInNormalShoot()
    {
        if (mapLogic.IsCharacterTotallyFightable(selectedTarget.character))
        {
            if (gentlyRotateAllBodyToTarget)
                if (RotateAllBodyToCurrentTarget())
                    SetLockAllBodyRotationOnTarget(true);

            if (lockAllBodyRotationOnTarget)
            {
                Vector3 distToTarg = selectedTarget.character.transform.position - controlledSoldier.transform.position;
                distToTarg.y = 0;

                controlledSoldier.transform.rotation = Quaternion.LookRotation(distToTarg);
            }
        }
    }

    bool ShouldCheckPlayerCritSitu()
    {
        if (mapLogic.player == null)
            return false;

        if (!mapLogic.playerCharInfo.IsAttackable())
            return false;

        if (mapLogic.playerCharInfo.GetEnemyFightSide() != soldCharInfo.FightSide)
            return false;

        return shouldConsiderPlayerCriticalSituation;
    }

    //bool CheckSlowlyIsPlayerInCriticalSituation()
    //{
    //    if (!ShouldCheckPlayerCritSitu())
    //        return false;

    //    checkingPlayerIsInCriticTimeCounter -= Time.deltaTime;
    //    if (checkingPlayerIsInCriticTimeCounter <= 0)
    //    {
    //        checkingPlayerIsInCriticTimeCounter = Random.Range(checkingPlayerIsInCriticMinTime, checkingPlayerIsInCriticMaxTime);
    //        lastIsPlayerInCriticResult = IsPlayerInCriticalSituation();
    //    }

    //    return lastIsPlayerInCriticResult;
    //}

    //<Lean>

    bool IsPlayerInCriticalSituation()
    {
        if (!ShouldCheckPlayerCritSitu())
            return false;

        if (initialEnemies != null && initialEnemies.Count > 0 && !initialEnemies.Contains(mapLogic.player))
            return false;

        return soldInfo.IsPlayerStillInCriticalState();
    }

    //</Lean>

    bool NeedsToGetBackToPosition()
    {
        if (!keepSoldierInPosition)
            return false;

        //<Temp>
        return false;
        //</Temp>
    }

    bool CheckSlowlyNeedsToGetBackToThePoint()
    {
        checkGettingBackToPointTimeCounter -= Time.deltaTime;
        if (checkGettingBackToPointTimeCounter <= 0)
        {
            checkGettingBackToPointTimeCounter = checkGettingBackToPointMaxTime;
            lastIsNotInPosResult = NeedsToGetBackToPosition();
        }

        return lastIsNotInPosResult;
    }

    void ResetCheckGetBackToPosTimeCounter()
    {
        checkGettingBackToPointTimeCounter = checkGettingBackToPointMaxTime;
    }

    bool NeedsReload()
    {
        return soldGun.NeedsReload();
    }

    void SetSituation(SituationEnum _situ)
    {
        situation = _situ;
    }

    Vector3 GetRaycastPos()
    {
        return SoldierStats.GetShootingRaycastPos(controlledSoldier.transform.position, fightRot, gunRaycastOffset);
    }

    void UpdateEnemies()
    {
        sortedEnemies = GetAttackableEnemiesForFightInfo(curSelectedFightInPointInfo);

        if (nowInCriticalFightWithPlayer)
        {
            if (mapLogic.playerCharInfo != null && mapLogic.playerCharInfo.IsAttackable())
            {
                int plyrIndex = -1;

                for (int i = 0; i < sortedEnemies.Count; i++)
                {
                    if (sortedEnemies[i].character == mapLogic.player)
                    {
                        plyrIndex = i;
                        break;
                    }
                }

                if (plyrIndex > 0)
                {
                    CharRaycastResult plyrRes = new CharRaycastResult();
                    plyrRes = sortedEnemies[plyrIndex];

                    sortedEnemies.RemoveAt(plyrIndex);
                    sortedEnemies.Insert(0, plyrRes);
                }
            }
        }
    }

    bool IsSelectedTargetOKForShootingNow()
    {
        ResetTargetRecheckTime();

        return mapLogic.IsCharacterOkAsTarget(controlledSoldier.gameObject, selectedTarget.character, GetRaycastPos(),
                                                fightRot, range, shootingStartAngle, shootingEndAngle, out selectedTarget);
    }

    float GetCoveringInfightTargetRecheckTimeForCurrentFightInfo()
    {
        return Random.Range(coveringInfightTargetRecheckMinTime, coveringInfightTargetRecheckMaxTime);
    }

    float GetCoveringInfightTargetRecheckTimeForNewFightInfo()
    {
        return Random.Range(newCoveringInfightMinTime, newCoveringInfightMaxTime);
    }

    float GetIdleTargetCheckTime()
    {
        return Random.Range(idleTargetCheckMinTime, idleTargetCheckMaxTime);
    }

    List<CharRaycastResult> GetAttackableEnemiesForFightInfo(SoldierFightInPointInfo _fightInfo)
    {
        Vector3 rayCastPos = GetRaycastPos();
        Quaternion raycastPosRot = fightRot;

        List<CharRaycastResult> result;

        if (initialEnemies != null && initialEnemies.Count > 0)
        {
            result = mapLogic.GetAttackableCharsFromList(initialEnemies, controlledSoldier.gameObject, rayCastPos, raycastPosRot, range, _fightInfo.shootingStartAngle, _fightInfo.shootingEndAngle);
        }
        else
        {
            result = mapLogic.GetAttackableEnemies(controlledSoldier.gameObject, rayCastPos, raycastPosRot, range, _fightInfo.shootingStartAngle, _fightInfo.shootingEndAngle);
        }

        result = mapLogic.RateEnemiesAndSort(result, controlledSoldier.gameObject, rayCastPos);

        return result;
    }

    SoldierFightInPointInfo SelectNewFightInPointInfo(bool _blindRandom)
    {
        SoldierFightInPointInfo curFInfo = curSelectedFightInPointInfo;

        int infCount = availableFightInPointInfos.Length;

        if (infCount == 1)
            return availableFightInPointInfos[0];

        List<SoldierFightInPointInfo> fInfos = new List<SoldierFightInPointInfo>();

        for (int i = 0; i < infCount; i++)
        {
            if (availableFightInPointInfos[i] != curFInfo)
                fInfos.Add(availableFightInPointInfos[i]);
        }

        if (_blindRandom)
            return fInfos[Random.Range(0, fInfos.Count)];

        int maxI = fInfos.Count;

        for (int i = 0; i < maxI; i++)
        {
            int index = Random.Range(0, fInfos.Count);
            SoldierFightInPointInfo fi = fInfos[index];

            List<CharRaycastResult> enes = GetAttackableEnemiesForFightInfo(fi);

            if (enes.Count > 0)
                return fi;
            else
                fInfos.RemoveAt(index);
        }

        return curFInfo;
    }

    void SetGentlyRotateAllBodyToTarget(bool _value)
    {
        if (_value)
            lockAllBodyRotationOnTarget = false;
        gentlyRotateAllBodyToTarget = _value;
    }

    void SetLockAllBodyRotationOnTarget(bool _value)
    {
        if (_value)
            gentlyRotateAllBodyToTarget = false;
        lockAllBodyRotationOnTarget = _value;
    }

    void SetNewTarget(CharRaycastResult _target)
    {
        if (selectedTarget != null && selectedTarget.character != null)
            selectedTarget.character.GetComponent<CharacterInfo>().RemoveTargettingEnemy(controlledSoldier.gameObject);

        selectedTarget = _target;

        if (selectedTarget != null && selectedTarget.character != null)
            selectedTarget.character.GetComponent<CharacterInfo>().AddTargettingEnemy(controlledSoldier.gameObject);

        SetLockAllBodyRotationOnTarget(false);

        SetGentlyRotateAllBodyToTarget(true);

        ResetTargetLockTime();
        ResetTargetRecheckTime();
    }

    void ResetTargetLockTime()
    {
        SetTargetLockTime(targetLockTimeMin, targetLockTimeMax);
    }

    void SetTargetLockTime(float _min, float _max)
    {
        targetLockTimeCounter = Random.Range(_min, _max);
    }

    void ResetTargetRecheckTime()
    {
        SetTargetRecheckTime(targetRecheckTimeMin, targetRecheckTimeMax);
    }

    void SetTargetRecheckTime(float _min, float _max)
    {
        targetRecheckTimeCounter = Random.Range(_min, _max);
    }

    void ResetShootingTime()
    {
        SetShootingTime(shootingMinTime, shootingMaxTime);
    }

    void SetShootingTime(float _min, float _max)
    {
        shootingTimeCounter = Random.Range(_min, _max);
    }

    void DoCommonPreShootingThings()
    {
        shootingTimeCounter -= Time.deltaTime;

        targetLockTimeCounter -= Time.deltaTime;

        targetRecheckTimeReached = false;
        targetRecheckTimeCounter -= Time.deltaTime;
        if (targetRecheckTimeCounter <= 0)
        {
            ResetTargetRecheckTime();
            targetRecheckTimeReached = true;
        }

        //CheckSlowlyIsPlayerInCriticalSituation();

        CheckSlowlyIsEnemyAround();

        CheckSlowlyNeedsToGetBackToThePoint();

        CheckSlowlyIsGunDirectionWrong(selectedTarget.character);
    }

    bool IsShootingTimeFinished()
    {
        return shootingTimeCounter <= 0;
    }

    bool IsTargetTotallyFightable(CharRaycastResult _char)
    {
        return mapLogic.IsCharacterTotallyFightable(_char.character);
    }

    bool IsTargetLockTimeFinished()
    {
        return targetLockTimeCounter <= 0;
    }

    bool CheckSlowlyIsGunDirectionWrong(GameObject _target)
    {
        if (_target == null)
            return false;

        checkGunDirectionTimeCounter -= Time.deltaTime;

        if (checkGunDirectionTimeCounter <= 0)
        {
            checkGunDirectionTimeCounter = checkGunDirectionMaxTime;
            lastIsGunDirNotTrueResult = (currentGunDirection != SoldierStats.GetSoldierGunDirectionForTarget(controlledSoldier.gameObject, _target));
        }

        return lastIsGunDirNotTrueResult;
    }

    bool IsGunReady()
    {
        return soldGun.IsReady();
    }

    bool CheckShootingDamage()
    {
        if (CheckFirstDamage())
        {
            float a = Random.Range(0f, 1f);

            return (a < shootingDamageChance);
        }

        return false;
    }

    void StartActInit()
    {
        lastIsEnemyAroundResult = IsEnemyAround();
        lastIsGunDirNotTrueResult = true;

        idleTargetCheckTimeCounter = 0;
    }

    void PlayDamageSound(DamageInfo _dmg)
    {
        if (soldInfo.shouldPlayDamageSound)
        {

            if (!soldInfo.IsVoiceOnBusyTimer())
            {
                AudioClip clip = soldInfo.voiceInfo.GetAudioClip_Damage(_dmg.damageType);
                soldInfo.PlayVoiceWithAdditionalBusyTime(clip, 0.1f, 0.1f);
            }

        }
    }

    void PlayDamageSoundIfRecieved()
    {
        if (CheckFirstDamage())
        {
            dmg = soldInfo.firstDamage;
            PlayDamageSound(dmg);
        }
    }

    public bool IsReloadingInFight()
    {
        return isReloadingInFight;
    }


    bool CheckAndStartPlayerCriticalState(FightCriticWithPlayerPos _Pos)
    {
        if (ShouldCheckPlayerCritSitu())
        {
            if (!nowInCriticalFightWithPlayer)
            {
                if (soldInfo.IsPlayerStillInCriticalState())
                {
                    if (!(initialEnemies != null && initialEnemies.Count > 0 && initialEnemies.Contains(mapLogic.player)))
                    {
                        return StartPlayerCriticalState(_Pos);
                    }
                }
            }
        }
        return false;
    }

    bool StartPlayerCriticalState(FightCriticWithPlayerPos _Pos)
    {
        nowInCriticalFightWithPlayer = true;

        switch (_Pos)
        {
            case FightCriticWithPlayerPos.Covering:
                if (curSelectedFightInPointInfo != curSelectedFightInPointInfo.playerCriticalFightInfoInCover)
                {
                    InitNewFightInPointInfo(curSelectedFightInPointInfo.playerCriticalFightInfoInCover);
                    return true;
                }
                break;

            case FightCriticWithPlayerPos.NormShoot_Shooting:
                return false;
        }

        return false;
    }

    bool CheckAndEndPlayerCriticalState()
    {
        if (nowInCriticalFightWithPlayer)
        {
            if (!soldInfo.IsPlayerStillInCriticalState())
            {
                return EndPlayerCriticalState();
            }
        }

        return false;
    }

    bool EndPlayerCriticalState()
    {
        nowInCriticalFightWithPlayer = false;

        if (curSelectedFightInPointInfo != defaultFightInPointInfo)
        {
            InitNewFightInPointInfo(defaultFightInPointInfo);
            return true;
        }

        return false;
    }

    // ///// 

    void ResetTalkingVoiceDelayCounter()
    {
        time_TalkingVoiceDelay_TimeCounter = Random.Range(time_TalkingVoiceDelay_Min, time_TalkingVoiceDelay_Max);
    }

    bool IsTalkDelayReady()
    {
        return time_TalkingVoiceDelay_TimeCounter == 0;
    }

    bool CanPlayAITalkVoice()
    {
        if (voiceSituation == AIVoiceSituation.Silence)
            return false;

        if (!IsTalkDelayReady())
            return false;

        if (soldInfo.IsVoiceOnBusyTimer())
            return false;

        return true;
    }

    void TryPlayVoice_Agressive()
    {
        if (!CanPlayAITalkVoice())
            return;

        if (soldInfo.TryTalk_Agressive())
            ResetTalkingVoiceDelayCounter();
    }

    void TryPlayVoice_ImReloading()
    {
        if (!CanPlayAITalkVoice())
            return;

        if (Random.Range(0f, 1f) > chanceToTalkImReloadig)
            return;

        if (soldInfo.TryTalk_Reloading())
            ResetTalkingVoiceDelayCounter();
    }

    //<Lean>

    bool ShouldStartLeanLeft()
    {
        if (!canLeanLeft)
            return false;

        if (IsPlayerInCriticalSituation())
            return false;

        if (Random.Range(0f, 1f) > leanChance)
            return false;

        if (GetAttackableEnemiesForFightInfo_LeanLeft(curSelectedFightInPointInfo).Count > 0)
            return true;

        return false;
    }

    void UpdateEnemiesForLeanLeft()
    {
        sortedEnemies = GetAttackableEnemiesForFightInfo_LeanLeft(curSelectedFightInPointInfo);
    }

    List<CharRaycastResult> GetAttackableEnemiesForFightInfo_LeanLeft(SoldierFightInPointInfo _fightInfo)
    {
        Vector3 rayCastPos = GetRaycastPosForLeanLeft(_fightInfo);
        Quaternion raycastPosRot = fightRot;

        List<CharRaycastResult> result;

        if (initialEnemies != null && initialEnemies.Count > 0)
        {
            result = mapLogic.GetAttackableCharsFromList(initialEnemies, controlledSoldier.gameObject, rayCastPos, raycastPosRot, range, -_fightInfo.leanHalfAngle, _fightInfo.leanHalfAngle);
        }
        else
        {
            result = mapLogic.GetAttackableEnemies(controlledSoldier.gameObject, rayCastPos, raycastPosRot, range, -_fightInfo.leanHalfAngle, _fightInfo.leanHalfAngle);
        }

        result = mapLogic.RateEnemiesAndSort(result, controlledSoldier.gameObject, rayCastPos);

        return result;
    }

    Vector3 GetRaycastPosForLeanLeft(SoldierFightInPointInfo _fightInfo)
    {
        return SoldierStats.GetShootingRaycastPos(controlledSoldier.transform.position, fightRot, _fightInfo.GetLeanLeftRaycastOffsetForGun(soldGun.name));
    }

    bool IsSelectedTargetOKForShootingNowForLeanLeft()
    {
        ResetTargetRecheckTime();



        return mapLogic.IsCharacterOkAsTarget(controlledSoldier.gameObject, selectedTarget.character, GetRaycastPosForLeanLeft(curSelectedFightInPointInfo),
                                                fightRot, range, -curSelectedFightInPointInfo.leanHalfAngle, curSelectedFightInPointInfo.leanHalfAngle, out selectedTarget);
    }

    //</Lean>

    public void SetOwnerFightInRegAct(SoldierAction_FightInReg _ownerFightInRegAct)
    {
        ownerFightInRegAct = _ownerFightInRegAct;
    }

    public void SetItsNowReadyForLaunchGrenade()
    {
        nowReadyForGreandeLaunch = true;
    }

    bool IsReadyToLaunchGrenade()
    {
        if (!nowReadyForGreandeLaunch)
            return false;

        if (!curSelectedFightInPointInfo.grenadeEnabled)
            return false;

        if (!(curSelectedFightInPointInfo.fightType == FightInPointTypeEnum.NormalCover))
            return false;

        if (fightPoint == null)
            return false;

        if (!fightPoint.IsGenerallyOkForGrenade())
            return false;

        return true;
    }

    void SetGrenadeIsLaunchedNow()
    {
        if (!nowReadyForGreandeLaunch)
            return;

        nowReadyForGreandeLaunch = false;

        if (ownerFightInRegAct != null)
        {
            ownerFightInRegAct.SetGrenadeIsLaunchedNow();
        }
    }

    void ThrowGrenade()
    {
        SetGrenadeIsLaunchedNow();

        GrenadeLaunchInfo grLaunchInfo = fightPoint.GetRandomReadyGrenadeLaunchInfo();

        GameObject gObj = Instantiate(soldInfo.soldierGeneralInfo.soldierGrenadeObject) as GameObject;

        gObj.transform.position = soldierRightHandTr.position; //SoldierStats.GetShootingRaycastPos(controlledSoldier.transform.position, fightRot, curSelectedFightInPointInfo.grenadeLaunchOffset);

        Vector3 gDir = grLaunchInfo.GetGrenadeDirection();

        Rigidbody gBody = gObj.transform.GetComponentInChildren<Rigidbody>();

        float gSpeed = grLaunchInfo.GetGrenadeSpeed();

        gBody.AddForceAtPosition(gDir * gSpeed, gObj.transform.position, ForceMode.Impulse);

        float torqueX = Random.Range(-GeneralStats.grenadeMaxTorqueRangeX, GeneralStats.grenadeMaxTorqueRangeX);
        float torqueY = Random.Range(-GeneralStats.grenadeMaxTorqueRangeY, GeneralStats.grenadeMaxTorqueRangeY);
        float torqueZ = Random.Range(-GeneralStats.grenadeMaxTorqueRangeZ, GeneralStats.grenadeMaxTorqueRangeZ);

        Vector3 torque = new Vector3(torqueX, torqueY, torqueZ);
        gBody.AddTorque(torque, ForceMode.Impulse);
    }
}
