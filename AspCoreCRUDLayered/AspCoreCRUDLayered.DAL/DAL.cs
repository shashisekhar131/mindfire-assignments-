using AspCoreCRUDLayered.DAL.DbModels;
using AspCoreCRUDLayered.Models;

namespace AspCoreCRUDLayered.DAL
{
    public class DataAccess:IDataAccess
    {
        private readonly SchoolDbContext _context;

        public DataAccess(SchoolDbContext context)
        {
            _context = context;
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
                // Handle exception
               
            }
            return students;
        }

        public bool DeleteStudent(int id)
        {
            bool flag  = false;
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
                // Handle exception

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
                // Handle exception

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
                // Handle exception

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
                // Handle exception

            }
            return student;
        }
      

    }
}
