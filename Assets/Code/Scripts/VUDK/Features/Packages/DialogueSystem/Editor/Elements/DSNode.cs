namespace VUDK.Features.Packages.DialogueSystem.Editor.Elements
{
    using System.Collections.Generic;
    using UnityEditor.Experimental.GraphView;
    using UnityEngine;
    using UnityEngine.UIElements;
    using VUDK.Features.Packages.DialogueSystem.Enums;
    using VUDK.Patterns.DependencyInjection.Interfaces;

    public class DSNode : Node, IInject<Vector2>
    {
        public string DialogueName;
        public string DialogueText;
        public List<string> Choices;
        public DSDialogueType DialogueType;

        public static DSNode Create(Vector2 position)
        {
            DSNode node = new DSNode();
            node.Inject(position);
            node.Draw();
            return node;
        }

        public virtual void Inject(Vector2 position)
        {
            Choices = new List<string>();
            DialogueName = "DialogueName";
            DialogueText = "Dialogue text.";
            SetPosition(new Rect(position, Vector2.zero));
        }

        public bool Check()
        {
            return DialogueName != null && DialogueText != null && Choices != null;
        }

        public virtual void Draw()
        {
            #region TITLE CONTAINER

            TextField dialogueNameTextField = new TextField()
            {
                value = DialogueName
            };
            titleContainer.Insert(0, dialogueNameTextField);

            #endregion TITLE CONTAINER

            #region INPUT CONTAINER

            Port inputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));
            inputPort.portName = "Dialogue Connection";
            inputContainer.Add(inputPort);

            #endregion INPUT CONTAINER

            #region EXTENSIONS CONTAINER

            VisualElement customDataContainer = new VisualElement();

            Foldout textFoldout = new Foldout()
            {
                text = "Dialogue Text",
            };

            TextField textTextField = new TextField()
            {
                value = DialogueText
            };

            textFoldout.Add(textTextField);
            customDataContainer.Add(textFoldout);
            extensionContainer.Add(customDataContainer);

            #endregion EXTENSIONS CONTAINER

            RefreshExpandedState();
        }
    }
}