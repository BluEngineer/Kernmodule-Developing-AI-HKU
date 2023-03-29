using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTaggedObjectVisibleNode : ActionNode
{
    public string tag;

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (agent.sensor.FindClosestVisibleObjectWithTag(tag) != null)
        {
            //Debug.Log(agent.sensor.FindClosestVisibleObjectWithTag(tag) + " is visible");
            blackboard.targetObject = agent.sensor.FindClosestVisibleObjectWithTag(tag);
            return State.Success;
            
        }

        return State.Failure;
    }
}
