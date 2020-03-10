using NUnit.Framework;
using ServicesStateMonitor.Models;
using System.Collections.Generic;

namespace ServicesStateTests
{
    public class ServiceTests
    {
        private Service _service;
        private Trigger _trigger;

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
        [TestCase(ServiceState.AllRight, ServiceState.AllRight, ServiceState.AllRight)]
        [TestCase(ServiceState.AffectedByProblem, ServiceState.AffectedByProblem, ServiceState.AllRight)]
        [TestCase(ServiceState.HasProblem, ServiceState.HasProblem, ServiceState.AllRight)]
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

            _service.UpdateState(_trigger);

            Assert.AreEqual(expectedState, _service.State, $"from {oldState} by {triggerState}");
            Assert.AreEqual(triggerState != ServiceState.AllRight, _service.ProblemList.Contains(_trigger.Name), "list problems update");
        }
    }
}