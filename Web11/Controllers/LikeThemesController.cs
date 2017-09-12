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
    public class LikeThemesController : ApiController
    {
        private AccessDB db = new AccessDB();

        // GET: api/LikeThemes
        [EnableQuery(MaxExpansionDepth = 5)]
        public IQueryable<LikeTheme> GetLikeThemes()
        {
            return db.LikeThemes;
        }

        // GET: api/LikeThemes/5
        [ResponseType(typeof(LikeTheme))]
        public IHttpActionResult GetLikeTheme(int id)
        {
            LikeTheme likeTheme = db.LikeThemes.Find(id);
            if (likeTheme == null)
            {
                return NotFound();
            }

            return Ok(likeTheme);
        }

        // PUT: api/LikeThemes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLikeTheme(int id, LikeTheme likeTheme)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != likeTheme.Id)
            {
                return BadRequest();
            }

            db.Entry(likeTheme).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LikeThemeExists(id))
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

        // POST: api/LikeThemes
        [ResponseType(typeof(LikeTheme))]
        public IHttpActionResult PostLikeTheme(LikeTheme likeTheme)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LikeThemes.Add(likeTheme);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = likeTheme.Id }, likeTheme);
        }

        // DELETE: api/LikeThemes/5
        [ResponseType(typeof(LikeTheme))]
        public IHttpActionResult DeleteLikeTheme(int id)
        {
            LikeTheme likeTheme = db.LikeThemes.Find(id);
            if (likeTheme == null)
            {
                return NotFound();
            }

            db.LikeThemes.Remove(likeTheme);
            db.SaveChanges();

            return Ok(likeTheme);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LikeThemeExists(int id)
        {
            return db.LikeThemes.Count(e => e.Id == id) > 0;
        }
    }
}