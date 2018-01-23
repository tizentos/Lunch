namespace Lunch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Menus", "FoodId", "dbo.Foods");
            DropIndex("dbo.Menus", new[] { "FoodId" });
            RenameColumn(table: "dbo.Menus", name: "FoodId", newName: "FirstChoice_Id");
            AddColumn("dbo.Foods", "Name", c => c.String());
            AddColumn("dbo.Menus", "SecondChoice_Id", c => c.Int());
            AlterColumn("dbo.Menus", "FirstChoice_Id", c => c.Int());
            CreateIndex("dbo.Menus", "FirstChoice_Id");
            CreateIndex("dbo.Menus", "SecondChoice_Id");
            AddForeignKey("dbo.Menus", "SecondChoice_Id", "dbo.Foods", "Id");
            AddForeignKey("dbo.Menus", "FirstChoice_Id", "dbo.Foods", "Id");
            DropColumn("dbo.Foods", "FirstOption");
            DropColumn("dbo.Foods", "SecondOptioin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Foods", "SecondOptioin", c => c.String());
            AddColumn("dbo.Foods", "FirstOption", c => c.String());
            DropForeignKey("dbo.Menus", "FirstChoice_Id", "dbo.Foods");
            DropForeignKey("dbo.Menus", "SecondChoice_Id", "dbo.Foods");
            DropIndex("dbo.Menus", new[] { "SecondChoice_Id" });
            DropIndex("dbo.Menus", new[] { "FirstChoice_Id" });
            AlterColumn("dbo.Menus", "FirstChoice_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Menus", "SecondChoice_Id");
            DropColumn("dbo.Foods", "Name");
            RenameColumn(table: "dbo.Menus", name: "FirstChoice_Id", newName: "FoodId");
            CreateIndex("dbo.Menus", "FoodId");
            AddForeignKey("dbo.Menus", "FoodId", "dbo.Foods", "Id", cascadeDelete: true);
        }
    }
}
