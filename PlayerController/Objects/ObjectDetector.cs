using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectDetector : MonoBehaviour
{

    [HideInInspector]
    List<Item> insideItems = new List<Item>();
    public List<Item> InsideItems
    {
        get
        {
            RemoveGarbagesOfItemsList();

            return insideItems;
        }
    }

    [HideInInspector]
    List<LogicObjective> insideLogicObjectives = new List<LogicObjective>();
    public List<LogicObjective> InsideLogicObjectives
    {
        get
        {
            RemoveGarbagesOfLogicObjectivesList();

            return insideLogicObjectives;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            GameObject obj = other.gameObject;
            Item objItem = obj.GetComponent<Item>();

            if (objItem != null && objItem.IsActive)
            {
                AddItemToInsideList(objItem);
                return;
            }

            LogicObjective objLogObj = obj.GetComponent<LogicObjective>();

            if (objLogObj != null && objLogObj.IsActive)
            {
                AddLogicObjectiveToInsideList(objLogObj);
                return;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other != null)
        {
            GameObject obj = other.gameObject;
            Item objItem = obj.GetComponent<Item>();

            if (objItem != null)
            {
                RemoveItemFromInsideList(objItem);
                return;
            }

            LogicObjective objLogObj = obj.GetComponent<LogicObjective>();

            if (objLogObj != null)
            {
                RemoveLogicObjectiveFromInsideList(objLogObj);
                return;
            }
        }
    }

    void AddItemToInsideList(Item _item)
    {
        Item item = _item;

        if (!insideItems.Contains(item))
        {
            insideItems.Add(item);
        }

        RemoveGarbagesOfItemsList();
    }

    void RemoveItemFromInsideList(Item _item)
    {
        Item item = _item;

        if (insideItems.Contains(item))
        {
            insideItems.Remove(item);
        }

        RemoveGarbagesOfItemsList();
    }

    void RemoveGarbagesOfItemsList()
    {
        int i = 0;
        while (i < insideItems.Count)
        {
            if (insideItems[i] == null)
            {
                insideItems.RemoveAt(i);
            }
            else
            {
                if (!insideItems[i].IsActive)
                {
                    insideItems.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
        }
    }

    void AddLogicObjectiveToInsideList(LogicObjective _logObj)
    {
        LogicObjective logObj = _logObj;

        if (!insideLogicObjectives.Contains(logObj))
        {
            insideLogicObjectives.Add(logObj);
        }

        RemoveGarbagesOfLogicObjectivesList();
    }

    void RemoveLogicObjectiveFromInsideList(LogicObjective _logObj)
    {
        LogicObjective logObj = _logObj;

        if (insideLogicObjectives.Contains(logObj))
        {
            insideLogicObjectives.Remove(logObj);
        }

        RemoveGarbagesOfLogicObjectivesList();
    }

    void RemoveGarbagesOfLogicObjectivesList()
    {
        int i = 0;
        while (i < insideLogicObjectives.Count)
        {
            if (insideLogicObjectives[i] == null)
            {
                insideLogicObjectives.RemoveAt(i);
            }
            else
            {
                if (!insideLogicObjectives[i].IsActive || insideLogicObjectives[i].IsDone)
                {
                    insideLogicObjectives.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
        }
    }

    public bool DoesContainAnyItem()
    {
        RemoveGarbagesOfItemsList();

        return insideItems.Count > 0;
    }

    public bool DoesContainAnyLogicObjectives()
    {
        RemoveGarbagesOfLogicObjectivesList();

        return insideLogicObjectives.Count > 0;
    }

    public bool DoesContainAnything()
    {
        return (DoesContainAnyItem() || DoesContainAnyLogicObjectives());
    }

    public Item GetFirstOKItemFromList()
    {
        List<Item> list = InsideItems;

        foreach (Item itm in list)
        {
            if (itm != null && itm.IsActive)
            {
                return itm;
            }
        }

        return null;
    }
}
