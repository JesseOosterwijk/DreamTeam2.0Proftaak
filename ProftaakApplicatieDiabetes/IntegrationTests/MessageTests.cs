using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Reflection;
using OpenQA.Selenium.Support.UI;

namespace Tests
{
    public class Tests
    {
        private IWebDriver _driver;
        public string _homeURL;

        [SetUp]
        public void Setup()
        {
            _homeURL = "https://localhost:44316/";
            _driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }

        [Test]
        public void LoadPatientMessage()
        {
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(_homeURL);

            _driver.FindElement(By.Id("AcceptCookie")).Click();
            _driver.FindElement(By.Id("Login")).Click();

            //If test fails make sure user is registered
            _driver.FindElement(By.Id("EmailAddress")).SendKeys("Jasperkohlen@hotmail.com");
            _driver.FindElement(By.Id("Password")).SendKeys("123");
            _driver.FindElement(By.Id("LoginUser")).Click();

            _driver.FindElement(By.Id("SeeChat")).Click();

            Assert.AreEqual("Message - ProftaakApplicatieDiabetes", _driver.Title);
        }

        [Test]
        public void PatientSendMessage()
        {
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(_homeURL);

            _driver.FindElement(By.Id("AcceptCookie")).Click();
            _driver.FindElement(By.Id("Login")).Click();

            //If test fails make sure user is registered
            _driver.FindElement(By.Id("EmailAddress")).SendKeys("Jasperkohlen@hotmail.com");
            _driver.FindElement(By.Id("Password")).SendKeys("123");
            _driver.FindElement(By.Id("LoginUser")).Click();

            _driver.FindElement(By.Id("SeeChat")).Click();
            
            DateTime currentDateTime = DateTime.Now;
            _driver.FindElement(By.Id("Title")).SendKeys("title " + currentDateTime);
            _driver.FindElement(By.Id("Content")).SendKeys("content " + currentDateTime);

            _driver.FindElement(By.Id("SendMessage")).Click();

            Assert.True(_driver.PageSource.Contains("title " + currentDateTime));
            Assert.True(_driver.PageSource.Contains("content " + currentDateTime));
            Assert.AreEqual("Message - ProftaakApplicatieDiabetes", _driver.Title);
        }

        [TearDown]
        public void TearDownTest()
        {
            _driver.Close();
            _driver.Dispose();
        }
    }
}