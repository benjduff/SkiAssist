using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkiAssistWebsite.Models
{
    public class AddDiet
    {
        public AddStudent AddStudent { get; set;}
        public List<DietList> DietList {get; set;}
    }

    public class DietList
    {
        public int ID { get; set; }
        public string DietRequirement { get; set; }
    }
}