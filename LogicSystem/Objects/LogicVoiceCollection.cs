using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum LogVoiceCollectionStep
{
    idle,
    waitingForFinishingVoiceStartDelay,
    startingNewVoice,
    playing,
}

public class LogicVoiceQueueInfo
{
    public string voiceName = "";
    public float delay = 0;
}

public class LogicVoiceCollection : MonoBehaviour
{
    public SoldierInfo ownerSoldInfo;
    public LogicVoiceInfo[] voiceInfos;

    List<LogicVoiceQueueInfo> queue = new List<LogicVoiceQueueInfo>();
    int queueCurIndex = 0;
    bool isNextQueueMemberInited = false;
    float queueTimeCounter = 0;


    int curIndex = 0;
    float curVoiceStartDelayTime = 0;

    LogVoiceCollectionStep step = LogVoiceCollectionStep.idle;

    bool shouldStopCurVoiceAfterItsFinishing = false;

    bool isPlayedRightNow = false;


    void Start()
    {
        for (int i = 0; i < voiceInfos.Length; i++)
        {
            for (int j = 0; j < voiceInfos.Length; j++)
            {
                if (i != j)
                {
                    if (voiceInfos[i].voiceName == voiceInfos[j].voiceName)
                    {
                        Debug.LogError("Two voice infos have same name!");
                    }
                }
            }
        }
    }

    void Update()
    {
        if (isPlayedRightNow)
            isPlayedRightNow = false;

        if (queue.Count > 0 && queueCurIndex < queue.Count)
        {
            if (!isNextQueueMemberInited)
            {
                isNextQueueMemberInited = true;
                queueTimeCounter = queue[queueCurIndex].delay;
            }

            queueTimeCounter = MathfPlus.DecByDeltatimeToZero(queueTimeCounter);

            if (queueTimeCounter == 0)
            {
                StopCurVoiceAfterItsFinishing();

                if (IsCurVoiceFinished())
                {
                    PlayName(queue[queueCurIndex].voiceName);

                    isNextQueueMemberInited = false;
                    queueCurIndex++;
                }
            }
        }

        #region waitingForFinishingVoiceStartDelay
        if (step == LogVoiceCollectionStep.waitingForFinishingVoiceStartDelay)
        {
            if (shouldStopCurVoiceAfterItsFinishing)
            {
                ownerSoldInfo.SetVoiceBusyTime(voiceInfos[curIndex].silenceAfterFinishing, voiceInfos[curIndex].silenceAfterFinishing);
                SetStep(LogVoiceCollectionStep.idle);
            }
            else
            {
                if (curVoiceStartDelayTime > 0)
                {
                    curVoiceStartDelayTime = MathfPlus.DecByDeltatimeToZero(curVoiceStartDelayTime);
                }

                if (curVoiceStartDelayTime == 0)
                {
                    SetStep(LogVoiceCollectionStep.startingNewVoice);
                }
            }
        }
        #endregion

        #region startingNewVoice
        if (step == LogVoiceCollectionStep.startingNewVoice)
        {
            if (shouldStopCurVoiceAfterItsFinishing)
            {
                ownerSoldInfo.SetVoiceBusyTime(voiceInfos[curIndex].silenceAfterFinishing, voiceInfos[curIndex].silenceAfterFinishing);
                SetStep(LogVoiceCollectionStep.idle);
            }
            else
            {
                if (!ownerSoldInfo.IsVoiceOnBusyTimer())
                {
                    ownerSoldInfo.PlayVoiceWithAdditionalBusyTime(voiceInfos[curIndex].audioClips, voiceInfos[curIndex].silenceBetweenRepeats_Min, voiceInfos[curIndex].silenceBetweenRepeats_Max);

                    isPlayedRightNow = true;

                    SetStep(LogVoiceCollectionStep.playing);
                }
            }
        }
        #endregion

        #region playing
        if (step == LogVoiceCollectionStep.playing)
        {
            if (ownerSoldInfo.IsVoiceLengthCountingFinished())
            {
                if (shouldStopCurVoiceAfterItsFinishing || !voiceInfos[curIndex].repeat)
                {
                    ownerSoldInfo.SetVoiceBusyTime(voiceInfos[curIndex].silenceAfterFinishing, voiceInfos[curIndex].silenceAfterFinishing);
                    SetStep(LogVoiceCollectionStep.idle);
                }
                else
                {
                    SetStep(LogVoiceCollectionStep.startingNewVoice);
                }
            }
        }
        #endregion
    }

    public void PlayName(string _name)
    {
        InitAndStartNewVoiceInfo(GetVoiceInfoByName(_name));
    }

    public void PlayIndex(int _index)
    {
        InitAndStartNewVoiceInfo(GetVoiceInfoByIndex(_index));
    }

    public void PlayNext()
    {
        if (curIndex + 1 >= voiceInfos.Length)
        {
            Debug.LogError("No more voice infos exist to play 'next'!");
            return;
        }

        PlayIndex(curIndex + 1);
    }

    public void AddToPlayQueue(string _name, float _delay)
    {
        LogicVoiceQueueInfo newQueueMember = new LogicVoiceQueueInfo();
        newQueueMember.voiceName = _name;
        newQueueMember.delay = _delay;

        queue.Add(newQueueMember);
    }

    public void EmptyQueue()
    {
        queue.Clear();
        queueCurIndex = 0;
        isNextQueueMemberInited = false;
    }

    void InitAndStartNewVoiceInfo(LogicVoiceInfo _voiceInfo)
    {
        LogicVoiceInfo vi = _voiceInfo;

        step = LogVoiceCollectionStep.waitingForFinishingVoiceStartDelay;

        curIndex = GetVoiceInfoIndex(vi);

        curVoiceStartDelayTime = voiceInfos[curIndex].startDelay;

        shouldStopCurVoiceAfterItsFinishing = false;

    }

    public bool IsCurVoiceFinished()
    {
        return step == LogVoiceCollectionStep.idle;
    }

    public void StopCurVoiceAfterItsFinishing()
    {
        shouldStopCurVoiceAfterItsFinishing = true;
    }

    public LogicVoiceInfo GetVoiceInfoByName(string _name)
    {
        foreach (LogicVoiceInfo lvi in voiceInfos)
        {
            if (lvi.voiceName == _name)
                return lvi;
        }

        Debug.LogError("Voice info with '" + _name + "' not found!");
        return null;
    }

    public LogicVoiceInfo GetVoiceInfoByIndex(int _index)
    {
        return voiceInfos[_index];
    }

    int GetVoiceInfoIndex(LogicVoiceInfo _voiceInfo)
    {
        LogicVoiceInfo vi = _voiceInfo;

        for (int i = 0; i < voiceInfos.Length; i++)
        {
            if (voiceInfos[i] == vi)
                return i;
        }

        Debug.LogError(vi + " not found in logVoiceInfos list!");

        return -1;
    }

    void SetStep(LogVoiceCollectionStep _step)
    {
        step = _step;
    }

    public bool IsPlaying()
    {
        return step == LogVoiceCollectionStep.playing;
    }

    public bool IsPlayedRightNow()
    {
        return isPlayedRightNow;
    }
}
