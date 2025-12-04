using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Crash.Models;

namespace Crash.Controllers;

[TypeFilter(typeof(BasicExceptionFilter))]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
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

    public IActionResult Calculate(int a, int b)
    {
//        try {
            ViewBag.Result = a / b;
            return View();
//        }
//        catch (Exception ex)
//        {
//            ViewBag.Result = ex;
//            return View();
//        }
    }

    public IActionResult Students(int studentId)
    {
        ViewBag.Result = studentId == 1 ? new { Name = "Andrei" } : null;
        return View();
    }
}
