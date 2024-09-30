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
        public IActionResult UpsertProducts(ProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {

                _unitOfWork.ProductRepository.Add(productVM.Product);
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
