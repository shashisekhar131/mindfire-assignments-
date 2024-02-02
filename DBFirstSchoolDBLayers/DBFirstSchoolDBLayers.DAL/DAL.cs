using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBFirstSchoolDBLayers.Utils;
using DBFirstSchoolDBLayers.Models;

namespace DBFirstSchoolDBLayers.DAL
{

    public class DataAccess
    {
        
        public List<StudentModel> ReadStudentTable()
        {
            List<StudentModel> ListOfStudents = new List<StudentModel>();
            try
            {
                using (var context = new XYZSchoolDBEntities())
                {                    
                    var students = context.Students.ToList();

                    foreach (var student in students)
                    {
                        StudentModel tempStudent = new StudentModel()
                        {
                            StudentID = student.StudentID,
                            Name = student.Name,
                        };

                        ListOfStudents.Add(tempStudent);
                    }
                }

            }
            catch (Exception ex)
            {
                LoggerClass.AddData("something went wrong during reading the student table" + ex);                
            }

            return ListOfStudents; 
        }


        public bool DeleteStudentRow()
        {
            bool flag = false;

            using (var context = new XYZSchoolDBEntities())
            {
               try
                {
                    var studentToDelete = context.Students.Find(Convert.ToInt32(Console.ReadLine()));
                    if (studentToDelete != null)
                    {
                        context.Students.Remove(studentToDelete);
                        context.SaveChanges();                        
                        flag = true;
                    }
                    
                }
                catch (DbUpdateException ex)
                {
                    LoggerClass.AddData("something went wrong during deleting a row in student table" + ex);
                }
            }
            return flag;

        }

        public bool InsertStudent()
        {
            bool flag = false;
            try
            {
                using (var context = new XYZSchoolDBEntities())
                {
                    // ask details
                    string newName = Console.ReadLine();

                    // create Student object
                    var newStudent = new Student
                    {
                        Name = newName,
                    };

                    // pass it to context API 
                    context.Students.Add(newStudent);
                    context.SaveChanges();
                    flag = true;
                }
            }
            catch (DbUpdateException ex)
            {
                LoggerClass.AddData("something went wrong during inserting a row in student table" + ex);
            }

            return flag;
        }


        public bool InsertCourse()
        {
            bool flag = false;
            try
            {
                using (var context = new XYZSchoolDBEntities())
                {
                    // ask details                   
                    string newName = Console.ReadLine();

                    // create Student object
                    var newCourse = new Course
                    {
                        CourseName = newName,
                    };

                    // pass it to context API 
                    context.Courses.Add(newCourse);
                    context.SaveChanges();
                    flag = true;
                }
            }
            catch (DbUpdateException ex)
            {
                LoggerClass.AddData("something went wrong during inserting a row in course table" + ex);
            }
            return flag;
        }

        public List<CourseModel> ReadCourseTable()
        {
            List<CourseModel> ListOfCourses = new List<CourseModel>();
            try
            {
                using (var context = new XYZSchoolDBEntities())
                {            
                    var courses = context.Courses.ToList();

                    foreach (var course in courses)
                    {
                        CourseModel tempCourse = new CourseModel()
                        {
                            CourseID = course.CourseID,
                            CourseName = course.CourseName,
                        };

                        ListOfCourses.Add(tempCourse);
                     }

                }
            }
            catch (DbUpdateException ex)
            {
                LoggerClass.AddData("something went wrong during reading the course table" + ex);
            }
            return ListOfCourses;
        }

        public bool DeleteCourseRow()
        {
            bool flag = false;
            

            using (var context = new XYZSchoolDBEntities())
            {
               

                var courseToDelete = context.Courses.Find(Convert.ToInt32(Console.ReadLine()));
                try
                {
                    if (courseToDelete != null)
                    {
                        context.Courses.Remove(courseToDelete);
                        context.SaveChanges();
                        flag = true;
                    }
                   
                }
                catch (DbUpdateException ex)
                {
                    LoggerClass.AddData("something went wrong during deleting a row in course table" + ex);
                }
            }
            return flag;
        }


       public bool InsertGrade()
        {
            bool flag = false;
            try
            {
                using (var context = new XYZSchoolDBEntities())
                {

                    // ask details                   
                    string newName = Console.ReadLine();

                    // create Student object
                    var newGrade = new Grade
                    {
                        GradeName = newName,
                    };

                    // pass it to context API
                    context.Grades.Add(newGrade);
                    context.SaveChanges();
                    flag =true;
                }
            }
            catch (DbUpdateException ex)
            {
                LoggerClass.AddData("something went wrong during inserting a row in grade table" + ex);
            }
            return flag;
        }

