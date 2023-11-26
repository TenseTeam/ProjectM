namespace VUDK.Patterns.Pooling
{
    using UnityEngine;
    using VUDK.Generic.Serializable;

    [DefaultExecutionOrder(-100)]
    public sealed class PoolsManager : MonoBehaviour
    {
        [field: SerializeField]
        public SerializableDictionary<PoolKeys, GameObjectPool> Pools { get; private set; }
    }
}