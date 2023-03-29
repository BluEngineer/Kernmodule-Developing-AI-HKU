using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToTargetPositionNode : ActionNode
{
    public float timer;

    protected override void OnStart()
    {
        //MoveToTargetPosition();
        
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        return MoveToTargetPosition();

    }

    public State MoveToTargetPosition()
    {
        
        if (blackboard.navMeshAgent.isPathStale) return State.Failure;
        if (blackboard.navMeshAgent.pathPending) return State.Running;
        blackboard.navMeshAgent.SetDestination(blackboard.moveToPosition);

        if (blackboard.navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            //Debug.Log("path complete");
            return State.Success;
        }

        timer += Time.deltaTime;
        if (timer > 15) return State.Failure;
        return State.Running;
    }
}
