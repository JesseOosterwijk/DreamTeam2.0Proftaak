using Data.Interfaces;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using System;

namespace DiabetesTests
{

    [TestClass]
    public class UserTests
    {
        public Mock<IUserContext> mockContext = new Mock<IUserContext>();

        [TestMethod]
        public void CreateUser_ValidDatabaseCall()
        {
            User mockUser = new Mock<User>(226044440, Enums.AccountType.CareRecipient, "", "Oosterwijk", "jesse.oosterwijk@outlook.com", "testpassword", "Kleidonk 1", "Beuningen", Enums.Gender.Male, 85, DateTime.Today, false).Object;
            UserLogic _logic = new UserLogic(mockContext.Object);
            mockContext.Setup(x => x.CreateUser(mockUser));

            _logic.CreateUser(mockUser);

            mockContext.Verify(x => x.CreateUser(mockUser), Times.Exactly(1));
        }

        [TestMethod]
        public void CheckIfUserAlreadyExists_Tests()
        {
            Mock<User> mockUser = new Mock<User>();
            mockContext.Setup(x => x.CheckIfUserAlreadyExists(""))
                .Returns(It.IsAny<bool>);
            UserLogic _logic = new UserLogic(mockContext.Object);

            bool result = _logic.CheckIfUserAlreadyExists(mockUser.Object.EmailAddress);

            mockContext.Verify(x => x.CheckIfUserAlreadyExists(mockUser.Object.EmailAddress), Times.Exactly(1));
            Assert.IsInstanceOfType(result, typeof(bool));
        }

        [TestMethod]
        public void CheckIfAccountIsActive_Tests()
        {
            Mock<User> mockUser = new Mock<User>();
            mockContext.Setup(x => x.CheckIfAccountIsActive(mockUser.Object.EmailAddress))
                .Returns(It.IsAny<bool>);
            UserLogic _logic = new UserLogic(mockContext.Object);

            bool result = _logic.CheckIfAccountIsActive(mockUser.Object.EmailAddress);

            mockContext.Verify(x => x.CheckIfAccountIsActive(mockUser.Object.EmailAddress), Times.Exactly(1));
            Assert.IsInstanceOfType(result, typeof(bool));
        }

