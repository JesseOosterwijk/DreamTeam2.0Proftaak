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

            var Expected = new List<int>();
            Expected.Add(1);
            Expected.Add(2);
            Expected.Add(3);
            
            //Act
            var messageResult = _messageLogic.ViewMessagesPatient(Enums.AccountType.CareRecipient, 1);

            //Assert
            Assert.AreEqual(Expected[0], messageResult[0].MessageId);
            Assert.AreEqual(Expected[1], messageResult[1].MessageId);
            Assert.AreEqual(Expected[2], messageResult[2].MessageId);
            Assert.AreEqual(3, messageResult.Count);
         }
         
        
         
    }
}
