using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OrderingFood.DataAccess.Repositories
{
    public interface IGenericRepository<T> where T: class
    {
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        T GetByID(int ID);
        void Insert(T entity);
        void Delete(int ID);
        void Update(T entity);
        void Save();
    }
}


