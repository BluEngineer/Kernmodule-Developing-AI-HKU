using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeFromTargetNode : ActionNode
{
    public float detectDistance = 7;
    public float fleeDistance = 2;
    
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if(blackboard.targetObject == null) return State.Failure;

        float distance = Vector3.Distance(agent.transform.position, blackboard.targetObject.transform.position);
        //Debug.Log(blackboard.targetObject);
        Debug.Log(distance);
        //if (distance < detectDistance)
        {
            Vector3 dir = (agent.transform.position - blackboard.targetObject.transform.position).normalized;
            Vector3 newPos = agent.transform.position + (dir * fleeDistance);
            blackboard.navMeshAgent.SetDestination((newPos));
        }
        
        
        return State.Success;
    }
}
