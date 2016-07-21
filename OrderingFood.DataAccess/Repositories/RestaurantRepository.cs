using OrderingFood.Data.Models;
using OrderingFood.Data.Context;
using System;

namespace OrderingFood.DataAccess.Repositories
{
    public class RestaurantRepository : GenericRepository<Restaurant>
    {
        public RestaurantRepository(IOrderingContext context) : base(context)
        {
        }
    }
}