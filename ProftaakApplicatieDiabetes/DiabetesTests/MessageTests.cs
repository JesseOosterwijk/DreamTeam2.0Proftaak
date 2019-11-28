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
        /*
        IMessageLogic _messageLogic;
        IMessageContext _messageContext;
        
        
        public MessageTests()
        {
            _messageContext = new MessageContextMemory();
            _messageLogic = new MessageLogic(_messageContext);
        }
       

        [TestMethod]
         public void ViewMessagesPatient_DoRightMessagesArrive()
         {
            //Arrange
            var Expected = new int[4];
            Expected[0] = 4;
            Expected[1] = 5;
            Expected[2] = 6;
            Expected[3] = 7;
            
            //Act
            var messageResult = _messageLogic.ViewMessagesPatient(Enums.AccountType.CareRecipient, 1);
            var intResult = new List<int>();

            foreach(MessageModel message in messageResult)
            {
                intResult.Add(message.MessageId);
            }
            intResult.ToArray();

            //Assert
            Assert.AreEqual(intResult[0], Expected[0]);
            Assert.AreEqual(intResult[1], Expected[1]);
            Assert.AreEqual(intResult[2], Expected[2]);
            Assert.AreEqual(intResult[3], Expected[3]);
         }
         */
    }
}
