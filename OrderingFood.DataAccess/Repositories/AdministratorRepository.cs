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
    }
}
