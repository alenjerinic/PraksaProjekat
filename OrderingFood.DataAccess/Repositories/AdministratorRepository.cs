using System.Collections.Generic;
using OrderingFood.Data.Context;
using OrderingFood.Data.Models;
using System.Linq;
using System;

namespace OrderingFood.DataAccess.Repositories
{
    public class AdministratorRepository : GenericRepository<Administrator>
    {
        public AdministratorRepository(IOrderingContext context) : base(context)
        {
        }

        public List<Administrator> GetAdministratorsByRestaurant(int id)
        {
            var result = (from admins in _context.Administrators
                          from restaurants in _context.Restaurants
                          where restaurants.ID == admins.RestaurantID && restaurants.ID == id
                          select admins).ToList();
            return result;
        }


        public void AddAdministrator(Administrator admin)
        {
            var administrator = new Administrator()
            {
                AdministratorName = admin.AdministratorName,                
                RestaurantID = admin.RestaurantID
            };
            _context.Administrators.Add(administrator);
            _context.SaveChanges();
        }
    }
}
