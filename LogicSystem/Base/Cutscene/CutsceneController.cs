using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum CutsceneStatus
{
    NotStarted,
    Running,
    Finished,
}

public class CutsceneController : MonoBehaviour
{
    //public float fadingSpeed = 1;
    //public float fadeInStartDelayTime = 0.5f;
    //public float fadeOutEndDelayTime = 0.36f;

    public float duration;

    public bool startWithBlackScreen = true;
    public bool endWithBlackScreen = true;

    public bool endCutsceneWithSoundFade = true;

    public GameObject[] characters;
    public AnimInfo[] charactersInitialAnimInfo;
    public Transform[] charactersInitialTr;

    public Camera cam;
    public CutsceneCameraController[] camControllers;
    public CutsceneCamSequence[] camSequences;
    public CutsceneTimeline[] timelines;

    public CutsceneAudioInfoController[] audioInfoPacks;

    public CutsceneSoldierActInfo[] soldierActInfos;

    public bool useCustomTimeToStartEndScreedFading = false;
    public float startEndScreenFadingAt = 0;

    public GameObject[] demJumpAnimChars;

    public GameObject cutsceneScriptOwner;

    public string ___OnePackMode_______________________ = "_________________________________________";
    public bool useOnePackMode = false;
    public GameObject datPack;
    public bool onePackMode_StartVisualsAfterDelay = false;
    public float onePackMode_delayToStartVisuals = 0;

    public ManualOccluderTrigger occluderTrigger;

    // /////////////

    //[HideInInspector]
    //public bool doCameras = false;
    [HideInInspector]
    public int currentCamSequenceIndex = 0;
    //[HideInInspector]
    //public float currentCamTimeCounter = 0;
    [HideInInspector]
    public CutsceneCameraController activeCamController;

    //bool skipThisFrameDeltaTimeForCamera = true;

    [HideInInspector]
    public float step = 0;

    [HideInInspector]
    public MapLogic mapLogic;

    [HideInInspector]
    public CutsceneStatus status = CutsceneStatus.NotStarted;

    float skipDelayCounter = 1;

    bool canSkipNow = false;
    //bool skipCalled = false;

    bool isEnding = false;

    AudioInfo[] childAudioInfos;

    List<SkinnedMeshRenderer[]> charactersSkinnedMeshRederers = new List<SkinnedMeshRenderer[]>();

    List<MeshRenderer[]> charactersMeshRederers = new List<MeshRenderer[]>();

    float audInfsDecreasingVolTime = 0.5f;

    [HideInInspector]
    public float durationTimeCounter = 0;

    bool isEndFadingStartedBefore = false;

    float onePackMode_delayToStartVisualsTimeCounter;
    bool onePackMode_AreVisualsStarted = false;

    // Use this for initialization
    void Start()
    {
        mapLogic = MapLogic.Instance;
        childAudioInfos = GetComponentsInChildren<AudioInfo>();

        if (!useOnePackMode)
            InitCharactersSkinnedMeshRenderers();

        foreach (CutsceneCameraController ccc in camControllers)
        {
            ccc.parentCutsceneController = this;
        }

        foreach (CutsceneTimeline ct in timelines)
        {
            ct.parentCutsceneController = this;
        }

        HideCharacters();
    }

    // Update is called once per frame
    void Update()
    {
        if (status == CutsceneStatus.Running)
        {
            UpdateMe();
        }
    }

    public virtual void StartIt()
    {
        step = 1;
        mapLogic.StopActiveLogics();
        mapLogic.isCutsceneMode = true;
        status = CutsceneStatus.Running;

        if (cutsceneScriptOwner != null)
            cutsceneScriptOwner.active = true;

        if (!useOnePackMode || (useOnePackMode && !onePackMode_StartVisualsAfterDelay))
            if (startWithBlackScreen)
                mapLogic.blackScreenFader.StartFadingIn();

        durationTimeCounter = duration;

        UnhideCharacters();
        
        if (occluderTrigger != null)
        {
            occluderTrigger.StartForCutscene();
        }

        if (!useOnePackMode)
        {
            SetCharactersInitialTransform();
            StartCharactersInitialAnimation();

            for (int i = 0; i < camControllers.Length; i++)
            {
                camControllers[i].StartIt(cam);
            }

            for (int i = 0; i < timelines.Length; i++)
            {
                timelines[i].StartIt();
            }

            //skipThisFrameDeltaTimeForCamera = true;
            //doCameras = true;

            SetActiveCamSequence(0);

            for (int i = 0; i < soldierActInfos.Length; i++)
            {
                soldierActInfos[i].StartIt();
            }
        }
        else
        {
            if (!onePackMode_StartVisualsAfterDelay)
            {
                onePackMode_AreVisualsStarted = true;
                SetDatPackEnabled();
            }
            else
            {
                onePackMode_delayToStartVisualsTimeCounter = onePackMode_delayToStartVisuals;
                skipDelayCounter += onePackMode_delayToStartVisualsTimeCounter;

                cam.active = true;
            }
        }

        mapLogic.SetMapActiveCamera(cam);
        mapLogic.SetCameraActivation(PlayerCharacterNew.Instance.fpsCamera, true);

        for (int i = 0; i < audioInfoPacks.Length; i++)
        {
            audioInfoPacks[i].StartIt();
        }
    }

