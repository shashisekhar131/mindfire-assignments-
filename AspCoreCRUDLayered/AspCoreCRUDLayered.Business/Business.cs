
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
        public List<StudentModel> GetAllStudents()
        {
            return _dataAccess.GetAllStudents();
        }
        public bool DeleteStudent(int id)
        {
            return _dataAccess.DeleteStudent(id);
        }
        public bool InsertStudent(StudentModel student)
        {
            return _dataAccess.InsertStudent(student);
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
