using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingFood.DataModels
{
    public class Order
    {
        public int ID { get; set; }
        public int Amount { get; set; }
        public Meal Meal { get; set; }
        public int MealID { get; set; }
        public DateTime Date { get; set; }
        public bool Delivery { get; set; }

    }
}
