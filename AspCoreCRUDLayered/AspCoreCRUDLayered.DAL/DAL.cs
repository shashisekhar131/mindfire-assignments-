using AspCoreCRUDLayered.DAL.DbModels;
using AspCoreCRUDLayered.Models;
using AspCoreCRUDLayered.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AspCoreCRUDLayered.DAL
{
    public class DataAccess:IDataAccess
    {
        private readonly SchoolDbContext _context;
        private readonly Utils.ILogger _logger;


        public DataAccess(SchoolDbContext context,Utils.ILogger logger)
        {
            _context = context;
            _logger  = logger;
        }

        public async Task<List<StudentModel>> GetAllStudentsAsync()
        {
            List<StudentModel> students = new List<StudentModel>();
            

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    students = await _context.Students
                        .Select(s => new StudentModel
                        {
                            StudentId = s.StudentId,
                            StudentName = s.StudentName,
                            Email = s.Email,
                            Mobile = s.Mobile
                        })
                        .ToListAsync();

                    await transaction.CommitAsync();

                }
                catch (Exception ex)
                {
                    _logger.LogException(ex);
                    await transaction.RollbackAsync();
                }
            }

            return students;
        }      

        public async Task<bool> DeleteStudentAsync(int id)
        {
            bool flag = false;
            try
            {
                var student = await _context.Students.FirstOrDefaultAsync(x => x.StudentId == id);

                if (student != null)
                {
                    _context.Students.Remove(student);
                    await _context.SaveChangesAsync();
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
            }
            return flag;
        }

        public async Task<bool> InsertStudentAsync(StudentModel student)
        {
            bool flag = false;
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var newStudent = new Student
                    {
                        StudentName = student.StudentName,
                        Email = student.Email,
                        Mobile = student.Mobile,
                    };

                    _context.Students.Add(newStudent);
                    await _context.SaveChangesAsync();
                    flag = true;

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogException(ex);
                    await transaction.RollbackAsync();
                }
            }
            return flag;
        }

        public async Task<bool> UpdateStudentAsync(StudentModel student)
        {
            bool flag = false;
            try
            {
                var currentStudent = await _context.Students.FirstOrDefaultAsync(x => x.StudentId == student.StudentId);

                if (currentStudent != null)
                {
                    currentStudent.Email = student.Email;
                    currentStudent.Mobile = student.Mobile;
                    currentStudent.StudentName = student.StudentName;

                    await _context.SaveChangesAsync();
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
            }
            return flag;
        }



        public bool InsertStudent(StudentModel student)
        {
            bool flag = false;
            try
            {
                
                    var newStudent = new Student
                    {
                        StudentName = student.StudentName,
                        Email = student.Email,
                        Mobile = student.Mobile,
                    };
                    _context.Students.Add(newStudent);
                    _context.SaveChanges();
                    flag = true;
                
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);

            }
            return flag;
        }
        public bool UpdateStudent(StudentModel student)
        {
            bool flag = false;
            try
            {
                    var currentstudent = _context.Students.FirstOrDefault(x => x.StudentId == student.StudentId);
                    currentstudent.Email = student.Email;
                    currentstudent.Mobile = student.Mobile;
                    currentstudent.StudentName = student.StudentName;

                    _context.SaveChanges();
                    flag = true;
                
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);

            }
            return flag;
        }

        public StudentModel getStudentDetailsById(int id)
        {
            StudentModel student = new StudentModel();
            try
            {
                    Student studentFromDB = _context.Students.FirstOrDefault(x => x.StudentId == id);
                    student = new StudentModel
                    {
                        StudentId = studentFromDB.StudentId,
                        StudentName = studentFromDB.StudentName,
                        Email = studentFromDB.Email,
                        Mobile = studentFromDB.Mobile
                    };
                
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);

            }
            return student;
        }

        public List<StudentModel> GetAllStudents()
        {
            List<StudentModel> students = new List<StudentModel>();
            try
            {

                students = _context.Students
                                    .Select(s => new StudentModel
                                    {
                                        StudentId = s.StudentId,
                                        StudentName = s.StudentName,
                                        Email = s.Email,
                                        Mobile = s.Mobile
                                    })
                                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);

            }
            return students;
        }

        public bool DeleteStudent(int id)
        {
            bool flag = false;
            try
            {

                var student = _context.Students.FirstOrDefault(x => x.StudentId == id);

                if (student != null)
                {
                    _context.Students.Remove(student);
                    flag = true;
                    _context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                _logger.LogException(ex);

            }
            return flag;
        }
    }
}
