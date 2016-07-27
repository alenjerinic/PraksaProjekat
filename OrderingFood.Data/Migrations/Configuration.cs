namespace OrderingFood.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<OrderingFood.Data.Context.OrderingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(OrderingFood.Data.Context.OrderingContext context)
        {
            if (context.Restaurants.Count() == 0)
            {
                var r = new Restaurant()
                {
                    RestaurantName = "Ultra",
                    Address = "Kralja Petra I Karadordjevica 10",
                    Telephone = "+381235668326",
                    Active=true
                };
                context.Restaurants.AddOrUpdate(r);

                var r1 = new Restaurant()
                {
                    RestaurantName = "Bridge",
                    Address = "Trg Slobode",
                    Telephone = "+381235238326",
                    Active = true
                };

                var a = new Administrator()
                {
                    Restaurant=r1,
                    AdministratorName="a.jerinic"
                };
                context.Administrators.AddOrUpdate(a);

                context.Administrators.AddOrUpdate(new Administrator()
                {
                    Restaurant = r,
                    AdministratorName = "Dule"
                });

                var m = new Meal()
                {
                    Restaurant=r,
                    MealName="Pomfrit",
                    CategoryName="Prilog",
                    Price=100.00,
                    Active=true
                    
                };
                context.Meals.AddOrUpdate(m);

                var m1 = new Meal()
                {
                    Restaurant = r1,
                    MealName = "Pomfrit",
                    CategoryName = "Prilog",
                    Price = 150.00,
                    Active=true

                };
                context.Meals.AddOrUpdate(m1);


                context.Meals.AddOrUpdate(new Meal()
                {
                    Restaurant = r,
                    MealName = "Capricoza",
                    CategoryName = "Pizza",
                    Price = 540.00,
                    Active=true
                });

                context.Orders.AddOrUpdate(new Order()
                {
                    UserName="Alen",
                    Meal =m,        
                    Amount = 1,
                    OrderTime = DateTime.Now,
                    Delivery = false
                });
            }
        }
    }
}
