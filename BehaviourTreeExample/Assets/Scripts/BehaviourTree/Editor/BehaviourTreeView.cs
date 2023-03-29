using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using System.Linq;
using BehaviourTree = BehaviourTree.BehaviourTree;

namespace BehaviourTree
{
    public class BehaviourTreeView : GraphView
    {
        public new class UxmlFactory : UxmlFactory<BehaviourTreeView, GraphView.UxmlTraits>{}
        public Action<NodeView> OnNodeSelected;

        public BehaviourTree tree;
        public BehaviourTreeView()
        {
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());


            Insert(0, new GridBackground());
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/BehaviourTree/Editor/BehaviourTreeEditor.uss");
            styleSheets.Add(styleSheet);  
        }
        
        public void PopulateView(BehaviourTree _tree)
        {
            this.tree = _tree;

            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements);
            graphViewChanged += OnGraphViewChanged;

            if (tree.rootNode == null)
            {
                tree.rootNode = tree.CreateNode(typeof(RootNode)) as RootNode;
                EditorUtility.SetDirty(tree);
                AssetDatabase.SaveAssets();
            }
            
            //creat e node view
            foreach (var n in tree.nodes)
            {
                CreateNodeView(n);
            }
            
            //create edge view
            foreach (var n in tree.nodes)
            {
                var children = BehaviourTree.GetChildren(n);
                foreach (var c in children)
                {
                    NodeView parentView = FindNodeView(n);
                    NodeView childView = FindNodeView(c);

                    Edge edge = parentView.output.ConnectTo(childView.input);
                    AddElement(edge);
                }
            }
            
        }

        private NodeView FindNodeView(Node node)
        {
            return GetNodeByGuid(node.guid) as NodeView;
            
        }
        
        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.ToList().Where(endPort => endPort.direction != startPort.direction && endPort.node != startPort.node).ToList();
        }

        private GraphViewChange OnGraphViewChanged(GraphViewChange _graphviewchange)
        {
            if (_graphviewchange.elementsToRemove != null)
            {
                foreach (var e in _graphviewchange.elementsToRemove)
                {
                    NodeView nodeView = e as NodeView;
                    if (nodeView != null)
                    {
                        tree.DeleteNode((nodeView.node));
                    }
                    
                    Edge edge = e as Edge;
                    if (edge != null)
                    {
                        NodeView parentView = edge.output.node as NodeView;
                        NodeView childView = edge.input.node as NodeView;
                        tree.RemoveChild(parentView.node, childView.node);

                    }
                }
            }

            if (_graphviewchange.edgesToCreate != null)
            {
                foreach (var e in _graphviewchange.edgesToCreate)
                {
                    NodeView parentView = e.output.node as NodeView;
                    NodeView childView = e.input.node as NodeView;
                    tree.AddChild(parentView.node, childView.node);
                }
            }
            
            return _graphviewchange;
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            {
                var types = TypeCache.GetTypesDerivedFrom<ActionNode>();
                foreach (var t in types)
                {
                    evt.menu.AppendAction($"[{t.BaseType?.Name}] {t.Name}", (a) => CreateNode(t));
                }
            }
            
            {
                var types = TypeCache.GetTypesDerivedFrom<CompositeNode>();
                foreach (var t in types)
                {
                    evt.menu.AppendAction($"[{t.BaseType?.Name}] {t.Name}", (a) => CreateNode(t));
                }
            }
            {
                var types = TypeCache.GetTypesDerivedFrom<DecoratorNode>();
                foreach (var t in types)
                {
                    evt.menu.AppendAction($"[{t.BaseType?.Name}] {t.Name}", (a) => CreateNode(t));
                }
            }
        }
        

        private void CreateNode(System.Type _type)
        {
            Node node = tree.CreateNode(_type);
            CreateNodeView(node);
        }
        
        public void CreateNodeView(Node _node)
        {
            NodeView nodeView = new NodeView(_node);

            nodeView.OnNodeSelected = OnNodeSelected;
            
            AddElement(nodeView);
        }
    }   
}

