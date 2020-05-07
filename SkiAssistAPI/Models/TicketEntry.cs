using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkiAssistAPI.Models
{
    public class TicketEntry
    {
        public string ticketNumber { get; set; }
        public DateTime ticketIssueDate { get; set; }
        public DateTime ticketExpiryDate { get; set; }
        public int ticketLessonsRemaining { get; set; }
        public byte ticketIsValid { get; set; }


    }
}