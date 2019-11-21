using Data.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
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
        public void GetPatientData()
        {
            doctorMemory.MakeTestCalcs(2, 0, "Jasper", "Kohlen");
            doctorMemory.MakeTestCalcs(2, 1, "Jesse", "Oosterwijk");

            var countJasperCalcs = doctorMemory.GetPatientData(0).Count();
            var countJesseCalcs = doctorMemory.GetPatientData(1).Count();

            Assert.AreEqual(countJasperCalcs, 2);
            Assert.AreEqual(countJesseCalcs, 2);
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

            CollectionAssert.AreNotEqual(doctorMemory.SortedTestCalcs(), doctorMemory.UnsortedTestCalcs());
            Assert.AreEqual(doctorMemory.SortedTestCalcs()[0], latest);
            Assert.AreEqual(doctorMemory.SortedTestCalcs()[1], middle);
            Assert.AreEqual(doctorMemory.SortedTestCalcs()[2], oldest);
        }
    }
}
