using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBFirstSchoolDBLayers.Models;
using DBFirstSchoolDBLayers.DAL;
using System.Net.Http.Headers;
using System.ComponentModel;
using System.IO;
using OfficeOpenXml;
using DBFirstSchoolDBLayers.Utils;

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

        public Dictionary<GradeModel, List<CourseModel>> DisplayAllCoursesGradeWise()
        {
            return dataAccess.DisplayAllCoursesGradeWise();
        }
        public Dictionary<GradeModel, List<StudentModel>> DisplayAllStudentsGradeWise()
        {
             return dataAccess.DisplayAllStudentsGradeWise();
        }



        public void ExportDataToExcel<T>(List<T> dataList, string fileName)
        {
            try
            {
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                FileInfo fileInfo = new FileInfo(fileName);
                using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
                {
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Count > 0
                        ? excelPackage.Workbook.Worksheets[0]
                        : excelPackage.Workbook.Worksheets.Add("Sheet1");

                    var properties = dataList[0].GetType().GetProperties();

                    for (int j = 0; j < properties.Length; j++)
                    {
                        worksheet.Cells[1, j + 1].Value = properties[j].Name;
                        worksheet.Cells[1, j + 1].Style.Font.Bold = true;
                    }

                    worksheet.Cells[1, properties.Length + 1].Value = "Date and Time";
                    worksheet.Cells[1, properties.Length + 1].Style.Font.Bold = true;

                    for (int i = 0; i < dataList.Count; i++)
                    {
                        properties = dataList[i].GetType().GetProperties();
                        for (int j = 0; j < properties.Length; j++)
                        {
                            worksheet.Cells[i + 2, j + 1].Value = properties[j].GetValue(dataList[i]);
                        }
                        worksheet.Cells[i + 2, properties.Length + 1].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }

                    excelPackage.Save();
                }

                Console.WriteLine($"Data successfully exported to Excel file: {fileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error exporting to Excel file: {ex.Message}");
            }
        }


        public void ExportDataToCsv<T>(List<T> data, string fileName)
        {
            if (data == null || data.Count == 0)
            {
                Console.WriteLine("No data to export.");
                return;
            }

            var csvContent = new StringBuilder();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                csvContent.Append(property.Name).Append(",");
            }
            csvContent.AppendLine();

            foreach (var item in data)
            {
                foreach (var property in properties)
                {
                    var value = property.GetValue(item);
                    csvContent.Append(value).Append(",");
                }
                csvContent.AppendLine();
            }

            File.WriteAllText(fileName, csvContent.ToString());
        }
    }
}
