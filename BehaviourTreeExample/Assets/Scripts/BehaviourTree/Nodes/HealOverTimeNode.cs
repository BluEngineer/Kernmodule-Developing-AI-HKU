using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOverTimeNode : ActionNode
{
    public int healthIncrease;
    private float healthOverTime;
    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (agent.healthComponent.currentHealth != agent.healthComponent.agentMaxHealth)
        {
            agent.healthComponent.currentHealth += healthIncrease;
        }
        
        return State.Success;
    }
}
