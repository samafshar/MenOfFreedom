using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameDifficulty
{
    Medium,
    Hard,
    Easy,
}

public enum TimeScaleCoefSource
{
    GameLogic,
    PlayerMissionFail,
    PlayerSnipe,
    IngameMenu,
}

public class TimeScaleCoef
{
    public TimeScaleCoefSource coefSource;
    public float currentValue = 1;
}

public static class GameController
{
    public static float playerInitialHPCoef = 1f;

    public static List<TimeScaleCoef> currentTimeScaleCoefs = new List<TimeScaleCoef>();

    public static int gameLastLevel = 1;

    public static int gameCurrentLevel = 1;

    public static int gameCurrentLevelLastCheckPoint = 0;

    public static MapLogic currentMapLogic = null;

    public static bool isGamePaused = false;

    public static float gameCurrentTimeScale = 1;

    public static GameDifficulty gameCurrentDifficulty = GameDifficulty.Medium;

    public static bool canShowSneakingHints = true;

    public static bool lvlNakhl1_AfterSorCutscene_PlacePlayerInRiver = true;

    public static bool lvlBushehr2_IsShootingHintShownBefore = false;

    public static bool lvlBushehr2_IsSnipeTimeScaleHintShownBefore = false;

    public static bool lvlBushehr2_ShouldShowSnipeTimeScaleHint = false;

    public static bool isMouseMiddleClickUsedInSnipe = false;

    public static bool isGameFirstInitDone = false;

    //Pause

    //public static void PauseGame()
    //{
    //    isGamePaused = true;
    //}

    public static void UnPauseGame(bool _isEscapeKeyUsed)
    {
        if (isGamePaused)
        {
            if (currentMapLogic != null)
                currentMapLogic.UnPauseGame(_isEscapeKeyUsed);
        }
    }

    //Game state

    public static void ResetGameStateToDefault()
    {
        SetGameLastLevel(1);
        SetGameCurrentLevel(1);
        SetGameCurrentLevelLastCheckPoint(0);

    }

    public static void SetGameLastLevel(int _gameLastLevel)
    {
        gameLastLevel = _gameLastLevel;
    }

    public static void SetGameCurrentLevel(int _gameCurrentLevel)
    {
        gameCurrentLevel = _gameCurrentLevel;
    }

    public static void SetGameCurrentLevelLastCheckPoint(int _lastCheckPoint)
    {
        gameCurrentLevelLastCheckPoint = _lastCheckPoint;
    }

    //Time scale

    public static void AddTimeScaleCoef(TimeScaleCoef _timeScaleCoef)
    {
        TimeScaleCoef timeScaleCoef = _timeScaleCoef;

        foreach (TimeScaleCoef tsc in currentTimeScaleCoefs)
        {
            if (tsc.coefSource == timeScaleCoef.coefSource)
                return;
        }

        TimeScaleCoef tmScCf = new TimeScaleCoef();
        tmScCf.coefSource = timeScaleCoef.coefSource;
        tmScCf.currentValue = timeScaleCoef.currentValue;

        currentTimeScaleCoefs.Add(tmScCf);

        UpdateGameTimeScale();
    }

    public static void RemoveTimeScaleCoef(TimeScaleCoef _timeScaleCoef)
    {
        TimeScaleCoef timeScaleCoef = _timeScaleCoef;

        for (int i = 0; i < currentTimeScaleCoefs.Count; i++)
        {
            if (currentTimeScaleCoefs[i].coefSource == timeScaleCoef.coefSource)
            {
                currentTimeScaleCoefs.RemoveAt(i);
                break;
            }
        }

        UpdateGameTimeScale();
    }

    public static void UpdateTimeScaleCoef(TimeScaleCoef _timeScaleCoef)
    {
        TimeScaleCoef timeScaleCoef = _timeScaleCoef;

        foreach (TimeScaleCoef tsc in currentTimeScaleCoefs)
        {
            if (tsc.coefSource == timeScaleCoef.coefSource)
            {
                tsc.currentValue = timeScaleCoef.currentValue;
                break;
            }
        }

        UpdateGameTimeScale();
    }

    public static void ResetGameTimeScale()
    {
        currentTimeScaleCoefs.Clear();

        UpdateGameTimeScale();
    }

    static void UpdateGameTimeScale()
    {
        float finalVal = 1;

        foreach (TimeScaleCoef tsc in currentTimeScaleCoefs)
        {
            finalVal *= tsc.currentValue;
        }

        SetGameCurrentTimeScale(finalVal);
    }

    static void SetGameCurrentTimeScale(float _value)
    {
        gameCurrentTimeScale = Mathf.Clamp(_value, 0.1f, 10f);
        AudioController.SetGeneralPitch(gameCurrentTimeScale);
        Time.timeScale = gameCurrentTimeScale;
    }


