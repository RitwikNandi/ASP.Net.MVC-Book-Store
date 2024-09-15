using Book_Store_DataAccess.Context;
using Book_Store_Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.Net.MVC_Book_Store.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public CategoryController( AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IActionResult Index()
        {
            return View(_appDbContext.Categories.ToList());
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
                _appDbContext.Categories.Add(category);
                _appDbContext.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult UpdateCategories(int? id)
        {
            if (id == null || id == 0)  return NotFound();

            Category? categoryObject = _appDbContext.Categories.Find(id);

            if (categoryObject == null) return NotFound();

            return View(categoryObject);
        }

        [HttpPost]
        public IActionResult UpdateCategories(Category category)
        {
           

            if (ModelState.IsValid)
            {
                _appDbContext.Categories.Update(category);
                _appDbContext.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult DeleteCategories(int? id)
        {
            if (id == null || id == 0) return NotFound();

            Category? categoryObject = _appDbContext.Categories.Find(id);

            if (categoryObject == null) return NotFound();

            return View(categoryObject);
        }

        [HttpPost, ActionName("DeleteCategories")]
        public IActionResult DeleteCategoriesPOST(int? id)
        {
            Category? categoryObject = _appDbContext.Categories.Find(id);

            if (categoryObject == null) return NotFound();

            _appDbContext.Categories.Remove(categoryObject);
            _appDbContext.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }


    }
}
