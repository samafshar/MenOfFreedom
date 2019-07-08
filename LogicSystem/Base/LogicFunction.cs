using UnityEngine;
using System.Collections;

public enum LogicFuncStatus
{
    NotStarted,
    NotDone,
    Done,
}

public class LogicFunction
{
    public LogicFuncStatus status = LogicFuncStatus.NotDone;
}
