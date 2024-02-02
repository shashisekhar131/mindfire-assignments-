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

                    LogDataToExcel.AddDataToExcel("read student table");
                    LoggerClass.AddData("read student table");

                }

            }
            catch (Exception ex)
            {
                LoggerClass.AddData("somehting went wrong during reading from student table");
                LogDataToExcel.AddDataToExcel("error in reading student table");

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
                        
                        LoggerClass.AddData("deleted the row in student table");
                        LogDataToExcel.AddDataToExcel("deleted the row in student table");
                        flag = true;
                    }
                    else
                    {
                       
                        LoggerClass.AddData("tried deleting id that is not present in table");
                        LogDataToExcel.AddDataToExcel("tried deleting id that is not present in table");
                    }
                }
                catch (DbUpdateException ex)
                {

                    LoggerClass.AddData($"Error: {ex}");
                    LogDataToExcel.AddDataToExcel("something went wrong");
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
                    LoggerClass.AddData("inserted student row into table");
                    LogDataToExcel.AddDataToExcel("inserted student row into table");
                }
            }
            catch (DbUpdateException ex)
            {               
                LoggerClass.AddData($"{ex}");
                LogDataToExcel.AddDataToExcel("something went wrong");
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
                    LoggerClass.AddData("inserted new course into table");
                    LogDataToExcel.AddDataToExcel("inserted new course into table");
                }
            }
            catch (DbUpdateException ex)
            {
                Exception innerException = ex;
           
                LoggerClass.AddData("something went wrong" + ex);
                LogDataToExcel.AddDataToExcel("something went wrong");

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

                    LoggerClass.AddData("read the course table");
                    LogDataToExcel.AddDataToExcel("read the course table");
                }
            }
            catch (DbUpdateException ex)
            {
                Exception innerException = ex;
                
                LoggerClass.AddData("something went wrong" + ex);
                LogDataToExcel.AddDataToExcel("something went wrong");
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
                        LoggerClass.AddData("deleted the row in course table ");
                        LogDataToExcel.AddDataToExcel("deleted the row in course table");
                    }
                    else
                    {
                        LoggerClass.AddData("tried deleting id that is not present in course table");
                        LogDataToExcel.AddDataToExcel("tried deleting id that is not present in course table");
                    }
                }
                catch (DbUpdateException ex)
                {
                    Exception innerException = ex;

                    while (innerException.InnerException != null)
                    {
                        innerException = innerException.InnerException;
                    }

                    LoggerClass.AddData($"Error: {innerException.Message}");
                    LogDataToExcel.AddDataToExcel("something went wrong");
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
                    LoggerClass.AddData("inserted grade into table");
                    LogDataToExcel.AddDataToExcel("inserted grade into table");
                }
            }
            catch (DbUpdateException ex)
            {

                Exception innerException = ex;

                LoggerClass.AddData($"Error: {innerException.Message}");
                LogDataToExcel.AddDataToExcel("something went wrong");
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

                    LoggerClass.AddData("read the grade table");
                    LogDataToExcel.AddDataToExcel("read the grade table");
                }

            }
            catch (DbUpdateException ex)
            {
                Exception innerException = ex;
                
                LoggerClass.AddData($"Error: {innerException.Message}");
                LogDataToExcel.AddDataToExcel("something went wrong");
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
                      
                        LoggerClass.AddData("deleted the row in grade table ");
                        LogDataToExcel.AddDataToExcel("deleted the row in grade table");
                    }
                    else
                    {
                        LoggerClass.AddData("tried deleting id that is not present in grade table");
                        LogDataToExcel.AddDataToExcel("tried deleting id that is not present in grade table");
                    }
                }
                catch (DbUpdateException ex)
                {
                    Exception innerException = ex;

                    while (innerException.InnerException != null)
                    {
                        innerException = innerException.InnerException;
                    }


                    LoggerClass.AddData($"Error: {innerException.Message}");
                    LogDataToExcel.AddDataToExcel("something went wrong");
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
                        LoggerClass.AddData("Assigned student to grade.");
                        LogDataToExcel.AddDataToExcel("Assigned student to grade.");

                    }
                    else
                    {
                        LoggerClass.AddData("Student is already assigned to the Grade.");
                        LogDataToExcel.AddDataToExcel("Student is already assigned to the Grade.");

                    }
                }
            }
            catch (Exception ex)
            {

                LoggerClass.AddData("something went wrong");
                LogDataToExcel.AddDataToExcel("something went wrong");


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
                        LoggerClass.AddData("assigned course to grade");
                        LogDataToExcel.AddDataToExcel("assigned course to grade");
                    }
                    else
                    {
                        LoggerClass.AddData("error in assiging course to grade");
                        LogDataToExcel.AddDataToExcel("error in assiging course to grade");
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData("error in assiging course to grade");
                LogDataToExcel.AddDataToExcel("something went wrong");
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

                        LoggerClass.AddData("removed course from grade");
                        LogDataToExcel.AddDataToExcel("removed course from grade");
                    }
                    
                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData("error in removing course from grade");
                LogDataToExcel.AddDataToExcel("something went wrong");

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
                        LoggerClass.AddData("Removed student from grade.");
                        LogDataToExcel.AddDataToExcel("Removed student from grade.");
                    }
                    else
                    {
                        LoggerClass.AddData("Student or Grade assignment not found.");
                        LogDataToExcel.AddDataToExcel("Student or Grade assignment not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData(" something went wrong");
                LogDataToExcel.AddDataToExcel("something went wrong");

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

                    LoggerClass.AddData("Displayed all students in grades.");
                    LogDataToExcel.AddDataToExcel("Displayed all students in grades.");
                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData("Something went wrong");
                LogDataToExcel.AddDataToExcel("something went wrong");

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

                    LoggerClass.AddData("displayed all courses in grades");
                    LogDataToExcel.AddDataToExcel("displayed all courses in grades");
                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData("something went wrong");
                LogDataToExcel.AddDataToExcel("something went wrong");
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

                    // Log the information
                    LoggerClass.AddData("displayed students gradewise");
                    LogDataToExcel.AddDataToExcel("displayed students gradewise");

                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData("some error");
                LogDataToExcel.AddDataToExcel("something went wrong");
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

                    // Log the completion
                    LoggerClass.AddData("Retrieved courses grade-wise.");
                    LogDataToExcel.AddDataToExcel("Retrieved courses grade-wise.");
                }
            }
            catch (Exception ex)
            {
                LoggerClass.AddData("Some error occurred.");
                LogDataToExcel.AddDataToExcel("something went wrong");
            }

            return gradeCourseDict;
        }



    }
}
