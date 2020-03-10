using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServicesStateMonitor.Models
{
    public class Service
    {
        [Required]
        public string Name { get; set; }

        public ServiceState State { get; set; } = ServiceState.AllRight;

        public List<string> EssentialLinks { get; set; } = new List<string>();

        public HashSet<string> ProblemList { get; set; } = new HashSet<string>();

        public HashSet<Service> Dependents { get; set; } = new HashSet<Service>();

        public HashSet<Service> DependFrom { get; set; } = new HashSet<Service>();

        public void UpdateState(Trigger trigger)
        {
            State = IsWorse(trigger.ServiceState) ? trigger.ServiceState : State;

            if (trigger.ServiceState == ServiceState.AllRight)
            {
                ProblemList.RemoveWhere(name => name.Contains(trigger.Name));
                if (NoProblems())
                    State = ServiceState.AllRight;
            }
            else
            {
                ProblemList.Add(trigger.Name);
            }
        }

        private bool IsWorse(ServiceState triggerState)
            => State < triggerState;

        private bool NoProblems()
            => ProblemList.Count == 0;
    }
}