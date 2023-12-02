namespace VUDK.Patterns.Initialization.Interfaces
{
    public interface IInitPackage<T> : IInject<T> where T : IDependencyPackage
    {
    }
}
