using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamageNode : ActionNode
{
    public int damage;
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        if(blackboard.targetObject.GetComponent<IDamageable>() == null) Debug.Log("Target is Null!!!");
        blackboard.targetObject.GetComponent<IDamageable>().TakeDamage(agent.gameObject, damage);
        Debug.Log(agent.name + " Damaged " + blackboard.targetObject.name + " for " + damage +" damage");

        return State.Success;
    }
}
