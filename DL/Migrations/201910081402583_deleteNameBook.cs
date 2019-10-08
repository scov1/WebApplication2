namespace DL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteNameBook : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "AuthorName");
            DropColumn("dbo.Orders", "BookName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "BookName", c => c.String());
            AddColumn("dbo.Orders", "AuthorName", c => c.String());
        }
    }
}
