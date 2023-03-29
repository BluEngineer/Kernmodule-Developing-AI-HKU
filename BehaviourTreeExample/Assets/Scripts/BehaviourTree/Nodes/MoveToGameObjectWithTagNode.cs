using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToGameObjectWithTagNode : ActionNode
{
    public string objectTag;
    private float timer = 15;
    
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return MoveToTargetObject();
        timer -= Time.deltaTime;
    }

    public State MoveToTargetObject()
    {
                
        if (blackboard.navMeshAgent.isPathStale) return State.Failure;
        if (blackboard.navMeshAgent.pathPending) return State.Running;
        if (agent.FindClosestObjectWithTag(objectTag) != null)
        {
            blackboard.navMeshAgent.SetDestination(agent.FindClosestObjectWithTag(objectTag).transform.position);
        }

        if (blackboard.navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            //Debug.Log("path complete");
            return State.Success;
        }
        return State.Running;
    }
}
