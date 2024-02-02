using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBFirstSchoolDBLayers.Utils
{
    public class LogDataToCSV
    {


        public static void AddDataToCsv(string logData)
        {
            try
            {
                string csvFilePath = "C:\\Users\\belagallus\\Desktop\\logcsvFile.csv";

                // Check if the file exists, if not, create it and add headers
                if (!File.Exists(csvFilePath))
                {
                    using (StreamWriter writer = new StreamWriter(csvFilePath))
                    {
                        writer.WriteLine("LogData,CreatedDate");
                    }
                }

                // Append log data to the CSV file
                using (StreamWriter writer = new StreamWriter(csvFilePath, true))
                {
                    writer.WriteLine($"{logData},{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
                }

                Console.WriteLine("Data successfully added to CSV file.");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception:");
                    Console.WriteLine($"  Message: {ex.InnerException.Message}");
                    Console.WriteLine($"  Stack Trace: {ex.InnerException.StackTrace}");
                }

                Console.WriteLine($"Error while adding data to CSV file: {ex.Message}");
            }
        }
    }
}
