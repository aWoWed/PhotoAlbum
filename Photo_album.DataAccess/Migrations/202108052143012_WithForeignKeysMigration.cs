namespace Photo_album.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WithForeignKeysMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "PostId", "dbo.Posts");
            DropPrimaryKey("dbo.Comments");
            DropPrimaryKey("dbo.Posts");
            AlterColumn("dbo.Comments", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Comments", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Comments", "PostId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Posts", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Posts", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Comments", "Id");
            AddPrimaryKey("dbo.Posts", "Id");
            CreateIndex("dbo.Comments", "UserId");
            CreateIndex("dbo.Comments", "PostId");
            CreateIndex("dbo.Posts", "UserId");
            AddForeignKey("dbo.Comments", "PostId", "dbo.Posts", "Id");
            AddForeignKey("dbo.Comments", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Posts", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id");
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Posts", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "PostId", "dbo.Posts");
            DropIndex("dbo.Posts", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "PostId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropPrimaryKey("dbo.Posts");
            DropPrimaryKey("dbo.Comments");
            AlterColumn("dbo.Posts", "UserId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Posts", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Comments", "PostId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Comments", "UserId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Comments", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Posts", "Id");
            AddPrimaryKey("dbo.Comments", "Id");
            AddForeignKey("dbo.Comments", "PostId", "dbo.Posts", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id", cascadeDelete: true);
        }
    }
}
