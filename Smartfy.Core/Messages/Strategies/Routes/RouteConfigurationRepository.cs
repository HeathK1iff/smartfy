using System.Configuration;
using Smartfy.Core.Exceptions;
using Smartfy.Core.Utils;

namespace Smartfy.Core.Messages.Strategies.Utils
{
    public class RouteConfigurationRepository : IRouteRepository
    {
        private static string SectionName = "routes";
        private readonly Configuration _configuration;
        private RoutesConfiguration _section;

        public RouteConfigurationRepository(Configuration configuration) 
        {
            _configuration = configuration;
            _section = _configuration.GetOrCreateSection<RoutesConfiguration>(SectionName, item =>
            {
                item.Groups.Add(new GroupElement()
                {
                    Group = "*",
                    Recepients = "*"
                }); 
            });
        }

        public void Add(Route route)
        {
            GroupElement group = _section.Groups.GetGroup(route.Group);
            if (group != null) 
            {
                var list = new List<string>(group.Recepients.Split(','));
                string recepient = route.Recepient.Trim();
                if (!list.Any(item => item.Equals(recepient, StringComparison.OrdinalIgnoreCase)))
                {
                    list.Add(recepient);
                }
                group.Recepients = string.Join(',', list);
            }
            else
            {
                var item = new GroupElement();
                item.Group = route.Group;
                item.Recepients = route.Recepient.Trim();
                _section.Groups.Add(item);
            }

            _configuration.Save();
        }

        public Route[] GetAll()
        {
            var list = new List<Route>();
            
            foreach (GroupElement group in _section.Groups)
            {
                foreach (string recepient in group.Recepients.Split(','))
                {
                    var route = new Route()
                    {
                        Group = group.Group,
                        Recepient = recepient.Trim()
                    };

                    list.Add(route);
                }    
            }

            return list.ToArray();
        }

        public void Remove(Route route)
        {
            GroupElement group = _section.Groups.GetGroup(route.Group);
            if (group != null)
            {
                var list = new List<string>(group.Recepients.Split(','));
                string recepient = route.Recepient.Trim();
                if (!list.Any(item => item.Equals(recepient, StringComparison.OrdinalIgnoreCase)))
                {
                    list.Remove(recepient);
                }
                group.Recepients = string.Join(',', list);
                _configuration.Save();
                
                return;
            }

            throw new GroupNotFoundException(route.Group);
        }
    }
}