using AspCoreCRUDLayered.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspCoreCRUDLayered.Business
{
    public interface IBusiness
    {
        public Task<List<StudentModel>> GetAllStudentsAsync();
        public Task<bool> DeleteStudentAsync(int id);
        public Task<bool> UpdateStudentAsync(StudentModel student);
        public Task<bool> InsertStudentAsync(StudentModel student);

        public bool InsertStudent(StudentModel student);
        public bool DeleteStudent(int id);
        public List<StudentModel> GetAllStudents();
        public bool UpdateStudent(StudentModel student);
        public StudentModel getStudentDetailsById(int id);
    }

}
