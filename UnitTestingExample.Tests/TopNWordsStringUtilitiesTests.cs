using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace UnitTestingExample.Tests
{
    [TestClass]
    public partial class StringUtilitiesTests
    {
        [TestMethod]
        public void TestSimpleLookup()
        {
            // Arrange
            const string s = "Test in this test method is the most common test word";

            // Act
            var result = s.TopNWords();

            // Assert
            result.First().Should().BeEquivalentTo("test");
        }

        [TestMethod]
        public void TestForNegativeNValues()
        {
            // Arrange
            const string s = "This is just some dummy text";

            // Act
            Action act = () => { s.TopNWords(-1); };

            // Assert
            act
                .ShouldThrow<ArgumentException>()
                .WithMessage($"Parameter must be a positive, non-zero value{Environment.NewLine}Parameter name: n");
        }

        [TestMethod]
        public void TestForTooFewWords()
        {
            // Arrange
            const string s = "This is a test This is a test This is a test";

            // Act
            var result = s.TopNWords(10);

            // Assert
            result.Should().HaveCount(4);
        }

        [TestMethod]
        public void TestForEmptySplitPlaces()
        {
            // Arrange
            const string s = "This|||is|a|test|with||test|data|and||blank|places";  // Pipes used to show where empty elements should be

            // Act
            var result = s.TopNWords(seperator: '|');

            // Assert
            result.First().Should().BeEquivalentTo("test");
        }

        [TestMethod]
        public void TestOrderingWhenCountsAreEqual()
        {
            // Arrange
            const string s = "test data test data test data";
            var expected = new List<string>() {"data", "test"};

            // Act
            var result = s.TopNWords(2);

            // Assert
            result
                .Should()
                .ContainInOrder(expected);
        }
    }
}
