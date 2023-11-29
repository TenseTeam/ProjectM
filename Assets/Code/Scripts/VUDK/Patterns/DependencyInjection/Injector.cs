namespace VUDK.Patterns.DependencyInjection
{
    using UnityEngine;
    using VUDK.Patterns.DependencyInjection.Interfaces;

    public abstract class Injector<T> : MonoBehaviour, IInjectPackage<T> where T : IDependencyPackage
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
