using UnityEngine;
using System.Collections;

public enum LogicJob_Anim_Type
{
    Waiting_Stand_Right,
    Waiting_Stand_Left,
    Waiting_Sit_Right,
    Waiting_Sit_Left,
    CrowdHappiness_A,
    CrowdHappiness_B,
    Kamiun,
    KickDoor_A,
    KickDoor_B,
    Sigar,
    WalkPatrol,
    Zakhmi_A,
    Zakhmi_B,
}

public class LogicJob_Anim_Info : MonoBehaviour {

    public LogicJob_Anim_Type animType;

    public AnimsList animsList;

    public float defaultCrossfadeTime = 0.5f;
}
