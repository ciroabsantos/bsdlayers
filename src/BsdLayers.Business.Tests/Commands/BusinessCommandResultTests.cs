using BsdLayers.Business.Commands;
using KellermanSoftware.CompareNetObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BsdLayers.Business.Tests.Commands
{
    [TestClass, TestCategory("Business.Command")]
    public class BusinessCommandResultTests
    {
        [TestMethod]
        public void Command_Result_Should_Be_Created_With_Success()
        {
            //arrange:
            var resultId = Guid.NewGuid();
            var status = BusinessResultStatus.Created;
            var messages = new List<string>() { "message 1", "message 2" };

            //act:
            var result = new BusinessCommandResult
            (
                resultId: resultId,
                status: status,
                messages: messages
            );

            //assert:
            Assert.AreEqual(resultId, result.ResultId);
            Assert.AreEqual(status, result.Status);

            var comparison = new CompareLogic();
            Assert.IsTrue(comparison.Compare(messages, result.Messages).AreEqual);
        }

        [TestMethod]
        public void Command_Result_Should_Be_Created_With_Default_ResultId_Success()
        {
            //arrange:
            var status = BusinessResultStatus.Conflict;
            var messages = new List<string>() { "message 1", "message 2" };

            //act:
            var result = new BusinessCommandResult
            (
                status: status,
                messages: messages
            );

            //assert:
            Assert.IsFalse(Guid.Empty.Equals(result.ResultId));
            var expectedResult = new BusinessCommandResult(result.ResultId, status, messages);

            var comparison = new CompareLogic();
            Assert.IsTrue(comparison.Compare(expectedResult, result).AreEqual);
        }

        [TestMethod]
        public void Command_Result_Should_Be_Created_With_Default_ResultId_And_Single_Message_Success()
        {
            //arrange:
            var status = BusinessResultStatus.Conflict;
            var messages = new List<string>() { "single message" };

            //act:
            var result = new BusinessCommandResult
            (
                status: status,
                message: messages[0]
            );

            //assert:
            Assert.IsFalse(Guid.Empty.Equals(result.ResultId));
            var expectedResult = new BusinessCommandResult(result.ResultId, status, messages);

            var comparison = new CompareLogic();
            Assert.IsTrue(comparison.Compare(expectedResult, result).AreEqual);
        }

        [TestMethod]
        public void Command_Result_Should_Be_Created_With_Default_ResultId_And_Empty_Message_Success()
        {
            //arrange:
            var status = BusinessResultStatus.Conflict;
            var messages = new List<string>();

            //act:
            var result = new BusinessCommandResult(status: status);

            //assert:
            Assert.IsFalse(Guid.Empty.Equals(result.ResultId));
            var expectedResult = new BusinessCommandResult(result.ResultId, status, messages);

            var comparison = new CompareLogic();
            Assert.IsTrue(comparison.Compare(expectedResult, result).AreEqual);
        }

        [TestMethod]
        public void Command_Result_Clone_Success()
        {
            //arrange:
            var status = BusinessResultStatus.Success;
            var messages = new List<string>() { "message", "message 2" };
            var resultId = Guid.NewGuid();

            //arrange:
            var result = new BusinessCommandResult
            (
                resultId: resultId,
                status: status,
                messages: messages
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