    public static void SetGameDifficulty(GameDifficulty _diffi)
    {
        gameCurrentDifficulty = _diffi;

        //...
    }

    public static void ResetSettingsForNewScene(bool _lockCursor)
    {
        isGamePaused = false;
        AudioController.RemoveAllAudioInfos();
        Screen.lockCursor = _lockCursor;
        ResetGameTimeScale();
    }

    //

    //public static bool GetKey(KeyCode _keyCode)
    //{
    //    return Input.GetKey(_keyCode);
    //}

    //public static bool GetKeyDown(KeyCode _keyCode)
    //{
    //    return Input.GetKeyDown(_keyCode);
    //}

    //public static bool GetKeyUp(KeyCode _keyCode)
    //{
    //    return Input.GetKeyUp(_keyCode);
    //}

    //public static bool GetButton(string _btnName)
    //{
    //    return Input.GetButton(_btnName);
    //}

    //public static bool GetButtonDown(string _btnName)
    //{
    //    return Input.GetButtonDown(_btnName);
    //}

    //public static bool GetButtonUp(string _btnName)
    //{
    //    return Input.GetButtonUp(_btnName);
    //}

    //public static bool GetKey_IfGameIsNotPaused(KeyCode _keyCode)
    //{
    //    if (isGamePaused)
    //        return false;

    //    return Input.GetKey(_keyCode);
    //}

    //public static bool GetKeyDown_IfGameIsNotPaused(KeyCode _keyCode)
    //{
    //    if (isGamePaused)
    //        return false;

    //    return Input.GetKeyDown(_keyCode);
    //}

    //public static bool GetKeyUp_IfGameIsNotPaused(KeyCode _keyCode)
    //{
    //    if (isGamePaused)
    //        return false;

    //    return Input.GetKeyUp(_keyCode);
    //}

    //public static bool GetButton_IfGameIsNotPaused(string _btnName)
    //{
    //    if (isGamePaused)
    //        return false;

    //    return Input.GetButton(_btnName);
    //}

    //public static bool GetButtonDown_IfGameIsNotPaused(string _btnName)
    //{
    //    if (isGamePaused)
    //        return false;

    //    return Input.GetButtonDown(_btnName);
    //}

    //public static bool GetButtonUp_IfGameIsNotPaused(string _btnName)
    //{
    //    if (isGamePaused)
    //        return false;

    //    return Input.GetButtonUp(_btnName);
    //}

    //

    public static void LoadLevel(int _lvlNum, int _checkpointNum)
    {
        int lvlNum = _lvlNum;
        int checkpointNum = _checkpointNum;

        SetGameCurrentLevel(lvlNum);
        SetGameCurrentLevelLastCheckPoint(checkpointNum);
        GameSaveLoadController.SaveGameState();

        Application.LoadLevel(GeneralStats.LevelsStartingIndex + gameCurrentLevel);
    }

    public static void LoadCurrentLevelLastCheckpoint()
    {
        Application.LoadLevel(GeneralStats.LevelsStartingIndex + gameCurrentLevel);
    }

    public static void LoadCurrentLevelFromBeginning()
    {
        SetGameCurrentLevelLastCheckPoint(0);
        GameSaveLoadController.SaveGameState();

        Application.LoadLevel(GeneralStats.LevelsStartingIndex + gameCurrentLevel);
    }

    public static void LoadLevelLoadingPage(int _lvlNum, int _lvlLastCheckpointNum)
    {
        int lvlNum = _lvlNum;
        int lvlLastCheckpointNum = _lvlLastCheckpointNum;

        SetGameCurrentLevel(lvlNum);
        SetGameCurrentLevelLastCheckPoint(lvlLastCheckpointNum);

        GameSaveLoadController.SaveGameState();

        Application.LoadLevel(GeneralStats.LoadingPagesStartingIndex + lvlNum);
    }

    public static void LoadMainMenu()
    {
        canShowSneakingHints = true;

        Application.LoadLevel(GeneralStats.mainMenuIndex);
    }

    public static void LoadCredits()
    {
        Application.LoadLevel(GeneralStats.CreditsIndex);
    }

    //

    public static int GetNextLevelNumber()
    {
        int nextLvl = gameCurrentLevel + 1;

        nextLvl = Mathf.Clamp(nextLvl, 1, GeneralStats.numOfLevels);

        return nextLvl;
    }

    public static void SetPlayerInitialHPCoef(float _coef)
    {
        playerInitialHPCoef = _coef;
    }

    //

    public static void GameFirstInit()
    {
        if (!isGameFirstInitDone)
        {
            isGameFirstInitDone = true;

            CustomInputManager.Init();
            AudioController.Init();
            VideoSettingsController.Init();

            GameSaveLoadController.LoadOptions();

            if (!GameSaveLoadController.optionsLoadWasOK)
            {
                GameSaveLoadController.SaveOptions();
            }
        }
    }
}
