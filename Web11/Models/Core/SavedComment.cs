using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web11.Models.Core
{
    public class SavedComment
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int User_Id { get; set; }

        public User User { get; set; }

        [ForeignKey("Comment")]
        public int Comment_Id { get; set; }

        public Comment Comment { get; set; }
    }
}