using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace OrderingFood.Web.Models
{
    public class RestaurantModel
    {
        [DisplayName("Identification Number")]
        public int ID { get; set; }

        [DisplayName("Restaurant Name")]
        public string RestaurantName { get; set; }

        [DisplayName("Restaurant Address")]
        public string Address { get; set; }

        [DisplayName("Restaurant Telephone")]
        public string Telephone { get; set; }

        [DisplayName("Restaurant Active")]
        public bool Active { get; set; }
    }
}