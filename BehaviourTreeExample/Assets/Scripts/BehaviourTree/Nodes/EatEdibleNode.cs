using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatEdibleNode : ActionNode
{
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        if (agent.interactableInRange is IEdible)
        {
            IEdible edible = (IEdible)agent.interactableInRange;

            blackboard.hunger -= edible.foodValue;
            Debug.Log(blackboard.hunger);
            //Debug.Log("food's value is " + edible.foodValue);

            if (agent.interactableInRange != null)
            {
                agent.interactableInRange.Interact();    
                agent.interactableInRange = null;
                blackboard.targetObject = null;

            }

            //agent.interactableInRange.s
            return State.Success;  
        }

        return State.Failure;
    }
}
