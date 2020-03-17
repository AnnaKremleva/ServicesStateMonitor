using ServicesStateMonitor.Enums;
using ServicesStateMonitor.Interfaces;
using ServicesStateMonitor.Models;
using System.Collections.Generic;

namespace ServicesStateMonitor.Implementations
{
    public class TriggerFactory : ITriggerFactory
    {
        private const string Separator = " <-- ";

        public Trigger GetFarewellTrigger(Service service)
            => new Trigger
            {
                Name = GetWithOwnerPrefix(service.Name),
                ServiceState = ServiceState.AllRight
            };

        public IEnumerable<Trigger> GetProblemTriggers(Service service)
        {
            foreach (string name in service.ProblemList)
            {
                yield return new Trigger()
                {
                    Name = name,
                    ServiceState = ServiceState.AffectedByProblem
                };
            }
        }

        public Trigger GetDependentTrigger(Service service, Trigger trigger)
            => new Trigger
            {
                Name = string.Concat(trigger.Name, GetWithOwnerPrefix(service.Name)),
                ServiceState = trigger.ServiceState == ServiceState.AllRight
                    ? ServiceState.AllRight
                    : ServiceState.AffectedByProblem
            };

        public Trigger GetUpdatedTrigger(Service service, Trigger trigger)
            => new Trigger
            {
                Name = string.Concat(trigger.Name, GetWithOwnerPrefix(service.Name)),
                ServiceState = trigger.ServiceState
            };

        public string GetWithOwnerPrefix(string name)
            => string.Concat(Separator, name);
    }
}