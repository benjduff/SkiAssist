using SkiAssistWebsite.Models;

namespace SkiAssistWebsite.Controllers
{
    public class AddGuardianViewModel
    {
        public int NewStudentId { get; set; }
        public AddGuardian AddGuardian { get; set; }
        public bool IsSuccess { get; set; }
    }
} 