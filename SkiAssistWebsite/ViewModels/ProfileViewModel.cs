using System.Collections.Generic;
using SkiAssistWebsite.Controllers;
using SkiAssistWebsite.Models;

namespace SkiAssistWebsite.ViewModels
{
    public class ProfileViewModel
    {
        public Student Student { get; set; }
        public List<Guardian> Guardian { get; set; }
    }
} 