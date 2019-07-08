using UnityEngine;
using System.Collections;

public class SoldierBodyInfo : MonoBehaviour {
    //public AnimationClip fullAnimsClip;
    public GameObject SoldierAnimObject;
    //public Transform soldierRoot;
    //public Transform soldierUpperBodyRoot;
    //public Transform soldierGunRoot;
    public Transform soldierGunFireTr;
    public Transform soldierHeadTr;
    public Transform soldierEyeTr;
    //mf
    public Transform soldierLeftFootTr;
    public Transform soldierRightFootTr;
    public Transform soldierRightHandTr;
    //~mf
    public Transform[] shootRaycastTargets;
    public string _PlayerViewCheck_ = "___________________";
    public Transform[] playerViewTrs;
}
