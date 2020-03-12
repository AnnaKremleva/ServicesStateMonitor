namespace ServicesStateMonitor.Utils
{
    public static class NameExtensions
    {
        private const string Separator = " <-- ";

        public static string GetWithPrefix(this string name)
            => string.Concat(Separator, name);
    }
}