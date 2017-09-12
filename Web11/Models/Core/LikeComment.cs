﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web11.Models.Core
{
    public class LikeComment
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        [Index("IX_Key", 1, IsUnique = true)]
        public int User_Id { get; set; }

        public User User { get; set; }

        [ForeignKey("Comment")]
        [Index("IX_Key", 2, IsUnique = true)]
        public int? Comment_Id { get; set; }

        public Comment Comment { get; set; }

        public bool IsLiked { get; set; }
    }
}