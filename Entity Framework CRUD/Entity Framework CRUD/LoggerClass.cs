using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Framework_CRUD.LOG
{
    internal class LoggerClass
    {
        public static void AddData(string logData)
        {
            string logFilePath = "C:\\Users\\belagallus\\source\\repos\\Entity Framework CRUD\\Entity Framework CRUD\\LogDataFile.txt";

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
