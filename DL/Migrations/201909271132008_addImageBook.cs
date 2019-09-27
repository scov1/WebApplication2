namespace DL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addImageBook : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "GenreId", c => c.Int(nullable: false));
            AddColumn("dbo.Books", "ImageData", c => c.Binary());
            CreateIndex("dbo.Books", "GenreId");
            AddForeignKey("dbo.Books", "GenreId", "dbo.Genres", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "GenreId", "dbo.Genres");
            DropIndex("dbo.Books", new[] { "GenreId" });
            DropColumn("dbo.Books", "ImageData");
            DropColumn("dbo.Books", "GenreId");
        }
    }
}
