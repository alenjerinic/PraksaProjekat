namespace OrderingFood.Data.Models

{
    public class Administrator
    {
        public int ID { get; set; }
        public string AdministratorName { get; set; }
        public Restaurant Restaurant { get; set; }
        public int RestaurantID { get; set;}        
    }
}
