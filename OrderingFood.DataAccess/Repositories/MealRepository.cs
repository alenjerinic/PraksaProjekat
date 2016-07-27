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

        public void UpdateMeal(Meal obrok, Restaurant rest)
        {
            var meal = new Meal()
            {
                ID = obrok.ID,
                MealName = obrok.MealName,
                CategoryName = obrok.CategoryName,
                Price = obrok.Price,
                Active = obrok.Active,
                Restaurant = _context.Restaurants.Find(rest.ID),
                RestaurantID = rest.ID
            };
            
            var m = _context.Meals.Find(obrok.ID);

            m.ID = meal.ID;
            m.MealName = meal.MealName;
            m.CategoryName = meal.CategoryName;
            m.Price = meal.Price;
            m.Active = meal.Active;
            m.Restaurant = meal.Restaurant;
            m.RestaurantID = meal.RestaurantID;

            _context.SaveChanges();
        }   

       
    }
}
