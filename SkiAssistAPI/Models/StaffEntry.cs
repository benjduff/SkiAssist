using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkiAssistAPI.Models
{
    public class StaffEntry
    {
        public string StaffFirstName { get; set; }
        public string StaffLastName { get; set; }
        public string StaffContactNum { get; set; }
        public string StaffContactEmail { get; set; }
        public string StaffUserName { get; set; }
        public string StaffPassword { get; set; }
        public string RoleType { get; set; }
    }
}