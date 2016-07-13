namespace OrderingFood.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<OrderingFood.DataAccess.OrderingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(OrderingFood.DataAccess.OrderingContext context)
        {
            if (context.Restaurants.Count() == 0)
            {
                var r = new DataModels.Restaurant()
                {
                    RestaurantName = "Ultra",
                    Address = "Kralja Petra I Karadordjevica 10",
                    Telephone = "+381235668326"
                };
                context.Restaurants.AddOrUpdate(r);

                var r1 = new DataModels.Restaurant()
                {
                    RestaurantName = "Bridge",
                    Address = "Trg Slobode",
                    Telephone = "+381235238326"
                };

                var a = new DataModels.Administrator()
                {
                    Restaurant=r1,
                    AdministratorName="Dusan"
                };
                context.Administrators.AddOrUpdate(a);

                context.Administrators.AddOrUpdate(new DataModels.Administrator()
                {
                    Restaurant = r,
                    AdministratorName = "Dule"
                });

                var m = new DataModels.Meal()
                {
                    Restaurant=r,
                    MealName="Pomfrit",
                    CategoryName="Prilog",
                    Price=100.00
                    
                };
                context.Meals.AddOrUpdate(m);

                var m1 = new DataModels.Meal()
                {
                    Restaurant = r1,
                    MealName = "Pomfrit",
                    CategoryName = "Prilog",
                    Price = 150.00

                };
                context.Meals.AddOrUpdate(m1);


                context.Meals.AddOrUpdate(new DataModels.Meal()
                {
                    Restaurant = r,
                    MealName = "Capricoza",
                    CategoryName = "Pizza",
                    Price = 540.00
                });

                context.Orders.AddOrUpdate(new DataModels.Order()
                {
                    Meal =m,        
                    Amount = 1,
                    Date = DateTime.Now,
                    Delivery = false
                });
            }
        }
    }
}
