namespace VUDK.Patterns.Initialization.Interfaces
{
    public interface IInit
    {
        /// <summary>
        /// Initialize object.
        /// </summary>
        public void Init();

        /// <summary>
        /// Check object correct initialization.
        /// </summary>
        public bool Check();
    }
}
