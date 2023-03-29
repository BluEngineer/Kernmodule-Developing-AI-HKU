using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class FindRandomPositionInRangeNode : ActionNode
{
    public Vector3 rangeFromStartPoint;
    public bool localPositionRange;

    private Vector3 randomPosition;

    private void Awake()
    {
        //rangeFromStartPoint += blackboard.agentObject.transform.position;

    }

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return  FindRandomPosition();
    }

    private State FindRandomPosition()
    {
        if (localPositionRange)
        {
            Vector3 agentPos = agent.transform.position;
            randomPosition = new Vector3(
            Random.Range(agentPos.x - rangeFromStartPoint.x, agentPos.x + rangeFromStartPoint.x),
            Random.Range(agentPos.y - rangeFromStartPoint.y, agentPos.y + rangeFromStartPoint.y),
            Random.Range(agentPos.z - rangeFromStartPoint.z, agentPos.z + rangeFromStartPoint.z));
        }
        else
        {
            randomPosition = new Vector3(
            Random.Range(-rangeFromStartPoint.x, rangeFromStartPoint.x),
            Random.Range(-rangeFromStartPoint.y, rangeFromStartPoint.y),
            Random.Range(-rangeFromStartPoint.z, rangeFromStartPoint.z));

            //if (NavMesh.SamplePosition( )

        }
        
            blackboard.moveToPosition = randomPosition;

        

        return State.Success;
    }
    
    

    
}
