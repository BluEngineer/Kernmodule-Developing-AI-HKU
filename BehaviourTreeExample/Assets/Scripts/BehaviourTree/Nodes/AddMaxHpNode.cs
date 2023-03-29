using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMaxHpNode : ActionNode
{
    public int additionalHp;
    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        agent.healthComponent.agentMaxHealth += additionalHp;
        return State.Success;
    }
}
