using BsdLayers.Business.Commands;
using KellermanSoftware.CompareNetObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BsdLayers.Business.Tests.Commands
{
    [TestClass, TestCategory("Business.Command")]
    public class BusinessCommandResultWithContentTests
    {
        [TestMethod]
        public void Command_Result_With_Content_Should_Be_Created_With_Success()
        {
            //arrange:
            var resultId = Guid.NewGuid();
            var status = BusinessResultStatus.Created;

            var messages = new List<string>() { "message 1", "message 2" };
            var content = 10;

            //act:
            var result = new BusinessCommandResult<int>
            (
                resultId: resultId,
                status: status,

                messages: messages,
                content: content
            );

            //assert:
            Assert.AreEqual(resultId, result.ResultId);
            Assert.AreEqual(status, result.Status);

            var comparison = new CompareLogic();
            Assert.IsTrue(comparison.Compare(messages, result.Messages).AreEqual);
        }

        [TestMethod]
        public void Command_Result_With_Type_Should_Be_Created_With_Default_ResultId_Success()
        {
            //arrange:
            var status = BusinessResultStatus.Conflict;
            var messages = new List<string>() { "message 1", "message 2" };
            var content = "content";

            //act:
            var result = new BusinessCommandResult<string>
            (
                status: status,
                messages: messages,
                content: content
            );

            //assert:
            var expectedResult = new BusinessCommandResult<string>
            (
                resultId: result.ResultId,
                status: status,

                messages: messages,
                content: content
            );

            var comparison = new CompareLogic();
            Assert.IsFalse(Guid.Empty.Equals(result.ResultId));
            Assert.IsTrue(comparison.Compare(expectedResult, result).AreEqual);
        }

        [TestMethod]
        public void Command_Result_With_Type_Should_Be_Created_With_Default_ResultId_And_Single_Message_Success()
        {
            //arrange:
            var status = BusinessResultStatus.Conflict;
            var messages = new List<string>() { "single message" };
            var content = 100;

            //act:
            var result = new BusinessCommandResult<int>
            (
                status: status,
                message: messages[0],
                content: content
            );

            //assert:
            var expectedResult = new BusinessCommandResult<int>
            (
                resultId: result.ResultId,
                status: status,

                messages: messages,
                content: content
            );

            var comparison = new CompareLogic();
            Assert.IsFalse(Guid.Empty.Equals(result.ResultId));
            Assert.IsTrue(comparison.Compare(expectedResult, result).AreEqual);
        }

        [TestMethod]
        public void Command_Result_WithType_Should_Be_Created_With_Default_ResultId_And_Empty_Message_Success()
        {
            //arrange:
            var status = BusinessResultStatus.Conflict;
            var messages = new List<string>();
            var content = 8882;

            //act:
            var result = new BusinessCommandResult<int>
            (
                status: status,
                content: content
            );

            //assert:
            var expectedResult = new BusinessCommandResult<int>
            (
                resultId: result.ResultId,
                status: status,

                messages: messages,
                content: content
            );

            var comparison = new CompareLogic();
            Assert.IsFalse(Guid.Empty.Equals(result.ResultId));
            Assert.IsTrue(comparison.Compare(expectedResult, result).AreEqual);
        }

        [TestMethod]
        public void Command_With_Type_Result_Clone_Success()
        {
            //arrange:
            var status = BusinessResultStatus.Success;
            var messages = new List<string>() { "message", "message 2" };

            var resultId = Guid.NewGuid();
            var content = "cloned";

            //arrange:
            var result = new BusinessCommandResult<string>
            (
                resultId: resultId,
                status: status,

                messages: messages,
                content: content
            );

            //act:
            var cloned = (BusinessCommandResult)result.Clone();

            //assert:
            var comparison = new CompareLogic();
            Assert.IsFalse(result == cloned);
            Assert.IsTrue(comparison.Compare(result, cloned).AreEqual);
        }
    }
}
