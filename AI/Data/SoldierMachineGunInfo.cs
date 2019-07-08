using UnityEngine;
using System.Collections;

public enum MachineGunType
{
    Stand,
}

public class SoldierMachineGunInfo : MonoBehaviour
{
    public MachineGunType machineGunType;

    public AnimsList animsList_SoldierOnMachineGun_Mid;
    public AnimsList animsList_SoldierOnMachineGun_Left;
    public AnimsList animsList_SoldierOnMachineGun_Right;	
}
