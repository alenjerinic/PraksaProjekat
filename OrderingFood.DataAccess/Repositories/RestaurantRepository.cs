using OrderingFood.Data.Models;
using OrderingFood.Data.Context;
using System;

namespace OrderingFood.DataAccess.Repositories
{
    public class RestaurantRepository : GenericRepository<Restaurant>
    {
        public RestaurantRepository(IOrderingContext context) : base(context)
        {
        }


        public Restaurant AddRestaurant(Restaurant rest)
        {
            var restaurant = new Restaurant()
            {
                RestaurantName =rest.RestaurantName,
                Address = rest.Address,
                Telephone = rest.Telephone,
                Active = rest.Active
            };
            _context.Restaurants.Add(restaurant);
            _context.SaveChanges();
            return restaurant;
        }               
    }
}