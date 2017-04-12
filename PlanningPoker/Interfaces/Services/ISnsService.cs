using System.Threading.Tasks;
using Amazon.SimpleNotificationService.Model;

namespace PlanningPoker.Interfaces.Services
{
    public interface ISnsService
    {
        Task<CreateTopicResponse> CreateSession(string sessionName);
    }
}