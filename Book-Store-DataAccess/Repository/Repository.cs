using Book_Store_DataAccess.Context;
using Book_Store_DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Linq.Expressions;

namespace Book_Store_DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _dbContext;

        internal DbSet<T> dbSet;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;

            this.dbSet = _dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);  
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);

            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
