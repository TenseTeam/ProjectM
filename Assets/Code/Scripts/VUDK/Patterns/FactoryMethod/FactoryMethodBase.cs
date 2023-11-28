namespace VUDK.Patterns.FactoryMethod
{
    using UnityEngine;
    using VUDK.Patterns.FactoryMethod.Interface;

    public abstract class FactoryMethodBase : MonoBehaviour, IFactoryMethod
    {
        public abstract void Init();

        public virtual bool Check()
        {
            return true;
        }
    }
}
