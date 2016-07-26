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

        [DisplayName("Amaunt")]
        public int Amount { get; set; }

        [DisplayName("Time")]
        public int OrderTime { get; set; }

        [DisplayName("Delivery")]
        public bool Delivery { get; set; }

        public int MealID { get; set; }
    }
}