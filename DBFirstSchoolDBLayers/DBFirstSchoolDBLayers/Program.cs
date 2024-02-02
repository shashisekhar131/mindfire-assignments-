using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

using DBFirstSchoolDBLayers.Business;
using DBFirstSchoolDBLayers.Models;
using DBFirstSchoolDBLayers.Utils;


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
        AddDataToExcel,
        AddDataToCsv,
        Stop
    }


    public enum DatabaseTable
    {
        Student=1,
        Course,
        Grade,
        StudentGrade,
        CourseGrade
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
                        " 16. display all students gradewise \n 17. display all courses gradewise \n 18. Add data to excel \n 19. Add data to csv \n 20.stop");


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
                            program.DisplayAllStudentsGradeWise();
                            break;
                        case Operation.DisplayAllCoursesGradeWise:
                            program.DisplayAllCoursesGradeWise();
                            break;
                        case Operation.AddDataToExcel:
                            program.AddDataToExcel();
                            break;
                        case Operation.AddDataToCsv: 
                            program.AddDataToCsv();
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

        private void AddDataToExcel()
        {

            string filePath = @"C:\Users\belagallus\Desktop\TableFile.xlsx";

            Console.WriteLine("\nDatabase Table Options:\n" +
                "1. Student\n" +
                "2. Course\n" +
                "3. Grade\n" +
                "4. StudentGrade\n" +
                "5. CourseGrade\n");

            DatabaseTable table= (DatabaseTable)Convert.ToInt32(Console.ReadLine());

            switch (table)
            {
                case DatabaseTable.Student:
                    List<StudentModel> ListofStudents = service.ReadStudentTable();
                    service.ExportDataToExcel(ListofStudents, filePath);
                    break;

                case DatabaseTable.Course:
                    List<CourseModel> ListofCourses = service.ReadCourseTable();
                    service.ExportDataToExcel(ListofCourses, filePath);
                    break;

                case DatabaseTable.Grade:
                    List<GradeModel> ListofGrades = service.ReadGradeTable();
                    service.ExportDataToExcel(ListofGrades, filePath);
                    break;

                case DatabaseTable.StudentGrade:
                    List<StudentGradeModel> ListofStudentWithGrades = service.DisplayAllStudentInGrades();
                    service.ExportDataToExcel(ListofStudentWithGrades,filePath);
                    break;

                case DatabaseTable.CourseGrade:
                    List<CourseGradeModel> ListofCourseWithGrades = service.DisplayAllCoursesInGrades();
                    service.ExportDataToExcel(ListofCourseWithGrades, filePath);
                    break;

                default:
                    Console.WriteLine("Invalid table");
                    break;
            }




        }



        private void AddDataToCsv()
        {

            string filePath = @"C:\Users\belagallus\Desktop\TableFile.csv";

            Console.WriteLine("\nDatabase Table Options:\n" +
                "1. Student\n" +
                "2. Course\n" +
                "3. Grade\n" +
                "4. StudentGrade\n" +
                "5. CourseGrade\n");

            DatabaseTable table = (DatabaseTable)Convert.ToInt32(Console.ReadLine());

            switch (table)
            {
                case DatabaseTable.Student:
                    List<StudentModel> ListofStudents = service.ReadStudentTable();
                    service.ExportDataToCsv(ListofStudents, filePath);
                    break;

                case DatabaseTable.Course:
                    List<CourseModel> ListofCourses = service.ReadCourseTable();
                    service.ExportDataToCsv(ListofCourses, filePath);
                    break;

                case DatabaseTable.Grade:
                    List<GradeModel> ListofGrades = service.ReadGradeTable();
                    service.ExportDataToCsv(ListofGrades, filePath);
                    break;

                case DatabaseTable.StudentGrade:
                    List<StudentGradeModel> ListofStudentWithGrades = service.DisplayAllStudentInGrades();
                    service.ExportDataToCsv(ListofStudentWithGrades, filePath);
                    break;

                case DatabaseTable.CourseGrade:
                    List<CourseGradeModel> ListofCourseWithGrades = service.DisplayAllCoursesInGrades();
                    service.ExportDataToCsv(ListofCourseWithGrades, filePath);
                    break;

                default:
                    Console.WriteLine("Invalid table");
                    break;
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
            Console.WriteLine("Enter Student ID  and Grade ID:");
            if (service.AssignStudentToGrade()) Console.WriteLine("Student assigned to Grade successfully.");
            else Console.WriteLine("something went wrong");
        }

        private void AssignCourseToGrade()
        {
            Console.WriteLine("Enter course ID  and Grade ID:");
            if (service.AssignCourseToGrade()) Console.WriteLine("Course assigned to Grade successfully.");
            else Console.WriteLine("something went wrong");
        }

        private void RemoveCourseFromGrade()
        {

            Console.WriteLine("Enter course ID  and Grade ID:");
            if (service.RemoveCourseFromGrade()) Console.WriteLine("Course removed from Grade successfully.");
            else Console.WriteLine("Course or Grade not found.");
        }

        private void RemoveStudentFromGrade()
        {

            Console.WriteLine("Enter Student ID  and Grade ID:");
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

        private void DisplayAllStudentsGradeWise()
        {
            Dictionary<GradeModel, List<StudentModel>> gradeStudentDict = service.DisplayAllStudentsGradeWise();

            foreach (var kvp in gradeStudentDict)
            {
                var grade = kvp.Key;
                var students = kvp.Value;

                if (students.Any())
                {
                    Console.Write($"All Students in Grade {grade.GradeName}: ");
                    foreach (var student in students)Console.Write($"{student.Name}. ");
                    Console.WriteLine();
                }
                else Console.WriteLine($"No Students in Grade {grade.GradeName}");
                
            }
        }

        private void DisplayAllCoursesGradeWise()
        {

            Dictionary<GradeModel, List<CourseModel>> gradeCourseDict = service.DisplayAllCoursesGradeWise();
            // Print the grades and their corresponding courses
            foreach (var kvp in gradeCourseDict)
            {
                var gradeModel = kvp.Key;
                var courses = kvp.Value;

                if (courses.Any())
                {
                    Console.Write($"All Courses in Grade {gradeModel.GradeName}: ");
                    foreach (var course in courses) Console.Write($"{course.CourseName}. ");
                    Console.WriteLine();
                }
                else Console.WriteLine($"No Courses in Grade {gradeModel.GradeName}");
                
            }

            // Log the completion
            LoggerClass.AddData("Displayed courses grade-wise.");
        }



    }
}
