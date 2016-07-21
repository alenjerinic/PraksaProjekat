using OrderingFood.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace OrderingFood.Web.Models
{
    public class DetailsModel
    {
        public RestaurantModel restoran { get; set; }
        public List<Meal> mealsForRestaurant { get; set; }
    }
}   


        


    