    public virtual void UpdateMe()
    {
        if (endWithBlackScreen && useCustomTimeToStartEndScreedFading)
        {
            if (!isEndFadingStartedBefore)
            {
                if (durationTimeCounter <= duration - startEndScreenFadingAt)
                {
                    isEndFadingStartedBefore = true;

                    canSkipNow = false;
                    mapLogic.blackScreenFader.StartFadingOut();
                }
            }
        }

        if (onePackMode_StartVisualsAfterDelay)
        {
            if (!onePackMode_AreVisualsStarted)
            {
                onePackMode_delayToStartVisualsTimeCounter = MathfPlus.DecByDeltatimeToZero(onePackMode_delayToStartVisualsTimeCounter);

                if (onePackMode_delayToStartVisualsTimeCounter == 0)
                {
                    onePackMode_AreVisualsStarted = true;

                    SetDatPackEnabled();

                    if (startWithBlackScreen)
                        mapLogic.blackScreenFader.StartFadingIn();
                }
            }
        }

        //if (doCameras)
        //{
        //if (skipThisFrameDeltaTimeForCamera)
        //{
        //    skipThisFrameDeltaTimeForCamera = false;
        //}
        //else
        //{
        //    currentCamTimeCounter = MathfPlus.DecByDeltatimeToZero(currentCamTimeCounter);
        //}

        //if (currentCamTimeCounter == 0)
        //{
        //currentCamSequenceIndex++;

        //if (currentCamSequenceIndex >= camSequences.Length)
        //{
        //    currentCamSequenceIndex--;
        //    //doCameras = false;
        //}
        //else
        //{
        //    SetActiveCamSequence(currentCamSequenceIndex);
        //}
        //}
        //}

        if (!isEnding)
        {
            durationTimeCounter = MathfPlus.DecByDeltatimeToZero(durationTimeCounter);

            if (durationTimeCounter == 0)
                StartEnding(false);
        }

        if (!isEnding)
        {
            skipDelayCounter = MathfPlus.DecByDeltatimeToZero(skipDelayCounter);

            if (skipDelayCounter == 0)
            {
                canSkipNow = true;
            }

            //if (canSkipNow && !skipCalled)
            //{
            //    if (GameController.GetKey_IfGameIsNotPaused(KeyCode.Return))
            //    {
            //        skipCalled = true;

            //        //StartEnding(true);
            //    }
            //}
        }

        if (step == 998)
        {
            if (!isEndFadingStartedBefore)
            {
                isEndFadingStartedBefore = true;
                mapLogic.blackScreenFader.StartFadingOut();
            }

            if (endCutsceneWithSoundFade)
            {
                if (childAudioInfos != null)
                {
                    for (int i = 0; i < childAudioInfos.Length; i++)
                    {
                        childAudioInfos[i].StartDecreasingCustomVolumeToEnd(audInfsDecreasingVolTime);
                    }
                }
            }

            SetStep(999);
        }

        if (step == 999)
        {
            if (mapLogic.blackScreenFader.isFadingFinished)
            {
                EndMe();
            }
        }
    }

    public virtual void EndMe()
    {
        step = 10000;
        mapLogic.ActiveOnlyPlayerCameras();
        mapLogic.RestartActiveLogics();
        mapLogic.isCutsceneMode = false;
        status = CutsceneStatus.Finished;

        if (cutsceneScriptOwner != null)
            cutsceneScriptOwner.active = false;

        if (childAudioInfos != null)
        {
            for (int i = 0; i < childAudioInfos.Length; i++)
            {
                childAudioInfos[i].SetCustomVolume(0);
            }
        }

        HideCharacters();

        if (occluderTrigger != null)
        {
            occluderTrigger.EndForCutscene();
        }

        if (useOnePackMode)
        {
            SetDatPackDisabled();
        }

        for (int i = 0; i < soldierActInfos.Length; i++)
        {
            soldierActInfos[i].StopIt();
        }
    }

    public void SetStep(float _step)
    {
        step = _step;
    }

    public void StartEnding(bool _isSkipCalled)
    {
        isEnding = true;

        if (_isSkipCalled)
            endCutsceneWithSoundFade = true;

        if (!endWithBlackScreen)
            EndMe();
        else
            SetStep(998);
    }

