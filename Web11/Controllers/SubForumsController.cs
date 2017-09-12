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
using System.Web.Http.OData;
using Web11.Models;
using Web11.Models.Core;

namespace Web11.Controllers
{
    public class SubForumsController : ApiController
    {
        private AccessDB db = new AccessDB();

        [EnableQuery]
        // GET: api/SubForums
        public IQueryable<SubForum> GetSubForums()
        {
            return db.SubForums;
        }

        [EnableQuery]
        // GET: api/SubForums/5
        [ResponseType(typeof(SubForum))]
        public IHttpActionResult GetSubForum(int id)
        {
            SubForum subForum = db.SubForums.Find(id);
            if (subForum == null)
            {
                return NotFound();
            }

            return Ok(subForum);
        }

        // PUT: api/SubForums/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSubForum(int id, SubForum subForum)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subForum.Id)
            {
                return BadRequest();
            }

            db.Entry(subForum).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubForumExists(id))
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

        // POST: api/SubForums
        [ResponseType(typeof(SubForum))]
        public IHttpActionResult PostSubForum(SubForum subForum)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.SubForums.Add(subForum);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = subForum.Id }, subForum);
            }
            catch(Exception exc)
            {
                return StatusCode(HttpStatusCode.NotAcceptable);
            }
        }

        // DELETE: api/SubForums/5
        [ResponseType(typeof(SubForum))]
        public IHttpActionResult DeleteSubForum(int id)
        {
            SubForum subForum = db.SubForums.Find(id);
            if (subForum == null)
            {
                return NotFound();
            }

            db.SubForums.Remove(subForum);
            db.SaveChanges();

            return Ok(subForum);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubForumExists(int id)
        {
            return db.SubForums.Count(e => e.Id == id) > 0;
        }
    }
}