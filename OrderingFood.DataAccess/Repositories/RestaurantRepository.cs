using OrderingFood.Data.Models;
using OrderingFood.Data.Context;

namespace OrderingFood.DataAccess.Repositories
{
    public class RestaurantRepository : GenericRepository<Restaurant>
    {
        public RestaurantRepository(IOrderingContext context) : base(context)
        {
        }
    }
}