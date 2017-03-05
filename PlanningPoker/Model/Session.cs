using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanningPoker.Model
{
    public class Session
    {
        private readonly List<Participant> _participants = new List<Participant>();
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public Participant Master { get; set; }

        public IEnumerable<Participant> Participants => _participants.AsEnumerable();

        public Session(string name, string masterName)
        {
            Name = name;
            var master = new Participant(masterName, ParticpantRole.Master);
            _participants.Add(master);
        }

        public void AddParticpant(Participant particpant)
        {
            _participants.Add(particpant);
        }
    }

    public class SessionId
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
    }

    public class Round
    {
        public int Id { get; set; }
        public RoundState State { get; set; }
        public IEnumerable<Vote> Votes { get; } = new List<Vote>();
    }

    public enum RoundState
    {
        Pending = 0,
        Started = 1,
        Closed = 2
    }

    public class Vote
    {
        public Participant Participant { get; set; }
        public int Value { get; set; }
    }
}
