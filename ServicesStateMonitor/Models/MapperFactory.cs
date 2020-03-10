using ServicesStateMonitor.Interfaces;
using System.Collections.Generic;

namespace ServicesStateMonitor.Models
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