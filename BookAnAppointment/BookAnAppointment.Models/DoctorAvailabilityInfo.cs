using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAnAppointment.Models
{
    public class DoctorAvailabilityInfo
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int SlotTime { get; set; }
        public List<TimeSpan> BookedSlots { get; set; }
    }
}
