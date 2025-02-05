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
            List<Product> productObj = _unitOfWork.ProductRepository.GetAll(includeProperties:"Category").ToList();   

            return View(productObj);
        }

        public IActionResult UpsertProducts(Guid? id)
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

            if(id == null) return View(productViewModel);

            else
            {
                productViewModel.Product = _unitOfWork.ProductRepository.Get(u => u.Id == id);
                return View(productViewModel);
            }
        }


        [HttpPost]
        public IActionResult UpsertProducts(ProductViewModel productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\products");

                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productVM.Product.ImageUrl = @"\images\products\" + fileName;
                }

                if (productVM.Product.Id == null) {
                    _unitOfWork.ProductRepository.Add(productVM.Product);

                }
                else
                {
                    _unitOfWork.ProductRepository.Update(productVM.Product);
                }

                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.CategoryName,
                    Value = u.CategoryId.ToString()
                });
                return View(productVM);
            }
        }

        #region APICALLS

        [HttpGet]
        public IActionResult GetAll() { 
            List<Product> productObj = _unitOfWork.ProductRepository.GetAll(includeProperties:"Category").ToList();

            return Json(new { data = productObj });
        }

        [HttpDelete]
        public IActionResult DeleteProducts(Guid? id)
        {
            var deleteProductObj = _unitOfWork.ProductRepository.Get(u=>u.Id == id);

            if (deleteProductObj == null) return Json(new { success = false, message= "Error while deleting" });

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, deleteProductObj.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.ProductRepository.Remove(deleteProductObj);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion

    }
}
