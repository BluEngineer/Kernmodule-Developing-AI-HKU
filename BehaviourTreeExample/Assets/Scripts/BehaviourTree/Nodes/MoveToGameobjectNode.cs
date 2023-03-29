using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToGameobjectNode : ActionNode
{
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return MoveToTargetObject();
    }

    public State MoveToTargetObject()
    {
                
        if (blackboard.navMeshAgent.isPathStale) return State.Failure;
        if (blackboard.navMeshAgent.pathPending) return State.Running;
        blackboard.navMeshAgent.SetDestination(blackboard.targetObject.transform.position);

        if (blackboard.navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            Debug.Log("path complete");
            return State.Success;
        }
        return State.Running;
    }
}
