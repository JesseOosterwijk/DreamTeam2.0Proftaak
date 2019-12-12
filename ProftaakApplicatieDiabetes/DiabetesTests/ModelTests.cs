using Data.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProftaakApplicatieDiabetes.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiabetesTests
{
    [TestClass]
    public class ModelTests
    {
        [TestMethod]
        public void Model_Is_Sent_Succesfully()
        {
            UserViewModel model = new UserViewModel()
            {
                UserId = 1,
                UserBSN = 123456789,
                FirstName = "Jasper",
                LastName = "Kohlen",
                Address = "Kerkrade",
                Residence = "Limburg",
                EmailAddress = "Jasperkohlen@hotmail.com",
                BirthDate = DateTime.Now,
                Weight = 70
            };

            var errorresults = TestModelHelper.Validate(model);

            Assert.AreEqual(errorresults.Count, 0);
        }

        [TestMethod]
        public void Model_Fails_If_No_BSN_Is_Given()
        {
            UserViewModel model = new UserViewModel()
            {
                UserId = 1,
                FirstName = "Jasper",
                LastName = "Kohlen",
                Address = "Kerkrade",
                Residence = "Limburg",
                EmailAddress = "Jasperkohlen@hotmail.com",
                BirthDate = DateTime.Now,
                Weight = 70
            };

            var errorresults = TestModelHelper.Validate(model);

            Assert.AreEqual(errorresults[0].ErrorMessage, "Zorg ervoor dat uw BSN klopt");
        }

        [TestMethod]
        public void Model_Fails_If_No_FirstName_Is_Given()
        {
            UserViewModel model = new UserViewModel()
            {
                UserId = 1,
                UserBSN = 123456789,
                LastName = "Kohlen",
                Address = "Kerkrade",
                Residence = "Limburg",
                EmailAddress = "Jasperkohlen@hotmail.com",
                BirthDate = DateTime.Now,
                Weight = 70
            };

            var errorresults = TestModelHelper.Validate(model);

            Assert.AreEqual(errorresults[0].ErrorMessage, "Voornaam vereist!");
        }

        [TestMethod]
        public void Model_Fails_If_No_LastName_Is_Given()
        {
            UserViewModel model = new UserViewModel()
            {
                UserId = 1,
                UserBSN = 123456789,
                FirstName = "Jasper",
                Address = "Kerkrade",
                Residence = "Limburg",
                EmailAddress = "Jasperkohlen@hotmail.com",
                BirthDate = DateTime.Now,
                Weight = 70
            };

            var errorresults = TestModelHelper.Validate(model);

            Assert.AreEqual(errorresults[0].ErrorMessage, "Achternaam vereist!");
        }

        [TestMethod]
        public void Model_Fails_If_No_Address_Is_Given()
        {
            UserViewModel model = new UserViewModel()
            {
                UserId = 1,
                UserBSN = 123456789,
                FirstName = "Jasper",
                LastName = "Kohlen",
                Residence = "Limburg",
                EmailAddress = "Jasperkohlen@hotmail.com",
                BirthDate = DateTime.Now,
                Weight = 70
            };

            var errorresults = TestModelHelper.Validate(model);

            Assert.AreEqual(errorresults[0].ErrorMessage, "Adres vereist!");
        }

        [TestMethod]
        public void Model_Fails_If_No_Residence_Is_Given()
        {
            UserViewModel model = new UserViewModel()
            {
                UserId = 1,
                UserBSN = 123456789,
                FirstName = "Jasper",
                LastName = "Kohlen",
                Address = "Kerkrade",
                EmailAddress = "Jasperkohlen@hotmail.com",
                BirthDate = DateTime.Now,
                Weight = 70
            };

            var errorresults = TestModelHelper.Validate(model);

            Assert.AreEqual(errorresults[0].ErrorMessage, "Woonplaats vereist!");
        }

        [TestMethod]
        public void Model_Fails_If_No_Email_Is_Given()
        {
            UserViewModel model = new UserViewModel()
            {
                UserId = 1,
                UserBSN = 123456789,
                FirstName = "Jasper",
                LastName = "Kohlen",
                Address = "Kerkrade",
                Residence = "Limburg",
                BirthDate = DateTime.Now,
                Weight = 70
            };

            var errorresults = TestModelHelper.Validate(model);

            Assert.AreEqual(errorresults[0].ErrorMessage, "EmailAdres vereist!");
        }

        [TestMethod]
        public void Model_Fails_If_No_Valid_Email_Is_Given()
        {
            UserViewModel model = new UserViewModel()
            {
                UserId = 1,
                UserBSN = 123456789,
                FirstName = "Jasper",
                LastName = "Kohlen",
                Address = "Kerkrade",
                Residence = "Limburg",
                EmailAddress = "Jasperkohlenhotmailcom",
                BirthDate = DateTime.Now,
                Weight = 70
            };

            var errorresults = TestModelHelper.Validate(model);

            Assert.AreEqual(errorresults.Count, 1);
        }

        [TestMethod]
        public void Model_Fails_If_BSN_Has_More_Than_Nine_Numbers()
        {
            UserViewModel model = new UserViewModel()
            {
                UserId = 1,
                UserBSN = 1234567899,
                FirstName = "Jasper",
                LastName = "Kohlen",
                Address = "Kerkrade",
                Residence = "Limburg",
                EmailAddress = "Jasperkohlen@hotmail.com",
                BirthDate = DateTime.Now,
                Weight = 70
            };

            var errorresults = TestModelHelper.Validate(model);

            Assert.AreEqual(errorresults.Count, 1);
            Assert.AreEqual(errorresults[0].ErrorMessage, "Zorg ervoor dat uw BSN klopt");
        }

        [TestMethod]
        public void Model_Fails_If_BSN_Has_Less_Than_Nine_Numbers()
        {
            UserViewModel model = new UserViewModel()
            {
                UserId = 1,
                UserBSN = 12345678,
                FirstName = "Jasper",
                LastName = "Kohlen",
                Address = "Kerkrade",
                Residence = "Limburg",
                EmailAddress = "Jasperkohlen@hotmail.com",
                BirthDate = DateTime.Now,
                Weight = 70
            };

            var errorresults = TestModelHelper.Validate(model);

            Assert.AreEqual(errorresults.Count, 1);
            Assert.AreEqual(errorresults[0].ErrorMessage, "Zorg ervoor dat uw BSN klopt");
        }
    }
}
