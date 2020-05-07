using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using SkiAssistAPI.Models;

namespace SkiAssistAPI.Controllers
{
    public class TicketsController : ApiController
    {
        private readonly SkiAssistDBEntities db = new SkiAssistDBEntities();

        // GET: api/Tickets/5
        /// <summary>
        /// Find ticket using the ticket number. Returns the ticket details and the linked student name and ID
        /// </summary>
        /// <param name="id">Ticket Number</param>
        /// <returns></returns>
        [ResponseType(typeof(Ticket))]
        public IHttpActionResult GetTicket(string id)
        {
            var ticket = db.Ticket.Find(id);
            if (ticket == null)
                return NotFound();

            var linkedStudent = db.Student.Single(s => s.ticketNumber == ticket.ticketNumber);

            var ticketReturn = new
            {
                ticket.ticketNumber,
                ticket.ticketIsValid,
                ticket.ticketLessonsRemaining,
                ticket.ticketIssueDate,
                ticket.ticketExpiryDate,
                linkedStudent.studentId,
                linkedStudent.studentFirstName,
                linkedStudent.studentLastName
            };
            return Ok(ticketReturn);
        }

        // PUT: api/Tickets/5
        /// <summary>
        /// Pass the ticket number in the url and send a ticket object to update lessonsRemaining and/or ticketIsValid values
        /// </summary>
        /// <param name="id">Ticket Number</param>
        /// <param name="ticket">Ticket Object Details</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTicket(string id, Ticket ticket)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedTicket = db.Ticket.Find(id);

            if (updatedTicket == null)
            {
                return NotFound();
            }

            updatedTicket.ticketLessonsRemaining = ticket.ticketLessonsRemaining ?? updatedTicket.ticketLessonsRemaining;
            updatedTicket.ticketIsValid = ticket.ticketIsValid ?? updatedTicket.ticketIsValid;

            db.Entry(updatedTicket).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
                    return NotFound();
            }

            return Ok($"Ticket number {updatedTicket.ticketNumber} successfully updated");
        }

        // POST: api/Tickets
        /// <summary>
        /// Pass ticket object to add it to the database
        /// </summary>
        /// <param name="tick">Ticket Object</param>
        /// <returns></returns>
        [ResponseType(typeof(Ticket))]
        public IHttpActionResult PostTicket(TicketEntry tick)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //create a ticket object to match db model

            var ticket = new Ticket
            {
                ticketNumber = tick.ticketNumber,
                ticketIssueDate = DateTime.Now,
                //issue date-time is when created, expires 6 months later by default
                ticketExpiryDate = DateTime.Now.AddMonths(6),
                ticketLessonsRemaining = tick.ticketLessonsRemaining,
                ticketIsValid = tick.ticketIsValid
            };

            db.Ticket.Add(ticket);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TicketExists(ticket.ticketNumber))
                    return Conflict();
                throw;
            }

            return Ok(ticket);
        }

        // PUT: api/Ticket/LinkStudentTicket
        /// <summary>
        /// Pass a Student and ticket Number in to assign ticket to a student. (make private, use in PostTicket -- Damien)
        /// </summary>
        /// <param name="studTick">StudentID and TicketNumber</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        [HttpPut]
        [Route("api/Ticket/LinkStudentTicket")]
        public IHttpActionResult LinkStudentTicket(StudentTicket studTick)
        {
            //If ticket already assigned give warning message
            foreach (var s in db.Student)
            {
                if (s.ticketNumber == studTick.TicketNumber)
                {
                    return BadRequest("Ticket Number already assigned");
                }
            }

            //create student to update ticket details

            var ticket = db.Ticket.Find(studTick.TicketNumber);
            var student = db.Student.Find(studTick.StudentNumber);

            if (student != null && ticket != null)
            {
                student.Ticket = ticket;
                db.Entry(student).State = EntityState.Modified;
            }
            else
            {
                return NotFound();
            }


            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest("You gone dun and broke something");

            }

            return Ok("Student " + student.studentId + " has ticket number " + student.ticketNumber);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }

        private bool TicketExists(string id)
        {
            return db.Ticket.Count(e => e.ticketNumber == id) > 0;
        }
    }
}

