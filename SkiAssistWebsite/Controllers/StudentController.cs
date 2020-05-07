using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Owin.Security.Provider;
using Newtonsoft.Json;
using SkiAssistWebsite.Helper;
using SkiAssistWebsite.Models;
using SkiAssistWebsite.ViewModels;

namespace SkiAssistWebsite.Controllers
{
    public class StudentController : Controller
    {

        #region Create new student and post to database

        public ActionResult AddStudent()
        {
            AddDiet addDiet = new AddDiet();
            addDiet.AddStudent = new AddStudent();

            List<DietList> dietList = new List<DietList>();
            dietList.Add(new DietList() { ID = 1, DietRequirement = "Halal" });
            dietList.Add(new DietList() { ID = 2, DietRequirement = "vagiterian2" });
            dietList.Add(new DietList() { ID = 3, DietRequirement = "vagiterian3" });
            dietList.Add(new DietList() { ID = 3, DietRequirement = "vagiterian3" });
            addDiet.DietList = dietList; 

            return View(addDiet);
        }
        
        public ActionResult PostStudent(AddStudent addStudent)
        {
            if (!ModelState.IsValid)
            {
                var addStudentVm = addStudent;
                return View("AddStudent", addStudentVm);
            }

            var newStudent = new AddStudent()
            {
                StudentDob = addStudent.StudentDob,
                StudentFirstName = addStudent.StudentFirstName,
                StudentLastName = addStudent.StudentLastName,
                TicketNumber = addStudent.TicketNumber
            };

            var studentAsJson = JsonConvert.SerializeObject(newStudent);
            var response = HttpHelper.Post(studentAsJson, "api/poststudent");
            var newStudentId = response.Result.Content.ReadAsStringAsync().Result;
            return RedirectToAction("AddGuardian", "Guardian", new { id = newStudentId });
        }

        #endregion

        #region Get students from the database

        // GET: Student
        [Route("student/studentList")]
        [HttpGet]
        public ActionResult StudentList()
        {
            var studentListVm = new StudentListViewModel();

            return View(studentListVm);
        }

        [Route("student/studentSearch")]
        public ActionResult StudentSearch(StudentListViewModel studentListVm)
        {
            var lowerString = studentListVm.SearchString?.ToLower(); // check if searchString is null. if not then save it in lowerString

            if (!string.IsNullOrWhiteSpace(studentListVm.SearchString))
            {
                // will check searchString against first name, last name, studentId and ticket number and return any records that match

                studentListVm.Students = studentListVm.Students
                    .Where(s => s.StudentFirstName.ToLower().Contains(lowerString) || 
                                s.StudentLastName.ToLower().Contains(lowerString) ||
                                s.TicketNumber.ToLower().Contains(lowerString) ||
                                s.StudentId.ToString().ToLower().Contains(lowerString)) 
                    .ToList();
            }

            return View("StudentList", studentListVm);
        }

        #endregion
    } 
}