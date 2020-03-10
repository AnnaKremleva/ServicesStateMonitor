using ServicesStateMonitor.Models;
using System.Collections.Generic;

namespace ServicesStateMonitor.Interfaces
{
    public interface IRepository
    {
        IEnumerable<Service> Services { get; }

        void UpdateState(Trigger trigger);

        void AddService(Service service);

        void DeleteService(Service service);
    }
}