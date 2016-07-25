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

        //public List<Administrator> GetAdministratorsByRestaurant(int id)
        //{
        //    var result = (from admins in _context.Administrators
        //                  from restaurants in _context.Restaurants
        //                  where restaurants.ID == admins.RestaurantID && restaurants.ID == id
        //                  select admins).ToList();
        //    return result;
        //}


        public List<Administrator> GetAdministratorsByRestaurant(int id)
        {

            var result = new List<Administrator>();
            result = _context.Administrators.Where(a => a.Restaurant.ID == id).ToList();
            return result;
        }



        public void AddAdministrator(Administrator admin, Restaurant rest)
        {
            var administrator = new Administrator()
            {
                AdministratorName = admin.AdministratorName, 
                Restaurant=rest,               
                RestaurantID = rest.ID
            };
            _context.Administrators.Add(administrator);
            _context.SaveChanges();
        }
    }
}
