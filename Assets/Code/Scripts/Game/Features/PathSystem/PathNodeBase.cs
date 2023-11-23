namespace ProjectM.Features.PathSystem
{
    using UnityEngine;
    using UnityEngine.UI;

    public abstract class PathNodeBase : MonoBehaviour
    {
        [field: Header("UI Settings")]
        [field: SerializeField]
        protected Button TriggerButton { get ; private set; }

        protected virtual void OnEnable()
        {
            TriggerButton.onClick.AddListener(TargetNode);
        }

        protected virtual void OnDisable()
        {
            TriggerButton.onClick.RemoveListener(TargetNode);
        }

        public abstract void TargetNode();
    }
}
