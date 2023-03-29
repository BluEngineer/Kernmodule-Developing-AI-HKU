using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimatorTriggerNode : ActionNode
{
    public string triggerName;
    
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        agent.animator.SetTrigger(triggerName);
        return State.Success;
    }
}
