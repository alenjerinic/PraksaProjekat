using OrderingFood.Data.Models;
using System.Collections.Generic;

namespace OrderingFood.Web.Models
{
    public class OrderDetailsModel
    {
        public RestaurantModel restoran { get; set; }
        public MealModel meal { get; set; }
        public List<Order> ordersForMeal { get; set; }
    }
}