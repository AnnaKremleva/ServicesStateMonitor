using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesStateMonitor.Interfaces;

namespace ServicesStateMonitor.Controllers
{
    [Route("api/trigger")]
    [ApiController]
    public class TriggerController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly ITriggerFactory _factory;

        public TriggerController(IRepository repository, ITriggerFactory factory)
        {
            _repository = repository;
            _factory = factory;
        }

        public StatusCodeResult Add([FromQuery] string message)
        {
            var trigger = _factory.GetTrigger(message);
            _repository.UpdateState(trigger);

            return new StatusCodeResult(StatusCodes.Status200OK);
        }
    }
}