using ServicesStateMonitor.Models;
using System.Collections.Generic;

namespace ServicesStateMonitor.Interfaces
{
    public interface ITriggerFactory
    {
        Trigger GetFarewellTrigger(Service service);

        IEnumerable<Trigger> GetProblemTriggers(Service service);

        Trigger GetDependentTrigger(Service service, Trigger trigger);

        Trigger GetUpdatedTrigger(Service service, Trigger trigger);

        string GetWithOwnerPrefix(string name);
    }
}