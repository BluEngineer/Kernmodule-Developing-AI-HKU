using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAgentSpeedNode : ActionNode
{
    public float newAgentSpeed;
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        blackboard.navMeshAgent.speed = newAgentSpeed;
        return State.Success;
    }
}
