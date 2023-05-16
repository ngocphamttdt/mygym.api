using System.Linq.Expressions;

namespace MyGym.Database.DAL.Contracts
{
    public  interface IRepository<T>
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate = null);

        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);

    }
}
