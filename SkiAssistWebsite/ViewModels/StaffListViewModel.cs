using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SkiAssistWebsite.Helper;
using SkiAssistWebsite.Models;

namespace SkiAssistWebsite.ViewModels
{
    public class StaffListViewModel
    {
        public List<Staff> StaffList { get; set; }

        [Display(Name = "Search by Id, Name, Number, Email or Role")]
        public string SearchString { get; set; }

        public StaffListViewModel()
        {
            StaffList = new List<Staff>();
            var allStaff = HttpHelper.GetList<Staff>("/api/staff");
            var staffMembers = allStaff.Result.ToList();
            StaffList = staffMembers;
        }

//        private async Task<List<Staff>> GetAllStaff()
//        {
//            var uriBaseAddress = HttpHelper.UriBaseAddress();
//            var client = new HttpClient { BaseAddress = uriBaseAddress };
//
//            var responseAsync = client.GetAsync("/api/staff/").Result;
//
//            var jsonAsString = await responseAsync.Content.ReadAsStringAsync();
//
//            var deserializedStudentObject = JsonConvert.DeserializeObject<List<Staff>>(jsonAsString);
//
//            return deserializedStudentObject;
//        }
    }
}