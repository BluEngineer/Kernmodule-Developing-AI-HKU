using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullHealNode : ActionNode
{

    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        agent.healthComponent.currentHealth = agent.healthComponent.agentMaxHealth;
        return State.Success;
    }
}
