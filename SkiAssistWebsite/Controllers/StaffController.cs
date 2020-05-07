using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using SkiAssistWebsite.Helper;
using SkiAssistWebsite.Models;
using SkiAssistWebsite.ViewModels;

namespace SkiAssistWebsite.Controllers
{
    public class StaffController : Controller
    {
        #region Get staff from database
        public ActionResult StaffList()
        {
            var staffListVm = new StaffListViewModel();

            return View(staffListVm);
        }

        [Route("staff/staffsearch")]
        public ActionResult StaffSearch(StaffListViewModel staffListVm)
        {
            var lowerString = staffListVm.SearchString.ToLower(); // check if searchString is null. if not then save it in lowerString

            if (!string.IsNullOrWhiteSpace(staffListVm.SearchString))
            {
                // will check searchString against first name, last name, studentId and ticket number and return any records that match

                staffListVm.StaffList = staffListVm.StaffList
                    .Where(s => s.FirstName.ToLower().Contains(lowerString) ||
                                s.LastName.ToLower().Contains(lowerString) ||
                                s.ContactEmail.ToLower().Contains(lowerString) ||
                                s.ContactNumber.ToLower().Contains(lowerString) ||
                                s.RoleType.ToLower().Contains(lowerString) ||
                                s.StaffId.ToString().ToLower().Contains(lowerString))
                    .ToList();
            }

            return View("StaffList", staffListVm);
        }

        #endregion

        #region Create new staff member and post to database

        public ActionResult AddStaff()
        {
            var roleTypes = HttpHelper.GetList<string>("api/roletypes");
            var addStaffVm = new AddStaffViewModel()
            {
                RoleTypes = roleTypes.Result.ToList(),
                Staff = new Staff()
            };
             
            return View(addStaffVm);
        }

        public ActionResult PostStaff(AddStaffViewModel staffModel)
        {
            if (!ModelState.IsValid)
            {
                return View("AddStaff", staffModel);
            }

            var staffEntry = new
            {
                StaffFirstName = staffModel.Staff.FirstName,
                StaffLastName = staffModel.Staff.LastName,
                StaffContactNum = staffModel.Staff.ContactNumber,
                StaffContactEmail = staffModel.Staff.ContactEmail,
                StaffUserName = staffModel.Staff.Username,
                StaffPassword = staffModel.Staff.StaffPassword,
                RoleType = staffModel.Staff.RoleType
            };

            var staffAsJson = JsonConvert.SerializeObject(staffEntry);
            var response = HttpHelper.Post(staffAsJson, "api/Staff");
            Thread.Sleep(1000);

            return RedirectToAction("StaffList", "Staff");
        }

        #endregion 
    }
}