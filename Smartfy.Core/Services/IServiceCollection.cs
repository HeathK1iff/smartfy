namespace Smartfy.Core.Services
{
    public interface IServiceCollection
    {
        void AddService<T>(T service) where T : IService;
        T? GetService<T>() where T : IService;
    }
}
