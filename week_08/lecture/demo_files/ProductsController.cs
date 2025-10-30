using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    public class ProductsController : Controller
    {
        // GET: ProductsController
        public IActionResult Index()
        {
//            return View();
            return Content("Products page is working!");
        }

        public string Browse(string song)
        {
//            string songDisplay = System.Net.WebUtility.HtmlEncode(song);
//            return $"Browsing products in song: {songDisplay}";
// vulnerable for XSS
            return $"Browsing products in song: {song}";
        }

        public string Details(int id)
        {
            return $"Showing details for product ID: {id}";
        }

    }
}
