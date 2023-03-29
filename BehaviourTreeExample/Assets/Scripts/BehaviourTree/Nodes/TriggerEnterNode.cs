using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnterNode : ActionNode
{
    public string tag;

    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {
        //blackboard.targetObject = null;
    }  
    
    protected override State OnUpdate()
    {
        //blackboard.targetObject = null;

        for (int i = agent.objectsInTrigger.Count - 1; i > -1; i--)
            {
            if (agent.objectsInTrigger[i] == null)
                agent.objectsInTrigger.RemoveAt(i);


            if (agent.objectsInTrigger[i].tag == tag)
                {
                    blackboard.targetObject = agent.objectsInTrigger[i];
                    return State.Success;
            }
        }
        //Debug.Log(agent.objectInTriggerTag);
        return State.Failure;

        if (agent.objectInTriggerTag == tag)
        {

        }
        //{

    }
    
    
    
    
}
