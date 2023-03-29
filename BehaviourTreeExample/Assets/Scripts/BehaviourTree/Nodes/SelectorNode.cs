using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;

public class SelectorNode : CompositeNode
{
    private int currentChildIndex;
    
    protected override void OnStart()
    {
        currentChildIndex = 0;
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {

        for (int i = 0; i < children.Count; i++)
        {
            currentChildIndex = i;
            Node currentChild = children[currentChildIndex];

            switch (currentChild.Update())
            {
                case State.Running:
                    return State.Running;
                case State.Success:
                    //Debug.Log("selector succes");
                    return State.Success;
                case State.Failure:
                    continue;
            }
            //Debug.Log("selector failed");
        }
        
        return State.Failure;

    }
}
