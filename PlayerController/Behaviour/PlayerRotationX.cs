using UnityEngine;
using System.Collections;



public class PlayerRotationX : MonoBehaviour
{
    public float sensitivityX = 15f;

    [HideInInspector]
    public Transform RotationBone;

    PlayerCharacterNew player;

    //private Vector3 prevRotationBone;

    void Start()
    {
        player = PlayerCharacterNew.Instance;

        if (rigidbody)
            rigidbody.freezeRotation = true;
    }

    void Update()
    {
        if (!PlayerCharacterNew.Instance.IsGamePaused())
        {
            if (!PlayerCharacterNew.Instance.IsPlayerStopped())
            {
                float mouseRotX = CustomInputManager.GetPlayerCamAxisX(); //Input.GetAxis("Mouse X") * sensitivityX;

                if (player.isOnSnipeMode)
                    mouseRotX *= player.snipeModeMouseSensitivityReductionCoef;

                float rotationX = mouseRotX;

                //rotationX += (RotationBone.localEulerAngles.x - prevRotationBone.x);

                transform.Rotate(0, rotationX, 0);

                //ResetPrevRotationBone();
            }

        }
    }

    public void SetBonesFromGuns(Transform rot)
    {
        RotationBone = rot;

        //ResetPrevRotationBone();
    }

    //void ResetPrevRotationBone()
    //{
    //    prevRotationBone = new Vector3(RotationBone.localEulerAngles.x, RotationBone.localEulerAngles.y, RotationBone.localEulerAngles.z);
    //}
}