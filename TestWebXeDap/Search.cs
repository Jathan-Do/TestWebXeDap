using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebXeDap
{
    internal class Search
    {
        private IWebDriver driver;

        public Search(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void PerformSearch(string searchData)
        {
            driver.Navigate().GoToUrl("http://localhost:4200/");
            driver.Manage().Window.Size = new System.Drawing.Size(1552, 880);
            driver.FindElement(By.CssSelector(".input-search")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector(".input-search")).SendKeys(searchData);
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector(".btn-search")).Click();
            Thread.Sleep(2000);
        }
    }
}
