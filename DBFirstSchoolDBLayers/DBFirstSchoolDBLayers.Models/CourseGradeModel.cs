using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBFirstSchoolDBLayers.Models
{
    public class CourseGradeModel
    {
        public int CourseID { get; set; }
        public int GradeID { get; set; }

        public string CourseName { get; set; }
        public string GradeName { get; set; }
    }
}
