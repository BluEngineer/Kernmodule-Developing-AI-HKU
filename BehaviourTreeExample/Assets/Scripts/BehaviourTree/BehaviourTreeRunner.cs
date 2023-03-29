using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviourTree
{

    public class BehaviourTreeRunner : MonoBehaviour
    {
        [SerializeField]public BehaviourTree tree;
        public GameObject playerObject;
        
        private void Start()
        {
            tree = tree.Clone();
            tree.Bind(GetComponent<AiAgent>());
            tree.blackboard.moveToPosition = new Vector3(3, 5, 8);
            tree.blackboard.navMeshAgent = GetComponent<NavMeshAgent>();
            tree.blackboard.agentObject = gameObject;
            tree.blackboard.targetObject = playerObject;


        }
        
        private void Update()
        {
            tree.Update();
            //Debug.Log(tree.name);
        }
        
    }
}
