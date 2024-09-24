using Book_Store_DataAccess.Repository.IRepository;
using Book_Store_Models;
using Book_Store_Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASP.Net.MVC_Book_Store.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Product> productObj = _unitOfWork.ProductRepository.GetAll().ToList();   

            return View(productObj);
        }

        public IActionResult UpsertProducts(int? id) 
        {

            ProductViewModel productViewModel = new()
            {
                CategoryList = _unitOfWork.CategoryRepository.
                GetAll().Select(u => new SelectListItem
                {
                    Text = u.CategoryName,
                    Value = u.CategoryId.ToString(),
                }),
                Product = new Product()
            };

            if(id == 0 || id == 0) return View(productViewModel);

            else
            {
                productViewModel.Product = _unitOfWork.ProductRepository.Get(u => u.Id == id);
                return View(productViewModel);
            }
        }

        [HttpPost]
        public IActionResult UpsertProducts(ProductViewModel productViewModel, IFormFile? file)
        {
            if (productViewModel.Product.Title.ToLower() == "test")
            {
                ModelState.AddModelError("", "Title name can not be test");
            }

            if (ModelState.IsValid)
            {
                if (productViewModel.Product.Id == 0)
                {
                    _unitOfWork.ProductRepository.Add(productViewModel.Product);
                }
                else
                {
                    _unitOfWork.ProductRepository.Update(productViewModel.Product);
                }

                _unitOfWork.Save();

                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\products");

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productViewModel.Product.ImageUrl = @"images\products" + fileName;
                }

                _unitOfWork.ProductRepository.Add(productViewModel.Product);
                _unitOfWork.Save();
                TempData["success"] = "Book created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                productViewModel.CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.CategoryName,
                    Value = u.CategoryId.ToString()
                });
                return View(productViewModel);
}

           
        }

        //[HttpPost]
        //public IActionResult Upsert(ProductViewModel productViewModel, List<IFormFile> files)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (productViewModel.Product.Id == 0)
        //        {
        //            _unitOfWork.ProductRepository.Add(productViewModel.Product);
        //        }
        //        else
        //        {
        //            _unitOfWork.ProductRepository.Update(productViewModel.Product);
        //        }

        //        _unitOfWork.Save();


        //        string wwwRootPath = _webHostEnvironment.WebRootPath;
        //        if (files != null)
        //        {

        //            foreach (IFormFile file in files)
        //            {
        //                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        //                string productPath = @"images\products\product-" + productViewModel.Product.Id;
        //                string finalPath = Path.Combine(wwwRootPath, productPath);

        //                if (!Directory.Exists(finalPath))
        //                    Directory.CreateDirectory(finalPath);

        //                using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
        //                {
        //                    file.CopyTo(fileStream);
        //                }

        //                ProductImage productImage = new()
        //                {
        //                    ImageUrl = @"\" + productPath + @"\" + fileName,
        //                    ProductId = productVM.Product.Id,
        //                };

        //                if (productVM.Product.ProductImages == null)
        //                    productVM.Product.ProductImages = new List<ProductImage>();

        //                productVM.Product.ProductImages.Add(productImage);

        //            }

        //            _unitOfWork.Product.Update(productVM.Product);
        //            _unitOfWork.Save();




        //        }


        //        TempData["success"] = "Product created/updated successfully";
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
        //        {
        //            Text = u.Name,
        //            Value = u.Id.ToString()
        //        });
        //        return View(productVM);
        //    }
        //}

        public IActionResult DeleteProducts(int? id)
        {
            if (id == 0) return NotFound();

            Product productObject = _unitOfWork.ProductRepository.Get(obj => obj.Id == id);

            if (productObject == null) return NotFound();

            return View(productObject);
        }
        
        [HttpPost, ActionName("DeleteProducts")]
        public IActionResult DeleteProductsPOST(int? id)
        {
            Product? productObject = _unitOfWork.ProductRepository.Get(obj => obj.Id == id);

            if (productObject == null) return NotFound();

            _unitOfWork.ProductRepository.Remove(productObject);
            _unitOfWork.Save();
            TempData["success"] = "Book deleted successfully";

            return RedirectToAction("Index");

        }


    }
}
