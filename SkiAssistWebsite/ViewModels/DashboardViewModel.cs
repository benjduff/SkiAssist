using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SkiAssistWebsite.Models;

namespace SkiAssistWebsite.ViewModels
{
    public class DashboardViewModel
    {
        public StaffListViewModel StaffListViewModel { get; set; }
        public LostStudentViewModel LostStudentViewModel { get; set; }

        public DashboardViewModel()
        {
            StaffListViewModel = new StaffListViewModel();
            LostStudentViewModel = new LostStudentViewModel();
            var staffWithCurrentCustodies = StaffListViewModel.StaffList.Where(staff => staff.HasCustody).ToList();

            StaffListViewModel.StaffList = staffWithCurrentCustodies;
        }
    }
}