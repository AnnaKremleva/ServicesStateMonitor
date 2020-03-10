using ServicesStateMonitor.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ServicesStateMonitor.Models
{
    public class Repository : IServicesRepository
    {
        private readonly ConcurrentDictionary<string, Service> _services;
        private readonly ITriggerFactory _triggerFactory;
        private readonly IServiceMapper _serviceMapper;

        public Repository(IServicesInitialData database, ITriggerFactory triggerFactory, IServiceMapper serviceMapper)
        {
            _triggerFactory = triggerFactory;
            _serviceMapper = serviceMapper;
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
                UpdateServices(service, trigger);
        }

        public void AddService(Service newService)
        {
            _services.AddOrUpdate(newService.Name, newService, (key, oldValue) =>
            {
                oldValue.Name = newService.Name;
                oldValue.DependFrom = newService.DependFrom;
                oldValue.EssentialLinks = newService.EssentialLinks;
                return oldValue;
            });

            foreach (var service in Services)
            {
                foreach (var dependCandidate in Services)
                {
                    if (dependCandidate.DependFrom.Contains(service))
                        service.Dependents.Add(dependCandidate);
                }
            }
        }

        public void UpdateService(Service service)
        {
            AddService(service);
        }

        public void DeleteService(string name)
        {
            if (_services.TryRemove(name, out var removed))
            {
                foreach (var pair in _services)
                {
                    pair.Value.Dependents.Remove(removed);
                    pair.Value.DependFrom.Remove(removed);
                    pair.Value.UpdateState(_triggerFactory.GetFarewellTrigger(removed));
                }
            }
        }

        private void InitRepo(IEnumerable<Service> services)
        {
            foreach (var service in services)
            {
                AddService(service);
            }
        }

        private void UpdateServices(Service serviceOwner, Trigger trigger)
        {
            serviceOwner.UpdateState(trigger);
            var currentServices = GetSnapshot();

            foreach (int index in _serviceMapper.FindDependentIndexes(currentServices, serviceOwner))
            {
                currentServices[index].UpdateState(_triggerFactory.GetDependentTrigger(serviceOwner, trigger));
            }
        }

        private List<Service> GetSnapshot()
        {
            var currentServices = new List<Service>();
            currentServices.AddRange(Services);
            return currentServices;
        }
    }
}