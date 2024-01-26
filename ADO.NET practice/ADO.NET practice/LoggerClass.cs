using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDOperation.LOG
{
    internal class LoggerClass
    {

        public static void AddData(string logData)
        {
            string logFilePath = "C:\\Users\\belagallus\\source\\repos\\ADO.NET practice\\ADO.NET practice\\LogDataFile.txt";

            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now}: {logData}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while writing to log file: {ex.Message}");
            }
        }
    }
}
