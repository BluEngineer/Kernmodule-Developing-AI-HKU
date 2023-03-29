using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public abstract class Node : ScriptableObject
    {
        public enum State
        {
            Running,
            Failure,
            Success
        }

        public State state = State.Running;
        [HideInInspector]public bool started = false;
        [HideInInspector]public string guid;
        [HideInInspector]public Vector2 position;
        public Blackboard blackboard;
        public AiAgent agent;

        public State Update()
        {
            if (!started)
            {
                OnStart();
                started = true;
            }

            state = OnUpdate();

            if (state == State.Failure || state == State.Success)
            {
                OnStop();
                started = false;
            }

            return state;
        }

        
        
        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract State OnUpdate();
        
        
        public void Abort() {
            BehaviourTree.Traverse(this, (node) => {
                node.started = false;
                node.state = State.Running;
                node.OnStop();
            });
        }

        public virtual Node Clone()
        {
            return Instantiate(this);
        }
    }
}
