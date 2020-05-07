using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SkiAssistWebsite.Models
{
    public class Staff
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Mobile number must be 10 digits long")]
        [Display(Name = "Mobile Number")]
        public string ContactNumber { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Email address")]
        [EmailAddress]
        public string ContactEmail { get; set; }

        [StringLength(50)]
        [Display(Name = "Staff Role")]
        public string RoleType { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string StaffPassword { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.Password)]
        [Display(Name = "Retype Password")]
        [Compare("StaffPassword", ErrorMessage = "Passwords mismatch")]
        public string RetypedPassword { get; set; }

        public bool HasCustody { get; set; }
        public List<string> CurrentCustodyTypeList { get; set; }
        public int StaffId { get; set; }
    }
} 