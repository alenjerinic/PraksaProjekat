using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingFood.DataModels

{
    public class Administrator
    {
        public int ID { get; set; }
        public string AdministratorName { get; set; }
        public Restaurant Restaurant { get; set; }
        public int RestaurantID { get; set;}        
    }
}