        [TestMethod]
        public void CheckIfEmailIsValid_Tests()
        {
            Mock<User> mockUser = new Mock<User>();
            mockContext.Setup(x => x.CheckIfEmailIsValid(mockUser.Object.EmailAddress))
                .Returns(It.IsAny<bool>);
            UserLogic _logic = new UserLogic(mockContext.Object);

            bool result = _logic.CheckIfEmailIsValid(mockUser.Object.EmailAddress);

            mockContext.Verify(x => x.CheckIfEmailIsValid(mockUser.Object.EmailAddress), Times.Exactly(1));
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        //TODO
        [TestMethod]
        public void CheckIfEmailFails_Tests()
        {
            Mock<User> mockUser = new Mock<User>();
            UserLogic _logic = new UserLogic(mockContext.Object);

            bool result = _logic.CheckIfEmailIsValid("");

            //Assert.ThrowsException<RegexMatchTimeoutException>(() => _logic.CheckIfEmailIsValid("!@!.COM"));
        }

        [TestMethod]
        public void CheckIfAccountIsActive_Fails_Tests()
        {
            Mock<User> mockUser = new Mock<User>();
            mockContext.Setup(x => x.CheckIfAccountIsActive(mockUser.Object.EmailAddress))
                .Returns(false);
            UserLogic _logic = new UserLogic(mockContext.Object);

            bool result = _logic.CheckIfAccountIsActive(mockUser.Object.EmailAddress);

            mockContext.Verify(x => x.CheckIfAccountIsActive(mockUser.Object.EmailAddress), Times.Exactly(1));
            Assert.IsInstanceOfType(result, typeof(bool));
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void GetUserInfo_Tests()
        {
            Mock<User> mockUser = new Mock<User>();
            UserLogic _logic = new UserLogic(mockContext.Object);

            mockContext.Setup(x => x.GetUserInfo(mockUser.Object.EmailAddress))
                .Returns(mockUser.Object);


            User result = _logic.GetUserInfo(mockUser.Object.EmailAddress);

            mockContext.Verify(x => x.GetUserInfo(mockUser.Object.EmailAddress), Times.Once);
            Assert.AreEqual(mockUser.Object, result);
        }

        [TestMethod]
        public void GetUserInfo_Fails_IfOtherEmailIsUsed()
        {
            Mock<User> mockUser = new Mock<User>();
            mockContext.Setup(x => x.GetUserInfo(mockUser.Object.EmailAddress))
                .Returns(mockUser.Object);
            UserLogic _logic = new UserLogic(mockContext.Object);

            User result = _logic.GetUserInfo("");

            Assert.AreNotEqual(mockUser, result);
        }

        [TestMethod]
        public void GetUserById_Tests()
        {
            Mock<User> mockUser = new Mock<User>();
            mockContext.Setup(x => x.GetUserById(mockUser.Object.BSN))
                .Returns(mockUser.Object);
            UserLogic _logic = new UserLogic(mockContext.Object);

            User result = _logic.GetUserById(mockUser.Object.BSN);

            mockContext.Verify(x => x.GetUserById(mockUser.Object.BSN), Times.Once);
            Assert.AreEqual(mockUser.Object, result);
        }

        [TestMethod]
        public void GetUserById_Fails_WrongBSN()
        {
            Mock<User> mockUser = new Mock<User>();
            mockContext.Setup(x => x.GetUserById(mockUser.Object.BSN))
                   .Returns(mockUser.Object);
            UserLogic _logic = new UserLogic(mockContext.Object);

            User result = _logic.GetUserById(9);

            Assert.AreNotEqual(mockUser, result);
        }
        
        [TestMethod]
        public void CheckValidityUser_Succes()
        {
            Mock<User> mockUser = new Mock<User>();
            mockContext.Setup(x => x.CheckValidityUser(mockUser.Object.EmailAddress, mockUser.Object.Password))
                   .Returns(mockUser.Object);

            UserLogic _logic = new UserLogic(mockContext.Object);

            User result = _logic.CheckValidityUser(mockUser.Object.EmailAddress, mockUser.Object.Password);

            mockContext.Verify(x => x.CheckValidityUser(mockUser.Object.EmailAddress, mockUser.Object.Password), Times.Once);
            Assert.AreEqual(mockUser.Object, result);
        }

        [TestMethod]
        public void CheckValidityUser_WrongPassword()
        {
            Mock<User> mockUser = new Mock<User>();
            mockContext.Setup(x => x.CheckValidityUser(mockUser.Object.EmailAddress, mockUser.Object.Password))
                   .Returns(mockUser.Object);

            UserLogic _logic = new UserLogic(mockContext.Object);

            User result = _logic.CheckValidityUser(mockUser.Object.EmailAddress, "456");

            Assert.AreNotEqual(mockUser, result);
        }

        [TestMethod]
        public void CheckValidityUser_WrongEmail()
        {
            Mock<User> mockUser = new Mock<User>();
            mockContext.Setup(x => x.CheckValidityUser(mockUser.Object.EmailAddress, mockUser.Object.Password))
                   .Returns(mockUser.Object);

            UserLogic _logic = new UserLogic(mockContext.Object);

            User result = _logic.CheckValidityUser("", mockUser.Object.Password);

            Assert.AreNotEqual(mockUser, result);
        }

        [TestMethod]
        public void CheckValidityUser_BothWrong()
        {
            Mock<User> mockUser = new Mock<User>();
            mockContext.Setup(x => x.CheckValidityUser(mockUser.Object.EmailAddress, mockUser.Object.Password))
                   .Returns(mockUser.Object);

            UserLogic _logic = new UserLogic(mockContext.Object);

            User result = _logic.CheckValidityUser("", "wrongPass");

            Assert.AreNotEqual(mockUser, result);
        }
    }
}
