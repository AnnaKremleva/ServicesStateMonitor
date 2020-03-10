using ServicesStateMonitor.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ServicesStateMonitor.Models
{
    public class TemporaryRepository : IRepository
    {
        private readonly ConcurrentDictionary<string, Service> _services;

        public TemporaryRepository()
        {
            _services = new ConcurrentDictionary<string, Service>();
        }

        public IEnumerable<Service> Services
        {
            get
            {
                foreach (var service in _services)
                {
                    yield return service.Value;
                }
            }
        }

        public void UpdateState(Trigger trigger)
        {
            if (_services.TryGetValue(trigger.OwnerName, out var service))
                service.UpdateState(trigger);
        }

        public void AddService(Service service)
        {
            _services.AddOrUpdate(service.Name, service, (key, oldValue) =>
            {
                oldValue.Name = service.Name;
                oldValue.Dependents = service.Dependents;
                oldValue.EssentialLinks = service.EssentialLinks;
                return oldValue;
            });
        }

        public void DeleteService(Service service)
        {
            _services.TryRemove(service.Name, out var _);
        }
    }
}