        public List<GradeModel> ReadGradeTable()
        {
            List<GradeModel> ListofGrades = new List<GradeModel>();

            try
            {
                using (var context = new XYZSchoolDBEntities())
                {            
                    var Grades = context.Grades.ToList();

                    foreach (var Grade in Grades)
                    {
                        GradeModel tempGrade = new GradeModel()
                        {
                            GradeID = Grade.GradeID,
                            GradeName = Grade.GradeName
                        };
                        ListofGrades.Add (tempGrade);                      
                    }

                }

            }
            catch (DbUpdateException ex)
            {
                LoggerClass.AddData("something went wrong during reading the grade table" + ex);
            }
            return ListofGrades;
        }

        public bool DeleteGradeRow()
        {
            bool flag = false;           

            using (var context = new XYZSchoolDBEntities())
            {                
                var GradeToDelete = context.Grades.Find(Convert.ToInt32(Console.ReadLine()));
                try
                {
                    if (GradeToDelete != null)
                    {
                        context.Grades.Remove(GradeToDelete);
                        context.SaveChanges();
                        flag = true;                      
                    }
                }
                catch (DbUpdateException ex)
                {
                    LoggerClass.AddData("something went wrong during deleting a row in grade table" + ex);
                }
            }
            return flag;
        }




        // we are creating using  exiting database and there is no third column in rlationship table so STudentGrades will not be in EF 
        public bool AssignStudentToGrade()
        {
            bool flag = false;

            try
            {               
                int studentId = int.Parse(Console.ReadLine());               
                int gradeId = int.Parse(Console.ReadLine());

                using (var context = new XYZSchoolDBEntities())
                {

                    StudentGrade existingAssignment = context.StudentGrades
                .FirstOrDefault(sg => sg.StudentID == studentId && sg.GradeID == gradeId);

                    if (existingAssignment == null)
                    {
                        // Create a new StudentGrade entity for the assignment
                        StudentGrade newAssignment = new StudentGrade
                        {
                            StudentID = studentId,
                            GradeID = gradeId
                        };

                        // Add the new assignment to the StudentGrades DbSet
                        context.StudentGrades.Add(newAssignment);

                        // Save changes to the database
                        context.SaveChanges();
                        flag = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData("something went wrong during assigning student to grade" + ex);
            }
            return flag;
        }


        public bool AssignCourseToGrade()
        {
            bool flag = false;
            try
            {

                int courseId = int.Parse(Console.ReadLine());
                int gradeId = int.Parse(Console.ReadLine());

                using (var context = new XYZSchoolDBEntities())
                {
                    Course course = context.Courses.Find(courseId);
                    Grade grade = context.Grades.Find(gradeId);

                    if (course != null && grade != null)
                    {
                        // Assign the course to the grade directly
                        course.Grades.Add(grade);

                        context.SaveChanges();
                        flag = true;

                    }
                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData("something went wrong during assigning course to grade" + ex);
            }
            return flag;
        }


        public bool RemoveCourseFromGrade()
        {

            bool flag = false;
            try
            {               
                int courseId = int.Parse(Console.ReadLine());               
                int gradeId = int.Parse(Console.ReadLine());

                using (var context = new XYZSchoolDBEntities())
                {
                    Course course = context.Courses.Find(courseId);
                    Grade grade = context.Grades.Find(gradeId);

                    if (course != null && grade != null)
                    {
                        // Remove the course from the grade
                        course.Grades.Remove(grade);

                        context.SaveChanges();
                        flag = true; 

                    }

                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData("something went wrong during removing course from grade" + ex);
            }
            return flag;
        }


        public bool RemoveStudentFromGrade()
        {
            bool flag = false;
            try
            {
                int studentId = int.Parse(Console.ReadLine());
                int gradeId = int.Parse(Console.ReadLine());

                using (var context = new XYZSchoolDBEntities())
                {
                    StudentGrade assignment = context.StudentGrades
               .FirstOrDefault(sg => sg.StudentID == studentId && sg.GradeID == gradeId);

                    if (assignment != null)
                    {
                        // Remove the assignment from the StudentGrades DbSet
                        context.StudentGrades.Remove(assignment);

                        // Save changes to the database
                        context.SaveChanges();
                        flag = true;

                    }
                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData("something went wrong during removing student from grade" + ex);
            }
            return flag;
        }


        public List<StudentGradeModel> DisplayAllStudentInGrades()
        {

            List<StudentGradeModel> ListOfstudentWithGrades = new List<StudentGradeModel>();
            try
            {
                using (var context = new XYZSchoolDBEntities())
                {
                    // Retrieve all students with their grades from the database
                    var studentsWithGrades = context.Students
                        .Select(student => new
                        {
                            Student = student,
                            Grades = student.StudentGrades.Select(sg => sg.Grade)
                        })
                        .ToList();

                  
                    foreach (var studentWithGrades in studentsWithGrades)
                    {
                        var student = studentWithGrades.Student;
                        foreach (var grade in studentWithGrades.Grades)
                        {                            
                            StudentGradeModel tempstudentGrade = new StudentGradeModel()
                            {
                                 GradeID = grade.GradeID,
                                 StudentID = student.StudentID,
                                 StudentName = student.Name,
                                 GradeName = grade.GradeName,
                            };

                            ListOfstudentWithGrades.Add(tempstudentGrade);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData("something went wrong during displaying students" + ex);
            }
            return ListOfstudentWithGrades;
        }



        public List<CourseGradeModel> DisplayAllCoursesInGrades()
        {

            List<CourseGradeModel> ListOfCourseWithGrades = new List<CourseGradeModel>();
            try
            {
                using (var context = new XYZSchoolDBEntities())
                {
                    var courses = context.Courses.Include("Grades");                   
                    foreach (var course in courses)
                    {
                        foreach (var grade in course.Grades)
                        {
                            CourseGradeModel tempcourseGrade = new CourseGradeModel()
                            {
                                GradeID = grade.GradeID,
                                CourseID = course.CourseID,
                                CourseName = course.CourseName,
                                GradeName = grade.GradeName,
                            };

                            ListOfCourseWithGrades.Add(tempcourseGrade);
                          }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData("something went wrong during displayig courses" + ex);
            }
            return ListOfCourseWithGrades;
        }




        public Dictionary<GradeModel, List<StudentModel>> DisplayAllStudentsGradeWise()
        {
            Dictionary<GradeModel, List<StudentModel>> gradeStudentDict = new Dictionary<GradeModel, List<StudentModel>>();
           try
            {
                using (var context = new XYZSchoolDBEntities())
                {
                    // Retrieve all grades with their students from the database
                    var gradesWithStudents = context.Grades
                        .Select(grade => new
                        {
                            Grade = grade,
                            Students = grade.StudentGrades.Select(sg => sg.Student).ToList()
                        })
                        .ToList();

                    foreach (var gradeWithStudents in gradesWithStudents)
                    {
                        var grade = gradeWithStudents.Grade;
                        var students = gradeWithStudents.Students;

                        GradeModel tempGrade = new GradeModel()
                        {
                            GradeID = grade.GradeID,
                            GradeName = grade.GradeName
                        };

                        List<StudentModel> ListOfStudents = new List<StudentModel>();
                        foreach (var student in students)
                        {
                            StudentModel tempStudent = new StudentModel()
                            {
                                StudentID = student.StudentID,
                                Name = student.Name,
                            };

                            ListOfStudents.Add(tempStudent);

                        }

                        // Add grade and students to the dictionary
                        gradeStudentDict.Add(tempGrade, ListOfStudents);

                    }
                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData("something went wrong during displaying students gradewise" + ex);
            }
            return gradeStudentDict;
        }



        public Dictionary<GradeModel, List<CourseModel>> DisplayAllCoursesGradeWise()
        {
            Dictionary<GradeModel, List<CourseModel>> gradeCourseDict = new Dictionary<GradeModel, List<CourseModel>>();

            try
            {
                using (var context = new XYZSchoolDBEntities())
                {
                    // Retrieve all grades with their courses from the database
                    var gradesWithCourses = context.Grades.Include("Courses").ToList();

                    foreach (var grade in gradesWithCourses)
                    {
                        GradeModel tempGrade = new GradeModel()
                        {
                            GradeID = grade.GradeID,
                            GradeName = grade.GradeName
                        };

                        List<CourseModel> listOfCourses = new List<CourseModel>();

                            foreach (var course in grade.Courses)
                            {
                                CourseModel tempCourse = new CourseModel()
                                {
                                    CourseID = course.CourseID,
                                    CourseName = course.CourseName,
                                    // Add other properties as needed
                                };

                                listOfCourses.Add(tempCourse);
                            }

                            // Add grade and courses to the dictionary
                            gradeCourseDict.Add(tempGrade, listOfCourses);

                    }

                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData("something went wrong during displaying course gradewise" + ex);
            }
            return gradeCourseDict;
        }


    }
}
