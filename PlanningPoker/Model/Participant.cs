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
        public ParticpantRole Role { get; set; }

        public Participant(string name, ParticpantRole role)
        {
            
        }
    }

    public enum ParticpantRole
    {
        Observer = 0,
        Voter = 1,
        Master = 2
    }
}
