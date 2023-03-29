using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFeedbackTextNode : ActionNode
{
    public string newText;
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        agent.UpdateUIText(newText);
        return State.Success;
    }
}
