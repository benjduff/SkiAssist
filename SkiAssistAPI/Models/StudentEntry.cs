using System;
using System.Linq;

namespace SkiAssistAPI.Models
{
    public class StudentEntry
    {
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public DateTime StudentDob { get; set; }
        public string StudentEmergencyContactNum { get; set; }
        public string StudentEmergencyContactName { get; set; }
        public string TicketNumber { get; set; }

    }
}