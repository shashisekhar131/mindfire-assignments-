using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DBFirstSchoolDBLayers.Business;
using DBFirstSchoolDBLayers.Models;


namespace DBFirstSchoolDBLayers
{
    public enum Operation
    {
        ReadStudentTable = 1,
        InsertStudent,
        DeleteStudentRow,
        ReadCourseTable,
        InsertCourse,
        DeleteCourseRow,
        ReadGradeTable,
        InsertGrade,
        DeleteGradeRow,
        AssignStudentToGrade,
        AssignCourseToGrade,
        RemoveCourseFromGrade,
        RemoveStudentFromGrade,
        DisplayAllStudentInGrades,
        DisplayAllCourseInGrades,
        DisplayAllStudentsGradeWise,
        DisplayAllCoursesGradeWise,
        Stop
    }

    internal class Program
    {


        static Service service = new Service();
       
        static void Main(string[] args)
        {
            Program program = new Program();
            try
            {
                 Operation selected;
                do
                {
                    Console.WriteLine("\n \n 1. Read students table \n 2. insert new student \n 3. delete student \n" +
                        " 4. Read courses table \n 5. insert new course  \n 6. delete course \n" +
                        " 7. Read grades table \n 8. insert new grade \n 9. delete grade  \n 10. assign student to course \n" +
                        " 11. assgin course to grade \n 12. remove course from grade \n 13. remove student from grade \n" +
                        " 14. display all stuents and their grades \n 15. display all courses and their grades \n" +
                        " 16. display all students gradewise \n 17. display all courses gradewise \n 18. stop");


                    selected = (Operation)Convert.ToInt32(Console.ReadLine());




                    switch (selected)
                    {
                        case Operation.ReadStudentTable:
                            program.ReadStudentTable();
                            break;
                        case Operation.InsertStudent:
                            program.InsertStudent();
                            break;
                        case Operation.DeleteStudentRow:
                            program.DeleteStudentRow();
                            break;
                        case Operation.ReadCourseTable:

                            program.ReadCourseTable();
                            break;
                        case Operation.InsertCourse:
                            program.InsertCourse();
                            break;
                        case Operation.DeleteCourseRow:
                            program.DeleteCourseRow();
                            break;
                        case Operation.ReadGradeTable:
                            program.ReadGradeTable();
                            break;
                        case Operation.InsertGrade:
                            program.InsertGrade();
                            break;
                        case Operation.DeleteGradeRow:
                            program.DeleteGradeRow();
                            break;
                        case Operation.AssignStudentToGrade:
                            program.AssignStudentToGrade();
                            break;
                        case Operation.AssignCourseToGrade:
                            program.AssignCourseToGrade();
                            break;
                        case Operation.RemoveCourseFromGrade:
                            program.RemoveCourseFromGrade();
                            break;
                        case Operation.RemoveStudentFromGrade:
                            program.RemoveStudentFromGrade();
                            break;
                        case Operation.DisplayAllStudentInGrades:
                            program.DisplayAllStudentInGrades();
                            break;
                        case Operation.DisplayAllCourseInGrades:
                            program.DisplayAllCoursesInGrades();
                            break;
                        case Operation.DisplayAllStudentsGradeWise:
                            service.DisplayAllStudentsGradeWise();
                            break;
                        case Operation.DisplayAllCoursesGradeWise:
                            service.DisplayAllCoursesGradeWise();
                            break;
                    }

                } while (selected != Operation.Stop);
            }
            catch (Exception ex)
            {
                Console.WriteLine("selected wrong option press any key to end the program");
                Console.ReadLine();
            }



        }


        private void ReadStudentTable()
        {
            Console.WriteLine("List of all students: \n loading please wait...");

            List<StudentModel> ListOfStudents = service.ReadStudentTable();
            for (int i = 0; i < ListOfStudents.Count; i++)
            {
                Console.WriteLine($"Sudent Name:{ListOfStudents[i].Name}  ID:{ListOfStudents[i].StudentID}");
            }
        }

