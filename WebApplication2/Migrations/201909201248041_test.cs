namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            //CreateIndex("dbo.Orders", "BookId");
            //CreateIndex("dbo.Orders", "UserId");
            //AddForeignKey("dbo.Orders", "BookId", "dbo.Books", "Id", cascadeDelete: true);
            //AddForeignKey("dbo.Orders", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
            //DropForeignKey("dbo.Orders", "BookId", "dbo.Books");
            //DropIndex("dbo.Orders", new[] { "UserId" });
            //DropIndex("dbo.Orders", new[] { "BookId" });
        }
    }
}
