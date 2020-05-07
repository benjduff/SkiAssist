using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using SkiAssistWebsite.Helper;
using SkiAssistWebsite.Models;

namespace SkiAssistWebsite.ViewModels
{
    public class LostStudentViewModel
    {
        public List<LostStudent> LostStudents { get; set; }

        public LostStudentViewModel()
        {
            LostStudents = new List<LostStudent>();
            var allLostStudents = HttpHelper.GetList<LostStudent>("/api/custodies/loststudents");

            // Only return students if they haven't been marked as found
            var currentlyLostStudents = allLostStudents.Result.ToList().Where(lostStudent => lostStudent.TimeAlertCancelled == null).ToList();

            LostStudents = currentlyLostStudents;
        }

//        private async Task<List<LostStudent>> GetLostStudents()
//        {
//            var uriBaseAddress = HttpHelper.UriBaseAddress();
//            var client = new HttpClient { BaseAddress = uriBaseAddress };
//
//            var responseAsync = client.GetAsync("/api/custodies/loststudents").Result;
//
//            var jsonAsString = await responseAsync.Content.ReadAsStringAsync();
//
//            var deserializedStudentObject = JsonConvert.DeserializeObject<List<LostStudent>>(jsonAsString);
//
//            return deserializedStudentObject;
//        }
    }
}