using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingFood.DataModels
{
    public class Meal
    {
        public int ID { get; set; }
        public string MealName { get; set; }
        public string CategoryName { get; set; }
        public int Price { get; set; }
        public Restaurant Restaurant { get; set; }
        public int Restaurant_ID { get; set; }
    }
}
