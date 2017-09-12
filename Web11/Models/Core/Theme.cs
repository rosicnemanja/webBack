using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Web11.Models.Core
{
    public class Theme
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("SubForum")]
        public int SubForum_Id { get; set; }

        public SubForum SubForum { get; set; }

        [Index(IsUnique = true)]
        [StringLength(450)]
        public string Title { get; set; }

        [ForeignKey("Author")]
        public int Author_Id { get; set; }

        public User Author { get; set; }
        
        public string Text { get; set; }

        public string Image { get; set; }

        public string Link { get; set; }

        public DateTime CreationDate { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

    }
}