    void OnGUI()
    {
        //if (status == CutsceneStatus.Running && !isEnding)
        //    if (canSkipNow && !skipCalled)
        //        GUI.TextArea(new Rect(100, 100, 400, 50), "Press Enter to skip cutscene.");
    }

    void InitCharactersSkinnedMeshRenderers()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            SkinnedMeshRenderer[] smrs = characters[i].GetComponentsInChildren<SkinnedMeshRenderer>();

            charactersSkinnedMeshRederers.Add(smrs);


            MeshRenderer[] mrs = characters[i].GetComponentsInChildren<MeshRenderer>();

            charactersMeshRederers.Add(mrs);
        }

        for (int i = 0; i < soldierActInfos.Length; i++)
        {
            SkinnedMeshRenderer[] smrs = soldierActInfos[i].fakeSoldier.GetComponentsInChildren<SkinnedMeshRenderer>();

            charactersSkinnedMeshRederers.Add(smrs);


            MeshRenderer[] mrs = soldierActInfos[i].fakeSoldier.GetComponentsInChildren<MeshRenderer>();

            charactersMeshRederers.Add(mrs);
        }
    }

    public void UnhideCharacters()
    {
        for (int i = 0; i < charactersSkinnedMeshRederers.Count; i++)
        {
            if (charactersSkinnedMeshRederers[i] != null)
            {
                SkinnedMeshRenderer[] smrs = charactersSkinnedMeshRederers[i];

                for (int j = 0; j < smrs.Length; j++)
                {
                    if (smrs[j] != null)
                    {
                        smrs[j].enabled = true;
                    }
                }
            }
        }

        for (int i = 0; i < charactersMeshRederers.Count; i++)
        {
            if (charactersMeshRederers[i] != null)
            {
                MeshRenderer[] mrs = charactersMeshRederers[i];

                for (int j = 0; j < mrs.Length; j++)
                {
                    if (mrs[j] != null)
                    {
                        mrs[j].enabled = true;
                    }
                }
            }
        }
    }
    public void HideCharacters()
    {
        for (int i = 0; i < charactersSkinnedMeshRederers.Count; i++)
        {
            if (charactersSkinnedMeshRederers[i] != null)
            {
                SkinnedMeshRenderer[] smrs = charactersSkinnedMeshRederers[i];

                for (int j = 0; j < smrs.Length; j++)
                {
                    if (smrs[j] != null)
                    {
                        smrs[j].enabled = false;
                    }
                }
            }

        }

        for (int i = 0; i < charactersMeshRederers.Count; i++)
        {
            if (charactersMeshRederers[i] != null)
            {
                MeshRenderer[] mrs = charactersMeshRederers[i];

                for (int j = 0; j < mrs.Length; j++)
                {
                    if (mrs[j] != null)
                    {
                        mrs[j].enabled = false;
                    }
                }
            }
        }
    }
    public void SetCharactersInitialTransform()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].transform.position = charactersInitialTr[i].position;
            characters[i].transform.rotation = charactersInitialTr[i].rotation;
        }
    }
    public void StartCharactersInitialAnimation()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].animation.Play(charactersInitialAnimInfo[i].AnimName);
        }
    }

    public void SetActiveCamSequence(int _index)
    {
        int i = _index;

        CutsceneCamSequence ccSeq = camSequences[i];

        if (activeCamController != null)
            activeCamController.SetDeactive();

        activeCamController = ccSeq.camController;

        if (!activeCamController.autoPlayAnimation)
        {
            activeCamController.PlayAnimation();
            activeCamController.fov = activeCamController.initial_FoV_ForNOautoplay;
        }

        activeCamController.SetActive();
        //float time = ccSeq.duration;

        currentCamSequenceIndex = i;
        //currentCamTimeCounter = time;

        if (ccSeq.doJumpAnim)
        {
            foreach (GameObject go in demJumpAnimChars)
            {
                go.animation[go.animation.clip.name].time = ccSeq.jumpingAnimsTime;
                go.animation.Play();
            }
        }
    }

    void SetDatPackEnabled()
    {
        datPack.SetActiveRecursively(true);

        for (int i = 0; i < soldierActInfos.Length; i++)
        {
            soldierActInfos[i].StartIt();
        }
    }

    void SetDatPackDisabled()
    {
        datPack.SetActiveRecursively(false);
    }

    //public bool IsSkipCalledByUser()
    //{
    //    return skipCalled;
    //}

    public void NextSequence()
    {
        if (currentCamSequenceIndex + 1 >= camSequences.Length)
            return;

        SetActiveCamSequence(currentCamSequenceIndex + 1);
    }
}