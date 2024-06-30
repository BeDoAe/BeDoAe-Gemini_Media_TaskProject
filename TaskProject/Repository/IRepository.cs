using System.Linq.Expressions;

namespace TaskProject.Repository
{
    public interface IRepository<T> where T : class
    {
        public void Delete(T entity);
        public List<T> GetAll();
        public T Get(Expression<Func<T, bool>> filter);
        //public void Insert(T obj);
        //public void Save();
        public Task<bool> InsertAsync(T obj);

        public void Update(T obj);

    }
}
