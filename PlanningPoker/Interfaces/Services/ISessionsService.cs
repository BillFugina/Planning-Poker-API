using System;
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
        Session GetSession(Guid sessionId);
        Round StartRound(Guid sessionId);
        void Vote(string sessionName, string participantName, int round, int vote, bool allowOverwrite = false);
        void EndRound(Guid sessionId, int roundId);
        void EndSession(Guid sessionId);
    }
}