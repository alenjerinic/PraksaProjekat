using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingFood.Web.Models
{
    public class AllOrdersForMeals
    {
        public MealDetailsModel MealDetail { get; set; }
        public OrderDetailsModel OrderDetail { get; set; }
    }
}