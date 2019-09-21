namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateBook : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Orders", "ReturnDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Orders", "Period", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Period");
            DropColumn("dbo.Orders", "ReturnDate");
            DropColumn("dbo.Orders", "CreateDate");
        }
    }
}
