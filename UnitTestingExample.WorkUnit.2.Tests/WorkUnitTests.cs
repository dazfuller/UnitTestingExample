using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnitTesting.WorkUnit._2;

namespace UnitTestingExample.WorkUnit._2.Tests
{
    [TestClass]
    public class WorkUnitTests
    {
        [TestMethod]
        public void MockedFibonacciTest()
        {
            // Arrange
            var moqLogger = new Mock<ILogger>();

            moqLogger
                .Setup(m => m.Log(It.Is<string>(s => string.Compare(s, "Starting Fibonacci calculation", StringComparison.CurrentCulture) == 0)))
                .Verifiable();

            moqLogger
                .Setup(m => m.Log(It.Is<string>(s => string.Compare(s, "Finished calculation for n=10: 55", StringComparison.CurrentCulture) == 0)))
                .Verifiable();

            // Act
            var workUnit = new UnitTesting.WorkUnit._2.WorkUnit(moqLogger.Object);
            var result = workUnit.GetNthFibonacci(10);

            // Assert
            result.Should().Be(55);
            moqLogger.Verify();
        }
    }
}
