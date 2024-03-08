using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAnAppointment.Models
{
    public class AppointmentModel
    {
        public int AppointmentID { get; set; }
        public System.DateTime AppointmentDate { get; set; }
        public System.TimeSpan AppointmentTime { get; set; }
        public int DoctorID { get; set; }
        public string PatientName { get; set; }
        public string PatientEmail { get; set; }
        public string PatientPhone { get; set; }
        public string AppointmentStatus { get; set; }
    }
}
