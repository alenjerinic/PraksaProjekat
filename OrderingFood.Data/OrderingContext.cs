using System.Data.Entity;
using OrderingFood.DataModels;

namespace OrderingFood.DataAccess
{
    public class OrderingContext:DbContext
    {

        public OrderingContext():base("Name=DefaultConnection")
        {
        }

        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrator>().ToTable("Administrator");

            modelBuilder.Entity<Administrator>().HasKey(a => a.ID);
            modelBuilder.Entity<Administrator>().Property(a=>a.ID).HasColumnName("ID");
            modelBuilder.Entity<Administrator>().Property(a => a.AdministratorName).HasColumnName("AdministratorName");
            modelBuilder.Entity<Administrator>().HasMany(a=>a.Restaurant).WithRequired(r=>r.Admin).HasForeignKey(r=>r.Admin_ID);

            Entity<Restaurant>()

        }

    }
}
