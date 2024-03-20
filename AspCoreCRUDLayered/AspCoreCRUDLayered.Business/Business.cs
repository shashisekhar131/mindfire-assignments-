
using AspCoreCRUDLayered.Models;
using AspCoreCRUDLayered.DAL;
using AspCoreCRUDLayered.DAL.DbModels;
using Microsoft.EntityFrameworkCore;


namespace AspCoreCRUDLayered.Business
{
    public class Business:IBusiness
    {

        private IDataAccess _dataAccess;
        public Business(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
       
        public Task<List<StudentModel>> GetAllStudentsAsync()
        {
            return _dataAccess.GetAllStudentsAsync();
        }     
        public Task<bool> DeleteStudentAsync(int id)
        {
            return _dataAccess.DeleteStudentAsync(id);
        }
        public Task<bool> InsertStudentAsync(StudentModel student)
        {
            return _dataAccess.InsertStudentAsync(student);
        }       
        public  Task<bool> UpdateStudentAsync(StudentModel student)
        {
            return _dataAccess.UpdateStudentAsync(student);
        }
        public bool InsertStudent(StudentModel student)
        {
            return _dataAccess.InsertStudent(student);
        }
        public bool DeleteStudent(int id)
        {
            return _dataAccess.DeleteStudent(id);
        }
        public List<StudentModel> GetAllStudents()
        {
            return _dataAccess.GetAllStudents();
        }
        public bool UpdateStudent(StudentModel student)
        {
            return _dataAccess.UpdateStudent(student);
        }
        public StudentModel getStudentDetailsById(int id)
        {
            return _dataAccess.getStudentDetailsById(id);
        }
    }
 }
