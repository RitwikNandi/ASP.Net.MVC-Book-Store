using Book_Store_DataAccess.Context;
using Book_Store_DataAccess.Repository.IRepository;
using Book_Store_Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store_DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDbContext _productContext;

        public ProductRepository(AppDbContext productContext) : base(productContext)
        {
            _productContext = productContext;
        }

        

        public void Update(Product product)
        {
            _productContext.Products.Update(product);
        }
    }
}
