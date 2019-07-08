using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LogicTrigger : MonoBehaviour
{
    public GameObject[] validObjects;

    public string[] validTags;

    public FightSideEnum validFightSide = FightSideEnum.Ally;

    //

    [HideInInspector]
    public List<GameObject> objectsIn = new List<GameObject>();

    [HideInInspector]
    public float OutStep = 0;

    [HideInInspector]
    public bool didPlayerEnter = false;

    //

    MapLogic mapLogic;

    [HideInInspector]
    public bool isEnabled = false;

    bool isEnabledFirstTick = false;

    float enabledFixedUpdateCount = 0;

    float time_InObjectsGarbageRemove_Max = 0.5f;
    float time_InObjectsGarbageRemove_Counter = 0.5f;

    void Start()
    {
        mapLogic = MapLogic.Instance;
    }

    void OnTriggerEnter(Collider _enteredCol)
    {
        CheckAndAddColldier(_enteredCol);
    }

    void OnTriggerStay(Collider _stayedCol)
    {
        if (isEnabled && isEnabledFirstTick)
            CheckAndAddColldier(_stayedCol);
    }

    void OnTriggerExit(Collider _exitedCol)
    {
        if (isEnabled)
        {
            Collider exitedCol = _exitedCol;

            if (exitedCol == null)
                return;

            GameObject exitedObj = exitedCol.transform.root.gameObject;

            if (objectsIn.Contains(exitedObj))
                objectsIn.Remove(exitedObj);
        }
    }

    void FixedUpdate()
    {
        if (isEnabled)
        {
            if (isEnabledFirstTick)
            {
                enabledFixedUpdateCount++;

                if (enabledFixedUpdateCount == 30)
                    isEnabledFirstTick = false;
            }

            time_InObjectsGarbageRemove_Counter = MathfPlus.DecByDeltatimeToZero(time_InObjectsGarbageRemove_Counter);

            if (time_InObjectsGarbageRemove_Counter == 0)
            {
                time_InObjectsGarbageRemove_Counter = time_InObjectsGarbageRemove_Max;

                int i = 0;

                while (i < objectsIn.Count)
                {
                    if (objectsIn[i] == null)
                    {
                        objectsIn.RemoveAt(i);
                        continue;
                    }

                    i++;
                }
            }
        }
    }

    void CheckAndAddColldier(Collider _col)
    {
        if (isEnabled)
        {
            Collider col = _col;

            if (col == null)
                return;

            GameObject obj = col.transform.root.gameObject;

            bool isColValid = false;

            if (validObjects != null && validObjects.Length > 0)
            {
                for (int i = 0; i < validObjects.Length; i++)
                {
                    if (validObjects[i] == null)
                        continue;

                    if (validObjects[i] == obj)
                    {
                        if (!objectsIn.Contains(obj))
                            objectsIn.Add(obj);

                        return;
                    }
                }
            }
            else
            {
                if (validTags != null && validTags.Length > 0)
                {
                    for (int i = 0; i < validTags.Length; i++)
                    {
                        if (validTags[i].ToLower() == obj.tag.ToLower())
                        {
                            if (!objectsIn.Contains(obj))
                            {
                                objectsIn.Add(obj);
                            }

                            return;
                        }
                    }
                }
                else
                {
                    CharacterInfo chInf = obj.GetComponent<CharacterInfo>();
                    if (chInf != null && chInf.FightSide == validFightSide)
                    {
                        isColValid = true;
                    }
                }
            }

            if (isColValid)
            {
                if (!objectsIn.Contains(obj))
                    objectsIn.Add(obj);
            }

            if (PlayerCharacterNew.Instance != null)
            {
                if (objectsIn.Contains(PlayerCharacterNew.Instance.gameObject))
                {
                    didPlayerEnter = true;
                }
            }
        }
    }


    public void SetEnabled(bool _value)
    {
        if (_value == isEnabled)
            return;

        isEnabled = _value;
        isEnabledFirstTick = true;
        enabledFixedUpdateCount = 0;
        objectsIn.Clear();

        didPlayerEnter = false;
    }

    public void SetOutStep(float _value)
    {
        OutStep = _value;
    }

    public bool IsSomethingIn()
    {
        return objectsIn.Count > 0;
    }

    public int GetInsideObjsCount()
    {
        return objectsIn.Count;
    }

    public bool IsInsideObjsCountEqualOrBiggerThanValue(int _val)
    {
        return GetInsideObjsCount() >= _val;
    }

    public bool IsPlayerIn()
    {
        if (PlayerCharacterNew.Instance == null)
            return false;

        return objectsIn.Contains(PlayerCharacterNew.Instance.gameObject);
    }

    public bool IsGameObjectIn(GameObject _gameObj)
    {
        if (_gameObj == null)
            return false;

        return objectsIn.Contains(_gameObj);
    }

    public void StartOutStepIfNotStarted()
    {
        if (OutStep == 0)
            SetOutStep(1);
    }

    public void StartFinishing_OutStepIfNotFishining()
    {
        if (OutStep >= 900)
            return;

        SetOutStep(900);
    }

}
