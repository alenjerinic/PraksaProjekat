using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingFood.DataModels
{
    public class Restaurant
    {
        public int ID { get; set; }
        public string RestaurantName { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public List<Meal> Meals { get; set; }
        public List<Administrator> Administrators { get; set; }
    }
}
