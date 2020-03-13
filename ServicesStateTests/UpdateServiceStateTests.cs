using Moq;
using NUnit.Framework;
using ServicesStateMonitor.Enums;
using ServicesStateMonitor.Interfaces;
using ServicesStateMonitor.Models;
using System.Collections.Generic;

namespace ServicesStateTests
{
    public class UpdateServiceStateTests
    {
        private Service _service;
        private Trigger _trigger;
        private ServiceStateHandler _stateHandler;

        [SetUp]
        public void Setup()
        {
            _service = new Service
            {
                Name = "TestServiceName",
                ProblemList = new HashSet<string>
                {
                    "OldProblem"
                }
            };

            _trigger = new Trigger
            {
                Name = "TestTriggerName"
            };

            var mockFactory = new Mock<ITriggerFactory>();
            mockFactory
                .Setup(m => m.GetWithPrefix(It.IsAny<string>()))
                .Returns((string s) => s);

            _stateHandler = new ServiceStateHandler(mockFactory.Object);
        }

        [TestCase(ServiceState.HasProblem, ServiceState.AllRight, ServiceState.HasProblem)]
        [TestCase(ServiceState.HasProblem, ServiceState.AffectedByProblem, ServiceState.HasProblem)]
        [TestCase(ServiceState.HasProblem, ServiceState.HasProblem, ServiceState.HasProblem)]
        [TestCase(ServiceState.AffectedByProblem, ServiceState.AllRight, ServiceState.AffectedByProblem)]
        [TestCase(ServiceState.AffectedByProblem, ServiceState.AffectedByProblem, ServiceState.AffectedByProblem)]
        [TestCase(ServiceState.HasProblem, ServiceState.HasProblem, ServiceState.AffectedByProblem)]
        [TestCase(ServiceState.AllRight, ServiceState.AllRight, ServiceState.AllRight)]
        [TestCase(ServiceState.AllRight, ServiceState.AffectedByProblem, ServiceState.AllRight)]
        [TestCase(ServiceState.AllRight, ServiceState.HasProblem, ServiceState.AllRight)]
        public void CanUpdateNoProblem(ServiceState expectedState, ServiceState oldState, ServiceState triggerState)
        {
            _service.ProblemList.Clear();

            BaseTest(expectedState, oldState, triggerState);
        }

        [TestCase(ServiceState.HasProblem, ServiceState.AllRight, ServiceState.HasProblem)]
        [TestCase(ServiceState.HasProblem, ServiceState.AffectedByProblem, ServiceState.HasProblem)]
        [TestCase(ServiceState.HasProblem, ServiceState.HasProblem, ServiceState.HasProblem)]
        [TestCase(ServiceState.AffectedByProblem, ServiceState.AllRight, ServiceState.AffectedByProblem)]
        [TestCase(ServiceState.AffectedByProblem, ServiceState.AffectedByProblem, ServiceState.AffectedByProblem)]
        [TestCase(ServiceState.HasProblem, ServiceState.HasProblem, ServiceState.AffectedByProblem)]
        [TestCase(ServiceState.AffectedByProblem, ServiceState.AllRight, ServiceState.AllRight)]
        [TestCase(ServiceState.AffectedByProblem, ServiceState.AffectedByProblem, ServiceState.AllRight)]
        [TestCase(ServiceState.AffectedByProblem, ServiceState.HasProblem, ServiceState.AllRight)]
        public void CanUpdateStateAnotherProblem(ServiceState expectedState, ServiceState oldState, ServiceState triggerState)
        {
            BaseTest(expectedState, oldState, triggerState);
        }

        [TestCase(ServiceState.HasProblem, ServiceState.AllRight, ServiceState.HasProblem)]
        [TestCase(ServiceState.HasProblem, ServiceState.AffectedByProblem, ServiceState.HasProblem)]
        [TestCase(ServiceState.HasProblem, ServiceState.HasProblem, ServiceState.HasProblem)]
        [TestCase(ServiceState.AffectedByProblem, ServiceState.AllRight, ServiceState.AffectedByProblem)]
        [TestCase(ServiceState.AffectedByProblem, ServiceState.AffectedByProblem, ServiceState.AffectedByProblem)]
        [TestCase(ServiceState.HasProblem, ServiceState.HasProblem, ServiceState.AffectedByProblem)]
        [TestCase(ServiceState.AllRight, ServiceState.AllRight, ServiceState.AllRight)]
        [TestCase(ServiceState.AllRight, ServiceState.AffectedByProblem, ServiceState.AllRight)]
        [TestCase(ServiceState.AllRight, ServiceState.HasProblem, ServiceState.AllRight)]
        public void CanUpdateTheSameProblem(ServiceState expectedState, ServiceState oldState, ServiceState triggerState)
        {
            _trigger.Name = "OldProblem";

            BaseTest(expectedState, oldState, triggerState);
        }

        private void BaseTest(ServiceState expectedState, ServiceState oldState, ServiceState triggerState)
        {
            _service.State = oldState;
            _trigger.ServiceState = triggerState;

            _stateHandler.UpdateServiceState(_service, _trigger);

            Assert.AreEqual(expectedState, _service.State, $"from {oldState} by {triggerState}");
            Assert.AreEqual(HasProblem(triggerState), ProcessedCorrectly(), 
                $"list problems update by trigger {_trigger.Name} with state {_trigger.ServiceState}");
        }

        private bool HasProblem(ServiceState triggerState)
            => triggerState != ServiceState.AllRight;

        private bool ProcessedCorrectly()
            => _service.ProblemList.Contains(_trigger.Name);
    }
}