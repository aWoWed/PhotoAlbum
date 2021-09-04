namespace Photo_album.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewLikesMigration : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Likes", name: "CommentId", newName: "Comment_Id");
            RenameIndex(table: "dbo.Likes", name: "IX_CommentId", newName: "IX_Comment_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Likes", name: "IX_Comment_Id", newName: "IX_CommentId");
            RenameColumn(table: "dbo.Likes", name: "Comment_Id", newName: "CommentId");
        }
    }
}
