using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameKeyInfo
{
    public KeyCode primary = KeyCode.None;
    public string prInputName = "";
    public KeyCode secondary = KeyCode.None;
    public string secInputName = "";
}

public class GameKeys
{
    public List<GameKeyInfo> _allKeys = new List<GameKeyInfo>();

    public GameKeyInfo MoveLeft = new GameKeyInfo();
    public GameKeyInfo MoveRight = new GameKeyInfo();
    public GameKeyInfo MoveForward = new GameKeyInfo();
    public GameKeyInfo MoveBackward = new GameKeyInfo();
    public GameKeyInfo Jump = new GameKeyInfo();
    public GameKeyInfo Sprint_SnipeSteady = new GameKeyInfo();
    public GameKeyInfo Crouch = new GameKeyInfo();
    public GameKeyInfo Action = new GameKeyInfo();
    public GameKeyInfo Fire = new GameKeyInfo();
    public GameKeyInfo Aim = new GameKeyInfo();
    public GameKeyInfo ChangeGun = new GameKeyInfo();
    public GameKeyInfo Grenade_SnipeTimeController = new GameKeyInfo();
    public GameKeyInfo Melee = new GameKeyInfo();
    public GameKeyInfo Reload = new GameKeyInfo();
    public GameKeyInfo Missions = new GameKeyInfo();

    //Warning: Every new key info must be added to allKeyList!!!
}

public static class CustomInputManager
{
    public static GameKeys keys = new GameKeys();

    public static float defaultSensitivityX = 1.5f;
    public static float defaultSensitivityY = 1.5f;

    public static float sensitivityX = 1.5f;
    public static float sensitivityY = 1.5f;

    public static bool invertMouse = false;

    public static bool useMouseWheelToChangeWeapon = true;

    public static List<KeyCode> validKeys = new List<KeyCode>();

    //

    public static void Init()
    {
        InitValidKeys();
        InitDefaultKeys();
    }

