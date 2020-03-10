using ServicesStateMonitor.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ServicesStateMonitor.Models
{
    public class ServicesRepository : IServicesRepository
    {
        private readonly ConcurrentDictionary<string, Service> _services;

        public ServicesRepository(IServicesInitialData database)
        {
            _services = new ConcurrentDictionary<string, Service>();
            InitRepo(database.GetInitialData());
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

        public void UpdateService(Service service)
        {
            AddService(service);
        }

        public void DeleteService(string name)
        {
            _services.TryRemove(name, out var _);
        }

        private void InitRepo(IEnumerable<Service> services)
        {
            foreach (var service in services)
            {
                AddService(service);
            }
        }
    }
}