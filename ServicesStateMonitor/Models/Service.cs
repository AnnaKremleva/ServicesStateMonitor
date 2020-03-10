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

        public List<Trigger> Triggers { get; set; } = new List<Trigger>();

        public List<string> ProblemList { get; set; } = new List<string>();

        public List<Service> Dependents { get; set; } = new List<Service>();

        public bool Attended { get; set; }

        public void UpdateState(Trigger trigger)
        {
            //TODO update
        }
    }
}