using System.Threading.Tasks;
using Amazon.SimpleNotificationService.Model;

namespace PlanningPoker.Interfaces.Services
{
    public interface ISnsService
    {
        Task<PublishResponse> RegisterVote(string sessionArn, string message);
        Task<DeleteTopicResponse> DeleteSession(string sessionName);
        Task<CreateTopicResponse> CreateSession(string sessionName);
    }
}