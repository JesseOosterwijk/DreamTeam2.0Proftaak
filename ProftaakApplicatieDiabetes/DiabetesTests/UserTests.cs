using Autofac.Extras.Moq;
using Data.Contexts;
using Data.Interfaces;
using Logic.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using ProftaakApplicatieDiabetes.Controllers;
using ProftaakApplicatieDiabetes.Models;
using System;

namespace DiabetesTests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void CreateUser_ValidCall()
        {
            //var mock = new Mock<IUserLogic>();
            //var user = new CareRecipient(1, "a", "b", "c,", "d", "f", Convert.ToDateTime("1988/12/20"), Enums.Gender.Male, true, Enums.AccountType.CareRecipient, "");
            //var userModel = new UserViewModel(user);

            //mock.Setup(x => x.CreateUser(user));

            //UserController userController = new UserController(mock.Object);

            //var result = userController.CreateAccount();
            //var viewResult = (ViewResult)result;
            //var model = viewResult.ViewData.Model;

            //Assert.AreEqual(user, model);
        }

        [TestMethod]
        public void CheckIfUserAlreadyExists_ValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var user = new CareRecipient(1, "a", "b", "c,", "d", "f", Convert.ToDateTime("1988/12/20"), Enums.Gender.Male, true, Enums.AccountType.CareRecipient, "");

                mock.Mock<IUserContext>()
                    .Setup(x => x.CheckIfUserAlreadyExists(user.EmailAddress));

                var cls = mock.Create<UserContextSQL>();

                cls.CheckIfUserAlreadyExists(user.EmailAddress);

                mock.Mock<IUserContext>()
                    .Verify(x => x.CheckIfUserAlreadyExists(user.EmailAddress), Times.Exactly(0));
            }
        }

    }
}
