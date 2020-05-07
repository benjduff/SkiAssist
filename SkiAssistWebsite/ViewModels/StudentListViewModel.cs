using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SkiAssistWebsite.Helper;
using SkiAssistWebsite.Models;

namespace SkiAssistWebsite.ViewModels
{
    public class StudentListViewModel
    {
        public List<Student> Students { get; set; }
        [Display(Name = "Search by Id, Name or TicketNumber")]
        public string SearchString { get; set; }

        public StudentListViewModel()
        {
            var studentTaskList = HttpHelper.GetList<Student>("api/students");
            var students = new List<Student>();
            foreach (var student in studentTaskList.Result)
            {
                students.Add(student);
            }

            Students = students;
        }
    }
}