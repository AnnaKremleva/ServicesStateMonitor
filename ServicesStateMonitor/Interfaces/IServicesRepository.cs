using ServicesStateMonitor.Models;
using System.Collections.Generic;

namespace ServicesStateMonitor.Interfaces
{
    public interface IServicesRepository
    {
        IEnumerable<Service> Services { get; }

        void UpdateState(Trigger trigger);

        void AddService(Service newService);

        void UpdateService(Service service);

        void DeleteService(string name);
    }
}