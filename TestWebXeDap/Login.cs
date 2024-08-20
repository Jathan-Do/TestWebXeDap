using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebXeDap
{
    internal class Login
    {
        private IWebDriver driver;

        public Login(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void PerformLogin(string username, string password)
        {
            //Perform login
            driver.Navigate().GoToUrl("http://localhost:4200/");
            driver.Manage().Window.Size = new System.Drawing.Size(1552, 880);
            Thread.Sleep(2000);
            driver.FindElement(By.LinkText("Đăng nhập")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.Name("email")).Click();
            driver.FindElement(By.Name("email")).SendKeys(username);
            Thread.Sleep(2000);
            driver.FindElement(By.Name("password")).Click();
            driver.FindElement(By.Name("password")).SendKeys(password);
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector(".btn")).Click();
            Thread.Sleep(2000);
        }
        public void PerformAdminLogin(string username, string password)
        {
            //Perform login
            driver.Navigate().GoToUrl("http://localhost:4200/");
            driver.Manage().Window.Size = new System.Drawing.Size(1552, 880);
            Thread.Sleep(2000);
            driver.FindElement(By.LinkText("Admin")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.Name("email")).Click();
            driver.FindElement(By.Name("email")).SendKeys(username);
            Thread.Sleep(2000);
            driver.FindElement(By.Name("password")).Click();
            driver.FindElement(By.Name("password")).SendKeys(password);
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector(".btn-register")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.LinkText("Đơn hàng")).Click();
            Thread.Sleep(2000);
        }
    }
}
