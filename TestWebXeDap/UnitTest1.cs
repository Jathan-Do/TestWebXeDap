using OfficeOpenXml.Style;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace TestWebXeDap
{
    [TestFixture]
    public class Tests
    {
        private IWebDriver driver;

        private Search search;
        private Login login;
        private AddToCart addToCart;
        private CheckOut checkOut;
        private Logout logout;
        private CancelOrder cancelOrder;
        private AdminManagement adminManagement;

        [OneTimeSetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            search = new Search(driver);
            login = new Login(driver);
            addToCart = new AddToCart(driver);
            checkOut = new CheckOut(driver);
            logout = new Logout(driver);
            cancelOrder = new CancelOrder(driver);
            adminManagement = new AdminManagement(driver);
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }

        [Test]
        public void TestSearch()
        {
            string excelFilePath = @"C:\Users\USER\Downloads\DBCLPM_XeDap.xlsx";
            ExcelDataReader excelDataReader = new ExcelDataReader();
            List<List<string>> testDataList = excelDataReader.ReadTestDataFromExcel(excelFilePath, 12, 17);
            List<string> testResults = new List<string>();
            foreach (List<string> rowData in testDataList)
            {
                if (rowData != null && rowData.Count >= 1) // Row has at least 1 data item
                {
                    string searchData = rowData[0];
                    //Perform search
                    search.PerformSearch( searchData );
                    if (string.IsNullOrEmpty(searchData))
                    {
                        testResults.Add("Không chạy chức năng tìm kiếm");
                    }
                    else
                    {
                        //tìm giao diện search
                        IWebElement searchElement = driver.FindElement(By.XPath("/html/body/app-root/app-search"));
                        string displayedContent = searchElement.Text;

                        bool isMatch = false; // Khởi tạo biến isMatch
                        foreach (char c in searchData)
                        {
                            if (displayedContent.IndexOf(c, StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                // Nếu có ít nhất một ký tự trong từ khóa tìm kiếm xuất hiện trong nội dung hiển thị, đặt isMatch là true và thoát khỏi vòng lặp
                                isMatch = true;
                                break;
                            }
                        }
                        string testResult = isMatch ? searchData : "Không tìm thấy kết quả";
                        testResults.Add(testResult);
                    }
                }
                else
                {
                    Console.WriteLine("Row data is null or does not have enough data items !!!");
                }
            }
            // Write the test results to Excel
            excelDataReader.WriteTestResultToExcel(excelFilePath, 12, testResults);
        }
        [Test]
        public void TestCheckOut()
        {
            string excelFilePath = @"C:\Users\USER\Downloads\DBCLPM_XeDap.xlsx";
            ExcelDataReader excelDataReader = new ExcelDataReader();
            List<List<string>> testDataList = excelDataReader.ReadTestDataFromExcel(excelFilePath, 31, 32);
            List<string> testResults = new List<string>();
            foreach (List<string> rowData in testDataList)
            {
                if (rowData != null && rowData.Count >= 6) // Row has at least 6 data items
                {
                    string tk = rowData[0];
                    string pass = rowData[1];
                    string email = rowData[2];
                    string address = rowData[3];
                    string contact = rowData[4];
                    string paymentMethod = rowData[5];

                    //Perform login
                    login.PerformLogin(tk, pass);

                    //Perform add to cart
                    addToCart.PerformAddToCart();

                    //Navigate to checkout
                    driver.FindElement(By.CssSelector(".btn")).Click();

                    //Fill checkout form
                    checkOut.PerformCheckOut(email, address, contact, paymentMethod);
                    string result;
                    //Check if checkout was successful
                    bool isMatch = checkOut.IsCheckoutSuccessful();
                    if (isMatch)
                    {
                        result = "Thêm thành công";
                    }
                    else
                    {

                        result = "Thêm không thành công";
                    }
                    testResults.Add(result);
                }
                else
                {
                    Console.WriteLine("Row data is null or does not have enough data items !!!");
                }
                logout.PerformLogout();

            }
            // Write the test results to Excel
            excelDataReader.WriteTestResultToExcel(excelFilePath, 31, testResults);
        }
        [Test]
        public void TestSelectPayment()
        {
            string excelFilePath = @"C:\Users\USER\Downloads\DBCLPM_XeDap.xlsx";
            ExcelDataReader excelDataReader = new ExcelDataReader();
            List<List<string>> testDataList = excelDataReader.ReadTestDataFromExcel(excelFilePath, 33, 33);
            List<string> testResults = new List<string>();
            foreach (List<string> rowData in testDataList)
            {
                if (rowData != null && rowData.Count >= 6) // Row has at least 1 data item
                {
                    string tk = rowData[0];
                    string pass = rowData[1];
                    string email = rowData[2];
                    string address = rowData[3];
                    string contact = rowData[4];
                    string paymentMethod = rowData[5];

                    //Perform login
                    login.PerformLogin(tk, pass);

                    //Perform add to cart
                    addToCart.PerformAddToCart();

                    //Navigate to checkout
                    driver.FindElement(By.CssSelector(".btn")).Click();

                    //Fill checkout form
                    checkOut.PerformCheckOut(email, address, contact, paymentMethod);

                    testResults.Add(paymentMethod);
                }
                else
                {
                    Console.WriteLine("Row data is null or does not have enough data items !!!");
                }
            }
            // Write the test results to Excel
            excelDataReader.WriteTestResultToExcel(excelFilePath, 33, testResults);
        }
        [Test]
        public void TestCancelOrder()
        {
            string excelFilePath = @"C:\Users\USER\Downloads\DBCLPM_XeDap.xlsx";
            ExcelDataReader excelDataReader = new ExcelDataReader();
            List<List<string>> testDataList = excelDataReader.ReadTestDataFromExcel(excelFilePath, 34, 34);
            List<string> testResults = new List<string>();
            foreach (List<string> rowData in testDataList)
            {
                if (rowData != null && rowData.Count >= 3) // Row has at least 1 data item
                {
                    string tk = rowData[0];
                    string pass = rowData[1];
                    string pos = rowData[2];

                    //Perform login
                    login.PerformLogin(tk, pass);
                    try
                    {
                        //Perform cancel order
                        cancelOrder.PerformCancelOrder(pos);
                        // Thêm kết quả vào danh sách
                        testResults.Add("Hủy thành công");
                    }
                    catch (NoSuchElementException)
                    {
                        testResults.Add("Hủy không thành công");
                    }
                }
                else
                {
                    Console.WriteLine("Row data is null or does not have enough data items !!!");
                }  
            }
            // Write the test results to Excel
            excelDataReader.WriteTestResultToExcel(excelFilePath, 34, testResults);
        }
        [Test]
        public void TestAdmin_OrderNumber()
        {
            string excelFilePath = @"C:\Users\USER\Downloads\DBCLPM_XeDap.xlsx";
            ExcelDataReader excelDataReader = new ExcelDataReader();
            List<List<string>> testDataList = excelDataReader.ReadTestDataFromExcel(excelFilePath, 36, 36);
            List<string> testResults = new List<string>();
            foreach (List<string> rowData in testDataList)
            {
                if (rowData != null && rowData.Count >= 3) // Row has at least 1 data item
                {
                    string tk = rowData[0];
                    string pass = rowData[1];
                    string num = rowData[2];

                    //Perform login
                    login.PerformAdminLogin(tk, pass);
                    //Perform order number
                    adminManagement.PerformAdmin_OrderNumber(num);
                    // Tìm tất cả các phần tử thẻ card trên trang
                    int cardCount = driver.FindElements(By.CssSelector(".card")).Count;

                    // Thêm số lượng thẻ card vào kết quả kiểm thử
                    testResults.Add(cardCount.ToString());

                }
                else
                {
                    Console.WriteLine("Row data is null or does not have enough data items !!!");
                }
            }
            // Write the test results to Excel
            excelDataReader.WriteTestResultToExcel(excelFilePath, 36, testResults);
        }
        [Test]
        public void TestAdmin_Search()
        {
            string excelFilePath = @"C:\Users\USER\Downloads\DBCLPM_XeDap.xlsx";
            ExcelDataReader excelDataReader = new ExcelDataReader();
            List<List<string>> testDataList = excelDataReader.ReadTestDataFromExcel(excelFilePath, 37, 44);
            List<string> testResults = new List<string>();
            foreach (List<string> rowData in testDataList)
            {
                if (rowData != null && rowData.Count >= 3) // Row has at least 1 data item
                {
                    string tk = rowData[0];
                    string pass = rowData[1];
                    string searchData = rowData[2];
                    string testResult;
                    login.PerformAdminLogin(tk, pass);

                    if (string.IsNullOrEmpty(searchData))
                    {
                        testResult = "Không chạy chức năng tìm kiếm";
                    }
                    else
                    {
                        // Perform search
                        adminManagement.PerformAdmin_Search(searchData);
                        Thread.Sleep(2000);

                        // Đếm số lượng phần tử "Không tìm thấy kết quả"
                        int ResultsCount = driver.FindElements(By.XPath("(//div[@class='card mb-3'])")).Count;

                        if (ResultsCount > 0)
                        {
                            testResult = searchData;
                        }
                        else
                        {
                            testResult = "Không tìm thấy kết quả";
                        }
                    }
                    testResults.Add(testResult);
                }
                else
                {
                    Console.WriteLine("Row data is null or does not have enough data items !!!");
                }
                logout.PerformAdminLogout();
            }
            // Write the test results to Excel
            excelDataReader.WriteTestResultToExcel(excelFilePath, 37, testResults);
        }
        [Test]
        public void TestAdmin_WaitingDelivery()
        {
            string excelFilePath = @"C:\Users\USER\Downloads\DBCLPM_XeDap.xlsx";
            ExcelDataReader excelDataReader = new ExcelDataReader();
            List<List<string>> testDataList = excelDataReader.ReadTestDataFromExcel(excelFilePath, 45, 45);
            List<string> testResults = new List<string>();
            foreach (List<string> rowData in testDataList)
            {
                if (rowData != null && rowData.Count >= 3) // Row has at least 1 data item
                {
                    string tk = rowData[0];
                    string pass = rowData[1];
                    string pos = rowData[2];
                    string page = rowData[3];

                    //Perform login
                    login.PerformAdminLogin(tk, pass);
                    try
                    {
                        //Perform waiting for delivery
                        adminManagement.PerformAdmin_WaitingDelivery(pos, page);
                        // Thêm kết quả vào danh sách
                        testResults.Add("Xác nhận giao hàng thành công");
                    }
                    catch (NoSuchElementException)
                    {
                        testResults.Add("Xác nhận giao hàng thất bại");
                    }
                }
                else
                {
                    Console.WriteLine("Row data is null or does not have enough data items !!!");
                }
            }
            // Write the test results to Excel
            excelDataReader.WriteTestResultToExcel(excelFilePath, 45, testResults);
        }
        [Test]
        public void TestAdmin_ConfirmDelivery()
        {
            string excelFilePath = @"C:\Users\USER\Downloads\DBCLPM_XeDap.xlsx";
            ExcelDataReader excelDataReader = new ExcelDataReader();
            List<List<string>> testDataList = excelDataReader.ReadTestDataFromExcel(excelFilePath, 46, 46);
            List<string> testResults = new List<string>();
            foreach (List<string> rowData in testDataList)
            {
                if (rowData != null && rowData.Count >= 3) // Row has at least 1 data item
                {
                    string tk = rowData[0];
                    string pass = rowData[1];
                    string pos = rowData[2];
                    string page = rowData[3];

                    //Perform login
                    login.PerformAdminLogin(tk, pass);
                    try
                    {
                        //Perform confirm for delivery
                        adminManagement.PerformAdmin_ConfirmDelivery(pos, page);
                        // Thêm kết quả vào danh sách
                        testResults.Add("Giao hàng thành công");
                    }
                    catch (NoSuchElementException)
                    {
                        testResults.Add("Giao hàng thất bại");
                    }

                }
                else
                {
                    Console.WriteLine("Row data is null or does not have enough data items !!!");
                }  
            }
            // Write the test results to Excel
            excelDataReader.WriteTestResultToExcel(excelFilePath, 46, testResults);
        }
        [Test]
        public void TestAdmin_ListProducts()
        {
            string excelFilePath = @"C:\Users\USER\Downloads\DBCLPM_XeDap.xlsx";
            ExcelDataReader excelDataReader = new ExcelDataReader();
            List<List<string>> testDataList = excelDataReader.ReadTestDataFromExcel(excelFilePath, 47, 47);
            List<string> testResults = new List<string>();
            foreach (List<string> rowData in testDataList)
            {
                if (rowData != null && rowData.Count >= 4) // Row has at least 3 data item
                {
                    string tk = rowData[0];
                    string pass = rowData[1];
                    string pos = rowData[2];
                    string nameProd = rowData[3];

                    //Perform login
                    login.PerformAdminLogin(tk, pass);
                    //Perform list of products
                    adminManagement.PerformAdmin_ListProducts(pos);
                    // Find the card elements
                    IWebElement nameProdElement = driver.FindElement(By.XPath($"(//h6[contains(text(),'{nameProd}')])"));

                    string displayedContent = nameProdElement.Text;
                    bool isMatch = false;
                    if (displayedContent.IndexOf(nameProd, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        isMatch = true;
                    }
                    // Nếu kết quả tìm kiếm chứa từ khóa tìm kiếm, ghi kết quả vào cột "kết quả thực tế"
                    string testResult = isMatch ? nameProd : "Không tìm thấy sản phẩm";
                    testResults.Add(testResult);
                }
                else
                {
                    Console.WriteLine("Row data is null or does not have enough data items !!!");
                }
            }
            // Write the test results to Excel
            excelDataReader.WriteTestResultToExcel(excelFilePath, 47, testResults);
        }
        [Test]
        public void TestAdmin_Navigation()
        {
            string excelFilePath = @"C:\Users\USER\Downloads\DBCLPM_XeDap.xlsx";
            ExcelDataReader excelDataReader = new ExcelDataReader();
            List<List<string>> testDataList = excelDataReader.ReadTestDataFromExcel(excelFilePath, 48, 48);
            List<string> testResults = new List<string>();
            foreach (List<string> rowData in testDataList)
            {
                if (rowData != null && rowData.Count >= 4) // Row has at least 4 data items
                {
                    string tk = rowData[0];
                    string pass = rowData[1];
                    string behavior = rowData[2];
                    string position = rowData[3];
                   
                    //Perform login
                    login.PerformAdminLogin(tk, pass);
                    bool isMatch = adminManagement.IsNavigateSuccessful(behavior);
                    adminManagement.PerformAdmin_Navigation(behavior, position);


                    //Check if checkout was successful
                    string result;
                    if (isMatch)
                    {
                        result = "Chuyển trang thành công";
                    }
                    else
                    {

                        result = "Chuyển trang không thành công";
                    }
                    testResults.Add(result);
                }
                else
                {
                    Console.WriteLine("Row data is null or does not have enough data items !!!");
                }
            }
            // Write the test results to Excel
            excelDataReader.WriteTestResultToExcel(excelFilePath, 48, testResults);
        }
        [Test]
        public void TestAdmin_NavigationByNum()
        {
            string excelFilePath = @"C:\Users\USER\Downloads\DBCLPM_XeDap.xlsx";
            ExcelDataReader excelDataReader = new ExcelDataReader();
            List<List<string>> testDataList = excelDataReader.ReadTestDataFromExcel(excelFilePath, 49, 49);
            List<string> testResults = new List<string>();
            foreach (List<string> rowData in testDataList)
            {
                if (rowData != null && rowData.Count >= 3) // Row has at least 3 data items
                {
                    string tk = rowData[0];
                    string pass = rowData[1];
                    string position = rowData[2];

                    //Perform login
                    login.PerformAdminLogin(tk, pass);

                    //Perform navigate
                    try
                    {
                        adminManagement.PerformAdmin_NavigationByNum(position);
                        testResults.Add("Chuyển trang thành công");
                    }
                    catch (NoSuchElementException)
                    {
                        testResults.Add("Chuyển trang không thành công");
                    }
                   
                }
                else
                {
                    Console.WriteLine("Row data is null or does not have enough data items !!!");
                }
            }
            // Write the test results to Excel
            excelDataReader.WriteTestResultToExcel(excelFilePath, 49, testResults);
        }
    }
}