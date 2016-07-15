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


        public Restaurant AddRestaurant(string name, string address, string tel, bool active)
        {
            var restaurant = new Restaurant()
            {
                RestaurantName = name,
                Address = address,
                Telephone = tel,
                Active = active
            };
            _context.Restaurants.Add(restaurant);
            _context.SaveChanges();
            return restaurant;
        }


        public void DeleteRestaurant(int id)
        {
            var restaurant = _context.Restaurants.Find(id);
            if (restaurant == null)
            {
                throw new ArgumentException("Specified restaurant does not exist", nameof(id));
            }
            restaurant = null;
            _context.SaveChanges();
        }
    }
}