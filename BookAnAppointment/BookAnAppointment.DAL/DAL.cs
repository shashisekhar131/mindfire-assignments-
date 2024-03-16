using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BookAnAppointment.Models;
using BookAnAppointment.Utils;

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
                LoggerClass.LogIntoFile(ex);
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
                LoggerClass.LogIntoFile(ex);
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
                        AppointmentStatus = (int)Enums.AppointmentStatusType.Open
                    };

                    context.Appointments.Add(appointmentEntity);
                    context.SaveChanges();
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                LoggerClass.LogIntoFile(ex);
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
                LoggerClass.LogIntoFile(ex);
            }
            return flag;
        }

        public bool UpdateDoctor(DoctorInfo doctorInfo)
        {
            bool flag = false;
            try
            {
                using (var context = new BookAnAppointmentEntities())
                {
                    var user = context.Users.FirstOrDefault(u => u.UserID == doctorInfo.DoctorID);
                    user.UserName = doctorInfo.DoctorName;
                    user.Password = doctorInfo.Password;    
                    user.Email = doctorInfo.Email;


                    var doctor = context.Doctors.FirstOrDefault(doc => doc.DoctorID == doctorInfo.DoctorID);
                    doctor.DoctorName = doctorInfo.DoctorName;
                   /* doctor.AppointmentSlotTime= doctorInfo.AppointmentSlotTime;
                    doctor.DayStartTime = doctorInfo.DayStartTime;
                    doctor.DayEndTime = doctorInfo.DayEndTime;*/
                    
                    context.SaveChanges();
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                LoggerClass.LogIntoFile(ex);
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
                LoggerClass.LogIntoFile(ex);
            }
            return doctorId;
        }
        public List<AppointmentModel> GetSortedAndPagedAppointmentsForDoctor(int doctorId, DateTime selectedDate, string sortExpression, string sortDirection, int pageIndex, int pageSize)
        {
            List<AppointmentModel> AppointmentsList = new List<AppointmentModel>();

            // Storing the integer value in db and sending the string value to frontend
            Dictionary<int, string> status = new Dictionary<int, string>
            {
                { (int)Enums.AppointmentStatusType.Cancelled, "Cancelled" },
                { (int)Enums.AppointmentStatusType.Open, "Open" },
                { (int)Enums.AppointmentStatusType.Closed, "Closed" }
            };

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

                    AppointmentStatus = status[appointment.AppointmentStatus],
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
                default:
                    query = query.OrderBy(appointment => appointment.AppointmentID);
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
                LoggerClass.LogIntoFile(ex);
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
                    appointment.AppointmentStatus = (int)Enums.AppointmentStatusType.Cancelled;
                    context.SaveChanges();
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                LoggerClass.LogIntoFile(ex);
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
                    appointment.AppointmentStatus = (int)Enums.AppointmentStatusType.Closed;
                    context.SaveChanges();
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                LoggerClass.LogIntoFile(ex);
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
                            ClosedAppointments = group.Count(a => a.AppointmentStatus == (int)Enums.AppointmentStatusType.Closed),
                            CancelledAppointments = group.Count(a => a.AppointmentStatus == (int)Enums.AppointmentStatusType.Cancelled)
                        })
                        .OrderBy(a => a.Date)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                LoggerClass.LogIntoFile(ex);
            }

            return summaryList;
        }

        public List<AppointmentDetailedInfo> GetDetailedForMonth(DateTime selectedMonth, int doctorId)
        {
            List<AppointmentDetailedInfo> detailedList = new List<AppointmentDetailedInfo>();
            Dictionary<int, string> status = new Dictionary<int, string>
            {
                { (int)Enums.AppointmentStatusType.Cancelled, "Cancelled" },
                { (int)Enums.AppointmentStatusType.Open, "Open" },
                { (int)Enums.AppointmentStatusType.Closed, "Closed" }
            };
            try
            {
                using (var context = new BookAnAppointmentEntities())
                {               

                  var tempDetailedList = context.Appointments
                   .Where(a => a.DoctorID == doctorId && a.AppointmentDate.Month == selectedMonth.Month && a.AppointmentDate.Year == selectedMonth.Year)
                   .OrderBy(a => a.AppointmentDate)
                   .Select(a => new
                   {
                       Date = a.AppointmentDate,
                       PatientName = a.PatientName,
                       AppointmentStatus = a.AppointmentStatus
                   })
                   .ToList();

                    // Perform dictionary lookup outside LINQ query( Status = status[a.AppointmentStatus] )
                    detailedList = tempDetailedList
                    .Select(a => new AppointmentDetailedInfo
                    {
                        Date = a.Date,
                        PatientName = a.PatientName,
                        Status = status[a.AppointmentStatus]
                    })
                    .ToList();
                }
            }
            catch (Exception ex)
            {
                LoggerClass.LogIntoFile(ex);
            }

            return detailedList;
        }
        public bool DeleteAllAppointments()
        {
            bool flag = false;
            try
            {
                using (var context = new BookAnAppointmentEntities())
                {
                    var appointments = context.Appointments.ToList();
                    // Remove all appointments from the context
                    context.Appointments.RemoveRange(appointments);
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                LoggerClass.LogIntoFile(ex);
            }
            return flag;
        }

        public string GetDoctorName(int doctorId)
        {
            string doctorName = "";
            try
            {
                using (var context = new BookAnAppointmentEntities())
                {
                    var doctor = context.Doctors.FirstOrDefault(doc => doc.DoctorID == doctorId);
                    doctorName = doctor.DoctorName;
                }
            }
            catch (Exception ex)
            {
                LoggerClass.LogIntoFile(ex);
            }
            return doctorName;
        }

        public DoctorInfo GetDoctorDetails(int doctorId)
        {
            DoctorInfo DoctorInfo = null;
            try
            {
                using (var context = new BookAnAppointmentEntities())
                {
                    var doctor = context.Doctors.FirstOrDefault(doc => doc.DoctorID == doctorId);
                    var user = context.Users.FirstOrDefault(u => u.UserID == doctorId);
                    DoctorInfo = new DoctorInfo
                    {
                        DayEndTime = doctor.DayEndTime,
                        DayStartTime = doctor.DayStartTime,
                        DoctorName = doctor.DoctorName,
                        AppointmentSlotTime = doctor.AppointmentSlotTime,
                        Email = user.Email,
                        Password = user.Password
                   };

                }
            }
            catch (Exception ex)
            {
                LoggerClass.LogIntoFile(ex);
            }
            return DoctorInfo;
        }
    }
}
