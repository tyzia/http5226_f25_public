using LecDemo.Models;
namespace LecDemo.Models.ViewModels;

public class AlbumListViewModel
{
    public IEnumerable<Album> Albums { get; set; } = new List<Album>();
    public string? PageTitle { get; set; }
    public string? CurrentUser { get; set; }
}
