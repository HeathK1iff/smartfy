using Microsoft.Extensions.Logging;
using Smartfy.Core.Exceptions;

namespace Smartfy.Core.Services
{
    public class Services : IServiceCollection
    {
        private Dictionary<Type, IService> _services = new();
        private readonly ILogger _logger;
        public Services(ILogger logger)
        {
            _logger = logger;
        }


        public void AddService<T>(T service) where T : IService
        {
            if (!_services.ContainsKey(typeof(T)))
            {
                _services.Add(typeof(T), service);
            }
        }

        public T? GetService<T>() where T : IService
        {
            if (!_services.TryGetValue(typeof(T), out var service))
            {
                _logger.LogWarning($"Service {nameof(T)} is not found");
            }

            return (T) service ?? default(T);
        }

    }
}
