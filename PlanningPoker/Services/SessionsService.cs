using System.Collections.Generic;
using System.Linq;
using PlanningPoker.Model;
using PlanningPoker.System.Exceptions;

namespace PlanningPoker.Services
{
    public class SessionsService : ISessionsService
    {
        private readonly List<Session> _sessions = new List<Session>();

        public IEnumerable<Session> Sessions => _sessions.AsEnumerable();

        public Session GetSession(string sessionName)
        {
            return _sessions.Single(s => s.Name == sessionName);
        }

        public Session  CreateSession(string sessionName, string masterName)
        {
            if (_sessions.Any(s => s.Name == sessionName))
            {
                throw new SessionClashException();
            }

            var result = new Session(sessionName, masterName);
            _sessions.Add(result);
            return result;
        }

        public Session JoinSession(string sessionName, string participantName)
        {
            var session = _sessions.SingleOrDefault(s => s.Name == sessionName);
            if (session == null)
            {
                throw new SessionMissingException();
            }

            if (session.Participants.Any(p => p.Name == participantName))
            {
             throw new ParticipantClashException();   
            }

            var particpant = new Participant(participantName, ParticpantRole.Voter);
            session.AddParticpant(particpant);
            return session;
        }
    }
}