    public static void InitValidKeys()
    {
        validKeys.Add(KeyCode.A);
        validKeys.Add(KeyCode.B);
        validKeys.Add(KeyCode.C);
        validKeys.Add(KeyCode.D);
        validKeys.Add(KeyCode.E);
        validKeys.Add(KeyCode.F);
        validKeys.Add(KeyCode.G);
        validKeys.Add(KeyCode.H);
        validKeys.Add(KeyCode.I);
        validKeys.Add(KeyCode.J);
        validKeys.Add(KeyCode.K);
        validKeys.Add(KeyCode.L);
        validKeys.Add(KeyCode.M);
        validKeys.Add(KeyCode.N);
        validKeys.Add(KeyCode.O);
        validKeys.Add(KeyCode.P);
        validKeys.Add(KeyCode.Q);
        validKeys.Add(KeyCode.R);
        validKeys.Add(KeyCode.S);
        validKeys.Add(KeyCode.T);
        validKeys.Add(KeyCode.U);
        validKeys.Add(KeyCode.V);
        validKeys.Add(KeyCode.W);
        validKeys.Add(KeyCode.X);
        validKeys.Add(KeyCode.Y);
        validKeys.Add(KeyCode.Z);

        validKeys.Add(KeyCode.Alpha0);
        validKeys.Add(KeyCode.Alpha1);
        validKeys.Add(KeyCode.Alpha2);
        validKeys.Add(KeyCode.Alpha3);
        validKeys.Add(KeyCode.Alpha4);
        validKeys.Add(KeyCode.Alpha5);
        validKeys.Add(KeyCode.Alpha6);
        validKeys.Add(KeyCode.Alpha7);
        validKeys.Add(KeyCode.Alpha8);
        validKeys.Add(KeyCode.Alpha9);

        validKeys.Add(KeyCode.F1);
        validKeys.Add(KeyCode.F2);
        validKeys.Add(KeyCode.F3);
        validKeys.Add(KeyCode.F4);
        validKeys.Add(KeyCode.F5);
        validKeys.Add(KeyCode.F6);
        validKeys.Add(KeyCode.F7);
        validKeys.Add(KeyCode.F8);
        validKeys.Add(KeyCode.F9);
        validKeys.Add(KeyCode.F10);
        validKeys.Add(KeyCode.F11);
        validKeys.Add(KeyCode.F12);
        validKeys.Add(KeyCode.F13);
        validKeys.Add(KeyCode.F14);
        validKeys.Add(KeyCode.F15);

        validKeys.Add(KeyCode.DownArrow); //Down arrow key 
        validKeys.Add(KeyCode.LeftArrow); //Left arrow key 
        validKeys.Add(KeyCode.RightArrow); //Right arrow key 
        validKeys.Add(KeyCode.UpArrow); //Up arrow key 

        validKeys.Add(KeyCode.Tab); //The tab key 
        validKeys.Add(KeyCode.Space); //Space key 
        validKeys.Add(KeyCode.Backspace); //The backspace key 
        validKeys.Add(KeyCode.Return); //Return key 

        validKeys.Add(KeyCode.AltGr);
        validKeys.Add(KeyCode.LeftAlt);
        validKeys.Add(KeyCode.LeftShift); //Left shift key 
        validKeys.Add(KeyCode.LeftControl); //Left Control key 
        validKeys.Add(KeyCode.RightAlt); //Right Alt key 
        validKeys.Add(KeyCode.RightControl); //Right Control key
        validKeys.Add(KeyCode.RightShift); //Right shift key 

        validKeys.Add(KeyCode.Keypad0);
        validKeys.Add(KeyCode.Keypad1);
        validKeys.Add(KeyCode.Keypad2);
        validKeys.Add(KeyCode.Keypad3);
        validKeys.Add(KeyCode.Keypad4);
        validKeys.Add(KeyCode.Keypad5);
        validKeys.Add(KeyCode.Keypad6);
        validKeys.Add(KeyCode.Keypad7);
        validKeys.Add(KeyCode.Keypad8);
        validKeys.Add(KeyCode.Keypad9);
        validKeys.Add(KeyCode.KeypadDivide); //Numeric keypad '/' 
        validKeys.Add(KeyCode.KeypadMinus); //Numeric keypad '-' 
        validKeys.Add(KeyCode.KeypadMultiply); //Numeric keypad '*' 
        validKeys.Add(KeyCode.KeypadPeriod); //Numeric keypad '.' 
        validKeys.Add(KeyCode.KeypadPlus); //Numeric keypad '+'
        validKeys.Add(KeyCode.KeypadEnter); //Numeric keypad enter
        validKeys.Add(KeyCode.KeypadEquals); //Numeric keypad '=' 

        validKeys.Add(KeyCode.Clear); //The Clear key 
        validKeys.Add(KeyCode.Help); //Help key 

        validKeys.Add(KeyCode.Print); //Print key 
        validKeys.Add(KeyCode.SysReq); //Sys Req key 
        validKeys.Add(KeyCode.Break); //Break key 
        validKeys.Add(KeyCode.Pause); //Pause on PC machines 

        validKeys.Add(KeyCode.Insert); //Insert key key 
        validKeys.Add(KeyCode.Delete); //The forward delete key
        validKeys.Add(KeyCode.Home); //Home key 
        validKeys.Add(KeyCode.End); //End key 
        validKeys.Add(KeyCode.PageUp); //Page up 
        validKeys.Add(KeyCode.PageDown); //Page down 

        validKeys.Add(KeyCode.ScrollLock); //Scroll lock key 
        validKeys.Add(KeyCode.CapsLock); //Capslock key 
        validKeys.Add(KeyCode.Numlock); //Numlock key 

        validKeys.Add(KeyCode.Ampersand); //Ampersand key '&' 
        validKeys.Add(KeyCode.Asterisk); //Asterisk key '*' 
        validKeys.Add(KeyCode.At); //At key '@' 
        validKeys.Add(KeyCode.BackQuote); //Back quote key '`' 
        validKeys.Add(KeyCode.Backslash); //Backslash key '\' 
        validKeys.Add(KeyCode.Caret); //Caret key '^' 
        validKeys.Add(KeyCode.Colon); //Colon ':' key 
        validKeys.Add(KeyCode.Comma); //Comma ',' key 
        validKeys.Add(KeyCode.Dollar); //Dollar sign key '$' 
        validKeys.Add(KeyCode.DoubleQuote); //Double quote key '"' 
        validKeys.Add(KeyCode.Equals); //Equals '=' key 
        validKeys.Add(KeyCode.Exclaim); //Exclamation mark key '!' 
        validKeys.Add(KeyCode.Greater); //Greater than '>' key 
        validKeys.Add(KeyCode.Hash); //Hash key '#' 
        validKeys.Add(KeyCode.LeftBracket); //Left square bracket key '[' 
        validKeys.Add(KeyCode.LeftParen); //Left Parenthesis key '(' 
        validKeys.Add(KeyCode.Less); //Less than '<' key 
        validKeys.Add(KeyCode.Minus); //Minus '-' key 
        validKeys.Add(KeyCode.Period); //Period '.' key 
        validKeys.Add(KeyCode.Plus); //Plus key '+' 
        validKeys.Add(KeyCode.Question); //Question mark '?' key 
        validKeys.Add(KeyCode.Quote); //Quote key
        validKeys.Add(KeyCode.RightBracket); //Right square bracket key ']' 
        validKeys.Add(KeyCode.RightParen); //Right Parenthesis key ')' ' 
        validKeys.Add(KeyCode.Semicolon); //Semicolon ';' key 
        validKeys.Add(KeyCode.Slash); //Slash '/' key 
        validKeys.Add(KeyCode.Underscore); //Underscore '_' key 

        validKeys.Add(KeyCode.Mouse0); //First (primary) mouse button 
        validKeys.Add(KeyCode.Mouse1); //Second (secondary) mouse button 
        validKeys.Add(KeyCode.Mouse2); //Third mouse button 

        //validKeys.Add(KeyCode.Mouse3); //Fourth mouse button 
        //validKeys.Add(KeyCode.Mouse4); //Fifth mouse button 
        //validKeys.Add(KeyCode.Mouse5); //Sixth mouse button 
        //validKeys.Add(KeyCode.Mouse6); //Seventh mouse button 

        //KeyCode.Escape
        //KeyCode.Menu //Menu key

        //KeyCode.RightWindows //Right Windows key 
        //KeyCode.LeftWindows //Left Windows key 
        //KeyCode.RightApple //Right Apple key 
        //KeyCode.LeftApple //Left Apple key 

        //KeyCode.Joystick1Button0 
        //Button 0 on first joystick 
        //KeyCode.Joystick1Button1 
        //Button 1 on first joystick 
        //KeyCode.Joystick1Button10 
        //Button 10 on first joystick 
        //KeyCode.Joystick1Button11 
        //Button 11 on first joystick 
        //KeyCode.Joystick1Button12 
        //Button 12 on first joystick 
        //KeyCode.Joystick1Button13 
        //Button 13 on first joystick 
        //KeyCode.Joystick1Button14 
        //Button 14 on first joystick 
        //KeyCode.Joystick1Button15 
        //Button 15 on first joystick 
        //KeyCode.Joystick1Button16 
        //Button 16 on first joystick 
        //KeyCode.Joystick1Button17 
        //Button 17 on first joystick 
        //KeyCode.Joystick1Button18 
        //Button 18 on first joystick 
        //KeyCode.Joystick1Button19 
        //Button 19 on first joystick 
        //KeyCode.Joystick1Button2 
        //Button 2 on first joystick 
        //KeyCode.Joystick1Button3 
        //Button 3 on first joystick 
        //KeyCode.Joystick1Button4 
        //Button 4 on first joystick 
        //KeyCode.Joystick1Button5 
        //Button 5 on first joystick 
        //KeyCode.Joystick1Button6 
        //Button 6 on first joystick 
        //KeyCode.Joystick1Button7 
        //Button 7 on first joystick 
        //KeyCode.Joystick1Button8 
        //Button 8 on first joystick 
        //KeyCode.Joystick1Button9 
        //Button 9 on first joystick 
        //KeyCode.Joystick2Button0 
        //Button 0 on second joystick 
        //KeyCode.Joystick2Button1 
        //Button 1 on second joystick 
        //KeyCode.Joystick2Button10 
        //Button 10 on second joystick 
        //KeyCode.Joystick2Button11 
        //Button 11 on second joystick 
        //KeyCode.Joystick2Button12 
        //Button 12 on second joystick 
        //KeyCode.Joystick2Button13 
        //Button 13 on second joystick 
        //KeyCode.Joystick2Button14 
        //Button 14 on second joystick 
        //KeyCode.Joystick2Button15 
        //Button 15 on second joystick 
        //KeyCode.Joystick2Button16 
        //Button 16 on second joystick 
        //KeyCode.Joystick2Button17 
        //Button 17 on second joystick 
        //KeyCode.Joystick2Button18 
        //Button 18 on second joystick 
        //KeyCode.Joystick2Button19 
        //Button 19 on second joystick 
        //KeyCode.Joystick2Button2 
        //Button 2 on second joystick 
        //KeyCode.Joystick2Button3 
        //Button 3 on second joystick 
        //KeyCode.Joystick2Button4 
        //Button 4 on second joystick 
        //KeyCode.Joystick2Button5 
        //Button 5 on second joystick 
        //KeyCode.Joystick2Button6 
        //Button 6 on second joystick 
        //KeyCode.Joystick2Button7 
        //Button 7 on second joystick 
        //KeyCode.Joystick2Button8 
        //Button 8 on second joystick 
        //KeyCode.Joystick2Button9 
        //Button 9 on second joystick 
        //KeyCode.Joystick3Button0 
        //Button 0 on third joystick 
        //KeyCode.Joystick3Button1 
        //Button 1 on third joystick 
        //KeyCode.Joystick3Button10 
        //Button 10 on third joystick 
        //KeyCode.Joystick3Button11 
        //Button 11 on third joystick 
        //KeyCode.Joystick3Button12 
        //Button 12 on third joystick 
        //KeyCode.Joystick3Button13 
        //Button 13 on third joystick 
        //KeyCode.Joystick3Button14 
        //Button 14 on third joystick 
        //KeyCode.Joystick3Button15 
        //Button 15 on third joystick 
        //KeyCode.Joystick3Button16 
        //Button 16 on third joystick 
        //KeyCode.Joystick3Button17 
        //Button 17 on third joystick 
        //KeyCode.Joystick3Button18 
        //Button 18 on third joystick 
        //KeyCode.Joystick3Button19 
        //Button 19 on third joystick 
        //KeyCode.Joystick3Button2 
        //Button 2 on third joystick 
        //KeyCode.Joystick3Button3 
        //Button 3 on third joystick 
        //KeyCode.Joystick3Button4 
        //Button 4 on third joystick 
        //KeyCode.Joystick3Button5 
        //Button 5 on third joystick 
        //KeyCode.Joystick3Button6 
        //Button 6 on third joystick 
        //KeyCode.Joystick3Button7 
        //Button 7 on third joystick 
        //KeyCode.Joystick3Button8 
        //Button 8 on third joystick 
        //KeyCode.Joystick3Button9 
        //Button 9 on third joystick 
        //KeyCode.Joystick4Button0 
        //Button 0 on forth joystick 
        //KeyCode.Joystick4Button1 
        //Button 1 on forth joystick 
        //KeyCode.Joystick4Button10 
        //Button 10 on forth joystick 
        //KeyCode.Joystick4Button11 
        //Button 11 on forth joystick 
        //KeyCode.Joystick4Button12 
        //Button 12 on forth joystick 
        //KeyCode.Joystick4Button13 
        //Button 13 on forth joystick 
        //KeyCode.Joystick4Button14 
        //Button 14 on forth joystick 
        //KeyCode.Joystick4Button15 
        //Button 15 on forth joystick 
        //KeyCode.Joystick4Button16 
        //Button 16 on forth joystick 
        //KeyCode.Joystick4Button17 
        //Button 17 on forth joystick 
        //KeyCode.Joystick4Button18 
        //Button 18 on forth joystick 
        //KeyCode.Joystick4Button19 
        //Button 19 on forth joystick 
        //KeyCode.Joystick4Button2 
        //Button 2 on forth joystick 
        //KeyCode.Joystick4Button3 
        //Button 3 on forth joystick 
        //KeyCode.Joystick4Button4 
        //Button 4 on forth joystick 
        //KeyCode.Joystick4Button5 
        //Button 5 on forth joystick 
        //KeyCode.Joystick4Button6 
        //Button 6 on forth joystick 
        //KeyCode.Joystick4Button7 
        //Button 7 on forth joystick 
        //KeyCode.Joystick4Button8 
        //Button 8 on forth joystick 
        //KeyCode.Joystick4Button9 
        //Button 9 on forth joystick 
        //KeyCode.JoystickButton0 
        //Button 0 on any joystick 
        //KeyCode.JoystickButton1 
        //Button 1 on any joystick 
        //KeyCode.JoystickButton10 
        //Button 10 on any joystick 
        //KeyCode.JoystickButton11 
        //Button 11 on any joystick 
        //KeyCode.JoystickButton12 
        //Button 12 on any joystick 
        //KeyCode.JoystickButton13 
        //Button 13 on any joystick 
        //KeyCode.JoystickButton14 
        //Button 14 on any joystick 
        //KeyCode.JoystickButton15 
        //Button 15 on any joystick 
        //KeyCode.JoystickButton16 
        //Button 16 on any joystick 
        //KeyCode.JoystickButton17 
        //Button 17 on any joystick 
        //KeyCode.JoystickButton18 
        //Button 18 on any joystick 
        //KeyCode.JoystickButton19 
        //Button 19 on any joystick 
        //KeyCode.JoystickButton2 
        //Button 2 on any joystick 
        //KeyCode.JoystickButton3 
        //Button 3 on any joystick 
        //KeyCode.JoystickButton4 
        //Button 4 on any joystick 
        //KeyCode.JoystickButton5 
        //Button 5 on any joystick 
        //KeyCode.JoystickButton6 
        //Button 6 on any joystick 
        //KeyCode.JoystickButton7 
        //Button 7 on any joystick 
        //KeyCode.JoystickButton8 
        //Button 8 on any joystick 
        //KeyCode.JoystickButton9 
        //Button 9 on any joystick 
    }

