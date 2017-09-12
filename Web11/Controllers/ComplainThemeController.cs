using System;
using System.Collections.Generic;
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
    public class ComplainThemeController : ApiController
    {
        private AccessDB db = new AccessDB();

        [EnableQuery(MaxExpansionDepth = 4)]
        // GET: api/ComplainsTheme
        public IQueryable<ComplainTheme> GetComplainsTheme()
        {
            return db.ComplainTheme;
        }

        [EnableQuery]
        // GET: api/ComplainsTheme/5
        [ResponseType(typeof(ComplainTheme))]
        public IHttpActionResult GetComplainTheme(int id)
        {
            ComplainTheme complainTheme = db.ComplainTheme.Find(id);
            if (complainTheme == null)
            {
                return NotFound();
            }

            return Ok(complainTheme);
        }
        // PUT: api/ComplainsTheme/5
        [ResponseType(typeof(Theme))]
        public IHttpActionResult PutComplainTheme(int id, ComplainTheme complainTheme)
        {
            if (complainTheme == null)
            {
                return NotFound();
            }

            Message msg = new Message();
            msg.Receiver_Id = complainTheme.User.Id;
            msg.Sender_Id = complainTheme.Theme.SubForum.ResponsibleModerator.Id;
            msg.Text = "Your complaint for comment \"" + complainTheme.Theme.Title + "\" with complaint \"" + complainTheme.Text + "\"  has been accepted!";

            Message msg1 = new Message();
            msg1.Receiver_Id = complainTheme.Theme.Author_Id;
            msg1.Sender_Id = complainTheme.Theme.SubForum.ResponsibleModerator.Id;
            msg1.Text = "Your theme \"" + complainTheme.Theme.Title + "\" has been deleted because of complaint \"" + complainTheme.Text + "\"";
            int themeId = complainTheme.Theme.Id;
            db.Messages.Add(msg);
            db.Messages.Add(msg1);
            Theme themeToDelete = db.Themes.Find(themeId);

            //var comments = db.Comments;
            //foreach(Comment com in comments)
            //{
            //    if (com.Theme_Id == themeToDelete.Id)
            //    {
            //        var commentComplaints = db.ComplainComment;
            //        foreach(ComplainComment cc in commentComplaints)
            //        {
            //            if (cc.Comment_Id == com.Id)
            //            {
            //                db.ComplainComment.Remove(cc);
            //            }
            //        }
            //        db.Comments.Remove(com);
            //    }
            //}

            //var themeComplaints = db.ComplainTheme;
            //foreach(ComplainTheme ct in themeComplaints)
            //{
            //    if (ct.Theme_Id == themeToDelete.Id)
            //    {
            //        db.ComplainTheme.Remove(ct);
            //    }
            //}

            db.Themes.Remove(themeToDelete);
            db.SaveChanges();

            return Ok(complainTheme.Theme);
        }

        // POST: api/ComplainsTheme
        [ResponseType(typeof(ComplainTheme))]
        public IHttpActionResult PostComplainTheme(ComplainTheme complainTheme)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ComplainTheme ct = new ComplainTheme();
            ct.Author_Id = complainTheme.Author.Id;
            ct.Date = DateTime.Now;
            ct.Text = complainTheme.Text;
            ct.Theme_Id = complainTheme.Theme.Id;
            ct.User_Id = complainTheme.User.Id;

            var allUsers = db.Users;
            foreach (User user in allUsers)
            {
                if (user.Role == Role.Admin || complainTheme.Theme.SubForum.ResponsibleModerator_Id == user.Id )
                {
                    Message msg = new Message();
                    msg.Receiver_Id = user.Id;
                    msg.Sender_Id = complainTheme.User.Id;
                    msg.Text = "There is a complain for theme with title \"" + complainTheme.Theme.Title + "\", complaint text:" + complainTheme.Text;
                    db.Messages.Add(msg);
                }
            }

            db.ComplainTheme.Add(ct);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = complainTheme.Id }, complainTheme);
        }

        // DELETE: api/ComplainsTheme/5
        [ResponseType(typeof(ComplainTheme))]
        public IHttpActionResult DeleteComplainTheme(int id)
        {
            ComplainTheme complaintTheme = db.ComplainTheme.Find(id);
            if (complaintTheme == null)
            {
                return NotFound();
            }

            User user = db.Users.Find(complaintTheme.User_Id);
            Theme theme = db.Themes.Find(complaintTheme.Theme_Id);
            SubForum subforum = db.SubForums.Find(theme.SubForum_Id);
            User responsibleModerator = db.Users.Find(subforum.ResponsibleModerator_Id);

            Message msg = new Message();
            msg.Receiver_Id = user.Id;
            msg.Sender_Id = responsibleModerator.Id;
            msg.Text = "Your complaint for theme \"" + theme.Title + "\" with complaint \"" + complaintTheme.Text + "\"  has been rejected!";

            db.Messages.Add(msg);
            db.ComplainTheme.Remove(complaintTheme);
            db.SaveChanges();

            return Ok(complaintTheme);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private bool ComplainThemeExists(int id)
        {
            return db.ComplainTheme.Count(e => e.Id == id) > 0;
        }
    }
}
