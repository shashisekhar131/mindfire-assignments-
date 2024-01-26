using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using CRUDOperation.LOG;

namespace ADO.NET_practice
{
    internal class Program
    {
        static void Main(string[] args)
        {


            new Program().Connecting();
            int selected;
            do
            {
                Console.WriteLine(" 1. ReadTable \n 2. insert the Data \n 3. delete the row \n 4. stop");

                selected = Convert.ToInt32(Console.ReadLine());

                switch (selected)
                {
                    case 1:
                        new Program().ReadTable();
                        break;
                    case 2:
                        new Program().InsertRow();
                        break;
                    case 3:
                        new Program().DeleteRow();
                        break;
                }

            } while (selected != 4);


        }

        public void Connecting()
        {
            try
            {
                string ConString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(ConString))
                {
                    connection.Open();
                    Console.WriteLine("Connection Established Successfully");
                    LoggerClass.AddData("connected to database");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong.\n" + e);
                LoggerClass.AddData(" error with connecting to the database");
            }
        }

        public void InsertRow()
        {

            // get last Id 

            try
            {
                string ConString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                int lastId = 0;

                using (SqlConnection connection = new SqlConnection(ConString))
                {
                    string selectMaxIdQuery = "SELECT MAX(ID) FROM StudentTable";

                    SqlCommand cmd = new SqlCommand(selectMaxIdQuery, connection);
                    connection.Open();
                    var result = cmd.ExecuteScalar();

                    // If result is DBNull, return 0; otherwise, return the result
                    lastId = result == DBNull.Value ? 0 : Convert.ToInt32(result);


                }


                int newId = lastId + 1;


                // insert the row 

                using (SqlConnection connection = new SqlConnection(ConString))
                {




                    Console.Write("Enter Name: ");
                    string name = Console.ReadLine();

                    Console.Write("Enter Email: ");
                    string email = Console.ReadLine();

                    Console.Write("Enter Mobile: ");
                    string mobile = Console.ReadLine();

                    string insertQuery = $"INSERT INTO StudentTable VALUES ({newId}, '{name}', '{email}', '{mobile}')";

                    SqlCommand cmd = new SqlCommand(insertQuery, connection);
                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0) Console.WriteLine("Inserted row successfully");

                    LoggerClass.AddData($"Inserted row with ID {newId}");

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong.\n" + e);
                LoggerClass.AddData(" error with Inserting row ");

            }

        }


        public void ReadTable()
        {
            try
            {
                string ConString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(ConString))
                {
                    SqlCommand cmd = new SqlCommand("select * from StudentTable", connection);
                    connection.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        Console.WriteLine("Name: " + sdr[0] + " Email:" + sdr[1] + " Moblie:" + sdr[2]);
                    }
                    LoggerClass.AddData("read some data");



                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong.\n" + e);
                LoggerClass.AddData(" error with Reading the table");
            }
        }

        public void DeleteRow()
        {
            try
            {
                string ConString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(ConString))
                {
                    // Display existing records before deletion
                    Console.WriteLine("look at the table and select Id of the row to be deleted");
                    new Program().ReadTable();


                    Console.Write("Enter the ID of the row to delete: ");
                    int idToDelete = Convert.ToInt32(Console.ReadLine());


                    string deleteQuery = "DELETE FROM StudentTable WHERE ID = @ID";


                    using (SqlCommand cmd = new SqlCommand(deleteQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@ID", idToDelete);

                        connection.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Row deleted successfully.");
                            LoggerClass.AddData($"Deleted row with ID {idToDelete}");
                        }
                        else
                        {

                            Console.WriteLine("No rows were deleted.");
                            LoggerClass.AddData(" error with deleting the row ");
                        }

                    }

                    // Display remaining records after deletion
                    new Program().ReadTable();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong.\n" + e);
            }
        }


    }
}
