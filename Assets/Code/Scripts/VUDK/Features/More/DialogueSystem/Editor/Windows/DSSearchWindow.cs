namespace VUDK.Features.More.DialogueSystem.Editor.Windows
{
    using System.Collections.Generic;
    using UnityEditor.Experimental.GraphView;
    using UnityEngine;
    using VUDK.Features.More.DialogueSystem.Editor.Elements;
    using VUDK.Features.More.DialogueSystem.Enums;

    public class DSSearchWindow : ScriptableObject, ISearchWindowProvider
    {
        private enum DSEntryType
        {
            SingleChoiceDialogue,
            MultipleChoiceDialogue,
            DialogueGroup
        }

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
                    userData = DSEntryType.SingleChoiceDialogue
                },
                new SearchTreeEntry(new GUIContent("Multiple Choice", _indentationIcon))
                {
                    level = 2,
                    userData = DSEntryType.MultipleChoiceDialogue
                },
                new SearchTreeGroupEntry(new GUIContent("Dialogue Group"), 1),
                new SearchTreeEntry(new GUIContent("Group", _indentationIcon))
                {
                    level = 2,
                    userData = DSEntryType.DialogueGroup
                },
            };

            return searchTreeEntries;
        }

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            Vector2 localMousePosition = _graphView.GetLocalMousePosition(context.screenMousePosition, true);

            switch ((DSEntryType)SearchTreeEntry.userData)
            {
                case DSEntryType.SingleChoiceDialogue:
                    DSSingleChoiceNode singleChoiceNode = _graphView.CreateNode("DialogueName", DSDialogueType.SingleChoice, Vector2.zero) as DSSingleChoiceNode;
                    _graphView.AddElement(singleChoiceNode);
                    return true;

                case DSEntryType.MultipleChoiceDialogue:
                    DSMultipleChoiceNode multChoiceNode = _graphView.CreateNode("DialogueName", DSDialogueType.MultipleChoice, Vector2.zero) as DSMultipleChoiceNode;
                    _graphView.AddElement(multChoiceNode);
                    return true;

                case DSEntryType.DialogueGroup:
                    _graphView.CreateGroup("DialogueGroup", localMousePosition);
                    return true;

                default:
                    return false;
            }
        }
    }
}