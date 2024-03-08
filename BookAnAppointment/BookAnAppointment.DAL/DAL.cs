using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using BookAnAppointment.Models;

namespace BookAnAppointment.DAL
{
    public class DataAccess
    {
        public List<DoctorModel> GetAllDoctors()
        {
            List<DoctorModel> listOfDoctors = new List<DoctorModel>();
            try
            {
                using (var context = new BookAnAppointmentEntities())
                {
                    listOfDoctors = context.Doctors
                        .Select(doctorEntity => new DoctorModel
                        {
                            DoctorID = doctorEntity.DoctorID,
                            DoctorName = doctorEntity.DoctorName,
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                //LoggerClass.AddData(ex);
            }

            return listOfDoctors;
        }

        public DoctorAvailabilityInfo GetAvailableSlotsForDoctor(int doctorId, DateTime date)
        {
            DoctorAvailabilityInfo doctorAvailabilityInfo = new DoctorAvailabilityInfo();
            try
            {
                using (var context = new BookAnAppointmentEntities())
                {
                    var doctor = context.Doctors
                        .Where(doc => doc.DoctorID == doctorId)
                        .FirstOrDefault();

                    string DateOnlyString = date.ToString("yyyy-MM-dd");

                    var appointmentsForDoctorOnDate = context.Appointments
                     .Where(app => app.DoctorID == doctorId && app.AppointmentDate.ToString() == DateOnlyString)
                     .Select(app => app.AppointmentTime)
                     .ToList();                   

                    doctorAvailabilityInfo.StartTime = doctor.DayStartTime;
                    doctorAvailabilityInfo.EndTime = doctor.DayEndTime;
                    doctorAvailabilityInfo.SlotTime = doctor.AppointmentSlotTime;

                    doctorAvailabilityInfo.BookedSlots = appointmentsForDoctorOnDate;
                }
            }
            catch (Exception ex)
            {
                //LoggerClass.AddData(ex);
            }

            return doctorAvailabilityInfo;
        }


        public bool InsertAppointment(AppointmentModel appointmentInfo)
        {
            bool flag = false;
            try
            {
                using (var context = new BookAnAppointmentEntities())
                {                   
                    var appointmentEntity = new Appointment
                    {
                        AppointmentDate = appointmentInfo.AppointmentDate,
                        AppointmentTime = appointmentInfo.AppointmentDate.TimeOfDay,
                        DoctorID = appointmentInfo.DoctorID,
                        PatientName = appointmentInfo.PatientName,
                        PatientEmail = appointmentInfo.PatientEmail,
                        PatientPhone = appointmentInfo.PatientPhone,
                        AppointmentStatus = "Open"
                    };

                    context.Appointments.Add(appointmentEntity);
                    context.SaveChanges();
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                //LoggerClass.AddData(ex);
            }
            return flag;
        }

        public bool InsertDoctor(DoctorInfo doctorInfo)
        {
            bool flag = false;
            try
            {
                using (var context = new BookAnAppointmentEntities())
                {

                    var userEntity = new User
                    {
                        UserName = doctorInfo.DoctorName,
                        Password = doctorInfo.Password,
                        Email = doctorInfo.Email,

                    };
                    context.Users.Add(userEntity);
                    context.SaveChanges();

                    var doctorEntity = new Doctor
                    {
                       DoctorName = doctorInfo.DoctorName,
                       DayEndTime = doctorInfo.DayEndTime,  
                       DayStartTime = doctorInfo.DayStartTime,
                       AppointmentSlotTime = doctorInfo.AppointmentSlotTime,
                       UserID = userEntity.UserID
                    };

                    context.Doctors.Add(doctorEntity);
                    context.SaveChanges();                    
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                //LoggerClass.AddData(ex);
            }
            return flag;
        }

        public int CheckIfDoctorExists(string UserEmail, string UserPassword)
        {
            int doctorId = -1;
            try
            {
                using (var context = new BookAnAppointmentEntities())
                {
                    var user = context.Users
                        .FirstOrDefault(u => u.Email == UserEmail);  
                    
                    // Check password case-sensitively
                    if (user != null && user.Password == UserPassword)
                    {
                        var doctor = context.Doctors
                         .FirstOrDefault(d => d.UserID == user.UserID);
                        if(doctor != null)
                        {
                            doctorId = doctor.DoctorID;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //LoggerClass.AddData(ex);
            }
            return doctorId;
        }
        public List<AppointmentModel> GetSortedAndPagedAppointmentsForDoctor(int doctorId, DateTime selectedDate, string sortExpression, string sortDirection, int pageIndex, int pageSize)
        {
            List<AppointmentModel> AppointmentsList = new List<AppointmentModel>();

            using (var Context = new BookAnAppointmentEntities()) 
            {
                IQueryable<Appointment> query = Context.Appointments
                    .Where(appointment => appointment.DoctorID == doctorId &&
                    appointment.AppointmentDate.Year == selectedDate.Year &&
                    appointment.AppointmentDate.Month == selectedDate.Month &&
                    appointment.AppointmentDate.Day == selectedDate.Day);
           
                // Apply dynamic Sorting
                query = ApplyAppointmentSorting(query, sortExpression, sortDirection);

                // Apply paging
                List<Appointment> Appointments = query.Skip(pageIndex * pageSize).Take(pageSize).ToList();

                AppointmentsList = Appointments.Select(appointment => new AppointmentModel
                {
                    AppointmentID = appointment.AppointmentID,
                    AppointmentTime = appointment.AppointmentTime,
                    AppointmentStatus = appointment.AppointmentStatus,
                    AppointmentDate = appointment.AppointmentDate,
                    PatientEmail = appointment.PatientEmail,
                    PatientName = appointment.PatientName,
                    PatientPhone = appointment.PatientPhone                    
                }).ToList();

                return AppointmentsList;
            }
        }

        private IQueryable<Appointment> ApplyAppointmentSorting(IQueryable<Appointment> query, string sortExpression, string sortDirection)
        {
            switch (sortExpression)
            {
                case "AppointmentID":
                    query = (sortDirection == "ASC") ? query.OrderBy(appointment => appointment.AppointmentID) : query.OrderByDescending(appointment => appointment.AppointmentID);
                    break;
                case "AppointmentTime":
                    query = (sortDirection == "ASC") ? query.OrderBy(appointment => appointment.AppointmentTime) : query.OrderByDescending(appointment => appointment.AppointmentTime);
                    break;
                case "PatientName":
                    query = (sortDirection == "ASC") ? query.OrderBy(appointment => appointment.PatientName) : query.OrderByDescending(appointment => appointment.PatientName);
                    break;
            }
            return query;
        }
        public int TotalAppointmentsForDoctor(int doctorId, DateTime selectedDate)
        {
            int total = 0;
            try
            {
                using (var context = new BookAnAppointmentEntities())
                {
                    total = context.Appointments
                        .Count(appointment => appointment.DoctorID == doctorId &&
                                              DbFunctions.TruncateTime(appointment.AppointmentDate) == DbFunctions.TruncateTime(selectedDate));
                }
            }
            catch (Exception ex)
            {
                // LoggerClass.AddData(ex);
            }
            return total;
        }

        public bool CancelAppointment(int id)
        {
            bool flag = false;
            try
            {
                using (var context = new BookAnAppointmentEntities())
                {
                    var appointment = context.Appointments.Find(id);                    
                    appointment.AppointmentStatus = "Cancelled";
                    context.SaveChanges();
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                // LoggerClass.AddData(ex);
            }
            return flag;
        }
        public bool CloseAppointment(int id)
        {
            bool flag = false;
            try
            {
                using (var context = new BookAnAppointmentEntities())
                {
                    var appointment = context.Appointments.Find(id);
                    appointment.AppointmentStatus = "Closed";
                    context.SaveChanges();
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                // LoggerClass.AddData(ex);
            }
            return flag;
        }

        public List<AppointmentSummaryInfo> GetSummaryForMonth(DateTime selectedMonth, int doctorId)
        {
            List<AppointmentSummaryInfo> summaryList = new List<AppointmentSummaryInfo>();

            try
            {
                using (var context = new BookAnAppointmentEntities())
                {
                    var filteredAppointments = context.Appointments
                         .Where(a => a.DoctorID == doctorId && a.AppointmentDate.Month == selectedMonth.Month && a.AppointmentDate.Year == selectedMonth.Year)
                         .ToList();

                    summaryList = filteredAppointments
                        .GroupBy(a => a.AppointmentDate.Date)
                        .Select(group => new AppointmentSummaryInfo
                        {
                            Date = group.Key,
                            TotalAppointments = group.Count(),
                            ClosedAppointments = group.Count(a => a.AppointmentStatus == "Closed"),
                            CancelledAppointments = group.Count(a => a.AppointmentStatus == "Cancelled")
                        })
                        .OrderBy(a => a.Date)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                // Handle the exception, log, or throw it as needed.
            }

            return summaryList;
        }

        public List<AppointmentDetailedInfo> GetDetailedForMonth(DateTime selectedMonth, int doctorId)
        {
            List<AppointmentDetailedInfo> detailedList = new List<AppointmentDetailedInfo>();

            try
            {
                using (var context = new BookAnAppointmentEntities())
                {
                    detailedList = context.Appointments
                        .Where(a => a.DoctorID == doctorId && a.AppointmentDate.Month == selectedMonth.Month && a.AppointmentDate.Year == selectedMonth.Year)
                        .OrderBy(a => a.AppointmentDate)
                        .Select(a => new AppointmentDetailedInfo
                        {
                            Date = a.AppointmentDate,
                            PatientName = a.PatientName,
                            Status = a.AppointmentStatus
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                // Handle the exception, log, or throw it as needed.
            }

            return detailedList;
        }
    }
}
