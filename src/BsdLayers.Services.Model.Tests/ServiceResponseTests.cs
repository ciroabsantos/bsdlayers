using KellermanSoftware.CompareNetObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BsdLayers.Services.Model.Tests
{
    [TestClass, TestCategory("Services.Model")]
    public class ServiceResponseTests
    {
        [TestMethod]
        public void ServiceResponse_Should_Be_Costructed_With_Messages_Success()
        {
            //arrange: status code and messages
            var messages = new List<string>() { "message 1", "message 2" };
            var statusCode = 200;

            //act:
            var response = new ServiceResponse(statusCode: statusCode, messages: messages);

            //assert:
            var comparison = new CompareLogic();

            Assert.AreEqual(statusCode, response.StatusCode);
            Assert.IsTrue(comparison.Compare(messages, response.Messages).AreEqual);
        }

        [TestMethod]
        public void ServiceResponse_Should_Be_Costructed_With_Single_Message_Success()
        {
            //arrange: status code and messages
            var message = "single message";
            var statusCode = 201;

            //act:
            var response = new ServiceResponse(statusCode: statusCode, message: message);

            //assert:
            var comparison = new CompareLogic();
            var expected = new ServiceResponse(statusCode: statusCode, messages: new List<string>() { message });
            Assert.IsTrue(comparison.Compare(expected, response).AreEqual);
        }

        [TestMethod]
        public void ServiceResponse_Should_Be_Costructed_With_Empty_Message_Success()
        {
            //arrange: status code and messages
            var statusCode = 401;

            //act:
            var response = new ServiceResponse(statusCode: statusCode);

            //assert:
            var comparison = new CompareLogic();
            var expected = new ServiceResponse(statusCode: statusCode, messages: new List<string>());
            Assert.IsTrue(comparison.Compare(expected, response).AreEqual);
        }

        [TestMethod]
        public void ServiceResponse_Clone_Success()
        {
            //arrange: status code and messages
            var statusCode = 500;
            var messages = new string[] { "error 1", "error 2" };

            //arrange: response
            var response = new ServiceResponse(statusCode: statusCode, messages: messages);

            //act:
            var cloned = (ServiceResponse)response.Clone();

            //assert:
            var comparison = new CompareLogic();
            Assert.IsFalse(response == cloned);
            Assert.IsTrue(comparison.Compare(response, cloned).AreEqual);
        }
    }
}