    public static void InitDefaultKeys()
    {
        keys.MoveLeft.primary = KeyCode.A;
        keys.MoveLeft.secondary = KeyCode.LeftArrow;

        keys.MoveLeft.prInputName = "A";
        keys.MoveLeft.secInputName = "Left";

        keys._allKeys.Add(keys.MoveLeft);

        //

        keys.MoveRight.primary = KeyCode.D;
        keys.MoveRight.secondary = KeyCode.RightArrow;

        keys.MoveRight.prInputName = "D";
        keys.MoveRight.secInputName = "Right";

        keys._allKeys.Add(keys.MoveRight);

        //

        keys.MoveForward.primary = KeyCode.W;
        keys.MoveForward.secondary = KeyCode.UpArrow;

        keys.MoveForward.prInputName = "W";
        keys.MoveForward.secInputName = "Up";

        keys._allKeys.Add(keys.MoveForward);

        //

        keys.MoveBackward.primary = KeyCode.S;
        keys.MoveBackward.secondary = KeyCode.DownArrow;

        keys.MoveBackward.prInputName = "S";
        keys.MoveBackward.secInputName = "Down";

        keys._allKeys.Add(keys.MoveBackward);

        //

        keys.Jump.primary = KeyCode.Space;
        keys.Jump.secondary = KeyCode.RightShift;

        keys.Jump.prInputName = "Space";
        keys.Jump.secInputName = "RightShift";

        keys._allKeys.Add(keys.Jump);

        //

        keys.Sprint_SnipeSteady.primary = KeyCode.LeftShift;
        keys.Sprint_SnipeSteady.secondary = KeyCode.Keypad0;

        keys.Sprint_SnipeSteady.prInputName = "LeftShift";
        keys.Sprint_SnipeSteady.secInputName = "Num0";

        keys._allKeys.Add(keys.Sprint_SnipeSteady);

        //

        keys.Crouch.primary = KeyCode.LeftControl;
        keys.Crouch.secondary = KeyCode.C;

        keys.Crouch.prInputName = "LeftControl";
        keys.Crouch.secInputName = "C";

        keys._allKeys.Add(keys.Crouch);

        //

        keys.Action.primary = KeyCode.F;
        keys.Action.secondary = KeyCode.Return;

        keys.Action.prInputName = "F";
        keys.Action.secInputName = "Return";

        keys._allKeys.Add(keys.Action);

        //

        keys.Fire.primary = KeyCode.Mouse0;
        keys.Fire.secondary = KeyCode.None;

        keys.Fire.prInputName = "MouseLeft";
        keys.Fire.secInputName = "";

        keys._allKeys.Add(keys.Fire);

        //

        keys.Aim.primary = KeyCode.Mouse1;
        keys.Aim.secondary = KeyCode.None;

        keys.Aim.prInputName = "MouseRight";
        keys.Aim.secInputName = "";

        keys._allKeys.Add(keys.Aim);

        //

        keys.ChangeGun.primary = KeyCode.Alpha1;
        keys.ChangeGun.secondary = KeyCode.Alpha2;

        keys.ChangeGun.prInputName = "Alpha1";
        keys.ChangeGun.secInputName = "Alpha2";

        keys._allKeys.Add(keys.ChangeGun);

        //

        keys.Grenade_SnipeTimeController.primary = KeyCode.Mouse2;
        keys.Grenade_SnipeTimeController.secondary = KeyCode.G;

        keys.Grenade_SnipeTimeController.prInputName = "MouseMid";
        keys.Grenade_SnipeTimeController.secInputName = "G";

        keys._allKeys.Add(keys.Grenade_SnipeTimeController);

        //

        keys.Melee.primary = KeyCode.E;
        keys.Melee.secondary = KeyCode.Keypad1;

        keys.Melee.prInputName = "E";
        keys.Melee.secInputName = "Num1";

        keys._allKeys.Add(keys.Melee);

        //

        keys.Reload.primary = KeyCode.R;
        keys.Reload.secondary = KeyCode.KeypadPeriod;

        keys.Reload.prInputName = "R";
        keys.Reload.secInputName = "NumPeriod";

        keys._allKeys.Add(keys.Reload);

        //

        keys.Missions.primary = KeyCode.Tab;
        keys.Missions.secondary = KeyCode.Delete;

        keys.Missions.prInputName = "Tab";
        keys.Missions.secInputName = "Delete";

        keys._allKeys.Add(keys.Missions);

        //

        sensitivityX = defaultSensitivityX;
        sensitivityY = defaultSensitivityY;

        invertMouse = false;

        useMouseWheelToChangeWeapon = true;
    }

