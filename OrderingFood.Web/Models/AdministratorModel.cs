using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace OrderingFood.Web.Models
{
    public class AdministratorModel
    {
        [DisplayName("Identification Number")]
        public int ID { get; set; }

        [DisplayName("Administrator Name")]
        public string AdministratorName { get; set; }

        public int RestaurantID { get; set; }

    }
}