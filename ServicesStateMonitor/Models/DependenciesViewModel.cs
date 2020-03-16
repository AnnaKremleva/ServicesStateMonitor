namespace ServicesStateMonitor.Models
{
    public class DependenciesViewModel
    {
        public bool Selected { get; set; }

        public string ServiceName { get; set; }

        public override string ToString()
            => ServiceName;
    }
}