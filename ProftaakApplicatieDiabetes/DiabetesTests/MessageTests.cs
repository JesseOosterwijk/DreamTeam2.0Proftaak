using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Data.Memory;
using Logic;
using Logic.Interface;
using Data.Interfaces;

namespace DiabetesTests
{
    [TestClass]
    public class MessageTests
    {
        
        private IMessageLogic _iMessageLogic;
        private IMessageContext _iMessageContext;
        
        
        private void InstanceLogic()
        {
            _iMessageContext = new MessageContextMemory();
            _iMessageLogic = new MessageLogic(_iMessageContext);
        }
       

        [TestMethod]
        public void ViewMessagesPatient_DoRightMessagesArrive()
        {
           //Arrange
           InstanceLogic();

           var expected = new List<int>();
           expected.Add(1);
           expected.Add(2);
           expected.Add(3);            
           //Act
           var messageResult = _iMessageLogic.ViewMessagesPatient(Enums.AccountType.CareRecipient, 1);

           //Assert
           Assert.AreEqual(expected[0], messageResult[0].MessageId);
           Assert.AreEqual(expected[1], messageResult[1].MessageId);
           Assert.AreEqual(expected[2], messageResult[2].MessageId);
           Assert.AreEqual(3, messageResult.Count);
        }

        [TestMethod]
        public void ViewMessagesPatient_NoCoupleId_ResultEmptyList()
        {
            //Arrange
            InstanceLogic();
            var expected = 0;
            //Act
            var result = _iMessageLogic.ViewMessagesPatient(Enums.AccountType.CareRecipient, 6);
            //Assert
            Assert.AreEqual(expected, result.Count);
        }

        [TestMethod]
        public void ViewMessagesPatient_AccountTypeDoctor_ResultEmptyList()
        {
            //Arrange
            InstanceLogic();
            var expected = 0;
            //Act
            var result = _iMessageLogic.ViewMessagesPatient(Enums.AccountType.Doctor, 2);

            //Assert
            Assert.AreEqual(expected, result.Count);

        }

        [TestMethod]
        public void ViewMessagesPatient_NonexistantPatient_ResultEmptyList()
        {
            //Arrange
            InstanceLogic();
            var expected = 0;
            var NonExistingPatientId = 12;
            //Act
            var result = _iMessageLogic.ViewMessagesPatient(Enums.AccountType.CareRecipient, NonExistingPatientId);

            //Assert
            Assert.AreEqual(expected, result.Count);
        }

        [TestMethod]
        public void ViewMessagesDoctor__DoRightMessagesArrive()
        {
            //Arrange
            InstanceLogic();
            var expected = new List<int>();
            expected.Add(7);
            expected.Add(6);
            expected.Add(5);
            expected.Add(4);
            var expectedLength = 4;

            //Act
            var result = _iMessageLogic.ViewMessagesDoctor(Enums.AccountType.Doctor, 2, 3);

            //Assert
            Assert.AreEqual(expectedLength, result.Count);
            Assert.AreEqual(expected[0], result[0].MessageId);
            Assert.AreEqual(expected[1], result[1].MessageId);
            Assert.AreEqual(expected[2], result[2].MessageId);
            Assert.AreEqual(expected[3], result[3].MessageId);
        }

        [TestMethod]
        public void ViewMessagesDoctor_AccountTypeDoctor_ResultEmptyList()
        {
            //Arrange
            InstanceLogic();
            var expected = 0;
            //Act
            var result = _iMessageLogic.ViewMessagesDoctor(Enums.AccountType.CareRecipient,1,2);

            //Assert
            Assert.AreEqual(expected, result.Count);

        }

        [TestMethod]
        public void ViewMessagesDoctor_NonexistantPatient_ResultEmptyList()
        {
            //Arrange
            InstanceLogic();
            var expected = 0;
            var NonExistingDoctorId = 12;
            //Act
            var result = _iMessageLogic.ViewMessagesDoctor(Enums.AccountType.Doctor, NonExistingDoctorId, 1);

            //Assert
            Assert.AreEqual(expected, result.Count);
        }

        [TestMethod]
        public void GetConversationPatient_ReserveRightPatient()
        {
            //Arrange
            InstanceLogic();
            var patientId = 5;
            var expect = 3;

            //Act
            var result = _iMessageLogic.GetConversationPatient(patientId);

            //Assert
            Assert.AreEqual(expect, result);
        }
        [TestMethod]
        public void GetConversationPatient_NonExistantPatient()
        {
            //Arrange
            InstanceLogic();
            var nonExistingPatientId = 10;
            var expect = 0;

            //Act
            var result = _iMessageLogic.GetConversationPatient(nonExistingPatientId);

            //Assert
            Assert.AreEqual(expect, result);
        }

    }
}
