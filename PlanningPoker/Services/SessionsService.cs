using System;
using System.Collections.Generic;
using System.Linq;
using PlanningPoker.Model;
using PlanningPoker.System.Exceptions;
using PlanningPoker.Interfaces.Services;

namespace PlanningPoker.Services
{
    public class SessionsService : ISessionsService
    {
        private readonly List<Session> _sessions = new List<Session>();
        private readonly INotificationService _notificationService;

        public IEnumerable<Session> Sessions => _sessions.AsEnumerable();

        public SessionsService(
            INotificationService notificationService
            )
        {
            _notificationService = notificationService;
        }

        public Session GetSession(string sessionName)
        {
            var session = _sessions.SingleOrDefault(s => s.Name == sessionName);
            if (session == null)
            {
                throw new SessionMissingException();
            }
            return session;
        }

        public Session GetSession(Guid sessionId)
        {
            var session = _sessions.SingleOrDefault(s => s.Id == sessionId);
            if (session == null)
            {
                throw new SessionMissingException();
            }
            return session;
        }

        public Session  CreateSession(string sessionName, string masterName)
        {
            if (_sessions.Any(s => s.Name == sessionName))
            {
                throw new SessionClashException();
            }

            var result = new Session(sessionName, masterName);
            _sessions.Add(result);
            _notificationService.StartSession(sessionName);
            return result;
        }

        public Session JoinSession(string sessionName, string participantName)
        {
            var session = GetSession(sessionName);

            if (session.Participants.Any(p => p.Name == participantName))
            {
             throw new ParticipantClashException();   
            }

            var participant = new Participant(participantName, ParticipantRole.Voter);
            session.AddParticpant(participant);
            _notificationService.RegisterParticipant(sessionName, participant);
            return session;
        }

        public Round StartRound(Guid sessionId)
        {
            var session = GetSession(sessionId);
            if (session.CurrentRound.State != RoundState.Null && session.CurrentRound.State != RoundState.Complete)
            {
                throw new RoundClashException();
            }

            var round = new Round()
            {
                Id = session.CurrentRound.Id + 1,
                State = RoundState.Started,
                End = DateTime.Now.ToUniversalTime().AddSeconds(10)
            };
            session.AddRound(round);
            _notificationService.StartRound(session.Name, round);
            return round;
        }

        public void Vote(string sessionName, string participantName, int round, int vote, bool allowOverwrite = false)
        {
            var session = GetSession(sessionName);
            var currentRound = session.CurrentRound;

            if (currentRound.Id != round)
            {
                throw new IncorrectRoundException("Round does not exist.");
            }

            if (session.CurrentRound.State != RoundState.Started)
            {
                throw new IncorrectRoundException("Round has not started.");
            }

            var participant = session.Participants.SingleOrDefault(v => v.Name == participantName);
            if (participant == null)
            {
                throw new MissingMemberException();
            }

            if (!allowOverwrite && currentRound.Votes.Any(v => v.Participant.Name == participantName))
            {
                throw new VoteClashException();
            }

            var newVote = new Vote()
            {
                Participant = participant,
                Value = vote
            };

            currentRound.AddVote(newVote);
            _notificationService.RegisterVote(session.Name, newVote);
        }

        public void EndRound(Guid sessionId, int roundId)
        {
            var session = GetSession(sessionId);
            var currentRound = session.CurrentRound;

            if (currentRound.Id != roundId)
            {
                throw new IncorrectRoundException();
            }

            if (currentRound.State != RoundState.Complete)
            {
                throw new IncorrectRoundException();
            }

            currentRound.State = RoundState.Complete;
        }

        public void EndSession(Guid sessionId)
        {
            var session = GetSession(sessionId);
            _sessions.Remove(session);
        }
    }
}
