using KellermanSoftware.CompareNetObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BsdLayers.Services.Model.Tests
{
    [TestClass, TestCategory("Services.Model")]
    public class ServiceResponseWithContentTests
    {
        [TestMethod]
        public void ServiceResponse_With_Content_Should_Be_Contructed_Full()
        {
            //arrange:
            var statusCode = 201;
            var messages = new List<string>() { "message 1", "message 2" };
            var resourceId = Guid.NewGuid().ToString();

            //act:
            var response = new ServiceResponse<string>
            (
                statusCode: statusCode,
                messages: messages,
                content: resourceId
            );

            //assert:
            var comparison = new CompareLogic();
            Assert.AreEqual(statusCode, response.StatusCode);

            Assert.AreEqual(resourceId, response.Content);
            Assert.IsTrue(comparison.Compare(messages, response.Messages).AreEqual);
        }

        [TestMethod]
        public void ServiceResponse_With_Content_Should_Be_Contructed_Single_Message()
        {
            //arrange:
            var statusCode = 500;
            var messages = new List<string>() { "internal error" };
            var content = new object();

            //arrange: expected
            var expected = new ServiceResponse<object>
            (
                statusCode: statusCode,
                messages: messages,
                content: content
            );

            //act:
            var response = new ServiceResponse<object>
            (
                statusCode: statusCode,
                message: messages[0],
                content: content
            );

            //assert:
            var comparison = new CompareLogic();
            Assert.IsTrue(comparison.Compare(expected, response).AreEqual);
        }

        [TestMethod]
        public void ServiceResponse_With_Content_Should_Be_Contructed_Empty_Message()
        {
            //arrange:
            var statusCode = 200;
            var content = 1000;

            //arrange: expected
            var expected = new ServiceResponse<int>
            (
                statusCode: statusCode,
                messages: new string[0],
                content: content
            );

            //act:
            var response = new ServiceResponse<int>
            (
                statusCode: statusCode,
                content: content
            );

            //assert:
            var comparison = new CompareLogic();
            Assert.IsTrue(comparison.Compare(expected, response).AreEqual);
        }

        [TestMethod]
        public void ServiceResponse_With_Content_Should_Be_Cloned_Success()
        {
            //arrange:
            var statusCode = 200;
            var messages = new string[] { "ashdgajsduy", "2123123131" };
            var content = (ServiceResponse)null;

            //arrange: expected
            var response = new ServiceResponse<ServiceResponse>
            (
                statusCode: statusCode,
                messages: messages,
                content: content
            );

            //act:
            var cloned = (ServiceResponse<ServiceResponse>)response.Clone();

            //assert:
            var comparison = new CompareLogic();
            Assert.IsTrue(comparison.Compare(response, cloned).AreEqual);
        }
    }
}
