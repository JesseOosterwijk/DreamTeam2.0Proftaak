using Data.Interfaces;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;

namespace DiabetesTests
{
    [TestClass]
    public class AccountTests
    {
        [TestMethod]
        public void AllowInfoSharing_ValidDatabaseCall()
        {
            Mock<IAccountContext> mockContext = new Mock<IAccountContext>();
            AccountLogic _logic = new AccountLogic(mockContext.Object);

            _logic.AllowInfoSharing(1);
            mockContext.Verify(x => x.AllowInfoSharing(1), Times.Once);
        }

        [TestMethod]
        public void DisableInfoSharing_ValidDatabaseCall()
        {
            Mock<IAccountContext> mockContext = new Mock<IAccountContext>();
            AccountLogic _logic = new AccountLogic(mockContext.Object);

            _logic.DisableInfoSharing(1);
            mockContext.Verify(x => x.DisableInfoSharing(1), Times.Once);
        }

        [TestMethod]
        public void SharingIsEnabled_GetsCalledCorrectly_CorrectUserSucceeds()
        {
            Mock <User> mockUser = new Mock<User>();
            Mock<IAccountContext> mockContext = new Mock<IAccountContext>();
            AccountLogic _logic = new AccountLogic(mockContext.Object);
            mockContext.Setup(x => x.SharingIsEnabled(mockUser.Object.UserId))
                .Returns(mockUser.Object.InfoSharing);

            var result = _logic.SharingIsEnabled(mockUser.Object.UserId);

            Assert.IsInstanceOfType(result, typeof(bool));
            Assert.AreEqual(mockUser.Object.InfoSharing, result);
        }

        [TestMethod]
        public void SharingIsEnabled_GetsCalledCorrectly_WrongUserFails()
        {
            Mock<User> mockUser = new Mock<User>();
            Mock<IAccountContext> mockContext = new Mock<IAccountContext>();
            AccountLogic _logic = new AccountLogic(mockContext.Object);
            mockContext.Setup(x => x.SharingIsEnabled(9))
                .Returns(true);

            var result = _logic.SharingIsEnabled(9);

            Assert.IsInstanceOfType(result, typeof(bool));
            Assert.AreNotEqual(mockUser.Object.InfoSharing, result);
        }

        [TestMethod]
        public void WeightIsUpdated_DatabaseCall()
        {
            Mock<IAccountContext> mockContext = new Mock<IAccountContext>();
            AccountLogic _logic = new AccountLogic(mockContext.Object);
            mockContext.Setup(x => x.UpdateWeight(70, 9));

            _logic.UpdateWeight(70, 9);
            mockContext.Verify(x => x.UpdateWeight(70, 9), Times.Once);
        }
    }
}
