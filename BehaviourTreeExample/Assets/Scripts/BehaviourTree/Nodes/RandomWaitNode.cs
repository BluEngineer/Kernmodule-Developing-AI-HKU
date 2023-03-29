using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWaitNode : ActionNode
{
    public Vector2 waitMinMax = new Vector2(-1,1);
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
        if (Time.time - startTime > Random.Range(waitMinMax.x, waitMinMax.y))
        {
            return State.Success;
        }

        return State.Running;
    }
}
