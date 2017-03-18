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
}
