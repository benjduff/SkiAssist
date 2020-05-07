using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SkiAssistWebsite.Helper;
using SkiAssistWebsite.Models;
using SkiAssistWebsite.ViewModels;

namespace SkiAssistWebsite.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile/StudentProfile/{id}
        public ActionResult StudentProfile(int id)
        {
            var student = HttpHelper.Get<Student>("api/students", id).Result;
            var guardians = HttpHelper.GetList<Guardian>("/api/studentguardian/guardiansviastudentid/", id);
            var profileVm = new ProfileViewModel();
            var guardianList = new List<Guardian>();

            foreach (var guardian in guardians.Result)
            {
                if (!String.IsNullOrWhiteSpace(guardian.GuardianFirstName) && !String.IsNullOrWhiteSpace(guardian.GuardianLastName))
                {
                    guardianList.Add(guardian);
                }
            }

            profileVm.Student = student;
            profileVm.Guardian = guardianList;

            return View(profileVm);
        }

//        public async Task<Student> GetStudent(int studentId)
//        {
//
//            var uriBaseAddress = HttpHelper.UriBaseAddress();
//            var client = new HttpClient { BaseAddress = uriBaseAddress };
//
//
//            var responseAsync = client.GetAsync("/api/students/" + studentId).Result;
//            var jsonAsString = await responseAsync.Content.ReadAsStringAsync();
//
//            var deserializedStudentObject = JsonConvert.DeserializeObject<Student>(jsonAsString);
//
//
//            return deserializedStudentObject;
//
//        }
//
//        public async Task<List<Guardian>> GetGuardian(int studentId)
//        {
//
//            var uriBaseAddress = HttpHelper.UriBaseAddress();
//            var client = new HttpClient { BaseAddress = uriBaseAddress };
//
//            var responseAsync = client.GetAsync("/api/studentguardian/guardiansviastudentid/" + studentId).Result;
//            var jsonAsString = await responseAsync.Content.ReadAsStringAsync();
//
//            var deserializedGuardianObject = JsonConvert.DeserializeObject<List<Guardian>>(jsonAsString);
//
//            return deserializedGuardianObject;
//
//        }
    }
}