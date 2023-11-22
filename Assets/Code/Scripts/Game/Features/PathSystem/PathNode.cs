namespace ProjectM.Features.PathSystem
{
    using System;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Generic.Managers.Main;
    using ProjectM.Constants;

    public class PathNode : MonoBehaviour
    {
        [Header("UI Settings")]
        [SerializeField]
        private Button _triggerButton;

        [Header("Path Node Settings")]
        [SerializeField]
        private PathLine _associatedPath;
        [SerializeField]
        private bool _isReversed;
        [SerializeField]
        private PathNode[] _nextNodes;

        private void OnEnable()
        {
            _triggerButton.onClick.AddListener(OnTriggerButtonClicked);
        }

        private void OnDisable()
        {
            _triggerButton.onClick.RemoveListener(OnTriggerButtonClicked);
        }

        private void OnTriggerButtonClicked()
        {
            Vector3[] positions = _associatedPath.GetPathPositions().ToArray();
            if (_isReversed) Array.Reverse(positions);

            MainManager.Ins.EventManager.TriggerEvent(GameEventKeys.OnPathTriggered, positions);
            EnableNextNodes();
        }

        private void EnableNextNodes()
        {
            gameObject.SetActive(false);
            foreach (PathNode node in _nextNodes)
                node.gameObject.SetActive(true);
        }
    }
}
