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


        //public List<Meal> GetMealByRestaurant(int id)
        //{
        //    var result = (from meals in _context.Meals
        //                  from restaurants in _context.Restaurants
        //                  where restaurants.ID == meals.RestaurantID && restaurants.ID == id
        //                  select meals).ToList();
        //    return result;
        //}

        public List<Meal> GetMealByRestaurant(int id)
        {

            var result = new List<Meal>();
            result = _context.Meals.Where(m => m.Restaurant.ID == id).ToList();
            return result;
        }


        public void AddMeal(Meal obrok,Restaurant rest)
        {
            var meal = new Meal()
            {
                ID = obrok.ID,
                MealName = obrok.MealName,
                CategoryName = obrok.CategoryName,
                Price = obrok.Price,
                Active = obrok.Active,
                Restaurant = _context.Restaurants.Find(rest.ID),
                RestaurantID=rest.ID                
            };
            _context.Meals.Add(meal);
            _context.SaveChanges();                      
        }
     

       
    }
}
