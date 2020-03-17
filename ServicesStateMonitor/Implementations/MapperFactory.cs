using ServicesStateMonitor.Interfaces;
using ServicesStateMonitor.Models;
using System.Collections.Generic;

namespace ServicesStateMonitor.Implementations
{
    public class MapperFactory : IServiceMapper
    {
        public IEnumerable<int> FindDependentIndexes(List<Service> currentServices, Service startService)
        {
            var mapper = new Mapper(currentServices);
            return mapper.FindDependentIndexes(startService);
        }
    }
}