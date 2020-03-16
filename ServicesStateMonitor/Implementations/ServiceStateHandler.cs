using ServicesStateMonitor.Enums;
using ServicesStateMonitor.Interfaces;
using ServicesStateMonitor.Models;

namespace ServicesStateMonitor.Implementations
{
    public class ServiceStateHandler : IServiceStateHandler
    {
        private readonly ITriggerFactory _triggerFactory;

        public ServiceStateHandler(ITriggerFactory triggerFactory)
        {
            _triggerFactory = triggerFactory;
        }

        public void UpdateServiceState(Service service, Trigger trigger)
        {
            service.State = FirstIsWorse(trigger.ServiceState, service.State) ? trigger.ServiceState : service.State;

            if (trigger.ServiceState == ServiceState.AllRight)
            {
                service.ProblemList.RemoveWhere(name => name.Contains(trigger.Name));
                if (NoProblems(service))
                    service.State = ServiceState.AllRight;
                else if (NoOwnProblems(service))
                    service.State = ServiceState.AffectedByProblem;
            }
            else
            {
                service.ProblemList.Add(trigger.Name);
            }
        }

        private bool FirstIsWorse(ServiceState triggerState, ServiceState serviceState)
            => serviceState < triggerState;

        private bool NoProblems(Service service)
            => service.ProblemList.Count == 0;

        private bool NoOwnProblems(Service service)
        {
            bool result = true;
            foreach (string problem in service.ProblemList)
            {
                if (problem.Contains(_triggerFactory.GetWithOwnerPrefix(service.Name)))
                    result = false;
            }
            return result;
        }
    }
}
