using ServicesStateMonitor.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServicesStateMonitor.Models
{
    public class Service
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public ServiceLevel Level { get; set; } = ServiceLevel.Frontend;

        public ServiceState State { get; set; } = ServiceState.AllRight;

        [Display(Name = "Instances")]
        public List<string> Instances { get; set; } = new List<string>();

        [Display(Name = "Instances (each one from new line)")]
        public string InstancesText { get; set; } = string.Empty;

        [Display(Name = "Essential links")]
        public List<string> EssentialLinks { get; set; } = new List<string>();

        [Display(Name = "Essential links (each one from new line)")]
        public string LinksText { get; set; } = string.Empty;

        [Display(Name = "Problem list")]
        public HashSet<string> ProblemList { get; set; } = new HashSet<string>();

        public HashSet<Service> Dependents { get; set; } = new HashSet<Service>();

        [Display(Name = "Depends from")]
        public HashSet<string> DependsFrom { get; set; } = new HashSet<string>();

        public override string ToString()
            => Name;
    }
}