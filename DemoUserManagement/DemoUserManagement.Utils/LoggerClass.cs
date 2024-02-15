using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.Utils
{
    public class LoggerClass
    {
        public static void AddData(Exception logException)
        {
            string logFilePath = "C:\\Users\\belagallus\\source\\repos";

            try
            {
                if (!File.Exists(logFilePath))
                {
                    // If the file doesn't exist, create it
                    using (FileStream fs = File.Create(logFilePath))
                    {
                    }
                }

                // Append the log data to the file
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now}: {logException}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while writing to log file: {ex.Message}");
            }
        }

    }
}
