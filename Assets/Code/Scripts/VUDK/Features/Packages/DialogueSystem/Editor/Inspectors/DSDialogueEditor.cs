namespace VUDK.Features.Packages.DialogueSystem.Editor.Inspectors
{
    using System.Collections.Generic;
    using UnityEditor;
    using VUDK.Features.Packages.DialogueSystem.Data;
    using VUDK.Features.Packages.DialogueSystem.Editor.Utilities;

    [CustomEditor(typeof(DSDialogue))]
    public class DSDialogueEditor : Editor
    {
        private SerializedProperty _dialogueContainerProperty;
        private SerializedProperty _dialogueGroupProperty;
        private SerializedProperty _dialogueProperty;

        private SerializedProperty _groupedDialoguesProperty;
        private SerializedProperty _startingDialoguesOnlyProperty;

        private SerializedProperty _selectedDialogueGroupIndexProperty;
        private SerializedProperty _selectedDialogueIndexProperty;

        private void OnEnable()
        {
            _dialogueContainerProperty = serializedObject.FindProperty("_dialogueContainer");
            _dialogueGroupProperty = serializedObject.FindProperty("_dialogueGroup");
            _dialogueProperty = serializedObject.FindProperty("_dialogue");

            _groupedDialoguesProperty = serializedObject.FindProperty("_groupedDialogues");
            _startingDialoguesOnlyProperty = serializedObject.FindProperty("_startingDialoguesOnly");

            _selectedDialogueGroupIndexProperty = serializedObject.FindProperty("_selectedDialogueGroupIndex");
            _selectedDialogueIndexProperty = serializedObject.FindProperty("_selectedDialogueIndex");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawDialogueContainerArea();

            DSDialogueContainerData dialogueContainer = _dialogueContainerProperty.objectReferenceValue as DSDialogueContainerData;

            if (_dialogueContainerProperty.objectReferenceValue == null)
            {
                StopDrawing("Please assign a Dialogue Container.");
                return;
            }

            DrawFiltersArea();

            bool currentStartingDialoguesOnlyFilter = _startingDialoguesOnlyProperty.boolValue;

            List<string> dialogueNames;
            string dialogueFolderPath = $"Assets/DialogueSystem/Dialogues/{dialogueContainer.FileName}";
            string dialogueInfoMessage;

            if(_groupedDialoguesProperty.boolValue)
            {
                List<string> dialogueGroupNames = dialogueContainer.GetDialogueGroupNames();

                if (dialogueGroupNames.Count == 0)
                {
                    StopDrawing("There are no Dialogue Groups in the Dialogue Container.");
                    return;
                }

                DrawDialogueGroupArea(dialogueContainer, dialogueGroupNames);

                DSDialogueGroupData dialogueGroup = _dialogueGroupProperty.objectReferenceValue as DSDialogueGroupData;
                dialogueNames = dialogueContainer.GetGroupedDialogueNames(dialogueGroup, currentStartingDialoguesOnlyFilter);
                dialogueFolderPath += $"/Groups/{dialogueGroup.GroupName}/Dialogues";
                dialogueInfoMessage = $"There are no" + (currentStartingDialoguesOnlyFilter ? " Starting" : "") + "Dialogues in the Dialogue Group {dialogueGroup.GroupName}.";
            }
            else
            {
                dialogueNames = dialogueContainer.GetUngroupedDialogueNames(currentStartingDialoguesOnlyFilter);
                dialogueFolderPath += "/Global/Dialogues";
                dialogueInfoMessage = "There are no" + (currentStartingDialoguesOnlyFilter ? " Starting" : "") + " Dialogues in the Dialogue Container.";
            }

            if(dialogueNames.Count == 0)
            {
                StopDrawing(dialogueInfoMessage);
                return;
            }

            DrawDialogueArea(dialogueNames, dialogueFolderPath);
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawDialogueContainerArea()
        {
            DSInspectorUtility.DrawHeader("Dialogue Container");

            _dialogueContainerProperty.DrawPropertyFiled();
            DSInspectorUtility.DrawSpace();
        }

        private void DrawFiltersArea()
        {
            DSInspectorUtility.DrawHeader("Filters");

            _groupedDialoguesProperty.DrawPropertyFiled();
            _startingDialoguesOnlyProperty.DrawPropertyFiled();
            DSInspectorUtility.DrawSpace();
        }

        private void DrawDialogueGroupArea(DSDialogueContainerData dialogueContainer, List<string> dialogueGroupNames)
        {
            DSInspectorUtility.DrawHeader("Dialogue Group");

            int oldSelectedDialogueGroupIndex = _selectedDialogueGroupIndexProperty.intValue;
            DSDialogueGroupData oldDialogueGroup = _dialogueGroupProperty.objectReferenceValue as DSDialogueGroupData;

            bool isOldDialogueGroupNull = oldDialogueGroup == null;
            string oldDialogueGroupName = isOldDialogueGroupNull ? string.Empty : oldDialogueGroup.GroupName;
            UpdateIndexOnNamesListUpdate(dialogueGroupNames, _selectedDialogueGroupIndexProperty, oldSelectedDialogueGroupIndex, oldDialogueGroupName, isOldDialogueGroupNull);

            _selectedDialogueGroupIndexProperty.intValue = DSInspectorUtility.DrawPopup("Dialogue Group", _selectedDialogueGroupIndexProperty, dialogueGroupNames.ToArray());

            string selectedDialogueGroupName = dialogueGroupNames[_selectedDialogueGroupIndexProperty.intValue];
            DSDialogueGroupData selectedDialogueGroup = DSIOUtility.LoadAsset<DSDialogueGroupData>($"Assets/DialogueSystem/Dialogues/{dialogueContainer.FileName}/Groups/{selectedDialogueGroupName}", selectedDialogueGroupName);

            _dialogueGroupProperty.objectReferenceValue = selectedDialogueGroup;

            DSInspectorUtility.DrawDisabledFields(() =>
            {
                _dialogueGroupProperty.DrawPropertyFiled();
            });

            DSInspectorUtility.DrawSpace();
        }

        private void DrawDialogueArea(List<string> dialogueNames, string dialogueFolderPath)
        {
            DSInspectorUtility.DrawHeader("Dialogue");

            int oldSelectedDialogueIndex = _selectedDialogueIndexProperty.intValue;

            DSDialogueData oldDialogue = _dialogueProperty.objectReferenceValue as DSDialogueData;

            bool isOldDialogueNull = oldDialogue == null;
            string oldDialogueName = isOldDialogueNull ? string.Empty : oldDialogue.DialogueName;

            UpdateIndexOnNamesListUpdate(dialogueNames, _selectedDialogueIndexProperty, oldSelectedDialogueIndex, oldDialogueName, isOldDialogueNull);

            _selectedDialogueIndexProperty.intValue = DSInspectorUtility.DrawPopup("Dialogue", _selectedDialogueIndexProperty, dialogueNames.ToArray());

            string selectedDialogueName = dialogueNames[_selectedDialogueIndexProperty.intValue];

            DSDialogueData selectedDialogue = DSIOUtility.LoadAsset<DSDialogueData>(dialogueFolderPath, selectedDialogueName);
            _dialogueProperty.objectReferenceValue = selectedDialogue;

            DSInspectorUtility.DrawDisabledFields(() =>
            {
                _dialogueProperty.DrawPropertyFiled();
            });

            DSInspectorUtility.DrawSpace();
        }

        private void StopDrawing(string reason, MessageType messageType = MessageType.Info)
        {
            DSInspectorUtility.DrawHelpBox(reason, messageType);
            DSInspectorUtility.DrawSpace();
            DSInspectorUtility.DrawHelpBox("You need to seleect a Dialogue for this component to work properly at Runtime!", MessageType.Warning);
            serializedObject.ApplyModifiedProperties();
        }

        private void UpdateIndexOnNamesListUpdate(List<string> optionNames, SerializedProperty indexProperty ,int oldSelectedPropertyIndex, string oldPropertyName, bool isOldPropertyNull)
        {
            if (isOldPropertyNull)
            {
                indexProperty.intValue = 0;
                return;
            }

            bool oldIndexIsOutOfBoundsOfNamesListCount = oldSelectedPropertyIndex > optionNames.Count - 1;
            bool oldNameIsDifferentThanSelectedName = oldIndexIsOutOfBoundsOfNamesListCount || oldPropertyName != optionNames[oldSelectedPropertyIndex];

            if (oldNameIsDifferentThanSelectedName)
            {
                if (optionNames.Contains(oldPropertyName))
                {
                    indexProperty.intValue = optionNames.IndexOf(oldPropertyName);
                }
                else
                {
                    indexProperty.intValue = 0;
                }
            }
        }
    }
}
