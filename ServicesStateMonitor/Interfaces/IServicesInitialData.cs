using ServicesStateMonitor.Models;
using System.Collections.Generic;

namespace ServicesStateMonitor.Interfaces
{
    public interface IServicesInitialData
    {
        IEnumerable<Service> GetInitialData();
    }
}