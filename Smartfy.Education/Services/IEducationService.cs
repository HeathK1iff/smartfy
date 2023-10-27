using Smartfy.Core.Services;

namespace Smartfy.Education.Services
{
    public interface IEducationService : IService
    {
        List<(string Subject, string Mark)> GetDailyMarks();
    }
}
