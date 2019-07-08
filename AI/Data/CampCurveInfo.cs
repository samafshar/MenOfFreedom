using UnityEngine;
using System.Collections;

public class CampCurveInfo : MonoBehaviour
{
    public AnimsList animsList;
    public float crossfadeTime;
    public float delayCoefForStartAnimation_Min;
    public float delayCoefForStartAnimation_Max;
    public string _Point___________________________________ = "_________________________________________";
    public StartPoint pointToStay;
    public float pointStayTime;
    public string _Curve___________________________________ = "_________________________________________";
    public AntaresBezierCurve curve;
    public float speed;
    public float endMinSpeed;
    public float speedInc_Speed;
    public float speedDec_Speed;
    public float endLengthToStartDecreasingSpeed;
    public float endLengthToChangeAnim;
}
