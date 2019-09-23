namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedDatabase : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.Orders", "Books_Id", "dbo.Books");
            //DropForeignKey("dbo.Orders", "Users_Id", "dbo.Users");
            //DropIndex("dbo.Orders", new[] { "Books_Id" });
            //DropIndex("dbo.Orders", new[] { "Users_Id" });
            //DropColumn("dbo.Orders", "BookId");
            //DropColumn("dbo.Orders", "UserId");
            //RenameColumn(table: "dbo.Orders", name: "Books_Id", newName: "BookId");
            //RenameColumn(table: "dbo.Orders", name: "Users_Id", newName: "UserId");
            AlterColumn("dbo.Orders", "BookId", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "BookId");
            CreateIndex("dbo.Orders", "UserId");
            AddForeignKey("dbo.Orders", "BookId", "dbo.Books", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
            DropForeignKey("dbo.Orders", "BookId", "dbo.Books");
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.Orders", new[] { "BookId" });
            AlterColumn("dbo.Orders", "UserId", c => c.Int());
            AlterColumn("dbo.Orders", "BookId", c => c.Int());
            RenameColumn(table: "dbo.Orders", name: "UserId", newName: "Users_Id");
            RenameColumn(table: "dbo.Orders", name: "BookId", newName: "Books_Id");
            AddColumn("dbo.Orders", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "BookId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "Users_Id");
            CreateIndex("dbo.Orders", "Books_Id");
            AddForeignKey("dbo.Orders", "Users_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Orders", "Books_Id", "dbo.Books", "Id");
        }
    }
}
