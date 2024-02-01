using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBFirstStudentDataBase_School.Utils
{
    public class LoggerClass
    {


        public static void AddData(string logData)
        {


            // add logData to Database LOGGER 
            string ConnectionString = "data source=.; database=LOGGER; integrated security=SSPI";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {

                try
                {
                    connection.Open();


                    string query = "INSERT INTO loggerTable (logData,createdDate) VALUES (@LogData,@LogDate)";
                    SqlCommand cmd = new SqlCommand(query, connection);

                    cmd.Parameters.AddWithValue("@LogDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@LogData", logData);

                    int rowsAffected = cmd.ExecuteNonQuery();


                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while inserting into database: {ex.Message}");
                }

            }

            // add to logDataFile.txt
            string logFilePath = "C:\\Users\\belagallus\\Desktop\\mindfire-assignments-\\DBFirstDataBase_School\\DBFirstStudentDataBase_School.Utils\\LogDataFile.txt";

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
