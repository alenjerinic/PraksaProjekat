using System;

namespace OrderingFood.Data.Models
{
    public class Order
    {
        public int ID { get; set; }
        public int Amount { get; set; }
        public Meal Meal { get; set; }
        public int MealID { get; set; }
        public int OrderTime { get; set; }
        public bool Delivery { get; set; }

    }
}
