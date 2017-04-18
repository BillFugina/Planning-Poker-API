using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanningPoker.Model
{
    public class Participant
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public ParticipantRole Role { get; set; }

        public Participant(string name, ParticipantRole role)
        {
            Name = name;
            Role = role;
        }
    }

    public enum ParticipantRole
    {
        Observer = 0,
        Voter = 1,
        Master = 2
    }

    public class ParticipantApplication
    {
        public string Name { get; set; }
        public ParticipantRole Role { get; set; }
    }

    public class Session
    {
        private readonly List<Participant> _participants = new List<Participant>();
        private readonly Stack<Round> _rounds = new Stack<Round>();

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public Participant Master { get; set; }


        public IEnumerable<Participant> Participants => _participants.AsEnumerable();

        public Round CurrentRound => _rounds.Peek();

        public Session(string name, string masterName)
        {
            Name = name;
            var master = new Participant(masterName, ParticipantRole.Master);
            this.Master = master;
            _participants.Add(master);
            _rounds.Push(new Round()
            {
                Id = 0,
                State = RoundState.Null
            });
        }

        public void AddParticpant(Participant particpant)
        {
            _participants.Add(particpant);
        }

        public void AddRound(Round round)
        {
            _rounds.Push(round);
        }
    }

    public class SessionId
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
    }

    public class SessionApplication
    {
        public string SessionName { get; set; }
        public string MasterName { get; set; }
    }

    public class Round
    {
        private readonly IList<Vote> _votes = new List<Vote>();
        public int Id { get; set; }
        public RoundState State { get; set; }

        public IEnumerable<Vote> Votes => _votes;

        public DateTime End { get; set; }

        public void AddVote(Vote vote)
        {
            _votes.Add(vote);
        }
    }

    public enum RoundState
    {
        Null = 0,
        Pending = 1,
        Started = 2,
        Complete = 3
    }

    public class Vote
    {
        public Participant Participant { get; set; }
        public int Value { get; set; }
    }

    public class VoteBallot
    {
        public string ParticipantName { get; set; }
        public int Value { get; set; }
    }
}
