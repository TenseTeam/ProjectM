namespace VUDK.Features.Packages.DialogueSystem.Editor.Elements
{
    using UnityEditor.Experimental.GraphView;
    using UnityEngine;
    using UnityEngine.UIElements;
    using VUDK.Features.Packages.DialogueSystem.Editor.Utilities;
    using VUDK.Features.Packages.DialogueSystem.Enums;

    public class DSMultipleChoiceNode : DSNode
    {
        public override void Init(Vector2 position)
        {
            base.Init(position);
            DialogueType = DSDialogueType.MultipleChoice;
            Choices.Add("New Choice");
        }

        public override void Draw()
        {
            base.Draw();

            #region MAIN CONTAINER

            Button addChoiceButton = DSElementUtility.CreateButton("Add Choice", () =>
            {
                Port choicePort = CreateChoicePort("New Choice");
                Choices.Add("New Choice");
                outputContainer.Add(choicePort);
            });
            addChoiceButton.AddToClassList("ds-node__button");

            mainContainer.Insert(1, addChoiceButton);

            #endregion MAIN CONTAINER

            #region OUTPUT CONTAINER

            foreach (string choice in Choices)
            {
                Port choicePort = CreateChoicePort(choice);
                outputContainer.Add(choicePort);
            }

            #endregion OUTPUT CONTAINER

            RefreshExpandedState();
        }

        private Port CreateChoicePort(string choice)
        {
            Port choicePort = this.CreatePort();

            Button deleteChoiceButton = DSElementUtility.CreateButton("x");
            deleteChoiceButton.AddToClassList("ds-node__button");

            TextField choiceTextField = DSElementUtility.CreateTextField(choice);

            choiceTextField.AddClasses
            (
                "ds-node__text-field",
                "ds-node__choice-text-field",
                "ds-node__text-field__hidden"
            );

            choicePort.Add(choiceTextField);
            choicePort.Add(deleteChoiceButton);
            return choicePort;
        }
    }
}