using PlanningPoker.Model;

namespace PlanningPoker.Interfaces.Services
{
    public interface INotificationService
    {
        void RegisterVote(string sessionName, Vote vote);
        void RegisterParticipant(string sessionName, Participant participant);
        void StartSession(string sessionName);
        void StartRound(string sessionName, Round round);
    }
}