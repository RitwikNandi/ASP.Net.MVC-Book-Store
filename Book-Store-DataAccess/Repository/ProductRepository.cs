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
            //var objFromDb = _productContext.Products.FirstOrDefault(p => p.Id == product.Id);

            //if (objFromDb != null) { 
            //    objFromDb.Title = product.Title;
            //    objFromDb.Description = product.Description;
            //    objFromDb.CategoryId = product.CategoryId;
            //    objFromDb.Author = product.Author;
            //    objFromDb.ISBN = product.ISBN;
            //    objFromDb.List50 = product.List50;
            //    objFromDb.ListPrice = product.ListPrice;
            //    objFromDb.Price100 = product.Price100;

            //    if (objFromDb.ImageUrl != null) {
            //        objFromDb.ImageUrl = product.ImageUrl;
            //    }
            //}

            _productContext.Products.Update(product);
        }
    }
}
