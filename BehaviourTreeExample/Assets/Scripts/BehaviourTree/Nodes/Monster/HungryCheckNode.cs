using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungryCheckNode : ActionNode
{
    public float hungerTreshold = 80;
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        //Debug.Log(blackboard.hungerIndicator);
        if (blackboard.hungerIndicator <= hungerTreshold)
        {

            return State.Failure;
        }
        
        return State.Success;
    }
}
