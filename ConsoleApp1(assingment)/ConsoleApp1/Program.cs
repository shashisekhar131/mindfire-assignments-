using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("enter some text that you want to write to file");

            // ask the user for the first time             
            File.WriteAllText("randomFile.txt",Console.ReadLine());
            Console.WriteLine("enter the option add / read / stop");

            // don't go away from console until user enters stop
            string userInput = Console.ReadLine().ToLower();

            while (userInput != "stop") {

                if(userInput == "add") {

                    try
                    {
                        // Append text to the existing file
                        using (StreamWriter sw = File.AppendText("randomFile.txt"))
                        {
                            Console.WriteLine("enter data that you want to add:");
                            sw.WriteLine(Console.ReadLine());
                        }

                        Console.WriteLine("Text appended successfully!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                    }
                
                }else if(userInput == "read")
                {

                    try
                    {
                        // Read all text from the file
                         string fileContent = File.ReadAllText("randomFile.txt");

                        // showing the content on console
                        Console.WriteLine("File Content:");
                        Console.WriteLine(fileContent);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                    }

                }

              Console.WriteLine("enter the option add / read / stop");
              userInput = Console.ReadLine().ToLower();
                
               

            }


        }
    }
}
