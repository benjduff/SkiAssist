using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json;
using SkiAssistAPI;
using SkiAssistAPI.Models;

namespace SkiAssistAPI.Controllers
{
    public class StudentsController : ApiController
    {
        private SkiAssistDBEntities db = new SkiAssistDBEntities();

        // GET: api/students
        /// <summary>
        /// Returns the details of every student
        /// </summary>
        /// <returns></returns>
        [Route("api/students")]
        [HttpGet]
        public IHttpActionResult GetAllStudents()
        {
            var returnDetails = db.Student
                .ToList()
                .Select(s => new
                {
                    s.studentId,
                    s.studentFirstName,
                    s.studentLastName,
                    s.ticketNumber,
                    studentAge = CalculateStudentAge(s.studentDOB)
                })
                .AsQueryable();

            return Ok(returnDetails);
        }

        // GET: api/Students/5
        /// <summary>
        /// Return student name and age by passing the student ID in the url
        /// </summary>
        /// <param name="id">StudentID</param>
        /// <returns></returns>
        [ResponseType(typeof(Student))]
        public IHttpActionResult GetStudent(int id)
        {
            var student = db.Student.Find(id);
            if (student == null)
                return NotFound();

            var studentDetails = new
            {
                student.studentFirstName,
                student.studentLastName,
                studentAge = CalculateStudentAge(student.studentDOB),
                student.studentId,
                student.ticketNumber
        };

            return Ok(studentDetails);
        }

        public static int CalculateStudentAge(DateTime studentDob)
        {
            var studentAgeInYears = DateTime.Now.Year - studentDob.Year;
            if (DateTime.Now.DayOfYear < studentDob.DayOfYear)
            {
                studentAgeInYears--;
            }

            return studentAgeInYears;
        }

        // THIS METHOD IS NOT WORKING BECAUSE IF A VALUE ISN'T ENTERED IN THE STUDENTENTRY OBJECT THEN IT SETS THE VALUE TO NULL -- Not working
        // PUT: api/Students/5
        /// <summary>
        /// Takes studentId in url and a studentEntry object as JSON and updates the necessary columns
        /// </summary>
        /// <param name="id">StudentId</param>
        /// <param name="student">StudentEntry object</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStudent(int id, StudentEntry student)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stud = db.Student.Find(id);
            if (stud == null)
            {
                return NotFound();
            }
            stud.ticketNumber = student.TicketNumber ?? stud.ticketNumber; // Changed this on 10/10. Added ?? stud.ticketNumber
            stud.studentFirstName = student.StudentFirstName ?? stud.studentFirstName;
            stud.studentLastName = student.StudentLastName ?? stud.studentLastName;
            // stud.studentDOB = student.StudentDob;   When are you ever going to update a students DOB?????

            db.Entry(stud).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                    return NotFound();
                throw;
            }

            return Ok(stud);
        }

        // POST: api/Students
        /// <summary>
        /// Add a new student to the database. Creates a new ticket object if required
        /// </summary>
        /// <param name="studentEntry">StudentEntry object</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/poststudent")] // Added custom route to solve a 405 error I suddenly got out of nowhere. Fixed and working now though...
        [ResponseType(typeof(Student))]
        public IHttpActionResult PostStudent(StudentEntry studentEntry)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ticketExists = (from t in db.Ticket
                where t.ticketNumber == studentEntry.TicketNumber
                select t).Any();

            if (!ticketExists)
            {
                var ticketController = new TicketsController();
                ticketController.PostTicket(new TicketEntry()
                {
                    ticketNumber = studentEntry.TicketNumber  // Provides no support for ticketIsValid or lessonsRemaining
                });
            }

            int nextStudentId = (db.Student
                            .OrderByDescending(n => n.studentId)
                            .Select(n => n.studentId)
                            .First()) + 1;

            var newStudent = new Student
            {
                studentId = nextStudentId,
                studentFirstName = studentEntry.StudentFirstName,
                studentLastName = studentEntry.StudentLastName,
                studentDOB = studentEntry.StudentDob,
                ticketNumber = studentEntry.TicketNumber
            };

            db.Student.Add(newStudent);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (StudentExists(newStudent.studentId))
                    return BadRequest("That student id already exists");
                throw;
            }
            return Ok(newStudent.studentId);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentExists(int id)
        {
            return db.Student.Count(e => e.studentId == id) > 0;
        }
    }
}