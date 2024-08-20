using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebXeDap
{
    internal class CancelOrder
    {
        private IWebDriver driver;

        public CancelOrder(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void PerformCancelOrder(string position)//position sẽ là vị trí đơn hàng có thể hủy 
        {
            driver.FindElement(By.XPath("/html/body/app-root/app-header/header/div[2]/div/div/a")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.LinkText("Hồ sơ")).Click();
            Thread.Sleep(2000);
            IWebElement element = driver.FindElement(By.XPath($"(//button[@class='cancel-order btn btn-outline-danger'][contains(text(),'Hủy đơn hàng')])[{position}]"));
            Thread.Sleep(2000);
            Actions actions = new Actions(driver);
            actions.MoveToElement(element).Perform();
            element.Click();
            Thread.Sleep(2000);

            // Lấy đối tượng alert
            IAlert firstAlert = driver.SwitchTo().Alert();
            Assert.That(firstAlert.Text, Is.EqualTo("Bạn có chắc chắn hủy đơn hàng này?\r\nBạn sẽ mất số điểm tích lũy của đơn!"));
            Thread.Sleep(2000);
            // Nhấn OK để đóng cửa sổ Alert
            firstAlert.Accept(); // hoặc alert.Dismiss() nếu hủy bỏ cửa sổ Alert

            Thread.Sleep(2000);
            IAlert secondAlert = driver.SwitchTo().Alert();
            Assert.That(secondAlert.Text, Is.EqualTo("Hủy đơn thành công!"));
            secondAlert.Accept();
            Thread.Sleep(2000);
        }
    }
}
