using UnityEngine;
using System.Collections;

public class SoldierMachineGun : MonoBehaviour
{
    [HideInInspector]
    public GameObject currentControllingSoldier = null;

    [HideInInspector]
    public float endRotateTimeCoef = 5;

    [HideInInspector]
    float angle = 0;

    public MachineGunType machineGunType = MachineGunType.Stand;

    public Bullet bullet;

    public float shootSpeed = 0.15f;

    public Transform standPosTr;

    public Transform shootPosTr;

    public Transform raycastStartTr;

    public GameObject rotatingObject;

    public SoldierGun gun;

    public AudioInfo audioInfo;

    //public AudioClip[] fireSounds;

    public void SetRotatingObjectAngle(float _angle)
    {
        rotatingObject.transform.localEulerAngles = new Vector3(0, _angle, 0);
        angle = _angle;
    }

    public void SetControllingSoldier(GameObject _controlledSoldier)
    {
        currentControllingSoldier = _controlledSoldier;

        gun.Set_ControlledSoldier(_controlledSoldier);
    }

    void Start()
    {
        gun.InitAudioInfo(audioInfo);
    }

    void Update()
    {
        if (currentControllingSoldier == null || currentControllingSoldier.GetComponent<CharacterInfo>().IsDead)
        {
            if (angle != 0)
            {
                SetRotatingObjectAngle(Mathf.LerpAngle(angle, 0, Time.deltaTime * endRotateTimeCoef));
            }

            if (gun.soldInfo != null)
                gun.Set_ControlledSoldier(null);
        }
    }
}
