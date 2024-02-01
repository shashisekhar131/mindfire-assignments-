using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBFirstSchoolDBLayers.Models
{
    public class StudentGradeModel
    {

        public int StudentID { get; set; }
        public int GradeID { get; set; }

        public string StudentName { get; set; }
        public string GradeName { get; set;}
    }
}
