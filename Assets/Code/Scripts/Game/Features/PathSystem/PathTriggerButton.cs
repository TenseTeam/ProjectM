namespace ProjectM.Features.PathSystem
{
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Generic.Managers.Main;
    using ProjectM.Constants;

    [RequireComponent(typeof(Button))]
    public class PathTriggerButton : MonoBehaviour
    {
        [SerializeField]
        private PathLine _pathLine;

        private void OnEnable()
        {
            TryGetComponent(out Button button);
            button.onClick.AddListener(TriggerSelectedPath);
        }

        private void TriggerSelectedPath()
        {
            MainManager.Ins.EventManager.TriggerEvent(GameEventKeys.OnPathTriggered, _pathLine.GetPathPositions());
        }
    }
}
