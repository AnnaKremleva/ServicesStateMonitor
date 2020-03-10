using ServicesStateMonitor.Interfaces;

namespace ServicesStateMonitor.Models
{
    public class TriggerFactory : ITriggerFactory
    {
        private const string Separator = " <-- ";

        public Trigger GetFarewellTrigger(Service serviceOwner)
            => new Trigger()
            {
                Name = serviceOwner.Name,
                ServiceState = ServiceState.AllRight
            };

        public Trigger GetDependentTrigger(Service serviceOwner, Trigger trigger)
            => new Trigger
            {
                Name = string.Concat(trigger.Name, Separator, serviceOwner.Name),
                ServiceState = trigger.ServiceState == ServiceState.AllRight
                    ? ServiceState.AllRight
                    : ServiceState.AffectedByProblem
            };
    }
}