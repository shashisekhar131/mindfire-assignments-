using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConversionMethods;

namespace ExtensionMethods
{

    public class Animal
    {
        public string Name { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {



            List<Animal> li = new List<Animal>() { new Animal { Name = "Lion" },
            new Animal { Name = "Dear" },
            new Animal { Name = "Wolf" },
            new Animal { Name = "Tiger" }};

            Console.Write("List: ");
           
            foreach (var item in li)
            {
                Console.Write(item.Name);
            }
           
            Console.WriteLine("\n \nconverted above list to data table");
            Console.WriteLine();
            Console.WriteLine("Data Table:");
            DataTable d =  li.ToDataTable();

            foreach (DataRow row in d.Rows)
            {
                foreach (DataColumn col in d.Columns)
                {
                    Console.Write($"{col.ColumnName}: {row[col]}   ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("converted above dataTable to list");
            Console.WriteLine();

            li =  d.ToList<Animal>();

            foreach (var item in li)
            {
                Console.Write(item.Name);
            }
           
            Console.ReadLine();

        }
    }
}
