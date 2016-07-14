using OrderingFood.Data.Models;
using OrderingFood.Data.Context;
using System.Collections.Generic;
using System.Linq;

namespace OrderingFood.DataAccess.Repositories
{
    public class MealRepository : GenericRepository<Meal>
    {
        public MealRepository(IOrderingContext context) : base(context)
        {
        }

        public List<Meal> GetMealByRestaurant(string name)
        {
            var result = (from meals in _context.Meals
                          from restaurants in _context.Restaurants
                          where restaurants.ID == meals.RestaurantID && restaurants.RestaurantName == name
                          select meals).ToList();
            return result;
        }
    }
}
