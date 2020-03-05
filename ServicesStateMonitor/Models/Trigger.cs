using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesStateMonitor.Models
{
    public class Trigger
    {
        public string Name { get; set; }

        public ServiceState ServiceState { get; set; } = ServiceState.HasProblems;

        public TriggerState State { get; set; } = TriggerState.OwnerError;

        public Service FindOwner()
            => null; //TODO need real
    }
}
