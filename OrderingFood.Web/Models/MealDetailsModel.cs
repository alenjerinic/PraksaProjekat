using OrderingFood.Data.Models;
using System.Collections.Generic;

namespace OrderingFood.Web.Models
{
    public class MealDetailsModel
    {
        public RestaurantModel restoran { get; set; }
        public List<Meal> mealsForRestaurant { get; set; }
    }
}   


        


    
