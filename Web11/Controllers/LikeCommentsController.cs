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
    public class LikeCommentsController : ApiController
    {
        private AccessDB db = new AccessDB();

        // GET: api/LikeComments
        [EnableQuery(MaxExpansionDepth = 5)]
        public IQueryable<LikeComment> GetLikeComment()
        {
            return db.LikeComment;
        }

        // GET: api/LikeComments/5
        [ResponseType(typeof(LikeComment))]
        public IHttpActionResult GetLikeComment(int id)
        {
            LikeComment likeComment = db.LikeComment.Find(id);
            if (likeComment == null)
            {
                return NotFound();
            }

            return Ok(likeComment);
        }

        // PUT: api/LikeComments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLikeComment(int id, LikeComment likeComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != likeComment.Id)
            {
                return BadRequest();
            }

            db.Entry(likeComment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LikeCommentExists(id))
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

        // POST: api/LikeComments
        [ResponseType(typeof(LikeComment))]
        public IHttpActionResult PostLikeComment(LikeComment likeComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LikeComment.Add(likeComment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = likeComment.Id }, likeComment);
        }

        // DELETE: api/LikeComments/5
        [ResponseType(typeof(LikeComment))]
        public IHttpActionResult DeleteLikeComment(int id)
        {
            LikeComment likeComment = db.LikeComment.Find(id);
            if (likeComment == null)
            {
                return NotFound();
            }

            db.LikeComment.Remove(likeComment);
            db.SaveChanges();

            return Ok(likeComment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LikeCommentExists(int id)
        {
            return db.LikeComment.Count(e => e.Id == id) > 0;
        }
    }
}