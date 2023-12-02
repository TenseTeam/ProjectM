namespace VUDK.Patterns.Initialization
{
    using UnityEngine;
    using VUDK.Patterns.Initialization.Interfaces;

    public abstract class Initializer : MonoBehaviour, IInit
    {
        public abstract void Init();

        public abstract bool Check();
    }

    public abstract class Initializer<T> : MonoBehaviour, IInitPackage<T> where T : IDependencyPackage
    {
        protected T Dependency;

        public void Inject(T dependency)
        {
            Dependency = dependency;
        }

        public virtual bool Check()
        {
            return Dependency != null;
        }
    }
}