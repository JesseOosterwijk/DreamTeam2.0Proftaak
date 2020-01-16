using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Reflection;
using OpenQA.Selenium.Support.UI;

namespace IntegrationTests
{
    class CalculationTests
    {
        // When running tests: Make sure the cisco vpn is logged in and the application is running in nondebug-mode

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
        }

        private void LoginAsPatient()
        {
            //If test fails make sure user is registered
            _driver.FindElement(By.Id("EmailAddress")).SendKeys("DemoPatient@gmail.com");
            _driver.FindElement(By.Id("Password")).SendKeys("123");
            _driver.FindElement(By.Id("LoginUser")).Click();
        }

        [Test]
        public void PatientCalculatesCurrentbloodsuger60()
        {
            LoadHome();
            AcceptCooky();
            _driver.FindElement(By.Id("Login")).Click();

            LoginAsPatient();
            _driver.Navigate().GoToUrl("https://localhost:44316/Calc/Calculate");
            _driver.FindElement(By.Name("TotalCarbs")).SendKeys("60");
            _driver.FindElement(By.Name("CurrentBloodsugar")).SendKeys("130");
            _driver.FindElement(By.Name("TargetBloodSugar")).SendKeys("100");
            _driver.FindElement(By.Id("Calculate")).Click();

            Assert.IsTrue(_driver.PageSource.Contains("6"));
        }

        [Test]
        public void PatientCalculatesCurrentbloodsuger100()
        {
            LoadHome();
            AcceptCooky();
            _driver.FindElement(By.Id("Login")).Click();

            LoginAsPatient();
            _driver.Navigate().GoToUrl("https://localhost:44316/Calc/Calculate");
            _driver.FindElement(By.Name("TotalCarbs")).SendKeys("100");
            _driver.FindElement(By.Name("CurrentBloodsugar")).SendKeys("130");
            _driver.FindElement(By.Name("TargetBloodSugar")).SendKeys("100");
            _driver.FindElement(By.Id("Calculate")).Click();

            Assert.IsTrue(_driver.PageSource.Contains("8"));
        }

        [TearDown]
        public void TearDownTest()
        {
            _driver.Close();
            _driver.Dispose();
        }
    }
}
