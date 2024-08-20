using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebXeDap
{
    internal class Logout
    {
        private IWebDriver driver;

        public Logout(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void PerformLogout()
        {
            driver.FindElement(By.XPath("/html/body/app-root/app-header/header/div[2]/div/div/a")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.LinkText("Đăng xuất")).Click();
            Thread.Sleep(2000);
        }
        public void PerformAdminLogout()
        {
            driver.FindElement(By.CssSelector(".nav-link")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.LinkText("Đăng xuất")).Click();
            Thread.Sleep(2000);
        }
    }
}
