using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ServicesStateMonitor.Hubs;
using ServicesStateMonitor.Interfaces;
using ServicesStateMonitor.Models;
using System.Threading.Tasks;

namespace ServicesStateMonitor.Controllers
{
    [Route("api/trigger")]
    [ApiController]
    public class TriggerController : ControllerBase
    {
        private readonly IServicesRepository _servicesRepository;
        private readonly IHubContext<ServicesHub> _hubContext;

        public TriggerController(IServicesRepository servicesRepository, IHubContext<ServicesHub> hubContext)
        {
            _servicesRepository = servicesRepository;
            _hubContext = hubContext;
        }

        [HttpPost]
        [Route("add")]
        public async Task<StatusCodeResult> Add([FromBody] Trigger trigger)
        {
            
            _servicesRepository.UpdateState(trigger);
            //await _hubContext.Clients.All.SendAsync(("UpdatedByTrigger", _servicesRepository.Services));

            return new StatusCodeResult(StatusCodes.Status200OK);
        }
    }
}