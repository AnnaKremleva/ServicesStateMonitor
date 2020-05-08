using System.ComponentModel.DataAnnotations;

namespace ServicesStateMonitor.Enums
{
    public enum ServiceLevel
    {
        [Display(Name = "Frontend")] Frontend,
        [Display(Name = "Backend")] Backend,
        [Display(Name = "Database")] Database
    }
}