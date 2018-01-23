namespace Lunch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Booking : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "FoodChoiceId", c => c.Int(nullable: false));
            AddColumn("dbo.Bookings", "CustomMessage", c => c.String());
            CreateIndex("dbo.Bookings", "FoodChoiceId");
            AddForeignKey("dbo.Bookings", "FoodChoiceId", "dbo.Foods", "Id");
            DropColumn("dbo.Bookings", "FoodChoice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bookings", "FoodChoice", c => c.String());
            DropForeignKey("dbo.Bookings", "FoodChoiceId", "dbo.Foods");
            DropIndex("dbo.Bookings", new[] { "FoodChoiceId" });
            DropColumn("dbo.Bookings", "CustomMessage");
            DropColumn("dbo.Bookings", "FoodChoiceId");
        }
    }
}
