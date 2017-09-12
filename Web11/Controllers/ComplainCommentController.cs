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
    public class ComplainCommentController : ApiController
    {
        private AccessDB db = new AccessDB();

        [EnableQuery(MaxExpansionDepth = 4)]
        // GET: api/ComplainsComment
        public IQueryable<ComplainComment> GetComplainsComment()
        {
            return db.ComplainComment;
        }

        [EnableQuery]
        // GET: api/ComplainsComment/5
        [ResponseType(typeof(ComplainComment))]
        public IHttpActionResult GetComplainComment(int id)
        {
            ComplainComment complainComment = db.ComplainComment.Find(id);
            if (complainComment == null)
            {
                return NotFound();
            }

            return Ok(complainComment);
        }
        // PUT: api/ComplainsComment/5
        [ResponseType(typeof(Comment))]
        public IHttpActionResult PutComplainComment(int id, ComplainComment complaintComment)
        {
            if (complaintComment == null)
            {
                return NotFound();
            }

            Message msg = new Message();
            msg.Receiver_Id = complaintComment.User.Id;
            msg.Sender_Id = complaintComment.Comment.Theme.SubForum.ResponsibleModerator.Id;
            msg.Text = "Your complaint for comment \"" + complaintComment.Comment.Content + "\" with complaint \"" + complaintComment.Text + "\"  has been accepted!";

            Message msg1 = new Message();
            msg1.Receiver_Id = complaintComment.Comment.Author_Id;
            msg1.Sender_Id = complaintComment.Comment.Theme.SubForum.ResponsibleModerator.Id;
            msg1.Text = "Your comment \"" + complaintComment.Comment.Content + "\" has been deleted because of complaint \"" + complaintComment.Text + "\"";
            
            db.Messages.Add(msg);
            db.Messages.Add(msg1);
            //ComplainComment comComment = db.ComplainComment.Find(complaintComment.Id);
            //db.ComplainComment.Remove(comComment);
            int commentId = complaintComment.Comment.Id;
            Comment commentToDelete = db.Comments.Find(commentId);
            db.Comments.Remove(commentToDelete);
            db.SaveChanges();

            return Ok(complaintComment.Comment);
        }

        // POST: api/ComplainsComment
        [ResponseType(typeof(ComplainComment))]
        public IHttpActionResult PostComplainComment(ComplainComment complainComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ComplainComment cc = new ComplainComment();
            cc.Author_Id = complainComment.Author.Id;
            cc.Comment_Id = complainComment.Comment.Id;
            cc.Date = DateTime.Now;
            cc.Text = complainComment.Text;
            cc.User_Id = complainComment.User.Id;

            var allUsers = db.Users;
            var allSubforums = db.SubForums;
            SubForum subforum = new SubForum();
            foreach(SubForum sub in allSubforums)
            {
                if (sub.Id == complainComment.Comment.Theme.SubForum_Id) {
                    subforum = sub;
                    break;
                }
            }
            foreach (User user in allUsers)
            {
                if (user.Role == Role.Admin || subforum.ResponsibleModerator_Id == user.Id)
                {
                    Message msg = new Message();
                    msg.Receiver_Id = user.Id;
                    msg.Sender_Id = complainComment.User.Id;
                    msg.Text = "There is a complain for comment \"" + complainComment.Comment.Content + "\" complaint text: " + complainComment.Text;
                    db.Messages.Add(msg);
                }
            }
            
            db.ComplainComment.Add(cc);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = complainComment.Id }, complainComment);
        }

        // DELETE: api/ComplainsComment/5
        [ResponseType(typeof(ComplainComment))]
        public IHttpActionResult DeleteComplainComment(int id)
        {
            ComplainComment complainComment = db.ComplainComment.Find(id);
            if (complainComment == null)
            {
                return NotFound();
            }

            User user = db.Users.Find(complainComment.User_Id);
            Comment comment = db.Comments.Find(complainComment.Comment_Id);
            Theme theme = db.Themes.Find(comment.Theme_Id);
            SubForum subforum = db.SubForums.Find(theme.SubForum_Id);
            User responsibleModerator = db.Users.Find(subforum.ResponsibleModerator_Id);

            Message msg = new Message();
            msg.Receiver_Id = user.Id;
            msg.Sender_Id = responsibleModerator.Id;
            msg.Text = "Your complaint for comment \"" + comment.Content + "\" with complaint \"" + complainComment.Text + "\"  has been rejected!";

            db.Messages.Add(msg);
            db.ComplainComment.Remove(complainComment);
            db.SaveChanges();

            return Ok(complainComment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private bool ComplainCommentExists(int id)
        {
            return db.ComplainComment.Count(e => e.Id == id) > 0;
        }
    }
}