    //

    public static bool KeyIfGameIsNotPaused_MoveLeft()
    {
        return GetKeyInfo_IfGameIsNotPaused(keys.MoveLeft);
    }

    public static bool KeyIfGameIsNotPaused_MoveRight()
    {
        return GetKeyInfo_IfGameIsNotPaused(keys.MoveRight);
    }

    public static bool KeyIfGameIsNotPaused_MoveForward()
    {
        return GetKeyInfo_IfGameIsNotPaused(keys.MoveForward);
    }

    public static bool KeyIfGameIsNotPaused_MoveBackward()
    {
        return GetKeyInfo_IfGameIsNotPaused(keys.MoveBackward);
    }

    public static bool KeyIfGameIsNotPaused_HorizontalMovement()
    {
        return KeyIfGameIsNotPaused_MoveLeft() || KeyIfGameIsNotPaused_MoveRight();
    }

    public static bool KeyIfGameIsNotPaused_VerticalMovement()
    {
        return KeyIfGameIsNotPaused_MoveForward() || KeyIfGameIsNotPaused_MoveBackward();
    }

    public static float GetHorizontalMovementAxis()
    {
        float sum = GetAxis(keys.MoveRight.prInputName) + GetAxis(keys.MoveRight.secInputName) - GetAxis(keys.MoveLeft.prInputName) - GetAxis(keys.MoveLeft.secInputName);
        return Mathf.Clamp(sum, -1f, 1f);
    }

