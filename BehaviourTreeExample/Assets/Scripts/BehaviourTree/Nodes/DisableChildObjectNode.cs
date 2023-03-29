using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableChildObjectNode : ActionNode
{
    public string ChildObjectName;
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        GameObject childObject = agent.transform.Find(ChildObjectName).gameObject;

        childObject.SetActive(false);

        return State.Success;
    }
}
