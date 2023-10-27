namespace Smartfy.Core.Services.Tasks
{
    public interface ITaskService : IService, IEnumerable<ITask>
    {
        Guid Add(ITask task);
        void Execute(Guid id);
    }
}
