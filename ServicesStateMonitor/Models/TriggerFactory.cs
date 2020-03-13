using ServicesStateMonitor.Enums;
using ServicesStateMonitor.Interfaces;

namespace ServicesStateMonitor.Models
{
    public class TriggerFactory : ITriggerFactory
    {
        private const string Separator = " <-- ";

        public Trigger GetFarewellTrigger(Service service)
            => new Trigger
            {
                Name = GetWithPrefix(service.Name),
                ServiceState = ServiceState.AllRight
            };

        public Trigger GetDependentTrigger(Service service, Trigger trigger)
            => new Trigger
            {
                Name = string.Concat(trigger.Name, GetWithPrefix(service.Name)),
                ServiceState = trigger.ServiceState == ServiceState.AllRight
                    ? ServiceState.AllRight
                    : ServiceState.AffectedByProblem
            };

        public Trigger GetUpdatedTrigger(Service service, Trigger trigger)
            => new Trigger
            {
                Name = string.Concat(trigger.Name, GetWithPrefix(service.Name)),
                ServiceState = trigger.ServiceState
            };

        public string GetWithPrefix(string name)
            => string.Concat(Separator, name);
    }
}