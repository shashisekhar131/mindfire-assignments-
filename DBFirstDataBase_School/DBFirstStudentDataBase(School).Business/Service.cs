using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBFirstStudentDataBase_School_.DAL;

namespace DBFirstStudentDataBase_School_.Business
{
    public class Service
    {
        DataAccess dataAccess = new DataAccess();

        public void ReadStudentTable()
        {
          dataAccess.ReadStudentTable();
        }

        public void DeleteStudentRow()
        {
            dataAccess.DeleteStudentRow();
        }

        public void InsertStudent()
        {
            dataAccess.InsertStudent();

        }

        public void InsertCourse()
        {
            dataAccess.InsertCourse();
        }

        public void DeleteCourseRow()
        {
            dataAccess.DeleteCourseRow();

        }

        public void ReadCourseTable()
        {
            dataAccess.ReadCourseTable();
        }

        public void ReadGradeTable()
        {
            dataAccess.ReadGradeTable();
        }

        public void InsertGrade()
        {
            dataAccess.InsertGrade();
        }
        public void DeleteGradeRow()
        {
            dataAccess.DeleteGradeRow();
        }

        public void AssignStudentToGrade()
        {
            dataAccess.AssignStudentToGrade();
        }
        public void AssignCourseToGrade()
        {
            dataAccess.AssignCourseToGrade();
        }
        public void RemoveCourseFromGrade()
        {
            dataAccess.RemoveCourseFromGrade();
        }
        public void RemoveStudentFromGrade()
        {
            dataAccess.RemoveStudentFromGrade();
        }

        public void DisplayAllStudentInGrades()
        {
            dataAccess.DisplayAllStudentInGrades();
        }
        public void DisplayAllCoursesInGrades()
        {
            dataAccess.DisplayAllCoursesInGrades();
        }



    }
}
