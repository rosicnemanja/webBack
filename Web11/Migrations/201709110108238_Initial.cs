namespace Web11.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
               "dbo.Comments",
               c => new
               {
                   Id = c.Int(nullable: false, identity: true),
                   Theme_Id = c.Int(nullable: false),
                   Author_Id = c.Int(nullable: false),
                   TimeStamp = c.DateTime(nullable: false),
                   Content = c.String(),
                   Likes = c.Int(nullable: false),
                   Dislikes = c.Int(nullable: false),
                   Edited = c.Boolean(nullable: false),
                   ParentComment_Id = c.Int(),
               })
               .PrimaryKey(t => t.Id)
               .ForeignKey("dbo.Users", t => t.Author_Id, cascadeDelete: false)
               .ForeignKey("dbo.Comments", t => t.ParentComment_Id, cascadeDelete: false)
               .ForeignKey("dbo.Themes", t => t.Theme_Id, cascadeDelete: true)
               .Index(t => t.Theme_Id)
               .Index(t => t.Author_Id)
               .Index(t => t.ParentComment_Id);

            CreateTable(
                "dbo.Users",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Username = c.String(maxLength: 450),
                    Password = c.String(),
                    Name = c.String(),
                    LastName = c.String(),
                    Role = c.Int(nullable: false),
                    Phone = c.String(),
                    Email = c.String(),
                    RegistrationTime = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Username, unique: true);

            CreateTable(
                "dbo.Themes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    SubForum_Id = c.Int(nullable: false),
                    Title = c.String(maxLength: 450),
                    Author_Id = c.Int(nullable: false),
                    Text = c.String(),
                    Image = c.String(),
                    Link = c.String(),
                    CreationDate = c.DateTime(nullable: false),
                    Likes = c.Int(nullable: false),
                    Dislikes = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Author_Id, cascadeDelete: false)
                .ForeignKey("dbo.SubForums", t => t.SubForum_Id, cascadeDelete: true)
                .Index(t => t.SubForum_Id)
                .Index(t => t.Title, unique: true)
                .Index(t => t.Author_Id);

            CreateTable(
                "dbo.SubForums",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(maxLength: 450),
                    Description = c.String(),
                    Image = c.String(),
                    Rules = c.String(),
                    ResponsibleModerator_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ResponsibleModerator_Id)
                .Index(t => t.Name, unique: true)
                .Index(t => t.ResponsibleModerator_Id, name: "IX_Key");

            CreateTable(
                "dbo.ComplainComments",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    User_Id = c.Int(),
                    Comment_Id = c.Int(),
                    Text = c.String(),
                    Date = c.DateTime(nullable: false),
                    Author_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Author_Id, cascadeDelete: false)
                .ForeignKey("dbo.Comments", t => t.Comment_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => new { t.User_Id, t.Comment_Id, t.Author_Id }, unique: true, name: "IX_Key");

            CreateTable(
                "dbo.ComplainSubforums",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    User_Id = c.Int(),
                    Subforum_Id = c.Int(),
                    Text = c.String(),
                    Date = c.DateTime(nullable: false),
                    Author_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Author_Id, cascadeDelete: false)
                .ForeignKey("dbo.SubForums", t => t.Subforum_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => new { t.User_Id, t.Subforum_Id, t.Author_Id }, unique: true, name: "IX_Key");

            CreateTable(
                "dbo.ComplainThemes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    User_Id = c.Int(),
                    Theme_Id = c.Int(),
                    Text = c.String(),
                    Date = c.DateTime(nullable: false),
                    Author_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Author_Id, cascadeDelete: false)
                .ForeignKey("dbo.Themes", t => t.Theme_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => new { t.User_Id, t.Theme_Id, t.Author_Id }, unique: true, name: "IX_Key");

            CreateTable(
                "dbo.FollowSubForums",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    User_Id = c.Int(nullable: false),
                    SubForum_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SubForums", t => t.SubForum_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: false)
                .Index(t => new { t.User_Id, t.SubForum_Id }, unique: true, name: "IX_Key");

            CreateTable(
                "dbo.LikeComments",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    User_Id = c.Int(nullable: false),
                    Comment_Id = c.Int(),
                    IsLiked = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comments", t => t.Comment_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: false)
                .Index(t => new { t.User_Id, t.Comment_Id }, unique: true, name: "IX_Key");

            CreateTable(
                "dbo.LikeThemes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    User_Id = c.Int(nullable: false),
                    Theme_Id = c.Int(),
                    IsLiked = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Themes", t => t.Theme_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: false)
                .Index(t => new { t.User_Id, t.Theme_Id }, unique: true, name: "IX_Key");

            CreateTable(
                "dbo.Messages",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Sender_Id = c.Int(nullable: false),
                    Receiver_Id = c.Int(nullable: false),
                    Text = c.String(),
                    Readed = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Receiver_Id, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.Sender_Id, cascadeDelete: false)
                .Index(t => t.Sender_Id)
                .Index(t => t.Receiver_Id);

            CreateTable(
                "dbo.SavedComments",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    User_Id = c.Int(nullable: false),
                    Comment_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comments", t => t.Comment_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: false)
                .Index(t => t.User_Id)
                .Index(t => t.Comment_Id);

            CreateTable(
                "dbo.SavedThemes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    User_Id = c.Int(nullable: false),
                    Theme_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Themes", t => t.Theme_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: false)
                .Index(t => t.User_Id)
                .Index(t => t.Theme_Id);

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SavedThemes", "User_Id", "dbo.Users");
            DropForeignKey("dbo.SavedThemes", "Theme_Id", "dbo.Themes");
            DropForeignKey("dbo.SavedComments", "User_Id", "dbo.Users");
            DropForeignKey("dbo.SavedComments", "Comment_Id", "dbo.Comments");
            DropForeignKey("dbo.Messages", "Sender_Id", "dbo.Users");
            DropForeignKey("dbo.Messages", "Receiver_Id", "dbo.Users");
            DropForeignKey("dbo.LikeThemes", "User_Id", "dbo.Users");
            DropForeignKey("dbo.LikeThemes", "Theme_Id", "dbo.Themes");
            DropForeignKey("dbo.LikeComments", "User_Id", "dbo.Users");
            DropForeignKey("dbo.LikeComments", "Comment_Id", "dbo.Comments");
            DropForeignKey("dbo.FollowSubForums", "User_Id", "dbo.Users");
            DropForeignKey("dbo.FollowSubForums", "SubForum_Id", "dbo.SubForums");
            DropForeignKey("dbo.ComplainThemes", "User_Id", "dbo.Users");
            DropForeignKey("dbo.ComplainThemes", "Theme_Id", "dbo.Themes");
            DropForeignKey("dbo.ComplainThemes", "Author_Id", "dbo.Users");
            DropForeignKey("dbo.ComplainSubforums", "User_Id", "dbo.Users");
            DropForeignKey("dbo.ComplainSubforums", "Subforum_Id", "dbo.SubForums");
            DropForeignKey("dbo.ComplainSubforums", "Author_Id", "dbo.Users");
            DropForeignKey("dbo.ComplainComments", "User_Id", "dbo.Users");
            DropForeignKey("dbo.ComplainComments", "Comment_Id", "dbo.Comments");
            DropForeignKey("dbo.ComplainComments", "Author_Id", "dbo.Users");
            DropForeignKey("dbo.Comments", "Theme_Id", "dbo.Themes");
            DropForeignKey("dbo.Themes", "SubForum_Id", "dbo.SubForums");
            DropForeignKey("dbo.SubForums", "ResponsibleModerator_Id", "dbo.Users");
            DropForeignKey("dbo.Themes", "Author_Id", "dbo.Users");
            DropForeignKey("dbo.Comments", "ParentComment_Id", "dbo.Comments");
            DropForeignKey("dbo.Comments", "Author_Id", "dbo.Users");
            DropIndex("dbo.SavedThemes", new[] { "Theme_Id" });
            DropIndex("dbo.SavedThemes", new[] { "User_Id" });
            DropIndex("dbo.SavedComments", new[] { "Comment_Id" });
            DropIndex("dbo.SavedComments", new[] { "User_Id" });
            DropIndex("dbo.Messages", new[] { "Receiver_Id" });
            DropIndex("dbo.Messages", new[] { "Sender_Id" });
            DropIndex("dbo.LikeThemes", "IX_Key");
            DropIndex("dbo.LikeComments", "IX_Key");
            DropIndex("dbo.FollowSubForums", "IX_Key");
            DropIndex("dbo.ComplainThemes", "IX_Key");
            DropIndex("dbo.ComplainSubforums", "IX_Key");
            DropIndex("dbo.ComplainComments", "IX_Key");
            DropIndex("dbo.SubForums", "IX_Key");
            DropIndex("dbo.SubForums", new[] { "Name" });
            DropIndex("dbo.Themes", new[] { "Author_Id" });
            DropIndex("dbo.Themes", new[] { "Title" });
            DropIndex("dbo.Themes", new[] { "SubForum_Id" });
            DropIndex("dbo.Users", new[] { "Username" });
            DropIndex("dbo.Comments", new[] { "ParentComment_Id" });
            DropIndex("dbo.Comments", new[] { "Author_Id" });
            DropIndex("dbo.Comments", new[] { "Theme_Id" });
            DropTable("dbo.SavedThemes");
            DropTable("dbo.SavedComments");
            DropTable("dbo.Messages");
            DropTable("dbo.LikeThemes");
            DropTable("dbo.LikeComments");
            DropTable("dbo.FollowSubForums");
            DropTable("dbo.ComplainThemes");
            DropTable("dbo.ComplainSubforums");
            DropTable("dbo.ComplainComments");
            DropTable("dbo.SubForums");
            DropTable("dbo.Themes");
            DropTable("dbo.Users");
            DropTable("dbo.Comments");
        }
    }
}
