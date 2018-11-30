using HouseholdExpensesTrackerServer21.Common.Type;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace HouseholdExpensesTrackerServer21.Web.Common.Middlewares.Tests
{
    [TestClass]
    public class ErrorHandlerMiddlewareTests
    {
        [TestMethod]
        public async Task ShouldLogDefaultException()
        {
            // Arrange
            string expectedFormattedMsg = "There was an error.";
            string extectedErrorMsg = "test";
            var logger = new Mock<ILogger<ErrorHandlerMiddleware>>();
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            var actual = new ErrorHandlerMiddleware(next: (innerHttpContext) =>
            {
                throw new Exception(extectedErrorMsg);
            }, logger: logger.Object);

            // Act
            await actual.Invoke(context);

            // Assert
            logger.Verify(e => e.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<FormattedLogValues>(a => a.ToString().Equals(expectedFormattedMsg)),
                It.Is<Exception>(a => a.Message.Equals(extectedErrorMsg)),
                It.IsAny<Func<object, Exception, string>>()),
                Times.Once
            );
        }

        [TestMethod]
        public async Task ShouldCreateResponseForException()
        {
            // Arrange
            string expectedErrorCode = "error";
            string extectedErrorMsg = "test";
            var logger = new Mock<ILogger<ErrorHandlerMiddleware>>();
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            var actual = new ErrorHandlerMiddleware(next: (innerHttpContext) =>
            {
                throw new Exception(extectedErrorMsg);
            }, logger: logger.Object);

            // Act
            await actual.Invoke(context);
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var streamText = reader.ReadToEnd();
            var objResponse = JsonConvert.DeserializeObject<dynamic>(streamText);

            // Assert
            Assert.IsTrue(context.Response.StatusCode == (int)HttpStatusCode.BadRequest);
            Assert.IsTrue(context.Response.ContentType == "application/json");
            Assert.IsTrue(objResponse != null);
            Assert.IsTrue(objResponse.code == expectedErrorCode);
            Assert.IsTrue(objResponse.message == extectedErrorMsg);
        }

        [TestMethod]
        public async Task ShouldLogHouseholdException()
        {
            // Arrange
            string expectedFormattedMsg = "test";
            string extectedErrorMsg = "test";
            var logger = new Mock<ILogger<ErrorHandlerMiddleware>>();
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            var actual = new ErrorHandlerMiddleware(next: (innerHttpContext) =>
            {
                throw new HouseholdException(extectedErrorMsg);
            }, logger: logger.Object);

            // Act
            await actual.Invoke(context);

            // Assert
            logger.Verify(e => e.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<FormattedLogValues>(a => a.ToString().Equals(expectedFormattedMsg)),
                It.Is<Exception>(a => a.Message.Equals(extectedErrorMsg)),
                It.IsAny<Func<object, Exception, string>>()),
                Times.Once
            );
        }

        [TestMethod]
        public async Task ShouldCreateResponseForHouseholdException()
        {
            // Arrange
            string expectedErrorCode = "E2223";
            string extectedErrorMsg = "test";
            var logger = new Mock<ILogger<ErrorHandlerMiddleware>>();
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            var actual = new ErrorHandlerMiddleware(next: (innerHttpContext) =>
            {
                throw new HouseholdException(expectedErrorCode, extectedErrorMsg);
            }, logger: logger.Object);

            // Act
            await actual.Invoke(context);
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var streamText = reader.ReadToEnd();
            var objResponse = JsonConvert.DeserializeObject<dynamic>(streamText);

            // Assert
            Assert.IsTrue(context.Response.StatusCode == (int)HttpStatusCode.BadRequest);
            Assert.IsTrue(context.Response.ContentType == "application/json");
            Assert.IsTrue(objResponse != null);
            Assert.IsTrue(objResponse.code == expectedErrorCode);
            Assert.IsTrue(objResponse.message == extectedErrorMsg);
        }

    }
}
