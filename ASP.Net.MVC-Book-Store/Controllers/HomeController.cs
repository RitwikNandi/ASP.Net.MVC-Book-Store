using Book_Store_DataAccess.Repository.IRepository;
using Book_Store_Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASP.Net.MVC_Book_Store.Controllers
{
  

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.ProductRepository.GetAll(includeProperties: "Category");
            return View(productList);
        }
        
        public IActionResult Details(Guid productId)
        {
            Product product = _unitOfWork.ProductRepository.Get(u=>u.Id == productId, includeProperties: "Category");
            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }


    }
}
