namespace VUDK.Features.Packages.DialogueSystem.Editor.Windows
{
    using System.Collections.Generic;
    using UnityEditor.Experimental.GraphView;
    using UnityEngine;
    using VUDK.Features.Packages.DialogueSystem.Editor.Elements;
    using VUDK.Features.Packages.DialogueSystem.Enums;

    public class DSSearchWindow : ScriptableObject, ISearchWindowProvider
    {
        private DSGraphView _graphView;
        private Texture2D _indentationIcon;

        public void Init(DSGraphView graphView)
        {
            _graphView = graphView;
            _indentationIcon = new Texture2D(1, 1);
            _indentationIcon.SetPixel(0, 0, Color.clear);
            _indentationIcon.Apply();
        }

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            List<SearchTreeEntry> searchTreeEntries = new List<SearchTreeEntry>()
            {
                new SearchTreeGroupEntry(new GUIContent("Create Element")),
                new SearchTreeGroupEntry(new GUIContent("Dialogue Node"), 1),
                new SearchTreeEntry(new GUIContent("Single Choice", _indentationIcon))
                {
                    level = 2,
                    userData = DSDialogueType.SingleChoice
                },
                new SearchTreeEntry(new GUIContent("Multiple Choice", _indentationIcon))
                {
                    level = 2,
                    userData = DSDialogueType.MultipleChoice
                },
                new SearchTreeGroupEntry(new GUIContent("Dialogue Group"), 1),
                new SearchTreeEntry(new GUIContent("Group", _indentationIcon))
                {
                    level = 2,
                    userData = new Group()
                },
            };

            return searchTreeEntries;
        }

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            Vector2 localMousePosition = _graphView.GetLocalMousePosition(context.screenMousePosition, true);

            switch (SearchTreeEntry.userData)
            {
                case DSDialogueType.SingleChoice:
                    DSSingleChoiceNode singleChoiceNode = DSNode.Create(DSDialogueType.SingleChoice, Vector2.zero) as DSSingleChoiceNode;
                    _graphView.AddElement(singleChoiceNode);
                    return true;

                case DSDialogueType.MultipleChoice:
                    DSMultipleChoiceNode multChoiceNode = DSNode.Create(DSDialogueType.MultipleChoice, Vector2.zero) as DSMultipleChoiceNode;
                    _graphView.AddElement(multChoiceNode);
                    return true;

                case Group _:
                    Group group = _graphView.CreateGroup("DialogueGroup", localMousePosition);
                    _graphView.AddElement(group);
                    return true;

                default:
                    return false;
            }
        }
    }
}