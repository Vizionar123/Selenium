﻿using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using EC = SeleniumExtras.WaitHelpers.ExpectedConditions;
namespace Cas28
{
    class SeleniumTests
    {
        IWebDriver driver;
        WebDriverWait wait;

        //private string TestData_Email = "korisnik@1234567890.commmmmm";
        //private string TestData_Password = "NekaJakaSifra";
        private string TestData_Email = UsefulFunctions.RandomEmail();
        private string TestData_Password = UsefulFunctions.RandomPassword();

        //[Test]
        public void TestHomePage()
        {
            IWebElement logo = driver.FindElement(By.ClassName("logo"));
            if (logo.Displayed)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void TestSignIn()
        {
            ClickOnSignInLink();

            IWebElement button = driver.FindElement(By.Name("SubmitLogin"));
            if (button.Displayed && button.Enabled)
            {
                IWebElement email = driver.FindElement(By.Name("email"));
                IWebElement password = driver.FindElement(By.Name("passwd"));

                email.SendKeys(TestData_Email);
                password.SendKeys(TestData_Password);

                button.Click();
                Assert.Pass();
            }
        }

       [Test]
        public void TestRegistration()
        {
            ClickOnSignInLink();

            IWebElement createButton = driver.FindElement(By.Name("SubmitCreate"));
            if (createButton.Displayed && createButton.Enabled)
            {
                IWebElement createEmail = driver.FindElement(By.Name("email_create"));
                if (createEmail.Displayed && createEmail.Enabled)
                {
                    createEmail.SendKeys(TestData_Email);
                }
                createButton.Click();
            }

            //System.Threading.Thread.Sleep(2000);
            IWebElement form_firstName = wait.Until(EC.ElementIsVisible(By.Name("customer_firstname")));
            //IWebElement form_firstName = driver.FindElement(By.Name("customer_firstname"));
            if (form_firstName.Displayed && form_firstName.Enabled)
            {
                form_firstName.SendKeys("Probnoime");
                IWebElement form_lastName = driver.FindElement(By.Name("customer_lastname"));
                if (form_lastName.Enabled) form_lastName.SendKeys("Probnoprezime");

                IWebElement form_password = driver.FindElement(By.Name("passwd"));
                if (form_password.Enabled) form_password.SendKeys(TestData_Password);

                IWebElement form_addrFName = driver.FindElement(By.Name("firstname"));
                if (form_addrFName.Enabled)
                {
                    form_addrFName.Clear();
                    form_addrFName.SendKeys("Probnoime");
                }

                IWebElement form_addrLName = driver.FindElement(By.Name("lastname"));
                if (form_addrLName.Enabled)
                {
                    form_addrLName.Clear();
                    form_addrLName.SendKeys("Probnoprezime");
                }

                IWebElement form_address1 = driver.FindElement(By.Name("address1"));
                if (form_address1.Enabled) form_address1.SendKeys("Adresa Neka 23");

                IWebElement form_city = driver.FindElement(By.Name("city"));
                if (form_city.Enabled) form_city.SendKeys("Grad");

                IWebElement form_state = driver.FindElement(By.Name("id_state"));

                wait.Until(EC.ElementExists(By.XPath("//option[@value='50']")));

                if (form_state.Enabled)
                {
                    SelectElement state = new SelectElement(form_state);
                    //state.SelectByText("Hawaii");
                    state.SelectByValue("11");
                }

                IWebElement form_zip = driver.FindElement(By.Name("postcode"));
                if (form_zip.Enabled) form_zip.SendKeys("12345");

                IWebElement form_mobile = driver.FindElement(By.Name("phone_mobile"));
                if (form_mobile.Enabled) form_mobile.SendKeys("123456");

                IWebElement form_alias = driver.FindElement(By.Name("alias"));
                if (form_alias.Enabled)
                {
                    form_alias.Clear();
                    form_alias.SendKeys("Moja adresa");
                }

                IWebElement registerButton = driver.FindElement(By.Name("submitAccount"));
                if (registerButton.Enabled) registerButton.Click(); ;
            }
        }

        public void ClickOnSignInLink()

        {
            IWebElement signin = wait.Until(EC.ElementIsVisible(By.LinkText("Sign in")));
            if (signin.Displayed && signin.Enabled)
            {
                signin.Click();
            }
        }
        [Test]
        public void Kupi_kosulju()
        {
            IWebElement buttonfind = driver.FindElement(By.Name("submit_search"));
            if (buttonfind.Enabled && buttonfind.Displayed)
            {
                IWebElement Find = driver.FindElement(By.Name("search_query"));
                if (Find.Enabled && Find.Displayed)
                {
                    Find.SendKeys("faded");


                }
                buttonfind.Click();
            }

            IWebElement picture = driver.FindElement(By.XPath("//img[@title='Faded Short Sleeve T-shirts']"));
            if (picture.Enabled && picture.Displayed)
            { picture.Click(); }
            IWebElement Chart = driver.FindElement(By.XPath("//span[text()='Add to cart']"));
            if (Chart.Enabled)
            {
                IWebElement quantity = driver.FindElement(By.Name("qty"));
                if (quantity.Enabled && quantity.Displayed)
                {
                    quantity.Clear();
                    quantity.SendKeys("2");
                }

                IWebElement Size = driver.FindElement(By.XPath("//select[@name='group_1']"));
                if (Size.Enabled)
                {
                    SelectElement S = new SelectElement(Size);
                    S.SelectByValue("3");
                    //System.Threading.Thread.Sleep(5000);

                }

                IWebElement Color = driver.FindElement(By.XPath("//ul[@id='color_to_pick_list']"));
                if (Color.Enabled)
                {
                    IWebElement choice = Color.FindElement(By.Name("Blue"));
                    choice.Click();
                    
                }
                Chart.Click();
                
                
            }
            IWebElement Verify = wait.Until(EC.ElementIsVisible(By.XPath("//span[contains(@class,'cross')]//following-sibling::h2")));
            if (Verify.Displayed)
            {
                IWebElement Contin = wait.Until(EC.ElementIsVisible(By.XPath("//div//span[@title='Continue shopping']")));
                if (Contin.Displayed && Contin.Enabled)
                {
                    Contin.Click();
                    System.Threading.Thread.Sleep(5000);
                }
            }


        }
        [SetUp]
        public void SetUp()
        {
            driver = new FirefoxDriver();
            wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://automationpractice.com/index.php");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }
    }
}

    

