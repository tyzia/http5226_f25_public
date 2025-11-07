using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LecDemo.Models;

namespace LecDemo.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
//    _logger.LogInformation("Home page visited at {Time}", DateTime.Now);
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

//    public IActionResult MyAction()
//    {
//        return Content("Hello");  // ← This method comes from base class
//    }

        public IActionResult About()
        {
            ViewBag.Message = "About my normal app";
            return View();
        }
}
