using NUnit.Framework;
using ServicesStateMonitor.Models;
using System.Collections.Generic;
using System.Linq;

namespace ServicesStateTests
{
    public class MapperTests
    {
        private Mapper _mapper;
        private List<Service> _services;

        [SetUp]
        public void Setup()
        {
            _services = GetData();
            _mapper = new Mapper(_services);
        }

        [Test]
        public void CanGetIndexes0()
        {
            var expected = new List<int>
            {
                1,
                4,
                3,
                0
            };
            var result = _mapper.FindDependentIndexes(_services[0]);

            int i = 0;
            Assert.AreEqual(expected.Count, result.Count());
            foreach (int index in result)
            {
                Assert.AreEqual(expected[i], index);
                i++;
            }
        }

        [Test]
        public void CanGetIndexes1()
        {
            var expected = new List<int>
            {
                3,
                0,
                1,
                4
            };
            var result = _mapper.FindDependentIndexes(_services[1]);

            int i = 0;
            Assert.AreEqual(expected.Count, result.Count());
            foreach (int index in result)
            {
                Assert.AreEqual(expected[i], index);
                i++;
            }
        }

        [Test]
        public void CanGetIndexes2()
        {
            var expected = new List<int>
            {
                4
            };
            var result = _mapper.FindDependentIndexes(_services[2]);

            int i = 0;
            Assert.AreEqual(expected.Count, result.Count());
            foreach (int index in result)
            {
                Assert.AreEqual(expected[i], index);
                i++;
            }
        }

        [Test]
        public void CanGetIndexes3()
        {
            var expected = new List<int>
            {
                0,
                1,
                4,
                3
            };
            var result = _mapper.FindDependentIndexes(_services[3]);

            int i = 0;
            Assert.AreEqual(expected.Count, result.Count());
            foreach (int index in result)
            {
                Assert.AreEqual(expected[i], index);
                i++;
            }
        }

        [Test]
        public void CanGetIndexes4()
        {
            var expected = new List<int>();
            var result = _mapper.FindDependentIndexes(_services[4]);

            int i = 0;
            Assert.AreEqual(expected.Count, result.Count());
            foreach (int index in result)
            {
                Assert.AreEqual(expected[i], index);
                i++;
            }
        }

        private List<Service> GetData()
        {
            var services = new List<Service>();

            var service0 = new Service();
            var service1 = new Service();
            var service2 = new Service();
            var service3 = new Service();
            var service4 = new Service();

            service0.Dependents.Add(service1);
            service0.Dependents.Add(service4);
            service1.Dependents.Add(service3);
            service2.Dependents.Add(service4);
            service3.Dependents.Add(service0); //cyclic dependency 3->0->1->3

            services.Add(service0);
            services.Add(service1);
            services.Add(service2);
            services.Add(service3);
            services.Add(service4);

            return services;
        }
    }
}