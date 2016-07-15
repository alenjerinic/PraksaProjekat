using OrderingFood.Data.Models;
using OrderingFood.Data.Context;
using System;

namespace OrderingFood.DataAccess.Repositories
{
    public class AdministratorRepository : GenericRepository<Administrator>
    {
        public AdministratorRepository(IOrderingContext context) : base(context)
        {
        }


        public Administrator AddAdministrator(string name, Restaurant restaurant)
        {
            var admin = new Administrator()
            {
               AdministratorName=name,
               Restaurant = restaurant
            };
            _context.Administrators.Add(admin);
            _context.SaveChanges();
            return admin;
        }


        public void DeleteAdministrator(int id)
        {
            var admin = _context.Administrators.Find(id);
            if (admin == null)
            {
                throw new ArgumentException("Specified admin does not exist", nameof(id));
            }
            admin.Restaurant = null;           
            _context.SaveChanges();
        }
    }
}
