using ServicesStateMonitor.Interfaces;
using System.Collections.Generic;

namespace ServicesStateMonitor.Models
{
    public class TemporaryData : IServicesInitialData
    {
        public IEnumerable<Service> GetInitialData()
        {
            var services = new List<Service>();

            var serviceFirst = new Service
            {
                Name = "First",
                EssentialLinks = new List<string>
                {
                    "https://www.google.com/",
                    "https://translate.google.com"
                }
            };
            var serviceSecond = new Service
            {
                Name = "Second",
                EssentialLinks = new List<string>
                {
                    "https://www.google.com/"
                }
            };
            var serviceThird = new Service
            {
                Name = "Third",
                EssentialLinks = new List<string>
                {
                    "https://translate.google.com"
                }
            };
            var serviceFourth = new Service
            {
                Name = "Fourth",
                EssentialLinks = new List<string>
                {
                    "https://www.google.ru/imghp"
                }
            };

            serviceFourth.Dependents.Add(serviceFirst);
            serviceThird.Dependents.Add(serviceSecond);
            serviceFirst.Dependents.Add(serviceFourth);

            services.Add(serviceFirst);
            services.Add(serviceSecond);
            services.Add(serviceThird);
            services.Add(serviceFourth);

            return services;
        }
    }
}