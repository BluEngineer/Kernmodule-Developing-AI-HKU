using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

namespace BehaviourTree
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        public Node node;
        public Port input;
        public Port output;

        public Action<NodeView> OnNodeSelected;

        public NodeView(Node _node)
        {
            this.node = _node;
            this.title = _node.name;
            this.viewDataKey = node.guid;
            style.left = _node.position.x;
            style.top = _node.position.y;

            CreateInputPorts();
            CreateOutputPorts();
        }

        private void CreateOutputPorts()
        {
            if (node is ActionNode)
            {
                //output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));

            } else if (node is CompositeNode)
            {
                output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(bool));

            } else if (node is DecoratorNode)
            {
                output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
            
            } else if (node is RootNode)
            {
                output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
            
            }

            if (output != null)
            {
                output.portName = "";
                outputContainer.Add((output));
            } 
        }

        private void CreateInputPorts()
        {
            if (node is ActionNode)
            {
                input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
            } else if (node is CompositeNode)
            {
                input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));

            } else if (node is DecoratorNode)
            {
                input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));

            } else if (node is RootNode)
            {

            }
            if (input != null)
            {
                input.portName = "";
                inputContainer.Add((input));
            }           
        }

        public override void SetPosition(Rect _newPos)
        {
            base.SetPosition(_newPos);
            node.position.x = _newPos.xMin;
            node.position.y = _newPos.yMin;

        }

        public override void OnSelected()
        {
            if (OnNodeSelected != null)
            {
                Debug.Log(this.title + " selected");
                OnNodeSelected.Invoke(this);
            }
        }
    }  
}

