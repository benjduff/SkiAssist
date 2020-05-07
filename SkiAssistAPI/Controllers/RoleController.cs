using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SkiAssistAPI.Controllers
{
    public class RoleController : ApiController
    {
        private SkiAssistDBEntities db = new SkiAssistDBEntities();

        [Route("api/roletypes")]
        [HttpGet]
        public IHttpActionResult GetRoleTypes()
        {
            var roleTypes = db.Role.Select(role => role.roleType).ToList();

            return Ok(roleTypes);
        }

        [Route("api/addroletype")]
        [HttpPost]
        public IHttpActionResult PostRoleType(Role roletype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            db.Role.Add(roletype);

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(roletype);
        }
    }
}
