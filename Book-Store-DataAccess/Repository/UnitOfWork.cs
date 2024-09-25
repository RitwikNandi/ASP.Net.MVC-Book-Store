using Book_Store_DataAccess.Context;
using Book_Store_DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store_DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository CategoryRepository {get; private set;}
        public IProductRepository ProductRepository { get; private set; }


        private readonly AppDbContext _dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            CategoryRepository = new CategoryRepository(_dbContext);
            ProductRepository = new ProductRepository(_dbContext);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
