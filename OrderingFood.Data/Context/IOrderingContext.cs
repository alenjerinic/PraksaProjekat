using OrderingFood.Data.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace OrderingFood.Data.Context
{
    public interface IOrderingContext : IDisposable
    {
        DbSet<T> Set<T>() where T : class;
        int SaveChanges();
        DbEntityEntry<T> Entry<T>(T entity) where T : class;
        new void Dispose();

        DbSet<Administrator> Administrators { get; set; }
        DbSet<Restaurant> Restaurants { get; set; }
        DbSet<Meal> Meals { get; set; }
        DbSet<Order> Orders { get; set; }

    }
}

