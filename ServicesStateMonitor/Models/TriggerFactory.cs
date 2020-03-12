using ServicesStateMonitor.Interfaces;
using ServicesStateMonitor.Utils;

namespace ServicesStateMonitor.Models
{
    public class TriggerFactory : ITriggerFactory
    {
        public Trigger GetFarewellTrigger(Service serviceOwner)
            => new Trigger
            {
                Name = serviceOwner.Name,
                ServiceState = ServiceState.AllRight
            };

        public Trigger GetDependentTrigger(Service serviceOwner, Trigger trigger)
            => new Trigger
            {
                Name = string.Concat(trigger.Name, serviceOwner.Name.GetWithPrefix()),
                ServiceState = trigger.ServiceState == ServiceState.AllRight
                    ? ServiceState.AllRight
                    : ServiceState.AffectedByProblem
            };

        public Trigger GetUpdatedTrigger(Service serviceOwner, Trigger trigger)
            => new Trigger
            {
                Name = string.Concat(trigger.Name, serviceOwner.Name.GetWithPrefix()),
                ServiceState = trigger.ServiceState
            };
    }
}