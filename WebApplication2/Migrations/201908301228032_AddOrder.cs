namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
              "dbo.Orders",
              c => new
              {
                  Id = c.Int(nullable: false, identity: true),
                  userId = c.Int(nullable: false),
                  bookId = c.Int(nullable: false),
              })
              .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropTable("dbo.Orders");
        }
    }
}
