using OfficeOpenXml.Style;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestWebXeDap
{
    internal class AdminManagement
    {
        private IWebDriver driver;

        public AdminManagement(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void PerformAdmin_OrderNumber(string number)
        {
            driver.FindElement(By.CssSelector(".form-control")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector($"body > app-root > app-seller-order > div.product-list.m-5 > div.row.mb-3 > div:nth-child(1) > select > option:nth-child({number})")).Click();
            Thread.Sleep(2000);

        }
        public void PerformAdmin_Search(string searchData)
        {
            driver.FindElement(By.Name("searchText")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.Name("searchText")).SendKeys(searchData);
            Thread.Sleep(2000);
        }
        public void PerformAdmin_WaitingDelivery(string position, string page)//position sẽ là vị trí đơn hàng có thể xác nhận giao hàng
        {
            PerformAdmin_NavigationByNum(page);
            Thread.Sleep(2000);
            IWebElement element = driver.FindElement(By.XPath($"(//button[normalize-space()='Giao hàng'])[{position}]"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(element).Click().Perform();
            Thread.Sleep(2000);
        }
        public void PerformAdmin_ConfirmDelivery(string position, string page)//position sẽ là vị trí đơn hàng có thể xác nhận đã giao hàng
        {
            PerformAdmin_NavigationByNum(page);
            Thread.Sleep(2000);
            IWebElement element = driver.FindElement(By.XPath($"(//button[normalize-space()='Đã giao hàng'])[{position}]"));
            Thread.Sleep(2000);
            Actions actions = new Actions(driver);
            actions.MoveToElement(element).Click().Perform();
            Thread.Sleep(2000);
        }
        public void PerformAdmin_ListProducts(string position)//position sẽ là vị trí đơn hàng cần xem ds sản phẩm
        {
            driver.FindElement(By.XPath($"(//button[@id='dropdownMenuButton'])[{position}]")).Click();
            Thread.Sleep(2000);
        }
        public void PerformAdmin_Navigation(string behavior, string position)//behavior sẽ là next/previous, position là số thứ tự thẻ li 
        {
            PerformAdmin_NavigationByNum(position);

            if (IsNavigateSuccessful(behavior))
            {
                //Submit checkout form
                IWebElement navigateButton = driver.FindElement(By.CssSelector($".pagination-{behavior}"));

                navigateButton.Click();
                Thread.Sleep(2000);

            }
            else
            {
                Console.WriteLine("Navigate button is not enabled. Test case failed!");
            }
        }
        public bool IsNavigateSuccessful(string behavior)
        {
            try
            {
                IWebElement navigateButtonDisabled = driver.FindElement(By.CssSelector($"#seller-order-list-pagination > pagination-template > nav > ul > li.pagination-{behavior}.disabled"));

                return false;
            }
            catch (NoSuchElementException)
            {
                return true;
            }
        }
        public void PerformAdmin_NavigationByNum(string position)//position là số thứ tự trang 
        {
            IWebElement element = driver.FindElement(By.CssSelector($"#seller-order-list-pagination > pagination-template > nav > ul > li:nth-child({position})"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(element).Click().Perform();
            Thread.Sleep(2000);
        }
    }
}



