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

        private void LoadHome()
        {
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(_homeURL);
        }

        private void AcceptCooky()
        {
            _driver.FindElement(By.Id("AcceptCookie")).Click();
            _driver.FindElement(By.Id("Login")).Click();
        }

        private void LoginAsPatient()
        {
            //If test fails make sure user is registered
            _driver.FindElement(By.Id("EmailAddress")).SendKeys("Jasperkohlen@hotmail.com");
            _driver.FindElement(By.Id("Password")).SendKeys("123");
            _driver.FindElement(By.Id("LoginUser")).Click();
        }
        private void LoginAsDoctor()
        {
            //If test fails make sure user is registered
            _driver.FindElement(By.Id("EmailAddress")).SendKeys("JasperVerkoper@hotmail.com");
            _driver.FindElement(By.Id("Password")).SendKeys("123");
            _driver.FindElement(By.Id("LoginUser")).Click();
        }


        private void SendMessage(DateTime currentDateTime)
        {
            _driver.FindElement(By.Id("Title")).SendKeys("title " + currentDateTime);
            _driver.FindElement(By.Id("Content")).SendKeys("content " + currentDateTime);
            _driver.FindElement(By.Id("SendMessage")).Click();
        }

        [Test]
        public void LoadPatientMessage()
        {
            LoadHome();

            AcceptCooky();

            LoginAsPatient();

            _driver.FindElement(By.Id("SeeChat")).Click();

            Assert.AreEqual("Message - ProftaakApplicatieDiabetes", _driver.Title);
        }

        [Test]
        public void PatientSendMessage()
        {
            LoadHome();
            AcceptCooky();
            LoginAsPatient();

            _driver.FindElement(By.Id("SeeChat")).Click();
            
            DateTime currentDateTime = DateTime.Now;

            SendMessage(currentDateTime);

            Assert.True(_driver.PageSource.Contains("title " + currentDateTime));
            Assert.True(_driver.PageSource.Contains("content " + currentDateTime));
            Assert.AreEqual("Message - ProftaakApplicatieDiabetes", _driver.Title);
        }

        [Test]
        public void LoadDoctorMessage()
        {
            LoadHome();
            AcceptCooky();
            LoginAsDoctor();
            //SeePatientOverview
        }

        [TearDown]
        public void TearDownTest()
        {
            _driver.Close();
            _driver.Dispose();
        }
    }
}