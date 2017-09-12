using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web11.Models.Core
{
    public class ComplainSubforum
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        [Index("IX_Key", 1, IsUnique = true)]
        public int? User_Id { get; set; }

        public User User { get; set; }

        [ForeignKey("Subforum")]
        [Index("IX_Key", 2, IsUnique = true)]
        public int? Subforum_Id { get; set; }

        public SubForum Subforum { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey("Author")]
        [Index("IX_Key", 3, IsUnique = true)]
        public int Author_Id { get; set; }

        public User Author { get; set; }


    }
}