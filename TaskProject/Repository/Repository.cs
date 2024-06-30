using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace TaskProject.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly Context context;
        internal DbSet<T> dbSet;
        public Repository(Context _context)
        {
            context = _context;
            this.dbSet = context.Set<T>();
        }
        public virtual void Delete(T entity)
        {
           
            dbSet.Remove(entity);
        }
        public virtual List<T> GetAll()
        {
            IQueryable<T> query = dbSet;

            return query.ToList();
        }
        public virtual T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }
        public async Task<bool> InsertAsync(T obj)
        {
            if (obj != null)
            {
                await dbSet.AddAsync(obj);
                return true;
            }
            return false;
        }

        public void Update(T obj)
        {
            dbSet.Update(obj);
        }
    }
}