    public static float GetVerticalMovementAxis()
    {
        float sum = GetAxis(keys.MoveForward.prInputName) + GetAxis(keys.MoveForward.secInputName) - GetAxis(keys.MoveBackward.prInputName) - GetAxis(keys.MoveBackward.secInputName);
        return Mathf.Clamp(sum, -1f, 1f);
    }



    public static bool KeyDownIfGameIsNotPaused_Jump()
    {
        return GetKeyInfoDown_IfGameIsNotPaused(keys.Jump);
    }

    public static bool KeyIfGameIsNotPaused_Sprint()
    {
        return GetKeyInfo_IfGameIsNotPaused(keys.Sprint_SnipeSteady);
    }

    public static bool KeyDownIfGameIsNotPaused_Crouch()
    {
        return GetKeyInfoDown_IfGameIsNotPaused(keys.Crouch);
    }

    public static bool KeyDownIfGameIsNotPaused_Action()
    {
        return GetKeyInfoDown_IfGameIsNotPaused(keys.Action);
    }

    public static bool KeyDownIfGameIsNotPaused_Fire()
    {
        return GetKeyInfoDown_IfGameIsNotPaused(keys.Fire);
    }

    public static bool KeyIfGameIsNotPaused_Fire()
    {
        return GetKeyInfo_IfGameIsNotPaused(keys.Fire);
    }

    public static bool KeyDownIfGameIsNotPaused_Aim()
    {
        return GetKeyInfoDown_IfGameIsNotPaused(keys.Aim);
    }

    public static bool KeyDownIfGameIsNotPaused_ChangeGun()
    {
        if (useMouseWheelToChangeWeapon)
            return GetKeyInfoDown_IfGameIsNotPaused(keys.ChangeGun) || GetMouse_ScrollUp() || GetMouse_ScrollDown();
        else
            return GetKeyInfoDown_IfGameIsNotPaused(keys.ChangeGun);
    }

    public static bool KeyDownIfGameIsNotPaused_Grenade()
    {
        return GetKeyInfoDown_IfGameIsNotPaused(keys.Grenade_SnipeTimeController);
    }

    public static bool KeyIfGameIsNotPaused_Grenade()
    {
        return GetKeyInfo_IfGameIsNotPaused(keys.Grenade_SnipeTimeController);
    }

    public static bool KeyDownIfGameIsNotPaused_Melee()
    {
        return GetKeyInfoDown_IfGameIsNotPaused(keys.Melee);
    }

