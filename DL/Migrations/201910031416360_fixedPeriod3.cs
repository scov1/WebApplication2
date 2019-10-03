namespace DL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixedPeriod3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Period", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Period");
        }
    }
}
