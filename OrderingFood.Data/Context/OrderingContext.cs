using OrderingFood.Data.Models;
using System.Data.Entity;

namespace OrderingFood.Data.Context
{
    public class OrderingContext:DbContext,IOrderingContext
    {

        public OrderingContext():base("Name=DefaultConnection")
        {
        }


        static OrderingContext()
        {
            Database.SetInitializer<OrderingContext>(null);

        }



        public virtual DbSet<Administrator> Administrators { get; set; }
        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<Meal> Meals { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>().ToTable("Restaurant");

            modelBuilder.Entity<Restaurant>().HasKey(r => r.ID);
            modelBuilder.Entity<Restaurant>().Property(r => r.ID).HasColumnName("ID");
            modelBuilder.Entity<Restaurant>().Property(r => r.RestaurantName).HasColumnName("RestaurantName");
            modelBuilder.Entity<Restaurant>().Property(r => r.Address).HasColumnName("Address");
            modelBuilder.Entity<Restaurant>().Property(r => r.Active).HasColumnName("Active");
            modelBuilder.Entity<Restaurant>().HasMany(r => r.Administrators).WithRequired(a => a.Restaurant).HasForeignKey(a => a.RestaurantID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Restaurant>().HasMany(r => r.Meals).WithRequired(m => m.Restaurant).HasForeignKey(m => m.RestaurantID).WillCascadeOnDelete(false);


            modelBuilder.Entity<Order>().ToTable("Order");

            modelBuilder.Entity<Order>().HasKey(o => o.ID);
            modelBuilder.Entity<Order>().Property(o => o.ID).HasColumnName("ID");
            modelBuilder.Entity<Order>().Property(o => o.Amount).HasColumnName("Amount");
            modelBuilder.Entity<Order>().Property(o => o.OrderTime).HasColumnName("Date");
            modelBuilder.Entity<Order>().Property(o => o.Delivery).HasColumnName("Delivery");
            modelBuilder.Entity<Order>().HasRequired(o => o.Meal).WithMany(m => m.Orders).WillCascadeOnDelete(false);


            modelBuilder.Entity<Administrator>().ToTable("Administrator");

            modelBuilder.Entity<Administrator>().HasKey(a => a.ID);
            modelBuilder.Entity<Administrator>().Property(a=>a.ID).HasColumnName("ID");
            modelBuilder.Entity<Administrator>().Property(a => a.AdministratorName).HasColumnName("AdministratorName");
            modelBuilder.Entity<Administrator>().HasRequired(a => a.Restaurant).WithOptional();


            modelBuilder.Entity<Meal>().ToTable("Meal");

            modelBuilder.Entity<Meal>().HasKey(m=>m.ID);
            modelBuilder.Entity<Meal>().Property(m => m.ID).HasColumnName("ID");
            modelBuilder.Entity<Meal>().Property(m => m.MealName).HasColumnName("MealName");
            modelBuilder.Entity<Meal>().Property(m => m.Price).HasColumnName("Price");
            modelBuilder.Entity<Meal>().Property(m => m.Active).HasColumnName("Active");
            modelBuilder.Entity<Meal>().HasRequired(m => m.Restaurant).WithOptional();




        }

    }
}