    public static bool KeyDownIfGameIsNotPaused_Reload()
    {
        return GetKeyInfoDown_IfGameIsNotPaused(keys.Reload);
    }

    public static bool KeyIfGameIsNotPaused_Missions()
    {
        return GetKeyInfo_IfGameIsNotPaused(keys.Missions);
    }

    public static bool KeyDownIfGameIsNotPaused_SnipeTimeControl()
    {
        return GetKeyInfoDown_IfGameIsNotPaused(keys.Grenade_SnipeTimeController);
    }

    public static bool KeyIfGameIsNotPaused_SnipeSteady()
    {
        return GetKeyInfo_IfGameIsNotPaused(keys.Sprint_SnipeSteady);
    }

    public static bool KeyDownIfGameIsNotPaused_Escape()
    {
        return GetKeyDown_IfGameIsNotPaused(KeyCode.Escape);
    }

    public static bool KeyDownIfGameIsNotPaused_FU()
    {
        return GetKeyDown_IfGameIsNotPaused(KeyCode.Backspace);
    }

    //

    public static bool KeyDown_SkipHints()
    {
        return GetKeyDown(KeyCode.Return);
    }


    public static bool KeyDown_Escape()
    {
        return GetKeyDown(KeyCode.Escape);
    }

    public static bool KeyDown_Enter()
    {
        return GetKeyDown(KeyCode.Return);
    }

    public static bool Key_Escape()
    {
        return GetKey(KeyCode.Escape);
    }

    public static bool Key_Enter()
    {
        return GetKey(KeyCode.Return);
    }

    public static bool KeyUp_Escape()
    {
        return GetKeyUp(KeyCode.Escape);
    }

    public static bool KeyUp_Enter()
    {
        return GetKeyUp(KeyCode.Return);
    }

    //

    static bool GetKeyInfoDown_IfGameIsNotPaused(GameKeyInfo _keyInfo)
    {
        GameKeyInfo keyInfo = _keyInfo;

        return GetKeyDown_IfGameIsNotPaused(keyInfo.primary) || GetKeyDown_IfGameIsNotPaused(keyInfo.secondary);
    }

    static bool GetKeyInfo_IfGameIsNotPaused(GameKeyInfo _keyInfo)
    {
        GameKeyInfo keyInfo = _keyInfo;

        return GetKey_IfGameIsNotPaused(keyInfo.primary) || GetKey_IfGameIsNotPaused(keyInfo.secondary);
    }


    static bool GetKeyInfo_Button_Down_IfGameIsNotPaused(GameKeyInfo _keyInfo)
    {
        GameKeyInfo keyInfo = _keyInfo;

        if (!string.IsNullOrEmpty(keyInfo.prInputName))
            if (GetButtonDown_IfGameIsNotPaused(keyInfo.prInputName))
                return true;

        if (!string.IsNullOrEmpty(keyInfo.secInputName))
            if (GetButtonDown_IfGameIsNotPaused(keyInfo.secInputName))
                return true;


        return false;
    }

    static bool GetKeyInfo_Button_IfGameIsNotPaused(GameKeyInfo _keyInfo)
    {
        GameKeyInfo keyInfo = _keyInfo;

        if (!string.IsNullOrEmpty(keyInfo.prInputName))
            if (GetButton_IfGameIsNotPaused(keyInfo.prInputName))
                return true;

        if (!string.IsNullOrEmpty(keyInfo.secInputName))
            if (GetButton_IfGameIsNotPaused(keyInfo.secInputName))
                return true;

        return false;
    }

    //

    public static bool GetKey(KeyCode _keyCode)
    {
        return Input.GetKey(_keyCode);
    }

    public static bool GetKeyDown(KeyCode _keyCode)
    {
        return Input.GetKeyDown(_keyCode);
    }

    public static bool GetKeyUp(KeyCode _keyCode)
    {
        return Input.GetKeyUp(_keyCode);
    }

    public static bool GetKey_IfGameIsNotPaused(KeyCode _keyCode)
    {
        if (GameController.isGamePaused)
            return false;

        return Input.GetKey(_keyCode);
    }

    public static bool GetKeyDown_IfGameIsNotPaused(KeyCode _keyCode)
    {
        if (GameController.isGamePaused)
            return false;

        return Input.GetKeyDown(_keyCode);
    }

    public static bool GetKeyUp_IfGameIsNotPaused(KeyCode _keyCode)
    {
        if (GameController.isGamePaused)
            return false;

        return Input.GetKeyUp(_keyCode);
    }


    public static bool GetButton(string _btnName)
    {
        return Input.GetButton(_btnName);
    }

    public static bool GetButtonDown(string _btnName)
    {
        return Input.GetButtonDown(_btnName);
    }

    public static bool GetButtonUp(string _btnName)
    {
        return Input.GetButtonUp(_btnName);
    }

    public static bool GetButton_IfGameIsNotPaused(string _btnName)
    {
        if (GameController.isGamePaused)
            return false;

        return Input.GetButton(_btnName);
    }

    public static bool GetButtonDown_IfGameIsNotPaused(string _btnName)
    {
        if (GameController.isGamePaused)
            return false;

        return Input.GetButtonDown(_btnName);
    }

    public static bool GetButtonUp_IfGameIsNotPaused(string _btnName)
    {
        if (GameController.isGamePaused)
            return false;

        return Input.GetButtonUp(_btnName);
    }


    public static float GetAxis(string _btnName)
    {
        string btnName = _btnName;

        return Input.GetAxis(btnName);
    }

