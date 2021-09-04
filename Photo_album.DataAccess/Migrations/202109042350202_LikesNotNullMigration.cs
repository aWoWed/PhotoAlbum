namespace Photo_album.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class LikesNotNullMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Likes", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Likes", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Likes", new[] { "UserId" });
            DropIndex("dbo.Likes", new[] { "PostId" });
            AlterColumn("dbo.Likes", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Likes", "PostId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Likes", "UserId");
            CreateIndex("dbo.Likes", "PostId");
            AddForeignKey("dbo.Likes", "PostId", "dbo.Posts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Likes", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Likes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Likes", "PostId", "dbo.Posts");
            DropIndex("dbo.Likes", new[] { "PostId" });
            DropIndex("dbo.Likes", new[] { "UserId" });
            AlterColumn("dbo.Likes", "PostId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Likes", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Likes", "PostId");
            CreateIndex("dbo.Likes", "UserId");
            AddForeignKey("dbo.Likes", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Likes", "PostId", "dbo.Posts", "Id");
        }
    }
}
