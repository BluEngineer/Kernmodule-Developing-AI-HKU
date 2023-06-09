using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitNode : ActionNode
{
    public float waitDuration = 1;
    private float startTime;
    
    protected override void OnStart()
    {
        startTime = Time.time;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (Time.time - startTime > waitDuration)
        {
            return State.Success;
        }

        return State.Running;
    }
}