    //

    public static bool IsAnyKeyDown()
    {
        return Input.anyKeyDown;
    }

    public static void ResetInputAxes()
    {
        Input.ResetInputAxes();
    }

    //

    public static float GetMouseAxisX()
    {
        return Input.GetAxis("Mouse X");
    }

    public static float GetMouseAxisY()
    {
        return Input.GetAxis("Mouse Y");
    }

    public static float GetPlayerCamAxisX()
    {
        return GetMouseAxisX() * sensitivityX;
    }

    public static float GetPlayerCamAxisY()
    {
        float coef = 1;

        if (invertMouse)
            coef = -1;

        return GetMouseAxisY() * sensitivityY * coef;
    }

    //

    public static float GetMouseX()
    {
        return Input.mousePosition.x;
    }

    public static float GetMouseY()
    {
        return Input.mousePosition.y;
    }

    //

    public static bool GetMouse_LeftButton_Up()
    {
        return Input.GetMouseButtonUp(0);
    }

    public static bool GetMouse_LeftButton_Down()
    {
        return Input.GetMouseButtonDown(0);
    }

    public static bool GetMouse_LeftButton()
    {
        return Input.GetMouseButton(0);
    }

    public static bool GetMouse_RightButton_Up()
    {
        return Input.GetMouseButtonUp(1);
    }

    public static bool GetMouse_RightButton_Down()
    {
        return Input.GetMouseButtonDown(1);
    }

    public static bool GetMouse_RightButton()
    {
        return Input.GetMouseButton(1);
    }

    public static bool GetMouse_MidButton_Up()
    {
        return Input.GetMouseButtonUp(2);
    }

    public static bool GetMouse_MidButton_Down()
    {
        return Input.GetMouseButtonDown(2);
    }

    public static bool GetMouse_MidButton()
    {
        return Input.GetMouseButton(2);
    }

    public static bool GetMouse_ScrollUp()
    {
        return Input.GetAxis("Mouse ScrollWheel") > 0;
    }

    public static bool GetMouse_ScrollDown()
    {
        return Input.GetAxis("Mouse ScrollWheel") < 0;
    }

    //

    public static KeyCode GetValidDownKeyCode()
    {
        for (int i = 0; i < validKeys.Count; i++)
        {
            if (GetKeyDown(validKeys[i]))
                return validKeys[i];
        }

        return KeyCode.None;
    }

    public static void AssignKeyToKeyInfo(GameKeyInfo _keyInfo, KeyCode _key, bool _isPrimary)
    {
        GameKeyInfo keyInfo = _keyInfo;
        KeyCode key = _key;
        bool isPrimary = _isPrimary;

        if (key == KeyCode.None)
        {
            if (isPrimary)
            {
                keyInfo.primary = KeyCode.None;
                keyInfo.prInputName = "";
            }
            else
            {
                keyInfo.secondary = KeyCode.None;
                keyInfo.secInputName = "";
            }

            return;
        }

        bool isKeyUsedBefore = false;

        for (int i = 0; i < keys._allKeys.Count; i++)
        {
            if (keys._allKeys[i].primary == key)
            {
                keys._allKeys[i].primary = KeyCode.None;
                keys._allKeys[i].prInputName = "";
                isKeyUsedBefore = true;
            }

            if (keys._allKeys[i].secondary == key)
            {
                keys._allKeys[i].secondary = KeyCode.None;
                keys._allKeys[i].secInputName = "";
                isKeyUsedBefore = true;
            }
        }

        if (isPrimary)
        {
            keyInfo.primary = key;
            keyInfo.prInputName = GetKeyButtonNameByItsKeyCode(key);
        }
        else
        {
            keyInfo.secondary = key;
            keyInfo.secInputName = GetKeyButtonNameByItsKeyCode(key);
        }
    }

