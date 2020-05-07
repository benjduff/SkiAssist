using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkiAssistAPI.Models
{
    public class StudentGuardianEntry
    {
        public int StudentId { get; set; }
        public int GuardianId { get; set; }
        public string StudentName { get; set; }
        public string GuardianName { get; set; }

    }
}