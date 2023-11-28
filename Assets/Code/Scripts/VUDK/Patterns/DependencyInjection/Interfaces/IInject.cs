namespace VUDK.Patterns.DependencyInjection.Interfaces
{
    public interface IInject<T>
    {
        /// <summary>
        /// Initialize object.
        /// </summary>
        /// <param name="dependency">Dependency references.</param>
        public void Inject(T dependency);

        /// <summary>
        /// Check object correct initialization.
        /// </summary>
        public bool Check();
    }
}
