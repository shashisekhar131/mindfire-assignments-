using AspCoreCRUDLayered.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspCoreCRUDLayered.DAL
{
    public interface IDataAccess
    {
        public List<StudentModel> GetAllStudents();
        public bool DeleteStudent(int id);
        public bool InsertStudent(StudentModel student);
        public bool UpdateStudent(StudentModel student);
        public StudentModel getStudentDetailsById(int id);
    }
}
