using OrderingFood.Data.Models;
using OrderingFood.Data.Context;

namespace OrderingFood.DataAccess.Repositories
{
    public class AdministratorRepository : GenericRepository<Administrator>
    {
        public AdministratorRepository(IOrderingContext context) : base(context)
        {
        }
    }
}