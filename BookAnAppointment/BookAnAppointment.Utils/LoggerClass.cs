using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAnAppointment.Utils
{
    public class LoggerClass
    {
        public static void LogIntoFile(Exception ex)
        {

            try
            {
                string filePath = Path.Combine(ConfigurationManager.AppSettings["LogFilePath"], "logData.txt");

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    while (ex.InnerException != null)
                        ex = ex.InnerException;

                    writer.WriteLine(ex.ToString());
                }
            }
            catch (Exception ex2)
            {
                Console.WriteLine(ex2.ToString());
            }
            
        }

    }
}
