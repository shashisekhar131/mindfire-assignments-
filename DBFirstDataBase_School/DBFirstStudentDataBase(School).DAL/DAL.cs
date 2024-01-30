using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBFirstStudentDataBase_School_.Utils;
namespace DBFirstStudentDataBase_School_.DAL
{
    public class DataAccess
    {

     

        public void ReadStudentTable()
        {
            try
            {
                using (var context = new XYZSchoolDBEntities())
                {
                    Console.WriteLine("List of all students: \n loading please wait...");

                    var students = context.Students.ToList();

                    foreach (var student in students)
                    {
                        Console.WriteLine($"ID: {student.StudentID}, Name: {student.Name}");
                    }
                    LoggerClass.AddData("read student table");

                }

            }catch (Exception ex)
            {
                LoggerClass.AddData("somehting went wrong");
                Console.WriteLine("something went wrong");

            }

        }


        public void DeleteStudentRow()
        {

            Console.WriteLine("from the table select the id");


            using (var context = new XYZSchoolDBEntities())
            {
                Console.WriteLine("Enter the student ID to delete:");

                var studentToDelete = context.Students.Find(Convert.ToInt32(Console.ReadLine()));
                try
                {
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

        public  void InsertStudent()
        {

            try {
                using (var context = new XYZSchoolDBEntities())
                {

                    // ask details
                    Console.WriteLine("Enter the new student's Name:");
                    string newName = Console.ReadLine();

                    // create Student object
                    var newStudent = new Student
                    {
                        Name = newName,
                    };

                    // pass it to context API 

                    context.Students.Add(newStudent);
                    context.SaveChanges();
                    Console.WriteLine("Student created successfully!");
                    LoggerClass.AddData("inserted into table");
                }
            }catch (DbUpdateException ex)
            {
                Exception innerException = ex;
                Console.WriteLine(ex);
                LoggerClass.AddData("something went wrong");
            }
            
        }


        public void InsertCourse()
        {
            try {
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
            }catch(DbUpdateException ex)
            {
                 Exception innerException = ex;
                Console.WriteLine("something went wrong" + ex);
                LoggerClass.AddData("something went wrong");

            }

            
        }

        public  void ReadCourseTable()
        {

            try
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
            }catch (DbUpdateException ex)
            {
                Exception innerException = ex;
                Console.WriteLine("something went wrong" + ex);
                LoggerClass.AddData("something went wrong");
            }
           
        }

        public void DeleteCourseRow()
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






        public  void InsertGrade()
        {

            try
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
            }catch (DbUpdateException ex) { 
                
                Exception innerException = ex;
                Console.WriteLine($"Error: {innerException.Message}");
                Console.WriteLine($"Error: {innerException.Message}");
                LoggerClass.AddData($"Error: {innerException.Message}");
            }
            
        }

        public  void ReadGradeTable()
        {

            try
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

            }catch(DbUpdateException ex)
            {
                Exception innerException = ex;
                Console.WriteLine($"Error: {innerException.Message}");
                Console.WriteLine($"Error: {innerException.Message}");
                LoggerClass.AddData($"Error: {innerException.Message}");
            }
            
        }

        public  void DeleteGradeRow()
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




        // we are creating using  exiting database and there is no third column in rlationship table so STudentGrades will not be in EF 
        public void AssignStudentToGrade()
        {
            Console.WriteLine("Enter Student ID:");
            int studentId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Grade ID:");
            int gradeId = int.Parse(Console.ReadLine());

            try
            {
                using (var context = new XYZSchoolDBEntities())
                {
                    Student student = context.Students.Find(studentId);
                    Grade grade = context.Grades.Find(gradeId);

                    if (student != null && grade != null)
                    {
                        // Assign the student to the grade directly
                        student.Grades.Add(grade);

                        context.SaveChanges();

                        Console.WriteLine("Student assigned to Grade successfully.");
                        LoggerClass.AddData("assgined student to grade ");
                    }
                    else
                    {
                        Console.WriteLine("Student or Grade not found.");
                        LoggerClass.AddData("error in assiging student to grade");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        public  void AssignCourseToGrade()
        {
            Console.WriteLine("Enter Course ID:");
            int courseId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Grade ID:");
            int gradeId = int.Parse(Console.ReadLine());

            try
            {
                using (var context = new XYZSchoolDBEntities())
                {
                    Course course = context.Courses.Find(courseId);
                    Grade grade = context.Grades.Find(gradeId);

                    if (course != null && grade != null)
                    {
                        // Assign the course to the grade directly
                        course.Grades.Add(grade);

                        context.SaveChanges();

                        Console.WriteLine("Course assigned to Grade successfully.");
                        LoggerClass.AddData("assigned course to grade");
                    }
                    else
                    {
                        Console.WriteLine("Course or Grade not found.");
                        LoggerClass.AddData("error in assiging course to grade");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        public void RemoveCourseFromGrade()
        {
            Console.WriteLine("Enter Course ID:");
            int courseId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Grade ID:");
            int gradeId = int.Parse(Console.ReadLine());

            try
            {
                using (var context = new XYZSchoolDBEntities())
                {
                    Course course = context.Courses.Find(courseId);
                    Grade grade = context.Grades.Find(gradeId);

                    if (course != null && grade != null)
                    {
                        // Remove the course from the grade
                        course.Grades.Remove(grade);

                        context.SaveChanges();

                        Console.WriteLine("Course removed from Grade successfully.");
                        LoggerClass.AddData("removed course from grade");
                    }
                    else
                    {
                        Console.WriteLine("Course or Grade not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        public void RemoveStudentFromGrade()
        {
            Console.WriteLine("Enter Student ID:");
            int studentId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Grade ID:");
            int gradeId = int.Parse(Console.ReadLine());

            try
            {
                using (var context = new XYZSchoolDBEntities())
                {
                    Student student = context.Students.Find(studentId);
                    Grade grade = context.Grades.Find(gradeId);

                    if (student != null && grade != null)
                    {
                        // Remove the student from the grade
                        student.Grades.Remove(grade);

                        context.SaveChanges();


                        Console.WriteLine("Student removed from Grade successfully.");
                        LoggerClass.AddData("removed student from grade");
                    }
                    else
                    {
                        Console.WriteLine("Student or Grade not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        public void DisplayAllStudentInGrades()
        {
            try
            {
                using (var context = new XYZSchoolDBEntities())
                {
                    var students = context.Students.Include("Grades");

                    Console.WriteLine("All StudentsInGrades:");
                    foreach (var student in students)
                    {
                        foreach (var grade in student.Grades)
                        {
                            Console.WriteLine($"Student ID: {student.StudentID}, Grade ID: {grade.GradeID}");
                        }
                    }
                    LoggerClass.AddData("displayed all students in grades ");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }



        public void DisplayAllCoursesInGrades()
        {
            try
            {
                using (var context = new XYZSchoolDBEntities())
                {
                    var courses = context.Courses.Include("Grades");

                    Console.WriteLine("All CourseGrades:");
                    foreach (var course in courses)
                    {
                        foreach (var grade in course.Grades)
                        {
                            Console.WriteLine($"Course ID: {course.CourseID}, Grade ID: {grade.GradeID}");
                        }
                    }

                    LoggerClass.AddData("displayed all courses in grades");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

    }
}
