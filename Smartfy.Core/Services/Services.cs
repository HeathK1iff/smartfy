using Smartfy.Core.Exceptions;

namespace Smartfy.Core.Services
{
    public class Services : IServiceCollection
    {
        private Dictionary<Type, IService> _services = new();
     
        public void AddService<T>(T service) where T : IService
        {
            if (!_services.ContainsKey(typeof(T)))
            {
                _services.Add(typeof(T), service);
            }
        }

        public T GetService<T>() where T : IService
        {
            if (!_services.TryGetValue(typeof(T), out var service))
            {
                throw new ServiceNotFoundException($"Service {nameof(T)} is not found");
            }

            return (T)service;
        }

    }
}
