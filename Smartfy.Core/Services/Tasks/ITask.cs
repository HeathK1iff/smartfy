using Microsoft.Extensions.Logging;

namespace Smartfy.Core.Services.Tasks
{
    public interface ITask
    {
        bool Prepare(Action? executeAction, ILoggerFactory logger);
        void Exeсute(IServiceCollection services, ref bool success);
    }
}
