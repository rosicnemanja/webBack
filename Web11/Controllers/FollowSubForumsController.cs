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
    public class FollowSubForumsController : ApiController
    {
        private AccessDB db = new AccessDB();

        [EnableQuery]
        // GET: api/FollowSubForums
        public IQueryable<FollowSubForum> GetFollowSubForums()
        {
            return db.FollowSubForums;
        }

        [EnableQuery]
        // GET: api/FollowSubForums/5
        [ResponseType(typeof(FollowSubForum))]
        public IHttpActionResult GetFollowSubForum(int id)
        {
            FollowSubForum followSubForum = db.FollowSubForums.Find(id);
            if (followSubForum == null)
            {
                return NotFound();
            }

            return Ok(followSubForum);
        }

        // PUT: api/FollowSubForums/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFollowSubForum(int id, FollowSubForum followSubForum)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != followSubForum.Id)
            {
                return BadRequest();
            }

            db.Entry(followSubForum).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FollowSubForumExists(id))
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

        // POST: api/FollowSubForums
        [ResponseType(typeof(FollowSubForum))]
        public IHttpActionResult PostFollowSubForum(FollowSubForum followSubForum)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FollowSubForums.Add(followSubForum);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = followSubForum.Id }, followSubForum);
        }

        // DELETE: api/FollowSubForums/5
        [ResponseType(typeof(FollowSubForum))]
        public IHttpActionResult DeleteFollowSubForum(int id)
        {
            FollowSubForum followSubForum = db.FollowSubForums.Find(id);
            if (followSubForum == null)
            {
                return NotFound();
            }

            db.FollowSubForums.Remove(followSubForum);
            db.SaveChanges();

            return Ok(followSubForum);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FollowSubForumExists(int id)
        {
            return db.FollowSubForums.Count(e => e.Id == id) > 0;
        }
    }
}