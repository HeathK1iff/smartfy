namespace Smartfy.Core.Services.Tasks
{
    public interface ITaskService : IService
    {
        void Add(ITask task);
        void Start();
    }
}
