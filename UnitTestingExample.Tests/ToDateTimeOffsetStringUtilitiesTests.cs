using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestingExample.Tests
{
    public partial class StringUtilitiesTests
    {
        [TestMethod]
        public void TestSimpleIsoDate()
        {
            // Arrange
            const string s = "2016-05-10T14:07:00+01:00";
            var expected = new DateTimeOffset(2016, 05, 10, 14, 07, 00, TimeSpan.FromHours(1));

            // Act
            var result = s.ToDateTimeOffset();

            // Assert
            result.Should().Be(expected);
        }

        [TestMethod]
        public void TestAlternativeFormatString()
        {
            // Arrange
            const string s = "10 May 2016 2:07 PM +01:00";
            var expected = new DateTimeOffset(2016, 05, 10, 14, 07, 00, TimeSpan.FromHours(1));

            // Act
            var result = s.ToDateTimeOffset("dd MMMM yyyy h:mm tt K");

            // Assert
            result.Should().Be(expected);
        }

        [TestMethod]
        public void TestInvalidInputString()
        {
            // Arrange
            const string s = "Hello world";

            // Act
            Action act = () => { s.ToDateTimeOffset(); };

            // Assert
            act
                .ShouldThrow<ArgumentException>()
                .WithInnerException<FormatException>();
        }

        [TestMethod]
        public void TestInvalidFormatString()
        {
            // Arrange
            const string s = "2016-05-10T14:07:00+01:00";

            // Act
            Action act = () => { s.ToDateTimeOffset("hello world"); };

            // Assert
            act
                .ShouldThrow<ArgumentException>()
                .WithInnerException<FormatException>();
        }

        [TestMethod]
        public void TestNullFormatString()
        {
            // Arrange
            const string s = "2016-05-10T14:07:00+01:00";

            // Act
            Action act = () => { s.ToDateTimeOffset(null); };

            // Assert
            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
