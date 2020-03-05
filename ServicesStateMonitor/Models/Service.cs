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

        public List<Service> Dependents { get; set; } = new List<Service>();
    }
}
