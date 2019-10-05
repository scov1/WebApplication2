namespace DL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeTypePeriod : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Period", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Period", c => c.Int(nullable: false));
        }
    }
}
