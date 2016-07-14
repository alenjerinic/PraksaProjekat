using System.Collections.Generic;

namespace OrderingFood.Data.Models
{
    public class Meal
    {
        public int ID { get; set; }
        public string MealName { get; set; }
        public string CategoryName { get; set; }
        public double Price { get; set; }
        public Restaurant Restaurant { get; set; }
        public int RestaurantID { get; set; }
        public List<Order> Orders { get; set; }
        public bool Active { get; set; }
    }
}
