using Microsoft.AspNetCore.Mvc;
using LecDemo.Models.ViewModels;
using LecDemo.Models;

namespace LecDemo.Controllers
{
    public class AlbumsController : Controller
    {
        // GET: AlbumsController
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            List<Album> albums = new List<Album>();
            for (int i = 0; i < 10; i++) {
                albums.Add(new Album { Title = "Album " + i });
            }

            AlbumListViewModel viewModel = new AlbumListViewModel
            {
                Albums = albums,
                PageTitle = "My Albums",
                CurrentUser = "admin"
            };

            return View(viewModel);
        }

    }
}
