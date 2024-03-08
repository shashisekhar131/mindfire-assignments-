using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAnAppointment.Models
{
    public class DoctorModel
    {
        public int DoctorID { get; set; }
        public string DoctorName { get; set; }
        public System.TimeSpan DayStartTime { get; set; }
        public System.TimeSpan DayEndTime { get; set; }

    }
}
