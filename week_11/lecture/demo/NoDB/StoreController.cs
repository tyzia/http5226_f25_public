using Microsoft.AspNetCore.Mvc;
using MusicStore.Models;
using MusicStore.Models.ViewModels;

namespace MusicStore.Controllers
{
    public class StoreController : Controller
    {
        // GET: /Store/Browse?genre=Rock
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
            // Create sample artists
            List<Artist> artists = new List<Artist>
            {
                new Artist { ArtistId = 1, Name = "The Beatles" },
                new Artist { ArtistId = 2, Name = "Taylor Swift" },
                new Artist { ArtistId = 3, Name = "Drake" }
            };

            // Create sample genres
            List<Genre> genres = new List<Genre>
            {
                new Genre { GenreId = 1, Name = "Rock" },
                new Genre { GenreId = 2, Name = "Pop" },
                new Genre { GenreId = 3, Name = "Hip Hop" }
            };

            // Create sample albums with navigation properties
            List<Album> albums = new List<Album>
            {
                new Album { AlbumId = 1, Title = "Abbey Road", ArtistId = 1, GenreId = 1, Artist = artists[0], Genre = genres[0] },
                new Album { AlbumId = 2, Title = "Let It Be", ArtistId = 1, GenreId = 1, Artist = artists[0], Genre = genres[0] },
                new Album { AlbumId = 3, Title = "1989", ArtistId = 2, GenreId = 2, Artist = artists[1], Genre = genres[1] },
                new Album { AlbumId = 4, Title = "Red", ArtistId = 2, GenreId = 2, Artist = artists[1], Genre = genres[1] },
                new Album { AlbumId = 5, Title = "Scorpion", ArtistId = 3, GenreId = 3, Artist = artists[2], Genre = genres[2] },
                new Album { AlbumId = 6, Title = "Views", ArtistId = 3, GenreId = 3, Artist = artists[2], Genre = genres[2] }
            };

            return albums;
        }
    }
}