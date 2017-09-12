using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web11.Models.Core
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Sender")]
        public int Sender_Id { get; set; }

        public User Sender { get; set; }

        [ForeignKey("Receiver")]
        public int Receiver_Id { get; set; }

        public User Receiver { get; set; }

        public string Text { get; set; }

        public bool Readed { get; set; }
    }
}