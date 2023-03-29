using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RotateTowardsTarget : ActionNode
{
    public float rotationSpeed;   
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (blackboard.targetObject != null)
        {
            RotateTowards(blackboard.targetObject.transform);
      
        }
        return State.Success;
    }

    private void RotateTowards (Transform target)
    {
        Transform agentTransform = agent.transform;
        Vector3 direction = (new Vector3(target.position.x,agent.transform.position.y,target.position.z) - agent.transform.position ).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y ,direction.z));
        //lookRotation = Quaternion.Euler(new Vector3(0,lookRotation.y,0));
        agent.gameObject.transform.rotation = Quaternion.Slerp(agent.gameObject.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        
    }
}
