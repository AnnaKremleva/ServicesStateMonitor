using ServicesStateMonitor.Models;

namespace ServicesStateMonitor.Interfaces
{
    public interface ITriggerFactory
    {
        Trigger GetTrigger(string message);
    }
}