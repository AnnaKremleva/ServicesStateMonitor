using ServicesStateMonitor.Models;
using System.Collections.Generic;

namespace ServicesStateMonitor.Interfaces
{
    public interface IServicesInitialData
    {
        IEnumerable<Service> GetInitialData();

        void Create(Service service);

        void Update(Service service);

        void Delete(Service service);
    }
}