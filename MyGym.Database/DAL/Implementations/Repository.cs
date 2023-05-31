using Microsoft.EntityFrameworkCore;
using MyGym.Database.DAL.Contracts;
using System.Linq.Expressions;

namespace MyGym.Database.DAL.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly MyGymContext _context;

        public Repository(MyGymContext context)
        {
            _context = context;
        }
        public async Task AddAsync(T entity)
        {
           await _context.Set<T>().AddAsync(entity);
        }

        public  IQueryable<T> Find(Expression<Func<T, bool>> expression)
        {
            return  _context.Set<T>().Where(expression);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate = null)
        {
            var data =  _context.Set<T>().AsQueryable().AsNoTracking();
            if(predicate != null)
            {
                data = data.Where(predicate);
            }
            return data;
        }

        public async Task RemoveAsync(T entity)
        {
           _context.Set<T>().Remove(entity);
        }

        public async Task UpdateAsync(T entity)
        {
           _context.Set<T>().Update(entity);
        }
    }
}
