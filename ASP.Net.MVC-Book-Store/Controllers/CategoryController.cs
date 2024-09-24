using Book_Store_DataAccess.Repository.IRepository;
using Book_Store_Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.Net.MVC_Book_Store.Controllers
{
   
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> obj = _unitOfWork.CategoryRepository.GetAll().ToList();
            return View(obj);
        }


        public IActionResult CreateCategories()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategories(Category category)
        {
            if (category.CategoryName == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CategoryName", "Display order cannot match the Category Name");
            }
            if (category.CategoryName.ToLower() == "test")
            {
                ModelState.AddModelError("", "Category Name cannot be 'test'");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepository.Add(category);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult UpdateCategories(int? id)
        {
            if (id == null || id == 0) return NotFound();

            Category? categoryObject = _unitOfWork.CategoryRepository.Get(obj => obj.CategoryId == id);

            if (categoryObject == null) return NotFound();

            return View(categoryObject);
        }

        [HttpPost]
        public IActionResult UpdateCategories(Category category)
        {


            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepository.Update(category);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult DeleteCategories(int? id)
        {
            if (id == null || id == 0) return NotFound();

            Category? categoryObject = _unitOfWork.CategoryRepository.Get(obj => obj.CategoryId == id);

            if (categoryObject == null) return NotFound();

            return View(categoryObject);
        }

        [HttpPost, ActionName("DeleteCategories")]
        public IActionResult DeleteCategoriesPOST(int? id)
        {
            Category? categoryObject = _unitOfWork.CategoryRepository.Get(obj => obj.CategoryId == id);

            if (categoryObject == null) return NotFound();

            _unitOfWork.CategoryRepository.Remove(categoryObject);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }


    }
}
