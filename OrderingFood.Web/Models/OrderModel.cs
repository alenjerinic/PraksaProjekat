using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace OrderingFood.Web.Models
{
    public class OrderModel
    {
        [DisplayName("Identification NUmber")]
        public int ID { get; set; }

        [DisplayName("UserName")]
        public string UserName { get; set; }

        [DisplayName("Amaunt")]
        public int Amount { get; set; }

        [DisplayName("Time")]        
        public DateTime OrderTime { get; set; }

        [DisplayName("Delivery")]
        public bool Delivery { get; set; }

        public MealModel Meal { get; set; }

        public OrderModel()
        {
            Meal = new MealModel();
        }

        //public string MealName { get;set; }

        //public int MealID { get; set; }

        //public int RestaurantID { get; set; }
    }
}