namespace Smartfy.Core.Messages.Strategies.Utils
{
    public interface IRouteRepository
    {
        void Add(Route route);
        void Remove(Route route);
        Route[] GetAll();
    }
}