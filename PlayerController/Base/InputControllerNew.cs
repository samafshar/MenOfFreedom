using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerCharacterNew))]
[RequireComponent(typeof(CharacterMotor))]

public class InputControllerNew : MonoBehaviour
{
    PlayerCharacterNew player;
    CharacterMotor playerCharMotor;
    PlayerKeys keys;


    void Start()
    {
        player = PlayerCharacterNew.Instance;
        playerCharMotor = GetComponent<CharacterMotor>();
        keys = player.keys;
    }

    void Update()
    {
        if (!PlayerCharacterNew.Instance.IsGamePaused())
        {
            if (!PlayerCharacterNew.Instance.IsPlayerStopped())
            {
                #region Char Motor

                //float forwardAxis = 0;
                //float backwardAxis = 0;
                //float leftAxis = 0;
                //float rightAxis = 0;

                //if (CustomInputManager.KeyIfGameIsNotPaused_MoveForward())
                //    forwardAxis = 1;

                //if (CustomInputManager.KeyIfGameIsNotPaused_MoveBackward())
                //    backwardAxis = -1;

                //if (CustomInputManager.KeyIfGameIsNotPaused_MoveLeft())
                //    leftAxis = -1;

                //if (CustomInputManager.KeyIfGameIsNotPaused_MoveRight())
                //    rightAxis = 1;

                //Vector3 directionVector = new Vector3(leftAxis + rightAxis, 0, forwardAxis + backwardAxis);

                Vector3 directionVector = new Vector3(CustomInputManager.GetHorizontalMovementAxis(), 0, CustomInputManager.GetVerticalMovementAxis());

                if (directionVector != Vector3.zero)
                {
                    float directionLength = directionVector.magnitude;
                    directionVector = directionVector / directionLength;

                    directionLength = Mathf.Min(1, directionLength);

                    directionLength = directionLength * directionLength;

                    directionVector = directionVector * directionLength;
                }

                playerCharMotor.inputMoveDirection = transform.rotation * directionVector;

                playerCharMotor.inputJump = CustomInputManager.KeyDownIfGameIsNotPaused_Jump();
 
                #endregion

                if (CustomInputManager.KeyIfGameIsNotPaused_Sprint()) //(GetButton(keys.sprint))
                {
                    if (player.canRefreshSprintKey)
                    {
                        player.canRefreshSprintKey = false;
                        player.sprintingOK = true;
                    }
                }
                else
                {
                    player.canRefreshSprintKey = true;
                }

                //

                if (CustomInputManager.KeyIfGameIsNotPaused_Fire()) //(GetButton(keys.fire))
                {
                    if (player.canRefreshFireKey)
                    {
                        player.canRefreshFireKey = false;
                        player.firingOK = true;
                    }
                }
                else
                {
                    player.canRefreshFireKey = true;
                }

                //

                #region Horiz Movement

                if (player.IsVertMovementState(PlayerVertMovementStateEnum.Stand)
                    || player.IsVertMovementState(PlayerVertMovementStateEnum.Jump)
                    || player.IsVertMovementState(PlayerVertMovementStateEnum.StandToSit_Init)
                    || player.IsVertMovementState(PlayerVertMovementStateEnum.StandToSit)
                    || player.IsVertMovementState(PlayerVertMovementStateEnum.SitToStand_Init)
                    || player.IsVertMovementState(PlayerVertMovementStateEnum.SitToStand))
                {
                    switch (player.horizMovementState)
                    {
                        case PlayerHorizMovementStateEnum.NoMove:
                        case PlayerHorizMovementStateEnum.NormalMove:
                        case PlayerHorizMovementStateEnum.FastMove:

                            bool shouldSprKeyPressed = true;
                            if (player.horizMovementState == PlayerHorizMovementStateEnum.FastMove)
                                shouldSprKeyPressed = false;

                            if (CustomInputManager.KeyIfGameIsNotPaused_HorizontalMovement() || CustomInputManager.KeyIfGameIsNotPaused_VerticalMovement()) //(GetButton(keys.moveHorizontal) || GetButton(keys.moveVertical))
                            {
                                if (ShouldRun(shouldSprKeyPressed))
                                {
                                    if (CustomInputManager.GetVerticalMovementAxis()>0) //(CustomInputManager.KeyIfGameIsNotPaused_MoveForward()) //(GetAxis(keys.moveVertical) > 0)
                                    {
                                        player.SetHorizMovementState(PlayerHorizMovementStateEnum.FastMove);
                                    }
                                    else
                                    {
                                        if (CustomInputManager.GetVerticalMovementAxis()==0) //(!CustomInputManager.KeyIfGameIsNotPaused_VerticalMovement()) //(GetAxis(keys.moveVertical) == 0)
                                        {
                                            if  (CustomInputManager.GetHorizontalMovementAxis() == 0) //(!CustomInputManager.KeyIfGameIsNotPaused_HorizontalMovement()) //(GetAxis(keys.moveHorizontal) == 0)
                                                player.SetHorizMovementState(PlayerHorizMovementStateEnum.NoMove);
                                            else
                                                player.SetHorizMovementState(PlayerHorizMovementStateEnum.NormalMove);
                                        }
                                        else
                                        {
                                            player.SetHorizMovementState(PlayerHorizMovementStateEnum.NormalMove);
                                        }
                                    }
                                }
                                else
                                {
                                    player.SetHorizMovementState(PlayerHorizMovementStateEnum.NormalMove);
                                }
                            }
                            else
                            {
                                player.SetHorizMovementState(PlayerHorizMovementStateEnum.NoMove);
                            }

                            break;
                    }
                }
                else
                {
                    if (player.IsVertMovementState(PlayerVertMovementStateEnum.Sit))
                    {
                        switch (player.horizMovementState)
                        {
                            case PlayerHorizMovementStateEnum.NoMove:
                            case PlayerHorizMovementStateEnum.SlowMove:
                            case PlayerHorizMovementStateEnum.NormalMove:
                            case PlayerHorizMovementStateEnum.FastMove:

                                if (CustomInputManager.KeyIfGameIsNotPaused_HorizontalMovement() || CustomInputManager.KeyIfGameIsNotPaused_VerticalMovement()) //(GetButton(keys.moveHorizontal) || GetButton(keys.moveVertical))
                                {
                                    if (ShouldRun(true))
                                    {
                                        if (CustomInputManager.GetVerticalMovementAxis() > 0) //(CustomInputManager.KeyIfGameIsNotPaused_MoveForward()) //(GetAxis(keys.moveVertical) > 0)
                                        {
                                            player.SetHorizMovementState(PlayerHorizMovementStateEnum.FastMove);
                                            player.SetVertMovementState(PlayerVertMovementStateEnum.SitToStand_Init);
                                        }
                                        else
                                        {
                                            if (CustomInputManager.GetVerticalMovementAxis() == 0) //(!CustomInputManager.KeyIfGameIsNotPaused_VerticalMovement()) //(GetAxis(keys.moveVertical) == 0)
                                            {
                                                if (CustomInputManager.GetHorizontalMovementAxis() == 0) //(!CustomInputManager.KeyIfGameIsNotPaused_HorizontalMovement()) //(GetAxis(keys.moveHorizontal) == 0)
                                                    player.SetHorizMovementState(PlayerHorizMovementStateEnum.NoMove);
                                                else
                                                    player.SetHorizMovementState(PlayerHorizMovementStateEnum.SlowMove);
                                            }
                                            else
                                            {
                                                player.SetHorizMovementState(PlayerHorizMovementStateEnum.SlowMove);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        player.SetHorizMovementState(PlayerHorizMovementStateEnum.SlowMove);
                                    }
                                }
                                else
                                {
                                    player.SetHorizMovementState(PlayerHorizMovementStateEnum.NoMove);
                                }

                                break;
                        }
                    }
                }

                #endregion

                #region Vertical
                switch (player.vertMovementState)
                {
                    case PlayerVertMovementStateEnum.Stand:

                        if (CustomInputManager.KeyDownIfGameIsNotPaused_Crouch()) //(GetButtonDown(keys.crouch))
                        {
                            if (CustomInputManager.KeyDownIfGameIsNotPaused_Jump()) //(GetButtonDown(keys.jump))
                            {
                                player.SetVertMovementState(PlayerVertMovementStateEnum.Stand);
                            }
                            else
                            {
                                player.TryStartSitting();
                            }
                        }
                        else
                        {
                            if (CustomInputManager.KeyDownIfGameIsNotPaused_Jump()) //(GetButtonDown(keys.jump))
                            {
                                player.SetVertMovementState(PlayerVertMovementStateEnum.Jump);
                            }
                        }

                        break;

                    case PlayerVertMovementStateEnum.Sit:

                        if (CustomInputManager.KeyDownIfGameIsNotPaused_Crouch()) //(GetButtonDown(keys.crouch))
                        {
                            player.SetVertMovementState(PlayerVertMovementStateEnum.SitToStand_Init);
                            player.SetHorizMovementState(PlayerHorizMovementStateEnum.NormalMove);
                        }
                        else
                        {
                            if (CustomInputManager.KeyDownIfGameIsNotPaused_Jump()) //(GetButtonDown(keys.jump))
                            {
                                player.SetVertMovementState(PlayerVertMovementStateEnum.SitToStand_Init);
                                player.SetHorizMovementState(PlayerHorizMovementStateEnum.NormalMove);
                            }
                        }

                        break;
                }
                #endregion

            }

        }
    }

    //public bool GetButton(string _btnName)
    //{
    //    string btnName = _btnName;

    //    return GameController.GetButton_IfGameIsNotPaused(btnName);
    //}

    //public bool GetButtonDown(string _btnName)
    //{
    //    string btnName = _btnName;

    //    return GameController.GetButtonDown_IfGameIsNotPaused(btnName);
    //}

    //public bool GetKey(KeyCode _keyCode)
    //{
    //    KeyCode keyCode = _keyCode;

    //    return GameController.GetKey_IfGameIsNotPaused(keyCode);
    //}

    //public bool GetKeyDown(KeyCode _keyCode)
    //{
    //    KeyCode keyCode = _keyCode;

    //    return GameController.GetKeyDown_IfGameIsNotPaused(keyCode);
    //}

    //public float GetAxis(string _btnName)
    //{
    //    string btnName = _btnName;

    //    return Input.GetAxis(btnName);
    //}

    bool ShouldRun(bool _shouldSprintKeyPressed)
    {
        bool shouldSprintKeyPressed = _shouldSprintKeyPressed;

        if (!player.isCampPlayer)
            return NormalMode_ShouldRun(shouldSprintKeyPressed);
        else
            return CampMode_ShouldRun(true);

        return true;
    }

    bool NormalMode_ShouldRun(bool _shouldSprintKeyPressed)
    {
        bool shouldSprintKeyPressed = _shouldSprintKeyPressed;

        if (player.playerShouldTakeABreathFromRunning)
            return false;

        if (shouldSprintKeyPressed)
            return player.sprintingOK && player.canRun && CustomInputManager.KeyIfGameIsNotPaused_Sprint(); //GetButton(keys.sprint);
        else
            return player.sprintingOK && player.canRun;

        return true;
    }

    bool CampMode_ShouldRun(bool _shouldSprintKeyPressed)
    {
        bool shouldSprintKeyPressed = _shouldSprintKeyPressed;

        if (shouldSprintKeyPressed)
            return player.sprintingOK && player.canRun && CustomInputManager.KeyIfGameIsNotPaused_Sprint(); //GetButton(keys.sprint);
        else
            return player.sprintingOK && player.canRun;

        return true;
    }
}
