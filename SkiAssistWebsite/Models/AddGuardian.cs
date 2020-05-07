using System.ComponentModel.DataAnnotations;

namespace SkiAssistWebsite.Models
{
    public class AddGuardian
    {
        [Required]
        [Display(Name = "First Name")]
        [StringLength(50)]
        public string GuardianFirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50)] 
        public string GuardianLastName { get; set; }

        [Required]
        [Display(Name = "Mobile Number")]
        [Phone]
        public string GuardianContactNumber { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [EmailAddress]
        public string GuardianContactEmail { get; set; }

        public int NewStudentId { get; set; }
    }
}