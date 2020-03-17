using ServicesStateMonitor.Enums;
using System.ComponentModel.DataAnnotations;

namespace ServicesStateMonitor.Models
{
    public class Trigger
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string OwnerName { get; set; } = string.Empty;

        [Required]
        [EnumDataType(typeof(ServiceState), ErrorMessage = "Value doesn't exist within enum ServiceState")]
        public ServiceState ServiceState { get; set; } = ServiceState.AllRight;

        public TriggerState State { get; set; } = TriggerState.OwnerError;
    }
}