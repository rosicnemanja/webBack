using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Web11.Models.Core;

namespace Web11.Models
{
    public class AccessDB : DbContext
    {
        public virtual DbSet<User> Users { get; set; }        
        public virtual DbSet<Theme> Themes { get; set; }
        public virtual DbSet<SubForum> SubForums { get; set; }
        public virtual DbSet<SavedTheme> SavedThemes { get; set; }
        public virtual DbSet<SavedComment> SavedComments { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<FollowSubForum> FollowSubForums { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<LikeTheme> LikeThemes { get; set; }
        public virtual DbSet<LikeComment> LikeComment { get; set; }
        public virtual DbSet<ComplainComment> ComplainComment { get; set; }
        public virtual DbSet<ComplainSubforum> ComplainSubforum { get; set; }
        public virtual DbSet<ComplainTheme> ComplainTheme { get; set; }

        public AccessDB() : base("MyDatabase1")
        {
            this.Configuration.ProxyCreationEnabled = false; // ADD THIS LINE !            
        }
    }
}