using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RepeatNode : DecoratorNode
{
    public int repeatCount;
    [FormerlySerializedAs("repeatUnlimited")] public bool repeatInfinite;
    private int repeatCounter;
    
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        if (repeatInfinite)
        {
            child.Update();
            return State.Running;  
        }
        
        if (repeatCounter < repeatCount && !repeatInfinite)
        {
            if (child.Update() == State.Success)
            {
                repeatCounter++;
                //Debug.Log(child.name + " returned succes on repeat, counter is now " + repeatCounter);
            }
            //child.Update();
            return State.Running;         
        }
        else
        {
            return State.Success;
            repeatCount = 0;
        }

    }
}
