using System.Collections.Generic;
using OrderingFood.Data.Context;
using OrderingFood.Data.Models;
using System.Linq;
using System;

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


        public Meal AddMeal(string name, string category, double price, bool active, Restaurant restaurant)
        {
            var meal = new Meal()
            {
                MealName = name,
                CategoryName = category,
                Price = price,
                Active = active,
                Orders = null,
                Restaurant = restaurant
            };
            _context.Meals.Add(meal);
            _context.SaveChanges();
            return meal;
        }


        public void DeleteMeal(int id)
        {
            var meal = _context.Meals.Find(id);
            if (meal == null)
            {
                throw new ArgumentException("Specified meal does not exist", nameof(id));
            }
            meal = null;
            _context.SaveChanges();

        }
    }
}
