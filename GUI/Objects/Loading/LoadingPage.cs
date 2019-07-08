using UnityEngine;
using System.Collections;

public class LoadingPage : MonoBehaviour
{

    public GameObject storyPage;
    public GameObject storyPageText;
    public GameObject loadingPage;
    public GameObject clickButtonPage;
    public GameObject loadingIcon;

    public AudioInfo storyAudInf;

    public AudioInfo loadingMusic;

    public int loadLevelIndex = 0;

    public bool shouldLoadImmediately = false;

    bool waitingForKey = true;

    float keyCheckCounter = 0.4f;

    float storyPageTextOriginalX;

    float scWbbHIn16b9 = 1.7777f;

    float xDiffIn16b9And5b4 = -10.4794f;

    // Use this for initialization
    void Awake()
    {
        GameController.ResetSettingsForNewScene(true);

        storyPageTextOriginalX = storyPageText.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        keyCheckCounter = MathfPlus.DecByDeltatimeToZero(keyCheckCounter);

        if (keyCheckCounter == 0)
        {
            if (waitingForKey)
            {
                if (CustomInputManager.IsAnyKeyDown() || shouldLoadImmediately)
                {
                    StartLoadingLevel();
                }
            }
        }

        float scWbbH = ((float)(Screen.width)) / ((float)(Screen.height));

        //print(scWbbH);

        if (scWbbH < scWbbHIn16b9)
        {
            float moreX = (scWbbHIn16b9 - scWbbH) * xDiffIn16b9And5b4;

            storyPageText.transform.position = new Vector3(storyPageTextOriginalX + moreX, storyPageText.transform.position.y, storyPageText.transform.position.z);
        }
    }

    IEnumerator loadLevel(int loadLevelIndex)
    {
        AsyncOperation aSync = Application.LoadLevelAsync(loadLevelIndex);
        yield return aSync;
    }

    void StartLoadingLevel()
    {
        waitingForKey = false;

        storyPage.active = false;
        loadingPage.active = true;
        clickButtonPage.active = false;
        loadingIcon.active = true;
        storyPageText.active = false;

        storyAudInf.Stop();

        //loadingMusic.Play();

        StartCoroutine(loadLevel(loadLevelIndex));
    }
}
