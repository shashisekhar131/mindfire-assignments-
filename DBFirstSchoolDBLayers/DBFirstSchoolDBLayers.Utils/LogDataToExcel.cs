using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBFirstSchoolDBLayers.Utils
{
    public class LogDataToExcel
    {


        public static void AddDataToExcel(string logData)
        {
            try
            {
                string excelFilePath = "C:\\Users\\belagallus\\Desktop\\logExcelFile.xlsx";
                string worksheetName = "Sheet1";

                // Set the license context to NonCommercial
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // Check if the file exists, if not, create it and add headers
                if (!File.Exists(excelFilePath))
                {
                    using (var package = new ExcelPackage(new FileInfo(excelFilePath)))
                    {
                        var worksheet = package.Workbook.Worksheets.Add(worksheetName);
                        worksheet.Cells["A1"].Value = logData;
                        worksheet.Cells["B1"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        package.Save();
                    }
                }

                // Open the existing Excel file
                using (var package = new ExcelPackage(new FileInfo(excelFilePath)))
                {
                    var worksheet = package.Workbook.Worksheets[worksheetName];

                    // If the worksheet doesn't exist, create it
                    if (worksheet == null)
                    {
                        worksheet = package.Workbook.Worksheets.Add(worksheetName);
                        worksheet.Cells["A1"].Value = logData;
                        worksheet.Cells["B1"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }

                    // Find the last used row in the worksheet
                    int row = worksheet.Dimension?.End.Row + 1 ?? 2;

                    // Add data to the Excel file
                    worksheet.Cells[row, 1].Value = logData;
                    worksheet.Cells[row, 2].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    // Save changes to the Excel file
                    package.Save();
                }

                Console.WriteLine("Data successfully added to Excel file.");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception:");
                    Console.WriteLine($"  Message: {ex.InnerException.Message}");
                    Console.WriteLine($"  Stack Trace: {ex.InnerException.StackTrace}");
                }

                Console.WriteLine($"Error while adding data to Excel file: {ex.Message}");
            }
        }
    }
}