    public static string GetKeyButtonNameByItsKeyCode(KeyCode _keyCode)
    {
        KeyCode keyCode = _keyCode;

        switch (keyCode)
        {
            case KeyCode.A:
                return "A";

            case KeyCode.B:
                return "B";

            case KeyCode.C:
                return "C";

            case KeyCode.D:
                return "D";

            case KeyCode.E:
                return "E";

            case KeyCode.F:
                return "F";

            case KeyCode.G:
                return "G";

            case KeyCode.H:
                return "H";

            case KeyCode.I:
                return "I";

            case KeyCode.J:
                return "J";

            case KeyCode.K:
                return "K";

            case KeyCode.L:
                return "L";

            case KeyCode.M:
                return "M";

            case KeyCode.N:
                return "N";

            case KeyCode.O:
                return "O";

            case KeyCode.P:
                return "P";

            case KeyCode.Q:
                return "Q";

            case KeyCode.R:
                return "R";

            case KeyCode.S:
                return "S";

            case KeyCode.T:
                return "T";

            case KeyCode.U:
                return "U";

            case KeyCode.V:
                return "V";

            case KeyCode.W:
                return "W";

            case KeyCode.X:
                return "X";

            case KeyCode.Y:
                return "Y";

            case KeyCode.Z:
                return "Z";



            case KeyCode.Alpha0:
                return "Alpha0";

            case KeyCode.Alpha1:
                return "Alpha1";

            case KeyCode.Alpha2:
                return "Alpha2";

            case KeyCode.Alpha3:
                return "Alpha3";

            case KeyCode.Alpha4:
                return "Alpha4";

            case KeyCode.Alpha5:
                return "Alpha5";

            case KeyCode.Alpha6:
                return "Alpha6";

            case KeyCode.Alpha7:
                return "Alpha7";

            case KeyCode.Alpha8:
                return "Alpha8";

            case KeyCode.Alpha9:
                return "Alpha9";


            case KeyCode.F1:
                return "F1";

            case KeyCode.F2:
                return "F2";

            case KeyCode.F3:
                return "F3";

            case KeyCode.F4:
                return "F4";

            case KeyCode.F5:
                return "F5";

            case KeyCode.F6:
                return "F6";

            case KeyCode.F7:
                return "F7";

            case KeyCode.F8:
                return "F8";

            case KeyCode.F9:
                return "F9";

            case KeyCode.F10:
                return "F10";

            case KeyCode.F11:
                return "F11";

            case KeyCode.F12:
                return "F12";

            case KeyCode.F13:
                return "F13";

            case KeyCode.F14:
                return "F14";

            case KeyCode.F15:
                return "F15";



            case KeyCode.Keypad0:
                return "Num0";

            case KeyCode.Keypad1:
                return "Num1";

            case KeyCode.Keypad2:
                return "Num2";

            case KeyCode.Keypad3:
                return "Num3";

            case KeyCode.Keypad4:
                return "Num4";

            case KeyCode.Keypad5:
                return "Num5";

            case KeyCode.Keypad6:
                return "Num6";

            case KeyCode.Keypad7:
                return "Num7";

            case KeyCode.Keypad8:
                return "Num8";

            case KeyCode.Keypad9:
                return "Num9";



            case KeyCode.KeypadEquals:
                return "NumEquals";

            case KeyCode.KeypadPeriod:
                return "NumPeriod";

            case KeyCode.KeypadEnter:
                return "NumEnter";

            case KeyCode.KeypadPlus:
                return "NumPlus";

            case KeyCode.KeypadMinus:
                return "NumMinus";

            case KeyCode.KeypadMultiply:
                return "NumStar";

            case KeyCode.KeypadDivide:
                return "NumSlash";



            case KeyCode.Numlock:
                return "NumLock";

            case KeyCode.CapsLock:
                return "CapsLock";

            case KeyCode.ScrollLock:
                return "ScrollLock";



            case KeyCode.Print:
                return "PrintScr";

            case KeyCode.SysReq:
                return "SysRq";

            case KeyCode.Pause:
                return "Pause";

            case KeyCode.Break:
                return "Break";

            case KeyCode.Insert:
                return "Insert";

            case KeyCode.Delete:
                return "Delete";

            case KeyCode.Home:
                return "Home";

            case KeyCode.End:
                return "End";

            case KeyCode.PageUp:
                return "PageUp";

            case KeyCode.PageDown:
                return "PageDown";



            case KeyCode.BackQuote:
                return "BackQuote";

            case KeyCode.Minus:
                return "Minus";

            case KeyCode.Equals:
                return "Equals";

            case KeyCode.Exclaim:
                return "Exclaim";

            case KeyCode.At:
                return "At";

            case KeyCode.Hash:
                return "Hash";

            case KeyCode.Dollar:
                return "Dollar";

            case KeyCode.Caret:
                return "Caret";

            case KeyCode.Ampersand:
                return "And";

            case KeyCode.Asterisk:
                return "Star";

            case KeyCode.LeftParen:
                return "Parenthesis";

            case KeyCode.RightParen:
                return "RightParenthesis";

            case KeyCode.Underscore:
                return "Underscore";

            case KeyCode.Plus:
                return "Plus";

            case KeyCode.LeftBracket:
                return "LeftBracket";

            case KeyCode.RightBracket:
                return "RightBracket";

            case KeyCode.Backslash:
                return "BackSlash";

            case KeyCode.Semicolon:
                return "SemiColon";

            case KeyCode.Colon:
                return "Colon";

            case KeyCode.Quote:
                return "Quote";

            case KeyCode.DoubleQuote:
                return "DoubleQuote";

            case KeyCode.Comma:
                return "Comma";

            case KeyCode.Period:
                return "Period";

            case KeyCode.Slash:
                return "Slash";

            case KeyCode.Less:
                return "Less";

            case KeyCode.Greater:
                return "Greater";

            case KeyCode.Question:
                return "Question";



            case KeyCode.Return:
                return "Return";

            case KeyCode.Tab:
                return "Tab";

            case KeyCode.Backspace:
                return "Backspace";

            case KeyCode.LeftShift:
                return "LeftShift";

            case KeyCode.LeftControl:
                return "LeftControl";

            case KeyCode.LeftAlt:
                return "LeftAlt";

            case KeyCode.RightShift:
                return "RightShift";

            case KeyCode.RightControl:
                return "RightControl";

            case KeyCode.RightAlt:
                return "RightAlt";



            case KeyCode.UpArrow:
                return "Up";

            case KeyCode.DownArrow:
                return "Down";

            case KeyCode.LeftArrow:
                return "Left";

            case KeyCode.RightArrow:
                return "Right";



            case KeyCode.Space:
                return "Space";

            case KeyCode.Mouse0:
                return "MouseLeft";

            case KeyCode.Mouse1:
                return "MouseRight";

            case KeyCode.Mouse2:
                return "MouseMid";



            case KeyCode.AltGr:
                return "AltGr";

            case KeyCode.Clear:
                return "Clear";

            case KeyCode.Help:
                return "Help";
        }

        return "";
    }
}
