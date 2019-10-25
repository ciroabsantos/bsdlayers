using BsdLayers.Business.Specs;
using KellermanSoftware.CompareNetObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace BsdLayers.Business.Tests.Specs
{
    [TestClass, TestCategory("Business.Specfication")]
    public class OrBusinessSpecTests
    {
        [TestMethod]
        public void OrBusinessSpec_Push_Success()
        {
            //arrange: first spec
            var firstSpecMock = new Mock<IBusinessSpec<object>>();
            var settledMocks = new List<Mock>() { firstSpecMock };
            var failResult = new BusinessSpecResult(isSatisfied: false, status: BusinessResultStatus.InvalidInputs);

            firstSpecMock
                .Setup(s => s.IsSatisfiedBy(It.IsAny<object>()))
                .Returns(() => { return (BusinessSpecResult)failResult.Clone(); })
                .Verifiable();

            //arrange: second spec
            var secondSpecMock = new Mock<IBusinessSpec<object>>();
            settledMocks.Add(secondSpecMock);

            secondSpecMock
                .Setup(s => s.IsSatisfiedBy(It.IsAny<object>()))
                .Returns(() => { return (BusinessSpecResult)failResult.Clone(); })
                .Verifiable();

            //arrange: third spec
            var thirdSpecMock = new Mock<IBusinessSpec<object>>();
            settledMocks.Add(thirdSpecMock);

            thirdSpecMock
                .Setup(s => s.IsSatisfiedBy(It.IsAny<object>()))
                .Returns(() => { return new BusinessSpecResult(isSatisfied: true, status: BusinessResultStatus.Success); })
                .Verifiable();

            //arrange composed spec:
            var bo = new object();
            var composedSpec = new OrBusinessSpec<object>(firstSpecMock.Object, secondSpecMock.Object);
            Assert.IsFalse(composedSpec.IsSatisfiedBy(bo).IsSatisfied);

            //act:
            composedSpec = composedSpec.Push(thirdSpecMock.Object);

            //assert:
            Assert.IsTrue(composedSpec.IsSatisfiedBy(bo).IsSatisfied);
            Mock.Verify(settledMocks.ToArray());
        }

        [TestMethod]
        public void OrBusinessSpec_IsStatisfied_Should_Return_Success_Result()
        {
            //arrange: first spec
            var firstSpecMock = new Mock<IBusinessSpec<object>>();
            var settledMocks = new List<Mock>() { firstSpecMock };

            var firstResult = new BusinessSpecResult
            (
                isSatisfied: true,

                messages: new BusinessSpecMessage[]
                {
                    new BusinessSpecMessage(status: BusinessResultStatus.Success, message: "success message")
                }
            );

            firstSpecMock
                .Setup(s => s.IsSatisfiedBy(It.IsAny<object>()))
                .Returns(() => { return (BusinessSpecResult)firstResult.Clone(); })
                .Verifiable();

            //arrange: second spec
            var secondSpecMock = new Mock<IBusinessSpec<object>>();
            settledMocks.Add(secondSpecMock);

            var secondResult = new BusinessSpecResult
            (
                isSatisfied: false,

                messages: new BusinessSpecMessage[]
                {
                    new BusinessSpecMessage(status: BusinessResultStatus.InvalidInputs, message: "invalid inputs")
                }
            );

            secondSpecMock
                .Setup(s => s.IsSatisfiedBy(It.IsAny<object>()))
                .Returns(() => { return (BusinessSpecResult)secondResult.Clone(); })
                .Verifiable();

            //arrange:
            var expectedResult = new BusinessSpecResult
            (
                isSatisfied: true,
                status: BusinessResultStatus.Success,
                messages: firstResult.Messages.Concat(secondResult.Messages)
            );

            //act:
            var composedSpec = new OrBusinessSpec<object>(first: firstSpecMock.Object, second: secondSpecMock.Object);
            var result = composedSpec.IsSatisfiedBy(bo: new object());

            //assert:
            var comparison = new CompareLogic();
            Assert.IsTrue(comparison.Compare(expectedResult, result).AreEqual);
            Mock.Verify(settledMocks.ToArray());
        }

        [TestMethod]
        public void OrBusinessSpec_IsStatisfied_Should_Return_Invalid_Inputs_Result()
        {
            //arrange: first spec
            var firstSpecMock = new Mock<IBusinessSpec<object>>();
            var settledMocks = new List<Mock>() { firstSpecMock };

            var firstResult = new BusinessSpecResult
            (
                isSatisfied: false,

                messages: new BusinessSpecMessage[]
                {
                    new BusinessSpecMessage(status: BusinessResultStatus.InvalidInputs, message: "invalid inputs")
                }
            );

            firstSpecMock
                .Setup(s => s.IsSatisfiedBy(It.IsAny<object>()))
                .Returns(() => { return (BusinessSpecResult)firstResult.Clone(); })
                .Verifiable();

            //arrange: second spec
            var secondSpecMock = new Mock<IBusinessSpec<object>>();
            settledMocks.Add(secondSpecMock);

            var secondResult = new BusinessSpecResult
            (
                isSatisfied: false,

                messages: new BusinessSpecMessage[]
                {
                    new BusinessSpecMessage(status: BusinessResultStatus.InternalError, message: "internal error")
                }
            );

            secondSpecMock
                .Setup(s => s.IsSatisfiedBy(It.IsAny<object>()))
                .Returns(() => { return (BusinessSpecResult)secondResult.Clone(); })
                .Verifiable();

            //arrange:
            var expectedResult = new BusinessSpecResult
            (
                isSatisfied: false,
                status: BusinessResultStatus.InvalidInputs,
                messages: firstResult.Messages.Concat(secondResult.Messages)
            );

            //act:
            var composedSpec = new OrBusinessSpec<object>(first: firstSpecMock.Object, second: secondSpecMock.Object);
            var result = composedSpec.IsSatisfiedBy(bo: new object());

            //assert:
            var comparison = new CompareLogic();
            Assert.IsTrue(comparison.Compare(expectedResult, result).AreEqual);
            Mock.Verify(settledMocks.ToArray());
        }
    }
}
