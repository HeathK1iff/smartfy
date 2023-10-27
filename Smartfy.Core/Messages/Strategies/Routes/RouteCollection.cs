using System.Text.RegularExpressions;

namespace Smartfy.Core.Messages.Strategies.Utils
{
    public class RouteCollection
    {
        private readonly IRouteRepository _routeRepository;

        public RouteCollection(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }

        public IEnumerable<Route> GetAll()
        {
            return _routeRepository.GetAll();
        }
        public void Add(Route route)
        {
            _routeRepository.Add(route);
        }

        public void AddRouteIfNotExist(Route route)
        {
            if (_routeRepository.GetAll().Any(f => f.Group.Equals(route.Group, StringComparison.InvariantCultureIgnoreCase)))
                return;

            Add(route);
        }

        public void Remove(Route route)
        {
            _routeRepository.Remove(route);
        }
    }
}