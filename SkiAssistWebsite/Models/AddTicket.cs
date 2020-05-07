using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkiAssistWebsite.Models
{
    public class AddTicket
    {
        [Required]
        [Display(Name = "Ticket Number")]
        [StringLength(50)]
        public string TicketNumber { get; set; }

        [Required]
        [Display(Name = "Number of Lessons")]
        public int TicketLessonsRemaining { get; set; }
    }

}