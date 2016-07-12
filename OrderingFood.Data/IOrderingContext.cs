using OrderingFood.DataModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingFood.Data
{
    public interface IOrderingContext:IDisposable
    {
         DbSet<Administrator> Administrators { get; set; }
         DbSet<Restaurant> Restaurants { get; set; }
         DbSet<Meal> Meals { get; set; }
         DbSet<Order> Orders { get; set; }


    }
}
