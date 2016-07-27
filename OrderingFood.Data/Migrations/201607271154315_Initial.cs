namespace OrderingFood.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Administrator",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AdministratorName = c.String(),
                        RestaurantID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Restaurant", t => t.RestaurantID)
                .Index(t => t.RestaurantID);
            
            CreateTable(
                "dbo.Restaurant",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RestaurantName = c.String(),
                        Address = c.String(),
                        Telephone = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Meal",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MealName = c.String(),
                        CategoryName = c.String(),
                        Price = c.Double(nullable: false),
                        RestaurantID = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Restaurant", t => t.RestaurantID)
                .Index(t => t.RestaurantID);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Amount = c.Int(nullable: false),
                        MealID = c.Int(nullable: false),
                        OrderTime = c.DateTime(nullable: false),
                        Delivery = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Meal", t => t.MealID)
                .Index(t => t.MealID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Meal", "RestaurantID", "dbo.Restaurant");
            DropForeignKey("dbo.Order", "MealID", "dbo.Meal");
            DropForeignKey("dbo.Administrator", "RestaurantID", "dbo.Restaurant");
            DropIndex("dbo.Order", new[] { "MealID" });
            DropIndex("dbo.Meal", new[] { "RestaurantID" });
            DropIndex("dbo.Administrator", new[] { "RestaurantID" });
            DropTable("dbo.Order");
            DropTable("dbo.Meal");
            DropTable("dbo.Restaurant");
            DropTable("dbo.Administrator");
        }
    }
}
