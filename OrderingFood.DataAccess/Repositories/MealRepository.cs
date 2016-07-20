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

        public List<Meal> GetMealByRestaurant(int id)
        {
            var result = (from meals in _context.Meals
                          from restaurants in _context.Restaurants
                          where restaurants.ID == meals.RestaurantID && restaurants.ID==id
                          select meals).ToList();
            return result;
        }


        public Meal AddMeal(Meal obrok)
        {
            var meal = new Meal()
            {
                MealName = obrok.MealName,
                CategoryName = obrok.CategoryName,
                Price = obrok.Price,
                Active = obrok.Active,
                //RestaurantID = obrok.RestaurantID
            };
            _context.Meals.Add(meal);
            _context.SaveChanges();
            return meal;
        }       
    }
}
