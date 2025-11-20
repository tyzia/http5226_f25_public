using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data;
using MusicStore.Models;
using MusicStore.ViewModels;

namespace MusicStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly MusicStoreContext _context;

        public StoreController(MusicStoreContext context)
        {
            _context = context;
        }

        public IActionResult Browse(string genre)
        {
            List<Album> allAlbums = GetSampleAlbums();

            if (string.IsNullOrEmpty(genre))
            {
                AlbumListViewModel viewModel = new AlbumListViewModel
                {
                    Albums = allAlbums,
                    PageTitle = "All Albums", // If genre filter is empty, we show all albums
                    SelectedGenre = "All"
                };
                return View(viewModel);
            }

            List<Album> filteredAlbums = allAlbums.Where(a => a.Genre.Name == genre).ToList();

            AlbumListViewModel filteredViewModel = new AlbumListViewModel
            {
                Albums = filteredAlbums,
                PageTitle = $"Genre: {genre}",
                SelectedGenre = genre
            };

            return View(filteredViewModel);
        }

        private List<Album> GetSampleAlbums()
        {
            // Now gets real data from database
            return _context.Albums
                    .Include(a => a.Artist)
                    .Include(a => a.Genre)
                    .ToList();
        }

        /*
        // Improved version

        public IActionResult Browse(string genre)
        {
            if (string.IsNullOrEmpty(genre))
            {
                // Get all albums
                List<Album> allAlbums = _context.Albums
                    .Include(a => a.Artist)
                    .Include(a => a.Genre)
                    .ToList();

                return View(new AlbumListViewModel
                {
                    Albums = allAlbums,
                    PageTitle = "All Albums",
                    SelectedGenre = "All"
                });
            }
            else
            {
                // Filter in DATABASE - only get what we need
                List<Album> filteredAlbums = _context.Albums
                    .Include(a => a.Artist)
                    .Include(a => a.Genre)
                    .Where(a => a.Genre.Name == genre)
                    .ToList();

                return View(new AlbumListViewModel
                {
                    Albums = filteredAlbums,
                    PageTitle = $"Genre: {genre}",
                    SelectedGenre = genre
                });
            }
        }
        */


        /*
        // A better improved version

        public IActionResult Browse(string genre)
        {
            var query = _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .AsQueryable();

            if (!string.IsNullOrEmpty(genre))
            {
                query = query.Where(a => a.Genre.Name == genre);
            }

            List<Album> albums = query.ToList(); // One call to DB

            AlbumListViewModel viewModel = new AlbumListViewModel
            {
                Albums = albums,
                PageTitle = string.IsNullOrEmpty(genre) ? "All Albums" : $"Genre: {genre}",
                SelectedGenre = genre ?? "All"
            };

            return View(viewModel);
        }
        */

        /*
        // Improved async version

        public async Task<IActionResult> Browse(string genre)
        {
            var query = _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .AsQueryable();

            if (!string.IsNullOrEmpty(genre))
            {
                query = query.Where(a => a.Genre.Name == genre);
            }

            List<Album> albums = await query.ToListAsync();

            AlbumListViewModel viewModel = new AlbumListViewModel
            {
                Albums = albums,
                PageTitle = string.IsNullOrEmpty(genre) ? "All Albums" : $"Genre: {genre}",
                SelectedGenre = genre ?? "All"
            };

            return View(viewModel);
        }
        */
    }
}