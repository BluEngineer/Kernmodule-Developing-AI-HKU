using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BehaviourTree
{
    [CreateAssetMenu()]
    public class BehaviourTree : ScriptableObject
    {
        public Node rootNode;
        public Node.State treeState = Node.State.Running;
        public List<Node> nodes = new List<Node>();

        public Blackboard blackboard = new Blackboard();

        public Node.State Update()
        {
            if (rootNode.state == Node.State.Running)
            {
                treeState = rootNode.Update();
            }
            return treeState;
        }
#if UNITY_EDITOR
        public Node CreateNode(System.Type _type)
        {
            Node node = ScriptableObject.CreateInstance(_type) as Node;
            node.name = _type.Name;
            node.guid = GUID.Generate().ToString();
            nodes.Add(node);
            
            AssetDatabase.AddObjectToAsset(node, this);
            AssetDatabase.SaveAssets();
            return node;
        }
        
        public void DeleteNode(Node _node)
        {
            nodes.Remove(_node);
            AssetDatabase.RemoveObjectFromAsset(_node);
            AssetDatabase.SaveAssets();
        }
#endif
        public void AddChild(Node parent, Node child)
        {
            DecoratorNode decorator = parent as DecoratorNode;
            if (decorator)
            {
                decorator.child = child;
            }
            
            RootNode root = parent as RootNode;
            if (root)
            {
                root.child = child;
            }
            
            CompositeNode composite = parent as CompositeNode;
            if (composite)
            {
                composite.children.Add(child);
            }

        }

        public static void Traverse(Node node, System.Action<Node> visitor)
        {
            if (node)
            {
                visitor.Invoke(node);
                var children = GetChildren(node);
                foreach (var n in children)
                {
                    Traverse(n, visitor);
                }
            }
        }

        public void Bind(AiAgent agent)
        {
            Traverse(rootNode, node =>
            {
                node.agent = agent;
                node.blackboard = blackboard;
            });
        }
        
        public void RemoveChild(Node parent, Node child)
        {
            DecoratorNode decorator = parent as DecoratorNode;
            if (decorator)
            {
                decorator.child = null;
            }
            
            RootNode root = parent as RootNode;
            if (root)
            {
                root.child = null;
            }
            
            CompositeNode composite = parent as CompositeNode;
            if (composite)
            {
                composite.children.Remove(child);
            } 
        }

        public static List<Node> GetChildren(Node parent)
        {
            List<Node> children = new List<Node>();
            DecoratorNode decorator = parent as DecoratorNode;
            if (decorator && decorator.child != null)
            {
                children.Add(decorator.child);
            }
            
            RootNode root = parent as RootNode;
            if (root && root.child != null)
            {
                children.Add(root.child);
            }
            
            CompositeNode composite = parent as CompositeNode;
            if (composite)
            {
                return composite.children;
            }

            return children;
        }
        
        public BehaviourTree Clone()
        {
            BehaviourTree tree = Instantiate(this);
            tree.rootNode = tree.rootNode.Clone();
            return tree;
        }
        
    }
}