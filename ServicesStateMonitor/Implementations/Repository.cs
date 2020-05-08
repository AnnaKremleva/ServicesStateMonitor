using ServicesStateMonitor.Enums;
using ServicesStateMonitor.Interfaces;
using ServicesStateMonitor.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ServicesStateMonitor.Implementations
{
    public class Repository : IServicesRepository
    {
        private readonly ConcurrentDictionary<string, Service> _services;
        private readonly ConcurrentDictionary<string, Trigger> _triggers;
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
            _triggers = new ConcurrentDictionary<string, Trigger>();
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

        public IEnumerable<DependenciesViewModel> GetDependencies(Service service)
        {
            foreach (var pair in _services)
            {
                yield return new DependenciesViewModel
                {
                    ServiceName = pair.Value.Name,
                    Selected = service?.DependsFrom.Contains(pair.Value.Name) ?? false
                };
            }
        }

        public void UpdateState(Trigger trigger)
        {
            if (_services.TryGetValue(trigger.OwnerName, out var service))
            {
                trigger.State = TriggerState.HasOwner;
                UpdateServices(service, trigger);
            }

            if (trigger.ServiceState != ServiceState.AllRight)
                _triggers.AddOrUpdate(string.Concat(trigger.OwnerName, trigger.Name), trigger, (key, oldValue) => trigger);
            else
                _triggers.TryRemove(string.Concat(trigger.OwnerName, trigger.Name), out var _);
        }

        public void Create(Service newService)
        {
            _services.AddOrUpdate(newService.Name, newService, (key, oldValue) =>
            {
                oldValue.Name = newService.Name;
                oldValue.Level = newService.Level;
                oldValue.Instances = newService.Instances;
                oldValue.DependsFrom = newService.DependsFrom;
                oldValue.EssentialLinks = newService.EssentialLinks;
                return oldValue;
            });

            if (_services.TryGetValue(newService.Name, out var created))
            {
                _database.Create(created);

                UpdateDependents();
                UpdateProblems();
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

                UpdateDependents();
                UpdateProblems();
            }
        }

        public Service GetById(string id)
            => _services.TryGetValue(id, out var service) ? service : null;

        public bool AlreadyExists(string name)
            => _services.TryGetValue(name, out var _);

        public IEnumerable<(string,string)> GetConnectionPairs()
        {
            var result = new List<(string,string)>();
            var currentServices = GetSnapshot();
            foreach (var service in currentServices)
            {
                foreach (var dependentService in service.Dependents)
                {
                    result.Add((service.Name,dependentService.Name));
                }
            }
            return result;
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

        private void UpdateDependents()
        {
            foreach (var service in Services)
            {
                foreach (var dependCandidate in Services)
                {
                    if (dependCandidate.DependsFrom.Contains(service.Name))
                        service.Dependents.Add(dependCandidate);
                    else
                        service.Dependents.Remove(dependCandidate);
                }
                _database.Update(service);
            }
        }

        private void UpdateProblems()
        {
            foreach (var service in Services)
            {
                service.ProblemList.Clear();
                service.State = ServiceState.AllRight;
            }

            foreach (var pair in _triggers)
            {
                UpdateState(pair.Value);
            }
        }
    }
}