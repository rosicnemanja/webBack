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
    public class ComplainSubforumController : ApiController
    {
        private AccessDB db = new AccessDB();

        [EnableQuery]
        // GET: api/ComplainsSubforum
        public IQueryable<ComplainSubforum> GetComplainsSubforum()
        {
            return db.ComplainSubforum;
        }

        [EnableQuery]
        // GET: api/ComplainsSubforum/5
        [ResponseType(typeof(ComplainSubforum))]
        public IHttpActionResult GetComplainSubforum(int id)
        {
            ComplainSubforum complainSubforum = db.ComplainSubforum.Find(id);
            if (complainSubforum == null)
            {
                return NotFound();
            }

            return Ok(complainSubforum);
        }
        // PUT: api/ComplainsSubforum/5
        [ResponseType(typeof(SubForum))]
        public IHttpActionResult PutComplainSubforum(int id, ComplainSubforum complainSubforum)
        {
            if (complainSubforum == null)
            {
                return NotFound();
            }

            Message msg = new Message();
            msg.Receiver_Id = complainSubforum.User.Id;
            msg.Sender_Id = complainSubforum.Subforum.ResponsibleModerator.Id;
            msg.Text = "Your complaint for subforum \"" + complainSubforum.Subforum.Name + "\" with complaint \"" + complainSubforum.Text + "\"  has been accepted!";

            Message msg1 = new Message();
            msg1.Receiver_Id = complainSubforum.Subforum.ResponsibleModerator.Id;
            msg1.Sender_Id = complainSubforum.Subforum.ResponsibleModerator.Id;
            msg1.Text = "Your subforum \"" + complainSubforum.Subforum.Name + "\" has been deleted because of complaint \"" + complainSubforum.Text + "\"";
            int subforumId = complainSubforum.Subforum.Id;
            db.Messages.Add(msg);
            db.Messages.Add(msg1);
           
            SubForum subforumToDelete = db.SubForums.Find(subforumId);
            //Theme themesComment = new Theme();
            //var comments = db.Comments;
            //var themes = db.Themes;
            //var commentComplaints = db.ComplainComment;
            //var themeComplaints = db.ComplainTheme;
            //foreach (Theme theme in themes)
            //{
            //    if(theme.SubForum_Id == subforumToDelete.Id)
            //    {
            //        foreach (Comment com in comments)
            //        {
            //            if (com.Theme_Id == theme.Id)
            //            {
            //                foreach (ComplainComment cc in commentComplaints)
            //                {
            //                    if (cc.Comment_Id == com.Id)
            //                    {
            //                        db.ComplainComment.Remove(cc);
            //                    }
            //                }
            //                db.Comments.Remove(com);
            //            }
            //        }
                
            //        foreach (ComplainTheme ct in themeComplaints)
            //        {
            //            if (ct.Theme_Id == theme.Id)
            //            {
            //                db.ComplainTheme.Remove(ct);
            //            }
            //        }
            //        db.Themes.Remove(theme);
            //    }
            //}

            //var subforumComplaints = db.ComplainSubforum;
            //foreach(ComplainSubforum cs in subforumComplaints)
            //{
            //    if(cs.Subforum_Id == subforumToDelete.Id)
            //    {
            //        db.ComplainSubforum.Remove(cs);
            //    }
            //}

            db.SubForums.Remove(subforumToDelete);
            db.SaveChanges();
            return Ok(complainSubforum.Subforum);
        }

        // POST: api/ComplainsSubforum
        [ResponseType(typeof(ComplainSubforum))]
        public IHttpActionResult PostComplainSubforum(ComplainSubforum complainSubforum)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ComplainSubforum cs = new ComplainSubforum();
            cs.Author_Id = complainSubforum.Author.Id;
            cs.Date = DateTime.Now;
            cs.Subforum_Id = complainSubforum.Subforum.Id;
            cs.Text = complainSubforum.Text;
            cs.User_Id = complainSubforum.User.Id;
            var allUsers = db.Users;
            foreach(User user in allUsers)
            {
                if (user.Role == Role.Admin) {
                    Message msg = new Message();
                    msg.Receiver_Id = user.Id;
                    msg.Sender_Id = complainSubforum.User.Id;
                    msg.Text = "There is a complain for subforum with name: \"" + complainSubforum.Subforum.Name + "\", complaint text:" + complainSubforum.Text;
                    db.Messages.Add(msg);
                }
            }
            //Message msg = new Message();
            //msg.Receiver_Id = complainSubforum.Author.Id;
            //msg.Sender_Id = complainSubforum.User.Id;
            //msg.Text = "You have got an complain for subforum with name: \"" + complainSubforum.Subforum.Name + "\", complaint text:" + complainSubforum.Text;

            db.ComplainSubforum.Add(cs);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = complainSubforum.Id }, complainSubforum);
        }

        // DELETE: api/ComplainsSubforum/5
        [ResponseType(typeof(ComplainSubforum))]
        public IHttpActionResult DeleteComplainSubforum(int id)
        {
            ComplainSubforum complaintSubforum = db.ComplainSubforum.Find(id);
            if (complaintSubforum == null)
            {
                return NotFound();
            }

            User user = db.Users.Find(complaintSubforum.User_Id);
            SubForum subforum = db.SubForums.Find(complaintSubforum.Subforum_Id);
            User responsibleModerator = db.Users.Find(subforum.ResponsibleModerator_Id);

            Message msg = new Message();
            msg.Receiver_Id = user.Id;
            msg.Sender_Id = responsibleModerator.Id;
            msg.Text = "Your complaint for subforum \"" + subforum.Name + "\" with complaint \"" + complaintSubforum.Text + "\"  has been rejected!";

            db.Messages.Add(msg);
            db.ComplainSubforum.Remove(complaintSubforum);
            db.SaveChanges();

            return Ok(complaintSubforum);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private bool ComplainSubforumExists(int id)
        {
            return db.ComplainSubforum.Count(e => e.Id == id) > 0;
        }
    }
}
