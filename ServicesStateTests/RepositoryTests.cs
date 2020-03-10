using NUnit.Framework;
using ServicesStateMonitor.Models;
using Moq;
using ServicesStateMonitor.Interfaces;

namespace ServicesStateTests
{
    public class RepositoryTests
    {
        private Repository _repository;

        private Mock<IServicesInitialData> _data;
        private Mock<ITriggerFactory> _factory;
        private Mock<IServiceMapper> _mapper;

        [SetUp]
        public void Setup()
        {
            _data = new Mock<IServicesInitialData>();
            _factory = new Mock<ITriggerFactory>();
            _mapper = new Mock<IServiceMapper>();

            _repository = new Repository(_data.Object, _factory.Object, _mapper.Object);
        }
    }
}
