using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAgentScaleNode : ActionNode
{
    public float sizeMultiplier;
    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        agent.transform.localScale += new Vector3(sizeMultiplier, sizeMultiplier, sizeMultiplier);
        return State.Success;
    }
}
