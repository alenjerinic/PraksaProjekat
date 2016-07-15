using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace OrderingFood.Web.Models
{
    public class MealModel
    {
        [DisplayName("Identification Number")]
        public int ID { get; set; }

        [DisplayName("Meal Name")]       
        public string MealName { get; set; }

        [DisplayName("Categori Name")]
        public string CategoryName { get; set; }

        [DisplayName("Price")]
        public double Price { get; set; }
        
    }
}


