using UnityEngine;
using System.Collections;

public enum CampWallType
{
    AlwaysBanView,
    OnCloseMovingLetView,
    OnBeingCloseLetView,
}

public class CampWall : MonoBehaviour
{
    public CampWallType wallType = CampWallType.AlwaysBanView;
}
