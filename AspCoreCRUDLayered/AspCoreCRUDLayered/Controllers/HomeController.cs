using AspCoreCRUDLayered.Business;
using AspCoreCRUDLayered.DAL.DbModels;
using AspCoreCRUDLayered.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MyBusiness =AspCoreCRUDLayered.Business.Business;

namespace AspCoreCRUDLayered.Controllers
{
    public class HomeController : Controller
    {
        private IBusiness _Business;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,IBusiness business)
        {
            _logger = logger;
            _Business = business;
        }

        public IActionResult Index()
        {
            ViewBag.studentList = _Business.GetAllStudents();           
            return View();
        }

        public IActionResult Delete(int id)
        {
            bool flag = _Business.DeleteStudent(id);
            
            return RedirectToAction("Index");
        }
        public IActionResult StudentDetailsForm()
        {
            int id = Convert.ToInt32(HttpContext.Request.RouteValues["id"]);
            TempData["id"] = id;
            StudentModel student= new StudentModel();
            if (id != 0)
            {
                 student = _Business.getStudentDetailsById(id);               
                return View(student);                
            }
            return View(student);
        }

        [HttpPost]
        public IActionResult SubmitStudentDetails(StudentModel student)
        {
            
            if (student.StudentId == 0)
            {
                bool flag = _Business.InsertStudent(student);
            }
            else
            {
                bool flag = _Business.UpdateStudent(student);
            }

            return RedirectToAction("Index");
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
