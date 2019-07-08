using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float Speed;
    public float MaxDistance;
    public float Damage;
    public float Impulse;
    public float Gravity;
    public float playerHitChance = 1;


    public float CriticalChance;

    public GameObject DecalObject;

    public GameObject MarkObject;

    //public LayerMask hitLayer;

    private Vector3 startPosition;
    private Vector3 shootPoint;
    private Vector3 direction;
    private float flyDistance;
    private Vector3 velocity;

    private bool firstTick = true;
    private bool shouldDelete = false;

    DamageInfo dmgInfo = new DamageInfo();

    private GameObject owner;

    public GameObject Owner
    {
        get { return owner; }
        set { owner = value; }
    }

    private HitEffectManager hitManager;
    public AudioInfo audioInfo;
    public AudioClip[] WhizzSounds;

    private GameGeneralInfo generalInfoHandler;

    private float BulletWhizzRadius;

    public void InitBulletProp(Vector3 movingDirection, GameObject own)
    {
        direction = movingDirection.normalized;
        Owner = own;

        dmgInfo.damageAmount = Damage;
        dmgInfo.damageSource = Owner;
        dmgInfo.damageSourcePosition = Owner.transform.position;
        dmgInfo.damageType = DamageType.Bullet;
        dmgInfo.Impulse = Impulse;
        dmgInfo.ownerTag = Owner.transform.root.tag.ToLower();
    }

    MeshRenderer meshRend;

    void Start()
    {
        meshRend = GetComponent<MeshRenderer>();

        generalInfoHandler = GameGeneralInfo.Instance;
        if (generalInfoHandler == null)
            Debug.LogError("General Info For Bullet Is Null");

        BulletWhizzRadius = (PlayerCharacterNew.Instance.bulletWhizzLot.GetComponent<SphereCollider>()).radius * 2 + 0.1f;

        if (direction == Vector3.zero)
            direction = transform.forward;

        Ray ray = new Ray(transform.position, direction);
        RaycastHit hitInfo;
        float rayMagnitude = MaxDistance;

        bool hitToPlayer = false;

        if (Physics.Raycast(ray, out hitInfo, rayMagnitude, generalInfoHandler.HitLayer))
        {
            Ray newRay = ray;
            RaycastHit newRayHitInfo = hitInfo;

            bool rayHittedOk = true;

            if (GameObject.Equals(hitInfo.collider.transform.root.gameObject, Owner))
            {
                newRay = new Ray(hitInfo.point + direction * 0.0001f, direction);

                if (!Physics.Raycast(newRay, out newRayHitInfo, rayMagnitude, generalInfoHandler.HitLayer))
                {

                    rayHittedOk = false;
                }
                else
                {
                    //Debug.DrawLine(newRay.origin, newRayHitInfo.point, Color.red, 10);
                }
            }

            if (rayHittedOk)
            {
                if (newRayHitInfo.collider.gameObject == PlayerCharacterNew.Instance.bulletWhizzLot)
                {
                    Vector3 startingPoint = newRayHitInfo.point + (newRay.direction * 0.0001f);
                    Ray checkingRay = new Ray(startingPoint, newRay.direction);
                    RaycastHit CheckinHitInfo;

                    if (Physics.Raycast(checkingRay, out CheckinHitInfo, BulletWhizzRadius, generalInfoHandler.HitLayer))
                    {
                        if (CheckinHitInfo.collider.gameObject == PlayerCharacterNew.Instance.gameObject)
                        {
                            hitToPlayer = true;

                            //Debug.DrawLine(checkingRay.origin, CheckinHitInfo.point, Color.blue, 10);
                        }
                    }
                }
                else
                {
                    if (!hitToPlayer)
                    {
                        string rootTag = newRayHitInfo.collider.transform.root.tag.ToLower();

                        if (rootTag == GeneralStats.playerTagName_ToLower)
                        {
                            //Debug.DrawLine(newRay.origin, newRayHitInfo.point, Color.yellow, 10);

                            hitToPlayer = true;
                        }
                    }
                }
            }
        }

        velocity = direction * Speed;

        if (hitToPlayer)
        {
            velocity *= 70;

            if (meshRend != null)
                meshRend.enabled = false;
        }

        startPosition = transform.position;
        shootPoint.x = startPosition.x;
        shootPoint.y = startPosition.y;
        shootPoint.z = startPosition.z;

        hitManager = GetComponent<HitEffectManager>();
    }

    void Update()
    {
        if (Speed != 0)
        {
            if (Gravity != 0)
                velocity.y -= Gravity * Time.deltaTime;

            Vector3 offset = velocity * Time.deltaTime;

            float distance = offset.magnitude;

            if (MaxDistance != 0)
            {
                if (flyDistance + distance >= MaxDistance)
                {
                    distance = MaxDistance - flyDistance;
                    if (distance <= 0)
                        distance = 0.001f;
                    offset = offset.normalized * distance;
                    shouldDelete = true;
                }
            }

            startPosition = transform.position;

            if (!firstTick)
            {
                startPosition -= offset.normalized * .0001f;
            }

            Ray ray = new Ray(startPosition, offset);
            RaycastHit hitInfo;

            float rayMagnitude = offset.magnitude;

            if (Physics.Raycast(ray, out hitInfo, rayMagnitude, generalInfoHandler.HitLayer))
            {
                if (!GameObject.Equals(hitInfo.collider.transform.root.gameObject, Owner))
                {
                    bool shouldBulletContinue = false;

                    if (!IsBulletWhizzLot(hitInfo, ray))
                    {
                        SoldierNearbyHitDetection solNearbyHitDetec = null;

                        if (MapLogic.Instance.isB2_SnipeMode)
                        {
                            solNearbyHitDetec = hitInfo.collider.GetComponent<SoldierNearbyHitDetection>();
                        }

                        if (solNearbyHitDetec != null)
                        {
                            solNearbyHitDetec.SetBulletHitNearby(hitInfo, ray, this);

                            shouldBulletContinue = true;
                        }
                        else
                        {
                            //Should Check For Hit Effects
                            transform.position = hitInfo.point;

                            //Should Check Bullet Should Die
                            OnHit(hitInfo, offset);
                            goto end;
                        }
                    }
                    else
                    {
                        shouldBulletContinue = true;
                    }

                    if (shouldBulletContinue)
                    {
                        //We make a ray from point to whizz lot then we check part 2 of the ray
                        //From whizz lot to the end of part 1 ray
                        Vector3 startingPoint_Part2 = hitInfo.point + 0.001f * offset;
                        Ray ray_part2 = new Ray(startingPoint_Part2, ray.direction);
                        RaycastHit hit_part2;
                        float distance_part2 = ((startPosition + offset) - (startingPoint_Part2)).magnitude;

                        if (Physics.Raycast(ray_part2, out hit_part2, distance_part2, generalInfoHandler.HitLayer))
                        {
                            if (!GameObject.Equals(hit_part2.collider.transform.root.gameObject, Owner))
                            {
                                transform.position = hit_part2.point;

                                //Should Check Bullet Should Die
                                OnHit(hit_part2, offset);
                                goto end;
                            }
                        }
                    }
                }
                else
                {
                    float distanceToOwner = (hitInfo.point - startPosition).magnitude;

                    transform.position = hitInfo.point + 0.001f * offset;
                    flyDistance += distanceToOwner;

                    goto end;
                }
            }


            transform.position = startPosition + offset;
            flyDistance += distance;

            //if (velocity != Vector3.zero)
            //{
            //    //Should Check For (FromDirectionZAxisUp)
            //    //transform.rotation
            //}

            if (shouldDelete)
            {
                GameObject.Destroy(this.gameObject);
                return;
            }

        end: ;
            firstTick = false;
        }
    }

    private bool IsBulletWhizzLot(RaycastHit hitInfo, Ray ray)
    {
        if (hitInfo.collider.gameObject == PlayerCharacterNew.Instance.bulletWhizzLot)
        {
            Vector3 startingPoint = hitInfo.point + (ray.direction * 0.001f);
            Ray checkingRay = new Ray(startingPoint, ray.direction);
            RaycastHit CheckinHitInfo;

            bool hitToPlayer = false;

            if (Physics.Raycast(checkingRay, out CheckinHitInfo, BulletWhizzRadius, generalInfoHandler.HitLayer))
            {
                //Debug.DrawLine(checkingRay.origin,(checkingRay.origin+checkingRay.direction*BulletWhizzRadius), Color.blue,10f);
                //Check if hit to player
                if (CheckinHitInfo.collider.gameObject == PlayerCharacterNew.Instance.gameObject)
                {
                    //Nothing Happened
                    hitToPlayer = true;
                }
            }
            if (!hitToPlayer)
            {
                audioInfo.PlayClip(WhizzSounds);
            }
            return true;
        }
        return false;
    }

    private void OnHit(RaycastHit hitInfo, Vector3 dir)
    {
        bool shouldDamage = false;
        bool shouldDoPhysics = false;
        bool shouldDoEffects = true;

        dmgInfo.BulletDirection = dir;
        dmgInfo.HitPoint = hitInfo.point;

        string rootTag = hitInfo.collider.transform.root.tag.ToLower();

        bool boodeh = false;

        switch (rootTag)
        {
            case GeneralStats.enemyTagName_ToLower:
            case GeneralStats.allyTagName_ToLower:

                boodeh = true;

                switch (hitInfo.collider.gameObject.tag.ToLower())
                {
                    case BodyParts.back:
                        dmgInfo.bodyPart = SoldierBodyPart.UpBack;
                        break;
                    case BodyParts.chest:
                    case BodyParts.leftArm:
                    case BodyParts.leftHand:
                    case BodyParts.rightArm:
                    case BodyParts.rightHand:
                        dmgInfo.bodyPart = SoldierBodyPart.UpFront;
                        break;
                    case BodyParts.head:
                        dmgInfo.bodyPart = SoldierBodyPart.Head;
                        break;
                    case BodyParts.leftFoot:
                    case BodyParts.rightFoot:
                        dmgInfo.bodyPart = SoldierBodyPart.UpFront;
                        break;
                }
                shouldDamage = true;
                break;

            case GeneralStats.playerTagName_ToLower:

                boodeh = true;

                shouldDoEffects = false;
                shouldDamage = false;
                if (IsPlayerHitChanceHappened())
                {
                    shouldDamage = true;
                }
                break;
        }

        if (!boodeh)
        {
            if (hitInfo.collider.transform.parent != null && hitInfo.collider.transform.parent.tag.ToLower() == "dynamic")
            {
                shouldDamage = true;
                shouldDoPhysics = true;
            }
        }

        if ((dmgInfo.ownerTag == GeneralStats.allyTagName_ToLower && (rootTag == GeneralStats.allyTagName_ToLower || rootTag == GeneralStats.playerTagName_ToLower))
            || (dmgInfo.ownerTag == GeneralStats.enemyTagName_ToLower && rootTag == GeneralStats.enemyTagName_ToLower))
        {
            shouldDamage = false;
            shouldDoPhysics = false;
            shouldDoEffects = false;
        }

        if (shouldDamage)
        {
            hitInfo.collider.gameObject.SendMessageUpwards("ApplyDamage", dmgInfo);
        }
        if (shouldDoPhysics)
        {
            DoPhysicalImpact(hitInfo.collider.gameObject, hitInfo.point);
        }

        if (shouldDoEffects)
            DoHitEffect(hitInfo);
        //GameObject.Destroy(this.gameObject);

        Die();
    }

    private void Die()
    {
        //Should Check : get audio info out of root to play audio completely before die
        audioInfo.KillYourself();
        GameObject.Destroy(this.gameObject);
    }

    #region Make Hit Effects
    private void DoHitEffect(RaycastHit hitInfo)
    {
        string hitTag = hitInfo.collider.tag.ToLower();

        HitEffect effect = hitManager.GetRelativeHitEffect(hitTag);

        if (effect == null)
            return;

        PlayHitSound(effect);
        PlayHitParticle(effect, hitInfo);
        MarkHitDecal(effect, hitInfo);
    }

    private void MarkHitDecal(HitEffect effect, RaycastHit hitInfo)
    {
        if (hitInfo.collider.renderer == null)
            return;

        if (effect.Hit_Texture.Length == 0)
        {
            Debug.LogError("no texture in decal");
            return;
        }

        if (MarkObject != null)
        {
            GameObject markObj = GameObject.Instantiate(MarkObject, hitInfo.point,
                                    Quaternion.FromToRotation(Vector3.up, hitInfo.normal)) as GameObject;

            MarkHandler markHandler = markObj.GetComponent<MarkHandler>();
            markHandler.GenerateMark(effect.Hit_Texture[Random.Range(0, effect.Hit_Texture.Length)],
                                            hitInfo);
        }

        #region Decal On All Surface Performance problem
        //GameObject obj = GameObject.Instantiate(DecalObject, hitInfo.point,
        //                        Quaternion.FromToRotation(Vector3.forward, -hitInfo.normal)) as GameObject;
        //DecalHandler decalHandler = obj.GetComponent<DecalHandler>();
        //decalHandler.GenerateDecal(effect.Hit_Texture[Random.Range(0, effect.Hit_Texture.Length)],
        //                                    hitInfo.collider.gameObject); 
        #endregion
    }

    private void PlayHitParticle(HitEffect effect, RaycastHit hitInfo)
    {
        float epsilon = -0.03f;

        Vector3 hitPoint = hitInfo.point + (hitInfo.normal * epsilon);
        Vector3 hitUpDir = hitInfo.normal;

        if (effect.Hit_Particle.Length == 0)
        {
            Debug.LogError("no particle");
            return;
        }
        GameObject obj = Instantiate(effect.Hit_Particle[Random.Range(0, effect.Hit_Particle.Length)], hitPoint,
                                Quaternion.FromToRotation(Vector3.forward, hitUpDir)) as GameObject;
    }

    private void PlayHitSound(HitEffect effect)
    {
        if (effect.Hit_Sound.Length == 0)
        {
            return;
        }

        audioInfo.PlayClip(effect.Hit_Sound[Random.Range(0, effect.Hit_Sound.Length)]);
    }
    #endregion

    private void DoPhysicalImpact(GameObject gameObject, Vector3 hitPos)
    {
        Rigidbody body = gameObject.rigidbody;
        if (body == null)
        {
            if (gameObject.transform.parent != null)
            {
                body = gameObject.transform.parent.rigidbody;
            }
        }

        if (body != null)
        {
            if (!body.isKinematic)
            {
                Vector3 dir = hitPos - shootPoint;
                body.AddForceAtPosition(dir.normalized * Impulse, hitPos, ForceMode.Impulse);
            }
        }
    }

    bool IsPlayerHitChanceHappened()
    {
        float curChance = playerHitChance * PlayerCharacterNew.Instance.GetCurBulletHitChance();

        float randVal = Random.Range(0f, 1f);

        return randVal < curChance;
    }

    public void SetDamage(float _val)
    {
        Damage = _val;
        dmgInfo.damageAmount = Damage;
    }
}