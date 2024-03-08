using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookAnAppointment.DAL;
using BookAnAppointment.Models;

namespace BookAnAppointment.Business
{
    public class Business
    {
        DataAccess DataAccess = new DataAccess();

        public List<DoctorModel> GetAllDoctors()
        {
            return DataAccess.GetAllDoctors();
        }

        public DoctorAvailabilityInfo GetAvailableSlotsForDoctor(int doctorId, DateTime date)
        {
            return DataAccess.GetAvailableSlotsForDoctor(doctorId, date);
        }

        public bool InsertAppointment(AppointmentModel appointmentInfo)
        {
            return DataAccess.InsertAppointment(appointmentInfo);
        }
        public bool InsertDoctor(DoctorInfo doctorInfo)
        {
            return DataAccess.InsertDoctor(doctorInfo);
        }

        public int CheckIfDoctorExists(string UserEmail, string UserPassword)
        {
            return DataAccess.CheckIfDoctorExists(UserEmail, UserPassword);
        }
        public List<AppointmentModel> GetSortedAndPagedAppointmentsForDoctor(int doctorId, DateTime selectedDate,string sortExpression, string sortDirection, int pageIndex, int pageSize)
        {
            return DataAccess.GetSortedAndPagedAppointmentsForDoctor(doctorId,selectedDate, sortExpression, sortDirection, pageIndex, pageSize);
        }
        public int TotalAppointmentsForDoctor(int doctorId, DateTime selectedDate)
        {
            return DataAccess.TotalAppointmentsForDoctor(doctorId,selectedDate);
        }

        public bool CancelAppointment(int id)
        {
            return DataAccess.CancelAppointment(id);
        }
        public bool CloseAppointment(int id)
        {
            return DataAccess.CloseAppointment(id);
        }
        public List<AppointmentSummaryInfo> GetSummaryForMonth(DateTime selectedMonth, int doctorId)
        {
            return DataAccess.GetSummaryForMonth(selectedMonth, doctorId);
        }
        public List<AppointmentDetailedInfo> GetDetailedForMonth(DateTime selectedMonth, int doctorId)
        {
            return DataAccess.GetDetailedForMonth(selectedMonth, doctorId);
        }



    }
}
