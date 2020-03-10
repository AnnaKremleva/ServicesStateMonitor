using ServicesStateMonitor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesStateMonitor.Models
{
    public class TriggerFactory : ITriggerFactory
    {
        public Trigger GetTrigger(string message)
            => new Trigger(); //TODO create real
    }
}
