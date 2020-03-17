using ServicesStateMonitor.Models;

namespace ServicesStateMonitor.Interfaces
{
    public interface IServiceStateHandler
    {
        void UpdateServiceState(Service service, Trigger trigger);
    }
}