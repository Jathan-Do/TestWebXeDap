using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebXeDap
{
    internal class CheckOut
    {
        private IWebDriver driver;

        public CheckOut(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void PerformCheckOut(string email, string address, string contact, string paymentMethod)
        {
            driver.FindElement(By.Name("email")).Click();
            driver.FindElement(By.Name("email")).SendKeys(email);
            Thread.Sleep(2000);

            driver.FindElement(By.Name("address")).Click();
            driver.FindElement(By.Name("address")).SendKeys(address);
            Thread.Sleep(2000);

            driver.FindElement(By.Name("contact")).Click();
            driver.FindElement(By.Name("contact")).SendKeys(contact);
            Thread.Sleep(2000);

            //Select payment method
            var paymentDropdown = driver.FindElement(By.Name("payment"));
            var paymentOption = paymentDropdown.FindElement(By.XPath("//option[text()='" + paymentMethod + "']"));
            Thread.Sleep(2000);
            paymentOption.Click();
            Thread.Sleep(2000);


            IWebElement checkoutButton = driver.FindElement(By.CssSelector(".btn"));
            // Thread.Sleep(2000);

            //if (checkoutButton.Enabled)
            if (IsCheckoutSuccessful())
            {
                //Submit checkout form
                checkoutButton.Click();
                Thread.Sleep(2000);

            }
            else
            {
                Console.WriteLine("Checkout button is not enabled. Test case failed!");
            }
        }
        public bool IsCheckoutSuccessful()
        {
            try
            {
                IWebElement checkoutButton = driver.FindElement(By.CssSelector(".btn"));
                return checkoutButton.Enabled;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
