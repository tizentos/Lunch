namespace Lunch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Menu1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Menus", "FirstChoiceId", "dbo.Foods");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.Menus", new[] { "SecondChoice_Id" });
            RenameColumn(table: "dbo.Menus", name: "SecondChoice_Id", newName: "SecondChoiceId");
            AlterColumn("dbo.Menus", "SecondChoiceId", c => c.Int(nullable: false));
            CreateIndex("dbo.Menus", "SecondChoiceId");
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Menus", "FirstChoiceId", "dbo.Foods", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id");
            DropColumn("dbo.Menus", "ScondChoiceId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Menus", "ScondChoiceId", c => c.Int(nullable: false));
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Menus", "FirstChoiceId", "dbo.Foods");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Menus", new[] { "SecondChoiceId" });
            AlterColumn("dbo.Menus", "SecondChoiceId", c => c.Int());
            RenameColumn(table: "dbo.Menus", name: "SecondChoiceId", newName: "SecondChoice_Id");
            CreateIndex("dbo.Menus", "SecondChoice_Id");
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Menus", "FirstChoiceId", "dbo.Foods", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
