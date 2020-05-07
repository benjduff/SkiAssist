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

namespace SkiAssistAPI.Controllers
{
    public class CustodyTypesController : ApiController
    {
        private SkiAssistDBEntities db = new SkiAssistDBEntities();

        // GET: api/CustodyTypes
        /// <summary>
        /// Return all custody types
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetCustodyType()
        {
            var custodyTypes = db.CustodyType.Select(custody => custody.custodyType1).ToList();

            return Ok(custodyTypes);
        }

        // PUT: api/CustodyTypes/5
        /// <summary>
        /// Can we delete this?
        /// </summary>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustodyType(string id, CustodyType custodyType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != custodyType.custodyType1)
            {
                return BadRequest();
            }

            db.Entry(custodyType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustodyTypeExists(id))
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

        // POST: api/CustodyTypes
        /// <summary>
        /// Not Done
        /// </summary>
        [ResponseType(typeof(CustodyType))]
        public IHttpActionResult PostCustodyType(CustodyType custodyType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CustodyType.Add(custodyType);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CustodyTypeExists(custodyType.custodyType1))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = custodyType.custodyType1 }, custodyType);
        }

        // DELETE: api/CustodyTypes/Admin
        /// <summary>
        /// Not Done
        /// </summary>
        [ResponseType(typeof(CustodyType))]
        public IHttpActionResult DeleteCustodyType(string id)
        {
            CustodyType custodyType = db.CustodyType.Find(id);
            if (custodyType == null)
            {
                return NotFound();
            }

            db.CustodyType.Remove(custodyType);
            db.SaveChanges();

            return Ok(custodyType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustodyTypeExists(string id)
        {
            return db.CustodyType.Count(e => e.custodyType1 == id) > 0;
        }
    }
}