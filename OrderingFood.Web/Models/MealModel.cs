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

        [DisplayName("Category Name")]
        public string CategoryName { get; set; }

        [DisplayName("Price")]
        public double Price { get; set; }

        [DisplayName("Active")]
        public bool Active { get; set; }

        public RestaurantModel Restaurant { get; set; }

        public MealModel()
        {
            Restaurant = new RestaurantModel();
        }

        //public string RestaurantName { get; set; }

        //public int RestaurantID { get; set; }

    }
}


