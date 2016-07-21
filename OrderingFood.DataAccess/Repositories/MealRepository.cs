﻿using System.Collections.Generic;
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
        //                  where restaurants.ID == meals.RestaurantID && restaurants.ID==id
        //                  select meals).ToList();
        //    return result;
        //}       
    }
}
