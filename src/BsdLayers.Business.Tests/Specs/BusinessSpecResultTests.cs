using BsdLayers.Business.Specs;
using KellermanSoftware.CompareNetObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BsdLayers.Business.Tests.Specs
{
    [TestClass, TestCategory("Business.Specfication")]
    public class BusinessSpecResultTests
    {
        [TestMethod]
        public void BusinessSpecResult_Full_Constructor_Success()
        {
            //arrange:
            var isSatisfied = false;
            var status = BusinessResultStatus.InvalidInputs;

            var messages = new List<BusinessSpecMessage>()
            {
                new BusinessSpecMessage(status: status, message: "invalid inputs")
            };

            //act:
            var result = new BusinessSpecResult
            (
                isSatisfied: isSatisfied,
                status: status,
                messages: messages
            );

            //assert:
            Assert.AreEqual(isSatisfied, result.IsSatisfied);
            Assert.AreEqual(status, result.Status);

            var comparison = new CompareLogic();
            Assert.IsTrue(comparison.Compare(messages, result.Messages).AreEqual);
        }

        [TestMethod]
        public void BusinessSpecResult_Constructor_Empty_Messages_Success()
        {
            //arrange:
            var isSatisfied = false;
            var status = BusinessResultStatus.InvalidInputs;
            var messages = new List<BusinessSpecMessage>();

            //act:
            var result = new BusinessSpecResult
            (
                isSatisfied: isSatisfied,
                status: status
            );

            //assert:
            Assert.AreEqual(isSatisfied, result.IsSatisfied);
            Assert.AreEqual(status, result.Status);

            var comparison = new CompareLogic();
            Assert.IsTrue(comparison.Compare(messages, result.Messages).AreEqual);
        }

        [TestMethod]
        public void BusinessSpecResult_Constructor_Status_From_Messages_Success()
        {
            //arrange: 
            var status = BusinessResultStatus.Locked;

            var messages = new List<BusinessSpecMessage>()
            {
                new BusinessSpecMessage(status: status, message: "locked resource"),
                new BusinessSpecMessage(status: BusinessResultStatus.InternalError, message: "internal server error"),
            };

            var expectedResult = new BusinessSpecResult
            (
                isSatisfied: false,
                status: status,
                messages: messages
            );

            //act:
            var result = new BusinessSpecResult(isSatisfied: false, messages: messages);
            var comparison = new CompareLogic();
            Assert.IsTrue(comparison.Compare(expectedResult, result).AreEqual);
        }

        [TestMethod]
        public void BusinessSpecResult_Clone_Success()
        {
            //arrange:
            var messages = new List<BusinessSpecMessage>
            {
                new BusinessSpecMessage(status: BusinessResultStatus.InternalError, message: "internal error")
            };

            var result = new BusinessSpecResult(isSatisfied: false, messages: messages);

            //act:
            var cloned = (BusinessSpecResult)result.Clone();

            //assert:
            var comparison = new CompareLogic();
            Assert.IsFalse(result == cloned);
            Assert.IsTrue(comparison.Compare(result, cloned).AreEqual);
        }
    }
}
