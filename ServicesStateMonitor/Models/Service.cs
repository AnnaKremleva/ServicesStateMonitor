using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesStateMonitor.Models
{
    public class Service
    {
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
