using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Data.Memory;
using Logic;
using Models;
using Logic.Interface;
using Data.Interfaces;

namespace DiabetesTests
{
    [TestClass]
    public class MessageTests
    {
        
        IMessageLogic _messageLogic;
        IMessageContext _messageContext;
        
        
        private void InstanceLogic()
        {
            _messageContext = new MessageContextMemory();
            _messageLogic = new MessageLogic(_messageContext);
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
           var messageResult = _messageLogic.ViewMessagesPatient(Enums.AccountType.CareRecipient, 1);

           //Assert
           Assert.AreEqual(expected[0], messageResult[0].MessageId);
           Assert.AreEqual(expected[1], messageResult[1].MessageId);
           Assert.AreEqual(expected[2], messageResult[2].MessageId);
           Assert.AreEqual(3, messageResult.Count);
        }

        [TestMethod]
        public void ViewMessagesPatient_NoCoupleId_ResultEmtyList()
        {
            //Arrange
            InstanceLogic();
            var expected = 0;
            //Act
            var result = _messageLogic.ViewMessagesPatient(Enums.AccountType.CareRecipient, 6);
            //Assert
            Assert.AreEqual(expected, result.Count);
        }

        [TestMethod]
        public void ViewMessagesPatient_AccountTypeDoctor_ResultEmtyList()
        {
            //Arrange
            InstanceLogic();
            var expected = 0;
            //Act
            var Result = _messageLogic.ViewMessagesPatient(Enums.AccountType.Doctor, 2);

            //Assert
            Assert.AreEqual(expected, Result.Count);

        }

        [TestMethod]
        public void ViewMessagesPatient_InexcistandPatient_ResultEmtyList()
        {
            //Arrange
            InstanceLogic();
            var expected = 0;
            var NonExistingPatientId = 12;
            //Act
            var result = _messageLogic.ViewMessagesPatient(Enums.AccountType.CareRecipient, NonExistingPatientId);

            //Assert
            Assert.AreEqual(expected, result.Count);
        }

        [TestMethod]
        public void ViewMessgagesDoctor__DoRightMessagesArrive()
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
            var Result = _messageLogic.ViewMessagesDoctor(Enums.AccountType.Doctor, 2, 3);

            //Assert
            Assert.AreEqual(expectedLength, Result.Count);
            Assert.AreEqual(expected[0], Result[0].MessageId);
            Assert.AreEqual(expected[1], Result[1].MessageId);
            Assert.AreEqual(expected[2], Result[2].MessageId);
            Assert.AreEqual(expected[3], Result[3].MessageId);
        }

        [TestMethod]
        public void ViewMessagesDoctor_AccountTypeDoctor_ResultEmtyList()
        {
            //Arrange
            InstanceLogic();
            var expected = 0;
            //Act
            var Result = _messageLogic.ViewMessagesDoctor(Enums.AccountType.CareRecipient,1,2);

            //Assert
            Assert.AreEqual(expected, Result.Count);

        }

        [TestMethod]
        public void ViewMessagesdoctor_InexcistandPatient_ResultEmtyList()
        {
            //Arrange
            InstanceLogic();
            var expected = 0;
            var NonExistingDoctorId = 12;
            //Act
            var result = _messageLogic.ViewMessagesDoctor(Enums.AccountType.Doctor, NonExistingDoctorId, 1);

            //Assert
            Assert.AreEqual(expected, result.Count);
        }

        [TestMethod]
        public void GetConversationPatient_ReseveRightPatient()
        {
            //Arrange
            InstanceLogic();
            var patientId = 5;
            var expect = 3;

            //Act
            var result = _messageLogic.GetConversationPatient(patientId);

            //Assert
            Assert.AreEqual(expect, result);
        }
        [TestMethod]
        public void GetConversationPatient_NonExsistingPatient()
        {
            //Arrange
            InstanceLogic();
            var nonExistingPatientId = 10;
            var expect = 0;

            //Act
            var result = _messageLogic.GetConversationPatient(nonExistingPatientId);

            //Assert
            Assert.AreEqual(expect, result);
        }

    }
}
