using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SkiAssistAPI;
using SkiAssistAPI.Models;

namespace SkiAssistAPI.Controllers
{
    public class StudentGuardianController : ApiController
    {
        private SkiAssistDBEntities db = new SkiAssistDBEntities();

        /// <summary>
        /// Takes Student id, returns name and contact details for all related guardians
        /// </summary>
        /// <param name="id">StudentID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/StudentGuardian/GuardiansViaStudentID/{id}")]
        public IHttpActionResult GetGuardians(int id)
        {
            var guards = new List<object>();
            foreach (var sg in db.StudentGuardian)
            {
                if (sg.studentId == id)
                {
                    guards.Add(
                    new
                    {
                        sg.Guardian.guardianFirstName,
                        sg.Guardian.guardianLastName,
                        sg.Guardian.guardianContactEmail,
                        sg.Guardian.guardianContactNumber,
                        sg.Guardian.guardianID
                    }
                    );
                }
            }

            return Ok(guards);

        }

        /// <summary>
        /// Takes Guardian id, returns details of all related students
        /// </summary>
        /// <param name="id">GuardianID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/StudentGuardian/StudentsViaGuardianID/{id}")]
        public IHttpActionResult GetStudents(int id)
        {
            var studs = new List<object>();

            foreach (var sg in db.StudentGuardian)
            {
                if (sg.guardianId == id)
                {
                    studs.Add(new
                    {
                        sg.Student.studentFirstName,
                        sg.Student.studentLastName,
                        sg.Student.studentId,
                        sg.Student.ticketNumber,
                        studentAgeInYears = StudentsController.CalculateStudentAge(sg.Student.studentDOB)
                    });
                }
            }

            return Ok(studs);
        }

        /// <summary>
        /// THIS METHOD IS NOT DONE. HAVEN'T WORKED OUT HOW TO STRUCTURE IT YET
        /// </summary>
        /// <param name="id"></param>
        /// <param name="studentGuardian"></param>
        /// <returns></returns>
        // PUT: api/StudentGuardian/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStudentGuardian(int id, StudentGuardian studentGuardian)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != studentGuardian.studentId)
            {
                return BadRequest();
            }

            db.Entry(studentGuardian).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentGuardianExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Takes a studentGuardian object and links the student to the guardian. Never accessed directly, only by student and guardian controllers
        /// </summary>
        /// <param name="studentGuardian">studentGuardian object</param>
        /// <returns></returns>
        // POST: api/StudentGuardian
        [ResponseType(typeof(StudentGuardian))]
        public IHttpActionResult PostStudentGuardian(StudentGuardian studentGuardian)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StudentGuardian.Add(studentGuardian);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (StudentGuardianExists(studentGuardian.studentId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = studentGuardian.studentId }, studentGuardian);
        }

        // DELETE: api/StudentGuardian?guardianId={guardianId}&studentId={studentId}
        /// <summary>
        /// Takes guardianId and studentID in URL and deletes entry from the db
        /// </summary>
        /// <param name="guardianId">GuardianId</param>
        /// <param name="studentId">StudentId</param>
        /// <returns></returns>
        [ResponseType(typeof(StudentGuardian))]
        public IHttpActionResult DeleteStudentGuardian(int guardianId, int studentId)
        {
            StudentGuardian studentGuardian = (from sg in db.StudentGuardian
                where sg.studentId == studentId && sg.guardianId == guardianId
                select sg).Single();

            if (studentGuardian == null)
            {
                return NotFound();
            }

            db.StudentGuardian.Remove(studentGuardian);
            db.SaveChanges();

            return Ok($"Guardian ID {studentGuardian.guardianId} removed as guardian of Student ID {studentGuardian.studentId}");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentGuardianExists(int id)
        {
            return db.StudentGuardian.Count(e => e.studentId == id) > 0;
        }
    }
}