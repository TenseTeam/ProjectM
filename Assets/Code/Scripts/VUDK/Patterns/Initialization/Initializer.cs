namespace VUDK.Patterns.Initialization
{
    using UnityEngine;
    using VUDK.Patterns.Initialization.Interfaces;

    public abstract class Initializer : MonoBehaviour, IInit
    {
        public abstract void Init();

        public abstract bool Check();
    }
}