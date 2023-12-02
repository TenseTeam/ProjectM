namespace VUDK.Patterns.Initialization.Interfaces
{
    public interface IInject<T>
    {
        public void Inject(T dependency);
    }
}