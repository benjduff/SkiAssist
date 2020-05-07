using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkiAssistAPI.Models
{
    public class GuardianEntry
    {
        public string GuardianFirstName { get; set; }
        public string GuardianLastName { get; set; }
        public string GuardianContactNumber { get; set; }
        public string GuardianContactEmail { get; set; }
        public int NewStudentId { get; set; }
    }
}