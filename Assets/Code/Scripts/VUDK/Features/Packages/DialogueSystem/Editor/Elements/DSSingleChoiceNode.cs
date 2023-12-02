namespace VUDK.Features.Packages.DialogueSystem.Editor.Elements
{
    using UnityEngine;
    using UnityEditor.Experimental.GraphView;
    using VUDK.Features.Packages.DialogueSystem.Editor.Utilities;
    using VUDK.Features.Packages.DialogueSystem.Enums;

    public class DSSingleChoiceNode : DSNode
    {
        public override void Init(Vector2 position)
        {
            base.Init(position);
            DialogueType = DSDialogueType.SingleChoice;
            Choices.Add("Next Dialogue");
        }

        public override void Draw()
        {
            base.Draw();

            #region OUTPUT CONTAINER

            foreach (string choice in Choices)
            {
                Port choicePort = DSElementUtility.CreatePort(this, choice, Orientation.Horizontal, Direction.Output, Port.Capacity.Single);
                outputContainer.Add(choicePort);
            }

            #endregion OUTPUT CONTAINER

            RefreshExpandedState();
        }
    }
}