using System.ComponentModel.DataAnnotations;

namespace ServicesStateMonitor.Models
{
    public class Trigger
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string OwnerName { get; set; }

        [Required]
        [EnumDataType(typeof(ServiceState), ErrorMessage = "Value doesn't exist within enum ServiceState")]
        public ServiceState ServiceState { get; set; } = ServiceState.HasProblem;

        public TriggerState State { get; set; } = TriggerState.OwnerError;

        public Service FindOwner()
            => null; //TODO need real
    }
}