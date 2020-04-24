using Microsoft.AspNetCore.SignalR;
using ServicesStateMonitor.Interfaces;
using System.Threading.Tasks;

namespace ServicesStateMonitor.Hubs
{
    public class ServicesHub : Hub
    {
        private readonly IServicesRepository _servicesRepository;

        public ServicesHub(IServicesRepository servicesRepository)
        {
            _servicesRepository = servicesRepository;
        }

        public async Task Init()
        {
            await UpdateState();
            await UpdateLines();
        }

        public async Task UpdateState()
        {
            foreach (var service in _servicesRepository.Services)
            {
                await Clients.Caller.SendAsync("Updated", service.Name, service.State.ToString());
            }
        }

        public async Task UpdateLines()
        {
            foreach (var servicePairs in _servicesRepository.GetConnectionPairs())
            {
                await Clients.Caller.SendAsync("LineDraw", servicePairs.Item1, servicePairs.Item2);
            }
        }
    }
}