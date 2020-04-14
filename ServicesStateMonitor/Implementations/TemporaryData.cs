using ServicesStateMonitor.Enums;
using ServicesStateMonitor.Interfaces;
using ServicesStateMonitor.Models;
using System.Collections.Generic;

namespace ServicesStateMonitor.Implementations
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
                },
                Instances = new List<string>()
                {
                    "1",
                    "2",
                    "3",
                    "ir",
                    "sg"
                },
                Level = ServiceLevel.Database
            };
            var serviceSecond = new Service
            {
                Name = "Second",
                EssentialLinks = new List<string>
                {
                    "https://www.google.com/"
                },
                Instances = new List<string>()
                {
                    "1",
                    "ir"
                },
                DependsFrom = new HashSet<string>
                {
                    "First"
                },
                Level = ServiceLevel.Backend
            };
            var serviceThird = new Service
            {
                Name = "Third",
                EssentialLinks = new List<string>
                {
                    "https://translate.google.com"
                },
                Instances = new List<string>()
                {
                    "1",
                    "2",
                    "sg"
                },
                Level = ServiceLevel.Frontend
            };
            var serviceFourth = new Service
            {
                Name = "Fourth",
                EssentialLinks = new List<string>
                {
                    "https://www.google.com/imghp"
                },
                Instances = new List<string>()
                {
                    "com",
                    "ir",
                    "sg"
                },
                DependsFrom = new HashSet<string>
                {
                    "Second"
                },
                Level = ServiceLevel.Database
            };
            var serviceFifth = new Service
            {
                Name = "Fifth",
                EssentialLinks = new List<string>
                {
                    "https://www.google.com/imghp"
                },
                Instances = new List<string>()
                {
                    "1",
                    "2"
                },
                DependsFrom = new HashSet<string>
                {
                    "First",
                    "Third"
                },
                Level = ServiceLevel.Backend
            };
            var serviceSixth = new Service
            {
                Name = "Sixth",
                EssentialLinks = new List<string>
                {
                    "https://www.google.com/imghp",
                    "https://translate.google.com",
                    "https://www.google.com/"
                }
            };
            var serviceSeventh = new Service
            {
                Name = "Seventh",
                EssentialLinks = new List<string>
                {
                    "https://translate.google.com"
                },
                Instances = new List<string>()
                {
                    "com"
                },
                DependsFrom = new HashSet<string>
                {
                    "Fifth",
                    "Third"
                },
                Level = ServiceLevel.Frontend
            };
            serviceFirst.DependsFrom.Add("Fourth");
            serviceSixth.DependsFrom.Add("Seventh");

            services.Add(serviceFirst);
            services.Add(serviceSecond);
            services.Add(serviceThird);
            services.Add(serviceFourth);
            services.Add(serviceFifth);
            services.Add(serviceSixth);
            services.Add(serviceSeventh);

            return services;
        }

        public void Create(Service service) { } 

        public void Update(Service service) { }

        public void Delete(Service service) { }
    }
}