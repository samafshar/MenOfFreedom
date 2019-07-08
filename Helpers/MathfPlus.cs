using UnityEngine;
using System.Collections;

public class MathfPlus : MonoBehaviour
{
    public static float HorizontalAngle(Vector3 direction)
    {
        return Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
    }

    public static Vector3 RotateVecAroundYAxis(Vector3 vec, float angle)
    {
        return new Vector3((float)(vec.x * Mathf.Cos(angle) + vec.z * Mathf.Sin(angle)), vec.y, (float)(vec.z * Mathf.Cos(angle) - vec.x * Mathf.Sin(angle)));
    }

    public static Vector3 RotateVecAroundYAxisByRotation(Vector3 vec, Quaternion rot)
    {
        return MathfPlus.RotateVecAroundYAxis(vec, rot.eulerAngles.y * Mathf.Deg2Rad);
    }

    public static float ClampLargerThanZero(float _value)
    {
        return Mathf.Clamp(_value, 0, float.MaxValue);
    }

    public static float DecByDeltatimeToZero(float _value)
    {
        float val = _value;

        if (val > 0)
        {
            val -= Time.deltaTime;
            val = ClampLargerThanZero(val);
        }

        return val;
    }

    public static float DecByDeltatimeToZeroWithAdditionalCoef(float _value, float _additionalCoef)
    {
        float val = _value;
        float additionalCoef = _additionalCoef;

        if (val > 0)
        {
            val -= Time.deltaTime * additionalCoef;
            val = ClampLargerThanZero(val);
        }

        return val;
    }

    public static float GetDeltaAngle(Vector3 _sourceForwardVec, Vector3 _sourcePos, Vector3 _targetPos)
    {
        Vector3 destForwardVec = _targetPos - _sourcePos;
        float deltaAngle = Mathf.DeltaAngle(
            MathfPlus.HorizontalAngle(_sourceForwardVec),
            MathfPlus.HorizontalAngle(destForwardVec));

        return deltaAngle;
    }

    public static float GetRandomFloat(float _minVal, float _maxVal)
    {
        return Random.Range(_minVal, _maxVal);
    }
}
