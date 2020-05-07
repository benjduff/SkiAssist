using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using SkiAssistAPI;
using SkiAssistAPI.Models;

namespace SkiAssistAPI.Controllers
{
    public class CustodiesController : ApiController
    {
        private SkiAssistDBEntities db = new SkiAssistDBEntities();

        // GET: api/Custodies
        /// <summary>
        /// Return a list of all of the current custodies with student details and staff details
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetCustody()
        {
            var returnDetails = db.Custody
                .ToList()
                .Select(s => new
                {
                    s.Student.studentFirstName,
                    s.Student.studentLastName,
                    s.Student.studentId,
                    s.Staff.staffFirstName,
                    s.Staff.staffLastName,
                    s.Staff.staffId,
                    s.Staff.staffContactNum

                })
                .AsQueryable();

            return Ok(returnDetails);
        }

        [HttpGet]
        [Route("api/custodies/loststudents")]
        public IHttpActionResult GetLostStudents()
        {
            var returnValue = new List<object>();

            foreach (var custody in db.Custody)
            {
                if (custody.TimeAlertRaised != null)
                {
                    returnValue.Add(new
                    {
                        custody.studentId,
                        custody.Student.studentFirstName,
                        custody.Student.studentLastName,
                        custody.Student.ticketNumber,
                        custody.Staff.staffFirstName,
                        custody.Staff.staffLastName,
                        custody.Staff.staffContactNum,
                        custody.TimeAlertRaised,
                        custody.TimeAlertCancelled,
                        custody.custodyStartTime,
                        custody.custodyEndTime,
                        custody.custodyType
                    });
                }
            }

            return Ok(returnValue);
        }

        /// <summary>
        /// Takes a custody and sets the CustodyEndTime equal to DateTime.Now
        /// </summary>
        /// <param name="pCustody"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/custodies/end")]
        public IHttpActionResult SQLEndCust(Custody pCustody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var custody = db.Custody.SingleOrDefault(c => c.studentId == pCustody.studentId && c.custodyEndTime == null);

            if (custody == null)
            {
                return NotFound();
            }

            if (custody.TimeAlertRaised != null)
            {
                CancelAlert(custody.studentId, custody.staffId);
            }

            var currentTime = CurrentTime(DateTime.Now);
            
            string queryString = @"UPDATE dbo.Custody
            SET custodyEndTime = '" + currentTime + "' WHERE studentId = " + pCustody.studentId + " and custodyEndTime IS null;";
            string connectionString = "Server=skiassistdb.database.windows.net;Database=SkiAssistDB;User Id=skiadmin;Password=Ski12345;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        //    Console.WriteLine(String.Format("{0}, {1}",
                        //        reader["tPatCulIntPatIDPk"], reader["tPatSFirstname"]));// etc
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            return Ok();
        }

        // PUT: api/Custodies/RaiseAlert
        /// <summary>
        /// Takes studentId and staffId and enters datetime.now into timeAlertRaised column on custody
        /// </summary>
        /// <param name="studentId">Student ID</param>
        /// <param name="staffId">Staff ID</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/custodies/raisealert")]
        public IHttpActionResult RaiseAlert(int studentId, int staffId)
        {
            var custody = db.Custody.SingleOrDefault(c => c.studentId == studentId && c.staffId == staffId &&
                                                c.custodyEndTime == null);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (custody == null)
            {
                return NotFound();
            }

            if (custody.TimeAlertRaised != null)
            {
                return BadRequest("Student already marked as lost");
            }

            var currentTime = CurrentTime(DateTime.Now);

            string queryString = $"UPDATE dbo.Custody SET TimeAlertRaised = '{currentTime}' WHERE StudentId = {studentId} AND StaffId = {staffId} and CustodyEndTime IS NULL AND TimeAlertRaised IS NULL;";
            string connectionString = "Server=skiassistdb.database.windows.net;Database=SkiAssistDB;User Id=skiadmin;Password=Ski12345;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        //    Console.WriteLine(String.Format("{0}, {1}",
                        //        reader["tPatCulIntPatIDPk"], reader["tPatSFirstname"]));// etc
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            return Ok();
        }

        // PUT: api/Custodies/RaiseAlert
        /// <summary>
        /// Takes studentId and staffId and enters datetime.now into timeAlertCancelled column on custody
        /// </summary>
        /// <param name="studentId">Student ID</param>
        /// <param name="staffId">Staff ID</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/custodies/cancelalert")]
        public IHttpActionResult CancelAlert(int studentId, int staffId)
        {
            var custody = db.Custody.SingleOrDefault(c => c.studentId == studentId && c.staffId == staffId &&
                                                 c.custodyEndTime == null);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (custody == null)
            {
                return NotFound();
            }

            if (custody.TimeAlertCancelled != null)
            {
                return BadRequest("Student already marked as found");
            }

            if (custody.TimeAlertRaised == null)
            {
                return BadRequest("That student is not marked as lost");
            }

            if (custody.custodyEndTime != null)
            {
                return BadRequest("That student is not in a current custody");
            }

            var currentTime = CurrentTime(DateTime.Now);

            string queryString = $"UPDATE dbo.Custody SET TimeAlertCancelled = '{currentTime}' WHERE StudentId = {studentId} AND StaffId = {staffId} and CustodyEndTime IS NULL AND TimeAlertRaised IS NOT NULL AND TimeAlertCancelled IS NULL;";
            string connectionString = "Server=skiassistdb.database.windows.net;Database=SkiAssistDB;User Id=skiadmin;Password=Ski12345;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        //    Console.WriteLine(String.Format("{0}, {1}",
                        //        reader["tPatCulIntPatIDPk"], reader["tPatSFirstname"]));// etc
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            return Ok();
        }


        // POST: api/Custodies
        /// <summary>
        /// Takes a CustodyEntry object and creates a new custody. If the student is in another custody, it ends that one before creating a new one
        /// </summary>
        /// <param name="cust">CustodyEntry object</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PostCustody(CustodyEntry cust)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var student = (from s in db.Student
                           where s.ticketNumber == cust.TicketNum
                           select s)
                .SingleOrDefault();

            if (student == null) return NotFound();


            //check for open custody
            if (db.Custody.Any(c => c.studentId == student.studentId && c.custodyEndTime == null)) 
            {
                var vCustody = (from c in db.Custody
                                where c.studentId == student.studentId && // Same thing here
                                      c.custodyEndTime == null
                                select c)
                    .Single();
                //end cust via private method which performs sql statement
                SQLEndCust(vCustody);
            }

            var custody = new Custody
            {

                custodyStartTime = DateTime.Now,
                custodyType = cust.CustType,
                staffId = cust.StaffNum,
                studentId = student.studentId,

            };

            var returnValue = new
            {
                student.studentId,
                student.studentFirstName,
                student.studentLastName,
                student.ticketNumber,
                student.studentDOB
            };

            db.Custody.Add(custody);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                return BadRequest(e.Message);
            }
            return Ok(returnValue);
        }

        private static StringBuilder CurrentTime(DateTime currentTime)
        {
            var sb = new StringBuilder();

            sb.Append(currentTime.Year + "-");
            sb.Append(currentTime.Month + "-");
            sb.Append(currentTime.Day + " ");

            sb.Append(currentTime.Hour + ":");
            sb.Append(currentTime.Minute + ":");
            sb.Append(currentTime.Second + ":");
            sb.Append(currentTime.Millisecond);

            return sb;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // Blah
    }
}