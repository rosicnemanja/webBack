using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Web11.Models.Core
{
    public class SubForum
    {
        [Key]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [StringLength(450)]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public string Rules { get; set; }

        [ForeignKey("ResponsibleModerator")]
        [Index("IX_Key", 1, IsUnique = false)]
        public int? ResponsibleModerator_Id { get; set; }

        public User ResponsibleModerator { get; set; }

    }
}