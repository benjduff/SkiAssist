using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using SkiAssistWebsite.Helper;
using SkiAssistWebsite.Models;

namespace SkiAssistWebsite.Controllers
{
    public class GuardianController : Controller
    {
        // GET: AddGuardian
        public ActionResult AddGuardian(int id)
        {
            var addGuardianViewModel = new AddGuardianViewModel
            {
                AddGuardian = new AddGuardian { NewStudentId = id },
                NewStudentId = id
            };
            return View(addGuardianViewModel);
        }

        [HttpPost]
        public ActionResult PostGuardian(AddGuardianViewModel addGuardianVm)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new AddGuardianViewModel
                {
                    AddGuardian = addGuardianVm.AddGuardian,
                    NewStudentId = addGuardianVm.NewStudentId
                };

                return View("AddGuardian", viewModel);
            }

            var newGuardian = new AddGuardian()
            {
                NewStudentId = addGuardianVm.AddGuardian.NewStudentId,
                GuardianContactNumber = addGuardianVm.AddGuardian.GuardianContactNumber,
                GuardianContactEmail = addGuardianVm.AddGuardian.GuardianContactEmail,
                GuardianFirstName = addGuardianVm.AddGuardian.GuardianFirstName,
                GuardianLastName = addGuardianVm.AddGuardian.GuardianLastName
            };

            var guardianAsJson = JsonConvert.SerializeObject(newGuardian);
            var responseAsync = HttpHelper.Post(guardianAsJson, "api/Guardians");
            Thread.Sleep(1500);
            return RedirectToAction("StudentProfile", "Profile", new { id = addGuardianVm.AddGuardian.NewStudentId });

//            if (responseAsync.Result.IsSuccessStatusCode)
//            {
//                var viewModel = new AddGuardianViewModel { IsSuccess = true };
//                return View("AddGuardian", viewModel);
//            }
//            else
//            {
//                var exception = new InvalidOperationException();
//                var handleErrorInfo = new HandleErrorInfo(exception, "Guardian", "PostGuardian");
//                return View("Error", handleErrorInfo);
//            }
        }
    }
}
