namespace VUDK.Features.Packages.DialogueSystem.Editor.Elements
{
    using UnityEngine;
    using UnityEditor.Experimental.GraphView;
    using VUDK.Features.Packages.DialogueSystem.Editor.Utilities;
    using VUDK.Features.Packages.DialogueSystem.Enums;
    using VUDK.Features.Packages.DialogueSystem.Editor.Windows;
    using VUDK.Features.Packages.DialogueSystem.Editor.Data.Save;

    public class DSSingleChoiceNode : DSNode
    {
        public override void Init(Vector2 position, DSGraphView graphView)
        {
            base.Init(position, graphView);
            DialogueType = DSDialogueType.SingleChoice;

            DSChoiceSaveData dSChoiceSaveData = new DSChoiceSaveData()
            {
                Text = "Next Dialogue",
            };

            Choices.Add(dSChoiceSaveData);
        }

        public override void Draw()
        {
            base.Draw();

            #region OUTPUT CONTAINER

            foreach (DSChoiceSaveData choiceData in Choices)
            {
                Port choicePort = this.CreatePort(choiceData.Text);
                choicePort.userData = choiceData;
                outputContainer.Add(choicePort);
            }

            #endregion OUTPUT CONTAINER

            RefreshExpandedState();
        }
    }
}