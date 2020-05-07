using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkiAssistAPI.Models
{
    public class StaffModel
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RoleType { get; set; }
        public string StaffID { get; set; }
        public string ContactNumber { get; set; }
        public string ContactEmail { get; set; }

        public List<string> CurrentCustodyTypeList { get; set; }

        //can be used to evaluate class level options
        public bool HasCustody { get; set; }


    }
}