using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SkiAssistWebsite.Models;

namespace SkiAssistWebsite.ViewModels
{
    public class AddStaffViewModel
    {
        public List<string> RoleTypes { get; set; }
        public Staff Staff { get; set; }
    }
}