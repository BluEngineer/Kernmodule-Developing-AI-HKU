using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLogNode : ActionNode
{
    public string message;
    protected override void OnStart()
    {
       // Debug.Log($"OnStart{message}");
    }

    protected override void OnStop()
    {
        //Debug.Log($"OnStop{message}");

    }

    protected override State OnUpdate()
    {
        //ebug.Log(blackboard.moveToPosition);
        return State.Success;
    }
}
