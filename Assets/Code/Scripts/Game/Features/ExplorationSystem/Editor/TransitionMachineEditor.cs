namespace ProjectM.Features.ExplorationSystem.Editor
{
    using UnityEditor;
    using ProjectM.Features.ExplorationSystem.Transition.Types.Keys;
    using ProjectM.Features.ExplorationSystem.Transition;

    [CustomEditor(typeof(TransitionMachine), true)]
    public class TransitionMachineEditor : Editor
    {
        private SerializedProperty _transitionTypeProperty;
        private SerializedProperty _timeProcessProperty;
        private SerializedProperty _fovChangerProperty;

        private void OnEnable()
        {
            _transitionTypeProperty = serializedObject.FindProperty("_transitionType");
            _timeProcessProperty = serializedObject.FindProperty("_timeProcess");
            _fovChangerProperty = serializedObject.FindProperty("_fovChanger");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_transitionTypeProperty);

            TransitionType transitionType = (TransitionType)_transitionTypeProperty.enumValueIndex;
            ShowCorrectFields(transitionType);

            serializedObject.ApplyModifiedProperties();
        }

        private void ShowCorrectFields(TransitionType transitionType)
        {
            switch (transitionType)
            {
                case TransitionType.Fov:
                    EditorGUILayout.PropertyField(_timeProcessProperty);
                    EditorGUILayout.PropertyField(_fovChangerProperty);
                    break;

                case TransitionType.Linear:
                    EditorGUILayout.PropertyField(_timeProcessProperty);
                    break;

                default:
                    break;
            }
        }
    }
}