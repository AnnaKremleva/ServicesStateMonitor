using ServicesStateMonitor.Models;
using System.Collections.Generic;

namespace ServicesStateMonitor.Interfaces
{
    public interface IServiceMapper
    {
        IEnumerable<int> FindDependentIndexes(List<Service> currentServices, Service startService);
    }
}