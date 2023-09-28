using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StudentManageent.Models;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILogger _logger;
        private readonly StudentDbContext _studentDbContext;

        [Obsolete]
        public HomeController(IStudentRepository studentRepository,
                               IHostingEnvironment hostingEnvironment,
                               ILogger<HomeController> logger, StudentDbContext studentDbContext)
        {
            _studentRepository = studentRepository;
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
            _studentDbContext = studentDbContext;

        }




        public IActionResult Index()
        {
            _logger.LogInformation("Fetching Students Details");
            var model = _studentRepository.GetAllStudent();
            _logger.LogInformation("All Student Details");
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Student model)
        {
            try
            {

                List<Student> subjects = new List<Student>();
                if (ModelState.IsValid)
                {
                     
                        Student newstudent = new Student
                        { 
                            Studname = model.Studname,
                            Gender = model.Gender,
                            City = model.City,
                            Subjects = model.Subjects,
                            Fees = model.Fees    
                        };
                    try
                    {
                        if (newstudent.Fees <= 100)
                        {
                            throw new InvalidOperationException("Invaild Input");
                        }
                    }
                    catch(Exception e)
                    {
                        _logger.LogWarning("Invalid Input",e.Message);
                        return StatusCode(406,"Enter Fees above 100");
                    }
                    _studentRepository.Add(newstudent);
                    _logger.LogInformation("New Student Created");
                    
                    return RedirectToAction("Index", new { id = newstudent.Studid });
                }
                return View("index");
            }
            catch(Exception e)
            {
                _logger.LogError("In Create ",e.Message);
                return StatusCode(500,"Internal Server Problem.. Please Try Again");
            }
            
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                Student student = _studentRepository.GetStudent(id);
                Student model = new Student
                {
                    Studid = student.Studid,
                    Studname = student.Studname,
                    Gender = student.Gender,
                    Subjects = student.Subjects,
                    City = student.City,
                    Fees = student.Fees
                };
                return View(student);
            }
            catch(Exception e)
            {
                _logger.LogError("In GET Edit ", e.Message);
                return StatusCode(500, "Internal Server Problem..");
            }
        }

        [HttpPost]
        public IActionResult Edit(Student model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Student student = _studentRepository.GetStudent(model.Studid);
                    student.Studname = model.Studname;
                    student.Gender = model.Gender;
                    student.Subjects = model.Subjects;
                    student.City = model.City;
                    student.Fees = model.Fees;

                    _studentRepository.Update(student);
                    _logger.LogInformation("Student Get Udpated");
                    return RedirectToAction("index");
                }

                return View();
            }
            catch(Exception e)
            {
                _logger.LogError("In POST Edit ", e.Message);
                return StatusCode(500, "Internal Server Problem..");
            }
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

       /* [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _studentDbContext.Students.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
       */
     /*  [HttpGet]
       public IActionResult DeletePost(int id)
        {
            Student student = _studentRepository.GetStudent(id);
            Student model = new Student
            {

            };

            return View(student);



        }
        [HttpPost]

        public IActionResult DeletePost(Student model)
        {
           if(ModelState.IsValid)
            {
                Student student = _studentRepository.GetStudent(model.Studid);

                _studentDbContext.Students.Remove(student);
                _studentDbContext.SaveChanges();
            }
            /* var obj = _studentDbContext.Students
                 .FirstOrDefault(s => s.Studid.Equals(id));

             if (obj == null)
             {
                 return NotFound();
             }
             _studentDbContext.Students.Remove(obj);
             _studentDbContext.SaveChanges();

             return RedirectToAction("index");
            return RedirectToAction("index");
        }*/

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _studentDbContext.Students.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult DeletePost(int? id)
        {
            var obj = _studentDbContext.Students.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _studentDbContext.Students.Remove(obj);
            _studentDbContext.SaveChanges();
            return RedirectToAction("Index");
        }









    }
}
