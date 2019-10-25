using BsdLayers.Business.Specs;
using KellermanSoftware.CompareNetObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace BsdLayers.Business.Tests.Specs
{
    [TestClass, TestCategory("Business.Specfication")]
    public class AndBusinessSpecTests
    {
        [TestMethod]
        public void AndBusiness_Push_Success()
        {
            //arrange: first spec
            var firstMock = new Mock<IBusinessSpec<object>>();
            var settledMocks = new List<Mock>() { firstMock };
            var successResult = new BusinessSpecResult(isSatisfied: true, status: BusinessResultStatus.Success);

            firstMock
                .Setup(s => s.IsSatisfiedBy(It.IsAny<object>()))
                .Returns(() => { return (BusinessSpecResult)successResult.Clone(); })
                .Verifiable();

            //arrange: second spec
            var secondMock = new Mock<IBusinessSpec<object>>();
            settledMocks.Add(secondMock);

            secondMock
                .Setup(s => s.IsSatisfiedBy(It.IsAny<object>()))
                .Returns(() => { return (BusinessSpecResult)successResult.Clone(); })
                .Verifiable();

            //arrange: create spec
            var spec = new AndBusinessSpec<object>(first: firstMock.Object, second: secondMock.Object);
            var bo = new object();
            Assert.IsTrue(spec.IsSatisfiedBy(bo).IsSatisfied);

            //arrange: third spec
            var thirdMock = new Mock<IBusinessSpec<object>>();
            settledMocks.Add(thirdMock);

            var errorResult = new BusinessSpecResult
            (
                isSatisfied: false,

                messages: new BusinessSpecMessage[]
                {
                    new BusinessSpecMessage
                    (
                        status: BusinessResultStatus.InvalidInputs,
                        message: "invalid name"
                    )
                }
            );

            thirdMock
                .Setup(s => s.IsSatisfiedBy(It.IsAny<object>()))
                .Returns(() => { return (BusinessSpecResult)errorResult.Clone(); })
                .Verifiable();

            //act:
            var result = spec.Push(thirdMock.Object).IsSatisfiedBy(bo);

            //assert:
            Assert.IsFalse(result.IsSatisfied);
            Mock.VerifyAll(settledMocks.ToArray());
        }

        [TestMethod]
        public void AndBusiness_IsSatisfied_Success()
        {
            //arrange: first spec
            var firstMock = new Mock<IBusinessSpec<object>>();
            var settledMocks = new List<Mock>() { firstMock };

            var firstResult = new BusinessSpecResult
            (
                isSatisfied: true,
                status: BusinessResultStatus.Success,

                messages: new BusinessSpecMessage[]
                {
                    new BusinessSpecMessage
                    (
                        status: BusinessResultStatus.Success,
                        message: "info message"
                    )
                }
            );

            firstMock
                .Setup(s => s.IsSatisfiedBy(It.IsAny<object>()))
                .Returns(() => { return (BusinessSpecResult)firstResult.Clone(); })
                .Verifiable();

            //arrange: second spec
            var secondMock = new Mock<IBusinessSpec<object>>();
            settledMocks.Add(secondMock);

            var secondResult = new BusinessSpecResult
            (
                isSatisfied: false,

                messages: new BusinessSpecMessage[]
                {
                    new BusinessSpecMessage
                    (
                        status: BusinessResultStatus.InvalidInputs,
                        message: "invalid input"
                    )
                }
           );

            secondMock
                .Setup(s => s.IsSatisfiedBy(It.IsAny<object>()))
                .Returns(() => { return (BusinessSpecResult)secondResult.Clone(); })
                .Verifiable();

            //arrange: expected result
            var expectedResult = new BusinessSpecResult
            (
                isSatisfied: false,
                status: secondResult.Status,
                messages: firstResult.Messages.Concat(secondResult.Messages)
            );

            //act:
            var spec = new AndBusinessSpec<object>(first: firstMock.Object, second: secondMock.Object);
            var result = spec.IsSatisfiedBy(new object());

            //assert:
            var comparison = new CompareLogic();
            Assert.IsTrue(comparison.Compare(expectedResult, result).AreEqual);
            Mock.Verify(settledMocks.ToArray());
        }
    }
}
