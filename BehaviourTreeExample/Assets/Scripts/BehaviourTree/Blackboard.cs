using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class Blackboard
{
    public GameObject agentObject;
    public Vector3 moveToPosition;
    public GameObject targetObject;
    public NavMeshAgent navMeshAgent;
    public AiAgent aiAgent;
    public float agentSpeed;
    public AgentHealthBar healthComponent;
    public InstantiateData instantiateData;

    //Monster specific variables:
    [HideInInspector]public float hunger = 120;
    public float hungerIndicator = 8;

    //Hunter specific variables:
    public float confidenceLevel = 0;
}
