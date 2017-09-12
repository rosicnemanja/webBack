using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web11.Models.Core
{
    public class SavedTheme
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int User_Id { get; set; }

        public User User { get; set; }

        [ForeignKey("Theme")]
        public int Theme_Id { get; set; }

        public Theme Theme { get; set; }
    }
}