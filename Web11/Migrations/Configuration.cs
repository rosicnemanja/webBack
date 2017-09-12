namespace Web11.Migrations
{
    using Models.Core;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Web11.Models.AccessDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Web11.Models.AccessDB context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            #region users
            User user1 = new User();
            user1.Name = "Nikola";
            user1.LastName = "Skoric";
            user1.Password = "02011994";
            user1.Phone = "0642993955";
            user1.RegistrationTime = DateTime.Now;
            user1.Role = Role.Admin;
            user1.Username = "skoricnik";
            user1.Email = "skore@gmail.com";

            User user2 = new User();
            user2.Name = "Mihael";
            user2.LastName = "Farkas";
            user2.Password = "12345";
            user2.Phone = "0632716283";
            user2.RegistrationTime = DateTime.Now;
            user2.Role = Role.Admin;
            user2.Username = "farkas";
            user2.Email = "farkas@gmail.com";

            User user3 = new User();
            user3.Name = "Nemanja";
            user3.LastName = "Rosic";
            user3.Password = "12345";
            user3.Phone = "061221832";
            user3.RegistrationTime = DateTime.Now;
            user3.Role = Role.Admin;
            user3.Username = "rosic";
            user3.Email = "rosic@gmail.com";

            User user4 = new User();
            user4.Name = "Aleksandar";
            user4.LastName = "Dudukovic";
            user4.Password = "12345";
            user4.Phone = "066123122";
            user4.RegistrationTime = DateTime.Now;
            user4.Role = Role.Admin;
            user4.Username = "duduk";
            user4.Email = "duduk@gmail.com";

            #endregion

            #region SUBFORUMS
            SubForum s1 = new SubForum();
            s1.Name = "Music";
            s1.ResponsibleModerator = user1;
            s1.Image = "https://image.freepik.com/free-icon/black-simple-music-note-vector_318-10095.jpg";
            s1.Description = "Music u like will be here";
            s1.Rules = "Be good";

            SubForum s2 = new SubForum();
            s2.Name = "Videos";
            s2.ResponsibleModerator = user2;
            s2.Image = "http://www.steppinoutband.net/wp-content/uploads/2015/03/media_video_icon_pc_800_clr_4466.png";
            s2.Description = "Funny videos";
            s2.Rules = "Quite";

            SubForum s3 = new SubForum();
            s3.Name = "Photos";
            s3.ResponsibleModerator = user3;
            s3.Image = "http://images.clipartpanda.com/camera-clipart-png-KineKyLXT.png";
            s3.Description = "PhotoBomb";
            s3.Rules = "Pirate";

            #endregion

            #region THEMES
            Theme t1 = new Theme();
            t1.Author = user1;
            t1.CreationDate = DateTime.Now;
            t1.Dislikes = 10;
            t1.Likes = 20;
            t1.Text = "Theme 1 text";
            t1.Title = "Theme 1";
            t1.SubForum = s1;

            Theme t2 = new Theme();
            t2.Author = user2;
            t2.CreationDate = DateTime.Now;
            t2.Dislikes = 12;
            t2.Likes = 21;
            t2.Text = "Theme 2 text";
            t2.Title = "Theme 2";
            t2.SubForum = s1;

            Theme t3 = new Theme();
            t3.Author = user3;
            t3.CreationDate = DateTime.Now;
            t3.Dislikes = 10;
            t3.Likes = 20;
            t3.Text = "Theme 3 text";
            t3.Title = "Theme 3";
            t3.SubForum = s2;

            Theme t4 = new Theme();
            t4.Author = user4;
            t4.CreationDate = DateTime.Now;
            t4.Dislikes = 11;
            t4.Likes = 210;
            t4.Text = "Theme 4 text";
            t4.Title = "Theme 4";
            t4.SubForum = s3;
            #endregion

            #region COMMENTS
            Comment c1 = new Comment();
            c1.Author = user1;
            c1.Content = "Hey, this is great!";
            c1.Dislikes = 22;
            c1.Likes = 21;
            c1.TimeStamp = DateTime.Now;
            c1.Edited = false;
            c1.Theme = t1;

            Comment c2 = new Comment();
            c2.Author = user2;
            c2.Content = "No, this is awesome!";
            c2.Dislikes = 212;
            c2.Likes = 1;
            c2.TimeStamp = DateTime.Now;
            c2.Edited = false;
            c2.ParentComment = c1;
            c2.Theme = t1;

            Comment c3 = new Comment();
            c3.Author = user3;
            c3.Content = "Great stuff!";
            c3.Dislikes = 2;
            c3.Likes = 11;
            c3.TimeStamp = DateTime.Now;
            c3.Edited = false;
            c3.Theme = t2;

            Comment c4 = new Comment();
            c4.Author = user4;
            c4.Content = "No way!";
            c4.Dislikes = 21;
            c4.Likes = 19;
            c4.TimeStamp = DateTime.Now;
            c4.Edited = false;
            c4.Theme = t3;


            #endregion

            #region FOLLOWS
            FollowSubForum f1 = new FollowSubForum();
            f1.SubForum = s1;
            f1.User = user1;

            FollowSubForum f2 = new FollowSubForum();
            f2.SubForum = s2;
            f2.User = user1;

            FollowSubForum f3 = new FollowSubForum();
            f3.SubForum = s2;
            f3.User = user2;

            FollowSubForum f4 = new FollowSubForum();
            f4.SubForum = s2;
            f4.User = user3;

            #endregion
            #region complains
            //ComplainComment cc = new ComplainComment();
            //cc.Author = user2;
            //cc.Comment = c2;
            //cc.Date = DateTime.Now;
            //cc.Text = "Zalba na komentar!";
            //cc.User = user1;

            //ComplainSubforum cs = new ComplainSubforum();
            //cs.Author = user1;
            //cs.Subforum = s3;
            //cs.Date = DateTime.Now;
            //cs.Text = "Zalba na subforum!";
            //cs.User = user2;

            //ComplainTheme ct = new ComplainTheme();
            //ct.Author = user2;
            //ct.Date = DateTime.Now;
            //ct.Text = "Zalba na temu!";
            //ct.Theme = t2;
            //ct.User = user1;
           
            #endregion


            try
            {
                context.Users.Add(user1);
                context.Users.Add(user2);
                context.Users.Add(user3);
                context.Users.Add(user4);
                context.SubForums.Add(s1);
                context.SubForums.Add(s2);
                context.SubForums.Add(s3);
                context.Themes.Add(t1);
                context.Themes.Add(t2);
                context.Themes.Add(t3);
                context.Themes.Add(t4);
                context.Comments.Add(c1);
                context.Comments.Add(c2);
                context.Comments.Add(c3);
                context.Comments.Add(c4);
                context.FollowSubForums.Add(f1);
                context.FollowSubForums.Add(f2);
                context.FollowSubForums.Add(f3);
                context.FollowSubForums.Add(f4);
                //context.ComplainComment.Add(cc);
                //context.ComplainSubforum.Add(cs);
                //context.ComplainTheme.Add(ct);
                context.SaveChanges();

            }
            catch (Exception e)
            {

            }
        }
    }
}
