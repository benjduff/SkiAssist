using System;
using System.ComponentModel.DataAnnotations;

namespace SkiAssistWebsite.Models
{
    public class AddStudent
    {
        public int? StudentId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(50)] 
        public string StudentFirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string StudentLastName { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime StudentDob { get; set; }

        [Required]
        [Display(Name = "Ticket Number")]
        [StringLength(50)]
        public string TicketNumber { get; set; }

        [Required]
        [Display(Name = "Diet Requerment")]
        [StringLength(50)]
        public string DietRequerment { get; set; }

        [Required]
        [Display(Name = "Medical Requerment")]
        [StringLength(100)]
        public string MedicalRequerment { get; set; }
    }
}