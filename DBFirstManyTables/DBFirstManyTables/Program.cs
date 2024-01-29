using System;
using System.Data.Entity.Infrastructure;
using System.Linq;

using Entity_Framework_MtoM_CRUD.LOG;

namespace DBFirstManyTables
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int selected;
            do
            {
                Console.WriteLine(" 1. ReadStudentTable \n 2. insert student Data \n 3. delete student row \n" +
                    "4. ReadCourseTable \n 5. insert Course Data \n 6. delete Course row \n"+
                    "7. ReadCourseTable \n 8. insert Course Data \n 9. delete Course row \n 10.stop"
                    );

                selected = Convert.ToInt32(Console.ReadLine());

                switch (selected)
                {
                    case 1:
                        ReadStudentTable();
                        break;
                    case 2:
                        InsertStudent();
                        break;
                    case 3:
                       DeleteStudentRow();
                        break;
                    case 4:
                        ReadCourseTable();
                        break;
                    case 5:
                        InsertCourse();
                        break;
                    case 6:
                        DeleteCourseRow();
                        break;
                    case 7:
                        ReadGradeTable();
                        break;
                    case 8:
                        InsertGrade();
                        break;
                    case 9:
                        DeleteGradeRow();
                        break;
                }

            } while (selected != 10);

        }


        public static void InsertStudent()
        {


            using (var context = new XYZSchoolDBEntities())
            {

                // ask details
                Console.WriteLine("Enter the new student's Name:");
                string newName = Console.ReadLine();

                // create Student object
                var newStudent = new Student{
                    Name = newName,
                };

                // pass it to context API 

                context.Students.Add(newStudent);
                context.SaveChanges();
                Console.WriteLine("Student created successfully!");
                LoggerClass.AddData("inserted into table");
            }
        }

        public static void ReadStudentTable()
        {
            using (var context = new XYZSchoolDBEntities())
            {
                Console.WriteLine("List of all students: \n loading please wait...");

                var students = context.Students.ToList();

                foreach (var student in students)
                {
                    Console.WriteLine($"ID: {student.StudentID}, Name: {student.Name}");
                }

                LoggerClass.AddData("read the table");
            }
        }

        public static void DeleteStudentRow()
        {
           
            Console.WriteLine("from the table select the id");


            using (var context = new XYZSchoolDBEntities())
            {
                Console.WriteLine("Enter the student ID to delete:");

                var studentToDelete = context.Students.Find(Convert.ToInt32(Console.ReadLine()));
                try {
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
                catch (DbUpdateException ex)
                {
                    Exception innerException = ex;

                    while (innerException.InnerException != null)
                    {
                        innerException = innerException.InnerException;
                    }

                    Console.WriteLine($"Error: {innerException.Message}");
                    Console.WriteLine($"Error: {innerException.Message}");
                    LoggerClass.AddData($"Error: {innerException.Message}");
                }


            }



        }


        public static void InsertCourse()
        {


            using (var context = new XYZSchoolDBEntities())
            {

                // ask details
                Console.WriteLine("Enter the new course Name:");
                string newName = Console.ReadLine();

                // create Student object
                var newCourse = new Course
                {
                   CourseName = newName,
                };

                // pass it to context API 

                context.Courses.Add(newCourse);
                context.SaveChanges();
                Console.WriteLine("Course created successfully!");
                LoggerClass.AddData("inserted into table");
            }
        }

        public static void ReadCourseTable()
        {
            using (var context = new XYZSchoolDBEntities())
            {
                Console.WriteLine("List of all courses: \n loading please wait...");

                var courses = context.Courses.ToList();

                foreach (var course in courses)
                {
                    Console.WriteLine($"ID: {course.CourseID}, Name: {course.CourseName}");
                }

                LoggerClass.AddData("read the table");
            }
        }

        public static void DeleteCourseRow()
        {

            Console.WriteLine("from the table select the id");


            using (var context = new XYZSchoolDBEntities())
            {
                Console.WriteLine("Enter the course ID to delete:");

                var courseToDelete = context.Courses.Find(Convert.ToInt32(Console.ReadLine()));
                try
                {
                    if (courseToDelete != null)
                    {
                        context.Courses.Remove(courseToDelete);
                        context.SaveChanges();
                        Console.WriteLine("course deleted successfully!");
                        LoggerClass.AddData("deleted the row");
                    }
                    else
                    {
                        Console.WriteLine("No course found with ID:");
                        LoggerClass.AddData("tried deleting id that is not present in table");
                    }
                }
                catch (DbUpdateException ex)
                {
                    Exception innerException = ex;

                    while (innerException.InnerException != null)
                    {
                        innerException = innerException.InnerException;
                    }

                    Console.WriteLine($"Error: {innerException.Message}");
                    Console.WriteLine($"Error: {innerException.Message}");
                    LoggerClass.AddData($"Error: {innerException.Message}");
                }


            }



        }






        public static void InsertGrade()
        {


            using (var context = new XYZSchoolDBEntities())
            {

                // ask details
                Console.WriteLine("Enter the new Grade Name:");
                string newName = Console.ReadLine();

                // create Student object
                var newGrade = new Grade
                {
                    GradeName = newName,
                };

                // pass it to context API 

                context.Grades.Add(newGrade);
                context.SaveChanges();
                Console.WriteLine("Grade created successfully!");
                LoggerClass.AddData("inserted into table");
            }
        }

        public static void ReadGradeTable()
        {
            using (var context = new XYZSchoolDBEntities())
            {
                Console.WriteLine("List of all Grades: \n loading please wait...");

                var Grades = context.Grades.ToList();

                foreach (var Grade in Grades)
                {
                    Console.WriteLine($"ID: {Grade.GradeID}, Name: {Grade.GradeName}");
                }

                LoggerClass.AddData("read the table");
            }
        }

        public static void DeleteGradeRow()
        {

            Console.WriteLine("from the table select the id");


            using (var context = new XYZSchoolDBEntities())
            {
                Console.WriteLine("Enter the Grade ID to delete:");

                var GradeToDelete = context.Grades.Find(Convert.ToInt32(Console.ReadLine()));
                try
                {
                    if (GradeToDelete != null)
                    {
                        context.Grades.Remove(GradeToDelete);
                        context.SaveChanges();
                        Console.WriteLine("Grade deleted successfully!");
                        LoggerClass.AddData("deleted the row");
                    }
                    else
                    {
                        Console.WriteLine("No Grade found with ID:");
                        LoggerClass.AddData("tried deleting id that is not present in table");
                    }
                }
                catch (DbUpdateException ex)
                {
                    Exception innerException = ex;

                    while (innerException.InnerException != null)
                    {
                        innerException = innerException.InnerException;
                    }

                    Console.WriteLine($"Error: {innerException.Message}");
                    Console.WriteLine($"Error: {innerException.Message}");
                    LoggerClass.AddData($"Error: {innerException.Message}");
                }


            }



        }



    }
}
