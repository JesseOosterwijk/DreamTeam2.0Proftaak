using Data.Interfaces;
using Data.Memory;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiabetesTests
{
    [TestClass]
    public class PatientCalcOverviewTest
    {
        DoctorContextMemory doctorMemory = new DoctorContextMemory();

        [TestMethod]
        public void GetAllLinkedPatients()
        {
            User doctor = new User(1, "Jasper", Enums.AccountType.Doctor);
            User patient1 = new User(2, "Jesse", Enums.AccountType.CareRecipient);
            User patient2 = new User(3, "Anuwat", Enums.AccountType.CareRecipient);
            User patient3 = new User(4, "Mark", Enums.AccountType.CareRecipient);

            User doctor2 = new User(5, "Luuk", Enums.AccountType.Doctor);
            User patient4 = new User(6, "Dennis", Enums.AccountType.CareRecipient);

            doctorMemory.MakeCouple(doctor, patient1);
            doctorMemory.MakeCouple(doctor, patient2);
            doctorMemory.MakeCouple(doctor, patient3);

            doctorMemory.MakeCouple(doctor2, patient4);

            Assert.AreEqual(doctorMemory.GetAllLinkedPatients(doctor.UserId).Count(), 3);
            Assert.AreEqual(doctorMemory.GetAllLinkedPatients(doctor2.UserId).Count(), 1);
        }

        [TestMethod]
        public void CalculationsOrderNewestFirst()
        {
            Calculation oldest = new Calculation(new DateTime(2019, 11, 11));
            Calculation middle = new Calculation(new DateTime(2019, 11, 12));
            Calculation latest = new Calculation(new DateTime(2019, 11, 13));

            doctorMemory.AddToTestList(oldest);
            doctorMemory.AddToTestList(middle);
            doctorMemory.AddToTestList(latest);

            CollectionAssert.AreNotEqual(doctorMemory.SortedTestCalcs(), doctorMemory.TestCalcs());
            Assert.AreEqual(doctorMemory.SortedTestCalcs()[0], latest);
            Assert.AreEqual(doctorMemory.SortedTestCalcs()[1], middle);
            Assert.AreEqual(doctorMemory.SortedTestCalcs()[2], oldest);
        }

        [TestMethod]
        public void DoctorCanSeeAllLinkedPatients_DatabaseCall()
        {
            Mock<User> mockUser = new Mock<User>();
            Mock<IDoctorContext> mockContext = new Mock<IDoctorContext>();
            DoctorLogic _logic = new DoctorLogic(mockContext.Object);
            mockContext.Setup(x => x.GetAllLinkedPatients(9))
                .Returns(doctorMemory.GetAllLinkedPatients(9));

            var result = _logic.GetAllLinkedPatients(9);

            mockContext.Verify(x => x.GetAllLinkedPatients(9), Times.Once);
        }

        [TestMethod]
        public void DoctorCanSeePatientData()
        {
            Mock<User> mockUser = new Mock<User>();
            Mock<IDoctorContext> mockContext = new Mock<IDoctorContext>();
            DoctorLogic _logic = new DoctorLogic(mockContext.Object);
            mockContext.Setup(x => x.GetPatientData(9))
                .Returns(doctorMemory.GetPatientData(9));

            doctorMemory.MakeTestCalcs(3, 9, "Jan", "Man");
            var result = _logic.GetPatientData(9);

            Assert.AreEqual(result.Count(), 3);
        }

        [TestMethod]
        public void DoctorCanSeePatientData_WhenPatientHasInfoShareEnabled()
        {
            //Arrange
            doctorMemory.MakeTestCalcs(3, 9, "Jan", "Man");

            Mock<IDoctorContext> mockDoctorContext = new Mock<IDoctorContext>();
            DoctorLogic _doctorlogic = new DoctorLogic(mockDoctorContext.Object);
            mockDoctorContext.Setup(x => x.GetPatientData(9))
                .Returns(doctorMemory.GetPatientData(9));

            Mock<IAccountContext> mockAccountContext = new Mock<IAccountContext>();
            AccountLogic _aLogic = new AccountLogic(mockAccountContext.Object);
            mockAccountContext.Setup(x => x.SharingIsEnabled(9))
                .Returns(true);

            //Act
            var isEnabled = _aLogic.SharingIsEnabled(9);
            List<Calculation> resultData = new List<Calculation>();
            if (isEnabled)
            {
                resultData = _doctorlogic.GetPatientData(9).ToList();
            }

            //Assert
            Assert.AreEqual(resultData.Count(), 3);
        }

        [TestMethod]
        public void DoctorCanNotSeePatientData_WhenPatientDoesNotHaveInfoShareEnabled()
        {
            //Arrange
            doctorMemory.MakeTestCalcs(3, 9, "Jan", "Man");

            Mock<IDoctorContext> mockDoctorContext = new Mock<IDoctorContext>();
            DoctorLogic _doctorlogic = new DoctorLogic(mockDoctorContext.Object);
            mockDoctorContext.Setup(x => x.GetPatientData(9))
                .Returns(doctorMemory.GetPatientData(9));

            Mock<IAccountContext> mockAccountContext = new Mock<IAccountContext>();
            AccountLogic _aLogic = new AccountLogic(mockAccountContext.Object);
            mockAccountContext.Setup(x => x.SharingIsEnabled(9))
                .Returns(false);

            //Act
            var isEnabled = _aLogic.SharingIsEnabled(9);
            List<Calculation> resultData = new List<Calculation>();
            if (isEnabled)
            {
                resultData = _doctorlogic.GetPatientData(9).ToList();
            }

            //Assert
            Assert.AreEqual(resultData.Count(), 0);
        }
    }
}
