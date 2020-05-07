using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SkiAssistWebsite.Controllers;
using SkiAssistWebsite.Helper;
using SkiAssistWebsite.Models;

namespace SkiAssistWebsite.Controllers
{
    public class TicketController : Controller
    {

        // GET: Ticket
        public ActionResult AddTicket(int id)
        {
            var createNewTicketVm = new AddTicketViewModel()
            { 
                AddTicket = new AddTicket(),
                StudentId = id
            };

            return View(createNewTicketVm);
        }

        public ActionResult PostTicket(AddTicketViewModel ticketVm)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new AddTicketViewModel()
                {
                    AddTicket = ticketVm.AddTicket,
                    StudentId = ticketVm.StudentId
                };

                return View("AddTicket", viewModel);
            }

            var newTicket = new AddTicket()
            {
                TicketNumber = ticketVm.AddTicket.TicketNumber,
                TicketLessonsRemaining = ticketVm.AddTicket.TicketLessonsRemaining
            };

            var ticketAsJson = JsonConvert.SerializeObject(newTicket);
            var responseAsync = HttpHelper.Post(ticketAsJson, "api/tickets");
            Thread.Sleep(1500); // Using this to 
            LinkStudentTicket(ticketVm.StudentId, ticketVm.AddTicket.TicketNumber);
            Thread.Sleep(1500);
            return RedirectToAction("StudentProfile", "Profile", new { id = ticketVm.StudentId });
        }

        private static void LinkStudentTicket(int studentId, string ticketNumber)
        {
            var studentTicket = new
            {
                TicketNumber = ticketNumber,
                StudentNumber = studentId
            };

            var ticketAsJson = JsonConvert.SerializeObject(studentTicket);
            HttpHelper.LinkStudentTicket(ticketAsJson, "api/Ticket/LinkStudentTicket");
        }
    }
}