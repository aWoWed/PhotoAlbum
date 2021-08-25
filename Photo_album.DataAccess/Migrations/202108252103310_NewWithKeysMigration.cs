namespace Photo_album.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewWithKeysMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "Image", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "Image", c => c.String());
        }
    }
}
