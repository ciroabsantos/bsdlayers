using BsdLayers.Business.Commands;
using BsdLayers.Business.Specs;
using KellermanSoftware.CompareNetObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BsdLayers.Business.Tests.Specs
{
    [TestClass, TestCategory("Business.Specfication")]
    public class BusinessSpecMessageTests
    {
        [TestMethod]
        public void Business_Spec_Message_Should_Be_Created_With_Success()
        {
            //arrange:
            var status = BusinessResultStatus.Success;
            var message = "spec message";

            //act:
            var specMessage = new BusinessSpecMessage
            (
                status: status,
                message: message
            );

            //assert:
            Assert.AreEqual(status, specMessage.Status);
            Assert.AreEqual(message, specMessage.Message);
        }

        [TestMethod]
        public void Business_Spec_Message_Clone_Success()
        {
            //arrange:
            var status = BusinessResultStatus.Success;
            var message = "spec message";

            var specMessage = new BusinessSpecMessage
            (
                status: status,
                message: message
            );

            //act:
            var cloned = (BusinessSpecMessage)specMessage.Clone();

            //assert:
            var comparison = new CompareLogic();
            Assert.IsTrue(comparison.Compare(specMessage, cloned).AreEqual);

        }
    }
}
