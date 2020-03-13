using ServicesStateMonitor.Models;
using System.Collections.Generic;

namespace ServicesStateMonitor.Interfaces
{
    public interface IServicesRepository
    {
        IEnumerable<Service> Services { get; }

        void UpdateState(Trigger trigger);

        void Create(Service newService);

        void Update(Service service);

        void Delete(Service service);
    }
}