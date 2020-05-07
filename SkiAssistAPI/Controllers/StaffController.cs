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
using Microsoft.Ajax.Utilities;
using SkiAssistAPI;
using SkiAssistAPI.Models;

namespace SkiAssistAPI.Controllers
{
    public class StaffController : ApiController
    {
        private SkiAssistDBEntities db = new SkiAssistDBEntities();

        /// <summary>
        /// Return custody list and custody type for selected staff member
        /// </summary>
        /// <param name="StaffId">Staff ID</param>
        /// <returns></returns>
        [ResponseType(typeof(Staff))]
        [Route("api/staff/custodylist/{id}")]
        public IHttpActionResult GetCustodyList(int id)
        {
            var staff = db.Staff.Find(id);
            var studentNames = new List<string>();
            if (staff == null)
            {
                return NotFound();
            }
            foreach (var custody in staff.Custody)
            {
                if (custody.custodyEndTime == null)
                {
                    studentNames.Add(custody.Student.studentFirstName + " " + custody.Student.studentLastName);
                }
            }

            var custodyType = from c in db.Custody
                where c.staffId == id && c.custodyEndTime == null
                select c.custodyType;

            /*
             * var custodyType query...
             * System.NotSupportedException: 'LINQ to Entities does not recognize the method 'System.String LastOrDefault[String](System.Linq.IQueryable`1[System.String])'
             * method, and this method cannot be translated into a store expression.'
             * 
             * Tried Last() First() FirstOrDefault() Single() and ToString() where needed
            */

            var returnValue = new
            {
                CustodyType = custodyType, // This is returning an IQueryable. Attempting to return a string throws an exception
                CustodyList = studentNames
            };

            return Ok(returnValue);
        }

        // GET: api/Staff
        /// <summary>
        /// Returns all staff members in the db
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult GetStaffList()
        {
            var vStaffList = new List<StaffModel>();

            foreach (Staff s in db.Staff)
            {
                var staffId = s.staffId;

                var vStaff = new StaffModel()
                {
                    FirstName = s.staffFirstName,
                    LastName = s.staffLastName,
                    RoleType = s.roleType,
                    StaffID = s.staffId.ToString(),
                    ContactEmail = s.staffContactEmail,
                    ContactNumber = s.staffContactNum,
                    HasCustody = HasCustody(s.staffId),
                    CurrentCustodyTypeList = (from t in db.Custody
                                         where t.staffId == staffId && t.custodyEndTime == null
                                         select t.custodyType).ToList()
                };

                vStaffList.Add(vStaff);
            }

            if (vStaffList.Count > 0)
            {
                return Ok(vStaffList);
            }

            return NotFound();
        }

        // GET: api/Staff/5
        /// <summary>
        /// Return staff details based on the staff id
        /// </summary>
        /// <param name="id">StaffId</param>
        /// <returns></returns>
        [ResponseType(typeof(Staff))]
        public IHttpActionResult GetStaff(int id)
        {
            Staff staff = db.Staff.Find(id);
            if (staff == null)
            {
                return NotFound();
            }

            bool hasCustody = HasCustody(id);

            var staffDetails = new
            {
                staff.staffFirstName,
                staff.staffLastName,
                staff.roleType,
                staff.staffId,
                hasCustody
            };

            return Ok(staffDetails);
        }

        private bool HasCustody(int id)
        {
            return (from c in db.Custody
                    where c.custodyEndTime == null && c.staffId == id
                    select c)
                            .Any();
        }

        // PUT: api/Staff/5
        /// <summary>
        /// Update staff entry using staffEntry object by passing the staffId in the url
        /// </summary>
        /// <param name="id">Staff ID</param>
        /// <param name="staff">StaffEntry Object</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStaff(int id, StaffEntry staffEntry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var staff = db.Staff.Find(id);

            if (staff == null)
            {
                return BadRequest();
            }

            staff.staffFirstName = staffEntry.StaffFirstName ?? staff.staffFirstName;
            staff.staffLastName = staffEntry.StaffLastName ?? staff.staffLastName;
            staff.staffContactEmail = staffEntry.StaffContactEmail ?? staff.staffContactEmail;
            staff.staffContactNum = staffEntry.StaffContactNum ?? staff.staffContactNum;
            staff.staffUsername = staffEntry.StaffUserName ?? staff.staffUsername;
            staff.staffPassword = staffEntry.StaffPassword ?? staff.staffPassword;

            db.Entry(staff).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Staff " + staff.staffFirstName + " " + staff.staffLastName + " updated successfully");
        }

        // POST: api/Staff
        /// <summary>
        /// Create a new staff member by passing a staff object
        /// </summary>
        /// <param name="staff">StaffEntry Object</param>
        /// <returns></returns>
        [ResponseType(typeof(Staff))]
        public IHttpActionResult PostStaff(StaffEntry staff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int nextStaffId = (db.Staff
                            .OrderByDescending(n => n.staffId)
                            .Select(n => n.staffId)
                            .First()) + 1;

            var newStaffMember = new Staff()
            {
                staffId = nextStaffId,
                staffFirstName = staff.StaffFirstName,
                staffLastName = staff.StaffLastName,
                staffContactNum = staff.StaffContactNum,
                staffContactEmail = staff.StaffContactEmail,
                staffUsername = staff.StaffUserName,
                staffPassword = staff.StaffPassword,
                roleType = staff.RoleType
            };

            db.Staff.Add(newStaffMember);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (StaffExists(newStaffMember.staffId))
                {
                    return BadRequest("That staff id number already exists");
                }
            }
            return Ok("Staff member " + newStaffMember.staffFirstName + " " + newStaffMember.staffLastName + " added successfully");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StaffExists(int id)
        {
            return db.Staff.Count(e => e.staffId == id) > 0;
        }
    }
}