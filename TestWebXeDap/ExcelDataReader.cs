using OfficeOpenXml;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebXeDap
{
    internal class ExcelDataReader
    {
        public List<List<string>> ReadTestDataFromExcel(string filePath, int startRow, int endRow)
        {
            List<List<string>> testDataList = new List<List<string>>();

            FileInfo fileInfo = new FileInfo(filePath);

            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1]; // Number of worksheet

                for (int row = startRow; row <= endRow; row++)
                {
                    List<string> rowData = new List<string>();
                    string testData = worksheet.Cells[row, 6].Value?.ToString(); // Cell[row,column]
                    if (!string.IsNullOrEmpty(testData))
                    {
                        string[] dataItems = testData.Split('/'); // Split cell value by comma

                        foreach (string item in dataItems)
                        {
                            rowData.Add(item.Trim()); // Add each data item to the row data list
                        }
                    }
                    testDataList.Add(rowData);
                }
            }

            return testDataList;
        }

        public void WriteTestResultToExcel(string filePath, int startRow, List<string> testResults)
        {
            FileInfo fileInfo = new FileInfo(filePath);

            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1]; // Number of worksheet

                int rowIndex = startRow;
                foreach (string result in testResults)
                {
                    worksheet.Cells[rowIndex, 9].Value = result;
                    rowIndex++;
                }

                package.Save();
            }
        }
    }
}
