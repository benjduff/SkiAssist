using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkiAssistAPI.Models
{
    public class CustodyEntry
    {
        public string TicketNum { get; set; }
        public int StaffNum { get; set; }
        public string CustType { get; set; }
    }
}