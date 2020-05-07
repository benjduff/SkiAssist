using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkiAssistWebsite.Models
{
    public class LostStudent
    {
        public int StudentId { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public string TicketNumber { get; set; }
        public string StaffFirstName { get; set; }
        public string StaffLastName { get; set; }
        public string StaffContactNum { get; set; }
        public DateTime? TimeAlertRaised { get; set; }
        public DateTime? TimeAlertCancelled { get; set; }
        public DateTime CustodyStartTime { get; set; }
        public DateTime? CustodyEndTime { get; set; }
        public string CustodyType { get; set; }

    }
}