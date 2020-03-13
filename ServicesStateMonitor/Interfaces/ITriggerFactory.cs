using ServicesStateMonitor.Models;

namespace ServicesStateMonitor.Interfaces
{
    public interface ITriggerFactory
    {
        Trigger GetFarewellTrigger(Service service);

        Trigger GetDependentTrigger(Service service, Trigger trigger);

        Trigger GetUpdatedTrigger(Service service, Trigger trigger);

        string GetWithOwnerPrefix(string name);
    }
}