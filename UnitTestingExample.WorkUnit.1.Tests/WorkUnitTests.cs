using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestingExample.WorkUnit._1.Tests
{
    [TestClass]
    public class WorkUnitTests
    {
        [TestMethod]
        public void SimpleFibonacciTest()
        {
            // Arrange
            var workUnit = new UnitTesting.WorkUnit._1.WorkUnit();

            // Act
            var result = workUnit.GetNthFibonacci(10);

            // Assert
            result.Should().Be(55);
        }
    }
}
