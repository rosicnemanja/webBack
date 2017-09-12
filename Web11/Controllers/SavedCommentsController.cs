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
    public class SavedCommentsController : ApiController
    {
        private AccessDB db = new AccessDB();

        [EnableQuery]
        // GET: api/SavedComments
        public IQueryable<SavedComment> GetSavedComments()
        {
            return db.SavedComments;
        }

        [EnableQuery]
        // GET: api/SavedComments/5
        [ResponseType(typeof(SavedComment))]
        public IHttpActionResult GetSavedComment(int id)
        {
            SavedComment savedComment = db.SavedComments.Find(id);
            if (savedComment == null)
            {
                return NotFound();
            }

            return Ok(savedComment);
        }

        // PUT: api/SavedComments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSavedComment(int id, SavedComment savedComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != savedComment.Id)
            {
                return BadRequest();
            }

            db.Entry(savedComment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SavedCommentExists(id))
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

        // POST: api/SavedComments
        [ResponseType(typeof(SavedComment))]
        public IHttpActionResult PostSavedComment(SavedComment savedComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SavedComments.Add(savedComment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = savedComment.Id }, savedComment);
        }

        // DELETE: api/SavedComments/5
        [ResponseType(typeof(SavedComment))]
        public IHttpActionResult DeleteSavedComment(int id)
        {
            SavedComment savedComment = db.SavedComments.Find(id);
            if (savedComment == null)
            {
                return NotFound();
            }

            db.SavedComments.Remove(savedComment);
            db.SaveChanges();

            return Ok(savedComment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SavedCommentExists(int id)
        {
            return db.SavedComments.Count(e => e.Id == id) > 0;
        }
    }
}