using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace JabaTalks_Automation
{
    [TestFixture]
    public class JabaTalks_Positive_WorkFlow
    {
        IWebDriver driver;
        private const string websiteUrl = "http://jt-dev.azurewebsites.net/#/SignUp";

        [SetUp]
        public void Start_Browser()
        {
            //Step 1 : Launch new browser
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void ValidateSignUp()
        {
            try
            {
                //Step 2: Open Url
                driver.Navigate().GoToUrl(websiteUrl);

                //We can create object of each webelement in below code, but as they are not resused so for simplicity no object created
                driver.FindElement(By.Id("language")).Click();

                var dropdownLanguages = driver.FindElements(By.ClassName("ui-select-choices-row"));
                var languageDropdownList = new List<string>();
                foreach (var option in dropdownLanguages)
                {
                    if (!string.IsNullOrEmpty(option.Text))
                        languageDropdownList.Add(option.Text);
                }
                var languageList = new List<string> { "English", "Dutch" };

                //Step 3: Validate dropdown values 
                Assert.AreEqual(languageDropdownList, languageList);

                // Step 4: Populate Name
                var name = ConfigurationManager.AppSettings["name"];
                driver.FindElement(By.Id("name")).SendKeys(name);

                //Step 5: Populate Organization
                driver.FindElement(By.Id("orgName")).SendKeys(name + "_org");

                //Step 6: Input Email address
                var emailAddress = ConfigurationManager.AppSettings["signUpEmail"];
                driver.FindElement(By.Id("singUpEmail")).SendKeys(emailAddress);

                //Step 7 : Accept the terms and conditions checkbox
                driver.FindElement(By.ClassName("ui-checkbox")).FindElement(By.TagName("span")).Click();

                //Step 8: Click SignUp
                IWebElement Submit = driver.FindElement(By.XPath("//button[@type = 'submit']"));
                Submit.Click();

                //Step 9: Validate, email is received
            }
            catch (Exception ex)
            {
                //Can implement logging here
                throw ex;
            }
        }

        [TearDown]
        public void Close_Browser()
        {
            driver.Close();
            driver.Quit();
        }
    }

}
