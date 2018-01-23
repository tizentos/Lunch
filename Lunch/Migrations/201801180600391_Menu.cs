namespace Lunch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Menu : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Menus", "FirstChoice_Id", "dbo.Foods");
            DropIndex("dbo.Menus", new[] { "FirstChoice_Id" });
            RenameColumn(table: "dbo.Menus", name: "FirstChoice_Id", newName: "FirstChoiceId");
            AddColumn("dbo.Menus", "ScondChoiceId", c => c.Int(nullable: false));
            AlterColumn("dbo.Menus", "FirstChoiceId", c => c.Int(nullable: false));
            CreateIndex("dbo.Menus", "FirstChoiceId");
            AddForeignKey("dbo.Menus", "FirstChoiceId", "dbo.Foods", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Menus", "FirstChoiceId", "dbo.Foods");
            DropIndex("dbo.Menus", new[] { "FirstChoiceId" });
            AlterColumn("dbo.Menus", "FirstChoiceId", c => c.Int());
            DropColumn("dbo.Menus", "ScondChoiceId");
            RenameColumn(table: "dbo.Menus", name: "FirstChoiceId", newName: "FirstChoice_Id");
            CreateIndex("dbo.Menus", "FirstChoice_Id");
            AddForeignKey("dbo.Menus", "FirstChoice_Id", "dbo.Foods", "Id");
        }
    }
}
