using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity_Framework_CRUD.LOG;

namespace Entity_Framework_CRUD
{
    internal class Program
    {
        static void Main(string[] args)
        {
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


        public void InsertRow()
        {
             
           
            using (var context = new SchoolDBEntities()) 
            {
                
                // get max id 
                int maxId = context.Students.Max(student => (int?)student.Id) ?? 0;

                // ask details
                Console.WriteLine("Enter the new student's Name:");
                string newName = Console.ReadLine();

                Console.WriteLine("Enter the new student's Mobile:");
                string newMobile = Console.ReadLine();

                Console.WriteLine("Enter the new student's Email:");
                string newEmail = Console.ReadLine();

                // create Student object
                var newStudent = new Student
                {
                    Name = newName,
                    Mobile = newMobile,
                    Email = newEmail,
                    Id = maxId +1 
                };

                // pass it to context API 

                context.Students.Add(newStudent);
                context.SaveChanges();
                Console.WriteLine("Student created successfully!");
                LoggerClass.AddData("inserted into table");
            }
        }

        public void ReadTable()
        {
            using (var context = new SchoolDBEntities()) 
            {
                Console.WriteLine("List of all students: \n loading please wait...");

                var students = context.Students.ToList();

                foreach (var student in students)
                {
                    Console.WriteLine($"ID: {student.Id}, Name: {student.Name}, Mobile: {student.Mobile}, Email: {student.Email}");
                }

                LoggerClass.AddData("read the table");
            }
        }

        public void DeleteRow()
        {
            new Program().ReadTable();
            Console.WriteLine("from the table select the id");
          

            using (var context = new SchoolDBEntities()) 
            {   
                Console.WriteLine("Enter the student ID to delete:");
                
                    var studentToDelete = context.Students.Find(Convert.ToInt32(Console.ReadLine()));

                    if (studentToDelete != null)
                    {
                        context.Students.Remove(studentToDelete);
                        context.SaveChanges();
                        Console.WriteLine("Student deleted successfully!");
                        LoggerClass.AddData("deleted the row");
                }
                    else
                    {
                        Console.WriteLine("No student found with ID:");
                       LoggerClass.AddData("tried deleting id that is not present in table");
                }
               
            }

        }

        public void UpdateRow()
        {
            using (var context = new SchoolDBEntities())
            {
                // ask Id to update 


                Console.WriteLine("enter the student id that you want to update");
                var studentToUpdate = context.Students.Find(Convert.ToInt32(Console.ReadLine()));


                if (studentToUpdate != null)
                {
                    Console.WriteLine("Enter Name:");
                    string newName = Console.ReadLine();
                    studentToUpdate.Name = newName;

                    Console.WriteLine("Enter  Mobile:");
                    string newMobile = Console.ReadLine();
                    studentToUpdate.Mobile = newMobile;

                    Console.WriteLine("Enter Email:");
                    string newEmail = Console.ReadLine();
                    studentToUpdate.Email = newEmail;

                    context.SaveChanges();
                    Console.WriteLine("Student updated successfully!");
                    LoggerClass.AddData("updated the table ");
                }
                else
                {
                    Console.WriteLine("No student found with ID:");
                    LoggerClass.AddData("tried updating the table with id that is not present");
                }
            }

        }
    }
}
