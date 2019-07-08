using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour
{

    public float moveUpSpeed = 1;
    public float maxY = 40.596f;

    public float timeToEndCredits = 152f;
    public float audioFadeSpeed = 1;
    public AudioInfo musicAudioInfo;

    float delayTimeToCheckEscapeKey = 0.5f;

    bool isEndingSceneByEscapeKey = false;

    // Use this for initialization
    void Start()
    {
        Screen.lockCursor = true;

        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < maxY)
            transform.Translate(new Vector3(0, moveUpSpeed * Time.deltaTime, 0));
        else
            transform.position = new Vector3(transform.position.x, maxY, transform.position.z);


        timeToEndCredits = MathfPlus.DecByDeltatimeToZero(timeToEndCredits);
        delayTimeToCheckEscapeKey = MathfPlus.DecByDeltatimeToZero(delayTimeToCheckEscapeKey);

        if (timeToEndCredits == 0)
        {
            GameController.LoadMainMenu();
        }

        if (!isEndingSceneByEscapeKey)
        {
            if (delayTimeToCheckEscapeKey == 0 && CustomInputManager.KeyDown_Escape()) //GameController.GetKeyDown(KeyCode.Escape))
            {
                isEndingSceneByEscapeKey = true;
            }
        }

        if (isEndingSceneByEscapeKey)
        {
            musicAudioInfo.SetCustomVolume(musicAudioInfo.customVolume - Time.deltaTime * audioFadeSpeed);

            if(musicAudioInfo.customVolume == 0)
                GameController.LoadMainMenu();
        }
    }
}
