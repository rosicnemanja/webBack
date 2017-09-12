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
    public class SavedThemesController : ApiController
    {
        private AccessDB db = new AccessDB();

        [EnableQuery]
        // GET: api/SavedThemes
        public IQueryable<SavedTheme> GetSavedThemes()
        {
            return db.SavedThemes;
        }

        [EnableQuery]
        // GET: api/SavedThemes/5
        [ResponseType(typeof(SavedTheme))]
        public IHttpActionResult GetSavedTheme(int id)
        {
            SavedTheme savedTheme = db.SavedThemes.Find(id);
            if (savedTheme == null)
            {
                return NotFound();
            }

            return Ok(savedTheme);
        }

        // PUT: api/SavedThemes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSavedTheme(int id, SavedTheme savedTheme)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != savedTheme.Id)
            {
                return BadRequest();
            }

            db.Entry(savedTheme).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SavedThemeExists(id))
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

        // POST: api/SavedThemes
        [ResponseType(typeof(SavedTheme))]
        public IHttpActionResult PostSavedTheme(SavedTheme savedTheme)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SavedThemes.Add(savedTheme);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = savedTheme.Id }, savedTheme);
        }

        // DELETE: api/SavedThemes/5
        [ResponseType(typeof(SavedTheme))]
        public IHttpActionResult DeleteSavedTheme(int id)
        {
            SavedTheme savedTheme = db.SavedThemes.Find(id);
            if (savedTheme == null)
            {
                return NotFound();
            }

            db.SavedThemes.Remove(savedTheme);
            db.SaveChanges();

            return Ok(savedTheme);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SavedThemeExists(int id)
        {
            return db.SavedThemes.Count(e => e.Id == id) > 0;
        }
    }
}