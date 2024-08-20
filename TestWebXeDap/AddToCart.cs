using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebXeDap
{
    internal class AddToCart
    {
        private IWebDriver driver;

        public AddToCart(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void PerformAddToCart()
        {
            //add to card
            IWebElement element = driver.FindElement(By.XPath("/html/body/app-root/app-home/div[2]/app-product/div/div/app-product-list/div/div[2]/div/div[1]/div/a/img"));
            Thread.Sleep(2000);
            Actions actions = new Actions(driver);
            actions.MoveToElement(element).Perform();
            element.Click();
            Thread.Sleep(2000);

            //kiem tra sp da them vao gio hang chua
            if (driver.FindElements(By.CssSelector(".btn-remove")).Count > 0)
            {
                //If the product has been added, navigate to the cart
                driver.FindElement(By.XPath("/html/body/app-root/app-header/header/div[2]/div/a")).Click();
                Thread.Sleep(2000);
            }
            else if (driver.FindElements(By.CssSelector(".btn-add")).Count > 0)
            {
                //If the product has not been added, click the add button
                driver.FindElement(By.CssSelector(".btn-add")).Click();
                Thread.Sleep(2000);
                //After clicking the add button, navigate to the cart
                driver.FindElement(By.XPath("/html/body/app-root/app-header/header/div[2]/div/a")).Click();
                Thread.Sleep(2000);

            }
        }
    }
}
