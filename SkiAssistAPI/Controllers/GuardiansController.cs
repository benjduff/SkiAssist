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
using System.Web.Http.Results;
using SkiAssistAPI;
using SkiAssistAPI.Models;

namespace SkiAssistAPI.Controllers
{
    public class GuardiansController : ApiController
    {
        private SkiAssistDBEntities db = new SkiAssistDBEntities();

        /// <summary>
        /// Return a guardian object by passing the guardianId in URL
        /// </summary>
        /// <param name="id">GuardianId</param>
        /// <returns></returns>
        // GET: api/Guardians/5
        [ResponseType(typeof(Guardian))]
        public IHttpActionResult GetGuardian(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Guardian guardian = db.Guardian.Find(id);
            if (guardian == null)
            {
                return NotFound();
            }

            var guardianReturnValue = new
            {
                guardian.guardianFirstName,
                guardian.guardianLastName,
                guardian.guardianContactNumber,
                guardian.guardianContactEmail,
                guardian.guardianID
            };

            return Ok(guardianReturnValue);
        }

        /// <summary>
        /// Pass a guardian ID in the url and a guardianEntry object. Send any number of properties. Will only update the values that are passed to it
        /// </summary>
        /// <param name="id">GuardianID</param>
        /// <param name="guard">GuardianEntry object</param>
        /// <returns></returns>
        // PUT: api/Guardians/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGuardian(int id, GuardianEntry guard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var guardian = db.Guardian.Find(id);

            if (guardian == null)
            {
                return NotFound();
            }

            // If new values are passed then it will update the db, otherwise it will leave the values as their originals
            guardian.guardianFirstName = guard.GuardianFirstName ?? guardian.guardianFirstName;
            guardian.guardianLastName = guard.GuardianLastName ?? guardian.guardianLastName;
            guardian.guardianContactEmail = guard.GuardianContactEmail ?? guardian.guardianContactEmail;
            guardian.guardianContactNumber = guard.GuardianContactNumber ?? guardian.guardianContactNumber;

            db.Entry(guardian).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!GuardianExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest("The update failed due to a DbUpdateConcurrencyException: " + e.InnerException);
                }
            }

            var guardianDetails = new
            {
                guardian.guardianFirstName,
                guardian.guardianLastName,
                guardian.guardianID,
                guardian.guardianContactEmail,
                guardian.guardianContactNumber
            };

            return Ok(guardianDetails);
        }

        /// <summary>
        /// Create a new guardian and link that guardian to an existing student. newStudentId will come from the UI
        /// </summary>
        /// <param name="guard">GuardianEntry object</param>
        /// <returns></returns>
        // POST: api/Guardians
        [ResponseType(typeof(Guardian))]
        public IHttpActionResult PostGuardian(GuardianEntry guard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //get the next ID
            var nextId = (db.Guardian
                         .OrderByDescending(g => g.guardianID)
                         .Select(g => g.guardianID)
                         .First()) + 1;

            //compare against DB to make sure sure it exceeds the last number
            //probably better to store some app settings in a separate table



            foreach (var g in db.Guardian)
            {
                if (g.guardianID > nextId)
                {
                    nextId = g.guardianID;
                }
            }

            
            //create new guardian from passed object
            var guardian = new Guardian()
            {
                guardianID = nextId,
                guardianFirstName = guard.GuardianFirstName,
                guardianLastName = guard.GuardianLastName,
                guardianContactNumber = guard.GuardianContactNumber,
                guardianContactEmail = guard.GuardianContactEmail
            };


            db.Guardian.Add(guardian);

            var studentGuardianController = new StudentGuardianController();
            var studentGuardian = new StudentGuardian()
            {
                guardianId = guardian.guardianID,
                studentId = guard.NewStudentId
            };

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                if (GuardianExists(guardian.guardianID))
                {
                    return BadRequest("That guardian already exists in the system");
                }
                else
                {
                    return BadRequest("Update failed with a DbUpdateException: " + e.InnerException);
                }
            }

            studentGuardianController.PostStudentGuardian(studentGuardian);

            return Ok("Guardian created and linked to student successfully");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GuardianExists(int id)
        {
            return db.Guardian.Count(e => e.guardianID == id) > 0;
        }
    }
}