using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsHealthOverNode : ActionNode
{
    public int healthTreshold;
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (agent.healthComponent.currentHealth > healthTreshold)
        {
            //Debug.Log("health over threshold");
            return State.Success;
        }
        return State.Failure;

    }
}
