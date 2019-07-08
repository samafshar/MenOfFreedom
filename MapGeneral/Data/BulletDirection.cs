using UnityEngine;
using System.Collections;

public class BulletDirection : MonoBehaviour
{
    private Vector3 pos1;
    private Vector3 posPlayer;
    private Vector3 vectorDis;
    private float distance;
    private float angle;
    private Vector3 Cross;
    //private GameObject obj;
    private CharacterController PlayerControler;
    public enum DirState {North,NorthEast,NorthWest,East,West,South,SouthWest,SouthEast };

    void Start()
    {
        //obj = GameObject.FindGameObjectWithTag("cube") as GameObject;
        PlayerControler = GetComponent("CharacterController") as CharacterController;
    }

    //void Update()
    //{
    //    //pos1 = obj.transform.position;
    //    //DirState testState;
    //    //testState=CalculateBulletDir(pos1);
    //}
    public DirState CalculateBulletDir(Vector3 bulletPos)
    {
        posPlayer = PlayerControler.transform.position;
        bulletPos = new Vector3(bulletPos.x, posPlayer.y, bulletPos.z);
        vectorDis = bulletPos - posPlayer;
        angle = CalculateAngle(vectorDis);
        Cross = CalculateCross(vectorDis);
       DirState BulletDirectionState = CalculateDirection(angle, Cross);
        return BulletDirectionState;
    }
    float CalculateAngle(Vector3 dis)
    {
        Vector3 forward = PlayerControler.transform.forward;
        float ang = Vector3.Angle(dis, forward);


        return ang;

    }

    Vector3 CalculateCross(Vector3 dis)
    {
        Vector3 forward = PlayerControler.transform.forward;
        Vector3 ang = Vector3.Cross(forward, dis);


        return ang;
    }

     DirState CalculateDirection(float currentAngle, Vector3 currentCross)
    {
        DirState currentDir = DirState.North;
        if (0 <= currentAngle && currentAngle < 22.5)
        {
            currentDir=DirState.North;
        }
        if (22.5 <= currentAngle && currentAngle < 67.5)
        {
            if (currentCross.y > 0)
            {
                currentDir = DirState.NorthEast;
            }
            if (currentCross.y < 0)
            {
                currentDir =DirState.NorthWest;
            }
        }
        if (67.5<= currentAngle && currentAngle < 112.5)
        {
            if (currentCross.y > 0)
            {
                currentDir = DirState.East;
            }
            if (currentCross.y < 0)
            {
                currentDir = DirState.West;
            }
        }
        if (112.5 <= currentAngle && currentAngle < 157.5)
        {
            if (currentCross.y > 0)
            {
                currentDir = DirState.SouthEast;
            }
            if (currentCross.y < 0)
            {
                currentDir = DirState.SouthWest;
            }
        }

        if (157.5 <= currentAngle && currentAngle < 180)
        {
            currentDir = DirState.South;
        }
        return currentDir;
    }
}
