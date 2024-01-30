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
        DisplayAllCourseGrades,
        Stop
    }
    internal class Program
    {
        static void Main(string[] args)
        {
             Service service = new Service();


            Operation selected;
            do
            {
                Console.WriteLine("\n \n  1. ReadStudentTable \n 2. insert student Data \n 3. delete student row \n" +
                    " 4. ReadCourseTable \n 5. insert Course Data \n 6. delete Course row \n" +
                    " 7. ReadCourseTable \n 8. insert Course Data \n 9. delete Course row \n 10.assign student to course \n" +
                    " 11.assgin course to grade \n 12.remove course from grade \n 13.remove student from grade \n" +
                    " 14.all stuents in grades \n 15.courses in grades \n 16.stop");

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
                    case Operation.DisplayAllCourseGrades:
                        service.DisplayAllCoursesInGrades();
                        break;
                }

            } while (selected != Operation.Stop);
        }
    }
}
