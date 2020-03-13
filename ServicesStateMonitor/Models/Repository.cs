using ServicesStateMonitor.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ServicesStateMonitor.Models
{
    public class Repository : IServicesRepository
    {
        private readonly ConcurrentDictionary<string, Service> _services;
        private readonly IServicesInitialData _database;
        private readonly ITriggerFactory _triggerFactory;
        private readonly IServiceMapper _serviceMapper;
        private readonly IServiceStateHandler _stateHandler;

        public Repository(IServicesInitialData database, ITriggerFactory triggerFactory, IServiceMapper serviceMapper, IServiceStateHandler stateHandler)
        {
            _database = database;
            _triggerFactory = triggerFactory;
            _serviceMapper = serviceMapper;
            _stateHandler = stateHandler;
            _services = new ConcurrentDictionary<string, Service>();
            InitRepo(_database.GetInitialData());
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

        public void Create(Service newService)
        {
            _services.AddOrUpdate(newService.Name, newService, (key, oldValue) =>
            {
                oldValue.Name = newService.Name;
                oldValue.DependFrom = newService.DependFrom;
                oldValue.EssentialLinks = newService.EssentialLinks;
                return oldValue;
            });
            _database.Create(newService);

            foreach (var service in Services)
            {
                foreach (var dependCandidate in Services)
                {
                    if (dependCandidate.DependFrom.Contains(service))
                        service.Dependents.Add(dependCandidate);
                }
                _database.Update(service);
            }
        }

        public void Update(Service service)
        {
            Create(service);
        }

        public void Delete(Service service)
        {
            if (_services.TryRemove(service.Name, out var removed))
            {
                _database.Delete(removed);
                foreach (var pair in _services)
                {
                    pair.Value.Dependents.Remove(removed);
                    pair.Value.DependFrom.Remove(removed);
                    _stateHandler.UpdateServiceState(pair.Value, _triggerFactory.GetFarewellTrigger(removed));
                    _database.Update(pair.Value);
                }
            }
        }

        private void InitRepo(IEnumerable<Service> services)
        {
            foreach (var service in services)
            {
                Create(service);
            }
        }

        private void UpdateServices(Service serviceOwner, Trigger trigger)
        {
            _stateHandler.UpdateServiceState(serviceOwner, _triggerFactory.GetUpdatedTrigger(serviceOwner, trigger));
            _database.Update(serviceOwner);

            var currentServices = GetSnapshot();

            foreach (int index in _serviceMapper.FindDependentIndexes(currentServices, serviceOwner))
            {
                _stateHandler.UpdateServiceState(currentServices[index], _triggerFactory.GetDependentTrigger(serviceOwner, trigger));
                _database.Update(currentServices[index]);
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