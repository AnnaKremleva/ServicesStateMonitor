using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesStateMonitor.Models
{
    public class Trigger
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string OwnerName { get; set; }

        [Required]
        [Range(0, 2, 
            ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public ServiceState ServiceState { get; set; } = ServiceState.HasProblem;

        public TriggerState State { get; set; } = TriggerState.OwnerError;

        public Service FindOwner()
            => null; //TODO need real
    }
}
