using ServicesStateMonitor.Models;
using System.Collections.Generic;

namespace ServicesStateMonitor.Interfaces
{
    public interface IServicesRepository
    {
        IEnumerable<Service> Services { get; }

        IEnumerable<DependenciesViewModel> GetDependencies(Service service);

        void UpdateState(Trigger trigger);

        void Create(Service newService);

        void Update(Service service);

        void Delete(Service service);

        Service GetById(string id);
    }
}