using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAnAppointment.Models
{
    public class AppointmentSummaryInfo
    {
        public DateTime Date { get; set; }
        public System.TimeSpan AppointmentTime { get; set; }
        public int TotalAppointments { get; set; }
        public int ClosedAppointments { get; set; }
        public int CancelledAppointments { get; set; }
    }
}
