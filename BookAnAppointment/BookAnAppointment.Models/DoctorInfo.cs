using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAnAppointment.Models
{
    public class DoctorInfo
    {
        public string DoctorName { get; set; }
        public System.TimeSpan DayStartTime { get; set; }
        public System.TimeSpan DayEndTime { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int AppointmentSlotTime { get; set; }
    }
}
