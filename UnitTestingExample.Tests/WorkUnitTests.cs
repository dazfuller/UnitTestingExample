using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestingExample.Tests
{
    [TestClass]
    public class WorkUnitTests
    {
        [TestMethod]
        public void SimpleFibonacciTest()
        {
            // Arrange
            var workUnit = new WorkUnit();

            // Act
            var result = workUnit.GetNthFibonacci(10);

            // Assert
            result.Should().Be(55);
        }
    }
}
