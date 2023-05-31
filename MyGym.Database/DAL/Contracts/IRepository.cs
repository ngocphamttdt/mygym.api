using System.Linq.Expressions;

namespace MyGym.Database.DAL.Contracts
{
    public  interface IRepository<T>
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
       IQueryable<T> Get(Expression<Func<T, bool>> predicate = null);
        IQueryable<T> Find(Expression<Func<T, bool>> expression);


    }

    /// <summary>
    /// interface  is composite
    /// class is inherit 
    /// </summary>

    public abstract class AbstractRepository
    {
        public abstract void fun1();
        public virtual void fun2()
        {

        }
    }
}
