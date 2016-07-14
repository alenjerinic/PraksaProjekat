using System.Collections.Generic;

namespace OrderingFood.Data.Models
{
    public class Restaurant
    {
        public int ID { get; set; }
        public string RestaurantName { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public List<Meal> Meals { get; set; }
        public List<Administrator> Administrators { get; set; }
        public bool Active { get; set; }        
    }
}
