using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

public class InspectorView : VisualElement
{
        
        public new class UxmlFactory : UxmlFactory<InspectorView, VisualElement.UxmlTraits>{}

        private Editor editor;
        
        public InspectorView()
        {

        }

        public void UpdateSelection(NodeView nodeView)
        {
               Clear();
               editor = Editor.CreateEditor(nodeView.node);
               Object.DestroyImmediate(editor, true);
               IMGUIContainer container = new IMGUIContainer(() => {editor.OnInspectorGUI();});
               //add editor container as child of VisualElement
               Add(container);
        }
}
