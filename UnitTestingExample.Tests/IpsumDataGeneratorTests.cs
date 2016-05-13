using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;

namespace UnitTestingExample.Tests
{
    [TestClass]
    public class IpsumDataGeneratorTests
    {
        [TestMethod]
        public void TestSuccessfulRequest()
        {
            // Arrange
            const string expectedString = "Lorem ipsum";
            var responseObject = new
            {
                type = "lorem",
                text_out = expectedString
            };

            var jsonString = JsonConvert.SerializeObject(responseObject, Formatting.Indented);

            // Create the mock response object
            var mockResponse = new Mock<HttpWebResponse>();
            mockResponse
                .SetupGet(m => m.StatusCode)
                .Returns(HttpStatusCode.OK)
                .Verifiable();

            mockResponse
                .Setup(m => m.GetResponseStream())
                .Returns(new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
                .Verifiable();

            var mockRequest = GetMockRequest(mockResponse);
            var mockCreate = GetMockWebRequestCreate(mockRequest);

            // Act
            var responseString = string.Empty;

            Action act = () =>
            {
                var generator = new IpsumDataGenerator(mockCreate.Object);
                responseString = generator.Generate();
            };

            // Assert
            act.ShouldNotThrow<WebException>();
            responseString.Should().Be(expectedString);

            mockCreate.Verify();
            mockRequest.Verify();
            mockResponse.Verify();
        }

        [TestMethod]
        public void TestInternalServerErrorResponse()
        {
            // Arrange
            var mockResponse = new Mock<HttpWebResponse>();
            mockResponse
                .Setup(m => m.StatusCode)
                .Returns(HttpStatusCode.InternalServerError)
                .Verifiable();

            var mockRequest = GetMockRequest(mockResponse, true);
            var mockCreate = GetMockWebRequestCreate(mockRequest);

            Action act = () =>
            {
                var generator = new IpsumDataGenerator(mockCreate.Object);
                generator.Generate();
            };

            // Assert
            act.ShouldThrow<HttpRequestException>();

            mockCreate.Verify();
            mockRequest.Verify();
            mockResponse.Verify();
        }

        [TestMethod]
        public void TestErrorWithoutResponse()
        {
            // Arrange
            var mockRequest = GetMockRequest(null, true);
            var mockCreate = GetMockWebRequestCreate(mockRequest);

            Action act = () =>
            {
                var generator = new IpsumDataGenerator(mockCreate.Object);
                generator.Generate();
            };

            // Assert
            act
                .ShouldThrow<WebException>()
                .And
                .Response
                .Should()
                .BeNull();

            mockCreate.Verify();
            mockRequest.Verify();
        }

        [TestMethod]
        public void TestEmptyResponseStream()
        {
            // Arrange
            
            // Create the mock response object
            var mockResponse = new Mock<HttpWebResponse>();
            mockResponse
                .SetupGet(m => m.StatusCode)
                .Returns(HttpStatusCode.OK)
                .Verifiable();

            mockResponse
                .Setup(m => m.GetResponseStream())
                .Returns<Stream>(null)
                .Verifiable();

            var mockRequest = GetMockRequest(mockResponse);
            var mockCreate = GetMockWebRequestCreate(mockRequest);

            // Act
            var responseString = string.Empty;

            Action act = () =>
            {
                var generator = new IpsumDataGenerator(mockCreate.Object);
                responseString = generator.Generate();
            };

            // Assert
            act.ShouldNotThrow<WebException>();
            responseString.Should().BeEmpty();

            mockCreate.Verify();
            mockRequest.Verify();
            mockResponse.Verify();
        }

        private static Mock<IWebRequestCreate> GetMockWebRequestCreate(IMock<WebRequest> mockRequest)
        {
            var mockCreate = new Mock<IWebRequestCreate>();

            mockCreate
                .Setup(m => m.Create(It.IsAny<Uri>()))
                .Returns(mockRequest.Object)
                .Verifiable();

            return mockCreate;
        }

        private static Mock<WebRequest> GetMockRequest(IMock<HttpWebResponse> mockResponse, bool isInvalidRequest = false)
        {
            var mockRequest = new Mock<WebRequest>();

            mockRequest
                .SetupProperty(m => m.Method)
                .SetupProperty(m => m.Timeout)
                .SetupProperty(m => m.Proxy);

            mockRequest
                .SetupSet(m => m.Method = "GET")
                .Verifiable();

            if (isInvalidRequest)
            {
                var exception = new WebException(
                    "An internal server error occurred",
                    null,
                    WebExceptionStatus.UnknownError,
                    mockResponse?.Object);

                mockRequest
                .Setup(m => m.GetResponse())
                .Throws(exception)
                .Verifiable();
            }
            else
            {
                mockRequest
                .Setup(m => m.GetResponse())
                .Returns(mockResponse.Object)
                .Verifiable();
            }
            
            return mockRequest;
        }
    }
}
