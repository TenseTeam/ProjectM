namespace VUDK.Features.Packages.DialogueSystem.Editor.Windows
{
    using System;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEditor.Experimental.GraphView;
    using UnityEngine;
    using UnityEngine.UIElements;
    using VUDK.Features.Packages.DialogueSystem.Editor.Elements;
    using VUDK.Features.Packages.DialogueSystem.Editor.Utilities;
    using VUDK.Features.Packages.DialogueSystem.Enums;

    public class DSGraphView : GraphView
    {
        private DSEditorWindow _editorWindow;
        private DSSearchWindow _searchWindow;

        public DSGraphView(DSEditorWindow editorWindow)
        {
            _editorWindow = editorWindow;
            AddManipulators();
            AddSearchWindow();
            AddGridBackground();
            AddStyles();
        }

        private void AddSearchWindow()
        {
            if (!_searchWindow)
            {
                _searchWindow = ScriptableObject.CreateInstance<DSSearchWindow>();
                _searchWindow.Init(this);
            }

            nodeCreationRequest += context => SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), _searchWindow);
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            List<Port> compatiblePorts = new List<Port>();
            ports.ForEach(port =>
            {
                if (startPort == port) return;
                if (startPort.node == port.node) return;
                if (startPort.direction == port.direction) return;

                compatiblePorts.Add(port);
            });
            return compatiblePorts;
        }

        private void AddManipulators()
        {
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(CreateNodeContextualMenu(DSDialogueType.SingleChoice, "Add Node (Single Choice)"));
            this.AddManipulator(CreateNodeContextualMenu(DSDialogueType.MultipleChoice, "Add Node (Multiple Choice)"));
            this.AddManipulator(CreateGroupContextualMenu());
        }

        private IManipulator CreateGroupContextualMenu()
        {
            ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator(
                menuEvent => menuEvent.menu.AppendAction("Add Group",
                    actionEvent => AddElement(CreateGroup("DialogueGroup", GetLocalMousePosition(actionEvent.eventInfo.localMousePosition))))
                );
            return contextualMenuManipulator;
        }

        private IManipulator CreateNodeContextualMenu(DSDialogueType dialogueType, string actionTitle)
        {
            ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator(
                menuEvent => menuEvent.menu.AppendAction(actionTitle,
                    actionEvent => AddElement (DSNode.Create(dialogueType, GetLocalMousePosition(actionEvent.eventInfo.localMousePosition))))
                );
            return contextualMenuManipulator;
        }

        public Group CreateGroup(string title, Vector2 localMousePosition)
        {
            Group group = new Group
            {
                title = title,
                autoUpdateGeometry = true,
            };

            group.SetPosition(new Rect(localMousePosition, Vector2.zero));

            return group;
        }

        public Vector2 GetLocalMousePosition(Vector2 mousePosition, bool isSearchWindow = false) // TODO: Make it better
        {
            Vector2 worldMousePosition = mousePosition;

            if (isSearchWindow)
                worldMousePosition -= _editorWindow.position.position;

            Vector2 localMousePosition = contentViewContainer.WorldToLocal(worldMousePosition);
            return localMousePosition;
        }

        private void AddStyles()
        {
            this.AddStyleSheets
            (
                "DialogueSystem/DSGraphViewStyles.uss",
                "DialogueSystem/DSNodeStyles.uss"
            );

            StyleSheet graphStyle = (StyleSheet) EditorGUIUtility.Load("DialogueSystem/DSGraphViewStyles.uss");
            StyleSheet nodeStyle = (StyleSheet)EditorGUIUtility.Load("DialogueSystem/DSNodeStyles.uss");

            styleSheets.Add(graphStyle);
            styleSheets.Add(nodeStyle);
        }

        private void AddGridBackground()
        {
            GridBackground gridBackground = new GridBackground();
            gridBackground.StretchToParentSize();
            Insert(0, gridBackground);
        }
    }
}