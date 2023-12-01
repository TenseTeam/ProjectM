namespace VUDK.Features.Packages.DialogueSystem.Editor.Elements
{
    using System;
    using System.Collections.Generic;
    using UnityEditor.Experimental.GraphView;
    using UnityEngine;
    using UnityEngine.UIElements;
    using VUDK.Features.Packages.DialogueSystem.Editor.Utilities;
    using VUDK.Features.Packages.DialogueSystem.Enums;

    public class DSNode : Node
    {
        public string DialogueName;
        public string DialogueText;
        public List<string> Choices;
        public DSDialogueType DialogueType;

        public static DSNode Create(DSDialogueType dialogueType, Vector2 position)
        {
            Type nodeType = Type.GetType($"VUDK.Features.Packages.DialogueSystem.Editor.Elements.DS{dialogueType}Node");
            DSNode node = Activator.CreateInstance(nodeType) as DSNode;
            node.Init(position);
            node.Draw();
            return node;
        }

        public virtual void Init(Vector2 position)
        {
            Choices = new List<string>();
            DialogueName = "DialogueName";
            DialogueText = "Dialogue text.";
            SetPosition(new Rect(position, Vector2.zero));
            mainContainer.AddToClassList("ds-node__main-container");
            extensionContainer.AddToClassList("ds-node__extension-container");
        }

        public virtual void Draw()
        {
            #region TITLE CONTAINER

            TextField dialogueNameTextField = DSElementUtility.CreateTextField(DialogueName);

            dialogueNameTextField.AddClasses
            (
                "ds-node__text-field", 
                "ds-node__filename-text-field",
                "ds-node__text-field__hidden"
            );

            titleContainer.Insert(0, dialogueNameTextField);

            #endregion TITLE CONTAINER

            #region INPUT CONTAINER

            Port inputPort = DSElementUtility.CreatePort(this, "Dialogue Input", Orientation.Horizontal, Direction.Input, Port.Capacity.Single);
            inputContainer.Add(inputPort);

            #endregion INPUT CONTAINER

            #region EXTENSIONS CONTAINER

            VisualElement customDataContainer = new VisualElement();

            customDataContainer.AddToClassList("ds-node__custom-data-container");

            Foldout textFoldout = DSElementUtility.CreateFoldout("Dialogue Text", true);
            TextField textTextField = DSElementUtility.CreateTextArea(DialogueText);

            textTextField.AddClasses
            (
                "ds-node__text-field",
                "ds-node__text-field__hidden"
            );

            textFoldout.Add(textTextField);
            customDataContainer.Add(textFoldout);
            extensionContainer.Add(customDataContainer);

            #endregion EXTENSIONS CONTAINER
        }
    }
}