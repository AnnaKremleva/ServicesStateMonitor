using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesStateMonitor.Interfaces;

namespace ServicesStateMonitor.Controllers
{
    [Route("api/trigger")]
    [ApiController]
    public class TriggerController : ControllerBase
    {
        private readonly IServicesRepository _servicesRepository;
        private readonly ITriggerFactory _factory;

        public TriggerController(IServicesRepository servicesRepository, ITriggerFactory factory)
        {
            _servicesRepository = servicesRepository;
            _factory = factory;
        }

        public StatusCodeResult Add([FromQuery] string message)
        {
            var trigger = _factory.GetTrigger(message);
            _servicesRepository.UpdateState(trigger);

            return new StatusCodeResult(StatusCodes.Status200OK);
        }
    }
}