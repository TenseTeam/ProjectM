namespace VUDK.Patterns.Pooling
{
    using UnityEngine;
    using VUDK.Patterns.Pooling.Interfaces;

    /// <summary>
    /// A fundamental class for objects eligible for pooling.
    /// Inherit from this class or implement the <see cref="IPooledObject"/> interface to suit your requirements.
    /// </summary>
    public abstract class PooledObject : MonoBehaviour, IPooledObject
    {
        public GameObjectPool RelatedPool { get; private set; }

        /// <inheritdoc/>
        public void AssociatePool(GameObjectPool associatedPool)
        {
            RelatedPool = associatedPool;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            RelatedPool.Dispose(gameObject);
        }

        /// <inheritdoc/>
        public virtual void Clear()
        {
        }
    }
}
