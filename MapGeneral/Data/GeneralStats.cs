//<91-04-12>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ActionStatus
{
    NotStarted,
    Running,
    Finished,
}

public enum FightSideEnum
{
    Ally,
    Enemy,
    Civil,
}

public static class SurfaceMaterials
{
    public const string Default = "default";
    public const string Concrete = "concrete";
    public const string Wood = "wood";
    public const string Metal = "metal";
    public const string Water = "water";
    public const string Mud = "mud";
}

public static class BodyParts
{
    public const string head = "head";
    public const string rightHand = "righthand";
    public const string leftHand = "lefthand";
    public const string rightArm = "rightarm";
    public const string leftArm = "leftarm";

    public const string rightFoot = "rightfoot";
    public const string leftFoot = "leftfoot";

    public const string chest = "chest";
    public const string back = "back";
}

public static class GeneralStats
{
    public const string key_Escape = "Escape";
    public const string key_Enter= "Enter";

    public const int mainMenuIndex = 1;
    public const int CreditsIndex = 2;
    public const int LoadingPagesStartingIndex = 10;
    public const int LevelsStartingIndex = 20;
    public const int numOfLevels = 10;

    public const float PointRadius = 0.51f;

    public const string playerTagName_ToLower = "player";
    public const string enemyTagName_ToLower = "enemy";
    public const string allyTagName_ToLower = "ally";
    public const string camWallTagName_ToLower = "campwall";

    public const float grenadeMaxTorqueRangeX = 6;
    public const float grenadeMaxTorqueRangeY = 6;
    public const float grenadeMaxTorqueRangeZ = 6;

    public const float pausedGameTimeScale = 0.000001f;

    public const float campLevelShadowOptimiseDistance = 19;

    public static float deadNash_minLifeTimeWhileTouchingAnotherNash = 4f;
    public static float deadNash_maxLifeTimeAfterTouchingAnotherNash = 2.5f;
    public static float deadNash_minLifeTime = 8f;
    public static float deadNash_maxLifeTime = 30f;
    public static float deadNash_HittingNewNashMaxDist = 0.65f;

    public static bool IsVecInView(Vector3 targetVec, Vector3 sourcePos, Quaternion sourcePosRotation, float startAngle, float endAngle, float viewRadius)
    {
        Vector3 sPos = sourcePos;
        Quaternion sRot = sourcePosRotation;
        Vector3 tPos = targetVec;

        Vector3 dist = tPos - sPos;

        if (dist.sqrMagnitude > viewRadius * viewRadius)
            return false;

        float midAngle = ((startAngle + endAngle) / 2);
        float midAngleRange = ((endAngle - startAngle) / 2);

        Quaternion middleRot = Quaternion.Euler(0, midAngle + sRot.ToEuler().y * Mathf.Rad2Deg, 0);

        dist.y = 0;
        Quaternion distRot = Quaternion.LookRotation(dist);
        return (Quaternion.Angle(middleRot, distRot) <= midAngleRange);
    }

    public static void EmptyAnimationStateArray(GameObject go)
    {
        if (!go.animation) return;

        WrapMode savedWrapMode = go.animation.wrapMode;
        bool savedPlayAutomaticallyState = go.animation.playAutomatically;
        bool savedAnimatePhysicsState = go.animation.animatePhysics;

        AnimationCullingType savedCulType = go.animation.cullingType;

        Object.DestroyImmediate(go.GetComponent<Animation>(), true);

        Animation newAnim = (Animation)go.AddComponent(typeof(Animation));
        newAnim.wrapMode = savedWrapMode;
        newAnim.playAutomatically = savedPlayAutomaticallyState;
        newAnim.animatePhysics = savedAnimatePhysicsState;
        newAnim.cullingType = savedCulType;
    }

    public static T[] ReverseArray<T>(T[] _array)
    {
        int arrayLength = _array.Length;

        T[] revArray = new T[arrayLength];

        for (int i = 0; i < arrayLength; i++)
        {
            revArray[i] = _array[arrayLength - 1 - i];
        }

        return revArray;
    }

    public static bool IsCharacterAlive(GameObject _charObject)
    {
        GameObject charObject = _charObject;

        if (charObject == null)
            return false;

        CharacterInfo charInfo = charObject.GetComponent<CharacterInfo>();

        if (charInfo == null)
            return false;

        if (charInfo.IsDeadOrDisabled())
            return false;

        return true;
    }

    public static bool IsUnloopedAnimFinishedOnObject(GameObject _obj, string _anim, float _endCFTime, out float _remainingEndTime)
    {
        GameObject obj = _obj;
        string anim = _anim;
        float endCFTime = _endCFTime;

        _remainingEndTime = obj.animation[anim].length - obj.animation[anim].time;
        _remainingEndTime = Mathf.Clamp(_remainingEndTime, 0, obj.animation[anim].length);

        if (obj.animation[anim].time >= obj.animation[anim].length - endCFTime)
            return true;

        if (!obj.animation[anim].enabled)
        {
            _remainingEndTime = 0;
            return true;
        }

        return false;
    }

    public static bool IsUnloopedAnimFinishedOnObject(GameObject _obj, string _anim, float _endCFTime)
    {
        float temp;

        return IsUnloopedAnimFinishedOnObject(_obj, _anim, _endCFTime, out temp);
    }

    public static Transform FindInChildren(Transform _root, string _name)
    {
        Transform root = _root;
        string name = _name;

        foreach (Transform child in root)
        {
            if (child.name == name)
            {
                return child;
            }

            Transform childResult = FindInChildren(child, name);

            if (childResult)
                return childResult;
        }

        return null;
    }

    public static Transform FindInChildrenByBoneEnum(Transform _root, BoneNameEnum _name)
    {
        Transform root = _root;
        BoneNameEnum name = _name;

        foreach (Transform child in root)
        {
            BoneNamesForCharacters bnfc = child.GetComponent<BoneNamesForCharacters>();

            if (bnfc && bnfc.boneName == name)
                return child;

            Transform childResult = FindInChildrenByBoneEnum(child, name);

            if (childResult)
                return childResult;
        }

        return null;
    }
}
