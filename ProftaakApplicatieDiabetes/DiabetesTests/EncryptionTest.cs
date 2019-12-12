using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Data.Memory;

namespace DiabetesTests
{
    [TestClass]
    public class EncryptionTest
    {
        [TestMethod]
        public void Encrypt()
        {
            //arrange
            EncryptionKeyCreator key = new EncryptionKeyCreator();
            var password = "test";
            var encryptionString = key.KeyCreator();
            var encryptedPassword = "";

            //act
            encryptedPassword =  Encrypting.Encrypt(password, encryptionString);

            //assert
            Assert.AreNotEqual(password, encryptedPassword);
        }

        [TestMethod]
        public void EncryptAndDecrypt()
        {
            //arrange
            EncryptionKeyCreator key = new EncryptionKeyCreator();
            var password = "test";
            var encryptionString = key.KeyCreator();
            var encryptedPassword = "";
            var decryptedPassword = "";

            //act
            encryptedPassword = Encrypting.Encrypt(password, encryptionString);
            decryptedPassword = Encrypting.Decrypt(encryptedPassword, encryptionString);

            //assert
            Assert.AreNotEqual(password, encryptedPassword);
            Assert.AreEqual(password, decryptedPassword);
        }

        [TestMethod]
        public void FailDecrypt()
        {
            //arrange
            EncryptionKeyCreator key = new EncryptionKeyCreator();
            var password = "test";
            var encryptionString = key.KeyCreator();
            var encryptedPassword = "";
            var decryptedPassword = "";

            //act
            try
            {
                encryptedPassword = Encrypting.Encrypt(password, encryptionString);
                decryptedPassword = Encrypting.Decrypt(encryptedPassword, "");
            }

            //assert
            catch(Exception ex)
            {
                Assert.IsTrue(ex is System.Security.Cryptography.CryptographicException);
            }
        }
    }
}
