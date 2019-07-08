using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MortarManager : MonoBehaviour
{
    public List<Mortar> mortars = new List<Mortar>();
    public float playerDeathChance = 0f;
    public int maxMortarInSameTime = 1;
    public float minTimeBetween;
    public float maxTimeBetween;
    public int chanceGroupA = 50;
    public int chanceGroupB = 35;
    public int chanceGroupC = 15;

    //

    float timer = 0f;
    float timeOfCheckingCurrentMortars = 1.5f;
    float timerForCheckingCurrentMortars;

    int chance;
    int step = -1;
    int chosenGroup;

    bool isFinished = false;
    bool isStarted = false;
    bool isReady = true;

    List<Mortar> curMortars = new List<Mortar>();

    MortarPriorityGroup selectedGroup;

    class ClassifiedMortars
    {
        public List<Mortar> mortars = new List<Mortar>();

        public MortarPriorityGroup priorityGroup;
    }
    ClassifiedMortars[] classifiedMortars;

    //

    void Start()
    {
        timerForCheckingCurrentMortars = timeOfCheckingCurrentMortars;

        classifiedMortars = new ClassifiedMortars[4];

        for (int i = 0; i < classifiedMortars.Length; i++)
        {
            classifiedMortars[i] = new ClassifiedMortars();
        }

        SetClassifiedMortars();
    }

    void Update()
    {
        if (step == 1)
        {
            bool found = false;
            selectedGroup = MortarPriorityGroup.none;

            if (curMortars.Count < mortars.Count)
            {
                while (!found)
                {
                    selectedGroup = SelectPriorityGroup();

                    for (int i = 0; i < classifiedMortars.Length; i++)
                    {
                        if (classifiedMortars[i].priorityGroup == selectedGroup)
                        {
                            if (classifiedMortars[i].mortars.Count > 0)
                            {
                                chosenGroup = i;
                                found = true;
                                break;
                            }
                        }
                    }
                }
            }

            if (selectedGroup == MortarPriorityGroup.KillingPlayer)
                SetStep(2);
            else
                SetStep(3);
        }

        if (step == 2)
        {
            if (classifiedMortars[chosenGroup].mortars[0] == null)
                Debug.LogError("MortarManager has no killing mortar");
            else
            {
                classifiedMortars[chosenGroup].mortars[0].transform.position = PlayerCharacterNew.Instance.transform.position;
                classifiedMortars[chosenGroup].mortars[0].StartIt();
            }

            SetStep(1000);
        }

        if (step == 3)
        {
            if (curMortars.Count == 0)
            {
                SetStep(5);
            }
            else
            {
                if (curMortars.Count < maxMortarInSameTime)
                {
                    timer = Random.Range(minTimeBetween, maxTimeBetween);

                    SetStep(4);
                }
            }
        }

        if (step == 4)
        {
            timer = MathfPlus.DecByDeltatimeToZero(timer);
            if (timer == 0)
            {
                SetStep(5);
            }
        }

        if (step == 5)
        {
            int rnd = Random.Range(0, classifiedMortars[chosenGroup].mortars.Count);

            Mortar selectedMortar;
            selectedMortar = classifiedMortars[chosenGroup].mortars[rnd];

            selectedMortar.StartIt();

            OnExplode();

            curMortars.Add(selectedMortar);
            classifiedMortars[chosenGroup].mortars.RemoveAt(rnd);

            EndIt();
        }

        timerForCheckingCurrentMortars = MathfPlus.DecByDeltatimeToZero(timerForCheckingCurrentMortars);
        if (timerForCheckingCurrentMortars == 0)
        {
            timerForCheckingCurrentMortars = timeOfCheckingCurrentMortars;

            foreach (Mortar mrt in curMortars)
            {
                if (mrt.IsFinished())
                {
                    mrt.Refresh();
                    AddMortarToProperGroup(mrt);

                    curMortars.Remove(mrt);

                    break;
                }
            }
        }
    }

    void SetStep(int _step)
    {
        step = _step;
    }

    void SetChance()
    {
        chance = (int)(Random.Range(0f, 1f) * 100);
    }

    MortarPriorityGroup SelectPriorityGroup()
    {
        SetChance();
        if (chance < playerDeathChance)
            return MortarPriorityGroup.KillingPlayer;

        SetChance();
        if (chance <= chanceGroupA)
        {
            return MortarPriorityGroup.A;
        }

        if (chance <= chanceGroupA + chanceGroupB)
        {
            return MortarPriorityGroup.B;
        }

        if (chance <= chanceGroupA + chanceGroupB + chanceGroupC)
        {
            return MortarPriorityGroup.C;
        }

        return MortarPriorityGroup.A;
    }

    void SetClassifiedMortars()
    {
        for (int i = 0; i < classifiedMortars.Length; i++)
        {
            classifiedMortars[i].priorityGroup = MortarPriorityGroup.A + i;
        }

        foreach (Mortar mrt in mortars)
        {
            AddMortarToProperGroup(mrt);
        }
    }

    void AddMortarToProperGroup(Mortar _mrt)
    {
        Mortar mrt = _mrt;

        for (int i = 0; i < classifiedMortars.Length; i++)
        {
            if (classifiedMortars[i].priorityGroup == mrt.priorityGroup)
            {
                classifiedMortars[i].mortars.Add(mrt);
                break;
            }
        }
    }

    void setStarted(bool _isStarted)
    {
        isStarted = _isStarted;
    }

    void SetReady(bool _isReady)
    {
        isReady = _isReady;
    }

    void OnExplode()
    {
        SetReady(true);
    }

    public void StartItIfNotStarted()
    {
        if (!isStarted)
        {
            SetFinished(false);

            setStarted(true);

            SetReady(false);

            SetStep(1);
        }
    }

    public void EndIt()
    {
        SetFinished(true);

        setStarted(false);

        SetStep(1000);
    }

    public void SetFinished(bool _isFinish)
    {
        isFinished = _isFinish;
    }

    public bool IsReady()
    {
        return isReady;
    }
}