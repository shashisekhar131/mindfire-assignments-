using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DBFirstStudentDataBase_School_.Business;

namespace DBFirstDataBase_School_
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
        static void Main(string[] args)
        {
             Service service = new Service();

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
                            service.ReadStudentTable();
                            break;
                        case Operation.InsertStudent:
                            service.InsertStudent();
                            break;
                        case Operation.DeleteStudentRow:
                            service.DeleteStudentRow();
                            break;
                        case Operation.ReadCourseTable:
                            service.ReadCourseTable();
                            break;
                        case Operation.InsertCourse:
                            service.InsertCourse();
                            break;
                        case Operation.DeleteCourseRow:
                            service.DeleteCourseRow();
                            break;
                        case Operation.ReadGradeTable:
                            service.ReadGradeTable();
                            break;
                        case Operation.InsertGrade:
                            service.InsertGrade();
                            break;
                        case Operation.DeleteGradeRow:
                            service.DeleteGradeRow();
                            break;
                        case Operation.AssignStudentToGrade:
                            service.AssignStudentToGrade();
                            break;
                        case Operation.AssignCourseToGrade:
                            service.AssignCourseToGrade();
                            break;
                        case Operation.RemoveCourseFromGrade:
                            service.RemoveCourseFromGrade();
                            break;
                        case Operation.RemoveStudentFromGrade:
                            service.RemoveStudentFromGrade();
                            break;
                        case Operation.DisplayAllStudentInGrades:
                            service.DisplayAllStudentInGrades();
                            break;
                        case Operation.DisplayAllCourseInGrades:
                            service.DisplayAllCoursesInGrades();
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
    }
}