        private void InsertStudent()
        {
            Console.WriteLine("Enter the new student's Name:");
            if (service.InsertStudent()) Console.WriteLine("Student created successfully!");
            else Console.WriteLine("something went wrong");
        }

        private void DeleteStudentRow()
        {
            Console.WriteLine("Enter the student ID to delete:");

            if (service.DeleteStudentRow()) Console.WriteLine("Student deleted successfully!");
            else Console.WriteLine("No student found with ID:");
        }

        private void ReadCourseTable()
        {
            List<CourseModel> ListOfCourses = service.ReadCourseTable();
            Console.WriteLine("List of all courses: \n loading please wait...");
            foreach (var course in ListOfCourses)
            {

                Console.WriteLine($"ID: {course.CourseID}, Name: {course.CourseName}");
            }

        }

        private void InsertCourse()
        {
            Console.WriteLine("Enter the new course Name:");
            if (service.InsertCourse()) Console.WriteLine("Course created successfully!");
            else Console.WriteLine("something went wrong");
        }

        private void DeleteCourseRow()
        {

            Console.WriteLine("Enter the course ID to delete:");

            if (service.DeleteCourseRow()) Console.WriteLine("course deleted successfully!");
            else Console.WriteLine("No course found with ID:");
        }

        private void ReadGradeTable()
        {
            Console.WriteLine("List of all Grades: \n loading please wait...");
            List<GradeModel> ListOfGrades = service.ReadGradeTable();

            foreach (var Grade in ListOfGrades)
            {
                Console.WriteLine($"ID: {Grade.GradeID}, Name: {Grade.GradeName}");
            }
        }

        private void InsertGrade()
        {
            Console.WriteLine("Enter the new Grade Name:");
            if (service.InsertGrade()) Console.WriteLine("Grade created successfully!");
            else Console.WriteLine("somehting went wrong");
        }

        private void DeleteGradeRow()
        {
            Console.WriteLine("Enter the grade ID to delete:");

            if (service.DeleteGradeRow()) Console.WriteLine("grade deleted successfully!");
            else Console.WriteLine("No grade found with ID:");
        }

        private void AssignStudentToGrade()
        {
            if (service.AssignStudentToGrade()) Console.WriteLine("Student assigned to Grade successfully.");
            else Console.WriteLine("something went wrong");
        }

        private void AssignCourseToGrade()
        {
            if (service.AssignCourseToGrade()) Console.WriteLine("Course assigned to Grade successfully.");
            else Console.WriteLine("something went wrong");
        }

        private void RemoveCourseFromGrade()
        {
            if (service.RemoveCourseFromGrade()) Console.WriteLine("Course removed from Grade successfully.");
            else Console.WriteLine("Course or Grade not found.");
        }

        private void RemoveStudentFromGrade()
        {
            if (service.RemoveStudentFromGrade()) Console.WriteLine("Student removed from Grade successfully.");
            else Console.WriteLine("something went wrong");
        }

        private void DisplayAllStudentInGrades()
        {
            List<StudentGradeModel> ListOfStudentWithGrades = service.DisplayAllStudentInGrades();
            Console.WriteLine("All Students In Grades:");
            foreach (var ele in ListOfStudentWithGrades)
            {
                Console.WriteLine($" Name:{ele.StudentName}(ID:{ele.StudentID}) , Name: {ele.GradeName} (ID: {ele.GradeID})");
            }
        }

        private void DisplayAllCoursesInGrades()
        {
            List<CourseGradeModel> ListOfCourseWithGrades = service.DisplayAllCoursesInGrades();

            Console.WriteLine("All CourseGrades:");
            foreach (var ele in ListOfCourseWithGrades)
            {
                Console.WriteLine($"Course {ele.CourseName}( ID: {ele.CourseID}), Grade {ele.GradeName}(ID: {ele.GradeID})");

            }
        }

    }
}
