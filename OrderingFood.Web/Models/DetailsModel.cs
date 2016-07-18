using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace OrderingFood.Web.Models
{
    public class DetailsModel : RestaurantModel
    {
        [DisplayName("Meal Name")]
        public string MealName { get; set; }
        [DisplayName("Price")]
        public double Price { get; set; }
        [DisplayName("Category")]
        public string Category { get; set; }

    }
}   