using System.Data.Entity;
using OrderingFood.DataModels;

namespace OrderingFood.DataAccess
{
    public class OrderingContext:DbContext
    {
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Order> Orders { get; set; }

    }
}
