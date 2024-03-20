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

        public async Task<IActionResult> Index()
        {
            ViewBag.studentList = await _Business.GetAllStudentsAsync();           
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            bool flag = await _Business.DeleteStudentAsync(id);
            
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
        public async Task<IActionResult> SubmitStudentDetails(StudentModel student)
        {
            
            if (student.StudentId == 0)
            {
                bool flag = await _Business.InsertStudentAsync(student);
            }
            else
            {
                bool flag = await _Business.UpdateStudentAsync(student);
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
