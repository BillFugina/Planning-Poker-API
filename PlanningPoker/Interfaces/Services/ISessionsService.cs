using System.Collections.Generic;
using PlanningPoker.Model;

namespace PlanningPoker.Services
{
    public interface ISessionsService
    {
        IEnumerable<Session> Sessions { get; }
        Session GetSession(string sessionName);
        Session CreateSession(string sessionName, string masterName);
        Session JoinSession(string sessionName, string participantName);
    }
}