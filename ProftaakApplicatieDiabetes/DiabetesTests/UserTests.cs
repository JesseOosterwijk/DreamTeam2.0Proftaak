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
        public Mock<IUserContext> mock = new Mock<IUserContext>();
        User user = new Mock<User>(226044440, Enums.AccountType.CareRecipient, "", "Oosterwijk", "jesse.oosterwijk@outlook.com", "testpassword", "Kleidonk 1", "Beuningen", Enums.Gender.Male, 85, DateTime.Today, false).Object;

        [TestMethod]
        public void CreateUser_ValidDatabaseCall()
        {
            UserLogic _logic = new UserLogic(mock.Object);
            _logic.CreateUser(user);

            Assert.AreEqual(user, user);
            mock.Verify(x => x.CreateUser(user), Times.Exactly(1));
        }

        [TestMethod]
        public void CheckIfUserAlreadyExists_Tests()
        {
            mock.Setup(x => x.CheckIfUserAlreadyExists(""))
                .Returns(It.IsAny<bool>);
            UserLogic _logic = new UserLogic(mock.Object);

            bool result = _logic.CheckIfUserAlreadyExists(user.EmailAddress);

            mock.Verify(x => x.CheckIfUserAlreadyExists(user.EmailAddress), Times.Exactly(1));
            Assert.IsInstanceOfType(result, typeof(bool));
        }

        [TestMethod]
        public void CheckIfAccountIsActive_Tests()
        {
            mock.Setup(x => x.CheckIfAccountIsActive(user.EmailAddress))
                .Returns(It.IsAny<bool>);
            UserLogic _logic = new UserLogic(mock.Object);

            bool result = _logic.CheckIfAccountIsActive(user.EmailAddress);

            mock.Verify(x => x.CheckIfAccountIsActive(user.EmailAddress), Times.Exactly(1));
            Assert.IsInstanceOfType(result, typeof(bool));
        }

        [TestMethod]
        public void CheckIfEmailIsValid_Tests()
        {
            mock.Setup(x => x.CheckIfEmailIsValid(user.EmailAddress))
                .Returns(It.IsAny<bool>);
            UserLogic _logic = new UserLogic(mock.Object);

            bool result = _logic.CheckIfEmailIsValid(user.EmailAddress);

            mock.Verify(x => x.CheckIfEmailIsValid(user.EmailAddress), Times.Exactly(1));
            Assert.IsInstanceOfType(result, typeof(bool));
        }
        //TODO
        [TestMethod]
        public void CheckIfEmailFails_Tests()
        {
            UserLogic _logic = new UserLogic(mock.Object);

            bool result = _logic.CheckIfEmailIsValid("");

            //Assert.ThrowsException<RegexMatchTimeoutException>(() => _logic.CheckIfEmailIsValid("!@!.COM"));
        }

        [TestMethod]
        public void CheckIfAccountIsActive_Fails_Tests()
        {
            mock.Setup(x => x.CheckIfAccountIsActive(user.EmailAddress))
                .Returns(false);
            UserLogic _logic = new UserLogic(mock.Object);

            bool result = _logic.CheckIfAccountIsActive(user.EmailAddress);

            mock.Verify(x => x.CheckIfAccountIsActive(user.EmailAddress), Times.Exactly(1));
            Assert.IsInstanceOfType(result, typeof(bool));
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void GetUserInfo_Tests()
        {
            mock.Setup(x => x.GetUserInfo(user.EmailAddress))
                .Returns(user);
            UserLogic _logic = new UserLogic(mock.Object);

            User result = _logic.GetUserInfo(user.EmailAddress);

            mock.Verify(x => x.GetUserInfo(user.EmailAddress), Times.Once);
            Assert.AreEqual(user, result);
        }

        [TestMethod]
        public void GetUserInfo_Fails_IfOtherEmailIsUsed()
        {
            mock.Setup(x => x.GetUserInfo(user.EmailAddress))
                .Returns(user);
            UserLogic _logic = new UserLogic(mock.Object);

            User result = _logic.GetUserInfo("");

            Assert.AreNotEqual(user, result);
        }

        [TestMethod]
        public void GetUserById_Tests()
        {
            mock.Setup(x => x.GetUserById(user.BSN))
                .Returns(user);
            UserLogic _logic = new UserLogic(mock.Object);

            User result = _logic.GetUserById(user.BSN);

            mock.Verify(x => x.GetUserById(user.BSN), Times.Once);
            Assert.AreEqual(user, result);
        }

        [TestMethod]
        public void GetUserById_Fails_WrongBSN()
        {
            mock.Setup(x => x.GetUserById(user.BSN))
                   .Returns(user);
            UserLogic _logic = new UserLogic(mock.Object);

            User result = _logic.GetUserById(9);

            Assert.AreNotEqual(user, result);
        }
        
        [TestMethod]
        public void CheckValidityUser_Succes()
        {
            mock.Setup(x => x.CheckValidityUser(user.EmailAddress, user.Password))
                   .Returns(user);

            UserLogic _logic = new UserLogic(mock.Object);

            User result = _logic.CheckValidityUser(user.EmailAddress, user.Password);

            mock.Verify(x => x.CheckValidityUser(user.EmailAddress, user.Password), Times.Once);
            Assert.AreEqual(user, result);
        }

        [TestMethod]
        public void CheckValidityUser_WrongPassword()
        {
            mock.Setup(x => x.CheckValidityUser(user.EmailAddress, user.Password))
                   .Returns(user);

            UserLogic _logic = new UserLogic(mock.Object);

            User result = _logic.CheckValidityUser(user.EmailAddress, "456");

            Assert.AreNotEqual(user, result);
        }

        [TestMethod]
        public void CheckValidityUser_WrongEmail()
        {
            mock.Setup(x => x.CheckValidityUser(user.EmailAddress, user.Password))
                   .Returns(user);

            UserLogic _logic = new UserLogic(mock.Object);

            User result = _logic.CheckValidityUser("", user.Password);

            Assert.AreNotEqual(user, result);
        }

        [TestMethod]
        public void CheckValidityUser_BothWrong()
        {
            mock.Setup(x => x.CheckValidityUser(user.EmailAddress, user.Password))
                   .Returns(user);

            UserLogic _logic = new UserLogic(mock.Object);

            User result = _logic.CheckValidityUser("", "wrongPass");

            Assert.AreNotEqual(user, result);
        }
    }
}
