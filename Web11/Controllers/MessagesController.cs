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
    public class MessagesController : ApiController
    {
        private AccessDB db = new AccessDB();
        
        [EnableQuery]
        // GET: api/Messages
        public IQueryable<Message> GetMessages()
        {
            return db.Messages;
        }

        [EnableQuery]
        // GET: api/Messages/5
        [ResponseType(typeof(Message))]
        public IHttpActionResult GetMessage(int id)
        {
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return NotFound();
            }

            return Ok(message);
        }

        // PUT: api/Messages/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMessage(int id, Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Message msg = new Message();
            msg.Receiver_Id = message.Receiver.Id;
            msg.Sender_Id = id;
            msg.Text = message.Text;

            Message msg1 = new Message();
            msg1.Receiver_Id = message.Sender.Id;
            msg1.Sender_Id = id;
            msg1.Text = message.Text;

            db.Messages.Add(msg);
            db.Messages.Add(msg1);
            db.SaveChanges();
            return Ok();
        }

        // POST: api/Messages
        [ResponseType(typeof(Message))]
        public IHttpActionResult PostMessage(Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Messages.Add(message);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = message.Id }, message);
        }

        // DELETE: api/Messages/5
        [ResponseType(typeof(Message))]
        public IHttpActionResult DeleteMessage(int id)
        {
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return NotFound();
            }

            db.Messages.Remove(message);
            db.SaveChanges();

            return Ok(message);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MessageExists(int id)
        {
            return db.Messages.Count(e => e.Id == id) > 0;
        }
    }
}