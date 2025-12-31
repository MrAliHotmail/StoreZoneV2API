using Microsoft.AspNetCore.Mvc;
using StoreZoneV2API.Application.Services;
using StoreZoneV2API.WebApp.Models;
using System.Diagnostics;

namespace StoreZoneV2API.WebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: HomeController From Sir.ali911@hotmail.com
        //
        //
        //
        // really good code
        // new commit++++ add new feature
        private readonly ProductService _productService;
        public HomeController(ProductService productService)
        {
            _productService = productService;
        }   
        public IActionResult Index() =>   View(_productService.GetAllAsync().Result);


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
