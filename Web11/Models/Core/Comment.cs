using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web11.Models.Core
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Theme")]
        public int Theme_Id { get; set; }

        public Theme Theme { get; set; }

        [ForeignKey("Author")]
        public int Author_Id { get; set; }

        public User Author { get; set; }

        public DateTime TimeStamp { get; set; }

        public Comment ParentComment { get; set; }

        public string Content { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public bool Edited { get; set; }
    }
}