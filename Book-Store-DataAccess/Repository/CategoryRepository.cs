using Book_Store_DataAccess.Context;
using Book_Store_DataAccess.Repository.IRepository;
using Book_Store_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store_DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository 
    {
        private readonly AppDbContext _dbContext;

        public CategoryRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
     
        public void Update(Category category)
        {
            _dbContext.Categories.Update(category);
        }
    }
}
