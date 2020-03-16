using ServicesStateMonitor.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServicesStateMonitor.Models
{
    public class Service
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public ServiceState State { get; set; } = ServiceState.AllRight;

        public List<string> EssentialLinks { get; set; } = new List<string>();

        [Display(Name = "Essential links (each one from new line)")]
        public string LinksText { get; set; } = string.Empty;

        public HashSet<string> ProblemList { get; set; } = new HashSet<string>();

        public HashSet<Service> Dependents { get; set; } = new HashSet<Service>();

        public HashSet<string> DependFrom { get; set; } = new HashSet<string>();

        public override string ToString()
            => Name;
    }
}