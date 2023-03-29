using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseBlackboardValue : ActionNode
{
    public float valueIncrease;

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        blackboard.hungerIndicator += valueIncrease;
        //Debug.Log("local Hungerindicator value is " + blackboard.hungerIndicator);
        return State.Success;
    }
}
