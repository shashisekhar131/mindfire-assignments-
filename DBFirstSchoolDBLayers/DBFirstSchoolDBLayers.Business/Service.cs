using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBFirstSchoolDBLayers.Models;
using DBFirstSchoolDBLayers.DAL;
using System.Net.Http.Headers;

namespace DBFirstSchoolDBLayers.Business
{

    public class Service
    {
        DataAccess dataAccess = new DataAccess();

        public List<StudentModel> ReadStudentTable()
        {
            
            List<StudentModel> ListOfStudents = dataAccess.ReadStudentTable();
            return ListOfStudents;

        }

        public bool DeleteStudentRow()
        {
            return dataAccess.DeleteStudentRow();
        }

        public bool InsertStudent()
        {
           return dataAccess.InsertStudent();

        }

        public bool InsertCourse()
        {
           return dataAccess.InsertCourse();
        }

        public bool DeleteCourseRow()
        {
            return dataAccess.DeleteCourseRow();

        }

        public List<CourseModel> ReadCourseTable()
        {
            List<CourseModel> ListOfCourses = dataAccess.ReadCourseTable();
            return ListOfCourses;
         
        }

        public List<GradeModel> ReadGradeTable()
        {
            List<GradeModel> ListOfGrades =  dataAccess.ReadGradeTable();
            return ListOfGrades;
        }

        public bool InsertGrade()
        {
           return dataAccess.InsertGrade();
        }
        public bool DeleteGradeRow()
        {
           return dataAccess.DeleteGradeRow();
        }

        public bool AssignStudentToGrade()
        {
            return dataAccess.AssignStudentToGrade();
        }
        public bool AssignCourseToGrade()
        {
           return dataAccess.AssignCourseToGrade();
        }
        public bool RemoveCourseFromGrade()
        {
           return dataAccess.RemoveCourseFromGrade();
        }
        public bool RemoveStudentFromGrade()
        {
            return dataAccess.RemoveStudentFromGrade();
        }

        public List<StudentGradeModel> DisplayAllStudentInGrades()
        {
            List<StudentGradeModel> ListOfStudentWithGrades = dataAccess.DisplayAllStudentInGrades();
            return ListOfStudentWithGrades;
        }
        public  List<CourseGradeModel> DisplayAllCoursesInGrades()
        {
            List<CourseGradeModel> ListOfCourseWithGrades =   dataAccess.DisplayAllCoursesInGrades();
            return ListOfCourseWithGrades;
        }

        public void DisplayAllCoursesGradeWise()
        {
            dataAccess.DisplayAllCoursesGradeWise();
        }
        public void DisplayAllStudentsGradeWise()
        {
            dataAccess.DisplayAllStudentsGradeWise();
        }

    }
}
