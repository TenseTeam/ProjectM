namespace VUDK.Patterns.DependencyInjection.Interfaces
{
    public interface IInjectPackage<T> : IInject<T> where T : IDependencyPackage
    {
    }
}
