using ServicesStateMonitor.Models;

namespace ServicesStateMonitor.Interfaces
{
    public interface ITriggerFactory
    {
        Trigger GetFarewellTrigger(Service serviceOwner);

        Trigger GetDependentTrigger(Service serviceOwner, Trigger trigger);
    }
}