using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHitEffectNode : ActionNode
{
    public GameObject hitEffect;
    public Vector3 spawnOffset;
    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        if (blackboard.targetObject == null) return State.Failure;
        Instantiate(hitEffect,blackboard.targetObject.transform.position + spawnOffset, blackboard.targetObject.transform.rotation);
        return State.Success;
    }
}
