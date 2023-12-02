namespace VUDK.Features.Packages.DialogueSystem.Editor.Elements
{
    using UnityEditor.Experimental.GraphView;
    using UnityEngine;
    using UnityEngine.UIElements;
    using VUDK.Features.Packages.DialogueSystem.Data;
    using VUDK.Features.Packages.DialogueSystem.Editor.Data.Save;
    using VUDK.Features.Packages.DialogueSystem.Editor.Utilities;
    using VUDK.Features.Packages.DialogueSystem.Editor.Windows;
    using VUDK.Features.Packages.DialogueSystem.Enums;

    public class DSMultipleChoiceNode : DSNode
    {
        public override void Init(Vector2 position, DSGraphView graphView)
        {
            base.Init(position, graphView);
            DialogueType = DSDialogueType.MultipleChoice;

            DSChoiceSaveData dsChoiceData = new DSChoiceSaveData()
            {
                Text = "New Choice",
            };

            Choices.Add(dsChoiceData);
        }

        public override void Draw()
        {
            base.Draw();

            #region MAIN CONTAINER

            Button addChoiceButton = DSElementUtility.CreateButton("Add Choice", () =>
            {
                DSChoiceSaveData dsChoiceData = new DSChoiceSaveData()
                {
                    Text = "New Choice",
                };
                Choices.Add(dsChoiceData);

                Port choicePort = CreateChoicePort(dsChoiceData);
                outputContainer.Add(choicePort);
            });

            addChoiceButton.AddToClassList("ds-node__button");

            mainContainer.Insert(1, addChoiceButton);

            #endregion MAIN CONTAINER

            #region OUTPUT CONTAINER

            foreach (DSChoiceSaveData choiceData in Choices)
            {
                Port choicePort = CreateChoicePort(choiceData);
                outputContainer.Add(choicePort);
            }

            #endregion OUTPUT CONTAINER

            RefreshExpandedState();
        }

        private Port CreateChoicePort(object userData)
        {
            Port choicePort = this.CreatePort();
            choicePort.userData = userData as DSChoiceSaveData;
            DSChoiceSaveData choiceData = choicePort.userData as DSChoiceSaveData;

            Button deleteChoiceButton = DSElementUtility.CreateButton("X", () =>
            {
                if (Choices.Count == 1) return;

                if (choicePort.connected)
                    GraphView.DeleteElements(choicePort.connections);

                Choices.Remove(choiceData);
                GraphView.RemoveElement(choicePort);
            });

            deleteChoiceButton.AddToClassList("ds-node__button");

            TextField choiceTextField = DSElementUtility.CreateTextField(choiceData.Text, null, callback =>
            {
                choiceData.Text = callback.newValue;
            });

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