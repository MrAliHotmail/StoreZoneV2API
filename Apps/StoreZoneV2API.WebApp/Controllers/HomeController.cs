using Microsoft.AspNetCore.Mvc;
using StoreZoneV2API.Application.Services;
using StoreZoneV2API.WebApp.Models;
using System.Diagnostics;

namespace StoreZoneV2API.WebApp.Controllers
{
    public class HomeController : Controller
    {

        // really good code
        // new commit++++ add new feature
        // now inThursday branch
        // add new commit  in 10:13 AM in starday   
        // add a new line in 10:14 AM in starday in saturday branch at 11:05 AM
        // add a new line in 10:14 AM in starday in saturday branch at 11:52 AM
        // add new command in 4:58 PM in Thursday branch for at 9:53 AM
        // add new command in 4:58 PM in wednesday branch for testing git fetch

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
